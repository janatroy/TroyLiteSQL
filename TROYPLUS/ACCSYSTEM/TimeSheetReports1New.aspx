<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TimeSheetReports1New.aspx.cs"
    Inherits="TimeSheetReports1New" %>

<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Time Sheet Report</title>
    <link href="App_Themes/DefaultTheme/calendar.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/DefaultTheme/DefaultTheme.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="Scripts/calendar_eu.js"></script>
    <script type="text/javascript" language="JavaScript">
        function CallPrint(strid) {
            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('', '', 'letf=0,top=0,width=1000,height=400,toolbar=0,scrollbars=1,status=0');
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
        <br />
        <div align="center">
            <div id="divmain" runat="server">
                <table width="700px">
                    <tr>
                        <td colspan="4">
                            <table width="100%">
                                <tr>
                                    <td style="width: 40%"></td>
                                    <td style="width: 20%">
                                        <input type="button" class="printbutton6" id="Button1" runat="Server" onclick="javascript: CallPrint('divPrint')" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" />
                                    </td>
                                    <td style="width: 10%">
                                       <%-- <asp:Button ID="btndet" CssClass="GoBack" EnableTheming="false" runat="server"
                                            OnClick="btndet_Click" Visible="False" />--%>
                                    </td>
                                    <td style="width: 30%"></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <br />
                <div id="divPrint" style="font-family: 'Trebuchet MS'; font-size: 11px;" runat="server">

                    <table cellpadding="1" cellspacing="1" border="1" class="gridview" style="font-family: 'Trebuchet MS'; font-size: 11px; width: 90%;">
                        <tr>
                            <td colspan="2" class="ReportHeadataRow" style="font-family: 'Trebuchet MS'; font-size: 20px; text-align:center;">Time Sheet Entry Report</td>
                        </tr>
                        <tr>
                            <td width="50%">Selected Week </td>
                            <td width="50%">
                                <asp:Label ID="lblSelectedWeek" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td width="50%">Employee Number</td>
                            <td width="50%">
                                <asp:Label ID="lblEmployeeNumber" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td width="50%">Employee Name</td>
                            <td width="50%">
                                <asp:Label ID="lblEmployeeName" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td width="50%">Approver Name </td>
                            <td width="50%">
                                <asp:Label ID="lblArroverName" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td width="50%">Approved Status </td>
                            <td width="50%">
                                <asp:Label ID="lblApprovedStatus" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                    <br />
                    <br />

                    <wc:ReportGridView runat="server" BorderWidth="1" ID="gvTSE" CssClass="gridview"
                        GridLines="Both" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="true" EmptyDataText="No TimeSheet Entry for the given Criteria!"
                        PrintPageSize="30" AllowPrintPaging="true" OnRowDataBound  ="gvTSE_RowDataBound" Width="90%" Style="font-family: 'Trebuchet MS'; font-size: 11px;table-layout:fixed;">
                        <HeaderStyle CssClass="ReportHeadataRow"/>
                        <RowStyle CssClass="ReportdataRow" />
                        <AlternatingRowStyle CssClass="ReportAltdataRow" />
                        <FooterStyle CssClass="ReportFooterRow" />
                        <PageHeaderTemplate>
                            <br />
                            <br />
                        </PageHeaderTemplate>
                       
                        <PagerTemplate>
                        </PagerTemplate>
                        <PageFooterTemplate>
                        </PageFooterTemplate>
                        <AlternatingRowStyle CssClass="even"></AlternatingRowStyle>
                    </wc:ReportGridView>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
