<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StockReconReport1.aspx.cs"
    Inherits="StockReconReport1" %>

<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Stock Reconcilation Report</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <link href="App_Themes/DefaultTheme/calendar.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/DefaultTheme/DefaultTheme.css" rel="stylesheet" type="text/css" />
    <link rel="Stylesheet" href="App_Themes/NewTheme/base.css" />
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
    <br />
    <div align="center" runat="server" id="div1">
        <table style="border: 1px solid blue; background-color:White; width:450px">
            <tr>
                <td colspan="3" class="subHeadFont2">
                    Stock Reconcilation Report
                </td>
            </tr>
            <tr style="height:6px">
                    
            </tr>
            <tr>
                <td class="ControlLabel2" width="30%">
                    Date
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtStartDate"
                        Display="None" CssClass="lblFont" ErrorMessage="Please Enter Start Date"></asp:RequiredFieldValidator>
                </td>
                <td class="ControlTextBox3" width="20%">
                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="cssTextBox" Width="100px"
                        MaxLength="10" />
                </td>
                <td align="left" width="30%">
                    <script type="text/javascript" language="JavaScript">
                        new tcal({ 'formname': 'form1', 'controlname': 'txtStartDate' });</script>
                </td>
            </tr>
            <tr style="height:6px">
                    
            </tr>
            <tr>
                <td colspan="3">
                    <table width="100%">
                        <tr>
                            <td width="30%">

                            </td>
                            <td width="20%">
                                <asp:Button ID="btnReport" EnableTheming="false" runat="server" CssClass="NewReport6"
                                Width="120px" OnClick="btnReport_Click" />
                            </td>
                            <td width="20%">
                                <asp:Button ID="Button1" EnableTheming="false" runat="server" CssClass="exportexl6"
                                Width="120px" OnClick="btnRep_Click" />
                                <asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="true" ShowSummary="false" />
                                
                            </td>
                            <td width="30%">
                                <asp:Label ID="err" runat="server" Text="" ForeColor="Red" Font-Bold="true" class="lblFont"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div id="Panel" runat="server" visible="false"  align="center">
        &nbsp;
        <table width="600px">
            <tr>
                <td colspan="4">
                    <table width="100%">
                        <tr>
                            <td style="width:40%">

                            </td>
                            <td style="width:20%">
                                <input type="button" value="" id="Button2" runat="Server" onclick="javascript:CallPrint('divPrint')"
                                class="printbutton6"  />
                            </td>
                            <td style="width:10%">
                                <asp:Button ID="btndet" CssClass="GoBack" EnableTheming="false" runat="server"
                                    OnClick="btndet_Click" Visible="False" />
                            </td>
                            <td style="width:30%">

                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
    <div id="divPrint" style="font-family: 'Trebuchet MS'; width: 70%; font-size: 11px;"
        runat="server">
        <table width="600px" border="0" style="font-family: Trebuchet MS; font-size: 14px;">
                        <tr>
                            <td width="140px" align="left">
                                <asp:Label runat="server" ID="Label16">TIN#:</asp:Label>
                                <asp:Label ID="lblTNGST" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                            <td align="center" width="320px" style="font-size: 20px;">
                                <asp:Label ID="lblCompany" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                            <td width="140px" align="left">
                                <asp:Label runat="server" ID="Label17">Ph:</asp:Label>
                                <asp:Label ID="lblPhone" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Label runat="server" ID="Label18">GST#:</asp:Label>
                                <asp:Label ID="lblGSTno" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lblAddress" runat="server" ></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label runat="server" ID="Label19">Date:</asp:Label>
                                <asp:Label ID="lblBillDate" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td align="center">
                                <asp:Label ID="lblCity" runat="server"></asp:Label>
                                ---<asp:Label ID="lblPinCode" runat="server"></asp:Label>
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
                                <asp:Label ID="lblState" runat="server"></asp:Label>
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
                                <asp:Label ID="Label1" runat="server"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
        <br />
        <wc:ReportGridView runat="server" BorderWidth="1" ID="gvStock" RowStyle-BackColor="#E9F1F6"
            AlternatingRowStyle-BackColor="#FFFFFF" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White"
            AutoGenerateColumns="false" AllowPrintPaging="true" Width="100%"
            Style="font-family: 'Trebuchet MS'; font-size: 11px;" OnRowDataBound="gvStock_RowDataBound">
            <PageHeaderTemplate>
                <br />
                <br />
            </PageHeaderTemplate>
            <Columns>
                <asp:TemplateField HeaderText="Product Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblItem" runat="server"
                            Text='<%# Eval("ItemCode") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Actual Stock" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblActual" runat="server"
                            Text='<%# Eval("ActualStock") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Physical Stock" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblPhysical"
                            runat="server" Text='<%# Eval("PhysicalStock") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </wc:ReportGridView>
        <br />
    </div>
    </div>
    </form>
</body>
</html>
