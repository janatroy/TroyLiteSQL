<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TimeSheetReportsNew.aspx.cs"
    Inherits="TimeSheetReportsNew" %>

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
            <div runat="server" id="div1">
                <table cellpadding="2" cellspacing="2" width="450px" border="0" style="border: 1px solid blue; background-color: White;">
                    <tr>
                        <td class="headerPopUp" colspan="4" rowspan="1">Time Sheet Report
                        </td>
                    </tr>
                    <tr>
                        <td class="ControlLabel2" style="width: 40%">Date Range
                        </td>
                        <td class="ControlTextBox3" style="width: 25%">
                            <asp:TextBox ID="txtDateRange" runat="server" CssClass="cssTextBox" Width="99px"
                                MaxLength="10" />
                        </td>
                        <td style="width: 15%" align="left">
                            <script type="text/javascript" language="JavaScript">
                                new tcal({ 'formname': 'form1', 'controlname': 'txtDateRange' });</script>
                        </td>
                        <td align="left" style="width: 20%">
                            <asp:RequiredFieldValidator ID="reqDateRange" runat="server" ControlToValidate="txtDateRange" 
                                Display="None" ErrorMessage="Please Enter Start Date" CssClass="lblFont"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                   
                    <tr>
                        <td class="ControlLabel2" style="width: 40%">Employee
                        </td>
                        <td class="ControlDrpBorder" style="width: 25%">
                            <asp:DropDownList ID="ddlEmployee" TabIndex="4" Enabled="True" AppendDataBoundItems="true" Style="border: 1px solid #90c9fc" Height="26px"
                                runat="server" Width="100%" CssClass="drpDownListMedium" BackColor="#90c9fc" DataTextField="empFirstName"
                                DataValueField="empno">
                                <asp:ListItem Text="ALL" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="width: 15%"></td>
                        <td style="width: 20%">
                            <asp:RangeValidator ID="ranEmployee" runat="server"  ControlToValidate="ddlEmployee" ErrorMessage="Select Employee" Display="None" Type="Integer"  MinimumValue="1" MaximumValue="10000000"></asp:RangeValidator>
                        </td>
                    </tr>
                     <tr>
                        <td class="ControlLabel2" style="width: 40%">Approver
                        </td>
                        <td class="ControlDrpBorder" style="width: 25%">
                            <asp:DropDownList ID="ddlApprover" TabIndex="4" Enabled="True" AppendDataBoundItems="true" Style="border: 1px solid #90c9fc" Height="26px"
                                runat="server" Width="100%" CssClass="drpDownListMedium" BackColor="#90c9fc" DataTextField="ManagerFirstName"
                                DataValueField="ManagerID">
                                <asp:ListItem Text="ALL" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="width: 15%"></td>
                        <td style="width: 20%">
                             <asp:RangeValidator ID="ranApprover" runat="server"  ControlToValidate="ddlApprover" ErrorMessage="Select Approver" Display="None" Type="Integer"  MinimumValue="1" MaximumValue="10000000"></asp:RangeValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="ControlLabel2" style="width: 40%">Approved
                        </td>
                        <td class="ControlDrpBorder" style="width: 25%">
                            <asp:DropDownList ID="drpsApproved" runat="server" Width="100%" Style="border: 1px solid #90c9fc" Height="26px" CssClass="drpDownListMedium" BackColor="#90c9fc"
                                AppendDataBoundItems="True" EnableTheming="False">
                                <asp:ListItem Value="">ALL</asp:ListItem>
                                <asp:ListItem Value="NO">No</asp:ListItem>
                                <asp:ListItem Value="YES">Yes</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="width: 15%"></td>
                        <td style="width: 20%"></td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <table width="100%">
                                <tr>
                                    <td width="30%"></td>
                                    <td width="20%">
                                        <asp:Button ID="btnGenerateReport" runat="server" OnClick="btnGenerateReport_Click" CssClass="NewReport6" 
                                            EnableTheming="false" />
                                    </td>
                                    <td width="20%">
                                        <asp:Button ID="btnExportToExcel" runat="server" OnClick="btnExportToExcel_Click" CssClass="exportexl6"
                                            EnableTheming="false" />
                                    </td>

                                    <td width="30%">
                                        <asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="true" ShowSummary="false" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divmain" runat="server" visible="false">
                <table width="700px">
                    <tr>
                        <td colspan="4">
                            <table width="100%">
                                <tr>
                                    <td style="width: 31%"></td>
                                    <td style="width: 19%">
                                        <input type="button" class="printbutton6" id="Button1" runat="Server" onclick="javascript: CallPrint('divPrint')" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" />
                                    </td>
                                    <td style="width: 19%">
                                        <asp:Button ID="btndet" CssClass="GoBack" EnableTheming="false" runat="server"
                                            OnClick="btndet_Click" />
                                    </td>
                                    <td style="width: 31%"></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <br />
                <div id="divPrint" style="font-family: 'Trebuchet MS'; font-size: 11px;" runat="server">

                    <table id="tblTimeSheet" cellpadding="1" cellspacing="0" border="1" class="gridview" style="font-family: 'Trebuchet MS'; font-size: 11px; width: 90%;" runat="server">
                        <tr>
                            <td colspan="2" class="ReportHeadataRow" style="font-family: 'Trebuchet MS'; font-size: 20px; text-align=center;">Time Sheet Entry Report</td>
                        </tr>
                        <tr>
                            <td width="50%">Selected Week is</td>
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
                            <td width="50%">Approver Name is</td>
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
                        PrintPageSize="30" AllowPrintPaging="true" Width="90%" OnRowDataBound="gvTSE_RowDataBound" Style="font-family: 'Trebuchet MS'; font-size: 11px; table-layout:fixed;">
                        <HeaderStyle CssClass="ReportHeadataRow"  />
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
