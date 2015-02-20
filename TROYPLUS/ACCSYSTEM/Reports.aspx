<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="Reports.aspx.cs" Inherits="Reports" Title="Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <br />
    <br />
    <table align="center" width="50%" style="border: 1px solid black; margin: 0 0 0 50px"
        cellpadding="5" cellspacing="5">
        <tr>
            <td class="SectionHeader">
                Inventory Reports
            </td>
        </tr>
        <tr align="left">
            <td>
                <asp:Button runat="server" OnClientClick="window.open('ReorderlevelReport.aspx ','Summary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=1000,width=900,left=10,top=10, scrollbars=yes');"
                    ID="ctl00_cplhControlPanel_lnkReorder" Text="ReOrder Level Report" Width="100%"
                    Font-Bold="false" SkinID="skinButtonCol"></asp:Button>
            </td>
        </tr>
        <tr align="left">
            <td>
                <asp:Button runat="server" OnClientClick="window.open('StockReport.aspx ','Summary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=1000,width=900,left=10,top=10, scrollbars=yes');"
                    ID="ctl00_cplhControlPanel_lnkStock" Text="Stock Report" Width="100%" Font-Bold="false"
                    SkinID="skinButtonCol"></asp:Button>
            </td>
        </tr>
        <tr align="left">
            <td>
                <asp:Button runat="server" OnClientClick="window.open('StockListReport.aspx ','Summary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=1000,width=900,left=10,top=10, scrollbars=yes');"
                    ID="ctl00_cplhControlPanel_lnkStockReport" Text="Stock List Report" Width="100%"
                    Font-Bold="false" SkinID="skinButtonCol"></asp:Button>
            </td>
        </tr>
        <tr align="left">
            <td>
                <asp:Button runat="server" OnClientClick="window.open('StockReconReport.aspx ','Summary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=1000,width=900,left=10,top=10, scrollbars=yes');"
                    ID="lnkRecon" Text="Stock Reconcilation Report" Width="100%" Font-Bold="false"
                    SkinID="skinButtonCol"></asp:Button>
            </td>
        </tr>
    </table>
</asp:Content>
