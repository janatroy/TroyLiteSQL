<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GrossProfitSummaryReport.aspx.cs"
    Inherits="GetPurchaseRate" %>

<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <link href="App_Themes/DefaultTheme/calendar.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="Scripts/calendar_eu.js"></script>
    <link href="App_Themes/DefaultTheme/DefaultTheme.css" rel="stylesheet" type="text/css" />
    <title>Gross Profit Summary Report</title>
    <script language="javascript" type="text/javascript">
        function CallPrint(strid) {
            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('', '', 'letf=0,top=0,width=600,height=400,toolbar=0,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();

        }
        function PrintItem(ID, Customername) {
            //alert(Customername);
            window.showModalDialog('BillCustomerView.aspx?ID=' + ID + '&cname=' + escape(Customername), self, 'dialogWidth:700px;dialogHeight:430px;status:no;dialogHide:yes;unadorned:yes;');
            //window.open('BillCustomerView.aspx?ID=' + ID + '&cname=' + Customername ,'','letf=0,top=0,width=600,height=400,toolbar=0,scrollbars=1,status=0');
            //alert('BillCustomerView.aspx?ID=' + ID + '&cname=' + Customername );
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
<body>
    <form id="form1" runat="server">
    <div style="width: 90%; text-align: center">
        <br />
        <table cellpadding="2" cellspacing="4" width="80%" border="0" style="border: 1px solid silver;
            text-align: left">
            <tr>
                <td colspan="3" class="subHeadFont">
                    Gross Profit Summary Report
                </td>
            </tr>
            <tr>
                <td class="ControlLabel">
                    Start Date
                </td>
                <td class="ControlTextBox">
                    <asp:TextBox ID="txtStartDate" CssClass="cssTextBox" Width="100px" MaxLength="10"
                        runat="server" />
                </td>
                <td>
                    <script type="text/javascript" language="JavaScript">
                        new tcal({ 'formname': 'form1', 'controlname': 'txtStartDate' });</script>
                </td>
                <td align="left" width="50%">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtStartDate"
                        Display="None" CssClass="lblFont" ErrorMessage="Please Enter Start Date"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="ControlLabel">
                    End Date
                </td>
                <td class="ControlTextBox">
                    <asp:TextBox ID="txtEndDate" CssClass="cssTextBox" Width="100px" MaxLength="10" runat="server" />
                </td>
                <td>
                    <script type="text/javascript" language="JavaScript">
                        new tcal({ 'formname': 'form1', 'controlname': 'txtEndDate' });</script>
                </td>
                <td align="left">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEndDate"
                        Display="None" ErrorMessage="Please Enter The End Date" CssClass="lblFont"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtStartDate"
                        ControlToValidate="txtEndDate" Display="None" ErrorMessage="Start Date Should Be Less Than the End Date"
                        CssClass="lblFont" Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="ControlLabel">
                    Data Display Mode
                </td>
                <td align="left">
                    <asp:DropDownList TabIndex="1" ID="cmbDisplayCat" CssClass="cssDropDown" Width="200px"
                        runat="server">
                        <asp:ListItem style="background-color: #bce1fe" Text="Select Display Mode" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Daywise" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Categorywise" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Brandwise" Value="3"></asp:ListItem>
                        <asp:ListItem Text="Modelwise" Value="4"></asp:ListItem>
                        <asp:ListItem Text="Billwise" Value="5"></asp:ListItem>
                        <asp:ListItem Text="Customerwise" Value="6"></asp:ListItem>
                        <asp:ListItem Text="Executivewise" Value="7"></asp:ListItem>
                        <asp:ListItem Text="Itemwise" Value="8"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="left">
                    <asp:RequiredFieldValidator CssClass="lblFont" ID="reqSuppllier" Text="Display Mode is mandatory"
                        InitialValue="0" ControlToValidate="cmbDisplayCat" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="ControlLabel">
                    Rate Type
                </td>
                <td align="left">
                    <asp:RadioButtonList ID="rdoRate" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="RATE" Text="Rate" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="NLP" Text="NLP"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td align="left">
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" CssClass="generatebutton"
                        EnableTheming="false" />
                    <input type="button" class="printbutton" id="Button1" runat="Server" onclick="javascript:CallPrint('divPrint')" />
                    <asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="true" ShowSummary="false" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Label ID="lblErr" runat="server" CssClass="errorMsg"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:Panel ID="pnlContent" runat="server" Visible="false">
            <br />
            <asp:Label ID="lblMessage" Text="" CssClass="lblFont" runat="server"></asp:Label>
            <br />
            <br />
            <center>
                <h5>
                    GROSS PROFIT SUMMARY</h5>
                <table width="50%" style="border: 1px solid red;">
                    <tr>
                        <td style="height: 100px">
                            <asp:Label ID="lblGrossPL" Font-Size="Large" ForeColor="Blue" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <br />
            </center>
            <wc:ReportGridView runat="server" BorderWidth="1" ID="gvMain" GridLines="Both" AlternatingRowStyle-CssClass="even"
                AutoGenerateColumns="false" AllowPaging="false" AllowPrintPaging="false" Width="100%"
                CellPadding="2" OnRowDataBound="gvMain_RowDataBound" ShowFooter="True" ShowHeader="True">
                <FooterStyle CssClass="ReportFooterRow" />
                <HeaderStyle CssClass="ReportHeadataRow" />
                <RowStyle CssClass="ReportdataRow" />
                <AlternatingRowStyle CssClass="ReportAltdataRow" />
                <Columns>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <a href="javascript:switchViews('dv<%# Eval("LinkName") %>', 'imdiv<%# Eval("LinkName") %>');"
                                style="text-decoration: none;">
                                <img id="imdiv<%# Eval("LinkName") %>" alt="Show" border="0" src="App_Themes/DefaultTheme/Images/plus.gif" />
                            </a>
                            <asp:Label ID="lblLink" runat="server" Text='<%# Eval("LinkName") %>' CssClass="lblFont"></asp:Label>
                            <br />
                            <div id="dv<%# Eval("LinkName") %>" style="display: none; position: relative; left: 25px;">
                                <wc:ReportGridView runat="server" BorderWidth="1" ID="gvSecond" GridLines="Both"
                                    AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false" AllowPaging="false"
                                    AllowPrintPaging="false" ShowFooter="true" Width="80%" Style="font-family: 'Trebuchet MS';
                                    font-size: 11px;" OnRowDataBound="gvSecond_RowDataBound">
                                    <FooterStyle CssClass="ReportFooterRow" />
                                    <HeaderStyle CssClass="ReportHeadataRow" />
                                    <RowStyle CssClass="ReportdataRow" />
                                    <PageHeaderTemplate>
                                        <br />
                                        <br />
                                    </PageHeaderTemplate>
                                    <Columns>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Billno" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <b>
                                                    <%# Eval("SalesBillno") %>
                                                </b>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Item Code" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <b>
                                                    <%# Eval("ItemCode") %>
                                                </b>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="ProductDesc" ItemStyle-Width="15%">
                                            <ItemTemplate>
                                                <b>
                                                    <%# Eval("ProductDesc") %>
                                                </b>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-BackColor="#99CCFF"
                                            ItemStyle-Font-Bold="true" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="XX-Small"
                                            HeaderText="Sales Rate">
                                            <ItemTemplate>
                                                <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblRate" runat="server"
                                                    Text='<%# Eval("SRate","{0:f2}") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-BackColor="#99CCFF"
                                            ItemStyle-Font-Bold="true" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="XX-Small"
                                            HeaderText="Qty.">
                                            <ItemTemplate>
                                                <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblSQty" runat="server"
                                                    Text='<%# Eval("Quantity","{0:f2}") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                                            HeaderText="Sales(Rate * Qty.)">
                                            <ItemTemplate>
                                                <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblNetRate"
                                                    runat="server" Text='<%# Eval("NetRate","{0:f2}") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                                            HeaderText="Discount Rate">
                                            <ItemTemplate>
                                                <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblDiscountRate"
                                                    runat="server" Text='<%# Eval("ActualDiscount","{0:f2}") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                                            HeaderText="VAT Rate">
                                            <ItemTemplate>
                                                <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblVatRate"
                                                    runat="server" Text='<%# Eval("ActualVat","{0:f2}") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                                            HeaderText="CST Rate">
                                            <ItemTemplate>
                                                <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblCSTRate"
                                                    runat="server" Text='<%# Eval("ActualCST","{0:f2}") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                                            HeaderText="Freight">
                                            <ItemTemplate>
                                                <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblFreightRate"
                                                    runat="server" Text='<%# Eval("SumFreight","{0:f2}") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                                            HeaderText="Ldg/Unldg">
                                            <ItemTemplate>
                                                <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblLURate" runat="server"
                                                    Text='<%# Eval("Loading","{0:f2}") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                                            HeaderText="Sales Total">
                                            <ItemTemplate>
                                                <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblTotal" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                                            ItemStyle-BackColor="#99CCFF" ItemStyle-Font-Bold="true" ItemStyle-Font-Size="XX-Small"
                                            HeaderText="Purchase Rate">
                                            <ItemTemplate>
                                                <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblPRate" runat="server"
                                                    Text='<%# Eval("PRate","{0:f2}") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                                            HeaderText="Purchase(Rate * Qty.)">
                                            <ItemTemplate>
                                                <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblPurchaseNetRate"
                                                    runat="server" Text='<%# Eval("PurchaseNetRate","{0:f2}") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                                            HeaderText="Discount Rate">
                                            <ItemTemplate>
                                                <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblPurchaseDiscountRate"
                                                    runat="server" Text='<%# Eval("PurchaseActualDiscount","{0:f2}") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                                            HeaderText="VAT Rate">
                                            <ItemTemplate>
                                                <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblPurchaseVatRate"
                                                    runat="server" Text='<%# Eval("PurchaseActualVat","{0:f2}") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                                            HeaderText="CST Rate">
                                            <ItemTemplate>
                                                <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblPurchaseCSTRate"
                                                    runat="server" Text='<%# Eval("PurchaseActualCST","{0:f2}") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                                            HeaderText="Freight">
                                            <ItemTemplate>
                                                <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblPurchaseFreightRate"
                                                    runat="server" Text='<%# Eval("SumPurchaseFreight","{0:f2}") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                                            HeaderText="Ldg/Unldg">
                                            <ItemTemplate>
                                                <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblPurchaseLURate"
                                                    runat="server" Text='<%# Eval("PurLoading","{0:f2}") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                                            HeaderText="Purchase Total">
                                            <ItemTemplate>
                                                <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblPurchaseTotal"
                                                    runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                                            HeaderText="GrossProfit">
                                            <ItemTemplate>
                                                <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblGp" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </wc:ReportGridView>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                        ItemStyle-Font-Size="XX-Small" HeaderText="Sales (Qty. * Rate)">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblNetRate"
                                runat="server" Text='<%# Eval("NetRate","{0:f2}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                        HeaderText="Discount Rate">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblDiscountRate"
                                runat="server" Text='<%# Eval("ActualDiscount","{0:f2}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                        HeaderText="VAT Rate">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblVatRate"
                                runat="server" Text='<%# Eval("ActualVat","{0:f2}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                        HeaderText="CST Rate">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblCSTRate"
                                runat="server" Text='<%# Eval("ActualCST","{0:f2}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                        HeaderText="Freight">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblFreightRate"
                                runat="server" Text='' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                        HeaderText="Ldg/Unldg">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblLURate" runat="server"
                                Text='' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                        HeaderText="Sales Total">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblTotal" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                        HeaderText="Purchase(Rate * Qty)">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblPurchaseNetRate"
                                runat="server" Text='<%# Eval("PurchaseNetRate","{0:f2}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                        HeaderText="Discount Rate">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblPurchaseDiscountRate"
                                runat="server" Text='<%# Eval("PurchaseActualDiscount","{0:f2}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                        HeaderText="VAT Rate">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblPurchaseVatRate"
                                runat="server" Text='<%# Eval("PurchaseActualVat","{0:f2}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                        HeaderText="CST Rate">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblPurchaseCSTRate"
                                runat="server" Text='<%# Eval("PurchaseActualCST","{0:f2}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                        HeaderText="Freight">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblPurchaseFreightRate"
                                runat="server" Text='<%# Eval("SumPurchaseFreight","{0:f2}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                        HeaderText="Ldg/Unldg">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblPurchaseLURate"
                                runat="server" Text='<%# Eval("PurLoading","{0:f2}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                        HeaderText="Purchase Total">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblPurchaseTotal"
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                        HeaderText="GrossProfit">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblGp" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </wc:ReportGridView>
            <br />
            <br />
        </asp:Panel>
    </div>
    </form>
</body>
</html>
