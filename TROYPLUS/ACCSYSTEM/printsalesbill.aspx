<%@ Page Language="C#" AutoEventWireup="true" CodeFile="printsalesbill.aspx.cs" Inherits="printsalesbill" %>

<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Print Preview</title>
    <script type="text/javascript">
        function CallPrint(strid) {
            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('', '', 'letf=0,top=0,width=600,height=400,toolbar=0,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();

        }
        function remove() {/*
var myObject;
myObject = new ActiveXObject("Scripting.FileSystemObject");
var f = myObject.GetFile(document.getElementById("hdFile").value);
alert("Event Firing" +f);
f.Delete();
*/
        }
    </script>
</head>
<body onunload="remove()">
    <form id="form1" runat="server">
    <div>
        <br />
        <input type="button" value="Print " id="Button1" runat="Server" onclick="javascript:CallPrint('divPrint')"
            class="button" />&nbsp;
        <asp:Button ID="btnBack" Text="Back" runat="server" OnClick="btnBack_Click" /><br />
        <br />
        <div id="divPrint" style="font-family: 'Trebuchet MS'; font-size: 11px;">
            <%--<table width="600px" border="0" style="font-family:'Trebuchet MS'; font-size:11px;  " cellspacing="0"   >
  <tr>
  <td colspan="3"><br /><h5><asp:Label ID="lblHeader" runat="server" Text="Sales Invoice"></asp:Label></h5> </td>
  
  </tr>
 <tr>
 <td width="310px"  align="left" valign="top"  style="border-left:solid 1px black ;border-right:solid 1px black;border-bottom:solid 1px black;border-top:solid 1px black   ">
    <table border="0" >
    <tr><td ><asp:Label Font-Bold="true"  ID="lblCompany" runat="server"></asp:Label></td></tr>
    <tr><td><asp:Label ID="lblAddress" runat="server"></asp:Label></td></tr>
    <tr><td>Ph: <asp:Label ID="lblPhone" runat="server"></asp:Label></td></tr>
    <tr><td ><asp:Label ID="lblCity" runat="server" /> - <asp:Label ID="lblPincode" runat="server"></asp:Label></td></tr>
    <tr><td ><asp:Label ID="lblState" runat="server"> </asp:Label></td></tr>
    <tr><tr><td>TIN # : <asp:Label ID="lblTNGST" runat="server"></asp:Label></td></tr></tr>
    <tr><td align="left">GST#: <asp:Label ID="lblGSTno" runat="server"></asp:Label></td></tr>
    </table>
 
 </td>
 <td align="left" width="143px"  valign="top" style="border-right:solid 1px black ;border-bottom:solid 1px black;border-top:solid 1px black   ">Invoice No.<br /><asp:Label ID="lblBillno" runat="server"></asp:Label></td>
 <td align="left" width="147px"  valign="top" style="border-right:solid 1px black ;border-bottom:solid 1px black;border-top:solid 1px black   "> Dated: <asp:Label ID="lblBillDate" runat="server"></asp:Label></td>
 </tr>
 <tr>
 <td align="left" valign="top" style="border-left:solid 1px black ;border-right:solid 1px black;border-bottom:solid 1px black; ">
  <table border="0">
   <tr><td><b>Buyer :</b></td></tr>
    <tr><td><asp:Label ID="lblSupplier" runat="server" Font-Bold="true" /></td></tr>
    <tr><td><asp:Label ID="lblSuppAdd1" runat="server"></asp:Label></td></tr>
    <tr><td><asp:Label ID="lblSuppAdd2" runat="server"></asp:Label></td></tr>
    <tr><td><asp:Label ID="lblSuppAdd3" runat="server"></asp:Label></td></tr>
    <tr><td>Ph : <asp:Label ID="lblSuppPh" runat="server"></asp:Label></td></tr>
    
    </table>
 </td>
 <td colspan="2" style="border-right:solid 1px black;border-bottom:solid 1px black;   " align="left">&nbsp;<asp:Label Visible="false"  ID="lblReason" runat="server"></asp:Label></td>
 
 </tr>
 
   
 
      
           <asp:HiddenField ID="hdPurchase" runat="server" Value="0" />
           <asp:HiddenField ID="hdFile" runat="server" Value="0" />
                       <asp:HiddenField ID="hdFilename" runat="server" Value="0" />
                       <asp:HiddenField ID="hdTotalAmt" runat="server" Value="0" />
                        <asp:HiddenField ID="hdMode" runat="server" Value="New" />
                        <tr><td colspan="3" style="border-left:solid 1px black;border-right:solid 1px black;border-bottom:solid 1px black;   ">
        <wc:ReportGridView runat="server" BorderWidth="1" ID="gvBillDetails" CssClass="left" GridLines="Both" AlternatingRowStyle-CssClass="even"
                     AutoGenerateColumns="false" PrintPageSize="23" AllowPrintPaging="true" 
                Width="600px" onrowdatabound="gvBillDetails_RowDataBound" style="font-family:'Trebuchet MS'; font-size:11px;  ">
               
                <PageHeaderTemplate>
                <br />
                 
            </PageHeaderTemplate>
            
            <Columns>
                 <asp:TemplateField HeaderText="Product Code">
                                  <ItemTemplate>
                                  <%# Eval("itemcode")%>
                                  </ItemTemplate>
                                  <EditItemTemplate>
                                  <asp:label runat="server" ID="lblCode" Text='<%# Eval("itemcode")%>' ></asp:label>
                                  </EditItemTemplate>
                                  </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product Name">
                                  <ItemTemplate>
                                  <%# Eval("ProductName")%>
                                  </ItemTemplate>
                                  <EditItemTemplate>
                                  <asp:label runat="server" ID="lblProdname" Text='<%# Eval("ProductName")%>' ></asp:label>
                                  </EditItemTemplate>
                                  </asp:TemplateField>
                                
                               <asp:TemplateField HeaderText="Description">
                                  <ItemTemplate>
                                  <%# Eval("ProductDesc")%> - <%# Eval("Model")%>
                                  </ItemTemplate>
                                  <EditItemTemplate>
                                  <asp:label runat="server" ID="lblDesc" Text='<%# Eval("ProductDesc")%>' ></asp:label>
                                  </EditItemTemplate>
                                  </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Qty">
                                  <ItemTemplate>
                                  <%# Eval("Qty")%>
                                  </ItemTemplate>
                                  <EditItemTemplate>
                                  <asp:TextBox ID="txtQty" runat="server" Text='<%# Eval("Qty") %>'></asp:TextBox>
                                   </EditItemTemplate> 
                                  <FooterTemplate>
                           
                                 </FooterTemplate> 
                                  </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Rate">
                                  <ItemTemplate>
                                  

                                  <%# Eval("Rate")%>
                                  </ItemTemplate>
                                  <EditItemTemplate>
                                  <asp:TextBox ID="txtRate" runat="server" Text='<%# Eval("Rate") %>'></asp:TextBox>
                                 
                                  
                                   </EditItemTemplate> 
                                  <FooterTemplate>
                             
                                 </FooterTemplate> 
                                  </asp:TemplateField>
                               
                                     <asp:TemplateField HeaderText="Discount (%)">
                                  <ItemTemplate>
                                  <%# Eval("Discount")%>
                                  </ItemTemplate>
                                   <EditItemTemplate>
                                  <asp:Label ID="lblDiscount" runat="server" Text='<%# Eval("Discount") %>'></asp:Label>
                                   </EditItemTemplate> 
                                  <FooterTemplate>
                           
                                 </FooterTemplate> 
                                  </asp:TemplateField>
                           
                              <asp:TemplateField HeaderText="VAT (%)">
                              <ItemTemplate>
                              <%# Eval("vat") %>
                              </ItemTemplate>
                               <EditItemTemplate>
                                  <asp:Label ID="lblVat" runat="server" Text='<%# Eval("vat") %>'></asp:Label>
                                   </EditItemTemplate> 
                              <FooterTemplate>

                             </FooterTemplate> 
                              </asp:TemplateField>
                               
                              <asp:TemplateField HeaderText="Total" FooterStyle-Font-Bold="True">
                                <ItemTemplate>
                                     <%# GetTotal(decimal.Parse(Eval("Qty").ToString()), decimal.Parse(Eval("rate").ToString()), decimal.Parse(Eval("discount").ToString()), decimal.Parse(Eval("vat").ToString()))%>
                                </ItemTemplate>
                             <FooterTemplate>
                               
                             </FooterTemplate> 

<FooterStyle Font-Bold="True"></FooterStyle>
                                </asp:TemplateField> 
                           
            </Columns>            
            <PagerTemplate>
        
            </PagerTemplate>
            <PageFooterTemplate>
               
                <br />
                          
               <%-- Page <%# gvCashDetails.CurrentPrintPage.ToString() %> / <%# gvCashDetails.PrintPageCount%>
                
            </PageFooterTemplate>
        </wc:ReportGridView>
        </td></tr>
        <tr>
        <td colspan="3" style="border-left:solid 1px black;border-right:solid 1px black;   ">
        <table width="600" border="0" style="font-family:'Trebuchet MS'; font-size:11px;  ">
        <tr>
        
     
        <td width="310px" valign="top" align="left" >&nbsp;<br /> <asp:Label Visible="false" style="font-size:smaller; " ID="lblAmtWord" runat="server" Font-Bold="true" ></asp:Label></td>
        <td width="140px" align="right" ><b>Total Amount :</b></td>
        <td width="150px" align="right"><hr /><asp:Label ID="lblTotalSum" runat="server"></asp:Label><hr /></td>
        </tr>
        </table>
        <br /><br /><br />
        </td>
        </tr>
        <tr>
        <td colspan="3" valign="top" align="left"   style=" font-size:smaller;   border-left:solid 1px black;border-right:solid 1px black;border-bottom:solid 1px black;   "> </td>
        </tr>
        <tr>
        <td align="left"   style=" font-size:smaller;   border-left:solid 1px black;border-right:solid 1px black;border-bottom:solid 1px black;   ">
        Declaration :<br /> We declare that this invoice show the actual price of the goods described and that all the particulars are true and correct
        </td>
        <td colspan="2" style="border-right:solid 1px black;border-bottom:solid 1px black;border-top:solid 1px black;   ">
        <table>
        <tr><td align="right">For <asp:Label Font-Bold="true"   ID="lblSignCompany" runat="server"></asp:Label> </td></tr>
        <tr><td><br /></td></tr>
        <tr><td  align="left" valign="bottom">Authorized Signature</td></tr>
        </table>
        </td>
        </tr>
     </table>  --%>
            <table width="600px" border="0" cellpadding="2" cellspacing="0" style="font-family: 'Trebuchet MS';
                font-size: 11px;" cellspacing="0">
                <tr>
                    <td align="left" valign="top" width="100px" style="border-top: 1px solid black; border-left: 1px solid black;
                        border-bottom: 1px solid black;">
                        <table>
                            <tr>
                                <td align="left">
                                    TIN
                                </td>
                                <td>
                                    :
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblTNGST" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    CST#
                                </td>
                                <td>
                                    :
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblGSTno" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td valign="top" width="400px" style="border-top: 1px solid black; border-left: 1px solid black;
                        border-bottom: 1px solid black;">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label Font-Bold="true" ID="lblCompany" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblAddress" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblCity" runat="server" />
                                    -
                                    <asp:Label ID="lblPincode" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblState" runat="server"> </asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td align="left" valign="top" width="100px" style="border-top: 1px solid black; border-left: 1px solid black;
                        border-bottom: 1px solid black; border-right: 1px solid black;">
                        <table>
                            <tr>
                                <td align="left">
                                    Ph:
                                    <asp:Label ID="lblPhone" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="middle" width="120px" style="border-left: 1px solid black;
                        height: 5px; border-bottom: 1px solid black; height: 2%;">
                        &nbsp;Invoice no :
                        <asp:Label ID="lblBillno" runat="server"></asp:Label>
                    </td>
                    <td width="360px" style="border-left: 1px solid black; border-bottom: 1px solid black;"
                        align="center" valign="middle">
                        <asp:Label ID="lblHeader" runat="server" Text="Sales Invoice" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" valign="middle" width="120px" style="border-left: 1px solid black;
                        border-bottom: 1px solid black; border-right: 1px solid black;">
                        &nbsp;Date
                        <asp:Label ID="lblBillDate" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" width="600px" align="left" style="border-left: 1px solid black; border-bottom: 1px solid black;
                        border-right: 1px solid black;">
                        <table border="0">
                            <tr>
                                <td>
                                    <asp:Label ID="lblSupplier" runat="server" Font-Bold="true" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblSuppAdd1" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblSuppAdd2" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblSuppAdd3" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Ph :
                                    <asp:Label ID="lblSuppPh" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" width="600px" style="border-left: solid 1px black; border-right: solid 1px black;
                        border-bottom: solid 1px black;">
                        <wc:ReportGridView runat="server" BorderWidth="1" ID="gvBillDetails" CssClass="left"
                            GridLines="Both" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                            PrintPageSize="23" AllowPrintPaging="true" Width="600px" OnRowDataBound="gvBillDetails_RowDataBound"
                            Style="font-family: 'Trebuchet MS'; font-size: 11px;">
                            <PageHeaderTemplate>
                                <br />
                                <br />
                            </PageHeaderTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="SL.No">
                                    <ItemTemplate>
                                        <%# ((GridViewRow)Container).RowIndex + 1%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProductName" runat="server" CssClass="left"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty">
                                    <ItemTemplate>
                                        <%# Eval("Quantity")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate">
                                    <ItemTemplate>
                                        <%# Eval("Rate")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total" FooterStyle-Font-Bold="True">
                                    <ItemTemplate>
                                        <%# Eval("total") %>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                    </FooterTemplate>
                                    <FooterStyle Font-Bold="True"></FooterStyle>
                                </asp:TemplateField>
                            </Columns>
                            <PagerTemplate>
                            </PagerTemplate>
                            <PageFooterTemplate>
                                <br />
                                <%-- Page <%# gvCashDetails.CurrentPrintPage.ToString() %> / <%# gvCashDetails.PrintPageCount%>--%>
                            </PageFooterTemplate>
                        </wc:ReportGridView>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" width="600px" style="border-left: solid 1px black; border-right: solid 1px black;">
                        <table width="600px" border="0" style="font-family: 'Trebuchet MS'; font-size: 11px;">
                            <tr>
                                <td width="310px" valign="top" align="left">
                                    &nbsp;<br />
                                    <asp:Label Visible="false" Style="font-size: smaller;" ID="lblAmtWord" runat="server"
                                        Font-Bold="true"></asp:Label>
                                </td>
                                <td width="460px" align="right">
                                    <b>GRAND TOTAL :</b>
                                </td>
                                <td width="140px" align="right">
                                    <hr />
                                    <asp:Label ID="lblTotalSum" runat="server"></asp:Label><hr />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="border-left: solid 1px black; border-right: solid 1px black;
                        border-bottom: solid 1px black;">
                        <table width="100%">
                            <tr>
                                <td align="left" style="font-size: smaller;">
                                    EXEMPTED - GOODS - NO TAX<br />
                                    <br />
                                    * Our responsibility cases when the goods leave out godown.<br />
                                    * Goods once sold cannot be taken back<br />
                                    * SUBJECT TO CHAMBER ARBITRATION TRIBUNAL MADURAI.
                                </td>
                                <td style="border-left: solid 1px black; border-top: solid 1px black;">
                                    <table>
                                        <tr>
                                            <td align="right">
                                                For
                                                <asp:Label Font-Bold="true" ID="lblSignCompany" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" valign="bottom">
                                                Authorized Signature
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="hdPurchase" runat="server" Value="0" />
            <asp:HiddenField ID="hdFile" runat="server" Value="0" />
            <asp:HiddenField ID="hdFilename" runat="server" Value="0" />
            <asp:HiddenField ID="hdTotalAmt" runat="server" Value="0" />
            <asp:HiddenField ID="hdMode" runat="server" Value="New" />
            <br />
            <br />
            <br />
        </div>
    </div>
    </form>
</body>
</html>
