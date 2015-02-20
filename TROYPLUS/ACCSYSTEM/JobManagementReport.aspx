<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JobManagementReport.aspx.cs"
    Inherits="JobManagementReport" %>

<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <title>Job Management Report</title>
    <link rel="Stylesheet" href="App_Themes/DefaultTheme/DefaultTheme.css" />
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
    <div align="center">
        <asp:ScriptManager ID="scriptmanager1" runat="server">
        </asp:ScriptManager>
        <br />
        <table cellpadding="2" cellspacing="2" width="460px" border="0" style="border: 1px solid blue;
            text-align: left">
            <tr>
                <td colspan="3" class="subHeadFont">
                    Job Management Report
                </td>
            </tr>
            <tr style="height:6px">
                
            </tr>
            <tr>
                <td class="ControlLabel" style="width:30%">
                    Assigned To:
                </td>
                <td class="ControlDrpBorder" style="width:30%">
                    <asp:DropDownList ID="drpIncharge" AutoPostBack="true" AppendDataBoundItems="true"
                        runat="server" Width="100%" DataSourceID="empSrc" DataTextField="empFirstName" CssClass="drpDownListMedium" BackColor = "#90c9fc" 
                        DataValueField="empno" OnSelectedIndexChanged="drpIncharge_SelectedIndexChanged" style="border: 1px solid #90c9fc" height="26px">
                        <asp:ListItem Text="Select Executive" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="width:30%">
                </td>
            </tr>
            <tr>
                <td class="ControlLabel"" style="width:30%">
                    Job Status
                </td>
                <td style="width:30%" class="ControlDrpBorder">
                    <asp:UpdatePanel ID="UpdatePanel12" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="drpIncharge" EventName="SelectedIndexChanged" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:DropDownList ID="drpJob" AppendDataBoundItems="true" runat="server"
                                 CssClass="drpDownListMedium" BackColor = "#90c9fc" DataTextField="JobTitle" Width="100%" DataValueField="JobID" style="border: 1px solid #90c9fc" height="26px">
                                <asp:ListItem Text="ALL" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td class="lblFont">
                </td>
            </tr>
            <tr>
                <td class="ControlLabel"" style="width:30%">
                    Status
                </td>
                <td style="width:30%" class="ControlDrpBorder" align="left">
                    <asp:CheckBox ID="chkComp" runat="server" />Completed &nbsp;<br />
                    <asp:CheckBox ID="chkPending" runat="server" />Pending &nbsp;
                </td>
                <td style="width:30%">
                </td>
            </tr>
            <tr style="height:6px">
                <td colspan="3">
                    <%--<asp:RequiredFieldValidator CssClass="lblFont" ID="regIncharge" Text=" * - Please select a person - * " InitialValue="0" ControlToValidate="drpIncharge" runat="server" ValidationGroup="cmpval" />&nbsp;--%>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <table width="100%">
                        <tr>
                            <td style="width:40%">
                    
                            </td>
                            <td style="width:20%">
                                <asp:Button ID="cmdReport" Style="width: 100px;" runat="server" CssClass="exportexl6" EnableTheming="false"
                                    OnClick="cmdReport_Click" ValidationGroup="cmpval" />
                                    
                            </td>
                            <td style="width:40%">
                                <input type="Button" value=""
                                        id="Button1" runat="Server" onclick="javascript:CallPrint('divPrint')" class="printbutton6" visible="False" />
                            </td>
                           
                        </tr>
                    </table>
                </td>
                
            </tr>
        </table>
        <br />
        <br />
        <div id="divPrint" runat="server" visible="false" style="font-family: 'Trebuchet MS';
            font-size: 11px;">
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
            </table>
            <br />
            <wc:ReportGridView runat="server" BorderWidth="1" ID="gvJob" CssClass="gridview"
                GridLines="Both" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                PrintPageSize="47" AllowPrintPaging="true" Width="650px" CellPadding="2" DataKeyNames="JobID"
                OnRowDataBound="gvJob_RowDataBound" Style="font-family: 'Trebuchet MS'; font-size: 11px;"
                ShowFooter="false" ShowHeader="true" FooterStyle-CssClass="lblFont">
                <HeaderStyle CssClass="ReportHeadataRow" />
                <RowStyle CssClass="ReportdataRow" />
                <AlternatingRowStyle CssClass="ReportAltdataRow" />
                <FooterStyle CssClass="ReportFooterRow" />
                <Columns>
                    <asp:BoundField DataField="JobTitle" HeaderText="Job Title" />
                    <asp:BoundField DataField="AssignedDate" HeaderText="Assigned Date" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="Qty_Assigned" HeaderText="Assigned Qty" />
                    <asp:BoundField DataField="Qty_returned" HeaderText="Return Qty" />
                    <asp:BoundField DataField="ExpReturnDate" HeaderText="Expected Return Date" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="IsCompleted" HeaderText="Completed" />
                    <asp:TemplateField HeaderText="Assigned Person">
                        <ItemTemplate>
                            <asp:Label ID="lblAssign" runat="server" CssClass="lblFont"></asp:Label>
                            <asp:HiddenField ID="hdass" runat="server" Value='<%# Bind("AssignedTo") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </wc:ReportGridView>
        </div>
        <asp:ObjectDataSource ID="empSrc" runat="server" SelectMethod="ListExecutive" TypeName="BusinessLogic">
            <SelectParameters>
                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
    </form>
</body>
</html>
