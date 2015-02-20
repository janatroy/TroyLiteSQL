<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CSTReprt.aspx.cs" Inherits="CSTReprt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <title>CST Report</title>
    <link href="App_Themes/DefaultTheme/calendar.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="Scripts/calendar_eu.js"></script>
    <link href="App_Themes/DefaultTheme/DefaultTheme.css" rel="stylesheet" type="text/css" />
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
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 700px" align="center">
        <a href="CSTSumaryReport.aspx">CST Summary</a>
        <asp:HiddenField ID="hdPurchase" runat="server" />
        <asp:HiddenField ID="hdSales" runat="server" />
        <br />
        <div align="center">
        <table cellpadding="2" align="center" cellspacing="2" width="500px" border="0" style="border: 1px solid blue; background-color:White;
            text-align: left">
            <tr>
                <td colspan="3" class="headerPopUp">
                    CST Detailed Report
                </td>
            </tr>
            <tr>
                <td style="width:30%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px" align="left">
                    Start Date
                </td>
                <td class="ControlTextBox3" width="20%">
                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="cssTextBox" Width="100px"
                        MaxLength="10" />
                    
                </td>
                <td align="left" width="30%">
                    <script language="JavaScript">                        new tcal({ 'formname': 'form1', 'controlname': 'txtStartDate' });</script>
                    <asp:RequiredFieldValidator class="lblFont" CssClass="lblFont" ID="RequiredFieldValidator1"
                        runat="server" ControlToValidate="txtStartDate" Display="None" ErrorMessage="Please Enter Start Date"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="left"  style="width:30%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                    End Date
                </td>
                <td class="ControlTextBox3" width="20%">
                    <asp:TextBox ID="txtEndDate" CssClass="cssTextBox" runat="server" Width="100px" MaxLength="10" />
                    <%--<a href="javascript:NewCal('txtEndDate','ddmmyyyy',false,24)"><img src="cal.gif" width="16" height="16" border="0" alt="Pick a date"></a>--%>
                    
                </td>
                <td align="left" width="30%">
                    <script language="JavaScript">                        new tcal({ 'formname': 'form1', 'controlname': 'txtEndDate' });</script>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEndDate"
                        Display="None" CssClass="lblFont" ErrorMessage="Please Enter The End Date"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" CssClass="lblFont" ControlToCompare="txtStartDate"
                        ControlToValidate="txtEndDate" Display="None" ErrorMessage="Start Date Should Be Less Than the End Date"
                        Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <table width="100%">
                        <tr>
                            <td width="25%">
                            </td>
                            <td width="20%">
                                <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" CssClass="CstDetail" EnableTheming="false" CausesValidation="false"
                                    Text="" />
                            </td>
                            <td width="50%">
                                <input type="button" value="" id="Button2" runat="Server" onclick="javascript:CallPrint('divPrint')"
                                    class="printbutton6" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        </div>
        <div id="divPrint" runat="server" style="font-family: 'Trebuchet MS'; font-size: 11px;">
            <h3>
                Summary</h3>
            <br />
            <div id="dvCST" runat="server" style="font-family: 'Trebuchet MS'; border: solid 1px black;
                font-size: 11px;" width="40%">
            </div>
            <br />
            <table style="font-family: 'Trebuchet MS'; border: solid 1px black; font-size: 11px;"
                width="40%" cellspacing="3" cellpadding="3">
                <tr>
                    <td>
                        1
                    </td>
                    <td align="left">
                        Purchase CST
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
                        Purchase Return CST
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
                        Sales CST
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
                        Sales Return CST
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
                        CST Need to PAY
                    </td>
                    <td class="tblLeft">
                        <asp:Label ID="sumCSTToPay" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <h5>
                Purchase CST Details</h5>
            <asp:GridView Style="font-family: 'Trebuchet MS'; font-size: 11px;" EmptyDataText="No Bills Found"
                HeaderStyle-HorizontalAlign="Left" CellPadding="2" Width="100%" ID="grdPurchaseCST"
                GridLines="Both" SkinID="GrdNoPaging" CssClass="gridview" AlternatingRowStyle-CssClass="even"
                AutoGenerateColumns="False" runat="server" OnRowDataBound="grdPurchaseCST_RowDataBound"
                ShowFooter="true" ForeColor="#333333">
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
                    <asp:TemplateField HeaderText="Supplier Name - Tin#">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblLedger" runat="server"
                                Text='<%# Eval("LedgerName") %>' />
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px; color: Red; font-weight: bold"
                                ID="lblTinNumber" runat="server" Text='<%# Eval("TinNumber") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CST (%)">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblCST" runat="server"
                                Text='<%# Eval("CST") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Actual Rate">
                        <ItemTemplate>
                            <asp:Label ID="lblRate" Style="font-family: 'Trebuchet MS'; font-size: 11px;" runat="server"
                                Text='<%# Eval("Rate") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Purchase CST">
                        <ItemTemplate>
                            <asp:Label ID="lblCSTRate" Style="font-family: 'Trebuchet MS'; font-size: 11px;"
                                runat="server" Text='<%# Eval("CSTRate") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CST paid">
                        <ItemTemplate>
                            <asp:Label ID="lblCSTPaid" Style="font-family: 'Trebuchet MS'; font-size: 11px;"
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
                Sales Return CST Details</h5>
            <asp:GridView Style="font-family: 'Trebuchet MS'; font-size: 11px;" ShowFooter="true"
                EmptyDataText="No Bills Found" HeaderStyle-HorizontalAlign="Left" CellPadding="2"
                Width="100%" ID="grdSalesReturnCST" GridLines="Both" SkinID="GrdNoPaging" CssClass="gridview"
                AlternatingRowStyle-CssClass="even" AutoGenerateColumns="False" runat="server"
                OnRowDataBound="grdSalesReturnCST_RowDataBound" ForeColor="#333333">
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
                                ID="lblTinNumber" runat="server" Text='<%# Eval("TinNumber") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CST (%)">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblCST" runat="server"
                                Text='<%# Eval("CST") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Actual Rate">
                        <ItemTemplate>
                            <asp:Label ID="lblRate" Style="font-family: 'Trebuchet MS'; font-size: 11px;" runat="server"
                                Text='<%# Eval("Rate") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Purchase CST">
                        <ItemTemplate>
                            <asp:Label ID="lblCSTRate" Style="font-family: 'Trebuchet MS'; font-size: 11px;"
                                runat="server" Text='<%# Eval("CSTRate") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CST paid">
                        <ItemTemplate>
                            <asp:Label ID="lblCSTPaid" Style="font-family: 'Trebuchet MS'; font-size: 11px;"
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
                Sales CST Details</h5>
            <asp:GridView Style="font-family: 'Trebuchet MS'; font-size: 11px;" ShowFooter="true"
                EmptyDataText="No Bills Found" HeaderStyle-HorizontalAlign="Left" CellPadding="2"
                Width="100%" ID="grdSalesCST" GridLines="Both" SkinID="GrdNoPaging" CssClass="gridview"
                AlternatingRowStyle-CssClass="even" AutoGenerateColumns="False" runat="server"
                OnRowDataBound="grdSalesCST_RowDataBound" ForeColor="#333333">
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
                    <asp:TemplateField HeaderText="CST (%)">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblCST" runat="server"
                                Text='<%# Eval("CST") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cusomer Name - Tin#">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblLedger" runat="server"
                                Text='<%# Eval("LedgerName") %>' />
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px; color: Red; font-weight: bold"
                                ID="lblTinNumber" runat="server" Text='<%# Eval("TinNumber") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Actual Rate">
                        <ItemTemplate>
                            <asp:Label ID="lblRate" Style="font-family: 'Trebuchet MS'; font-size: 11px;" runat="server"
                                Text='<%# Eval("Rate") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Purchase CST">
                        <ItemTemplate>
                            <asp:Label ID="lblCSTRate" Style="font-family: 'Trebuchet MS'; font-size: 11px;"
                                runat="server" Text='<%# Eval("CSTRate") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CST paid">
                        <ItemTemplate>
                            <asp:Label ID="lblCSTPaid" Style="font-family: 'Trebuchet MS'; font-size: 11px;"
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
                Purchase Return CST Details</h5>
            <asp:GridView Style="font-family: 'Trebuchet MS'; font-size: 11px;" ShowFooter="true"
                EmptyDataText="No Bills Found" HeaderStyle-HorizontalAlign="Left" CellPadding="2"
                Width="100%" ID="grdPurchaseReturnCST" GridLines="Both" SkinID="GrdNoPaging"
                CssClass="gridview" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="False"
                runat="server" OnRowDataBound="grdPurchaseReturnCST_RowDataBound" ForeColor="#333333">
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
                    <asp:TemplateField HeaderText="CST (%)">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblCST" runat="server"
                                Text='<%# Eval("CST") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Supplier Name - Tin#">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblLedger" runat="server"
                                Text='<%# Eval("LedgerName") %>' />
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px; color: Red; font-weight: bold"
                                ID="lblTinNumber" runat="server" Text='<%# Eval("TinNumber") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Actual Rate">
                        <ItemTemplate>
                            <asp:Label ID="lblRate" Style="font-family: 'Trebuchet MS'; font-size: 11px;" runat="server"
                                Text='<%# Eval("Rate") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Purchase CST">
                        <ItemTemplate>
                            <asp:Label ID="lblCSTRate" Style="font-family: 'Trebuchet MS'; font-size: 11px;"
                                runat="server" Text='<%# Eval("CSTRate") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CST paid">
                        <ItemTemplate>
                            <asp:Label ID="lblCSTPaid" Style="font-family: 'Trebuchet MS'; font-size: 11px;"
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
        </div>
    </div>
    </form>
</body>
</html>
