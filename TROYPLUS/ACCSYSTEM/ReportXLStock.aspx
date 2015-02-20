<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true" CodeFile="ReportXLStock.aspx.cs" Inherits="ReportXLStock" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    
    <script src="Scripts/JScriptSales.js" type="text/javascript">

    </script>

    <style id="Style1" runat="server">
        
        
        .fancy-green .ajax__tab_header
        {
	        background: url(App_Themes/NewTheme/Images/green_bg_Tab.gif) repeat-x;
	        cursor:pointer;
        }
        .fancy-green .ajax__tab_hover .ajax__tab_outer, .fancy-green .ajax__tab_active .ajax__tab_outer
        {
	        background: url(App_Themes/NewTheme/Images/green_left_Tab.gif) no-repeat left top;
        }
        .fancy-green .ajax__tab_hover .ajax__tab_inner, .fancy-green .ajax__tab_active .ajax__tab_inner
        {
	        background: url(App_Themes/NewTheme/Images/green_right_Tab.gif) no-repeat right top;
        }
        .fancy .ajax__tab_header
        {
	        font-size: 13px;
	        font-weight: bold;
	        color: #000;
	        font-family: sans-serif;
        }
        .fancy .ajax__tab_active .ajax__tab_outer, .fancy .ajax__tab_header .ajax__tab_outer, .fancy .ajax__tab_hover .ajax__tab_outer
        {
	        height: 46px;
        }
        .fancy .ajax__tab_active .ajax__tab_inner, .fancy .ajax__tab_header .ajax__tab_inner, .fancy .ajax__tab_hover .ajax__tab_inner
        {
	        height: 46px;
	        margin-left: 16px; /* offset the width of the left image */
        }
        .fancy .ajax__tab_active .ajax__tab_tab, .fancy .ajax__tab_hover .ajax__tab_tab, .fancy .ajax__tab_header .ajax__tab_tab
        {
	        margin: 16px 16px 0px 0px;
        }
        .fancy .ajax__tab_hover .ajax__tab_tab, .fancy .ajax__tab_active .ajax__tab_tab
        {
	        color: #fff;
        }
        .fancy .ajax__tab_body
        {
	        font-family: Arial;
	        font-size: 10pt;
	        border-top: 0;
	        border:1px solid #999999;
	        padding: 8px;
	        background-color: #ffffff;
        }
        
    </style>

    <asp:UpdatePanel ID="UpdatePnlMaster" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
    <input id="dummyStock" type="button" style="display: none" runat="server" />
    <input id="BtnPopUpCancel" type="button" style="display: none" runat="server" />
                        
    <cc1:ModalPopupExtender ID="ModalPopupStock" runat="server" BackgroundCssClass="modalBackgroundNew"
         RepositionMode="RepositionOnWindowResizeAndScroll" DynamicServicePath="" Enabled="True" PopupControlID="pnlStockForm" TargetControlID="dummyStock" OkControlID="dummyStock" CancelControlID="BtnPopUpCancel">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlStockForm" runat="server" Style="width:100%; height:100%; display: none">
        <asp:UpdatePanel ID="updatePnlStock" runat="server" UpdateMode="Conditional">
             <ContentTemplate>
                <div id="Div1" style="width:100%; height:100%;">
                    <table style="width: 100%;" align="center">
                        <tr style="width: 100%">
                            <td style="width: 100%">
                                <table style="text-align: left;" width="100%">
                                    <tr>
                                        <td>
                                            <cc1:TabContainer ID="tabs2" runat="server" Width="100%" ActiveTabIndex="0" CssClass="fancy fancy-green">
                                                <cc1:TabPanel ID="TabPanel6" runat="server" HeaderText="Field">
                                                    <HeaderTemplate>
                                                        Stock Report
                                                    </HeaderTemplate>
                                                        <ContentTemplate>
                                                            <div>
                                                                <table style="width:540px; height: 100%; background-color:#90C9FC">
                                                                    <tr style="height:40px">
                                                                   
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="ControlLabel2" style="width:35%;">
                                                                            Date
                                                                        </td>
                                                                        <td style="width:30%;">
                                                                            <asp:TextBox ID="txtStartDate" runat="server" Height="22px"  AutoPostBack="True" CssClass="cssTextBox" BackColor = "#90c9fc" BorderColor="Blue" BorderStyle="Solid" BorderWidth="1px"
                                                                                Width="95%"
                                                                                MaxLength="10" TabIndex="1"></asp:TextBox>
                                                                            <cc1:CalendarExtender ID="calStartDate" runat="server" Format="dd/MM/yyyy" OnClientDateSelectionChanged="checkDate"
                                                                                PopupButtonID="btnStartDate" TargetControlID="txtStartDate" Enabled="True">
                                                                            </cc1:CalendarExtender>
                                                                        </td>
                                                                        <td align="left" style="width:35%;">
                                                                            <asp:ImageButton ID="btnStartDate" ImageUrl="App_Themes/NewTheme/images/cal.gif"
                                                                                CausesValidation="False" Width="20px" runat="server" />    
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="height:50px">
                                                                   
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                      </ContentTemplate>
                                                  </cc1:TabPanel>
                                                <cc1:TabPanel ID="tabMaster" runat="server" HeaderText="Field">
                                                    <HeaderTemplate>
                                                        Field
                                                    </HeaderTemplate>
                                                        <ContentTemplate>
                                                            <div>
                                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <table style="width:540px; height: 100%; background-color:#90C9FC">
                                                              <!--  <tr>
                                                               <td align="center" colspan="4">
                                                                   <asp:Label ID="Label1" runat="server" style="color:#0567AE" Font-Names ="arial" Font-Size="14px" Font-Bold="true" Text="Select fields to be Generated"></asp:Label>
                                                                </td> </tr>-->
                                                                <tr style="height:5px">
                                                                   
                                                                </tr>
                                                                
                                                                <tr style="height:10px">
                                                                   
                                                                </tr>
                                                                <tr>
                                                                    <td width="15%">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td align="left" width="35%">
                                                                        <asp:CheckBox ID="chkboxCategory" runat="server" style="color:Black" Text="CategoryName" Font-Names="arial" Font-Size="12px" AutoPostBack="true"/>
                                                                    </td>
                                                                    <td align="left" width="35%">
                                                                        <asp:CheckBox ID="chkboxStock" runat="server" Text="Quantity" style="color:Black" Font-Names="arial" Font-Size="12px" AutoPostBack="true"/>
                                                                    </td>
                                                                    <td width="15%">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="15%">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td align="left" width="35%">
                                                                        <asp:CheckBox ID="chkboxBrand" runat="server" style="color:Black" Text="Brand" Font-Names="arial" Font-Size="12px" AutoPostBack="true"/>
                                                                    </td>
                                                                    <td align="left" width="35%">
                                                                        <asp:CheckBox ID="chkboxVat" runat="server" Text="VAT" style="color:Black" Font-Names="arial" Font-Size="12px" AutoPostBack="true"/>
                                                                    </td>
                                                                    <td width="15%">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="15%">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td align="left" width="35%">
                                                                        <asp:CheckBox ID="chkboxProduct" runat="server" style="color:Black" Text="ProductName" Font-Names="arial" Font-Size="12px" AutoPostBack="true"></asp:CheckBox>
                                                                    </td>
                                                                    <td align="left" width="35%">
                                                                        <asp:CheckBox ID="chkboxNlc" runat="server" Text="NLC" style="color: Black" Font-Names="arial" Font-Size="12px" AutoPostBack="true">
                                                                        </asp:CheckBox>
                                                                     </td>
                                                                     <td width="15%">
                                                                        &nbsp;
                                                                     </td>
                                                                  </tr>
                                                                  <tr>
                                                                      <td width="15%">
                                                                            &nbsp;
                                                                      </td>
                                                                      <td align="left" width="35%">
                                                                           <asp:CheckBox ID="chkboxModel" runat="server" style="color: Black" Text="Model" Font-Names="arial" Font-Size="12px" AutoPostBack="true">
                                                                           </asp:CheckBox>
                                                                      </td>
                                                                      <td align="left" width="35%">
                                                                            <asp:CheckBox ID="chkboxBuyrate" runat="server" Text="BuyRate" style="color:Black" Font-Names="arial" Font-Size="12px" AutoPostBack="true">
                                                                            </asp:CheckBox>
                                                                      </td>
                                                                      <td width="15%">
                                                                            &nbsp;
                                                                      </td>
                                                                   </tr>
                                                                   <tr>
                                                                        <td width="15%">&nbsp;</td>
                                                                        <td align="left" width="35%">
                                                                                <asp:CheckBox ID="chkboxItemCode" runat="server" style="color:Black" Text="ItemCode" Font-Names="arial" Font-Size="12px" AutoPostBack="true">
                                                                                </asp:CheckBox>
                                                                        </td>
                                                                        <td align="left" width="35%">
                                                                                <asp:CheckBox ID="chkboxRate" runat="server" Text="Rate" style="color:Black" Font-Names="arial" Font-Size="12px" AutoPostBack="true">
                                                                                </asp:CheckBox>
                                                                        </td>
                                                                        <td width="15%">&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width:25%">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td align="left" colspan="2" style="width:50%" valign="middle">
                                                                            <asp:CheckBox ID="chkboxAll" runat="server" AutoPostBack="True" OnCheckedChanged="chkboxAll_CheckedChanged" style="color:Black" Text="Select All" Font-Names="arial" Font-Size="12px">
                                                                            </asp:CheckBox>
                                                                        </td>
                                                                        <td style="width:25%">
                                                                            &nbsp;
                                                                        </td>
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
                                                                                            <table style="width:540px; height: 100%; background-color:#90C9FC">
                                                                                             <!-- <tr>
                                                                                                  <td align="center" colspan="4">
                                                                                                       <asp:Label ID="Label4" runat="server" style="color:#0567AE" Font-Names ="arial" Font-Size="14px" Font-Bold="true" Text="Filter by Particular Item"></asp:Label>
                                                                                                  </td>
                                                                                               </tr>-->
                                                                                               <tr style="height:5px">
                                                                                               </tr>
                                                                                                <tr>  
                                                                                                  <td class="ControlLabel2" align="left" width="25%">
                                                                                                      Category Name
                                                                                                  </td>
                                                                                                  <td width="20%" class="ControlTextBox2">
                                                                                                      
                                                                                                              <asp:DropDownList ID="ddlCategory" OnSelectedIndexChanged="LoadProducts"  AutoPostBack="true" style="border: 1px solid Blue" runat="server" Height="24px" CssClass="drpDownListMedium" BackColor = "#90C9FC">
                                                                                                              <asp:ListItem Selected="True" Value="0" style="background-color: #90c9fc">Select Category</asp:ListItem>
                                                                                                              </asp:DropDownList>
                                                                                                   </td>
                                                                                                   <td class="ControlLabel2" align="left" width="18%">
                                                                                                       Quantity
                                                                                                   </td>
                                                                                                   <td width="20%" class="ControlTextBox2">
                                                                                                        
                                                                                                                <asp:DropDownList ID="ddlStock" runat="server" Height="24px" style="border: 1px solid Blue"
                                                                                                                     CssClass="drpDownListMedium" BackColor = "#90C9FC" AutoPostBack="true" OnSelectedIndexChanged="ddlStock_SelectedIndexChanged">
                                                                                                                     <asp:ListItem>All</asp:ListItem>
                                                                                                                     <asp:ListItem Value="GT = StockValues">=</asp:ListItem>
                                                                                                                     <asp:ListItem Value="GT < StockValues">&lt;</asp:ListItem>
                                                                                                                     <asp:ListItem Value="GT > StockValues">&gt;</asp:ListItem>
                                                                                                                </asp:DropDownList>
                                                                                                            
                                                                                                   </td>
                                                                                                   <td width="17%">
                                                                                                        <asp:TextBox ID="Stocktxtbox" runat="server" Width="50px" BorderColor="blue" BorderStyle="Solid" Height="20px"
                                                                                                                BackColor = "#90C9FC" AutoPostBack="True" BorderWidth="1"></asp:TextBox>
                                                                                                        <asp:Label ID="Label27" runat="server"></asp:Label>
                                                                                                        <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="Stocktxtbox"
                                                                                                                    Display="Dynamic" EnableClientScript="True" MaximumValue="10000" Type="Double"
                                                                                                                    MinimumValue="0" Text="Please Enter a valid Quantity"></asp:RangeValidator>
                                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom, Numbers" TargetControlID="Stocktxtbox" />
                                                                                                   </td>
                                                                                                   </tr>
                                                                                                   <tr>
                                                                                                        <td class="ControlLabel2" align="left"  width="25%">
                                                                                                            Product Name
                                                                                                        </td>
                                                                                                        <td width="20%" class="ControlTextBox2">
                                                                                                              <asp:DropDownList ID="ddlPrdctNme" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadForProductName" style="border: 1px solid Blue" Height="24px" CssClass="drpDownListMedium" BackColor = "#90C9FC">
                                                                                                              <asp:ListItem Selected="True" Value="0" style="background-color: #90c9fc">Select Product Name</asp:ListItem>
                                                                                                              </asp:DropDownList>
                                                                                                        </td>
                                                                                                        <td class="ControlLabel2"  align="left" width="18%">
                                                                                                            VAT
                                                                                                        </td>
                                                                                                        <td width="20%" class="ControlTextBox2">
                                                                                                       
                                                                                                            <asp:DropDownList ID="ddlVat" runat="server" Height="24px" style="border: 1px solid Blue"
                                                                                                                CssClass="drpDownListMedium" BackColor = "#90C9FC" AutoPostBack="true" OnSelectedIndexChanged="ddlVat_SelectedIndexChanged">
                                                                                                                  <asp:ListItem>All</asp:ListItem>
                                                                                                                <asp:ListItem Value="GT < Vats">&lt;</asp:ListItem>
                                                                                                                <asp:ListItem Value="GT > Vats">&gt;</asp:ListItem>
                                                                                                                <asp:ListItem Value="GT = Vats">=</asp:ListItem>

                                                                                                            </asp:DropDownList>
                                                                                                            
                                                                                                        </td>
                                                                                                         <td width="17%">
                                                                                                            <asp:TextBox ID="Vattxtbox" runat="server" Width="50px"
                                                                                                                BackColor = "#90C9FC"  AutoPostBack="True"   BorderColor="blue" BorderStyle="Solid" Height="20px" BorderWidth="1"></asp:TextBox>
                                                                                                                  <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Custom, Numbers" TargetControlID="Vattxtbox" ValidChars="." />
                                                                                                            <asp:Label ID="Label28" runat="server"></asp:Label>
                                                                                                           <asp:RangeValidator ID="RangeValidator1Add" runat="server" ControlToValidate="Vattxtbox"
                                                                                                                                                                                            Display="Dynamic" EnableClientScript="True" MaximumValue="100" Type="Double"
                                                                                                                                                                                            MinimumValue="0" Text="VAT cannot be Greater than 100% and Less than 0%"></asp:RangeValidator>
                                                                                                        </td>
                                                                                                     </tr>
                                                                                                     <tr>
                                                                                                          <td class="ControlLabel2"  align="left" width="25%">
                                                                                                              Model
                                                                                                          </td>
                                                                                                          <td width="20%" class="ControlTextBox2">
                                                                                                                <asp:DropDownList ID="ddlMdl" runat="server" Height="24px" OnSelectedIndexChanged="LoadForModel"   AutoPostBack="true"  style="border: 1px solid Blue"
                                                                                                                    CssClass="drpDownListMedium" BackColor = "#90C9FC">
                                                                                                                    <asp:ListItem Selected="True" Value="0" style="background-color: #90c9fc">Select Model</asp:ListItem>
                                                                                                                </asp:DropDownList>
                                                                                                          </td>
                                                                                                          <td class="ControlLabel2"  align="left" width="18%">
                                                                                                                NLC
                                                                                                          </td>
                                                                                                          <td width="20%" class="ControlTextBox2">
                                                                                                          
                                                                                                                <asp:DropDownList ID="ddlNlc" runat="server" Height="24px" style="border: 1px solid Blue"
                                                                                                                    CssClass="drpDownListMedium" BackColor = "#90C9FC" AutoPostBack="true" OnSelectedIndexChanged="ddlNlc_SelectedIndexChanged">
                                                                                                                    <asp:ListItem>All</asp:ListItem>
                                                                                                                    <asp:ListItem Value="GT < Nlcs ">&lt;</asp:ListItem>
                                                                                                                    <asp:ListItem Value="GT > Nlcs">&gt;</asp:ListItem>
                                                                                                                    <asp:ListItem Value="GT = Nlcs">=</asp:ListItem>

                                                                                                                </asp:DropDownList>
                                                                                                                
                                                                                                           </td>
                                                                                                           <td width="17%">
                                                                                                                 <asp:TextBox ID="Nlctxtbox" runat="server" Width="50px"  BorderWidth="1" BorderColor="blue" BorderStyle="Solid" Height="20px"
                                                                                                                        BackColor = "#90C9FC"  AutoPostBack="True"></asp:TextBox>
                                                                                                                 <asp:Label ID="Label29" runat="server"></asp:Label>
                                                                                                                 <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="Nlctxtbox"
                                                                                                                    Display="Dynamic" EnableClientScript="True" MaximumValue="9999999" Type="Double"
                                                                                                                    MinimumValue="0" Text="Please enter a valid Nlc"></asp:RangeValidator>
                                                                                                                 <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom, Numbers" TargetControlID="Nlctxtbox" />
                                                                                                           </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td class="ControlLabel2" align="left"  width="25%">
                                                                                                                ItemCode
                                                                                                            </td>
                                                                                                             <td width="20%" class="ControlTextBox2">
                                                                                                                   <asp:DropDownList ID="ddlItemCode" DataTextField="ProductName" DataValueField="ItemCode" OnSelectedIndexChanged="LoadForProduct" runat="server" Height="24px"  AutoPostBack="true" style="border: 1px solid Blue"
                                                                                                                    CssClass="drpDownListMedium" BackColor = "#90C9FC">
                                                                                                                    <asp:ListItem Selected="True" Value="0" style="background-color: #90c9fc">Select Item Code</asp:ListItem>
                                                                                                                </asp:DropDownList>
                                                                                                            </td>
                                                                                                            <td class="ControlLabel2"  align="left" width="18%">
                                                                                                                    Buy Rate
                                                                                                            </td>
                                                                                                            <td width="20%" class="ControlTextBox2">
                                                                                                            
                                                                                                                <asp:DropDownList ID="ddlBuyrate" runat="server" Height="24px" style="border: 1px solid Blue"
                                                                                                                    CssClass="drpDownListMedium" BackColor = "#90C9FC" AutoPostBack="True" OnSelectedIndexChanged="ddlBuyrate_SelectedIndexChanged">
                                                                                                                        <asp:ListItem>All</asp:ListItem>
                                                                                                                    <asp:ListItem Value="GT < Buyrates">&lt;</asp:ListItem>
                                                                                                                    <asp:ListItem Value="GT > Buyrates">&gt;</asp:ListItem>
                                                                                                                    <asp:ListItem Value="GT = Buyrates">=</asp:ListItem>

                                                                                                                </asp:DropDownList>
                                                                                                                
                                                                                                            </td>
                                                                                                            <td width="17%">
                                                                                                                <asp:TextBox ID="buyratetxtbox" runat="server" Width="50px"  BorderColor="blue" BorderStyle="Solid" Height="20px" BorderWidth="1"
                                                                                                                    BackColor = "#90C9FC" AutoPostBack="True"></asp:TextBox>
                                                                                                                <asp:Label ID="Label31" runat="server"></asp:Label>
                                                                                                                <asp:RangeValidator ID="rvBuyVATAdd" runat="server" ControlToValidate="buyratetxtbox" Display="Dynamic" EnableClientScript="True" MaximumValue="9999999" Type="Double" MinimumValue="0" Text="Please enter a valid buyrate"></asp:RangeValidator>
                                                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Custom, Numbers" TargetControlID="buyratetxtbox" />
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                           <td class="ControlLabel2"  align="left" width="25%">
                                                                                                            Brand Name
                                                                                                           </td>
                                                                                                           <td width="20%" class="ControlTextBox2">
                                                                                                               <asp:DropDownList ID="ddlBrand" OnSelectedIndexChanged="LoadForBrand"  AutoPostBack="true"  runat="server" style="border: 1px solid Blue" Height="24px" CssClass="drpDownListMedium" BackColor = "#90C9FC">
                                                                                                               <asp:ListItem Selected="True" Value="0" style="background-color: #90c9fc">Select Brand</asp:ListItem> 
                                                                                                               </asp:DropDownList>
                                                                                                           </td>
                                                                                                            <td class="ControlLabel2"  align="left" width="18%">
                                                                                                                    Rate
                                                                                                            </td>
                                                                                                            <td width="20%" class="ControlTextBox2">
                                                                                                            
                                                                                                                <asp:DropDownList ID="ddlRate" runat="server" Height="24px" style="border: 1px solid Blue" 
                                                                                                                    CssClass="drpDownListMedium" BackColor = "#90C9FC" AutoPostBack="true" OnSelectedIndexChanged="ddlRate_SelectedIndexChanged">
                                                                                                                    <asp:ListItem>All</asp:ListItem>
                                                                                                                    <asp:ListItem Value="GT < Rated">&lt;</asp:ListItem>
                                                                                                                    <asp:ListItem Value="GT > Rated">&gt;</asp:ListItem>
                                                                                                                    <asp:ListItem Value="GT = Rated">=</asp:ListItem>
                                                                                                                </asp:DropDownList>
                                                                                                               
                                                                                                            </td>
                                                                                                            <td width="17%">
                                                                                                                <asp:TextBox ID="Ratetxtbox" runat="server" Width="50px"   BorderColor="blue" BorderStyle="Solid" Height="20px"
                                                                                                                    BackColor = "#90C9FC" AutoPostBack="True" BorderWidth="1"></asp:TextBox>
                                                                                                                <asp:Label ID="Label30" runat="server"></asp:Label>
                                                                                                                <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="Ratetxtbox"
                                                                                                                                                                                                Display="Dynamic" EnableClientScript="True" MaximumValue="9999999" Type="Double"
                                                                                                                                                                                                MinimumValue="0" Text="Please enter a valid rate"></asp:RangeValidator>
                                                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Custom, Numbers" TargetControlID="Ratetxtbox" />
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                       <%-- <tr style="height:5px">
                                                                                                        </tr>--%>
                                                                                                        <asp:UpdatePanel ID="UpdatePanel12" runat="server" UpdateMode="Conditional">
                                                                                                           <ContentTemplate>
                                                                                                        </ContentTemplate>
                                                                                                            <Triggers>
                                                                                                                <asp:AsyncPostBackTrigger ControlID="ddlVat" EventName="SelectedIndexChanged" />
                                                                                                                <asp:AsyncPostBackTrigger ControlID="ddlNlc" EventName="SelectedIndexChanged" />
                                                                                                                
                                                                                                                <asp:AsyncPostBackTrigger ControlID="ddlBuyrate" EventName="SelectedIndexChanged" />
                                                                                                                <asp:AsyncPostBackTrigger ControlID="ddlRate" EventName="SelectedIndexChanged" />
                                                                                                                <asp:AsyncPostBackTrigger ControlID="ddlStock" EventName="SelectedIndexChanged" />
                                                                                                            </Triggers>
                                                                                                        </asp:UpdatePanel>
                                                                                            </table>
                                                                                         </div>
                                                                                         <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="BulletList" ShowMessageBox="true"  ShowSummary="false" HeaderText="Validation Messages" Font-Names="'Trebuchet MS'" Font-Size="12" runat="server" />
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
                                                                                                        <table style="width:540px; height:100%; background-color:#90C9FC">
                                                                                                        
                                                                                                          <!-- <tr>
                                                                                                              <td align="center" colspan="4">
                                                                                                                  <asp:Label ID="Label2" runat="server" style="color:#0567AE" Font-Names ="arial" Font-Size="14px" Font-Bold="true" Text="Groupby Particular Category"></asp:Label>
                                                                                                               </td>
                                                                                                            </tr>-->
                                                                                                            <tr>
                                                                                                               <td  width="15%">
                                                                                                               </td>
                                                                                                               <td class="ControlLabel2"  width="25%">
                                                                                                                    First Level
                                                                                                               </td>
                                                                                                               <td width="25%" class="ControlTextBox2">
                                                                                                                   <asp:DropDownList ID="ddlfirstlvl" runat="server" Height="24px" Width="144%" style="border: 1px solid Blue"
                                                                                                                        CssClass="drpDownListMedium" BackColor = "#90C9FC">
                                                                                                                    </asp:DropDownList>
                                                                                                                </td>
                                                                                                                <td class="ControlLabel2" width="35%">
                                                                                                                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td  width="15%">
                                                                                                                </td>
                                                                                                                <td class="ControlLabel2"  width="25%">
                                                                                                                    Second Level
                                                                                                                </td>
                                                                                                                <td  width="25%" class="ControlTextBox2">
                                                                                                                    <asp:DropDownList ID="ddlsecondlvl" runat="server" Height="24px" Width="144%" style="border: 1px solid Blue"
                                                                                                                        CssClass="drpDownListMedium" BackColor = "#90C9FC">
                                                                                                                    </asp:DropDownList>
                                                                                                                </td>
                                                                                                                <td  width="35%">
                                                                                                                </td>
            
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                               <td  width="15%">
                                                                                                               </td>
                                                                                                               <td class="ControlLabel2" width="25%">
                                                                                                                    Third Level
                                                                                                                </td>
                                                                                                                <td  width="25%" class="ControlTextBox2">
                                                                                                                    <asp:DropDownList ID="ddlthirdlvl" runat="server" Height="24px" Width="144%" style="border: 1px solid Blue"
                                                                                                                        CssClass="drpDownListMedium" BackColor = "#90C9FC">
                                                                                                                    </asp:DropDownList>
                                                                                                                </td>
                                                                                                                <td  width="35%">
                                                                                                                </td>
               
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td  width="15%">
                                                                                                                </td>
                                                                                                                <td class="ControlLabel2"  width="25%">
                                                                                                                    Fourth Level
                                                                                                                </td>
                                                                                                                <td  width="25%" class="ControlTextBox2">
                                                                                                                    <asp:DropDownList ID="ddlfourlvl" runat="server" Height="24px" Width="144%" style="border: 1px solid Blue"
                                                                                                                        CssClass="drpDownListMedium" BackColor = "#90C9FC">
                                                                                                                    </asp:DropDownList>
                                                                                                                </td>
               
                                                                                                                <td  width="35%">
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                 <td  width="15%">
                                                                                                                 </td>
                                                                                                                 <td class="ControlLabel2"  width="25%">
                                                                                                                    Fifth Level
                                                                                                                </td>
                                                                                                                <td  width="25%" class="ControlTextBox2">
                                                                                                                    <asp:DropDownList ID="ddlfifthlvl" runat="server"  Height="24px" Width="144%" style="border: 1px solid Blue"
                                                                                                                        CssClass="drpDownListMedium" BackColor = "#90C9FC">
                                                                                                                    </asp:DropDownList>
                                                                                                                </td>
                                                                                                                <td  width="35%">
                                                                                                                </td>
              
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
                                                                                                            <table style="width:540px; background-color:#90C9FC">
                                                                                                               <!--<tr>
               
                                                                                                                <td align="center" colspan="4">
                                                                                                                  <asp:Label ID="Label3" runat="server" style="color:#0567AE" Font-Names ="arial" Font-Size="14px" Font-Bold="true" Text="Orderby Particular Category"></asp:Label>
                  
                                                                                                                </td>
              
                                                                                                            </tr>-->
                                                                                                            <tr>
                                                                                                               <td  width="15%">
                                                                                                               </td>
                                                                                                               <td class="ControlLabel2"  width="25%">
                                                                                                                    First Level
                                                                                                               </td>
                                                                                                               <td width="25%" class="ControlTextBox2">
                                                                                                                    <asp:DropDownList ID="odlfirstlvl" runat="server" Height="24px" Width="144%" style="border: 1px solid Blue"
                                                                                                                        CssClass="drpDownListMedium" BackColor = "#90C9FC">
                                                                                                                    </asp:DropDownList>
                                                                                                               </td>
                                                                                                               <td  width="35%">
                                                                                                               </td>
                
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                             <td  width="15%">
                                                                                                             </td>
                                                                                                            <td class="ControlLabel2"  width="25%">
                                                                                                                Second Level
                                                                                                            </td>
                                                                                                            <td  width="25%" class="ControlTextBox2">
                                                                                                                <asp:DropDownList ID="odlsecondlvl" runat="server" Height="24px" Width="144%" style="border: 1px solid Blue"
                                                                                                                    CssClass="drpDownListMedium" BackColor = "#90C9FC">
                                                                                                                </asp:DropDownList>
                                                                                                            </td>
                                                                                                            <td  width="35%">
                                                                                                            </td>
            
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                           <td  width="15%">
                                                                                                           </td>
                                                                                                           <td class="ControlLabel2" width="25%">
                                                                                                                Third Level
                                                                                                            </td>
                                                                                                            <td  width="25%" class="ControlTextBox2">
                                                                                                                <asp:DropDownList ID="odlthirdlvl" runat="server" Height="24px" Width="144%" style="border: 1px solid Blue"
                                                                                                                    CssClass="drpDownListMedium" BackColor = "#90C9FC">
                                                                                                                </asp:DropDownList>
                                                                                                            </td>
                                                                                                            <td  width="35%">
                                                                                                            </td>
               
                                                                                                        </tr>
                                                                                                            <tr>
                                                                                                                <td  width="15%">
                                                                                                                </td>
                                                                                                                <td class="ControlLabel2"  width="25%">
                                                                                                                    Fourth Level
                                                                                                                </td>
                                                                                                                <td  width="25%" class="ControlTextBox2">
                                                                                                                    <asp:DropDownList ID="odlfourlvl" runat="server" Height="24px" Width="144%" style="border: 1px solid Blue"
                                                                                                                        CssClass="drpDownListMedium" BackColor = "#90C9FC">
                                                                                                                    </asp:DropDownList>
                                                                                                                </td>
               
                                                                                                                <td  width="35%">
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                 <td  width="15%">
                                                                                                                 </td>
                                                                                                                 <td class="ControlLabel2"  width="25%">
                                                                                                                    Fifth Level
                                                                                                                </td>
                                                                                                                <td  width="25%" class="ControlTextBox2">
                                                                                                                    <asp:DropDownList ID="odlfifthlvl" runat="server" Height="24px" Width="144%" style="border: 1px solid Blue"
                                                                                                                        CssClass="drpDownListMedium" BackColor = "#90C9FC">
                                                                                                                    </asp:DropDownList>
                                                                                                                </td>
                                                                                                                <td  width="35%">
                                                                                                                </td>
              
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
                                                                                                            <HeaderTemplate>chkboxProduct</HeaderTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField>
                                                                                                           <HeaderTemplate>chkboxModel</HeaderTemplate>
        
                                                                                                            </asp:TemplateField>
                                                                                                             <asp:TemplateField>
                                                                                                           <HeaderTemplate>chkboxItemcode</HeaderTemplate>
        
                                                                                                            </asp:TemplateField>
        
                                                                                                             <asp:TemplateField>
                                                                                                           <HeaderTemplate>chkboxstock</HeaderTemplate>
        
                                                                                                            </asp:TemplateField>
                                                                                                             <asp:TemplateField>
                                                                                                           <HeaderTemplate>chkboxnlc</HeaderTemplate>
        
                                                                                                            </asp:TemplateField>
                                                                                                             <asp:TemplateField>
                                                                                                           <HeaderTemplate>chkboxvat</HeaderTemplate>
        
                                                                                                            </asp:TemplateField>
                                                                                                             <asp:TemplateField>
                                                                                                           <HeaderTemplate>chkboxbuyrate</HeaderTemplate>
        
                                                                                                            </asp:TemplateField>
                                                                                                             <asp:TemplateField>
                                                                                                           <HeaderTemplate>chkboxrate</HeaderTemplate>
        
                                                                                                            </asp:TemplateField>
                                                                                                            
                                                                                                            
                                                                                                             <asp:TemplateField>
                                                                                                           <HeaderTemplate>chkboxall</HeaderTemplate>
        
                                                                                                            </asp:TemplateField>
                                                                                                             <asp:TemplateField>
                                                                                                           <HeaderTemplate>dbrand</HeaderTemplate>
        
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
                                                                                                           <HeaderTemplate>dstock</HeaderTemplate>
        
                                                                                                            </asp:TemplateField>
                                                                                                             <asp:TemplateField>
                                                                                                           <HeaderTemplate>dnlc</HeaderTemplate>
        
                                                                                                            </asp:TemplateField>
       
                                                                                                             <asp:TemplateField>
                                                                                                           <HeaderTemplate>dvat</HeaderTemplate>
        
                                                                                                            </asp:TemplateField>
                                                                                                             <asp:TemplateField>
                                                                                                           <HeaderTemplate>dbuyrate</HeaderTemplate>
        
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
                                                                                                           <HeaderTemplate>txtstock</HeaderTemplate>
                                                                                                           </asp:TemplateField>
                                                                                                            <asp:TemplateField>
                                                                                                           <HeaderTemplate>txtvat</HeaderTemplate>
                                                                                                           </asp:TemplateField>
                                                                                                            <asp:TemplateField>
                                                                                                           <HeaderTemplate>txtbuyrate</HeaderTemplate>
                                                                                                           </asp:TemplateField>
                                                                                                            <asp:TemplateField>
                                                                                                           <HeaderTemplate>txtrate</HeaderTemplate>
                                                                                                           </asp:TemplateField>
                                                                                                            <asp:TemplateField>
                                                                                                           <HeaderTemplate>txtnlc</HeaderTemplate>
                                                                                                           </asp:TemplateField>
                                                                                                            <asp:TemplateField>
                                                                                                           <HeaderTemplate>dat</HeaderTemplate>
                                                                                                           </asp:TemplateField>
                                                                                                            </Columns>
                                                                                                        </asp:GridView>
                                                                                                       </table>
                                                                                                      </div>
                                                                                                      </ContentTemplate>
                                                                                    
                                                                                                    </asp:UpdatePanel>      
                                                                                                    </ContentTemplate>
            
                                                                                                  </cc1:TabPanel>

                                                                                                  <cc1:TabPanel ID="TabPanel5" runat="server" HeaderText="Groupby">
                                                                                                        <HeaderTemplate>
                                                                                                            SubTotal
                                                                                                        </HeaderTemplate>
                                                                                                        <ContentTemplate>
                                                                                                            <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                                                                                                                <ContentTemplate>
                                                                                                                    <div>
                                                                                                                        <table style="width:540px; height:100%; background-color:#90C9FC">
                                                                                                                            <tr>
                                                                                                                               <td  width="15%">
                                                                                                                               </td>
                                                                                                                               <td class="ControlLabel2"  width="25%">
                                                                                                                                    First Level
                                                                                                                               </td>
                                                                                                                               <td width="25%" class="ControlTextBox2">
                                                                                                                                   <asp:DropDownList ID="DdlFirstSub" runat="server" Height="24px" Width="144%" style="border: 1px solid Blue"
                                                                                                                                        CssClass="drpDownListMedium" BackColor = "#90C9FC">
                                                                                                                                    </asp:DropDownList>
                                                                                                                                </td>
                                                                                                                                <td class="ControlLabel2" width="35%">
                                                                                                                                    <asp:Label ID="Label111" runat="server" Text=""></asp:Label>
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                        <tr>
                                                                                                                            <td  width="15%">
                                                                                                                            </td>
                                                                                                                            <td class="ControlLabel2"  width="25%">
                                                                                                                                Second Level
                                                                                                                            </td>
                                                                                                                            <td  width="25%" class="ControlTextBox2">
                                                                                                                                <asp:DropDownList ID="DdlSecondSub" runat="server" Height="24px" Width="144%" style="border: 1px solid Blue"
                                                                                                                                    CssClass="drpDownListMedium" BackColor = "#90C9FC">
                                                                                                                                </asp:DropDownList>
                                                                                                                            </td>
                                                                                                                            <td  width="35%">
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                           <td  width="15%">
                                                                                                                           </td>
                                                                                                                           <td class="ControlLabel2" width="25%">
                                                                                                                                Third Level
                                                                                                                            </td>
                                                                                                                            <td  width="25%" class="ControlTextBox2">
                                                                                                                                <asp:DropDownList ID="DdlThirdSub" runat="server" Height="24px" Width="144%" style="border: 1px solid Blue"
                                                                                                                                    CssClass="drpDownListMedium" BackColor = "#90C9FC">
                                                                                                                                </asp:DropDownList>
                                                                                                                            </td>
                                                                                                                            <td  width="35%">
                                                                                                                            </td>
               
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                            <td  width="15%">
                                                                                                                            </td>
                                                                                                                            <td class="ControlLabel2"  width="25%">
                                                                                                                                Fourth Level
                                                                                                                            </td>
                                                                                                                            <td  width="35%" class="ControlTextBox2">
                                                                                                                                <asp:DropDownList ID="DdlFourSub" runat="server" Height="24px" Width="144%" style="border: 1px solid Blue"
                                                                                                                                    CssClass="drpDownListMedium" BackColor = "#90C9FC">
                                                                                                                                </asp:DropDownList>
                                                                                                                            </td>
               
                                                                                                                            <td  width="25%">
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                             <td  width="15%">
                                                                                                                             </td>
                                                                                                                             <td class="ControlLabel2"  width="25%">
                                                                                                                                Fifth Level
                                                                                                                            </td>
                                                                                                                            <td  width="25%" class="ControlTextBox2">
                                                                                                                                <asp:DropDownList ID="DdlFiveSub" runat="server"  Height="24px" Width="144%" style="border: 1px solid Blue"
                                                                                                                                    CssClass="drpDownListMedium" BackColor = "#90C9FC">
                                                                                                                                </asp:DropDownList>
                                                                                                                            </td>
                                                                                                                            <td  width="35%">
                                                                                                                            </td>
              
                                                                                                                        </tr>
                                                                                                           
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
                                                                                                             <div style="width:100%;">
                                                                                                                <asp:UpdatePanel ID="UpdatePanel22" runat="server" UpdateMode="Conditional">
                                                                                                                    <ContentTemplate>
                                                                                                                        <table style="width:540px; background-color:#90c9fc">
                                                                                                                                 <!-- <tr>
                                                                                                              <td align="center" colspan="4">
                                                                                                                  <asp:Label ID="Label5" runat="server" style="color:#0567AE" Font-Names ="arial" Font-Size="14px" Font-Bold="true" Text="Save Selections"></asp:Label>
                                                                                                               </td>
                                                                                                            </tr>-->
                                                                                                            <tr>
                                                                                                            <td>
                                                                                                            </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="ControlLabel2" style="width:35%;">
                                                                                                                    Save Selections
                                                                                                                </td>
                                                                                                                <td  style="width:35%;">
                                                                                                                    <asp:TextBox ID="Savetxtbox" runat="server" Width="95%" Height="22px"   BackColor = "#90c9fc" BorderColor="Blue" BorderStyle="Solid" BorderWidth="1px"
                                                                                                                        AutoPostBack="True"></asp:TextBox>
                                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="Savetxtbox"
                                                                                                                        CssClass="lblFont" Display="Dynamic" ErrorMessage="Please enter a name to save selections"></asp:RequiredFieldValidator>
                                                                                                                </td>
                                                                                                                <td style="width:35%;">
                                                                                                                    <asp:Label ID="Label32" runat="server"></asp:Label>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                 <td class="ControlLabel2" style="width:35%;">
                                                                                                                      Retrive Selections
                                                                                                                 </td>
                                                                                                                 <td style="width:35%;" align="left">
                                                                                                                     <asp:DropDownList ID="ddlretrive" runat="server" AppendDataBoundItems="true" AutoPostBack="true" style="border: 1px solid Blue"
                                                                                                                          Width="98%" Height="25px" CssClass="drpDownListMedium" BackColor = "#90c9fc"  TabIndex="2">
                                                                                                                     <asp:ListItem style="background-color: #90c9fc" ></asp:ListItem>
                                                                                                                     </asp:DropDownList>
                                                                                                                </td>
                                                                                                                <td style="width:35%;">
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                </td>
                                                                                                             </tr>
                                                                                                             
                                                                                                            <tr style="height:10px">
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                        <table>
                                                                                                            <tr style="height:10px">
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td style="width:15%;">
                                                                                                                </td>
                                                                                                                <td  style="width:17%">
                                                                                                                      <asp:Button ID="savebtn" runat="server" OnClick="savebtn_Click" CssClass="savebutton1231" 
                                                                                                                            EnableTheming="false" Text="" Enabled="true">
                                                                                                                      </asp:Button>
                                                                                                                </td>
                                                                                                                <td  style="width:20%">
                                                                                                                    <asp:Button ID="retrivebtn" runat="server" OnClick="retrivebtn_Click" Text=""
                                                                                                                            CssClass="RRetrive" EnableTheming="false" Enabled="true">
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

                                                                                           <table width="100%">
                                                                                                <tr style="height:8px">
                                                                                                </tr>
                                                                                                        <tr>
                                                                                                            <td style="width:12%">
                                                                                                            </td>
                                                                                                            <td align="right" style="width:25%">
                                                                                                                <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                                                                                                    <ContentTemplate>
                                                                                                                        <asp:Button ID="btnxls" runat="server" CssClass="exportexl6"
                                                                                                                            EnableTheming="false" OnClick="btnxls_Click" Width="152px" />
                                                                                                                    </ContentTemplate>
                                                                                                                    <Triggers>
                                                                                                                        <asp:PostBackTrigger ControlID="btnxls" />
                                                                                                                    </Triggers>
                                                                                                                </asp:UpdatePanel>
                                                                                                            </td>
                                                                                                            <td align="center" style="width:25%">
                                                                                                                <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                                                                                                                    <ContentTemplate>
                                                                                                                        <asp:Button ID="btncancel1" runat="server" CssClass="cancelbutton6" Width="152px"
                                                                                                                            EnableTheming="False" OnClientClick="window.close();" TabIndex="6" />
                                                                                                                    </ContentTemplate>
                                                                                                                    <Triggers>
                                                                                                                        <asp:PostBackTrigger ControlID="btncancel1" />
                                                                                                                    </Triggers>
                                                                                                                </asp:UpdatePanel>
                                                                                                            </td>
                                                                                                            <td align="left" style="width:25%">
                                                                                                                <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                                                                                                    <ContentTemplate>
                                                                                                                        <asp:Button ID="btnclear" runat="server" CssClass="RClear" 
                                                                                                                            EnableTheming="False" OnClientClick="document.location.href=document.location.href;" TabIndex="7" Text="" />
                                                                                                                          
                                                                                                                    </ContentTemplate>
                                                                                                                    <Triggers>
                                                                                                                        <asp:AsyncPostBackTrigger ControlID="btnclear" EventName="Click" />
                                                                                                                    </Triggers>
                                                                                                                </asp:UpdatePanel>
                                                                                                            </td>
                                                                                                            <td style="width:13%"></td>
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
    

</asp:Content>



