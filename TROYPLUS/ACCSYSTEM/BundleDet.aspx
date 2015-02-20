<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BundleDet.aspx.cs" Inherits="BundleDet" %>

<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divPrint" style="font-family: 'Trebuchet MS'; font-size: 11px;" runat="server">
        <br />
        <wc:ReportGridView runat="server" BorderWidth="1" ID="gvBundle" CssClass="gridview"
            GridLines="Both" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
            PrintPageSize="30" AllowPrintPaging="true" Width="100%" Style="font-family: 'Trebuchet MS';
            font-size: 11px;">
            <PageHeaderTemplate>
                <br />
                <br />
            </PageHeaderTemplate>
            <Columns>
                <asp:TemplateField HeaderText=" Coir " ItemStyle-Width="10%">
                    <ItemTemplate>
                        <asp:Label Visible="true" ID="lblCoir" runat="server" Text='<%# Eval("Coir")%>' />
                    </ItemTemplate>
                    <ItemStyle Width="10%"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Qty" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <asp:Label Visible="true" ID="lblcreationdate" runat="server" Text='<%# Eval("Qty")%>' />
                    </ItemTemplate>
                    <ItemStyle Width="10%"></ItemStyle>
                </asp:TemplateField>
            </Columns>
            <PagerTemplate>
            </PagerTemplate>
            <PageFooterTemplate>
            </PageFooterTemplate>
            <AlternatingRowStyle CssClass="even"></AlternatingRowStyle>
        </wc:ReportGridView>
    </div>
    </form>
</body>
</html>
