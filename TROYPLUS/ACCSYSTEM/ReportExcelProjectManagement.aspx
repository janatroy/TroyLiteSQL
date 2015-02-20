<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ReportExcelProjectManagement.aspx.cs" Inherits="ReportExcelProjectManagement" Title="Project Management Page" %>

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
        <div align="center">
            <table width="100%" style="background-color:White; border:1px solid blue;">
                <%--<tr style="height:15px">

                </tr>--%>
                <%--<tr class="subHeadFont">
                    <td colspan="3">
                        <table>
                            <tr>
                                <td>
                                    Products History Report
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>--%>
                <tr>
                        <td colspan="3" class="headerPopUp">
                            Project Management Report
                        </td>
                    </tr>
                    
                <tr style="height:10px">

                </tr>
                <tr>
                    <td style="width:30%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                        Project
                    </td>
                    <td style="width: 30%;" class="ControlDrpBorder">
                        <asp:DropDownList ID="drpProjectCode" runat="server" Width="100%" AutoPostBack="true" style="border: 1px solid #90c9fc" height="26px"
                            CssClass="drpDownListMedium" BackColor = "#90C9FC" OnSelectedIndexChanged="drpProjectCode_SelectedIndexChanged" DataTextField="Project_Name" DataValueField="Project_Id">
                            
                        </asp:DropDownList>
                    </td>
                    <td style="width: 25%;">

                    </td>
                </tr>
                <tr>
                    <td style="width:30%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                        Employee
                    </td>
                    <td style="width: 30%;" class="ControlDrpBorder">
                            <asp:DropDownList ID="drpIncharge" runat="server" Width="100%" AutoPostBack="true" style="border: 1px solid #90c9fc" height="26px"
                            CssClass="drpDownListMedium" BackColor = "#90C9FC" OnSelectedIndexChanged="drpIncharge_SelectedIndexChanged" DataTextField="empFirstName" DataValueField="empno">
                            
                            </asp:DropDownList>
                    </td>
                    <td style="width: 25%;">

                    </td>
                </tr>
                <tr>
                    <td style="width:30%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                        Task Type
                    </td>
                    <td style="width: 30%;" class="ControlDrpBorder">
                            <asp:DropDownList ID="drpTaskType" runat="server" Width="100%"  style="border: 1px solid #90c9fc" height="26px" AutoPostBack="true"
                            CssClass="drpDownListMedium" BackColor = "#90C9FC" OnSelectedIndexChanged="drpTaskType_SelectedIndexChanged" DataTextField="Task_Type_Name" DataValueField="Task_Type_Id">
                            
                            </asp:DropDownList>
                    </td>
                    <td style="width: 25%;">

                    </td>
                </tr>
                <tr>
                    <td class="ControlLabel2"  style="width:30%;">
                        Task Status
                    </td>
                    <td class="ControlDrpBorder" style="width: 30%">
                        <asp:DropDownList ID="drpTaskStatus" runat="server" BackColor = "#90c9fc"
                            DataTextField="Task_Status_Name" DataValueField="Task_Status_Id" OnSelectedIndexChanged="drpTaskStatus_SelectedIndexChanged" style="border: 1px solid #90c9fc" height="26px" CssClass="drpDownListMedium"
                            AutoPostBack="true" Width="100%">
                            
                        </asp:DropDownList>
                    </td>
                    <td class="ControlLabel" style="width:25%;">
                        
                    </td>
                </tr>
                <tr style="height:10px">

                </tr>
                <tr>
                    <td colspan="3">
                        <table width="100%">
                            <tr>
                                <td style="width:40%">
                    
                                </td>
                                <td  style="width:20%">
                                    <asp:Button ID="btnxls" runat="server" OnClick="btnxls_Click"  CssClass="exportexl6" EnableTheming="false" />
                                </td>
                                <td style="width:40%">
                                    <asp:Button ID="btnData" runat="server" OnClick="btnData_Click" CssClass="generatebutton" EnableTheming="false"   Visible="False" />
                                </td>
                            </tr>
                        </table>
                    </td>
                 </tr>
                 <tr style="height:7px">

                </tr>
             </table>
            </div>

            <table style="width:100%">
            <tr style="height:6px">

                </tr>
            <tr>
                <td style="width:100%">
                    <asp:GridView ID="GridCust" runat="server" BackColor="White" BorderColor="blue" Width="100%"  OnRowCreated="GridCust_RowCreated" OnPageIndexChanging="GridCust_PageIndexChanging"
                        BorderStyle="Solid" BorderWidth="1px" CellPadding="3" CellSpacing="1" Font-Size="Small" Font-Names="Trebuchet MS" CssClass="someClass"
                        AutoGenerateColumns="False" OnRowDataBound="GridCust_RowDataBound" AllowPaging="True" HeaderStyle-ForeColor="Black">
                        <Columns>
                            <%--<asp:TemplateField HeaderText="Rate Type"  HeaderStyle-BorderColor="Blue" >
                                <ItemTemplate>
                                    <asp:Label ID="lblLedger" runat="server" Text='<%# Eval("ProductName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Start Date"  HeaderStyle-BorderColor="Blue" >
                                <ItemTemplate>
                                    <asp:Label ID="lblLed" runat="server" Text='<%# Eval("ItemCode") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="End Date"  HeaderStyle-BorderColor="Blue" >
                                <ItemTemplate>
                                    <asp:Label ID="lblLedg" runat="server" Text='<%# Eval("Model") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rate"  HeaderStyle-BorderColor="Blue" >
                                <ItemTemplate>
                                    <asp:Label ID="lblLedg" runat="server" Text='<%# Eval("Rate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Product Name"  HeaderStyle-BorderColor="Blue" >
                                <ItemTemplate>
                                    <asp:Label ID="lblLedger" runat="server" Text='<%# Eval("ProductName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Code"  HeaderStyle-BorderColor="Blue" >
                                <ItemTemplate>
                                    <asp:Label ID="lblLed" runat="server" Text='<%# Eval("ItemCode") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Model"  HeaderStyle-BorderColor="Blue" >
                                <ItemTemplate>
                                    <asp:Label ID="lblLedg" runat="server" Text='<%# Eval("Model") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Brand"  HeaderStyle-BorderColor="Blue" >
                                <ItemTemplate>
                                    <asp:Label ID="lblLedge" runat="server" Text='<%# Eval("Productdesc") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MRP"  HeaderStyle-BorderColor="Blue">
                                <ItemTemplate>
                                    <asp:Label ID="lblMRP" runat="server" Text='<%# Eval("MRP") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MRP Date"  HeaderStyle-BorderColor="Blue">
                                <ItemTemplate>
                                    <asp:Label ID="lblMRPDate" runat="server" Text='<%# Eval("MRPDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="NLC"  HeaderStyle-BorderColor="Blue">
                                <ItemTemplate>
                                    <asp:Label ID="lblNLC" runat="server" Text='<%# Eval("NLC") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="NLC Date"  HeaderStyle-BorderColor="Blue">
                                <ItemTemplate>
                                    <asp:Label ID="lblNLCDate" runat="server" Text='<%# Eval("NLCDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DP"  HeaderStyle-BorderColor="Blue">
                                <ItemTemplate>
                                    <asp:Label ID="lblDP" runat="server" Text='<%# Eval("DP") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DP Date"  HeaderStyle-BorderColor="Blue">
                                <ItemTemplate>
                                    <asp:Label ID="lblDPdate" runat="server" Text='<%# Eval("DPdate") %>'></asp:Label>
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
               
            </tr>
        </table>

    </center>
</asp:Content>
