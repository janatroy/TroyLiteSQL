<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BankStatementReport1.aspx.cs"
    Inherits="BankStatementReport1" %>

<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <title>Bank Statements Report</title>
    <link rel="Stylesheet" href="App_Themes/DefaultTheme/DefaultTheme.css" />
    <link rel="stylesheet" href="App_Themes/DefaultTheme/calendar.css" />
    <script type="text/javascript" language="JavaScript" src="Scripts/calendar_eu.js"></script>
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
    <script language="javascript" type="text/javascript" src="Scripts\calendar_eu.js"></script>
</head>
<body style="font-family: 'Trebuchet MS'; font-size: 11px;">
    <form id="form1" runat="server">
    <br />
    <div align="center" style="min-height: 300px">
        <div id="div1" runat="server">
        
        <table cellpadding="1" cellspacing="2" width="450px" border="0" style="border: 1px solid blue; background-color:White;
            text-align: left">
            <tr>
                <td colspan="4" class="subHeadFont2">
                    Bank Statement Report
                </td>
            </tr>
            <tr style="height:6px">

            </tr>
            <tr>
                <td  style="width:30%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                    Bank Name
                    <asp:CompareValidator ID="cvBank" runat="server" ControlToValidate="drpBankName"
                        EnableClientScript="true" Display="Dynamic" ErrorMessage="Bank is Mandatory"
                        Operator="GreaterThan" Text="*" ValueToCompare="0"></asp:CompareValidator>
                </td>
                <td  style="width:20%" class="ControlDrpBorder">
                    <asp:DropDownList ID="drpBankName" runat="server" Width="100%" DataTextField="LedgerName" style="border: 1px solid #90c9fc" height="26px"
                        DataValueField="LedgerID"  CssClass="drpDownListMedium" BackColor = "#90c9fc">
                        <asp:ListItem Selected="True" Value="0" style="background-color: #bce1fe">Select Bank</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="width:20%">
                </td>
                <td  style="width:10%">
                </td>
            </tr>
            <tr>
                <td  style="width:30%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                    Start Date
                </td>
                <td class="ControlTextBox3" style="width:25%">
                    <asp:TextBox ID="txtStartDate" CssClass="cssTextBox" Width="100px" MaxLength="10"
                        runat="server" />
                </td>
                <td style="width:20%">
                    <script type="text/javascript" language="JavaScript">
                        new tcal({ 'formname': 'form1', 'controlname': 'txtStartDate' });</script>
                </td>
                <td align="left" width="10%">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtStartDate"
                        Display="None" CssClass="lblFont" ErrorMessage="Please Enter Start Date"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td  style="width:30%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                    End Date
                </td>
                <td class="ControlTextBox3"  style="width:25%">
                    <asp:TextBox ID="txtEndDate" CssClass="cssTextBox" Width="100px" MaxLength="10" runat="server" />
                </td>
                <td style="width:20%">
                    <script type="text/javascript" language="JavaScript">
                        new tcal({ 'formname': 'form1', 'controlname': 'txtEndDate' });</script>
                </td>
                <td align="left" style="width:10%">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEndDate"
                        Display="None" ErrorMessage="Please Enter The End Date" CssClass="lblFont"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtStartDate"
                        ControlToValidate="txtEndDate" Display="None" ErrorMessage="Start Date Should Be Less Than the End Date"
                        CssClass="lblFont" Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
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
                                <asp:Button ID="Button2" runat="server" OnClick="btnReport_Click" CssClass="NewReport6"
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
        
        <div id="bnkPanel" runat="server" visible="false">
            
            <table width="700px">
                <tr>
                    <td style="width:40%">
                    </td>
                    <td style="width:19%">
                        <input type="button" class="printbutton6" id="Button3" runat="Server" onclick="javascript:CallPrint('divPrint')" />        
                    </td>
                    <td style="width:10%">
                        <asp:Button ID="btndet" CssClass="GoBack" EnableTheming="false" runat="server"
                                        OnClick="btndet_Click" Visible="False" />
                    </td>
                    <td style="width:31%">
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
                                Bank Statement From
                                <asp:Label ID="lblStartDate" runat="server"> </asp:Label>
                                To
                                <asp:Label ID="lblEndDate" runat="server"> </asp:Label></h5>
                        </td>
                    </tr>
                </table>
                <br />
                <wc:ReportGridView runat="server" BorderWidth="1" ID="gvBank" CssClass="gridview"
                    GridLines="Both" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                    AllowPrintPaging="true" Width="700px" Style="font-family: 'Trebuchet MS';
                    font-size: 11px;" OnRowDataBound="gvBank_RowDataBound">
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
                        <asp:BoundField ItemStyle-Width="20%" DataField="VoucherType" HeaderText="Voucher Type" />
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
                        <td width="540px" align="right">
                            <b>Total :</b>
                        </td>
                        <td width="80px" align="right">
                            <hr />
                            <asp:Label ID="lblDebitSum" runat="server"></asp:Label><hr />
                        </td>
                        <td width="80px" align="right">
                            <hr />
                            <asp:Label ID="lblCreditSum" runat="server"></asp:Label><hr />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <b>Difference :</b>
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
    </form>
</body>
</html>
