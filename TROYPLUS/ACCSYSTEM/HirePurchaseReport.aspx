<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HirePurchaseReport.aspx.cs"
    Inherits="HirePurchaseReport" %>

<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <title>Hire Purchase Report</title>
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
    <script language="javascript" type="text/javascript" src="Scripts\calendar_eu.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center" runat="server" id="div1">
        <br />
        <table cellpadding="2" cellspacing="2" width="450px" border="0" style="border: 1px solid blue; background-color:White;
            text-align: left">
            <tr>
                <td colspan="5" class="headerPopUp">
                    Hire Purchase Outstanding
                </td>
            </tr>
            <tr>
                <td class="ControlLabel2" style="width: 40%;">
                    Customer
                </td>
                <td class="ControlDrpBorder" style="width: 25%;">
                    <asp:DropDownList TabIndex="1" ID="drpCustomer" AppendDataBoundItems="true"
                        DataSourceID="srcCreditorDebitor" runat="server" AutoPostBack="false" DataValueField="LedgerID" CssClass="drpDownListMedium" BackColor = "#90c9fc"
                        DataTextField="LedgerName" ValidationGroup="edit" Width="100%"  style="border: 1px solid #90c9fc" height="26px">
                        <asp:ListItem style="background-color: #90c9fc" Text="All" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="ControlLabel" style="width: 35%;">
                    
                </td>
            </tr>
            <%--<tr>
                <td class="ControlLabel2" style="width: 40%;">
                    Frequency
                </td>
                <td class="ControlDrpBorder" style="width: 25%;">
                    <asp:DropDownList ID="drpFrequency" TabIndex="4" AppendDataBoundItems="True" CssClass="drpDownListMedium" BackColor = "#90c9fc"
                        Width="100%" runat="server" AutoPostBack="false" style="border: 1px solid #90c9fc" height="26px" SelectedValue='<%# Bind("Frequency") %>'
                        ValidationGroup="salesval">
                        <asp:ListItem Text="Monthly" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Quarterly" Value="3"></asp:ListItem>
                        <asp:ListItem Text="Annually" Value="12"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="ControlLabel" style="width: 35%;">
                    
                </td>
            </tr>--%>
            <tr>
                <td class="ControlLabel2" style="width: 40%;">
                    Start Date
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtStartDate"
                        Display="None" CssClass="lblFont" ErrorMessage="Please Enter Start Date"></asp:RequiredFieldValidator>
                </td>
                <td class="ControlTextBox3" style="width: 25%;">
                    <asp:TextBox ID="txtStartDate" CssClass="cssTextBox" Width="100px" MaxLength="10"
                        runat="server" />
                </td>
                <td style="width: 35%;" align="left">
                    <script type="text/javascript" language="JavaScript">
                        new tcal({ 'formname': 'form1', 'controlname': 'txtStartDate' });</script>
                </td>
            </tr>
            <tr>
                <td class="ControlLabel2" style="width: 40%;">
                    End Date
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEndDate"
                        Display="None" ErrorMessage="Please Enter The End Date" CssClass="lblFont"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtStartDate"
                        ControlToValidate="txtEndDate" Display="None" ErrorMessage="Start Date Should Be Less Than the End Date"
                        CssClass="lblFont" Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
                </td>
                <td class="ControlTextBox3" style="width: 25%;">
                    <asp:TextBox ID="txtEndDate" CssClass="cssTextBox" Width="100px" MaxLength="10" runat="server" />
                </td>
                <td style="width: 35%;" align="left">
                    <script type="text/javascript" language="JavaScript">
                        new tcal({ 'formname': 'form1', 'controlname': 'txtEndDate' });</script>
                </td>
            </tr>
           <%-- <tr>
                <td class="ControlLabel2" style="width: 25%;">
                    Missed Visits
                </td>
                <td style="width: 25%;">
                    <asp:CheckBox ID="chkMissedVisit" runat="server" />
                </td>
                <td class="leftCol" style="width: 15%;">
                </td>
                <td style="width: 25%;">
                    <asp:CheckBox ID="chkVisitMade" runat="server" Visible="false" />
                </td>
            </tr>--%>
            <tr>
                <td colspan="5">
                    <table width="100%">
                        <tr>
                            <td style="width: 30%;">
                            </td>
                            <%--<td style="width: 20%;">
                                <asp:Button ID="Button2" runat="server" OnClick="btnReport_Click" CssClass="NewReport6"
                                    EnableTheming="false" />
                            </td>--%>
                            
                            <td style="width: 20%;">
                                <asp:Button ID="btnReport" runat="server" OnClick="btnRep_Click" CssClass="exportexl6"
                                    EnableTheming="false" />
                            </td>
                            <td style="width: 30%;">
                                
                            </td>
                            
                    </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div id="divmain" runat="server" align="center" visible="false">
            <table width="700px">
                    <tr>
                        <td colspan="4">
                            <table width="100%">
                                <tr>
                                    <td style="width:31%">

                                    </td>
                                    <td style="width:19%">
                                        <input type="button" id="Button1" runat="Server" onclick="javascript:CallPrint('divPrint')"
                                            class="printbutton6" />
                                        <asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="true" ShowSummary="false" />
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
    <div id="divPrint" runat="server" visible="false" align="center" style="font-family: 'Trebuchet MS';
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
        <br />
         <wc:ReportGridView runat="server" BorderWidth="1" ID="gvSales" SkinID="gridview"
                AutoGenerateColumns="false" AllowPrintPaging="true" Width="90%"
                OnRowDataBound="gvSales_RowDataBound">
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
                    <asp:TemplateField HeaderText="Bill Date" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label Visible="true" ID="lblBillDate" runat="server" Text='<%# Eval("BillDate","{0:dd/MM/yyyy}")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Bill No" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblBillno" runat="server"
                                Text='<%# Eval("slno") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Customername" HeaderText="Customer Name" ItemStyle-Width="20%" />
                    <asp:TemplateField HeaderText="CustomerId">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ItemStyle-Width="3%"
                                ID="lblCustomer" runat="server" Text='<%# Eval("CustomerId") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Final Payment" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblfin" runat="server"  Text='<%# Eval("finpay") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Total Due" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblDue" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Total Payment" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblpay" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    

                    <asp:TemplateField HeaderText="Total Outstanding" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblOut" runat="server"/>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Installment Dues" ItemStyle-Width="62%" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <br />
                            <a href="javascript:switchViews('div<%# Eval("CustomerID") %>', 'imgdiv<%# Eval("CustomerID") %>');"
                                style="text-decoration: none;">
                                <img id="imgdiv<%# Eval("CustomerID") %>" alt="Show" border="0" src="App_Themes/DefaultTheme/Images/plus.gif" />
                            </a>
                            <br />
                            <div id="div<%# Eval("CustomerID") %>" style="display: none; position: relative; left: 25px;">
                                <wc:ReportGridView runat="server" BorderWidth="1" ID="gvProducts" ShowFooter="true"
                                    AutoGenerateColumns="false" PrintPageSize="23" AllowPrintPaging="true" Width="90%"
                                    Style="font-family: 'Trebuchet MS'; font-size: 11px;" OnRowDataBound="gvProducts_RowDataBound">
                                    <HeaderStyle CssClass="ReportHeadataRow" />
                                    <RowStyle CssClass="ReportdataRow" />
                                    <AlternatingRowStyle CssClass="ReportAltdataRow" />
                                    <FooterStyle CssClass="ReportFooterRow" />
                                    <PageHeaderTemplate>
                                        <br />
                                        <br />
                                    </PageHeaderTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Installment Date" ItemStyle-Width="3%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblInsDate" runat="server" Text='<%# Eval("startdate") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount" ItemStyle-Width="57%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("eachpay") %>' /><br />
                                               <%--<b>Model :</b>--%>
                                                <%--<asp:Label ID="lblModel" runat="server" Text='<%# Eval("Model") %>' /><br />
                                                <b>Description :</b><asp:Label ID="lblDes" runat="server" Text='<%# Eval("ProductDesc") %>' />--%>
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
                    <asp:TemplateField HeaderText="Payment Received" ItemStyle-Width="62%" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <br />
                            <a href="javascript:switchViews('div<%# Eval("CustomerID") %>', 'imgdiv<%# Eval("CustomerID") %>');"
                                style="text-decoration: none;">
                                <img id="imgdiv<%# Eval("CustomerID") %>" alt="Show" border="0" src="App_Themes/DefaultTheme/Images/plus.gif" />
                            </a>
                            <br />
                            <div id="div<%# Eval("CustomerID") %>" style="display: none; position: relative; left: 25px;">
                                <wc:ReportGridView runat="server" BorderWidth="1" ID="gvPayment" ShowFooter="true"
                                    AutoGenerateColumns="false" PrintPageSize="23" AllowPrintPaging="true" Width="90%"
                                    Style="font-family: 'Trebuchet MS'; font-size: 11px;" OnRowDataBound="gvPayment_RowDataBound">
                                    <HeaderStyle CssClass="ReportHeadataRow" />
                                    <RowStyle CssClass="ReportdataRow" />
                                    <AlternatingRowStyle CssClass="ReportAltdataRow" />
                                    <FooterStyle CssClass="ReportFooterRow" />
                                    <PageHeaderTemplate>
                                        <br />
                                        <br />
                                    </PageHeaderTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Installment Date" ItemStyle-Width="3%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTransDate" runat="server" Text='<%# Eval("TransDate") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount" ItemStyle-Width="57%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount") %>' /><br />
                                               <%--<b>Model :</b>
                                                <asp:Label ID="lblModel" runat="server" Text='<%# Eval("Model") %>' /><br />
                                                <b>Description :</b><asp:Label ID="lblDes" runat="server" Text='<%# Eval("ProductDesc") %>' />--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerTemplate>
                                    </PagerTemplate>
                                    <PageFooterTemplate>
                                        <br />
                                    </PageFooterTemplate>
                                </wc:ReportGridView>s
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerTemplate>
                </PagerTemplate>
                <PageFooterTemplate>
                </PageFooterTemplate>
            </wc:ReportGridView>
        <asp:ObjectDataSource ID="srcCreditorDebitor" runat="server" SelectMethod="ListCustomersDealers"
            TypeName="BusinessLogic">
            <SelectParameters>
                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
    </div>
    </form>
</body>
</html>
