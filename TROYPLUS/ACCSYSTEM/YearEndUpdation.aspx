<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="YearEndUpdation.aspx.cs" Inherits="YearEndUpdation" Title="Year End Updation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">

 <script src="Scripts/JScriptSales.js" type="text/javascript">
 
 </script>
    
    

    <table style="width: 100%">      
        <tr style="width: 100%">
            <td style="width: 100%">
                <div class="mainConBody">
                    <table style="width: 100%;margin: -1px 0px 0px 1px;" cellpadding="2px" cellspacing="2px" class="searchbg">
                        <tr>
                            <td style="width: 1%">
                            </td>
                            <td style="width: 25%; font-size: 20px; color: #000000;" >
                                    Year End Updation
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
                <table align="center" width="50%" style="border: 1px solid #86b2d1; margin: 0 0 0 0px"
                    cellpadding="1" cellspacing="1">
                    <tr style="height:2px">
                        
                    </tr>
                    <tr align="left" id="RowDatabaseCopy" runat="server">
                        <td>
                            <asp:Button ID="Button19" runat="server" Text="Make Database Copy" Width="100%"
                                Font-Bold="false" SkinID="skinButtonCol2" OnClientClick="window.open('ReportXlAuditdetails.aspx','CSTSummary', 'toolbar=no,status=no,menu=no,location=no,height=310,width=500,left=425,top=220 ,resizable=yes, scrollbars=yes');" />
                        </td>
                    </tr>
                    <tr align="left" id="RowClearEntries" runat="server">
                        <td>
                            <asp:Button ID="Button13" runat="server" Text="Clear Entries" Width="100%"
                                Font-Bold="false" SkinID="skinButtonCol2" OnClientClick="window.open('QueryMaster.aspx','CSTSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=400,width=550,left=425,top=220, scrollbars=yes');" />
                        </td>
                    </tr>
                    <tr align="left" id="RowMasterUpd" runat="server">
                        <td>
                            <asp:Button ID="ctl00_cplhControlPanel_lnkSummreports" runat="server" Text="Master Updation"
                                Width="100%" Font-Bold="false" SkinID="skinButtonCol2" OnClientClick="window.open('AccountSummary.aspx ','Summary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=1000,width=900,left=200,top=10, scrollbars=yes');">
                            </asp:Button>
                        </td>
                    </tr>
                    <tr align="left" id="RowOpenBalUpd" runat="server">
                        <td>
                                <asp:Button ID="Button3" runat="server" Text="Opening Balance Updation" Width="100%" Font-Bold="false"
                                SkinID="skinButtonCol2" OnClientClick="window.open('ReportXlGProfit.aspx ','CSTSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=390,width=610,left=400,top=220, scrollbars=no');" />
                        </td>
                    </tr>
                    <tr align="left" id="RowProductsUpd" runat="server">
                        <td>
                            <asp:Button ID="Button4" runat="server" Text="Category / Brand / Products Updation" Width="100%" Font-Bold="false"
                                SkinID="skinButtonCol2" OnClientClick="window.open('CSTSumaryReport.aspx','CSTSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=310,width=500,left=425,top=220, scrollbars=yes');" />
                        </td>
                    </tr>
                    <tr align="left" id="RowStockUpd" runat="server">
                        <td>
                            <asp:Button ID="Button11" runat="server" Text="Stock Updation" Width="100%"
                                Font-Bold="false" SkinID="skinButtonCol2" OnClientClick="window.open('ReportXlExpense.aspx','CSTSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=310,width=500,left=425,top=220, scrollbars=yes');" />
                        </td>
                    </tr>
                    <%--<tr>
                        <td>
                            <asp:Button ID="Button1" runat="server" Text="Report Runner" Width="100%"
                                Font-Bold="false" SkinID="skinButtonCol2" OnClientClick="window.open('QueryRunner.aspx','CSTSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=310,width=500,left=425,top=220, scrollbars=yes');" />
                        </td>
                    </tr>--%>
                    <tr style="height:2px">
                    </tr>
                </table>
             </td>
        </tr>
        
    </table>
    
     
                        
</asp:Content>
