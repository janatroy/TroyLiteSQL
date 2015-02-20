<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YearEndLedgerReport.aspx.cs"
    Inherits="YearEndLedgerReport" %>

<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Year End Report</title>
    <link rel="Stylesheet" href="App_Themes/DefaultTheme/DefaultTheme.css" />
    <!-- March 17  -->
    <link rel="stylesheet" href="App_Themes/DefaultTheme/calendar.css" />
    <script type="text/javascript" language="JavaScript" src="Scripts/calendar_eu.js"></script>
    <!-- March 17  -->
    <script language="javascript" type="text/javascript">
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
    <div>
        <!-- Start March 17  -->
        <br />
        <table cellpadding="2" cellspacing="4" width="700px" border="0" style="border: 1px solid silver;
            text-align: left">
            <tr>
                <td colspan="3" class="subHeadFont">
                    Year End Report
                </td>
            </tr>
            <tr>
                <td class="lblFont" align="left" width="20%">
                    Start Date:
                </td>
                <td class="lblFont" align="left" width="40%">
                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="cssTextBox" Width="100px"
                        MaxLength="10" />
                    <script type="text/javascript" language="JavaScript">                        new tcal({ 'formname': 'form1', 'controlname': 'txtStartDate' });</script>
                </td>
                <td align="left" width="40%">
                    <asp:RequiredFieldValidator class="lblFont" CssClass="lblFont" ID="RequiredFieldValidator1"
                        runat="server" ControlToValidate="txtStartDate" Display="None" ErrorMessage="Please Enter Start Date"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="left" class="lblFont">
                    End Date:
                </td>
                <td align="left">
                    <asp:TextBox ID="txtEndDate" CssClass="cssTextBox" runat="server" Width="100px" MaxLength="10" />
                    <script type="text/javascript" language="JavaScript">                        new tcal({ 'formname': 'form1', 'controlname': 'txtEndDate' });</script>
                    <%--<a href="javascript:NewCal('txtEndDate','ddmmyyyy',false,24)"><img src="cal.gif" width="16" height="16" border="0" alt="Pick a date"></a>--%>
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
                    <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" CssClass="Button"
                        Text="Year End Report" />
                    &nbsp;<input type="button" value="Print " id="Button2" runat="Server" onclick="javascript:CallPrint('divPrint')"
                        class="pageButton" />
                </td>
            </tr>
        </table>
        <asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="true" ShowSummary="false" />
        <br />
        <%--<table width="450px"  border="0" cellpadding="1" cellspacing="2" >
        <tr>
            <td class="lblFont" style="background-color:gray;color:White;">Year End Report</td>
            <td class="lblFont" style="background-color:gray;color:White;"> <input type="button" value="Print " id="Button1" runat="Server" 
             onclick="javascript:CallPrint('divPrint')" class="button"  />&nbsp;</td>
        </tr>
        </table>--%>
        <div id="dvYearEndReport" runat="server" visible="false">
            <div id="divPrint" style="font-family: 'Trebuchet MS'; font-size: 11px;">
                <table width="750px" border="0" cellpadding="1" cellspacing="2" style="font-family: 'Trebuchet MS';
                    font-size: 11px;">
                    <tr>
                        <td colspan="2" class="lblFont">
                            <br />
                            <h5>
                                Year End From
                                <asp:Label ID="lblStartDate" runat="server"> </asp:Label>
                                To
                                <asp:Label ID="lblEndDate" runat="server"> </asp:Label></h5>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <table>
                                <tr>
                                    <td align="left" class="lblFont">
                                        Ledger Index <a href="javascript:switchViews('dvIndex', 'imdiv');" style="text-decoration: none;">
                                            <img id="imdiv" alt="Show" border="0" src="App_Themes/DefaultTheme/Images/plus.gif" />
                                        </a>
                                        <div id="dvIndex" style="display: none; position: relative; left: 25px;">
                                            <wc:ReportGridView runat="server" BorderWidth="1" ID="gvLedger" CssClass="gridview"
                                                GridLines="Both" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                                AllowPrintPaging="true" PrintPageSize="49" PageSize="49" Width="450px" Style="font-family: 'Trebuchet MS';
                                                font-size: 11px;">
                                                <PageHeaderTemplate>
                                                    <br />
                                                    <br />
                                                </PageHeaderTemplate>
                                                <Columns>
                                                    <asp:BoundField DataField="LedgerName" HtmlEncode="false" HeaderText="Ledger" ItemStyle-HorizontalAlign="Left" />
                                                    <asp:BoundField DataField="pagenum" HeaderText="Page Number" ItemStyle-HorizontalAlign="Left" />
                                                    <%--<asp:TemplateField >
                <ItemTemplate >
                <asp:Label ID="lblPageNum"  runat="server"></asp:Label>
                </ItemTemplate> 
                </asp:TemplateField> --%>
                                                </Columns>
                                                <PagerTemplate>
                                                </PagerTemplate>
                                                <PageFooterTemplate>
                                                </PageFooterTemplate>
                                            </wc:ReportGridView>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="700px" border="0" cellpadding="1" cellspacing="2" style="font-family: 'Trebuchet MS';
                    font-size: 11px;">
                    <tr>
                        <td>
                            <wc:ReportGridView runat="server" BorderWidth="1" ID="gvMainLedger" CssClass="gridview"
                                GridLines="Both" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                AllowPrintPaging="true" PrintPageSize="49" PageSize="49" Width="700px" Style="font-family: 'Trebuchet MS';
                                font-size: 11px;" DataKeyNames="LedgerID" OnRowDataBound="gvMainLedger_RowDataBound">
                                <PageHeaderTemplate>
                                    <br />
                                    <br />
                                </PageHeaderTemplate>
                                <PageFooterTemplate>
                                    <br />
                                    <%# gvMainLedger.CurrentPrintPage %><br />
                                </PageFooterTemplate>
                                <Columns>
                                    <asp:BoundField DataField="LedgerName" ItemStyle-HorizontalAlign="Left" HeaderText="Ledger Name" />
                                    <asp:BoundField DataField="Debit" ItemStyle-HorizontalAlign="Left" HeaderText="Debit" />
                                    <asp:BoundField DataField="Credit" ItemStyle-HorizontalAlign="Left" HeaderText="Credit" />
                                    <asp:TemplateField HeaderText="L.FNO" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPg" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </wc:ReportGridView>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <!--End  March 17  -->
    </div>
    </form>
</body>
</html>
