<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="DeleteItems.aspx.cs" Inherits="DeleteItems" Title="Others > Delete Transactions" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <table style="width: 100%;" align="center">
        <tr>
            <td style="width: 918px;">
                <span>
                    <asp:RadioButtonList ID="rdoType" runat="server" RepeatDirection="Horizontal" AutoPostBack="true"
                        OnSelectedIndexChanged="rdoType_SelectedIndexChanged" CssClass="lblFont">
                        <asp:ListItem Text="Payment" Value="Payment"></asp:ListItem>
                        <asp:ListItem Text="Receipt" Value="Receipt"></asp:ListItem>
                        <asp:ListItem Text="Journal" Value="Journal"></asp:ListItem>
                        <asp:ListItem Text="Purchase" Value="Purchase"></asp:ListItem>
                        <asp:ListItem Text="Sales" Value="Sales"></asp:ListItem>
                    </asp:RadioButtonList>
                </span>
                <br />
                <asp:Label ID="lblErr" CssClass="errorMsg" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr style="width: 100%">
            <td style="width: 100%">
                <div id="divPayment" runat="server" visible="false">
                    <table style="width: 100%;" align="center" cellpadding="3" cellspacing="3" style="border: 1px solid #5078B3">
                        <tr>
                            <td colspan="4" align="left">
                                <div class="mainConHd">
                                    <table cellspacing="0" cellpadding="0" border="0">
                                        <tr valign="middle">
                                            <td>
                                                <span>Search Payment </span>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr style="height: 25px">
                            <td style="width: 25%">
                                Ref. No. / Pay To *
                                <asp:RequiredFieldValidator ID="rvSearch" runat="server" ControlToValidate="txtPaymentSearch"
                                    Display="Dynamic" EnableClientScript="False" Enabled="false">Search is mandatory</asp:RequiredFieldValidator>
                            </td>
                            <td style="width: 25%">
                                <asp:TextBox ID="txtPaymentSearch" runat="server" SkinID="skinTxtBoxGrid"></asp:TextBox>
                            </td>
                            <td style="width: 25%">
                                <asp:DropDownList ID="ddPaymentCriteria" runat="server" Width="100%">
                                    <asp:ListItem Value="0">-- All --</asp:ListItem>
                                    <asp:ListItem Value="RefNo">Ref. No.</asp:ListItem>
                                    <asp:ListItem Value="TransDate">Transaction Date</asp:ListItem>
                                    <asp:ListItem Value="LedgerName">Paid To</asp:ListItem>
                                    <asp:ListItem Value="Narration">Narration</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="width: 25%">
                                <asp:Button ID="btnPaymentSearch" runat="server" Text="Search" SkinID="skinBtnSearch"
                                    OnClick="btnPaymentSearch_Click" />
                            </td>
                        </tr>
                        <tr style="height: 25px">
                            <td style="width: 25%">
                                Deleted : ?
                            </td>
                            <td style="width: 25%" class="tblLeft">
                                <asp:CheckBox ID="ChkPayDel" CssClass="chkContent" runat="server" Text="Yes" />
                            </td>
                            <td style="width: 25%">
                                &nbsp;
                            </td>
                            <td style="width: 25%">
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divReceipt" runat="server" visible="false">
                    <table style="width: 100%;" align="center" cellpadding="3" cellspacing="3" class="accordionContent"
                        style="border: 1px solid #5078B3">
                        <tr>
                            <td colspan="4" align="left">
                                <div class="mainConHd">
                                    <table cellspacing="0" cellpadding="0" border="0">
                                        <tr valign="middle">
                                            <td>
                                                <span>Search Receipts </span>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr style="height: 25px">
                            <td style="width: 25%">
                                Ref. No. / Received From *
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtReceiptSearch"
                                    Display="Dynamic" EnableClientScript="False" Enabled="false">Search is mandatory</asp:RequiredFieldValidator>
                            </td>
                            <td style="width: 25%">
                                <asp:TextBox ID="txtReceiptSearch" runat="server" SkinID="skinTxtBoxGrid"></asp:TextBox>
                            </td>
                            <td style="width: 25%">
                                <asp:DropDownList ID="ddReceiptCriteria" runat="server" Width="100%">
                                    <asp:ListItem Value="0">-- All --</asp:ListItem>
                                    <asp:ListItem Value="RefNo">Ref. No.</asp:ListItem>
                                    <asp:ListItem Value="TransDate">Transaction Date</asp:ListItem>
                                    <asp:ListItem Value="LedgerName">Received From</asp:ListItem>
                                    <asp:ListItem Value="Narration">Narration</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="width: 25%">
                                <asp:Button ID="btnReceiptSearch" runat="server" Text="Search" SkinID="skinBtnSearch"
                                    OnClick="btnReceiptSearch_Click" />
                            </td>
                        </tr>
                        <tr style="height: 25px">
                            <td style="width: 25%">
                                Deleted : ?
                            </td>
                            <td style="width: 25%" class="tblLeft">
                                <asp:CheckBox ID="ChkReceipt" CssClass="chkContent" runat="server" Text="Yes" />
                            </td>
                            <td style="width: 25%">
                                &nbsp;
                            </td>
                            <td style="width: 25%">
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divJournal" visible="false" runat="server">
                    <table width="100%" style="border: 1px solid #5078B3" border="0" cellpadding="3"
                        cellspacing="3" class="accordionContent">
                        <tr>
                            <td colspan="4" align="left">
                                <div class="mainConHd">
                                    <table cellspacing="0" cellpadding="0" border="0">
                                        <tr valign="middle">
                                            <td>
                                                <span>Search Journals </span>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 25%">
                                Ref. No.
                                <cc1:FilteredTextBoxExtender ID="ftbe" runat="server" TargetControlID="txtRefno"
                                    FilterType="Numbers" />
                            </td>
                            <td style="width: 25%" class="tblLeft">
                                <asp:TextBox ID="txtRefno" runat="server" SkinID="skinTxtBoxGrid"></asp:TextBox>
                            </td>
                            <td style="width: 25%">
                                Narration:
                            </td>
                            <td style="width: 25%" class="tblLeft">
                                <asp:TextBox ID="txtNaration" runat="server" SkinID="skinTxtBoxGrid"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 25%">
                                Date:
                            </td>
                            <td style="width: 25%" class="tblLeft">
                                <asp:TextBox ID="txtDate" runat="server" CssClass="cssTextBox" Width="100px" MaxLength="10"></asp:TextBox>
                                <script type="text/javascript" language="JavaScript">                                    new tcal({ 'formname': 'aspnetForm', 'controlname': 'ctl00$cplhControlPanel$txtDate' });</script>
                            </td>
                            <td style="width: 25%">
                                Deleted : ?
                            </td>
                            <td style="width: 25%" class="tblLeft">
                                <asp:CheckBox ID="ChkJournal" CssClass="chkContent" runat="server" Text="Yes" />
                            </td>
                        </tr>
                        <tr style="height: 25px">
                            <td style="width: 10%" class="tblLeft" colspan="2">
                                <asp:Button ID="btnJournalSearch" runat="server" OnClick="btnJournalSearch_Click"
                                    Text="Search" SkinID="skinBtnSearch" />
                            </td>
                            <td style="width: 20%; text-align: left">
                            </td>
                            <td style="width: 70%" colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divPurchase" runat="server" visible="false">
                    <table style="width: 100%;" align="center" cellpadding="3" cellspacing="3" class="accordionContent"
                        style="border: 1px solid #5078B3">
                        <tr>
                            <td colspan="5" align="left">
                                <div class="mainConHd">
                                    <table cellspacing="0" cellpadding="0" border="0">
                                        <tr valign="middle">
                                            <td>
                                                <span>Search Purchase </span>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 15%">
                                Bill No.
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtBillnoPurchaseSearch"
                                    FilterType="Numbers" />
                            </td>
                            <td style="width: 20%">
                                <asp:TextBox ValidationGroup="search" ID="txtBillnoPurchaseSearch" CssClass="cssTextBox"
                                    Width="80px" runat="server" MaxLength="8"></asp:TextBox>
                            </td>
                            <td style="width: 15%">
                                Deleted : ?
                            </td>
                            <td style="width: 10%" class="tblLeft">
                                <asp:CheckBox ID="ChkPurchase" CssClass="chkContent" runat="server" Text="Yes" />
                            </td>
                            <td style="width: 40%">
                                <asp:Button ValidationGroup="search" ID="btnPurchaseSearch" OnClick="btnPurchaseSearch_Click"
                                    runat="server" SkinID="skinBtnSearch" Text="Search" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divSales" runat="server" visible="false">
                    <table style="width: 100%; border: 1px solid #5078B3" cellpadding="3" cellspacing="3"
                        class="accordionContent">
                        <tr>
                            <td colspan="5">
                                <div class="mainConHd">
                                    <table cellspacing="0" cellpadding="0" border="0">
                                        <tr valign="middle">
                                            <td>
                                                <span>Search Sales </span>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 10%">
                                Bill No.
                            </td>
                            <td style="width: 15%" class="tblLeft">
                                <asp:TextBox ValidationGroup="search" ID="txtBillnoSalesSearch" CssClass="cssTextBox"
                                    Width="80px" runat="server" MaxLength="8"></asp:TextBox>
                            </td>
                            <td style="width: 10%" class="tblLeft">
                                Deleted : ?
                            </td>
                            <td style="width: 10%" class="tblLeft">
                                <asp:CheckBox ID="ChkSales" CssClass="chkContent" runat="server" Text="Yes" />
                            </td>
                            <td style="width: 50%; vertical-align: middle" class="tblLeft">
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender221" runat="server" TargetControlID="txtBillnoSalesSearch"
                                    FilterType="Numbers" />
                                <asp:Button ValidationGroup="search" ID="btnSalesSearch" OnClick="btnSalesSearch_Click"
                                    runat="server" SkinID="skinBtnSearch" Text="Search" />
                            </td>
                        </tr>
                    </table>
                </div>
                <br />
            </td>
        </tr>
        <tr style="width: 100%">
            <td style="width: 100%">
                <br />
                <div id="divPaymentResult" runat="server" visible="false">
                    <asp:GridView ID="GrdViewPayment" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        Width="100%" AllowPaging="True" DataKeyNames="TransNo" PageSize="10" OnSelectedIndexChanged="GrdViewPayment_SelectedIndexChanged"
                        OnRowDeleting="GrdViewPayment_RowDeleting" OnPageIndexChanging="GrdViewPayment_PageIndexChanging">
                        <EmptyDataRowStyle CssClass="GrdContent" />
                        <Columns>
                            <asp:BoundField DataField="RefNo" HeaderText="Ref. No." HeaderStyle-Wrap="false" />
                            <asp:BoundField DataField="TransDate" HeaderText="Transaction Date" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="Debi" HeaderText="Paid To" />
                            <asp:BoundField DataField="LedgerName" HeaderText="Bank Name / Cash" />
                            <asp:BoundField DataField="Paymode" HeaderText="Payment Mode" />
                            <asp:BoundField DataField="ChequeNo" HeaderText="Cheque No." />
                            <asp:BoundField DataField="Amount" HeaderText="Amount" />
                            <asp:BoundField DataField="Narration" HeaderText="Narration" />
                            <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="25px">
                                <ItemTemplate>
                                    <cc1:ConfirmButtonExtender ID="CnrfmRestore" TargetControlID="btnEdit" ConfirmText="Are you sure to Restore this Payment?"
                                        runat="server">
                                    </cc1:ConfirmButtonExtender>
                                    <asp:ImageButton ID="btnEdit" runat="server" SkinID="Restore" CommandName="Select" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="25px">
                                <ItemTemplate>
                                    <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure want to hide this Payment?"
                                        runat="server">
                                    </cc1:ConfirmButtonExtender>
                                    <asp:ImageButton ID="lnkB" SkinID="SoftDelete" runat="Server" CommandName="Delete">
                                    </asp:ImageButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div id="divReceiptResult" runat="server" visible="false">
                    <asp:GridView ID="GrdViewReceipt" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        Width="100%" OnSelectedIndexChanged="GrdViewReceipt_SelectedIndexChanged" OnRowDeleting="GrdViewReceipt_RowDeleting"
                        OnPageIndexChanging="GrdViewReceipt_PageIndexChanging" AllowPaging="True" DataKeyNames="TransNo"
                        PageSize="10">
                        <EmptyDataRowStyle CssClass="GrdContent" />
                        <Columns>
                            <asp:BoundField DataField="RefNo" HeaderText="Ref. No." HeaderStyle-Wrap="false" />
                            <asp:BoundField DataField="TransDate" HeaderText="Transaction Date" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="LedgerName" HeaderText="Received From" />
                            <asp:BoundField DataField="Debi" HeaderText="Bank Name / Cash" />
                            <asp:BoundField DataField="Paymode" HeaderText="Payment Mode" />
                            <asp:BoundField DataField="ChequeNo" HeaderText="Cheque No." />
                            <asp:BoundField DataField="Amount" HeaderText="Amount" />
                            <asp:BoundField DataField="Narration" HeaderText="Narration" />
                            <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="25px">
                                <ItemTemplate>
                                    <cc1:ConfirmButtonExtender ID="CnrfmRestore" TargetControlID="btnEdit" ConfirmText="Are you sure to Restore this Receipt?"
                                        runat="server">
                                    </cc1:ConfirmButtonExtender>
                                    <asp:ImageButton ID="btnEdit" runat="server" SkinID="Restore" CommandName="Select" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="25px">
                                <ItemTemplate>
                                    <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure want to hide this Receipt?"
                                        runat="server">
                                    </cc1:ConfirmButtonExtender>
                                    <asp:ImageButton ID="lnkB" SkinID="SoftDelete" runat="Server" CommandName="Delete">
                                    </asp:ImageButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div id="divJournalResult" runat="server" visible="false">
                    <asp:GridView ID="GrdViewJournal" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        Width="100%" DataKeyNames="TransNo" AllowPaging="True" OnPageIndexChanging="GrdViewJournal_PageIndexChanging"
                        OnSelectedIndexChanged="GrdViewJournal_SelectedIndexChanged" OnRowDeleting="GrdViewJournal_RowDeleting"
                        PageSize="10">
                        <EmptyDataRowStyle CssClass="GrdContent" />
                        <Columns>
                            <asp:BoundField DataField="Refno" HeaderText="Ref. No." HeaderStyle-Wrap="false" />
                            <asp:BoundField DataField="TransDate" HeaderText="Date" />
                            <asp:BoundField DataField="Debi" HeaderText="Debtor" />
                            <asp:BoundField DataField="Cred" HeaderText="Creditor" />
                            <asp:BoundField DataField="Amount" HeaderText="Amount" />
                            <asp:BoundField DataField="Narration" HeaderText="Narration" />
                            <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="25px">
                                <ItemTemplate>
                                    <cc1:ConfirmButtonExtender ID="CnrfmRestore" TargetControlID="btnEdit" ConfirmText="Are you sure to Restore this Journal?"
                                        runat="server">
                                    </cc1:ConfirmButtonExtender>
                                    <asp:ImageButton ID="btnEdit" runat="server" SkinID="Restore" CommandName="Select" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="25px">
                                <ItemTemplate>
                                    <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure want to hide this Journal?"
                                        runat="server">
                                    </cc1:ConfirmButtonExtender>
                                    <asp:ImageButton ID="lnkB" SkinID="SoftDelete" runat="Server" CommandName="Delete">
                                    </asp:ImageButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataRowStyle CssClass="GrdContent" />
                    </asp:GridView>
                </div>
                <div id="divPurchaseResult" runat="server" visible="false">
                    <asp:GridView ID="GrdViewPurchase" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        Width="100%" DataKeyNames="PurchaseID" AllowPaging="True" OnPageIndexChanging="GrdViewPurchase_PageIndexChanging"
                        OnSelectedIndexChanged="GrdViewPurchase_SelectedIndexChanged" OnRowDeleting="GrdViewPurchase_RowDeleting"
                        PageSize="10">
                        <EmptyDataRowStyle CssClass="GrdContent" />
                        <Columns>
                            <asp:BoundField DataField="PurchaseID" Visible="false" />
                            <asp:BoundField DataField="Billno" HeaderText="Bill No." />
                            <asp:BoundField DataField="BillDate" HeaderText="Date" />
                            <asp:TemplateField HeaderText="Paymode">
                                <ItemTemplate>
                                    <asp:Label ID="lblPaymode" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="SupplierID" HeaderText="Supplier ID" />
                            <asp:BoundField DataField="Chequeno" HeaderText="Chequeno" />
                            <asp:BoundField DataField="Creditor" HeaderText="Creditor" />
                            <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="{0:F2}" />
                            <asp:BoundField DataField="SalesReturn" HeaderText="Sales Return" />
                            <asp:BoundField DataField="SalesReturnReason" HeaderText="Sales Return - Reason" />
                            <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="25px">
                                <ItemTemplate>
                                    <cc1:ConfirmButtonExtender ID="CnrfmRestore" TargetControlID="btnEdit" ConfirmText="Are you sure to Restore this Purchase?"
                                        runat="server">
                                    </cc1:ConfirmButtonExtender>
                                    <asp:ImageButton ID="btnEdit" runat="server" SkinID="Restore" CommandName="Select" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="25px">
                                <ItemTemplate>
                                    <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure want to hide this Purchase?"
                                        runat="server">
                                    </cc1:ConfirmButtonExtender>
                                    <asp:ImageButton ID="lnkB" SkinID="SoftDelete" runat="Server" CommandName="Delete">
                                    </asp:ImageButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataRowStyle CssClass="GrdContent" />
                    </asp:GridView>
                </div>
                <div id="divSalesResult" runat="server" visible="false">
                    <asp:GridView ID="GrdViewSales" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        Width="100%" DataKeyNames="Billno" AllowPaging="True" OnPageIndexChanging="GrdViewSales_PageIndexChanging"
                        OnSelectedIndexChanged="GrdViewSales_SelectedIndexChanged" OnRowDeleting="GrdViewSales_RowDeleting">
                        <EmptyDataRowStyle CssClass="GrdContent" />
                        <Columns>
                            <asp:BoundField DataField="Billno" HeaderText="Bill No." HeaderStyle-Wrap="false" />
                            <asp:BoundField DataField="BillDate" HeaderText="Date" />
                            <asp:BoundField DataField="CustomerName" HeaderText="CustomerName" />
                            <asp:BoundField DataField="CustomerAddress" HeaderText="Address" />
                            <asp:TemplateField HeaderText="Paymode">
                                <ItemTemplate>
                                    <asp:Label ID="lblPaymode" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="{0:F2}" />
                            <asp:BoundField DataField="CreditCardNo" HeaderText="Card No." />
                            <asp:BoundField DataField="Debtor" HeaderText="Ledger Name" />
                            <asp:BoundField DataField="PurchaseReturn" HeaderText="Purchase Return" />
                            <asp:BoundField DataField="PurchaseReturnReason" HeaderText="Reason" />
                            <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="25px">
                                <ItemTemplate>
                                    <cc1:ConfirmButtonExtender ID="CnrfmRestore" TargetControlID="btnEdit" ConfirmText="Are you sure to Restore this Sales?"
                                        runat="server">
                                    </cc1:ConfirmButtonExtender>
                                    <asp:ImageButton ID="btnEdit" runat="server" SkinID="Restore" CommandName="Select" />
                                </ItemTemplate>
                                <ItemStyle CssClass="command"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="25px">
                                <ItemTemplate>
                                    <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure want to hide this Sales?"
                                        runat="server">
                                    </cc1:ConfirmButtonExtender>
                                    <asp:ImageButton ID="lnkB" SkinID="SoftDelete" runat="Server" CommandName="Delete">
                                    </asp:ImageButton>
                                </ItemTemplate>
                                <ItemStyle CssClass="command"></ItemStyle>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </td>
        </tr>
    </table>
    <input type="hidden" id="hidAdvancedState" runat="server" />
    <br />
</asp:Content>
