<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QtyReturnReport.aspx.cs"
    Inherits="QtyReturnReport" %>

<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <title>Item Tracking Report</title>
    <link href="App_Themes/DefaultTheme/calendar.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="Scripts/calendar_eu.js"></script>
    <link href="App_Themes/DefaultTheme/DefaultTheme.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript">
        function unl() {

            document.form1.submit();
        }
    </script>
    <%--<script language="javascript" type="text/javascript" src="datetimepicker.js"></script>--%>
</head>
<body style="font-family: 'Trebuchet MS'; font-size: 12px;" onbeforeunload="unl()">
    <form id="form1" method="post" runat="server">
    <br />
    <div align="center">
        <table cellpadding="2" cellspacing="4" width="350px" border="0" style="border: 1px solid silver; background-color:White;
            text-align: left">
            <tr class="subHeadFont">
                <td colspan="3">
                    Item Tracking Report
                </td>
            </tr>
            <tr>
                <td class="ControlLabel" style="width:30%">
                    Ledger :
                </td>
                <td class="ControlDrpBorder">
                    <asp:DropDownList ID="drpLedgerName" runat="server" Width="180px" DataTextField="LedgerName" CssClass="drpDownListMedium" BackColor = "#90c9fc" 
                         style="border: 1px solid #90c9fc" height="25px" DataValueField="LedgerID">
                    </asp:DropDownList>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <table width="100%">
                        <tr>
                            <td style="width:35%">
                    
                            </td>
                            <td style="width:20%">
                                <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click"
                                     CssClass="generatebutton" EnableTheming="false"  />
                            </td>
                            <td style="width:30%">
                                <asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="true" ShowSummary="false" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <asp:HiddenField ID="hdFilename" runat="server" />
        <asp:HiddenField ID="hdToDelete" Value="" runat="server" />
    </div>
    </form>
</body>
</html>
