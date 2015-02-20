<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProfitAndLossReport1.aspx.cs"
    Inherits="ProfitAndLossReport1" %>

<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <title>Profit And Loss Report</title>
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
        function switchViews(obj, imG) {
            var div = document.getElementById(obj);
            var img = document.getElementById(imG);

            if (div.style.display == "none") {
                div.style.display = "inline";


                img.src = "App_Themes/DefaultTheme/Images/minus.gif";

            }
            else {
                div.style.display = "none";
                img.src = "App_Themes/DefaultTheme/Images/plus.gif";

            }
        }

    </script>
    <link rel="stylesheet" href="App_Themes/DefaultTheme/calendar.css" />
    <script type="text/javascript" language="JavaScript" src="Scripts/calendar_eu.js"></script>
    <link rel="Stylesheet" href="App_Themes/DefaultTheme/DefaultTheme.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <br />
        <div id="div1" runat="server">
        <table cellpadding="1" cellspacing="2" width="450px" border="0" style="border: 1px solid blue; background-color:White;
            text-align: left">
            <tr>
                <td colspan="4" class="subHeadFont2">
                    Profit and Loss Report
                </td>
            </tr>
            <tr style="height:6px">
            
            </tr>
            <tr>
                <td  style="width:30%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                    Start Date
                </td>
                <td class="ControlTextBox3" width="25%">
                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="cssTextBox" Width="100px"
                        MaxLength="10" />
                </td>
                <td width="20%">
                    <script type="text/javascript" language="JavaScript">
                        new tcal({ 'formname': 'form1', 'controlname': 'txtStartDate' });</script>
                </td>
                <td align="left" width="10%">
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
                <td width="15%">
                    <script type="text/javascript" language="JavaScript">
                        new tcal({ 'formname': 'form1', 'controlname': 'txtEndDate' });</script>
                </td>
                <td align="left" width="10%">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEndDate"
                        Display="None" CssClass="lblFont" ErrorMessage="Please Enter The End Date"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" CssClass="lblFont" ControlToCompare="txtStartDate"
                        ControlToValidate="txtEndDate" Display="None" ErrorMessage="Start Date Should Be Less Than the End Date"
                        Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
                </td>
            </tr>
            <tr style="height:6px">
            
            </tr>
            <tr>
                <td colspan="4">
                    <table width="100%">
                        <tr>
                            <td  width="20%">
                            </td>
                            <td width="20%">
                                <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" CssClass="NewReport6"
                                        EnableTheming="false" />
                            </td>
                            <td width="25%">
                                <asp:Button ID="btndetails" CssClass="exportexl6" EnableTheming="false" runat="server"
                                    OnClick="btndetails_Click" />
                                
                            </td>
                            <td width="20%">
                            </td>
                        </tr>
                    </table>
                </td>
             </tr>
             
        </table>
        </div>
        <asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="true" ShowSummary="false" />
        <div id="divmain" runat="server" visible="false">
            <table style="width:700px">
                <tr>
                    <td style="width:40%">
                    </td>
                    <td style="width:20%">
                        <input type="button" id="Button2" runat="Server" onclick="javascript:CallPrint('divPrint')"
                            class="printbutton6" />
                    </td>
                    <td style="width:19%">
                        <asp:Button ID="btndet" CssClass="GoBack" EnableTheming="false" runat="server"
                                            OnClick="btndet_Click" Visible="False" />        
                    </td>
                    <td style="width:30%">
                    </td>
                </tr>
            </table>
            <br />
        <div id="divPrint" runat="server" style="font-family: 'Trebuchet MS'; font-size: 11px;"
            visible="false">
            
        
            <table cellpadding="2" cellspacing="0" width="700px" border="0" class="lblFont" style="border: 1px solid black">
                <tr class="subHeadFont2">
                    <td align="center" colspan="2">
                        <b>Profit & Loss Report </b>
                    </td>
                </tr>
                <tr>
                    <td style="border-right: 1px solid black;">
                        <table cellpadding="2" cellspacing="4" border="0" style="font-weight: bold;" class="lblFont">
                            <tr>
                                <td align="left">
                                    &nbsp;Opening Stock
                                </td>
                                <td align="right">
                                    &nbsp;<asp:Label ID="lblOpeningStock" Text="0" runat="server" CssClass="lblFont"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    &nbsp;Purchase A/c
                                </td>
                                <td align="right">
                                    &nbsp;<asp:Label ID="lblPurchaseTotal" Text="0" runat="server" CssClass="lblFont"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    &nbsp;Purchase Return
                                </td>
                                <td align="right">
                                    &nbsp;<asp:Label ID="lblPurchaseReturnTotal" Text="0" runat="server" CssClass="lblFont"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    &nbsp;
                                </td>
                                <td align="right" style="border-top: 1px solid black; border-bottom: 1px solid black;">
                                    &nbsp;<asp:Label ID="lblFirstMidTotal" Text="0" runat="server" CssClass="lblFont"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    &nbsp;
                                    <img onclick="javascript:switchViews('dvDX','imgDX');" id="imgDX" runat="server"
                                        src="App_Themes/DefaultTheme/Images/plus.gif" alt="Show" />
                                    &nbsp;Direct Expenses
                                </td>
                                <td align="right">
                                    &nbsp;<asp:Label ID="lblDXTotal" Text="0" runat="server" CssClass="lblFont"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="left">
                                    <div id="dvDX" style="display: none;" runat="server">
                                        <%--<asp:GridView ShowHeader="true" ShowFooter="false" Style="font-family: 'Trebuchet MS';
                                            font-size: 11px;" HeaderStyle-HorizontalAlign="Left" CellPadding="2"
                                            Width="100%" ID="gvDirectExp" GridLines="Both" CssClass="gridview" AlternatingRowStyle-CssClass="even"
                                            AutoGenerateColumns="False" runat="server" ForeColor="#333333">
                                            <Columns>
                                                <asp:BoundField DataField="Folionumber" ItemStyle-HorizontalAlign="Left" HeaderText="L.FNO" />
                                                <asp:BoundField DataField="LedgerName" ItemStyle-HorizontalAlign="Left" HeaderText="Ledger" />
                                                <asp:BoundField DataField="Expenses" ItemStyle-HorizontalAlign="Right" HeaderText="Amount" />
                                            </Columns>
                                        </asp:GridView>--%>
                                        <wc:ReportGridView runat="server" BorderWidth="1" ID="gvDirectExp" CssClass="gridview"
                                            GridLines="Both" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                            Width="100%" Style="font-family: 'Trebuchet MS'; font-size: 11px;">
                                            <HeaderStyle CssClass="ReportHeadataRow" />
                                            <RowStyle CssClass="ReportdataRow" />
                                            <AlternatingRowStyle CssClass="ReportAltdataRow" />
                                            <FooterStyle CssClass="ReportFooterRow"/>
                                            <PageHeaderTemplate>
                                                <br />
                                                <br />
                                            </PageHeaderTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="Folionumber" ItemStyle-HorizontalAlign="Left" HeaderText="L.FNO" />
                                                <asp:BoundField DataField="LedgerName" ItemStyle-HorizontalAlign="Left" HeaderText="Ledger" />
                                                <asp:BoundField DataField="Expenses" ItemStyle-HorizontalAlign="Right" HeaderText="Amount" />
                                            </Columns>
                                        </wc:ReportGridView>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    &nbsp;Gross Profit
                                </td>
                                <td align="right" style="border-top: 1px solid black; border-bottom: 1px solid black;">
                                    &nbsp;<asp:Label ID="lblGP" Text="0" runat="server" CssClass="lblFont"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <table cellpadding="2" cellspacing="4" width="70%" border="0" style="font-weight: bold;"
                            class="lblFont">
                            <tr>
                                <td align="left">
                                    &nbsp;Closing Stock
                                </td>
                                <td align="right">
                                    &nbsp;<asp:Label ID="lblClosingStock" Text="0" runat="server" CssClass="lblFont"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    &nbsp;Sales A/c
                                </td>
                                <td align="right">
                                    &nbsp;<asp:Label ID="lblSalesTotal" Text="0" runat="server" CssClass="lblFont"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    &nbsp;Sales Return
                                </td>
                                <td align="right">
                                    &nbsp;<asp:Label ID="lblSalesReturnTotal" Text="0" runat="server" CssClass="lblFont"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    &nbsp;
                                </td>
                                <td align="right" style="border-top: 1px solid black; border-bottom: 1px solid black;">
                                    &nbsp;<asp:Label ID="lblSecondMidTotal" Text="0" runat="server" CssClass="lblFont"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    &nbsp;
                                    <img onclick="javascript:switchViews('dvDI','imgDI');" id="imgDI" runat="server"
                                        src="App_Themes/DefaultTheme/Images/plus.gif" alt="Show" />
                                    &nbsp;Direct Income
                                </td>
                                <td align="right">
                                    &nbsp;<asp:Label ID="lblDIncome" Text="0" runat="server" CssClass="lblFont"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="left">
                                    <div id="dvDI" style="display: none;" runat="server">
                                        <%--<asp:GridView ShowHeader="true" ShowFooter="false" Style="font-family: 'Trebuchet MS';
                                            font-size: 11px;" HeaderStyle-HorizontalAlign="Left" CellPadding="2"
                                            Width="100%" ID="gvDirectInc" GridLines="Both" CssClass="gridview" AlternatingRowStyle-CssClass="even"
                                            AutoGenerateColumns="False" runat="server" ForeColor="#333333">
                                            <Columns>
                                                <asp:BoundField DataField="Folionumber" ItemStyle-HorizontalAlign="Left" HeaderText="L.FNO" />
                                                <asp:BoundField DataField="LedgerName" ItemStyle-HorizontalAlign="Left" HeaderText="Ledger" />
                                                <asp:BoundField DataField="Expenses" ItemStyle-HorizontalAlign="Right" HeaderText="Amount" />
                                            </Columns>
                                        </asp:GridView>--%>
                                        <wc:ReportGridView runat="server" BorderWidth="1" ID="gvDirectInc" CssClass="gridview"
                                            GridLines="Both" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                            Width="100%" Style="font-family: 'Trebuchet MS'; font-size: 11px;">
                                            <HeaderStyle CssClass="ReportHeadataRow" />
                                            <RowStyle CssClass="ReportdataRow" />
                                            <AlternatingRowStyle CssClass="ReportAltdataRow" />
                                            <FooterStyle CssClass="ReportFooterRow"/>
                                            <PageHeaderTemplate>
                                                <br />
                                                <br />
                                            </PageHeaderTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="Folionumber" ItemStyle-HorizontalAlign="Left" HeaderText="L.FNO" />
                                                <asp:BoundField DataField="LedgerName" ItemStyle-HorizontalAlign="Left" HeaderText="Ledger" />
                                                <asp:BoundField DataField="Expenses" ItemStyle-HorizontalAlign="Right" HeaderText="Amount" />
                                            </Columns>
                                        </wc:ReportGridView>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    &nbsp;Gross Loss
                                </td>
                                <td align="right" style="border-top: 1px solid black; border-bottom: 1px solid black;">
                                    &nbsp;<asp:Label ID="lblGL" Text="0" runat="server" CssClass="lblFont"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="border-right: 1px solid black;">
                        <table cellpadding="2" cellspacing="4" width="70%" border="0" style="font-weight: bold;"
                            class="lblFont">
                            <tr>
                                <td align="left">
                                    &nbsp;
                                    <img onclick="javascript:switchViews('dvIDX','imgIDX');" id="imgIDX" runat="server"
                                        src="App_Themes/DefaultTheme/Images/plus.gif" alt="Show" />
                                    &nbsp;Indirect Expenses
                                </td>
                                <td align="right">
                                    &nbsp;<asp:Label ID="lblIDXExptotal" Text="0" runat="server" CssClass="lblFont"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="left">
                                    <div id="dvIDX" style="display: none; position: relative;" runat="server">
                                        <%--<asp:GridView ShowHeader="true" Style="font-family: 'Trebuchet MS';
                                            font-size: 11px;" HeaderStyle-HorizontalAlign="Left" OnPageIndexChanging="gvIDirectExp_PageIndexChanging"
                                            Width="100%" ID="gvIDirectExp" GridLines="Both" CssClass="gridview" AlternatingRowStyle-CssClass="even"
                                            AutoGenerateColumns="False" runat="server" ForeColor="#333333">
                                            <Columns>
                                                <asp:BoundField DataField="Folionumber" ItemStyle-HorizontalAlign="Left" HeaderText="L.FNO" />
                                                <asp:BoundField DataField="LedgerName" ItemStyle-HorizontalAlign="Left" HeaderText="Ledger" />
                                                <asp:BoundField DataField="Expenses" ItemStyle-HorizontalAlign="Right" HeaderText="Amount" />
                                            </Columns>
                                        </asp:GridView>--%>
                                        <wc:ReportGridView runat="server" BorderWidth="1" ID="gvIDirectExp" CssClass="gridview"
                                            GridLines="Both" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                            Width="100%" Style="font-family: 'Trebuchet MS'; font-size: 11px;">
                                            <HeaderStyle CssClass="ReportHeadataRow" />
                                            <RowStyle CssClass="ReportdataRow" />
                                            <AlternatingRowStyle CssClass="ReportAltdataRow" />
                                            <FooterStyle CssClass="ReportFooterRow"/>
                                            <PageHeaderTemplate>
                                                <br />
                                                <br />
                                            </PageHeaderTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="Folionumber" ItemStyle-HorizontalAlign="Left" HeaderText="L.FNO" />
                                                <asp:BoundField DataField="LedgerName" ItemStyle-HorizontalAlign="Left" HeaderText="Ledger" />
                                                <asp:BoundField DataField="Expenses" ItemStyle-HorizontalAlign="Right" HeaderText="Amount" />
                                            </Columns>
                                        </wc:ReportGridView>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    &nbsp;Net Profit
                                </td>
                                <td align="right" style="border-top: 1px solid black; border-bottom: 1px solid black;">
                                    &nbsp;<asp:Label ID="lblNetProfit" Text="0" runat="server" CssClass="lblFont"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <table cellpadding="2" cellspacing="4" width="70%" border="0" style="font-weight: bold;"
                            class="lblFont">
                            <tr>
                                <td align="left">
                                    &nbsp;
                                    <img onclick="javascript:switchViews('dvIDI','imgIDI');" id="imgIDI" runat="server"
                                        src="App_Themes/DefaultTheme/Images/plus.gif" alt="Show" />
                                    &nbsp;Indirect Income
                                </td>
                                <td align="right">
                                    &nbsp;<asp:Label ID="lblIDIncome" Text="0" runat="server" CssClass="lblFont"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="left">
                                    <div id="dvIDI" style="display: none;" runat="server">
                                        <%--<asp:GridView ShowHeader="true" ShowFooter="false" Style="font-family: 'Trebuchet MS';
                                            font-size: 11px;" HeaderStyle-HorizontalAlign="Left" CellPadding="2"
                                            Width="100%" ID="gvIDirectInc" GridLines="Both" CssClass="gridview" AlternatingRowStyle-CssClass="even"
                                            AutoGenerateColumns="False" runat="server" ForeColor="#333333">
                                            <Columns>
                                                <asp:BoundField DataField="Folionumber" ItemStyle-HorizontalAlign="Left" HeaderText="L.FNO" />
                                                <asp:BoundField DataField="LedgerName" ItemStyle-HorizontalAlign="Left" HeaderText="Ledger" />
                                                <asp:BoundField DataField="Expenses" ItemStyle-HorizontalAlign="Right" HeaderText="Amount" />
                                            </Columns>
                                        </asp:GridView>--%>
                                        <wc:ReportGridView runat="server" BorderWidth="1" ID="gvIDirectInc" CssClass="gridview"
                                            GridLines="Both" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                            Width="100%" Style="font-family: 'Trebuchet MS'; font-size: 11px;">
                                            <HeaderStyle CssClass="ReportHeadataRow" />
                                            <RowStyle CssClass="ReportdataRow" />
                                            <AlternatingRowStyle CssClass="ReportAltdataRow" />
                                            <FooterStyle CssClass="ReportFooterRow"/>
                                            <PageHeaderTemplate>
                                                <br />
                                                <br />
                                            </PageHeaderTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="Folionumber" ItemStyle-HorizontalAlign="Left" HeaderText="L.FNO" />
                                                <asp:BoundField DataField="LedgerName" ItemStyle-HorizontalAlign="Left" HeaderText="Ledger" />
                                                <asp:BoundField DataField="Expenses" ItemStyle-HorizontalAlign="Right" HeaderText="Amount" />
                                            </Columns>
                                        </wc:ReportGridView>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    &nbsp;Net Loss
                                </td>
                                <td align="right" style="border-top: 1px solid black; border-bottom: 1px solid black;">
                                    &nbsp;<asp:Label ID="lblNetLoss" Text="0" runat="server" CssClass="lblFont"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        </div>
    </div>
    </form>
</body>
</html>
