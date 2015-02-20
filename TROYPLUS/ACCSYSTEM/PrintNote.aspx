<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintNote.aspx.cs" Inherits="PrintNote" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="App_Themes/DefaultTheme/DefaultTheme.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function CallPrint(strid) {
            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('');
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
        }
        function unl() {
            document.form1.submit();
        }

    </script>
</head>
<body style="font-size: 11px; font-family: 'Trebuchet MS';" onbeforeunload="unl()">
    <form id="form1" runat="server">
    <br />
    <div style="text-align: center" align="center">
        <input type="button" value="Print " id="btnPrint" runat="Server" onclick="javascript:CallPrint('divPrint')"
            class="button" />&nbsp;
        <br />
        <br />
        <div id="divPrint" style="font-family: 'Trebuchet MS'; font-size: 11px;" align="center">
            <table width="600px" border="0" cellpadding="0" cellspacing="0" style="font-family: 'Trebuchet MS';
                font-size: 11px;">
                <tr>
                    <td width="140px" align="left">
                        TIN#:
                        <asp:Label ID="lblTNGST" runat="server"></asp:Label>
                    </td>
                    <td align="center" width="320px">
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
                        <hr />
                    </td>
                    <td align="center">
                        <hr />
                    </td>
                    <td>
                        <hr />
                    </td>
                </tr>
            </table>
            <table width="600" border="0" cellpadding="3" cellspacing="5" style="font-family: 'Trebuchet MS';
                font-size: 11px;">
                <tr>
                    <td style="width: 30%" align="left">
                        <br />
                        <h5>
                            <asp:Label ID="lblNoteType" runat="server"></asp:Label>
                            <br />
                        </h5>
                    </td>
                    <td colspan="2" style="text-align: left">
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%" align="left">
                        Reference No
                    </td>
                    <td style="width: 30%" align="left">
                        <asp:Label ID="lblRefNo" runat="server"></asp:Label>
                    </td>
                    <td style="width: 30%">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%" align="left">
                        Date
                    </td>
                    <td style="width: 30%" align="left">
                        <asp:Label ID="lblNoteDate" runat="server"></asp:Label>
                    </td>
                    <td style="width: 30%">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%" align="left">
                        Ledger
                    </td>
                    <td style="width: 30%" align="left">
                        <asp:Label ID="lblLedger" runat="server"></asp:Label>
                    </td>
                    <td style="width: 30%">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%" align="left">
                        Amount
                    </td>
                    <td style="width: 30%" align="left">
                        <asp:Label ID="lblAmount" runat="server"></asp:Label>
                    </td>
                    <td style="width: 30%">
                        &nbsp;
                    </td>
                </tr>
                <tr id="rowBank" runat="server">
                    <td style="width: 30%" align="left">
                        Note
                    </td>
                    <td style="width: 30%" align="left">
                        <asp:Label ID="lblNote" runat="server"></asp:Label>
                    </td>
                    <td style="width: 30%">
                        &nbsp;
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <table width="600" border="0" cellpadding="2" cellspacing="0" style="font-family: 'Trebuchet MS';
                font-size: 11px;">
                <tr>
                    <td style="width: 30%" align="left">
                        <hr />
                    </td>
                    <td style="text-align: left; width: 40%">
                    </td>
                    <td style="width: 30%; text-align: right">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%" align="left">
                        Date
                    </td>
                    <td style="text-align: left; width: 40%">
                    </td>
                    <td style="text-align: right; width: 30%">
                        Authorized Signature
                    </td>
                </tr>
            </table>
            <br />
        </div>
    </div>
    </form>
</body>
</html>
