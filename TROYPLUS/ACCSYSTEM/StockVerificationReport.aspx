<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StockVerificationReport.aspx.cs"
    Inherits="StockVerificationReport" %>

<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Stock Verification Report</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <link href="App_Themes/DefaultTheme/DefaultTheme.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <%--OnPageIndexChanging="GrdViewSales_PageIndexChanging" --%>
        <br />
        <h3>
            Stock Verification Report</h3>
        <br />
        <div id="divPrint" style="font-family: 'Trebuchet MS'; font-size: 11px;" visible="false">
            <table width="600px" border="0" style="font-family: Trebuchet MS; font-size: 14px; background-color:White;">
                <tr>
                    <td width="140px" align="left">
                        TIN#:
                        <asp:Label ID="lblTNGST" runat="server"></asp:Label>
                    </td>
                    <td align="center" width="320px" style="font-size: 20px;">
                        <asp:Label ID="lblCompany" runat="server"></asp:Label>
                    </td>
                    <td width="140px" align="left">
                        Ph:
                        <asp:Label ID="lblPhone" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        GST#:
                        <asp:Label ID="lblGSTno" runat="server"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:Label ID="lblAddress" runat="server"></asp:Label>
                    </td>
                    <td align="left">
                        Date:
                        <asp:Label ID="lblBillDate" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td align="center">
                        <asp:Label ID="lblCity" runat="server" />
                        -
                        <asp:Label ID="lblPincode" runat="server"></asp:Label>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td align="center">
                        <asp:Label ID="lblState" runat="server"> </asp:Label>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td align="center">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
            <asp:GridView ID="GrdViewItems" runat="server" AllowPaging="true" AutoGenerateColumns="False" Width="600px"
                DataKeyNames="Itemcode" PageSize="100" OnRowDataBound="GrdViewItems_RowDataBound"
                EmptyDataText="Stock is verified successfully - No unmatched stock found" ShowHeader="true" OnPageIndexChanging="GrdViewItems_PageIndexChanging">
                <EmptyDataRowStyle CssClass="GrdContent" />
                <Columns>
                    <asp:BoundField DataField="itemcode" HeaderText="Product Code" />
                    <asp:BoundField DataField="Product" HeaderText="Product Name" />
                    <asp:BoundField DataField="Stock" HeaderText="Stock in Hand " />
                    <asp:BoundField DataField="ArrStock" HeaderText="Arrived Stock" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
    </form>
</body>
</html>
