<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BulkReceiptCheque.aspx.cs" Inherits="BulkReceiptCheque" %>

<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>Bulk Receipt Cheque</title>
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

        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to save data?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }

        </script>

</head>
<body style="font-family: 'Trebuchet MS'; font-size: 11px; height: 100%; text-align: center">
    <form id="form1" runat="server">
    <br />
    <div id="div1" runat="server" align="center">
        <table cellpadding="2" cellspacing="2" width="460px" border="0"
            style="border: 1px solid blue; text-align: left;" >
            <tr class="subHeadFont2">
                <td colspan="3">
                    Bulk Receipt Cheque
                </td>
            </tr>
            <tr style="height:6px">
                
            </tr>
            <tr>
                <td style="width:35%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                    Date
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
            <%--<tr>
                <td  style="width:35%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                    End Date
                </td>
                <td align="left" class="ControlTextBox3" width="40%">
                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="cssTextBox" Width="100px" MaxLength="10" />
                    
                    
                </td>
                <td align="left">
                    <script type="text/javascript" language="JavaScript">                        new tcal({ 'formname': 'form1', 'controlname': 'txtEndDate' });</script>--%>
                    <%--<a href="javascript:NewCal('txtEndDate','ddmmyyyy',false,24)"><img src="cal.gif" width="16" height="16" border="0" alt="Pick a date"></a>--%>
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEndDate"
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
               
            </tr>--%>
            <tr>
                <td colspan="3">
                    <table width="100%">
                        <tr>
                            <td width="45%">
                            
                            </td>
                            <td width="20%">
                                <asp:Button ID="btnReport" EnableTheming="false" runat="server" OnClick="btnReport_Click"
                                Text="" CssClass="NewReport6"  />
                            </td>
                            
                            <td width="5%">
                                <%--<asp:Button ID="btnExl" EnableTheming="false" runat="server" OnClick="btnExl_Click"
                                Text="" CssClass="exportexl6" />--%>
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
         <%--<table width="600px">
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
            <br />--%>
        <div id="divPrint" style="font-family: 'Trebuchet MS'; font-size: 11px;" align="center">
           
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
                <%--<tr>
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
                </tr>--%>
                <tr>
                    <td colspan="3" align="center">
                        <%--<br />--%>
                        <h5>
                            Register as on
                            <asp:Label ID="lblStartDate" runat="server"> </asp:Label>
                            <%--To
                            <asp:Label ID="lblEndDate" runat="server"> </asp:Label>--%></h5>
                    </td>
                </tr>
            </table>
            <%--<br />--%>
            <%--OnRowDataBound="gvProducts_RowDataBound"--%>
            <wc:ReportGridView runat="server" BorderWidth="1" ID="gvSales" SkinID="gridview"
                AutoGenerateColumns="false" AllowPrintPaging="true" Width="90%"  EmptyDataText="No Details Found"
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
                    <asp:BoundField DataField="Slno" HeaderText="Sl No"  HeaderStyle-BorderColor="Gray"/>
                    <asp:BoundField DataField="customername" HeaderText="Customer Name"  HeaderStyle-BorderColor="Gray"/>
                    <asp:BoundField DataField="ChequeNo" HeaderText="Cheque No"  HeaderStyle-BorderColor="Gray"/>
                    <asp:BoundField DataField="DueDate" HeaderText="Due Date"  HeaderStyle-BorderColor="Gray"/>
                    <asp:BoundField DataField="Amount" HeaderText="Amount"  HeaderStyle-BorderColor="Gray"/>
                    <asp:BoundField DataField="Narration" HeaderText="Narration"  HeaderStyle-BorderColor="Gray"/>
                    <asp:BoundField DataField="Paymentmode" HeaderText="Payment Mode"  HeaderStyle-BorderColor="Gray"/>
                    <asp:BoundField DataField="Customerid" HeaderText="Customer id"  HeaderStyle-BorderColor="Gray"/>
                    <asp:BoundField DataField="BankId" HeaderText="Bank Id"  HeaderStyle-BorderColor="Gray"/>
                </Columns>
                <PagerTemplate>
                </PagerTemplate>
                <PageFooterTemplate>
                </PageFooterTemplate>
            </wc:ReportGridView>
            <br />
            
        </div>
        <table width="100%" border="0" style="font-family: Trebuchet MS; font-size: 14px;">
            <tr>
                <td width="33%">
                </td>
                <td width="17%">
                    <asp:Button ID="AddButton" runat="server" SkinID="skinBtnSave"
                        OnClick="AddButton_Click" CssClass="savebutton1231" EnableTheming="false" OnClientClick = "Confirm()"></asp:Button>
                </td>
                <td width="17%">
                    <input type="button" value="" id="Button1" runat="Server" onclick="javascript:CallPrint('divPrint')"
                        class="printbutton6"  />
                </td>
                <td width="33%">
                </td>
            </tr>
        </table>
        
        
    </div>
    </form>
</body>
</html>
