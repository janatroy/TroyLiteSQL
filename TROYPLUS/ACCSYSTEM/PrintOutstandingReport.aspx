<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintOutstandingReport.aspx.cs"
    Inherits="PrintOutstandingReport" %>

<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Print Preview - Outstanding Report</title>
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
    <input type="button" value="Print " id="Button1" runat="Server" onclick="javascript:CallPrint('divPrint')"
        class="button" />&nbsp;
    <asp:Button ID="btnBack" Text="Back" runat="server" OnClick="btnBack_Click" />
    <div id="divPrint" style="font-family: 'Trebuchet MS'; font-size: 11px;">
        <asp:HiddenField ID="hdToDelete" Value="" runat="server" />
        <br />
        <br />
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
                <td colspan="3">
                    <br />
                    <h5>
                        Outstanding Report For
                        <asp:Label ID="lblSundry" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <wc:ReportGridView runat="server" BorderWidth="1" ID="gvLedger" CssClass="gridview"
            GridLines="Both" AlternatingRowStyle-CssClass="even" HeaderStyle-HorizontalAlign="Left"
            AutoGenerateColumns="false" PrintPageSize="47" AllowPrintPaging="true" Width="600"
            Style="font-family: 'Trebuchet MS'; font-size: 11px;" OnRowDataBound="gvLedger_RowDataBound">
            <PageHeaderTemplate>
                <br />
                <br />
            </PageHeaderTemplate>
            <Columns>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" DataField="Ledgername" HeaderText="Particulars" />
                <asp:TemplateField HeaderText="Debit" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblDebit" Text='<%# Eval("Debit","{0:f2}") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Credit" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblCredit" Text='<%# Eval("Credit","{0:f2}") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerTemplate>
            </PagerTemplate>
            <PageFooterTemplate>
            </PageFooterTemplate>
        </wc:ReportGridView>
        <br />
        <br />
        <table width="600" border="0" cellpadding="0" cellspacing="2" style="font-family: 'Trebuchet MS';
            font-size: 11px;">
            <tr>
                <td width="460px" align="right">
                    <b></b>
                </td>
                <td width="140px" align="right">
                    <hr />
                    <asp:Label ID="lblDebitSum" runat="server"></asp:Label><hr />
                </td>
                <td width="140px" align="right">
                    <hr />
                    <asp:Label ID="lblCreditSum" runat="server"></asp:Label><hr />
                </td>
            </tr>
            <tr>
                <td width="460px" align="right">
                    <b></b>
                </td>
                <td width="140px" align="right">
                    <hr />
                    <asp:Label ID="lblDebitDiff" runat="server"></asp:Label><hr />
                </td>
                <td width="140px" align="right">
                    <hr />
                    <asp:Label ID="lblCreditDiff" runat="server"></asp:Label><hr />
                </td>
            </tr>
        </table>
        <br />
        <br />
    </div>
    </form>
</body>
</html>
