<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalesPerformanceDealerReport1.aspx.cs"
    Inherits="SalesPerformanceDealerReport1" %>

<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <title>Sales Performance - Dealer Report</title>
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
</head>
<body style="font-family: 'Trebuchet MS'; font-size: 11px;">
    <form id="form1" runat="server">
    <div align="center">
        <br />
        <a name="#Top"></a>
        <table width="100%" cellpadding="2" style="font-weight: bold; font-size: 11px;">
            <tr>
                <td colspan="8" align="center">
                </td>
            </tr>
            <tr>
                <td bgcolor="#E9F1F6" align="center">
                    <a href="#SB">Sales - Billwise</a>
                </td>
                <td bgcolor="#E9F1F6" align="center">
                    <a href="#SRB">Sales Return - Billwise</a>
                </td>
                <td bgcolor="#E9F1F6" align="center">
                    <a href="#SA">Sales - ALL items</a>
                </td>
                <td bgcolor="#E9F1F6" align="center">
                    <a href="#SRAI">Sales Return - ALL items</a>
                </td>
                <td bgcolor="#E9F1F6" align="center">
                    <a href="#SI">Sales - Itemwise</a>
                </td>
                <td bgcolor="#E9F1F6" align="center">
                    <a href="#SRI">Sales Return - Itemwise</a>
                </td>
                <td bgcolor="#E9F1F6" align="center">
                    <a href="#SAT">All Transactions</a>
                </td>
                <td bgcolor="#E9F1F6" align="center">
                    <a href="#Sm">Summary</a>
                </td>
            </tr>
        </table>
        <br />
        <div runat="server" id="div1">
        <table cellpadding="2" cellspacing="2" width="60%" border="0" style="border: 1px solid blue; background-color:White;
            text-align: left">
            <tr>
                <td colspan="3" class="subHeadFont2">
                    Sales Performance Dealer Report
                </td>
            </tr>
            <tr>
                <td style="width:30%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px" >
                    Start Date
                </td>
                <td style="width:20%"  class="ControlTextBox3">
                    <asp:TextBox ID="txtStartDate" CssClass="cssTextBox" MaxLength="10"
                        runat="server" />                   
                </td>
                <td style="width:25%">
                    <script type="text/javascript" language="JavaScript">                        new tcal({ 'formname': 'form1', 'controlname': 'txtStartDate' });</script>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtStartDate"
                        Display="None" CssClass="lblFont" ErrorMessage="Please Enter Start Date"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="width:30%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                    End Date
                </td>
                <td style="width:20%" class="ControlTextBox3">
                    <asp:TextBox ID="txtEndDate" CssClass="cssTextBox" MaxLength="10" runat="server" />
                    
                </td>
                <td style="width:25%">
                    <script type="text/javascript" language="JavaScript">                        new tcal({ 'formname': 'form1', 'controlname': 'txtEndDate' });</script>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEndDate"
                        Display="None" ErrorMessage="Please Enter The End Date" CssClass="lblFont"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtStartDate"
                        ControlToValidate="txtEndDate" Display="None" ErrorMessage="Start Date Should Be Less Than the End Date"
                        CssClass="lblFont" Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td style="width:30%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                    Dealer/Customer
                </td>
                <td  style="width:20%" class="ControlDrpBorder">
                    <asp:DropDownList ID="drpLedgerName" runat="server" Width="100%" DataTextField="LedgerName" CssClass="drpDownListMedium" BackColor = "#90c9fc"
                        DataValueField="LedgerID" style="border: 1px solid #90c9fc" height="26px" >
                    </asp:DropDownList>
                </td>
                <td style="width:25%">
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <table width="100%">
                        <tr>
                            <td width="30%">
                            
                            </td>
                            <td style="width:20%" align="right">
                                <asp:Button ID="btnReport"  CssClass="NewReport6" EnableTheming="false" runat="server"
                                    OnClick="btnReport_Click" Text="" />
                            </td>
                            <td width="40%">
                                <input type="button" value="" class="printbutton6"
                                        id="Button1" runat="Server" onclick="javascript:CallPrint('divPrint')" />
                                <asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="true" ShowSummary="false" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        </div>
        <br />
        <div id="divPrint" style="font-family: 'Trebuchet MS'; font-size: 11px;" runat="server">
            <br />
            <h3>
                Sales Performance Dealer/Customer Report</h3>
            <br />
            <br />
            <a name="#Sm"></a>
            <h3>
                Summary</h3>
            <table border="0" style="border: solid 1px black; width: 400px" cellspacing="1" cellpadding="5">
                <tr>
                    <td class="tblLeft">
                        Total Sales
                    </td>
                    <td>
                        Rs.
                        <asp:Label CssClass="tblLeft" ID="lblSales" Text="0.00" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tblLeft">
                        Total Sales Return
                    </td>
                    <td>
                        Rs.
                        <asp:Label CssClass="tblLeft" ID="lblSalesReturn" Text="0.00" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tblLeft">
                        Balance
                    </td>
                    <td>
                        Rs.
                        <asp:Label CssClass="tblLeft" ID="lblBalance" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <a name="#SB"></a>
            <h3 align="left">
                Sales - Billwise</h3>
            <wc:ReportGridView runat="server" BorderWidth="1" ID="gvSalesBill" AutoGenerateColumns="false"
                CssClass="gridview" GridLines="Both" AlternatingRowStyle-CssClass="even"
                AllowPrintPaging="true" Width="100%" OnRowDataBound="gvSalesBill_RowDataBound"
                Style="font-family: 'Trebuchet MS'; font-size: 11px;">
                <HeaderStyle CssClass="ReportHeadataRow" />
                <RowStyle CssClass="ReportdataRow" />
                <AlternatingRowStyle CssClass="ReportAltdataRow" />
                <PageHeaderTemplate>
                    <br />
                    <br />
                </PageHeaderTemplate>
                <Columns>
                    <asp:TemplateField HeaderText="Bill Date">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" Visible="true" ID="lblBillDate"
                                runat="server" Text='<%# Eval("BillDate","{0:dd/MM/yyyy}")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Billno">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblBillno" runat="server"
                                Text='<%# Eval("Billno") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Paymode">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblPaymode"
                                runat="server" Text='<%# Eval("Paymode") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amount">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblAmount" runat="server"
                                Text='<%# Eval("Amount","{0:f2}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </wc:ReportGridView>
            <br />
            <br />
            <b>Total Sales Amount Billwise : </b>Rs.
            <asp:Label ID="lblTotalBill" CssClass="tblLeft" runat="server"></asp:Label>
            <br />
            <br />
            <a href="#Top">Back To Top</a><br />
            <br />
            <a name="#SRB"></a>
            <h3 align="left">
                Sales Return - Billwise</h3>
            <wc:ReportGridView runat="server" BorderWidth="1" ID="gvSalesReturnBill" CssClass="gridview"
                GridLines="Both" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                AllowPrintPaging="true" Width="100%" Style="font-family: 'Trebuchet MS';
                font-size: 11px;" OnRowDataBound="gvSalesReturnBill_RowDataBound">
                <HeaderStyle CssClass="ReportHeadataRow" />
                <RowStyle CssClass="ReportdataRow" />
                <AlternatingRowStyle CssClass="ReportAltdataRow" />
                <PageHeaderTemplate>
                    <br />
                    <br />
                </PageHeaderTemplate>
                <Columns>
                    <asp:TemplateField HeaderText="Bill Date">
                        <ItemTemplate>
                            <asp:Label Visible="true" Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblBillDate"
                                runat="server" Text='<%# Eval("BillDate","{0:dd/MM/yyyy}")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PurchaseID">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblBillno" runat="server"
                                Text='<%# Eval("PurchaseID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Paymode">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblPaymode"
                                runat="server" Text='<%# Eval("Paymode") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Reason">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblSalesReturn"
                                runat="server" Text='<%# Eval("SalesReturnReason") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amount">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblAmount" runat="server"
                                Text='<%# Eval("Amount","{0:f2}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </wc:ReportGridView>
            <br />
            <br />
            <b>Total Sales Return Amount Billwise : </b>Rs.
            <asp:Label ID="lblTotalReturnBill" CssClass="tblLeft" runat="server"></asp:Label>
            <br />
            <br />
            <a href="#Top">Back To Top</a><br />
            <a name="#SA"></a>
            <h3 align="left">
                Sales Transactions</h3>
            <wc:ReportGridView runat="server" BorderWidth="1" ID="gvSales" AutoGenerateColumns="false"
                AllowPrintPaging="true" CssClass="gridview" GridLines="Both"
                AlternatingRowStyle-CssClass="even" Width="100%" OnRowDataBound="gvSales_RowDataBound"
                Style="font-family: 'Trebuchet MS'; font-size: 11px;">
                <HeaderStyle CssClass="ReportHeadataRow" />
                <RowStyle CssClass="ReportdataRow" />
                <AlternatingRowStyle CssClass="ReportAltdataRow" />
                <Columns>
                    <asp:TemplateField HeaderText="Bill Date" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" Visible="true" ID="lblBillDate"
                                runat="server" Text='<%# Eval("BillDate","{0:dd/MM/yyyy}")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Billno" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblBillno" runat="server"
                                Text='<%# Eval("Billno") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Paymode" ItemStyle-Width="3%">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblPaymode"
                                runat="server" Text='<%# Eval("Paymode") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sold Items" ItemStyle-Width="82%" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <br />
                            <a href="javascript:switchViews('div<%# Eval("Billno") %>', 'imgdiv<%# Eval("Billno") %>');"
                                style="text-decoration: none;">
                                <img id="imgdiv<%# Eval("Billno") %>" alt="Show" border="0" src="App_Themes/DefaultTheme/Images/plus.gif" />
                            </a>
                            <%--<a style="text-decoration:none" href='BalanceSheetLevel2.aspx?HeadingName=<%# Eval("HeadingName") %>&HeadingID=<%# Eval("HeadingID") %>'><asp:Label style="font-family:'Trebuchet MS'; font-size:11px;  " ID="lblparticulars" runat="server" Text = '<%# Eval("HeadingName") %>' /></a>--%>
                            View Bill Summary
                            <br />
                            <div id="div<%# Eval("Billno") %>" style="display: none; position: relative; left: 25px;">
                                <wc:ReportGridView runat="server" ID="gvProducts" AutoGenerateColumns="false" PrintPageSize="23"
                                    AllowPrintPaging="true" Width="90%" OnRowDataBound="gvProducts_RowDataBound"
                                    Style="font-family: 'Trebuchet MS'; font-size: 11px;" GridLines="Both" AlternatingRowStyle-CssClass="even">
                                    <HeaderStyle CssClass="ReportHeadataRow" />
                                    <RowStyle CssClass="ReportdataRow" />
                                    <AlternatingRowStyle CssClass="ReportAltdataRow" />
                                    <PageHeaderTemplate>
                                        <br />
                                        <br />
                                    </PageHeaderTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Item Code" ItemStyle-Width="3%">
                                            <ItemTemplate>
                                                <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblItemCode"
                                                    runat="server" Text='<%# Eval("ItemCode") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Product Name" ItemStyle-Width="57%">
                                            <ItemTemplate>
                                                <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblProductName"
                                                    runat="server" Text='<%# Eval("ProductName") %>' /><br />
                                                <b>Model :</b>
                                                <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblModel" runat="server"
                                                    Text='<%# Eval("Model") %>' /><br />
                                                <b>Description :</b><asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;"
                                                    ID="lblDes" runat="server" Text='<%# Eval("ProductDesc") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qty" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblQty" runat="server"
                                                    Text='<%# Eval("Qty") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblRate" runat="server"
                                                    Text='<%# Eval("Rate","{0:f2}") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Discount" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblDisc" runat="server"
                                                    Text='<%# Eval("Discount","{0:f2}") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="VAT" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblVat" runat="server"
                                                    Text='<%# Eval("VAT","{0:f2}") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CST" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCst" runat="server" Text='<%# Eval("CST","{0:f2}") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Value" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblValue" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </wc:ReportGridView>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </wc:ReportGridView>
            <br />
            <br />
            <b>Total Sales Transaction : </b>Rs.
            <asp:Label ID="lblTotalSalesTran" CssClass="tblLeft" runat="server"></asp:Label>
            <br />
            <br />
            <a href="#Top">Back To Top</a><br />
            <a name="#SRAI"></a>
            <h3 align="left">
                Sales Return Transactions</h3>
            <wc:ReportGridView runat="server" BorderWidth="1" ID="gvSalesReturn" CssClass="gridview"
                GridLines="Both" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                AllowPrintPaging="true" Width="100%" OnRowDataBound="gvSalesReturn_RowDataBound"
                Style="font-family: 'Trebuchet MS'; font-size: 11px;">
                <HeaderStyle CssClass="ReportHeadataRow" />
                <RowStyle CssClass="ReportdataRow" />
                <AlternatingRowStyle CssClass="ReportAltdataRow" />
                <PageHeaderTemplate>
                    <br />
                    <br />
                </PageHeaderTemplate>
                <Columns>
                    <asp:TemplateField HeaderText="Bill Date" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" Visible="true" ID="lblBillDate"
                                runat="server" Text='<%# Eval("BillDate","{0:dd/MM/yyyy}")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Billno" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblBillno" runat="server"
                                Text='<%# Eval("Billno") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Paymode" ItemStyle-Width="3%">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblPaymode"
                                runat="server" Text='<%# Eval("Paymode") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sales Return Items" ItemStyle-Width="82%">
                        <ItemTemplate>
                            <br />
                            <wc:ReportGridView runat="server" ID="gvProductsPurchase" AutoGenerateColumns="false"
                                PrintPageSize="23" AllowPrintPaging="true" GridLines="Both" AlternatingRowStyle-CssClass="even"
                                Width="100%" OnRowDataBound="gvProductsPurchase_RowDataBound" Style="font-family: 'Trebuchet MS';
                                font-size: 11px;">
                                <PageHeaderTemplate>
                                    <br />
                                    <br />
                                </PageHeaderTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="Item Code" ItemStyle-Width="3%">
                                        <ItemTemplate>
                                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblItemCode"
                                                runat="server" Text='<%# Eval("ItemCode") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Product Name" ItemStyle-Width="57%">
                                        <ItemTemplate>
                                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblProductName"
                                                runat="server" Text='<%# Eval("ProductName") %>' /><br />
                                            <b>Model :</b>
                                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblModel" runat="server"
                                                Text='<%# Eval("Model") %>' /><br />
                                            <b>Description :</b><asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;"
                                                ID="lblDes" runat="server" Text='<%# Eval("ProductDesc") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qty" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblQty" runat="server"
                                                Text='<%# Eval("Qty") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblRate" runat="server"
                                                Text='<%# Eval("Rate","{0:f2}") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Discount" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblDisc" runat="server"
                                                Text='<%# Eval("Discount","{0:f2}") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="VAT" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblVat" runat="server"
                                                Text='<%# Eval("VAT","{0:f2}") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CST" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCst" runat="server" Text='<%# Eval("CST","{0:f2}") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Value" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblValue" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerTemplate>
                                </PagerTemplate>
                                <PageFooterTemplate>
                                    <br />
                                </PageFooterTemplate>
                            </wc:ReportGridView>
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
            <b>Total Sales Return Transaction : </b>Rs.
            <asp:Label ID="lblTotalSalesReturnTran" CssClass="tblLeft" runat="server"></asp:Label>
            <br />
            <br />
            <a href="#Top">Back To Top</a><br />
            <a name="#SI"></a>
            <h3 align="left">
                Sales - Itemwise</h3>
            <wc:ReportGridView runat="server" BorderWidth="1" ID="gvSalesItemwise" AutoGenerateColumns="false"
                AllowPrintPaging="true" Style="font-family: 'Trebuchet MS';
                font-size: 11px;" Width="100%" GridLines="Both" AlternatingRowStyle-CssClass="even"
                CssClass="gridview" OnRowDataBound="gvSalesItemwise_RowDataBound">
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
            <br />
            <br />
            <b>Total Sales Amount Itemwise : </b>Rs.
            <asp:Label ID="lblSalesItemwise" CssClass="tblLeft" runat="server"></asp:Label>
            <br />
            <br />
            <a href="#Top">Back To Top</a><br />
            <a name="#SRI"></a>
            <h3 align="left">
                Sales Return- Itemwise</h3>
            <wc:ReportGridView runat="server" BorderWidth="1" ID="gvSalesReturnItemwise" GridLines="Both"
                AlternatingRowStyle-CssClass="even" CssClass="gridview" AutoGenerateColumns="false"
                AllowPrintPaging="true" Width="100%" OnRowDataBound="gvSalesReturnItemwise_RowDataBound"
                Style="font-family: 'Trebuchet MS'; font-size: 11px;">
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
            <br />
            <br />
            <b>Total Sales Return Amount Itemwise : </b>Rs.
            <asp:Label ID="lblSalesReturnItemwise" CssClass="tblLeft" runat="server"></asp:Label>
            <br />
            <br />
            <a href="#Top">Back To Top</a><br />
            <a name="#SAT"></a>
            <h3 align="left">
                All Transactions</h3>
            <wc:ReportGridView runat="server" BorderWidth="1" ID="gvTran" GridLines="Both" AlternatingRowStyle-CssClass="even"
                CssClass="gridview" AutoGenerateColumns="false" AllowPrintPaging="true"
                Width="100%" OnRowDataBound="gvTran_RowDataBound" Style="font-family: 'Trebuchet MS';
                font-size: 11px;">
                <HeaderStyle CssClass="ReportHeadataRow" />
                <RowStyle CssClass="ReportdataRow" />
                <AlternatingRowStyle CssClass="ReportAltdataRow" />
                <PageHeaderTemplate>
                    <br />
                    <br />
                </PageHeaderTemplate>
                <Columns>
                    <asp:TemplateField HeaderText="Transno">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblTransno"
                                runat="server" Text='<%# Eval("TransNo") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="TransDate">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblTransDate"
                                runat="server" Text='<%# Eval("TransDate","{0:dd/MM/yyyy}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Debtor">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblDebtor" runat="server"
                                Text='<%# Eval("Debitor") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Creditor">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblCreditor"
                                runat="server" Text='<%# Eval("Creditor") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Voucher Type">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblVoucherType"
                                runat="server" Text='<%# Eval("VoucherType") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amount">
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
            <br />
            <br />
            <b>Total All Transaction : </b>Rs.
            <asp:Label ID="lblAllTran" CssClass="tblLeft" runat="server"></asp:Label>
            <br />
            <br />
            <a href="#Top">Back To Top</a><br />
        </div>
        </div>
    </form>
</body>
</html>
