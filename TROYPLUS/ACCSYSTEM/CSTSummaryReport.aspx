<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="CSTSummaryReport.aspx.cs" Inherits="CSTSummaryReport" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <table cellpadding="2" cellspacing="4" width="600px" border="0" style="border: 1px solid silver;
        background-image: url('App_Themes/DefaultTheme/Images/bluebg.jpg');">
        <tr>
            <td colspan="3" style="background-image: url('App_Themes/DefaultTheme/Images/bgReportheader.jpg');
                color: White; background-repeat: repeat-x; font-size: 11px; font-weight: bold;">
                CST Summary Report
            </td>
        </tr>
        <tr>
            <td class="lblFont" align="right" width="20%">
                <b>Start Date:</b>:
            </td>
            <td class="ControlTextBox3" align="left" width="40%">
                <asp:TextBox ID="txtStartDate" runat="server" CssClass="lblFont" Width="100px" MaxLength="10" />
                
            </td>
            <td align="left" width="40%">
                <script language="JavaScript">                    new tcal({ 'formname': 'aspnetForm', 'controlname': 'ctl00$cplhControlPanel$txtStartDate' });</script>
                <asp:RequiredFieldValidator class="lblFont" CssClass="lblFont" ID="RequiredFieldValidator1"
                    runat="server" ControlToValidate="txtStartDate" Display="None" ErrorMessage="Please Enter Start Date"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right" class="lblFont">
                <b>End Date:</b>
            </td>
            <td align="left" class="ControlTextBox3">
                <asp:TextBox ID="txtEndDate" CssClass="lblFont" runat="server" Width="100px" MaxLength="10" />
                <%--<a href="javascript:NewCal('txtEndDate','ddmmyyyy',false,24)"><img src="cal.gif" width="16" height="16" border="0" alt="Pick a date"></a>--%>
                
            </td>
            <td align="left">
                <script language="JavaScript">                    new tcal({ 'formname': 'aspnetForm', 'controlname': 'ctl00$cplhControlPanel$txtEndDate' });</script>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEndDate"
                    Display="None" CssClass="lblFont" ErrorMessage="Please Enter The End Date"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" CssClass="lblFont" ControlToCompare="txtStartDate"
                    ControlToValidate="txtEndDate" Display="None" ErrorMessage="Start Date Should Be Less Than the End Date"
                    Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td colspan="2" align="left">
                <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" SkinID="skinButtonBig"
                    Text="CST Summary Report" />
                &nbsp;<input type="button" value="Print " id="Button2" runat="Server" onclick="javascript:CallPrint('divPrint')"
                    class="button" />
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="true" ShowSummary="false" />
    <br />
    <div id="dvCST" visible="false" style="font-family: 'Trebuchet MS'; font-size: 11px;"
        runat="Server">
        <table style="border: 1px solid black; font-size: 11px; background-color: Aliceblue"
            cellpadding="2" cellspacing="0" border="0" height="100%" width="600px">
            <tr>
                <td colspan="3" align="right">
                    <a href="CSTReport.aspx">CST Detailed Report</a>
                </td>
            </tr>
            <tr>
                <td colspan="3" height="2%" class="accordionHeader">
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
</asp:Content>
