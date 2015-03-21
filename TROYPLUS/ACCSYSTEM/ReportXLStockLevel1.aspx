<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportXLStockLevel1.aspx.cs" Inherits="ReportXLStockLevel1" %>

<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %><!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <title>Obsolete Report</title>
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
        <div style="text-align: center; min-height: 400px">
            <div align="center">
                <br />

            </div>
         
            <div id="divPr" runat="server" align="center" visible="false">
                <table width="700px">
                    <tr>
                        <td colspan="4">
                            <table width="100%">
                                <tr>
                                    <td style="width: 30%"></td>
                                    <td style="width: 19%">
                                        <input type="button" id="Button3" runat="Server" onclick="javascript: CallPrint('divPrint')"
                                            class="printbutton6" style="padding-left: 25px;" />
                                    </td>
                                    <td style="width: 15%">
                                        <asp:Button ID="btndet" CssClass="GoBack" EnableTheming="false" runat="server"
                                            OnClick="btndet_Click" Visible="False" />
                                    </td>
                                    <td style="width: 31%"></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <br />
                <div class="fontName" align="center" id="divPrint" runat="server" visible="false" style="font-family: 'Trebuchet MS'; font-size: 11px;">


                    <table width="700px" border="0" style="font-family: Trebuchet MS; font-size: 14px;">
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
                            <td colspan="3" align="center">
                                <br />
                                <h5>
                            <asp:Label ID="lblHeading" runat="server"> </asp:Label></h5>
                            </td>
                        </tr>
                    </table>
                  <div style="width: 700px" runat="server"  align="center">
                <wc:ReportGridView runat="server"  BorderWidth="1" ID="Grdreport" GridLines="Both"
                    AutoGenerateColumns="true"
                    AllowPrintPaging="true" Width="700px" Style="font-family: 'Trebuchet MS'; font-size: 11px;"
                     >
                    <HeaderStyle CssClass="ReportHeadataRow" />
                    <RowStyle CssClass="ReportdataRow" />
                    <AlternatingRowStyle CssClass="ReportAltdataRow" />
                    <%-- <Columns>
                          <asp:BoundField DataField="brand" HeaderText="brand Name" HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Green" />
                           <asp:BoundField DataField="ProductName" HeaderText="Product Name" HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Green" />
                           <asp:BoundField DataField="Model" HeaderText="Model" HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Green" />
                           <asp:BoundField DataField="ItemCode" HeaderText="Item Code" HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Green" />
                           <asp:BoundField DataField="Rol" HeaderText="Rol" HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Green" />--%>
                          <%-- <asp:BoundField DataField="ProductName" HeaderText="ProductName" HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Green" />
                           <asp:BoundField DataField="ProductDesc" HeaderText="ProductDesc" HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Green" />
                           <asp:BoundField DataField="Stock" HeaderText="Current Stock" HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Green" />
                           <asp:BoundField DataField="BranchCode" HeaderText="Branch Code" HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Green" />--%>
                                                                                                    
                                                <%--</Columns>--%>
                </wc:ReportGridView>
                <br />
                <div style="text-align: right; visibility:hidden">
                    <b><span style="font-family: 'Trebuchet MS'; font-size: 11px;">Grand Total : </span>
                    </b>
                    <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblGrandTotal"
                        runat="server" Font-Bold="true" />
                </div>
            </div>
                </div>                
            </div>
            <br />
            <br />
            <br />
        </div>
    </form>
</body>
</html>
