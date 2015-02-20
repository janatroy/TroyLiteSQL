<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="ResMgmReports.aspx.cs" Inherits="ResMgmReports" Title="Reports > Resource Management Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    
    <table style="width: 100%">
        <tr style="width: 100%">
            <td style="width: 100%">
                <%--<table class="mainConHd" style="width: 994px;">
                    <tr valign="middle">
                        <td style="font-size: 20px;">
                            RESOURCE MANAGEMENT REPORTS
                        </td>
                    </tr>
                </table>--%>
                <div class="mainConBody">
                                <table style="width: 100%;margin: -1px 0px 0px 1px;" cellpadding="2px" cellspacing="2px" class="searchbg">
                                    <tr>
                                        <td style="width: 1%">
                                        </td>
                                        <td style="width: 30%; font-size: 20px; color: #000000;" >
                                             RESOURCE MANAGEMENT REPORTS
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
        <%--<div class="mainConHd">
            <table cellspacing="0" cellpadding="0" border="0">
                <tr valign="middle">
                    <td>
                        <span>RESOURCE MANAGEMENT REPORTS</span>
                    </td>
                </tr>
            </table>
        </div>--%>
    
                <div class="mainConDiv" id="IdmainConDiv" align="center">
                    <table align="center" width="50%" style="border: 1px solid #86b2d1; margin: 0 0 0 0px"
                        cellpadding="5" cellspacing="5">
                        <tr align="left" id="RowTimeSheet" runat="server">
                            <td>
                                <asp:Button runat="server" OnClientClick="window.open('TimeSheetReports.aspx ','Summary', 'toolbar=no,status=no,menu=no,location=no,height=250,width=500,left=425,top=220,resizable=yes, scrollbars=yes');"
                                    ID="lnkTMReport" Text="TimeSheet Report" Width="100%" Font-Bold="false" SkinID="skinButtonCol2">
                                </asp:Button>
                            </td>
                        </tr>
                        <tr align="left" id="RowWrokManagement" runat="server">
                            <td>
                                <asp:Button runat="server" OnClientClick="window.open('WorkReport.aspx ','Summary', 'toolbar=no,status=no,menu=no,location=no,height=280,width=500,left=425,top=220,resizable=yes, scrollbars=yes');"
                                    ID="lnkWorkReport" Text="Work Management Report" Width="100%" Font-Bold="false"
                                    SkinID="skinButtonCol2"></asp:Button>
                            </td>
                        </tr>
                        <tr align="left" id="Tr1" runat="server">
                            <td>
                                <asp:Button runat="server" OnClientClick="window.open('ReportExcelProjectManagement.aspx ','Summary', 'toolbar=no,status=no,menu=no,location=no,height=280,width=500,left=425,top=220,resizable=yes, scrollbars=yes');"
                                    ID="Button1" Text="Project Management Report" Width="100%" Font-Bold="false"
                                    SkinID="skinButtonCol2"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
