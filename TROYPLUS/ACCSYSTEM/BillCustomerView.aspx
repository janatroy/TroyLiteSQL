<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BillCustomerView.aspx.cs"
    Inherits="BillCustomerView" %>

<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bill View</title>
    <script language="javascript" type="text/javascript">
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
    <div>
        &nbsp;<input type="button" value="Print " id="Button1" runat="Server" onclick="javascript:CallPrint('divPrint')"
            class="button" /><br />
        <center>
            <asp:Label ID="lblCustomer" runat="server" CssClass="lblFont" Font-Bold="true" Style="color: Navy;"></asp:Label>
        </center>
        <br />
        <div id="divPrint" style="font-family: 'Trebuchet MS'; font-size: 11px;">
            <wc:ReportGridView runat="server" BorderWidth="1" ID="gvThird" CssClass="gridview"
                GridLines="Both" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                OnRowDataBound="gvThird_RowDataBound" Width="100%" Style="font-family: 'Trebuchet MS';
                font-size: 11px;" ShowFooter="true" ShowHeader="True">
                <PageHeaderTemplate>
                    <br />
                    <br />
                </PageHeaderTemplate>
                <Columns>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                        HeaderText="Billno">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblBillno" runat="server"
                                Text='<%# Eval("Billno") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                        HeaderText="BillDate">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblBillDate"
                                runat="server" Text='<%# Eval("BillDate","{0:dd/MM/yyyy}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                        HeaderText="Net Rate">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblNetRate"
                                runat="server" Text='<%# Eval("SalesRate","{0:f2}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                        HeaderText="Discount Rate">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblDiscountRate"
                                runat="server" Text='<%# Eval("ActualDiscount","{0:f2}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                        HeaderText="VAT Rate">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblVatRate"
                                runat="server" Text='<%# Eval("ActualVat","{0:f2}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                        HeaderText="CST Rate">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblCSTRate"
                                runat="server" Text='<%# Eval("ActualCST","{0:f2}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                        HeaderText="Freight">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblFreightRate"
                                runat="server" Text='<%# Eval("SumFreight","{0:f2}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                        HeaderText="Loading / Unloading">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblLURate" runat="server"
                                Text='<%# Eval("Loading","{0:f2}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                        HeaderText="Total">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblTotal" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </wc:ReportGridView>
        </div>
    </div>
    </form>
</body>
</html>
