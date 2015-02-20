<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OutstandingBalance.aspx.cs"
    Inherits="OutstandingBalance" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>Outstanding Balance Report</title>
    <link href="App_Themes/DefaultTheme/DefaultTheme.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="background: white; margin: 20px 80px 20px 60px; text-align: center">
        <table width="60%" cellpadding="2" cellspacing="2">
            <tr>
                <td colspan="4" class="SectionHeader">
                    Outstanding Balance Report
                </td>
            </tr>
            <tr>
                <td align="right" width="20%" class="LMSLeftColumnColor">
                    Area:
                </td>
                <td width="20%">
                    <asp:DropDownList ID="drpArea" runat="server" SkinID="skinDdlBox" DataSourceID="srcArea"
                        DataTextField="area" DataValueField="area">
                        <asp:ListItem Value="0"> -- All Areas -- </asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td colspan="2" width="40%">
                    <asp:Button ID="btnPrint" runat="server" SkinID="skinButtonBig" OnClick="btnPrint_Click"
                        Text="Print Report" />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                </td>
            </tr>
        </table>
    </div>
    <asp:SqlDataSource ID="srcArea" runat="server" SelectCommand="SELECT [area] FROM [AreaMaster]"
        ProviderName="System.Data.OleDb"></asp:SqlDataSource>
    <asp:SqlDataSource ID="AccessDataSource2" runat="server" ProviderName="System.Data.OleDb"
        SelectCommand="SELECT DISTINCT [category] FROM [CustomerMaster]"></asp:SqlDataSource>
    </form>
</body>
</html>
