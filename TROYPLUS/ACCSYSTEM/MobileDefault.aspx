<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MobileDefault.aspx.cs" Inherits="MobileDefault" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home</title>
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
                    <asp:Button ID="BtnPurchase" runat="server" Font-Size="Medium" Text="PURCHASE" Width="100%"
                        Height="30px" Font-Bold="true" SkinID="skinButtonCol" OnClick="BtnPurchase_Click" />
                </td>
            </tr>
            <tr align="center">
                <td>
                    <asp:Button ID="BtnSales" runat="server" Font-Size="Medium" Text="SALES" Width="100%"
                        Height="30px" Font-Bold="true" SkinID="skinButtonCol" OnClick="lnkBtnSales_Click" />
                </td>
            </tr>
            <tr align="center">
                <td>
                    <asp:Button ID="lnkBtnPayment" runat="server" Font-Size="Medium" Text="PAYMENT" Width="100%"
                        Height="30px" Font-Bold="true" SkinID="skinButtonCol" OnClick="lnkBtnPayment_Click" />
                </td>
            </tr>
            <tr align="center">
                <td>
                    <asp:Button ID="lnkBtnReceipts" runat="server" Font-Size="Medium" Text="RECEIPT"
                        Width="100%" Height="30px" Font-Bold="true" SkinID="skinButtonCol" OnClick="lnkBtnReceipts_Click" />
                </td>
            </tr>
            <tr align="center">
                <td>
                    <asp:Button ID="lnkBtnReport" runat="server" Font-Size="Medium" Text="REPORTS" Width="100%"
                        Height="30px" Font-Bold="true" SkinID="skinButtonCol" OnClick="lnkBtnReports_Click" />
                </td>
            </tr>
            <tr align="center">
                <td>
                    <asp:Button ID="lnkLogout" runat="server" Font-Size="Medium" Text="LOGOUT" Width="100%"
                        Height="30px" SkinID="skinButtonCol" OnClick="lnkBtnLogout_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
