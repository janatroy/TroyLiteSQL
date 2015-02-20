<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintQtyReturnReport.aspx.cs"
    Inherits="PrintQtyReturnReport" %>

<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>Print Preview - Ledger Report</title>
    <link href="App_Themes/DefaultTheme/DefaultTheme.css" rel="stylesheet" type="text/css" />
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
        function unl() {

            document.form1.submit();
        }

    </script>
</head>
<body style="font-size: 11px; font-family: 'Trebuchet MS';" onbeforeunload="unl()">
    <form id="form1" runat="server">
    <div>
        <input type="button" value="Print " id="Button1" runat="Server" onclick="javascript:CallPrint('divPrint')"
            class="pageButton" />&nbsp;
        <asp:Button ID="btnBack" Text="Back" CssClass="Button" runat="server" OnClick="btnBack_Click"
            Visible="True" /><br />
        <br />
        <div id="divPrint" style="font-family: 'Trebuchet MS'; font-size: 11px;">
            <asp:HiddenField ID="hdToDelete" Value="" runat="server" />
            <table width="600px" border="0" style="font-family: Trebuchet MS; font-size: 14px;">
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
            </table>
            <tr>
                <td colspan="3">
                    <br />
                    <h5>
                        Ledger :
                        <asp:Label ID="lblLedger" runat="server"></asp:Label>
                        <br />
                        <asp:Label ID="lblStartDate" runat="server"> </asp:Label><asp:Label ID="lblEndDate"
                            runat="server"> </asp:Label></h5>
                </td>
            </tr>
            <br />
            <wc:ReportGridView runat="server" BorderWidth="1" ID="gvLedger" CssClass="gridview"
                GridLines="Both" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                PrintPageSize="45" AllowPrintPaging="true" Width="600px" Style="font-family: 'Trebuchet MS';
                font-size: 11px;" OnRowDataBound="gvLedger_RowDataBound">
                <HeaderStyle CssClass="ReportHeadataRow" />
                <RowStyle CssClass="ReportdataRow" />
                <AlternatingRowStyle CssClass="ReportAltdataRow" />
                <FooterStyle CssClass="ReportFooterRow" />
                <PageHeaderTemplate>
                    <br />
                    <br />
                </PageHeaderTemplate>
                <Columns>
                    <asp:BoundField DataField="BillNo" HeaderText="BillNo" ItemStyle-Width="140px" />
                    <asp:BoundField DataField="BillDate" HeaderText="BillDate" ItemStyle-Width="140px" />
                    <asp:BoundField DataField="QtySale" HeaderText="QtySale" ItemStyle-Width="140px" />
                    <asp:BoundField DataField="QtyReturn" HeaderText="QtyReturn" ItemStyle-Width="140px" />
                    <asp:BoundField DataField="QtyPending" HeaderText="QtyPending" ItemStyle-Width="140px" />
                </Columns>
                <PagerTemplate>
                </PagerTemplate>
                <PageFooterTemplate>
                </PageFooterTemplate>
            </wc:ReportGridView>
            <br />
            <table width="600" border="0" cellpadding="0" cellspacing="2" style="font-family: 'Trebuchet MS';
                font-size: 11px;">
                <tr>
                    <td width="460px" align="center">
                        <b>Total :</b>
                    </td>
                    <td width="250px" align="center">
                        <hr />
                        <asp:Label ID="lblQtySum" runat="server"></asp:Label><hr />
                    </td>
                    <td width="250px" align="center">
                        <hr />
                        <asp:Label ID="lblQtyReturn" runat="server"></asp:Label><hr />
                    </td>
                    <td width="250px" align="center">
                        <hr />
                        <asp:Label ID="lblQtyPend" runat="server"></asp:Label><hr />
                    </td>
                </tr>
            </table>
            <br />
            <br />
        </div>
    </form>
</body>
</html>
