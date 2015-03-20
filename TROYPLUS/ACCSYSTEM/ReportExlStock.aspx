<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportExlStock.aspx.cs"
    Inherits="ReportExlStock" %>

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
            <table cellpadding="2" cellspacing="2" width="100%" border="0" style="border: 1px solid blue; text-align: left">
                <tr>
                    <td colspan="3" class="headerPopUp">Stock Comprehency Report
                    </td>
                </tr>

                <tr style="height: 5px">
                </tr>
                <tr>
                    <td>
                        <table cellpadding="3" cellspacing="1" width="100%">
                            <tr style="height: 5px">
                            </tr>
                            <tr>
                                <td colspan="5">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 8%"></td>
                                            <td style="width: 17%;" class="ControlLabel2">As On Date *
                                            </td>
                                            <td class="ControlTextBox3" style="width: 20%;">
                                                <asp:TextBox ID="txtStartDate" Enabled="false" CssClass="cssTextBox" MaxLength="10"
                                                    runat="server" />
                                                <cc1:CalendarExtender ID="calStartDate" runat="server" Enabled="True"
                                                    Format="dd/MM/yyyy"
                                                    PopupButtonID="btnStartDate" TargetControlID="txtStartDate">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td style="width: 3%;">
                                                <asp:ImageButton ID="btnStartDate" runat="server" CausesValidation="False"
                                                    ImageUrl="App_Themes/NewTheme/images/cal.gif"
                                                    Width="20px" />
                                            </td>
                                            <td style="width: 5%;"></td>
                                            <td style="width: 17%;"></td>
                                        </tr>
                                        <tr style="height: 2px;" />
                                        <tr>
                                            <td style="width: 8%"></td>
                                            <td style="width: 17%;" class="ControlLabel2">Option
                                            </td>
                                            <td class="ControlTextBox3" style="width: 20%;">
                                                <asp:RadioButtonList ID="opnradio" runat="server"
                                                    BackColor="#e7e7e7" Font-Size="Small" RepeatDirection="Horizontal">
                                                    <asp:ListItem Selected="True">GroupBy</asp:ListItem>
                                                    <asp:ListItem>Normal</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td style="width: 3%;"></td>
                                            <td style="width: 5%;"></td>
                                            <td style="width: 17%;"></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 8%"></td>
                                            <td style="width: 17%;"></td>
                                            <td style="width: 20%;">
                                                <div style="overflow-y: scroll; height: 150px; width: 300px" runat="server">
                                                    <asp:Label ID="lblbranch" CssClass="ControlLabelproject" runat="server" Text="Select Branch:"></asp:Label>

                                                    <asp:CheckBoxList ID="lstBranch" runat="server" AutoPostBack="true" SelectionMode="Multiple" AppendDataBoundItems="true" DataTextField="BranchName" DataValueField="Branchcode" OnSelectedIndexChanged="lstBranch_SelectedIndexChanged">
                                                        <asp:ListItem Text="All" Value="0" />
                                                    </asp:CheckBoxList>
                                                </div>
                                            </td>
                                            <td style="width: 3%;"></td>
                                            <td style="width: 5%;"></td>
                                            <td style="width: 17%;"></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 8%"></td>
                                            <td style="width: 17%;"></td>
                                            <td style="width: 20%;"></td>
                                            <td style="width: 3%;"></td>
                                            <td style="width: 5%;"></td>
                                            <td style="width: 17%;"></td>
                                        </tr>
                                    </table>
                                </td>

                            </tr>
                            <tr style="height: 12px">
                            </tr>
                            <tr>
                                <td style="width: 5%;"></td>
                                <td align="left" style="width: 52%" class="ControlTextBox3">
                                    <asp:RadioButtonList ID="chkoption" runat="server"
                                        BackColor="#e7e7e7" Font-Size="Small">
                                        <asp:ListItem Selected="True">Category Wise</asp:ListItem>
                                        <asp:ListItem>Brand Wise</asp:ListItem>
                                       <%-- <asp:ListItem>Product Wise</asp:ListItem>--%>
                                        <%--<asp:ListItem>Brand / Product Wise</asp:ListItem>--%>
                                        <asp:ListItem>Category / Brand Wise</asp:ListItem>
                                       <%-- <asp:ListItem>Category / Product Wise</asp:ListItem>--%>
                                       <%-- <asp:ListItem>Product / Model Wise</asp:ListItem>--%>
                                         <asp:ListItem>Model Wise</asp:ListItem>
                                        <asp:ListItem>Brand / Model Wise</asp:ListItem>
                                        <%--<asp:ListItem>Brand / Product / Model Wise</asp:ListItem>--%>
                                       <%-- <asp:ListItem>Category / Brand / Product Wise</asp:ListItem>--%>
                                    </asp:RadioButtonList>
                                </td>
                                <td class="ControlTextBox3" style="width: 14%;">
                                    <asp:CheckBox ID="chkboxQty" runat="server" Text="Qty" />
                                    <asp:CheckBox ID="chkboxVal" runat="server" Text="Value" />
                                    <asp:CheckBox ID="chkboxper" runat="server" Text="%" />
                                    <%--<asp:CheckBoxList ID="chkitem" runat="server" Font-Size="Small"
                                        BackColor="#90C9FC">
                                        <asp:ListItem Selected="True">Qty</asp:ListItem>
                                        <asp:ListItem>Value</asp:ListItem>
                                        <asp:ListItem>Percentage</asp:ListItem>
                                </asp:CheckBoxList>--%>
                                </td>
                                <td class="ControlTextBox3" style="width: 14%;">
                                    <asp:CheckBox ID="chkMRP" runat="server" Text="MRP" Visible="false" />
                                    <asp:CheckBox ID="chkNLC" runat="server" Text="NLC" Visible="false" />
                                    <asp:CheckBox ID="chkDP" runat="server" Text="DP" Visible="false" />
                                    <%--<asp:CheckBoxList ID="chkvalue" runat="server"  Font-Size="Small"
                                        BackColor="#90C9FC">
                                        <asp:ListItem>MRP</asp:ListItem>
                                        <asp:ListItem>NLC</asp:ListItem>
                                        <asp:ListItem>DP</asp:ListItem>
                                </asp:CheckBoxList>--%>
                                    <div style="overflow-y: scroll; height: 150px; width: 300px">
                                        <asp:Label ID="Label1" CssClass="ControlLabelproject" runat="server" Text="Select Pricelist:"></asp:Label>

                                        <asp:CheckBoxList ID="lstPricelist" AutoPostBack="true" runat="server" SelectionMode="Multiple" AppendDataBoundItems="true" DataTextField="PriceName" DataValueField="PriceName" OnSelectedIndexChanged="lstPricelist_SelectedIndexChanged">
                                            <asp:ListItem Text="All" Value="0" />
                                        </asp:CheckBoxList>
                                    </div>
                                </td>
                                <td style="width: 5%;"></td>
                            </tr>
                            <tr style="height: 10px">
                            </tr>
                            <tr>
                                <td colspan="5">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 3%;"></td>
                                            <td style="width: 35%;"></td>
                                            <td style="width: 20%;">
                                                <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" CssClass="exportexl6"
                                                    EnableTheming="false" />
                                            </td>
                                            <td style="width: 40%;" align="left"></td>
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
        <div id="divPrint" runat="server" visible="false" style="font-family: 'Trebuchet MS'; font-size: 11px;">
            <table width="700px" border="0" style="font-family: Trebuchet MS; font-size: 14px;">
                <tr>
                    <td width="140px" align="left">TIN#:
                    <asp:Label ID="lblTNGST" runat="server"></asp:Label>
                    </td>
                    <td align="center" width="420px" style="font-size: 20px;">
                        <asp:Label ID="lblCompany" runat="server"></asp:Label>
                    </td>
                    <td width="140px" align="left">Ph:
                    <asp:Label ID="lblPhone" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left">GST#:
                    <asp:Label ID="lblGSTno" runat="server"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:Label ID="lblAddress" runat="server"></asp:Label>
                    </td>
                    <td align="left">Date:
                    <asp:Label ID="lblBillDate" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                    <td align="center">
                        <asp:Label ID="lblCity" runat="server" />
                        -
                    <asp:Label ID="lblPincode" runat="server"></asp:Label>
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                    <td align="center">
                        <asp:Label ID="lblState" runat="server"> </asp:Label>
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>

            </table>
        </div>
    </form>
</body>
</html>
