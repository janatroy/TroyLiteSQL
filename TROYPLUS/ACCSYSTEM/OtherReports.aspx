<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="OtherReports.aspx.cs" Inherits="OtherReports" Title="Other Reports" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">

 <script src="Scripts/JScriptSales.js" type="text/javascript">
 
 </script>
    
    

    <table style="width: 100%">      
        <tr style="width: 100%">
            <td style="width: 100%">
                
                   <%--<div class="mainConHd" style="width:994px">
                        <table cellspacing="0" cellpadding="0" border="0">
                            <tr>
                                <td>
                                    <span>OTHER REPORTS</span>
                                </td>
                            </tr>
                        </table>
                    </div>     --%>
                    <%--<table class="mainConHd" style="width: 994px;">
                                <tr valign="middle">
                                    <td style="font-size: 20px;">
                                        OTHER REPORTS
                                    </td>
                                </tr>
                            </table>--%>
                            <div class="mainConBody">
                                <table style="width: 100%;margin: -1px 0px 0px 1px;" cellpadding="2px" cellspacing="2px" class="searchbg">
                                    <tr>
                                        <td style="width: 1%">
                                        </td>
                                        <td style="width: 25%; font-size: 20px; color: #000000;" >
                                             OTHER REPORTS
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
                        <tr>
                            <td style="padding-left: 5px">
                                <div runat="server" id="divIP">
                                    MAC Address :
                                    <asp:Label ID="lblIP" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="Label1" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="Label2" runat="server"></asp:Label>
                                    <asp:HiddenField ID="hdMAC" runat="server" Value="0" />
                                    <asp:HiddenField ID="hdIsInternetExplorer" runat="server" Value="False" />
                                </div>
                            </td>
                        </tr>
                    </table>
                    <table align="center" width="50%" style="border: 1px solid #86b2d1; margin: 0 0 0 0px"
                        cellpadding="1" cellspacing="1">
                        <tr style="height:2px">
                        
                        </tr>
                        
                        <%--<tr align="left" id="RowExlSales" runat="server">
                            <td>
                                <asp:Button ID="Button21" runat="server" Text="Excel Export - Sales Report" Width="100%"
                                    Font-Bold="false" SkinID="skinButtonCol" OnClientClick="window.open('ReportExlSales.aspx','CSTSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=540,width=700,left=290,top=120, scrollbars=yes');" />
                            </td>
                        </tr> --%> 
                        <%--<tr align="left" id="Tr3" runat="server">
                            <td>
                                <asp:Button ID="Button6" runat="server" Text="YearEndLedgerReport"
                                    Width="100%" Font-Bold="false" SkinID="skinButtonCol2" OnClientClick="window.open('YearEndLedgerReport.aspx ','Summary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=1000,width=900,left=200,top=10, scrollbars=yes');">
                                </asp:Button>
                            </td>
                        </tr> --%>   
                        <%--<tr align="left" id="Tr123" runat="server">
                            <td>
                                <asp:Button ID="Button213" runat="server" Text="Year End Updation"
                                    Width="100%" Font-Bold="false" SkinID="skinButtonCol2" OnClientClick="window.open('NewDBCreation.aspx ','Summary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=1000,width=900,left=200,top=10, scrollbars=yes');">
                                </asp:Button>
                            </td>
                        </tr>--%>
                        <%--<tr align="left" id="Tr2" runat="server">
                            <td>
                                <asp:Button ID="Button5" runat="server" Text="Year End Updation"
                                    Width="100%" Font-Bold="false" SkinID="skinButtonCol2" OnClientClick="window.open('YearEndUpdation.aspx ','Summary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=1000,width=900,left=200,top=10, scrollbars=yes');">
                                </asp:Button>
                            </td>
                        </tr>--%>
                        <%--<tr align="left" id="Tr1" runat="server">
                            <td>
                                <asp:Button ID="Button2" runat="server" Text="Bulk Addition"
                                    Width="100%" Font-Bold="false" SkinID="skinButtonCol2" OnClientClick="window.open('BulkAddition.aspx ','Summary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=1000,width=900,left=200,top=10, scrollbars=yes');">
                                </asp:Button>
                            </td>
                        </tr>--%>
                        <%--<tr align="left" id="Tr2" runat="server">
                            <td>
                                <asp:Button ID="Button5" runat="server" Text="Data Backup"
                                    Width="100%" Font-Bold="false" SkinID="skinButtonCol2" OnClientClick="window.open('DataBackupAndRestore.aspx ','Summary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=1000,width=900,left=200,top=10, scrollbars=yes');">
                                </asp:Button>
                            </td>
                        </tr>--%>
                        <%--<tr align="left" id="RowBusinessTrans" runat="server">
                            <td>
                                <asp:Button ID="ctl00_cplhControlPanel_lnkSummreports" runat="server" Text="Business Daily Transaction"
                                    Width="100%" Font-Bold="false" SkinID="skinButtonCol2" OnClientClick="window.open('AccountSummary.aspx ','Summary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=1000,width=900,left=200,top=10, scrollbars=yes');">
                                </asp:Button>
                            </td>
                        </tr>--%>
                        <tr align="left" id="RowGrossProfit" runat="server">
                            <td>
                                <%--<asp:Button ID="Button3" runat="server" Text="Gross Profit" Width="100%" Font-Bold="false"
                                    SkinID="skinButtonCol" OnClientClick="window.open('ReportXlGrossprofit.aspx ','CSTSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=360,width=500,left=350,top=100, scrollbars=no');" />
