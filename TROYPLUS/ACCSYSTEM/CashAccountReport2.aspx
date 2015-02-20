<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CashAccountReport2.aspx.cs"
    Inherits="CashAccountReport2" %>

<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <title>Cash Account Report</title>
    <link href="App_Themes/DefaultTheme/DefaultTheme.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/DefaultTheme/calendar.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="Scripts/calendar_eu.js"></script>
    <script type="text/javascript" language="JavaScript">
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
    <br />
    <div align="center" style="min-height: 300px">
        <div id="div1" runat="server">
        <table cellpadding="2" cellspacing="2" width="450px" border="0" style="border: 1px solid blue; background-color:White;
            text-align: left">
            <tr>
                <td colspan="4" class="subHeadFont2">
                    Cash Account Report
                </td>
            </tr>
            <tr style="height:6px">

            </tr>
            <tr>
                <td class="ControlLabel2" width="35%">
                    Start Date
                </td>
                <td class="ControlTextBox3" width="25%">
                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="cssTextBox" Width="100px"
                        MaxLength="10" />
                </td>
                <td width="15%">
                    <script type="text/javascript" language="JavaScript">
                        new tcal({ 'formname': 'form1', 'controlname': 'txtStartDate' });</script>
                </td>
                <td align="left" width="15%">
                    <asp:RequiredFieldValidator class="lblFont" CssClass="lblFont" ID="RequiredFieldValidator1"
                        runat="server" ControlToValidate="txtStartDate" Display="None" ErrorMessage="Please Enter Start Date"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="ControlLabel2" width="35%">
                    End Date
                </td>
                <td class="ControlTextBox3" width="25%">
                    <asp:TextBox ID="txtEndDate" CssClass="cssTextBox" runat="server" Width="100px" MaxLength="10" />
                </td>
                <td width="15%">
                    <script type="text/javascript" language="JavaScript">
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
            <tr style="height:6px">

            </tr>
            
             <tr>
                <td colspan="4">
                    <table width="100%">
                        <tr>
                            <td style="width:31%">

                            </td>
                            <td style="width:19%">
                                <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" CssClass="NewReport6"
                                EnableTheming="false" />
                            </td>
                            <td style="width:19%">
                                <asp:Button ID="btndetails" CssClass="exportexl6" EnableTheming="false" runat="server"
                                    OnClick="btndetails_Click" />
                            </td>
                            <td style="width:31%">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
           
        </table>
        </div>
        <asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="true" ShowSummary="false" />
        
        <div id="CashPanel" runat="server" visible="false">
            &nbsp;
            <div id="divmain" runat="server">
                <table width="700px">
                    <tr>
                        <td colspan="3">
                            <table width="100%">
                                <tr>
                                    <td style="width:40%">

                                    </td>
                                    <td style="width:19%">
                                        <input type="button" class="printbutton6" id="Button1" runat="Server" onclick="javascript:CallPrint('divPrint')" />
                                    </td>
                                    <td style="width:10%">
                                        
                                        <asp:Button ID="btndet" CssClass="GoBack" EnableTheming="false" runat="server"
                                            OnClick="btndet_Click" Visible="False" />
                                    </td>
                                    <td style="width:31%">

                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <br />
            <div id="divPrint" style="font-family: 'Trebuchet MS'; font-size: 11px;">
                <asp:HiddenField ID="hdToDelete" Value="" runat="server" />
                <table width="700px" border="0" style="font-family: Trebuchet MS; font-size: 14px;">
                    <tr>
                        <td width="140px" align="left">
                            TIN#:
                            <asp:Label ID="lblTNGST" runat="server"></asp:Label>
                        </td>
                        <td align="center" width="420px" style="font-size: 20px;">
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
                        <td colspan="3">
                            <br />
                            <h5>
                                Cash Account From
                                <asp:Label ID="lblStartDate" runat="server"> </asp:Label>
                                To
                                <asp:Label ID="lblEndDate" runat="server"> </asp:Label></h5>
                        </td>
                    </tr>
                </table>
                <br />
                <wc:ReportGridView runat="server" BorderWidth="1" ID="gvCash" CssClass="gridview"
                    GridLines="Both" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                    PrintPageSize="45" AllowPrintPaging="true" Width="700px" OnRowDataBound="gvCash_RowDataBound">
                    <HeaderStyle CssClass="ReportHeadataRow" />
                    <RowStyle CssClass="ReportdataRow" />
                    <AlternatingRowStyle CssClass="ReportAltdataRow" />
                    <FooterStyle CssClass="ReportFooterRow" />
                    <PageHeaderTemplate>
                        <br />
                        <br />
                    </PageHeaderTemplate>
                    <Columns>
                        <asp:BoundField ItemStyle-Width="5%" DataField="Date" HeaderText="Date" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField ItemStyle-Width="53%" DataField="Particulars" HeaderText="Particulars" />
                        <asp:BoundField ItemStyle-Width="15%" DataField="VoucherType" HeaderText="Voucher Type" />
                        <asp:BoundField ItemStyle-Width="12%" DataField="Debit" HeaderStyle-HorizontalAlign="Right"
                            HeaderText="Debit" DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField ItemStyle-Width="12%" DataField="Credit" HeaderStyle-HorizontalAlign="Right"
                            HeaderText="Credit" DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="Right" />
                    </Columns>
                    <PagerTemplate>
                    </PagerTemplate>
                    <PageFooterTemplate>
                        <%-- Page <%# gvCashDetails.CurrentPrintPage.ToString() %> / <%# gvCashDetails.PrintPageCount%>--%>
                    </PageFooterTemplate>
                </wc:ReportGridView>
                <br />
                <table width="700px" border="0" cellspacing="0" cellpadding="1" style="font-family: 'Trebuchet MS';
                    font-size: 11px;">
                    <tr>
                        <td width="460px" align="right">
                            <b>Opening Balance :</b>
                        </td>
                        <td width="80px">
                            &nbsp;
                        </td>
                        <td width="80px" align="right">
                            <hr />
                            <asp:Label ID="lblOBDR" runat="server"></asp:Label><hr />
                        </td>
                        <td width="80px" align="right">
                            <hr />
                            <asp:Label ID="lblOBCR" runat="server"></asp:Label><hr />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <b>Total :</b>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td align="right">
                            <hr />
                            <asp:Label ID="lblDebitSum" runat="server"></asp:Label><hr />
                        </td>
                        <td align="right">
                            <hr />
                            <asp:Label ID="lblCreditSum" runat="server"></asp:Label><hr />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <%--<b>Difference :</b>--%>
                            <b>Closing Balance :</b>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td align="right">
                            <hr />
                            <asp:Label ID="lblDebitDiff" runat="server"></asp:Label><hr />
                        </td>
                        <td align="right">
                            <hr />
                            <asp:Label ID="lblCreditDiff" runat="server"></asp:Label><hr />
                        </td>
                    </tr>
                </table>
                <br />
                <br />
            </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
