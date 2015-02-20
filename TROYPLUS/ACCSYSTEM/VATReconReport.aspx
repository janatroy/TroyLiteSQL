<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VATReconReport.aspx.cs" Inherits="VATReconReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <title>VAT Report</title>
    <link href="App_Themes/DefaultTheme/calendar.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/DefaultTheme/DefaultTheme.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="Scripts/calendar_eu.js"></script>
    <script type="text/javascript" language="JavaScript">

        function CallPrint(strid) {
            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('', '', 'letf=0,top=0,width=600,,toolbar=0,scrollbars=1,status=0');
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
<body>
    <form id="form1" runat="server">
    <div style="width: 700px" align="center">
        <br />
        <a href="VATSumaryReport.aspx">Back to VAT Summary Report</a>
        <br />
        <br />
        <asp:HiddenField ID="hdPurchase" runat="server" />
        <asp:HiddenField ID="hdSales" runat="server" />
        <table cellpadding="2" cellspacing="4" width="700px" border="0" style="border: 1px solid blue;
            background-image: url('App_Themes/DefaultTheme/Images/bluebg.jpg');">
        </table>
        <input type="button" value=" " id="Button2" runat="Server" onclick="javascript:CallPrint('divPrint')"
            class="printbutton6" />
        <br />
        <div id="divPrint" align="center" runat="server" style="font-family: 'Trebuchet MS'; font-size: 11px;">
            <table style="font-family: 'Trebuchet MS'; border: solid 1px blue; font-size: 11px;"
                cellspacing="3" cellpadding="3" width="100%">
                <tr>
                    <td colspan="3" class="subHeadFont2">
                        VAT Reconciliation Report
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <h3>
                            Summary</h3>
                        <div id="dvVAT" runat="server" style="font-family: 'Trebuchet MS'; border-bottom: solid 1px black;
                            border-top: solid 1px black; font-size: 11px;">
                        </div>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        1
                    </td>
                    <td align="left">
                        Purchase VAT
                    </td>
                    <td class="tblLeft">
                        <asp:Label ID="sumPurchase" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        2
                    </td>
                    <td align="left">
                        Purchase Return VAT
                    </td>
                    <td class="tblLeft">
                        <asp:Label ID="sumPurchaseReturn" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        3
                    </td>
                    <td align="left">
                        Sales VAT
                    </td>
                    <td class="tblLeft">
                        <asp:Label ID="sumSales" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        4
                    </td>
                    <td align="left">
                        Sales Return VAT
                    </td>
                    <td class="tblLeft">
                        <asp:Label ID="sumSalesReturn" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        5
                    </td>
                    <td align="left">
                        VAT Paid
                    </td>
                    <td class="tblLeft">
                        <asp:Label ID="sumVatPaid" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        6
                    </td>
                    <td align="left">
                        VAT Need to PAY
                    </td>
                    <td class="tblLeft">
                        <asp:Label ID="sumVatToPay" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <h5>
                Purchase VAT Details</h5>
            <asp:GridView Style="font-family: 'Trebuchet MS'; font-size: 11px;" EmptyDataText="No Bills Found"
                HeaderStyle-HorizontalAlign="Left" CellPadding="2" Width="100%" ID="grdPurchaseVat" 
                GridLines="Both" SkinID="GrdNoPaging" CssClass="gridview" AlternatingRowStyle-CssClass="even"
                AutoGenerateColumns="False" runat="server" OnRowDataBound="grdPurchaseVat_RowDataBound"
                ShowFooter="true" ForeColor="#333333">
                <Columns>
                    <asp:TemplateField HeaderText="Billno">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblBillno" runat="server"
                                Text='<%# Eval("BillNo") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date">
                        <ItemTemplate>
                            <asp:Label Visible="true" Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblDate"
                                runat="server" Text='<%# Eval("BillDate","{0:dd/MM/yyyy}")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Item Name">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblProduct"
                                runat="server" Text='<%# Eval("ProductName") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Supplier Name - Tin#">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblLedger" runat="server"
                                Text='<%# Eval("LedgerName") %>' />
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px; color: Red; font-weight: bold"
                                ID="lblTinNumber" runat="server" Text='' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="VAT (%)">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblVAT" runat="server"
                                Text='<%# Eval("VAT","{0:F2}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Actual Rate">
                        <ItemTemplate>
                            <asp:Label ID="lblRate" Style="font-family: 'Trebuchet MS'; font-size: 11px;" runat="server"
                                Text='<%# Eval("Rate","{0:F2}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Purchase VAT">
                        <ItemTemplate>
                            <asp:Label ID="lblVatRate" Style="font-family: 'Trebuchet MS'; font-size: 11px;"
                                runat="server" Text='<%# Eval("VatRate","{0:F2}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="VAT paid">
                        <ItemTemplate>
                            <asp:Label ID="lblVatPaid" Style="font-family: 'Trebuchet MS'; font-size: 11px;"
                                runat="server" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblGrossTotal" Style="font-family: 'Trebuchet MS'; font-size: 11px;
                                font-weight: bold;" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <br />
            <h5>
                Sales Return VAT Details</h5>
            <asp:GridView Style="font-family: 'Trebuchet MS'; font-size: 11px;" ShowFooter="true"
                EmptyDataText="No Bills Found" HeaderStyle-HorizontalAlign="Left" CellPadding="2"
                Width="100%" ID="grdSalesReturnVAT" GridLines="Both" SkinID="GrdNoPaging" CssClass="gridview"
                AlternatingRowStyle-CssClass="even" AutoGenerateColumns="False" runat="server"
                OnRowDataBound="grdSalesReturnVAT_RowDataBound" ForeColor="#333333">
                <Columns>
                    <asp:TemplateField HeaderText="Billno">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblBillno" runat="server"
                                Text='<%# Eval("PurchaseID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date">
                        <ItemTemplate>
                            <asp:Label Visible="true" Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblDate"
                                runat="server" Text='<%# Eval("BillDate","{0:dd/MM/yyyy}")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Item Name">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblProduct"
                                runat="server" Text='<%# Eval("ProductName") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Customer Name - Tin#">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblLedger" runat="server"
                                Text='<%# Eval("LedgerName") %>' />
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px; color: Red; font-weight: bold"
                                ID="lblTinNumber" runat="server" Text='' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="VAT (%)">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblVAT" runat="server"
                                Text='<%# Eval("VAT") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Actual Rate">
                        <ItemTemplate>
                            <asp:Label ID="lblRate" Style="font-family: 'Trebuchet MS'; font-size: 11px;" runat="server"
                                Text='<%# Eval("Rate","{0:F2}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Purchase VAT">
                        <ItemTemplate>
                            <asp:Label ID="lblVatRate" Style="font-family: 'Trebuchet MS'; font-size: 11px;"
                                runat="server" Text='<%# Eval("VatRate","{0:F2}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="VAT paid">
                        <ItemTemplate>
                            <asp:Label ID="lblVatPaid" Style="font-family: 'Trebuchet MS'; font-size: 11px;"
                                runat="server" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblGrossTotal" Style="font-family: 'Trebuchet MS'; font-size: 11px;
                                font-weight: bold;" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <br />
            <h5>
                Sales VAT Details</h5>
            <asp:GridView Style="font-family: 'Trebuchet MS'; font-size: 11px;" ShowFooter="true"
                EmptyDataText="No Bills Found" HeaderStyle-HorizontalAlign="Left" CellPadding="2"
                Width="100%" ID="grdSalesVAT" GridLines="Both" SkinID="GrdNoPaging" CssClass="gridview"
                AlternatingRowStyle-CssClass="even" AutoGenerateColumns="False" runat="server"
                OnRowDataBound="grdSalesVAT_RowDataBound" ForeColor="#333333">
                <Columns>
                    <asp:TemplateField HeaderText="Billno">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblBillno" runat="server"
                                Text='<%# Eval("Billno") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date">
                        <ItemTemplate>
                            <asp:Label Visible="true" Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblDate"
                                runat="server" Text='<%# Eval("BillDate","{0:dd/MM/yyyy}")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Item Name">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblProduct"
                                runat="server" Text='<%# Eval("ProductName") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="VAT (%)">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblVAT" runat="server"
                                Text='<%# Eval("VAT") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cusomer Name - Tin#">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblLedger" runat="server"
                                Text='<%# Eval("CustomerName") %>' />
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px; color: Red; font-weight: bold"
                                ID="lblTinNumber" runat="server" Text='' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Actual Rate">
                        <ItemTemplate>
                            <asp:Label ID="lblRate" Style="font-family: 'Trebuchet MS'; font-size: 11px;" runat="server"
                                Text='<%# Eval("Rate","{0:F2}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Purchase VAT">
                        <ItemTemplate>
                            <asp:Label ID="lblVatRate" Style="font-family: 'Trebuchet MS'; font-size: 11px;"
                                runat="server" Text='<%# Eval("VatRate","{0:F2}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="VAT paid">
                        <ItemTemplate>
                            <asp:Label ID="lblVatPaid" Style="font-family: 'Trebuchet MS'; font-size: 11px;"
                                runat="server" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblGrossTotal" Style="font-family: 'Trebuchet MS'; font-size: 11px;
                                font-weight: bold;" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <br />
            <h5>
                Purchase Return VAT Details</h5>
            <asp:GridView Style="font-family: 'Trebuchet MS'; font-size: 11px;" ShowFooter="true"
                EmptyDataText="No Bills Found" HeaderStyle-HorizontalAlign="Left" CellPadding="2"
                Width="100%" ID="grdPurchaseReturnVAT" GridLines="Both" SkinID="GrdNoPaging"
                CssClass="gridview" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="False"
                runat="server" OnRowDataBound="grdPurchaseReturnVAT_RowDataBound" ForeColor="#333333">
                <Columns>
                    <asp:TemplateField HeaderText="Billno">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblBillno" runat="server"
                                Text='<%# Eval("Billno") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date">
                        <ItemTemplate>
                            <asp:Label Visible="true" Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblDate"
                                runat="server" Text='<%# Eval("BillDate","{0:dd/MM/yyyy}")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Item Name">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblProduct"
                                runat="server" Text='<%# Eval("ProductName") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="VAT (%)">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblVAT" runat="server"
                                Text='<%# Eval("VAT") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Supplier Name - Tin#">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblLedger" runat="server"
                                Text='<%# Eval("CustomerName") %>' />
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px; color: Red; font-weight: bold"
                                ID="lblTinNumber" runat="server" Text='' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Actual Rate">
                        <ItemTemplate>
                            <asp:Label ID="lblRate" Style="font-family: 'Trebuchet MS'; font-size: 11px;" runat="server"
                                Text='<%# Eval("Rate","{0:F2}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Purchase VAT">
                        <ItemTemplate>
                            <asp:Label ID="lblVatRate" Style="font-family: 'Trebuchet MS'; font-size: 11px;"
                                runat="server" Text='<%# Eval("VatRate","{0:F2}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="VAT paid">
                        <ItemTemplate>
                            <asp:Label ID="lblVatPaid" Style="font-family: 'Trebuchet MS'; font-size: 11px;"
                                runat="server" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblGrossTotal" Style="font-family: 'Trebuchet MS'; font-size: 11px;
                                font-weight: bold;" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <br />
            <br />
            <h5>
                VAT Payment Details</h5>
            <br />
            <asp:GridView Style="font-family: 'Trebuchet MS'; font-size: 11px;" ShowFooter="true"
                EmptyDataText="No VAT Payment Found" HeaderStyle-HorizontalAlign="Left" CellPadding="2"
                Width="100%" ID="grdVATPayment" AlternatingRowStyle-HorizontalAlign="Left" EditRowStyle-HorizontalAlign="Left"
                RowStyle-HorizontalAlign="Left" FooterStyle-HorizontalAlign="Left" GridLines="Both"
                SkinID="GrdNoPaging" CssClass="gridview" AlternatingRowStyle-CssClass="even"
                AutoGenerateColumns="False" runat="server" OnRowDataBound="grdVATPayment_RowDataBound"
                ForeColor="#333333">
                <Columns>
                    <asp:TemplateField HeaderText="RefNo">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblRefNo" runat="server"
                                Text='<%# Eval("RefNo") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date">
                        <ItemTemplate>
                            <asp:Label Visible="true" Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblDate"
                                runat="server" Text='<%# Eval("TransDate","{0:dd/MM/yyyy}")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Narration">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblNarration"
                                runat="server" Text='<%# Eval("Narration") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amount">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblAmount" runat="server"
                                Text='<%# Eval("Amount") %>' />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblVATPaidTotal" Style="font-family: 'Trebuchet MS'; font-size: 11px;
                                font-weight: bold;" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <br />
            <br />
        </div>
    </div>
    </form>
</body>
</html>
