<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ReportExcelEmployee.aspx.cs" Inherits="ReportExcelEmployee" Title="Employee Page" %>

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
        <div >
            <table width="400px" style="background-color:White; border:1px solid blue;">
                <tr class="subHeadFont">
                    <td colspan="4">
                        <table>
                            <tr>
                                <td>
                                    Employee Report
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr style="height:15px">

                </tr>
                <tr>
                    <td style="width:25%">
                    
                    </td>
                    <td style="width:1%">
                        <asp:Button ID="btnData" runat="server" OnClick="btnData_Click" CssClass="griddata" EnableTheming="false"   Visible="False" />
                    </td>
                    <td  style="width:20%">
                        <asp:Button ID="btnxls" runat="server" OnClick="btnxls_Click"  CssClass="exportexl6" EnableTheming="false" />
                    </td>
                    <td style="width:30%">
                    
                    </td>
                </tr>
             </table>
            </div>

            <table style="width:100%">
            <tr style="height:6px">

                </tr>
            <tr>
                <td style="width:20%">
                    
                </td>
                <td style="width:60%">
                    <asp:GridView ID="GridCust" runat="server" BackColor="White" BorderColor="blue" Width="100%" OnRowCreated="GridCust_RowCreated" OnPageIndexChanging="GridCust_PageIndexChanging"
                        BorderStyle="Solid" BorderWidth="1px" CellPadding="3" CellSpacing="1" Font-Size="Small" Font-Names="Trebuchet MS" CssClass="someClass"
                        AutoGenerateColumns="False" OnRowDataBound="GridCust_RowDataBound" AllowPaging="True" HeaderStyle-ForeColor="Black">
                        <Columns>
                            <asp:TemplateField HeaderText="First Name"  HeaderStyle-BorderColor="Blue" >
                                <ItemTemplate>
                                    <asp:Label ID="lblLedger" runat="server" Text='<%# Eval("empFirstName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Middle Name"  HeaderStyle-BorderColor="Blue">
                                <ItemTemplate>
                                    <asp:Label ID="lblAname" runat="server" Text='<%# Eval("empMiddleName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sur Name"  HeaderStyle-BorderColor="Blue">
                                <ItemTemplate>
                                    <asp:Label ID="lblAlname" runat="server" Text='<%# Eval("empSurName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DOJ"  HeaderStyle-BorderColor="Blue">
                                <ItemTemplate>
                                    <asp:Label ID="lblAliname" runat="server" Text='<%# Eval("empDOJ") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DOB"  HeaderStyle-BorderColor="Blue">
                                <ItemTemplate>
                                    <asp:Label ID="lblAli" runat="server" Text='<%# Eval("empDOB") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Designation"  HeaderStyle-BorderColor="Blue">
                                <ItemTemplate>
                                    <asp:Label ID="lblAl" runat="server" Text='<%# Eval("empDesig") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remarks"  HeaderStyle-BorderColor="Blue">
                                <ItemTemplate>
                                    <asp:Label ID="lblAlisnam" runat="server" Text='<%# Eval("empRemarks") %>'></asp:Label>
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
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="black" />
                    </asp:GridView>
                </td>
                <td style="width:20%">
                    
                </td>
            </tr>
        </table>

    </center>
</asp:Content>
