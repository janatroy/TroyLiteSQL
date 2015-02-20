<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BundleProductPurchaseBill.aspx.cs"
    Inherits="BundleProductPurchaseBill" %>

<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Purchase Bill</title>
    <link rel="Stylesheet" href="App_Themes/DefaultTheme/DefaultTheme.css" />
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
    <div>
        <input type="button" value="Print " id="Button1" runat="Server" onclick="javascript:CallPrint('divPrint')"
            class="button" />&nbsp;
        <asp:Button ID="btnBack" Text="Back" runat="server" OnClick="btnBack_Click" /><br />
        <br />
        <div id="divPrint" style="font-family: 'Trebuchet MS'; font-size: 11px;">
            <div id="viswasteel" runat="server">
                <table width="600px" border="0" cellpadding="2" cellspacing="0" style="font-family: 'Trebuchet MS';
                    font-size: 11px; border: 1px solid black;">
                    <%--Start Header--%>
                    <tr>
                        <td style="border-right: 1px solid black;">
                            <table style="font-family: 'Trebuchet MS'; font-size: 11px;">
                                <tr>
                                    <td align="left">
                                        TIN
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblTNGST" runat="server"></asp:Label>
                                    </td>
                                    <td align="right">
                                        BillNo :
                                        <asp:Label ID="lblBillno" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        CST#
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblGSTno" runat="server"></asp:Label>
                                    </td>
                                    <td align="right">
                                        TransNo :
                                        <asp:Label ID="lblTransNo" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="left" colspan="4" width="400px">
                                        <table style="font-family: 'Trebuchet MS'; font-size: 11px;">
                                            <tr>
                                                <td>
                                                    <asp:Label Font-Bold="true" ID="lblCompany" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblAddress" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblCity" runat="server" />
                                                    -
                                                    <asp:Label ID="lblPincode" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblState" runat="server"> </asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="55%" align="left">
                            <table width="100%" style="font-family: 'Trebuchet MS'; font-size: 11px;">
                                <tr>
                                    <td valign="top">
                                        <b>&nbsp;</b><asp:Label ID="lblHeader" Text="Purchase - Bill" runat="server" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td valign="top" align="right">
                                        Ph:
                                        <asp:Label ID="lblPhone" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" valign="top" align="right">
                                        Date:
                                        <asp:Label ID="lblBillDate" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="left" valign="top">
                                        <table border="0" style="font-family: 'Trebuchet MS'; font-size: 11px;">
                                            <tr>
                                                <td>
                                                    M/s:&nbsp;<asp:Label ID="lblSupplier" runat="server" Font-Bold="true" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblSuppAdd1" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblSuppAdd2" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblSuppAdd3" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Ph :
                                                    <asp:Label ID="lblSuppPh" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <%--End Header--%>
                    <%-- Start Product Details --%>
                    <tr>
                        <td colspan="2" style="border-bottom: 1px solid black; border-top: 1px solid black;">
                            <wc:ReportGridView runat="server" BorderWidth="1" ID="gvGeneral" CssClass="left"
                                GridLines="Both" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                PrintPageSize="10" AllowPrintPaging="true" Visible="false" Width="600px" OnRowDataBound="gvGeneral_RowDataBound"
                                Style="font-family: 'Trebuchet MS'; font-size: 11px;">
                                <PageHeaderTemplate>
                                    <br />
                                    <br />
                                </PageHeaderTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="No">
                                        <ItemTemplate>
                                            <%# ((GridViewRow)Container).RowIndex + 1%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField ItemStyle-HorizontalAlign="right" ItemStyle-Width="40px" DataFormatString="{0:f2}"
                                        DataField="PurchaseRate" HeaderText="Rate" />
                                    <asp:BoundField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="410px" DataField="Particulars"
                                        HeaderText="Particulars" />
                                    <asp:BoundField ItemStyle-HorizontalAlign="right" ItemStyle-Width="40px" DataFormatString="{0:f2}"
                                        DataField="NetRate" HeaderText="NetRate" Visible="false" />
                                    <asp:BoundField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40px" DataField="Qty"
                                        HeaderText="Qty" />
                                    <asp:BoundField ItemStyle-HorizontalAlign="right" ItemStyle-Width="70px" DataFormatString="{0:f2}"
                                        DataField="Amount" HeaderText="Amount" />
                                </Columns>
                                <PagerTemplate>
                                </PagerTemplate>
                                <PageFooterTemplate>
                                    <br />
                                </PageFooterTemplate>
                            </wc:ReportGridView>
                            <%--<table width="600px" border="0">
                        <tr>
                        <td colspan="4" align="right" width="530px">Total (INR)</td>
                        <td align="right"  width="80px" ><asp:Label ID="lblAmt" CssClass="lblFont" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="4"  align="right">Discount (INR)</td>
                        <td align="right"><asp:Label ID="lblGrandDiscount" CssClass="lblFont" Text="0" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="4" align="right">VAT (INR)</td>
                        <td align="right"><asp:Label ID="lblGrandVat" CssClass="lblFont" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                        <td colspan="4"  align="right">CST (INR)</td>
                        <td align="right"><asp:Label ID="lblGrandCst" CssClass="lblFont" Text="0" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                        <td colspan="4"  align="right">Loading / Unloading / Freight </td>
                        <td align="right"><asp:Label ID="lblFg" CssClass="lblFont" Text="0" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td align="left">Rs. <asp:label ID="lblRs" runat="server" CssClass="lblFont" /></td>
                        <td colspan="3"  align="right">GRAND TOTAL (INR)</td>
                        <td align="right" style="border-top:1px solid black;border-bottom:1px solid black;"><asp:Label ID="lblNetTotal" CssClass="lblFont" runat="server"></asp:Label></td>
                    </tr>
                </table>--%>
                            <div id="dvAmount" runat="server">
                                <table width="600px" border="0" style="font-family: 'Trebuchet MS'; font-size: 11px;"
                                    cellpadding="2" cellspacing="0">
                                    <tr>
                                        <td width="340px">
                                            &nbsp;
                                        </td>
                                        <td align="left" width="189px">
                                            Total (INR)
                                        </td>
                                        <td width="1px">
                                            :
                                        </td>
                                        <td align="right" width="70px">
                                            <asp:Label ID="lblAmt" CssClass="lblFont" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div id="dvDiscountTotal" runat="server" visible="false">
                                <table width="600px" border="0" cellpadding="2" cellspacing="0" style="font-family: 'Trebuchet MS';
                                    font-size: 11px;">
                                    <tr>
                                        <td width="340px">
                                            &nbsp;
                                        </td>
                                        <td align="left" width="189px">
                                            Discount (INR)
                                        </td>
                                        <td width="1px">
                                            :
                                        </td>
                                        <td align="right" width="70px">
                                            <asp:Label ID="lblGrandDiscount" CssClass="lblFont" Text="0" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div id="dvVatTotal" runat="server" visible="false">
                                <table width="600px" border="0" cellpadding="2" cellspacing="0" style="font-family: 'Trebuchet MS';
                                    font-size: 11px;">
                                    <tr>
                                        <td width="340px">
                                            &nbsp;
                                        </td>
                                        <td align="left" width="189px">
                                            VAT &nbsp;<asp:Label ID="lblVatDisplay" runat="server" CssClass="lblFont"></asp:Label>&nbsp;
                                            (INR)
                                        </td>
                                        <td width="1px">
                                            :
                                        </td>
                                        <td align="right" width="70px">
                                            <asp:Label ID="lblGrandVat" CssClass="lblFont" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div id="dvFrgTotal" runat="server" visible="false">
                                <table width="600px" border="0" cellpadding="2" cellspacing="0" style="font-family: 'Trebuchet MS';
                                    font-size: 11px;">
                                    <tr>
                                        <td width="343px">
                                            &nbsp;
                                        </td>
                                        <td align="left" width="186px">
                                            Loading / Unloading / Freight (INR)
                                        </td>
                                        <td width="1px">
                                            :
                                        </td>
                                        <td align="right" width="70px">
                                            <asp:Label ID="lblFg" CssClass="lblFont" Text="0" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div id="dvCSTTotal" runat="server" visible="false">
                                <table width="600px" border="0" cellpadding="2" cellspacing="0" style="font-family: 'Trebuchet MS';
                                    font-size: 11px;">
                                    <tr>
                                        <td width="340px">
                                            &nbsp;
                                        </td>
                                        <td align="left" width="189px">
                                            CST &nbsp;<asp:Label ID="lblCSTDisplay" runat="server" CssClass="lblFont"></asp:Label>&nbsp;
                                            (INR)
                                        </td>
                                        <td width="1px">
                                            :
                                        </td>
                                        <td align="right" width="70px">
                                            <asp:Label ID="lblGrandCst" CssClass="lblFont" Text="0" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <table width="600px" border="0" cellpadding="2" cellspacing="0" style="font-family: 'Trebuchet MS';
                                font-size: 11px;">
                                <tr>
                                    <td align="left" width="340px">
                                        Rs.
                                        <asp:Label ID="lblRs" runat="server" CssClass="lblFont" />
                                    </td>
                                    <td align="left" width="189px">
                                        GRAND TOTAL (INR)
                                    </td>
                                    <td width="1px">
                                        :
                                    </td>
                                    <td align="right" width="70px" style="border-top: 1px solid black; border-bottom: 1px solid black;">
                                        <asp:Label ID="lblNetTotal" CssClass="lblFont" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <%-- End Product Details --%>
                    <%--Start Footer--%>
                    <tr>
                        <td colspan="2">
                            <table width="600px" border="0" cellpadding="2" cellspacing="0" style="font-family: 'Trebuchet MS';
                                font-size: 11px;">
                                <tr>
                                    <td width="350">
                                        &nbsp;<br />
                                        <br />
                                        <br />
                                    </td>
                                    <td align="center">
                                        For
                                        <asp:Label Font-Bold="true" ID="lblSignCompany" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="left" style="font-size: :xx-small;">
                                        <div id="viswasfooter" visible="false" runat="server">
                                            X Our Liability ceases as soon as the goods leave our place.<br />
                                            X Weight recorded at our weighbridge is final.<br />
                                            X All disputes subject to Aruppukottai jurisdiction.<br />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <%--End Footer--%>
                </table>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
