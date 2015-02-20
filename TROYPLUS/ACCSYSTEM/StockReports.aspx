<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="StockReports.aspx.cs" Inherits="StockReports" Title="Stock Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <br />
    <br />
    <table align="center" width="50%" style="border: 1px solid #86b2d1; margin: 0 0 0 0px"
        cellpadding="5" cellspacing="5">
        <tr>
            <td class="HeadataRow" style="font-weight: bold; text-align: center">
                STOCK REPORTS
            </td>
        </tr>
        <tr align="left" id="RowReoderlevel" runat="server">
            <td>
                <asp:Button runat="server" Width="100%" Font-Bold="false" SkinID="skinButtonCol"
                    OnClientClick="window.open('ReOrderlevelReport.aspx','Summary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=1000,width=900,left=10,top=10, scrollbars=yes');"
                    ID="Button1" Text="ReOrder Level Report"></asp:Button>
            </td>
        </tr>
        <tr align="left" id="RowStockReport" runat="server">
            <td>
                <asp:Button runat="server" Width="100%" Font-Bold="false" SkinID="skinButtonCol"
                    OnClientClick="window.open('StockReport.aspx','Summary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=1000,width=900,left=10,top=10, scrollbars=yes');"
                    ID="Button2" Text="Stock Report"></asp:Button>
            </td>
        </tr>
        <tr align="left" id="RowstockLedger" runat="server">
            <td>
                <asp:Button runat="server" Width="100%" Font-Bold="false" SkinID="skinButtonCol"
                    OnClientClick="window.open('StockListReport.aspx','Summary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=1000,width=900,left=10,top=10, scrollbars=yes');"
                    ID="Button3" Text="Stock Ledger Report"></asp:Button>
            </td>
        </tr>
        <tr align="left" id="RowStockRecon" runat="server">
            <td>
                <asp:Button runat="server" Width="100%" Font-Bold="false" SkinID="skinButtonCol"
                    OnClientClick="window.open('StockReconReport.aspx','Summary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=1000,width=900,left=10,top=10, scrollbars=yes');"
                    ID="Button4" Text="Stock Reconcilation Report"></asp:Button>
            </td>
        </tr>
        <tr align="left" id="RowStockVerification" runat="server">
            <td>
                <asp:Button runat="server" Width="100%" Font-Bold="false" SkinID="skinButtonCol"
                    OnClientClick="window.open('StockVerificationReport.aspx','Summary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=1000,width=900,left=10,top=10, scrollbars=yes');"
                    ID="Button5" Text="Stock Verification Report"></asp:Button>
            </td>
        </tr>
        <tr align="left" id="RowStockAging" runat="server">
            <td>
                <asp:Button runat="server" Width="100%" Font-Bold="false" SkinID="skinButtonCol"
                    OnClientClick="window.open('StockAgeingReport.aspx','Summary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=1000,width=900,left=10,top=10, scrollbars=yes');"
                    ID="Button6" Text="Stock Ageing Report"></asp:Button>
            </td>
        </tr>
    </table>
    <br />
</asp:Content>
