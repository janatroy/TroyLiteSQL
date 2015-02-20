<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="BankRecon.aspx.cs" Inherits="BankRecon"
    Title="Banking > Bank Reconciliation" %>

<%@ Register Assembly="RealWorld.Grids" Namespace="RealWorld.Grids" TagPrefix="rwg" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <style type="text/css">
        .hide {
            display: none;
        }
    </style>
    <script src="Scripts/JScriptPurchase.js" type="text/javascript">
        
    </script>
    <asp:UpdatePanel ID="UpdatePnlMaster" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <div style="">
                <table style="width: 100%;" align="center">
                    <tr style="width: 100%">
                        <td style="width: 100%" valign="middle">
                            <%--<table class="mainConHd" style="width: 994px;">
                                <tr valign="middle">
                                    <td style="font-size: 20px;">
                                       Bank Reconciliation
                                    </td>
                                </tr>
                            </table>--%>
                            <div class="mainConBody">
                                <div>
                                    <table cellspacing="2px" cellpadding="3px" border="0" width="99.8%"
                                        class="searchbg" style="margin: -2px 0px 0px 1px;">
                                        <tr>
                                            <td style="width: 75%; font-size: 22px; color: White;">Bank Reconciliation
                                            </td>
                                            <td style="width: 0%">
                                                <div style="text-align: right;">
                                                    <asp:Panel ID="pnlSearch" runat="server">
                                                        <asp:Button ID="lnkBtnAdd" runat="server" OnClick="lnkBtnAdd_Click" CssClass="ButtonDoreconciliation"
                                                            EnableTheming="false" Text=""></asp:Button>
                                                    </asp:Panel>
                                                </div>
                                            </td>
                                            <td style="width: 23%; color: White;" align="right">Ref No.
                                            </td>
                                            <td style="width: 5%; text-align: center" class="NewBox">
                                                <asp:TextBox ValidationGroup="search" ID="txtBillnoSrc" runat="server" MaxLength="8"
                                                    CssClass="cssTextBox" Width="75%"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtBillnoSrc"
                                                    FilterType="Numbers" />
                                            </td>
                                            <td style="width: 40%; color: White;" align="right">Trans No.
                                            </td>
                                            <td style="width: 3%" class="NewBox">
                                                <asp:TextBox ValidationGroup="search" ID="txtTransNo" runat="server" MaxLength="8"
                                                    CssClass="cssTextBox" Width="75%"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="txtTransNo"
                                                    FilterType="Numbers" />
                                            </td>
                                            <td style="width: 15%; text-align: left">
                                                <asp:Button ID="btnSearch" runat="server" Text="" CssClass="ButtonSearch6" EnableTheming="false" OnClick="btnSearch_Click" />
                                            </td>
                                            <td style="width: 15%" class="tblLeftNoPad">
                                                <asp:Button ID="BtnClearFilter" runat="server" OnClick="BtnClearFilter_Click" EnableTheming="false" Text="" CssClass="ClearFilter6" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>

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
                            <asp:Panel ID="purchasePanel" runat="server" Style="width: 50%; display: none">
                                <asp:UpdatePanel ID="updatePnlPurchase" runat="server" RenderMode="Block" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div id="Div1" style="background-color: White;">
                                            <table style="width: 100%;" align="center">
                                                <tr style="width: 100%">
                                                    <td style="width: 100%">
                                                        <table style="text-align: left; border: 1px solid    blue" width="100%" cellpadding="3" cellspacing="3">
                                                            <tr>
                                                                <td>
                                                                    <table class="headerPopUp" width="100%">
                                                                        <tr>
                                                                            <td>Bank Reconciliation
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 10px">
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <table width="100%" cellpadding="1" cellspacing="1">
                                                                        <tr>
                                                                            <td>
                                                                                <table width="100%" cellpadding="3" cellspacing="1">
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 39%">Start Date
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtStartDate"
                                                                                                CssClass="lblFont" Display="Dynamic" ErrorMessage="Start Date is mandatory" Text="*"
                                                                                                ValidationGroup="purchaseval"></asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 20%">
                                                                                            <asp:TextBox ID="txtStartDate" Enabled="false" CssClass="cssTextBox" Width="100px" MaxLength="10"
                                                                                                runat="server" />
                                                                                            <cc1:CalendarExtender ID="calBillDate" runat="server" Format="dd/MM/yyyy"
                                                                                                PopupButtonID="btnBillDate" TargetControlID="txtStartDate" Enabled="True">
                                                                                            </cc1:CalendarExtender>
                                                                                        </td>

                                                                                        <td align="left" width="16%">
                                                                                            <asp:ImageButton ID="btnBillDate" ImageUrl="App_Themes/NewTheme/images/cal.gif"
                                                                                                CausesValidation="False" Width="20px" runat="server" />
                                                                                        </td>
                                                                                        <td style="width: 22%"></td>
                                                                                    </tr>
                                                                                    <%--<tr>
                                                                                        <td class="ControlLabel" style="width:30%">
                                                                                            End Date
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEndDate"
                                                                                                CssClass="lblFont" Display="Dynamic" ErrorMessage="End Date is mandatory" Text="*"
                                                                                                ValidationGroup="purchaseval"></asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                        <td class="ControlTextBox3"  style="width:25%">
                                                                                            <asp:TextBox ID="txtEndDate" CssClass="cssTextBox" Width="100px" MaxLength="10" runat="server" />
                                                                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                                                                    PopupButtonID="ImageButton1" TargetControlID="txtEndDate" Enabled="True">
                                                                                            </cc1:CalendarExtender>
                                                                                        </td>
                                                                                                    
                                                                                        <td align="left" style="width:15%">
                                                                                            <asp:ImageButton ID="ImageButton1" ImageUrl="App_Themes/NewTheme/images/cal.gif"
                                                                                                                CausesValidation="False" Width="20px" runat="server" />   
                                                                                        </td>
                                                                                        <td style="width:22%">
                                                                                                        
                                                                                        </td>
                                                                                    </tr>--%>
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 39%">Option
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 20%">
                                                                                            <asp:RadioButtonList ID="opnbank" runat="server" Style="font-size: 14px"
                                                                                                RepeatDirection="Horizontal" BackColor="#e7e7e7" Height="35px" OnSelectedIndexChanged="opnbank_SelectedIndexChanged" AutoPostBack="True">
                                                                                                <asp:ListItem Selected="True">Bank</asp:ListItem>
                                                                                                <asp:ListItem>Customer</asp:ListItem>
                                                                                            </asp:RadioButtonList>
                                                                                        </td>

                                                                                        <td align="left" style="width: 16%"></td>
                                                                                        <td style="width: 22%"></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 39%">Bank
                                                                                        </td>
                                                                                        <td class="ControlDrpBorder" style="width: 20%">
                                                                                            <asp:DropDownList ID="ddlbank" AppendDataBoundItems="true" Style="border: 1px solid #e7e7e7" runat="server" Height="25px" Width="100%" CssClass="drpDownListMedium" BackColor="#e7e7e7" DataTextField="LedgerName" DataValueField="LedgerID">
                                                                                            </asp:DropDownList>
                                                                                        </td>

                                                                                        <td align="left" style="width: 16%"></td>
                                                                                        <td style="width: 22%"></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 39%">Customer
                                                                                        </td>
                                                                                        <td class="ControlDrpBorder" style="width: 20%">
                                                                                            <asp:DropDownList ID="ddlCustomer" AppendDataBoundItems="true" Style="border: 1px solid #e7e7e7" runat="server" Height="25px" Width="100%" CssClass="drpDownListMedium" BackColor="#e7e7e7" DataTextField="LedgerName" DataValueField="LedgerID">
                                                                                            </asp:DropDownList>
                                                                                        </td>

                                                                                        <td align="left" style="width: 16%"></td>
                                                                                        <td style="width: 22%"></td>
                                                                                    </tr>
                                                                                    <tr style="height: 10px">
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td colspan="4">
                                                                                            <table style="width: 100%">
                                                                                                <tr>
                                                                                                    <td style="width: 38%"></td>
                                                                                                    <td style="width: 15%">
                                                                                                        <asp:Button ID="cmdMetho" runat="server" CssClass="Start6"
                                                                                                            EnableTheming="false" OnClick="cmdMetho_Click" Text=""
                                                                                                            ValidationGroup="contact" />
                                                                                                    </td>
                                                                                                    <td style="width: 15%">
                                                                                                        <asp:Button ID="cmdCancel" runat="server" Text="" CssClass="cancelbutton6" EnableTheming="false"
                                                                                                            Visible="true" OnClick="cmdCancel_Click" SkinID="skinBtnCancel" />
                                                                                                    </td>
                                                                                                    <td style="width: 31%"></td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr style="height: 5px">
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
                                            </table>
                                        </div>
                                        <div>
                                            <table style="width: 100%">
                                                <tr style="width: 100%">
                                                    <td style="width: 100%" colspan="4">
                                                        <cc1:ModalPopupExtender ID="ModalPopupProduct" runat="server"
                                                            BackgroundCssClass="modalBackground" CancelControlID="CancelPopUp"
                                                            DynamicServicePath="" Enabled="True" PopupControlID="pnlItems"
                                                            TargetControlID="ShowPopUp">
                                                        </cc1:ModalPopupExtender>
                                                        <input id="ShowPopUp" runat="server" style="display: none" type="button" />
                                                        &nbsp;
                                            <input id="CancelPopUp" runat="server" style="display: none" type="button"></input>
                                                        <asp:Panel ID="pnlItems" runat="server" CssClass="pnlPopUp" Width="1000px">
                                                            <asp:UpdatePanel ID="updatePnlProduct" runat="server" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <div id="contentPopUp">
                                                                        <table cellpadding="0" cellspacing="1"
                                                                            width="100%">
                                                                            <tr>
                                                                                <td colspan="4">
                                                                                    <table class="headerPopUp" width="100%">
                                                                                        <tr>
                                                                                            <td>Details
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="4">
                                                                                    <table cellpadding="0" cellspacing="1"
                                                                                        width="100%">
                                                                                        <tr>
                                                                                            <td style="width: 100%">
                                                                                                <div id="div" runat="server" style="height: 330px; overflow: scroll">
                                                                                                    <%--<asp:GridView ID="GrdViewItems" runat="server" BorderWidth="1px" DataKeyNames="TransNo"
                                                                                    EmptyDataText="No Entries Found." OnPageIndexChanging="GrdViewItems_PageIndexChanging"
                                                                                    OnRowCancelingEdit="GrdViewItems_RowCancelingEdit" OnRowCreated="GrdViewItems_RowCreated" OnPreRender="GrdViewItems_PreRender"  
                                                                                    OnRowDeleting="GrdViewItems_RowDeleting" OnRowDataBound="GrdViewItems_RowDataBound" 
                                                                                    OnRowEditing="GrdViewItems_RowEditing" OnRowUpdating="GrdViewItems_RowUpdating"
                                                                                    OnSelectedIndexChanged="GrdViewItems_SelectedIndexChanged" ShowFooter="false" CssClass="someClass"
                                                                                    Width="100%" AllowPaging="True" PageSize="6">
                                                                                    <EditRowStyle VerticalAlign="Middle" />
                                                                                    <RowStyle Font-Bold="false" />
                                                                                    <FooterStyle CssClass="HeadataRow" Font-Bold="true" Height="27px" />--%>
                                                                                                    <rwg:BulkEditGridView ID="GrdViewItems" AutoGenerateColumns="False" BorderWidth="1px"
                                                                                                        BorderStyle="Solid" GridLines="Both" SaveButtonID="SaveButton" runat="server" CssClass="someClass" OnRowDataBound="GrdViewItems_RowDataBound"
                                                                                                        Width="100%">
                                                                                                        <RowStyle CssClass="dataRow" />
                                                                                                        <SelectedRowStyle CssClass="SelectdataRow" />
                                                                                                        <AlternatingRowStyle CssClass="altRow" />
                                                                                                        <EmptyDataRowStyle CssClass="HeadataRow" Font-Bold="true" />
                                                                                                        <HeaderStyle CssClass="HeadataRow" Wrap="false" />
                                                                                                        <FooterStyle CssClass="dataRow" />
                                                                                                        <Columns>
                                                                                                            <asp:BoundField DataField="TransNo" HeaderText="TransNo" HeaderStyle-BorderColor="Gray" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-Width="60px" />
                                                                                                            <asp:BoundField DataField="TransDate" HeaderText="Trans Date" HeaderStyle-BorderColor="Gray" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-Width="60px" />
                                                                                                            <asp:BoundField DataField="Debtor" HeaderText="Name" HeaderStyle-BorderColor="Gray" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-Width="80px" />
                                                                                                            <asp:TemplateField Visible="false">
                                                                                                                <ItemTemplate>
                                                                                                                    <%--<asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("DebtorID") %>' />--%>
                                                                                                                    <asp:Label ID="lblDebtorID" runat="server" Text='<%# Eval("DebtorID")%>' />
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <%--<asp:BoundField DataField="DebtorID" HeaderText="DebtorID" ItemStyle-HorizontalAlign="Center" ShowHeader="false"><ItemStyle CssClass="hide"/><HeaderStyle CssClass="hide"/></asp:BoundField>--%>

                                                                                                            <asp:BoundField DataField="Creditor" HeaderText="Ledger Name" HeaderStyle-BorderColor="Gray" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-Width="80px" />
                                                                                                            <%--<asp:BoundField DataField="CreditorID" HeaderText="CreditorID" HeaderStyle-BorderColor="blue" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-Width="30px" />--%>
                                                                                                            <asp:TemplateField Visible="false">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="lblCreditorID" runat="server" Text='<%# Eval("CreditorID")%>' />
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:BoundField DataField="Amount" HeaderText="Amount" HeaderStyle-BorderColor="Gray" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-Width="80px" />
                                                                                                            <asp:BoundField DataField="Narration" HeaderText="Narration" HeaderStyle-BorderColor="Gray" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-Width="80px" />
                                                                                                            <asp:BoundField DataField="VoucherType" HeaderText="Voucher Type" HeaderStyle-BorderColor="Gray" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-Width="50px" />
                                                                                                            <asp:BoundField DataField="ChequeNo" HeaderText="ChequeNo" HeaderStyle-BorderColor="Gray" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-Width="50px" />
                                                                                                            <asp:BoundField DataField="ReconcilatedBy" HeaderText="Reconcilated By" HeaderStyle-BorderColor="Gray" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-Width="50px" />
                                                                                                            <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-BorderColor="blue" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-Width="50px" Visible="False" />
                                                                                                            <%--<asp:TemplateField  Visible="false">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label id="lblStatus" runat ="server" Text='<%# Eval("Status")%>'/>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>--%>
                                                                                                            <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Reconciled Date" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="30px">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:TextBox ID="txtDate" Enabled="false" runat="server" Width="70px" Text='<%# Bind("Reconcilateddate") %>'></asp:TextBox>
                                                                                                                    <cc1:CalendarExtender ID="calBillDate" runat="server" Format="dd/MM/yyyy"
                                                                                                                        PopupButtonID="btnBillDate" TargetControlID="txtDate" Enabled="True">
                                                                                                                    </cc1:CalendarExtender>
                                                                                                                    <asp:ImageButton ID="btnBillDate" ImageUrl="App_Themes/NewTheme/images/cal.gif"
                                                                                                                        CausesValidation="False" Width="20px" runat="server" />
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Remarks" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="10px">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:TextBox ID="txtResult" runat="server" Width="100px" Text='<%# Bind("Result") %>'></asp:TextBox>
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
                                                                            <tr>
                                                                                <td colspan="4">
                                                                                    <table style="border: 0px solid #5078B3" cellpadding="0" cellspacing="2"
                                                                                        width="100%">
                                                                                        <tr>
                                                                                            <td style="width: 30%"></td>
                                                                                            <td style="width: 20%" align="right">
                                                                                                <asp:Button ID="cmdsave" runat="server" CssClass="savebutton1231"
                                                                                                    EnableTheming="false" OnClick="cmdsave_Click" Text="" />
                                                                                            </td>
                                                                                            <td style="width: 20%" align="left">
                                                                                                <asp:Button ID="cmdprodcancel" runat="server" Text="" CssClass="cancelbutton6" EnableTheming="false"
                                                                                                    Visible="true" OnClick="cmdprodcancel_Click" SkinID="skinBtnCancel" />
                                                                                            </td>
                                                                                            <td style="width: 30%"></td>
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

                    <tr style="width: 100%">
                        <td style="width: 90%; text-align: left">
                            <asp:Panel ID="PanelBill" Direction="LeftToRight" runat="server">
                                <table width="100%" style="text-align: left; margin: -6px 0px 0px 0px;">
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GrdViewPurchase" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                CssClass="someClass" Width="100.2%" DataKeyNames="TransNo" AllowPaging="True"
                                                EmptyDataText="No Details Found" OnPageIndexChanging="GrdViewPurchase_PageIndexChanging"
                                                OnRowCommand="GrdViewPurchase_RowCommand" OnRowEditing="GrdViewPurchase_RowEditing"
                                                OnSelectedIndexChanged="GrdViewPurchase_SelectedIndexChanged" OnRowCreated="GrdViewPurchase_RowCreated"
                                                OnRowDataBound="GrdViewPurchase_RowDataBound" OnRowDeleting="GrdViewPurchase_RowDeleting">
                                                <HeaderStyle Height="30px" HorizontalAlign="Center" Font-Bold="true" BackColor="#cccccc" BorderColor="Gray" Font-Size="Small" />
                                                <RowStyle Font-Bold="true" HorizontalAlign="Center" Height="30px" Font-Size="Small" ForeColor="#0567AE" />
                                                <Columns>
                                                    <%--<asp:BoundField DataField="ID" HeaderText="ID" HeaderStyle-Width="50px" HeaderStyle-Wrap="false" HeaderStyle-BorderColor="blue"/>
                                                    <asp:BoundField DataField="DateT" HeaderText="Date" HeaderStyle-Width="65px"  HeaderStyle-BorderColor="blue"/>
                                                    <asp:BoundField DataField="Ledger" HeaderStyle-Width="130px" HeaderText="Ledger Name"  HeaderStyle-BorderColor="blue"/>
                                                    <asp:BoundField DataField="Type" Visible="false" HeaderText="Types"  HeaderStyle-BorderColor="blue"/>
                                                    <asp:BoundField DataField="UserName" HeaderStyle-Width="130px" HeaderText="User Name"  HeaderStyle-BorderColor="blue"/>--%>
                                                    <asp:BoundField DataField="TransNo" HeaderText="TransNo" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="60px" />
                                                    <asp:BoundField DataField="RefNo" HeaderText="RefNo" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="50px" />
                                                    <asp:BoundField DataField="TransDate" HeaderText="Trans Date" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="60px" />
                                                    <asp:BoundField DataField="LedgerName" HeaderText="Credit" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="80px" />
                                                    <asp:BoundField DataField="debi" HeaderText="Debit" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="80px" />
                                                    <asp:BoundField DataField="Amount" HeaderText="Amount" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="80px" />
                                                    <asp:BoundField DataField="Narration" HeaderText="Narration" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="80px" />
                                                    <asp:BoundField DataField="VoucherType" HeaderText="Voucher Type" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="50px" />
                                                </Columns>
                                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                <PagerTemplate>
                                                    <table style="border-color: white">
                                                        <tr style="border-color: white">
                                                            <td style="border-color: white">Goto Page
                                                            </td>
                                                            <td style="border-color: white">
                                                                <asp:DropDownList ID="ddlPageSelector" BackColor="#e7e7e7" OnSelectedIndexChanged="ddlPageSelector_SelectedIndexChanged"
                                                                    runat="server" AutoPostBack="true" Width="80px" Height="24px" Style="border: 1px solid blue">
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
                                <asp:HiddenField ID="hdSMSRequired" runat="server" Value="NO" />
            <asp:HiddenField ID="hdEmailRequired" runat="server" Value="NO" />
                            </asp:Panel>
                            <asp:Label ID="err" runat="server" Style="color: Red; font-weight: bold; font-family: 'Trebuchet MS'; font-size: 11px;"
                                Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <table align="center" style="width: 100%">
        <tr>
            <td style="width: 33%"></td>
            <td style="width: 17%">
                <asp:Button ID="btnSale" runat="server"
                    CssClass="exportexl6" EnableTheming="false" CausesValidation="false"
                    OnClientClick="window.open('ReportXlBankRecon.aspx','CSTSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=310,width=500,left=425,top=220, scrollbars=yes');" Font-Bold="True" ForeColor="White"></asp:Button>
            </td>
            <td style="width: 17%">
                <asp:Button ID="Button1" runat="server"
                    CssClass="NewReport6" EnableTheming="false" CausesValidation="false"
                    OnClientClick="window.open('ReportXlBankRec.aspx','CSTSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=250,width=500,left=425,top=220, scrollbars=yes');" Font-Bold="True" ForeColor="White"></asp:Button>
            </td>
            <td style="width: 33%"></td>
        </tr>
    </table>
</asp:Content>
