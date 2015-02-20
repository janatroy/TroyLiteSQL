<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CashDetailsReport.aspx.cs"
    Inherits="CashDetailsReport" %>

<%@ Register Src="~/SMS/UserControls/errordisplay.ascx" TagName="errordisplay" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cash Details Report</title>
    <link href="App_Themes/DefaultTheme/DefaultTheme.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 55%;
        }
        .style2
        {
            background-color: #E6E6E6;
            font: 12px Arial ,sans-serif;
            vertical-align: middle;
            width: 35%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <br />
    <div style="text-align: center; margin: 20px 50px 20px 60px">
        <table cellpadding="2" style="border: solid 1px Silver" cellspacing="2" width="90%">
            <tr>
                <td class="SectionHeader" colspan="3">
                    Cash Details Report
                </td>
            </tr>
            <tr>
                <td align="left" class="style2">
                    Area:
                </td>
                <td align="left" class="style1">
                    <asp:DropDownList ID="ddArea" runat="server" AppendDataBoundItems="True" DataSourceID="srcArea"
                        DataTextField="area" DataValueField="area" Width="100%" SkinID="skinDdlBox">
                        <asp:ListItem Value="0"> -- All -- </asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="left" width="20%">
                </td>
            </tr>
            <tr>
                <td align="left" class="style2">
                    Start Date:
                </td>
                <td align="left" class="style1">
                    <asp:TextBox ID="txtStartDate" runat="server" Width="70%" SkinID="skinTxtBox" />
                </td>
                <td align="left" width="20%">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtStartDate"
                        Display="None" ErrorMessage="Please Enter Start Date"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="left" class="style2">
                    End Date:
                </td>
                <td align="left" class="style1">
                    <asp:TextBox ID="txtEndDate" runat="server" Width="70%" SkinID="skinTxtBox" />
                </td>
                <td align="left" width="20%">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEndDate"
                        Display="None" ErrorMessage="Please Enter The End Date"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtStartDate"
                        ControlToValidate="txtEndDate" Display="None" ErrorMessage="Start Date Should Be Less Than the End Date"
                        Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click"
                        Text="Cash Details Report" SkinID="skinButtonBig" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:SqlDataSource ID="srcArea" runat="server" SelectCommand="SELECT [area] FROM [AreaMaster]"
                        ProviderName="System.Data.OleDb"></asp:SqlDataSource>
                </td>
            </tr>
        </table>
        <asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="true" ShowSummary="false" />
    </div>
    </form>
</body>
</html>
