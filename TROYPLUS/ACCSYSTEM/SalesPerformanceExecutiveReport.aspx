<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalesPerformanceExecutiveReport.aspx.cs"
    Inherits="SalesPerformanceExecutiveReport" %>

<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <title>Sales Performance Executive Report</title>
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
    <div  align="center">
        <div id="div1" runat="server" align="center">
            <asp:ScriptManager ID="scrp1" runat="server">
            </asp:ScriptManager>
            <br />
            <table cellpadding="2" cellspacing="2" width="450px" style="border: 1px solid blue;
                " align="center">
                <tr>
                    <%--<td colspan="3" class="HeadataRow" style="font-weight: bold; text-align: center;
                        size: 14px">--%>
                    <td colspan="4" class="headerPopUp">
                        Sales Performance Executive Report
                    </td>
                </tr>
                <tr style="height:6px">
                
                </tr>
                <tr>
                    <td  style="width:40%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                        Start Date
                    </td>
                    <td class="ControlTextBox3" style="width:20%">
                        <asp:TextBox ID="txtStartDate" CssClass="cssTextBox" Width="100px" MaxLength="10"
                            runat="server" />
                        
                    </td>
                    <td align="left" style="width:25%">
                        <script type="text/javascript" language="JavaScript">                            new tcal({ 'formname': 'form1', 'controlname': 'txtStartDate' });</script>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtStartDate"
                            Display="None" CssClass="lblFont" ErrorMessage="Please Enter Start Date"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="width:30%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                        End Date
                    </td>
                    <td  class="ControlTextBox3" style="width:20%">
                        <asp:TextBox ID="txtEndDate" CssClass="cssTextBox" Width="100px" MaxLength="10" runat="server" />
                        
                    </td>
                    <td style="width:25%" align="left">
                        <script type="text/javascript" language="JavaScript">                            new tcal({ 'formname': 'form1', 'controlname': 'txtEndDate' });</script>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEndDate"
                            Display="None" ErrorMessage="Please Enter The End Date" CssClass="lblFont"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtStartDate"
                            ControlToValidate="txtEndDate" Display="None" ErrorMessage="Start Date Should Be Less Than the End Date"
                            CssClass="lblFont" Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td style="width:30%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                        Executive Incharge
                    </td>
                    <td style="width:20%" class="ControlDrpBorder">
                        <asp:DropDownList ID="drpIncharge" AppendDataBoundItems="true" Width="100%" runat="server"  CssClass="drpDownListMedium" BackColor = "#90c9fc"
                            DataSourceID="empSrc" DataTextField="empFirstName" DataValueField="empno" style="border: 1px solid #90c9fc" height="26px">
                        </asp:DropDownList>
                    </td>
                    <td style="width:25%">
                    </td>
                </tr>
                <tr style="height:6px">
                
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <table width="100%">
                            <tr>
                                <td style="width:31%">
                            
                                </td>
                                <td style="width:19%">
                                    <asp:Button ID="Button2" SkinID="skinButtonBig" runat="server" CssClass="NewReport6"
                                        OnClick="btnRep_Click" Text="" />
                                </td>
                                <td style="width:19%">
                                    <asp:Button ID="btnReport" SkinID="skinButtonBig" runat="server" CssClass="exportexl6"
                                        OnClick="btnReport_Click" Text="" />
                                    <asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="true" ShowSummary="false" />
                                </td>
                                <td style="width:31%">
                            
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr style="display: none">
                    <td style="width: 40%">
                    </td>
                    <td style="width:20%">
                        <asp:TextBox ID="txtCommistion" runat="server" CssClass="lblFont" Width="90px" MaxLength="10"
                            Visible="False" />
                    </td>
                    <td style="width:40%">
                    </td>
                </tr>
            </table>
        </div>
        <div id="divPrint" style="font-family: 'Trebuchet MS'; font-size: 11px; width: 90%"
            visible="false" runat="server" align="center">
            <br />
            <table width="600px">
                <tr>
                    <td colspan="4">
                        <table width="100%">
                            <tr>
                                <td style="width:31%">

                                </td>
                                <td style="width:19%">
                                    <input type="button" value=""
                                            id="Button1" runat="Server" onclick="javascript:CallPrint('divPrint')" class="printbutton6" />
                                </td>
                                <td style="width:19%">
                                    <asp:Button ID="btndet" CssClass="GoBack" EnableTheming="false" runat="server"
                                        OnClick="btndet_Click" />
                                </td>
                                <td style="width:31%">

                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            
            <br />
            <h3 align="left">
                Sales - Dealerwise</h3>
            <wc:ReportGridView runat="server" BorderWidth="1" ID="gvSales" AutoGenerateColumns="false"
                AllowPrintPaging="true" Width="100%" OnRowDataBound="gvSales_RowDataBound"
                CssClass="gridview" GridLines="Both" AlternatingRowStyle-CssClass="even">
                <FooterStyle CssClass="ReportFooterRow" />
                <HeaderStyle CssClass="ReportHeadataRow" />
                <RowStyle CssClass="ReportdataRow" />
                <AlternatingRowStyle CssClass="ReportAltdataRow" />
                <PageHeaderTemplate>
                    <br />
                    <br />
                </PageHeaderTemplate>
                <Columns>
                    <asp:TemplateField HeaderText="Customer">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblCustomer"
                                runat="server" Text='<%# Eval("CustomerName") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amount">
                        <ItemTemplate>
                            <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount","{0:f3}") %>' />
                            <%-- krishnavelu 26 June --%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Commission">
                        <ItemTemplate>
                            <asp:Label ID="lblCommission" runat="server" Text='<%# Eval("ExecCharge","{0:f2}") %>' />
                            <%-- krishnavelu 26 June --%>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </wc:ReportGridView>
            <br />
            <h3 align="left">
                &nbsp;</h3>
            <wc:ReportGridView runat="server" BorderWidth="1" ID="gvSalesReturn" CssClass="gridview"
                GridLines="Both" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                AllowPrintPaging="true" Width="100%" Style="font-family: 'Trebuchet MS';
                font-size: 11px;" OnRowDataBound="gvSalesReturn_RowDataBound" Visible="False">
                <FooterStyle CssClass="ReportFooterRow" />
                <HeaderStyle CssClass="ReportHeadataRow" />
                <RowStyle CssClass="ReportdataRow" />
                <AlternatingRowStyle CssClass="ReportAltdataRow" />
                <PageHeaderTemplate>
                    <br />
                    <br />
                </PageHeaderTemplate>
                <Columns>
                    <asp:TemplateField HeaderText="Customer">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblCustomer"
                                runat="server" Text='<%# Eval("LedgerName") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amount">
                        <ItemTemplate>
                            <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount","{0:f2}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <AlternatingRowStyle CssClass="even"></AlternatingRowStyle>
            </wc:ReportGridView>
            <br />
            <h3 align="left">
                Sales - Itemwise</h3>
            <wc:ReportGridView runat="server" BorderWidth="1" ID="gvSalesItemwise" AutoGenerateColumns="false"
                AllowPrintPaging="true" Style="font-family: 'Trebuchet MS';
                font-size: 11px;" Width="100%" GridLines="Both" AlternatingRowStyle-CssClass="even"
                CssClass="gridview" OnRowDataBound="gvSalesItemwise_RowDataBound">
                <FooterStyle CssClass="ReportFooterRow" />
                <HeaderStyle CssClass="ReportHeadataRow" />
                <RowStyle CssClass="ReportdataRow" />
                <AlternatingRowStyle CssClass="ReportAltdataRow" />
                <PageHeaderTemplate>
                    <br />
                    <br />
                </PageHeaderTemplate>
                <Columns>
                    <asp:TemplateField HeaderText="Itemcode">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblitemCode"
                                runat="server" Text='<%# Eval("itemCode") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Product Name">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblProductName"
                                runat="server" Text='<%# Eval("ProductName") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Description">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblProductDesc"
                                runat="server" Text='<%# Eval("ProductDesc") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Qty">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblQty" runat="server"
                                Text='<%# Eval("Qty") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amount">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblAmount" runat="server"
                                Text='<%# Eval("Amount") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Comission">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblCommision"
                                runat="server" Text='<%# Eval("ExecCharge") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerTemplate>
                </PagerTemplate>
                <PageFooterTemplate>
                    <br />
                </PageFooterTemplate>
            </wc:ReportGridView>
            <br />
            <br />
            <h3 align="left">
                &nbsp;</h3>
            <wc:ReportGridView runat="server" BorderWidth="1" ID="gvSalesReturnItemwise" GridLines="Both"
                AlternatingRowStyle-CssClass="even" CssClass="gridview" AutoGenerateColumns="false"
                AllowPrintPaging="true" Width="100%" Style="font-family: 'Trebuchet MS';
                font-size: 11px;" OnRowDataBound="gvSalesReturnItemwise_RowDataBound">
                <FooterStyle CssClass="ReportFooterRow" />
                <HeaderStyle CssClass="ReportHeadataRow" />
                <RowStyle CssClass="ReportdataRow" />
                <AlternatingRowStyle CssClass="ReportAltdataRow" />
                <PageHeaderTemplate>
                    <br />
                    <br />
                </PageHeaderTemplate>
                <Columns>
                    <asp:TemplateField HeaderText="Itemcode">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblitemCode"
                                runat="server" Text='<%# Eval("itemCode") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Product Name">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblProductName"
                                runat="server" Text='<%# Eval("ProductName") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Description">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblProductDesc"
                                runat="server" Text='<%# Eval("ProductDesc") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Qty">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblQty" runat="server"
                                Text='<%# Eval("Qty") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amount">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblAmount" runat="server"
                                Text='<%# Eval("Amount") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Comission">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblCommision"
                                runat="server" Text='0' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerTemplate>
                </PagerTemplate>
                <PageFooterTemplate>
                    <br />
                </PageFooterTemplate>
            </wc:ReportGridView>
            <h5>
                Total Sales:</h5>
            <asp:Label ID="lblSales" runat="server"></asp:Label><br />
            <h5>
                &nbsp;</h5>
            <asp:Label ID="lblSalesReturn" runat="server" Visible="False"></asp:Label><br />
            <h5>
                &nbsp;</h5>
            <asp:Label ID="lblBalance" runat="server" Visible="False"></asp:Label><br />
            <h5>
                Total Sales Commision:</h5>
            <asp:Label ID="lblTotalSalesComm" runat="server"></asp:Label><br />
            <asp:HiddenField ID="hdSalesReturn" runat="server" Value="0" />
            <asp:ObjectDataSource ID="empSrc" runat="server" SelectMethod="ListExecutive" TypeName="BusinessLogic">
                <SelectParameters>
                    <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
    </div>
    </form>
</body>
</html>
