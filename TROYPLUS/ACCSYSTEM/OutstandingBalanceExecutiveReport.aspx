<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OutstandingBalanceExecutiveReport.aspx.cs"
    Inherits="OutstandingBalanceExecutiveReport" %>

<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>Outstanding Balance Executive Report</title>
    <link href="App_Themes/DefaultTheme/calendar.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/DefaultTheme/DefaultTheme.css" rel="stylesheet" type="text/css" />
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
    <br />
    <br />
    <div>
        <table cellpadding="2" cellspacing="4" width="700px" border="0" style="border: 1px solid silver;
            text-align: left">
            <tr>
                <td colspan="3" class="subHeadFont">
                    Outstanding Balance Executive Report
                </td>
            </tr>
            <tr>
                <td style="width: 25%" class="tblFont">
                    Executive Incharge :
                </td>
                <td class="tblLeft">
                    <asp:DropDownList ID="drpIncharge" AppendDataBoundItems="true" runat="server" CssClass="cssDropDown"
                        Width="200px" DataSourceID="empSrc" DataTextField="empFirstName" DataValueField="empno">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 25%" class="tblFont">
                </td>
                <td align="left">
                    <asp:Button ID="btnReport" CssClass="Button" runat="server" OnClick="btnReport_Click"
                        Text="Generate Report" />&nbsp;<input type="button" value="Print " id="Button1" runat="Server"
                            onclick="javascript:CallPrint('divPrint')" class="pageButton" />&nbsp;
                    <asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="true" ShowSummary="false" />
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div id="divPrint" style="font-family: 'Trebuchet MS'; font-size: 11px;" visible="false"
        runat="server">
        <br />
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
        <br />
        <h5>
            Outstanding Balance Executive Report</h5>
        <wc:ReportGridView runat="server" BorderWidth="1" ID="gvOuts" GridLines="Both" AlternatingRowStyle-CssClass="even"
            AutoGenerateColumns="false" PrintPageSize="40" AllowPrintPaging="true" CssClass="gridview"
            Width="700px" OnRowDataBound="gvOuts_RowDataBound" ShowFooter="true" FooterStyle-CssClass="lblFont">
            <HeaderStyle CssClass="ReportHeadataRow" />
            <RowStyle CssClass="ReportdataRow" />
            <AlternatingRowStyle CssClass="ReportAltdataRow" />
            <FooterStyle CssClass="ReportFooterRow" />
            <PageHeaderTemplate>
            </PageHeaderTemplate>
            <Columns>
                <%-- <asp:TemplateField HeaderText="TransDate">
                <ItemTemplate>
                    <asp:Label style="font-family:'Trebuchet MS'; font-size:11px;  " ID="lblTransDate" runat="server" Text = '<%# Eval("TransDate","{0:dd/MM/yyyy}") %>' />
                </ItemTemplate> 
            </asp:TemplateField> --%>
                <asp:TemplateField HeaderText="Customer">
                    <ItemTemplate>
                        <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblCustomer"
                            runat="server" Text='<%# Eval("Customer") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Outstanding(0-10) Days">
                    <ItemTemplate>
                        <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblOuts10" runat="server"
                            Text='<%# Eval("Out10") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Outstanding(11-30) Days">
                    <ItemTemplate>
                        <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblOuts30" runat="server"
                            Text='<%# Eval("Out30") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Outstanding(31-60) Days">
                    <ItemTemplate>
                        <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblOuts60" runat="server"
                            Text='<%# Eval("Out60") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Outstanding(61-90) Days">
                    <ItemTemplate>
                        <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblOuts90" runat="server"
                            Text='<%# Eval("Out90") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Outstanding  (90-Above) Days">
                    <ItemTemplate>
                        <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblOuts91" runat="server"
                            Text='<%# Eval("Out90above") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Amount" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblAmount" runat="server"
                            Text='<%# Eval("Amount","{0:f2}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerTemplate>
            </PagerTemplate>
            <PageFooterTemplate>
                <br />
            </PageFooterTemplate>
        </wc:ReportGridView>
        <%--<h5>Total Outstading Amount:</h5>--%>
        <asp:Label CssClass="tblLeft" ID="lblTotalAmt" runat="server"></asp:Label>
    </div>
    <asp:ObjectDataSource ID="empSrc" runat="server" SelectMethod="ListExecutive" TypeName="BusinessLogic">
        <SelectParameters>
            <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    </form>
</body>
</html>
