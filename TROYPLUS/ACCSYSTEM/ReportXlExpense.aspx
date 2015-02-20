<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportXlExpense.aspx.cs"
    Inherits="ReportXlExpense" %>

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
        <br />
        <table cellpadding="2" cellspacing="2" width="400px" border="0" style="border: 1px solid Blue;
            text-align: left">
            <tr>
                <td colspan="3" class="headerPopUp">
                    Expense Report
                </td>
            </tr>

            <%--<tr>
                <td colspan="3">
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Date Wise" Font-Bold="True" Font-Size="Medium"></asp:Label>
                            </td>
                        </tr>
                    </table>  
                </td>
            </tr>--%>
            <tr style="height:6px">
            </tr>
            <tr>
                <td>
                
                <cc1:TabContainer ID="tabs2" runat="server" Width="100%" ActiveTabIndex="0" CssClass="fancy fancy-green">
                    <cc1:TabPanel ID="tabMaster" runat="server" HeaderText="Date Wise">
                        <HeaderTemplate>
                            Date Wise
                        </HeaderTemplate>
                        <ContentTemplate>
                            <table cellpadding="3" cellspacing="1" width="400px">
                                <tr style="height:10px">
                                </tr>
                                <tr>
                                    <%--<td class="ControlLabel" style="width: 25%;">
                                        Customer
                                    </td>
                                    <td class="ControlDrpBorder" style="width: 25%;">
                                        <asp:DropDownList TabIndex="1" ID="drpCustomer" AppendDataBoundItems="true"
                                            DataSourceID="srcCreditorDebitor" runat="server" AutoPostBack="false" DataValueField="LedgerID" CssClass="drpDownListMedium" BackColor = "#90c9fc"
                                            DataTextField="LedgerName" ValidationGroup="edit" Width="100%"  style="border: 1px solid #90c9fc" height="26px">
                                            <asp:ListItem style="background-color: #90c9fc" Text="Select Customer" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>--%>
                                    <td class="ControlLabel2" style="width: 20%;">
                                        Start Date
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtStartDate"
                                            Display="None" CssClass="lblFont" ErrorMessage="Please Enter Start Date"></asp:RequiredFieldValidator>
                                    </td>
                                    <td class="ControlTextBox3" style="width: 25%;">
                                        <asp:TextBox ID="txtStartDate" CssClass="cssTextBox" MaxLength="10"
                                            runat="server" />
                                        <cc1:CalendarExtender ID="calStartDate" runat="server" Enabled="True" 
                                            Format="dd/MM/yyyy"
                                            PopupButtonID="btnStartDate" TargetControlID="txtStartDate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td style="width: 13%;">
                                        <%--<script type="text/javascript" language="JavaScript">
                                            new tcal({ 'formname': 'form1', 'controlname': 'txtStartDate' });</script>--%>
                                            <asp:ImageButton ID="btnStartDate" runat="server" CausesValidation="False" 
                                                ImageUrl="App_Themes/NewTheme/images/cal.gif" 
                                                Width="20px" />
                                    </td>
                                </tr>
                                <tr>
                                    <%--<td class="ControlLabel" style="width: 25%;">
                                        Frequency
                                    </td>
                                    <td class="ControlDrpBorder" style="width: 25%;">
                                        <asp:DropDownList ID="drpFrequency" TabIndex="4" AppendDataBoundItems="True" CssClass="drpDownListMedium" BackColor = "#90c9fc"
                                            Width="100%" runat="server" AutoPostBack="false" style="border: 1px solid #90c9fc" height="26px" SelectedValue='<%# Bind("Frequency") %>'
                                            ValidationGroup="salesval">
                                            <asp:ListItem Text="Monthly" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Quarterly" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="Annually" Value="12"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>--%>
                                    <td class="ControlLabel2" style="width: 20%;">
                                        End Date
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEndDate"
                                            Display="None" ErrorMessage="Please Enter The End Date" CssClass="lblFont"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtStartDate"
                                            ControlToValidate="txtEndDate" Display="None" ErrorMessage="Start Date Should Be Less Than the End Date"
                                            CssClass="lblFont" Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
                                    </td>
                                    <td class="ControlTextBox3" style="width: 25%;">
                                        <asp:TextBox ID="txtEndDate" CssClass="cssTextBox" MaxLength="10" runat="server" />
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" 
                                            Format="dd/MM/yyyy"
                                            PopupButtonID="ImageButton1" TargetControlID="txtEndDate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td style="width: 13%;">
                                        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" 
                                                ImageUrl="App_Themes/NewTheme/images/cal.gif" 
                                                Width="20px" />
                                        <%--<script type="text/javascript" language="JavaScript">
                                            new tcal({ 'formname': 'form1', 'controlname': 'txtEndDate' });</script>--%>
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
                                                <td style="width: 40%;">
                                                </td>
                                                <td align="center" style="width: 32%;">
                                                <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" CssClass="exportexl6"
                                                    EnableTheming="false" />
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="Additional Details">
                        <HeaderTemplate>
                            Month Wise
                        </HeaderTemplate>
                        <ContentTemplate>
                            <table width="400px">
                                <tr style="height:5px">

                                </tr>
                                <tr>
                                    <td style="width: 14%;">
                                    </td>
                                    <td style="width: 25%;">
                                        <asp:CheckBox ID="CheckBox1" runat="server" Text="April" style="color:Black" Font-Names="arial" Font-Size="12px" AutoPostBack="true"/>
                                    </td>
                                    <td  style="width: 25%;">
                                        <asp:CheckBox ID="CheckBox2" runat="server" Text="May" style="color:Black" Font-Names="arial" Font-Size="12px" AutoPostBack="true"/>
                                    </td>
                                    <td style="width: 25%;">
                                        <asp:CheckBox ID="CheckBox3" runat="server" Text="June" style="color:Black" Font-Names="arial" Font-Size="12px" AutoPostBack="true"/>
                                    </td>
                                    <td style="width: 10%;">
                                    </td>
                                </tr>
                                <tr>    
                                    <td style="width: 14%;">
                                    </td>                                
                                    <td style="width: 25%;">
                                        <asp:CheckBox ID="CheckBox4" runat="server" Text="July" style="color:Black" Font-Names="arial" Font-Size="12px" AutoPostBack="true"/>
                                    </td>
                                    <td style="width: 25%;">
                                        <asp:CheckBox ID="CheckBox5" runat="server" Text="August" style="color:Black" Font-Names="arial" Font-Size="12px" AutoPostBack="true"/>
                                    </td>
                                    <td style="width: 25%;">
                                        <asp:CheckBox ID="CheckBox6" runat="server" Text="September" style="color:Black" Font-Names="arial" Font-Size="12px" AutoPostBack="true"/>
                                    </td>
                                    <td style="width: 10%;">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 14%;">
                                    </td>
                                    <td style="width: 25%;">
                                        <asp:CheckBox ID="CheckBox7" runat="server" Text="October" style="color:Black" Font-Names="arial" Font-Size="12px" AutoPostBack="true"/>
                                    </td>
                                    <td style="width: 25%;">
                                        <asp:CheckBox ID="CheckBox8" runat="server" Text="November" style="color:Black" Font-Names="arial" Font-Size="12px" AutoPostBack="true"/>
                                    </td>
                                    <td style="width: 25%;">
                                        <asp:CheckBox ID="CheckBox9" runat="server" Text="December" style="color:Black" Font-Names="arial" Font-Size="12px" AutoPostBack="true"/>
                                    </td>
                                    <td style="width: 10%;">
                                    
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 14%;">
                                    </td>
                                    <td style="width: 25%;">
                                        <asp:CheckBox ID="CheckBox10" runat="server" Text="January" style="color:Black" Font-Names="arial" Font-Size="12px" AutoPostBack="true"/>
                                    </td>
                                    <td style="width: 25%;">
                                        <asp:CheckBox ID="CheckBox11" runat="server" Text="February" style="color:Black" Font-Names="arial" Font-Size="12px" AutoPostBack="true"/>
                                    </td>
                                    <td style="width: 25%;">
                                        <asp:CheckBox ID="CheckBox12" runat="server" Text="March" style="color:Black" Font-Names="arial" Font-Size="12px" AutoPostBack="true"/>
                                    </td>
                                    <td style="width: 10%;">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 14%;">
                                    </td>
                                    <td style="width: 25%;">
                                    
                                    </td>
                                    <td style="width: 25%;">
                                        <asp:CheckBox ID="CheckBox13" runat="server" Text="All Months" style="color:Black" Font-Names="arial" OnCheckedChanged="CheckBox13_CheckedChanged"  Font-Size="12px" AutoPostBack="true"/>
                                    </td>
                                    
                                    <td style="width: 20%;">
                                    </td>
                                </tr>
                                <tr style="height:5px">

                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <table width="100%">
                                            <tr>

                                                <td class="ControlTextBox3" style="width: 100%;">
                                                    <asp:RadioButtonList ID="optionrate" runat="server" CssClass="label" 
                                                    RepeatDirection="Horizontal" BackColor="#90C9FC" Height="25px">
                                                            <asp:ListItem Selected="True">Monthly Expense Head Wise</asp:ListItem>
                                                            <asp:ListItem>Monthly Wise</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td colspan="4">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 40%;">
                                                </td>
                                                <td align="center" style="width: 32%;">
                                                <asp:Button ID="btnExcel" runat="server" OnClick="btnExcel_Click" CssClass="exportexl6"
                                                    EnableTheming="false" />
                                                </td>
                                                <td>
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
            Width="100%" OnRowDataBound="gvReport_RowDataBound">
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
