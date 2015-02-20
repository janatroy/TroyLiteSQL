<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportXlCommission.aspx.cs"
    Inherits="ReportXlCommission" %>

<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <title></title>
    <link href="App_Themes/DefaultTheme/DefaultTheme.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/NewTheme/base.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/DefaultTheme/calendar.css" rel="stylesheet" type="text/css" />
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
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager runat="server" ID="scr"></asp:ScriptManager>
    <div align="center">
        <br />
        <table cellpadding="2" cellspacing="2" width="450px" border="0" style="border: 1px solid blue;
            text-align: left">
            <tr>
                <td colspan="3" class="headerPopUp">
                    Commission Report
                </td>
            </tr>
            <tr style="height:1px">
            </tr>
            <tr>
                <td>
                    <table cellpadding="3" cellspacing="1" width="100%">
                        <tr style="height:5px">
                        </tr>
                        <tr>
                            <td class="ControlLabel" style="width: 40%;">
                                Start Date
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtStartDate"
                                    Display="None" CssClass="lblFont" ErrorMessage="Please Enter Start Date"></asp:RequiredFieldValidator>
                            </td>
                            <td class="ControlTextBox3" style="width: 20%;">
                                <asp:TextBox ID="txtStartDate" Enabled="false" CssClass="cssTextBox" MaxLength="10"
                                    runat="server" />
                                    <cc1:CalendarExtender ID="calStartDate" runat="server" Enabled="True" 
                                        Format="dd/MM/yyyy"
                                        PopupButtonID="btnStartDate" TargetControlID="txtStartDate">
                                    </cc1:CalendarExtender>
                            </td>
                            <td style="width: 30%;">
                                <asp:ImageButton ID="btnStartDate" runat="server" CausesValidation="False" 
                                    ImageUrl="App_Themes/NewTheme/images/cal.gif" 
                                    Width="20px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="ControlLabel" style="width: 40%;">
                                End Date
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEndDate"
                                    Display="None" ErrorMessage="Please Enter The End Date" CssClass="lblFont"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtStartDate"
                                    ControlToValidate="txtEndDate" Display="None" ErrorMessage="Start Date Should Be Less Than the End Date"
                                    CssClass="lblFont" Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
                            </td>
                            <td class="ControlTextBox3" style="width: 20%;">
                                <asp:TextBox ID="txtEndDate" Enabled="false" CssClass="cssTextBox" MaxLength="10" runat="server" />
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" 
                                    Format="dd/MM/yyyy"
                                    PopupButtonID="ImageButton1" TargetControlID="txtEndDate">
                                </cc1:CalendarExtender>
                            </td>
                            <td style="width: 30%;">
                                <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" 
                                    ImageUrl="App_Themes/NewTheme/images/cal.gif" 
                                    Width="20px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="ControlLabel"  width="40%">
                                Customer Name
                            </td>
                            <td width="20%"  class="ControlTextBox2" style="border: 1px solid Blue">
                                <asp:DropDownList ID="ddlCustomer" AppendDataBoundItems="true" style="border: 1px solid #90C9FC" runat="server" Height="25px" Width="100%"  CssClass="drpDownListMedium" BackColor = "#90C9FC" DataTextField="LedgerName"  DataValueField="LedgerID">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 30%;">

                            </td>
                        </tr>
                        <tr>  
                            <td class="ControlLabel"  width="40%">
                                Supplier Name
                            </td>
                            <td width="20%" class="ControlTextBox2" style="border: 1px solid Blue">
                                <asp:DropDownList ID="ddlSupplier" runat="server" style="border: 1px solid #90C9FC" Height="25px" Width="100%"  CssClass="drpDownListMedium" BackColor = "#90C9FC" DataTextField="LedgerName"  DataValueField="LedgerID">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 30%;">

                            </td>
                        </tr>
                        <%--<tr>  
                            <td class="ControlLabel"  width="40%">
                                Category Name
                            </td>
                            <td width="20%" class="ControlTextBox2" style="border: 1px solid Blue">
                                <asp:DropDownList ID="ddlCategory" runat="server" style="border: 1px solid #90C9FC" Height="24px" Width="98%"  CssClass="drpDownListMedium" BackColor = "#90C9FC">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 30%;">

                            </td>
                        </tr>--%>
                        <tr>  
                            <td class="ControlLabel"  width="40%">
                                Summary Details
                            </td>
                            <td width="20%" class="ControlTextBox2" style="border: 1px solid Blue">
                                <asp:DropDownList ID="ddlSummary" runat="server" style="border: 1px solid #90C9FC" Height="25px" Width="100%"  CssClass="drpDownListMedium" BackColor = "#90C9FC">
                                    <asp:ListItem Value="1" Text="No"></asp:ListItem>
                                    <asp:ListItem Text="Yes" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="width: 30%;">

                            </td>
                        </tr>
                        <tr>  
                            <td class="ControlLabel"  width="40%">
                                
                            </td>
                            <td width="20%" align="center">
                                <asp:CheckBox ID="chkboxAll" runat="server" style="color: Blue" Text="Customer Receipt" Font-Names="arial" Font-Size="12px">
                                </asp:CheckBox>
                            </td>
                            <td style="width: 30%;">

                            </td>
                        </tr>
                        
                        <tr>
                            <td colspan="3">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 40%;">
                                        </td>
                                        <td style="width: 20%;">
                                            <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" CssClass="exportexl6"
                                                EnableTheming="false" />
                                        </td>
                                        <td style="width: 30%;">

                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            
        </table>
    </div>
    <br />
    <div id="divPrint" runat="server" visible="false" style="font-family: 'Trebuchet MS';
        font-size: 11px;">
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
        <tr>

        <wc:ReportGridView runat="server" BorderWidth="1" ID="gvRepor" GridLines="Both"
            AlternatingRowStyle-CssClass="even" AutoGenerateColumns="true" PrintPageSize="40"
            AllowPrintPaging="true" EmptyDataText="No Data Found" SkinID="gridview" CssClass="gridview"
            Width="100%" OnRowDataBound="gvReport_RowDataBound">
            <RowStyle CssClass="dataRow" />
            <SelectedRowStyle CssClass="SelectdataRow" />
            <AlternatingRowStyle CssClass="altRow" />
            <EmptyDataRowStyle CssClass="HeadataRow" Font-Bold="true" />
            <HeaderStyle CssClass="HeadataRow" Wrap="false" />
            <FooterStyle CssClass="dataRow" />
            <PagerStyle CssClass="footer-row allPad" VerticalAlign="Middle" HorizontalAlign="Left" />
            <PageHeaderTemplate>
                <br />
                <br />
            </PageHeaderTemplate>
            <Columns>
            </Columns>
            <PagerTemplate>
            </PagerTemplate>
            <PageFooterTemplate>
                <br />
            </PageFooterTemplate>
        </wc:ReportGridView>
        </tr>
        </table>
        <%--<asp:ObjectDataSource ID="srcCreditorDebitor" runat="server" SelectMethod="ListCustomersDealers"
            TypeName="BusinessLogic">
            <SelectParameters>
                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>--%>
    </div>
    </form>
</body>
</html>
