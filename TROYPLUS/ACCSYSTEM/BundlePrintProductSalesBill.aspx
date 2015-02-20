﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BundlePrintProductSalesBill.aspx.cs" Inherits="BundlePrintProductSalesBill" %>
<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sales Bill</title>
    <link rel="Stylesheet" href="App_Themes/DefaultTheme/DefaultTheme.css" />  
    <script type="text/javascript">
  function CallPrint(strid)
  {
      var prtContent = document.getElementById(strid);
      var WinPrint = window.open('','','letf=0,top=0,width=600,height=400,toolbar=0,scrollbars=1,status=0');
      WinPrint.document.write(prtContent.innerHTML);
      WinPrint.document.close();
      WinPrint.focus();
      WinPrint.print();
      WinPrint.close();

}

</script>
</head>
<body style="font-family:'Trebuchet MS';font-size:11px; ">
    <form id="form1" runat="server">
   
    <input type="button" value="Print " id="Button1" runat="Server" 
             onclick="javascript:CallPrint('divPrint')" class="button"/>&nbsp;
     <asp:Button ID="btnBack" Text="Back" runat="server" onclick="btnBack_Click" /><br /><br />
     <table width="600px" border="0" cellpadding="2" cellspacing="0"   style="font-family:'Trebuchet MS'; font-size:11px;" >
      <tr>
       
       <td align="left">
       <table width="50%" cellpadding="2" cellspacing="2" >
       <tr>
       <td colspan="2"><b>Sales Bill Details Entry</b></td>
       </tr>
       <tr>
       <td>Copy</td>
       <td align="left">&nbsp;&nbsp;<asp:TextBox ID="txtCopy" runat="server" Text="Customer Copy" CssClass="lblFont" width="120px" ></asp:TextBox></td>
       
       </tr>
       </table>
       <div id="dvPF2" runat="server" visible="false">
       <table width="50%" cellpadding="2" cellspacing="2" >
              <tr>
       <td>Boxes</td>
       <td align="left"><asp:TextBox ID="txtBoxes" runat="server" Text="0" CssClass="lblFont" width="120px" ></asp:TextBox></td>
       
       </tr>
              <tr>
       <td>Lorry</td>
       <td align="left"><asp:TextBox ID="txtLorryName" runat="server" Text="" CssClass="lblFont" width="120px" ></asp:TextBox></td>
       
       </tr>
       <tr>
       <td colspan="2">&nbsp;<asp:Button Text="Enter" ID="btnCopy"  runat="server" onclick="btnCopy_Click" /></td>
       </tr>
       </table>
       </div>
           
           
        </td>
      </tr>
      </table><br />
      <div id="divPrint" style="font-family:'Trebuchet MS'; font-size:11px;  " >
        
        <%--Start Header--%>
        <center><asp:Label Font-Bold="true" ID="lblBillCopy" style="font-family:'Trebuchet MS'; font-size:11px;" runat="server" CssClass="lblFont"></asp:Label></center>
        <div id="dvHeaderPF2" runat="server" visible="false">
