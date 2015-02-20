<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ComplaintReport.aspx.cs"
    Inherits="ComplaintReport" %>

<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <title>Complaints Report</title>
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
 

    </script>
    <script language="javascript" type="text/javascript" src="Scripts\calendar_eu.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center" style="min-height: 300px">
        <br />
        <table cellpadding="2" cellspacing="2" width="586px" border="0" style="border: 1px solid silver; background-color:White;
            text-align: left">
            <tr>
                <td colspan="5" class="subHeadFont">
                    Complaint Report
                </td>
            </tr>
            <tr>
                <td class="ControlLabel" style="width: 20%;">
                    Customer *
                    <asp:CompareValidator ID="cvCustomer" runat="server" ControlToValidate="drpCustomer"
                        ValidationGroup="edit" Display="Dynamic" ErrorMessage="Customer is Mandatory"
                        Operator="GreaterThan" Text="*" ValueToCompare="0"></asp:CompareValidator>
                </td>
                <td style="width: 30%; text-align: left" class="ControlDrpBorder">
                    <asp:DropDownList TabIndex="1" ID="drpCustomer" AppendDataBoundItems="true" CssClass="drpDownListMedium" BackColor = "#90c9fc" Width="100%"
                        DataSourceID="srcDebitors" runat="server" AutoPostBack="false" DataValueField="LedgerID" style="border: 1px solid #90c9fc" height="26px"
                        DataTextField="LedgerName" ValidationGroup="edit">
                        <asp:ListItem style="background-color: #90c9fc" Text="Select Customer" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="width: 15%;" class="ControlLabel">
                    Start Date
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtStartDate"
                        Display="None" CssClass="lblFont" ErrorMessage="Please Enter Start Date"></asp:RequiredFieldValidator>
                </td>
                <td style="width: 25%;" class="ControlTextBox3">
                    <asp:TextBox ID="txtStartDate" CssClass="cssTextBox" Width="100px" MaxLength="10"
                        runat="server" />
                </td>
                <td>
                    <script type="text/javascript" language="JavaScript">
                        new tcal({ 'formname': 'form1', 'controlname': 'txtStartDate' });</script>
                </td>
            </tr>
            <tr>
                <td class="ControlLabel" style="width: 20%;">
                    Assigned To
                    <asp:RequiredFieldValidator ID="reqAssignedTo" ErrorMessage="Assigned To is mandatory"
                        InitialValue="0" EnableClientScript="true" Text="*" ControlToValidate="drpAssignedTo"
                        runat="server" ValidationGroup="edit" Display="Dynamic" />
                </td>
                <td  style="width: 30%;" class="ControlDrpBorder">
                    <asp:DropDownList ID="drpAssignedTo" TabIndex="4" Enabled="True" AppendDataBoundItems="true" style="border: 1px solid #90c9fc" height="26px"
                        DataSourceID="srcAssinedTo" runat="server" CssClass="drpDownListMedium" BackColor = "#90c9fc" DataTextField="empFirstName" Width="100%"
                        DataValueField="empno">
                        <asp:ListItem Text="Select Employee" style="background-color: #90c9fc" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="width: 15%;" class="ControlLabel">
                    End Date
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEndDate"
                        Display="None" ErrorMessage="Please Enter The End Date" CssClass="lblFont"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtStartDate"
                        ControlToValidate="txtEndDate" Display="None" ErrorMessage="Start Date Should Be Less Than the End Date"
                        CssClass="lblFont" Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
                </td>
                <td style="width: 25%;" class="ControlTextBox3">
                    <asp:TextBox ID="txtEndDate" CssClass="cssTextBox" Width="100px" MaxLength="10" runat="server" />
                </td>
                <td>
                    <script type="text/javascript" language="JavaScript">
                        new tcal({ 'formname': 'form1', 'controlname': 'txtEndDate' });</script>
                </td>
            </tr>
            <tr>
                <td class="ControlLabel" style="width: 20%;">
                    Status
                </td>
                <td style="width: 30%;" class="ControlDrpBorder">
                    <asp:DropDownList ID="drpStatus" runat="server" CssClass="drpDownListMedium" BackColor = "#90c9fc" AutoPostBack="False" Width="100%"
                        AppendDataBoundItems="true" style="border: 1px solid #90c9fc" height="26px">
                        <asp:ListItem Text="Select Status" Value="0"></asp:ListItem>
                        <asp:ListItem Text="In Progress" Value="In Progress"></asp:ListItem>
                        <asp:ListItem Text="Resolved" Value="Resolved"></asp:ListItem>
                        <asp:ListItem Text="Closed" Value="Closed"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="ControlLabel" style="width: 15%;">
                    Is Billed
                </td>
                <td style="width: 25%;" class="ControlDrpBorder">
                    <asp:DropDownList ID="drpBilled" runat="server" CssClass="drpDownListMedium" BackColor = "#90c9fc" AutoPostBack="False" Width="100%"
                        AppendDataBoundItems="true" style="border: 1px solid #90c9fc" height="26px">
                        <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                        <asp:ListItem Text="No" Value="No"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <table width="100%">
                        <tr>
                            <td style="width: 35%;">
                            </td>
                            <td style="width: 20%;">
                                <asp:Button ID="btnReport" CssClass="generatebutton" EnableTheming="false" runat="server"
                                    OnClick="btnReport_Click" />
                    
                            </td>
                            <td style="width: 25%;">
                                <input type="button" class="printbutton" id="Button1" runat="Server" onclick="javascript:CallPrint('divPrint')" />
                            </td>
                            <td style="width: 25%;">
                            </td>
                         </tr>
                      </table>
                 </td>
            </tr>
        </table>
        <asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="true" ShowSummary="false" />
        <br />
        <br />
        <div id="compPanel" runat="server" visible="false">
            <div id="divPrint" style="font-family: 'Trebuchet MS'; font-size: 11px;">
                <asp:HiddenField ID="hdToDelete" Value="" runat="server" />
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
                <wc:ReportGridView runat="server" BorderWidth="1" ID="gvComplaint" CssClass="gridview"
                    AllowPaging="false" AllowPrintPaging="true" PrintPageSize="45" GridLines="Both"
                    AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false" EmptyDataText="No Complaints Found."
                    Width="700px" Style="font-family: 'Trebuchet MS'; font-size: 11px;">
                    <HeaderStyle CssClass="ReportHeadataRow" />
                    <RowStyle CssClass="ReportdataRow" />
                    <AlternatingRowStyle CssClass="ReportAltdataRow" />
                    <FooterStyle CssClass="ReportFooterRow" />
                    <EmptyDataRowStyle CssClass="ReportFooterRow" />
                    <PageHeaderTemplate>
                        <br />
                        <br />
                    </PageHeaderTemplate>
                    <Columns>
                        <asp:BoundField DataField="ComplaintID" HeaderText="Complaint ID" HeaderStyle-Wrap="false" />
                        <asp:BoundField DataField="ComplaintDate" HeaderText="Date" HeaderStyle-Wrap="false"
                            DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="ComplaintDetails" HeaderText="Details" HeaderStyle-Wrap="false"
                            Visible="false" />
                        <asp:BoundField DataField="CustomerName" HeaderText="Customer" HeaderStyle-Wrap="false" />
                        <asp:BoundField DataField="ComplaintStatus" HeaderText="Status" />
                        <asp:BoundField DataField="AssignedTo" HeaderText="AssignedTo" />
                        <asp:BoundField DataField="IsBilled" HeaderText="Is Billed" />
                    </Columns>
                    <PagerTemplate>
                    </PagerTemplate>
                    <PageFooterTemplate>
                        <%-- Page <%# gvCashDetails.CurrentPrintPage.ToString() %> / <%# gvCashDetails.PrintPageCount%>--%>
                    </PageFooterTemplate>
                </wc:ReportGridView>
                <asp:ObjectDataSource ID="srcAssinedTo" runat="server" SelectMethod="ListExecutive"
                    TypeName="BusinessLogic">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="hdDataSource" Name="connection" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="srcDebitors" runat="server" SelectMethod="ListSundryDebtors"
                    TypeName="BusinessLogic">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="hdDataSource" Name="connection" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <br />
                <asp:HiddenField ID="hdDataSource" runat="server" />
                <br />
                <br />
            </div>
        </div>
    </div>
    </form>
</body>
</html>
