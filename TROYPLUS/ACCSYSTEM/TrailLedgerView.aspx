<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="TrailLedgerView.aspx.cs" Inherits="TrailLedgerView" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <asp:HiddenField ID="hdFilename" runat="server" />
    <h5>
        Ledgers List for the Group -
        <asp:Label ID="lGroupName" runat="server" CssClass="accordionHeaderSelected"></asp:Label>
        &nbsp;<a href="javascript:history.go(-1)" style="text-decoration: none">[Go Back]</a></h5>
    <asp:GridView runat="server" BorderWidth="1" ID="gvTrailBalance" GridLines="Both"
        AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false" PageSize="50"
        ShowFooter="true" CssClass="gridview" Width="100%" Style="font-family: 'Trebuchet MS';
        font-size: 11px;" AllowPaging="true" OnPageIndexChanging="gvTrailBalance_PageIndexChanging"
        OnRowDataBound="gvTrailBalance_RowDataBound" FooterStyle-CssClass="lblFont">
        <Columns>
            <asp:TemplateField HeaderText="Particulars" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblparticulars"
                        runat="server" Text='<%# Eval("Particulars") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Debit" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblDebit" runat="server"
                        Text='<%# Eval("Debit","{0:f2}") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Credit" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblCredit" runat="server"
                        Text='<%# Eval("Credit","{0:f2}") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <table width="90%" cellpadding="3" cellspacing="3" class="lblFont">
        <tr>
            <td width="60%">
                &nbsp;
            </td>
            <td width="20%" align="right">
                <asp:Label CssClass="tblLeft" ID="lblDebitTotal" runat="server"></asp:Label>
            </td>
            <td width="20%" align="right">
                <asp:Label CssClass="tblLeft" ID="lblCreditTotal" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
