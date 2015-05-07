<%@ Page Title="" Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true" CodeFile="InternalTransferApproval.aspx.cs" Inherits="InternalTransferApproval" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <script language="javascript" type="text/javascript">

        function pageLoad() {
            //  get the behavior associated with the tab control
            var tabContainer = $find('ctl00_cplhControlPanel_frmViewAdd_tablInsert');

            if (tabContainer == null)
                tabContainer = $find('ctl00_cplhControlPanel_frmViewAdd_tabEdit');

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

        function PrintItem(ID) {
            window.showModalDialog('./PrintPayment.aspx?ID=' + ID, self, 'dialogWidth:700px;dialogHeight:430px;status:no;dialogHide:yes;unadorned:yes;');
        }
        window.onload = function Showalert() {

            var txt = document.getElementById("<%= txtSearch.ClientID %>");
            var btn = document.getElementById("<%= BtnClearFilter.ClientID %>");
            // alert('test');
            if (txt.value == "") {
                // alert(txt.value);
                btn.style.visibility = "hidden";
                // when the window is loaded, hide the button if the textbox is empty
            }

        }

        function clearfilterclick() {
            var button = document.getElementById('<%=BtnClearFilter.ClientID %>');
            alert('clicent');
            button.style.visibility = "hidden";
            //button.click();

        }


        function EnableDisableButton(sender, target) {
            var first = document.getElementById('<%=txtSearch.ClientID %>');
            //alert('test');
            <%-- var second = document.getElementById('<%=txtText.ClientID %>');--%>


            if (sender.value.length >= 1 && first.value.length >= 1) {
                // alert(sender.value.length);
                // alert(first.value.length);
                //BtnClearFilter.disabled = false;
                <%--  document.getElementById('<%=BtnClearFilter.ClientID %>').disabled = false;--%>
                document.getElementById('<%=BtnClearFilter.ClientID %>').style.visibility = "visible";
                // window.onload = function ();
            }

            if (sender.value.length < 1 && first.value.length < 1) {
                //alert(sender.value.length);
                // alert(first.value.length);
                //BtnClearFilter.disabled = true;
                <%-- document.getElementById('<%=BtnClearFilter.ClientID %>').disabled = true;--%>
                document.getElementById('<%=BtnClearFilter.ClientID %>').style.visibility = "Hidden";
            }
            //else {

            //    document.getElementById(target).disabled = false;
            //}
        }
 
    </script>
    <style id="Style1" runat="server">
        .fancy-green .ajax__tab_header
        {
            background: url(App_Themes/NewTheme/Images/green_bg_Tab.gif) repeat-x;
            cursor: pointer;
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
            border: 1px solid #999999;
            padding: 8px;
            background-color: #ffffff;
        }
    </style>
    <asp:UpdatePanel ID="UpdateMainArea" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <table style="width: 100%">
                <tr style="width: 100%">
                    <td style="width: 100%">
                        <div class="mainConBody">
                            <table style="width: 100.3%; margin: -3px 0px 0px 0px;" cellpadding="3" cellspacing="2"
                                class="searchbg">
                                <tr style="height: 25px; vertical-align: middle">
                                    <td style="width: 2%">
                                    </td>
                                    <td style="width: 40%; font-size: 22px; color: white;">
                                        Internal Transfer Approval
                                    </td>
                                    
                                    <td style="width: 15%; color: white;" align="center">
                                        Search
                                        <asp:RequiredFieldValidator ID="rvSearch" runat="server" ControlToValidate="txtSearch"
                                            Display="Dynamic" EnableClientScript="False" Enabled="false">Search is mandatory</asp:RequiredFieldValidator>
                                    </td>
                                    <td style="width: 18%" class="NewBox">
                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="cssTextBox" Width="92%"></asp:TextBox>
                                    </td>
                                    <td style="width: 18%" class="NewBox">
                                        <div style="width: 160px; font-family: 'Trebuchet MS';">
                                            <asp:DropDownList ID="ddCriteria" runat="server" Width="154px" BackColor="white"
                                                Height="23px" Style="text-align: center; border: 1px solid White">
                                                <asp:ListItem Value="ItemCode">Product Code</asp:ListItem>
                                                <asp:ListItem Value="Status">Status</asp:ListItem>
                                                <asp:ListItem Value="RequestedBranch">Requested Branch</asp:ListItem>
                                                <asp:ListItem Value="CompletedDate">Completed Date</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                    <td style="width: 22%; text-align: left">
                                        <asp:Button ID="btnSearch" runat="server" Text="" CssClass="ButtonSearch6" EnableTheming="false"
                                            ForeColor="White" OnClick="btnSearch_Click" />
                                    </td>
                                      <td style="width: 16%" class="tblLeftNoPad">
                                            <asp:Button ID="BtnClearFilter" runat="server" OnClick="BtnClearFilter_Click" EnableTheming="false" Text="" CssClass="ClearFilter6" />
                                        </td>
                                    <td style="width: 16%" class="tblLeftNoPad">&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <input id="dummy" type="button" style="display: none" runat="server" />
                        <input id="Button1" type="button" style="display: none" runat="server" />
                        <input id="dummy1" type="button" style="display: none" runat="server" />
                        <input id="Button3" type="button" style="display: none" runat="server" />
                        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                            CancelControlID="Button1" DynamicServicePath="" Enabled="True" PopupControlID="popUp"
                            TargetControlID="dummy">
                        </cc1:ModalPopupExtender>
                        <cc1:ModalPopupExtender ID="modalPopupApproveReject" runat="server" BackgroundCssClass="modalBackground"
                            CancelControlID="Button3" DynamicServicePath="" Enabled="True" PopupControlID="popUpApproveReject"
                            TargetControlID="dummy1">
                        </cc1:ModalPopupExtender>
                        <asp:Panel runat="server" ID="popUpApproveReject" Style="width:70%">
                            <div id="Div1" runat="server" style="text-align:left">
                                <table class="tblLeft" cellpadding="0" cellspacing="0" style="border: 0px solid #5078B3;
                                    background-color: #fff; color: #000; text-align:left" width="100%">
                                    
                                    <tr>
                                        <td align="left">
                                            <div class="divArea" style="text-align:left">
                                            <table cellpadding="1" cellspacing="3" style="border: 1px solid #86b2d1; width: 100%;">
                                                <tr>
                                                        <td colspan="4" class="headerPopUp">
                                                            Approval Details
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 5px">
                                                    </tr>
                                                <tr>
                                                        <td colspan="4" align="left">
                                                            <cc1:TabContainer ID="TabContainer2" runat="server" style="Width:100%" ActiveTabIndex="0" CssClass="fancy fancy-green">
                                                                <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="Request Details">
                                                                    <ContentTemplate>
                                                                        <table style="width: 100%; border: 0px solid #86b2d1; vertical-align: text-top"
                                                                            align="center" cellpadding="1" cellspacing="1">
                                                                            <tr>
                                                                                <td class="ControlLabel" style="width: 25%;">
                                                                                    Product *
                                                                                </td>
                                                                                <td class="ControlDrpBorder" style="width: 22%;">
                                                                                    <asp:DropDownList ID="DropDownList1" runat="server" AppendDataBoundItems="true" DataTextField="ProductName" Enabled="false"
                                                                                        DataValueField="ItemCode" BackColor="#e7e7e7" CssClass="drpDownListMedium"
                                                                                        Height="26px" Style="text-align: center; border: 1px solid #e7e7e7" Width="100%">
                                                                                        <asp:ListItem Text="Select Product" Value="0"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td class="ControlLabel" style="width: 3%;" align="left">
                                                                                    
                                                                                </td>
                                                                                <td class="ControlLabel" style="width: 25%;">
                                                                                    Requested Quantity: *
                                                                                </td>
                                                                                <td class="ControlTextBox3">
                                                                                    <asp:TextBox ID="TextBox1" runat="server" BackColor="#e7e7e7" CssClass="cssTextBox" Enabled="false"
                                                                                        Height="23px" Text="0" Width="70px"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr style="height: 3px">
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="ControlLabel" style="width: 25%;">
                                                                                    Requested Branch:*
                                                                                </td>
                                                                                <td class="ControlDrpBorder" style="width: 22%;">
                                                                                    <asp:DropDownList ID="DropDownList2" runat="server" AppendDataBoundItems="true"
                                                                                        DataTextField="BranchName" DataValueField="BranchCode" Enabled="false"
                                                                                        BackColor="#e7e7e7" CssClass="drpDownListMedium" Height="26px" Style="text-align: center;
                                                                                        border: 1px solid #e7e7e7" Width="100%">
                                                                                        <asp:ListItem Text="Select Requested Branch" Value="0"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td class="ControlLabel" style="width: 3%;" align="left">
                                                                                    
                                                                                </td>
                                                                                <td class="ControlLabel" style="width: 15%;">
                                                                                    Branch Has Stock: *
                                                                                </td>
                                                                                <td class="ControlDrpBorder" style="width: 25%;">
                                                                                    <asp:DropDownList ID="DropDownList3" runat="server" AppendDataBoundItems="true" Enabled="false"
                                                                                        DataTextField="BranchName" DataValueField="BranchCode"
                                                                                        BackColor="#e7e7e7" CssClass="drpDownListMedium" Height="26px" Style="text-align: center;
                                                                                        border: 1px solid #e7e7e7" Width="100%">
                                                                                        <asp:ListItem Text="Select Branch Has Stock" Value="0"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr style="height: 3px">
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="ControlLabel" style="width: 29%;">
                                                                                  Requested Branch Current Stock
                                                                                </td>
                                                                                <td class="ControlDrpBorder" style="width: 22%;">
                                                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                                                                                                <ContentTemplate>
                                                                                   <asp:TextBox ID="txtReqStock" runat="server" CssClass="cssTextBox" Enabled="false"
                                                                                        Width="200px"></asp:TextBox>
                                                                                                                                    </ContentTemplate>
                                                                                                                                    </asp:UpdatePanel>
                                                                                </td>
                                                                                <td class="ControlLabel" style="width: 3%;" align="left">
                                                                                    
                                                                                </td>
                                                                                <td class="ControlLabel" style="width: 26%;">
                                                                                   Branch Has Stock Current Stock
                                                                                </td>
                                                                                <td class="ControlDrpBorder" style="width: 25%;">
                                                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                                                                                                <ContentTemplate>
                                                                                    <asp:TextBox ID="txtHasStock" runat="server" CssClass="cssTextBox" Enabled="false"
                                                                                        Width="200px"></asp:TextBox>     
                                                                                                                                    </ContentTemplate>
                                                                                                                                    </asp:UpdatePanel>      
                                                                                </td>
                                                                            </tr>
                                                                            <tr style="height: 3px">
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="ControlLabel" style="width: 25%;">
                                                                                    Status:
                                                                                </td>
                                                                                <td class="ControlDrpBorder" style="width: 22%;">
                                                                                    <asp:DropDownList ID="DropDownList4" runat="server" BackColor="#e7e7e7" CssClass="drpDownListMedium" Enabled="false"
                                                                                        Height="26px" Style="text-align: center; border: 1px solid #e7e7e7" Width="100%">
                                                                                        <asp:ListItem Value="Pending">Pending</asp:ListItem>
                                                                                        <asp:ListItem Value="Rejected">Rejected</asp:ListItem>
                                                                                        <asp:ListItem Value="Completed">Completed</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td class="ControlLabel" style="width: 3%;" align="left">
                                                                                    
                                                                                </td>
                                                                                <td class="ControlLabel" style="width: 25%;">
                                                                                    Rejected Reason:
                                                                                </td>
                                                                                <td class="ControlTextBox3">
                                                                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("RejectedReason") %>' CssClass="cssTextBox" Enabled="false"
                                                                                        Width="200px"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr style="height: 3px">
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="ControlLabel" style="width: 15%;">
                                                                                    RequestedDate: *
                                                                                </td>
                                                                                <td class="ControlTextBox3" style="width: 22%;">
                                                                                    <asp:TextBox ID="TextBox3" Enabled="false" runat="server" Text='<%# Bind("RequestedDate") %>'
                                                                                        CssClass="cssTextBox" Width="100px"></asp:TextBox>
                                                                                     
                                                                                </td>
                                                                                <td class="ControlLabel" style="width: 3%;" align="left">
                                                                                </td>
                                                                                <td class="ControlLabel" style="width: 25%;">
                                                                                    
                                                                                    Completed Date:
                                                                                </td>
                                                                                <td class="ControlTextBox3" style="width: 25%;">
                                                                                    <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("CompletedDate") %>' Enabled="false"
                                                                                        CssClass="cssTextBox" Width="200px"></asp:TextBox>
                                                                                </td>
                                                                                <td class="ControlLabel" style="width: 4%;" align="left">
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </ContentTemplate>
                                                                </cc1:TabPanel>
                                                            </cc1:TabContainer>
                                                            <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="BulletList" ShowMessageBox="true"
                                                                ValidationGroup="grpDetails" ShowSummary="false" HeaderText="Validation Messages"
                                                                Font-Names="'Trebuchet MS'" Font-Size="12" runat="server" />
                                                        </td>
                                                    </tr>    
                                                <tr>
                                                        <td>
                                                <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" ActiveTabIndex="0"
                                                    CssClass="fancy fancy-green">
                                                    <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="Status Update">
                                                        <ContentTemplate>
                                                            <table style="width: 600px; border: 0px solid #86b2d1; vertical-align: text-top; text-align:left"
                                                                cellpadding="1" cellspacing="1">
                                                                <tr>
                                                                    <td class="ControlLabel" style="width: 30%;">
                                                                        Status: *
                                                                    </td>
                                                                    <td class="ControlDrpBorder" style="width: 40%;">
                                                                        <asp:DropDownList ID="cmbApproveReject" runat="server" BackColor="#e7e7e7" AutoPostBack="true" CssClass="drpDownListMedium"
                                                                            Height="26px" Style="text-align: center; border: 1px solid #e7e7e7" Width="100%" OnSelectedIndexChanged="cmbApproveReject_SelectedIndexChanged">
                                                                            <asp:ListItem Value="Approve">Approve</asp:ListItem>
                                                                            <asp:ListItem Value="Reject">Reject</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td style="width: 30%; text-align:center">
                                                                        <asp:Button ID="btnSaveComments" runat="server" CausesValidation="True"
                                                                            CommandName="Insert" CssClass="Updatebutton1231" EnableTheming="false" SkinID="skinBtnSave"
                                                                            OnClick="SaveCommentsButton_Click"></asp:Button>
                                                                    </td>
                                                                </tr>
                                                                <tr runat="server" id="rowComments">
                                                                    <td class="ControlLabel" style="width: 20%;">
                                                                        Comments:
                                                                        <asp:RequiredFieldValidator ID="rvComments" runat="server" ControlToValidate="txtComments"
                                                                                        ErrorMessage="Comments is mandatory for Rejection" Enabled="false" Text="*"  />
                                                                    </td>
                                                                    <td style="width: 40%;" class="ControlDrpBorder">
                                                                        <asp:TextBox ID="txtComments" runat="server" BackColor="#e7e7e7" CssClass="cssTextBox"
                                                                            Height="23px" ValidationGroup="grpDetails" Width="115px"></asp:TextBox>
                                                                    </td>
                                                                    <td style="width: 30%; text-align:center">
                                                                        <asp:Button ID="btnCancelSaveComments" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                            CssClass="cancelbutton6" EnableTheming="false" SkinID="skinBtnCancel" OnClick="btnCancelSaveComments_Click">
                                                                        </asp:Button>
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
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="popUp" Style="width: 61%">
                            <div id="contentPopUp" runat="server">
                                <table class="tblLeft" cellpadding="0" cellspacing="0" style="border: 0px solid #5078B3;
                                    background-color: #fff; color: #000;" width="100%">
                                    <tr>
                                        <td>
                                            <div class="divArea">
                                                <table cellpadding="1" cellspacing="3" style="border: 1px solid #86b2d1; width: 100%;">
                                                    <tr>
                                                        <td colspan="4" class="headerPopUp">
                                                            Request Details
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 5px">
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" align="left">
                                                            <cc1:TabContainer ID="tabEdit" runat="server" Width="100%" ActiveTabIndex="0" CssClass="fancy fancy-green">
                                                                <cc1:TabPanel ID="tabEditMain" runat="server" HeaderText="Request Details">
                                                                    <ContentTemplate>
                                                                        <table style="width: 800px; border: 0px solid #86b2d1; vertical-align: text-top"
                                                                            align="center" cellpadding="1" cellspacing="1">
                                                                            <tr>
                                                                                <td class="ControlLabel" style="width: 25%;">
                                                                                    Product *
                                                                                    <asp:CompareValidator ID="cvProduct" runat="server" ControlToValidate="cmbProd" Display="Dynamic"
                                                                                        ValidationGroup="grpDetails" ErrorMessage="Product is Mandatory" Operator="GreaterThan"
                                                                                        Text="*" ValueToCompare="0"></asp:CompareValidator>
                                                                                </td>
                                                                                <td class="ControlDrpBorder" style="width: 25%;">
                                                                                    <asp:DropDownList ID="cmbProd" runat="server" AppendDataBoundItems="true" DataTextField="ProductName"
                                                                                        DataValueField="ItemCode" ValidationGroup="grpDetails" BackColor="#e7e7e7" CssClass="drpDownListMedium"
                                                                                        Height="26px" Style="text-align: center; border: 1px solid #e7e7e7" Width="100%">
                                                                                        <asp:ListItem Text="Select Product" Value="0"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td class="ControlLabel" style="width: 25%;">
                                                                                    Quantity: *
                                                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtQtyAdd"
                                                                                        Display="Dynamic" ErrorMessage="Product Qty. must be greater than Zero" Operator="GreaterThan"
                                                                                        Text="*" ValidationGroup="grpDetails" ValueToCompare="0"></asp:CompareValidator>
                                                                                    <asp:RequiredFieldValidator ID="rvQty" runat="server" ControlToValidate="txtQtyAdd"
                                                                                        ErrorMessage="Qty. is mandatory" Text="*" ValidationGroup="grpDetails" />
                                                                                </td>
                                                                                <td class="ControlTextBox3">
                                                                                    <asp:TextBox ID="txtQtyAdd" runat="server" BackColor="#e7e7e7" CssClass="cssTextBox"
                                                                                        Height="23px" Text="0" ValidationGroup="grpDetails" Width="70px"></asp:TextBox>
                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom, Numbers"
                                                                                        TargetControlID="txtQtyAdd" ValidChars="." />
                                                                                </td>
                                                                            </tr>
                                                                            <tr style="height: 3px">
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="ControlLabel" style="width: 25%;">
                                                                                    Requested Branch:*
                                                                                    <asp:CompareValidator ID="cvRequestedBranch" runat="server" ControlToValidate="cmbRequestedBranch"
                                                                                        Display="Dynamic" ValidationGroup="grpDetails" ErrorMessage="RequestedBranch is Mandatory"
                                                                                        Operator="GreaterThan" Text="*" ValueToCompare="0"></asp:CompareValidator>
                                                                                </td>
                                                                                <td class="ControlDrpBorder" style="width: 25%;">
                                                                                    <asp:DropDownList ID="cmbRequestedBranch" runat="server" AppendDataBoundItems="true"
                                                                                        DataTextField="BranchName" DataValueField="BranchCode" ValidationGroup="grpDetails"
                                                                                        BackColor="#e7e7e7" CssClass="drpDownListMedium" Height="26px" Style="text-align: center;
                                                                                        border: 1px solid #e7e7e7" Width="100%">
                                                                                        <asp:ListItem Text="Select Requested Branch" Value="0"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td class="ControlLabel" style="width: 15%;">
                                                                                    Branch Has Stock: *
                                                                                    <asp:CompareValidator ID="cvBranchHasStock" runat="server" ControlToValidate="cmbBranchHasStock"
                                                                                        Display="Dynamic" ValidationGroup="grpDetails" ErrorMessage="BranchHasStock is Mandatory"
                                                                                        Operator="GreaterThan" Text="*" ValueToCompare="0"></asp:CompareValidator>
                                                                                </td>
                                                                                <td class="ControlDrpBorder" style="width: 25%;">
                                                                                    <asp:DropDownList ID="cmbBranchHasStock" runat="server" AppendDataBoundItems="true"
                                                                                        DataTextField="BranchName" DataValueField="BranchCode" ValidationGroup="grpDetails"
                                                                                        BackColor="#e7e7e7" CssClass="drpDownListMedium" Height="26px" Style="text-align: center;
                                                                                        border: 1px solid #e7e7e7" Width="100%">
                                                                                        <asp:ListItem Text="Select Branch Has Stock" Value="0"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="ControlLabel" style="width: 25%;">
                                                                                    Status:
                                                                                </td>
                                                                                <td class="ControlDrpBorder" style="width: 25%;">
                                                                                    <asp:DropDownList ID="cmbStatus" runat="server" BackColor="#e7e7e7" CssClass="drpDownListMedium"
                                                                                        Height="26px" Style="text-align: center; border: 1px solid #e7e7e7" Width="100%">
                                                                                        <asp:ListItem Value="Pending">Pending</asp:ListItem>
                                                                                        <asp:ListItem Value="Rejected">Rejected</asp:ListItem>
                                                                                        <asp:ListItem Value="Completed">Completed</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td class="ControlLabel" style="width: 25%;">
                                                                                    Rejected Reason:
                                                                                </td>
                                                                                <td class="ControlTextBox3">
                                                                                    <asp:TextBox ID="txtReason" runat="server" Text='<%# Bind("RejectedReason") %>' CssClass="cssTextBox"
                                                                                        Width="200px"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="ControlLabel" style="width: 15%;">
                                                                                    RequestedDate: *
                                                                                    <asp:RequiredFieldValidator ID="rvRequesteDate" runat="server" ValidationGroup="grpDetails"
                                                                                        ControlToValidate="txtRequestedDate" Text="*" ErrorMessage="RequestedDate is mandatory"
                                                                                        CssClass="rfv" Display="Dynamic" EnableClientScript="True"></asp:RequiredFieldValidator>
                                                                                </td>
                                                                                <td class="ControlTextBox3" style="width: 25%;">
                                                                                    <asp:TextBox ID="txtRequestedDate" runat="server" Text='<%# Bind("RequestedDate") %>'
                                                                                        CssClass="cssTextBox" Width="100px"></asp:TextBox>
                                                                                </td>
                                                                                <td class="ControlLabel" style="width: 25%;">
                                                                                    Completed Date:
                                                                                </td>
                                                                                <td class="ControlTextBox3" style="width: 25%;">
                                                                                    <asp:TextBox ID="txtCompletedDate" runat="server" Text='<%# Bind("CompletedDate") %>'
                                                                                        CssClass="cssTextBox" Width="200px"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </ContentTemplate>
                                                                </cc1:TabPanel>
                                                            </cc1:TabContainer>
                                                            <asp:ValidationSummary ID="valSum" DisplayMode="BulletList" ShowMessageBox="true"
                                                                ValidationGroup="grpDetails" ShowSummary="false" HeaderText="Validation Messages"
                                                                Font-Names="'Trebuchet MS'" Font-Size="12" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 2px">
                                                        <td colspan="4">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td align="right" style="width: 36%;">
                                                                    </td>
                                                                    <td align="center" style="width: 17%;">
                                                                        <asp:Button ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                            CssClass="cancelbutton6" EnableTheming="false" SkinID="skinBtnCancel" OnClick="UpdateCancelButton_Click">
                                                                        </asp:Button>
                                                                    </td>
                                                                    <td align="center" style="width: 17%;">
                                                                        <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update"
                                                                            CssClass="Updatebutton1231" EnableTheming="false" SkinID="skinBtnSave" OnClick="UpdateButton_Click">
                                                                        </asp:Button>
                                                                        <asp:Button ID="InsertButton" runat="server" ValidationGroup="grpDetails" CausesValidation="True"
                                                                            CommandName="Insert" CssClass="Updatebutton1231" EnableTheming="false" SkinID="skinBtnSave"
                                                                            OnClick="InsertButton_Click"></asp:Button>
                                                                    </td>
                                                                    <td style="width: 30%;">
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
                            </div>
                        </asp:Panel>
                    </td>
                </tr>
                <tr style="width: 100%; margin: -4px 0px 0px 0px;">
                    <td style="width: 100%">
                        <table width="100%" style="margin: -4px 0px 0px 0px;">
                            <tr style="width: 100%">
                                <td>
                                    <div class="mainGridRequestes" id="searchGrid">
                                        <asp:GridView ID="GrdViewRequestes" runat="server" AllowSorting="True" AutoGenerateColumns="False" OnPageIndexChanging="GrdViewRequestes_PageIndexChanging"
                                            OnRowCreated="GrdViewRequestes_RowCreated" Width="100.3%" CssClass="someClass"
                                            AllowPaging="True" DataKeyNames="RequestID" EmptyDataText="No Internal Transfer Requests found!" OnRowDataBound="GrdViewRequestes_RowDataBound"
                                            OnRowCommand="GrdViewRequestes_RowCommand"  OnSelectedIndexChanged="GrdViewRequestes_SelectedIndexChanged"
                                            OnRowDeleting="GrdViewRequestes_RowDeleting">
                                            <EmptyDataRowStyle CssClass="GrdContent" />
                                            <HeaderStyle Height="30px" HorizontalAlign="Center" Font-Bold="true" BackColor="#cccccc" BorderColor="Gray" Font-Size="Small" />
                                            <RowStyle Font-Bold="true" HorizontalAlign="Center" Height="30px" Font-Size="12px" ForeColor="#414141" />
                                            <Columns>
                                                <asp:BoundField DataField="RequestID" HeaderStyle-Wrap="false" HeaderText="Req.ID"
                                                    HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="UserID" HeaderStyle-Wrap="false" HeaderText="Req.User"
                                                    HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="RequestedDate" HeaderStyle-Wrap="false" HeaderText="Req.Date" DataFormatString="{0:dd/MM/yyyy}"
                                                    HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="ItemCode" HeaderStyle-Wrap="false" HeaderText="Product"
                                                    HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="Quantity" HeaderStyle-Wrap="false" HeaderText="Quantity"
                                                    HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="RequestedBranch" HeaderText="Req.Branch" HeaderStyle-Wrap="false"
                                                    HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="BranchHasStock" HeaderText="BranchHasStock" HeaderStyle-Wrap="false"
                                                    HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="Status" HeaderStyle-Wrap="false" HeaderText="Status" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="CompletedDate" HeaderStyle-Wrap="false" HeaderText="CompletedDate" DataFormatString="{0:dd/MM/yyyy}"
                                                    HeaderStyle-BorderColor="Gray" />
                                                <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="50px" HeaderText="Approve/Reject"
                                                    HeaderStyle-BorderColor="Gray" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnApprove" runat="server" SkinID="edit" CommandName="Select" />
                                                        <asp:ImageButton ID="btnApproveDisabled" Enabled="false" SkinID="editDisable" runat="Server">
                                                        </asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="50px" HeaderText="Edit"
                                                    HeaderStyle-BorderColor="Gray" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Select" />
                                                        <asp:ImageButton ID="btnEditDisabled" Enabled="false" SkinID="editDisable" runat="Server">
                                                        </asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="50px" HeaderText="Delete"
                                                    HeaderStyle-BorderColor="Gray" Visible="false">
                                                    <ItemTemplate>
                                                        <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to Delete this Internal Request?"
                                                            runat="server">
                                                        </cc1:ConfirmButtonExtender>
                                                        <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" CommandName="Delete"></asp:ImageButton>
                                                        <asp:ImageButton ID="lnkBDisabled" Enabled="false" SkinID="deleteDisable" runat="Server">
                                                        </asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerTemplate>
                                                <table style="border-color: white">
                                                    <tr style="border-color: white">
                                                        <td style="border-color: white">
                                                            Goto Page
                                                        </td>
                                                        <td style="border-color: white">
                                                            <asp:DropDownList ID="ddlPageSelector" runat="server" AutoPostBack="true" Width="65px"
                                                                Height="23px" Style="border: 1px solid blue" OnSelectedIndexChanged="ddlPageSelector_SelectedIndexChanged" BackColor="#BBCAFB">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="border-color: white; width: 5px">
                                                        </td>
                                                        <td style="border-color: white">
                                                            <asp:Button Text="" CommandName="Page" CommandArgument="First" CausesValidation="false" runat="server" CssClass="NewFirst"
                                                                EnableTheming="false" Width="22px" Height="18px" ID="btnFirst" />
                                                        </td>
                                                        <td style="border-color: white">
                                                            <asp:Button Text="" CommandName="Page" CommandArgument="Prev" CausesValidation="false" runat="server" CssClass="NewPrev"
                                                                EnableTheming="false" Width="22px" Height="18px" ID="btnPrevious" />
                                                        </td>
                                                        <td style="border-color: white">
                                                            <asp:Button Text="" CommandName="Page" CommandArgument="Next" CausesValidation="false" runat="server" CssClass="NewNext"
                                                                EnableTheming="false" Width="22px" Height="18px" ID="btnNext" />
                                                        </td>
                                                        <td style="border-color: white">
                                                            <asp:Button Text="" CommandName="Page" CommandArgument="Last" CausesValidation="false" runat="server" CssClass="NewLast"
                                                                EnableTheming="false" Width="22px" Height="18px" ID="btnLast" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </PagerTemplate>
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr style="width: 100%">
                    <td style="width: 918px" align="left">
                            <table align="center">
                            <tr>
                                <td style="width:50%">
                                     <asp:Panel ID="pnlSearch" runat="server" Width="100px">
                                                <asp:Button ID="lnkBtnAdd" CausesValidation="false" runat="server" OnClick="lnkBtnAdd_Click"
                                                    CssClass="ButtonAdd66" EnableTheming="false" Width="80px" Text="" Visible="false"></asp:Button>
                                            </asp:Panel>
                                </td>
                                <td style="width:50%">
                                    
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%">
                        <asp:HiddenField ID="hdRequestID" runat="server" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>