<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Stock.aspx.cs" Inherits="Stock"
    Title="Stock Page" %>

<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Stock Ledger Report</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <link href="App_Themes/DefaultTheme/DefaultTheme.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/DefaultTheme/calendar.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="Scripts/calendar_eu.js"></script>
    <script type="text/javascript">
        function CallPrint(strid) {
            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('', '', 'letf=0,top=0,width=600,height=400,toolbar=0,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();

        }
 
    </script>
</head>
<body style="font-family: 'Trebuchet MS'; font-size: 11px;">
    <form id="form1" runat="server">
    <table>
        <tr>
            <td colspan="4" align="center" class="mainConHd">
                Stock Report
            </td>
        </tr>
        <tr>
            <td class="ControlLabel">
                Brand
            </td>
            <td style="text-align: left">
                <asp:DropDownList ID="ddlBrand" runat="server">
                </asp:DropDownList>
            </td>
            <td class="ControlLabel">
                Category
            </td>
            <td style="text-align: left">
                <asp:DropDownList ID="ddlCategory" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="ControlLabel">
                Model
            </td>
            <td style="text-align: left">
                <asp:DropDownList ID="ddlMdl" runat="server">
                </asp:DropDownList>
            </td>
            <td class="ControlLabel">
                Product Name
            </td>
            <td style="text-align: left">
                <asp:DropDownList ID="ddlPrdctNme" runat="server" CssClass="textbox">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="ControlLabel">
                Stock
            </td>
            <td style="text-align: left">
                <asp:DropDownList ID="ddlStock" runat="server" CssClass="textbox">
                    <asp:ListItem>All</asp:ListItem>
                    <asp:ListItem Value="GT 0">&gt; 0</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ControlLabel">
                First Level
            </td>
            <td style="text-align: left">
                <asp:DropDownList ID="ddlfirstlvl" runat="server" CssClass="textbox" OnSelectedIndexChanged="ddlfirstlvl_SelectedIndexChanged"
                    AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td class="ControlLabel">
                Second Level
            </td>
            <td style="text-align: left">
                <asp:DropDownList ID="ddlsecondlvl" runat="server" AutoPostBack="True" CssClass="textbox">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="ControlLabel">
                Third Level
            </td>
            <td style="text-align: left">
                <asp:DropDownList ID="ddlthirdlvl" runat="server" CssClass="textbox">
                </asp:DropDownList>
            </td>
            <td class="ControlLabel">
                Four Level
            </td>
            <td style="text-align: left">
                <asp:DropDownList ID="ddlfourlvl" runat="server" CssClass="textbox">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                <asp:Button ID="btnData" runat="server" OnClick="btnData_Click" Text="GriData" />
                <asp:Button ID="btnxls" runat="server" OnClick="btnxls_Click" Text="Export to Excel" />
                <asp:Button ID="btnLevls" runat="server" OnClick="btnLevls_Click" Text="ClickLevel" />
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:GridView ID="GridStock" runat="server" BackColor="White" BorderColor="White"
                    BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" Font-Size="Small"
                    ShowFooter="True" AutoGenerateColumns="false" OnRowDataBound="GridStock_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Brand">
                            <ItemTemplate>
                                <asp:Label ID="lblRefno" runat="server" Text='<%# Eval("Brand") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CategoryID">
                            <ItemTemplate>
                                <asp:Label ID="lblCatid" runat="server" Text='<%# Eval("CategoryID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Model">
                            <ItemTemplate>
                                <asp:Label ID="lblModel" runat="server" Text='<%# Eval("Model") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ProductName">
                            <ItemTemplate>
                                <asp:Label ID="lblPName" runat="server" Text='<%# Eval("ProductName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ItemCode">
                            <ItemTemplate>
                                <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("ItemCode") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                Total :
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Quantity">
                            <ItemTemplate>
                                <asp:Label ID="lblQty" runat="server" Text='<%# Eval("Quantity") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblTotalQty" runat="server"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rate">
                            <ItemTemplate>
                                <asp:Label ID="lblRate" runat="server" Text='<%# Eval("Rate") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblTotalRate" runat="server"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Value">
                            <ItemTemplate>
                                <asp:Label ID="lblVaule" runat="server" Text='<%# Eval("Vaule") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblTotalValue" runat="server"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                    <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                    <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                    <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
                </asp:GridView>
                &nbsp;
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
