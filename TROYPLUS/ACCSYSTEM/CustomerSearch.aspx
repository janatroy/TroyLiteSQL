<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="CustomerSearch.aspx.cs" Inherits="CustomerSearch" Title="Customer Search" %>

<%@ Register Src="~/SMS/UserControls/errordisplay.ascx" TagName="errordisplay" TagPrefix="uc1" %>
<%@ Register Namespace="UrlNameSpace" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <script language="javascript" type="text/javascript">

        function clickButton(e, buttonid) {
            var evt = e ? e : window.event;
            var bt = document.getElementById(buttonid);

            if (bt) {
                if (evt.keyCode == 13) {
                    bt.click();
                    return false;
                }
            }
        }

    </script>
    <table style="width: 100%;">
        <tr>
            <td colspan="5">
                <br />
            </td>
        </tr>
        <tr style="width: 100%">
            <td class="SectionHeader" colspan="5">
                <span>Search Customer </span>
            </td>
        </tr>
        <tr>
            <td class="LMSLeftColumnColor" style="width: 20%">
                Area :
            </td>
            <td style="width: 25%">
                <asp:DropDownList ID="ddArea" runat="server" AppendDataBoundItems="True" Width="100%"
                    SkinID="skinDdlBox" DataSourceID="srcArea" DataTextField="area" DataValueField="area">
                    <asp:ListItem Value="0">   -- All Areas --    </asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="width: 20%" class="LMSLeftColumnColor">
                Active Customer ? :
            </td>
            <td style="width: 25%; text-align: left">
                <asp:CheckBox ID="CheckBox1" runat="server" />
            </td>
            <td style="width: 10%">
            </td>
        </tr>
        <tr>
            <td class="LMSLeftColumnColor" style="width: 20%">
                Customer Name
            </td>
            <td style="width: 25%">
                <asp:TextBox ID="txtUserId" runat="server" SkinID="skinTxtBoxGrid" Width="100%"></asp:TextBox>
            </td>
            <td style="width: 20%" class="LMSLeftColumnColor">
                Code :
            </td>
            <td style="width: 25%">
                <asp:TextBox ID="txtCode" runat="server" SkinID="skinTxtBoxGrid" Width="100%"></asp:TextBox>
            </td>
            <td style="width: 10%; text-align: left; height: 20px; padding-left: 25px">
                <asp:ImageButton ID="lnkBtnSearchId" runat="server" ImageUrl="~/App_Themes/DefaultTheme/Images/gtk-find_24x24.png"
                    OnClick="lnkBtnSearch_Click" Text="Search" ToolTip="Click here to search" TabIndex="3" />
            </td>
        </tr>
        <tr>
            <td style="width: 20px" colspan="5">
                <asp:SqlDataSource ID="srcArea" runat="server" SelectCommand="SELECT [area] FROM [AreaMaster]"
                    EnableCaching="True" ProviderName="System.Data.OleDb"></asp:SqlDataSource>
            </td>
        </tr>
        <tr style="width: 100%; text-align: left">
            <td width="100%" colspan="5">
                <br />
                <asp:GridView ID="GrdViewCust" TabIndex="6" Width="100%" runat="server" AutoGenerateColumns="False"
                    OnRowCreated="GrdViewCust_RowCreated" AllowPaging="true" OnRowCommand="GrdViewCust_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="name" HeaderText="Customer">
                            <ItemStyle Width="20%" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="code" HeaderText="Code">
                            <ItemStyle Width="10%" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="area" HeaderText="Area">
                            <ItemStyle Width="20%" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="doorno" HeaderText="Door No">
                            <ItemStyle Width="10%" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="place" HeaderText="Place">
                            <ItemStyle Width="15%" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="category" HeaderText="Category">
                            <ItemStyle Width="10%" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="balance" HeaderText="Balance">
                            <ItemStyle Width="10%" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="effectdate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Effective Date">
                            <ItemStyle Width="12%" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <uc:BoundUrlColumn DataField="areanamecode" Tooltip="Edit Customer" BaseUrl="CustomerDetails.aspx?ID="
                            IconPath="App_Themes/DefaultTheme/Images/edit.png" ItemStyle-BorderWidth="0">
                            <ItemStyle Width="50px" BorderWidth="0" />
                        </uc:BoundUrlColumn>
                    </Columns>
                    <PagerTemplate>
                        Goto Page
                        <asp:DropDownList ID="ddlPageSelector" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSelector_SelectedIndexChanged"
                            SkinID="dropDownPage">
                        </asp:DropDownList>
                    </PagerTemplate>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td style="height: 20px" colspan="5">
                <asp:LinkButton ID="lnkBtnAdd" runat="server" PostBackUrl="~/CustomerDetails.aspx?ID=">Click 
                here to Add New Customer</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <uc1:errordisplay ID="errordisplay1" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
