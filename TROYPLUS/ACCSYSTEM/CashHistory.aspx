<%@ Page Language="C#" MasterPageFile="~/SimplePage.master" AutoEventWireup="true"
    CodeFile="CashHistory.aspx.cs" Inherits="CashHistory" Title="Cash History" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <br />
    <table style="width: 80%;" align="center">
        <tr>
            <td class="SectionHeader">
                Cash History
            </td>
        </tr>
        <tr style="width: 100%">
            <td>
                <asp:GridView ID="grdCash" runat="server" Width="100%" PageSize="5" DataKeyNames="slno"
                    GridLines="Horizontal" AutoGenerateColumns="False" AllowPaging="True" DataSourceID="srcCashHistory"
                    EmptyDataText="No Cash details found for this Customer" OnPreRender="grdCash_PreRender"
                    OnRowCommand="grdCash_RowCommand" OnRowCreated="grdCash_RowCreated" OnRowDataBound="grdCash_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="slno" HeaderText="Sl No">
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="billno" HeaderText="Bill No.">
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="amount" HeaderText="Amount">
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="discount" HeaderText="Discount">
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="reason" HeaderText="Reason">
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="date_paid" HeaderText="Date Paid" DataFormatString="{0:dd/MM/yyyy}">
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="date_entered" HeaderText="Date Entered" DataFormatString="{0:dd/MM/yyyy}">
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                    </Columns>
                    <PagerTemplate>
                        Goto Page
                        <asp:DropDownList ID="ddlPageSelector" SkinID="dropDownPage" runat="server" AutoPostBack="true">
                        </asp:DropDownList>
                        <asp:Button Text="First" CommandName="Page" CommandArgument="First" runat="server"
                            ID="btnFirst" />
                        <asp:Button Text="Previous" CommandName="Page" CommandArgument="Prev" runat="server"
                            ID="btnPrevious" />
                        <asp:Button Text="Next" CommandName="Page" CommandArgument="Next" runat="server"
                            ID="btnNext" />
                        <asp:Button Text="Last" CommandName="Page" CommandArgument="Last" runat="server"
                            ID="btnLast" />
                    </PagerTemplate>
                </asp:GridView>
            </td>
        </tr>
        <tr height="10%" width="100%">
            <td style="width: 100%" align="left">
            </td>
        </tr>
        <tr height="10%" width="100%">
            <td align="left" style="width: 100%">
                &nbsp;
            </td>
        </tr>
        <tr height="10%" width="100%">
            <td style="width: 100%" align="left">
                <asp:SqlDataSource ID="srcCashHistory" runat="server" SelectCommand="SELECT slno, [code], [area], [amount], [discount], [reason], [date_paid], [date_entered], [billno] FROM [CashDetails] WHERE (([code] = ?) AND ([area] = ?)) ORDER BY slno DESC"
                    ProviderName="System.Data.OleDb">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="code" QueryStringField="Code" Type="Int32" />
                        <asp:QueryStringParameter Name="area" QueryStringField="Area" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr width="100%">
            <td align="left" style="width: 100%">
            </td>
        </tr>
        <tr>
            <td class="SectionHeader">
                <span>Adjustment History&nbsp;</span>
            </td>
        </tr>
        <tr style="width: 100%">
            <td>
                <asp:GridView ID="grdAdjustment" runat="server" Width="100%" PageSize="5" GridLines="Horizontal"
                    AutoGenerateColumns="False" AllowPaging="True" DataSourceID="srcAdjustments"
                    EmptyDataText="No Adjustments found for this Customer" OnRowCommand="grdAdjustment_RowCommand"
                    OnRowCreated="grdAdjustment_RowCreated" OnRowDataBound="grdAdjustment_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="amount" HeaderText="Amount">
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="reason" HeaderText="Reason">
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="date_entered" HeaderText="Date Entered" DataFormatString="{0:dd/MM/yyyy}">
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                    </Columns>
                    <PagerTemplate>
                        Goto Page
                        <asp:DropDownList ID="ddlPageSelector" SkinID="dropDownPage" runat="server" AutoPostBack="true">
                        </asp:DropDownList>
                        <asp:Button Text="First" CommandName="Page" CommandArgument="First" runat="server"
                            ID="btnFirst" />
                        <asp:Button Text="Previous" CommandName="Page" CommandArgument="Prev" runat="server"
                            ID="btnPrevious" />
                        <asp:Button Text="Next" CommandName="Page" CommandArgument="Next" runat="server"
                            ID="btnNext" />
                        <asp:Button Text="Last" CommandName="Page" CommandArgument="Last" runat="server"
                            ID="btnLast" />
                    </PagerTemplate>
                </asp:GridView>
            </td>
        </tr>
        <tr height="10%" width="100%">
            <td style="width: 100%" align="left">
            </td>
        </tr>
        <tr height="10%" width="100%">
            <td align="left" style="width: 100%">
                &nbsp;
            </td>
        </tr>
        <tr height="10%" width="100%">
            <td style="width: 100%" align="left">
                &nbsp;<asp:SqlDataSource ID="srcAdjustments" runat="server" SelectCommand="SELECT [amount],[reason],[date_entered] FROM [Adjustment] WHERE (([code] = ?) AND ([area] = ?)) ORDER BY date_entered DESC"
                    ProviderName="System.Data.OleDb">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="code" QueryStringField="Code" Type="Int32" />
                        <asp:QueryStringParameter Name="area" QueryStringField="Area" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr width="100%">
            <td align="left" style="width: 100%">
            </td>
        </tr>
    </table>
</asp:Content>
