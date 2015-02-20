<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ReportExcelSuppliers.aspx.cs" Inherits="Suppliers" Title="Suppliers Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <center>
    <br />
        <div>
            <table width="400px" style=" background-color:White; border:1px solid blue;">
                <%--<tr class="subHeadFont">
                    <td colspan="4">
                        <table>
                            <tr>
                                <td>
                                    Supplier Report
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>--%>
                <tr>
                        <td colspan="4" class="headerPopUp">
                            Supplier Report
                        </td>
                    </tr>
                <tr style="height:6px">

                </tr>
                <%--<tr>
                    <td colspan="4">
                        <table style="width:100%">
                            <tr>
                                <td style="width:30%">
                                    
                                </td>
                                <td  style="width:40%">
                                    <asp:Label ID="Label7" runat="server" Text="Supplier Details" Font-Bold="True" Font-Size="13" ForeColor="#0033CC"></asp:Label>
                                </td>
                                <td style="width:30%">
                    
                                </td>
                            </tr>
                        </table>
                    </td>
                    
                </tr>    --%>
                
                <tr>
                    
                    <td style="width: 35%" align="right">
                        Search
                    </td>
                    <td style="width: 15%" class="ControlTextBox3">
                        <asp:TextBox ID="txtSearch" runat="server"  CssClass="cssTextBox"></asp:TextBox>
                    </td>
                    <td style="width: 20%">
                        
                    </td>
                    <td style="width: 15%">
                        
                    </td>
                </tr>
                <tr>
                    <td style="width: 35%" align="right">
                        
                    </td>
                    <td style="width: 20%" class="ControlDrpBorder">
                        <div style="width: 150px; font-family: 'Trebuchet MS';">
                            <asp:DropDownList ID="ddCriteria" runat="server" Width="170px" CssClass="drpDownListMedium"  BackColor = "#90c9fc" height="26px" style="border: 1px solid #90c9fc">
                                <asp:ListItem Value="0">All</asp:ListItem>
                                <asp:ListItem Value="LedgerName">Supplier Name</asp:ListItem>
                                <asp:ListItem Value="AliasName">Alias Name</asp:ListItem>
                                <asp:ListItem Value="Phone">Phone</asp:ListItem>
                                <asp:ListItem Value="TinNo">Tin No</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td style="width: 20%">
                        
                    </td>
                    <td style="width: 15%">
                        
                    </td>
                </tr>
                <tr style="height:10px">

                </tr>
            <tr>
                <td colspan="4">
                     <table width="100%">
                        <tr>
                            <td style="width:40%" align="right">
                                <asp:Button ID="btnData" runat="server" OnClick="btnData_Click"  CssClass="generatebutton" EnableTheming="false" Visible="False" />
                            </td>
                            <td style="width:20%" align="left">
                                <asp:Button ID="btnxls" runat="server" OnClick="btnxls_Click" CssClass="exportexl6" EnableTheming="false" />
                            </td>
                            <td style="width:40%">
                    
                            </td>
                        </tr>
                     </table>
                </td>
                
            </tr>
         </table>
      </div>
      <table>
          <tr>
                <td>
                    &nbsp;
                </td>
                <td colspan="4">
                    &nbsp;
                    <asp:GridView ID="GridSpply" runat="server" BackColor="White" BorderColor="blue" OnRowCreated="GridSpply_RowCreated" OnPageIndexChanging="GridSpply_PageIndexChanging"
                        BorderStyle="Solid" BorderWidth="1px" CellPadding="3" CellSpacing="1" Font-Size="Small" Font-Names="Trebuchet MS" CssClass="someClass"
                        AutoGenerateColumns="False" OnRowDataBound="GridSpply_RowDataBound" AllowPaging="True" HeaderStyle-ForeColor="Black">
                        <Columns>
                            <asp:TemplateField HeaderText="LedgerName"  HeaderStyle-BorderColor="Blue">
                                <ItemTemplate>
                                    <asp:Label ID="lblLedger" runat="server" Text='<%# Eval("LedgerName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="AliasName"  HeaderStyle-BorderColor="Blue">
                                <ItemTemplate>
                                    <asp:Label ID="lblAlisname" runat="server" Text='<%# Eval("AliasName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Address"  HeaderStyle-BorderColor="Blue">
                                <ItemTemplate>
                                    <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("Address") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TINnumber"  HeaderStyle-BorderColor="Blue">
                                <ItemTemplate>
                                    <asp:Label ID="lblTINnumber" runat="server" Text='<%# Eval("TINnumber") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Total :
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Credit Limit"  HeaderStyle-BorderColor="Blue">
                                <ItemTemplate>
                                    <asp:Label ID="lblCreditLimit" runat="server" Text='<%# Eval("CreditLimit") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblCredit" runat="server"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <%-- <asp:TemplateField HeaderText="OpenBalanceDR">
                <ItemTemplate>
                <asp:Label ID="lblOpenBalanceDR" runat="server" Text='<%# Eval("OpenBalanceDR") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                <asp:Label ID="lblBalanceDR" runat="server"></asp:Label>
                </FooterTemplate>
                </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="OpenBalance"  HeaderStyle-BorderColor="Blue">
                                <ItemTemplate>
                                    <asp:Label ID="OpenBal" runat="server" Text='<%# Eval("OpenBal") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblBalance" runat="server"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Phone"  HeaderStyle-BorderColor="Blue">
                                <ItemTemplate>
                                    <asp:Label ID="lblPhone" runat="server" Text='<%# Eval("Phone") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="LedgerCategory"  HeaderStyle-BorderColor="Blue">
                                <ItemTemplate>
                                    <asp:Label ID="lblLedgerCategory" runat="server" Text='<%# Eval("LedgerCategory") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mobile"  HeaderStyle-BorderColor="Blue">
                                <ItemTemplate>
                                    <asp:Label ID="lblMobile" runat="server" Text='<%# Eval("Mobile") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Credit Days"  HeaderStyle-BorderColor="Blue">
                                <ItemTemplate>
                                    <asp:Label ID="lblCreditDays" runat="server" Text='<%# Eval("CreditDays") %>'></asp:Label>
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
                                                <asp:DropDownList ID="ddlPageSelector" runat="server" AutoPostBack="true"  OnSelectedIndexChanged="ddlPageSelector_SelectedIndexChanged" style="border:1px solid blue"  Width="65px">
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
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="black" />
                    </asp:GridView>
                    &nbsp;
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
