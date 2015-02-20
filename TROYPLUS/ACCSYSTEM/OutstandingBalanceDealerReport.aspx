<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OutstandingBalanceDealerReport.aspx.cs"
    Inherits="OutstandingBalanceDealerReport" %>

<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>Outstanding Customer / Dealer Report</title>
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
    </script>
</head>
<body style="font-family: 'Trebuchet MS'; font-size: 11px;">
    <form id="form1" runat="server">
    <br />
    <br />
    <div>
        <table cellpadding="2" cellspacing="4" width="700px" border="0" style="border: 1px solid silver;
            text-align: left">
            <tr>
                <td colspan="3" class="subHeadFont">
                    Outstanding Balance Dealer Report
                </td>
            </tr>
            <tr>
                <td class="left">
                    Ledger Name
                </td>
                <td colspan="2" class="tblLeft">
                    <asp:DropDownList ID="drpLedgerName" runat="server" Width="200px" DataTextField="LedgerName"
                        DataValueField="LedgerID" CssClass="lblFont">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="lblFont">
                </td>
                <td colspan="2" class="tblLeft">
                    <asp:Label ID="lblDate" CssClass="lblFont" runat="server"></asp:Label>
                    <asp:Button ID="btnReport" CssClass="Button" runat="server" OnClick="btnReport_Click"
                        Text="Oustanding Report" />&nbsp;<input type="button" value="Print " id="Button1"
                            runat="Server" onclick="javascript:CallPrint('divPrint')" class="pageButton" />&nbsp;
                </td>
            </tr>
        </table>
    </div>
    <div id="divPrint" style="font-family: 'Trebuchet MS'; font-size: 11px; width: 700px"
        visible="false" runat="server">
        <br />
        <table width="700px" border="0" style="font-family: Trebuchet MS; font-size: 14px;">
            <tr>
                <td width="140px" align="left">
                    TIN#:
                    <asp:Label ID="lblTNGST" runat="server"></asp:Label>
                </td>
                <td align="center" width="420px" style="font-size: 20px;">
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
        </table>
        <br />
        <wc:ReportGridView runat="server" BorderWidth="1" ID="gvOuts" GridLines="Both" AlternatingRowStyle-CssClass="even"
            CssClass="gridview" AutoGenerateColumns="false" PrintPageSize="40" AllowPrintPaging="true"
            Width="700px" OnRowDataBound="gvOuts_RowDataBound">
            <HeaderStyle CssClass="ReportHeadataRow" />
            <RowStyle CssClass="ReportdataRow" />
            <AlternatingRowStyle CssClass="ReportAltdataRow" />
            <FooterStyle CssClass="ReportFooterRow" />
            <PageHeaderTemplate>
                <br />
                <br />
            </PageHeaderTemplate>
            <Columns>
                <asp:TemplateField HeaderText="Transno">
                    <ItemTemplate>
                        <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblTransno"
                            runat="server" Text='<%# Eval("TransNo") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="TransDate">
                    <ItemTemplate>
                        <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblTransDate"
                            runat="server" Text='<%# Eval("TransDate","{0:dd/MM/yyyy}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Outstanding(0-10) Days">
                    <ItemTemplate>
                        <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblOuts10" runat="server"
                            Text='<%# Eval("Out10") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Outstanding(11-30) Days">
                    <ItemTemplate>
                        <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblOuts30" runat="server"
                            Text='<%# Eval("Out30") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Outstanding(31-60) Days">
                    <ItemTemplate>
                        <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblOuts60" runat="server"
                            Text='<%# Eval("Out60") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Outstanding(61-90) Days">
                    <ItemTemplate>
                        <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblOuts90" runat="server"
                            Text='<%# Eval("Out90") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Outstanding(90-Above) Days">
                    <ItemTemplate>
                        <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblOuts91" runat="server"
                            Text='<%# Eval("Out90above") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Amount" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblAmount" runat="server"
                            Text='<%# Eval("Amount","{0:F2}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerTemplate>
            </PagerTemplate>
            <PageFooterTemplate>
                <br />
            </PageFooterTemplate>
        </wc:ReportGridView>
        <table width="700px" cellpadding="0px" cellspacing="0px" style="font-family: 'Trebuchet MS';
            font-size: 11px;" border="0px">
            <tr>
                <td width="65px">
                    &nbsp;
                </td>
                <td width="85px">
                    &nbsp;
                </td>
                <td width="110px">
                    &nbsp;<asp:Label Text="0" ID="lblTotalOuts10" runat="server" CssClass="lblFont"></asp:Label>
                </td>
                <td width="110px">
                    &nbsp;<asp:Label Text="0" ID="lblTotalOuts30" runat="server" CssClass="lblFont"></asp:Label>
                </td>
                <td width="110px">
                    &nbsp;<asp:Label Text="0" ID="lblTotalOuts60" runat="server" CssClass="lblFont"></asp:Label>
                </td>
                <td width="110px">
                    &nbsp;<asp:Label Text="0" ID="lblTotalOuts90" runat="server" CssClass="lblFont"></asp:Label>
                </td>
                <td width="115px">
                    &nbsp;<asp:Label Text="0" ID="lblTotalOuts91" runat="server" CssClass="lblFont"></asp:Label>
                </td>
                <td width="50px">
                    <asp:Label CssClass="tblLeft" ID="lblTotalAmt" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
