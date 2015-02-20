<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VATSumaryReport1.aspx.cs"
    Inherits="VATSumaryReport1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>VAT Summary Report</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <link rel="Stylesheet" href="App_Themes/DefaultTheme/DefaultTheme.css" />
    <link rel="Stylesheet" href="App_Themes/DefaultTheme/calendar.css" />
    <script language="javascript" type="text/javascript" src="Scripts/calendar_eu.js"></script>
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
    <br />
    <br />
    <div align="center" style="min-height: 300px">
        <div id="div1" runat="server">
        <table cellpadding="2" cellspacing="2" width="500px" border="0" style="border: 1px solid blue; background-color:White;
            height: 100%;">
            <tr>
                <td colspan="4" class="subHeadFont2">
                    VAT Summary Report
                </td>
            </tr>
            <tr>
                <td  style="width:30%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                    Start Date
                </td>
                <td class="ControlTextBox3" width="25%">
                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="cssTextBox" Width="100px"
                        MaxLength="10" />
                </td>
                <td width="15%" align="left">
                    <script language="JavaScript">
                        new tcal({ 'formname': 'form1', 'controlname': 'txtStartDate' });</script>
                </td>
                <td align="left" width="15%">
                    <asp:RequiredFieldValidator class="lblFont" CssClass="lblFont" ID="RequiredFieldValidator1"
                        runat="server" ControlToValidate="txtStartDate" Display="None" ErrorMessage="Please Enter Start Date"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td  style="width:30%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                    End Date
                </td>
                <td class="ControlTextBox3" width="25%">
                    <asp:TextBox ID="txtEndDate" CssClass="cssTextBox" runat="server" Width="100px" MaxLength="10" />
                </td>
                <td width="15%" align="left">
                    <script language="JavaScript">
                        new tcal({ 'formname': 'form1', 'controlname': 'txtEndDate' });</script>
                </td>
                <td align="left" width="15%">
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
                            <td style="width:30%">
                            </td>
                            <td style="width:20%">
                                <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" CssClass="NewReport6"
                                    EnableTheming="false" />
                            </td>
                            <td style="width:20%">
                                <input type="button" class="printbutton6" id="Button2" runat="Server" onclick="javascript:CallPrint('divPrint')" />
                            </td>
                            <td style="width:20%">
                            
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        </div>
        <asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="true" ShowSummary="false" />
        <br />
        <div>
            <table>
                <tr>
                    <td>
                        <input type="button" class="printbutton6" id="Button1" runat="Server" onclick="javascript:CallPrint('divPrint')" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="divPrint">
            <div id="dvVat" style="font-family: 'Trebuchet MS'; font-size: 11px;" runat="Server">
                <table style="border: 1px solid black; font-size: 11px; background-color: Aliceblue"
                    cellpadding="2" cellspacing="0" border="0" height="100%" width="700px">
                    <tr>
                        <td colspan="3" align="right" style="height: 50px" valign="middle">
                            <%--<asp:Button ID="BtnVATDetailed" runat="server" OnClick="btnVATDetailedReport_Click"
                                CssClass="Button" Text="VAT Detailed Report" />--%>
                            <asp:Button ID="BtnVATDetailed" runat="server" CssClass="VatDetail" Height="45px" OnClick="btnVATDetailedReport_Click" CausesValidation="false"
                                EnableTheming="false"/>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" height="2%" class="subHeadFont2">
                            VAT Summary
                        </td>
                    </tr>
                    <tr>
                        <td rowspan="2" width="70%" style="border-right: 1px solid black;" align="left">
                            &nbsp;&nbsp;<b>Particulars</b>
                        </td>
                        <td colspan="2" width="30%" style="border-bottom: 1px solid black;">
                            <asp:Label Font-Bold="true" ID="lblCompany" runat="server"></asp:Label><br />
                            <asp:Label CssClass="left" ID="lblFromDate" runat="server"></asp:Label>
                            &nbsp;to&nbsp;<asp:Label ID="lblToDate" CssClass="left" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="border-right: 1px solid black;">
                            <b>Assessable Value</b>
                        </td>
                        <td>
                            <b>TAX Amount</b>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" align="left" style="border-top: 1px solid black;">
                            <b><u>Sales</u></b>
                            <br />
                            <br />
                            <b>&nbsp;&nbsp;<u>A. Output Tax</u></b><br />
                            <asp:GridView Style="font-family: 'Trebuchet MS'; font-size: 11px;" RowStyle-BackColor="Azure"
                                AlternatingRowStyle-BackColor="Azure" ShowHeader="false" EmptyDataText="No VAT Found"
                                HeaderStyle-HorizontalAlign="Left" CellPadding="2" Width="100%" ID="grdSalesVat"
                                GridLines="None" AutoGenerateColumns="False" runat="server" ForeColor="#333333">
                                <Columns>
                                    <asp:TemplateField ItemStyle-Width="70%">
                                        <ItemTemplate>
                                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblOutputTax"
                                                runat="server" Text='<%# Eval("OutputTax") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label Visible="true" Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblActual"
                                                runat="server" Text='<%# Eval("Actual")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblVat" runat="server"
                                                Text='<%# Eval("Vat") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <table width="100%">
                                <tr>
                                    <td width="70%" align="center" style="font-family: 'Trebuchet MS'; font-size: 11px;">
                                        <b>Total Output Tax</b>
                                    </td>
                                    <td width="15%" align="right">
                                        <asp:Label ID="lblActualSales" runat="server" Style="font-family: 'Trebuchet MS';
                                            font-size: 11px;"></asp:Label>
                                    </td>
                                    <td width="15%" align="right">
                                        <asp:Label ID="lblVatSales" runat="server" Style="font-family: 'Trebuchet MS'; font-size: 11px;"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <br />
                            <br />
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" align="left">
                            <b><u>Purchase</u></b>
                            <br />
                            <br />
                            <b>&nbsp;&nbsp;<u>A. Input Tax</u></b><br />
                            <asp:GridView Style="font-family: 'Trebuchet MS'; font-size: 11px;" RowStyle-BackColor="Azure"
                                AlternatingRowStyle-BackColor="Azure" ShowHeader="false" EmptyDataText="No VAT Found"
                                HeaderStyle-HorizontalAlign="Left" CellPadding="2" Width="100%" ID="grdPurchaseVat"
                                GridLines="None" AutoGenerateColumns="False" runat="server" ForeColor="#333333">
                                <Columns>
                                    <asp:TemplateField ItemStyle-Width="70%">
                                        <ItemTemplate>
                                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblOutputTax"
                                                runat="server" Text='<%# Eval("OutputTax") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label Visible="true" Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblActual"
                                                runat="server" Text='<%# Eval("Actual")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblVat" runat="server"
                                                Text='<%# Eval("Vat") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <table width="100%">
                                <tr>
                                    <td width="70%" align="center" style="font-family: 'Trebuchet MS'; font-size: 11px;">
                                        <b>Total Input Credit</b>
                                    </td>
                                    <td width="15%" align="right">
                                        <asp:Label ID="lblActualPurchase" runat="server" Style="font-family: 'Trebuchet MS';
                                            font-size: 11px;"></asp:Label>
                                    </td>
                                    <td width="15%" align="right">
                                        <asp:Label ID="lblVatPurchase" runat="server" Style="font-family: 'Trebuchet MS';
                                            font-size: 11px;"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <br />
                            <br />
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="border-top: 1px solid black;">
                            <b>VAT Payable</b>
                        </td>
                        <td style="border-top: 1px solid black;">
                            &nbsp;
                        </td>
                        <td style="border-top: 1px solid black;" align="right">
                            <asp:Label ID="lblVatPayable" runat="server" Style="font-family: 'Trebuchet MS';
                                font-size: 11px;"></asp:Label>
                        </td>
                    </tr>
                </table>
                <br />
            </div>
        </div>
    </div>
    </form>
</body>
</html>
