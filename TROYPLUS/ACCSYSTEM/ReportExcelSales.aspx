<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ReportExcelSales.aspx.cs" Inherits="Sales" Title="Sales Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <center>
        <table>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td class="style1">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td colspan="4" align="center">
                    <strong>
                        <asp:Label ID="Label8" runat="server" Text="Sales Report" CssClass="td"></asp:Label></strong>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td class="style1">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td class="style1">
                    <asp:Label ID="Label7" runat="server" Text="StartDate" CssClass="label"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtStrtDt" runat="server" CssClass="textbox"></asp:TextBox>
                    <%--<asp:Calendar ID="txtStrtDt_CalendarExtender1" runat="server" Enabled="true"
                                Format="MM/dd/yyyy" TargetControlID="txtStrtDt">--%>
                    </asp:Calendar>
                    <%--                            <asp:MaskedEditExtender ID="txtStrtDt_MaskedEditExtender1" runat="server" AcceptNegative="Left"
                                ErrorTooltipEnabled="true" InputDirection="RightToLeft" Mask="99/99/9999" MaskType="Date"
                                MessageValidatorTip="true" TargetControlID="txtStrtDt">
                            </asp:MaskedEditExtender>--%>
                </td>
                <td style="text-align: right">
                    <asp:Label ID="Label9" runat="server" Text="EndDate" CssClass="label"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtEndDt" runat="server" CssClass="textbox"></asp:TextBox>
                    <%--<asp:Calendar ID="txtEndDt_CalendarExtender2" runat="server" Enabled="true"
                                Format="MM/dd/yyyy" TargetControlID="txtEndDt">
                            </asp:Calendar>--%>
                    <%--                            <asp:MaskedEditExtender ID="txtEndDt_MaskedEditExtender2" runat="server" AcceptNegative="Left"
                                ErrorTooltipEnabled="true" InputDirection="RightToLeft" Mask="99/99/9999" MaskType="Date"
                                MessageValidatorTip="true" TargetControlID="txtEndDt">
                            </asp:MaskedEditExtender>--%>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td class="style1">
                    &nbsp;
                </td>
                <td style="text-align: left">
                    &nbsp;
                </td>
                <td style="text-align: right">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td class="style1">
                    <asp:Label ID="Label10" runat="server" Text="PurChase Return" CssClass="label"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:RadioButtonList ID="rblPurchseRtn" runat="server" CssClass="label" RepeatDirection="Horizontal">
                        <asp:ListItem>Yes</asp:ListItem>
                        <asp:ListItem Selected="True">No</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>
                    <asp:Label ID="Label11" runat="server" Text="Internel Transfer" CssClass="label"></asp:Label>
                </td>
                <td>
                    <asp:RadioButtonList ID="rblIntrnlTrns" runat="server" CssClass="label" RepeatDirection="Horizontal">
                        <asp:ListItem>Yes</asp:ListItem>
                        <asp:ListItem Selected="True">No</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td class="style1">
                    &nbsp;
                </td>
                <td style="text-align: left">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td class="style1">
                    <asp:Label ID="Label12" runat="server" Text="Customer Id" CssClass="label"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="ddlCustid" runat="server" CssClass="textbox">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="Label14" runat="server" Text="Category Id" CssClass="label"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlCatgryid" runat="server" CssClass="textbox">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td class="style1">
                    &nbsp;
                </td>
                <td style="text-align: left">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td class="style1">
                    <asp:Label ID="Label17" runat="server" Text="Product Code" CssClass="label"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="ddlPrdctCode" runat="server" CssClass="textbox">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="Label16" runat="server" Text="Brand" CssClass="label"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlBrand" runat="server" CssClass="textbox">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td class="style1">
                    &nbsp;
                </td>
                <td style="text-align: left">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td class="style1">
                    <asp:Label ID="Label15" runat="server" Text="Product Name" CssClass="label"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="ddlPrdctNme" runat="server" CssClass="textbox">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="Label13" runat="server" Text="Payment Mode" CssClass="label"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlPayMode" runat="server" CssClass="textbox">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td class="style1">
                    &nbsp;
                </td>
                <td style="text-align: left">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td class="style1">
                    <asp:Label ID="Label18" runat="server" Text="First Level" CssClass="label"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="ddlFirstLvl" runat="server" CssClass="textbox">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="Label19" runat="server" Text="Second Level" CssClass="label"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlSecondLvl" runat="server" CssClass="textbox">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td class="style1">
                    &nbsp;
                </td>
                <td style="text-align: left">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td class="style1">
                    <asp:Label ID="Label20" runat="server" Text="Third Level" CssClass="label"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="ddlThirdLvl" runat="server" CssClass="textbox">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="Label21" runat="server" Text="Fourth Level" CssClass="label"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlFourthLvl" runat="server" CssClass="textbox">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td class="style1">
                    &nbsp;
                </td>
                <td style="text-align: left">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td class="style1">
                    <asp:Label ID="Label22" runat="server" Text="Fifth Level" CssClass="label"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="ddlFifthLvl" runat="server" CssClass="textbox">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="Label23" runat="server" Text="Sixth Level" CssClass="label"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlSixthLvl" runat="server" CssClass="textbox">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td class="style1">
                    &nbsp;
                </td>
                <td style="text-align: left">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td class="style1">
                    <asp:Label ID="Label24" runat="server" Text="Seventh Level" CssClass="label"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="ddlSeventhLvl" runat="server" CssClass="textbox">
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td colspan="4" align="center">
                    <asp:Button ID="btnData" runat="server" OnClick="btnData_Click" Text="GridData" />
                    &nbsp;
                    <asp:Button ID="btnxls" runat="server" OnClick="btnxls_Click" Text="Export to Excel" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td colspan="4">
                    &nbsp; &nbsp; &nbsp;
                </td>
            </tr>
        </table>
        <asp:GridView ID="GridSales" runat="server" BackColor="White" BorderColor="White"
            BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" GridLines="None">
            <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
            <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
            <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
        </asp:GridView>
    </center>
</asp:Content>
