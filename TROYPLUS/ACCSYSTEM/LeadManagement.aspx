<%@ Page Title="" Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true" CodeFile="LeadManagement.aspx.cs" Inherits="LeadManagement" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" Runat="Server">
<script language="javascript" type="text/javascript">

    /*@cc_on@*/
    /*@if (@_win32 && @_jscript_version>=5)

    function window.confirm(str) {
        execScript('n = msgbox("' + str + '","4132")', "vbscript");
        return (n == 6);
    }

    @end@*/


    function pageLoad() {
        //  get the behavior associated with the tab control
        var tabContainer = $find('ctl00_cplhControlPanel_tabs2');

//        if (tabContainer == null)
//            tabContainer = $find('ctl00_cplhControlPanel_tabPanel2');

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

    function OnKeyPress(args) {
        if (args.keyCode == Sys.UI.Key.esc) {
            $find("ctl00_cplhControlPanel_ModalPopupExtender2").hide();
        }
    }

    $("#ctl00_cplhControlPanel_UpdateCancelButton").live("click", function () {
        $find("ctl00_cplhControlPanel_ModalPopupExtender2").hide();
    });

    function CheckLeadContact() {

    }
   
    </script>

        <style id="Style1" runat="server">
         .someClass td
        {
            font-size: 12px;
            border : 1px solid Gray ;
        }
        
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


    <asp:UpdatePanel ID="UpdatePanelPage" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table style="width: 100%;" align="center">
                <tr style="width: 100%">
                    <td style="width: 100%" valign="middle">
                        
                            <%--<div class="mainConHd">
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tr valign="middle">
                                        <td>
                                            <span>Lead Management</span>
                                        </td>
                                    </tr>
                                </table>
                            </div>--%>
                            <%--<table class="mainConHd" style="width: 994px;">
                                <tr valign="middle">
                                    <td style="font-size: 20px;">
                                        Lead Management
                                    </td>
                                </tr>
                            </table>--%>
                            <div class="mainConBody">
                                <div>
                                    <table cellspacing="2px" cellpadding="3px" border="0" width="100%"  style="margin: -1px 0px 0px 1px;"
                                        class="searchbg">
                                        <tr>
                                            <td style="width: 2%">
                                            </td>
                                            <td style="width: 20%; font-size: 22px; color: #000000;" >
                                                Lead Management
                                            </td>
                                            <td style="width: 13%">
                                                <div style="text-align: right;">
                                                    <asp:Panel ID="pnlSearch" runat="server" Width="100px">
                                                        <asp:Button ID="lnkBtnAdd" runat="server" OnClick="lnkBtnAdd_Click" CssClass="ButtonAdd66" CausesValidation="false"
                                                            EnableTheming="false" Width="80px" Text=""></asp:Button>
                                                    </asp:Panel>
                                                </div>
                                            </td>
                                            <td style="width: 5%; color: #000080;" align="right">
                                                Search
                                            </td>
                                            <td style="width: 17%" class="Box1">
                                                <asp:TextBox ID="txtSearch" runat="server" CssClass="cssTextBox" Width="92%"></asp:TextBox>
                                            </td>
                                            
                                            <td style="width: 18%" class="Box1">
                                                <div style="width: 150px; font-family: 'Trebuchet MS';">
                                                    <asp:DropDownList ID="ddCriteria" runat="server"  BackColor="#BBCAFB"  Width="157px" Height="23px" style="text-align:center;border:1px solid #BBCAFB ">
                                                        <asp:ListItem Value="0">All</asp:ListItem>
                                                        <asp:ListItem Value="LeadID">Lead ID</asp:ListItem>
                                                        <asp:ListItem Value="CustomerName">Customer Name</asp:ListItem>
                                                        <asp:ListItem Value="CreationDate">Creation Date</asp:ListItem>
                                                        <asp:ListItem Value="Status">Status</asp:ListItem>
                                                        <asp:ListItem Value="Mobile">Mobile</asp:ListItem>
                                                        <asp:ListItem Value="LastCompletedAction">Last Completed Action</asp:ListItem>
                                                        <asp:ListItem Value="NextAction">Next Action</asp:ListItem>
                                                        <asp:ListItem Value="Category">Category</asp:ListItem>
                                                        <asp:ListItem Value="BusinessType">Business Type</asp:ListItem>
                                                        <asp:ListItem Value="Branch">Branch</asp:ListItem>
                                                    </asp:DropDownList>
                                            </td>
                                            <%--<td style="width: 22%" class="Box">
                                                <div style="width: 100px; font-family: 'Trebuchet MS';">
                                                    <asp:DropDownList ID="ddmethods" runat="server"   Width="154px" Height="23px" style="text-align:center;border:1px solid White ">
                                                        <asp:ListItem Value="0" style="background-color: White">All</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </td>--%>
                                            <td style="width: 13%; text-align: left">
                                                <asp:Button ID="btnSearch" runat="server" Text="" OnClick="btnSearch_Click" CausesValidation="false"
                                                     CssClass="ButtonSearch6" EnableTheming="false" ForeColor="White" />
                                            </td>
                                            <td style="width: 25%">
                                                <div style="text-align: right;">
                                                    <asp:Panel ID="Panel1" runat="server" Width="100px">
                                                        <asp:Button ID="AddTheRef" runat="server" OnClick="AddTheRef_Click" CssClass="addReferencebutton6" CausesValidation="false"
                                                            EnableTheming="false" Width="80px" Text=""></asp:Button>
                                                    </asp:Panel>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        
                        <input id="dummy" type="button" style="display: none" runat="server" />
                        <input id="Button1" type="button" style="display: none" runat="server" />
                        <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
                            CancelControlID="Button1" DynamicServicePath="" Enabled="True" PopupControlID="popUp"
                            TargetControlID="dummy">
                        </cc1:ModalPopupExtender>
                        <asp:Panel runat="server" ID="popUp" Style="width: 60%; display: none">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <div id="contentPopUp">
                                        <asp:Panel ID="pnlEdit" runat="server">
                                            <table class="tblLeft" cellpadding="0" cellspacing="0" style="border: 0px solid #5078B3;
                                                background-color: #fff; color: #000;" width="100%">
                                                <tr>
                                                    <td>
                                                        <div class="divArea">
                                                            <table class="tblLeft" cellpadding="3" cellspacing="3" style="padding-left: 2px; width: 100%;"
                                                                >
                                                                <tr>
                                                                    <td colspan="5">
                                                                        <table class="headerPopUp">
                                                                            <tr>
                                                                                <td>
                                                                                    Lead Management
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="5">
                                                                        <cc1:TabContainer ID="tabs2" runat="server" Width="100%" ActiveTabIndex="0" CssClass="fancy fancy-green">
                                                                            <cc1:TabPanel ID="tabMaster" runat="server" HeaderText="Lead Details">
                                                                                <HeaderTemplate>
                                                                                    <div>
                                                                                          <table> 
                                                                                              <tr><td> <b> Lead Details </b></td></tr>
                                                                                          </table>
                                                                                      </div>
                                                                                </HeaderTemplate>
                                                                                <ContentTemplate>
                                                                                <div>
                                                                                    <table style="width: 750px; border: 0px solid #86b2d1" align="center" cellpadding="3" cellspacing="2">
                                                                                        <tr>
                                                                                            <td class="ControlLabel" style="width: 23%;">
                                                                                                Lead Number
                                                                                            </td>
                                                                                            <td class="ControlTextBox3" style="width: 30%;">
                                                                                                <asp:TextBox ID="txtLeadNo" runat="server" Text='<%# Bind("LeadNo") %>' Enabled="false"
                                                                                                    SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                            </td>
                                                                                            <td class="ControlLabel" style="width: 18%;">
                                                                                                <asp:RequiredFieldValidator ID="rvStock" runat="server" ControlToValidate="txtCreationDate"
                                                                                                    ErrorMessage="Creation Date is mandatory" Display="Dynamic">*</asp:RequiredFieldValidator>
                                                                                                <asp:CompareValidator ControlToValidate="txtCreationDate" Operator="DataTypeCheck" Type="Date"
                                                                                                    ErrorMessage="Please enter a valid date" runat="server" ID="cmpValtxtDate">*</asp:CompareValidator>
                                                                                                <asp:RangeValidator ID="myRangeValidator" runat="server" ControlToValidate="txtCreationDate"
                                                                                                    ErrorMessage="Creation date cannot be future date." Text="*" Type="Date"></asp:RangeValidator>
                                                                                                Creation Date *
                                                                                            </td>
                                                                                            <td class="ControlTextBox3" style="width: 30%;">
                                                                                                <asp:TextBox ID="txtCreationDate" runat="server" Text='<%# Bind("CreationDate","{0:dd/MM/yyyy}") %>'
                                                                                                    CssClass="cssTextBox" ></asp:TextBox>
                                                                                            </td>
                                                                                            <td style="width: 10%;" align="left">
                                                                                                <cc1:CalendarExtender ID="calExtender3" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                                                    PopupButtonID="btnBillDate" PopupPosition="BottomLeft" TargetControlID="txtCreationDate">
                                                                                                </cc1:CalendarExtender>
                                                                                                <asp:ImageButton ID="btnBillDate" ImageUrl="App_Themes/NewTheme/images/cal.gif"
                                                                                                                                    CausesValidation="False" Width="20px" runat="server" />  
                                                                                            </td>
                                                                                        </tr>
                                                                                
                                                                                        <tr style="height: 30px">
                                                                                            <td class="ControlLabel" style="width: 23%;">
                                                                                                Prospect Customer Name *
                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtProspCustName"
                                                                                                    ErrorMessage="Prospect Customer Name is mandatory" Display="Dynamic">*</asp:RequiredFieldValidator>
                                                                                            </td>
                                                                                            <td class="ControlTextBox3" style="width: 22%;">
                                                                                                <asp:TextBox ID="txtProspCustName" runat="server" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                            </td>
                                                                                            <td class="ControlLabel" style="width: 18%;">
                                                                                                Address
                                                                                            </td>
                                                                                            <td class="ControlTextBox3" style="width: 22%;">
                                                                                                <asp:TextBox ID="txtAddress" runat="server" Text='<%# Bind("Address") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                            </td>
                                                                                            <td style="width: 10%;">
                                                                                    
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr style="height: 30px">
                                                                                            <td class="ControlLabel" style="width: 23%;">
                                                                                                Mobile *
                                                                                                <asp:RequiredFieldValidator ID="rvBDate" runat="server" ControlToValidate="txtMobile"
                                                                                                    Display="Dynamic" ErrorMessage="Mobile is mandatory.">*</asp:RequiredFieldValidator>
                                                                                            </td>
                                                                                            <td class="ControlTextBox3" style="width: 22%;">
                                                                                                <asp:TextBox ID="txtMobile" runat="server" MaxLength="10" SkinID="skinTxtBoxGrid"
                                                                                                            TabIndex="5"></asp:TextBox>
                                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                                                                                            TargetControlID="txtMobile" />
                                                                                            </td>
                                                                                            <td class="ControlLabel" style="width: 18%;">
                                                                                                Landline 
                                                                                            </td>
                                                                                            <td class="ControlTextBox3" style="width: 22%;">
                                                                                                <asp:TextBox ID="txtLandline" runat="server" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                            </td>
                                                                                            <td style="width: 10%;">
                                                                                    
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="ControlLabel" style="width: 23%;">
                                                                                               Email
                                                                                               <asp:RegularExpressionValidator ID="regextxtPercentage" runat="server" ControlToValidate="txtEmail"
                                                                                                    Display="Dynamic" ErrorMessage="Invalid email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                                                                    ></asp:RegularExpressionValidator>
                                                                                            </td>
                                                                                            <td class="ControlTextBox3" style="width: 18%;">
                                                                                                <asp:TextBox ID="txtEmail" runat="server" SkinID="skinTxtBoxGrid"
                                                                                                            TabIndex="5"></asp:TextBox>
                                                                                            </td>
                                                                                            <td class="ControlLabel" style="width: 18%;">
                                                                                                <asp:CompareValidator ID="cvModeOfContact" runat="server" ControlToValidate="cmbModeOfContact"
                                                                                                    Display="Dynamic" ErrorMessage="Please Select MOC" Operator="GreaterThan"
                                                                                                    Text="*" ValueToCompare="0"></asp:CompareValidator>
                                                                                                Mode Of Contact *
                                                                                            </td>
                                                                                            <td class="ControlDrpBorder" style="width: 22%;">
                                                                                                <asp:DropDownList ID="cmbModeOfContact" runat="server" AppendDataBoundItems="true" style="border: 1px solid #90c9fc" height="26px"
                                                                                                    DataTextField="TextValue" DataValueField="TextValue" Width="100%" CssClass="drpDownListMedium" BackColor = "#90c9fc" >
                                                                                                    <asp:ListItem style="background-color: #90c9fc" Text="Select MOC" Value="0"></asp:ListItem>
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                            <td style="width: 10%;">
                                                                                    
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr style="height: 30px">
                                                                                            <td class="ControlLabel" style="width: 23%;">
                                                                                                Personal Responsible *
                                                                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="cmbPersonalResp"
                                                                                                    Display="Dynamic" ErrorMessage="Please Select Personal Responsible." Operator="GreaterThan"
                                                                                                    Text="*" ValueToCompare="0"></asp:CompareValidator>
                                                                                            </td>
                                                                                            <td class="ControlDrpBorder" style="width: 22%;">
                                                                                                <asp:DropDownList ID="cmbPersonalResp" runat="server" AppendDataBoundItems="true" style="border: 1px solid #90c9fc" height="26px"
                                                                                                    DataTextField="TextValue" DataValueField="TextValue" Width="100%" CssClass="drpDownListMedium" BackColor = "#90c9fc" >
                                                                                                    <asp:ListItem style="background-color: #90c9fc" Text="Select Personal Responsible" Value="0"></asp:ListItem>
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                            <td  class="ControlLabel" style="width: 18%;">
                                                                                                Business Type *
                                                                                                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="cmbBussType"
                                                                                                    Display="Dynamic" ErrorMessage="Please Select Business Type." Operator="GreaterThan"
                                                                                                    Text="*" ValueToCompare="0"></asp:CompareValidator>
                                                                                            </td>
                                                                                            <td class="ControlDrpBorder" style="width: 22%">
                                                                                                <asp:DropDownList ID="cmbBussType" runat="server" AppendDataBoundItems="true" style="border: 1px solid #90c9fc" height="26px"
                                                                                                    DataTextField="TextValue" DataValueField="TextValue" Width="100%" CssClass="drpDownListMedium" BackColor = "#90c9fc" >
                                                                                                    <asp:ListItem style="background-color: #90c9fc" Text="Select Business Type" Value="0"></asp:ListItem>
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                            <td style="width: 10%;">
                                                                                    
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr style="height: 30px">
                                                                                            <td class="ControlLabel" style="width: 23%;">
                                                                                                Branch *
                                                                                                <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="cmbBranch"
                                                                                                    Display="Dynamic" ErrorMessage="Please Select Branch" Operator="GreaterThan"
                                                                                                    Text="*" ValueToCompare="0"></asp:CompareValidator>
                                                                                            </td>
                                                                                            <td class="ControlDrpBorder" style="width: 22%">
                                                                                                <asp:DropDownList ID="cmbBranch" runat="server" AppendDataBoundItems="true" style="border: 1px solid #90c9fc" height="26px"
                                                                                                    DataTextField="TextValue" DataValueField="TextValue" Width="100%" CssClass="drpDownListMedium" BackColor = "#90c9fc" >
                                                                                                    <asp:ListItem style="background-color: #90c9fc" Text="Select Branch" Value="0"></asp:ListItem>
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                            <td  class="ControlLabel" style="width: 18%;">
                                                                                                Status *
                                                                                                <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="cmbStatus"
                                                                                                    Display="Dynamic" ErrorMessage="Please Select The Status." Operator="GreaterThan"
                                                                                                    Text="*"  ValueToCompare="0"></asp:CompareValidator>
                                                                                            </td>
                                                                                            <td class="ControlDrpBorder" style="width: 22%">
                                                                                                <asp:DropDownList ID="cmbStatus" runat="server" AppendDataBoundItems="true" style="border: 1px solid #90c9fc" height="26px"
                                                                                                    DataTextField="TextValue" DataValueField="TextValue" Width="100%" CssClass="drpDownListMedium" BackColor = "#90c9fc" >
                                                                                                    <asp:ListItem style="background-color: #90c9fc" Text="Select Status" Value="0"></asp:ListItem>
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                            <td style="width: 10%;">
                                                                                    
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr style="height: 30px">
                                                                                            <td class="ControlLabel" style="width: 23%;">
                                                                                                Last Completed Action *
                                                                                                <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="cmbLastCompAction"
                                                                                                    Display="Dynamic" ErrorMessage="Please Select Last Completed Action" Operator="GreaterThan"
                                                                                                    Text="*"  ValueToCompare="0"></asp:CompareValidator>
                                                                                            </td>
                                                                                            <td class="ControlDrpBorder" style="width: 22%">
                                                                                                <asp:DropDownList ID="cmbLastCompAction" runat="server" AppendDataBoundItems="true" style="border: 1px solid #90c9fc" height="26px"
                                                                                                    DataTextField="TextValue" DataValueField="TextValue" Width="100%" CssClass="drpDownListMedium" BackColor = "#90c9fc" >
                                                                                                    <asp:ListItem style="background-color: #90c9fc" Text="Select Last Completed Action" Value="0"></asp:ListItem>
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                            <td  class="ControlLabel" style="width: 18%;">
                                                                                                Next Action *
                                                                                                <asp:CompareValidator ID="CompareValidator6" runat="server" ControlToValidate="cmbNextAction"
                                                                                                    Display="Dynamic" ErrorMessage="Please Select Next Action." Operator="GreaterThan"
                                                                                                    Text="*"  ValueToCompare="0"></asp:CompareValidator>
                                                                                            </td>
                                                                                            <td class="ControlDrpBorder" style="width: 22%">
                                                                                                <asp:DropDownList ID="cmbNextAction" runat="server" AppendDataBoundItems="true" style="border: 1px solid #90c9fc" height="26px"
                                                                                                    DataTextField="TextValue" DataValueField="TextValue" Width="100%" CssClass="drpDownListMedium" BackColor = "#90c9fc" >
                                                                                                    <asp:ListItem style="background-color: #90c9fc" Text="Select Next Action" Value="0"></asp:ListItem>
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                            <td style="width: 10%;">
                                                                                    
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr style="height: 30px">
                                                                                            <td class="ControlLabel" style="width: 23%;">
                                                                                                Category *
                                                                                                <asp:CompareValidator ID="CompareValidator7" runat="server" ControlToValidate="cmbCategory"
                                                                                                    Display="Dynamic" ErrorMessage="Please Select Category" Operator="GreaterThan"
                                                                                                    Text="*"  ValueToCompare="0"></asp:CompareValidator>
                                                                                            </td>
                                                                                            <td class="ControlDrpBorder" style="width: 22%">
                                                                                                <asp:DropDownList ID="cmbCategory" runat="server" AppendDataBoundItems="true" Width="100%" style="border: 1px solid #90c9fc" height="26px" CssClass="drpDownListMedium" BackColor = "#90c9fc" 
                                                                                                    DataTextField="TextValue" DataValueField="TextValue">
                                                                                                    <asp:ListItem style="background-color: #90c9fc" Text="Select Category" Value="0"></asp:ListItem>
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                            <td  class="ControlLabel" style="width: 18%;">
                                                                                                Information 1
                                                                                            </td>
                                                                                            <td style="width: 22%;" class="ControlTextBox3">
                                                                                                <asp:TextBox ID="txtInfo1" runat="server" SkinID="skinTxtBoxGrid"
                                                                                                            TabIndex="5"></asp:TextBox>
                                                                                            </td>
                                                                                            <td style="width: 10%;">
                                                                                    
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr style="height: 30px">
                                                                                            <td class="ControlLabel" style="width: 23%;">
                                                                                                Information 2
                                                                                            </td>
                                                                                            <td class="ControlDrpBorder" style="width: 22%">
                                                                                                <asp:TextBox ID="txtInfo2" runat="server" SkinID="skinTxtBoxGrid"
                                                                                                            TabIndex="5"></asp:TextBox>
                                                                                            </td>
                                                                                            <td  class="ControlLabel" style="width: 18%;">
                                                                                              Information 3
                                                                                            </td>
                                                                                            <td class="ControlDrpBorder" style="width: 22%">
                                                                                                <asp:DropDownList ID="ddlinfo3" runat="server" AppendDataBoundItems="true" style="border: 1px solid #90c9fc" height="26px"
                                                                                                    DataTextField="TextValue" DataValueField="TextValue" Width="100%" CssClass="drpDownListMedium" BackColor = "#90c9fc" >
                                                                                                    <asp:ListItem style="background-color: #90c9fc" Text="Select Information" Value="0"></asp:ListItem>
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                            <td style="width: 10%;">
                                                                                    
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr style="height: 30px">
                                                                                            <td class="ControlLabel" style="width: 23%;">
                                                                                                Information 4
                                                                                            </td>
                                                                                            <td class="ControlDrpBorder" style="width: 22%">
                                                                                                <asp:DropDownList ID="ddlinfo4" runat="server" AppendDataBoundItems="true" style="border: 1px solid #90c9fc" height="26px"
                                                                                                    DataTextField="TextValue" DataValueField="TextValue" Width="100%" CssClass="drpDownListMedium" BackColor = "#90c9fc" >
                                                                                                    <asp:ListItem style="background-color: #90c9fc" Text="Select Information" Value="0"></asp:ListItem>
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                            <td  class="ControlLabel" style="width: 18%;">
                                                                                               Information 5
                                                                                            </td>
                                                                                            <td class="ControlDrpBorder" style="width: 22%">
                                                                                                <asp:DropDownList ID="ddlinfo5" runat="server" AppendDataBoundItems="true" style="border: 1px solid #90c9fc" height="26px"
                                                                                                    DataTextField="TextValue" DataValueField="TextValue" Width="100%" CssClass="drpDownListMedium" BackColor = "#90c9fc" >
                                                                                                    <asp:ListItem style="background-color: #90c9fc" Text="Select Information" Value="0"></asp:ListItem>
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                            <td style="width: 10%;">
                                                                                    
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr style="height: 30px" id="Tr1" runat="server" visible="true">
                                                                                            <td  class="ControlLabel" style="width: 23%;">
                                                                                                Call Back Flag
                                                                                            </td>
                                                                                            <td class="ControlDrpBorder" style="width: 22%">
                                                                                                <asp:DropDownList ID="DropDownList1" runat="server" AppendDataBoundItems="true" style="border: 1px solid #90c9fc" height="26px"
                                                                                                     Width="100%" CssClass="drpDownListMedium" BackColor = "#90c9fc" Enabled="False">
                                                                                                    <asp:ListItem style="background-color: #90c9fc" Text="NO" Selected="True" Value="1"></asp:ListItem>
                                                                                                    <asp:ListItem style="background-color: #90c9fc" Text="YES" Value="2"></asp:ListItem>
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                            <td style="width: 18%" class="ControlLabel">
                                                                                                Call Back Date
                                                                                            </td>
                                                                                            <td style="width: 22%" class="ControlTextBox3">
                                                                                                <asp:TextBox ID="TextBox1" runat="server" CssClass="cssTextBox" ValidationGroup="contact" Width="70px" Height="23px" BackColor = "#90c9fc" Enabled="False"></asp:TextBox>
                                                                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                                                                                PopupButtonID="ImageButton3" TargetControlID="TextBox1" Enabled="True">
                                                                                                </cc1:CalendarExtender>
                                                                                            </td>
                                                                                            <td style="width: 10%;">
                                                                                                <asp:ImageButton ID="ImageButton3" ImageUrl="App_Themes/NewTheme/images/cal.gif"
                                                                                                        CausesValidation="False" Width="20px" runat="server" />                                         
                                                                                            </td>
                                                                                        </tr>
                                                                                
                                                                                       <%-- <tr>
                                                                                            <td colspan="5" style="padding-top: 40px; padding-left: 15px;">
                                                                                        
                                                                                            </td>
                                                                                        </tr>--%>
                                                                                        
                                                                                        
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:Label runat="server" ID="Error" ForeColor="Red"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                    <asp:ValidationSummary ID="valSum" ShowMessageBox="True" ShowSummary="False" HeaderText="Validation Messages"
                                                                                        Font-Names="'Trebuchet MS'" Font-Size="12pt" runat="server" />
                                                                                </div>
                                                                                </ContentTemplate>
                                                                            </cc1:TabPanel>
                                                                            <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="Contact Details">
                                                                                <HeaderTemplate>
                                                                                    <div>
                                                                                         <table> 
                                                                                               <tr><td> <b>Contact Details</b> </td></tr>
                                                                                         </table>
                                                                                    </div>
                                                                                </HeaderTemplate>
                                                                                <ContentTemplate>
                                                                                    <table style="width:750px" cellpadding="3" cellspacing="1">
                                                                                        <tr style="height:5px">
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan="5">
                                                                                                <asp:Panel ID="pnlPopup" BackColor="White" BorderStyle="Solid" BorderColor="White"
                                                                                                    Width="100%" runat="server">
                                                                                                    <div style="text-align: center;">
                                                                                                        <asp:GridView ID="GrdViewLeadContact" runat="server" AllowSorting="True" AutoGenerateColumns="False" OnRowDeleting="GrdViewLeadContact_RowDeleting" OnSelectedIndexChanged="GrdViewLeadContact_SelectedIndexChanged"
                                                                                                            Width="100%" DataKeyNames="ContactRefID" AllowPaging="True" EmptyDataText="No Lead Contact found."  CssClass="someClass">
                                                                                                            <EmptyDataRowStyle CssClass="GrdContent" />
                                                                                                            <Columns>
                                                                                                                <asp:BoundField DataField="ContactRefID" HeaderText="ContactRefID" ItemStyle-Width="10%" Visible="false"  HeaderStyle-BorderColor="Gray"/>
                                                                                                                <asp:BoundField DataField="ContactedDate" HeaderText="Contacted Date" ItemStyle-Width="30%" DataFormatString="{0:dd/MM/yyyy}"  HeaderStyle-BorderColor="Gray"/>
                                                                                                                <asp:BoundField DataField="ContactSummary" HeaderText="ContactSummary" ItemStyle-Width="30%"  HeaderStyle-BorderColor="Gray"/>
                                                                                                                <asp:BoundField DataField="CallBackFlag" HeaderText="CallBackDate" ItemStyle-Width="20%"  HeaderStyle-BorderColor="Gray"/>
                                                                                                                <asp:BoundField DataField="CallBackDate" HeaderText="CallBack Date" ItemStyle-Width="30%" DataFormatString="{0:dd/MM/yyyy}"   HeaderStyle-BorderColor="Gray"/>
                                                                                                                <asp:TemplateField HeaderStyle-Width="30px" ItemStyle-CssClass="command" HeaderStyle-BorderColor="Gray">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:ImageButton ID="btnEdit" runat="server" CommandName="Select" SkinID="edit" CausesValidation="false" />
                                                                                                                    </ItemTemplate>
                                                                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField HeaderStyle-Width="30px" ItemStyle-CssClass="command" HeaderStyle-BorderColor="Gray">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:ImageButton ID="lnkB" runat="Server" CommandName="Delete" SkinID="delete" OnClientClick="return confirm('Are you sure you want to delete this Lead Contact?');"  CausesValidation="false" />
                                                                                                                    </ItemTemplate>
                                                                                                                    <HeaderStyle Width="4%" />
                                                                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                                                                </asp:TemplateField>
                                                                                                            </Columns>
                                                                                                            <PagerTemplate>
                                                                                                                <table style=" border-color:white">
                                                                                                                    <tr style=" border-color:white; height:1px">
                                                                                                                    </tr>
                                                                                                                    <tr style=" border-color:white">
                                                                                                                        <td style=" border-color:white">
                                                                                                                            Goto Page
                                                                                                                        </td> 
                                                                                                                        <td style=" border-color:white">
                                                                                                                            <asp:DropDownList ID="ddlPageSelector" runat="server" style="border:1px solid blue" AutoPostBack="true" SkinID="skinPagerDdlBox">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td style=" border-color:white">
                                                                                                                            <asp:Button Text="" CommandName="Page" CommandArgument="First" runat="server" CssClass="NewFirst" EnableTheming="false" Width="22px" Height="18px"
                                                                                                                                ID="btnFirst" />
                                                                                                                        </td>
                                                                                                                        <td style=" border-color:white">
                                                                                                                            <asp:Button Text="" CommandName="Page" CommandArgument="Prev" runat="server" CssClass="NewPrev" EnableTheming="false" Width="22px" Height="18px"
                                                                                                                                ID="btnPrevious" />
                                                                                                                        </td>
                                                                                                                        <td style=" border-color:white">
                                                                                                                            <asp:Button Text="" CommandName="Page" CommandArgument="Next" runat="server" CssClass="NewNext" EnableTheming="false" Width="22px" Height="18px"
                                                                                                                                ID="btnNext" />
                                                                                                                        </td>
                                                                                                                        <td style=" border-color:white">
                                                                                                                            <asp:Button Text="" CommandName="Page" CommandArgument="Last" runat="server" CssClass="NewLast" EnableTheming="false" Width="22px" Height="18px"
                                                                                                                                ID="btnLast" />
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </PagerTemplate>
                                                                                                        </asp:GridView>
                                                                                                    </div>
                                                                                                </asp:Panel>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan="5">
                                                                                                <table  style="width: 100%">
                                                                                                    <tr>
                                                                                                        <td style="width: 80%">

                                                                                                        </td>
                                                                                                        <td style="width: 20%">
                                                                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                                                                <ContentTemplate>
                                                                                                                    <asp:Button ID="lnkAddContact" Text="" CausesValidation="False" runat="server" CssClass="addContactbutton"
                                                                                                                        EnableTheming="false" SkinID="skinBtnUpload" Width="80px"  OnClick="lnkAddContact_Click"
                                                                                                                            />
                                                                                                                </ContentTemplate>
                                                                                                            </asp:UpdatePanel>
                                                                                                        </td>
                                                                                                        
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="left" colspan="5">
                                                                                                <cc1:ModalPopupExtender ID="ModalPopupContact" runat="server" BackgroundCssClass="modalBackground"
                                                                                                    CancelControlID="CancelPopUpContact" DynamicServicePath="" Enabled="True" PopupControlID="pnlContact"
                                                                                                    TargetControlID="ShowPopUpContact">
                                                                                                </cc1:ModalPopupExtender>
                                                                                                <input id="ShowPopUpContact" type="button" style="display: none" runat="server" />
                                                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;<input ID="CancelPopUpContact" runat="server" style="display: none" 
                                                                                                    type="button" /> </input>
                                                                                                </input>
                                                                                                &nbsp;<asp:ValidationSummary ID="VSContact" runat="server" Font-Names="'Trebuchet MS'" Font-Size="12pt"
                                                                                                    HeaderText="Validation Messages" ShowMessageBox="true" ShowSummary="true" ValidationGroup="contact" />
                                                                                                <asp:Panel ID="pnlContact" runat="server" Width="800px" CssClass="modalPopup">
                                                                                                    <asp:UpdatePanel ID="updatePnlContact" runat="server" UpdateMode="Conditional">
                                                                                                        <ContentTemplate>
                                                                                                            <asp:Panel ID="pnlContactItems" CssClass="pnlPopUp" runat="server">
                                                                                                                <div id="Div1">
                                                                                                                    <table class="tblLeft" cellpadding="3" cellspacing="2" width="100%">
                                                                                                                        <tr>
                                                                                                                            <td>
                                                                                                                                <table class="headerPopUp" width="100%">
                                                                                                                                    <tr>
                                                                                                                                        <td>
                                                                                                                                            Lead Contact
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                </table>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                
                                                                                                                        <tr>
                                                                                                                            <td>
                                                                                                                                <table style="width: 100%; border: 1px solid #86b2d1" cellpadding="3" cellspacing="1">
                                                                                                                                    <tr style="height:5px">
                                                                                                                                        <td>
                                                                                                                                            <asp:HiddenField ID="hdCurrentRow" runat="server" Value="0" />
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                    <tr style="height:10px">
                                                                                                                                    </tr>
                                                                                                                                    <tr>
                                                                                                                                        <td style="width: 25%" class="ControlLabel">
                                                                                                                                            <asp:RequiredFieldValidator ID="rvDate" runat="server" ControlToValidate="txtContactedDate"
                                                                                                                                                ErrorMessage="Date is mandatory" Text="*" ValidationGroup="contact" />
                                                                                                                                        Contacted Date
                                                                                                                                        </td>
                                                                                                                                        <td style="width: 20%" class="ControlTextBox3">
                                                                                                                                            <asp:TextBox ID="txtContactedDate" runat="server" CssClass="cssTextBox" ValidationGroup="contact" Width="70px" Height="23px" BackColor = "#90c9fc" ></asp:TextBox>
                                                                                                                                            <cc1:CalendarExtender ID="calContactedDate" runat="server" Format="dd/MM/yyyy"
                                                                                                                                                            PopupButtonID="ImageButton1" TargetControlID="txtContactedDate" Enabled="True">
                                                                                                                                            </cc1:CalendarExtender>
                                                                                                                                   
                                                                                                                                        </td>
                                                                                                                                        <td style="width:5%" align="left">
                                                                                                                                            <asp:ImageButton ID="ImageButton1" ImageUrl="App_Themes/NewTheme/images/cal.gif"
                                                                                                                                                    CausesValidation="False" Width="20px" runat="server" /> 
                                                                                                                                        </td>
                                                                                                                                        <td style="width: 20%" class="ControlLabel">
                                                                                                                                            Contact Summary
                                                                                                                                            <asp:RequiredFieldValidator ID="rvContactSummary" runat="server" ControlToValidate="txtContactSummary"
                                                                                                                                                ErrorMessage="Summary is mandatory" Text="*" ValidationGroup="contact" />
                                                                                                                                        </td> 
                                                                                                                                        <td style="width: 20%" class="ControlTextBox3">
                                                                                                                                            <asp:TextBox ID="txtContactSummary" runat="server" CssClass="cssTextBox" ValidationGroup="contact" Width="100%" Height="23px" BackColor = "#90c9fc" ></asp:TextBox>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                    
                                                                                                                                    <tr style="height: 30px">
                                                                                                                                        <td class="ControlLabel" style="width: 25%;">
                                                                                                                                            Last Completed Action
                                                                                                                                        </td>
                                                                                                                                        <td class="ControlDrpBorder" style="width: 20%">
                                                                                                                                            <asp:DropDownList ID="cmblastaction" runat="server" AppendDataBoundItems="true" style="border: 1px solid #90c9fc" height="26px"
                                                                                                                                                DataTextField="TextValue" DataValueField="TextValue" Width="100%" CssClass="drpDownListMedium" BackColor = "#90c9fc" >
                                                                                                                                                <asp:ListItem style="background-color: #90c9fc" Text="Select Last Completed Action" Value="0"></asp:ListItem>
                                                                                                                                            </asp:DropDownList>
                                                                                                                                        </td>
                                                                                                                                        <td  class="ControlLabel" style="width: 5%;">
                                                                                                                                        </td>
                                                                                                                                        <td  class="ControlLabel" style="width: 20%;">
                                                                                                                                            Next Action
                                                                                                                                        </td>
                                                                                                                                        <td class="ControlDrpBorder" style="width: 20%">
                                                                                                                                            <asp:DropDownList ID="cmbnxtaction" runat="server" AppendDataBoundItems="true" style="border: 1px solid #90c9fc" height="26px"
                                                                                                                                                DataTextField="TextValue" DataValueField="TextValue" Width="100%" CssClass="drpDownListMedium" BackColor = "#90c9fc" >
                                                                                                                                                <asp:ListItem style="background-color: #90c9fc" Text="Select Next Action" Value="0"></asp:ListItem>
                                                                                                                                            </asp:DropDownList>
                                                                                                                                        </td>
                                                                                                                                        <td style="width: 10%;">
                                                                                    
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                    <tr style="height: 30px">
                                                                                                                                        
                                                                                                                                        <td style="width: 25%" class="ControlLabel">
                                                                                                                                           Status
                                                                                                                                        </td>
                                                                                                                                        <td style="width: 20%" class="ControlDrpBorder">
                                                                                                                                            <asp:DropDownList ID="cmbnewstatus" runat="server" AppendDataBoundItems="true" style="border: 1px solid #90c9fc" height="26px"
                                                                                                                                                DataTextField="TextValue" DataValueField="TextValue" Width="100%" CssClass="drpDownListMedium" BackColor = "#90c9fc" >
                                                                                                                                                <asp:ListItem style="background-color: #90c9fc" Text="Select Status" Value="0"></asp:ListItem>
                                                                                                                                            </asp:DropDownList>
                                                                                                                                        </td>
                                                                                                                                        <td style="width:5%" align="left">
                                                                                                                                            
                                                                                                                                        </td>
                                                                                                                                        <td  class="ControlLabel" style="width: 20%;">
                                                                                                                                            Call Back Flag
                                                                                                                                        </td>
                                                                                                                                        <td class="ControlDrpBorder" style="width: 20%">
                                                                                                                                            <asp:UpdatePanel ID="UP1" runat="server" UpdateMode="Conditional">
                                                                                                                                                <ContentTemplate>
                                                                                                                                                    <asp:DropDownList ID="ComboBox2" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ComboBox2_SelectedIndexChanged" style="border: 1px solid #90c9fc" height="26px"
                                                                                                                                                        DataTextField="TextValue" DataValueField="TextValue" Width="100%" CssClass="drpDownListMedium" BackColor = "#90c9fc" AutoPostBack="True">
                                                                                                                                                        <asp:ListItem style="background-color: #90c9fc" Text="NO" Selected="True" Value="1"></asp:ListItem>
                                                                                                                                                        <asp:ListItem style="background-color: #90c9fc" Text="YES" Value="2"></asp:ListItem>
                                                                                                                                                    </asp:DropDownList>
                                                                                                                                                </ContentTemplate>
                                                                                                                                            </asp:UpdatePanel>
                                                                                                                                        </td>
                                                                                                                                        <td style="width: 10%;">
                                                                                    
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                    <tr style="height: 30px" id="rowcall" runat="server" visible="false">
                                                                                                                                        <td style="width: 25%" class="ControlLabel">
                                                                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtcallback"
                                                                                                                                                ErrorMessage="Call Back Date is mandatory" Text="*" ValidationGroup="contact" />
                                                                                                                                            Call Back Date
                                                                                                                                        </td>
                                                                                                                                        <td style="width: 20%" class="ControlTextBox3">
                                                                                                                                            <asp:TextBox ID="txtcallback" runat="server" CssClass="cssTextBox" ValidationGroup="contact" Width="70px" Height="23px" BackColor = "#90c9fc" ></asp:TextBox>
                                                                                                                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                                                                                                                            PopupButtonID="ImageButton2" TargetControlID="txtcallback" Enabled="True">
                                                                                                                                            </cc1:CalendarExtender>
                                                                                                                                        </td>
                                                                                                                                        <td>
                                                                                                                                            <asp:ImageButton ID="ImageButton2" ImageUrl="App_Themes/NewTheme/images/cal.gif"
                                                                                                                                                    CausesValidation="False" Width="20px" runat="server" /> 
                                                                                                                                        </td>
                                                                                                                                        <td>
                                                                                                                                        
                                                                                                                                        </td>
                                                                                                                                        <td>
                                                                                                                                        
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                    <tr style="height:10px">
                                                                                                                                    </tr>
                                                                                                                                </table>
                                                                                                                            </td>
                                                                                                                            
                                                                                                                        </tr>
                                                                                                                        
                                                                                                                        <tr>
                                                                                                                            <td>
                                                                                                                                <table class="tblLeft" cellpadding="1" cellspacing="2"
                                                                                                                                    width="100%">
                                                                                                                                    <tr>
                                                                                                                                        <td align="center">
                                                                                                                                            <table style="border: 1px solid #86b2d1">
                                                                                                                                                <tr>
                                                                                                                                                    <td>
                                                                                                                                                        <asp:Panel ID="Panel2" runat="server" Width="120px" Height="32px">
                                                                                                                                                            <asp:Button ID="cmdCancelContact" runat="server" CssClass="CloseWindow6" Height="45px" OnClick="cmdCancelContact_Click" CausesValidation="false"
                                                                                                                                                                EnableTheming="false"/>
                                                                                                                                                        </asp:Panel>
                                                                                                                                                    </td>
                                                                                                                                                    <td>
                                                                                                                                                        <asp:Panel ID="Panel3" runat="server" Width="120px" Height="32px">
                                                                                                                                                            <asp:Button ID="cmdSaveContact" runat="server" CssClass="AddLeadContact" 
                                                                                                                                                                EnableTheming="false" OnClick="cmdSaveContact_Click" Text="" Height="45px" 
                                                                                                                                                                ValidationGroup="contact" />
                                                                                                                                                    
                                                                                                                                                            <asp:Button ID="cmdUpdateContact" runat="server" CssClass="UpdateLeadContact" 
                                                                                                                                                                EnableTheming="false" Height="45px" 
                                                                                                                                                                OnClick="cmdUpdateContact_Click" Text="" ValidationGroup="contact" 
                                                                                                                                                                Width="45px" />
                                                                                                                                                    
                                                                                                                                                        </asp:Panel>
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                       
                                                                                                                                            </table>
                                                                                                                                        </td>
                                                                                                                                        <td>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                </table>
                                                                                                                            </td>
                                                                                                                         </tr>
                                                                                                                    </table>
                                                                                                                </div>
                                                                                                            </asp:Panel>
                                                                                                        </ContentTemplate>
                                                                                                    </asp:UpdatePanel>
                                                                                                </asp:Panel>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </ContentTemplate>
                                                                             </cc1:TabPanel>
                                                                        </cc1:TabContainer>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="5">
                                                                        <table style="width: 100%" cellpadding="3" cellspacing="2">
                                                                            <tr>
                                                                                <td style="width: 32%">

                                                                                </td>
                                                                                <td style="width: 18%">
                                                                                    <asp:Button ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                        OnClick="UpdateCancelButton_Click" SkinID="skinBtnCancel" CssClass="cancelbutton6"
                                                                                        EnableTheming="false"></asp:Button>
                                                                                </td>
                                                                                <td style="width: 18%">
                                                                                    <asp:Button ID="UpdateButton" runat="server" SkinID="skinBtnSave" OnClientClick="javascript:CheckLeadContact();"
                                                                                        OnClick="UpdateButton_Click" CssClass="Updatebutton1231" EnableTheming="false"></asp:Button>
                                                                                    <asp:Button ID="AddButton" runat="server" SkinID="skinBtnSave" OnClientClick="javascript:CheckLeadContact()"
                                                                                        OnClick="AddButton_Click" CssClass="savebutton1231" EnableTheming="false"></asp:Button>
                                                                                </td>
                                                                                <td style="width: 32%" align="center">
                                                                                    
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:ObjectDataSource ID="srcBanks" runat="server" SelectMethod="ListBanks" TypeName="BusinessLogic">
                                                                                        <SelectParameters>
                                                                                            <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                                        </SelectParameters>
                                                                                    </asp:ObjectDataSource>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:ObjectDataSource ID="srcCreditorDebitor" runat="server" SelectMethod="ListSundryDebitors"
                                                                                        TypeName="BusinessLogic">
                                                                                        <SelectParameters>
                                                                                            <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                                        </SelectParameters>
                                                                                    </asp:ObjectDataSource>
                                                                                </td>
                                                                                <td>
                                                                                </td>
                                                                                <td>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </td>
                </tr>
                <tr style="width: 100%">
                    <td style="width: 100%">
                        <table width="100%" style="margin: -3px 0px 0px 0px;">
                                <tr style="width: 100%">
                                    <td>
                        <div class="mainGridHold" id="searchGrid">
                            <asp:GridView ID="GrdViewLead" runat="server" AllowSorting="false" AutoGenerateColumns="False"
                                OnRowCreated="GrdViewLead_RowCreated" Width="99.9%"
                                AllowPaging="True" DataKeyNames="LeadID" EmptyDataText="No Records found!" 
                                OnRowCommand="GrdViewLead_RowCommand" OnRowDataBound="GrdViewLead_RowDataBound" OnPageIndexChanging="GrdViewLead_PageIndexChanging"
                                OnSelectedIndexChanged="GrdViewLead_SelectedIndexChanged" OnRowDeleting="GrdViewLead_RowDeleting"
                                OnRowDeleted="GrdViewLead_RowDeleted" CssClass="someClass">
                                <EmptyDataRowStyle CssClass="GrdContent" />
                                <Columns>
                                    <asp:BoundField DataField="LeadID" HeaderText="Trans. No." HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Gray" />
                                    <asp:BoundField DataField="CreationDate" HeaderText="Created Date" HeaderStyle-Wrap="false"
                                        DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-BorderColor="Gray" />
                                    <asp:BoundField DataField="ProspectCustName" HeaderText="ProspectCustName" HeaderStyle-Wrap="false"  HeaderStyle-BorderColor="Gray"/>
                                    <asp:BoundField DataField="Mobile" HeaderText="Mobile" HeaderStyle-Wrap="false"  HeaderStyle-BorderColor="Gray"/>
                                    <asp:BoundField DataField="LastCompletedAction" HeaderText="LastCompletedAction" HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Gray" />
                                    <asp:BoundField DataField="NextAction" HeaderText="NextAction" HeaderStyle-Wrap="false"  HeaderStyle-BorderColor="Gray"/>
                                    <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-Wrap="false"  HeaderStyle-BorderColor="Gray"/>
                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="50px" HeaderText="Edit" HeaderStyle-BorderColor="Gray">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEdit" CausesValidation="false" runat="server" SkinID="edit"
                                                CommandName="Select" />
                                            <asp:ImageButton ID="btnEditDisabled" Enabled="false" SkinID="editDisable" runat="Server"></asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="50px" HeaderText="Delete" HeaderStyle-BorderColor="Gray">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" OnClientClick="return confirm('Are you sure you want to delete?');" CommandName="Delete"></asp:ImageButton>
                                            <asp:ImageButton ID="lnkBDisabled" Enabled="false" SkinID="deleteDisable" runat="Server"></asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="50px" HeaderText="Print">
                                        <ItemTemplate>
                                            <a href='<%# DataBinder.Eval(Container, "DataItem.LeadID", "javascript:PrintItem({0});") %>'>
                                                <img alt="Print" border="0" src="App_Themes/DefaultTheme/Images/Print.png">
                                            </a>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <%--<asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                </Columns>
                                <PagerTemplate>
                                    <table style=" border-color:white">
                                        <tr style=" border-color:white; height:1px">
                                        </tr>
                                        <tr style=" border-color:white">
                                            <td style=" border-color:white">
                                                Goto Page
                                            </td> 
                                            <td style=" border-color:white">
                                                <asp:DropDownList ID="ddlPageSelector" runat="server" AutoPostBack="true" Width="65px" style="border:1px solid blue" height="23px" BackColor="#BBCAFB" OnSelectedIndexChanged="ddlPageSelector_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td style=" border-color:white; Width:5px">
                                            
                                            </td>
                                            <td style=" border-color:white">
                                                <asp:Button Text="" CommandName="Page" CommandArgument="First" runat="server" CausesValidation="false" CssClass="NewFirst" EnableTheming="false" Width="22px" Height="18px"
                                                    ID="btnFirst" />
                                            </td>
                                            <td style=" border-color:white">
                                                <asp:Button Text="" CommandName="Page" CommandArgument="Prev" runat="server" CausesValidation="false" CssClass="NewPrev" EnableTheming="false" Width="22px" Height="18px"
                                                    ID="btnPrevious" />
                                            </td>
                                            <td style=" border-color:white">
                                                <asp:Button Text="" CommandName="Page" CommandArgument="Next" runat="server" CausesValidation="false" CssClass="NewNext" EnableTheming="false" Width="22px" Height="18px"
                                                    ID="btnNext" />
                                            </td>
                                            <td style=" border-color:white">
                                                <asp:Button Text="" CommandName="Page" CommandArgument="Last" runat="server" CausesValidation="false" CssClass="NewLast" EnableTheming="false" Width="22px" Height="18px"
                                                    ID="btnLast" />
                                            </td>
                                        </tr>
                                    </table>
                                </PagerTemplate>
                            </asp:GridView>
                        </div>
                        </td>
                        </tr>
                        <tr>
                            <td>
                               
                            </td>
                        </tr>
                        </table>
                    </td>
                </tr>
                <%--<tr style="width:100%">
                    <td align="left">--%>
                        <%--<asp:ObjectDataSource ID="GridSource" runat="server" SelectMethod="ListLeadMaster"
                            TypeName="LeadBusinessLogic">--%>
                            <%--<SelectParameters>
                                <%--<asp:ControlParameter ControlID="GrdViewLead" Name="LeadId" PropertyName="SelectedValue"
                                    Type="Int32" />--%>
                                <%--<asp:CookieParameter CookieName="Company" Type="String" Name="connection" />
                            </SelectParameters>--%>
                         <%--   <SelectParameters>
                                <asp:CookieParameter Name="LeadID" CookieName="LeadID" Type="Int32" />
                            </SelectParameters>--%>
                        <%--</asp:ObjectDataSource>
                    </td>--%>
                <%--</tr>--%>
                <tr>
                    <td style="width: 100%">
                    </td>
                </tr>
            </table>
            <input type="hidden" id="hidAdvancedState" runat="server" />
            <asp:HiddenField ID="hdSMS" runat="server" Value="NO" />
            <asp:HiddenField ID="hdText" runat="server" />
            <asp:HiddenField ID="hdMobile" runat="server" />
            <asp:HiddenField ID="hdSMSRequired" runat="server" Value="NO" />
            <asp:HiddenField ID="hdPendingCount" runat="server" Value="0" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <div align="center">
        <asp:Button ID="btnExportToExcel" runat="server" CssClass="exportexl6" Height="45px" OnClick="btnExportToExcel_Click" CausesValidation="false"
                                   EnableTheming="false"/>
    </div>
</asp:Content>