<br />
        <table border="0" width="700px" style="font-family:'Trebuchet MS'; font-size:12px;">
        <tr>
        <td></td>
        <td>
        <table width="100%">
        <tr>
        <td ></td>
        <td align="right"><asp:Label ID="lblTinNumberPF2" runat="server" CssClass="lblFont"></asp:Label></td>
        </tr>
        <tr>
        <td></td>
        <td align="right"><asp:Label ID="lblCSTNumberPF2" runat="server" CssClass="lblFont"></asp:Label></td>
        </tr>
        </table>
        
        </td>
        </tr>
        <tr>
        <td colspan="2" height="40px">&nbsp;</td>
        </tr>
        
        <tr>
         <td align="left" width="350px">
          <table  border="0" style="font-family:'Trebuchet MS'; font-size:14px;">
                                         <tr><td>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblSupplierPF2" runat="server" Font-Bold="true" /></td></tr>
                                         <tr><td><asp:Label ID="lblSuppAdd1PF2" runat="server"></asp:Label></td></tr>
                                        <tr><td><asp:Label ID="lblSuppAdd2PF2" runat="server"></asp:Label></td></tr>
                                        <tr><td><asp:Label ID="lblSuppAdd3PF2" runat="server"></asp:Label></td></tr>
                                        <tr><td><asp:Label ID="lblSuppPhPF2" runat="server"></asp:Label></td></tr>
                                        <tr><td> &nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;<asp:Label ID="lblSuppTin" runat="server"></asp:Label></td></tr>
                                </table>
         </td>
          <td>
                                <table width="100%" border="0" style="font-family:'Trebuchet MS'; font-size:14px;">
                                <tr>
                                <td width="48%"></td>
                                <td align="right" style="font-weight:bold"><asp:Label ID="lblBillnoPF2" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblBillCopyF2" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                <td></td>
                                <td align="left">&nbsp;&nbsp;&nbsp;<asp:Label ID="lblBillDatePF2" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                <td></td>
                                <td align="left">&nbsp;&nbsp;&nbsp;<asp:Label ID="lblBoxes" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                <td></td>
                                <td align="left">&nbsp;&nbsp;&nbsp;<asp:Label ID="lblLorry" runat="server"></asp:Label></td>
                                </tr>
                                </table>
                                </td>
        </tr>
                                </table>
        </div>
        <div id="dvHeader" runat="server" >
                <table   width="605px" border="0" cellpadding="2" cellspacing="0" class="lblFont"  style="font-family:'Trebuchet MS'; font-size:11px;border:1px solid black;" >
               
                <tr>
                    <td style="border-right:1px solid black;">
                        <table width="300px" style="font-family:'Trebuchet MS'; font-size:11px;">
                             <tr>
                                <td align="left">TIN</td>
                                <td>:</td>
                                <td  align="left"><asp:Label ID="lblTNGST" runat="server"></asp:Label></td>
                                <td align="right">BillNo :  <asp:Label ID="lblBillno" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td align="left">CST#</td>
                                <td>:</td>
                                <td  align="left" colspan="2" ><asp:Label ID="lblGSTno" runat="server"></asp:Label></td>            
                            </tr>
                            <tr>
                             <td valign="top" align="left" colspan="4" width="400px" >
                                 <table style="font-family:'Trebuchet MS'; font-size:11px;">
                                    <tr><td ><asp:Label Font-Bold="true"  ID="lblCompany" runat="server"></asp:Label></td></tr>
                                    <tr><td><asp:Label ID="lblAddress" runat="server"></asp:Label></td></tr>
                                    
                                    <tr><td ><asp:Label ID="lblCity" runat="server" /> - <asp:Label ID="lblPincode" runat="server"></asp:Label></td></tr>
                                    <tr><td ><asp:Label ID="lblState" runat="server"> </asp:Label></td></tr>
                                 </table>
                             </td>  
                            
                            </tr>
                        </table>
                    </td>
                    <td width="50%" align="left" >
                       <table width="100%" style="font-family:'Trebuchet MS'; font-size:11px;">
                        <tr>
                         <td valign="top"  style="background-color:Black;color:White;"><b>VAT TAX INVOICE</b><asp:Label ID="lblHeader" runat="server" Font-Bold="true"  ></asp:Label></td>
                         <td valign="top" align="right">Ph: <asp:Label ID="lblPhone" runat="server"></asp:Label></td>
                         </tr>
                         <tr>
                         <td colspan="2" valign="top" align="right">Date: <asp:Label ID="lblBillDate" runat="server"></asp:Label></td>
                        </tr>  
                        <tr>
                            <td colspan="2" align="left" valign="top">
                                <table border="0" style="font-family:'Trebuchet MS'; font-size:11px;>
                                         <tr><td>M/s:&nbsp;<asp:Label ID="lblSupplier" runat="server" Font-Bold="true" /></td></tr>
                                         <tr><td><asp:Label ID="lblSuppAdd1" runat="server"></asp:Label></td></tr>
                                        <tr><td><asp:Label ID="lblSuppAdd2" runat="server"></asp:Label></td></tr>
                                        <tr><td><asp:Label ID="lblSuppAdd3" runat="server"></asp:Label></td></tr>
                                        <tr><tdPh : <asp:Label ID="lblSuppPh" runat="server"></asp:Label></td></tr>
                                </table>
                            </td>
                        </tr>
                      </table>
                    </td>
                    
                </tr>
                
                </table>
                <%--End Header--%>
         </div>       
                 <%-- Start Product Details --%>
                <div id="dvFormat" runat="server">
                <table   width="600px" border="0" cellpadding="2" cellspacing="0"   style="font-family:'Trebuchet MS'; font-size:11px;border:1px solid black;" >
               
                <tr>
                 <td>
                       <wc:ReportGridView runat="server" BorderWidth="1" ID="gvViswas" CssClass="left" GridLines="Both" AlternatingRowStyle-CssClass="even"
                         AutoGenerateColumns="false" PrintPageSize="10" AllowPrintPaging="true" Visible="false"  
                        Width="600px" cellpadding="2" cellspacing="0" onrowdatabound="gvVishwas_RowDataBound" style="font-family:'Trebuchet MS'; font-size:11px;  ">
               
                            <PageHeaderTemplate>
                                <br />
                                 
                                <br />
                            </PageHeaderTemplate>
                            <Columns>
                              <asp:BoundField ItemStyle-HorizontalAlign="right" ItemStyle-Width="25px"   DataFormatString="{0:f2}"  DataField="Rate" HeaderText="Rate"/>
                              <asp:BoundField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="410px"                              DataField="Particulars" HeaderText="Particulars"/>
                              <asp:BoundField ItemStyle-HorizontalAlign="right" ItemStyle-Width="40px"  DataFormatString="{0:f2}"   DataField="NetRate" HeaderText="NetRate"/>
                              <asp:BoundField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25px"    DataField="Coir" HeaderText="Coir"/>
                              <asp:BoundField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25px"    DataField="Bundles" HeaderText="No. of Bundles"/>
                              <asp:BoundField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25px"    DataField="Rods" HeaderText="No. of Rods"/>
                              <asp:BoundField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25px"    DataField="Qty" HeaderText="Weight"/>
                              <asp:BoundField ItemStyle-HorizontalAlign="right" ItemStyle-Width="50px"  DataFormatString="{0:f2}"   DataField="Amount" HeaderText="Amount"/>
                            </Columns>            
                            <PagerTemplate>
                            </PagerTemplate>
                             <PageFooterTemplate>
                              <br />
                             </PageFooterTemplate>
             </wc:ReportGridView>   
                       <wc:ReportGridView runat="server" BorderWidth="1" ID="gvVishwasCement" CssClass="left" GridLines="Both" AlternatingRowStyle-CssClass="even"
                         AutoGenerateColumns="false" PrintPageSize="45" AllowPrintPaging="true" Visible="false"  
                        Width="600px" onrowdatabound="gvVishwasCement_RowDataBound" style="font-family:'Trebuchet MS'; font-size:11px;  ">
               
                            <PageHeaderTemplate>
                                <br />
                                 
                                <br />
                            </PageHeaderTemplate>
                            <Columns>
                              <asp:BoundField ItemStyle-HorizontalAlign="right" ItemStyle-Width="40px"   DataFormatString="{0:f2}"  DataField="Rate" HeaderText="Rate"/>
                              <asp:BoundField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="410px"                              DataField="Particulars" HeaderText="Particulars"/>
                              <asp:BoundField ItemStyle-HorizontalAlign="right" ItemStyle-Width="40px"  DataFormatString="{0:f2}"   DataField="NetRate" HeaderText="NetRate"/>
                              <asp:BoundField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40px"    DataField="Coir" HeaderText="Coir"/>
                              <asp:BoundField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40px"    DataField="Qty" HeaderText="No of Bags"/>
                              <asp:BoundField ItemStyle-HorizontalAlign="right" ItemStyle-Width="70px"  DataFormatString="{0:f2}"   DataField="Amount" HeaderText="Amount"/>
                            </Columns>            
                            <PagerTemplate>
                            </PagerTemplate>
                             <PageFooterTemplate>
                              <br />
                             </PageFooterTemplate>
             </wc:ReportGridView>       
                       <wc:ReportGridView runat="server" BorderWidth="1" ID="gvGeneral" CssClass="left" GridLines="Both" AlternatingRowStyle-CssClass="even"
                         AutoGenerateColumns="false" PrintPageSize="45" AllowPrintPaging="true" Visible="false"  
                        Width="600px" onrowdatabound="gvGeneral_RowDataBound" style="font-family:'Trebuchet MS'; font-size:11px;  ">
               
                            <PageHeaderTemplate>
                                <br />
                                 
                                <br />
                            </PageHeaderTemplate>
                            <Columns>
                             <asp:TemplateField HeaderText="SI.No" >
                                <ItemTemplate>   
                                    <%# ((GridViewRow)Container).RowIndex + 1%>
                                </ItemTemplate>
                              </asp:TemplateField>
                              
                              <asp:BoundField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="410px"                              DataField="Particulars" HeaderText="Particulars"/>
                              <asp:BoundField ItemStyle-HorizontalAlign="right" ItemStyle-Width="40px"  DataFormatString="{0:f2}"   DataField="NetRate" HeaderText="NetRate" Visible="false"/>
                              <asp:BoundField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40px"    DataField="Qty" HeaderText="Qty"/>
                              <asp:BoundField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40px"    DataField="Coir" HeaderText="Coir"/>
                              <asp:BoundField ItemStyle-HorizontalAlign="right" ItemStyle-Width="40px"   DataFormatString="{0:f2}"  DataField="Rate" HeaderText="Rate"/>
                              <asp:BoundField ItemStyle-HorizontalAlign="right" ItemStyle-Width="70px"  DataFormatString="{0:f2}"   DataField="Amount" HeaderText="Amount"/>
                            </Columns>            
                            <PagerTemplate>
                            </PagerTemplate>
                             <PageFooterTemplate>
                              <br />
                             </PageFooterTemplate>
             </wc:ReportGridView>  
             
                        <div id="dvAmount" runat="server">
                       <table width="600px" border="0" cellpadding="2" cellspacing="0" style="font-family:'Trebuchet MS'; font-size:11px;">
                        <tr>
                            <td width="340px">&nbsp;</td>
                            <td align="left" width="189px">Total (INR))</td><td width="1px">:</td>
                            <td align="right"  width="70px" ><asp:Label ID="lblAmt" CssClass="lblFont" runat="server"></asp:Label></td>
                        </tr>
                        </table>
                  </div>      
                
                        <div id="dvDiscountTotal" runat="server" visible="false">
                            <table width="600px" border="0" cellpadding="2" cellspacing="0" style="font-family:'Trebuchet MS'; font-size:11px;">
                            <tr>
                            <td width="340px">&nbsp;</td>
                            <td align="left" width="189px" >Discount (INR)</td><td width="1px">:</td>
                            <td align="right" width="70px"><asp:Label ID="lblGrandDiscount" CssClass="lblFont" Text="0" runat="server"></asp:Label></td>
                            </tr>
                            </table>
                         </div>
                
                        <div id="dvVatTotal" runat="server" visible="false" >
                        <table width="600px" border="0" cellpadding="2" cellspacing="0" style="font-family:'Trebuchet MS'; font-size:11px;">
                        <tr>
                        <td width="340px">&nbsp;</td>
                            <td  align="left" width="189px">VAT &nbsp;<asp:Label ID="lblVatDisplay" runat="server" CssClass="lblFont" ></asp:Label>&nbsp; (INR)</td><td width="1px">:</td>
                            <td align="right" width="70px"><asp:Label ID="lblGrandVat" CssClass="lblFont" runat="server"></asp:Label></td>
                        </tr>
                        </table>
                        </div>
                       
                        <div id="dvFrgTotal" runat="server" visible="false">
                        <table width="600px" border="0" cellpadding="2" cellspacing="0" style="font-family:'Trebuchet MS'; font-size:11px;">
                        <tr>
                        <td width="343px">&nbsp;</td>
                            <td   align="left" width="186px">Loading / Unloading / Freight (INR)</td><td width="1px">:</td>
                            <td align="right" width="70px"><asp:Label ID="lblFg" CssClass="lblFont" Text="0" runat="server"></asp:Label></td>
                        </tr>
                        </table>
                        </div>
              
                        <div id="dvCSTTotal" runat="server" visible="false">
                        <table width="600px" border="0" cellpadding="2" cellspacing="0" style="font-family:'Trebuchet MS'; font-size:11px;">
                        <tr>
                        <td width="340px">&nbsp;</td>
                            <td align="left" width="189px">CST &nbsp;<asp:Label ID="lblCSTDisplay" runat="server" CssClass="lblFont" ></asp:Label>&nbsp; (INR)</td><td width="1px">:</td>
                            <td align="right" width="70px"><asp:Label ID="lblGrandCst" CssClass="lblFont" Text="0" runat="server"></asp:Label></td>
                        </tr>
                        </table>
                        </div> 
                         <table width="600px" border="0" cellpadding="2" cellspacing="0" style="font-family:'Trebuchet MS'; font-size:11px;">
                        <tr>
                        <td align="left" width="340px"> <asp:label ID="lblRs" runat="server" CssClass="lblFont" Text="Rs. "/></td>
                        <td align="left" width="189px"><asp:label ID="lblGrandlbl" runat="server" text="GRAND TOTAL (INR):"></asp:label></td><td width="1px"></td>
                        <td align="right" width="70px" style="border-top:1px solid black;border-bottom:1px solid black;"><asp:Label ID="lblNetTotal" CssClass="lblFont" runat="server"></asp:Label></td>
                        </tr>
                        </table>
                       
                </td>
                 </tr>
                 
                 </table>
                 </div>
                 
              <%-- End Product Details --%>
              <%--Start Footer--%>
                    
              <div id="dvFooter" runat="server" >
              <table   width="600px" border="0" cellpadding="2" cellspacing="0"   style="font-family:'Trebuchet MS'; font-size:11px;border-right:1px solid black;border-left:1px solid black;border-bottom:1px solid black;" >
              
              
              <tr>
                <td >
                
                  <table width="600px" border="0" cellpadding="2" cellspacing="0"   style="font-family:'Trebuchet MS'; font-size:11px;" >
                    <tr>
                        <td width="350px">&nbsp;<br /><br /><br /></td>   
                        <td align="center" width="250px">For <asp:Label Font-Bold="true"   ID="lblSignCompany" runat="server"></asp:Label> </td>
                    </tr>
                    <tr>
                     <td colspan="2" align="left" style="font-size::xx-small; ">
                      <div id="viswasfooter" visible="false" runat="server" >
                       X Our Liability ceases as soon as the goods leave our place.<br />
                       X Weight recorded at our weighbridge is final.<br />
                       X All disputes subject to Aruppukottai jurisdiction.<br />
                       </div>
                       <div id="dvGeneral" visible="false" runat="server" >
                       X Goods once sold cannot be taken back.<br />
                       X All disputes subject to madurai jurisdiction.<br />
                       </div>
                     </td>
                    </tr>
                 </table>
                </td>
              </tr>
              </table>
              </div>
              
              
              
              <%--End Footer--%>

	      <table   width="700px" border="0" cellpadding="2" cellspacing="0"   style="font-family:'Trebuchet MS'; font-size:11px;border:0px solid black;" >
               
                <tr>
                 <td><br/><br/>
                           <wc:ReportGridView runat="server" BorderWidth="0" ID="gvGeneralPF2" CssClass="left" GridLines="Both" AlternatingRowStyle-CssClass="even"
                         AutoGenerateColumns="false" PrintPageSize="45" ShowHeader="false"  AllowPrintPaging="true" Visible="false"  
                        Width="690px" BorderColor="Transparent"    onrowdatabound="gvGeneralPF2_RowDataBound" style="font-family:'Trebuchet MS'; font-size:14px;  ">
               
                            <PageHeaderTemplate>
                                <br />
                                 
                                <br />
                            </PageHeaderTemplate>
                            <Columns>
                             
                              <asp:BoundField ItemStyle-HorizontalAlign="left" ItemStyle-Width="80px"   DataFormatString="{0:f2}"  DataField="Rate" HeaderText="Rate"/>
                              <asp:BoundField ItemStyle-HorizontalAlign="left" ItemStyle-Width="480px"                              DataField="Particulars" HeaderText="Description"/>
                              
                              <asp:BoundField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px"    DataField="Qty" HeaderText="Qty"/>
                              
                              <asp:TemplateField ItemStyle-HorizontalAlign="right" ItemStyle-Width="110px" >
                              <ItemTemplate>
                              <asp:Label ID="lblAmtt" runat="server" Text='<%#Eval("Amount")%>'></asp:Label>
                              </ItemTemplate>
                              </asp:TemplateField>
                            </Columns>            
                            <PagerTemplate>
                            </PagerTemplate>
                             <PageFooterTemplate>
                              <br /><br/>
                             </PageFooterTemplate>
             </wc:ReportGridView>  <br/>
                        <table width="700px" border="0" cellpadding="2" cellspacing="0" style="font-family:'Trebuchet MS'; font-size:12px;">
                        <tr>
                        <td align="left" width="340px"> </td>
                        <td align="left" width="189px"></td>
                        <td align="right" width="120px"><asp:Label ID="lblTotalPf2" CssClass="lblFont" runat="server" style="font-family:'Trebuchet MS'; font-size:14px;Font-weight:Bold"></asp:Label></td>
                        </tr>
                        </table>
                      </td>
                 </tr>      
                 <tr>
                 <td >
                 
              
                            
                       
                    
                  </td>      
               
                </tr>   
              
              </table>
        </div>
     
    
    </form>
</body>
</html>
