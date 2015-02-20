<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CSTSumaryReport1.aspx.cs"
    Inherits="CSTSumaryReport1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <title>CST Summary Report</title>
    <link href="App_Themes/DefaultTheme/calendar.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="Scripts/calendar_eu.js"></script>
    <link href="App_Themes/DefaultTheme/DefaultTheme.css" rel="stylesheet" type="text/css" />
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
        <br />
        <div align="center">
            <div align="center" runat="server" id="div1">
        <table cellpadding="2" cellspacing="2" width="500px" border="0" style="border: 1px solid blue; background-color:White;
            text-align: left">
            <tr>
                <td colspan="4" class="subHeadFont2">
                    CST Summary Report
                </td>
            </tr>
            <tr>
                <td  style="width:30%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                    Start Date
                </td>
                <td class="ControlTextBox3" width="20%">
                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="cssTextBox" Width="100px"
                        MaxLength="10" />
                </td>
                <td>
                    <script type="text/javascript" language="JavaScript">
                        new tcal({ 'formname': 'form1', 'controlname': 'txtStartDate' });</script>
                </td>
                <td align="left" width="20%">
                    <asp:RequiredFieldValidator class="lblFont" CssClass="lblFont" ID="RequiredFieldValidator1"
                        runat="server" ControlToValidate="txtStartDate" Display="None" ErrorMessage="Please Enter Start Date"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td  style="width:30%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                    End Date
                </td>
                <td class="ControlTextBox3" width="20%">
                    <asp:TextBox ID="txtEndDate" CssClass="cssTextBox" runat="server" Width="100px" MaxLength="10" />
                </td>
                <td>
                    <script type="text/javascript" language="JavaScript">
                        new tcal({ 'formname': 'form1', 'controlname': 'txtEndDate' });</script>
                </td>
                <td align="left">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEndDate"
                        Display="None" CssClass="lblFont" ErrorMessage="Please Enter The End Date"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" CssClass="lblFont" ControlToCompare="txtStartDate"
                        ControlToValidate="txtEndDate" Display="None" ErrorMessage="Start Date Should Be Less Than the End Date"
                        Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <table width="100%">
                        <tr>
                            <td style="width:22%">

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
        <div id="dvCST" style="font-family: 'Trebuchet MS'; font-size: 11px;" runat="Server">
            <table style="border: 1px solid black; font-size: 11px; background-color: Aliceblue"
                cellpadding="2" cellspacing="0" border="0" height="100%" width="700px">
                <tr>
                    <td colspan="3" align="right" style="height: 50px">
                        <%--<asp:Button ID="BtnCSTDetailed" runat="server" OnClick="btnCSTDetailedReport_Click"
                            CssClass="Button" Text="CST Detailed Report" />--%>
                            <asp:Button ID="BtnCSTDetailed" runat="server" CssClass="CstDetail" Height="45px" OnClick="btnCSTDetailedReport_Click" CausesValidation="false"
                                   EnableTheming="false"/>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" height="2%" class="subHeadFont2">
                        CST Summary
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
                            AlternatingRowStyle-BackColor="Azure" ShowHeader="false" EmptyDataText="No CST Found"
                            HeaderStyle-HorizontalAlign="Left" CellPadding="2" Width="100%" ID="grdSalesCST"
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
                                        <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblCST" runat="server"
                                            Text='<%# Eval("CST") %>' />
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
                                    <asp:Label ID="lblCSTSales" runat="server" Style="font-family: 'Trebuchet MS'; font-size: 11px;"></asp:Label>
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
                            AlternatingRowStyle-BackColor="Azure" ShowHeader="false" EmptyDataText="No CST Found"
                            HeaderStyle-HorizontalAlign="Left" CellPadding="2" Width="100%" ID="grdPurchaseCST"
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
                                        <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblCST" runat="server"
                                            Text='<%# Eval("CST") %>' />
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
                                    <asp:Label ID="lblCSTPurchase" runat="server" Style="font-family: 'Trebuchet MS';
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
                        <b>CST Payable</b>
                    </td>
                    <td style="border-top: 1px solid black;">
                        &nbsp;
                    </td>
                    <td style="border-top: 1px solid black;" align="right">
                        <asp:Label ID="lblCSTPayable" runat="server" Style="font-family: 'Trebuchet MS';
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
