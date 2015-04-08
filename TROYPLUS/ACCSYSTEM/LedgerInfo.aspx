<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="LedgerInfo.aspx.cs" Inherits="LedgerInfo" Title="Financials > Ledger Information" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server" VerticalScrollBarVisibility="Hidden">
    <script language="javascript" type="text/javascript">

        window.onload = function Showalert() {

            var txt = document.getElementById("<%= txtSearch.ClientID %>");
            var btn = document.getElementById("<%= BtnClearFilter.ClientID %>");
            if (txt.value == "") {
                // alert(txt.value);
                btn.style.visibility = "hidden";
                // when the window is loaded, hide the button if the textbox is empty
            }

        }

        function EnableDisableButton(sender, target) {
            var first = document.getElementById('<%=txtSearch.ClientID %>');

            if (sender.value.length >= 1 && first.value.length >= 1) {
                document.getElementById('<%=BtnClearFilter.ClientID %>').style.visibility = "visible";

            }

            if (sender.value.length < 1 && first.value.length < 1) {

                document.getElementById('<%=BtnClearFilter.ClientID %>').style.visibility = "Hidden";
            }
        }

        function Mobile_Validator() {
            var ctrMobile = document.getElementById('ctl00_cplhControlPanel_frmViewAdd_txtMobile');

            if (ctrMobile == null)
                ctrMobile = document.getElementById('ctl00_cplhControlPanel_frmViewAdd_txtMobileAdd');

            var txtMobile = ctrMobile.value;

            if (txtMobile.length > 0) {
                if (txtMobile.length != 10) {
                    alert("Customer Mobile Number should be minimum of 10 digits.");
                    Page_IsValid = false;
                    return window.event.returnValue = false;
                }

                if (txtMobile.charAt(0) == "0") {
                    alert("Customer Mobile should not start with Zero. Please remove Zero and try again.");
                    Page_IsValid = false;
                    return window.event.returnValue = false;
                }
            }
            else {
                Page_IsValid = true;
            }
        }


        function Check() {
            var ctr = document.getElementById('ctl00_cplhControlPanel_frmViewAdd_drpModeofContact');

            var ctrMobile = document.getElementById('ctl00_cplhControlPanel_frmViewAdd_txtMobile');
            var ctrEmail = document.getElementById('ctl00_cplhControlPanel_frmViewAdd_txtEmailId');
            var txt = ctr.value;
            var Mobile = ctrMobile.value;
            var Email = ctrEmail.value;

            if (txt == "1") {
                if (Mobile == "") {
                    alert("Enter Mobile Number");
                    Page_IsValid = false;
                    return window.event.returnValue = false;
                }
            }
            else if (txt == "2") {
                if (Email == "") {
                    alert("Enter Email Id");
                    Page_IsValid = false;
                    return window.event.returnValue = false;
                }
            }
            else {
                Page_IsValid = true;
            }
        }




        function CheckMode() {
            var ctr = document.getElementById('ctl00_cplhControlPanel_frmViewAdd_drpModeofContactAdd');

            var ctrMobile = document.getElementById('ctl00_cplhControlPanel_frmViewAdd_txtMobileAdd');
            var ctrEmail = document.getElementById('ctl00_cplhControlPanel_frmViewAdd_txtEmailIdAdd');
            var txt = ctr.value;
            var Mobile = ctrMobile.value;
            var Email = ctrEmail.value;

            if (txt == "1") {
                if (Mobile == "") {
                    alert("Enter Mobile Number");
                    Page_IsValid = false;
                    return window.event.returnValue = false;
                }
            }
            else if (txt == "2") {
                if (Email == "") {
                    alert("Enter Email Id");
                    Page_IsValid = false;
                    return window.event.returnValue = false;
                }
            }
            else {
                Page_IsValid = true;
            }
        }

    </script>
    <asp:UpdatePanel ID="UpdatePanel16" runat="server" UpdateMode="Always">
        <ContentTemplate>

            <table style="width: 100%">
                <tr style="width: 100%">
                    <td style="width: 100%">

                        <%--<div class="mainConHd">
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tr valign="middle">
                                        <td>
                                            <span>Account Ledgers</span>
                                        </td>
                                    </tr>
                                </table>
                            </div>--%>
                        <%--<table class="mainConHd" style="width: 994px;">
                                <tr valign="middle">
                                    <td style="font-size: 20px;">
                                        Account Ledgers
                                    </td>
                                </tr>
                            </table>--%>
                        <div class="mainConBody">
                            <table style="width: 99.8%; margin: -2px 0px 0px 1px;" cellpadding="3" cellspacing="2" class="searchbg">
                                <tr style="height: 25px; vertical-align: middle">
                                    <td style="width: 2%;"></td>
                                    <td style="width: 25%; font-size: 22px; color: White;">Account Ledgers
                                    </td>
                                    <td style="width: 10%">
                                        <div style="text-align: right;">
                                         
                                        </div>
                                    </td>
                                    <td style="width: 13%; color: White;" align="right">Search
                                    </td>
                                    <td style="width: 18%" class="NewBox">
                                        <asp:TextBox ID="txtSearch" runat="server" SkinID="skinTxtBoxSearch"></asp:TextBox>
                                    </td>
                                    <td style="width: 18%" class="NewBox">
                                        <div style="width: 160px; font-family: 'Trebuchet MS';">
                                            <asp:DropDownList ID="ddCriteria" runat="server" Width="154px" BackColor="White" Height="23px" Style="text-align: center; border: 1px solid White">
                                                <asp:ListItem Value="0">All</asp:ListItem>
                                                <asp:ListItem Value="LedgerName">Ledger Name</asp:ListItem>
                                                <asp:ListItem Value="AliasName">Alias Name</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                    <td style="width: 19%" class="tblLeftNoPad">
                                        <asp:Button ID="btnSearch" onkeyup="EnableDisableButton(this,'BtnClearFilter')" runat="server" Text="" CssClass="ButtonSearch6" EnableTheming="false" />
                                    </td>
                                    <td style="width: 20%" class="tblLeftNoPad">
                                        <asp:Button ID="BtnClearFilter" runat="server" OnClick="BtnClearFilter_Click" EnableTheming="false" Text="" CssClass="ClearFilter6" />
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <input id="dummy" type="button" style="display: none" runat="server" />
                        <input id="Button1" type="button" style="display: none" runat="server" />
                        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                            CancelControlID="Button1" DynamicServicePath="" Enabled="True" PopupControlID="popUp"
                            TargetControlID="dummy">
                        </cc1:ModalPopupExtender>
                        <asp:Panel runat="server" ID="popUp" Style="width: 56%">
                            <div id="contentPopUp">
                                <table class="tblLeft" cellpadding="2" cellspacing="2" style="border: 0px solid #5078B3; background-color: #fff; color: #000;"
                                    width="100%">
                                    <tr>
                                        <td>
                                            <asp:FormView ID="frmViewAdd" runat="server" Width="100%" DataSourceID="frmSource"
                                                DataKeyNames="LedgerID" OnItemCommand="frmViewAdd_ItemCommand" DefaultMode="Edit"
                                                OnItemCreated="frmViewAdd_ItemCreated" Visible="False"
                                                EmptyDataText="No Records" OnItemInserted="frmViewAdd_ItemInserted" OnModeChanged="frmViewAdd_ModeChanged" OnDataBound="frmViewAdd_DataBound"
                                                OnItemUpdated="frmViewAdd_ItemUpdated">
                                                <RowStyle HorizontalAlign="left" CssClass="GrdContent allPad" VerticalAlign="Middle"
                                                    BorderColor="#cccccc" Height="20px" />
                                                <EditRowStyle HorizontalAlign="left" CssClass="GrdAlternateColor allPad" BorderColor="#cccccc"
                                                    VerticalAlign="middle" Height="20px" />
                                                <HeaderStyle HorizontalAlign="left" CssClass="GrdHeaderbgClr GrdHdrContent allPad"
                                                    Height="25px" BorderColor="#cccccc" VerticalAlign="Middle" />
                                                <EditItemTemplate>
                                                    <div class="divArea">
                                                        <table cellpadding="1" cellspacing="1" style="border: 1px solid #86b2d1; width: 100%;">
                                                            <tr>
                                                                <td colspan="6" class="headerPopUp">Ledger Information
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 10px">
                                                            </tr>
                                                            <tr>
                                                               
                                                                <td class="ControlLabel" style="width: 15%">Ledger Name *
                                                                    <asp:RequiredFieldValidator ID="rvLdgrName" runat="server" ControlToValidate="txtLdgrName"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Please enter Ledger Name. It cannot be left blank.">*</asp:RequiredFieldValidator>
                                                                     <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="LowercaseLetters, UppercaseLetters,Numbers,Custom" ValidChars=" .-/\"  TargetControlID="txtLdgrName" />
                                                                </td>
                                                                <td class="ControlTextBox3" style="width: 25%">
                                                                    <asp:TextBox ID="txtLdgrName" runat="server" Text='<%# Bind("LedgerName") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                </td>
                                                                 <td style="width: 5%"></td>
                                                                <td class="ControlLabel" style="width: 15%">Alias Name
                                                                    <%--<asp:RequiredFieldValidator ID="rvAliasName" runat="server" ControlToValidate="txtAliasName"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Alias is mandatory">*</asp:RequiredFieldValidator>--%>
                                                                </td>
                                                                <td class="ControlTextBox3" style="width: 20%">
                                                                    <asp:TextBox ID="txtAliasName" runat="server" Text='<%# Bind("AliasName") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 10%"></td>
                                                            </tr>
                                                            <tr style="height: 3px">
                                                            </tr>
                                                            <tr>
                                                                
                                                                <td class="ControlLabel" style="width: 15%">Contact Person Name
                                                                </td>
                                                                <td class="ControlTextBox3" style="width: 25%">
                                                                    <asp:TextBox ID="txtContName" runat="server" Text='<%# Bind("ContactName") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 5%"></td>
                                                                <td class="ControlLabel" style="width: 15%">Opening Balance *
                                                                    <asp:CompareValidator ID="cvOpenBal" runat="server" ControlToValidate="txtOpenBal"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Open Balance should be numeric value"
                                                                        Operator="DataTypeCheck" Type="Double">*</asp:CompareValidator>
                                                                    <asp:RequiredFieldValidator ID="rvOpenBal" runat="server" ControlToValidate="txtOpenBal"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Please enter Opening Balance. It cannot be left blank.">*</asp:RequiredFieldValidator>
                                                                    <cc1:FilteredTextBoxExtender ID="OBvalid" runat="server" FilterType="Numbers" TargetControlID="txtOpenBal" />
                                                                </td>
                                                                <td class="ControlNumberBox3" style="width: 20%">
                                                                    <asp:TextBox ID="txtOpenBal" TabIndex="4" runat="server" Text='<%# Bind("OpenBalance") %>'
                                                                                                CssClass="cssTextBox" Width="100%"></asp:TextBox>
                                                                                            <asp:DropDownList ID="ddCRDR" EnableTheming="false" TabIndex="5" runat="server" Style="border: 1px solid Gray" Height="26px" CssClass="drpDownListMedium" BackColor="#e7e7e7"
                                                                                                Width="68px" SelectedValue='<%# Bind("DRORCR") %>'>
                                                                                                <asp:ListItem Text="CR" Value="CR"></asp:ListItem>
                                                                                                <asp:ListItem Text="DR" Value="DR"></asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                </td>
                                                                <td align="left" style="width: 10%"></td>
                                                            </tr>
                                                            <tr style="height: 3px">
                                                            </tr>
                                                            <tr>
                                                              
                                                                <td class="ControlLabel" style="width: 15%">Name On Cheque 
                                                                </td>
                                                                <td class="ControlTextBox3" style="width: 20%">
                                                                    <asp:TextBox ID="txtChequeName" runat="server" Text='<%# Bind("ChequeName") %>'
                                                                        SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                </td>
                                                                  <td style="width: 5%"></td>
                                                                <td class="ControlLabel" style="width: 15%">Account Group *
                                                                    <asp:CompareValidator ID="cvPhase" runat="server" ControlToValidate="ddAccGroup"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Please select Account Group. It cannot be left blank."
                                                                        Operator="GreaterThan" ValueToCompare="0">*</asp:CompareValidator>
                                                                </td>
                                                                <td class="ControlDrpBorder" style="width: 20%">
                                                                    <asp:DropDownList ID="ddAccGroup" DataSourceID="srcGroupInfo" CssClass="drpDownListMedium" BackColor="#e7e7e7"
                                                                        runat="server" SelectedValue='<%# Bind("GroupID") %>' DataTextField="GroupName" Style="border: 1px solid #e7e7e7" Height="26px"
                                                                        DataValueField="GroupID" Width="100%" AppendDataBoundItems="True">
                                                                        <asp:ListItem style="background-color: #90c9fc" Selected="True" Value="0">Select Account Group</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>

                                                            </tr>
                                                            <tr style="height: 3px">
                                                            </tr>
                                                            <tr>
                                                              
                                                                <td class="ControlLabel" style="width: 15%">Is Active *
                                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="drpunuse"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Is Active is Mandatory"
                                                                        Operator="GreaterThan" ValueToCompare="0">*</asp:CompareValidator>
                                                                </td>
                                                                <td class="ControlDrpBorder" style="width: 20%">
                                                                    <asp:DropDownList ID="drpunuse" TabIndex="10" AutoPostBack="false" runat="server" BackColor="#e7e7e7" OnDataBound="drpunuse_DataBound" CssClass="drpDownListMedium"
                                                                        Width="100%" Style="border: 1px solid #e7e7e7" Height="26px" SelectedValue='<%# Bind("UnUse") %>'>
                                                                        <asp:ListItem Text="NO" Value="NO"></asp:ListItem>
                                                                        <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                  <td style="width: 5%"></td>
                                                                <td class="ControlLabel" style="width: 15%">Mode of Contact *
                                                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="drpModeofContact"
                                                                        Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="Please select Mode of Contact. It cannot be left blank."
                                                                        Operator="GreaterThan" ValueToCompare="0" />
                                                                </td>
                                                                <td style="width: 20%" class="ControlDrpBorder">
                                                                    <asp:DropDownList ID="drpModeofContact" TabIndex="11" Width="100%" CssClass="drpDownListMedium" BackColor="#e7e7e7" Style="border: 1px solid #e7e7e7" Height="26px"
                                                                        runat="server" OnDataBound="drpModeofContact_DataBound" AutoPostBack="false" SelectedValue='<%# Bind("ModeofContact") %>'>
                                                                       <%-- <asp:ListItem Text="Select Mode of Contact" Value="0" Selected="True"></asp:ListItem>--%>
                                                                        <asp:ListItem Text="SMS" Value="1" ></asp:ListItem>
                                                                        <asp:ListItem Text="Email" Value="2"></asp:ListItem>
                                                                        <asp:ListItem Text="Paper" Value="3"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>

                                                            </tr>
                                                            <tr style="height: 3px">
                                                            </tr>
                                                            <tr>
                                                               
                                                                <td class="ControlLabel" style="width: 15%">Email Id
                                                                    <asp:RegularExpressionValidator ID="remail12" runat="server"
                                                                                                ControlToValidate="txtEmailId" Display="Dynamic" Text="*" EnableClientScript="True" ErrorMessage="Please enter Correct Email Address. It cannot be left blank."
                                                                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                                                                            </asp:RegularExpressionValidator>
                                                                </td>
                                                                <td class="ControlTextBox3" style="width: 20%">
                                                                    <asp:TextBox ID="txtEmailId" TabIndex="12" MaxLength="20" runat="server" Text='<%# Bind("EmailId") %>'
                                                                        SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                </td>
                                                                 <td style="width: 5%"></td>
                                                                <td class="ControlLabel" style="width: 15%">Mobile
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                                                        TargetControlID="txtMobile" />
                                                                </td>
                                                                <td class="ControlTextBox3" style="width: 20%">
                                                                    <asp:TextBox ID="txtMobile" MaxLength="10" TabIndex="13" runat="server" Text='<%# Bind("Mobile") %>'
                                                                        SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                </td>

                                                            </tr>
                                                            <tr style="height: 3px">
                                                            </tr>
                                                            <tr>
                                                               
                                                                <td class="ControlLabel" style="width: 15%">OB Due Date    
                                                                </td>
                                                                <td class="ControlTextBox3" style="width: 20%">
                                                                    <asp:TextBox ID="txtdueDate" Enabled="false" MaxLength="10" TabIndex="13" Width="150%" BackColor="#e7e7e7" runat="server" Text='<%# Bind("OpDueDate") %>'
                                                                        SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="calBillDate1" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="btnBillDate1" TargetControlID="txtdueDate">
                                                                    </cc1:CalendarExtender>
                                                                </td>
                                                                 <td style="width: 5%" align="left">
                                                                      <asp:ImageButton ID="btnBillDate1" runat="server" CausesValidation="False" ImageUrl="App_Themes/NewTheme/images/cal.gif" Width="20px" />
                                                                 </td>
                                                                <td style="width: 15%;"   >
                                                                   
                                                                   
                                                                </td>
                                                                <td style="width: 20%">
                                                                     <asp:DropDownList Visible="false" ID="drpBranch" TabIndex="14" Width="100%" CssClass="drpDownListMedium" AppendDataBoundItems="true" BackColor="#e7e7e7" Style="border: 1px solid #e7e7e7" Height="26px"
                                                                         runat="server">
                                                                     </asp:DropDownList>
                                                                </td>

                                                            </tr>

                                                            <tr>
                                                                <td></td>
                                                                <td>
                                                                    <asp:TextBox ID="txtAdd2" TabIndex="4" runat="server" Text='<%# Bind("Add2") %>'
                                                                        SkinID="skinTxtBoxGrid" Visible="False"></asp:TextBox>
                                                                </td>
                                                                <td>

                                                                    <cc1:FilteredTextBoxExtender ID="FTBoxE5Add" runat="server" FilterType="Custom,Numbers"
                                                                        TargetControlID="txtPhone" ValidChars="+" />
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtPhone" TabIndex="8" runat="server" Text='<%# Bind("Phone") %>'
                                                                        SkinID="skinTxtBoxGrid" Visible="False"></asp:TextBox>
                                                                </td>

                                                            </tr>
                                                            <tr>
                                                                <td></td>
                                                                <td>
                                                                    <asp:TextBox ID="txtAdd3" TabIndex="5" runat="server" Text='<%# Bind("Add3") %>'
                                                                        SkinID="skinTxtBoxGrid" Visible="False"></asp:TextBox>
                                                                </td>
                                                                <td></td>
                                                                <td></td>

                                                            </tr>
                                                            <tr>
                                                                <td></td>
                                                                <td>
                                                                    <asp:DropDownList ID="drpIncharge" Width="99%" EnableTheming="false" TabIndex="11" BackColor="#e7e7e7"
                                                                        AppendDataBoundItems="true" runat="server" CssClass="drpDownListMedium" DataSourceID="empSrcAdd"
                                                                        DataTextField="empFirstName" DataValueField="empno" SelectedValue='<%# Bind("ExecutiveIncharge") %>' Visible="False">
                                                                        <asp:ListItem Text="Select Executive" Value="0" Selected="True"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="drpdc" TabIndex="10" AutoPostBack="false" runat="server" BackColor="#e7e7e7" CssClass="drpDownListMedium"
                                                                        Width="100%" Style="border: 1px solid #e7e7e7" Height="26px" SelectedValue='<%# Bind("dc") %>' Visible="False">
                                                                        <asp:ListItem Text="NO" Value="NO" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtTin" TabIndex="12" runat="server" Width="150%" BackColor="#e7e7e7" Text='<%# Bind("TINnumber") %>'
                                                                        SkinID="skinTxtBoxGrid" Visible="False"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td></td>
                                                                <td>
                                                                    <asp:TextBox ID="txtAdd1" TabIndex="3" runat="server" Text='<%# Bind("Add1") %>'
                                                                        SkinID="skinTxtBoxGrid" Visible="False"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <%--<asp:CompareValidator ID="cvLedgerAdd" runat="server" ControlToValidate="drpLedgerCatAdd"
                                                                        Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="Category is mandatory"
                                                                        Operator="GreaterThan" ValueToCompare="0" />--%>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="drpLedgerCatAdd" Width="99%" EnableTheming="false" TabIndex="10" Height="24px" BackColor="#e7e7e7" CssClass="drpDownListMedium"
                                                                        runat="server" OnDataBound="drpLedgerCatAdd_DataBound" Visible="False">
                                                                        <asp:ListItem Text="Select Category" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 5px">
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:DropDownList ID="drpPaymentmade" TabIndex="10" AutoPostBack="false" runat="server" BackColor="#e7e7e7" OnDataBound="drpPaymentmade_DataBound" CssClass="drpDownListMedium"
                                                                        Width="100%" Style="border: 1px solid #90c9fc" Height="26px" Visible="false" SelectedValue='<%# Bind("Paymentmade") %>'>
                                                                        <asp:ListItem Text="NO" Value="NO" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="drpIntTrans" TabIndex="10" AutoPostBack="false" runat="server" BackColor="#e7e7e7" CssClass="drpDownListMedium"
                                                                        Width="100%" Style="border: 1px solid #90c9fc" Height="26px" Visible="false" SelectedValue='<%# Bind("Inttrans") %>'>
                                                                        <asp:ListItem Text="NO" Value="NO" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 6px">
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6">
                                                                    <table style="width: 100%">
                                                                        <tr>
                                                                            <td style="width: 32%"></td>
                                                                            <td style="width: 18%">
                                                                                <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update"
                                                                                    CssClass="Updatebutton1231" EnableTheming="false" SkinID="skinBtnSave" OnClientClick="Mobile_Validator();Check();"
                                                                                    OnClick="UpdateButton_Click"></asp:Button>
                                                                            </td>
                                                                            <td style="width: 18%">
                                                                                <asp:Button ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                    CssClass="cancelbutton6" EnableTheming="false" SkinID="skinBtnCancel" OnClick="UpdateCancelButton_Click"></asp:Button>
                                                                            </td>
                                                                            <td style="width: 30%"></td>

                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 5px">
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <table cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <asp:ObjectDataSource ID="srcGroupInfo" runat="server" SelectMethod="ListGroupForLedInfo"
                                                                    TypeName="BusinessLogic" OldValuesParameterFormatString="original_{0}">
                                                                    <SelectParameters>
                                                                        <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                    </SelectParameters>
                                                                </asp:ObjectDataSource>
                                                            </td>
                                                            <td>
                                                                <asp:ValidationSummary ID="valSum" DisplayMode="BulletList" ShowMessageBox="true"
                                                                    ShowSummary="false" HeaderText="" Font-Names="Trebuchet MS"
                                                                    Font-Size="12" runat="server" />
                                                            </td>
                                                            <td></td>
                                                            <td></td>
                                                        </tr>
                                                    </table>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                                <InsertItemTemplate>
                                                    <div class="divArea">
                                                        <table cellpadding="1" cellspacing="1" style="border: 1px solid #86b2d1; width: 100%;">
                                                            <tr>
                                                                <td colspan="6" class="headerPopUp">Ledger Information
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 5px">
                                                            </tr>
                                                            <tr>
                                                                
                                                                <td class="ControlLabel" style="width: 15%">Ledger Name *
                                                                    <asp:RequiredFieldValidator ID="rvLdgrNameAdd" runat="server" ControlToValidate="txtLdgrNameAdd"
                                                                        Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="Please enter Ledger Name. It cannot be left blank."></asp:RequiredFieldValidator>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="LowercaseLetters, UppercaseLetters,Numbers,Custom" ValidChars=" .-/\"  TargetControlID="txtLdgrNameAdd" />
                                                                </td>
                                                                <td class="ControlTextBox3" style="width: 20%">
                                                                    <asp:TextBox ID="txtLdgrNameAdd" runat="server" Text='<%# Bind("LedgerName") %>'
                                                                        SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 5%"></td>
                                                                <td class="ControlLabel" style="width: 15%">Alias Name
                                                                    <%--<asp:RequiredFieldValidator ID="rvAliasNameAdd" runat="server" ControlToValidate="txtAliasNameAdd"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Alias is mandatory">*</asp:RequiredFieldValidator>--%>
                                                                </td>
                                                                <td class="ControlTextBox3" style="width: 20%">
                                                                    <asp:TextBox ID="txtAliasNameAdd" runat="server" Text='<%# Bind("AliasName") %>'
                                                                        SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 10%"></td>
                                                            </tr>
                                                            <tr style="height: 3px">
                                                            </tr>
                                                            <tr>
                                                               
                                                                <td class="ControlLabel" style="width: 15%">Contact Person Name
                                                                </td>
                                                                <td class="ControlTextBox3" style="width: 20%">
                                                                    <asp:TextBox ID="txtContNameAdd" runat="server" Text='<%# Bind("ContactName") %>'
                                                                        SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                </td>
                                                                 <td style="width: 5%"></td>
                                                                <td class="ControlLabel" style="width: 15%">Opening Balance *
                                                                    <asp:CompareValidator ID="cvOpenBalAdd" runat="server" ControlToValidate="txtOpenBalAdd"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Open Balance should be numeric value"
                                                                        Operator="DataTypeCheck" Type="Double">*</asp:CompareValidator>
                                                                    <asp:RequiredFieldValidator ID="rvOpenBalAdd" runat="server" ControlToValidate="txtOpenBalAdd"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Please enter Opening Balance. It cannot be left blank.">*</asp:RequiredFieldValidator>
                                                                    <cc1:FilteredTextBoxExtender ID="OBvalidAdd" runat="server" FilterType="Numbers"
                                                                        TargetControlID="txtOpenBalAdd" />
                                                                </td>
                                                                <td class="ControlNumberBox3" style="width: 20%">
                                                                    <asp:TextBox ID="txtOpenBalAdd" TabIndex="4" runat="server" BackColor="#e7e7e7" Text="0"
                                                                                                CssClass="cssTextBox" Width="100%"></asp:TextBox>
                                                                                            <asp:DropDownList ID="ddCRDRAdd" EnableTheming="false" TabIndex="5" runat="server" Style="border: 1px solid Gray" Height="26px" CssClass="drpDownListMedium" BackColor="#e7e7e7"
                                                                                                Width="68px" SelectedValue='<%# Bind("DRORCR") %>'>
                                                                                                <asp:ListItem Text="CR" Value="CR"></asp:ListItem>
                                                                                                <asp:ListItem Text="DR" Value="DR"></asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                </td>
                                                                <td align="left" style="width: 10%"></td>
                                                            </tr>
                                                            <tr style="height: 3px">
                                                            </tr>
                                                            <tr>
                                                                
                                                                <td class="ControlLabel" style="width: 15%">Name On Cheque 
                                                                </td>
                                                                <td class="ControlTextBox3" style="width: 20%">
                                                                    <asp:TextBox ID="txtChequeNameAdd" runat="server" Text='<%# Bind("ChequeName") %>'
                                                                        SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 5%"></td>
                                                                <td class="ControlLabel" style="width: 15%">Account Group *
                                                                    <asp:CompareValidator ID="cvPhaseAdd" runat="server" ControlToValidate="ddAccGroupAdd"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Please select Account Group. It cannot be left blank."
                                                                        Operator="GreaterThan" ValueToCompare="0">*</asp:CompareValidator>
                                                                </td>
                                                                <td class="ControlDrpBorder" style="width: 20%">
                                                                    <asp:DropDownList ID="ddAccGroupAdd" DataSourceID="srcGroupInfoAdd" CssClass="drpDownListMedium" BackColor="#e7e7e7"
                                                                        runat="server" SelectedValue='<%# Bind("GroupID") %>' DataTextField="GroupName" Style="border: 1px solid #e7e7e7" Height="26px"
                                                                        DataValueField="GroupID" Width="100%" AppendDataBoundItems="True">
                                                                        <asp:ListItem style="background-color: #e7e7e7" Selected="True" Value="0">Select Account Group</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="width: 10%"></td>
                                                            </tr>
                                                            <tr style="height: 3px">
                                                            </tr>
                                                            <tr>
                                                                
                                                                <td class="ControlLabel" style="width: 15%">
                                                                    <%--Un Use--%>
                                                                    Is Active *
                                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="drpunuseAdd"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Is Active is Mandatory"
                                                                        Operator="GreaterThan" ValueToCompare="0">*</asp:CompareValidator>
                                                                </td>
                                                                <td class="ControlDrpBorder" style="width: 20%">
                                                                    <asp:DropDownList ID="drpunuseAdd" TabIndex="10" AutoPostBack="false" runat="server" BackColor="#e7e7e7" OnDataBound="drpunuseAdd_DataBound" CssClass="drpDownListMedium"
                                                                        Width="100%" Style="border: 1px solid #e7e7e7" Height="26px" SelectedValue='<%# Bind("unuse") %>'>
                                                                        <asp:ListItem Text="NO" Value="NO" Enabled="false"></asp:ListItem>
                                                                        <asp:ListItem Text="YES" Value="YES" Selected="True" Enabled="true"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="width: 5%"></td>
                                                                <td class="ControlLabel" style="width: 15%">Mode of Contact *
                                                                    <asp:CompareValidator ID="CompareValidator213" runat="server" ControlToValidate="drpModeofContactAdd"
                                                                        Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="Please select Mode of Contact. It cannot be left blank."
                                                                        Operator="GreaterThan" ValueToCompare="0" />
                                                                </td>
                                                                <td style="width: 20%" class="ControlDrpBorder">
                                                                    <asp:DropDownList ID="drpModeofContactAdd" TabIndex="11" Width="100%" AutoPostBack="false" OnDataBound="drpModeofContactAdd_DataBound" CssClass="drpDownListMedium" BackColor="#e7e7e7" Style="border: 1px solid #e7e7e7" Height="26px"
                                                                        runat="server" SelectedValue='<%# Bind("ModeofContact") %>'>
                                                                        <%--<asp:ListItem Text="Select Mode of Contact" Value="0" Selected="True"></asp:ListItem>--%>
                                                                        <asp:ListItem Text="SMS" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="Email" Value="2"></asp:ListItem>
                                                                        <asp:ListItem Text="Paper" Value="3" Selected="True"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="width: 10%"></td>
                                                            </tr>
                                                            <tr style="height: 3px">
                                                            </tr>
                                                            <tr>
                                                              
                                                                <td class="ControlLabel" style="width: 15%">Email Id
                                                                    <asp:RegularExpressionValidator ID="remail1" runat="server"
                                                                                                ControlToValidate="txtEmailIdAdd" Display="Dynamic" Text="*" EnableClientScript="True" ErrorMessage="Please enter Correct Email Address. It cannot be left blank."
                                                                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                                                                            </asp:RegularExpressionValidator>
                                                                    
                                                                </td>
                                                                <td class="ControlTextBox3" style="width: 20%">
                                                                    <asp:TextBox ID="txtEmailIdAdd" MaxLength="20" TabIndex="12" Width="150%" BackColor="#e7e7e7" runat="server" Text='<%# Bind("EmailId") %>'
                                                                        SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                </td>
                                                                  <td style="width: 5%"></td>
                                                                <td class="ControlLabel" style="width: 15%">Mobile
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender123" runat="server" FilterType="Numbers"
                                                                        TargetControlID="txtMobileAdd" />
                                                                </td>
                                                                <td style="width: 20%" class="ControlTextBox3">
                                                                    <asp:TextBox ID="txtMobileAdd" MaxLength="10" TabIndex="13" runat="server" Text='<%# Bind("Mobile") %>'
                                                                        SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 10%;"></td>
                                                            </tr>


                                                            <tr style="height: 3px">
                                                            </tr>
                                                            <tr>
                                                               
                                                                <td class="ControlLabel" style="width: 15%">OB Due Date    
                                                                </td>
                                                                <td class="ControlTextBox3" style="width: 20%">
                                                                    <asp:TextBox ID="txtdueDateadd" Enabled="false" MaxLength="10" TabIndex="13" Width="150%" BackColor="#e7e7e7" runat="server" Text='<%# Bind("OpDueDate") %>'
                                                                        SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="calBillDate" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="btnBillDate" TargetControlID="txtdueDateadd">
                                                                    </cc1:CalendarExtender>
                                                                </td>
                                                                 <td style="width: 5%" align="left">
                                                                     <asp:ImageButton ID="btnBillDate" runat="server" CausesValidation="False" ImageUrl="App_Themes/NewTheme/images/cal.gif" Width="20px" />
                                                                 </td>
                                                                <td style="width: 15%;" >
                                                                    
                                                                    
                                                                </td>
                                                                <td style="width: 20%" >
                                                                     <asp:DropDownList ID="drpBranchAdd" Visible="false" TabIndex="10" Width="100%" CssClass="drpDownListMedium" AppendDataBoundItems="true" BackColor="#e7e7e7" Style="border: 1px solid #e7e7e7" Height="26px"
                                                                         runat="server">
                                                                     </asp:DropDownList>
                                                                </td>

                                                            </tr>
                                                            <%--<tr>
                                                                <td colspan="5">
                                                                    &nbsp
                                                                </td>
                                                            </tr>--%>
                                                            <tr>
                                                                <td></td>
                                                                <td>
                                                                    <asp:TextBox ID="txtAdd2Add" TabIndex="4" runat="server" Text='<%# Bind("Add2") %>'
                                                                        SkinID="skinTxtBoxGrid" Visible="False"></asp:TextBox>
                                                                </td>
                                                                <td>

                                                                    <cc1:FilteredTextBoxExtender ID="FTBoxE5Add" runat="server" FilterType="Custom,Numbers"
                                                                        TargetControlID="txtPhoneAdd" ValidChars="+" />
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtPhoneAdd" TabIndex="8" runat="server" Text='<%# Bind("Phone") %>'
                                                                        SkinID="skinTxtBoxGrid" Visible="False"></asp:TextBox>
                                                                </td>

                                                            </tr>
                                                            <tr>
                                                                <td></td>
                                                                <td>
                                                                    <asp:TextBox ID="txtAdd3Add" TabIndex="5" runat="server" Text='<%# Bind("Add3") %>'
                                                                        SkinID="skinTxtBoxGrid" Visible="False"></asp:TextBox>
                                                                </td>
                                                                <td></td>
                                                                <td></td>

                                                            </tr>
                                                            <tr>
                                                                <td></td>
                                                                <td>
                                                                    <asp:DropDownList ID="drpInchargeAdd" Width="99%" EnableTheming="false" TabIndex="11" BackColor="#e7e7e7"
                                                                        AppendDataBoundItems="true" runat="server" CssClass="drpDownListMedium" DataSourceID="empSrcAdd" Height="24px"
                                                                        DataTextField="empFirstName" DataValueField="empno" SelectedValue='<%# Bind("ExecutiveIncharge") %>' Visible="False">
                                                                        <asp:ListItem Text="Select Executive" Value="0" Selected="True"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td></td>
                                                                <td>
                                                                    <asp:TextBox ID="txtTinAdd" TabIndex="12" runat="server" Width="150%" BackColor="#e7e7e7" Text='<%# Bind("TINnumber") %>'
                                                                        SkinID="skinTxtBoxGrid" Visible="False"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td></td>
                                                                <td>
                                                                    <asp:TextBox ID="txtAdd1Add" TabIndex="3" runat="server" Text='<%# Bind("Add1") %>'
                                                                        SkinID="skinTxtBoxGrid" Visible="False"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="drpdcAdd" TabIndex="10" AutoPostBack="false" runat="server" BackColor="#e7e7e7" CssClass="drpDownListMedium"
                                                                        Width="100%" Style="border: 1px solid #e7e7e7" Height="26px" SelectedValue='<%# Bind("dc") %>' Visible="False">
                                                                        <asp:ListItem Text="NO" Value="NO" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="drpLedgerCatAdd" Width="99%" EnableTheming="false" TabIndex="10" Height="24px" BackColor="#e7e7e7" CssClass="drpDownListMedium"
                                                                        runat="server" OnDataBound="drpLedgerCatAdd_DataBound" Visible="False">
                                                                        <asp:ListItem Text="Select Category" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:DropDownList ID="drpPaymentmadeAdd" TabIndex="10" AutoPostBack="false" runat="server" BackColor="#e7e7e7" OnDataBound="drpPaymentmadeAdd_DataBound" CssClass="drpDownListMedium"
                                                                        Width="100%" Style="border: 1px solid #e7e7e7" Height="26px" Visible="false" SelectedValue='<%# Bind("Paymentmade") %>'>
                                                                        <asp:ListItem Text="NO" Value="NO" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="drpIntTransAdd" TabIndex="10" AutoPostBack="false" runat="server" BackColor="#e7e7e7" CssClass="drpDownListMedium"
                                                                        Width="100%" Style="border: 1px solid #e7e7e7" Height="26px" Visible="false" SelectedValue='<%# Bind("Inttrans") %>'>
                                                                        <asp:ListItem Text="NO" Value="NO" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td></td>
                                                            </tr>
                                                            <tr style="height: 6px">
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6">
                                                                    <table style="width: 100%">
                                                                        <tr>
                                                                            <td style="width: 32%"></td>
                                                                            <td style="width: 18%">
                                                                                <asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                                                                    CssClass="savebutton1231" EnableTheming="false" SkinID="skinBtnSave" OnClientClick="Mobile_Validator();CheckMode();"
                                                                                    OnClick="InsertButton_Click"></asp:Button>
                                                                            </td>
                                                                            <td style="width: 18%">
                                                                                <asp:Button ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                    CssClass="cancelbutton6" EnableTheming="false" SkinID="skinBtnCancel" OnClick="InsertCancelButton_Click"></asp:Button>
                                                                            </td>
                                                                            <td style="width: 30%"></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>

                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <table cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <asp:ObjectDataSource ID="srcGroupInfoAdd" runat="server" SelectMethod="ListGroupForLedInfo"
                                                                    TypeName="BusinessLogic" OldValuesParameterFormatString="original_{0}">
                                                                    <SelectParameters>
                                                                        <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                    </SelectParameters>
                                                                </asp:ObjectDataSource>
                                                            </td>
                                                            <td>
                                                                <asp:ValidationSummary ID="valSumAdd" DisplayMode="BulletList" ShowMessageBox="true"
                                                                    ShowSummary="false" HeaderText="" Font-Names="Trebuchet MS"
                                                                    Font-Size="12" runat="server" />
                                                            </td>
                                                            <td></td>
                                                            <td></td>
                                                        </tr>
                                                    </table>
                                                </InsertItemTemplate>
                                            </asp:FormView>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                    </td>
                </tr>
                <tr style="width: 100%">
                    <td style="width: 100%">
                        <table width="100%" style="margin: -5px 0px 0px 0px;">
                            <tr style="width: 100%">
                                <td>
                                    <div class="mainGridHold" id="searchGrid">
                                        <asp:GridView ID="GrdViewLedger" runat="server" AllowSorting="True" AutoGenerateColumns="False" OnRowDeleting="GrdViewLedger_RowDeleting"
                                            OnRowCreated="GrdViewLedger_RowCreated" Width="100.4%" DataSourceID="GridSource"
                                            AllowPaging="True" DataKeyNames="LedgerID" EmptyDataText="No Ledger Data Found." CssClass="someClass"
                                            OnRowCommand="GrdViewLedger_RowCommand" OnRowDataBound="GrdViewLedger_RowDataBound"
                                            OnRowDeleted="GrdViewLedger_RowDeleted" Font-Names="Trebuchet MS">
                                           <HeaderStyle Height="30px" HorizontalAlign="Center" Font-Bold="true" BackColor="#cccccc" BorderColor="Gray" Font-Size="15px" />
                                                    <RowStyle Font-Bold="true" HorizontalAlign="Center" Height="30px" Font-Size="15px" CssClass="GrdItemForecolor" ForeColor="#414141" />
                                            <Columns>
                                                <asp:BoundField DataField="LedgerName" HeaderText="Ledger Name" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="AliasName" HeaderText="Alias Name" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="GroupName" HeaderText="Group Name" HeaderStyle-BorderColor="Gray" />
                                                <asp:TemplateField HeaderText="Opening Balance" HeaderStyle-BorderColor="Gray">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBalance" runat="server"></asp:Label>
                                                        <asp:HiddenField ID="DRORCR" runat="server" Value='<%# Bind("DRORCR") %>' />
                                                        <asp:HiddenField ID="OpenBalance" runat="server" Value='<%# Bind("OpenBalance") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ContactName" HeaderText="Contact Name" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="LedgerCategory" HeaderText="Category" Visible="false" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="TinNumber" HeaderText="TIN#" Visible="false" HeaderStyle-BorderColor="Gray" />
                                                <asp:TemplateField ItemStyle-CssClass="command" ItemStyle-Width="50px" HeaderText="Edit" HeaderStyle-BorderColor="Gray">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Select" />
                                                        <asp:ImageButton ID="btnEditDisabled" Enabled="false" SkinID="editDisable" runat="Server"></asp:ImageButton>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="command" Width="50px"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="50px" HeaderText="Delete" HeaderStyle-BorderColor="Gray">
                                                    <ItemTemplate>
                                                        <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to Delete this Ledger?"
                                                            runat="server">
                                                        </cc1:ConfirmButtonExtender>
                                                        <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" CommandName="Delete"></asp:ImageButton>
                                                        <asp:ImageButton ID="lnkBDisabled" Enabled="false" SkinID="deleteDisable" runat="Server"></asp:ImageButton>
                                                        <asp:HiddenField ID="ldgID" runat="server" Value='<%# Bind("LedgerID") %>' />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="50px"></HeaderStyle>
                                                    <ItemStyle CssClass="command"></ItemStyle>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerTemplate>
                                                <table style="border-color: white">
                                                    <tr style="border-color: white">
                                                        <td style="border-color: white">Goto Page
                                                        </td>
                                                        <td style="border-color: white">
                                                            <asp:DropDownList ID="ddlPageSelector" runat="server" AutoPostBack="true" Width="70px" Height="23px" BackColor="#e7e7e7" Style="border: 1px solid Gray">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="border-color: white; width: 5px"></td>
                                                        <td style="border-color: white">
                                                            <asp:Button Text="" CommandName="Page" CommandArgument="First" runat="server" CssClass="NewFirst" EnableTheming="false" Width="22px" Height="18px"
                                                                ID="btnFirst" />
                                                        </td>
                                                        <td style="border-color: white">
                                                            <asp:Button Text="" CommandName="Page" CommandArgument="Prev" runat="server" CssClass="NewPrev" EnableTheming="false" Width="22px" Height="18px"
                                                                ID="btnPrevious" />
                                                        </td>
                                                        <td style="border-color: white">
                                                            <asp:Button Text="" CommandName="Page" CommandArgument="Next" runat="server" CssClass="NewNext" EnableTheming="false" Width="22px" Height="18px"
                                                                ID="btnNext" />
                                                        </td>
                                                        <td style="border-color: white">
                                                            <asp:Button Text="" CommandName="Page" CommandArgument="Last" runat="server" CssClass="NewLast" EnableTheming="false" Width="22px" Height="18px"
                                                                ID="btnLast" />
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
                        <asp:ObjectDataSource ID="GridSource" runat="server" SelectMethod="ListLedgerInfoOthers"
                            TypeName="BusinessLogic" DeleteMethod="DeleteLedger" OnDeleting="GridSource_Deleting">
                            <DeleteParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                <asp:Parameter Name="LedgerID" Type="Int32" />
                                <asp:Parameter Name="Username" Type="String" />
                            </DeleteParameters>
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="empSrc" runat="server" SelectMethod="ListExecutive" TypeName="BusinessLogic">
                            <SelectParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="empSrcAdd" runat="server" SelectMethod="ListExecutive"
                            TypeName="BusinessLogic">
                            <SelectParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="frmSource" runat="server" SelectMethod="GetLedgerInfoForId"
                            TypeName="BusinessLogic" OnUpdating="frmSource_Updating" OnInserting="frmSource_Inserting"
                            InsertMethod="InsertLedgerInfo" UpdateMethod="UpdateLedgerInfo1">
                            <UpdateParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                <asp:Parameter Name="LedgerID" Type="Int32" />
                                <asp:Parameter Name="LedgerName" Type="String" />
                                <asp:Parameter Name="AliasName" Type="String" />
                                <asp:Parameter Name="GroupID" Type="Int32" />
                                <asp:Parameter Name="OpenBalanceDR" Type="Double" />
                                <asp:Parameter Name="OpenBalanceCR" Type="Double" />
                                <asp:Parameter Name="OpenBalance" Type="Double" />
                                <asp:Parameter Name="ContactName" Type="String" />
                                <asp:Parameter Name="DRORCR" Type="String" />
                                <asp:Parameter Name="Add1" Type="String" />
                                <asp:Parameter Name="Add2" Type="String" />
                                <asp:Parameter Name="Add3" Type="String" />
                                <asp:Parameter Name="Phone" Type="String" />
                                <asp:Parameter Name="LedgerCategory" Type="String" />
                                <asp:Parameter Name="ExecutiveIncharge" Type="Int32" />
                                <asp:Parameter Name="TinNumber" Type="String" />
                                <asp:Parameter Name="Mobile" Type="String" />
                                <asp:Parameter Name="Inttrans" Type="String" />
                                <asp:Parameter Name="Paymentmade" Type="String" />
                                <asp:Parameter Name="dc" Type="String" />
                                <asp:Parameter Name="ChequeName" Type="String" />
                                <asp:Parameter Name="Username" Type="String" />
                                <asp:Parameter Name="unuse" Type="String" />
                                <asp:Parameter Name="EmailId" Type="String" />
                                <asp:Parameter Name="ModeofContact" Type="Int32" />
                                <asp:Parameter Name="OpDueDate" Type="String" />
                                <asp:Parameter Name="BranchCode" Type="String" />
                            </UpdateParameters>
                            <SelectParameters>
                                <asp:ControlParameter ControlID="GrdViewLedger" Name="LedgerID" PropertyName="SelectedValue"
                                    Type="String" />
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                            </SelectParameters>
                            <InsertParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                <asp:Parameter Name="LedgerName" Type="String" />
                                <asp:Parameter Name="AliasName" Type="String" />
                                <asp:Parameter Name="GroupID" Type="Int32" />
                                <asp:Parameter Name="OpenBalanceDR" Type="Double" />
                                <asp:Parameter Name="OpenBalanceCR" Type="Double" />
                                <asp:Parameter Name="OpenBalance" Type="Double" />
                                <asp:Parameter Name="ContactName" Type="String" />
                                <asp:Parameter Name="DRORCR" Type="String" />
                                <asp:Parameter Name="Add1" Type="String" />
                                <asp:Parameter Name="Add2" Type="String" />
                                <asp:Parameter Name="Add3" Type="String" />
                                <asp:Parameter Name="Phone" Type="String" />
                                <asp:Parameter Name="LedgerCategory" Type="String" />
                                <asp:Parameter Name="ExecutiveIncharge" Type="Int32" />
                                <asp:Parameter Name="TinNumber" Type="String" />
                                <asp:Parameter Name="Mobile" Type="String" />
                                <asp:Parameter Name="Inttrans" Type="String" />
                                <asp:Parameter Name="Paymentmade" Type="String" />
                                <asp:Parameter Name="dc" Type="String" />
                                <asp:Parameter Name="ChequeName" Type="String" />
                                <asp:Parameter Name="Username" Type="String" />
                                <asp:Parameter Name="unuse" Type="String" />
                                <asp:Parameter Name="EmailId" Type="String" />
                                <asp:Parameter Name="ModeofContact" Type="Int32" />
                                <asp:Parameter Name="OpDueDate" Type="String" />
                                <asp:Parameter Name="BranchCode" Type="String" />
                            </InsertParameters>
                        </asp:ObjectDataSource>
                    </td>
                </tr>
              
            </table>
            <table width="100%">
                  <tr>
                    <td style="width:20%"></td>
                    <td style="width:20%" align="right">
                         
                                                <asp:Button ID="lnkBtnAdd" runat="server" OnClick="lnkBtnAdd_Click" CssClass="ButtonAdd66"
                                                    EnableTheming="false" Width="80px" Text=""></asp:Button>
                                           
                    </td>
                    <td style="width:20%" align="left" >
                        <asp:Button ID="btnpay" runat="server"
                            CssClass="exportexl6" EnableTheming="false" CausesValidation="false"
                            OnClientClick="window.open('ReportExcelLedger.aspx','CSTSummary', 'toolbar=no,status=no,menu=no,location=no,height=310,width=500,left=425,top=220 ,resizable=yes, scrollbars=yes');"></asp:Button>
                    </td>
                    <td style="width:20%"></td>
                    <td style="width:20%"></td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
