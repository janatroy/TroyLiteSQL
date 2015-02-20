<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="AccReports.aspx.cs" Inherits="AccReports" Title="Reports > Stock Reports" %>

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
                        <span>ACCOUNT REPORTS</span>
                    </td>
                </tr>
            </table>
        </div>--%>
        <%--<table class="mainConHd" style="width: 994px;">
                                <tr valign="middle">
                                    <td style="font-size: 20px;">
                                        ACCOUNT REPORTS
                                    </td>
                                </tr>
                            </table>--%>
        <div class="mainConBody">
                                <table style="width: 100%;margin: -1px 0px 0px 1px;" cellpadding="2px" cellspacing="2px" class="searchbg">
                                    <tr>
                                        <td style="width: 1%">
                                        </td>
                                        <td style="width: 25%; font-size: 20px; color: #000000;" >
                                             ACCOUNT REPORTS
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
                        <tr align="left" id="RowTrialBalance" runat="server">
                            <td>
                                <asp:Button ID="lnkBtnTrailBal" runat="server" Text="Trial Balance Report" Width="100%"
                                    Font-Bold="false" OnClientClick="window.open('Trialbalance.aspx ','trialBalance', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=220,width=500,left=425,top=220, scrollbars=yes');"
                                    SkinID="skinButtonCol2" />
                            </td>
                        </tr>
                        <tr align="left" id="RowBalanceSheet" runat="server">
                            <td>
                                <asp:Button ID="lnkBtnBalSheet" runat="server" Text="Balance Sheet Report" Width="100%"
                                    Font-Bold="false" SkinID="skinButtonCol2" OnClientClick="window.open('BalanceSheet.aspx ','trialBalance', 'toolbar=no,status=no,menu=no,location=no,height=230,width=500,left=425,top=220,resizable=yes, scrollbars=yes');" />
                            </td>
                        </tr>
                        <tr align="left" id="RowProfitLoss" runat="server">
                            <td>
                                <asp:Button ID="lnkBtnProfitLoss" runat="server" Text="Profit Loss Report" Width="100%"
                                    Font-Bold="false" SkinID="skinButtonCol2" OnClientClick="window.open('ProfitAndLossReport.aspx ','trialBalance', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=230,width=500,left=425,top=220, scrollbars=yes');" />
                            </td>
                        </tr>
                        <tr align="left" id="RowBankStatement" runat="server">
                            <td>
                                <asp:Button runat="server" Width="100%" Font-Bold="false" SkinID="skinButtonCol2"
                                    OnClientClick="window.open('BankStatementReport.aspx ','Summary', 'toolbar=no,status=no,menu=no,location=no,height=260,width=500,left=425,top=220,resizable=yes, scrollbars=yes');"
                                    ID="ctl00_cplhControlPanel_lnkBankStmt" Text="Bank Statement"></asp:Button>
                            </td>
                        </tr>
                        <tr align="left" id="RowDayBook" runat="server">
                            <td>
                                <asp:Button runat="server" OnClientClick="window.open('DayBookReport.aspx ','Summary', 'toolbar=no,status=no,menu=no,location=no,height=230,width=500,left=425,top=220,resizable=yes, scrollbars=yes');"
                                    ID="ctl00_cplhControlPanel_lnkDayBook" Text="Day Book Report" Width="100%" Font-Bold="false"
                                    SkinID="skinButtonCol2"></asp:Button>
                            </td>
                        </tr>
                        <tr align="left" id="RowLedgerReport" runat="server">
                            <td>
                                <asp:Button runat="server" OnClientClick="window.open('LedgerReport.aspx ','Summary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=340,width=500,left=425,top=220, scrollbars=yes');"
                                    ID="ctl00_cplhControlPanel_lnkLeder" Text="Ledger Report" Width="100%" Font-Bold="false"
                                    SkinID="skinButtonCol2"></asp:Button>
                            </td>
                        </tr>
                        <%--<tr align="left" id="Tr1" runat="server">
                            <td>
                                <asp:Button runat="server" OnClientClick="window.open('LedgerMultipleReport.aspx ','Summary', 'toolbar=no,status=no,menu=no,location=no,height=1000,width=900,resizable=yes,left=210,top=10, scrollbars=yes');"
                                    ID="Button1" Text="Ledger Report (Multiple Selection)" Width="100%" Font-Bold="false"
                                    SkinID="skinButtonCol2"></asp:Button>
                            </td>
                        </tr>--%>
                        <tr align="left" id="RowCashAccount" runat="server">
                            <td>
                                <asp:Button runat="server" OnClientClick="window.open('CashAccountReport.aspx ','Summary', 'toolbar=no,status=no,menu=no,location=no,height=210,width=500,left=425,top=220,resizable=yes, scrollbars=yes');"
                                    ID="ctl00_cplhControlPanel_lnkCashAccount" Text="Cash Account Report" Width="100%"
                                    Font-Bold="false" SkinID="skinButtonCol2"></asp:Button>
                            </td>
                        </tr>
                        <tr align="left" id="Tr14" runat="server">
                            <td>
                                <asp:Button ID="Button17" runat="server" Text="Sales Tax Filing Report" Width="100%"
                                    Font-Bold="false" SkinID="skinButtonCol2" OnClientClick="window.open('ReportXlSalesTax.aspx','CSTSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=340,width=500,left=425,top=220, scrollbars=yes');" />
                            </td>
                        </tr>
                        <%--<tr align="left" id="Tr1" runat="server">
                            <td>
                                <asp:Button ID="Button1" runat="server" Text="Flash Report" Width="100%"
                                    Font-Bold="false" SkinID="skinButtonCol2" OnClientClick="window.open('ReportXlFlash.aspx','CSTSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=220,width=500,left=425,top=220, scrollbars=yes');" />
                            </td>
                        </tr>--%>
                        <tr align="left" id="Tr11" runat="server">
                            <td>
                                <asp:Button ID="Button11" runat="server" Text="Expense Report" Width="100%"
                                    Font-Bold="false" SkinID="skinButtonCol2" OnClientClick="window.open('ReportXlExpense.aspx','CSTSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=290,width=500,left=425,top=220, scrollbars=yes');" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
