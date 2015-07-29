<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StockListReport.aspx.cs"
    Inherits="StockListReport" %>

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
            <br />
            <tr>
                <td class="headerPopUp" colspan="6">
                    Stock Ledger Report
                </td>
            </tr>
            <tr style="height:6px">
            </tr>
            <tr>
                <td colspan="6">
                    <table style="width: 100%">
                        <tr>
                            <td style="width:20%;" class="ControlLabel">
                                Start Date
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtStartDate"
                                    Display="None" CssClass="lblFont" ErrorMessage="Please Enter Start Date"></asp:RequiredFieldValidator>
                            </td>
                            <td class="ControlTextBox3" style="width:25%;">
                                <asp:TextBox ID="txtStartDate" CssClass="cssTextBox" MaxLength="10"
                                    runat="server" />
                            </td>
                            <td align="left" style="width:5%;" class="ControlLabel">
                                <script type="text/javascript" language="JavaScript">
                                    new tcal({ 'formname': 'form1', 'controlname': 'txtStartDate' });</script>
                            </td>
                            <td style="width:15%;" class="ControlLabel">
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
                            <td align="left" style="width:10%;">
                                <script type="text/javascript" language="JavaScript">
                                    new tcal({ 'formname': 'form1', 'controlname': 'txtEndDate' });</script>
                            </td>
                        </tr>
                        <tr style="height: 2px;"/> 
                        <tr>
                            <td style="width:20%;" class="ControlLabel">
                                Branch *
                                
                            </td>
                            <td class="ControlDrpBorder" style="width:20%;"> 
                                                          <asp:DropDownList ID="drpBranchAdd" TabIndex="10" Width="100%" DataTextField="BranchName" DataValueField="Branchcode" CssClass="drpDownListMedium" AppendDataBoundItems="true" BackColor="#e7e7e7" Style="border: 1px solid #e7e7e7" Height="26px"
                                                                                                        runat="server">
                                                                                                    </asp:DropDownList>                                                
                            </td>
                            <td  style="width: 5%"></td>
                            <td style="width:15%;" class="ControlLabel">
                                Type
                            </td>
                            <td class="ControlDrpBorder" style="width: 20%">
                                   <asp:DropDownList ID="drpsaletype" TabIndex="10" Width="100%" CssClass="drpDownListMedium" AppendDataBoundItems="true" BackColor="#e7e7e7" Style="border: 1px solid #e7e7e7" Height="26px"
                                                                                                        runat="server">
                                       <asp:ListItem Selected="True" Value="0">All</asp:ListItem>
                                       <asp:ListItem Value="1">Delivery Note</asp:ListItem>
                                       <asp:ListItem Value="2">Internal Transfer</asp:ListItem>
                                       <asp:ListItem Value="3">Purchase Return</asp:ListItem>
                                       <asp:ListItem Value="4">Sales Return</asp:ListItem>
                                                                                                    </asp:DropDownList>    
                            </td>
                            <td  style="width: 10%"></td>
                        </tr>
                        <tr style="height: 2px;"/> 
                        <tr>
                            <td style="width:20%;" class="ControlLabel">
                                Category *
                                
                            </td>
                            <td class="ControlDrpBorder" style="width:25%;"> 
                                                                                                                                    
                                    <asp:DropDownList ID="cmbCategory"  CssClass="chzn-select" runat="server" AutoPostBack="true" 
                                        Width="100%" OnSelectedIndexChanged="LoadProducts" AppendDataBoundItems="True" height="26px">
                                        <asp:ListItem Selected="True" Value="0">Select Category</asp:ListItem>
                                    </asp:DropDownList>
                                                                                                                                    
                            </td>
                            <td  style="width: 5%"></td>
                            <td style="width:15%;" class="ControlLabel">
                                Brand
                            </td>
                            <td class="ControlDrpBorder" style="width: 20%">
                                    <asp:DropDownList ID="cmbBrand" runat="server" Width="100%" AutoPostBack="true" height="26px"  CssClass="chzn-select"
                                        OnSelectedIndexChanged="LoadForBrand" AppendDataBoundItems="True">
                                        <asp:ListItem Selected="True" Value="0" >Select Brand</asp:ListItem>
                                    </asp:DropDownList>
                            </td>
                            <td  style="width: 5%"></td>
                        </tr>
                        <tr style="height: 2px;"/> 
                        <tr>
                            
                            <td style="width:20%;" class="ControlLabel">
                                Product Name
                            </td>
                            <td class="ControlDrpBorder" style="width:25%;">
                                <asp:DropDownList ID="cmbProdName" runat="server" Width="100%"  CssClass="chzn-select" height="26px"
                                    AutoPostBack="true" OnSelectedIndexChanged="LoadForProductName" AppendDataBoundItems="True">
                                    <asp:ListItem Selected="True" Value="0" >Select Product</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td  style="width: 5%"></td>
                            <td style="width:15%;" class="ControlLabel">
                                Model
                            </td>
                            <td class="ControlDrpBorder" style="width:25%;">
                                <asp:DropDownList ID="cmbModel" runat="server" OnSelectedIndexChanged="LoadForModel"  CssClass="chzn-select" height="26px" 
                                    AutoPostBack="true" Width="100%" AppendDataBoundItems="True">
                                    <asp:ListItem Selected="True" Value="0" >Select Model</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td  style="width: 5%"></td>
                        </tr>
            <tr style="height: 2px;"/> 
                        <tr>
                            
                            <td style="width:20%;"  class="ControlLabel">
                                Product Code
                            </td>
                            <td class="ControlDrpBorder" style="width: 25%">
                                                                                                                                    
                                    <asp:DropDownList ID="cmbProdAdd" runat="server" AppendDataBoundItems="true" AutoPostBack="true"  CssClass="chzn-select"
                                        DataTextField="ProductName" DataValueField="ItemCode" OnSelectedIndexChanged="LoadForProduct" height="26px"
                                        ValidationGroup="product" Width="100%">
                                        <asp:ListItem Text="Select Product Code" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                                                                                                                    
                            </td>
                            <td  style="width: 5%">product
                                
                            </td>
                            <td>
                             <asp:DropDownList ID="drpPrd" Width="230px" runat="server" AppendDataBoundItems="true" BackColor="White" ForeColor="#0567AE" Font-Bold="false" AutoPostBack="true" CssClass="chzn-select" DataTextField="ProductName1" DataValueField="Retrieve" OnSelectedIndexChanged="drpPrd_SelectedIndexChanged" onchange="GetSelectedTextValue(this)">
                                                                                                                                                                            </asp:DropDownList>
                            </td>
                            <td style="width: 25%">
                                <asp:CheckBox ID="chkvalue" runat="server" style="color:Black" Text="With Value" Font-Names="arial" Font-Size="12px" AutoPostBack="true" Font-Bold="True" />
                                <asp:CheckBox ID="chktrans" Visible="false" runat="server" style="color:Black" Text="With Item transfered" Font-Names="arial" Font-Size="12px" AutoPostBack="true" Font-Bold="True" />
                                
                                
                            </td>
                            <td  style="width: 5%"></td>
                        </tr>
                        <tr style="height:8px">
                                                </tr>
                        <tr>
                            <td colspan="6">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 20%">
                                            <asp:DropDownList ID="drpLedgerName" runat="server" DataTextField="Productcode" DataValueField="Product" Visible="False">
                                                                                                                        </asp:DropDownList>
                                        </td>
                                        
                                        <td style="width: 18%">
                                            <asp:Button ID="btnReport" runat="server" CssClass="NewReport6" EnableTheming="false"
                                                OnClick="btnReport_Click" />
                                        </td>
                                        
                                        <td  style="width: 18%">
                                            <asp:Button ID="BtnClearFilter" runat="server" Visible="false" OnClick="btnClearFilter_Click" EnableTheming="false" CssClass="ClearFilter66"
                                                        Text="" />
                                        </td>
                                        <td  style="width: 18%">
                                                <asp:Button ID="Button2" runat="server" CssClass="exportexl6" EnableTheming="false"
                                                OnClick="btnExl_Click" />
                                        </td>
                                        <td  style="width: 1%">
                                           <input type="button" id="Button1" runat="Server" onclick="javascript:CallPrint('divPrint')"
                                                class="printbutton6" visible="False" />
                                        </td>
                                        <td style="width: 20%">
                                            
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        
                     </table>
                 </td>
            </tr>
        </table>
        <div id="divReport" runat="server">
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
                                    <asp:BoundField DataField="PurchaseSale" HeaderText="Purchase/Sales"   HeaderStyle-BorderColor="Gray"/>
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
                                    <asp:BoundField DataField="PurchaseSale" HeaderText="Purchase/Sales"   HeaderStyle-BorderColor="Gray"/>
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
