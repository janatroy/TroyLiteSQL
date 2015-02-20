<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintReceipt.aspx.cs" Inherits="PrintReceipt" %>

<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Print Preview - Receipt Report</title>
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
                <tr>
                    <td colspan="3" style="text-align: left">
                        <br />
                        <h5>
                            Receipt<br />
                        </h5>
                    </td>
                </tr>
            </table>
            <br />
            <table width="600" border="0" cellpadding="0" cellspacing="2" style="font-family: 'Trebuchet MS';
                font-size: 11px;">
                <tr>
                    <td style="width: 30%" align="left">
                        &nbsp;
                    </td>
                    <td style="width: 30%" align="left">
                        &nbsp;
                    </td>
                    <td style="width: 10%">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%" align="left">
                        Reference No
                    </td>
                    <td style="width: 30%" align="left">
                        <asp:Label ID="lblRefNo" runat="server"></asp:Label>
                    </td>
                    <td style="width: 10%">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%" align="left">
                        Received From
                    </td>
                    <td style="width: 30%" align="left">
                        <asp:Label ID="lblReceivedFrom" runat="server"></asp:Label>
                    </td>
                    <td style="width: 10%">
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
                    <td style="width: 10%">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%" align="left">
                        Payment Mode
                    </td>
                    <td style="width: 30%" align="left">
                        <asp:Label ID="lblPayMode" runat="server"></asp:Label>
                    </td>
                    <td style="width: 10%">
                        &nbsp;
                    </td>
                </tr>
                <tr id="rowBank" runat="server">
                    <td style="width: 30%" align="left">
                        Bank Name
                    </td>
                    <td style="width: 30%" align="left">
                        <asp:Label ID="lblBank" runat="server"></asp:Label>
                    </td>
                    <td style="width: 10%">
                        &nbsp;
                    </td>
                </tr>
                <tr id="rowCheque" runat="server">
                    <td style="width: 30%" align="left">
                        Cheque No.
                    </td>
                    <td style="width: 30%" align="left">
                        <asp:Label ID="lblCheque" runat="server"></asp:Label>
                    </td>
                    <td style="width: 10%">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%" align="left">
                        Narration
                    </td>
                    <td style="width: 30%" align="left">
                        <asp:Label ID="lblNarration" runat="server"></asp:Label>
                    </td>
                    <td style="width: 10%">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%" align="left">
                        Payment Date
                    </td>
                    <td style="width: 30%" align="left">
                        <asp:Label ID="lblPayDate" runat="server"></asp:Label>
                    </td>
                    <td style="width: 10%">
                        &nbsp;
                    </td>
                </tr>
            </table>
            <br />
            <br />
        </div>
    </div>
    </form>
</body>
</html>
