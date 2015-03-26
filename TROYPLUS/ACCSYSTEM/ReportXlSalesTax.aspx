<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportXlSalesTax.aspx.cs"
    Inherits="ReportXlSalesTax" %>

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
        <table cellpadding="2" cellspacing="2" width="400px" border="0" style="border: 1px solid Blue; background-color:White;
            text-align: left">
            <tr>
                <td colspan="3" class="headerPopUp">
                    Sales Tax Filing Report
                </td>
            </tr>
            <tr style="height:5px">
            </tr>
            <tr>
                <td>
                
                <cc1:TabContainer ID="tabs2" runat="server" Width="100%" ActiveTabIndex="0" CssClass="fancy fancy-green">
                    <cc1:TabPanel ID="tabMaster" runat="server" HeaderText="Purchase">
                        <HeaderTemplate>
                            Purchase
                        </HeaderTemplate>
                        <ContentTemplate>
                            <table cellpadding="3" cellspacing="1" width="400px">
                                <tr style="height:5px">
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:label runat="server" Text="Purchase Annuxere" Font-Bold="True" Font-Size="Medium" ForeColor="Blue">
                                        </asp:label>
                                    </td>
                                </tr>
                                <tr style="height:5px">
                                </tr>
                                <tr>
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
                                        <asp:ImageButton ID="btnStartDate" runat="server" CausesValidation="False" 
                                                ImageUrl="App_Themes/NewTheme/images/cal.gif" 
                                                Width="20px" />
                                    </td>
                                </tr>
                                <tr style="height: 2px;"/>
                                <tr>
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
                                    </td>
                                </tr>
                                <tr style="height: 2px;"/>
                                <tr>
                                    <td class="ControlLabel2" style="width: 20%;">
                                        Branch
                                    </td>
                                    <td class="ControlDrpBorder" style="width: 25%;">
                                        <asp:DropDownList ID="drpBranchAdd" Width="100%" DataTextField="BranchName" DataValueField="Branchcode" CssClass="drpDownListMedium" AppendDataBoundItems="true" BackColor="#e7e7e7" Style="border: 1px solid #e7e7e7" Height="26px"
                                                                                                        runat="server">
                                                                                                    </asp:DropDownList>
                                    </td>
                                    <td style="width: 13%;">
                                        
                                    </td>
                                </tr>
                                <tr style="height: 2px;"/>
                                <tr>
                                    <td class="ControlLabel2" style="width: 15%;">
                                        Option
                                    </td>
                                    <td class="ControlTextBox3" style="width: 30%;">
                                        <asp:RadioButtonList ID="optionrate" runat="server" CssClass="label" 
                                                RepeatDirection="Horizontal" BackColor="#e7e7e7" Height="25px">
                                                <asp:ListItem>5%</asp:ListItem>
                                                <asp:ListItem>14.5%</asp:ListItem>
                                                <asp:ListItem Selected="True">All</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr style="height: 2px;"/>
                                <tr>
                                    <td colspan="3">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 40%;">
                                                </td>
                                                <td align="center" style="width: 32%;">
                                                    <asp:Button ID="Button4" EnableTheming="false" runat="server" CssClass="NewReport6"
                                            Width="120px" OnClick="btnReport1_Click" />
                                                <asp:Button ID="btnReport" runat="server" OnClick="btnReport2_Click" CssClass="exportexl6"
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
                    <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="Sales">
                        <HeaderTemplate>
                            Sales
                        </HeaderTemplate>
                        <ContentTemplate>
                            <table  cellpadding="3" cellspacing="1" width="400px">
                                <tr style="height:5px">
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:label ID="Label1" runat="server" Text="Sales Annuxere" Font-Bold="True" Font-Size="Medium" ForeColor="Blue">
                                        </asp:label>
                                    </td>
                                </tr>
                                <tr style="height:5px">
                                </tr>
                                <tr>
                                    <td class="ControlLabel2" style="width: 20%;">
                                        Start Date
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtStartDt"
                                            Display="None" CssClass="lblFont" ErrorMessage="Please Enter Start Date"></asp:RequiredFieldValidator>
                                    </td>
                                    <td class="ControlTextBox3" style="width: 25%;">
                                        <asp:TextBox ID="txtStartDt" CssClass="cssTextBox" MaxLength="10"
                                            runat="server" />
                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" 
                                            Format="dd/MM/yyyy"
                                            PopupButtonID="ImageButton2" TargetControlID="txtStartDt">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td style="width: 13%;">
                                        <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" 
                                                ImageUrl="App_Themes/NewTheme/images/cal.gif" 
                                                Width="20px" />
                                    </td>
                                </tr>
                                <tr style="height: 2px;"/>
                                <tr>
                                    <td class="ControlLabel2" style="width: 20%;">
                                        End Date
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtEndDt"
                                            Display="None" ErrorMessage="Please Enter The End Date" CssClass="lblFont"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="txtStartDt"
                                            ControlToValidate="txtEndDt" Display="None" ErrorMessage="Start Date Should Be Less Than the End Date"
                                            CssClass="lblFont" Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
                                    </td>
                                    <td class="ControlTextBox3" style="width: 25%;">
                                        <asp:TextBox ID="txtEndDt" CssClass="cssTextBox" MaxLength="10" runat="server" />
                                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" 
                                            Format="dd/MM/yyyy"
                                            PopupButtonID="ImageButton3" TargetControlID="txtEndDt">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td style="width: 13%;">
                                        <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="False" 
                                                ImageUrl="App_Themes/NewTheme/images/cal.gif" 
                                                Width="20px" />
                                    </td>
                                </tr>
                                <tr style="height: 2px;"/>
                                <tr>
                                    <td class="ControlLabel2" style="width: 20%;">
                                        Branch
                                    </td>
                                    <td class="ControlDrpBorder" style="width: 25%;">
                                        <asp:DropDownList ID="DropDownList1" Width="100%" DataTextField="BranchName" DataValueField="Branchcode" CssClass="drpDownListMedium" AppendDataBoundItems="true" BackColor="#e7e7e7" Style="border: 1px solid #e7e7e7" Height="26px"
                                                                                                        runat="server">
                                                                                                    </asp:DropDownList>
                                    </td>
                                    <td style="width: 13%;">
                                        
                                    </td>
                                </tr>
                                <tr style="height: 2px;"/>
                                <tr>
                                    <td class="ControlLabel2" style="width: 15%;">
                                        Option
                                    </td>
                                    <td class="ControlTextBox3" style="width: 30%;">
                                        <asp:RadioButtonList ID="optionsal" runat="server" CssClass="label" 
                                                RepeatDirection="Horizontal" BackColor="#e7e7e7" Height="25px">
                                                <asp:ListItem>5%</asp:ListItem>
                                                <asp:ListItem>14.5%</asp:ListItem>
                                                <asp:ListItem Selected="True">All</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr style="height: 2px;"/>
                                <tr>
                                    <td colspan="3">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 40%;">
                                                </td>
                                                <td align="center" style="width: 32%;">
                                                    <asp:Button ID="Button3" EnableTheming="false" runat="server" CssClass="NewReport6"
                                            Width="120px" OnClick="btnReport22_Click" />
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
                    <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="Purchase Return">
                        <HeaderTemplate>
                            Purchase Return
                        </HeaderTemplate>
                        <ContentTemplate>
                            <table  cellpadding="3" cellspacing="1" width="400px">
                                <tr style="height:5px">
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:label ID="Label2" runat="server" Text="Purchase Return Annuxere" Font-Bold="True" Font-Size="Medium" ForeColor="Blue">
                                        </asp:label>
                                    </td>
                                </tr>
                                <tr style="height:5px">
                                </tr>
                                <tr>
                                    <td class="ControlLabel2" style="width: 20%;">
                                        Start Date
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtStrtDt"
                                            Display="None" CssClass="lblFont" ErrorMessage="Please Enter Start Date"></asp:RequiredFieldValidator>
                                    </td>
                                    <td class="ControlTextBox3" style="width: 25%;">
                                        <asp:TextBox ID="txtStrtDt"  CssClass="cssTextBox" MaxLength="10"
                                            runat="server" />
                                        <cc1:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" 
                                            Format="dd/MM/yyyy"
                                            PopupButtonID="ImageButton4" TargetControlID="txtStrtDt">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td style="width: 13%;">
                                        <asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="False" 
                                                ImageUrl="App_Themes/NewTheme/images/cal.gif" 
                                                Width="20px" />
                                    </td>
                                </tr>
                                <tr style="height: 2px;"/>
                                <tr>
                                    <td class="ControlLabel2" style="width: 20%;">
                                        End Date
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtEdDt"
                                            Display="None" ErrorMessage="Please Enter The End Date" CssClass="lblFont"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToCompare="txtStrtDt"
                                            ControlToValidate="txtEdDt" Display="None" ErrorMessage="Start Date Should Be Less Than the End Date"
                                            CssClass="lblFont" Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
                                    </td>
                                    <td class="ControlTextBox3" style="width: 25%;">
                                        <asp:TextBox ID="txtEdDt" CssClass="cssTextBox" MaxLength="10" runat="server" />
                                        <cc1:CalendarExtender ID="CalendarExtender5" runat="server" Enabled="True" 
                                            Format="dd/MM/yyyy"
                                            PopupButtonID="ImageButton5" TargetControlID="txtEdDt">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td style="width: 13%;">
                                        <asp:ImageButton ID="ImageButton5" runat="server" CausesValidation="False" 
                                                ImageUrl="App_Themes/NewTheme/images/cal.gif" 
                                                Width="20px" />
                                    </td>
                                </tr>
                                <tr style="height: 2px;"/>
                                <tr>
                                    <td class="ControlLabel2" style="width: 20%;">
                                        Branch
                                    </td>
                                    <td class="ControlDrpBorder" style="width: 25%;">
                                        <asp:DropDownList ID="DropDownList2" Width="100%" DataTextField="BranchName" DataValueField="Branchcode" CssClass="drpDownListMedium" AppendDataBoundItems="true" BackColor="#e7e7e7" Style="border: 1px solid #e7e7e7" Height="26px"
                                                                                                        runat="server">
                                                                                                    </asp:DropDownList>
                                    </td>
                                    <td style="width: 13%;">
                                        
                                    </td>
                                </tr>
                                <tr style="height: 2px;"/>
                                <tr>
                                    <td class="ControlLabel2" style="width: 15%;">
                                        Option
                                    </td>
                                    <td class="ControlTextBox3" style="width: 30%;">
                                        <asp:RadioButtonList ID="optionpurret" runat="server" CssClass="label" 
                                                RepeatDirection="Horizontal" BackColor="#e7e7e7" Height="25px">
                                                <asp:ListItem>5%</asp:ListItem>
                                                <asp:ListItem>14.5%</asp:ListItem>
                                                <asp:ListItem Selected="True">All</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr style="height: 2px;"/>
                                <tr>
                                    <td colspan="3">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 40%;">
                                                </td>
                                                <td align="center" style="width: 32%;">
                                                    <asp:Button ID="Button1" EnableTheming="false" runat="server" CssClass="NewReport6"
                                                        Width="120px" OnClick="btnReport3_Click" />
                                                <asp:Button ID="btnRep" runat="server" OnClick="btnRep_Click" CssClass="exportexl6"
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
                    <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="Sales Return">
                        <HeaderTemplate>
                            Sales Return
                        </HeaderTemplate>
                        <ContentTemplate>
                            <table  cellpadding="3" cellspacing="1" width="400px">
                                <tr style="height:5px">
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:label ID="Label3" runat="server" Text="Sales Return Annuxere" Font-Bold="True" Font-Size="Medium" ForeColor="Blue">
                                        </asp:label>
                                    </td>
                                </tr>
                                <tr style="height:5px">
                                </tr>
                                <tr>
                                    <td class="ControlLabel2" style="width: 20%;">
                                        Start Date
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtStDate"
                                            Display="None" CssClass="lblFont" ErrorMessage="Please Enter Start Date"></asp:RequiredFieldValidator>
                                    </td>
                                    <td class="ControlTextBox3" style="width: 25%;">
                                        <asp:TextBox ID="txtStDate" CssClass="cssTextBox" MaxLength="10"
                                            runat="server" />
                                        <cc1:CalendarExtender ID="CalendarExtender6" runat="server" Enabled="True" 
                                            Format="dd/MM/yyyy"
                                            PopupButtonID="ImageButton6" TargetControlID="txtStDate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td style="width: 13%;">
                                        <asp:ImageButton ID="ImageButton6" runat="server" CausesValidation="False" 
                                                ImageUrl="App_Themes/NewTheme/images/cal.gif" 
                                                Width="20px" />
                                    </td>
                                </tr>
                                <tr style="height: 2px;"/>
                                <tr>
                                    <td class="ControlLabel2" style="width: 20%;">
                                        End Date
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtEdDate"
                                            Display="None" ErrorMessage="Please Enter The End Date" CssClass="lblFont"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToCompare="txtStDate"
                                            ControlToValidate="txtEdDate" Display="None" ErrorMessage="Start Date Should Be Less Than the End Date"
                                            CssClass="lblFont" Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
                                    </td>
                                    <td class="ControlTextBox3" style="width: 25%;">
                                        <asp:TextBox ID="txtEdDate" CssClass="cssTextBox" MaxLength="10" runat="server" />
                                        <cc1:CalendarExtender ID="CalendarExtender7" runat="server" Enabled="True" 
                                            Format="dd/MM/yyyy"
                                            PopupButtonID="ImageButton7" TargetControlID="txtEdDate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td style="width: 13%;">
                                        <asp:ImageButton ID="ImageButton7" runat="server" CausesValidation="False" 
                                                ImageUrl="App_Themes/NewTheme/images/cal.gif" 
                                                Width="20px" />
                                    </td>
                                </tr>
                                <tr style="height: 2px;"/>
                                <tr>
                                    <td class="ControlLabel2" style="width: 20%;">
                                        Branch
                                    </td>
                                    <td class="ControlDrpBorder" style="width: 25%;">
                                        <asp:DropDownList ID="DropDownList3" Width="100%" DataTextField="BranchName" DataValueField="Branchcode" CssClass="drpDownListMedium" AppendDataBoundItems="true" BackColor="#e7e7e7" Style="border: 1px solid #e7e7e7" Height="26px"
                                                                                                        runat="server">
                                                                                                    </asp:DropDownList>
                                    </td>
                                    <td style="width: 13%;">
                                        
                                    </td>
                                </tr>
                                <tr style="height: 2px;"/>
                                <tr>
                                    <td class="ControlLabel2" style="width: 15%;">
                                        Option
                                    </td>
                                    <td class="ControlTextBox3" style="width: 30%;">
                                        <asp:RadioButtonList ID="optionsalret" runat="server" CssClass="label" 
                                                RepeatDirection="Horizontal" BackColor="#e7e7e7" Height="25px">
                                                <asp:ListItem>5%</asp:ListItem>
                                                <asp:ListItem>14.5%</asp:ListItem>
                                                <asp:ListItem Selected="True">All</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr style="height: 2px;"/>
                                <tr>
                                    <td colspan="3">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 40%;">
                                                </td>
                                                <td align="center" style="width: 32%;">
                                                    <asp:Button ID="Button2" EnableTheming="false" runat="server" CssClass="NewReport6"
                                            Width="120px" OnClick="btnReport4_Click" />
                                                <asp:Button ID="btnt" runat="server" OnClick="btnt_Click" CssClass="exportexl6"
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
