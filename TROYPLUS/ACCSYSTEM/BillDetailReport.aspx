<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BillDetailReport.aspx.cs"
    Inherits="BillDetailReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bill Details Report</title>
    <link href="App_Themes/DefaultTheme/DefaultTheme.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <br />
    <div style="background: white; margin: 20px 50px 20px 60px; text-align: center">
        <table cellpadding="2" style="border: solid 1px Silver" cellspacing="2" width="100%">
            <tr>
                <td colspan="3" class="SectionHeader">
                    Bill Details Report
                </td>
            </tr>
            <tr>
                <td align="left" width="15%" class="LMSLeftColumnColor">
                    Area:
                </td>
                <td align="left" width="20%">
                    <asp:DropDownList ID="drpArea" runat="server" AppendDataBoundItems="True" DataSourceID="srcArea"
                        DataTextField="area" DataValueField="area" Width="100%" SkinID="skinDdlBox">
                        <asp:ListItem Value="0"> -- All Areas -- </asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="left" width="50%">
                </td>
            </tr>
            <tr>
                <td align="left" width="15%" class="LMSLeftColumnColor">
                    Start Date:
                </td>
                <td align="left" width="20%">
                    <asp:TextBox ID="txtStartDate" runat="server" SkinID="skinTxtBox" />
                </td>
                <td align="left" width="50%">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtStartDate"
                        Display="None" ErrorMessage="Please Enter Start Date"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="left" width="15%" class="LMSLeftColumnColor">
                    End Date:
                </td>
                <td align="left" width="20%">
                    <asp:TextBox ID="txtEndDate" runat="server" SkinID="skinTxtBox" />
                </td>
                <td align="left" width="50%">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEndDate"
                        Display="None" ErrorMessage="Please Enter The End Date"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtStartDate"
                        ControlToValidate="txtEndDate" Display="None" ErrorMessage="Start Date Should Be Less Than the End Date"
                        Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td align="left" width="30%">
                    <br />
                    <br />
                    <br />
                </td>
                <td align="left" width="20%">
                    <asp:Button ID="btnReport" runat="server" SkinID="skinButtonBig" OnClick="btnReport_Click"
                        Text="Generate Report" CssClass="button" Width="150px" />
                </td>
                <td align="left" width="50%">
                    &nbsp;
                </td>
            </tr>
        </table>
        <asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="true" ShowSummary="false" />
        <table style="border: solid 0px Silver" width="100%">
            <tr>
                <td colspan="3" style="vertical-align: middle;">
                    <asp:SqlDataSource ID="srcArea" runat="server" SelectCommand="SELECT [area] FROM [AreaMaster]"
                        ProviderName="System.Data.OleDb"></asp:SqlDataSource>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
