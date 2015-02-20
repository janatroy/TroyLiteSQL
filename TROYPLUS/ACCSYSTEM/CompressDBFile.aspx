<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="CompressDBFile.aspx.cs" Inherits="CompressDBFile" Title="Compress DB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <br />
    <br />
    <br />
    <table align="center" width="50%" style="border: 1px solid black; margin: 0 0 0 50px"
        cellpadding="5" cellspacing="5">
        <tr align="center">
            <td>
                <asp:Button ID="btnCompress" runat="server" Text="Compress DB" CausesValidation="false"
                    OnClick="btnCompress_Click" Width="90%" Font-Bold="false" SkinID="skinButtonCol" />
            </td>
        </tr>
    </table>
    <br />
    <br />
</asp:Content>
