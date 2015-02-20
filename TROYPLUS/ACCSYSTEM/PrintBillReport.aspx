<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintBillReport.aspx.cs"
    Inherits="PrintBillReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Preview - Bill Details Report</title>
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
    <link href="App_Themes/DefaultTheme/DefaultTheme.css" rel="stylesheet" type="text/css" />
</head>
<body style="font-size: 11px; font-family: 'Trebuchet MS';">
    <form id="form1" runat="server">
    <br />
    <asp:Button ID="btnPrint" Text="Print" runat="server" OnClientClick="javascript:CallPrint('divPrint')" />
    <asp:Button ID="btnBack" Text="Back" runat="server" OnClick="btnBack_Click" />
    <div id="divPrint" style="font-family: 'Trebuchet MS'; font-size: 11px;">
        <table width="600px" border="0" style="font-family: 'Trebuchet MS'; font-size: 11px;">
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
                <td>
                    Date:
                    <asp:Label ID="lblDate" runat="server"> </asp:Label>
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
                        Bill Details Report :&nbsp; From
                        <asp:Label ID="lblStartDate" runat="server"> </asp:Label>
                        &nbsp;To
                        <asp:Label ID="lblEndDate" runat="server"> </asp:Label></h5>
                </td>
            </tr>
        </table>
        <br />
        <wc:ReportGridView runat="server" BorderWidth="1" ID="gvBillDetails" AutoGenerateColumns="false"
            PrintPageSize="23" AllowPrintPaging="true" Width="600px" OnRowDataBound="gvBillDetails_RowDataBound"
            Style="font-family: 'Trebuchet MS'; font-size: 11px;">
            <PageHeaderTemplate>
                <br />
                <br />
            </PageHeaderTemplate>
            <Columns>
                <asp:BoundField DataField="Area" HeaderText="Area" />
                <asp:BoundField DataField="Name" HeaderText="Name" />
                <asp:BoundField DataField="Code" HeaderText="Code" />
                <asp:BoundField DataField="Doorno" HeaderText="Doorno" />
                <asp:BoundField DataField="date_print" HeaderText="Date Print" />
                <asp:BoundField ItemStyle-HorizontalAlign="Right" DataField="monthlycharge" HeaderText="Monthly Charge" />
            </Columns>
            <PagerTemplate>
            </PagerTemplate>
            <PageFooterTemplate>
                <br />
                <hr />
                <%-- Page <%# gvCashDetails.CurrentPrintPage.ToString() %> / <%# gvCashDetails.PrintPageCount%>--%>
            </PageFooterTemplate>
        </wc:ReportGridView>
        <br />
        <table width="600" border="0" style="font-family: 'Trebuchet MS'; font-size: 11px;">
            <tr>
                <td width="460px" align="right">
                    <b>Total Amount :ount :</b>
                </td>
                <td width="140px" align="right">
                    <hr />
                    <asp:Label ID="lblAmount" runat="server"></asp:Label><hr />
                </td>
            </tr>
        </table>
        <br />
        <br />
    </div>
    </form>
</body>
</html>
