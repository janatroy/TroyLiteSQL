<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ReportExcelDebitnote.aspx.cs" Inherits="Debitnote" Title="Debitnote Page" %>

    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <center>
    <br />
        <div>
            <table width="450px" style=" background-color:White; border:1px solid blue;">
                <%--<tr class="subHeadFont">
                    <td colspan="4">
                        <table>
                            <tr>
                                <td>
                                    Debit Note Report
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>--%>
                <tr>
                    <td colspan="3" class="subHeadFont">
                        Debit Note Report
                    </td>
                </tr>
                <tr style="height:5px">

                </tr>
            
            <tr>
                <td class="ControlLabel" style="width:35%">
                    <asp:Label ID="Label7" runat="server" Text="StartDate"></asp:Label>
                </td>
                <td style="width:30%" class="ControlTextBox3">
                    <asp:TextBox ID="txtStrtDt" runat="server" CssClass="textbox" style="border: 1px solid #90c9fc"  BackColor = "#90c9fc" TabIndex="1"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" 
                            Format="dd/MM/yyyy"
                            PopupButtonID="ImageButton1" TargetControlID="txtStrtDt">
                        </cc1:CalendarExtender>
                    <%--                            <asp:MaskedEditExtender ID="txtStrtDt_MaskedEditExtender1" runat="server" AcceptNegative="Left"
                                ErrorTooltipEnabled="true" InputDirection="RightToLeft" Mask="99/99/9999" MaskType="Date"
                                MessageValidatorTip="true" TargetControlID="txtStrtDt">
                            </asp:MaskedEditExtender>--%>
                </td>
                <td align="left">
                    <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" 
                            ImageUrl="App_Themes/NewTheme/images/cal.gif" 
                            Width="20px" />
                </td>
                
            </tr>
            <tr>
                <td class="ControlLabel" style="width:35%">
                    <asp:Label ID="Label9" runat="server" Text="EndDate"></asp:Label>
                </td>
                <td style="width:30%" class="ControlTextBox3">
                    <asp:TextBox ID="txtEndDt" runat="server" CssClass="textbox" style="border: 1px solid #90c9fc" BackColor = "#90c9fc" TabIndex="2"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender123" runat="server" Enabled="True" 
                            Format="dd/MM/yyyy"
                            PopupButtonID="ImageButton2" TargetControlID="txtEndDt">
                        </cc1:CalendarExtender>
                    <%--                            <asp:MaskedEditExtender ID="txtEndDt_MaskedEditExtender1" runat="server" AcceptNegative="Left"
                                ErrorTooltipEnabled="true" InputDirection="RightToLeft" Mask="99/99/9999" MaskType="Date"
                                MessageValidatorTip="true" TargetControlID="txtEndDt">
                            </asp:MaskedEditExtender>--%>
                </td>
                <td align="left">
                    <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" 
                            ImageUrl="App_Themes/NewTheme/images/cal.gif" 
                            Width="20px" />
                </td>
            </tr>
            <tr style="height:6px">

            </tr>
            <tr>
                <td colspan="4">
                    <table width="100%">
                        <tr>
                            <td style="width:25%">
                    
                            </td>
                            <td style="width:15%">
                                <asp:Button ID="GridDebit" runat="server" OnClick="GridDebit_Click"  CssClass="generatebutton" EnableTheming="false"
                                    TabIndex="3" Visible="False" />
                            </td>
                            <td style="width:20%">
                                <asp:Button ID="btnxls" runat="server" OnClick="btnxls_Click"   CssClass="exportexl6" EnableTheming="false"
                                    TabIndex="4" />
                            </td>
                            <td style="width:40%">
                    
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
     </div>
     <div style="width: 100%">
        <table width="100%">
            <tr style="height:20px">

            </tr>
            <tr>
                <td>
                    <asp:GridView ID="GridDebits" runat="server" BackColor="White" BorderColor="Blue" OnPageIndexChanging="GridDebits_PageIndexChanging" OnRowCreated="GridDebits_RowCreated"
                        BorderStyle="Solid" BorderWidth="1px" Font-Size="Small" CssClass="someClass"
                        AutoGenerateColumns="False" Width="100%" OnRowDataBound="GridDebits_RowDataBound" ShowFooter="True">
                        <Columns>
                            <asp:TemplateField HeaderText="Ref. No." HeaderStyle-BorderColor="Blue" HeaderStyle-ForeColor="Black">
                                <ItemTemplate>
                                    <asp:Label ID="lblRefno" runat="server" Text='<%# Eval("RefNo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="NoteDate" HeaderStyle-BorderColor="Blue" HeaderStyle-ForeColor="Black">
                                <ItemTemplate>
                                    <asp:Label ID="lblDt" runat="server" Text='<%# Eval("NoteDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Debitor" HeaderStyle-BorderColor="Blue" HeaderStyle-ForeColor="Black">
                                <ItemTemplate>
                                    <asp:Label ID="lblCredit" runat="server" Text='<%# Eval("Debitor") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Total :
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount" HeaderStyle-BorderColor="Blue" HeaderStyle-ForeColor="Black">
                                <ItemTemplate>
                                    <asp:Label ID="lblAmt" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblTotalAmt" runat="server"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Narration" HeaderStyle-BorderColor="Blue" HeaderStyle-ForeColor="Black">
                                <ItemTemplate>
                                    <asp:Label ID="lblNarration" runat="server" Text='<%# Eval("Narration") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerTemplate>
                            <table style=" border-color:white">
                                <tr style=" border-color:white">
                                    <td style=" border-color:white">
                                        Goto Page
                                    </td>
                                    <td style=" border-color:white">
                                        <asp:DropDownList ID="ddlPageSelector" OnSelectedIndexChanged="ddlPageSelector_SelectedIndexChanged" runat="server" AutoPostBack="true" style="border:1px solid blue"  Width="65px">
                                        </asp:DropDownList>
                                    </td>
                                    <td style=" border-color:white;Width:5px">
                                            
                                    </td>
                                    <td style=" border-color:white">
                                        <asp:Button Text="" CommandName="Page" CommandArgument="First" runat="server" CssClass="NewFirst" EnableTheming="false" Width="22px" Height="18px"
                                            ID="btnFirst" />
                                    </td>
                                    <td style=" border-color:white">
                                        <asp:Button Text="" CommandName="Page" CommandArgument="Prev" runat="server" CssClass="NewPrev" EnableTheming="false" Width="22px" Height="18px"
                                            ID="btnPrevious" />
                                    </td>
                                    <td style=" border-color:white">
                                        <asp:Button Text="" CommandName="Page" CommandArgument="Next" runat="server" CssClass="NewNext" EnableTheming="false" Width="22px" Height="18px"
                                            ID="btnNext" />
                                    </td>
                                    <td style=" border-color:white">
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
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
                    </asp:GridView>
                    
                </td>
            </tr>
        </table>
        </div>
    </center>
</asp:Content>
