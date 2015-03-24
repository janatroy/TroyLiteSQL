<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportXLSal1.aspx.cs" Inherits="ReportXLSal1" %>

<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>

<!DOCTYPE html>

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
<body>
    <form id="form1" runat="server">

        <br />
        <div id="SalesPanel" runat="server">
            <center>
            &nbsp;
         <table width="600px">
             <tr>
                 <td colspan="4">
                     <table width="100%">
                         <tr>
                             <td style="width: 40%"></td>
                             <td style="width: 19%">
                                 <input type="button" value="" id="Button1" runat="Server" onclick="javascript: CallPrint('divPrint')"
                                     class="printbutton6" />
                             </td>
                             <td style="width: 10%">
                                 <asp:Button ID="btndet" CssClass="GoBack" EnableTheming="false" runat="server"
                                     Visible="False" />
                             </td>
                             <td style="width: 31%"></td>
                         </tr>
                     </table>
                 </td>
             </tr>
         </table>
            <br />
            <div id="divPrint" style="font-family: 'Trebuchet MS'; font-size: 11px;">

                <table width="600px" border="0" style="font-family: Trebuchet MS; font-size: 14px;">
                    <tr>
                        <td width="140px" align="left">TIN#:
                        <asp:Label ID="lblTNGST" runat="server"></asp:Label>
                        </td>
                        <td align="center" width="320px" style="font-size: 20px;">
                            <asp:Label ID="lblCompany" runat="server"></asp:Label>
                        </td>
                        <td width="140px" align="left">Ph:
                        <asp:Label ID="lblPhone" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">GST#:
                        <asp:Label ID="lblGSTno" runat="server"></asp:Label>
                        </td>
                        <td align="center">
                            <asp:Label ID="lblAddress" runat="server"></asp:Label>
                        </td>
                        <td align="left">Date:
                        <asp:Label ID="lblBillDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                        <td align="center">
                            <asp:Label ID="lblCity" runat="server" />
                            -
                        <asp:Label ID="lblPincode" runat="server"></asp:Label>
                        </td>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                        <td align="center">
                            <asp:Label ID="lblState" runat="server"> </asp:Label>
                        </td>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                        <td align="center">&nbsp;
                        </td>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                        <td align="center">&nbsp;
                        </td>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <asp:Image ID="Image1" runat="server" />
                    </tr>
                    <tr>
                        <td colspan="3">
                            <br />
                            <h5>Sales Turnover report From
                            <asp:Label ID="lblStartDate" runat="server"> </asp:Label>
                                To
                            <asp:Label ID="lblEndDate" runat="server"> </asp:Label></h5>
                        </td>
                    </tr>
                </table>
                <br />
                 <div id="div1" runat="server">
                <wc:ReportGridView runat="server" BorderWidth="1" ID="gvSales" SkinID="gridview"
                    AutoGenerateColumns="true" AllowPrintPaging="true" Width="90%">
                    <RowStyle CssClass="dataRow" />
                    <SelectedRowStyle CssClass="SelectdataRow" />
                    <AlternatingRowStyle CssClass="altRow" />
                    <EmptyDataRowStyle CssClass="HeadataRow" Font-Bold="true" />
                    <HeaderStyle CssClass="HeadataRow" />
                    <FooterStyle CssClass="dataRow" />
                    <PagerStyle CssClass="footer-row allPad" VerticalAlign="Middle" HorizontalAlign="Left" />

                </wc:ReportGridView>
            </div>
            </div>
                </center>
        </div>
    </form>
</body>
</html>
