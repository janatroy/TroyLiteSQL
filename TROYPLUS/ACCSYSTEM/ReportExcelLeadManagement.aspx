<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ReportExcelLeadManagement.aspx.cs" Inherits="ReportExcelLeadManagement" Title="Lead Management Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1 {
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <center>
        <br />
        <div align="center">
            <table width="100%" style="background-color: White; border: 1px solid blue;">
                <tr>
                    <td colspan="7" class="headerPopUp">Lead Management Report
                    </td>
                </tr>
                <tr style="height: 10px">
                </tr>
                <tr>
                    <td style="width: 5%"></td>
                    <td class="ControlLabelproject" style="width: 35%">Start Date
                    </td>
                    <td class="ControlTextBox3" style="width: 45%">
                        <asp:TextBox ID="txtStrtDt"  runat="server" CssClass="textbox" Style="border: 1px solid #e7e7e7" BackColor="#e7e7e7"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
                            Format="dd/MM/yyyy"
                            PopupButtonID="ImageButton2" TargetControlID="txtStrtDt">
                        </cc1:CalendarExtender>
                    </td>
                    <td style="width: 10%" align="left">
                        <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False"
                            ImageUrl="App_Themes/NewTheme/images/cal.gif"
                            Width="20px" />
                    </td>
                    <td style="width: 30%"></td>
                    <%--<td style="width: 0%"></td>--%>
                </tr>
                <tr style="height: 2px">
                </tr>
                <tr>
                    <td style="width: 5%"></td>
                    <td class="ControlLabelproject" style="width: 35%">End Date
                    </td>
                    <td class="ControlTextBox3" style="width: 45%">
                        <asp:TextBox ID="txtEndDt" runat="server" CssClass="textbox" Style="border: 1px solid #e7e7e7" BackColor="#e7e7e7"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender123" runat="server" Enabled="True"
                            Format="dd/MM/yyyy"
                            PopupButtonID="ImageButton1" TargetControlID="txtEndDt">
                        </cc1:CalendarExtender>
                    </td>
                    <td style="width:10%" align="left">
                        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False"
                            ImageUrl="App_Themes/NewTheme/images/cal.gif"
                            Width="20px" />
                    </td>
                    <td style="width: 30%"></td>
                    <%--<td style="width: 0%"></td>--%>
                </tr>
                <tr style="height: 2px">
                </tr>
                <tr>
                    <td colspan="5">
                        <table width="100%" style="background-color: White;">
                            <tr>
                                <td colspan="5"></td>
                            </tr>
                            <tr style="height: 10px">
                            </tr>
                            <tr>
                                <td class="ControlLabelproject" style="width: 15%;">Status                              
                                </td>
                                <td class="ControlDrpBorder" style="width: 15%;">
                                   
                                    <asp:DropDownList ID="drpStatus" runat="server" AutoPostBack="true" BackColor="White" ForeColor="#0567AE" Font-Bold="true"
                                        Width="100%" AppendDataBoundItems="True" Height="26px" CssClass="drpDownListMedium">
                                        <asp:ListItem Selected="True" Value="0" style="background-color: white">Select Lead Status</asp:ListItem>
                                          <asp:ListItem Text="Open" Value="1"></asp:ListItem>
                                          <asp:ListItem Text="Closed" Value="2"></asp:ListItem>
                                    </asp:DropDownList>

                                </td>

                                <td class="ControlLabelproject" style="width: 15%;">Employee
                                </td>
                                <td class="ControlDrpBorder" style="width: 15%">
                                    <asp:DropDownList ID="drpIncharge" DataTextField="empFirstName" DataValueField="empno" runat="server" Width="100%" AutoPostBack="true" BackColor="White" ForeColor="#0567AE" Font-Bold="true" Height="26px" CssClass="drpDownListMedium"
                                        AppendDataBoundItems="True">
                                        <asp:ListItem Selected="True" Value="0" style="background-color: White">Select Employee Responsible</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 10%"></td>
                            </tr>
                            <tr style="height: 2px">
                            </tr>
                            <tr>
                                <td class="ControlLabelproject" style="width: 15%;">Location
                                </td>
                                <td class="ControlDrpBorder" style="width: 15%">
                                    <asp:DropDownList ID="drpArea" runat="server" DataTextField="TextValue" DataValueField="ID" Width="100%" AutoPostBack="true" BackColor="White" ForeColor="#0567AE" Font-Bold="true" Height="26px" CssClass="drpDownListMedium"
                                        AppendDataBoundItems="True">
                                        <asp:ListItem Selected="True" Value="0" style="background-color: white">Select Location</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td class="ControlLabelproject" style="width: 15%;">Category
                                </td>
                                <td class="ControlDrpBorder" style="width: 15%">
                                    <asp:DropDownList ID="drpCategory" runat="server" DataTextField="TextValue" DataValueField="ID" Width="100%" AutoPostBack="true" BackColor="White" ForeColor="#0567AE" Font-Bold="true" Height="26px" CssClass="drpDownListMedium"
                                        AppendDataBoundItems="True">
                                        <asp:ListItem Selected="True" Value="0" style="background-color: white">Select Category</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 10%"></td>
                            </tr>
                            <tr style="height: 2px">
                            </tr>
                            <tr>
                                <td class="ControlLabelproject" style="width: 15%;">Lead Activity
                                </td>
                                <td class="ControlDrpBorder" style="width: 15%">
                                    <asp:DropDownList ID="drpactivityName" runat="server" DataTextField="TextValue" DataValueField="ID" Width="100%" AutoPostBack="true" BackColor="White" ForeColor="#0567AE" Font-Bold="true" Height="26px" CssClass="drpDownListMedium"
                                        AppendDataBoundItems="True">
                                        <asp:ListItem Selected="True" Value="0" style="background-color: white">Select Lead Activity</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td class="ControlLabelproject" style="width: 15%;">Follow-up Activity
                                </td>
                                <td class="ControlDrpBorder" style="width: 15%">
                                    <asp:DropDownList ID="drpnxtActivity" runat="server" DataTextField="TextValue" DataValueField="ID" Width="100%" AutoPostBack="true" BackColor="White" ForeColor="#0567AE" Font-Bold="true" Height="26px" CssClass="drpDownListMedium"
                                        AppendDataBoundItems="True">
                                        <asp:ListItem Selected="True" Value="0" style="background-color: white">Select Follow-up Activity</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 10%"></td>
                            </tr>
                            <tr style="height: 2px">
                            </tr>
                            <tr>
                                <td class="ControlLabelproject" style="width: 15%;">Additional Information3
                                </td>
                                <td class="ControlDrpBorder" style="width: 15%">
                                    <asp:DropDownList ID="drpInformation3" runat="server" Width="100%" DataTextField="TextValue" DataValueField="ID" AutoPostBack="true" BackColor="White" ForeColor="#0567AE" Font-Bold="true" Height="26px" CssClass="drpDownListMedium"
                                        AppendDataBoundItems="True">
                                        <asp:ListItem Selected="True" Value="0" style="background-color: white">Select Additional Information 3</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td class="ControlLabelproject" style="width: 15%;">Additional Information4
                                </td>
                                <td class="ControlDrpBorder" style="width: 15%">
                                    <asp:DropDownList ID="drpInformation4" runat="server" Width="100%" DataTextField="TextValue" DataValueField="ID" AutoPostBack="true" BackColor="White" ForeColor="#0567AE" Font-Bold="true" Height="26px" CssClass="drpDownListMedium"
                                        AppendDataBoundItems="True">
                                        <asp:ListItem Selected="True" Value="0" style="background-color: white">Select Additional Information 4</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 10%"></td>
                            </tr>
                             <tr style="height: 7px">
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 25%"></td>
                                            <td style="width: 25%">
                                                <asp:Button ID="btnReport" runat="server" CssClass="NewReport6" OnClick="btnReport_Click" EnableTheming="false" />
                                            </td>                                           
                                            <td style="width: 25%">
                                                <asp:Button ID="btnxls" runat="server" OnClick="btnxls_Click" CssClass="exportexl6" EnableTheming="false" />
                                            </td>
                                            <td style="width: 25%">
                                                <asp:Button ID="btnData" runat="server" CssClass="generatebutton" EnableTheming="false" Visible="False" />
                                            </td>                                            
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr style="height: 7px">
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

        </div>

   <%--     <table style="width: 100%">
            <tr style="height: 6px">
            </tr>
            <tr>
                <td style="width: 100%">
                    <asp:GridView ID="GridCust" runat="server" BackColor="White" BorderColor="blue" Width="100%" OnRowCreated="GridCust_RowCreated" OnPageIndexChanging="GridCust_PageIndexChanging"
                        BorderStyle="Solid" BorderWidth="1px" CellPadding="3" CellSpacing="1" Font-Size="Small" Font-Names="Trebuchet MS" CssClass="someClass"
                        AutoGenerateColumns="False" OnRowDataBound="GridCust_RowDataBound" AllowPaging="True" HeaderStyle-ForeColor="Black">
                        <Columns>                          
                            <asp:TemplateField HeaderText="Product Name" HeaderStyle-BorderColor="Blue">
                                <ItemTemplate>
                                    <asp:Label ID="lblLedger" runat="server" Text='<%# Eval("ProductName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Code" HeaderStyle-BorderColor="Blue">
                                <ItemTemplate>
                                    <asp:Label ID="lblLed" runat="server" Text='<%# Eval("ItemCode") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Model" HeaderStyle-BorderColor="Blue">
                                <ItemTemplate>
                                    <asp:Label ID="lblLedg" runat="server" Text='<%# Eval("Model") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Brand" HeaderStyle-BorderColor="Blue">
                                <ItemTemplate>
                                    <asp:Label ID="lblLedge" runat="server" Text='<%# Eval("Productdesc") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MRP" HeaderStyle-BorderColor="Blue">
                                <ItemTemplate>
                                    <asp:Label ID="lblMRP" runat="server" Text='<%# Eval("MRP") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MRP Date" HeaderStyle-BorderColor="Blue">
                                <ItemTemplate>
                                    <asp:Label ID="lblMRPDate" runat="server" Text='<%# Eval("MRPDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="NLC" HeaderStyle-BorderColor="Blue">
                                <ItemTemplate>
                                    <asp:Label ID="lblNLC" runat="server" Text='<%# Eval("NLC") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="NLC Date" HeaderStyle-BorderColor="Blue">
                                <ItemTemplate>
                                    <asp:Label ID="lblNLCDate" runat="server" Text='<%# Eval("NLCDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DP" HeaderStyle-BorderColor="Blue">
                                <ItemTemplate>
                                    <asp:Label ID="lblDP" runat="server" Text='<%# Eval("DP") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DP Date" HeaderStyle-BorderColor="Blue">
                                <ItemTemplate>
                                    <asp:Label ID="lblDPdate" runat="server" Text='<%# Eval("DPdate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerTemplate>
                            <table style="border-color: white">
                                <tr style="border-color: white">
                                    <td style="border-color: white">Goto Page
                                    </td>
                                    <td style="border-color: white">
                                        <asp:DropDownList ID="ddlPageSelector" OnSelectedIndexChanged="ddlPageSelector_SelectedIndexChanged" runat="server" AutoPostBack="true" Style="border: 1px solid blue" Width="65px">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="border-color: white; width: 5px"></td>
                                    <td style="border-color: white">
                                        <asp:Button Text="" CommandName="Page" CommandArgument="First" runat="server" CssClass="NewFirst" EnableTheming="false" Width="22px" Height="18px"
                                            ID="btnFirst" />
                                    </td>
                                    <td style="border-color: white">
                                        <asp:Button Text="" CommandName="Page" CommandArgument="Prev" runat="server" CssClass="NewPrev" EnableTheming="false" Width="22px" Height="18px"
                                            ID="btnPrevious" />
                                    </td>
                                    <td style="border-color: white">
                                        <asp:Button Text="" CommandName="Page" CommandArgument="Next" runat="server" CssClass="NewNext" EnableTheming="false" Width="22px" Height="18px"
                                            ID="btnNext" />
                                    </td>
                                    <td style="border-color: white">
                                        <asp:Button Text="" CommandName="Page" CommandArgument="Last" runat="server" CssClass="NewLast" EnableTheming="false" Width="22px" Height="18px"
                                            ID="btnLast" />
                                    </td>
                                </tr>
                            </table>
                        </PagerTemplate>
                        <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                        <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="black" />
                    </asp:GridView>
                </td>

            </tr>
        </table>--%>

    </center>
</asp:Content>
