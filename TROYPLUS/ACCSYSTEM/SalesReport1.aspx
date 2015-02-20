<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalesReport1.aspx.cs" Inherits="SalesReport1" %>

<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>Sales Report</title>
    <link href="App_Themes/DefaultTheme/calendar.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/DefaultTheme/DefaultTheme.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="Scripts/calendar_eu.js"></script>
    <script type="text/javascript" language="JavaScript">
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
    

        function OpenWindow() {
            var ddLedger = document.getElementById('ctl00_cplhControlPanel_drpCustomer');
            var iLedger = ddLedger.options[ddLedger.selectedIndex].text;
            window.open('Service.aspx?ID=' + iLedger, '', "height=400, width=700,resizable=yes, toolbar =no");
            return false;
        }
    

        function showMacAddress() {
            var obj = new ActiveXObject("WbemScripting.SWbemLocator");
            var s = obj.ConnectServer(".");
            var properties = s.ExecQuery("SELECT * FROM Win32_NetworkAdapterConfiguration");
            var e = new Enumerator(properties);
            var output;
            output = '<table border="0" cellPadding="5px" cellSpacing="1px" bgColor="#CCCCCC">';
            output = output + '<tr bgColor="#EAEAEA"><td>Caption</td><td>MACAddress</td></tr>';
            while (!e.atEnd()) {
                e.moveNext();
                var p = e.item();
                if (!p) continue;
                output = output + '<tr bgColor="#FFFFFF">';
                output = output + '<td>' + p.Caption; +'</td>';
                output = output + '<td>' + p.MACAddress + '</td>';
                output = output + '</tr>';
            }
            output = output + '</table>';
            document.getElementById("box").innerHTML = output;
        }
        </script>

