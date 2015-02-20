<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="OldDataView.aspx.cs" Inherits="OldDataView" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <table cellpadding="2" cellspacing="2" width="100%" style="border: solid 1px black"
        border="0" class="accordionContent">
        <tr>
            <td colspan="3" class="accordionHeader">
                Configuration for Old Year Data
            </td>
        </tr>
        <tr>
            <td colspan="3" class="lblFont">
                <asp:Label ID="lblMsg" runat="server" Style="color: Red; font-weight: bold;"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="lblFont">
                Select the Available Data
            </td>
            <td align="left" class="lblFont">
                <asp:DropDownList ID="drpYear" runat="server" CssClass="lblFont">
                </asp:DropDownList>
            </td>
            <td class="lblFont">
                <asp:Button ID="btnMode" SkinID="skinButtonBig" runat="server" OnClick="btnMode_Click"
                    Text="Change Year" />
            </td>
        </tr>
</asp:Content>
