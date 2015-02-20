<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DayBookReport1.aspx.cs" Inherits="DayBookReport1" %>

<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <title>Day Book Report</title>
    <link href="App_Themes/DefaultTheme/calendar.css" rel="stylesheet" type="text/css" />
    <link rel="Stylesheet" href="App_Themes/DefaultTheme/DefaultTheme.css" />
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
    <div>
    </div>
    <br />
    <div align="center">
        <div id="div1" runat="server">
        <table cellpadding="2" cellspacing="2" width="450px" border="0" style="border: 1px solid blue; background-color:White;
            text-align: left">
            <tr>
                <td colspan="4" class="subHeadFont2">
                    Day Book Report
                </td>
            </tr>
            <tr style="height:6px">

            </tr>
            <tr>
                <td style="width:35%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
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
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtStartDate"
                        Display="None" ErrorMessage="Please Enter Start Date" CssClass="lblFont"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="width:35%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                    End Date
                </td>
                <td class="ControlTextBox3" width="25%">
                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="cssTextBox" Width="100px" MaxLength="10" />
                </td>
                <td width="15%">
                    <script type="text/javascript" language="JavaScript">
                        new tcal({ 'formname': 'form1', 'controlname': 'txtEndDate' });</script>
                </td>
                <td align="left" width="15%">
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
                                <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" CssClass="NewReport6"
                                    EnableTheming="false"  />
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
    </div>
    <div id="DayBookPanel" runat="server" visible="false">
        &nbsp;
        <table width="700px" align="center">
                <tr>
                    <td colspan="4">
                        <table width="100%">
                            <tr>
                                <td style="width:40%">
                                </td>
                                <td style="width:20%">
                                    <input type="button" class="printbutton6" id="Button1" runat="Server" onclick="javascript:CallPrint('divPrint')" />
                                </td>
                                <td style="width:10%">
                                    <asp:Button ID="btndet" CssClass="GoBack" EnableTheming="false" runat="server"
                                            OnClick="btndet_Click" Visible="False" />
                                    <asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="true" ShowSummary="false" />
                                </td>
                                <td style="width:30%">
                                
                                </td>
                            
                            </tr>
                         </table>
                    </td>
                </tr>
            </table>
            <br />
        <div id="divPrint" align="center" style="font-family: 'Trebuchet MS'; font-size: 11px;">
            <table width="700px" align="center" border="0" style="font-family: Trebuchet MS; font-size: 14px;">
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
                            DayBook From
                            <asp:Label ID="lblStartDate" runat="server"> </asp:Label>
                            To
                            <asp:Label ID="lblEndDate" runat="server"> </asp:Label></h5>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        Opening Balance : &nbsp;
                        <asp:Label ID="lblOB" runat="server" CssClass="lblFont"></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
            <wc:ReportGridView runat="server" BorderWidth="1" ID="gvLedger" ShowFooter="true"
                CssClass="gridview" GridLines="Both" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                PrintPageSize="45" AllowPrintPaging="true" Width="700px" Style="font-family: 'Trebuchet MS';
                font-size: 11px;" CellPadding="5" OnRowDataBound="gvLedger_RowDataBound">
                <HeaderStyle CssClass="ReportHeadataRow" />
                <RowStyle CssClass="ReportdataRow" />
                <AlternatingRowStyle CssClass="ReportAltdataRow" />
                <FooterStyle CssClass="ReportFooterRow" />
                <PageHeaderTemplate>
                    <br />
                    <br />
                </PageHeaderTemplate>
                <Columns>
                    <asp:TemplateField HeaderText="Date" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label ID="lblTranDate" runat="server" Text='<%# Eval("Date","{0:dd/MM/yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="65%" HeaderText="Particulars" ItemStyle-VerticalAlign="Top">
                        <ItemTemplate>
                            <asp:Label ID="lblDebtor" runat="server" Text='<%# Eval("Debitor") %>'></asp:Label><br />
                            <br />
                            <asp:Label ID="lblCreditor" runat="server" Text='<%# Eval("Creditor") %>'></asp:Label><br />
                            <br />
                            <asp:Label ID="lblNarration" runat="server" Text='<%# Eval("Narration") %>'></asp:Label><br />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField ItemStyle-VerticalAlign="Top" ItemStyle-Width="15%" DataFormatString="{0:f2}"
                        DataField="Debit" HeaderText="Debit" />
                    <asp:BoundField ItemStyle-Width="15%" DataFormatString="{0:f2}" DataField="Credit"
                        HeaderText="Credit" />
                </Columns>
                <PagerTemplate>
                </PagerTemplate>
                <PageFooterTemplate>
                    <br />
                </PageFooterTemplate>
            </wc:ReportGridView>
            <table width="700px" cellpadding="5">
                <tr>
                    <td colspan="2" width="500px">
                        &nbsp;
                    </td>
                    <td width="100px">
                        <asp:Label ID="lblSumDebit" runat="server" CssClass="lblFont"></asp:Label>
                    </td>
                    <td width="100px">
                        <asp:Label ID="lblSumCredit" runat="server" CssClass="lblFont"></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
        </div>
    </div>
    </form>
</body>
</html>
