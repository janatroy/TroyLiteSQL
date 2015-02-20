<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ReportExcelPayments.aspx.cs" Inherits="Payments" Title="Payments Page" %>

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
        <div align="center" style="width: 90%">
            <table width="570px" style=" background-color:White; border:1px solid blue;">
                <%--<tr class="subHeadFont">
                    <td colspan="6">
                        <table>
                            <tr>
                                <td>
                                    Payment Report
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>--%>
                <tr >
                    <td colspan="6" class="headerPopUp">
                        Payment Report
                    </td>
                </tr>
                <tr style="height:5px">

                </tr>
            
            <tr>
                <td  class="ControlLabel2" style="width:20%">
                    <asp:Label ID="Label7" runat="server" Text="StartDate"></asp:Label>
                </td>
                <td  class="ControlTextBox3" style="width:25%">
                    <asp:TextBox ID="txtStrtDt" runat="server" CssClass="textbox" style="border: 1px solid #90c9fc"  BackColor = "#90c9fc"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" 
                            Format="dd/MM/yyyy"
                            PopupButtonID="ImageButton2" TargetControlID="txtStrtDt">
                        </cc1:CalendarExtender>
                    
                    <%--                            <asp:MaskedEditExtender ID="txtStrtDt_MaskedEditExtender1" runat="server" AcceptNegative="Left"
                                ErrorTooltipEnabled="true" InputDirection="RightToLeft" Mask="99/99/9999" MaskType="Date"
                                MessageValidatorTip="true" TargetControlID="txtStrtDt">
                            </asp:MaskedEditExtender>--%>
                </td>
                <td style="width:3%" align="left">
                                    <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" 
                            ImageUrl="App_Themes/NewTheme/images/cal.gif" 
                            Width="20px" />
                                </td>
                <td  class="ControlLabel2" style="width:15%">
                    
                    <asp:Label ID="Label9" runat="server" Text="EndDate"></asp:Label>
                </td>
                <td  class="ControlTextBox3" style="width:25%">
                    <asp:TextBox ID="txtEndDt" runat="server" CssClass="textbox" style="border: 1px solid #90c9fc"  BackColor = "#90c9fc"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender123" runat="server" Enabled="True" 
                            Format="dd/MM/yyyy"
                            PopupButtonID="ImageButton1" TargetControlID="txtEndDt">
                        </cc1:CalendarExtender>

                    <%--                            <asp:MaskedEditExtender ID="txtEndDt_MaskedEditExtender1" runat="server" AcceptNegative="Left"
                                ErrorTooltipEnabled="true" InputDirection="RightToLeft" Mask="99/99/9999" MaskType="Date"
                                MessageValidatorTip="true" TargetControlID="txtEndDt">
                            </asp:MaskedEditExtender>--%>
                </td>
                <td style="width:4%" align="left">
                    <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" 
                            ImageUrl="App_Themes/NewTheme/images/cal.gif" 
                            Width="20px" />
                </td>
            </tr>
            
            <tr>
                <td  class="ControlLabel2" style="width:20%">
                    <asp:Label ID="Label10" runat="server" Text="Categories"></asp:Label>
                </td>
                <td  class="ControlDrpBorder" style="width:25%">
                    <asp:DropDownList ID="ddlCategory" runat="server" Width="100%" AutoPostBack="True"  CssClass="drpDownListMedium" Height="30px" style="border: 1px solid #90c9fc"  BackColor = "#90c9fc"
                        OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td style="width:3%" align="left">
                                    
                                </td>
                <td  class="ControlLabel2" style="width:15%">
                    <asp:Label ID="Label15" runat="server" Text="SubCategories"></asp:Label>
                </td>
                <td  class="ControlDrpBorder" style="width:25%">
                    <asp:DropDownList ID="ddlsubcategory" runat="server" Width="100%" Height="30px" CssClass="drpDownListMedium"  style="border: 1px solid #90c9fc"  BackColor = "#90c9fc">
                    </asp:DropDownList>
                </td>
                <td style="width:4%">
                
                </td>
            </tr>
            
            <tr>
                <td  class="ControlLabel2" style="width:20%">
                    <asp:Label ID="Label16" runat="server" Text="Payment Mode"></asp:Label>
                </td>
                <td  class="ControlDrpBorder" style="width:25%">
                    <asp:DropDownList ID="ddlPayment" runat="server"  Width="100%" AutoPostBack="True" CssClass="drpDownListMedium"  Height="30px" style="border: 1px solid #90c9fc"  BackColor = "#90c9fc"
                        OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td style="width:3%" align="left">
                                    
                                </td>
                <td  class="ControlLabel2" style="width:15%">
                    <asp:Label ID="Label11" runat="server" Text="FirstLevel"></asp:Label>
                </td>
                <td  class="ControlDrpBorder" style="width:25%">
                    <asp:DropDownList ID="ddlfirstlvl" runat="server"  Width="100%" CssClass="drpDownListMedium"  OnTextChanged="ddlfirstlvl_SelectedIndexChanged" AutoPostBack="true" Height="30px" style="border: 1px solid #90c9fc"  BackColor = "#90c9fc">
                    </asp:DropDownList>
                </td>
                <td style="width:4%">
                
                </td>
            </tr>
            
            <tr>
                <td  class="ControlLabel2" style="width:20%">
                    <asp:Label ID="Label12" runat="server" Text="SecondLevel"></asp:Label>
                </td>
                <td  class="ControlDrpBorder" style="width:25%">
                    <asp:DropDownList ID="ddlsecondlvl" runat="server"  Width="100%" CssClass="drpDownListMedium"  OnTextChanged="ddlsecondlvl_SelectedIndexChanged" AutoPostBack="true" Height="30px" style="border: 1px solid #90c9fc"  BackColor = "#90c9fc">
                    </asp:DropDownList>
                </td>
                <td style="width:3%">
                
                </td>
                <td  class="ControlLabel2" style="width:15%">
                    <asp:Label ID="Label14" runat="server" Text="ThirdLevel"></asp:Label>
                </td>
                <td  class="ControlDrpBorder" style="width:25%">
                    <asp:DropDownList ID="ddlthirdlvl" runat="server" Height="30px" CssClass="drpDownListMedium"  Width="100%" style="border: 1px solid #90c9fc"  BackColor = "#90c9fc">
                    </asp:DropDownList>
                </td>
                <td style="width:4%">
                
                </td>
            </tr>
            <tr style="height:6px">
                
                </tr>
            <tr>
                <td colspan="6">
                    <table width="100%">
                        <tr>
                            <td style="width:40%">
                    
                            </td>
                            <td style="width:20%">
                                <asp:Button ID="btnxls" runat="server" OnClick="btnxls_Click"   CssClass="exportexl6" EnableTheming="false" />
                                <asp:Button ID="btnGrd" runat="server" OnClick="btnGrd_Click" CssClass="generatebutton" EnableTheming="false" Visible="False" />
                            </td>
                            <td style="width:30%">
                                
                            </td>
                            <td style="width:10%">
                
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="height:4px">

                </tr>    
        </table>
        <table>
            <tr style="height:10px">

                </tr>    
        </table>
        </div>
        <asp:GridView ID="Gridpayments" runat="server" BackColor="White" BorderColor="Blue" OnPageIndexChanging="Gridpayments_PageIndexChanging" OnRowCreated="Gridpayments_RowCreated"
            BorderStyle="Solid" BorderWidth="1px" GridLines="None" CssClass="someClass"
            Font-Size="Small" AutoGenerateColumns="False" OnRowDataBound="Gridpayments_RowDataBound" Width="100%"
            ShowFooter="True">
            <Columns>
                <asp:TemplateField HeaderText="Ref. No." HeaderStyle-BorderColor="Blue" HeaderStyle-ForeColor="Black">
                    <ItemTemplate>
                        <asp:Label ID="lblRefno" runat="server" Text='<%# Eval("RefNo") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="TransDate" HeaderStyle-BorderColor="Blue" HeaderStyle-ForeColor="Black">
                    <ItemTemplate>
                        <asp:Label ID="lblDt" runat="server" Text='<%# Eval("TransDate") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DebtorID" HeaderStyle-BorderColor="Blue" HeaderStyle-ForeColor="Black">
                    <ItemTemplate>
                        <asp:Label ID="lblDebit" runat="server" Text='<%# Eval("PaidTo") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PaymentMode" HeaderStyle-BorderColor="Blue" HeaderStyle-ForeColor="Black">
                    <ItemTemplate>
                        <asp:Label ID="lblPayMode" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Cheque No" HeaderStyle-BorderColor="Blue" HeaderStyle-ForeColor="Black">
                    <ItemTemplate>
                        <asp:Label ID="lblBank" runat="server" Text='<%# Eval("CNo") %>'></asp:Label>
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
    </center>
</asp:Content>
