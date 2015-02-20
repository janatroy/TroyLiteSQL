<%@ Page Title="Complaint Reports" Language="C#" MasterPageFile="~/PageMaster.master"
    AutoEventWireup="true" CodeFile="ComplaintReports.aspx.cs" Inherits="ComplaintReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    
    <table style="width: 100%">
        <tr style="width: 100%">
            <td style="width: 100%">
            <%--<table class="mainConHd" style="width: 994px;">
                                <tr valign="middle">
                                    <td style="font-size: 20px;">
                                        COMPLAINT REPORTS
                                    </td>
                                </tr>
                            </table>--%>
        <%--<div class="mainConHd">
            <table cellspacing="0" cellpadding="0" border="0">
                <tr valign="middle">
                    <td>
                        <span>COMPLAINT REPORTS</span>
                    </td>
                </tr>
            </table>
        </div>--%>
        <div class="mainConBody">
                                <table style="width: 100%;margin: -1px 0px 0px 1px;" cellpadding="2px" cellspacing="2px" class="searchbg">
                                    <tr>
                                        <td style="width: 1%">
                                        </td>
                                        <td style="width: 25%; font-size: 20px; color: #000000;" >
                                             COMPLAINT REPORTS
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
                        <tr align="left" id="RowComplaintreport" runat="server">
                            <td>
                                <asp:Button ID="Button7" runat="server" Text="Complaint Report" Width="100%" Font-Bold="false"
                                    SkinID="skinButtonCol" OnClientClick="window.open('ComplaintReport.aspx ','billSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=1100,width=900,left=210,top=10, scrollbars=yes');" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
