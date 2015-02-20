<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductLevelReport1.aspx.cs"
    Inherits="ProductLevelReport1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <title>Sales Level Report</title>
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
<body>
    <form id="form1" runat="server">
    <br />
    <div align="center">
        <div id="div1" runat="server">
        <%--<table cellpadding="2" cellspacing="4" width="700px" border="0" style="border: 1px solid silver;
            text-align: left">--%>

        <table style="border: 1px solid blue; background-color:White" width="450px">
            <%--<tr>
                <td colspan="3">
                    <table class="headerPopUp">
                        <tr>
                            <td>
                                <span>Sales Level Report</span>
                            </td>
                        </tr>
                    </table>
                </td>
                <%--<td colspan="3" class="mainConHd">
                    <span>Prodcut Level Report</span>
                </td>--%>
            <%--/tr>--%>
            <tr class="subHeadFont2">
                    <td colspan="3" class="subHeadFont2">
                        Sales Level Report
                    </td>                          
                </tr>
                <tr style="height:6px">
            </tr>
            <tr>
                <td style="width:40%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                    Start Date
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtStartDate"
                        Display="None" CssClass="lblFont" ErrorMessage="Please Enter Start Date"></asp:RequiredFieldValidator>
                </td>
                <td class="ControlTextBox3" width="20%">
                    <asp:TextBox ID="txtStartDate" CssClass="cssTextBox" Width="100%" MaxLength="10"
                        runat="server" />
                </td>
                <td align="left" width="40%">
                    <script type="text/javascript" language="JavaScript">
                        new tcal({ 'formname': 'form1', 'controlname': 'txtStartDate' });</script>
                </td>
            </tr>
            <tr>
                <td style="width:40%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                    End Date
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEndDate"
                        Display="None" ErrorMessage="Please Enter The End Date" CssClass="lblFont"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtStartDate"
                        ControlToValidate="txtEndDate" Display="None" ErrorMessage="Start Date Should Be Less Than the End Date"
                        CssClass="lblFont" Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
                </td>
                <td align="left" class="ControlTextBox3" width="20%">
                    <asp:TextBox ID="txtEndDate" CssClass="cssTextBox" Width="100%" MaxLength="10" runat="server" />
                </td>
                <td align="left" width="40%">
                    <script type="text/javascript" language="JavaScript">
                        new tcal({ 'formname': 'form1', 'controlname': 'txtEndDate' });</script>
                </td>
            </tr>
            <tr style="height:6px">
            </tr>
            <tr>
                <td colspan="3">
                    <table width="100%">
                        <tr>
                            <td width="31%">
                            
                            </td>
                            <td width="19%">
                                <asp:Button ID="btnReport" CssClass="NewReport6" EnableTheming="false" runat="server"
                                    OnClick="btnReport_Click" />
                            </td>
                            <td width="19%">
                                <asp:Button ID="Button1" CssClass="exportexl6" EnableTheming="false" runat="server"
                                    OnClick="btnRep_Click" />
                            </td>
                            <td width="31%">
                            
                            </td>
                        </tr>
                    </table>
                </td>
                
            </tr>
        </table>
        </div>
        <div id="divmain" runat="server" visible="false">
            <table width="700px">
                <tr>
                    <td colspan="4">
                        <table width="100%">
                            <tr>
                                <td style="width:40%">

                                </td>
                                <td style="width:20%">
                                    <input type="button" value=" " class="printbutton6" id="Button2" runat="Server" onclick="javascript:CallPrint('divPrint')"
                                        style="padding-left: 25px;" />
                                    <%--<asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="true" ShowSummary="false" />--%>
                                </td>
                                <td style="width:10%">
                                    <asp:Button ID="btndet" CssClass="GoBack" EnableTheming="false" runat="server"
                                        OnClick="btndet_Click" Visible="False" />
                                </td>
                                <td style="width:30%">

                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <br />
        <div id="divPrint" runat="server" visible="false" style="font-family: 'Trebuchet MS';
            font-size: 11px;">
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
            </table>
            <h3>
                Sales Level 1</h3>
            <wc:ReportGridView runat="server" BorderWidth="1" ID="gvSalesLevel1" ShowFooter="false"
                AutoGenerateColumns="false" AllowPrintPaging="true" Width="90%" Style="font-family: 'Trebuchet MS';
                font-size: 11px;" OnRowDataBound="gvSalesLevel1_RowDataBound">
                <HeaderStyle CssClass="ReportHeadataRow" />
                <RowStyle CssClass="ReportdataRow" />
                <AlternatingRowStyle CssClass="ReportAltdataRow" />
                <FooterStyle CssClass="ReportFooterRow" />
                <PageHeaderTemplate>
                    <br />
                    <br />
                </PageHeaderTemplate>
                <Columns>
                    <asp:TemplateField HeaderText="Product" ItemStyle-Width="3%">
                        <ItemTemplate>
                            <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("ProductName") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Qty. Sold" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label ID="lblQtySold" runat="server" Text='<%# Eval("SoldQty") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total Amount(Exc Tax)" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label ID="lblQty" runat="server" Text='<%# Eval("TotalAmount") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Avg Rate" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="lblAvgRate" runat="server" Text='<%# Eval("AvgRate","{0:f2}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Avg Profit" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label ID="lblAvgProfit" runat="server" Text='<%# Eval("AvgProfit","{0:f2}") %>' />
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
            <h3>
                Sales Level 2</h3>
            <wc:ReportGridView runat="server" BorderWidth="1" ID="gvSalesLevel2" ShowFooter="false"
                AutoGenerateColumns="false" AllowPrintPaging="true" Width="90%" Style="font-family: 'Trebuchet MS';
                font-size: 11px;" OnRowDataBound="gvSalesLevel2_RowDataBound">
                <HeaderStyle CssClass="ReportHeadataRow" />
                <RowStyle CssClass="ReportdataRow" />
                <AlternatingRowStyle CssClass="ReportAltdataRow" />
                <FooterStyle CssClass="ReportFooterRow" />
                <PageHeaderTemplate>
                    <br />
                    <br />
                </PageHeaderTemplate>
                <Columns>
                    <asp:TemplateField HeaderText="Product Name" ItemStyle-Width="3%">
                        <ItemTemplate>
                            <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("ProductName") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Brand" ItemStyle-Width="3%">
                        <ItemTemplate>
                            <asp:Label ID="lblBrand" runat="server" Text='<%# Eval("ProductDesc") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Qty. Sold" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label ID="lblQtySold" runat="server" Text='<%# Eval("QtySold") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total Amount(Exclude Tax)" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label ID="lblQty" runat="server" Text='<%# Eval("TotalAmount") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Avg Rate" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="lblAvgRate" runat="server" Text='<%# Eval("AvgRate","{0:f2}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Avg Profit" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label ID="lblAvgProfit" runat="server" Text='<%# Eval("AvgProfit","{0:f2}") %>' />
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
            <h3>
                Sales Level 3</h3>
            <wc:ReportGridView runat="server" BorderWidth="1" ID="gvSalesLevel3" ShowFooter="false"
                AutoGenerateColumns="false" AllowPrintPaging="true" Width="90%" Style="font-family: 'Trebuchet MS';
                font-size: 11px;" OnRowDataBound="gvSalesLevel3_RowDataBound">
                <HeaderStyle CssClass="ReportHeadataRow" />
                <RowStyle CssClass="ReportdataRow" />
                <AlternatingRowStyle CssClass="ReportAltdataRow" />
                <FooterStyle CssClass="ReportFooterRow" />
                <PageHeaderTemplate>
                    <br />
                    <br />
                </PageHeaderTemplate>
                <Columns>
                    <asp:TemplateField HeaderText="Product Name" ItemStyle-Width="3%">
                        <ItemTemplate>
                            <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("ProductName") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Brand" ItemStyle-Width="3%">
                        <ItemTemplate>
                            <asp:Label ID="lblBrand" runat="server" Text='<%# Eval("ProductDesc") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Model" ItemStyle-Width="3%">
                        <ItemTemplate>
                            <asp:Label ID="lblModel" runat="server" Text='<%# Eval("Model") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Qty. Sold" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label ID="lblQtySold" runat="server" Text='<%# Eval("QtySold") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total Amount(Exclude Tax)" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label ID="lblQty" runat="server" Text='<%# Eval("TotalAmount") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Avg Rate" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="lblAvgRate" runat="server" Text='<%# Eval("AvgRate","{0:f2}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Avg Profit" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label ID="lblAvgProfit" runat="server" Text='<%# Eval("AvgProfit","{0:f2}") %>' />
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
            <h3>
                Sales Level 4</h3>
            <wc:ReportGridView runat="server" BorderWidth="1" ID="gvSalesLevel4" ShowFooter="false"
                AutoGenerateColumns="false" AllowPrintPaging="true" Width="90%" Style="font-family: 'Trebuchet MS';
                font-size: 11px;" OnRowDataBound="gvSalesLevel4_RowDataBound">
                <HeaderStyle CssClass="ReportHeadataRow" />
                <RowStyle CssClass="ReportdataRow" />
                <AlternatingRowStyle CssClass="ReportAltdataRow" />
                <FooterStyle CssClass="ReportFooterRow" />
                <PageHeaderTemplate>
                    <br />
                    <br />
                </PageHeaderTemplate>
                <Columns>
                    <asp:TemplateField HeaderText="Product Name" ItemStyle-Width="3%">
                        <ItemTemplate>
                            <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("ProductName") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Brand" ItemStyle-Width="3%">
                        <ItemTemplate>
                            <asp:Label ID="lblBrand" runat="server" Text='<%# Eval("ProductDesc") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Model" ItemStyle-Width="3%">
                        <ItemTemplate>
                            <asp:Label ID="lblModel" runat="server" Text='<%# Eval("Model") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Bill No." ItemStyle-Width="3%">
                        <ItemTemplate>
                            <asp:Label ID="lblbillNo" runat="server" Text='<%# Eval("billno") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Bill Date" ItemStyle-Width="3%">
                        <ItemTemplate>
                            <asp:Label ID="lblbillDate" runat="server" Text='<%# Eval("BillDate") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Qty. Sold" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label ID="lblQtySold" runat="server" Text='<%# Eval("QtySold") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total Amount(Exclude Tax)" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label ID="lblQty" runat="server" Text='<%# Eval("TotalAmount") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Avg Rate" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="lblAvgRate" runat="server" Text='<%# Eval("AvgRate","{0:f2}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Avg Profit" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label ID="lblAvgProfit" runat="server" Text='<%# Eval("AvgProfit","{0:f2}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerTemplate>
                </PagerTemplate>
                <PageFooterTemplate>
                    <br />
                </PageFooterTemplate>
            </wc:ReportGridView>
        </div>
        </div>
    </div>
    </form>
</body>
</html>
