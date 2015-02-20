<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Service.aspx.cs" Inherits="Service" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Service Entries</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <link href="App_Themes/NewTheme/base.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 700px; overflow-x: hidden; text-align: center">
        <div class="mainConDiv" style="width: 650px">
            <div class="mainConBody">
                <div class="shadow1">
                    &nbsp;
                </div>
                <div>
                    <table style="width: 100%;" align="center" cellpadding="3" cellspacing="2" style="border: 0px solid #5078B3">
                        <tr>
                            <td style="width: 25%; padding-left: 50px" align="left">
                                Search Service *
                            </td>
                            <td style="width: 25%">
                                <asp:TextBox ID="txtSearch" runat="server" Width="140px" CssClass="cssTextBox"></asp:TextBox>
                            </td>
                            <td style="width: 25%">
                                <div style="border-width: 1px; border-color: #bce1fe; border-style: solid; width: 98%;
                                    font-family: 'Trebuchet MS';">
                                    <asp:DropDownList ID="ddCriteria" runat="server" CssClass="cssDD" Width="100%">
                                        <asp:ListItem Value="0" style="background-color: #bce1fe">All Entries</asp:ListItem>
                                        <asp:ListItem Value="RefNumber">Ref. No.</asp:ListItem>
                                        <asp:ListItem Value="Details">Details</asp:ListItem>
                                        <asp:ListItem Value="Ledger">Customer / Dealer</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </td>
                            <td style="width: 25%" class="tblLeft">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" SkinID="skinBtnSearch" OnClick="btnSearch_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <br />
        <br />
        <asp:GridView ID="GrdViewSerEntry" runat="server" SkinID="GrdNoPaging" AllowSorting="True"
            AutoGenerateColumns="False" PageSize="100" OnRowCreated="GrdViewSerEntry_RowCreated"
            Width="99%" DataSourceID="GridSource" AllowPaging="True" DataKeyNames="ServiceID"
            EmptyDataText="No Service Entry found!" OnRowCommand="GrdViewSerEntry_RowCommand"
            OnSelectedIndexChanged="GrdViewSerEntry_SelectedIndexChanged" OnRowDataBound="GrdViewSerEntry_RowDataBound">
            <EmptyDataRowStyle CssClass="GrdContent" />
            <Columns>
                <asp:BoundField DataField="ServiceID" HeaderText="ServiceID" HeaderStyle-Wrap="false" />
                <asp:BoundField DataField="RefNumber" HeaderText="Ref. No." HeaderStyle-Wrap="false" />
                <asp:BoundField DataField="LedgerName" HeaderText="Customer / Dealer" HeaderStyle-Wrap="false" />
                <asp:BoundField DataField="Frequency" HeaderText="Frequency" HeaderStyle-Wrap="false" />
                <asp:BoundField DataField="StartDate" HeaderText="StartDate" DataFormatString="{0:dd/MM/yyyy}" />
                <asp:BoundField DataField="EndDate" HeaderText="EndDate" DataFormatString="{0:dd/MM/yyyy}" />
                <asp:BoundField DataField="Amount" HeaderText="Amount" />
                <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="25px">
                    <ItemTemplate>
                        <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Select" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="false">
                    <ItemTemplate>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataRowStyle CssClass="GrdContent" />
            <PagerTemplate>
                Goto Page
                <asp:DropDownList ID="ddlPageSelector" runat="server" SkinID="skinPagerDdlBox">
                </asp:DropDownList>
                <asp:Button Text="First" CommandName="Page" CommandArgument="First" runat="server"
                    ID="btnFirst" />
                <asp:Button Text="Previous" CommandName="Page" CommandArgument="Prev" runat="server"
                    ID="btnPrevious" />
                <asp:Button Text="Next" CommandName="Page" CommandArgument="Next" runat="server"
                    ID="btnNext" />
                <asp:Button Text="Last" CommandName="Page" CommandArgument="Last" runat="server"
                    ID="btnLast" />
            </PagerTemplate>
        </asp:GridView>
        <asp:ObjectDataSource ID="GridSource" runat="server" SelectMethod="ListServiceEntries"
            TypeName="BusinessLogic"></asp:ObjectDataSource>
        <asp:HiddenField ID="hdRefNo" runat="server" Value="0" />
        <asp:HiddenField ID="hdDueDate" runat="server" />
        <asp:HiddenField ID="hdCustomerID" runat="server" />
    </div>
    </form>
</body>
</html>
