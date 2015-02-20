<%@ Page Title="Reports > VAT Reports" Language="C#" MasterPageFile="~/PageMaster.master"
    AutoEventWireup="true" CodeFile="VATReports.aspx.cs" Inherits="VATReports" %>

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
                        <span>VAT REPORTS</span>
                    </td>
                </tr>
            </table>
        </div>--%>
                <%--<table class="mainConHd" style="width: 994px;">
                    <tr valign="middle">
                        <td style="font-size: 20px;">
                            VAT REPORTS
                        </td>
                    </tr>
                </table>--%>
                <div class="mainConBody">
                                <table style="width: 100%;margin: -1px 0px 0px 1px;" cellpadding="2px" cellspacing="2px" class="searchbg">
                                    <tr>
                                        <td style="width: 1%">
                                        </td>
                                        <td style="width: 25%; font-size: 20px; color: #000000;" >
                                             VAT REPORTS
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
                        <tr align="left" id="RowVATSummary" runat="server">
                            <td>
                                <asp:Button ID="Button1" runat="server" Text="Summary" OnClientClick="window.open('VATSumaryReport.aspx','trialBalance', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=220,width=500,left=425,top=220, scrollbars=yes');"
                                    Width="100%" Font-Bold="false" SkinID="skinButtonCol2"></asp:Button>
                            </td>
                        </tr>
                        <tr align="left" id="RowVATReconReport" runat="server">
                            <td>
                                <asp:Button ID="Button6" runat="server" Text="Reconciliation" OnClientClick="window.open('VATReconReport.aspx','trialBalance', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=1000,width=900,left=210,top=10, scrollbars=yes');"
                                    Width="100%" Font-Bold="false" SkinID="skinButtonCol2"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
