<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TroyPlusTool.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width:100%">
            <tr>
                <td style="width:10%"></td>
                <td style="width:20%">Key</td>
                <td style="width:70%"><asp:TextBox ID="txtKey" runat="server" Width="100%"></asp:TextBox></td>
                <td style="width:10%"></td>
            </tr>
             <tr>
                <td></td>
                <td>Encrypted Key</td>
                <td><asp:TextBox ID="txtEncryptedKey" runat="server" Width="100%"></asp:TextBox></td>
                 <td></td>
            </tr>
             <tr>
                <td></td>
                <td><asp:Button ID="btnGenerate" runat="server" Text="Generate" OnClick="btnGenerate_Click" /></td>
                <td></td>
                 <td></td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
