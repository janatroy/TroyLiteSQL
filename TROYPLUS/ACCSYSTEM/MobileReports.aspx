<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MobileReports.aspx.cs" Inherits="MobileReports" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
        <br />
        <table align="center" width="98%" style="border: 1px solid #86b2d1; margin: 0 0 0 0px"
            cellpadding="5" cellspacing="5">
            <tr align="center">
                <td>
                    <asp:Button ID="BtnStockLedger" runat="server" Font-Size="Medium" Text="Stock Ledger Report"
                        Width="100%" Height="30px" Font-Bold="true" SkinID="skinButtonCol" OnClick="BtnStockLedger_Click" />
                </td>
            </tr>
            <tr align="center">
                <td>
                    <asp:Button ID="BtnLedger" runat="server" Font-Size="Medium" Text="Ledger Report"
                        Width="100%" Height="30px" Font-Bold="true" SkinID="skinButtonCol" OnClick="BtnLedger_Click" />
                </td>
            </tr>
            <tr align="center">
                <td>
                    <asp:Button ID="BtnSalesSummary" runat="server" Font-Size="Medium" Text="Sales Summary Report"
                        Width="100%" Height="30px" Font-Bold="true" SkinID="skinButtonCol" OnClick="BtnSalesSummary_Click" />
                </td>
            </tr>
            <tr align="center">
                <td>
                    <asp:Button ID="BtnPurchaseRpt" runat="server" Font-Size="Medium" Text="Purchase Summary Report"
                        Width="100%" Height="30px" Font-Bold="true" SkinID="skinButtonCol" OnClick="BtnPurchaseRpt_Click" />
                </td>
            </tr>
            <tr align="center">
                <td>
                    <asp:Button ID="BtnBack" runat="server" Font-Size="Medium" Text="BACK" Width="100%"
                        Height="30px" SkinID="skinButtonCol" OnClick="BtnBack_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
