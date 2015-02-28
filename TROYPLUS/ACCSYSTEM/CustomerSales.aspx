<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="CustomerSales.aspx.cs" Inherits="CustomerSales"
    Title="Sales > CustomerSales" %>

<%@ Register Assembly="RealWorld.Grids" Namespace="RealWorld.Grids" TagPrefix="rwg" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <%-- <script type="text/javascript">
        //function OnChangetxt() {
        //    debugger;
        //    alert("test");
        //}
        //var button1 = document.getElementById('Textbox4');  

        //button1.onchange = OnChangetxt;      
    </script>--%>

    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Selected Customer category is different from previous.Do you want to continue?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
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
            width: 98.5%;
        }
    </style>

    <asp:UpdatePanel ID="UpdatePnlMaster" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <%--<cc1:ToolkitScriptManager runat="server" />--%>
            <table style="width: 998px;" align="center">
                <tr style="width: 100%">
                    <td style="width: 100%" valign="middle">
                        <%--<div class="mainConHd">
                               <span>Sales</span>
                            </div>--%>
                        <%--<table class="mainConHd" style="width: 994px;">
                                <tr valign="middle">
                                    <td style="font-size: 20px;">
                                        Sales
                                    </td>
                                </tr>
                            </table>--%>
                        <div class="mainConBody">
                            <div>
                                <table cellspacing="0px" cellpadding="0px" border="0" width="100%"
                                    class="searchbg" style="/*margin: -3px 0px 0px 2px; */">
                                    <tr style="vertical-align: middle">
                                        <td style="width: 2%;"></td>
                                        <td style="width: 14%; font-size: 22px; color: White;">SALES
                                        </td>
                                        <td style="width: 16%"></td>
                                        <td style="width: 15%; color: White;" align="right">
                                            <%--Bill No.--%>
                                                Search
                                        </td>
                                        <td style="width: 18%; text-align: center" class="NewBox">
                                            <asp:TextBox ValidationGroup="search" ID="txtBillnoSrc" runat="server" CssClass="cssTextBox"
                                                Width="92%" MaxLength="8" Visible="False"></asp:TextBox>
                                            <asp:TextBox ID="txtSearch" runat="server" SkinID="skinTxtBoxSearch"></asp:TextBox>
                                        </td>
                                        <td style="width: 1%" align="center">
                                            <%--Trans. No.--%>
                                            <%--Search By--%>
                                        </td>
                                        <td style="width: 18%" class="NewBox">
                                            <div style="width: 150px; font-family: 'Trebuchet MS';">
                                                <asp:DropDownList ID="ddCriteria" runat="server" BackColor="White" Width="149px" Height="24px" Style="text-align: center; border: 1px solid White">
                                                    <asp:ListItem Value="0">All</asp:ListItem>
                                                    <asp:ListItem Value="BillNo">Bill No</asp:ListItem>
                                                    <asp:ListItem Value="TransNo">Trans No</asp:ListItem>
                                                    <asp:ListItem Value="BillDate">Bill Date</asp:ListItem>
                                                    <asp:ListItem Value="CustomerName">Customer Name</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <asp:TextBox ValidationGroup="search" ID="txtTransNo" runat="server" MaxLength="8"
                                                CssClass="cssTextBox" Visible="False"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtTransNo"
                                                FilterType="Numbers" />
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender221" runat="server" TargetControlID="txtBillnoSrc"
                                                FilterType="Numbers" />
                                        </td>
                                        <td style="width: 21%;">
                                            <div style="text-align: right;">
                                                <asp:Panel ID="Panel6" runat="server" Width="120px">
                                                    <asp:Button ID="btnSearch" OnClick="btnSearch_Click" Font-Bold="True" runat="server" CssClass="ButtonSearch6"
                                                        EnableTheming="false" ForeColor="White" Width="80px" />

                                                </asp:Panel>
                                            </div>
                                        </td>
                                        <td style="width: 16%" class="tblLeftNoPad">
                                            <asp:Button ID="BtnClearFilter1" runat="server" OnClick="BtnClearFilter_Click" EnableTheming="false" Text="" CssClass="ClearFilter6" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <%-- <div>
                            <asp:DropDownList ID="drpMobile2" runat="server" TabIndex="2" CssClass="chzn-select" Width="313px">
                            </asp:DropDownList>
                        </div>--%>
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
                        <asp:Panel ID="pnlMethod" runat="server" Style="width: 54%; display: none" CssClass="chzn-container">
                            <asp:UpdatePanel ID="updatePnlMethod" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Panel ID="pnlContactItems" CssClass="pnlPopUp" runat="server">
                                        <div id="Div2" class="divArea6">
                                            <table cellpadding="3" cellspacing="2" style="width: 100%" align="center">
                                                <tr style="width: 100%">
                                                    <td style="width: 100%">
                                                        <table style="text-align: left; width: 100%; border: 1px solid Blue;" cellpadding="3" cellspacing="2">
                                                            <tr>

                                                                <td>
                                                                    <table class="headerPopUp" width="100%">
                                                                        <tr>
                                                                            <td>Select the Sales Type
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>

                                                            </tr>

                                                            <tr>

                                                                <td>
                                                                    <table style="width: 100%;" cellpadding="3" cellspacing="1">
                                                                        <tr>
                                                                            <td style="width: 4%"></td>
                                                                            <td style="width: 92%;" class="ControlTextBox3">

                                                                                <asp:RadioButtonList ID="optionmethod" runat="server" Style="font-size: 12px;" align="center"
                                                                                    RepeatDirection="Horizontal" BackColor="#e7e7e7" Height="35px">
                                                                                    <asp:ListItem Selected="True" Value="NormalSales">Normal Sales&nbsp;&nbsp;</asp:ListItem>
                                                                                    <asp:ListItem Value="InternalTransfer">Internal Transfer&nbsp;&nbsp;</asp:ListItem>
                                                                                    <asp:ListItem Value="DeliveryNote">Delivery Note&nbsp;&nbsp;</asp:ListItem>
                                                                                    <asp:ListItem Value="PurchaseReturn">Purchase Return&nbsp;&nbsp;</asp:ListItem>
                                                                                    <asp:ListItem Value="ManualSales">Manual Sales&nbsp;&nbsp;</asp:ListItem>
                                                                                </asp:RadioButtonList>
                                                                            </td>
                                                                            <td style="width: 3%"></td>

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
                                                        <center>
                                                            <asp:ValidationSummary ID="valSum" DisplayMode="BulletList" ShowMessageBox="true"
                                                                ShowSummary="false" ValidationGroup="product" HeaderText="Validation Messages"
                                                                Font-Names="'Trebuchet MS'" Font-Size="12" runat="server" />
                                                            <input id="dummySales" type="button" style="display: none" runat="server" />
                                                            <input id="BtnPopUpCancel" type="button" style="display: none" runat="server" />
                                                            <cc1:ModalPopupExtender ID="ModalPopupSales" runat="server" BackgroundCssClass="modalBackground"
                                                                RepositionMode="RepositionOnWindowResizeAndScroll" CancelControlID="BtnPopUpCancel"
                                                                DynamicServicePath="" Enabled="True" PopupControlID="pnlSalesForm" TargetControlID="dummySales">
                                                            </cc1:ModalPopupExtender>

                                                            <asp:Panel ID="pnlSalesForm" runat="server" Style="width: 99%; display: none">
                                                                <asp:UpdatePanel ID="updatePnlSales" runat="server" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <div id="Div1" style="background-color: white; width: 95%">
                                                                            <table style="width: 100%;" align="center">
                                                                                <tr style="width: 100%">
                                                                                    <td style="width: 100%">
                                                                                        <table style="text-align: left;" width="100%" cellpadding="0" cellspacing="2">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <table class="headerPopUp" width="95%">
                                                                                                        <tr>
                                                                                                            <td style="width: 95%">
                                                                                                                 <asp:Label ID="lblHeading" Text="Sales Invoice Details" runat="server"></asp:Label>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <cc1:TabContainer ID="tabs2" runat="server" ActiveTabIndex="0" CssClass="fancy fancy-green" Width="1280px">
                                                                                                        <cc1:TabPanel ID="tabMaster" runat="server" HeaderText="Sales Details" Width="1260px">
                                                                                                            <HeaderTemplate>
                                                                                                                <div>
                                                                                                                    <table>
                                                                                                                        <tr>
                                                                                                                            <td><b>Invoice Header Details </b></td>
                                                                                                                        </tr>
                                                                                                                    </table>
                                                                                                                </div>
                                                                                                            </HeaderTemplate>
                                                                                                            <ContentTemplate>
                                                                                                                <table cellpadding="0" cellspacing="0" width="120%">
                                                                                                                    <tr>
                                                                                                                        <td>
                                                                                                                            <table cellpadding="0" cellspacing="0" width="1260px">
                                                                                                                                <tr>
                                                                                                                                    <td colspan="5"></td>
                                                                                                                                </tr>
                                                                                                                                <tr>
                                                                                                                                    <td style="width: 25%;" class="ControlLabelproject">
                                                                                                                                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="drpCustomerCategoryAdd" Display="Dynamic" ErrorMessage="Please Select Customer Category" Operator="GreaterThan" Text="*" ValidationGroup="salesval" ValueToCompare="0"></asp:CompareValidator>
                                                                                                                                        Purchase InvoiceNo *
                                                                                                                                    </td>
                                                                                                                                    <td class="ControlDrpBorder" style="width: 24%">
                                                                                                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                                                                                                            <ContentTemplate>
                                                                                                                                                <asp:DropDownList ID="drpPurID" runat="server" AutoPostBack="true" AppendDataBoundItems="true" BackColor="#e7e7e7" CssClass="drpDownListMedium" DataTextField="PurchaseID" DataValueField="PurchaseID" Height="26px" Style="border: 1px solid #e7e7e7" TabIndex="2" Width="100%" OnSelectedIndexChanged="drpPurID_SelectedIndexChanged">
                                                                                                                                                    <asp:ListItem style="background-color: #e7e7e7" Text="Select Invoice No" Value="0"></asp:ListItem>
                                                                                                                                                </asp:DropDownList>
                                                                                                                                            </ContentTemplate>
                                                                                                                                            <Triggers>
                                                                                                                                                <asp:AsyncPostBackTrigger ControlID="drpPurID" EventName="SelectedIndexChanged" />
                                                                                                                                                <asp:AsyncPostBackTrigger ControlID="cmbCustomer" EventName="SelectedIndexChanged" />
                                                                                                                                                <asp:AsyncPostBackTrigger ControlID="drpMobile" EventName="SelectedIndexChanged" />
                                                                                                                                            </Triggers>
                                                                                                                                        </asp:UpdatePanel>
                                                                                                                                    </td>
                                                                                                                                     <td style="width: 7%;"></td>
                                                                                                                                     <td style="width: 10%;"></td>
                                                                                                                                     <td style="width: 24%;"></td>
                                                                                                                                     <td style="width: 13%;"></td>
                                                                                                                                </tr>
                                                                                                                                 <tr style="height: 2px;">
                                                                                                                                </tr>
                                                                                                                                <tr>
                                                                                                                                    <td class="ControlLabelproject" style="width: 25%;">Bill No. </td>
                                                                                                                                    <td class="ControlTextBox3" style="width: 24%;">
                                                                                                                                        <asp:Label ID="lblBillNo" runat="server" BackColor="#e7e7e7" Height="25px" Width="200px"></asp:Label>
                                                                                                                                        <asp:DropDownList ID="ddSeriesType" runat="server" AppendDataBoundItems="True" BackColor="#e7e7e7" Height="25px" SkinID="skinDdlBox" TabIndex="7" Width="100%">
                                                                                                                                        </asp:DropDownList>
                                                                                                                                    </td>
                                                                                                                                    <td style="width: 10%;"></td>
                                                                                                                                    <td class="ControlLabelproject" style="width: 10%;">
                                                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtBillDate" CssClass="lblFont" Display="Dynamic" ErrorMessage="BillDate is mandatory" Text="*" ValidationGroup="salesval"></asp:RequiredFieldValidator>
                                                                                                                                        <asp:RangeValidator ID="mrBillDate" runat="server" ControlToValidate="txtBillDate" ErrorMessage="Bill date cannot be future date." Text="*" Type="Date" ValidationGroup="salesval"></asp:RangeValidator>
                                                                                                                                        Invoice Date * </td>
                                                                                                                                    <td class="ControlTextBox3" style="width: 24%;">
                                                                                                                                        <asp:TextBox ID="txtBillDate" Enabled="false" runat="server" AutoPostBack="True" BackColor="#e7e7e7" CssClass="cssTextBox" Height="23px" MaxLength="10" OnTextChanged="txtBillDate_TextChanged" TabIndex="1" ValidationGroup="salesval"></asp:TextBox>
                                                                                                                                        <cc1:CalendarExtender ID="calBillDate" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="btnBillDate" TargetControlID="txtBillDate">
                                                                                                                                        </cc1:CalendarExtender>
                                                                                                                                    </td>
                                                                                                                                    <td align="left" style="width: 10%;">
                                                                                                                                        <asp:ImageButton ID="btnBillDate" runat="server" CausesValidation="False" ImageUrl="App_Themes/NewTheme/images/cal.gif" Width="20px" />
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                                <tr style="height: 2px;">
                                                                                                                                </tr>
                                                                                                                                <tr>
                                                                                                                                    <td class="ControlLabelproject" style="width: 25%;">Mobile No(Without International Code)
                                                                                                                                    </td>
                                                                                                                                    <td class="ControlDrpBorder" style="width: 24%">
                                                                                                                                        <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                                                                                                                                            <ContentTemplate>
                                                                                                                                                <asp:DropDownList ID="drpMobile" runat="server" AppendDataBoundItems="true" AutoPostBack="true" BackColor="#e7e7e7" CssClass="drpDownListMedium" DataTextField="Mobile" DataValueField="LedgerID" Height="26px" OnSelectedIndexChanged="drpMobile_SelectedIndexChanged" Style="border: 1px solid #e7e7e7" TabIndex="2" ValidationGroup="salesval" Width="100%">
                                                                                                                                                    <asp:ListItem style="background-color: #e7e7e7" Text="Select Mobile" Value="0"></asp:ListItem>
                                                                                                                                                </asp:DropDownList>
                                                                                                                                                <asp:TextBox ID="txtCustomerId" runat="server" BackColor="#e7e7e7" MaxLength="200" SkinID="skinTxtBoxGrid" TabIndex="8"></asp:TextBox>
                                                                                                                                            </ContentTemplate>
                                                                                                                                            <Triggers>
                                                                                                                                                <asp:AsyncPostBackTrigger ControlID="tabs2$TabPanel1$drpPurchaseReturn" EventName="SelectedIndexChanged" />
                                                                                                                                                <asp:AsyncPostBackTrigger ControlID="chk" EventName="CheckedChanged" />
                                                                                                                                                <asp:AsyncPostBackTrigger ControlID="drpPurID" EventName="SelectedIndexChanged" />
                                                                                                                                                <asp:AsyncPostBackTrigger ControlID="cmbCustomer" EventName="SelectedIndexChanged" />
                                                                                                                                            </Triggers>
                                                                                                                                        </asp:UpdatePanel>
                                                                                                                                    </td>
                                                                                                                                    <td style="width: 15%;" align="left">
                                                                                                                                        <%--<asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
                                                                                                                                    <ContentTemplate>--%>
                                                                                                                                        <asp:CheckBox runat="server" ID="chk" Text="Existing Customer" OnCheckedChanged="chk_CheckedChanged" AutoPostBack="true" />
                                                                                                                                        <%--</ContentTemplate>
                                                                                                                                </asp:UpdatePanel>--%>
                                                                                                                                    </td>
                                                                                                                                    <td style="width: 10%;" class="ControlLabelproject">
                                                                                                                                        <asp:CompareValidator ID="cvCustomer" runat="server" ControlToValidate="drpCustomerCategoryAdd" Display="Dynamic" ErrorMessage="Please Select Customer Category" Operator="GreaterThan" Text="*" ValidationGroup="salesval" ValueToCompare="0"></asp:CompareValidator>
                                                                                                                                        Customer Category *
                                                                                                                                    </td>
                                                                                                                                    <td class="ControlDrpBorder" style="width: 24%">
                                                                                                                                        <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                                                                                                                                            <ContentTemplate>
                                                                                                                                                <asp:DropDownList ID="drpCustomerCategoryAdd" runat="server" AppendDataBoundItems="true" BackColor="#e7e7e7" CssClass="drpDownListMedium" DataTextField="CusCategory_Name" DataValueField="CusCategory_Value" Height="26px" Style="border: 1px solid #e7e7e7" TabIndex="2" ValidationGroup="salesval" Width="100%">
                                                                                                                                                    <asp:ListItem style="background-color: #e7e7e7" Text="Select Customer Category" Value="0"></asp:ListItem>
                                                                                                                                                </asp:DropDownList>
                                                                                                                                            </ContentTemplate>
                                                                                                                                            <Triggers>
                                                                                                                                                <asp:AsyncPostBackTrigger ControlID="drpPurID" EventName="SelectedIndexChanged" />
                                                                                                                                                <asp:AsyncPostBackTrigger ControlID="cmbCustomer" EventName="SelectedIndexChanged" />
                                                                                                                                            </Triggers>
                                                                                                                                        </asp:UpdatePanel>
                                                                                                                                    </td>
                                                                                                                                    <td align="left" style="width: 13%;"></td>
                                                                                                                                </tr>
                                                                                                                                <tr style="height: 2px;">
                                                                                                                                </tr>
                                                                                                                                <tr>
                                                                                                                                    <td class="ControlLabelproject" style="width: 25%;">Customer Name *
                                                                                                                                <%--<asp:CompareValidator ID="cvCustomer" runat="server" ControlToValidate="cmbCustomer" Display="Dynamic" ErrorMessage="Please Select Customer!!" Operator="GreaterThan" Text="*" ValidationGroup="salesval" ValueToCompare="0"></asp:CompareValidator>--%>
                                                                                                                                    </td>
                                                                                                                                    <td class="ControlDrpBorder" style="width: 24%;">
                                                                                                                                        <%--   <asp:TextBox ID="TextBox4" AutoPostBack="false" runat="server" Width="500px" onchange="OnChangetxt()"></asp:TextBox>--%>

                                                                                                                                        <asp:DropDownList Visible="false" ID="cmbCustomer" runat="server" AppendDataBoundItems="true" AutoPostBack="true" BackColor="#e7e7e7" CssClass="chzn-select" DataTextField="LedgerName" DataValueField="LedgerID" Height="26px" OnSelectedIndexChanged="cmbCustomer_SelectedIndexChanged" Style="border: 1px solid #e7e7e7" TabIndex="2" ValidationGroup="salesval" Width="313px">
                                                                                                                                            <asp:ListItem style="background-color: #e7e7e7" Text="Select Customer" Value="0"></asp:ListItem>
                                                                                                                                        </asp:DropDownList>
                                                                                                                                        <asp:UpdatePanel ID="UpdatePanel21" runat="server" UpdateMode="Conditional">
                                                                                                                                            <ContentTemplate>
                                                                                                                                                <asp:TextBox ID="TextBox4" AutoPostBack="true" runat="server" Width="99%" OnTextChanged="TextBox4_TextChanged" Visible="false"></asp:TextBox>

                                                                                                                                                <asp:TextBox ID="txtCustomerName" runat="server" SkinID="skinTxtBoxGrid" Visible="false"></asp:TextBox>


                                                                                                                                            </ContentTemplate>
                                                                                                                                            <Triggers>
                                                                                                                                                <asp:AsyncPostBackTrigger ControlID="tabs2$TabPanel1$drpPurchaseReturn" EventName="SelectedIndexChanged" />
                                                                                                                                                <asp:AsyncPostBackTrigger ControlID="chk" EventName="CheckedChanged" />
                                                                                                                                                <asp:AsyncPostBackTrigger ControlID="drpMobile" EventName="SelectedIndexChanged" />
                                                                                                                                                <asp:AsyncPostBackTrigger ControlID="TextBox4" EventName="TextChanged" />
                                                                                                                                            </Triggers>
                                                                                                                                        </asp:UpdatePanel>
                                                                                                                                        <asp:UpdatePanel ID="UpdatePanel20" runat="server" UpdateMode="Conditional">
                                                                                                                                            <ContentTemplate>
                                                                                                                                                <asp:TextBox ID="txtOtherCusName" runat="server" BackColor="#e7e7e7" Height="10px" SkinID="skinTxtBox" TabIndex="2" ValidationGroup="salesval" Visible="False"></asp:TextBox>
                                                                                                                                                <asp:Label ID="lblledgerCategory" runat="server" CssClass="lblFont" Style="color: royalblue; font-weight: normal; font-size: smaller"></asp:Label>
                                                                                                                                            </ContentTemplate>
                                                                                                                                            <Triggers>
                                                                                                                                                <asp:AsyncPostBackTrigger ControlID="drpPurID" EventName="SelectedIndexChanged" />
                                                                                                                                                <asp:AsyncPostBackTrigger ControlID="cmbCustomer" EventName="SelectedIndexChanged" />
                                                                                                                                            </Triggers>
                                                                                                                                        </asp:UpdatePanel>
                                                                                                                                    </td>
                                                                                                                                    <td style="width: 7%;"></td>
                                                                                                                                    <td class="ControlLabelproject" style="width: 10%;">Address1</td>
                                                                                                                                    <td class="ControlTextBox3" style="width: 24%;">
                                                                                                                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                                                                                                                            <ContentTemplate>
                                                                                                                                                <asp:TextBox ID="txtAddress" runat="server" BackColor="#e7e7e7" CssClass="cssTextBox" Height="26px" MaxLength="200" SkinID="skinTxtBox" TabIndex="3" Width="50%"></asp:TextBox>
                                                                                                                                            </ContentTemplate>
                                                                                                                                            <Triggers>
                                                                                                                                                <asp:AsyncPostBackTrigger ControlID="drpPurID" EventName="SelectedIndexChanged" />
                                                                                                                                                <asp:AsyncPostBackTrigger ControlID="cmbCustomer" EventName="SelectedIndexChanged" />
                                                                                                                                                <asp:AsyncPostBackTrigger ControlID="TextBox4" EventName="TextChanged" />
                                                                                                                                            </Triggers>
                                                                                                                                        </asp:UpdatePanel>
                                                                                                                                    </td>
                                                                                                                                    <td style="width: 13%;"></td>
                                                                                                                                </tr>
                                                                                                                                <tr style="height: 2px;">
                                                                                                                                </tr>
                                                                                                                                <tr>
                                                                                                                                    <td class="ControlLabelproject" style="width: 25%;">Mode of Payment </td>
                                                                                                                                    <td class="ControlDrpBorder" style="width: 24%;">
                                                                                                                                        <asp:UpdatePanel ID="UpdatePanelPayMode" runat="server" UpdateMode="Conditional">
                                                                                                                                            <ContentTemplate>
                                                                                                                                                <asp:DropDownList ID="drpPaymode" runat="server" AppendDataBoundItems="True" AutoPostBack="True" BackColor="#e7e7e7" CssClass="drpDownListMedium" Height="26px" OnSelectedIndexChanged="drpPaymode_SelectedIndexChanged" Style="text-align: center; border: 1px solid #e7e7e7" TabIndex="4" ValidationGroup="salesval" Width="100%">
                                                                                                                                                    <asp:ListItem Text="Cash" Value="1"></asp:ListItem>
                                                                                                                                                    <asp:ListItem Text="Bank / Credit Card" Value="2"></asp:ListItem>
                                                                                                                                                    <asp:ListItem Text="Credit" Value="3"></asp:ListItem>
                                                                                                                                                    <asp:ListItem Text="Multiple Payment" Value="4"></asp:ListItem>
                                                                                                                                                </asp:DropDownList>
                                                                                                                                            </ContentTemplate>
                                                                                                                                        </asp:UpdatePanel>
                                                                                                                                    </td>
                                                                                                                                    <td style="width: 7%;"></td>
                                                                                                                                    <td class="ControlLabelproject" style="width: 10%;">Address2</td>
                                                                                                                                    <td class="ControlTextBox3" style="width: 24%">
                                                                                                                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                                                                                                                            <ContentTemplate>
                                                                                                                                                <asp:TextBox ID="txtAddress2" runat="server" BackColor="#e7e7e7" CssClass="cssTextBox" Height="26px" MaxLength="200" SkinID="skinTxtBox" TabIndex="5" Width="500px"></asp:TextBox>
                                                                                                                                            </ContentTemplate>
                                                                                                                                            <Triggers>
                                                                                                                                                <asp:AsyncPostBackTrigger ControlID="drpPurID" EventName="SelectedIndexChanged" />
                                                                                                                                                <asp:AsyncPostBackTrigger ControlID="cmbCustomer" EventName="SelectedIndexChanged" />
                                                                                                                                            </Triggers>
                                                                                                                                        </asp:UpdatePanel>
                                                                                                                                    </td>
                                                                                                                                    <td style="width: 13%;"></td>
                                                                                                                                </tr>
                                                                                                                                <tr style="height: 2px;">
                                                                                                                                </tr>
                                                                                                                                <tr>
                                                                                                                                    <td class="ControlLabelproject" style="width: 25%;" valign="middle">Mobile </td>
                                                                                                                                    <td class="ControlTextBox3" style="width: 24%;">
                                                                                                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                                                                                                            <ContentTemplate>
                                                                                                                                                <asp:TextBox ID="txtCustPh" runat="server" BackColor="#e7e7e7" MaxLength="10" SkinID="skinTxtBoxGrid" TabIndex="6" Width="200px"></asp:TextBox>
                                                                                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxEx" runat="server" FilterType="Numbers" TargetControlID="txtCustPh" />
                                                                                                                                            </ContentTemplate>
                                                                                                                                            <Triggers>
                                                                                                                                                <asp:AsyncPostBackTrigger ControlID="drpPurID" EventName="SelectedIndexChanged" />
                                                                                                                                                <asp:AsyncPostBackTrigger ControlID="cmbCustomer" EventName="SelectedIndexChanged" />

                                                                                                                                            </Triggers>
                                                                                                                                        </asp:UpdatePanel>
                                                                                                                                    </td>
                                                                                                                                    <td style="width: 7%;"></td>
                                                                                                                                    <td style="width: 10%;" class="ControlLabelproject">Address3
                                                                                                                                    </td>
                                                                                                                                    <td class="ControlTextBox3" style="width: 24%">
                                                                                                                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                                                                                                                            <ContentTemplate>
                                                                                                                                                <asp:TextBox ID="txtAddress3" runat="server" BackColor="#e7e7e7" CssClass="cssTextBox" MaxLength="200" SkinID="skinTxtBox" Width="500px"></asp:TextBox>
                                                                                                                                            </ContentTemplate>
                                                                                                                                            <Triggers>
                                                                                                                                                <asp:AsyncPostBackTrigger ControlID="drpPurID" EventName="SelectedIndexChanged" />
                                                                                                                                                <asp:AsyncPostBackTrigger ControlID="cmbCustomer" EventName="SelectedIndexChanged" />
                                                                                                                                            </Triggers>
                                                                                                                                        </asp:UpdatePanel>
                                                                                                                                    </td>
                                                                                                                                    <td style="width: 13%;"></td>
                                                                                                                                </tr>
                                                                                                                                <tr style="height: 2px;">
                                                                                                                                </tr>
                                                                                                                                <tr>
                                                                                                                                    <td class="ControlLabelproject" style="width: 25%;" valign="middle">
                                                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtdespatced" CssClass="lblFont" Display="Dynamic" ErrorMessage="Despatched From is mandatory" Text="*" ValidationGroup="salesval"></asp:RequiredFieldValidator>
                                                                                                                                        To be Despatched From * </td>
                                                                                                                                    <td class="ControlTextBox3" style="width: 24%;">
                                                                                                                                        <asp:TextBox ID="txtdespatced" runat="server" BackColor="#e7e7e7" MaxLength="10" SkinID="skinTxtBoxGrid" TabIndex="8" Width="200px"></asp:TextBox>
                                                                                                                                    </td>
                                                                                                                                    <td style="width: 7%;"></td>
                                                                                                                                      <td class="ControlLabelproject" style="width: 10%;" valign="middle">
                                                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtfixedtotal" CssClass="lblFont" Display="Dynamic" ErrorMessage="Fixed Total is mandatory" Text="*" ValidationGroup="salesval"></asp:RequiredFieldValidator>
                                                                                                                                        Rounded off Total *
                                                                                                                            <td class="ControlTextBox3" style="width: 24%;">
                                                                                                                                <asp:TextBox ID="txtfixedtotal" runat="server" BackColor="#e7e7e7" CssClass="cssTextBox" MaxLength="200" SkinID="skinTxtBox" TabIndex="9" Width="500px"></asp:TextBox>
                                                                                                                            </td>
                                                                                                                                    <td style="width: 13%;"></td>
                                                                                                                                </tr>
                                                                                                                                <tr style="height: 2px;">
                                                                                                                                </tr>
                                                                                                                                <tr>                         
                                                                                                                                     <td style="width: 25%;">
                                                                                                                                          <td style="width: 24%;">                                                                                                         
                                                                                                                                        <td style="width: 7%;">
                                                                                                                                            <td class="ControlLabel" style="width: 10%;"></td>
                                                                                                                                            <td style="width: 24%">
                                                                                                                                                <div>
                                                                                                                                                    <asp:DropDownList ID="drpMobile1" runat="server" TabIndex="2" CssClass="chzn-select" Width="313px" Visible="false">
                                                                                                                                                        <%-- <asp:DropDownList ID="drpMobile1" runat="server" TabIndex="2" CssClass="chzn-select" Width="313px">--%>
                                                                                                                                                    </asp:DropDownList>
                                                                                                                                                </div>
                                                                                                                                            </td>

                                                                                                                                        </td>
                                                                                                                                               <td style="width: 10%;">
                                                                                                                                                    <td style="width: 24%;">
                                                                                                                                        <td align="left" style="width: 13%;"></td>
                                                                                                                                </tr>
                                                                                                                                <%--<tr style="height: 2px;">
                                                                                                                            
                                                                                                                        </tr>--%>
                                                                                                                                <tr>
                                                                                                                                    <td colspan="6">
                                                                                                                                        <asp:UpdatePanel ID="bankPanel" runat="server" UpdateMode="Conditional">
                                                                                                                                            <ContentTemplate>
                                                                                                                                                <asp:Panel ID="pnlBank" runat="Server" Visible="false">
                                                                                                                                                    <table cellpadding="2" cellspacing="2" width="100%">
                                                                                                                                                        <tr style="height: 2px;">
                                                                                                                                                        </tr>
                                                                                                                                                        <tr>
                                                                                                                                                            <td class="ControlLabelproject" style="width: 25%;" valign="middle"><%--<asp:Label ID="Label98" runat="server" Text="Cheque / Credit Card No.*" Width="120px"                                                                                                                    ></asp:Label>--%>
                                                                                                                                                                <asp:RequiredFieldValidator ID="rvCredit" runat="server" ControlToValidate="txtCreditCardNo" Enabled="false" ErrorMessage="Cheque\Card number is mandatory" Text="*" ValidationGroup="salesval" />
                                                                                                                                                                Cheque / Credit Card No.* </td>
                                                                                                                                                            <td class="ControlTextBox3" style="width: 24%;">
                                                                                                                                                                <asp:TextBox ID="txtCreditCardNo" runat="server" CssClass="cssTextBox" Height="20px" TabIndex="10" ValidationGroup="salesval" Width="500px"></asp:TextBox>
                                                                                                                                                            </td>
                                                                                                                                                            <td style="width: 7%;"></td>
                                                                                                                                                            <td class="ControlLabelproject" style="width: 10%;"><%--Bank Name*--%><%--<asp:Label ID="Label888" runat="server" Text="Bank Name*" Width="120px"
                                                                                                                    ></asp:Label>--%>
                                                                                                                                                                <asp:RequiredFieldValidator ID="rvbank" runat="server" ControlToValidate="drpBankName" Enabled="false" ErrorMessage="Bankname is mandatory" InitialValue="0" Text="*" ValidationGroup="salesval" />
                                                                                                                                                                Bank Name* </td>

                                                                                                                                                            <td class="ControlDrpBorder" style="width: 24%;">
                                                                                                                                                                <asp:DropDownList ID="drpBankName" runat="server" AppendDataBoundItems="true" BackColor="#e7e7e7" CssClass="drpDownListMedium" DataTextField="LedgerName" DataValueField="LedgerID" Height="26px" Style="border: 1px solid #e7e7e7" TabIndex="11" ValidationGroup="salesval" Width="313px">
                                                                                                                                                                    <asp:ListItem style="background-color: #e7e7e7" Text="Select Bank" Value="0"></asp:ListItem>
                                                                                                                                                                </asp:DropDownList>
                                                                                                                                                            </td>
                                                                                                                                                            <td style="width: 13%;"></td>
                                                                                                                                                        </tr>
                                                                                                                                                    </table>
                                                                                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" FilterType="Numbers" TargetControlID="txtCreditCardNo" />
                                                                                                                                                </asp:Panel>
                                                                                                                                            </ContentTemplate>
                                                                                                                                            <Triggers>
                                                                                                                                                <asp:AsyncPostBackTrigger ControlID="drpPaymode" EventName="SelectedIndexChanged" />
                                                                                                                                            </Triggers>
                                                                                                                                        </asp:UpdatePanel>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                                <tr style="height: 2px;">
                                                                                                                                </tr>
                                                                                                                                <%--<tr>
                                                                                                                            <td class="ControlLabel" style="width: 25%;" valign="middle">
                                                                                                                                
                                                                                                                            <td class="ControlDrpBorder" style="width: 24%;">
                                                                                                                                
                                                                                                                            </td>
                                                                                                                            <td style="width: 7%;"></td>
                                                                                                                            <td class="ControlLabel" style="width: 10%;">
                                                                                                                                 </td>
                                                                                                                            </td>
                                                                                                                            <td class="ControlTextBox3" style="width: 24%">
                                                                                                                                
                                                                                                                            </td>
                                                                                                                            <td style="width: 13%;"></td>
                                                                                                                        </tr>--%>
                                                                                                                                <tr>
                                                                                                                                    <td colspan="4">
                                                                                                                                        <asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="Conditional">
                                                                                                                                            <ContentTemplate>
                                                                                                                                                <asp:HiddenField ID="hdnDisplay" runat="server" Value="N" />
                                                                                                                                                <asp:HiddenField ID="hdStock" runat="server" Value="0" />
                                                                                                                                                <asp:HiddenField ID="hdTotalAmt" runat="server" Value="0" />
                                                                                                                                                <asp:HiddenField ID="hdsales" runat="server" Value="0" />
                                                                                                                                                <asp:HiddenField ID="hdSeries" runat="server" Value="0" />
                                                                                                                                                <asp:HiddenField ID="hdContact" runat="server" />
                                                                                                                                                <asp:HiddenField ID="hdCreditSMS" runat="server" Value="NO" />
                                                                                                                                                <asp:HiddenField ID="hdCustCreditLimit" runat="server" Value="0" />
                                                                                                                                                <asp:HiddenField ID="hdAllowSales" runat="server" Value="NO" />
                                                                                                                                                <asp:HiddenField ID="hdPrevSalesTotal" runat="server" Value="0" />
                                                                                                                                                <asp:HiddenField ID="hdBalance" runat="server" Value="0" />
                                                                                                                                                <asp:HiddenField ID="hdPrevMode" runat="server" Value="0" />
                                                                                                                                                <asp:HiddenField ID="hdCREDITEXD" runat="server" Value="NO" />
                                                                                                                                                <asp:HiddenField ID="hdDaysLimit" runat="server" Value="NO" />
                                                                                                                                                <asp:HiddenField ID="hdCurrentRow" runat="server" Value="0" />
                                                                                                                                                <asp:HiddenField ID="hdCurrRole" runat="server" />
                                                                                                                                                <asp:HiddenField ID="hdOpr" runat="server" />
                                                                                                                                                <asp:HiddenField ID="hdEditQty" runat="server" Value="0" />
                                                                                                                                            </ContentTemplate>
                                                                                                                                            <Triggers>
                                                                                                                                                <asp:AsyncPostBackTrigger ControlID="drpPurID" EventName="SelectedIndexChanged" />
                                                                                                                                                <asp:AsyncPostBackTrigger ControlID="cmbCustomer" EventName="SelectedIndexChanged" />
                                                                                                                                                <asp:AsyncPostBackTrigger ControlID="cmdSave" EventName="Click" />
                                                                                                                                                <asp:AsyncPostBackTrigger ControlID="cmdCancel" EventName="Click" />
                                                                                                                                                <asp:AsyncPostBackTrigger ControlID="CmdCat" EventName="Click" />
                                                                                                                                            </Triggers>
                                                                                                                                        </asp:UpdatePanel>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                            </table>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td colspan="4">
                                                                                                                            <asp:Panel ID="errPanel" runat="server" Visible="False">
                                                                                                                                <table cellpadding="3" cellspacing="3" class="tblLeft" width="100%">
                                                                                                                                    <tr>
                                                                                                                                        <td class="SalesHeader" colspan="2">Exception !!! </td>
                                                                                                                                    </tr>
                                                                                                                                    <tr>
                                                                                                                                        <td width="15%">Error Message: </td>
                                                                                                                                        <td>
                                                                                                                                            <asp:Label ID="ErrMsg" runat="server" CssClass="errorMsg"></asp:Label>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                    <tr>
                                                                                                                                        <td colspan="2">PLEASE TAKE THE SCREENSHOT AND SEND IT TO ADMINISTRATOR FOR INVESTIGATION </td>
                                                                                                                                    </tr>
                                                                                                                                </table>
                                                                                                                            </asp:Panel>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </ContentTemplate>


                                                                                                        </cc1:TabPanel>
                                                                                                        <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="Product Details">
                                                                                                            <HeaderTemplate>
                                                                                                                <div>
                                                                                                                    <table>
                                                                                                                        <tr>
                                                                                                                            <td><b>Product Sold</b> </td>
                                                                                                                        </tr>
                                                                                                                    </table>
                                                                                                                </div>
                                                                                                            </HeaderTemplate>
                                                                                                            <ContentTemplate>
                                                                                                                <table cellpadding="3" cellspacing="1" class="tblLeft" width="600px">
                                                                                                                    <tr style="height: 5px">
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td align="center" colspan="4">
                                                                                                                            <asp:UpdatePanel ID="UpdatePanelSalesItems" runat="server" UpdateMode="Conditional">
                                                                                                                                <ContentTemplate>
                                                                                                                                    <asp:Label ID="Labelll" runat="server" Font-Size="20px" Font-Bold="true" Text="" Width="250px"></asp:Label>
                                                                                                                                    <asp:Panel ID="pnlProduct" runat="server">
                                                                                                                                        <table cellpadding="0" cellspacing="1" style="border: 0px Solid White; min-height: 50px" width="96%">
                                                                                                                                            <tr>
                                                                                                                                                <td align="left" colspan="4">
                                                                                                                                                    <cc1:ModalPopupExtender ID="ModalPopupProduct" runat="server" BackgroundCssClass="modalBackground" CancelControlID="CancelPopUp" DynamicServicePath="" Enabled="True" PopupControlID="pnlPopup" TargetControlID="ShowPopUp">
                                                                                                                                                    </cc1:ModalPopupExtender>
                                                                                                                                                    <input id="ShowPopUp" type="button" style="display: none" runat="server" />
                                                                                                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;<input id="CancelPopUp" runat="server" style="display: none"
                                                                                                                                                        type="button" />
                                                                                                                                                    </input>
                                                                                                                    </input>
                                                                                                                    &nbsp;<asp:ValidationSummary ID="VS" runat="server" Font-Names="'Trebuchet MS'" Font-Size="12pt" HeaderText="Validation Messages" ShowMessageBox="True" ShowSummary="False" ValidationGroup="salesval" />
                                                                                                                                                    <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Width="700px">
                                                                                                                                                        <asp:UpdatePanel ID="updatePnlProduct" runat="server" UpdateMode="Conditional">
                                                                                                                                                            <ContentTemplate>
                                                                                                                                                                <asp:Panel ID="pnlItems" runat="server" CssClass="pnlPopUp" Visible="false">
                                                                                                                                                                    <div id="contentPopUp">
                                                                                                                                                                        <table cellpadding="3" cellspacing="1" class="tblLeft" style="border: 1px Solid blue;" width="85%">
                                                                                                                                                                            <tr>
                                                                                                                                                                                <td style="width: 2px;"></td>
                                                                                                                                                                                <td>
                                                                                                                                                                                    <table class="headerPopUp" width="100%">
                                                                                                                                                                                        <tr>
                                                                                                                                                                                            <td>Products Details </td>
                                                                                                                                                                                        </tr>
                                                                                                                                                                                    </table>
                                                                                                                                                                                </td>
                                                                                                                                                                                <td></td>
                                                                                                                                                                            </tr>
                                                                                                                                                                            <tr>
                                                                                                                                                                                <td style="width: 2px;"></td>
                                                                                                                                                                                <td>
                                                                                                                                                                                    <table align="center" cellpadding="2" cellspacing="1" width="100%">
                                                                                                                                                                                        <tr id="rowBarcode" runat="server">
                                                                                                                                                                                            <td class="ControlLabel" style="width: 17%;">Barcode
                                                                                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtBarcode" CssC0lass="lblFont" Text="BarCode is mandatory" ValidationGroup="lookUp" />
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td class="ControlTextBox3" style="width: 28%;">
                                                                                                                                                                                                <asp:TextBox ID="txtBarcode" runat="server" CssClass="cssTextBox" SkinID="skinTxtBox" Width="80px" />
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td colspan="2" style="width: 16.7%;">
                                                                                                                                                                                                <asp:Button ID="cmdBarcode" runat="server" SkinID="skinBtnMedium" Text="Lookup Product" ValidationGroup="lookUp" />
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td style="width: 28.4%"></td>
                                                                                                                                                                                            <td style="width: 17.6%"></td>
                                                                                                                                                                                        </tr>
                                                                                                                                                                                        <tr>
                                                                                                                                                                                            <td class="ControlLabel" style="width: 17%;">Category *
                                                                                                                                                                <asp:CompareValidator ID="cvCatergory" runat="server" ControlToValidate="cmbCategory" Display="Dynamic" ErrorMessage="Category is Mandatory" Operator="GreaterThan" Text="*" ValidationGroup="product" ValueToCompare="0"></asp:CompareValidator>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td class="ControlDrpBorder" style="width: 28%;">
                                                                                                                                                                                                <asp:DropDownList ID="cmbCategory" runat="server" AppendDataBoundItems="True" AutoPostBack="true" BackColor="#e7e7e7" CssClass="drpDownListMedium" Height="26px" OnSelectedIndexChanged="LoadProducts" Style="border: 1px solid #e7e7e7" Width="100%">
                                                                                                                                                                                                    <asp:ListItem Selected="True" style="background-color: #e7e7e7" Value="0">Select Category</asp:ListItem>
                                                                                                                                                                                                </asp:DropDownList>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td class="ControlLabel" style="width: 16.7%;">Product Code </td>
                                                                                                                                                                                            <td class="ControlDrpBorder" style="width: 28.4%">
                                                                                                                                                                                                <asp:DropDownList ID="cmbProdAdd" runat="server" AppendDataBoundItems="true" AutoPostBack="true" BackColor="#e7e7e7" CssClass="drpDownListMedium" DataTextField="ProductName" DataValueField="ItemCode" Height="26px" OnSelectedIndexChanged="LoadForProduct" Style="border: 1px solid #e7e7e7" ValidationGroup="product" Width="100%">
                                                                                                                                                                                                    <asp:ListItem style="background-color: #90c9fc;" Text="Select Product" Value="0"></asp:ListItem>
                                                                                                                                                                                                </asp:DropDownList>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td style="width: 17.6%"></td>
                                                                                                                                                                                        </tr>
                                                                                                                                                                                        <tr style="height: 1px;">
                                                                                                                                                                                        </tr>
                                                                                                                                                                                        <tr>
                                                                                                                                                                                            <td class="ControlLabel" style="width: 17%;">Product Name </td>
                                                                                                                                                                                            <td class="ControlDrpBorder" style="width: 27%;">
                                                                                                                                                                                                <asp:DropDownList ID="cmbProdName" runat="server" AppendDataBoundItems="True" AutoPostBack="true" BackColor="#e7e7e7" CssClass="drpDownListMedium" Height="26px" OnSelectedIndexChanged="LoadForProductName" Style="border: 1px solid #e7e7e7" Width="100%">
                                                                                                                                                                                                    <asp:ListItem Selected="True" style="background-color: #bce1fe" Value="0">Select Product</asp:ListItem>
                                                                                                                                                                                                </asp:DropDownList>
                                                                                                                                                                                                <asp:TextBox ID="lblProdDescAdd" runat="server" CssClass="cssTextBox" ReadOnly="true" Visible="false" Width="196px"></asp:TextBox>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td class="ControlLabel" style="width: 16.7%;">Brand </td>
                                                                                                                                                                                            <td class="ControlDrpBorder" style="width: 28.4%">
                                                                                                                                                                                                <asp:DropDownList ID="cmbBrand" runat="server" AppendDataBoundItems="True" AutoPostBack="true" BackColor="#e7e7e7" CssClass="drpDownListMedium" Height="26px" OnSelectedIndexChanged="LoadForBrand" Style="border: 1px solid #e7e7e7" Width="100%">
                                                                                                                                                                                                    <asp:ListItem Selected="True" style="background-color: #90c9fc" Value="0">Select Category</asp:ListItem>
                                                                                                                                                                                                </asp:DropDownList>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td style="width: 17.6%"></td>
                                                                                                                                                                                        </tr>
                                                                                                                                                                                        <tr style="height: 1px;">
                                                                                                                                                                                        </tr>
                                                                                                                                                                                        <tr>
                                                                                                                                                                                            <td class="ControlLabel" style="width: 17%;">Model </td>
                                                                                                                                                                                            <td class="ControlDrpBorder" style="width: 27%;">
                                                                                                                                                                                                <asp:DropDownList ID="cmbModel" runat="server" AppendDataBoundItems="True" AutoPostBack="true" BackColor="#e7e7e7" CssClass="drpDownListMedium" Height="26px" OnSelectedIndexChanged="LoadForModel" Style="border: 1px solid #e7e7e7" Width="100%">
                                                                                                                                                                                                    <asp:ListItem Selected="True" style="background-color: #e7e7e7" Value="0">Select Category</asp:ListItem>
                                                                                                                                                                                                </asp:DropDownList>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td class="ControlLabel" style="width: 16.7%;">Stock </td>
                                                                                                                                                                                            <td class="ControlTextBox3" style="width: 28.4%">
                                                                                                                                                                                                <asp:TextBox ID="txtstock" runat="server" BackColor="#e7e7e7" CssClass="cssTextBox" Enabled="False" Height="23px" ValidationGroup="product" Width="70px"></asp:TextBox>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td style="width: 17.6%"></td>
                                                                                                                                                                                        </tr>
                                                                                                                                                                                        <tr style="height: 4px">
                                                                                                                                                                                        </tr>
                                                                                                                                                                                    </table>
                                                                                                                                                                                </td>
                                                                                                                                                                                <td></td>
                                                                                                                                                                            </tr>
                                                                                                                                                                            <tr style="height: 1px;">
                                                                                                                                                                            </tr>
                                                                                                                                                                            <tr id="rowTotal" runat="server">
                                                                                                                                                                                <td style="width: 2px;"></td>
                                                                                                                                                                                <td>
                                                                                                                                                                                    <table cellpadding="2" cellspacing="1" style="width: 100%;">
                                                                                                                                                                                        <tr>
                                                                                                                                                                                            <td class="ControlLabel" style="width: 17%">Total MRP </td>
                                                                                                                                                                                            <td class="ControlTextBox3" style="width: 28%">
                                                                                                                                                                                                <asp:TextBox ID="txttotal" runat="server" AutoPostBack="True" BackColor="#e7e7e7" CssClass="cssTextBox" Height="23px" ValidationGroup="product" Width="70px"></asp:TextBox>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td class="ControlLabel" style="width: 16.7%">Sub Total </td>
                                                                                                                                                                                            <td class="ControlTextBox3" style="width: 28.4%">
                                                                                                                                                                                                <asp:TextBox ID="txtsubtot" runat="server" AutoPostBack="True" BackColor="#e7e7e7" CssClass="cssTextBox" Height="23px" ValidationGroup="product" Width="70px"></asp:TextBox>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td style="width: 17.6%;"></td>
                                                                                                                                                                                        </tr>
                                                                                                                                                                                        <tr id="rowTotal1" runat="server">
                                                                                                                                                                                            <td class="ControlLabel" style="width: 17%">Vat Amount </td>
                                                                                                                                                                                            <td class="ControlTextBox3" style="width: 28%">
                                                                                                                                                                                                <asp:TextBox ID="TextBox1" runat="server" AutoPostBack="True" BackColor="#e7e7e7" CssClass="cssTextBox" Height="23px" ValidationGroup="product" Width="70px"></asp:TextBox>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td class="ControlLabel" style="width: 16.7%"></td>
                                                                                                                                                                                            <td align="right" style="width: 28.4%">
                                                                                                                                                                                                <asp:Button ID="BtnGet" runat="server" CssClass="LoadData" EnableTheming="false" OnClick="BtnGet_Click" Text="" />
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td style="width: 17.6%;"></td>
                                                                                                                                                                                        </tr>
                                                                                                                                                                                        <tr style="height: 3px">
                                                                                                                                                                                        </tr>
                                                                                                                                                                                    </table>
                                                                                                                                                                                </td>
                                                                                                                                                                            </tr>
                                                                                                                                                                            <tr>
                                                                                                                                                                                <td style="width: 2px;"></td>
                                                                                                                                                                                <td>
                                                                                                                                                                                    <table cellpadding="2" cellspacing="1" style="width: 100%;">
                                                                                                                                                                                        <tr>
                                                                                                                                                                                            <td class="ControlLabel" style="width: 17%">
                                                                                                                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtRateAdd" ErrorMessage="Product Rate is mandatory" Text="*" ValidationGroup="product" />
                                                                                                                                                                                                Rate </td>
                                                                                                                                                                                            <td class="ControlTextBox3" style="width: 28%">
                                                                                                                                                                                                <asp:TextBox ID="txtRateAdd" runat="server" BackColor="#e7e7e7" CssClass="cssTextBox" Height="23px" ValidationGroup="product" Width="70px"></asp:TextBox>
                                                                                                                                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender41" runat="server" FilterType="Custom, Numbers" TargetControlID="txtRateAdd" ValidChars="." />
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td class="ControlLabel" style="width: 16.7%">Exec Charge </td>
                                                                                                                                                                                            <td class="ControlTextBox3" style="width: 28.4%">
                                                                                                                                                                                                <asp:TextBox ID="txtExecCharge" runat="server" BackColor="#e7e7e7" CssClass="cssTextBox" Height="23px" Text="0" ValidationGroup="product" Width="70px"></asp:TextBox>
                                                                                                                                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" FilterType="Custom, Numbers" TargetControlID="txtExecCharge" ValidChars="." />
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td style="width: 17.6%;"></td>
                                                                                                                                                                                        </tr>
                                                                                                                                                                                        <tr style="height: 1px">
                                                                                                                                                                                        </tr>
                                                                                                                                                                                        <tr>
                                                                                                                                                                                            <td class="ControlLabel" style="width: 17%"><%--<asp:Label ID="Labe" runat="server" Width="120px" Text=""></asp:Label>--%>
                                                                                                                                                                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtQtyAdd" Display="Dynamic" ErrorMessage="Product Qty. must be greater than Zero" Operator="GreaterThan" Text="*" ValidationGroup="product" ValueToCompare="0"></asp:CompareValidator>
                                                                                                                                                                                                <asp:RequiredFieldValidator ID="rvQty" runat="server" ControlToValidate="txtQtyAdd" ErrorMessage="Qty. is mandatory" Text="*" ValidationGroup="product" />
                                                                                                                                                                                                Qty. * </td>
                                                                                                                                                                                            <td class="ControlTextBox3" style="width: 28%">
                                                                                                                                                                                                <asp:TextBox ID="txtQtyAdd" runat="server" BackColor="#e7e7e7" CssClass="cssTextBox" Height="23px" Text="0" ValidationGroup="product" Width="70px"></asp:TextBox>
                                                                                                                                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom, Numbers" TargetControlID="txtQtyAdd" ValidChars="." />
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td class="ControlLabel" style="width: 16.7%">
                                                                                                                                                                                                <asp:Label ID="lblDiscType" runat="server"></asp:Label>
                                                                                                                                                                                                <asp:RangeValidator ID="cvDisc" runat="server" ControlToValidate="lblDisAdd" Display="Dynamic" ErrorMessage="Discount cannot be Greater than 100% and Less than 0%" MaximumValue="100" MinimumValue="0" Text="*" Type="Double" ValidationGroup="product"></asp:RangeValidator>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td class="ControlTextBox3" style="width: 28.4%">
                                                                                                                                                                                                <asp:TextBox ID="lblDisAdd" runat="server" BackColor="#e7e7e7" CssClass="cssTextBox" Height="23px" Text="0" ValidationGroup="product" Width="70px"></asp:TextBox>
                                                                                                                                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Custom, Numbers" TargetControlID="lblDisAdd" ValidChars="." />
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td style="width: 17.6%;"></td>
                                                                                                                                                                                        </tr>
                                                                                                                                                                                        <tr style="height: 1px">
                                                                                                                                                                                        </tr>
                                                                                                                                                                                        <tr>
                                                                                                                                                                                            <td class="ControlLabel" style="width: 17%"><%--<asp:Label ID="Label3" runat="server" Width="120px" Text=""></asp:Label>--%>
                                                                                                                                                                                                <asp:RangeValidator ID="cvVAT" runat="server" ControlToValidate="lblVATAdd" Display="Dynamic" ErrorMessage="VAT cannot be Greater than 100% and Less than 0%" MaximumValue="100" MinimumValue="0" Text="*" Type="Double" ValidationGroup="product"></asp:RangeValidator>
                                                                                                                                                                                                VAT (%) </td>
                                                                                                                                                                                            <td class="ControlTextBox3" style="width: 28%">
                                                                                                                                                                                                <asp:TextBox ID="lblVATAdd" runat="server" BackColor="#e7e7e7" CssClass="cssTextBox" Height="23px" Text="0" ValidationGroup="product" Width="70px"></asp:TextBox>
                                                                                                                                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Custom, Numbers" TargetControlID="lblVATAdd" ValidChars="." />
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td class="ControlLabel" style="width: 16.7%">CST (%)
                                                                                                                                                                                <asp:RangeValidator ID="cvCST" runat="server" ControlToValidate="lblCSTAdd" Display="Dynamic" ErrorMessage="CST cannot be Greater than 100% and Less than 0%" MaximumValue="100" MinimumValue="0" Text="*" Type="Double" ValidationGroup="product"></asp:RangeValidator>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td class="ControlTextBox3" style="width: 28.4%">
                                                                                                                                                                                                <asp:TextBox ID="lblCSTAdd" runat="server" BackColor="#e7e7e7" CssClass="cssTextBox" Height="23px" Text="0" ValidationGroup="product" Width="70px"></asp:TextBox>
                                                                                                                                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Custom, Numbers" TargetControlID="lblCSTAdd" ValidChars="." />
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td style="width: 17.6%;"></td>
                                                                                                                                                                                        </tr>
                                                                                                                                                                                        <tr style="height: 4px">
                                                                                                                                                                                        </tr>
                                                                                                                                                                                    </table>
                                                                                                                                                                                </td>
                                                                                                                                                                                <td></td>
                                                                                                                                                                            </tr>
                                                                                                                                                                            <tr>
                                                                                                                                                                                <td style="width: 2px;"></td>
                                                                                                                                                                                <td>
                                                                                                                                                                                    <table cellpadding="1" cellspacing="2" class="tblLeft" width="100%">
                                                                                                                                                                                        <tr>
                                                                                                                                                                                            <td align="center">
                                                                                                                                                                                                <table>
                                                                                                                                                                                                    <tr>
                                                                                                                                                                                                        <td>
                                                                                                                                                                                                            <asp:Label ID="Labelll6" runat="server" Font-Size="12px" Text="" Width="120px"></asp:Label>
                                                                                                                                                                                                        </td>
                                                                                                                                                                                                        <td>
                                                                                                                                                                                                            <asp:Panel ID="Panel2" runat="server" Height="32px" Width="120px">
                                                                                                                                                                                                                <asp:Button ID="cmdCancelProduct" runat="server" CssClass="CloseWindow6" EnableTheming="false" Height="45px" OnClick="cmdCancelProduct_Click" />
                                                                                                                                                                                                                <%--SkinID="skinBtnUpdateProduct"--%><%--<asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Close Window"></asp:Label>--%>
                                                                                                                                                                                                            </asp:Panel>
                                                                                                                                                                                                        </td>
                                                                                                                                                                                                        <td>
                                                                                                                                                                                                            <asp:Panel ID="Panel3" runat="server" Height="32px" Width="120px">
                                                                                                                                                                                                                <asp:Button ID="cmdSaveProduct" runat="server" CssClass="AddProd6" EnableTheming="false" Height="45px" OnClick="cmdSaveProduct_Click" Text="" ValidationGroup="product" />
                                                                                                                                                                                                                <%--SkinID="skinBtnUpdateProduct"--%><%--<asp:Label ID="Label2" runat="server" Font-Bold="True" Text="Add Product"></asp:Label>--%>
                                                                                                                                                                                                                <asp:Button ID="cmdUpdateProduct" runat="server" CssClass="UpdateProd6" Enabled="false" EnableTheming="false" Height="45px" OnClick="cmdUpdateProduct_Click" Text="" ValidationGroup="product" Width="45px" />
                                                                                                                                                                                                                <%--SkinID="skinBtnUpdateProduct"--%><%--<asp:Label ID="Label3" runat="server" Enabled="false" Font-Bold="True" 
                                                                                                                                                        Text="Update Product"></asp:Label>--%>
                                                                                                                                                                                                            </asp:Panel>
                                                                                                                                                                                                        </td>
                                                                                                                                                                                                        <td>
                                                                                                                                                                                                            <asp:Panel ID="Panel1" runat="server">
                                                                                                                                                                                                                <asp:Button ID="BtnClearFilter" runat="server" CssClass="ClearFilter666" EnableTheming="false" OnClick="btnClearFilter_Click" Text="" />
                                                                                                                                                                                                            </asp:Panel>
                                                                                                                                                                                                        </td>
                                                                                                                                                                                                    </tr>
                                                                                                                                                                                                </table>
                                                                                                                                                                                            </td>
                                                                                                                                                                                            <td></td>
                                                                                                                                                                                        </tr>
                                                                                                                                                                                    </table>
                                                                                                                                                                                </td>
                                                                                                                                                                                <td></td>
                                                                                                                                                                            </tr>
                                                                                                                                                                        </table>
                                                                                                                                                                    </div>
                                                                                                                                                                </asp:Panel>
                                                                                                                                                            </ContentTemplate>
                                                                                                                                                        </asp:UpdatePanel>
                                                                                                                                                    </asp:Panel>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                            <tr>
                                                                                                                                                <td style="width: 50%;">
                                                                                                                                                    <asp:GridView ID="GrdViewItems" runat="server" AllowPaging="True" BorderWidth="1px" CssClass="someClass" DataKeyNames="Roles" EmptyDataText="No Sales Items added." OnRowDataBound="GrdViewItems_RowDataBound" OnRowDeleting="GrdViewItems_RowDeleting" OnSelectedIndexChanged="GrdViewItems_SelectedIndexChanged" ShowFooter="false" Width="100%" Visible="false">
                                                                                                                                                        <RowStyle Font-Bold="false" />
                                                                                                                                                        <FooterStyle Font-Bold="true" Wrap="false" />
                                                                                                                                                        <Columns>
                                                                                                                                                            <asp:BoundField DataField="itemcode" HeaderStyle-BorderColor="Gray" HeaderText="Product" />
                                                                                                                                                            <asp:BoundField DataField="ProductName" HeaderStyle-BorderColor="Gray" HeaderText="Description" />
                                                                                                                                                            <asp:BoundField DataField="ProductDesc" HeaderStyle-BorderColor="Gray" HeaderText="Brand" Visible="false" />
                                                                                                                                                            <asp:BoundField DataField="Rate" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="50px" HeaderText="Rate" />
                                                                                                                                                            <asp:BoundField DataField="Qty" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="50px" HeaderText="Qty." />
                                                                                                                                                            <asp:BoundField DataField="ExecCharge" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="70px" HeaderText="Exec Comm" />
                                                                                                                                                            <asp:BoundField DataField="Measure_Unit" HeaderStyle-BorderColor="Gray" HeaderText="Unit" Visible="false" />
                                                                                                                                                            <asp:BoundField DataField="Discount" DataFormatString="{0:F2}" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="60px" HeaderText="Disc(%)" />
                                                                                                                                                            <asp:BoundField DataField="Vat" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="60px" HeaderText="VAT(%)" />
                                                                                                                                                            <asp:BoundField DataField="CST" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="60px" HeaderText="CST(%)" />
                                                                                                                                                            <asp:BoundField DataField="VatAmount" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="60px" HeaderText="VatAmt" />
                                                                                                                                                            <asp:BoundField DataField="Totalmrp" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="60px" HeaderText="Rate With Vat" />
                                                                                                                                                            <asp:TemplateField FooterStyle-Font-Bold="True" HeaderStyle-BorderColor="Gray" HeaderText="Total">
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <asp:Label ID="lbltotal" runat="server" Text='<%# GetTotal(Convert.ToDouble(Eval("Qty").ToString()), Convert.ToDouble(Eval("rate").ToString()), Convert.ToDouble(Eval("discount").ToString()), Convert.ToDouble(Eval("vat").ToString()), Convert.ToDouble(Eval("CST").ToString()), Convert.ToDouble(Eval("Totalmrp").ToString()))%>'></asp:Label>
                                                                                                                                                                    <%--<asp:Label ID="lbltotal" runat="server" Text='<%# GetTotal(Eval("Qty").ToString(), Eval("rate").ToString(), Eval("discount").ToString(), Eval("vat").ToString(), Eval("CST").ToString())%>'></asp:Label>--%>
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                                <FooterTemplate>
                                                                                                                                                                    <asp:Label ID="lbltotalSummary" runat="server" Text=""></asp:Label>
                                                                                                                                                                </FooterTemplate>
                                                                                                                                                                <FooterStyle />
                                                                                                                                                            </asp:TemplateField>
                                                                                                                                                            <asp:TemplateField HeaderStyle-BorderColor="Gray" HeaderStyle-Width="30px" HeaderText="Edit" ItemStyle-CssClass="command">
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <asp:ImageButton ID="btnEdit" runat="server" CommandName="Select" SkinID="edit" />
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                                                                                                            </asp:TemplateField>
                                                                                                                                                            <asp:TemplateField HeaderStyle-BorderColor="Gray" HeaderStyle-Width="30px" HeaderText="Delete" ItemStyle-CssClass="command">
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <cc1:ConfirmButtonExtender ID="CnrfmDel" runat="server" ConfirmText="Are you sure to delete this product from Sales?" TargetControlID="lnkB">
                                                                                                                                                                    </cc1:ConfirmButtonExtender>
                                                                                                                                                                    <asp:ImageButton ID="lnkB" runat="Server" CommandName="Delete" SkinID="delete" />
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                                <HeaderStyle Width="4%" />
                                                                                                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                                                                                                            </asp:TemplateField>
                                                                                                                                                        </Columns>
                                                                                                                                                    </asp:GridView>
                                                                                                                                                    <asp:GridView ID="GrdViewEmptyItems" runat="server" AllowPaging="True" AutoGenerateColumns="false" BorderWidth="1px" CssClass="someClass" EnableTheming="false" GridLines="Both" ShowFooter="false" Width="100%" Visible="false">
                                                                                                                                                        <RowStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" CssClass="dataRow" />
                                                                                                                                                        <SelectedRowStyle CssClass="SelectdataRow" Font-Bold="true" />
                                                                                                                                                        <AlternatingRowStyle CssClass="altRow" />
                                                                                                                                                        <EmptyDataRowStyle CssClass="HeadataRow" Font-Bold="true" Height="25px" />
                                                                                                                                                        <HeaderStyle CssClass="HeadataRow" Font-Bold="true" Height="25px" />
                                                                                                                                                        <FooterStyle CssClass="dataRow" Height="27px" />
                                                                                                                                                        <Columns>
                                                                                                                                                            <asp:BoundField DataField="itemcode" HeaderStyle-BorderColor="Gray" HeaderText="Product" />
                                                                                                                                                            <asp:BoundField DataField="ProductName" HeaderStyle-BorderColor="Gray" HeaderText="Description" />
                                                                                                                                                            <asp:BoundField DataField="ProductDesc" HeaderStyle-BorderColor="Gray" HeaderText="Brand" Visible="false" />
                                                                                                                                                            <asp:BoundField DataField="Rate" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="50px" HeaderText="Rate" />
                                                                                                                                                            <asp:BoundField DataField="Qty" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="50px" HeaderText="Qty." />
                                                                                                                                                            <asp:BoundField DataField="ExecCharge" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="70px" HeaderText="Exec Comm" />
                                                                                                                                                            <asp:BoundField DataField="Measure_Unit" HeaderStyle-BorderColor="Gray" HeaderText="Unit" Visible="false" />
                                                                                                                                                            <asp:BoundField DataField="Discount" DataFormatString="{0:F2}" HeaderStyle-BorderColor="blue" HeaderStyle-Width="60px" HeaderText="Disc(%)" />
                                                                                                                                                            <asp:BoundField DataField="Vat" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="60px" HeaderText="VAT(%)" />
                                                                                                                                                            <asp:BoundField DataField="CST" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="60px" HeaderText="CST(%)" />
                                                                                                                                                            <asp:BoundField DataField="VatAmount" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="60px" HeaderText="VatAmount" Visible="false" />
                                                                                                                                                        </Columns>
                                                                                                                                                    </asp:GridView>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                            <tr>
                                                                                                                                                <td colspan="4">
                                                                                                                                                    <center>
                                                                                                                                                        <div style="height: 250px; width: 100%; overflow: scroll">

                                                                                                                                                            <asp:GridView ID="grvStudentDetails" runat="server" Width="100%"
                                                                                                                                                                ShowFooter="True" AutoGenerateColumns="False"
                                                                                                                                                                CellPadding="1" OnRowDataBound="grvStudentDetails_RowDataBound"
                                                                                                                                                                GridLines="None" OnRowDeleting="grvStudentDetails_RowDeleting">

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
                                                                                                                                                                            <asp:DropDownList ID="drpPrd" Width="230px" runat="server" AppendDataBoundItems="true" BackColor="White" ForeColor="#0567AE" Font-Bold="false" AutoPostBack="true" CssClass="chzn-select" DataTextField="ProductName" DataValueField="ItemCode" OnSelectedIndexChanged="drpPrd_SelectedIndexChanged">
                                                                                                                                                                            </asp:DropDownList>
                                                                                                                                                                        </ItemTemplate>
                                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                                    <asp:TemplateField HeaderText="Quantity" ItemStyle-Width="40px" ItemStyle-Font-Size="10px" HeaderStyle-ForeColor="Black">
                                                                                                                                                                        <ItemTemplate>
                                                                                                                                                                            <cc1:FilteredTextBoxExtender ID="Qty" runat="server" FilterType="Numbers" TargetControlID="txtQty" />
                                                                                                                                                                            <asp:TextBox ID="txtQty" Style="text-align: right" runat="server" Width="40px" ForeColor="#0567AE" Font-Bold="false" AutoPostBack="true" OnTextChanged="txtQty_TextChanged"></asp:TextBox>
                                                                                                                                                                        </ItemTemplate>
                                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                                    <asp:TemplateField HeaderText="Employee Name" ItemStyle-Width="135px" ItemStyle-Font-Size="10px" HeaderStyle-ForeColor="Black">
                                                                                                                                                                        <ItemTemplate>
                                                                                                                                                                            <asp:DropDownList ID="drpIncharge" Width="135px" runat="server" AppendDataBoundItems="true" BackColor="White" ForeColor="#0567AE" Font-Bold="false" AutoPostBack="true" CssClass="chzn-select" DataTextField="empFirstName" DataValueField="empno" OnSelectedIndexChanged="drpIncharge_SelectedIndexChanged">
                                                                                                                                                                            </asp:DropDownList>
                                                                                                                                                                        </ItemTemplate>
                                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                                    <asp:TemplateField HeaderText="Brand" ItemStyle-Width="115px" ItemStyle-Font-Size="10px" HeaderStyle-ForeColor="Black">
                                                                                                                                                                        <ItemTemplate>
                                                                                                                                                                            <asp:TextBox ID="txtDesc" runat="server" ReadOnly="true" Width="115px" ForeColor="#0567AE" Font-Bold="false"></asp:TextBox>
                                                                                                                                                                        </ItemTemplate>
                                                                                                                                                                        <FooterStyle HorizontalAlign="Left" />
                                                                                                                                                                        <FooterTemplate>
                                                                                                                                                                            <asp:Button ID="ButtonAdd" runat="server" AutoPostback="true" EnableTheming="false" Width="115px"
                                                                                                                                                                                ValidationGroup="DynRowAdd" Text="Add New Product" OnClick="ButtonAdd_Click" />
                                                                                                                                                                        </FooterTemplate>
                                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                                    <asp:TemplateField HeaderText="Return Quantity" ItemStyle-Width="40px" ItemStyle-Font-Size="10px" HeaderStyle-ForeColor="Black">
                                                                                                                                                                        <ItemTemplate>
                                                                                                                                                                            <cc1:FilteredTextBoxExtender ID="RtnQty" runat="server" FilterType="Numbers" TargetControlID="txtRtnQty" />
                                                                                                                                                                            <asp:TextBox ID="txtRtnQty" Style="text-align: right" runat="server" Width="40px" ForeColor="#0567AE" Font-Bold="false" AutoPostBack="true" OnTextChanged="txtRtnQty_TextChanged"></asp:TextBox>
                                                                                                                                                                        </ItemTemplate>
                                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                                    <asp:TemplateField HeaderText="Unit Price" ItemStyle-Width="63px" ItemStyle-Font-Size="10px" HeaderStyle-ForeColor="Black">
                                                                                                                                                                        <ItemTemplate>
                                                                                                                                                                            <cc1:FilteredTextBoxExtender ID="rte" runat="server" Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtRate" ValidChars="." />
                                                                                                                                                                            <asp:TextBox ID="txtRate" Style="text-align: right" runat="server" Width="63px" ForeColor="#0567AE" Font-Bold="false" AutoPostBack="true" OnTextChanged="txtRate_TextChanged"></asp:TextBox>
                                                                                                                                                                        </ItemTemplate>
                                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                                    <asp:TemplateField HeaderText="Total Price" ItemStyle-Width="75px" ItemStyle-Font-Size="10px" HeaderStyle-ForeColor="Black">
                                                                                                                                                                        <ItemTemplate>
                                                                                                                                                                            <asp:TextBox ID="txtTot" Style="text-align: right" ReadOnly="true" runat="server" Width="75px" ForeColor="#0567AE" Font-Bold="false"></asp:TextBox>
                                                                                                                                                                        </ItemTemplate>
                                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                                    <asp:TemplateField HeaderText="Stock" ItemStyle-Width="30px" ItemStyle-Font-Size="10px" HeaderStyle-ForeColor="Black">
                                                                                                                                                                        <ItemTemplate>
                                                                                                                                                                            <%-- <asp:CompareValidator ID="cvCustomer" runat="server" ControlToValidate="txtStock" ControlToCompare="txtQty" Display="Dynamic" ErrorMessage="Quantity is greater than Stock" Operator="GreaterThan" Text="*" ValidationGroup="DynRowAdd"></asp:CompareValidator>--%>
                                                                                                                                                                            <asp:TextBox ID="txtStock" Style="text-align: right" ReadOnly="true" runat="server" Width="30px" ForeColor="#0567AE" Font-Bold="false"></asp:TextBox>
                                                                                                                                                                        </ItemTemplate>
                                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                                    <asp:TemplateField HeaderText="Exec Commission" ItemStyle-Width="50px" ItemStyle-Font-Size="10px" HeaderStyle-ForeColor="Black">
                                                                                                                                                                        <ItemTemplate>
                                                                                                                                                                            <asp:TextBox ID="txtExeComm" Style="text-align: right" runat="server" ReadOnly="true" Width="50px" ForeColor="#0567AE" Font-Bold="false"></asp:TextBox>
                                                                                                                                                                        </ItemTemplate>
                                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                                    <asp:TemplateField HeaderText="Disc(%)" ItemStyle-Width="35px" ItemStyle-Font-Size="10px" HeaderStyle-ForeColor="Black">
                                                                                                                                                                        <ItemTemplate>
                                                                                                                                                                            <asp:TextBox ID="txtDisPre" Style="text-align: right" runat="server" Width="35px" ForeColor="#0567AE" Font-Bold="false" ReadOnly="true" AutoPostBack="true" OnTextChanged="txtQty_TextChanged"></asp:TextBox>
                                                                                                                                                                        </ItemTemplate>
                                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                                    <asp:TemplateField HeaderText="VAT(%)" ItemStyle-Width="35px" ItemStyle-Font-Size="10px" HeaderStyle-ForeColor="Black">
                                                                                                                                                                        <ItemTemplate>
                                                                                                                                                                            <asp:TextBox ID="txtVATPre" Style="text-align: right" ReadOnly="true" runat="server" Width="35px" ForeColor="#0567AE" Font-Bold="false"></asp:TextBox>
                                                                                                                                                                        </ItemTemplate>
                                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                                    <asp:TemplateField HeaderText="CST(%)" ItemStyle-Width="35px" ItemStyle-Font-Size="10px" HeaderStyle-ForeColor="Black">
                                                                                                                                                                        <ItemTemplate>
                                                                                                                                                                            <asp:TextBox ID="txtCSTPre" Style="text-align: right" runat="server" ReadOnly="true" Width="35px" ForeColor="#0567AE" Font-Bold="false"></asp:TextBox>
                                                                                                                                                                        </ItemTemplate>
                                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                                    <asp:TemplateField HeaderText="Price Before VAT" ItemStyle-Width="80px" ItemStyle-Font-Size="10px" HeaderStyle-ForeColor="Black">
                                                                                                                                                                        <ItemTemplate>
                                                                                                                                                                            <asp:TextBox ID="txtPrBefVAT" Style="text-align: right" ReadOnly="true" runat="server" Width="80px" ForeColor="#0567AE" Font-Bold="false"></asp:TextBox>
                                                                                                                                                                        </ItemTemplate>
                                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                                    <asp:TemplateField HeaderText="VAT Amount" ItemStyle-Width="70px" ItemStyle-Font-Size="10px" HeaderStyle-ForeColor="Black">
                                                                                                                                                                        <ItemTemplate>
                                                                                                                                                                            <asp:TextBox ID="txtVATAmt" Style="text-align: right" runat="server" ReadOnly="true" Width="70px" ForeColor="#0567AE" Font-Bold="false"></asp:TextBox>
                                                                                                                                                                        </ItemTemplate>
                                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                                    <asp:TemplateField HeaderText="Rate with VAT" ItemStyle-Width="80px" ItemStyle-Font-Size="10px" HeaderStyle-ForeColor="Black" Visible="false">
                                                                                                                                                                        <ItemTemplate>
                                                                                                                                                                            <asp:TextBox ID="txtRtVAT" Style="text-align: right" runat="server" ReadOnly="true" Width="80px" ForeColor="#0567AE" Font-Bold="false" Visible="false"></asp:TextBox>
                                                                                                                                                                        </ItemTemplate>
                                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                                    <asp:TemplateField HeaderText="Row Total" ItemStyle-Width="80px" ItemStyle-Font-Size="10px" HeaderStyle-ForeColor="Black">
                                                                                                                                                                        <ItemTemplate>
                                                                                                                                                                            <asp:TextBox ID="txtTotal" Style="text-align: right" runat="server" ReadOnly="true" Width="78px" ForeColor="#0567AE" Font-Bold="false"></asp:TextBox>
                                                                                                                                                                        </ItemTemplate>
                                                                                                                                                                        <%-- <FooterStyle HorizontalAlign="Left" />
                                                                                                                                                                    <FooterTemplate>
                                                                                                                                                                        <asp:Button ID="ButtonAdd" runat="server" AutoPostback="true" EnableTheming="false"
                                                                                                                                                                            ValidationGroup="DynRowAdd" Text="Add New" OnClick="ButtonAdd_Click" />
                                                                                                                                                                    </FooterTemplate>--%>
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

                                                                                                                                                        </div>
                                                                                                                                                    </center>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                        </table>
                                                                                                                                    </asp:Panel>

                                                                                                                                </ContentTemplate>
                                                                                                                                <Triggers>
                                                                                                                                    <asp:AsyncPostBackTrigger ControlID="cmdSaveProduct" EventName="Click" />
                                                                                                                                    <asp:AsyncPostBackTrigger ControlID="cmdUpdateProduct" EventName="Click" />
                                                                                                                                    <asp:AsyncPostBackTrigger ControlID="GrdViewItems" />
                                                                                                                                    <%--<asp:AsyncPostBackTrigger ControlID="cmdUpdate" />--%>
                                                                                                                                </Triggers>
                                                                                                                            </asp:UpdatePanel>
                                                                                                                            <br></br>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr style="height: 5px">
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td colspan="4">
                                                                                                                            <asp:UpdatePanel ID="UpdatePanelTotalSummary" runat="server" UpdateMode="Conditional">
                                                                                                                                <ContentTemplate>
                                                                                                                                    <table width="100%">
                                                                                                                                        <tr>
                                                                                                                                            <td style="width: 11px"></td>
                                                                                                                                            <%--<td class="uploadingbg312">--%>
                                                                                                                                            <td>
                                                                                                                                                <div>
                                                                                                                                                    <div>
                                                                                                                                                        <div>
                                                                                                                                                            <table border="0" cellpadding="0px" cellspacing="5px" style="margin: 0px auto;">
                                                                                                                                                                <tr style="display: none">
                                                                                                                                                                    <td align="left">
                                                                                                                                                                        <asp:Label ID="lblDispTotal" runat="server" CssClass="item" Visible="false"></asp:Label>
                                                                                                                                                                    </td>
                                                                                                                                                                    <td width="1px"></td>
                                                                                                                                                                    <td align="left">
                                                                                                                                                                        <asp:Label ID="lblTotalSum" runat="server" CssClass="item" Font-Bold="true" Visible="false"></asp:Label>
                                                                                                                                                                        <br />
                                                                                                                                                                    </td>
                                                                                                                                                                </tr>
                                                                                                                                                                <tr style="display: none">
                                                                                                                                                                    <td align="left">
                                                                                                                                                                        <asp:Label ID="lblDispDisRate" runat="server" CssClass="item" Visible="false"></asp:Label>
                                                                                                                                                                        <asp:Label ID="lblDispTotalRate" runat="server" CssClass="item" Visible="false"></asp:Label>
                                                                                                                                                                    </td>
                                                                                                                                                                    <td width="1px"></td>
                                                                                                                                                                    <td align="left">
                                                                                                                                                                        <asp:Label ID="lblTotalDis" runat="server" CssClass="item" Font-Bold="true" Visible="false"></asp:Label>
                                                                                                                                                                        <br />
                                                                                                                                                                    </td>
                                                                                                                                                                </tr>
                                                                                                                                                                <tr style="display: none">
                                                                                                                                                                    <td align="left">
                                                                                                                                                                        <asp:Label ID="lblDispIncVAT" runat="server" CssClass="item" Visible="false"></asp:Label>
                                                                                                                                                                    </td>
                                                                                                                                                                    <td width="1px"></td>
                                                                                                                                                                    <td align="left">
                                                                                                                                                                        <asp:Label ID="lblTotalVAT" runat="server" CssClass="item" Font-Bold="true" Visible="false"></asp:Label>
                                                                                                                                                                    </td>
                                                                                                                                                                </tr>
                                                                                                                                                                <tr style="display: none">
                                                                                                                                                                    <td align="left">
                                                                                                                                                                        <asp:Label ID="lblDispIncCST" runat="server" CssClass="item" Visible="false"></asp:Label>
                                                                                                                                                                    </td>
                                                                                                                                                                    <td width="1px"></td>
                                                                                                                                                                    <td align="left">
                                                                                                                                                                        <asp:Label ID="lblTotalCST" runat="server" CssClass="item" Font-Bold="true" Visible="false"></asp:Label>
                                                                                                                                                                    </td>
                                                                                                                                                                </tr>
                                                                                                                                                                <tr>
                                                                                                                                                                    <td align="left">
                                                                                                                                                                        <asp:Label ID="lblDispLoad" runat="server" CssClass="item" Visible="false"></asp:Label>
                                                                                                                                                                    </td>
                                                                                                                                                                    <td width="1px"></td>
                                                                                                                                                                    <td align="right">
                                                                                                                                                                        <asp:Label ID="lblFreight" runat="server" CssClass="item" Font-Bold="true" Visible="false"></asp:Label>
                                                                                                                                                                    </td>
                                                                                                                                                                </tr>
                                                                                                                                                                <tr>
                                                                                                                                                                    <td align="left">
                                                                                                                                                                        <asp:Label ID="lblDispGrandTtl" runat="server" CssClass="item" Visible="false"></asp:Label>
                                                                                                                                                                    </td>
                                                                                                                                                                    <td width="1px"></td>
                                                                                                                                                                    <td align="right">
                                                                                                                                                                        <asp:Label ID="lblNet1" runat="server" CssClass="item" Font-Bold="true" Text="0" Visible="false"></asp:Label>
                                                                                                                                                                    </td>
                                                                                                                                                                </tr>
                                                                                                                                                            </table>
                                                                                                                                                        </div>
                                                                                                                                                    </div>
                                                                                                                                                </div>
                                                                                                                                            </td>
                                                                                                                                            <td style="text-align: right">
                                                                                                                                                <div style="text-align: right">
                                                                                                                                                    <asp:Panel ID="PanelCmd" runat="server">
                                                                                                                                                        <table style="width: 100%;">
                                                                                                                                                            <tr>
                                                                                                                                                                <td style="width: 48%;"></td>
                                                                                                                                                                <td style="width: 16%;">
                                                                                                                                                                    <asp:Button ID="AddNewProd" runat="server" CssClass="AddProd6" EnableTheming="false" OnClick="lnkAddProduct_Click" SkinID="skinBtnAddProduct" Text="" Visible="false" /></td>
                                                                                                                                                                <td style="width: 36%;"></td>
                                                                                                                                                                <td></td>
                                                                                                                                                                <td style="width: 6px"></td>
                                                                                                                                                            </tr>
                                                                                                                                                            <tr>
                                                                                                                                                                <td style="width: 48%;"></td>
                                                                                                                                                                <td style="width: 16%;">
                                                                                                                                                                    <asp:Button ID="CmdProd" runat="server" CssClass="Newproductbutton6" EnableTheming="false" Height="27px" OnClick="cmdprod_click" SkinID="skinBtnGeneral" Text="" Width="28px" Visible="false" /></td>
                                                                                                                                                                <td style="width: 36%;"></td>
                                                                                                                                                                <td style="width: 6px"></td>
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
                                                                                                                                                                <td style="width: 40%;"></td>
                                                                                                                                                                <td style="width: 15%"></td>
                                                                                                                                                                <td style="width: 25%;" class="ControlLabelproject">Freight Charges                                                                                                                                                               
                                                                                                                                                                <td style="width: 14%;">
                                                                                                                                                                    <asp:TextBox ID="txtFreight" Style="text-align: right" runat="server" AutoPostBack="True" BorderWidth="1px" OnTextChanged="txtLU_TextChanged" SkinID="skinTxtBox" TabIndex="6" Text="0" ValidationGroup="product" Width="200px"></asp:TextBox>
                                                                                                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtFreight" ValidChars="." />
                                                                                                                                                                </td>
                                                                                                                                                                    <td style="width: 6%;"></td>
                                                                                                                                                            </tr>
                                                                                                                                                            <tr>
                                                                                                                                                                <td></td>
                                                                                                                                                                <td style="width: 40%;"></td>
                                                                                                                                                                <td style="width: 15%"></td>
                                                                                                                                                                <td style="width: 25%;" class="ControlLabelproject">Loading / Unloading Charges                                                                                                                                                               
                                                                                                                                                                <td style="width: 14%;">
                                                                                                                                                                    <asp:TextBox ID="txtLU" Style="text-align: right" runat="server" AutoPostBack="True" BorderWidth="1px" OnTextChanged="txtLU_TextChanged" SkinID="skinTxtBox" TabIndex="7" Text="0" ValidationGroup="product" Width="200px"></asp:TextBox>
                                                                                                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtLU" ValidChars="." />
                                                                                                                                                                </td>
                                                                                                                                                                    <td style="width: 6%;"></td>
                                                                                                                                                            </tr>
                                                                                                                                                            <tr>
                                                                                                                                                                <td></td>
                                                                                                                                                                <td style="width: 40%;"></td>
                                                                                                                                                                <td style="width: 15%"></td>
                                                                                                                                                                <td style="width: 25%;" class="ControlLabelproject">Grand Total(INR)   
                                                                                                                                                                <td style="width: 14%;">
                                                                                                                                                                    <asp:TextBox ID="lblNet" Style="text-align: right" runat="server" AutoPostBack="True" SkinID="skinTxtBox" TabIndex="7" Text="0" Width="200px"></asp:TextBox>
                                                                                                                                                                    <%--  <asp:Label ID="lblNet" Style="text-align: right" runat="server" CssClass="ControlLabelproject" Text="0"></asp:Label>--%>
                                                                                                                                                                </td>
                                                                                                                                                                    <td style="width: 6%;"></td>
                                                                                                                                                            </tr>
                                                                                                                                                        </table>
                                                                                                                                                    </asp:Panel>
                                                                                                                                                </div>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                    </table>
                                                                                                                                    <%--<br />--%>
                                                                                                                                </ContentTemplate>
                                                                                                                                <Triggers>
                                                                                                                                    <%--<asp:AsyncPostBackTrigger ControlID="cmbCustomer" EventName="SelectedIndexChanged" />--%><%--<asp:AsyncPostBackTrigger ControlID="cmdSave" EventName="Click" />
                                                                                            <asp:AsyncPostBackTrigger ControlID="cmdCancel" EventName="Click" />--%>
                                                                                                                                    <asp:AsyncPostBackTrigger ControlID="CmdProd" EventName="Click" />
                                                                                                                                    <%--<asp:AsyncPostBackTrigger ControlID="cmdUpdateProduct" EventName="Click" />
                                                                                            <asp:AsyncPostBackTrigger ControlID="cmdSaveProduct" EventName="Click" />--%>
                                                                                                                                </Triggers>
                                                                                                                            </asp:UpdatePanel>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </ContentTemplate>
                                                                                                        </cc1:TabPanel>
                                                                                                        <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="Additional Sales Details">
                                                                                                            <HeaderTemplate>
                                                                                                                <div>
                                                                                                                    <table>
                                                                                                                        <tr>
                                                                                                                            <td><b>Additional Sales Details </b></td>
                                                                                                                        </tr>
                                                                                                                    </table>
                                                                                                                </div>
                                                                                                            </HeaderTemplate>
                                                                                                            <ContentTemplate>
                                                                                                                <table cellpadding="0" cellspacing="1" class="tblLeft" width="1260px">

                                                                                                                    <tr style="height: 2px">
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td class="ControlLabelproject" style="width: 20%">Delivery Note
                                                                                                                        </td>
                                                                                                                        <td class="ControlDrpBorder" style="width: 25%">
                                                                                                                            <asp:DropDownList ID="ddDeliveryNote" runat="server" AutoPostBack="false" BackColor="#e7e7e7" CssClass="drpDownListMedium" Height="26px" Style="border: 1px solid #e7e7e7" TabIndex="10" Width="100%">
                                                                                                                                <asp:ListItem Selected="True" Text="NO" Value="NO"></asp:ListItem>
                                                                                                                                <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td class="ControlLabelproject" style="width: 20%">Manual Sales </td>
                                                                                                                        <td class="ControlDrpBorder" style="width: 25%">
                                                                                                                            <asp:DropDownList ID="drpmanualsales" runat="server" AutoPostBack="false" BackColor="#e7e7e7" CssClass="drpDownListMedium" Height="26px" Style="border: 1px solid #e7e7e7" TabIndex="10" Width="100%">
                                                                                                                                <asp:ListItem Selected="True" Text="NO" Value="NO"></asp:ListItem>
                                                                                                                                <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td style="width: 15%"></td>
                                                                                                                    </tr>
                                                                                                                    <tr style="height: 2px">
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td class="ControlLabelproject" style="width: 20%">Normal Sales </td>
                                                                                                                        <td class="ControlDrpBorder" style="width: 25%">
                                                                                                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                                                                                                                <ContentTemplate>
                                                                                                                                    <asp:DropDownList ID="drpnormalsales" runat="server" AutoPostBack="True" BackColor="#e7e7e7" CssClass="drpDownListMedium" Height="26px" Style="border: 1px solid #e7e7e7" TabIndex="8" Width="100%">
                                                                                                                                        <asp:ListItem Selected="True" Text="NO" Value="NO"></asp:ListItem>
                                                                                                                                        <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                                                                                                                    </asp:DropDownList>
                                                                                                                                </ContentTemplate>
                                                                                                                            </asp:UpdatePanel>
                                                                                                                        </td>
                                                                                                                        <td class="ControlLabelproject" style="width: 20%">Internal Transfer </td>
                                                                                                                        <td class="ControlDrpBorder" style="width: 25%">
                                                                                                                            <asp:DropDownList ID="drpIntTrans" runat="server" AutoPostBack="false" BackColor="#e7e7e7" CssClass="drpDownListMedium" Height="26px" Style="border: 1px solid #e7e7e7" TabIndex="10" Width="100%">
                                                                                                                                <asp:ListItem Selected="True" Text="NO" Value="NO"></asp:ListItem>
                                                                                                                                <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td style="width: 15%"></td>
                                                                                                                    </tr>
                                                                                                                    <tr style="height: 2px">
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td class="ControlLabelproject" style="width: 20%">Purchase Return </td>
                                                                                                                        <td class="ControlDrpBorder" style="width: 25%">
                                                                                                                            <asp:UpdatePanel ID="UpdatePanelPReturn" runat="server" UpdateMode="Conditional">
                                                                                                                                <ContentTemplate>
                                                                                                                                    <asp:DropDownList ID="drpPurchaseReturn" runat="server" AutoPostBack="True" BackColor="#e7e7e7" CssClass="drpDownListMedium" Height="26px" OnSelectedIndexChanged="drpPurchaseReturn_SelectedIndexChanged" Style="border: 1px solid #e7e7e7" TabIndex="8" Width="100%">
                                                                                                                                        <asp:ListItem Selected="True" Text="NO" Value="NO"></asp:ListItem>
                                                                                                                                        <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                                                                                                                    </asp:DropDownList>
                                                                                                                                </ContentTemplate>
                                                                                                                            </asp:UpdatePanel>
                                                                                                                        </td>
                                                                                                                        <td class="ControlLabelproject" style="width: 20%">Narration</td>
                                                                                                                        <td style="width: 25%" class="ControlTextBox3">
                                                                                                                            <asp:TextBox ID="txtnarr" runat="server" BackColor="#e7e7e7" MaxLength="200" SkinID="skinTxtBoxGrid" TabIndex="8" Width="200px"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 15%"></td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td colspan="5">
                                                                                                                            <asp:UpdatePanel ID="UpdatePanel22" runat="server" UpdateMode="Conditional">
                                                                                                                                <ContentTemplate>
                                                                                                                                    <table id="rowReason" runat="server" cellpadding="0" cellspacing="1" width="100%">
                                                                                                                                        <tr>
                                                                                                                                            <td class="ControlLabelproject" style="width: 20%">Return Reason </td>
                                                                                                                                            <td class="ControlTextBox3" style="width: 25%">
                                                                                                                                                <asp:TextBox ID="txtPRReason" runat="server" BackColor="#e7e7e7" CssClass="cssTextBox" Height="23px" MaxLength="200" TabIndex="11" TextMode="MultiLine"></asp:TextBox>
                                                                                                                                            </td>
                                                                                                                                            <td style="width: 20%"></td>
                                                                                                                                            <td style="width: 25%"></td>
                                                                                                                                            <td style="width: 15%"></td>
                                                                                                                                        </tr>
                                                                                                                                    </table>
                                                                                                                                </ContentTemplate>
                                                                                                                                <Triggers>
                                                                                                                                    <asp:AsyncPostBackTrigger ControlID="drpPurchaseReturn" EventName="SelectedIndexChanged" />
                                                                                                                                </Triggers>
                                                                                                                            </asp:UpdatePanel>
                                                                                                                        </td>
                                                                                                                        <%--<td style="width: 35%">
                                                                                        
                                                                                    </td>--%>
                                                                                                                    </tr>
                                                                                                                    <tr id="rowmanual" runat="server">
                                                                                                                        <td class="ControlLabelproject" style="width: 20%;">
                                                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtmanual" CssClass="lblFont" Display="Dynamic" ErrorMessage="Manual Bill No is mandatory" Text="*" ValidationGroup="salesval"></asp:RequiredFieldValidator>
                                                                                                                            Manual Bill No </td>
                                                                                                                        <td class="ControlTextBox3" style="width: 25%;">
                                                                                                                            <asp:TextBox ID="txtmanual" runat="server" BackColor="#e7e7e7" CssClass="cssTextBox" Text="0" Width="25px"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 20%;"></td>
                                                                                                                        <td style="width: 25%"></td>
                                                                                                                        <td style="width: 15%;"></td>
                                                                                                                    </tr>
                                                                                                                    <tr style="height: 3px">
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td colspan="6" style="text-align: center">
                                                                                                                            <asp:UpdatePanel ID="UpdatePanelMP" runat="server" UpdateMode="Conditional">
                                                                                                                                <ContentTemplate>
                                                                                                                                    <div id="divMultiPayment" runat="server" visible="false">
                                                                                                                                        <div id="divAddMPayments" runat="server" style="width: 100%; text-align: center">
                                                                                                                                            <table cellpadding="0" cellspacing="1" width="100%">
                                                                                                                                                <tr style="height: 5px">
                                                                                                                                                </tr>
                                                                                                                                                <tr>
                                                                                                                                                    <td style="width: 9%"></td>
                                                                                                                                                    <td class="ControlLabelproject" style="width: 22%; text-align: center">Receipt Source </td>
                                                                                                                                                    <td class="ControlLabelproject" style="width: 20%; text-align: center">Ref / SF No </td>
                                                                                                                                                    <td class="ControlLabelproject" style="width: 20%; text-align: center">Cheque/CreditCard </td>
                                                                                                                                                    <td class="ControlLabelproject" style="width: 20%; text-align: center">Amount </td>
                                                                                                                                                    <td style="width: 9%"></td>
                                                                                                                                                </tr>
                                                                                                                                                <tr>
                                                                                                                                                    <td style="width: 9%"></td>
                                                                                                                                                    <td class="ControlDrp3" style="width: 22%">
                                                                                                                                                        <asp:DropDownList ID="ddBank1" runat="server" AppendDataBoundItems="true" AutoPostBack="True" BackColor="#e7e7e7" CssClass="drpDownListMedium" DataTextField="LedgerName" DataValueField="LedgerID" Height="26px" onchange="OnBankselectedChange(1);" OnSelectedIndexChanged="ddBank1_SelectedIndexChanged" Style="border: 1px solid #e7e7e7" TabIndex="12" ValidationGroup="salesval" Width="100%">
                                                                                                                                                            <asp:ListItem style="background-color: #e7e7e7" Text="Select Receipt Source" Value="0"></asp:ListItem>
                                                                                                                                                        </asp:DropDownList>
                                                                                                                                                    </td>
                                                                                                                                                    <td class="ControlDrp3" style="width: 20%">
                                                                                                                                                        <asp:TextBox ID="txtRefNo1" runat="server" CssClass="cssTextBox" MaxLength="20" TabIndex="13" Width="97%"></asp:TextBox>
                                                                                                                                                    </td>
                                                                                                                                                    <td class="ControlDrp3" style="width: 20%">
                                                                                                                                                        <asp:TextBox ID="txtCCard1" runat="server" CssClass="cssTextBox" MaxLength="20" TabIndex="14" Width="97%"></asp:TextBox>
                                                                                                                                                        <cc1:FilteredTextBoxExtender ID="dtCard1" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtCCard1" />
                                                                                                                                                    </td>
                                                                                                                                                    <td class="ControlDrp3" style="width: 20%">
                                                                                                                                                        <asp:TextBox ID="txtAmount1" runat="server" AutoPostBack="true" CssClass="cssTextBox" MaxLength="20" OnTextChanged="txtRAmount_TextChanged" TabIndex="15" Width="97%"></asp:TextBox>
                                                                                                                                                        <cc1:FilteredTextBoxExtender ID="ftAmt1" runat="server" Enabled="True" FilterType="Custom,Numbers" TargetControlID="txtAmount1" ValidChars="." />
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 9%"></td>
                                                                                                                                                </tr>
                                                                                                                                                <tr style="height: 2px">
                                                                                                                                                </tr>
                                                                                                                                                <tr>
                                                                                                                                                    <td style="width: 9%"></td>
                                                                                                                                                    <td class="ControlDrp3" style="width: 22%">
                                                                                                                                                        <asp:DropDownList ID="ddBank2" runat="server" AppendDataBoundItems="true" AutoPostBack="True" BackColor="#e7e7e7" CssClass="drpDownListMedium" DataTextField="LedgerName" DataValueField="LedgerID" Height="26px" onchange="OnBankselectedChange(2);" OnSelectedIndexChanged="ddBank2_SelectedIndexChanged" Style="border: 1px solid #e7e7e7" TabIndex="15" ValidationGroup="salesval" Width="100%">
                                                                                                                                                            <asp:ListItem style="background-color: #e7e7e7" Text="Select Receipt Source" Value="0"></asp:ListItem>
                                                                                                                                                        </asp:DropDownList>
                                                                                                                                                    </td>
                                                                                                                                                    <td class="ControlDrp3" style="width: 20%">
                                                                                                                                                        <asp:TextBox ID="txtRefNo2" runat="server" CssClass="cssTextBox" MaxLength="20" TabIndex="13" Width="97%"></asp:TextBox>
                                                                                                                                                    </td>
                                                                                                                                                    <td class="ControlDrp3" style="width: 20%">
                                                                                                                                                        <asp:TextBox ID="txtCCard2" runat="server" CssClass="cssTextBox" MaxLength="20" TabIndex="16" Width="97%"></asp:TextBox>
                                                                                                                                                        <cc1:FilteredTextBoxExtender ID="ftCard2" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtCCard2" />
                                                                                                                                                    </td>
                                                                                                                                                    <td class="ControlDrp3" style="width: 20%">
                                                                                                                                                        <asp:TextBox ID="txtAmount2" runat="server" AutoPostBack="true" CssClass="cssTextBox" MaxLength="20" OnTextChanged="txtRAmount_TextChanged" TabIndex="17" Width="97%"></asp:TextBox>
                                                                                                                                                        <cc1:FilteredTextBoxExtender ID="ftAmt2" runat="server" Enabled="True" FilterType="Custom,Numbers" TargetControlID="txtAmount2" ValidChars="." />
                                                                                                                                                        <asp:CompareValidator ID="cvAmount2" runat="server" ControlToValidate="txtAmount2" Display="Dynamic" ErrorMessage="Amount must be greater than Zero" Operator="GreaterThan" Text="*" ValidationGroup="salesval" ValueToCompare="0"></asp:CompareValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 9%"></td>
                                                                                                                                                </tr>
                                                                                                                                                <tr style="height: 2px">
                                                                                                                                                </tr>
                                                                                                                                                <tr>
                                                                                                                                                    <td style="width: 9%"></td>
                                                                                                                                                    <td class="ControlDrp3" style="width: 22%">
                                                                                                                                                        <asp:DropDownList ID="ddBank3" runat="server" AppendDataBoundItems="true" AutoPostBack="True" BackColor="#e7e7e7" CssClass="drpDownListMedium" DataTextField="LedgerName" DataValueField="LedgerID" Height="26px" onchange="OnBankselectedChange(3);" OnSelectedIndexChanged="ddBank3_SelectedIndexChanged" Style="border: 1px solid #e7e7e7" TabIndex="18" ValidationGroup="salesval" Width="100%">
                                                                                                                                                            <asp:ListItem style="background-color: #e7e7e7" Text="Select Receipt Source" Value="0"></asp:ListItem>
                                                                                                                                                        </asp:DropDownList>
                                                                                                                                                    </td>
                                                                                                                                                    <td class="ControlDrp3" style="width: 20%">
                                                                                                                                                        <asp:TextBox ID="txtRefNo3" runat="server" CssClass="cssTextBox" MaxLength="20" TabIndex="19" Width="97%"></asp:TextBox>
                                                                                                                                                    </td>
                                                                                                                                                    <td class="ControlDrp3" style="width: 20%">
                                                                                                                                                        <asp:TextBox ID="txtCCard3" runat="server" CssClass="cssTextBox" MaxLength="20" TabIndex="19" Width="97%"></asp:TextBox>
                                                                                                                                                        <cc1:FilteredTextBoxExtender ID="ftCard3" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtCCard3" />
                                                                                                                                                    </td>
                                                                                                                                                    <td class="ControlDrp3" style="width: 20%">
                                                                                                                                                        <asp:TextBox ID="txtAmount3" runat="server" AutoPostBack="true" CssClass="cssTextBox" MaxLength="20" OnTextChanged="txtRAmount_TextChanged" TabIndex="20" Width="97%"></asp:TextBox>
                                                                                                                                                        <cc1:FilteredTextBoxExtender ID="ftAmount3" runat="server" Enabled="True" FilterType="Custom,Numbers" TargetControlID="txtAmount3" ValidChars="." />
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 9%"></td>
                                                                                                                                                </tr>
                                                                                                                                                <tr style="height: 2px">
                                                                                                                                                </tr>
                                                                                                                                                <tr>
                                                                                                                                                    <td style="width: 9%"></td>
                                                                                                                                                    <td style="width: 22%" class="ControlDrp3">
                                                                                                                                                        <asp:TextBox ID="TextBox3" runat="server" CssClass="cssTextBox" Text="Cash Amount" Enabled="false"></asp:TextBox></td>
                                                                                                                                                    <td align="right" class="ControlDrp3" style="width: 20%;">
                                                                                                                                                        <asp:TextBox ID="TextBox5" runat="server" CssClass="cssTextBox" Text="0"></asp:TextBox>
                                                                                                                                                    </td>
                                                                                                                                                    <td align="right" class="ControlDrp3" style="width: 20%;">
                                                                                                                                                        <asp:TextBox ID="TextBox2" runat="server" CssClass="cssTextBox" Text="0"></asp:TextBox>
                                                                                                                                                    </td>
                                                                                                                                                    <td class="ControlDrp3" style="width: 20%">
                                                                                                                                                        <asp:TextBox ID="txtCashAmount" runat="server" AutoPostBack="true" CssClass="cssTextBox" OnTextChanged="txtRAmount_TextChanged" TabIndex="21" Width="97%"></asp:TextBox>
                                                                                                                                                        <cc1:FilteredTextBoxExtender ID="ftCash" runat="server" Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtCashAmount" ValidChars="." />
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 9%"></td>
                                                                                                                                                </tr>
                                                                                                                                                <tr style="height: 2px">
                                                                                                                                                </tr>
                                                                                                                                                <tr>
                                                                                                                                                    <td style="width: 9%"></td>
                                                                                                                                                    <td style="width: 22%"></td>
                                                                                                                                                    <td style="width: 20%"></td>
                                                                                                                                                    <td style="width: 20%; text-align: center"></td>
                                                                                                                                                    <td class="tblLeft allPad" style="width: 20%; font-weight: bold; display: none">
                                                                                                                                                        <asp:Label ID="lblReceivedTotal" runat="server"></asp:Label>
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 9%"></td>
                                                                                                                                                </tr>
                                                                                                                                            </table>
                                                                                                                                        </div>
                                                                                                                                        <div id="divListMPayments" runat="server" align="center" style="text-align: center">
                                                                                                                                            <asp:GridView ID="GrdViewReceipt" runat="server" AllowPaging="True" AllowSorting="false" AutoGenerateColumns="False" DataKeyNames="TransNo" EmptyDataText="No Customer Receipts found!" OnRowCreated="GrdViewReceipt_RowCreated" Width="99%">
                                                                                                                                                <EmptyDataRowStyle CssClass="GrdContent" />
                                                                                                                                                <Columns>
                                                                                                                                                    <asp:BoundField DataField="TransNo" HeaderStyle-Wrap="false" HeaderText="Trans. No." />
                                                                                                                                                    <asp:BoundField DataField="RefNo" HeaderStyle-Wrap="false" HeaderText="Ref. No." />
                                                                                                                                                    <asp:BoundField DataField="TransDate" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Wrap="false" HeaderText="Transaction Date" />
                                                                                                                                                    <asp:BoundField DataField="Debi" HeaderStyle-Wrap="false" HeaderText="Bank Name / Cash" />
                                                                                                                                                    <asp:BoundField DataField="Amount" HeaderStyle-Wrap="false" HeaderText="Amount" />
                                                                                                                                                    <asp:BoundField DataField="Narration" HeaderStyle-Wrap="false" HeaderText="Narration" />
                                                                                                                                                </Columns>
                                                                                                                                                <PagerTemplate>
                                                                                                                                                    Goto Page
                                                                                                                                                    <asp:DropDownList ID="ddlPageSelector" runat="server" AutoPostBack="true" SkinID="skinPagerDdlBox">
                                                                                                                                                    </asp:DropDownList>
                                                                                                                                                    <asp:Button ID="btnFirst" runat="server" CommandArgument="First" CommandName="Page" Text="First" />
                                                                                                                                                    <asp:Button ID="btnPrevious" runat="server" CommandArgument="Prev" CommandName="Page" Text="Previous" />
                                                                                                                                                    <asp:Button ID="btnNext" runat="server" CommandArgument="Next" CommandName="Page" Text="Next" />
                                                                                                                                                    <asp:Button ID="btnLast" runat="server" CommandArgument="Last" CommandName="Page" Text="Last" />
                                                                                                                                                </PagerTemplate>
                                                                                                                                            </asp:GridView>
                                                                                                                                            <br />
                                                                                                                                        </div>
                                                                                                                                        <br />
                                                                                                                                    </div>
                                                                                                                                </ContentTemplate>
                                                                                                                            </asp:UpdatePanel>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </ContentTemplate>
                                                                                                        </cc1:TabPanel>
                                                                                                    </cc1:TabContainer>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td colspan="5" align="center" style="width: 1260px">
                                                                                                    <table>
                                                                                                        <tr>
                                                                                                            <td style="width: 18%"></td>
                                                                                                            <td style="width: 5%">
                                                                                                                <asp:Button ID="CmdCat" runat="server" Text="" SkinID="skinBtnGeneral"
                                                                                                                    OnClick="cmdcat_click" EnableTheming="false" CssClass="NewCustomerNew"
                                                                                                                    Width="28px" Height="27px" Visible="false" />
                                                                                                            </td>
                                                                                                            <td style="width: 16%">
                                                                                                                <asp:Button ID="cmdUpdate" runat="server" Enabled="false" OnClick="cmdUpdate_Click"
                                                                                                                    OnClientClick="javascript:Mobile_Validator();ConfirmSMSUpdate();" SkinID="Updatebutton1231"
                                                                                                                    CssClass="Updatebutton1231" EnableTheming="false" Text="" ValidationGroup="salesval" />
                                                                                                                <asp:Button ID="cmdSave" runat="server" OnClick="cmdSave_Click" OnClientClick="javascript:Mobile_Validator();ConfirmSMS();"
                                                                                                                    SkinID="skinBtnSave" Text="" ValidationGroup="salesval" CssClass="savebutton1231"
                                                                                                                    EnableTheming="false" />
                                                                                                            </td>
                                                                                                            <td style="width: 16%">

                                                                                                                <asp:Button ID="cmdCancel" runat="server" CausesValidation="false" Enabled="false"
                                                                                                                    OnClick="cmdCancel_Click" SkinID="skinBtnCancel" Text="" CssClass="cancelbutton6"
                                                                                                                    EnableTheming="false" />
                                                                                                            </td>
                                                                                                            <td style="width: 16%">

                                                                                                                <asp:Button ID="cmdPrint" runat="server" Enabled="false" OnClick="cmdPrint_Click"
                                                                                                                    SkinID="skinBtnPrint" Text="" CssClass="printbutton6" EnableTheming="false" ValidationGroup="salesval" />
                                                                                                            </td>
                                                                                                            <td style="width: 28%"></td>
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
                                                    </td>
                                                    </center>
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
                            <table width="100%" style="/*margin: -6px 0px 0px 0px; */">
                                <tr style="width: 100%">
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanelSalesGrid" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:GridView ID="GrdViewSales" runat="server" AllowSorting="false" AutoGenerateColumns="False"
                                                    Width="100.2%" DataKeyNames="Billno" AllowPaging="True" EmptyDataText="No Sales Details Found"
                                                    OnRowCreated="GrdViewSales_RowCreated" OnRowDataBound="GrdViewSales_RowDataBound"
                                                    OnSelectedIndexChanged="GrdViewSales_SelectedIndexChanged" OnPageIndexChanging="GrdViewSales_PageIndexChanging"
                                                    OnRowDeleting="GrdViewSales_RowDeleting" CssClass="someClass">
                                                    <EmptyDataRowStyle CssClass="GrdContent" />
                                                    <HeaderStyle Height="30px" HorizontalAlign="Center" Font-Bold="true" BackColor="#cccccc" BorderColor="Gray" Font-Size="15px" />
                                                    <RowStyle Font-Bold="true" HorizontalAlign="Center" Height="30px" Font-Size="15px" ForeColor="#0567AE" />

                                                    <Columns>
                                                        <asp:BoundField DataField="TransNo" HeaderText="Trans. No." SortExpression="TransNo" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Height="20px" ItemStyle-HorizontalAlign="Center" ItemStyle-Height="30px" ItemStyle-Font-Bold="true" ItemStyle-Font-Size="15px" ItemStyle-ForeColor="#0567AE" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="Small"
                                                            HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Gray" />
                                                        <asp:BoundField DataField="Billno" HeaderText="Bill No." SortExpression="BillNo" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Height="20px" ItemStyle-HorizontalAlign="Center" ItemStyle-Height="30px" ItemStyle-Font-Bold="true" ItemStyle-Font-Size="15px" ItemStyle-ForeColor="#0567AE" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="Small"
                                                            HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Gray" />
                                                        <asp:BoundField DataField="BillDate" SortExpression="BillDate" HeaderText="Bill Date" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Height="20px" ItemStyle-HorizontalAlign="Center" ItemStyle-Height="30px" ItemStyle-Font-Bold="true" ItemStyle-Font-Size="15px" ItemStyle-ForeColor="#0567AE" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="Small"
                                                            HeaderStyle-BorderColor="Gray" />
                                                        <asp:BoundField DataField="CustomerName" SortExpression="Customer Name" HeaderText="Customer Name" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Height="20px" ItemStyle-HorizontalAlign="Center" ItemStyle-Height="30px" ItemStyle-Font-Bold="true" ItemStyle-Font-Size="15px" ItemStyle-ForeColor="#0567AE" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="Small"
                                                            HeaderStyle-BorderColor="Gray" />
                                                        <asp:TemplateField HeaderText="Payment Mode" SortExpression="Payment Mode" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Height="20px" ItemStyle-HorizontalAlign="Center" ItemStyle-Height="30px" ItemStyle-Font-Bold="true" ItemStyle-Font-Size="15px" ItemStyle-ForeColor="#0567AE" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="Small"
                                                            HeaderStyle-BorderColor="Gray">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPaymode" runat="server"></asp:Label>
                                                                <asp:HiddenField runat="server" ID="hdPaymode" Value='<%# Bind("MultiPayment") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Amount" HeaderText="Amount" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Height="20px" ItemStyle-HorizontalAlign="Center" ItemStyle-Height="30px" ItemStyle-Font-Bold="true" ItemStyle-Font-Size="15px" ItemStyle-ForeColor="#0567AE" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="Small"
                                                            HeaderStyle-BorderColor="Gray" SortExpression="Amount" DataFormatString="{0:F2}" />
                                                        <asp:BoundField DataField="CreditCardNo" HeaderText="Card No." HeaderStyle-HorizontalAlign="Center" HeaderStyle-Height="20px" ItemStyle-HorizontalAlign="Center" ItemStyle-Height="30px" ItemStyle-Font-Bold="true" ItemStyle-Font-Size="15px" ItemStyle-ForeColor="#0567AE" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="Small"
                                                            HeaderStyle-BorderColor="Gray" Visible="false" HeaderStyle-Wrap="false" />
                                                        <asp:BoundField DataField="Debtor" HeaderText="Ledger Name" SortExpression="Debtor" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Height="20px" ItemStyle-HorizontalAlign="Center" ItemStyle-Height="30px" ItemStyle-Font-Bold="true" ItemStyle-Font-Size="15px" ItemStyle-ForeColor="#0567AE" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="Small"
                                                            HeaderStyle-BorderColor="Gray"
                                                            HtmlEncode="false" />
                                                        <asp:BoundField DataField="PurchaseReturn" HeaderText="Purchase Return" HeaderStyle-Width="60px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Height="20px" ItemStyle-HorizontalAlign="Center" ItemStyle-Height="30px" ItemStyle-Font-Bold="true" ItemStyle-Font-Size="15px" ItemStyle-ForeColor="#0567AE" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="Small"
                                                            HeaderStyle-BorderColor="Gray"
                                                            HeaderStyle-Wrap="true" />
                                                        <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="45px" HeaderText="Edit" HeaderStyle-BorderColor="Gray">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Select" />
                                                                <asp:ImageButton ID="btnEditDisabled" Enabled="false" SkinID="editDisable" runat="Server"></asp:ImageButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-Width="45px" HeaderText="Print" HeaderStyle-BorderColor="Gray">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <a href='<%# DataBinder.Eval(Container, "DataItem.Billno", "javascript:PrintItem({0});") %>'>
                                                                    <asp:Image runat="server" ID="lnkprint" alt="Print" border="0" src="App_Themes/DefaultTheme/Images/PrintIcon_btn.png" />
                                                                </a>
                                                                <asp:ImageButton ID="btnViewDisabled" Enabled="false" SkinID="search" runat="Server"></asp:ImageButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="45px" HeaderText="Delete" HeaderStyle-BorderColor="Gray">
                                                            <ItemStyle Width="45px" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to Delete this Sale?"
                                                                    runat="server">
                                                                </cc1:ConfirmButtonExtender>
                                                                <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" CommandName="Delete"></asp:ImageButton>
                                                                <asp:ImageButton ID="lnkBDisabled" Enabled="false" SkinID="deleteDisable" runat="Server"></asp:ImageButton>
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
                                                                    <asp:DropDownList ID="ddlPageSelector" OnSelectedIndexChanged="ddlPageSelector_SelectedIndexChanged"
                                                                        runat="server" AutoPostBack="true" Width="75px" Style="border: 1px solid #8dcaff" Height="23px" BackColor="#e7e7e7">
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
                                                            <tr style="height: 1px">
                                                            </tr>
                                                        </table>
                                                    </PagerTemplate>
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
                    <td style="width: 100%">
                        <%--<asp:ObjectDataSource ID="GridSource" runat="server" SelectMethod="GetSalesList"
                            TypeName="BusinessLogic" DeleteMethod="DeleteSalesNew" OnDeleting="GridSource_Deleting">
                            <DeleteParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                <asp:Parameter Name="BillNo" Type="Int32" />
                                <asp:Parameter Name="UserID" Type="String" />
                            </DeleteParameters>
                        </asp:ObjectDataSource>--%>
                        <asp:HiddenField ID="hdSMS" runat="server" Value="NO" />
                        <asp:HiddenField ID="hdSMSRequired" runat="server" Value="NO" />
                        <asp:HiddenField ID="hdEmailRequired" runat="server" Value="NO" />
                        <asp:HiddenField ID="HiddenField1" Value="true" runat="server" />
                        <asp:Button ID="cmdDelete" runat="server" Enabled="false" Visible="false" SkinID="skinBtnDelete"
                            Text="Delete" ValidationGroup="salesval" />
                        <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="cmdDelete" ConfirmText="Are you sure you want to Delete this Sales?"
                            runat="server">
                        </cc1:ConfirmButtonExtender>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>

    <table align="center">
        <tr>
            <td style="width: 50%">
                <div style="text-align: right;">
                    <asp:Panel ID="pnlSearch" runat="server" Width="100px">
                        <asp:Button ID="lnkBtnAdd" ForeColor="White" runat="server" OnClick="lnkBtnAdd_Click" CssClass="ButtonAdd66"
                            EnableTheming="false" Width="80px" Font-Bold="True"></asp:Button>
                    </asp:Panel>
                </div>
            </td>
            <%--<td>
                <asp:Button ID="Button1" runat="server" CssClass="NewReport6" Font-Bold="True" ForeColor="White"
                     EnableTheming="false" Width="80px"  OnClientClick="window.open('ReportExlSales.aspx','CSTSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=550,width=790,left=290,top=60, scrollbars=yes');"></asp:Button>
            </td>--%>
            <td style="width: 50%">
                <asp:Button ID="btnSale" runat="server"
                    CssClass="exportexl6" EnableTheming="false" CausesValidation="false"
                    OnClientClick="window.open('ReportXlSales.aspx','CSTSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=360,width=610,left=400,top=220, scrollbars=no');" Font-Bold="True" ForeColor="White"></asp:Button>
            </td>
        </tr>
    </table>
</asp:Content>
