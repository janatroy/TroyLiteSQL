<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default6.aspx.cs" Inherits="Default6" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
   <form id="form1" runat="server">
   <asp:ScriptManager runat="server" ID="scr"></asp:ScriptManager>
   <div>
   <asp:UpdatePanel ID="UpdatePnlMaster" runat="server" UpdateMode="Conditional">
   <ContentTemplate>
   <input id="dummyStock" type="button" style="display: none" runat="server" />
   <input id="BtnPopUpCancel" type="button" style="display: none" runat="server" />
   <cc1:ModalPopupExtender ID="ModalPopupSales" runat="server" BackgroundCssClass="modalBackground"
         RepositionMode="RepositionOnWindowResizeAndScroll" DynamicServicePath="" Enabled="True" PopupControlID="pnlSalesForm" TargetControlID="dummyStock" OkControlID="dummyStock" CancelControlID="BtnPopUpCancel">
   </cc1:ModalPopupExtender>
   <asp:Panel ID="pnlSalesForm" runat="server" Style="width: 100%; display: none">
       <asp:UpdatePanel ID="updatePnlSales" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="Div1" class="divArea" style="width:100%">
                    <table style="width: 100%;" align="center">
                        <tr style="width: 100%">
                            <td style="width: 100%">
                                <table style="text-align: left;" width="100%" cellpadding="3" cellspacing="5">
                                    <tr>
                                        <td>
                                            <cc1:TabContainer ID="tabs2" runat="server" Width="100%" ActiveTabIndex="0">
                                                <cc1:TabPanel ID="tabMaster" runat="server" HeaderText="Field">
                                                    <HeaderTemplate>
                                                        Sales Report
                                                    </HeaderTemplate>
                                                        <ContentTemplate>
                                                            <div>
                                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <table style="width:100%; height: 100%; background-color:#90C9FC">
                                                               <!-- <tr>
                                                                <td align="center" valign="middle" colspan="4" style="width:100%">
                                                                     <asp:Label ID="Label5" runat="server" style="color:#0567AE" Font-Names ="arial" Font-Size="14px" Font-Bold="true" Text="Select Fields to be Generated"></asp:Label>
                                                                </td>
                                                                </tr>-->
                                                                <tr style="height:5px">
                                                                </tr>
                                                                <tr>
                                                                    <td style="width:25%">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td align="left" style="width:35%">
                                                                        <asp:CheckBox ID="chkboxCategory" runat="server" style="color:Black" Text="CategoryName" Font-Names="arial" Font-Size="11px" AutoPostBack="true" />
                                                                    </td>
                                                                    <td align="left" style="width:30%">
                                                                        <asp:CheckBox ID="chkboxCustomer" runat="server" Text="Customer" style="color:Black" Font-Names="arial" Font-Size="11px" AutoPostBack="true"/>
                                                                    </td>
                                                                    <td style="width:10%">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width:25%">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td align="left" style="width:35%">
                                                                        <asp:CheckBox ID="chkboxProductCode" runat="server" style="color:Black" Text="ProductCode" Font-Names="arial" Font-Size="11px" AutoPostBack="true"/>
                                                                    </td>
                                                                    <td align="left" style="width:30%">
                                                                    <asp:CheckBox ID="chkboxBrand" runat="server" Text="Brand" style="color:Black" Font-Names="arial" Font-Size="11px" AutoPostBack="true"/>
                                                                    </td>
                                                                    <td style="width:10%">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width:25%">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td align="left" style="width:35%">
                                                                        <asp:CheckBox ID="chkboxProductName" runat="server" style="color:Black" Text="ProductName" Font-Names="arial" Font-Size="11px" AutoPostBack="true"></asp:CheckBox>
                                                                    </td>
                                                                    <td align="left" style="width:30%">
                                                                        <asp:CheckBox ID="chkboxBillno" runat="server" Text="Billno" style="color: Black" Font-Names="arial" Font-Size="11px" AutoPostBack="true">
                                                                        </asp:CheckBox>
                                                                    </td>
                                                                    <td style="width:10%">&nbsp;</td>
                                                                 </tr>
                                                                 <tr>
                                                                    <td style="width:25%">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td align="left" style="width:35%">
                                                                        <asp:CheckBox ID="chkboxBillDate" runat="server" style="color: Black" Text="BillDate" Font-Names="arial" Font-Size="11px" AutoPostBack="true">
                                                                        </asp:CheckBox>
                                                                    </td>
                                                                    <td align="left" style="width:30%">
                                                                        <asp:CheckBox ID="chkboxInternalTransfer" runat="server" Text="InternalTransfer" style="color:Black" Font-Names="arial" Font-Size="11px" AutoPostBack="true">
                                                                        </asp:CheckBox>
                                                                    </td>
                                                                    <td style="width:10%">
                                                                        &nbsp;
                                                                    </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width:25%">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td align="left" style="width:35%">
                                                                            <asp:CheckBox ID="chkboxPurchaseReturn" runat="server" style="color:Black" Text="PurchaseReturn" Font-Names="arial" Font-Size="11px" AutoPostBack="true">
                                                                            </asp:CheckBox>
                                                                        </td>
                                                                        <td align="left" style="width:30%">
                                                                            <asp:CheckBox ID="chkboxModel" runat="server" style="color:Black" Text="Model" Font-Names="arial" Font-Size="11px" AutoPostBack="true">
                                                                            </asp:CheckBox>
                                                                        </td>
                                                                        <td style="width:10%">
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width:25%">
                                                                        &nbsp;
                                                                        </td>
                                                                        <td align="left" style="width:35%">
                                                                            <asp:CheckBox ID="ChkboxCustaddr" runat="server" style="color:Black" Text="Customer Address" Font-Names="arial" Font-Size="11px" AutoPostBack="true">
                                                                            </asp:CheckBox>
                                                                        </td>
                                                                        <td align="left" style="width:30%">
                                                                            <asp:CheckBox ID="ChkboxCustphone" runat="server" style="color:Black" Text="Customer Phone" Font-Names="arial" Font-Size="11px" AutoPostBack="true">
                                                                            </asp:CheckBox>
                                                                        </td>
                                                                        <td style="width:10%">
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>                              
                                                                        <td style="width:25%">
                                                                            &nbsp;
                                                                        </td>                                                               
                                                                        <td align="left" style="width:35%">
                                                                            <asp:CheckBox ID="ChkboxEmpname" runat="server" style="color:Black" Text="Employee Name" Font-Names="arial" Font-Size="11px" AutoPostBack="true">
                                                                            </asp:CheckBox>
                                                                        </td>
                                                                        <td align="left" style="width:30%">
                                                                            <asp:CheckBox ID="ChkboxPaymode" runat="server" AutoPostBack="True" style="color: Black" Text="Paymode" Font-Names="arial" Font-Size="11px">
                                                                            </asp:CheckBox>
                                                                        </td>
                                                                        <td style="width:10%">&nbsp;</td>
                                                                    </tr>
                                                                    <tr>                              
                                                                        <td style="width:25%">
                                                                            &nbsp;
                                                                        </td>                                                               
                                                                        <td align="left" style="width:35%">
                                                                            <asp:CheckBox ID="chkboxStock" runat="server" style="color:Black" Text="Quantity" Font-Names="arial" Font-Size="11px" AutoPostBack="true">
                                                                            </asp:CheckBox>
                                                                        </td>
                                                                        <td align="left" style="width:30%">
                                                                            <asp:CheckBox ID="chkboxDiscount" runat="server" AutoPostBack="True" style="color: Black" Text="Discount" Font-Names="arial" Font-Size="11px">
                                                                            </asp:CheckBox>
                                                                        </td>
                                                                        <td style="width:10%">
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>                              
                                                                    <td style="width:25%">&nbsp;</td>                                                               
                                                                    <td align="left" style="width:35%">
                                                                            <asp:CheckBox ID="chkboxFreight" runat="server" style="color:Black" Text="Freight" Font-Names="arial" Font-Size="11px" AutoPostBack="true">
                                                                            </asp:CheckBox>
                                                                    </td>
                                                                    <td align="left" style="width:30%">
                                                                                             
                                                                        <asp:CheckBox ID="chkboxRate" runat="server" AutoPostBack="True" style="color: Black" Text="Rate" Font-Names="arial" Font-Size="11px">
                                                                        </asp:CheckBox>
                                                                        </td>
                                                                            <td style="width:10%">&nbsp;</td>
                                                                                          
                                                                    </tr>
                                                                    <tr>
                                                                    <td style="width:25%">&nbsp;</td>
                                                                    <td align="left" style="width:35%">
                                                                                             
                                                                        <asp:CheckBox ID="chkboxAll" runat="server" AutoPostBack="True" OnCheckedChanged="chkboxAll_CheckedChanged" style="color: Black" Text="Select All" Font-Names="arial" Font-Size="11px">
                                                                        </asp:CheckBox>
                                                                        </td>
                                                                            <td style="width:40%">&nbsp;</td>
                                                                            <td style="width:10%">&nbsp;</td>
                                                                                          
                                                                    </tr>
                                                                    <tr style="height:5px">
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="chkboxAll" EventName="CheckedChanged" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </ContentTemplate>
                                        </cc1:TabPanel>
                                        <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="Filter">
                                            <HeaderTemplate>
                                                Filter
                                            </HeaderTemplate>
                                            <ContentTemplate>
                                                                                          <div>
                                                                                            <table style="width:450px; height: 100%; background-color:#90C9FC">
                                                                                             <!-- <tr>
                                                                                                <td align="center" colspan="4">
                                                                                                     <asp:Label ID="Label1" runat="server" style="color:#0567AE" Font-Names ="arial" Font-Size="14px" Font-Bold="true" Text="Filter by Particular Item"></asp:Label>
                                                                                                </td>
                                                                                               </tr>-->
                                                                                               <tr style="height:5px">
                                                                                               </tr>
                                                                                               <tr>  
                                                                                                  <td class="ControlLabel2"  width="25%">
                                                                                                    Category Name
                                                                                                   </td>
                                                                                                    <td width="25%" align="left" class="ControlTextBox2">
                                                                                                     <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                                                                        <ContentTemplate>
                                                                                                      <asp:DropDownList ID="ddlCategory" runat="server" Height="24px" Width="98%"  CssClass="drpDownListMedium" BackColor = "#90C9FC">
                                                                                                      </asp:DropDownList>
                                                                                                    </td>
                                                                                                  <td class="ControlLabel2"  width="20%">
                                                                                                        Customer Name
                                                                                                       </td>
                                                                                                       <td width="25%" align="left" class="ControlTextBox2">
                                                                                                           <asp:DropDownList ID="ddlCustNme" AppendDataBoundItems="true" AutoPostBack="true" runat="server" Height="24px" Width="98%"  CssClass="drpDownListMedium" BackColor = "#90C9FC">
                                                                                                            </asp:DropDownList>
                                                                                                           </td>
                                                                                                   </tr>
                                                                                                   
                                                                                                        <tr>
                                                                                                          <td class="ControlLabel2"  width="25%">
                                                                                                            Product Name
                                                                                                           </td>
                                                                                                            <td width="25%" align="left" class="ControlTextBox2">
                                                                                                                <asp:DropDownList ID="ddlPrdctNme" runat="server" Height="24px" Width="98%"  CssClass="drpDownListMedium" BackColor = "#90C9FC">
                                                                                                                </asp:DropDownList>
                                                                                                             </td>
                                                                                                             <td class="ControlLabel2"  width="20%">
                                                                                                              Product Code
                                                                                                            </td>
                                                                                                            <td width="25%" align="left" class="ControlTextBox2">
                                                                                                                <asp:DropDownList ID="ddlPrdctCode" runat="server" Height="24px" Width="98%" 
                                                                                                                    CssClass="drpDownListMedium" BackColor = "#90C9FC">
                                                                                                                </asp:DropDownList>
                                                                                                            </td>
                                                                                                          </tr>
                                                                                                          
                                                                                                         <tr>
                                                                                                            <td class="ControlLabel2"  width="25%">
                                                                                                              Brand
                                                                                                            </td>
                                                                                                             <td width="25%" align="left" class="ControlTextBox2">
                                                                                                                   <asp:DropDownList ID="ddlBrand" runat="server" Height="24px" Width="98%" 
                                                                                                                    CssClass="drpDownListMedium" BackColor = "#90C9FC">
                                                                                                                </asp:DropDownList>
                                                                                                            </td>
                                                                                                            <td class="ControlLabel2"  width="20%">
                                                                                                                PayMode
                                                                                                            </td>
                                                                                                             <td width="25%" align="left" class="ControlTextBox2">
                                                                                                                   <asp:DropDownList ID="ddlPayMode" runat="server" Height="24px" Width="98%" 
                                                                                                                    CssClass="drpDownListMedium" BackColor = "#90C9FC">
                                                                                                                </asp:DropDownList>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr style="height:5px">
                                                                                                        </tr>
                                                                                                     </ContentTemplate>
                                                                                                </asp:UpdatePanel>
                                                                                             </table>
                                                                                          </div>
                                                                                       </ContentTemplate>
                                                                                    </cc1:TabPanel>
                                                                                    <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="Groupby">
                                                                                        <HeaderTemplate>
                                                                                            Groupby
                                                                                        </HeaderTemplate>
                                                                                        <ContentTemplate>
                                                                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                                                                                  <ContentTemplate>
                                                                                                     <div>
                                                                                                        <table style="width:450px; height:100%; background-color:#90C9FC;">
                                                                                                        
                                                                                                         <!--<tr>
                                                                                                              <td align="center" colspan="4">
                                                                                                                 <asp:Label ID="Label2" runat="server" style="color:#0567AE" Font-Names ="arial" Font-Size="14px" Font-Bold="true" Text="Groupby Particular Category"></asp:Label>
                                                                                                               </td>
                                                                                                            </tr>-->
                                                                                                            <tr style="height:5px">
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                             
                                                                                                                <td class="ControlLabel2"  width="25%">
                                                                                                                    First Level
                                                                                                                </td>
                                                                                                                <td width="25%" align="left" class="ControlTextBox2">
                                                                                                                
                                                                                                                    <asp:DropDownList ID="ddlFirstLvl" runat="server" Height="24px" Width="98%" 
                                                                                                                        CssClass="drpDownListMedium" BackColor = "#90C9FC">
                                                                                                                    </asp:DropDownList>
                                                                                                                </td>
                                                                                                                 <td class="ControlLabel2"  width="20%">
                                                                                                                    Fourth Level
                                                                                                                </td>
                                                                                                                <td  width="25%" align="left" class="ControlTextBox2">
                                                                                                                    <asp:DropDownList ID="ddlFourthLvl" runat="server" Height="24px" Width="98%"  
                                                                                                                        CssClass="drpDownListMedium" BackColor = "#90C9FC">
                                                                                                                    </asp:DropDownList>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                
                                                                                                                <td class="ControlLabel2"  width="25%">
                                                                                                                    Second Level
                                                                                                                </td>
                                                                                                                <td  width="25%" align="left" class="ControlTextBox2">
                                                                                                                    <asp:DropDownList ID="ddlSecondLvl" runat="server" Height="24px" Width="98%" 
                                                                                                                        CssClass="drpDownListMedium" BackColor = "#90C9FC">
                                                                                                                    </asp:DropDownList>
                                                                                                                </td>
                                                                                                                 <td class="ControlLabel2"  width="20%">
                                                                                                                    Fifth Level
                                                                                                                </td>
                                                                                                                <td  width="25%" align="left" class="ControlTextBox2">
                                                                                                                    <asp:DropDownList ID="ddlFifthLvl" runat="server" Height="24px" Width="98%" 
                                                                                                                        CssClass="drpDownListMedium" BackColor = "#90C9FC">
                                                                                                                    </asp:DropDownList>
                                                                                                                </td>
                                                                                                               
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                  
                                                                                                               <td class="ControlLabel2" width="25%">
                                                                                                                    Third Level
                                                                                                                </td>
                                                                                                                <td  width="25%" align="left" class="ControlTextBox2">
                                                                                                                    <asp:DropDownList ID="ddlThirdLvl" runat="server" Height="24px" Width="98%"  
                                                                                                                        CssClass="drpDownListMedium" BackColor = "#90C9FC">
                                                                                                                    </asp:DropDownList>
                                                                                                                </td>
                                                                                                                 <td class="ControlLabel2"  width="20%">
                                                                                                                    Sixth Level
                                                                                                                </td>
                                                                                                                <td  width="25%" align="left" class="ControlTextBox2">
                                                                                                                    <asp:DropDownList ID="ddlSixthLvl" runat="server" Height="24px" Width="98%" 
                                                                                                                        CssClass="drpDownListMedium" BackColor = "#90C9FC">
                                                                                                                    </asp:DropDownList>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                          
                                                                                                           
                                                                                                           
                                                                                                            <tr>
                                                                                                                 
                                                                                                                 <td class="ControlLabel2"  width="25%">
                                                                                                                    Seventh Level
                                                                                                                </td>
                                                                                                                <td  width="25%" align="left" class="ControlTextBox2">
                                                                                                                    <asp:DropDownList ID="ddlSeventhLvl" runat="server" Height="24px" Width="98%"  
                                                                                                                        CssClass="drpDownListMedium" BackColor = "#90C9FC">
                                                                                                                    </asp:DropDownList>
                                                                                                                </td>
                                                                                                                <td class="ControlLabel" width="20%">
                                                                                                                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                                                                                                                </td>
              
                                                                                                            </tr>
                                                                                                            <tr style="height:5px">
                                                                                                            </tr>
                                                                                                          </table>
                                                                                                        </div>     
                                                                                                          </ContentTemplate>
                                                                                   
                                                                                  </asp:UpdatePanel>      
                                                                              </ContentTemplate>
                                                                          </cc1:TabPanel>                                              
                                                                                                                  
                                                                                        <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="Orderby">
                                                                                                    <HeaderTemplate>
                                                                                                        Orderby
                
                                                                                                    </HeaderTemplate>
                
                                                                                                    <ContentTemplate>
                                                                                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                                                                                              <ContentTemplate>
                                                                                                        <div>
                                                                                                            <table style="width:450px; height:100%; background-color:#90C9FC">
                                                                                                             <!--<tr>
               
                                                                                                                <td align="center" colspan="4">
                                                                                                                      <asp:Label ID="Label3" runat="server" style="color:#0567AE" Font-Names ="arial" Font-Size="14px" Font-Bold="true" Text="Orderby Particular Category"></asp:Label>
                  
                                                                                                                </td>
              
                                                                                                            </tr>-->
                                                                                                            <tr style="height:5px">
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                             
                                                                                                                <td class="ControlLabel2"  width="25%">
                                                                                                                    First Level
                                                                                                                </td>
                                                                                                                <td width="25%" align="left" class="ControlTextBox2">
                                                                                                                    <asp:DropDownList ID="odlfirstlvl" runat="server" Height="24px" Width="98%" 
                                                                                                                        CssClass="drpDownListMedium" BackColor = "#90C9FC">
                                                                                                                    </asp:DropDownList>
                                                                                                                </td>
                                                                                                               <td class="ControlLabel2"  width="20%">
                                                                                                                    Fourth Level
                                                                                                                </td>
                                                                                                                <td  width="25%" align="left" class="ControlTextBox2">
                                                                                                                    <asp:DropDownList ID="odlfourlvl" runat="server" Height="24px" Width="98%" 
                                                                                                                        CssClass="drpDownListMedium" BackColor = "#90C9FC">
                                                                                                                    </asp:DropDownList>
                                                                                                                </td>
                
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                            
                                                                                                            <td class="ControlLabel2"  width="25%">
                                                                                                                Second Level
                                                                                                            </td>
                                                                                                            <td  width="25%" align="left" class="ControlTextBox2">
                                                                                                                <asp:DropDownList ID="odlsecondlvl" runat="server" Height="24px" Width="98%"  
                                                                                                                    CssClass="drpDownListMedium" BackColor = "#90C9FC">
                                                                                                                </asp:DropDownList>
                                                                                                            </td>
                                                                                                           <td class="ControlLabel2"  width="20%">
                                                                                                                    Fifth Level
                                                                                                                </td>
                                                                                                                <td  width="25%" align="left" class="ControlTextBox2">
                                                                                                                    <asp:DropDownList ID="odlfifthlvl" runat="server" Height="24px" Width="98%" 
                                                                                                                        CssClass="drpDownListMedium" BackColor = "#90C9FC">
                                                                                                                    </asp:DropDownList>
                                                                                                                </td>
            
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                              
                                                                                                           <td class="ControlLabel2" width="25%">
                                                                                                                Third Level
                                                                                                            </td>
                                                                                                            <td  width="25%" align="left" class="ControlTextBox2">
                                                                                                                <asp:DropDownList ID="odlthirdlvl" runat="server" Height="24px" Width="98%" 
                                                                                                                    CssClass="drpDownListMedium" BackColor = "#90C9FC">
                                                                                                                </asp:DropDownList>
                                                                                                            </td>
                                                                                                         <td class="ControlLabel2"  width="20%">
                                                                                                                    Sixth Level
                                                                                                                </td>
                                                                                                                <td  width="25%" align="left" class="ControlTextBox2">
                                                                                                                    <asp:DropDownList ID="odlsixthlvl" runat="server" Height="24px" Width="98%" 
                                                                                                                        CssClass="drpDownListMedium" BackColor = "#90C9FC">
                                                                                                                    </asp:DropDownList>
                                                                                                                </td>
                                                                                                        </tr>
                                                                                                           
                                                                                                          
                                                                                                               
                                                                                                                <tr>
                                                                                                                
                                                                                                                 <td class="ControlLabel2"  width="25%">
                                                                                                                    Seventh Level
                                                                                                                </td>
                                                                                                                <td  width="25%" align="left" class="ControlTextBox2">
                                                                                                                    <asp:DropDownList ID="odlseventhlvl" runat="server" Height="24px" Width="98%" 
                                                                                                                        CssClass="drpDownListMedium" BackColor = "#90C9FC">
                                                                                                                    </asp:DropDownList>
                                                                                                                </td>
                                                                                                                 
                                                                                                                </tr>
                                                                                                                <tr style="height:5px">
                                                                                                                </tr>
                                                                                                                
                                                                                                            </table>
                                                                                                              <table>
                                                                                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns= "False" 
                                                                                                                     EnableViewState="False">
                                                                                                            <Columns>
                                                                                                            <asp:TemplateField>
                                                                                                            <HeaderTemplate>name</HeaderTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField>
                                                                                                          <HeaderTemplate>chkboxCategory</HeaderTemplate>
        
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField>
                                                                                                            <HeaderTemplate>chkboxBrand</HeaderTemplate>  
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField>
                                                                                                            <HeaderTemplate>chkboxProductCode</HeaderTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField>
                                                                                                           <HeaderTemplate>chkboxProductNme</HeaderTemplate>
        
                                                                                                            </asp:TemplateField>
                                                                                                            
        
                                                                                                             <asp:TemplateField>
                                                                                                           <HeaderTemplate>chkboxCustomer</HeaderTemplate>
        
                                                                                                            </asp:TemplateField>
                                                                                                             <asp:TemplateField>
                                                                                                           <HeaderTemplate>chkboxPurchasertn</HeaderTemplate>
        
                                                                                                            </asp:TemplateField>
                                                                                                             <asp:TemplateField>
                                                                                                           <HeaderTemplate>chkboxInternaltransfer</HeaderTemplate>
        
                                                                                                            </asp:TemplateField>
                                                                                                             <asp:TemplateField>
                                                                                                           <HeaderTemplate>chkboxPaymode</HeaderTemplate>
        
                                                                                                           
                                                                                                            </asp:TemplateField>
                                                                                                              <asp:TemplateField>
                                                                                                           <HeaderTemplate>chkboxStock</HeaderTemplate>
        
                                                                                                           
                                                                                                            </asp:TemplateField>
                                                                                                              <asp:TemplateField>
                                                                                                           <HeaderTemplate>chkboxDiscount</HeaderTemplate>
        
                                                                                                           
                                                                                                            </asp:TemplateField>
                                                                                                              <asp:TemplateField>
                                                                                                           <HeaderTemplate>chkboxFreight</HeaderTemplate>
        
                                                                                                           
                                                                                                            </asp:TemplateField>
                                                                                                              <asp:TemplateField>
                                                                                                           <HeaderTemplate>chkboxRate</HeaderTemplate>
        
                                                                                                           
                                                                                                            </asp:TemplateField>
                                                                                                             <asp:TemplateField>
                                                                                                           <HeaderTemplate>chkboxall</HeaderTemplate>
        
                                                                                                            </asp:TemplateField>
                                                                                                             <asp:TemplateField>
                                                                                                           <HeaderTemplate>dproductdesc</HeaderTemplate>
        
                                                                                                            </asp:TemplateField>
                                                                                                             <asp:TemplateField>
                                                                                                           <HeaderTemplate>dcat</HeaderTemplate>
        
                                                                                                            </asp:TemplateField>
                                                                                                             <asp:TemplateField>
                                                                                                           <HeaderTemplate>dprod</HeaderTemplate>
        
                                                                                                            </asp:TemplateField>
                                                                                                             <asp:TemplateField>
                                                                                                           <HeaderTemplate>ditem</HeaderTemplate>
        
                                                                                                            </asp:TemplateField>
                                                                                                             <asp:TemplateField>
                                                                                                           <HeaderTemplate>dpay</HeaderTemplate>
        
                                                                                                            </asp:TemplateField>
                                                                                                             <asp:TemplateField>
                                                                                                           <HeaderTemplate>drate</HeaderTemplate>
        
                                                                                                            </asp:TemplateField>
                                                                                                             <asp:TemplateField>
                                                                                                           <HeaderTemplate>dfirst</HeaderTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                             <asp:TemplateField>
                                                                                                           <HeaderTemplate>dsecond</HeaderTemplate>
        
                                                                                                            </asp:TemplateField>
                                                                                                             <asp:TemplateField>
                                                                                                           <HeaderTemplate>dthird</HeaderTemplate>
        
                                                                                                            </asp:TemplateField>
                                                                                                             <asp:TemplateField>
                                                                                                           <HeaderTemplate>dfour</HeaderTemplate>
        
                                                                                                            </asp:TemplateField>
                                                                                                             <asp:TemplateField>
                                                                                                           <HeaderTemplate>dfifth</HeaderTemplate>
                                                                                                           </asp:TemplateField>
                                                                                                             <asp:TemplateField>
                                                                                                           <HeaderTemplate>dsixth</HeaderTemplate>
                                                                                                           </asp:TemplateField>
                                                                                                             <asp:TemplateField>
                                                                                                           <HeaderTemplate>dseventh</HeaderTemplate>

                                                                                                           </asp:TemplateField>
                                                                                                             <asp:TemplateField>
                                                                                                           <HeaderTemplate>odfirst</HeaderTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                             <asp:TemplateField>
                                                                                                           <HeaderTemplate>odsecond</HeaderTemplate>
        
                                                                                                            </asp:TemplateField>
                                                                                                             <asp:TemplateField>
                                                                                                           <HeaderTemplate>odthird</HeaderTemplate>
        
                                                                                                            </asp:TemplateField>
                                                                                                             <asp:TemplateField>
                                                                                                           <HeaderTemplate>odfour</HeaderTemplate>
        
                                                                                                            </asp:TemplateField>
                                                                                                             <asp:TemplateField>
                                                                                                           <HeaderTemplate>odfifth</HeaderTemplate>
                                                                                                           </asp:TemplateField>
                                                                                                            <asp:TemplateField>
                                                                                                           <HeaderTemplate>odsixth</HeaderTemplate>
                                                                                                           </asp:TemplateField>
                                                                                                            <asp:TemplateField>
                                                                                                           <HeaderTemplate>odseventh</HeaderTemplate>
                                                                                                           </asp:TemplateField>

                                                                                                           <asp:TemplateField>
                                                                                                           <HeaderTemplate>rblpurchase</HeaderTemplate>
                                                                                                           </asp:TemplateField>
                                                                                                             <asp:TemplateField>
                                                                                                           <HeaderTemplate>rblinternal</HeaderTemplate>
                                                                                                           </asp:TemplateField>
                                                                                                             <asp:TemplateField>
                                                                                                           <HeaderTemplate>stdate</HeaderTemplate>
                                                                                                           </asp:TemplateField>
                                                                                                             <asp:TemplateField>
                                                                                                           <HeaderTemplate>endate</HeaderTemplate>
                                                                                                           </asp:TemplateField>


                                                                                                            </Columns>
                                                                                                        </asp:GridView>
                                                                                                     </table>
                                                                                                 </div>
                                                                                             </ContentTemplate>
                                                                                        </asp:UpdatePanel>      
                                                                                   </ContentTemplate>
                                                                               </cc1:TabPanel>


                                                                               <cc1:TabPanel ID="TabPanel5" runat="server" HeaderText="SubTotal">
                                                                                     <HeaderTemplate>
                                                                                         SubTotal
                                                                                     </HeaderTemplate>
                                                                                     <ContentTemplate>
                                                                                          <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                                                                                                <ContentTemplate>
                                                                                                    <div>
                                                                                                        <table style="width:450px; height:100%; background-color:#90C9FC;">
                                                                                                            <tr style="height:5px">
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="ControlLabel2"  width="40%">
                                                                                                                    First Level
                                                                                                                </td>
                                                                                                                <td width="30%" class="ControlTextBox2">
                                                                                                                    <asp:DropDownList ID="firstsub" runat="server" Height="24px" Width="100%" 
                                                                                                                        CssClass="drpDownListMedium" BackColor = "#90C9FC">
                                                                                                                    </asp:DropDownList>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="ControlLabel2"  width="40%">
                                                                                                                    Second Level
                                                                                                                </td>
                                                                                                                <td  width="30%" class="ControlTextBox2">
                                                                                                                    <asp:DropDownList ID="secondsub" runat="server" Height="24px" Width="100%" 
                                                                                                                        CssClass="drpDownListMedium" BackColor = "#90C9FC">
                                                                                                                    </asp:DropDownList>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                               <td class="ControlLabel2" width="40%">
                                                                                                                    Third Level
                                                                                                                </td>
                                                                                                                <td  width="30%" class="ControlTextBox2">
                                                                                                                    <asp:DropDownList ID="thirdsub" runat="server" Height="24px" Width="100%"  
                                                                                                                        CssClass="drpDownListMedium" BackColor = "#90C9FC">
                                                                                                                    </asp:DropDownList>
                                                                                                                </td>
                                                                                                                 <td class="ControlLabel" width="20%">
                                                                                                                    <asp:Label ID="Label33" runat="server" Text=""></asp:Label>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="ControlLabel2"  width="40%">
                                                                                                                    Fourth Level
                                                                                                                </td>
                                                                                                                <td  width="30%" class="ControlTextBox2">
                                                                                                                    <asp:DropDownList ID="foursub" runat="server" Height="24px" Width="100%"  
                                                                                                                        CssClass="drpDownListMedium" BackColor = "#90C9FC">
                                                                                                                    </asp:DropDownList>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="ControlLabel2"  width="40%">
                                                                                                                    Fifth Level
                                                                                                                </td>
                                                                                                                <td  width="30%" class="ControlTextBox2">
                                                                                                                    <asp:DropDownList ID="fivesub" runat="server" Height="24px" Width="100%" 
                                                                                                                        CssClass="drpDownListMedium" BackColor = "#90C9FC">
                                                                                                                    </asp:DropDownList>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <%--<tr>
                                                                                                                <td class="ControlLabel2"  width="40%">
                                                                                                                    Sixth Level
                                                                                                                </td>
                                                                                                                <td  width="40%" class="ControlTextBox2">
                                                                                                                    <asp:DropDownList ID="sixsub" runat="server" Height="24px" Width="108%"  
                                                                                                                        CssClass="drpDownListMedium" BackColor = "#90C9FC">
                                                                                                                    </asp:DropDownList>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="ControlLabel2"  width="40%">
                                                                                                                    Seventh Level
                                                                                                                </td>
                                                                                                                <td  width="40%" class="ControlTextBox2">
                                                                                                                    <asp:DropDownList ID="sevensub" runat="server" Height="24px" Width="108%"  
                                                                                                                        CssClass="drpDownListMedium" BackColor = "#90C9FC">
                                                                                                                    </asp:DropDownList>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="ControlLabel2"  width="40%">
                                                                                                                    Eighth Level
                                                                                                                </td>
                                                                                                                <td  width="40%" class="ControlTextBox2">
                                                                                                                    <asp:DropDownList ID="eightsub" runat="server" Height="24px" Width="108%"  
                                                                                                                        CssClass="drpDownListMedium" BackColor = "#90C9FC">
                                                                                                                    </asp:DropDownList>
                                                                                                                </td>
                                                                                                            </tr>--%>
                                                                                                        </table>
                                                                                                    </div>     
                                                                                               </ContentTemplate>
                                                                                         </asp:UpdatePanel>      
                                                                                    </ContentTemplate>
                                                                                </cc1:TabPanel>   





                                                                               <cc1:TabPanel ID="TabPanel4" runat="server" HeaderText="Save">
                                                                                    <HeaderTemplate>
                                                                                         FinalReport
                                                                                    </HeaderTemplate>
                                                                                    <ContentTemplate>
                                                                                         <div>
                                                                                              <asp:UpdatePanel ID="UpdatePanel22" runat="server" UpdateMode="Conditional">
                                                                                                    <ContentTemplate>
                                                                                                          <table style="width:100%; height:100%; background-color:#90C9FC">
                                                                                                                        <!--  <tr>
                                                                                                              <td align="center" colspan="4">
                                                                                                                  <asp:Label ID="Label4" runat="server" style="color:#0567AE" Font-Names ="arial" Font-Size="14px" Font-Bold="true" Text="Save Selections"></asp:Label>
                                                                                                               </td>
                                                                                                            </tr>-->
                                                                                                                            <tr style="height:5px">
                                                                                                                               
                                                                                                                            </tr>
                                                                                                                            <tr>
                                                                                                                                <td class="ControlLabel2" style="width:35%;">
                                                                                                                                        Save Selections
                                                                                                                                 </td>
                                                                                                                                 <td style="width:35%;">
                                                                                                                                      <asp:TextBox ID="Savetxtbox" runat="server" Width="92%" BorderColor="White" BorderStyle="Solid"
                                                                                                                                            BackColor = "#90C9FC"  AutoPostBack="True"></asp:TextBox>
                                                                                                                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="Savetxtbox"
                                                                                                                                            CssClass="lblFont" Display="Dynamic" ErrorMessage="Please enter a name to save selections" Text="*"></asp:RequiredFieldValidator>
                                                                                                                                 </td>
                                                                                                                                 <td style="width:30%;">
                                                                                                                                     <asp:Label ID="Label32" runat="server"></asp:Label>
                                                                                                                                 </td>
                                                                                                                             </tr>
                                                                                                                               
                                                                                                                                <tr>
                                                                                                                                    <td class="ControlLabel2" style="width:35%;">
                                                                                                                                        Retrive Selections
                                                                                                                                    </td>
                                                                                                                                    <td style="width:35%;" align="left">
                                                                                                                                          <asp:DropDownList ID="ddlretrive" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                                                                                                                                Width="98%" Height="24px" CssClass="drpDownListMedium" BackColor = "#90c9fc"  TabIndex="2">
                                                                                                                                          <asp:ListItem style="background-color: #90C9FC" ></asp:ListItem>
                                                                                                                                          </asp:DropDownList>
                                                                                                                                     </td>
                                                                                                                                     <td style="width:30%;">
                                                                                                                                          &nbsp;
                                                                                                                                     </td>
                                                                                                                                </tr>
                                                                                                                                <tr style="height:10px">
                                                                                                                            </tr>
                                                                                                                        </table>
                                                                                                                        <table>
                                                                                                                            <tr style="height:8px">
                                                                                                                            </tr>
                                                                                                                            <tr>
                                                                                                                                <td style="width:15%;">
                                                                                                                                </td>
                                                                                                                                <td style="width:10%">
                                                                                                                                    <asp:Button ID="savebtn" runat="server" CssClass="savebutton1" 
                                                                                                                                                                EnableTheming="false" Text="" Enabled="true" OnClick="savebtn_Click"></asp:Button>
                                                                                                                                </td>
                                                                                                                                <td style="width:20%">
                                                                                                                                    <asp:Button ID="retrivebtn" runat="server" EnableTheming="false"
                                                                                                                                        CssClass="RRetrive"  Enabled="true" OnClick="retrivebtn_Click">
                                                                                                                                    </asp:Button>       
                                                                                                                                </td>
                                                                                                                                <td style="width:15%;">
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                            <tr style="height:5px">
                                                                                                                                
                                                                                                                            </tr>
                                                                                                                        </table>
                                                                                                                 <asp:ValidationSummary ID="ValidationSummary2" DisplayMode="BulletList" ShowMessageBox="true"  ShowSummary="false" HeaderText="Validation Messages" Font-Names="'Trebuchet MS'" Font-Size="12" runat="server" />
                                                                                                                 </ContentTemplate>
                                                                                                                     <Triggers>
                                                                                                                          <asp:AsyncPostBackTrigger ControlID="savebtn" EventName="Click" />
                                                                                                                          <asp:PostBackTrigger ControlID="retrivebtn" />
                                                                                                                     </Triggers>
                                                                                                                 </asp:UpdatePanel>
                                                                                                             </div>
                                                                                                         </ContentTemplate>
                                                                                                     </cc1:TabPanel>
                                                                                                 </cc1:TabContainer> 
                                                                                             </td>
                                                                                          </tr>

                                                                                    <table>
                                                                                        <tr style="height:10px">
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="left" class="ControlLabel2" style="width:40%">
                                                                                                Start Date
                                                                                            </td>
                                                                                            <td align="left" style="width:40%" class="ControlTextBox3">
                                                                                                <asp:TextBox ID="txtStrtDt" runat="server" AutoPostBack="True" SkinID="skinTxtBoxGrid"
                                                                                                    BackColor="#90C9FC" Height="25px" Width="150px"></asp:TextBox>
                                                                                                <cc1:CalendarExtender ID="calStartDate" runat="server" Enabled="True" 
                                                                                                    Format="dd/MM/yyyy" OnClientDateSelectionChanged="checkDate" 
                                                                                                    PopupButtonID="btnStartDate" TargetControlID="txtStrtDt">
                                                                                                </cc1:CalendarExtender>
                                                                                            </td>
                                                                                            <td align="left" style="width:20%">
                                                                                                <%--<asp:Panel ID="Panel17" runat="server">--%>
                                                                                                <asp:ImageButton ID="btnStartDate" runat="server" CausesValidation="False" 
                                                                                                   ImageUrl="App_Themes/NewTheme/images/cal.gif" 
                                                                                                   Width="20px" />
                                                                                                <%--</asp:Panel>--%>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="left" class="ControlLabel2" width="40%">
                                                                                                End Date
                                                                                            </td>
                                                                                            <td align="left" width="40%" class="ControlTextBox3">
                                                                                                <asp:TextBox ID="txtEndDt" runat="server" AutoPostBack="True"  SkinID="skinTxtBoxGrid"
                                                                                                    BackColor="#90C9FC" Height="25px" Width="150px"></asp:TextBox>
                                                                                                <cc1:CalendarExtender ID="CalEndDate" runat="server" Enabled="True" 
                                                                                                    Format="dd/MM/yyyy" OnClientDateSelectionChanged="checkDate" 
                                                                                                    PopupButtonID="btnEndDate" TargetControlID="txtEndDt">
                                                                                                </cc1:CalendarExtender>
                                                                                            </td>
                                                                                            <td align="left" style="width:20%">
                                                                                                <%--<asp:Panel ID="Panel17" runat="server">--%>
                                                                                                <asp:ImageButton ID="btnEndDate" runat="server" CausesValidation="False" 
                                                                                                    ImageUrl="App_Themes/NewTheme/images/cal.gif" 
                                                                                                    Width="20px" />
                                                                                                <%--</asp:Panel>--%>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="left" class="ControlLabel2" width="40%">
                                                                                                Purchase Return
                                                                                            </td>
                                                                                            <td align="left" style="width:40%" class="ControlTextBox3">
                                                                                                              
                                                                                                <asp:RadioButtonList ID="rblPurchseRtn" runat="server" CssClass="label" 
                                                                                                    RepeatDirection="Horizontal" BackColor="#90C9FC" Width="150px" Height="25px">
                                                                                                    <asp:ListItem>Yes</asp:ListItem>
                                                                                                    <asp:ListItem>No</asp:ListItem>
                                                                                                    <asp:ListItem Selected="True">All</asp:ListItem>
                                                                                                </asp:RadioButtonList>
                                                                                            </td>
                                                                                            <td style="width:20%">&nbsp;</td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="left" class="ControlLabel2" width="40%">
                                                                                                Internal Transfer
                                                                                            </td>
                                                                                            <td align="left" style="width:40%" class="ControlTextBox3">
                                                                                                <asp:RadioButtonList ID="rblIntrnlTrns" runat="server" CssClass="label" 
                                                                                                    RepeatDirection="Horizontal" BackColor="#90C9FC" Width="150px" Height="25px">
                                                                                                    <asp:ListItem>Yes</asp:ListItem>
                                                                                                    <asp:ListItem>No</asp:ListItem>
                                                                                                     <asp:ListItem Selected="True">All</asp:ListItem>
                                                                                                </asp:RadioButtonList>
                                                                                            </td>
                                                                                            <td style="width:20%">&nbsp;</td>
                                                                                        </tr>
                                                                                    </table>
                                                                                    <table>
                                                                                        <tr style="height:10px">
                                        
                                                                                        </tr>
                                                                                        <tr>
                                                                                        <td style="width:12%">&nbsp;</td>
                                                                                            <td align="center" style="width:25%">
                                                                                                <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                                                                                    <ContentTemplate>
                                                                                                        <asp:Button ID="btnxls" runat="server" CssClass="generatebutton" 
                                                                                                            EnableTheming="false" OnClick="btnxls_Click" Width="153px" />
                                                                                                    </ContentTemplate>
                                                                                                    <Triggers>
                                                                                                        <asp:PostBackTrigger ControlID="btnxls" />
                                                                                                    </Triggers>
                                                                                                </asp:UpdatePanel>
                                                                                            </td>
                                                                                            <td align="center" style="width:25%">
                                                                                                <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                                                                                    <ContentTemplate>
                                                                                                        <asp:Button ID="clearbtn" runat="server" CssClass="RClear" 
                                                                                                            EnableTheming="False" OnClientClick="document.location.href=document.location.href;"  TabIndex="5" />
                                                                                                    </ContentTemplate>
                                                                                                    <Triggers>
                                                                                                       <asp:AsyncPostBackTrigger ControlID="clearbtn" EventName="Click" />
                                                                                                    </Triggers>
                                                                                                </asp:UpdatePanel>
                                                                                            </td>
                                                                                            <td align="center" style="width:25%">
                                                                                                <asp:Button ID="btncancel1" runat="server" CssClass="RCancel" 
                                                                                                    EnableTheming="False" OnClientClick="window.close();" TabIndex="6" />
                                                                                            </td>
                                                                                            <td style="width:12%">
                                                                                                &nbsp;
                                                                                            </td>
                                                                                         </tr>
                                                                                     </table>
                                                                                </table>
                                                                            </td>
                                                                         </tr>
                                                                      </table>
                                                                   </td>
                                                               </tr>
                                                           </table>
                                                       </div>
                                                   </ContentTemplate>
                                               </asp:UpdatePanel>
                                           </asp:Panel>
                                     </ContentTemplate>
                                 </asp:UpdatePanel>
                            </div>
    </form>
</body>
</html>
