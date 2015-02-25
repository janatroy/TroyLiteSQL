<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="PhysicalStockForm.aspx.cs" Inherits="PhysicalStockForm" Title="Inventory > Stock Reconcilliation Form" %>

<%@ Register Assembly="RealWorld.Grids" Namespace="RealWorld.Grids" TagPrefix="rwg" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajX" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel16" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <table style="width: 1000px; vertical-align: top" align="center">
                <tr style="width: 100.3%">
                    <td style="width: 100.3%">
                        <table style="width: 99.8%; border: 0px solid #86b2d1; margin: -3px 0px 0px -3px;" align="center" cellpadding="3" cellspacing="5">
                            <tr>
                                <td>
                                    <asp:Label ID="err" runat="server" Width="94%" CssClass="info" SkinID="skinHistoryMsg"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <br />


                        <table style="width: 100%; border: 1px solid #86b2d1; margin: -3px 0px 0px 2px;" align="center" cellpadding="3" cellspacing="5">
                            <tr>
                                <td colspan="2" class="subHeadFont2">Stock Reconciliation Form
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="left">

                                    <asp:TextBox ID="txtDate" runat="server" Enabled="false" CssClass="cssTextBox" Width="100px" MaxLength="10" BackColor="#90c9fc" Style="border: 1px solid blue" />
                                    <%--<script type="text/javascript" language="JavaScript">                    new tcal({ 'formname': 'aspnetForm', 'controlname': GettxtBoxName('txtDate') });</script>
                                    &nbsp;--%>
                                    <ajX:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
                                        Format="dd/MM/yyyy"
                                        PopupButtonID="ImageButton2" TargetControlID="txtDate">
                                    </ajX:CalendarExtender>
                                    <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False"
                                        ImageUrl="App_Themes/NewTheme/images/cal.gif"
                                        Width="20px" />

                                    <asp:Button ID="cmdShow" runat="server" ValidationGroup="salesval" CssClass="Button"
                                        Width="150px" Text="Show Entered Stock" OnClick="cmdShow_Click" />
                                    &nbsp;<asp:Button ID="cmdSave" runat="server" ValidationGroup="salesval" CssClass="Button"
                                        Width="150px" Text="Save Closing Stock" OnClick="cmdSave_Click" />
                                    &nbsp;<asp:Button ID="cmdOpen" runat="server" ValidationGroup="salesval" CssClass="Button"
                                        Width="150px" Text="Show Op.Stock" Visible="false" OnClick="cmdOpen_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center" class="lblFont">
                                    <asp:RequiredFieldValidator ID="rq" runat="server" ErrorMessage="Date is Required"
                                        Display="Dynamic" ValidationGroup="salesval" ControlToValidate="txtDate"></asp:RequiredFieldValidator>
                                    <asp:HiddenField ID="hiddenCurrentDate" Value="<%= DateTime.Now %>" runat="server" />
                                    <asp:HiddenField ID="hiddenDate" runat="server" />
                                    <%--<asp:CompareValidator ID="cmpVal" runat="server" ControlToValidate="hiddenDate" Display="Dynamic"    
            ErrorMessage="Date Should Be Less Than or equal to current date"   ControlToCompare="hiddenCurrentDate"
            Operator="GreaterThanEqual"  SetFocusOnError="True" Type="Date" ValidationGroup="salesval"></asp:CompareValidator>--%>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">


                                    <asp:Button runat="server" ID="SaveButton" Text="Save Data" SkinID="skinButtonBig"
                                        Visible="false" />

                                    <rwg:BulkEditGridView ID="EditableGrid" AutoGenerateColumns="False" BorderWidth="1px"
                                        BorderStyle="Solid" GridLines="Both" SaveButtonID="SaveButton" runat="server"
                                        Width="100%" CssClass="someClass">
                                        <RowStyle CssClass="dataRow" />
                                        <SelectedRowStyle CssClass="SelectdataRow" />
                                        <AlternatingRowStyle CssClass="altRow" />
                                        <EmptyDataRowStyle CssClass="HeadataRow" Font-Bold="true" />
                                        <HeaderStyle Wrap="false" />
                                        <FooterStyle />
                                        <HeaderStyle Height="30px" HorizontalAlign="Center" Font-Bold="true" BackColor="#cccccc" BorderColor="Gray" Font-Size="Small" />
                                        <RowStyle Font-Bold="true" HorizontalAlign="Center" Height="30px" Font-Size="Small" ForeColor="#0567AE" />
                                        <Columns>
                                            <asp:BoundField HeaderText="ItemCode" ReadOnly="true" DataField="itemCode" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" />
                                            <asp:BoundField HeaderText="ProductName" ReadOnly="true" DataField="ProductName" ItemStyle-HorizontalAlign="Left"
                                                ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" />
                                            <asp:BoundField HeaderText="ProductDesc" ReadOnly="true" DataField="ProductDesc" ItemStyle-HorizontalAlign="Left"
                                                ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" />
                                            <asp:BoundField HeaderText="Model" DataField="Model" ReadOnly="true" ApplyFormatInEditMode="false" ItemStyle-HorizontalAlign="Left" HeaderStyle-BorderColor="Gray" />
                                            <asp:BoundField HeaderText="Date" DataField="ClosingDate" ReadOnly="true" Visible="false" ItemStyle-HorizontalAlign="Left"
                                                ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" />
                                            <asp:TemplateField HeaderText="Stock" HeaderStyle-BorderColor="Gray" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtStock" Width="80px" runat="server" style="text-align:right" CssClass="cssTextBox" Text="<%#Bind('Stock')%>"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ValidationGroup="salesval" ID="rq" runat="server" Display="Dynamic"
                                                        ControlToValidate="txtStock" ErrorMessage="Empty"></asp:RequiredFieldValidator>
                                                    <ajX:FilteredTextBoxExtender ID="FilteredTextBoxExtender41" runat="server" TargetControlID="txtStock"
                                                        FilterType="Custom, Numbers" ValidChars="." />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </rwg:BulkEditGridView>
                                    <asp:SqlDataSource ID="datasrc" runat="server" ProviderName="System.Data.SqlClient" UpdateCommand="INSERT INTO [ClosingStock] ([ItemCode], [ClosingDate], [Stock]) VALUES (@Itemcode, @ClosingDate, @Stock)" />

                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
