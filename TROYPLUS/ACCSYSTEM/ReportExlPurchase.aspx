<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportExlPurchase.aspx.cs"
    Inherits="ReportExlPurchase" %>

<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <title></title>
    <link href="App_Themes/DefaultTheme/calendar.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="Scripts/calendar_eu.js"></script>
    <link href="App_Themes/NewTheme/base.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/DefaultTheme/DefaultTheme.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">

        function pageLoad() {
            //  get the behavior associated with the tab control
            var tabContainer = $find('ctl00_cplhControlPanel_tabEdit');

//            if (tabContainer == null)
//                tabContainer = $find('ctl00_cplhControlPanel_tabEdit_tabEditAddTab');

            if (tabContainer != null) {
                //  get all of the tabs from the container
                var tabs = tabContainer.get_tabs();

                //  loop through each of the tabs and attach a handler to
                //  the tab header's mouseover event
                for (var i = 0; i < tabs.length; i++) {
                    var tab = tabs[i];

                    $addHandler(
                tab.get_headerTab(),
                'mouseover',
                Function.createDelegate(tab, function () {
                    tabContainer.set_activeTab(this);
                }
            ));
                }
            }
    }


        function CallPrint(strid) {
            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('', '', 'letf=0,top=0,width=600,height=400,toolbar=0,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();

        }
        </script>
    <script language="javascript" type="text/javascript" src="Scripts\calendar_eu.js"></script>

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

</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager runat="server" ID="scr"></asp:ScriptManager>
    <div align="center">
        
        <table cellpadding="2" cellspacing="2" width="100%" border="0" style="border: 0px solid  blue;
            text-align: left">
            <tr>
                <td colspan="3" class="headerPopUp">
                    Purchase Comprehency Report
                </td>
            </tr>
            <tr style="height:5px">
            </tr>
            <tr>
                <td colspan="3" style="width:100%">
                    <cc1:TabContainer ID="tabEdit" runat="server" Width="100%" ActiveTabIndex="0" CssClass="fancy fancy-green">
                        <cc1:TabPanel ID="tabEditMain" runat="server" HeaderText="Basic">
                            <ContentTemplate>
                                <table cellpadding="3" cellspacing="1" style="width:760px">
                                    <tr style="height:10px">
                                    </tr>
                                    <tr>
                                        <td class="ControlLabel" style="width: 40%;">
                                            Start Date
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtStartDate"
                                                Display="None" CssClass="lblFont" ErrorMessage="Please Enter Start Date"></asp:RequiredFieldValidator>
                                        </td>
                                        <td class="ControlTextBox3"  style="width: 25%;">
                                            <asp:TextBox ID="txtStartDate" Enabled="false" CssClass="cssTextBox" MaxLength="10"
                                                runat="server" />
                                            <cc1:CalendarExtender ID="calStartDate" runat="server" Enabled="True" 
                                                Format="dd/MM/yyyy"
                                                PopupButtonID="btnStartDate" TargetControlID="txtStartDate">
                                            </cc1:CalendarExtender>
                                        </td>
                                        <td style="width: 35%;" align="left">
                                            <asp:ImageButton ID="btnStartDate" runat="server" CausesValidation="False" 
                                                    ImageUrl="App_Themes/NewTheme/images/cal.gif" 
                                                    Width="20px" />
                                        </td>
                                    </tr>
                                    <tr style="height: 2px;"/> 
                                    <tr>
                                        <td class="ControlLabel" style="width: 40%;">
                                            End Date
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEndDate"
                                                Display="None" ErrorMessage="Please Enter The End Date" CssClass="lblFont"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtStartDate"
                                                ControlToValidate="txtEndDate" Display="None" ErrorMessage="Start Date Should Be Less Than the End Date"
                                                CssClass="lblFont" Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
                                        </td>
                                        <td class="ControlTextBox3" style="width: 25%;">
                                            <asp:TextBox ID="txtEndDate" Enabled="false" CssClass="cssTextBox" MaxLength="10" runat="server" />
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" 
                                                                                                Format="dd/MM/yyyy"
                                                                                                PopupButtonID="ImageButton1" TargetControlID="txtEndDate">
                                                                                            </cc1:CalendarExtender>
                                        </td>
                                        <td style="width: 35%;" align="left">
                                            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" 
                                                                                                ImageUrl="App_Themes/NewTheme/images/cal.gif" 
                                                                                                Width="20px" />
                                        </td>
                                    </tr>
                                    <tr style="height: 2px;"/> 
                                    <tr>
                                        <td class="ControlLabel"  width="40%">
                                            Category
                                        </td>
                                        <td style="width: 25%;" class="ControlDrpBorder">
                                             <asp:DropDownList ID="ddlCategory" runat="server" Width="100%" AutoPostBack="true" style="border: 1px solid #e7e7e7" height="26px"
                                                CssClass="drpDownListMedium" BackColor = "#e7e7e7" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                                <asp:ListItem Selected="True" Value="0" style="background-color: #90c9fc">All</asp:ListItem>
                                             </asp:DropDownList>
                                        </td>
                                        <td style="width: 35%;">

                                        </td>
                                    </tr>
                                    <tr style="height: 2px;"/> 
                                    <tr>
                                        <td class="ControlLabel"  width="40%">
                                            Brand
                                        </td>
                                        <td style="width: 25%;" class="ControlDrpBorder">
                                             <asp:DropDownList ID="ddlBrand" runat="server" Width="100%" AutoPostBack="true" style="border: 1px solid #e7e7e7" height="26px"
                                                CssClass="drpDownListMedium" BackColor = "#e7e7e7" OnSelectedIndexChanged="ddlBrand_SelectedIndexChanged">
                                                <asp:ListItem Selected="True" Value="All" style="background-color: #90c9fc">All</asp:ListItem>
                                             </asp:DropDownList>
                                        </td>
                                        <td style="width: 35%;">

                                        </td>
                                    </tr>
                                    <tr style="height: 2px;"/> 
                                    <tr>
                                        <td class="ControlLabel"  width="40%">
                                             Product Name
                                        </td>
                                        <td style="width: 25%;" class="ControlDrpBorder">
                                             <asp:DropDownList ID="ddlproduct" runat="server" Width="100%"  style="border: 1px solid #e7e7e7" height="26px"
                                                CssClass="drpDownListMedium" BackColor = "#e7e7e7">
                                                <asp:ListItem Selected="True" Value="All" style="background-color: #90c9fc">All</asp:ListItem>
                                             </asp:DropDownList>
                                        </td>
                                        <td style="width: 35%;">

                                        </td>
                                    </tr>                                    
                                    <%--<tr>--%>
                                        <%--<td class="ControlLabel" style="width: 25%;">
                                            Missed Visits
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:CheckBox ID="chkMissedVisit" runat="server" />
                                        </td>
                                        <td class="leftCol" style="width: 15%;">
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:CheckBox ID="chkVisitMade" runat="server" Visible="false" />
                                        </td>--%>
                                    <%--</tr>--%>
                                    <%--<tr>
                                        <td class="ControlLabel" style="width: 15%;">
                                            Option
                                        </td>
                                        <td class="ControlTextBox3" style="width: 30%;">
                                            <asp:RadioButtonList ID="optionrate" runat="server" CssClass="label" 
                                                    RepeatDirection="Horizontal" BackColor="#90C9FC" Height="25px">
                                                    <asp:ListItem Selected="True">Date Wise</asp:ListItem>
                                                    <asp:ListItem>Month Wise</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        </tr>
                                    <tr>--%>
                                    <tr style="height:10px">
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 43%;">
                                                    </td>
                                                    <td align="center" style="width: 20%;">
                                                    <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" CssClass="exportexl6"
                                                        EnableTheming="false" />
                                                    </td>
                                                    <td style="width: 37%;">
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                     </tr>
                                  </table>
                              </ContentTemplate>
                          </cc1:TabPanel>
                          <cc1:TabPanel ID="tabEditAddTab" runat="server" HeaderText="Advance">
                               <ContentTemplate>   
                                   <table cellpadding="1" cellspacing="2" style="width:760px">
                                        <tr style="height:10px">
                                        </tr>
                                        <tr>
                                            <td class="ControlLabel" style="width: 10%;">
                                                Start Date
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtstdate"
                                                    Display="None" CssClass="lblFont" ErrorMessage="Please Enter Start Date"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="ControlTextBox3" style="width: 18%;">
                                                <asp:TextBox ID="txtstdate" Enabled="false" CssClass="cssTextBox" MaxLength="10"
                                                    runat="server" />
                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" 
                                                    Format="dd/MM/yyyy"
                                                    PopupButtonID="ImageButton2" TargetControlID="txtstdate">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td style="width: 3%;" align="left">
                                                <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" 
                                                        ImageUrl="App_Themes/NewTheme/images/cal.gif" 
                                                        Width="20px" />
                                            </td>
                                            <td style="width: 7%;" class="ControlLabel">
                                                End Date
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txteddate"
                                                    Display="None" ErrorMessage="Please Enter The End Date" CssClass="lblFont"></asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="txtstDate"
                                                    ControlToValidate="txtedDate" Display="None" ErrorMessage="Start Date Should Be Less Than the End Date"
                                                    CssClass="lblFont" Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
                                            </td>
                                            <td style="width: 18%;" class="ControlTextBox3">
                                                <asp:TextBox ID="txteddate" Enabled="false" CssClass="cssTextBox" MaxLength="10" runat="server" />
                                                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" 
                                                                                                    Format="dd/MM/yyyy"
                                                                                                    PopupButtonID="ImageButton3" TargetControlID="txtedDate">
                                                                                                </cc1:CalendarExtender>
                                            </td>
                                            <td style="width: 5%;">
                                                <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="False" 
                                                                                                    ImageUrl="App_Themes/NewTheme/images/cal.gif" 
                                                                                                    Width="20px" />
                                            </td>
                                            <td style="width: 20%;">
                                                
                                            </td>
                                        </tr>
                                       <tr style="height: 2px;"/> 
                                        <tr>
                                            <td class="ControlLabel" style="width: 10%;">
                                                Option
                                            </td>
                                            <td class="ControlTextBox3" style="width: 18%;">
                                                <asp:RadioButtonList ID="optoption" runat="server" RepeatDirection="Horizontal"
                                                                    >
                                                                    <asp:ListItem Text="GroupBy" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="Normal"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                            </td>
                                            <td class="ControlLabel" style="width: 3%;">
                                                
                                            </td>
                                            <td class="ControlLabel" style="width: 7%;">
                                                
                                            </td>
                                            <td class="ControlLabel" style="width: 18%;">
                                                
                                            </td>
                                            <td class="ControlLabel" style="width: 5%;">
                                                
                                            </td>
                                            <td class="ControlLabel" style="width: 20%;">
                                                
                                            </td>
                                        </tr>
                                       <tr style="height: 2px;"/> 
                                        <tr>
                                            <td colspan="7">
                                              <table cellpadding="0" cellspacing="0" style="width:100%">
                                                  <tr>
                                                        <td class="ControlLabel" style="width: 12%;">
                                                            Option
                                                        </td>
                                                        <td style="width: 62%;"  class="ControlTextBox3">
                                                            <asp:RadioButtonList ID="opttype" runat="server"
                                                                Width="100%" RepeatDirection="Horizontal" >
                                                                <asp:ListItem Text="Purchase" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="Internal Transfer"></asp:ListItem>
                                                                <asp:ListItem Text="Delivery Note"></asp:ListItem>
                                                                <asp:ListItem Text="Sales Return"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                       <td  style="width: 27%;" align="left">
                                                            
                                                        </td>
                                                        
                                                    </tr>
                                                </table>
                                            </td>
                                       </tr>
                                       <tr style="height: 2px;"/> 
                                       <tr>
                                          <td colspan="7">
                                              <table cellpadding="1" cellspacing="1" style="width:100%">
                                                  <tr>
                                                      <td  class="ControlLabel" style="width: 11%;">
                                                        Option
                                                      </td>
                                                      <td class="ControlTextBox3" style="width: 40%;">
                                                          <asp:RadioButtonList ID="opt" runat="server"
                                                              Width="100%">
                                                              <asp:ListItem Text="Category Wise" Selected="True"></asp:ListItem>
                                                              <asp:ListItem Text="Product Wise"></asp:ListItem>
                                                              <asp:ListItem Text="Brand Wise"></asp:ListItem>
                                                              <asp:ListItem Text="Brand / Product Wise"></asp:ListItem>
                                                              <asp:ListItem Text="Brand / Product / Model Wise"></asp:ListItem>
                                                              <asp:ListItem Text="Bill Wise"></asp:ListItem>
                                                              <asp:ListItem Text="Brand / Model Wise"></asp:ListItem>
                                                              <asp:ListItem Text="Category / Brand Wise"></asp:ListItem>
                                                              <asp:ListItem Text="Category / Brand / Product Wise"></asp:ListItem>
                                                              <asp:ListItem Text="PayMode Wise"></asp:ListItem>
                                                              <asp:ListItem Text="Date Wise"></asp:ListItem>
                                                              <asp:ListItem Text="Month Wise"></asp:ListItem>
                                                              <asp:ListItem Text="SupplierWise"></asp:ListItem>
                                                          </asp:RadioButtonList>
                                                      </td>
                                                      <td class="ControlTextBox3" style="width: 12%;">
                                                          <asp:CheckBox ID="chkboxQty" runat="server" Text="Qty"/>
                                                          <asp:CheckBox ID="chkboxrate" runat="server" Text="Rate"/>
                                                          <asp:CheckBox ID="chkboxVal" runat="server" Text="Value"/>
                                                          <asp:CheckBox ID="chkboxAvg" runat="server" Text="Avg"/>
                                                          <asp:CheckBox ID="chkboxPer" runat="server" Text="Per%"/>
                                                      </td>
                                                      <td class="ControlTextBox3" style="width: 16%;">
                                                          <asp:CheckBox ID="chkboxNlcvalue" runat="server" Text="NLC Value"/>
                                                          <asp:CheckBox ID="chkboxNlcper" runat="server" Text="NLC Per%"/>
                                                          <asp:CheckBox ID="chkboxMRPvalue" runat="server" Text="MRP Value"/>
                                                          <asp:CheckBox ID="chkboxMRPper" runat="server" Text="MRP Per%" />
                                                          <asp:CheckBox ID="chkboxDpvalue" runat="server" Text="DP Value" />
                                                          <asp:CheckBox ID="chkboxDpper" runat="server" Text="DP Per%" />
                                                      </td>
                                                      <td style="width: 16%;">
                                                          <asp:CheckBox ID="chkgpmrp" runat="server" Text="GP for MRP" Visible="False" />
                                                          <asp:CheckBox ID="chkgpnlc" runat="server" Text="GP for NLC" Visible="False" />
                                                          <asp:CheckBox ID="chkgpdp" runat="server" Text="GP for DP" Visible="False" />
                                                      </td>
                                                      
                                                  </tr>
                                              </table>
                                          </td>
                                       </tr>
                                       <tr style="height:1px">
                                       </tr>
                                       
                                       <tr>
                                        <td colspan="5">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 39%;">
                                                    </td>
                                                    <td align="center" style="width: 20%;">
                                                        <asp:Button ID="Btnexl" runat="server" OnClick="Btnexl_Click" CssClass="exportexl6"
                                                            EnableTheming="false" />
                                                    </td>
                                                    <td style="width: 41%;">
                                                        
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                     </tr>
                                   </table>
                               </ContentTemplate>
                          </cc1:TabPanel>
                     </cc1:TabContainer>
                </td>
             </tr>
        </table>
    </div>
    <br />
    <div id="divPrint" runat="server" visible="false" style="font-family: 'Trebuchet MS';
        font-size: 11px;">
        <table width="700px" border="0" style="font-family: Trebuchet MS; font-size: 14px;">
            <tr>
                <td width="140px" align="left">
                    TIN#:
                    <asp:Label ID="lblTNGST" runat="server"></asp:Label>
                </td>
                <td align="center" width="420px" style="font-size: 20px;">
                    <asp:Label ID="lblCompany" runat="server"></asp:Label>
                </td>
                <td width="140px" align="left">
                    Ph:
                    <asp:Label ID="lblPhone" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left">
                    GST#:
                    <asp:Label ID="lblGSTno" runat="server"></asp:Label>
                </td>
                <td align="center">
                    <asp:Label ID="lblAddress" runat="server"></asp:Label>
                </td>
                <td align="left">
                    Date:
                    <asp:Label ID="lblBillDate" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td align="center">
                    <asp:Label ID="lblCity" runat="server" />
                    -
                    <asp:Label ID="lblPincode" runat="server"></asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td align="center">
                    <asp:Label ID="lblState" runat="server"> </asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        <tr>

        <wc:ReportGridView runat="server" BorderWidth="1" ID="gvRepor" GridLines="Both"
            AlternatingRowStyle-CssClass="even" AutoGenerateColumns="true" PrintPageSize="40"
            AllowPrintPaging="true" EmptyDataText="No Data Found" SkinID="gridview" CssClass="gridview"
            Width="100%" >
            <RowStyle CssClass="dataRow" />
            <SelectedRowStyle CssClass="SelectdataRow" />
            <AlternatingRowStyle CssClass="altRow" />
            <EmptyDataRowStyle CssClass="HeadataRow" Font-Bold="true" />
            <HeaderStyle CssClass="HeadataRow" Wrap="false" />
            <FooterStyle CssClass="dataRow" />
            <PagerStyle CssClass="footer-row allPad" VerticalAlign="Middle" HorizontalAlign="Left" />
            <PageHeaderTemplate>
                <br />
                <br />
            </PageHeaderTemplate>
            <Columns>
            </Columns>
            <PagerTemplate>
            </PagerTemplate>
            <PageFooterTemplate>
                <br />
            </PageFooterTemplate>
        </wc:ReportGridView>
        </tr>
        </table>
        <%--<asp:ObjectDataSource ID="srcCreditorDebitor" runat="server" SelectMethod="ListCustomersDealers"
            TypeName="BusinessLogic">
            <SelectParameters>
                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>--%>
    </div>
    </form>
</body>
</html>
