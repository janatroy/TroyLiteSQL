<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AccountSummary.aspx.cs" Inherits="AccountSummary" %>

<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <title>Account Summary</title>
    <link rel="Stylesheet" href="App_Themes/DefaultTheme/DefaultTheme.css" />
    <link rel="Stylesheet" href="App_Themes/DefaultTheme/calendar.css" />
    <style type="text/css">
        h5
        {
            font-family: Verdana;
        }
    </style>
    <script type="text/javascript" language="JavaScript">
        function CallPrint(strid) {
            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('', '', 'letf=0,top=0,width=600,,toolbar=0,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();

        }
        function unl() {

            document.form1.submit();
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
    <%--<script language="javascript" type="text/javascript" src="datetimepicker.js"></script>--%>
    <script type="text/javascript" language="JavaScript" src="Scripts/calendar_eu.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <%-- <a href="Default.aspx">Back To Main Report Menu</a> <br /><br />--%>
    </div>
    <a name="#Top"></a>
    <div>
        <br />
        <div id="dvHead1" runat="server" visible="false">
            <table cellpadding="2" cellspacing="4" width="450px" border="0" style="border: 1px solid blue;
                background-image: url('App_Themes/DefaultTheme/Images/bluebg.jpg');">
                <tr>
                    <td colspan="14" align="center" class="headerPopUp">
                       
                            Business Transaction Report
                    </td>
                </tr>
                <tr>
                    <td colspan="3" bgcolor="Gray" class="headColor" align="center">
                        Purchase
                    </td>
                    <td colspan="3" bgcolor="Gray" class="headColor" align="center">
                        Sales
                    </td>
                    <td colspan="2" bgcolor="Gray" class="headColor" align="center">
                        Payment
                    </td>
                    <td colspan="2" bgcolor="Gray" class="headColor" align="center">
                        Receipt
                    </td>
                    <td colspan="2" bgcolor="Gray" class="headColor" align="center">
                        Products
                    </td>
                    <td colspan="3" bgcolor="Gray" class="headColor" align="center">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#E9F1F6" align="center">
                        <a href="#CaPD">Cash</a>
                    </td>
                    <td bgcolor="#E9F1F6" align="center">
                        <a href="#ChPD">Cheque</a>
                    </td>
                    <td bgcolor="#E9F1F6" align="center">
                        <a href="#CrPD">Credit</a>
                    </td>
                    <td bgcolor="#E9F1F6" align="center">
                        <a href="#CaSD">Cash</a>
                    </td>
                    <td bgcolor="#E9F1F6" align="center">
                        <a href="#ChSD">Cheque</a>
                    </td>
                    <td bgcolor="#E9F1F6" align="center">
                        <a href="#CrSD">Credit</a>
                    </td>
                    <td bgcolor="#E9F1F6" align="center">
                        <a href="#CaPaid">Cash</a>
                    </td>
                    <td bgcolor="#E9F1F6" align="center">
                        <a href="#ChPaid">Cheque</a>
                    </td>
                    <td bgcolor="#E9F1F6" align="center">
                        <a href="#CaRec">Cash</a>
                    </td>
                    <td bgcolor="#E9F1F6" align="center">
                        <a href="#ChRec">Cheque</a>
                    </td>
                    <td bgcolor="#E9F1F6" align="center">
                        <a href="#itemS">Itemwise Sales</a>
                    </td>
                    <td bgcolor="#E9F1F6" align="center">
                        <a href="#itemP">Itemwise Purchase</a>
                    </td>
                    <td bgcolor="#E9F1F6" align="center">
                        <a href="#grossP">Gross profit</a>
                    </td>
                    <td bgcolor="#E9F1F6" align="center">
                        <a href="#summary">Account Summary</a>
                    </td>
                    <td bgcolor="#E9F1F6" align="center">
                        <a href="#vatreport">VAT Report</a>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <br />
    <div align="center">
    <table cellpadding="2" cellspacing="2" width="600px" border="0" style="border: 1px solid blue; background-color:White;">
        <tr>
            <td colspan="3" class="headerPopUp">
                
                    Business Transaction Report
                
            </td>
        </tr>
        <tr>
            <td style="width:30%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                Start Date
            </td>
            <td class="ControlTextBox3" width="20%">
                <asp:TextBox ID="txtStartDate" CssClass="cssTextBox"  BackColor = "#90C9FC" Width="100px" MaxLength="10"
                    runat="server" />
                
            </td>
            <td align="left" width="30%">
                <script type="text/javascript" language="JavaScript">                    new tcal({ 'formname': 'form1', 'controlname': 'txtStartDate' });</script>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtStartDate"
                    Display="None" CssClass="lblFont" ErrorMessage="Please Enter Start Date"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width:30%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                End Date
            </td>
            <td class="ControlTextBox3"  width="20%">
                <asp:TextBox ID="txtEndDate" CssClass="cssTextBox" Width="100px"  BackColor = "#90C9FC" MaxLength="10" runat="server" />
                
            </td>
            <td align="left"  width="30%">
                <script type="text/javascript" language="JavaScript">                    new tcal({ 'formname': 'form1', 'controlname': 'txtEndDate' });</script>
                Send SMS
                <asp:CheckBox ID="chkSMS" runat="server" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEndDate"
                    Display="None" ErrorMessage="Please Enter The End Date" CssClass="lblFont"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtStartDate"
                    ControlToValidate="txtEndDate" Display="None" ErrorMessage="Start Date Should Be Less Than the End Date"
                    CssClass="lblFont" Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="30%">
                
            </td>
            <td class="ControlTextBox3" width="20%">
                <asp:CheckBox ID="chkSales" runat="server" />
                Sales Return
                <br />
                <asp:CheckBox ID="chkPurchase" runat="server" />
                Purchase Return
                
            </td>
            <td  width="30%">
            
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <table width="100%">
                    <tr>
                        <td width="32%">
                        
                        </td>
                        <td width="18%">
                            <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click"
                                Text="" CssClass="NewReport6" EnableTheming="false" />
                        </td>
                        <td width="18%">
                            <input type="button" value="" id="Button1"
                                    runat="Server" onclick="javascript:CallPrint('divPrint')" class="printbutton6" />
                        </td>
                        <td width="32%">
                        
                        </td>
                    </tr>
                </table>
            </td>
            
            
        </tr>
    </table>
    </div>
    <asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="true" ShowSummary="false" />
    <br />
    <br />
    <asp:HiddenField ID="hdFilename" runat="server" />
    <asp:HiddenField ID="hdToDelete" Value="" runat="server" />
    <div id="divPrint" runat="server" style="font-family: Verdana; font-size: 11px;">
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
        <div id="dvSUmmary" runat="server" visible="false">
            <h3>
                Summary</h3>
        </div>
        <a name="#CaPD"></a>
        <%--(Start)1. Cash purchase Details --%>
        <br />
        <%--Start Summary--%>
        <table width="80%" cellpadding="3" cellspacing="3" border="0" style="border: 1px solid silver">
            <tr>
                <td colspan="2" class="mainConHd">
                    <span>Detailed View</span>
                </td>
            </tr>
            <tr>
                <td align="left" width="85%">
                    <a href="javascript:switchViews('dvPD', 'imdivPD');" style="text-decoration: none;">
                        <img id="imdivPD" alt="Show" border="0" src="App_Themes/DefaultTheme/Images/plus.gif" />
                    </a><b>Cash Purchase Details</b>
                    <div id="dvPD" style="display: none; position: relative; left: 25px;">
                        <asp:GridView DataKeyNames="PurchaseiD" SkinID="gridview" CssClass="gridview" Width="100%"
                            EmptyDataText="No Cash Purchase Found Matching" ID="gvCashPurchase" GridLines="Both"
                            AlternatingRowStyle-CssClass="even" AutoGenerateColumns="False" runat="server"
                            OnRowDataBound="gvCashPurchase_RowDataBound" ForeColor="#333333">
                            <Columns>
                                <asp:TemplateField HeaderText="Bill Date" ItemStyle-Width="8%">
                                    <ItemTemplate>
                                        <asp:Label Visible="true" ID="lblBillDate" runat="server" Text='<%# Eval("BillDate","{0:dd/MM/yyyy}")%>' />
                                        <asp:Label Visible="false" ID="lblPurchaseID" runat="server" Text='<%# Eval("PurchaseID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Billno" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBillno" runat="server" Text='<%# Eval("Billno") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ledger Name" ItemStyle-Width="17%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLedger" runat="server" Text='<%# Eval("LedgerName") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Purchase Total Value" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("TotalAmt","{0:f2}") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sales Return" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSalesReturn" runat="server" Text='<%# Eval("salesreturn") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reason" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSalesReturnReason" runat="server" Text='<%# Eval("salesreturnreason") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Purchased Items" ItemStyle-Width="60%">
                                    <ItemTemplate>
                                        <br />
                                        <a href="javascript:switchViews('dv<%# Eval("PurchaseID") %>', 'imdiv<%# Eval("PurchaseID") %>');"
                                            style="text-decoration: none;">
                                            <img id="imdiv<%# Eval("PurchaseID") %>" alt="Show" border="0" src="App_Themes/DefaultTheme/Images/plus.gif" />
                                        </a>
                                        <div id="dv<%# Eval("PurchaseID") %>" style="display: none; position: relative; left: 25px;">
                                            <wc:ReportGridView runat="server" BorderWidth="1" ID="gvProducts" ShowFooter="true"
                                                AutoGenerateColumns="false"  AllowPrintPaging="true" Width="100%"
                                                Style="font-family: Verdana; font-size: 11px;" OnRowDataBound="gvProducts_RowDataBound">
                                                <PageHeaderTemplate>
                                                    <br />
                                                    <br />
                                                </PageHeaderTemplate>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Item Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("ItemCode") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Product Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("ProductName") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Qty.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQty" runat="server" Text='<%# Eval("Qty") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRate" runat="server" Text='<%# Eval("PurchaseRate","{0:f2}") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Discount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDisc" runat="server" Text='<%# Eval("Discount","{0:f2}") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="VAT">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVat" runat="server" Text='<%# Eval("VAT","{0:f2}") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Value">
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
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <br />
                        <b>Total Cash Purchase: </b>
                        <asp:Label Font-Bold="true" ID="lblGrandCashPurchase" runat="server"></asp:Label><br />
                        <%--(End)1. Cash purchase Details--%>
                        <a name="#ChPD"></a>
                    </div>
                    <br />
                    <a href="#Top"></a>
                    <br />
                </td>
                <td valign="top">
                    <asp:Label ID="lblsumCashPurchase" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <hr />
                </td>
            </tr>
            <tr>
                <td align="left">
                    <%--(Start)2.Cheque Purchase Details--%>
                    <a href="javascript:switchViews('dvCPD', 'imdivCPD');" style="text-decoration: none;">
                        <img id="imdivCPD" alt="Show" border="0" src="App_Themes/DefaultTheme/Images/plus.gif" />
                    </a><b>Cheque Purchase Details</b>
                    <div id="dvCPD" style="display: none; position: relative; left: 25px;">
                        <asp:GridView DataKeyNames="PurchaseiD" Style="font-family: Verdana; font-size: 11px;"
                            EmptyDataText="No Cheque Purchase Found Matching" Width="100%" ID="gvChequePurchase"
                            GridLines="Both" SkinID="gridview" CssClass="gridview" AlternatingRowStyle-CssClass="even"
                            AutoGenerateColumns="False" runat="server" OnRowDataBound="gvChequePurchase_RowDataBound"
                            ForeColor="#333333">
                            <Columns>
                                <asp:TemplateField HeaderText="Bill Date" ItemStyle-Width="8%">
                                    <ItemTemplate>
                                        <asp:Label Visible="true" ID="lblBillDate" runat="server" Text='<%# Eval("BillDate","{0:dd/MM/yyyy}")%>' />
                                        <asp:Label Visible="false" ID="lblPurchaseID" runat="server" Text='<%# Eval("PurchaseID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Billno" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label Style="font-family: Verdana; font-size: 11px;" ID="lblBillno" runat="server"
                                            Text='<%# Eval("Billno") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ledger Name" ItemStyle-Width="17%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLedger" runat="server" Text='<%# Eval("LedgerName") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Purchase Total Value" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("TotalAmt","{0:f2}") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sales Return" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSalesReturn" runat="server" Text='<%# Eval("salesreturn") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reason" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSalesReturnReason" runat="server" Text='<%# Eval("salesreturnreason") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Purchased Items" ItemStyle-Width="40%">
                                    <ItemTemplate>
                                        <br />
                                        <a href="javascript:switchViews('dvp<%# Eval("Billno") %>', 'imdivp<%# Eval("Billno") %>');"
                                            style="text-decoration: none;">
                                            <img id="imdivp<%# Eval("Billno") %>" alt="Show" border="0" src="App_Themes/DefaultTheme/Images/plus.gif" />
                                        </a>
                                        <div id="dvp<%# Eval("Billno") %>" style="display: none; position: relative; left: 25px;">
                                            <wc:ReportGridView runat="server" BorderWidth="1" ID="gvProducts" ShowFooter="true"
                                                AutoGenerateColumns="false" AllowPrintPaging="true" Width="600px"
                                                Style="font-family: Verdana; font-size: 11px;" OnRowDataBound="gvProducts_RowDataBound">
                                                <PageHeaderTemplate>
                                                    <br />
                                                    <br />
                                                </PageHeaderTemplate>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Item Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("ItemCode") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Product Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("ProductName") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Qty.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQty" runat="server" Text='<%# Eval("Qty") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRate" runat="server" Text='<%# Eval("PurchaseRate","{0:f2}") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Discount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDisc" runat="server" Text='<%# Eval("Discount","{0:f2}") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="VAT">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVat" runat="server" Text='<%# Eval("VAT","{0:f2}") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Value">
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
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <br />
                        <b>Total Cheque Purchase: </b>
                        <asp:Label Font-Bold="true" ID="lblGrandCheqPurchase" runat="server"></asp:Label><br />
                        <%--(End)2.Cheque Purchase Details--%>
                        <a name="#CrPD"></a>
                    </div>
                    <br />
                    <a href="#Top"></a>
                    <br />
                </td>
                <td align="right" valign="top">
                    <asp:Label ID="lblsumChequePurchase" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <hr />
                </td>
            </tr>
            <tr>
                <td align="left">
                    <%--(Start)3.Credit Purchase Details--%>
                    <a href="javascript:switchViews('dvCRPD', 'imdivCRPD');" style="text-decoration: none;">
                        <img id="imdivCRPD" alt="Show" border="0" src="App_Themes/DefaultTheme/Images/plus.gif" />
                    </a><b>Credit Purchase Details</b>
                    <div id="dvCRPD" style="display: none; position: relative; left: 25px;">
                        <asp:GridView DataKeyNames="PurchaseiD" Style="font-family: Verdana; font-size: 11px;"
                            EmptyDataText="No Credit Purchase Found Matching" Width="100%" ID="gvCreditPurchase"
                            GridLines="Both" SkinID="gridview" CssClass="gridview" AlternatingRowStyle-CssClass="even"
                            AutoGenerateColumns="False" runat="server" OnRowDataBound="gvCreditPurchase_RowDataBound"
                            ForeColor="#333333">
                            <Columns>
                                <asp:TemplateField HeaderText="Bill Date" ItemStyle-Width="8%">
                                    <ItemTemplate>
                                        <asp:Label Visible="true" ID="lblBillDate" runat="server" Text='<%# Eval("BillDate","{0:dd/MM/yyyy}")%>' />
                                        <asp:Label Visible="false" ID="lblPurchaseID" runat="server" Text='<%# Eval("PurchaseID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Billno" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label Style="font-family: Verdana; font-size: 11px;" ID="lblBillno" runat="server"
                                            Text='<%# Eval("Billno") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ledger Name" ItemStyle-Width="17%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLedger" runat="server" Text='<%# Eval("LedgerName") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Purchase Total Value" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("TotalAmt","{0:f2}") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sales Return" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSalesReturn" runat="server" Text='<%# Eval("salesreturn") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reason" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSalesReturnReason" runat="server" Text='<%# Eval("salesreturnreason") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Purchased Items" ItemStyle-Width="40%">
                                    <ItemTemplate>
                                        <br />
                                        <a href="javascript:switchViews('dvcr<%# Eval("Billno") %>', 'imdivcr<%# Eval("Billno") %>');"
                                            style="text-decoration: none;">
                                            <img id="imdivcr<%# Eval("Billno") %>" alt="Show" border="0" src="App_Themes/DefaultTheme/Images/plus.gif" />
                                        </a>
                                        <div id="dvcr<%# Eval("Billno") %>" style="display: none; position: relative; left: 25px;">
                                            <wc:ReportGridView runat="server" BorderWidth="1" ID="gvProducts" ShowFooter="true"
                                                AutoGenerateColumns="false" AllowPrintPaging="true" Width="400px"
                                                Style="font-family: Verdana; font-size: 11px;" OnRowDataBound="gvProducts_RowDataBound">
                                                <PageHeaderTemplate>
                                                    <br />
                                                    <br />
                                                </PageHeaderTemplate>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Item Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("ItemCode") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Product Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("ProductName") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Qty.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQty" runat="server" Text='<%# Eval("Qty") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRate" runat="server" Text='<%# Eval("PurchaseRate","{0:f2}") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Discount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDisc" runat="server" Text='<%# Eval("Discount","{0:f2}") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="VAT">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVat" runat="server" Text='<%# Eval("VAT","{0:f2}") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Value">
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
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <br />
                        <b>Total Credit Purchase: </b>
                        <asp:Label Font-Bold="true" ID="lblGrandCreditPurchase" runat="server"></asp:Label><br />
                        <%--(End)3.Credit Purchase Details--%>
                        <a name="#CaSD"></a>
                    </div>
                    <br />
                    <a href="#Top"></a>
                    <br />
                </td>
                <td align="right" valign="top">
                    <asp:Label ID="lblsumCreditPurchase" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <hr />
                </td>
            </tr>
            <tr>
                <td align="left">
                    <%--(Start)4.Cash Sales Details--%>
                    <a href="javascript:switchViews('dvSD', 'imdivSD');" style="text-decoration: none;">
                        <img id="imdivSD" alt="Show" border="0" src="App_Themes/DefaultTheme/Images/plus.gif" />
                    </a><b>Cash Sales Details</b>
                    <div id="dvSD" style="display: none; position: relative; left: 25px;">
                        <asp:GridView Style="font-family: Verdana; font-size: 11px;" EmptyDataText="No Cash Sales Found Matching"
                            Width="100%" ID="gvCashSales" GridLines="Both" SkinID="gridview" CssClass="gridview"
                            AlternatingRowStyle-CssClass="even" AutoGenerateColumns="False" runat="server"
                            OnRowDataBound="gvCashSales_RowDataBound" ForeColor="#333333">
                            <Columns>
                                <asp:TemplateField HeaderText="Bill Date" ItemStyle-Width="8%">
                                    <ItemTemplate>
                                        <asp:Label Visible="true" ID="lblBillDate" runat="server" Text='<%# Eval("BillDate","{0:dd/MM/yyyy}")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Billno" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label Style="font-family: Verdana; font-size: 11px;" ID="lblBillno" runat="server"
                                            Text='<%# Eval("Billno") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ledger Name" ItemStyle-Width="17%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLedger" runat="server" Text='<%# Eval("LedgerName") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sales Total Value" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("Amount","{0:f2}") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Purchase Return" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPurchaseReturn" runat="server" Text='<%# Eval("PurchaseReturn") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reason" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPurchaseReturnReason" runat="server" Text='<%# Eval("PurchaseReturnReason") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sold Items" ItemStyle-Width="40%">
                                    <ItemTemplate>
                                        <br />
                                        <a href="javascript:switchViews('dv<%# Eval("Billno") %>', 'imdiv<%# Eval("Billno") %>');"
                                            style="text-decoration: none;">
                                            <img id="imdiv<%# Eval("Billno") %>" alt="Show" border="0" src="App_Themes/DefaultTheme/Images/plus.gif" />
                                        </a>
                                        <div id="dv<%# Eval("Billno") %>" style="display: none; position: relative; left: 25px;">
                                            <wc:ReportGridView runat="server" BorderWidth="1" ID="gvProducts" ShowFooter="true"
                                                AutoGenerateColumns="false" PrintPageSize="23" AllowPrintPaging="true" Width="400px"
                                                Style="font-family: Verdana; font-size: 11px;" OnRowDataBound="gvProducts_RowDataBound">
                                                <PageHeaderTemplate>
                                                    <br />
                                                    <br />
                                                </PageHeaderTemplate>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Item Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("ItemCode") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Product Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("ProductName") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Qty.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQty" runat="server" Text='<%# Eval("Qty") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRate" runat="server" Text='<%# Eval("Rate","{0:f2}") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Discount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDisc" runat="server" Text='<%# Eval("Discount","{0:f2}") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="VAT">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVat" runat="server" Text='<%# Eval("VAT","{0:f2}") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Value">
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
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <br />
                        <b>Total Cash Sales: </b>
                        <asp:Label Font-Bold="true" ID="lblGrandCashSales" runat="server"></asp:Label><br />
                        <%--(End)4.Cash Sales Details--%>
                        <a name="#ChSD"></a>
                    </div>
                    <br />
                    <a href="#Top"></a>
                    <br />
                </td>
                <td align="right" valign="top">
                    <asp:Label ID="lblsumCashSales" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <hr />
                </td>
            </tr>
            <tr>
                <td align="left">
                    <%--(Start)5.Cheque Sales Details--%>
                    <a href="javascript:switchViews('dvCSD', 'imdivCSD');" style="text-decoration: none;">
                        <img id="imdivCSD" alt="Show" border="0" src="App_Themes/DefaultTheme/Images/plus.gif" />
                    </a><b>Cheque Sales Details</b>
                    <div id="dvCSD" style="display: none; position: relative; left: 25px;">
                        <asp:GridView Style="font-family: Verdana; font-size: 11px;" EmptyDataText="No Cheque Sales Found Matching"
                            Width="100%" ID="gvChequeSales" GridLines="Both" SkinID="gridview" CssClass="gridview"
                            AlternatingRowStyle-CssClass="even" AutoGenerateColumns="False" runat="server"
                            OnRowDataBound="gvChequeSales_RowDataBound" ForeColor="#333333">
                            <Columns>
                                <asp:TemplateField HeaderText="Bill Date" ItemStyle-Width="8%">
                                    <ItemTemplate>
                                        <asp:Label Visible="true" ID="lblBillDate" runat="server" Text='<%# Eval("BillDate","{0:dd/MM/yyyy}")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Billno" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label Style="font-family: Verdana; font-size: 11px;" ID="lblBillno" runat="server"
                                            Text='<%# Eval("Billno") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ledger Name" ItemStyle-Width="17%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLedger" runat="server" Text='<%# Eval("LedgerName") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sales Total Value" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("Amount","{0:f2}") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Purchase Return" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPurchaseReturn" runat="server" Text='<%# Eval("PurchaseReturn") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reason" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPurchaseReturnReason" runat="server" Text='<%# Eval("PurchaseReturnReason") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sold Items" ItemStyle-Width="40%">
                                    <ItemTemplate>
                                        <br />
                                        <a href="javascript:switchViews('dvch<%# Eval("Billno") %>', 'imdivch<%# Eval("Billno") %>');"
                                            style="text-decoration: none;">
                                            <img id="imdiv<%# Eval("Billno") %>" alt="Show" border="0" src="App_Themes/DefaultTheme/Images/plus.gif" />
                                        </a>
                                        <div id="dvch<%# Eval("Billno") %>" style="display: none; position: relative; left: 25px;">
                                            <wc:ReportGridView runat="server" BorderWidth="1" ID="gvProducts" ShowFooter="true"
                                                AutoGenerateColumns="false" AllowPrintPaging="true" Width="400px"
                                                Style="font-family: Verdana; font-size: 11px;" OnRowDataBound="gvProducts_RowDataBound">
                                                <PageHeaderTemplate>
                                                    <br />
                                                    <br />
                                                </PageHeaderTemplate>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Item Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("ItemCode") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Product Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("ProductName") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Qty.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQty" runat="server" Text='<%# Eval("Qty") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRate" runat="server" Text='<%# Eval("Rate","{0:f2}") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Discount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDisc" runat="server" Text='<%# Eval("Discount","{0:f2}") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="VAT">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVat" runat="server" Text='<%# Eval("VAT","{0:f2}") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Value">
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
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <br />
                        <b>Total Cheque Sales: </b>
                        <asp:Label Font-Bold="true" ID="lblGrandChequeSales" runat="server"></asp:Label><br />
                        <%--(End)5.Cheque Sales Details--%>
                        <a name="#CrSD"></a>
                    </div>
                    <br />
                    <a href="#Top"></a>
                    <br />
                </td>
                <td align="right" valign="top">
                    <asp:Label ID="lblsumChequeSales" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <hr />
                </td>
            </tr>
            <tr>
                <td align="left">
                    <%--(Start)6.Credit Sales Details--%>
                    <a href="javascript:switchViews('dvCRSD', 'imdivRCSD');" style="text-decoration: none;">
                        <img id="imdivRCSD" alt="Show" border="0" src="App_Themes/DefaultTheme/Images/plus.gif" />
                    </a><b>Credit Sales Details</b>
                    <div id="dvCRSD" style="display: none; position: relative; left: 25px;">
                        <asp:GridView Style="font-family: Verdana; font-size: 11px;" EmptyDataText="No Credit Sales Found Matching"
                            Width="100%" ID="gvCreditSales" GridLines="Both" SkinID="gridview" CssClass="gridview"
                            AlternatingRowStyle-CssClass="even" AutoGenerateColumns="False" runat="server"
                            OnRowDataBound="gvCreditSales_RowDataBound" ForeColor="#333333">
                            <Columns>
                                <asp:TemplateField HeaderText="Bill Date" ItemStyle-Width="8%">
                                    <ItemTemplate>
                                        <asp:Label Visible="true" ID="lblBillDate" runat="server" Text='<%# Eval("BillDate","{0:dd/MM/yyyy}")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Billno" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label Style="font-family: Verdana; font-size: 11px;" ID="lblBillno" runat="server"
                                            Text='<%# Eval("Billno") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ledger Name" ItemStyle-Width="17%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLedger" runat="server" Text='<%# Eval("LedgerName") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sales Total Value" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("Amount","{0:f2}") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Purchase Return" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPurchaseReturn" runat="server" Text='<%# Eval("PurchaseReturn") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reason" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPurchaseReturnReason" runat="server" Text='<%# Eval("PurchaseReturnReason") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sold Items" ItemStyle-Width="40%">
                                    <ItemTemplate>
                                        <br />
                                        <a href="javascript:switchViews('dvcrs<%# Eval("Billno") %>', 'imdivcrs<%# Eval("Billno") %>');"
                                            style="text-decoration: none;">
                                            <img id="imdiv<%# Eval("Billno") %>" alt="Show" border="0" src="App_Themes/DefaultTheme/Images/plus.gif" />
                                        </a>
                                        <div id="dvcrs<%# Eval("Billno") %>" style="display: none; position: relative; left: 25px;">
                                            <wc:ReportGridView runat="server" BorderWidth="1" ID="gvProducts" ShowFooter="true"
                                                AutoGenerateColumns="false" PrintPageSize="23" AllowPrintPaging="true" Width="400px"
                                                Style="font-family: Verdana; font-size: 11px;" OnRowDataBound="gvProducts_RowDataBound">
                                                <PageHeaderTemplate>
                                                    <br />
                                                    <br />
                                                </PageHeaderTemplate>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Item Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("ItemCode") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Product Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("ProductName") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Qty.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQty" runat="server" Text='<%# Eval("Qty") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRate" runat="server" Text='<%# Eval("Rate","{0:f2}") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Discount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDisc" runat="server" Text='<%# Eval("Discount","{0:f2}") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="VAT">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVat" runat="server" Text='<%# Eval("VAT","{0:f2}") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Value">
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
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <br />
                        <b>Total Credit Sales: </b>
                        <asp:Label Font-Bold="true" ID="lblGrandCreditSales" runat="server"></asp:Label><br />
                        <%--(End)6.Credit Sales Details--%>
                        <a name="#CaPaid"></a>
                    </div>
                    <br />
                    <a href="#Top"></a>
                    <br />
                </td>
                <td align="right" valign="top">
                    <asp:Label ID="lblsumCreditSales" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <hr />
                </td>
            </tr>
            <tr>
                <td align="left">
                    <%--(Start)7.Cash Paid--%>
                    <a href="javascript:switchViews('dvCaPD', 'imdivCaPD');" style="text-decoration: none;">
                        <img id="imdivCaPD" alt="Show" border="0" src="App_Themes/DefaultTheme/Images/plus.gif" />
                    </a><b>Cash Paid</b>
                    <div id="dvCaPD" style="display: none; position: relative; left: 25px;">
                        <asp:GridView Style="font-family: Verdana; font-size: 11px;" EmptyDataText="No Cash Paid Found Matching"
                            HeaderStyle-HorizontalAlign="Left" CellPadding="2" Width="100%" ID="gvCashPaid"
                            GridLines="Both" SkinID="gridview" CssClass="gridview" AlternatingRowStyle-CssClass="even"
                            AutoGenerateColumns="False" runat="server" OnRowDataBound="gvCashPaid_RowDataBound"
                            ForeColor="#333333">
                            <Columns>
                                <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label Visible="true" Style="font-family: Verdana; font-size: 11px;" ID="lblDate"
                                            runat="server" Text='<%# Eval("TransDate","{0:dd/MM/yyyy}")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Narration">
                                    <ItemTemplate>
                                        <asp:Label Style="font-family: Verdana; font-size: 11px;" ID="lblNarration" runat="server"
                                            Text='<%# Eval("Narration") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Debtor">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLedger" Style="font-family: Verdana; font-size: 11px;" runat="server"
                                            Text='<%# Eval("Debtor") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Creditor">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcLedger" Style="font-family: Verdana; font-size: 11px;" runat="server"
                                            Text='<%# Eval("Creditor") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotal" Style="font-family: Verdana; font-size: 11px;" runat="server"
                                            Text='<%# Eval("Amount","{0:f2}") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <br />
                        <b>Total Cash Paid: </b>
                        <asp:Label Font-Bold="true" ID="lblGrandCashPaid" runat="server"></asp:Label><br />
                        <%--(End)7.Cheque Paid--%>
                        <a name="#ChPaid"></a>
                    </div>
                    <br />
                    <a href="#Top"></a>
                    <br />
                </td>
                <td align="right" valign="top">
                    <asp:Label ID="lblsumCashPaid" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <hr />
                </td>
            </tr>
            <tr>
                <td align="left">
                    <%--(Start)8.Cheque Paid--%>
                    <a href="javascript:switchViews('dvChPD', 'imdivChPD');" style="text-decoration: none;">
                        <img id="imdivChPD" alt="Show" border="0" src="App_Themes/DefaultTheme/Images/plus.gif" />
                    </a><b>Cheque Paid</b>
                    <div id="dvChPD" style="display: none; position: relative; left: 25px;">
                        <asp:GridView Style="font-family: Verdana; font-size: 11px;" EmptyDataText="No Cheque Paid Found Matching"
                            HeaderStyle-HorizontalAlign="Left" CellPadding="2" Width="100%" ID="gvChequePaid"
                            GridLines="Both" SkinID="gridview" CssClass="gridview" AlternatingRowStyle-CssClass="even"
                            AutoGenerateColumns="False" runat="server" OnRowDataBound="gvChequePaid_RowDataBound"
                            ForeColor="#333333">
                            <Columns>
                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label Visible="true" Style="font-family: Verdana; font-size: 11px;" ID="lblDate"
                                            runat="server" Text='<%# Eval("TransDate","{0:dd/MM/yyyy}")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Narration">
                                    <ItemTemplate>
                                        <asp:Label Style="font-family: Verdana; font-size: 11px;" ID="lblNarration" runat="server"
                                            Text='<%# Eval("Narration") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Debtor">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLedger" Style="font-family: Verdana; font-size: 11px;" runat="server"
                                            Text='<%# Eval("Debtor") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Creditor">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcLedger" Style="font-family: Verdana; font-size: 11px;" runat="server"
                                            Text='<%# Eval("Creditor") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotal" Style="font-family: Verdana; font-size: 11px;" runat="server"
                                            Text='<%# Eval("Amount","{0:f2}") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <br />
                        <b>Total Cheque Paid: </b>
                        <asp:Label Font-Bold="true" ID="lblGrandChequePaid" runat="server"></asp:Label><br />
                        <%--(End)8.Cheque Paid--%></div>
                </td>
                <td align="right" valign="top">
                    <asp:Label ID="lblsumChequePaid" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <hr />
                </td>
            </tr>
            <tr>
                <td align="left">
                    <a name="#CaRec"></a><a href="#Top"></a>
                    <br />
                    <%--(Start)9.Cash Received--%>
                    <a href="javascript:switchViews('dvCaRD', 'imdivCaRD');" style="text-decoration: none;">
                        <img id="imdivCaRD" alt="Show" border="0" src="App_Themes/DefaultTheme/Images/plus.gif" />
                    </a><b>Cash Received</b>
                    <div id="dvCaRD" style="display: none; position: relative; left: 25px;">
                        <asp:GridView Style="font-family: Verdana; font-size: 11px;" EmptyDataText="No Cash Received Found Matching"
                            HeaderStyle-HorizontalAlign="Left" CellPadding="2" Width="100%" ID="gvCashReceived"
                            GridLines="Both" SkinID="gridview" CssClass="gridview" AlternatingRowStyle-CssClass="even"
                            AutoGenerateColumns="False" runat="server" OnRowDataBound="gvCashReceived_RowDataBound"
                            ForeColor="#333333">
                            <Columns>
                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label Visible="true" Style="font-family: Verdana; font-size: 11px;" ID="lblDate"
                                            runat="server" Text='<%# Eval("TransDate","{0:dd/MM/yyyy}")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Narration">
                                    <ItemTemplate>
                                        <asp:Label Style="font-family: Verdana; font-size: 11px;" ID="lblNarration" runat="server"
                                            Text='<%# Eval("Narration") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Debtor">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLedger" Style="font-family: Verdana; font-size: 11px;" runat="server"
                                            Text='<%# Eval("Debtor") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Creditor">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcLedger" Style="font-family: Verdana; font-size: 11px;" runat="server"
                                            Text='<%# Eval("Creditor") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotal" Style="font-family: Verdana; font-size: 11px;" runat="server"
                                            Text='<%# Eval("Amount","{0:f2}") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <br />
                        <b>Total Cash Received: </b>
                        <asp:Label Font-Bold="true" ID="lblGrandCashRec" runat="server"></asp:Label><br />
                        <%--(End)9.Cash Received--%>
                        <a name="#ChRec"></a>
                    </div>
                    <br />
                    <a href="#Top"></a>
                    <br />
                </td>
                <td align="right" valign="top">
                    <asp:Label ID="lblsumCashRec" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <hr />
                </td>
            </tr>
            <tr>
                <td align="left">
                    <%--(Start)10.Cheque Received--%>
                    <a href="javascript:switchViews('dvChRD', 'imdivChRD');" style="text-decoration: none;">
                        <img id="imdivChRD" alt="Show" border="0" src="App_Themes/DefaultTheme/Images/plus.gif" />
                    </a><b>Cheque Received</b>
                    <div id="dvChRD" style="display: none; position: relative; left: 25px;">
                        <asp:GridView Style="font-family: Verdana; font-size: 11px;" EmptyDataText="No Cheque Received Found Matching"
                            HeaderStyle-HorizontalAlign="Left" CellPadding="2" Width="100%" ID="gvChequeReceived"
                            GridLines="Both" SkinID="gridview" CssClass="gridview" AlternatingRowStyle-CssClass="even"
                            AutoGenerateColumns="False" runat="server" OnRowDataBound="gvChequeReceived_RowDataBound"
                            ForeColor="#333333">
                            <Columns>
                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label Visible="true" Style="font-family: Verdana; font-size: 11px;" ID="lblDate"
                                            runat="server" Text='<%# Eval("TransDate","{0:dd/MM/yyyy}")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Narration">
                                    <ItemTemplate>
                                        <asp:Label Style="font-family: Verdana; font-size: 11px;" ID="lblNarration" runat="server"
                                            Text='<%# Eval("Narration") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Debtor">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLedger" Style="font-family: Verdana; font-size: 11px;" runat="server"
                                            Text='<%# Eval("Debtor") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Creditor">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcLedger" Style="font-family: Verdana; font-size: 11px;" runat="server"
                                            Text='<%# Eval("Creditor") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotal" Style="font-family: Verdana; font-size: 11px;" runat="server"
                                            Text='<%# Eval("Amount","{0:f2}") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <br />
                        <b>Total Cheque Received: </b>
                        <asp:Label Font-Bold="true" ID="lblGrandChequeRecPaid" runat="server"></asp:Label><br />
                        <%--(End)10.Cheque Received--%>
                        <a name="#itemS"></a>
                    </div>
                    <br />
                    <a href="#Top"></a>
                    <br />
                </td>
                <td align="right" valign="top">
                    <asp:Label ID="lblsumChequeRec" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <hr />
                </td>
            </tr>
            <tr>
                <td align="left">
                    <div style="display: none;">
                        <%--(Start)11.Itemwise Sales--%>
                        <a href="javascript:switchViews('dvIS', 'imdivIS');" style="text-decoration: none;">
                            <img id="imdivIS" alt="Show" border="0" src="App_Themes/DefaultTheme/Images/plus.gif" />
                        </a><b>Itemwise Sales</b>
                        <div id="dvIS" style="display: none; position: relative; left: 25px;">
                            <asp:GridView Style="font-family: Verdana; font-size: 11px;" EmptyDataText="No Items Sold"
                                HeaderStyle-HorizontalAlign="Left" CellPadding="2" Width="100%" ID="gvSales"
                                GridLines="Both" SkinID="gridview" CssClass="gridview" AlternatingRowStyle-CssClass="even"
                                AutoGenerateColumns="False" runat="server" ForeColor="#333333">
                                <Columns>
                                    <asp:TemplateField HeaderText="Itemcode">
                                        <ItemTemplate>
                                            <asp:Label Style="font-family: Verdana; font-size: 11px;" ID="lblitemCode" runat="server"
                                                Text='<%# Eval("itemCode") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Product Name">
                                        <ItemTemplate>
                                            <asp:Label Style="font-family: Verdana; font-size: 11px;" ID="lblProductName" runat="server"
                                                Text='<%# Eval("ProductName") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:Label Style="font-family: Verdana; font-size: 11px;" ID="lblProductDesc" runat="server"
                                                Text='<%# Eval("ProductDesc") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qty.">
                                        <ItemTemplate>
                                            <asp:Label Style="font-family: Verdana; font-size: 11px;" ID="lblQty" runat="server"
                                                Text='<%# Eval("Qty") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label Style="font-family: Verdana; font-size: 11px;" ID="lblAmount" runat="server"
                                                Text='<%# Eval("Amount") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Purchase Return">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPR" Style="font-family: Verdana; font-size: 11px;" runat="server"
                                                Text='<%# Eval("PurchaseReturn") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <br />
                            <%--(Start)11.Itemwise Sales--%>
                            <a name="#itemP"></a>
                        </div>
                        <br />
                        <a href="#Top"></a>
                        <br />
                    </div>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <%--(Start)12.Itemwise Purchase--%>
                    <div style="display: none;">
                        <a href="javascript:switchViews('dvIP', 'imdivIP');" style="text-decoration: none;">
                            <img id="imdivIP" alt="Show" border="0" src="App_Themes/DefaultTheme/Images/plus.gif" />
                        </a><b>Itemwise Purchase</b>
                        <div id="dvIP" style="display: none; position: relative; left: 25px;">
                            <asp:GridView Style="font-family: Verdana; font-size: 11px;" EmptyDataText="No Items Purchased"
                                HeaderStyle-HorizontalAlign="Left" CellPadding="2" Width="100%" ID="gvPurchase"
                                GridLines="Both" SkinID="gridview" CssClass="gridview" AlternatingRowStyle-CssClass="even"
                                AutoGenerateColumns="False" runat="server" ForeColor="#333333">
                                <Columns>
                                    <asp:TemplateField HeaderText="Item Code">
                                        <ItemTemplate>
                                            <asp:Label Visible="true" Style="font-family: Verdana; font-size: 11px;" ID="lblItemCode"
                                                runat="server" Text='<%# Eval("ItemCode")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item Name">
                                        <ItemTemplate>
                                            <asp:Label Style="font-family: Verdana; font-size: 11px;" ID="lblProduct" runat="server"
                                                Text='<%# Eval("ProductName") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Qty. Purchased">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotal" Style="font-family: Verdana; font-size: 11px;" runat="server"
                                                Text='<%# Eval("SumQty") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sales Return">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSR" Style="font-family: Verdana; font-size: 11px;" runat="server"
                                                Text='<%# Eval("SalesReturn") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <br />
                            <%--(End)11.Itemwise Purchase--%>
                            <a name="#grossP"></a>
                        </div>
                        <br />
                        <a href="#Top"></a>
                        <br />
                    </div>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <%--(Start)12.Gross Profit--%>
                    <div style="display: none">
                        <a href="javascript:switchViews('dvGP', 'imdivGP');" style="text-decoration: none;">
                            <img id="imdivGP" alt="Show" border="0" src="App_Themes/DefaultTheme/Images/plus.gif" />
                        </a><b>Gross Profit</b>
                        <div id="dvGP" style="display: none; position: relative; left: 25px;">
                            <asp:GridView Style="font-family: Verdana; font-size: 11px;" ShowFooter="true" EmptyDataText="No Items sold for calculating gross profit"
                                HeaderStyle-HorizontalAlign="Left" CellPadding="2" Width="100%" ID="gvGross"
                                GridLines="Both" SkinID="gridview" CssClass="gridview" AlternatingRowStyle-CssClass="even"
                                OnRowDataBound="gvGross_RowDataBound" AutoGenerateColumns="False" runat="server"
                                ForeColor="#333333">
                                <Columns>
                                    <asp:TemplateField HeaderText="Item Code">
                                        <ItemTemplate>
                                            <asp:Label Visible="true" Style="font-family: Verdana; font-size: 11px;" ID="lblItemCode"
                                                runat="server" Text='<%# Eval("ItemCode")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label Visible="true" Style="font-family: Verdana; font-size: 11px;" ID="lblDate"
                                                runat="server" Text='<%# Eval("TransDate","{0:dd/MM/yyyy}")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item Name">
                                        <ItemTemplate>
                                            <asp:Label Style="font-family: Verdana; font-size: 11px;" ID="lblProduct" runat="server"
                                                Text='<%# Eval("ProductName") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qty.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQty" Style="font-family: Verdana; font-size: 11px;" runat="server"
                                                Text='<%# Eval("SumOfQty") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Buying Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBuyRate" Style="font-family: Verdana; font-size: 11px;" runat="server"
                                                Text='<%# Eval("BuyRate") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Buying Value">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBuyValue" Style="font-family: Verdana; font-size: 11px;" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="VAT">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVat" Visible="false" Style="font-family: Verdana; font-size: 11px;"
                                                runat="server" Text='<%# Eval("Va") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Discount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDisc" Visible="false" Style="font-family: Verdana; font-size: 11px;"
                                                runat="server" Text='<%# Eval("Dis") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sold Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSoldRate" Style="font-family: Verdana; font-size: 11px;" runat="server"
                                                Text='<%# Eval("SoldRate") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sold Value">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSoldValue" Style="font-family: Verdana; font-size: 11px;" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="VAT">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSoldVat" Style="font-family: Verdana; font-size: 11px;" runat="server"
                                                Text='<%# Eval("Vat") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Discount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSoldDisc" Visible="false" Style="font-family: Verdana; font-size: 11px;"
                                                runat="server" Text='<%# Eval("Discount") %>' />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <b>Gross Total Profit</b>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Gross Profit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGrossProfit" Style="font-family: Verdana; font-size: 11px;" runat="server" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblGrossTotal" Style="font-family: Verdana; font-size: 11px; font-weight: bold;"
                                                runat="server" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <br />
                            <%--(End)12.Gross Profit--%>
                        </div>
                        <br />
                        <a href="#Top"></a>
                        <br />
                    </div>
                    <a name="#vatreport"></a>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <%--(Start)13.VAT Report--%>
                    <div style="display: none">
                        <asp:Label ID="sumVatTotal" runat="server"></asp:Label>
                        <a href="javascript:switchViews('dvVR', 'imdivVR');" style="text-decoration: none;">
                            <img id="imdivVR" alt="Show" border="0" src="App_Themes/DefaultTheme/Images/plus.gif" />
                        </a><b>VAT Report</b>
                        <div id="dvVR" style="display: none; position: relative; left: 25px;">
                            <asp:GridView Style="font-family: Verdana; font-size: 11px;" EmptyDataText="No VATs Found"
                                HeaderStyle-HorizontalAlign="Left" CellPadding="2" Width="100%" ID="grdVat" GridLines="Both"
                                SkinID="gridview" CssClass="gridview" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="False"
                                runat="server" OnRowDataBound="grdVat_RowDataBound" ForeColor="#333333">
                                <Columns>
                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label Visible="true" Style="font-family: Verdana; font-size: 11px;" ID="lblDate"
                                                runat="server" Text='<%# Eval("BillDate","{0:dd/MM/yyyy}")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item Code">
                                        <ItemTemplate>
                                            <asp:Label Visible="true" Style="font-family: Verdana; font-size: 11px;" ID="lblItemCode"
                                                runat="server" Text='<%# Eval("ItemCode")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item Name">
                                        <ItemTemplate>
                                            <asp:Label Style="font-family: Verdana; font-size: 11px;" ID="lblProduct" runat="server"
                                                Text='<%# Eval("ProductName") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sold Vat">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSoldVat" Style="font-family: Verdana; font-size: 11px;" runat="server"
                                                Text='<%# Eval("vat") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Buy Vat">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBuyVat" Style="font-family: Verdana; font-size: 11px;" runat="server"
                                                Text='<%# Eval("buyvat") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Difference Vat">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDiffVat" Style="font-family: Verdana; font-size: 11px;" runat="server"
                                                Text='<%# Eval("Differencevat") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <br />
                            <b>Total VAT: </b>
                            <asp:Label Font-Bold="true" ID="lblVatTotal" runat="server"></asp:Label><br />
                            <%--(End)13.VAT Report--%>
                        </div>
                        <br />
                        <a name="#summary"></a><a href="#Top"></a>
                        <br />
                    </div>
                </td>
            </tr>
        </table>
        <br />
        <br />
    </div>
    </form>
</body>
</html>
