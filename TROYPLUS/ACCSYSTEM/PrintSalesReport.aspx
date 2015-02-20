<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintSalesReport.aspx.cs"
    Inherits="PrintSalesReport" %>

<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Print Preview - Sales Report</title>
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
    <asp:Button ID="btnBack" Text="Back" runat="server" OnClick="btnBack_Click" Visible="True" /><br />
    <br />
    <div id="divPrint" style="font-family: 'Trebuchet MS'; font-size: 11px;">
        <asp:HiddenField ID="hdToDelete" Value="" runat="server" />
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
                        Sales Register From
                        <asp:Label ID="lblStartDate" runat="server"> </asp:Label>
                        To
                        <asp:Label ID="lblEndDate" runat="server"> </asp:Label></h5>
                </td>
            </tr>
        </table>
        <br />
        <%--OnRowDataBound="gvProducts_RowDataBound"--%>
        <wc:ReportGridView runat="server" BorderWidth="1" ID="gvSales" CssClass="gridview"
            GridLines="Both" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
            PrintPageSize="4" AllowPrintPaging="true" Width="100%" Style="font-family: 'Trebuchet MS';
            font-size: 11px;" OnRowDataBound="gvSales_RowDataBound">
            <PageHeaderTemplate>
                <br />
                <br />
            </PageHeaderTemplate>
            <Columns>
                <asp:TemplateField HeaderText="Bill Date">
                    <ItemTemplate>
                        <asp:Label Visible="true" ID="lblBillDate" runat="server" Text='<%# Eval("BillDate","{0:dd/MM/yyyy}")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Billno">
                    <ItemTemplate>
                        <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblBillno" runat="server"
                            Text='<%# Eval("Billno") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Customername" HeaderText="Customer" />
                <asp:TemplateField HeaderText="Paymode">
                    <ItemTemplate>
                        <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblPaymode"
                            runat="server" Text='<%# Eval("Paymode") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Sold Items">
                    <ItemTemplate>
                        <br />
                        <wc:ReportGridView runat="server" BorderWidth="1" ID="gvProducts" ShowFooter="true"
                            AutoGenerateColumns="false" PrintPageSize="23" AllowPrintPaging="true" Width="100%"
                            Style="font-family: 'Trebuchet MS'; font-size: 11px;" OnRowDataBound="gvProducts_RowDataBound">
                            <PageHeaderTemplate>
                                <br />
                                <br />
                            </PageHeaderTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="Item Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("ItemCode") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("ProductName") %>' /><br />
                                        <b>Model :</b>
                                        <asp:Label ID="lblModel" runat="server" Text='<%# Eval("Model") %>' /><br />
                                        <b>Description :</b><asp:Label ID="lblDes" runat="server" Text='<%# Eval("ProductDesc") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQty" runat="server" Text='<%# Eval("Qty") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRate" runat="server" Text='<%# Eval("Rate","{0:f2}") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Discount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDisc" runat="server" Text='<%# Eval("Discount","{0:f2}") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="VAT">
                                    <ItemTemplate>
                                        <asp:Label ID="lblVat" runat="server" Text='<%# Eval("VAT","{0:f2}") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Value">
                                    <ItemTemplate>
                                        <asp:Label ID="lblValue" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerTemplate>
                            </PagerTemplate>
                            <PageFooterTemplate>
                                <br />
                            </PageFooterTemplate>
                        </wc:ReportGridView>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerTemplate>
            </PagerTemplate>
            <PageFooterTemplate>
            </PageFooterTemplate>
        </wc:ReportGridView>
    </form>
</body>
</html>
