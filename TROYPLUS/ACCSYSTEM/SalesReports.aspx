<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="SalesReports.aspx.cs" Inherits="SalesReports" Title="Reports > Sales Reports" %>

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
                                    <span>SALES REPORTS</span>
                                </td>
                            </tr>
                        </table>
                    </div>--%>
                    <%--<table class="mainConHd" style="width: 994px;">
                        <tr valign="middle">
                            <td style="font-size: 20px;">
                                SALES REPORTS
                            </td>
                        </tr>
                    </table>--%>
                <div class="mainConBody">
                                <table style="width: 100%;margin: -1px 0px 0px 1px;" cellpadding="2px" cellspacing="2px" class="searchbg">
                                    <tr>
                                        <td style="width: 1%">
                                        </td>
                                        <td style="width: 25%; font-size: 20px; color: #000000;" >
                                             SALES REPORTS
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
                        <tr align="left" id="RowSalesReport" runat="server">
                            <td>
                                <asp:Button runat="server" OnClientClick="window.open('Salesreport.aspx ','Summary', 'toolbar=no,status=no,menu=no,location=no,height=230,width=500,left=420,top=230,resizable=yes, scrollbars=yes');"
                                    ID="ctl00_cplhControlPanel_lnkSalesReport" Text="Daily" Width="100%" Font-Bold="false"
                                    SkinID="skinButtonCol2"></asp:Button>
                            </td>
                        </tr>
                        <%--<tr align="left" id="RowSalesPerDealer" runat="server">
                            <td>
                                <asp:Button runat="server" OnClientClick="window.open('SalesPerformanceDealerReport.aspx ','Summary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=310,width=500,left=425,top=220, scrollbars=yes');"
                                    ID="lnkDealer" Text="Sales Performance Dealer Report" Width="100%" Font-Bold="false"
                                    SkinID="skinButtonCol2"></asp:Button>
                            </td>
                        </tr>
                        <tr align="left" id="RowSalesPerExec" runat="server">
                            <td>
                                <asp:Button runat="server" OnClientClick="window.open('SalesPerformanceExecutiveReport.aspx ','Summary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=230,width=500,left=425,top=220, scrollbars=yes');"
                                    ID="llnkExec" Text="Sales Performance Executive Report" Width="100%" Font-Bold="false"
                                    SkinID="skinButtonCol2"></asp:Button>
                            </td>
                        </tr>--%>
                        <tr align="center" id="RowSalesSumm" runat="server">
                            <td>
                                <asp:Button ID="btnSalesSummary" runat="server" Text="Summary" Width="100%" Font-Bold="false"
                                    SkinID="skinButtonCol2" OnClientClick="window.open('SalesSummaryReport.aspx ','Summary', 'toolbar=no,status=yes,menu=no,location=no,resizable=yes,height=310,width=550,left=425,top=220, scrollbars=yes');" />
                            </td>
                        </tr>
                        <%--<tr align="center" id="RowSalesVATCST" runat="server">
                            <td>
                                <asp:Button ID="btnSalesVATCSTSummary" runat="server" Text="Sales VAT/CST Summary Report"
                                    Width="100%" Font-Bold="false" SkinID="skinButtonCol2" OnClientClick="window.open('SalesVatCstSummaryReport.aspx ','Summary', 'toolbar=no,status=yes,menu=no,location=no,height=310,width=500,left=425,top=220,resizable=yes, scrollbars=yes');" />
                            </td>
                        </tr>--%>
                        <%--<tr align="center" id="Tr1" runat="server">
                            <td>
                                <asp:Button ID="Button1" runat="server" Text="Sales Level Report" Width="100%" Font-Bold="false"
                                    SkinID="skinButtonCol2" OnClientClick="window.open('ProductLevelReport.aspx ','Summary', 'toolbar=no,status=no,menu=no,location=no,height=210,width=500,left=420,top=250,resizable=yes, scrollbars=yes');" />
                            </td>
                        </tr>--%>
                        <%--<tr align="center" id="Tr2" runat="server">
                            <td>
                                <asp:Button ID="Button2" runat="server" Text="New Sales Report" Width="100%" Font-Bold="false"
                                    SkinID="skinButtonCol" OnClientClick="window.open('Stock.aspx ','Summary', 'toolbar=no,status=yes,menu=no,location=no,resizable=yes,height=700,width=1000,left=210,top=10, scrollbars=yes');" />
                            </td>
                        </tr>--%>
                        <tr align="left" id="RowMissingDC" runat="server">
                            <td>
                                <asp:Button runat="server" Width="100%" Font-Bold="false" SkinID="skinButtonCol2"
                                    OnClientClick="window.open('ReportXLMissingDC.aspx','Summary', 'toolbar=no,status=no,menu=no,location=no,height=210,width=500,left=420,top=250 ,resizable=yes, scrollbars=yes');"
                                    ID="Button20" Text="Missing DC Report"></asp:Button>
                            </td>
                        </tr>
                        <tr align="left" id="Tr2" runat="server">
                            <td>
                                <asp:Button ID="Button2" runat="server" Text="Sales Comprehency Report" Width="100%"
                                    Font-Bold="false" SkinID="skinButtonCol2" OnClientClick="window.open('ReportExlSales.aspx','CSTSummary', 'toolbar=no,status=no,menu=no,location=no,height=555,width=805,resizable=yes,left=210,top=100, scrollbars=yes');" />
                            </td>
                        </tr>
                        <tr align="left" id="Tr12" runat="server">
                            <td>
                                <asp:Button ID="Button15" runat="server" Text="Sales TurnOver Report" Width="100%"
                                    Font-Bold="false" SkinID="skinButtonCol2" OnClientClick="window.open('ReportXlSal.aspx','CSTSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=270,width=500,left=425,top=220, scrollbars=yes');" />
                            </td>
                        </tr>
                        <tr align="left" id="Tr13" runat="server">
                            <td>
                                <asp:Button ID="Button16" runat="server" Text="Total Sales Report" Width="100%"
                                    Font-Bold="false" SkinID="skinButtonCol2" OnClientClick="window.open('ReportXlTotalSales.aspx','CSTSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=320,width=500,left=425,top=220, scrollbars=yes');" />
                            </td>
                        </tr>
                        <tr align="left" id="Tr15" runat="server">
                            <td>
                                <asp:Button ID="Button18" runat="server" Text="Zero / 1 Rs Sales Report" Width="100%"
                                    Font-Bold="false" SkinID="skinButtonCol2" OnClientClick="window.open('ReportXlZeroSales.aspx','CSTSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=290,width=500,left=425,top=220, scrollbars=yes');" />
                            </td>
                        </tr>
                        <%--<tr align="left" id="Tr122" runat="server">
                            <td>
                                <asp:Button ID="BtnCreditBill" runat="server" Text="Credit Bill Details Report"
                                    Width="100%" Font-Bold="false" SkinID="skinButtonCol2" OnClientClick="window.open('ReportXLCreditBill.aspx ','Summary', 'toolbar=no,status=no,menu=no,location=no,height=310,width=500,left=425,top=220 ,resizable=yes, scrollbars=yes');">
                                </asp:Button>
                            </td>
                        </tr>--%>
                    </table>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