</head>
<body style="font-family: 'Trebuchet MS'; font-size: 11px; height: 100%; text-align: center">
    <form id="form1" runat="server">
    <br />
    <div id="div1" runat="server">
        <table cellpadding="2" cellspacing="2" width="460px" border="0"
            style="border: 1px solid blue; text-align: left;">
            <tr class="subHeadFont2">
                <td colspan="3">
                    Sales Report
                </td>
            </tr>
            <tr style="height:6px">
                
            </tr>
            <tr>
                <td style="width:35%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                    Start Date
                </td>
                <td width="40%" class="ControlTextBox3">
                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="cssTextBox" Width="100px"
                        MaxLength="10" />
                    
                    
                </td>
                <td align="left" width="25%">
                    <script type="text/javascript" language="JavaScript">                        new tcal({ 'formname': 'form1', 'controlname': 'txtStartDate' });</script>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtStartDate"
                        Display="None" ErrorMessage="Please Enter Start Date" CssClass="lblFont"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td  style="width:35%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                    End Date
                </td>
                <td align="left" class="ControlTextBox3" width="40%">
                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="cssTextBox" Width="100px" MaxLength="10" />
                    
                    
                </td>
                <td align="left">
                    <script type="text/javascript" language="JavaScript">                        new tcal({ 'formname': 'form1', 'controlname': 'txtEndDate' });</script>
                    <%--<a href="javascript:NewCal('txtEndDate','ddmmyyyy',false,24)"><img src="cal.gif" width="16" height="16" border="0" alt="Pick a date"></a>--%>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEndDate"
                        Display="None" ErrorMessage="Please Enter The End Date" CssClass="lblFont"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtStartDate"
                        ControlToValidate="txtEndDate" Display="None" ErrorMessage="Start Date Should Be Less Than the End Date"
                        CssClass="lblFont" Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <table style="width:100%">
                        <tr>
                            <td class="ControlTextBox3" style="width:100%">
                                <asp:RadioButtonList ID="optionmethod" runat="server" style="font-size:12px" align="center"
                                    RepeatDirection="Horizontal" BackColor="#90C9FC">
                                    <asp:ListItem Selected="True">All</asp:ListItem>
                                    <asp:ListItem>Sales</asp:ListItem>
                                    <asp:ListItem>Internal Transfer</asp:ListItem>
                                    <asp:ListItem>Delivery Note</asp:ListItem>
                                    <asp:ListItem>Purchase Return</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                    
                </td>
            </tr>
            <tr style="height:6px">
                <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="Label1" runat="server"
                                 />
                
            </tr>
            <tr style="height:6px">
                <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="Label2" runat="server"
                                 />
               
            </tr>
            <tr style="height:6px">
                <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="Label3" runat="server"
                                 />
               
            </tr>
            <tr>
                <td colspan="3">
                    <table width="100%">
                        <tr>
                            <td width="30%">
                            
                            </td>
                            <td width="20%">
                                <asp:Button ID="btnReport" EnableTheming="false" runat="server" OnClick="btnReport_Click"
                                Text="" CssClass="NewReport6"  />
                            </td>
                            
                            <td width="20%">
                                <asp:Button ID="btnExl" EnableTheming="false" runat="server" OnClick="btnExl_Click"
                                Text="" CssClass="exportexl6" />
                            </td>
                            <td width="30%">
                                
                            </td>
                         </tr>
                    </table>
                </td>
                
            </tr>
        </table>
        <asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="true" ShowSummary="false" />
        
    </div>
    <div id="SalesPanel" runat="server">
        &nbsp;
         <table width="600px">
                <tr>
                    <td colspan="4">
                        <table width="100%">
                            <tr>
                                <td style="width:40%">

                                </td>
                                <td style="width:19%">
                                    <input type="button" value="" id="Button1" runat="Server" onclick="javascript:CallPrint('divPrint')"
                                    class="printbutton6"  />
                                </td>
                                <td style="width:10%">
                                    <asp:Button ID="btndet" CssClass="GoBack" EnableTheming="false" runat="server"
                                        OnClick="btndet_Click" Visible="False" />
                                </td>
                                <td style="width:31%">

                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <br />
        <div id="divPrint" style="font-family: 'Trebuchet MS'; font-size: 11px;">
           
            <table width="600px" border="0" style="font-family: Trebuchet MS; font-size: 14px;">
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
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <br />
                        <h5>
                            Sales Register From
                            <asp:Label ID="lblStartDate" runat="server"> </asp:Label>
                            To
                            <asp:Label ID="lblEndDate" runat="server"> </asp:Label></h5>
                    </td>
                </tr>
            </table>
            <br />
            <%--OnRowDataBound="gvProducts_RowDataBound"--%>
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
                    <asp:TemplateField HeaderText="Billno" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblBillno" runat="server"
                                Text='<%# Eval("Billno") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Customername" HeaderText="Customer" ItemStyle-Width="20%" />
                    <asp:TemplateField HeaderText="Paymode">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ItemStyle-Width="3%"
                                ID="lblPaymode" runat="server" Text='<%# Eval("Paymode") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sold Items" ItemStyle-Width="62%" ItemStyle-HorizontalAlign="Left">
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
                                        <asp:TemplateField HeaderText="Item Code" ItemStyle-Width="3%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("ItemCode") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Product Name" ItemStyle-Width="57%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("ProductName") %>' /><br />
                                                <b>Model :</b>
                                                <asp:Label ID="lblModel" runat="server" Text='<%# Eval("Model") %>' /><br />
                                                <b>Description :</b><asp:Label ID="lblDes" runat="server" Text='<%# Eval("ProductDesc") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qty" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQty" runat="server" Text='<%# Eval("Qty") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRate" runat="server" Text='<%# Eval("Rate","{0:f2}") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Discount" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDisc" runat="server" Text='<%# Eval("Discount","{0:f2}") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="VAT" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVat" runat="server" Text='<%# Eval("VAT","{0:f2}") %>' />
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
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerTemplate>
                </PagerTemplate>
                <PageFooterTemplate>
                </PageFooterTemplate>
            </wc:ReportGridView>
        </div>
    </div>
    </form>
</body>
</html>
