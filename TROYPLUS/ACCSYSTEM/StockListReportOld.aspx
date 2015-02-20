<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StockListReportOld.aspx.cs"
    Inherits="StockListReportOld" %>

<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Stock Ledger Report</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <link href="App_Themes/DefaultTheme/DefaultTheme.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/DefaultTheme/calendar.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="Scripts/calendar_eu.js"></script>
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
    <div align="center">
        <asp:HiddenField ID="hdStock" runat="server" />
        <table style="border: 1px solid silver; background-color:White;" width="350px">
            <%--<tr class="mainConHd">
                <td colspan="4">
                    <span>Stock Ledger Report</span>
                </td>
            </tr>--%>
            <tr class="subHeadFont">
                <td colspan="3">
                    <table>
                        <tr>
                            <td>
                                Stock Ledger Report
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="ControlLabel" width="30%">
                    Start Date
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtStartDate"
                        Display="None" CssClass="lblFont" ErrorMessage="Please Enter Start Date"></asp:RequiredFieldValidator>
                </td>
                <td class="ControlTextBox3" width="10%">
                    <asp:TextBox ID="txtStartDate" CssClass="cssTextBox" Height="28px" MaxLength="10"
                        runat="server" />
                        
                </td>
                <td align="left" width="20%">
                    <script type="text/javascript" language="JavaScript">
                        new tcal({ 'formname': 'form1', 'controlname': 'txtStartDate' });</script>
                </td>
            </tr>
            <tr>
                <td class="ControlLabel" width="30%">
                    End Date
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEndDate"
                        Display="None" ErrorMessage="Please Enter The End Date" CssClass="lblFont"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtStartDate"
                        ControlToValidate="txtEndDate" Display="None" ErrorMessage="Start Date Should Be Less Than the End Date"
                        CssClass="lblFont" Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
                </td>
                <td class="ControlTextBox3" width="10%">
                    <asp:TextBox ID="txtEndDate" CssClass="cssTextBox" Height="28px" MaxLength="10" runat="server" />
                    
                </td>
                <td align="left" width="20%">
                    <script type="text/javascript" language="JavaScript">
                        new tcal({ 'formname': 'form1', 'controlname': 'txtEndDate' });</script>
                </td>
            </tr>
            <tr>
                <td class="ControlLabel" width="30%">
                    Product name
                </td>
                <td class="ControlDrpBorder" width="10%">
                 <asp:DropDownList ID="drpLedgerName" runat="server" Width="100%" CssClass="drpDownListMedium" BackColor = "#90C9FC" DataTextField="Productcode" DataValueField="Product" AutoPostBack="false" style="border: 1px solid #90c9fc" height="24px">
                                                                                                            </asp:DropDownList>
                  
                </td>
                <td width="20%">
                    
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <table width="100%">
                        <tr>
                            <td width="40%">
                            
                            </td>
                            <td width="20%">
                                <asp:Button ID="btnReport" runat="server" CssClass="generatebutton" EnableTheming="false"
                                    OnClick="btnReport_Click" />
                            </td>
                            <td width="30%">
                                <asp:CheckBox ID="chkvalue" runat="server" style="color:Blue" Text="With Value" Font-Names="arial" Font-Size="12px" AutoPostBack="true" Font-Bold="True" />
                            </td>
                        </tr>
                    </table>
                </td>
                
            </tr>
        </table>
        <div id="divReport" runat="server">
            <div id="divPrint" style="font-family: 'Trebuchet MS'; font-size: 11px;">
                <table width="100%" border="0" style="font-family: Trebuchet MS; font-size: 14px;">
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
                <table cellspacing="5" cellpadding="3" style="font-family: Trebuchet MS; font-size: 12px;
                    width: 100%; text-align: left">
                    <tr>
                        <td colspan="4">
                            <wc:ReportGridView runat="server" BorderWidth="1" ID="gvLedger" SkinID="gridview"
                                AutoGenerateColumns="false" AllowPrintPaging="true" Width="100%"
                                OnRowDataBound="gvLedger_RowDataBound">
                                <RowStyle CssClass="dataRow" />
                                <SelectedRowStyle CssClass="SelectdataRow" />
                                <AlternatingRowStyle CssClass="altRow" />
                                <EmptyDataRowStyle CssClass="HeadataRow" Font-Bold="true" />
                                <HeaderStyle CssClass="HeadataRow" />
                                <FooterStyle CssClass="dataRow" />
                                <PagerStyle CssClass="footer-row allPad" VerticalAlign="Middle" HorizontalAlign="Left" />
                                <PageHeaderTemplate>
                                    <br />
                                    <br />
                                </PageHeaderTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label Visible="true" Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblDate"
                                                runat="server" Text='<%# Eval("BillDate","{0:dd/MM/yyyy}")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="'Purchase/Sale'" HeaderText="Purchase/Sales" />
                                    <asp:BoundField DataField="billno" HeaderText="BillNo" />
                                    <asp:BoundField DataField="LedgerName" HeaderText="Customer/Supplier Name" />
                                    <asp:TemplateField HeaderText="Qty.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQty" Text='<%# Eval("Qty") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ClosingStock">
                                        <ItemTemplate>
                                            <asp:Label ID="lblClosingStock" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerTemplate>
                                </PagerTemplate>
                                <PageFooterTemplate>
                                    <br />
                                </PageFooterTemplate>
                            </wc:ReportGridView>
                        </td>
                    </tr>
                </table>
                <div style="text-align: left">
                    <table cellspacing="1" cellpadding="1" style="font-family: Trebuchet MS; font-size: 11px;
                        width: 450px; text-align: left; border: Solid 1px">
                        <tr>
                            <td style="width: 50%; text-align: right">
                                <b>Item Name</b>
                            </td>
                            <td>
                                :&nbsp;
                            </td>
                            <td style="width: 50%">
                                <asp:Label ID="lblItem" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <b>Model</b>
                            </td>
                            <td>
                                :&nbsp;
                            </td>
                            <td>
                                <asp:Label ID="lblModel" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <b>Opening Stock</b>
                            </td>
                            <td>
                                :&nbsp;
                            </td>
                            <td>
                                <asp:Label ID="lblOpenStock" runat="server" Text="0" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <b>Closing Stock as per below</b>
                            </td>
                            <td>
                                :&nbsp;
                            </td>
                            <td>
                                <asp:Label ID="lblClosingStock" Text="0" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <b>Closing stock as per product master</b>
                            </td>
                            <td>
                                :&nbsp;
                            </td>
                            <td>
                                <asp:Label ID="lblClosingStockPM" Text="0" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
