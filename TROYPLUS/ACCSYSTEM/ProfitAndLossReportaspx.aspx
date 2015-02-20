<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProfitAndLossReportaspx.aspx.cs"
    Inherits="ProfitAndLossReportaspx" Title="Untitled Page" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Stock Report</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="3" cellspacing="3" width="100%" border="0" style="border: 1px solid black;"
            class="lblFont">
            <tr>
                <td colspan="3" class="headColor" bgcolor="Gray">
                    Profit And Loss Report
                </td>
            </tr>
            <tr>
                <td align="right" width="20%">
                    Start Date:
                </td>
                <td align="left" width="40%">
                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="lblFont" Width="100px" MaxLength="10" />
                    <script type="text/javascript" language="JavaScript">                        new tcal({ 'formname': 'aspnetForm', 'controlname': 'ctl00$cplhControlPanel$txtStartDate' });</script>
                </td>
                <td align="left" width="40%">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtStartDate"
                        Display="None" ErrorMessage="Please Enter Start Date"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    End Date:
                </td>
                <td align="left">
                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="lblFont" Width="100px" MaxLength="10" />
                    <script type="text/javascript" language="JavaScript">                        new tcal({ 'formname': 'aspnetForm', 'controlname': 'ctl00$cplhControlPanel$txtEndDate' });</script>
                </td>
                <td align="left">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEndDate"
                        Display="None" ErrorMessage="Please Enter The End Date"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtStartDate"
                        ControlToValidate="txtEndDate" Display="None" ErrorMessage="Start Date Should Be Less Than the End Date"
                        Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td colspan="2" align="left">
                    <asp:Button ID="btnReport" SkinID="skinButtonBig" runat="server" CssClass="btnStyle"
                        OnClick="btnReport_Click" Text="Generate Report" />
                    <asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="true" ShowSummary="false" />
                </td>
            </tr>
        </table>
        <br />
        <table cellpadding="2" cellspacing="0" width="100%" border="0" class="lblFont" style="border: 1px solid black;
            background-color: khaki">
            <tr>
                <td colspan="2" align="center" style="font-weight: bold;" class="headColor" bgcolor="Gray">
                    Particulars
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblFromDate" runat="server" CssClass="lblFont"></asp:Label>
                    &nbsp;To &nbsp;
                    <asp:Label ID="lblToDate" runat="server" CssClass="lblfont"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="2" cellspacing="4" width="70%" border="0" style="font-weight: bold;"
                        class="lblFont">
                        <tr>
                            <td>
                                &nbsp;Purchase Accounts
                            </td>
                            <td>
                                &nbsp;<asp:Label ID="lblPurchaseTotal" Text="0" runat="server" CssClass="lblFont"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;Expenses
                            </td>
                            <td>
                                &nbsp;<asp:Label ID="lblExpensesTotal" Text="0" runat="server" CssClass="lblFont"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;<asp:Label ID="lblL" Text="Net Loss" runat="server" CssClass="lblFont"></asp:Label>
                            </td>
                            <td>
                                &nbsp;<asp:Label ID="lblNeLoss" runat="server" Text="0" CssClass="lblFont"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table cellpadding="2" cellspacing="4" width="70%" border="0" style="font-weight: bold;"
                        class="lblFont">
                        <tr>
                            <td>
                                &nbsp;Sales Accounts
                            </td>
                            <td>
                                &nbsp;<asp:Label ID="lblSalesTotal" Text="0" runat="server" CssClass="lblFont"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;Gross Profit
                            </td>
                            <td>
                                &nbsp;<asp:Label ID="lblGP" Text="0" runat="server" CssClass="lblFont"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;<asp:Label ID="lblP" Text="Net Profit" runat="server" CssClass="lblFont"></asp:Label>
                            </td>
                            <td>
                                &nbsp;<asp:Label ID="lblNeProfit" runat="server" Text="0" CssClass="lblFont" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        <div>
            <h5>
                Gross Profit Summary</h5>
            <asp:GridView Style="font-family: 'Trebuchet MS'; font-size: 11px;" AllowPaging="true"
                ShowFooter="true" EmptyDataText="No Items sold for calculating gross profit"
                HeaderStyle-HorizontalAlign="Left" CellPadding="2" Width="100%" ID="gvGross"
                GridLines="Both" SkinID="gridview" CssClass="gridview" AlternatingRowStyle-CssClass="even"
                OnRowDataBound="gvGross_RowDataBound" OnPageIndexChanging="gvGross_PageIndexChanging"
                AutoGenerateColumns="False" runat="server" PageSize="15" ForeColor="#333333">
                <Columns>
                    <asp:TemplateField HeaderText="Item Code">
                        <ItemTemplate>
                            <asp:Label Visible="true" Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblItemCode"
                                runat="server" Text='<%# Eval("ItemCode")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date">
                        <ItemTemplate>
                            <asp:Label Visible="true" Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblDate"
                                runat="server" Text='<%# Eval("TransDate","{0:dd/MM/yyyy}")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Item Name">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblProduct"
                                runat="server" Text='<%# Eval("ProductName") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Qty">
                        <ItemTemplate>
                            <asp:Label ID="lblQty" Style="font-family: 'Trebuchet MS'; font-size: 11px;" runat="server"
                                Text='<%# Eval("SumOfQty") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Purchase Amount">
                        <ItemTemplate>
                            <asp:Label ID="lblBuyRate" Style="font-family: 'Trebuchet MS'; font-size: 11px;"
                                runat="server" Text='<%# Eval("BuyRate","{0:f2}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sold Amount">
                        <ItemTemplate>
                            <asp:Label ID="lblSoldRate" Style="font-family: 'Trebuchet MS'; font-size: 11px;"
                                runat="server" Text='<%# Eval("SoldRate","{0:f2}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Transaction">
                        <ItemTemplate>
                            <asp:Label ID="lblVoucherType" Style="font-family: 'Trebuchet MS'; font-size: 11px;"
                                runat="server" Text='<%# Eval("VoucherType") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Gross Profit">
                        <ItemTemplate>
                            <asp:Label ID="lblGrossProfit" Style="font-family: 'Trebuchet MS'; font-size: 11px;"
                                runat="server" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblGrossTotal" Style="font-family: 'Trebuchet MS'; font-size: 11px;
                                font-weight: bold;" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <br />
            <table class="lblFont">
                <tr>
                    <td>
                        Gross Profit
                    </td>
                    <td>
                        <asp:Label ID="lblGPTotal" Text="0" Style="font-family: 'Trebuchet MS'; font-size: 11px;
                            font-weight: bold;" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