--%>
                                    <%--<asp:Button ID="Button14" runat="server" Text="Gross Profit" Width="100%" Font-Bold="false"
                                    SkinID="skinButtonCol" OnClientClick="window.open('GrossProfitSummaryReport.aspx ','billSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=1000,width=1200,left=10,top=10, scrollbars=yes');" />--%>

                                    <asp:Button ID="Button3" runat="server" Text="Gross Profit" Width="100%" Font-Bold="false"
                                    SkinID="skinButtonCol2" OnClientClick="window.open('ReportXlGProfit.aspx ','CSTSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=390,width=610,left=400,top=220, scrollbars=no');" />
                            
                            </td>

                        </tr>
                        <tr align="left" id="RowCSTSummary" runat="server">
                            <td>
                                <asp:Button ID="Button4" runat="server" Text="CST Summary" Width="100%" Font-Bold="false"
                                    SkinID="skinButtonCol2" OnClientClick="window.open('CSTSumaryReport.aspx','CSTSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=230,width=500,left=425,top=220, scrollbars=yes');" />
                            </td>
                        </tr>
                        <%--<tr align="left" id="Tr1" runat="server">
                            <td>
                                <asp:Button ID="Button1" runat="server" Text="Excel Export - Credit Note Report"
                                    Width="100%" Font-Bold="false" SkinID="skinButtonCol" OnClientClick="window.open('ReportExcelCreditnote.aspx ','Summary', 'toolbar=no,status=no,menu=no,location=no,width=900,height=1000,left=210,top=10 ,resizable=yes, scrollbars=yes');">
                                </asp:Button>
                            </td>
                        </tr>--%>
                        <%--<tr align="left" id="Tr2" runat="server">
                            <td>
                                <asp:Button ID="Button2" runat="server" Text="Excel Export - Customer Report" Width="100%"
                                    Font-Bold="false" SkinID="skinButtonCol" OnClientClick="window.open('ReportExcelCustomers.aspx ','billSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=1000,width=1200,left=10,top=10, scrollbars=yes');" />
                            </td>
                        </tr>--%>
                        <%--<tr align="left" id="Tr3" runat="server">
                            <td>
                                <asp:Button ID="Button5" runat="server" Text="Excel Export - Debit Note Report" Width="100%"
                                    Font-Bold="false" SkinID="skinButtonCol" OnClientClick="window.open('ReportExcelDebitnote.aspx','CSTSummary', 'toolbar=no,status=no,menu=no,location=no,width=900,height=1000,left=210,top=10 ,resizable=yes, scrollbars=yes');" />
                            </td>
                        </tr>--%>
                        
                        <%--<tr align="left" id="Tr1" runat="server">
                            <td>
                                <asp:Button ID="Button2" runat="server" Text="Excel Export - Bank Reconciliation" Width="100%"
                                    Font-Bold="false" SkinID="skinButtonCol" OnClientClick="window.open('ReportXlBankRecon.aspx','CSTSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=310,width=550,left=430,top=150, scrollbars=yes');" />
                            </td>
                        </tr>--%>
                        <%--<tr align="left" id="Tr4" runat="server">
                            <td>
                                <asp:Button ID="Button6" runat="server" Text="Excel Export - Journel Report" Width="100%"
                                    Font-Bold="false" SkinID="skinButtonCol" OnClientClick="window.open('ReportExcelJournel.aspx','CSTSummary', 'toolbar=no,status=no,menu=no,location=no,width=900,height=1000,left=210,top=10 ,resizable=yes, scrollbars=yes');" />
                            </td>
                        </tr>--%>
                        <%--<tr align="left" id="Tr5" runat="server">
                            <td>
                                <asp:Button ID="Button7" runat="server" Text="Excel Export - Payment Report" Width="100%"
                                    Font-Bold="false" SkinID="skinButtonCol" OnClientClick="window.open('ReportExcelPayments.aspx','CSTSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=1000,width=900,left=10,top=10, scrollbars=yes');" />
                            </td>
                        </tr>--%>
                        <%--<tr align="left" id="Tr6" runat="server">
                            <td>
                                <asp:Button ID="Button8" runat="server" Text="Excel Export - Purchase Report" Width="100%"
                                    Font-Bold="false" SkinID="skinButtonCol" OnClientClick="window.open('ReportXLPurchase.aspx','CSTSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=340,width=500,left=350,top=100, scrollbars=yes');" />
                            </td>
                        </tr>--%>
                        <%--<tr align="left" id="Tr7" runat="server">
                            <td>
                                <asp:Button ID="Button9" runat="server" Text="Excel Export - Receipts Report" Width="100%"
                                    Font-Bold="false" SkinID="skinButtonCol" OnClientClick="window.open('ReportExcelReceipts.aspx','CSTSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=800,width=900,left=10,top=10, scrollbars=yes');" />
                            </td>
                        </tr>--%>
                        <%--<tr align="left" id="Tr8" runat="server">
                            <td>
                                <asp:Button ID="Button10" runat="server" Text="Excel Export - Sales Report" Width="100%"
                                    Font-Bold="false" SkinID="skinButtonCol" OnClientClick="window.open('ReportXlSales.aspx','CSTSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=360,width=500,left=350,top=100, scrollbars=no');" />
                            </td>
                        </tr>--%>
                        
                       <%-- <tr align="left" id="Tr19" runat="server">
                            <td>
                                <asp:Button ID="Button22" runat="server" Text="Excel Export - Commission Report" Width="100%"
                                    Font-Bold="false" SkinID="skinButtonCol" OnClientClick="window.open('ReportXlCommission.aspx','CSTSummary', 'toolbar=no,status=no,menu=no,location=no,width=900,height=1000,left=210,top=10 ,resizable=yes, scrollbars=yes');" />
                            </td>
                        </tr>--%>
                        
                        
                        <%--<tr align="left" id="Tr15" runat="server">
                            <td>
                                <asp:Button ID="Button18" runat="server" Text="Excel Export - Sales Purchase Comparision Report" Width="100%"
                                    Font-Bold="false" SkinID="skinButtonCol" OnClientClick="window.open('ReportXlSalesPur.aspx','CSTSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=1000,width=900,left=10,top=10, scrollbars=yes');" />
                            </td>
                        </tr>--%>
                        
                        <%--<tr align="left" id="Tr1Flash" runat="server">
                            <td>
                                <asp:Button ID="BtnFlash" runat="server" Text="Excel Export - Flash Report"
                                    Width="100%" Font-Bold="false" SkinID="skinButtonCol" OnClientClick="window.open('ReportXLFlash.aspx ','Summary', 'toolbar=no,status=no,menu=no,location=no,width=900,height=1000,left=210,top=10 ,resizable=yes, scrollbars=yes');">
                                </asp:Button>
                            </td>
                        </tr>--%>
                        <%--<tr align="left" id="Tr16" runat="server">
                            <td>
                                <asp:Button ID="Button19" runat="server" Text="Audit Details Report" Width="100%"
                                    Font-Bold="false" SkinID="skinButtonCol2" OnClientClick="window.open('ReportXlAuditdetails.aspx','CSTSummary', 'toolbar=no,status=no,menu=no,location=no,height=220,width=500,left=425,top=220 ,resizable=yes, scrollbars=yes');" />
                            </td>
                        </tr>--%>
                        
                        
                        <%--<tr align="left" id="Tr17" runat="server">
                            <td>
                                <asp:Button ID="BnAbsolute" runat="server" Text="Obsolute Item List Report" Width="100%"
                                    Font-Bold="false" SkinID="skinButtonCol" OnClientClick="window.open('ReportXlAbsolute.aspx','CSTSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=310,width=500,left=425,top=220, scrollbars=yes');" />
                            </td>
                        </tr>--%>
                        
                        
                        <%--<tr align="left" id="Tr10" runat="server">
                            <td>
                                <asp:Button ID="Button12" runat="server" Text="Excel Export - Suppliers Report" Width="100%"
                                    Font-Bold="false" SkinID="skinButtonCol" OnClientClick="window.open('ReportExcelSuppliers.aspx','CSTSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=1000,width=900,left=250,top=100, scrollbars=yes');" />
                            </td>
                        </tr>--%>
                        <tr align="left" id="RowCquery" runat="server">
                            <td>
                                <asp:Button ID="Button13" runat="server" Text="Create Own SQL Report" Width="100%"
                                    Font-Bold="false" SkinID="skinButtonCol2" OnClientClick="window.open('QueryMaster.aspx','CSTSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=400,width=550,left=425,top=220, scrollbars=yes');" />
                            </td>
                        </tr>
			<tr align="left" id="Tr222" runat="server">
                            <td>
                                <asp:Button ID="Button2" runat="server" Text="Bulk Rate Updation"
                                    Width="100%" Font-Bold="false" SkinID="skinButtonCol2" OnClientClick="window.open('BulkProductUpdation.aspx','billSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=400,width=480,left=425,top=150, scrollbars=yes');">
                                </asp:Button>
                            </td>
                        </tr>
                        <tr align="left" id="Tr1" runat="server">
                            <td>
                                <asp:Button ID="Button1" runat="server" Text="Flash" Width="100%"
                                    Font-Bold="false" SkinID="skinButtonCol2" OnClientClick="window.open('ReportXLFlash.aspx','CSTSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=400,width=550,left=425,top=220, scrollbars=yes');" />
                            </td>
                        </tr>
                        <%--<tr>
                            <td>
                                <asp:Button ID="Button1" runat="server" Text="Report Runner" Width="100%"
                                    Font-Bold="false" SkinID="skinButtonCol2" OnClientClick="window.open('QueryRunner.aspx','CSTSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=310,width=500,left=425,top=220, scrollbars=yes');" />
                            </td>
                        </tr>--%>
                        <%--<tr>
                            <td>
                                <asp:Button ID="ReportRunner" runat="server" Text="Report Runner" Width="100%"
                                    Font-Bold="false" SkinID="skinButtonCol"  OnClick="ReportRunner_Click" />
                            </td>
                        </tr>--%>
			<tr align="left" id="Tr16" runat="server">
                            <td>
                                <asp:Button ID="Button19" runat="server" Text="Analysis Details Report" Width="100%"
                                    Font-Bold="false" SkinID="skinButtonCol2" OnClientClick="window.open('ReportXlAnalysis.aspx','CSTSummary', 'toolbar=no,status=no,menu=no,location=no,height=220,width=500,left=425,top=220 ,resizable=yes, scrollbars=yes');" />
                            </td>
                        </tr>
                        <tr style="height:2px">
                        </tr>
                        <tr>
                            <td>
                                 <asp:GridView ID="GridView1" runat="server"

                                AutoGenerateColumns = "false" Font-Names = "Arial"

                                Font-Size = "11pt" AlternatingRowStyle-BackColor = "#C2D69B" 

                                HeaderStyle-BackColor = "green" AllowPaging ="true"  

                                >

                               <Columns>

                                <asp:BoundField ItemStyle-Width = "150px" DataField = "ItemCode"

                                   HeaderText = "ItemCode" />

                                <asp:BoundField ItemStyle-Width = "150px" DataField = "ProductName"

                                   HeaderText = "ProductName"/>
                                   <asp:BoundField ItemStyle-Width = "150px" DataField = "Stock"

                                   HeaderText = "Stock"/>
                                   <asp:BoundField ItemStyle-Width = "150px" DataField = "QuantityMismatch"

                                   HeaderText = "QuantityMismatch"/>

                               </Columns>

                            </asp:GridView>
                            </td>
                            </tr>
                            <tr>
                            <td >
                                <%--<asp:GridView ID="GridView1" runat="server" 
                                    AutoGenerateColumns="true"
                                    >
                                </asp:GridView>--%>
                                <%--<asp:GridView ID="GridView1" AutoGenerateColumns = "false" runat="server" Font-Names = "Arial"

                                    Font-Size = "11pt" AlternatingRowStyle-BackColor = "#C2D69B" 

                                    HeaderStyle-BackColor = "green" AllowPaging ="true"  

                                     Width="100%"
                                     >
                                    <Columns>
                                        <asp:TemplateField HeaderText="Item Code" HeaderStyle-BorderColor="Blue" HeaderStyle-ForeColor="Black">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRefno" runat="server" Text='<%# Eval("ItemCode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Product Name" HeaderStyle-BorderColor="Blue" HeaderStyle-ForeColor="Black">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDt" runat="server" Text='<%# Eval("ProductName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Stock" HeaderStyle-BorderColor="Blue" HeaderStyle-ForeColor="Black">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDebit" runat="server" Text='<%# Eval("Stock") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PaymentMode" HeaderStyle-BorderColor="Blue" HeaderStyle-ForeColor="Black">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPayMode" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="QuantityMismatch" HeaderStyle-BorderColor="Blue" HeaderStyle-ForeColor="Black">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBank" runat="server" Text='<%# Eval("QuantityMismatch") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                Total :
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        
                                    </Columns>
                                    
                            </asp:GridView>--%>
                                    
                                <asp:GridView ID="GridView2" runat="server" 
                                    AutoGenerateColumns="true"
                                    >
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%">      
                        
                    </table>
            </td>
        </tr>
        
    </table>
    
     
                        
</asp:Content>
