<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StockListReport1.aspx.cs"
    Inherits="StockListReport1" %>

<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Stock Ledger Report</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <link href="App_Themes/DefaultTheme/DefaultTheme.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/DefaultTheme/calendar.css" rel="stylesheet" type="text/css" />
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
    <div align="center">
        <div id="div1" runat="server">
        <asp:HiddenField ID="hdStock" runat="server" />
        <table style="border: 1px solid   blue; background-color:White;" width="600px">
            <%--<tr class="mainConHd">
                <td colspan="4">
                    <span>Stock Ledger Report</span>
                </td>
            </tr>--%>
            <%--<tr class="subHeadFont">
                <td colspan="6">
                    <table>
                        <tr>
                            <td>
                                Stock Ledger Report
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>--%>

            <tr>
                <td class="subHeadFont2" colspan="6">
                    Stock Ledger Report
                </td>
            </tr>
            <tr style="height:6px">
            </tr>
            <tr>
                <td colspan="6">
                    <table style="width: 100%">
                        <tr>
                            <td style="width:20%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                                Start Date
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtStartDate"
                                    Display="None" CssClass="lblFont" ErrorMessage="Please Enter Start Date"></asp:RequiredFieldValidator>
                            </td>
                            <td class="ControlTextBox3" width="20%">
                                <asp:TextBox ID="txtStartDate" CssClass="cssTextBox" MaxLength="10"
                                    runat="server" />
                            </td>
                            <td align="left" width="5%">
                                <script type="text/javascript" language="JavaScript">
                                    new tcal({ 'formname': 'form1', 'controlname': 'txtStartDate' });</script>
                            </td>
                            <td style="width:20%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                                End Date
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEndDate"
                                    Display="None" ErrorMessage="Please Enter The End Date" CssClass="lblFont"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtStartDate"
                                    ControlToValidate="txtEndDate" Display="None" ErrorMessage="Start Date Should Be Less Than the End Date"
                                    CssClass="lblFont" Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
                            </td>
                            <td class="ControlTextBox3" width="25%">
                                <asp:TextBox ID="txtEndDate" CssClass="cssTextBox" MaxLength="10" runat="server" />
                    
                            </td>
                            <td align="left" width="5%">
                                <script type="text/javascript" language="JavaScript">
                                    new tcal({ 'formname': 'form1', 'controlname': 'txtEndDate' });</script>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:20%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                                Category *
                                
                            </td>
                            <td class="ControlDrpBorder" style="width:20%;"> 
                                                                                                                                    
                                    <asp:DropDownList ID="cmbCategory" runat="server" AutoPostBack="true" BackColor = "#90c9fc"
                                        Width="100%" OnSelectedIndexChanged="LoadProducts" AppendDataBoundItems="True" style="border: 1px solid #90c9fc" height="26px" CssClass="drpDownListMedium">
                                        <asp:ListItem Selected="True" Value="0" style="background-color: #90c9fc">Select Category</asp:ListItem>
                                    </asp:DropDownList>
                                                                                                                                    
                            </td>
                            <td  style="width: 5%"></td>
                            <td style="width:20%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                                Brand
                            </td>
                            <td class="ControlDrpBorder" style="width: 20%">
                                    <asp:DropDownList ID="cmbBrand" runat="server" Width="100%" AutoPostBack="true" BackColor = "#90c9fc"  style="border: 1px solid #90c9fc" height="26px" CssClass="drpDownListMedium"
                                        OnSelectedIndexChanged="LoadForBrand" AppendDataBoundItems="True">
                                        <asp:ListItem Selected="True" Value="0" style="background-color: #90c9fc">Select Brand</asp:ListItem>
                                    </asp:DropDownList>
                            </td>
                            <td  style="width: 5%"></td>
                        </tr>
                        <tr>
                            
                            <td style="width:20%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                                Product Name
                            </td>
                            <td class="ControlDrpBorder" style="width:20%;">
                                <asp:DropDownList ID="cmbProdName" runat="server" Width="100%" BackColor = "#90c9fc" style="border: 1px solid #90c9fc" height="26px" CssClass="drpDownListMedium"
                                    AutoPostBack="true" OnSelectedIndexChanged="LoadForProductName" AppendDataBoundItems="True">
                                    <asp:ListItem Selected="True" Value="0" style="background-color: #90c9fc">Select Product</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td  style="width: 5%"></td>
                            <td style="width:20%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                                Model
                            </td>
                            <td class="ControlDrpBorder" style="width:25%;">
                                <asp:DropDownList ID="cmbModel" runat="server" OnSelectedIndexChanged="LoadForModel" BackColor = "#90c9fc" style="border: 1px solid #90c9fc" height="26px" CssClass="drpDownListMedium"
                                    AutoPostBack="true" Width="100%" AppendDataBoundItems="True">
                                    <asp:ListItem Selected="True" Value="0" style="background-color: #90c9fc">Select Model</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td  style="width: 5%"></td>
                        </tr>
            
                        <tr>
                            
                            <td style="width:20%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                                Product Code
                            </td>
                            <td class="ControlDrpBorder" style="width: 20%">
                                                                                                                                    
                                    <asp:DropDownList ID="cmbProdAdd" runat="server" AppendDataBoundItems="true" AutoPostBack="true" BackColor = "#90c9fc"
                                        DataTextField="ProductName" DataValueField="ItemCode" OnSelectedIndexChanged="LoadForProduct" style="border: 1px solid #90c9fc" height="26px" CssClass="drpDownListMedium"
                                        ValidationGroup="product" Width="100%">
                                        <asp:ListItem style="background-color: #90c9fc;" Text="Select Product Code" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                                                                                                                    
                            </td>
                            <td  style="width: 5%">
                                
                            </td>
                            <td>
                            <asp:CheckBox ID="chkvalue" runat="server" style="color:Black" Text="With Value" Font-Names="arial" Font-Size="12px" AutoPostBack="true" Font-Bold="True" />
                            </td>
                            <td style="width: 25%">
                                <asp:CheckBox ID="chktrans" runat="server" style="color:Black" Text="With Item transfered" Font-Names="arial" Font-Size="12px" AutoPostBack="true" Font-Bold="True" />
                                
                                
                            </td>
                            <td  style="width: 5%"></td>
                        </tr>
                        <tr style="height:8px">
                                                </tr>
                        <tr>
                            <td colspan="6">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 10%">
                                            <asp:DropDownList ID="drpLedgerName" runat="server" DataTextField="Productcode" DataValueField="Product" Visible="False">
                                                                                                                        </asp:DropDownList>
                                        </td>
                                        
                                        <td style="width: 20%">
                                            <asp:Button ID="btnReport" runat="server" CssClass="NewReport6" EnableTheming="false"
                                                OnClick="btnReport_Click" />
                                        </td>
                                        <td  style="width: 20%">
                                           
                                        </td>
                                        <td  style="width: 20%">
                                            <asp:Button ID="BtnClearFilter" runat="server" OnClick="btnClearFilter_Click" EnableTheming="false" CssClass="ClearFilter66"
                                                        Text="" />
                                        </td>
                                        <td  style="width: 20%">
                                                <asp:Button ID="Button2" runat="server" CssClass="exportexl6" EnableTheming="false"
                                                OnClick="btnExl_Click" />
                                        </td>
                                        <td style="width: 10%">
                                            
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        
                     </table>
                 </td>
            </tr>
        </table>
        </div>
        <div id="divReport" runat="server">
            <table style="width:100%">
                <tr>
                    <td style="width:45%">

                    </td>
                    <td style="width:15%">
                        <input type="button" id="Button1" runat="Server" onclick="javascript:CallPrint('divPrint')"
                                                class="printbutton6" />
                    </td>
                    <td style="width:40%">

                    </td>
                </tr>
            </table>
            <div id="divPrint" style="font-family: 'Trebuchet MS'; font-size: 11px;">
                <table width="100%" border="0" style="font-family: Trebuchet MS; font-size: 14px;">
                    <tr>
                        <td width="140px" align="left">
                            TIN#:
                            <asp:Label ID="lblTNGST" runat="server"></asp:Label>
                        </td>
                        <td align="center" width="320px" style="font-size: 20px;">
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
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td align="center">
                            Stock Ledger For &nbsp; <asp:Label ID="tblproduct1" runat="server"> </asp:Label>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <table cellspacing="5" cellpadding="3" style="font-family: Trebuchet MS; font-size: 12px;
                    width: 100%; text-align: left">
                    <tr>
                        <td colspan="4">
                            <wc:ReportGridView runat="server" BorderWidth="1" ID="gvLedger" SkinID="gridview"
                                AutoGenerateColumns="false" AllowPrintPaging="true" Width="100%"  CssClass="someClass" BorderColor="Gray" BorderStyle="Solid"
                                OnRowDataBound="gvLedger_RowDataBound">
                                <RowStyle CssClass="dataRow" />
                                <SelectedRowStyle CssClass="SelectdataRow" />
                                <AlternatingRowStyle CssClass="altRow" />
                                <EmptyDataRowStyle CssClass="HeadataRow" Font-Bold="true" />
                                <HeaderStyle CssClass="HeadataRow" />
                                <FooterStyle CssClass="dataRow" />
                                <PagerStyle CssClass="footer-row allPad" VerticalAlign="Middle" HorizontalAlign="Left" />
                                <PageHeaderTemplate>
                                    <br />
                                    <br />
                                </PageHeaderTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="Date"  HeaderStyle-BorderColor="Gray">
                                        <ItemTemplate>
                                            <asp:Label Visible="true" Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblDate"
                                                runat="server" Text='<%# Eval("BillDate","{0:dd/MM/yyyy}")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="'Purchase/Sale'" HeaderText="Purchase/Sales"   HeaderStyle-BorderColor="Gray"/>
                                    <asp:BoundField DataField="billno" HeaderText="BillNo"   HeaderStyle-BorderColor="Gray"/>
                                    <asp:BoundField DataField="LedgerName" HeaderText="Customer/Supplier Name"   HeaderStyle-BorderColor="Gray"/>
                                    <asp:TemplateField HeaderText="Qty."  HeaderStyle-BorderColor="Gray">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQty" Text='<%# Eval("Qty") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ClosingStock"  HeaderStyle-BorderColor="Gray">
                                        <ItemTemplate>
                                            <asp:Label ID="lblClosingStock" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerTemplate>
                                </PagerTemplate>
                                <PageFooterTemplate>
                                    <br />
                                </PageFooterTemplate>
                            </wc:ReportGridView>
                        </td>
                    </tr>
                    <tr>
                        <wc:ReportGridView runat="server" BorderWidth="1" ID="gvledgerwithvalue" SkinID="gridview"
                                AutoGenerateColumns="false" AllowPrintPaging="true" Width="100%"  CssClass="someClass" BorderColor="Gray" BorderStyle="Solid"
                                OnRowDataBound="gvledgerwithvalue_RowDataBound">
                                <RowStyle CssClass="dataRow" />
                                <SelectedRowStyle CssClass="SelectdataRow" />
                                <AlternatingRowStyle CssClass="altRow" />
                                <EmptyDataRowStyle CssClass="HeadataRow" Font-Bold="true" />
                                <HeaderStyle CssClass="HeadataRow" />
                                <FooterStyle CssClass="dataRow" />
                                <PagerStyle CssClass="footer-row allPad" VerticalAlign="Middle" HorizontalAlign="Left" />
                                <PageHeaderTemplate>
                                    <br />
                                    <br />
                                </PageHeaderTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="Date"  HeaderStyle-BorderColor="Gray">
                                        <ItemTemplate>
                                            <asp:Label Visible="true" Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblDate"
                                                runat="server" Text='<%# Eval("BillDate","{0:dd/MM/yyyy}")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="'Purchase/Sale'" HeaderText="Purchase/Sales"   HeaderStyle-BorderColor="Gray"/>
                                    <asp:BoundField DataField="billno" HeaderText="BillNo"   HeaderStyle-BorderColor="Gray"/>
                                    <asp:BoundField DataField="LedgerName" HeaderText="Customer/Supplier Name"   HeaderStyle-BorderColor="Gray"/>
                                    <asp:TemplateField HeaderText="Qty."  HeaderStyle-BorderColor="Gray">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQty" Text='<%# Eval("Qty") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="rate" HeaderText="Value"   HeaderStyle-BorderColor="Gray"/>
                                    <asp:TemplateField HeaderText="ClosingStock"  HeaderStyle-BorderColor="Gray">
                                        <ItemTemplate>
                                            <asp:Label ID="lblClosingStock" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerTemplate>
                                </PagerTemplate>
                                <PageFooterTemplate>
                                    <br />
                                </PageFooterTemplate>
                            </wc:ReportGridView>
                    </tr>
                </table>
                <div style="text-align: left">
                    <table cellspacing="1" cellpadding="1" style="font-family: Trebuchet MS; font-size: 11px;
                        width: 450px; text-align: left; border: Solid 1px">
                        <tr>
                            <td style="width: 50%; text-align: right">
                                <b>Item Name</b>
                            </td>
                            <td>
                                :&nbsp;
                            </td>
                            <td style="width: 50%">
                                <asp:Label ID="lblItem" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <b>Model</b>
                            </td>
                            <td>
                                :&nbsp;
                            </td>
                            <td>
                                <asp:Label ID="lblModel" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <b>Opening Stock</b>
                            </td>
                            <td>
                                :&nbsp;
                            </td>
                            <td>
                                <asp:Label ID="lblOpenStock" runat="server" Text="0" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <b>Closing Stock as per below</b>
                            </td>
                            <td>
                                :&nbsp;
                            </td>
                            <td>
                                <asp:Label ID="lblClosingStock" Text="0" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <b>Closing stock as per product master</b>
                            </td>
                            <td>
                                :&nbsp;
                            </td>
                            <td>
                                <asp:Label ID="lblClosingStockPM" Text="0" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
