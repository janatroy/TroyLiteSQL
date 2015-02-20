<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="InventoryReports.aspx.cs" Inherits="InventoryReports" Title="Reports > Inventory Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">

    <table style="width: 100%">
        <tr style="width: 100%">
            <td style="width: 100%">
            
<%--<div class="mainConHd">
                        <table cellspacing="0" cellpadding="0" border="0">
                            <tr valign="middle">
                                <td>
                                    <span>INVENTORY REPORTS</span>
                                </td>
                            </tr>
                        </table>
                    </div>--%>
                    <%--<table class="mainConHd" style="width: 994px;">
                        <tr valign="middle">
                            <td style="font-size: 20px;">
                                
                            </td>
                        </tr>
                    </table>--%>
                <div class="mainConBody">
                                <table style="width: 100%;margin: -1px 0px 0px 1px;" cellpadding="2px" cellspacing="2px" class="searchbg">
                                    <tr>
                                        <td style="width: 1%">
                                        </td>
                                        <td style="width: 25%; font-size: 20px; color: #000000;" >
                                             INVENTORY REPORTS
                                         </td>
                                        <td style="width: 14%">
                                            
                                        </td>
                                        <td style="width: 10%; color: #000000;" align="right">
                                            
                                        </td>
                                        <td style="width: 19%">
                                            
                                        </td>
                                        <td style="width: 18%">
                                            
                                        </td>
                                        
                                     </tr>
                                </table>
                            </div>
                <div class="mainConDiv" id="IdmainConDiv" align="center">
                    
                    <table align="center" width="50%" style="border: 1px solid #86b2d1; margin: 0 0 0 0px"
                        cellpadding="5" cellspacing="5">
                        <%--<tr align="left" id="RowReoderlevel" runat="server">
                            <td>
                                <asp:Button runat="server" Width="100%" Font-Bold="false" SkinID="skinButtonCol2"
                                    OnClientClick="window.open('ReOrderlevelReport.aspx','Summary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=310,width=500,left=425,top=220, scrollbars=yes');"
                                    ID="Button1" Text="ReOrder"></asp:Button>
                            </td>
                        </tr>--%>
                        <tr align="left" id="RowStockReport" runat="server">
                            <td>
                                <asp:Button runat="server" Width="100%" Font-Bold="false" SkinID="skinButtonCol2"
                                    OnClientClick="window.open('StockReport.aspx','Summary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=180,width=500,left=415,top=270, scrollbars=yes');"
                                    ID="Button2" Text="Current Stock"></asp:Button>
                            </td>
                        </tr>
                        <tr align="left" id="RowstockLedger" runat="server">
                            <td>
                                <asp:Button runat="server" Width="100%" Font-Bold="false" SkinID="skinButtonCol2"
                                    OnClientClick="window.open('StockListReport.aspx','Summary', 'toolbar=no,status=no,menu=no,location=no,height=280,width=670,left=350,top=220 ,resizable=yes, scrollbars=yes');"
                                    ID="Button3" Text="Stock Ledger"></asp:Button>
                            </td>
                        </tr>
                        <tr align="left" id="Tr1" runat="server">
                            <td>
                                <asp:Button runat="server" Width="100%" Font-Bold="false" SkinID="skinButtonCol2"
                                    OnClientClick="window.open('StockSummaryReport.aspx','Summary', 'toolbar=no,status=yes,menu=no,location=no,resizable=yes,height=310,width=550,left=425,top=220, scrollbars=yes');"
                                    ID="Button1" Text="Stock Summary"></asp:Button>
                            </td>
                        </tr>
                        <%--<tr align="left" id="Tr1" runat="server">
                            <td>
                                <asp:Button runat="server" Width="100%" Font-Bold="false" SkinID="skinButtonCol"
                                    OnClientClick="window.open('StockListReportOld.aspx','Summary', 'toolbar=no,status=no,menu=no,location=no,width=900,height=1000,left=210,top=10 ,resizable=yes, scrollbars=yes');"
                                    ID="Button8" Text="Stock Ledger"></asp:Button>
                            </td>
                        </tr>--%>
                        <%--<tr align="left" id="RowstockStatement" runat="server">
                            <td>
                                <asp:Button runat="server" Width="100%" Font-Bold="false" SkinID="skinButtonCol2"
                                    OnClientClick="window.open('ReportXLStockStmt.aspx','Summary', 'toolbar=no,status=no,menu=no,location=no,height=220,width=500,left=415,top=260 ,resizable=yes, scrollbars=yes');"
                                    ID="Button7" Text="Stock Statement"></asp:Button>
                            </td>
                        </tr>--%>
                        <tr align="left" id="RowStockRecon" runat="server">
                            <td>
                                <asp:Button runat="server" Width="100%" Font-Bold="false" SkinID="skinButtonCol2"
                                    OnClientClick="window.open('StockReconReport.aspx','Summary', 'toolbar=no,status=no,menu=no,location=no,height=180,width=500,left=415,top=270,resizable=yes, scrollbars=yes');"
                                    ID="Button4" Text="Reconcilation"></asp:Button>
                            </td>
                        </tr>
                        <%--<tr align="left" id="RowStockVerification" runat="server">
                            <td>
                                <asp:Button runat="server" Width="100%" Font-Bold="false" SkinID="skinButtonCol2"
                                    OnClientClick="window.open('StockVerificationReport.aspx','Summary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=310,width=500,left=425,top=220, scrollbars=yes');"
                                    ID="Button5" Text="Verification"></asp:Button>
                            </td>
                        </tr>--%>
                        <tr align="left" id="RowStockAging" runat="server">
                            <td>
                                <asp:Button runat="server" Width="100%" Font-Bold="false" SkinID="skinButtonCol2"
                                    OnClientClick="window.open('StockAgeingReport.aspx','Summary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=310,width=700,left=359,top=220, scrollbars=no');"
                                    ID="Button6" Text="Ageing"></asp:Button>
                            </td>
                        </tr>
                        <tr align="left" id="Tr18" runat="server">
                            <td>
                                <asp:Button ID="Button23" runat="server" Text="Stock Level Report" Width="100%"
                                    Font-Bold="false" SkinID="skinButtonCol2" OnClientClick="window.open('ReportXlStockLevel.aspx','CSTSummary', 'toolbar=no,status=no,menu=no,location=no,height=250,width=500,left=415,top=260 ,resizable=yes, scrollbars=yes');" />
                            </td>
                        </tr>
                        <tr align="left" id="Tr17" runat="server">
                            <td>
                                <asp:Button ID="BnAbsolute" runat="server" Text="Obsolete Item List Report" Width="100%"
                                    Font-Bold="false" SkinID="skinButtonCol2" OnClientClick="window.open('ReportXlAbsolute.aspx','CSTSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=230,width=500,left=415,top=235, scrollbars=yes');" />
                            </td>
                        </tr>
                        <tr align="left" id="Tr123" runat="server">
                            <td>
                                  <asp:Button ID="btn123" runat="server" Text="Stock Comprehency Report" Width="100%"
                                    Font-Bold="false" SkinID="skinButtonCol2" OnClientClick="window.open('ReportExlStock.aspx','CSTSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=450,width=600,left=380,top=140, scrollbars=no');" />
                            </td>
                        </tr>
                        <%--<tr align="left" id="Tr9" runat="server">
                            <td>
                                <asp:Button ID="Button11" runat="server" Text="Excel Export - Stock Report" Width="100%"
                                    Font-Bold="false" SkinID="skinButtonCol" OnClientClick="window.open('ReportExcelStock.aspx','CSTSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=295,width=500,left=380,top=150, scrollbars=no');" />

                                    <asp:Button ID="Button14" runat="server" Text="Stock Report" Width="100%"
                                    Font-Bold="false" SkinID="skinButtonCol2" OnClientClick="window.open('ReportXLStock.aspx','CSTSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=310,width=610,left=400,top=220, scrollbars=no');" />
                            </td>
                        </tr>--%>
                    </table>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
