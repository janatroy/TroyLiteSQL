<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomerReport.aspx.cs" Inherits="CustomerReport"
    Title="Customer Report" %>

<%@ Register Src="~/SMS/UserControls/errordisplay.ascx" TagName="errordisplay" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link href="App_Themes/DefaultTheme/DefaultTheme.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center; margin: 20px 50px 0px 60px">
        <table cellpadding="2" style="border: solid 1px Silver; text-align: left" cellspacing="2"
            width="90%">
            <tr style="text-align: center">
                <td colspan="4" class="SectionHeader">
                    Customer Report
                </td>
            </tr>
            <tr>
                <td class="LMSLeftColumnColor" style="width: 25%">
                    Area *
                    <asp:CompareValidator ID="cvArea" runat="server" EnableClientScript="false" ControlToValidate="ddArea"
                        ErrorMessage="Please select Area to Generate the report" Display="Dynamic" Operator="GreaterThan"
                        ValueToCompare="0">*</asp:CompareValidator>
                    &nbsp;
                </td>
                <td style="width: 25%">
                    <asp:DropDownList ID="ddArea" runat="server" SkinID="skinDdlBox" AppendDataBoundItems="True"
                        Width="100%" DataSourceID="srcArea" DataTextField="area" DataValueField="area">
                        <asp:ListItem Value="0">--Please Select Area--</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="width: 25%" class="LMSLeftColumnColor">
                    Active Customer ? :
                </td>
                <td style="width: 25%; text-align: left">
                    <asp:CheckBox ID="CheckBox1" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="LMSLeftColumnColor" style="width: 25%">
                    Balance :
                </td>
                <td style="width: 25%" align="left" valign="top">
                    <asp:DropDownList ID="ddOper" runat="server" Style="height: 19px" SkinID="skinDdlBox"
                        Width="30%">
                        <asp:ListItem Value="&gt;=">&gt;=</asp:ListItem>
                        <asp:ListItem Value="&gt;">&gt;</asp:ListItem>
                        <asp:ListItem Value="=">=</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txtBalance" runat="server" SkinID="skinUsrTxtBox" Height="14px"
                        Width="60%"></asp:TextBox>
                </td>
                <td style="width: 25%" class="LMSLeftColumnColor">
                    Order By :
                </td>
                <td style="width: 25%">
                    <asp:DropDownList ID="ddOrderBy" runat="server" SkinID="skinDdlBox" Style="height: 19px"
                        Width="100%">
                        <asp:ListItem Value="name">Name</asp:ListItem>
                        <asp:ListItem Value="code">Code</asp:ListItem>
                        <asp:ListItem Value="doorno">Door No</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 25%">
                    &nbsp;
                </td>
                <td style="width: 25%" align="center" colspan="2" valign="middle">
                    &nbsp;
                </td>
                <td style="width: 25%">
                    <uc1:errordisplay ID="errDisp" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="width: 25%">
                    &nbsp;
                </td>
                <td align="center" style="width: 25%" colspan="2" valign="middle">
                    <asp:Button ID="lnkBtnSearchId" runat="server" Text="Generate Report" SkinID="skinButtonBig"
                        ToolTip="Click here to generate report" TabIndex="3" OnClick="lnkBtnSearchId_Click" />
                </td>
                <td style="width: 25%">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 25%">
                    &nbsp;
                </td>
                <td align="center" style="width: 25%" colspan="2" valign="middle">
                    <asp:SqlDataSource ID="srcArea" runat="server" SelectCommand="SELECT [area] FROM [AreaMaster] Order by area"
                        EnableCaching="True" ProviderName="System.Data.OleDb"></asp:SqlDataSource>
                </td>
                <td style="width: 25%">
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
