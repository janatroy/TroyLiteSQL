<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintJournal.aspx.cs" Inherits="PrintJournal" %>

<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Preview</title>
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
<body>
    <form id="form1" runat="server">
    <div style="width: 90%; text-align: center" align="center">
        <br />
        <input type="button" value="Print " id="Button1" runat="Server" onclick="javascript:CallPrint('divPrint')"
            class="button" />&nbsp;
        <asp:Button ID="btnBack" Text="Back" runat="server" OnClick="btnBack_Click" Visible="false" /><br />
        <br />
        <div id="divPrint" style="font-family: 'Trebuchet MS'; font-size: 11px;" align="center">
            <table width="600px" border="0" style="font-family: 'Trebuchet MS'; font-size: 11px;">
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
                        &nbsp;
                    </td>
                    <td align="center">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td width="140px" align="left">
                        Bill No.<asp:Label ID="lblBillno" runat="server"></asp:Label>
                    </td>
                    <td align="center" width="320px">
                        <asp:Label ID="lblSupplier" runat="server" Font-Bold="true"></asp:Label>
                    </td>
                    <td width="140px" align="left">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <br />
                        <h5>
                            Journal</h5>
                    </td>
                </tr>
            </table>
            <br />
            <asp:HiddenField ID="hdJournal" runat="server" Value="0" />
            <table width="600" border="0" cellpadding="0" cellspacing="2" style="font-family: 'Trebuchet MS';
                font-size: 11px;">
                <tr>
                    <td style="width: 30%" align="left">
                        &nbsp;
                    </td>
                    <td style="width: 30%" align="left">
                        &nbsp;
                    </td>
                    <td style="width: 30%">
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
                    <td style="width: 30%">
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
                    <td style="width: 30%">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%" align="left">
                        Debtor
                    </td>
                    <td style="width: 30%" align="left">
                        <asp:Label ID="lblDebtor" runat="server"></asp:Label>
                    </td>
                    <td style="width: 30%">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%" align="left">
                        Creditor
                    </td>
                    <td style="width: 30%" align="left">
                        <asp:Label ID="lblCreditor" runat="server"></asp:Label>
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
                <tr>
                    <td style="width: 30%" align="left">
                        Narration
                    </td>
                    <td style="width: 30%" align="left">
                        <asp:Label ID="lblNarration" runat="server"></asp:Label>
                    </td>
                    <td style="width: 30%">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
