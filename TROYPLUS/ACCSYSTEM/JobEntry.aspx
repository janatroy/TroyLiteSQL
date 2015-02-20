<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="JobEntry.aspx.cs" Inherits="JobEntry" Title="Others > Job Entry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
<script language="javascript" type="text/javascript">

    function pageLoad() {
        //  get the behavior associated with the tab control
        var tabContainer = $find('ctl00_cplhControlPanel_tabs2');

//        if (tabContainer == null)
//            tabContainer = $find('ctl00_cplhControlPanel_frmViewAdd_TabPanel1');

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
  
  <table style="width: 100%;" align="center">
  <tr>
  <td>
    
        <%--<div class="mainConHd">
            <table cellspacing="0" cellpadding="0" border="0">
                <tr valign="middle">
                    <td>
                        <span>Job Entries</span>
                    </td>
                </tr>
            </table>
        </div>--%>
        <%--<table class="mainConHd" style="width: 994px;">
                                <tr valign="middle">
                                    <td style="font-size: 20px;">
                                        Job Entries
                                    </td>
                                </tr>
                            </table>--%>
        <div class="mainConBody">
            <table style="width: 100%; margin: -1px 0px 0px 1px;" cellspacing="2px" cellpadding="3px" class="searchbg">
                <tr style="height: 25px; vertical-align: middle">
                    <td style="width: 2%;"></td>
                    <td style="width: 18%; font-size: 22px; color: #000000;" >
                        Job Entries
                    </td>
                    <td style="width: 14%">
                        <div style="text-align: right;">
                            <asp:Panel ID="pnlSearch" runat="server" Width="100px">
                                <asp:Button ID="lnkBtnAdd" runat="server" OnClick="lnkBtnAdd_Click" CssClass="ButtonAdd66"
                                    EnableTheming="false" Width="80px" Text=""></asp:Button>
                            </asp:Panel>
                        </div>
                    </td>
                    <td style="width: 10%; color: #000000;" align="right">
                        Search
                    </td>
                    <td style="width: 20%" class="Box1">
                        <asp:TextBox ID="txtSJobTitle" runat="server" SkinID="skinTxtBoxSearch" MaxLength="100"></asp:TextBox>
                    </td>
                    <td style="width: 17%" class="tblLeft">
                        <asp:Button ID="btnSales" runat="server" EnableTheming="false" CssClass="ButtonSearch6" Text="" OnClick="cmdSearch_Click" />
                    </td>
                    <td style="width: 28%" class="tblLeft">
                        <a onclick="window.open('JobManagementReport.aspx ','Summary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=310,width=500,left=425,top=220, scrollbars=yes');"
                            id="lnkye" href="javascript:__doPostBack('ctl00$cplhControlPanel$lnkJM','')">view
                            Job Management Report</a>
                    </td>
                </tr>
            </table>
        </div>

    <input id="dummyPurchase" type="button" style="display: none" runat="server" />
    <input id="BtnPopUpCancel1" type="button" style="display: none" runat="server" />
    <cc1:ModalPopupExtender ID="ModalPopupJob" runat="server" BackgroundCssClass="modalBackground"
        CancelControlID="BtnPopUpCancel1" DynamicServicePath="" Enabled="True" PopupControlID="pnlDetails"
        RepositionMode="RepositionOnWindowResizeAndScroll" TargetControlID="dummyPurchase">
    </cc1:ModalPopupExtender>

    <asp:Panel ID="pnlDetails" runat="server" Style="width: 60%; display: none">
        <asp:UpdatePanel ID="updatePnlPurchase" runat="server" UpdateMode="Conditional" >
            <ContentTemplate>
                <div id="Div1" class="divArea" style="background-color: White;">
                    <table style="width: 100%;" align="center">
                        <tr style="width: 100%">
                            <td style="width: 100%">
                                <table style="text-align: left;" width="100%" cellpadding="3" cellspacing="5">
                                    <tr>
                                        <td>
                                            <table class="headerPopUp" width="100%">
                                                <tr>
                                                    <td>
                                                        Job Entry
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <cc1:TabContainer ID="tabs2" runat="server" Width="100%" CssClass="fancy fancy-green">
                                                <cc1:TabPanel ID="tabMaster" runat="server" HeaderText="Job Assign">
                                                    <ContentTemplate>
                                                        <asp:Panel ID="pnlJob" runat="server">
                                                            <table width="760px" cellpadding="3" cellspacing="2" class="tblLeft">
                                                                <tr>
                                                                    <td class="ControlLabel" style="width: 25%">
                                                                        Ref. No.
                                                                    </td>
                                                                    <td style="width: 20%" class="ControlTextBox3">
                                                                        <asp:TextBox ID="txtRefno" Width="90%" runat="server" MaxLength="10" SkinID="skinTxtBox"
                                                                            ValidationGroup="jobval"></asp:TextBox>
                                                                    </td>
                                                                    <td class="ControlLabel" style="width: 15%">
                                                                        <asp:RequiredFieldValidator CssClass="lblFont" ID="reqJobTitle" ErrorMessage="Job Title is mandatory"
                                                                            Text="*" ControlToValidate="txtJobTitle" runat="server" ValidationGroup="jobval" />
                                                                        Job Title
                                                                    </td>
                                                                    <td style="width: 20%" class="ControlTextBox3">
                                                                        <asp:TextBox ID="txtJobTitle" SkinID="skinTxtBox" runat="server" MaxLength="100"
                                                                            ValidationGroup="jobval"></asp:TextBox>
                                                                    </td>
                                                                    <td style="width: 10%">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="ControlLabel" style="width: 25%">
                                                
                                                                        <asp:RequiredFieldValidator CssClass="lblFont" ID="regJobDesc" ErrorMessage="Job Description is mandatory"
                                                                            Text="*" ControlToValidate="txtDesc" runat="server" ValidationGroup="jobval" />
                                                                        Short Description
                                                                    </td>
                                                                    <td style="width: 20%" class="ControlTextBox3">
                                                                        <asp:TextBox ID="txtDesc" SkinID="skinTxtBox" runat="server" MaxLength="255" TextMode="MultiLine"
                                                                            ValidationGroup="jobval"></asp:TextBox>
                                                                    </td>
                                                                    <td style="width: 15%" class="ControlLabel">
                                                                        <asp:RequiredFieldValidator CssClass="lblFont" ID="regIncharge" Text="*" ErrorMessage="Assiging a person is mandatory"
                                                                            InitialValue="0" ControlToValidate="drpIncharge" runat="server" ValidationGroup="jobval" />
                                                                        Assigned To
                                                                    </td>
                                                                    <td style="width: 20%" class="ControlDrpBorder">
                                                                        <asp:DropDownList ID="drpIncharge" AppendDataBoundItems="true" runat="server" CssClass="drpDownListMedium" BackColor = "#90c9fc" Width="100%"
                                                                            DataSourceID="empSrc" DataTextField="empFirstName" DataValueField="empno" style="border: 1px solid #90c9fc" height="26px">
                                                                            <asp:ListItem Text="Select Executive" Value="0"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td style="width: 10%">
                                                                    </td>
                                                                </tr>
                                                                
                                                                <tr>
                                                                    <td class="ControlLabel" style="width: 25%">
                                                
                                                                        <asp:CompareValidator ID="cmpAssQty" runat="server" ControlToValidate="txtAssQty"
                                                                            Display="Dynamic" ErrorMessage="Qty. must be greater than Zero!!" Operator="GreaterThan"
                                                                            Text="*" ValidationGroup="jobval" ValueToCompare="0"></asp:CompareValidator>
                                                                        <asp:RequiredFieldValidator CssClass="lblFont" ID="reqAssQty" ErrorMessage="Assigned Qty. is mandatory"
                                                                            Text="*" ControlToValidate="txtAssQty" runat="server" ValidationGroup="jobval" />
                                                                        Qty. Assigned
                                                                    </td>
                                                                    <td style="width: 20%" class="ControlTextBox3">
                                                                        <asp:TextBox ID="txtAssQty" SkinID="skinTxtBox" runat="server" MaxLength="10" ValidationGroup="jobval"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FltAssQty" runat="server" TargetControlID="txtAssQty"
                                                                            FilterType="Custom, Numbers" ValidChars="." />
                                                                    </td>
                                                                    <td class="ControlLabel" style="width: 15%">
                                                
                                                                        <asp:RequiredFieldValidator CssClass="lblFont" ID="reqAssignDate" ErrorMessage="Assigned Date is mandatory"
                                                                            Text="*" ControlToValidate="txtAssignedDate" runat="server" ValidationGroup="jobval" />
                                                                        <asp:RangeValidator ID="rvAssignedDate" runat="server" ValidationGroup="jobval" ControlToValidate="txtAssignedDate"
                                                                            ErrorMessage="Assigned date cannot be future date." Text="*" Type="Date"></asp:RangeValidator>
                                                                        Assigned Date
                                                                    </td>
                                                                    <td style="width: 20%" class="ControlTextBox3">
                                                                        <asp:TextBox ID="txtAssignedDate" CssClass="cssTextBox" Width="100px" runat="server"
                                                                            MaxLength="100" ValidationGroup="jobval"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="calAssDate" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                            PopupButtonID="btnAssDate" PopupPosition="BottomLeft" TargetControlID="txtAssignedDate">
                                                                        </cc1:CalendarExtender>
                                                                    </td>
                                                                    <td align="left" style="width:10%">
                                                                        <asp:ImageButton ID="btnAssDate" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                            Width="20px" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 25%" class="ControlLabel">
                                                                        Qty. Returned
                                                                    </td>
                                                                    <td style="width: 20%" class="ControlTextBox3">
                                                                        <asp:Label ID="lblRetQty" CssClass="lblFont" Width="80px" Text="0" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td class="ControlLabel" style="width: 15%">
                                                
                                                                        <asp:RequiredFieldValidator CssClass="lblFont" ID="regExpRetDate" Text="*" ErrorMessage="Expected Return Date is mandatory"
                                                                            ControlToValidate="txtExpRetDate" runat="server" ValidationGroup="jobval" />
                                                                        Expected Returned Date
                                                                    </td>
                                                                    <td style="width: 20%" class="ControlTextBox3">
                                                                        <asp:TextBox ID="txtExpRetDate" CssClass="cssTextBox" Width="100px" runat="server"
                                                                            MaxLength="100" ValidationGroup="jobval"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="Calendaer1" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                            PopupButtonID="btnExpDate" PopupPosition="BottomLeft" TargetControlID="txtExpRetDate">
                                                                        </cc1:CalendarExtender>
                                                                    </td>
                                                                    <td align="left" style="width:10%">
                                                                        <asp:ImageButton ID="btnExpDate" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                            Width="20px" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr style="height:5px">
                                            
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 25%">
                                                                    </td>
                                                                    <td align="right" style="width: 20%">
                                                                        <asp:Button ID="cmdReset" Style="width: 100px;" runat="server" SkinID="skinBtnCancel"
                                                                            CssClass="cancelbutton6" EnableTheming="false" OnClick="cmdReset_Click" />
                                                                    </td>
                                                                    <td align="left" style="width: 15%">
                                                                        <asp:Button ID="cmdSaveJob" Style="width: 100px;" SkinID="skinBtnSave" runat="server"
                                                                            CssClass="savebutton1231" EnableTheming="false" OnClick="cmdSaveJob_Click" ValidationGroup="jobval" />
                                                                        <asp:Button ID="cmdUpdateJob" Enabled="false" Style="width: 110px;" runat="server"
                                                                            SkinID="skinBtnSave" CssClass="Updatebutton1231" EnableTheming="false" OnClick="cmdUpdateJob_Click"
                                                                            ValidationGroup="jobval" />
                                                                    </td>
                                                                    <td style="width: 20%">
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnlException" Visible="false" runat="server">
                                                            <table width="100%" cellpadding="3" cellspacing="3" class="tblLeft">
                                                                <tr>
                                                                    <td class="SalesHeader" colspan="2">
                                                                        Exceptions
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="tblLeft">
                                                                        Error Message
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label runat="server" ID="ErrMsg" CssClass="errorMsg"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnlJobReturnDetails" runat="server">
                                                            <table width="100%" cellpadding="0" cellspacing="0" class="tblLeft">
                                                                <tr>
                                                                    <td>
                                                                        <asp:GridView ID="gvJobReturn" Width="100%" DataKeyNames="JobCompletedID" AutoGenerateColumns="False"
                                                                            runat="server" EmptyDataText="No Data found!" CellPadding="4" OnRowDeleting="gvJobReturn_RowDeleting">
                                                                            <RowStyle Font-Bold="false" />
                                                                            <Columns>
                                                                                <asp:BoundField DataField="Qty_Return" ItemStyle-Width="15%" HeaderText="Qty. Returned">
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="ReturnedDate" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="15%"
                                                                                    HeaderText="Return Date"></asp:BoundField>
                                                                                <asp:BoundField DataField="Remarks" ItemStyle-Width="50%" HeaderText="Remarks"></asp:BoundField>
                                                                                <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="50px" HeaderText="Delete">
                                                                                    <ItemTemplate>
                                                                                        <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to delete this Item?"
                                                                                            runat="server">
                                                                                        </cc1:ConfirmButtonExtender>
                                                                                        <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" CommandName="Delete"></asp:ImageButton>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle Width="4%" />
                                                                                    <ItemStyle CssClass="command" />
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </ContentTemplate>
                                                </cc1:TabPanel>
                                                <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="Job Return Entry">
                                                    <ContentTemplate>
                                                        <asp:Panel ID="pnlJobReturnFrm" runat="server">
                                                            <table width="760px" cellpadding="3" cellspacing="2" class="tblLeft">
                                                                <tr>
                                                                    <td class="ControlLabel" style="width: 25%">
                                                                       <asp:RequiredFieldValidator CssClass="lblFont" ID="RequiredFieldValidator1" Text="*"
                                                                            ErrorMessage="Qty. Return is mandatory" ControlToValidate="txtCRetQty" runat="server"
                                                                            ValidationGroup="cmpval" />
                                                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtCRetQty"
                                                                            Display="Dynamic" ErrorMessage="Qty. must be greater than Zero!!" Operator="GreaterThan"
                                                                            Text="*" ValidationGroup="cmpval" ValueToCompare="0"></asp:CompareValidator>
                                                                         Qty. Returned
                                                                    </td>
                                                                    <td class="ControlTextBox3" style="width: 20%">
                                                                        <asp:TextBox ID="txtCRetQty" SkinID="skinTxtBox" runat="server" MaxLength="10" ValidationGroup="cmpval"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="ftRetQty" runat="server" TargetControlID="txtCRetQty"
                                                                            FilterType="Custom, Numbers" ValidChars="." />
                                                                    </td>
                                                                    <td class="ControlLabel" style="width: 15%">
                                                
                                                                        <asp:RangeValidator ID="rvReturnDate" runat="server" ValidationGroup="cmpval" ControlToValidate="txtCRetDate"
                                                                            ErrorMessage="Return date cannot be future date." Text="*" Type="Date"></asp:RangeValidator>
                                                                        <asp:RequiredFieldValidator CssClass="lblFont" ID="RequiredFieldValidator2" Text="*"
                                                                            ErrorMessage="Qty. Return Date is mandatory" ControlToValidate="txtCRetDate"
                                                                            runat="server" ValidationGroup="cmpval" />
                                                                        Return Date
                                                                    </td>
                                                                    <td class="ControlTextBox3" style="width: 20%">
                                                                        <asp:TextBox ID="txtCRetDate" CssClass="cssTextBox" Width="100px" runat="server"
                                                                            MaxLength="100" ValidationGroup="cmpval"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="calRetDate" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                            PopupButtonID="btnRetDate" PopupPosition="BottomLeft" TargetControlID="txtCRetDate">
                                                                        </cc1:CalendarExtender>
                                                                    </td>
                                                                    <td align="left" style="width:10%">
                                                                        <asp:ImageButton ID="btnRetDate" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                            Width="20px" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="ControlLabel" style="width: 25%">
                                                
                                                                        <asp:RequiredFieldValidator CssClass="lblFont" ID="RequiredFieldValidator3" Text="*"
                                                                            ErrorMessage="Remarks is mandatory" ControlToValidate="txtRemarks" runat="server"
                                                                            ValidationGroup="cmpval" />
                                                                        Remarks
                                                                    </td>
                                                                    <td class="ControlTextBox3" style="width: 20%">
                                                                        <asp:TextBox ID="txtRemarks" SkinID="skinTxtBox" Width="152px" runat="server" Height="40px"
                                                                            MaxLength="255" TextMode="MultiLine" ValidationGroup="cmpval"></asp:TextBox>
                                                                    </td>
                                                                    <td style="width: 15%">
                                                                    </td>
                                                                    <td style="width: 20%">
                                                                    </td>
                                                                </tr>
                                                                <tr style="height:5px"> 
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 20%">
                                                                    </td>
                                                                    <td style="width: 20%">
                                                                    </td>
                                                                    <td class="lblFont" colspan="4">
                                                                        <asp:Button ID="cmdSaveJobReturn" runat="server" CssClass="savebutton1231" EnableTheming="false"
                                                                            SkinID="skinBtnSave" OnClick="cmdSaveJobReturn_Click" ValidationGroup="cmpval" />
                                                                        <asp:ValidationSummary ID="valSum" DisplayMode="BulletList" ShowMessageBox="true"
                                                                            ValidationGroup="cmpval" ShowSummary="false" HeaderText="Validation Messages"
                                                                            Font-Names="'Trebuchet MS'" Font-Size="12" runat="server" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </ContentTemplate>
                                                </cc1:TabPanel>
                                            </cc1:TabContainer>
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
    <asp:ObjectDataSource ID="empSrc" runat="server" SelectMethod="ListExecutive" TypeName="BusinessLogic">
        <SelectParameters>
            <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:HiddenField ID="hdJobID" runat="server" Value="0" />
    <asp:Panel ID="pnlJobDetails" runat="server">
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server">
    
        <table width="100%" style="margin: -3px 0px 0px 0px;">
            <tr>
                <td>
                    <asp:GridView ID="gvJob" Width="100.2%" AllowPaging="true" runat="server" DataKeyNames="JobID"
                        EmptyDataText="No Data found!" OnSelectedIndexChanged="gvJob_SelectedIndexChanged"  CssClass="someClass" OnRowDataBound="gvJob_RowDataBound"
                        OnRowDeleting="gvJob_RowDeleting" OnPageIndexChanging="gvJob_PageIndexChanging" OnRowCreated="gvJob_RowCreated">
                        <Columns>
                            <asp:BoundField DataField="Ref" HeaderText="Ref. No." />
                            <asp:BoundField DataField="JobTitle" HeaderText="Job Title" />
                            <asp:BoundField DataField="AssignedDate" HeaderText="Assigned Date" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="Qty_Assigned" HeaderText="Assigned Qty." />
                            <asp:BoundField DataField="Qty_returned" HeaderText="Return Qty." />
                            <asp:BoundField DataField="ExpReturnDate" HeaderText="Expected Return Date" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="50px" HeaderText="Edit">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Select" />
                                    <asp:ImageButton ID="btnEditDisabled" Enabled="false" SkinID="editDisable" runat="Server"></asp:ImageButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="50px" HeaderText="Delete">
                                <ItemTemplate>
                                    <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to delete this Job?"
                                        runat="server">
                                    </cc1:ConfirmButtonExtender>
                                    <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" CommandName="Delete"></asp:ImageButton>
                                    <asp:ImageButton ID="lnkBDisabled" Enabled="false" SkinID="deleteDisable" runat="Server"></asp:ImageButton>
                                </ItemTemplate>
                                <HeaderStyle Width="50px" />
                                <ItemStyle CssClass="command" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
        
    </asp:Panel>
    <asp:ValidationSummary ID="VS" runat="server" Font-Names="'Trebuchet MS'" Font-Size="12pt"
        HeaderText="Validation Messages" ShowMessageBox="True" ShowSummary="False" ValidationGroup="jobval" />
        </td>
        </tr>
        </table> 
        </ContentTemplate>
        </asp:UpdatePanel>
        
</asp:Content>
