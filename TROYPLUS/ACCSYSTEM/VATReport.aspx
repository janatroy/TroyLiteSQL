<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="VATReport.aspx.cs" Inherits="VATReport" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <a href="VATSummaryReport.aspx">VAT Summary</a>
    <asp:HiddenField ID="hdPurchase" runat="server" />
    <asp:HiddenField ID="hdSales" runat="server" />
    <%-- <table cellpadding ="2" cellspacing="2" border="0" width="50%" style="font-size :11px;">
      

          <tr>
    <td align="right" width="40%">
      Start Date:
    </td>
    <td align="left" width="60%">
      <asp:TextBox ID="txtStartDate" Width="100px" MaxLength="10" Runat="server"  CssClass="lblFont" />
      <%-- <img src="cal.gif" width="16" height="16" onclick="javascript:NewCal('txtStartDate','ddmmyyyy',false,24)" style="cursor:pointer" border="0" alt="Pick a date">
     <script language="JavaScript">new tcal ({'formname': 'aspnetForm','controlname': 'ctl00$cplhControlPanel$txtStartDate'});</script>

    
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ControlToValidate="txtStartDate" Display="None" 
            ErrorMessage="Please Enter Start Date"></asp:RequiredFieldValidator>
    </td>
  </tr>
  <tr>
    <td align="right">
      End Date:
    </td>
    <td align="left">
      <asp:TextBox ID="txtEndDate" Width="100px" MaxLength="10"  Runat="server" CssClass="lblFont" />
      <script language="JavaScript">new tcal ({'formname': 'aspnetForm','controlname': 'ctl00$cplhControlPanel$txtEndDate'});</script>
    
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
            ControlToValidate="txtEndDate" Display="None" 
            ErrorMessage="Please Enter The End Date"></asp:RequiredFieldValidator>
        <asp:CompareValidator ID="CompareValidator1" runat="server" 
            ControlToCompare="txtStartDate" ControlToValidate="txtEndDate" Display="None" 
            ErrorMessage="Start Date Should Be Less Than the End Date" 
            Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
    </td>
  </tr>
  
        <tr>
        <td colspan="2" align="center"> 
            <asp:Button ID="btnReport" SkinID="skinButtonBig" runat="server"  Width="200px" onclick="btnReport_Click" 
            Text="Summary  Report" />
           </td>
        </tr>
        </table>--%>
    <table cellpadding="2" cellspacing="4" width="600px" border="0" style="border: 1px solid silver;
        background-image: url('App_Themes/DefaultTheme/Images/bluebg.jpg');">
        <tr>
            <td colspan="3" style="background-image: url('App_Themes/DefaultTheme/Images/bgReportheader.jpg');
                color: White; background-repeat: repeat-x; font-size: 11px; font-weight: bold;">
                VAT Detailed Report
            </td>
        </tr>
        <tr>
            <td class="lblFont" align="right" width="20%">
                <b>Start Date:</b>:
            </td>
            <td class="lblFont" align="left" width="40%">
                <asp:TextBox ID="txtStartDate" runat="server" CssClass="lblFont" Width="100px" MaxLength="10" />
                <script language="JavaScript">                    new tcal({ 'formname': 'form1', 'controlname': 'txtStartDate' });</script>
            </td>
            <td align="left" width="40%">
                <asp:RequiredFieldValidator class="lblFont" CssClass="lblFont" ID="RequiredFieldValidator1"
                    runat="server" ControlToValidate="txtStartDate" Display="None" ErrorMessage="Please Enter Start Date"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right" class="lblFont">
                <b>End Date:</b>
            </td>
            <td align="left">
                <asp:TextBox ID="txtEndDate" CssClass="lblFont" runat="server" Width="100px" MaxLength="10" />
                <%--<a href="javascript:NewCal('txtEndDate','ddmmyyyy',false,24)"><img src="cal.gif" width="16" height="16" border="0" alt="Pick a date"></a>--%>
                <script language="JavaScript">                    new tcal({ 'formname': 'form1', 'controlname': 'txtEndDate' });</script>
            </td>
            <td align="left">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEndDate"
                    Display="None" CssClass="lblFont" ErrorMessage="Please Enter The End Date"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" CssClass="lblFont" ControlToCompare="txtStartDate"
                    ControlToValidate="txtEndDate" Display="None" ErrorMessage="Start Date Should Be Less Than the End Date"
                    Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td colspan="2" align="left">
                <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" SkinID="skinButtonBig"
                    Text="VAT Detailed Report" />
                &nbsp;<input type="button" value="Print " id="Button2" runat="Server" onclick="javascript:CallPrint('divPrint')"
                    class="button" />
            </td>
        </tr>
    </table>
    <div id="divPrint" runat="server" visible="false" style="font-family: 'Trebuchet MS';
        font-size: 11px;">
        <h3>
            Summary</h3>
        <br />
        <div id="dvVAT" runat="server" style="font-family: 'Trebuchet MS'; border: solid 1px black;
            font-size: 11px;" width="40%">
        </div>
        <br />
        <table style="font-family: 'Trebuchet MS'; border: solid 1px black; font-size: 11px;"
            width="40%" cellspacing="3" cellpadding="3">
            <tr>
                <td>
                    1
                </td>
                <td align="left">
                    Purchase VAT
                </td>
                <td class="tblLeft">
                    <asp:Label ID="sumPurchase" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    2
                </td>
                <td align="left">
                    Purchase Return VAT
                </td>
                <td class="tblLeft">
                    <asp:Label ID="sumPurchaseReturn" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    3
                </td>
                <td align="left">
                    Sales VAT
                </td>
                <td class="tblLeft">
                    <asp:Label ID="sumSales" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    4
                </td>
                <td align="left">
                    Sales Return VAT
                </td>
                <td class="tblLeft">
                    <asp:Label ID="sumSalesReturn" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    5
                </td>
                <td align="left">
                    VAT Need to PAY
                </td>
                <td class="tblLeft">
                    <asp:Label ID="sumVatToPay" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <h5>
            Purchase VAT Details</h5>
        <asp:GridView Style="font-family: 'Trebuchet MS'; font-size: 11px;" EmptyDataText="No Bills Found"
            HeaderStyle-HorizontalAlign="Left" CellPadding="2" Width="100%" ID="grdPurchaseVat"
            GridLines="Both" SkinID="gridview" CssClass="gridview" AlternatingRowStyle-CssClass="even"
            AutoGenerateColumns="False" runat="server" OnRowDataBound="grdPurchaseVat_RowDataBound"
            ShowFooter="true" ForeColor="#333333">
            <Columns>
                <asp:TemplateField HeaderText="Billno">
                    <ItemTemplate>
                        <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblBillno" runat="server"
                            Text='<%# Eval("PurchaseID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date">
                    <ItemTemplate>
                        <asp:Label Visible="true" Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblDate"
                            runat="server" Text='<%# Eval("BillDate","{0:dd/MM/yyyy}")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Item Name">
                    <ItemTemplate>
                        <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblProduct"
                            runat="server" Text='<%# Eval("ProductName") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Supplier Name - Tin#">
                    <ItemTemplate>
                        <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblLedger" runat="server"
                            Text='<%# Eval("LedgerName") %>' />
                        <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px; color: Red; font-weight: bold"
                            ID="lblTinNumber" runat="server" Text='' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="VAT (%)">
                    <ItemTemplate>
                        <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblVAT" runat="server"
                            Text='<%# Eval("VAT") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Actual Rate">
                    <ItemTemplate>
                        <asp:Label ID="lblRate" Style="font-family: 'Trebuchet MS'; font-size: 11px;" runat="server"
                            Text='<%# Eval("Rate") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Purchase VAT">
                    <ItemTemplate>
                        <asp:Label ID="lblVatRate" Style="font-family: 'Trebuchet MS'; font-size: 11px;"
                            runat="server" Text='<%# Eval("VatRate") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="VAT paid">
                    <ItemTemplate>
                        <asp:Label ID="lblVatPaid" Style="font-family: 'Trebuchet MS'; font-size: 11px;"
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
        <h5>
            Sales Return VAT Details</h5>
        <asp:GridView Style="font-family: 'Trebuchet MS'; font-size: 11px;" ShowFooter="true"
            EmptyDataText="No Bills Found" HeaderStyle-HorizontalAlign="Left" CellPadding="2"
            Width="100%" ID="grdSalesReturnVAT" GridLines="Both" SkinID="gridview" CssClass="gridview"
            AlternatingRowStyle-CssClass="even" AutoGenerateColumns="False" runat="server"
            OnRowDataBound="grdSalesReturnVAT_RowDataBound" ForeColor="#333333">
            <Columns>
                <asp:TemplateField HeaderText="Billno">
                    <ItemTemplate>
                        <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblBillno" runat="server"
                            Text='<%# Eval("PurchaseID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date">
                    <ItemTemplate>
                        <asp:Label Visible="true" Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblDate"
                            runat="server" Text='<%# Eval("BillDate","{0:dd/MM/yyyy}")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Item Name">
                    <ItemTemplate>
                        <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblProduct"
                            runat="server" Text='<%# Eval("ProductName") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Customer Name - Tin#">
                    <ItemTemplate>
                        <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblLedger" runat="server"
                            Text='<%# Eval("LedgerName") %>' />
                        <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px; color: Red; font-weight: bold"
                            ID="lblTinNumber" runat="server" Text='' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="VAT (%)">
                    <ItemTemplate>
                        <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblVAT" runat="server"
                            Text='<%# Eval("VAT") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Actual Rate">
                    <ItemTemplate>
                        <asp:Label ID="lblRate" Style="font-family: 'Trebuchet MS'; font-size: 11px;" runat="server"
                            Text='<%# Eval("Rate") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Purchase VAT">
                    <ItemTemplate>
                        <asp:Label ID="lblVatRate" Style="font-family: 'Trebuchet MS'; font-size: 11px;"
                            runat="server" Text='<%# Eval("VatRate") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="VAT paid">
                    <ItemTemplate>
                        <asp:Label ID="lblVatPaid" Style="font-family: 'Trebuchet MS'; font-size: 11px;"
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
        <h5>
            Sales VAT Details</h5>
        <asp:GridView Style="font-family: 'Trebuchet MS'; font-size: 11px;" ShowFooter="true"
            EmptyDataText="No Bills Found" HeaderStyle-HorizontalAlign="Left" CellPadding="2"
            Width="100%" ID="grdSalesVAT" GridLines="Both" SkinID="gridview" CssClass="gridview"
            AlternatingRowStyle-CssClass="even" AutoGenerateColumns="False" runat="server"
            OnRowDataBound="grdSalesVAT_RowDataBound" ForeColor="#333333">
            <Columns>
                <asp:TemplateField HeaderText="Billno">
                    <ItemTemplate>
                        <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblBillno" runat="server"
                            Text='<%# Eval("Billno") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date">
                    <ItemTemplate>
                        <asp:Label Visible="true" Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblDate"
                            runat="server" Text='<%# Eval("BillDate","{0:dd/MM/yyyy}")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Item Name">
                    <ItemTemplate>
                        <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblProduct"
                            runat="server" Text='<%# Eval("ProductName") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="VAT (%)">
                    <ItemTemplate>
                        <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblVAT" runat="server"
                            Text='<%# Eval("VAT") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Cusomer Name - Tin#">
                    <ItemTemplate>
                        <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblLedger" runat="server"
                            Text='<%# Eval("CustomerName") %>' />
                        <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px; color: Red; font-weight: bold"
                            ID="lblTinNumber" runat="server" Text='' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Actual Rate">
                    <ItemTemplate>
                        <asp:Label ID="lblRate" Style="font-family: 'Trebuchet MS'; font-size: 11px;" runat="server"
                            Text='<%# Eval("Rate") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Purchase VAT">
                    <ItemTemplate>
                        <asp:Label ID="lblVatRate" Style="font-family: 'Trebuchet MS'; font-size: 11px;"
                            runat="server" Text='<%# Eval("VatRate") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="VAT paid">
                    <ItemTemplate>
                        <asp:Label ID="lblVatPaid" Style="font-family: 'Trebuchet MS'; font-size: 11px;"
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
        <h5>
            Purchase Return VAT Details</h5>
        <asp:GridView Style="font-family: 'Trebuchet MS'; font-size: 11px;" ShowFooter="true"
            EmptyDataText="No Bills Found" HeaderStyle-HorizontalAlign="Left" CellPadding="2"
            Width="100%" ID="grdPurchaseReturnVAT" GridLines="Both" SkinID="gridview" CssClass="gridview"
            AlternatingRowStyle-CssClass="even" AutoGenerateColumns="False" runat="server"
            OnRowDataBound="grdPurchaseReturnVAT_RowDataBound" ForeColor="#333333">
            <Columns>
                <asp:TemplateField HeaderText="Billno">
                    <ItemTemplate>
                        <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblBillno" runat="server"
                            Text='<%# Eval("Billno") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date">
                    <ItemTemplate>
                        <asp:Label Visible="true" Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblDate"
                            runat="server" Text='<%# Eval("BillDate","{0:dd/MM/yyyy}")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Item Name">
                    <ItemTemplate>
                        <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblProduct"
                            runat="server" Text='<%# Eval("ProductName") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="VAT (%)">
                    <ItemTemplate>
                        <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblVAT" runat="server"
                            Text='<%# Eval("VAT") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Supplier Name - Tin#">
                    <ItemTemplate>
                        <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblLedger" runat="server"
                            Text='<%# Eval("CustomerName") %>' />
                        <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px; color: Red; font-weight: bold"
                            ID="lblTinNumber" runat="server" Text='' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Actual Rate">
                    <ItemTemplate>
                        <asp:Label ID="lblRate" Style="font-family: 'Trebuchet MS'; font-size: 11px;" runat="server"
                            Text='<%# Eval("Rate") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Purchase VAT">
                    <ItemTemplate>
                        <asp:Label ID="lblVatRate" Style="font-family: 'Trebuchet MS'; font-size: 11px;"
                            runat="server" Text='<%# Eval("VatRate") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="VAT paid">
                    <ItemTemplate>
                        <asp:Label ID="lblVatPaid" Style="font-family: 'Trebuchet MS'; font-size: 11px;"
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
    </div>
</asp:Content>
