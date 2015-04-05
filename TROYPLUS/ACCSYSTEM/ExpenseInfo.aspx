<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="ExpenseInfo.aspx.cs" Inherits="ExpenseInfo" Title="Financials > Expense Information" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
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
                                            <span>Expense Types</span>
                                        </td>
                                    </tr>
                                </table>
                            </div>--%>
                        <%--<table class="mainConHd" style="width: 994px;">
                                <tr valign="middle">
                                    <td style="font-size: 20px;">
                                        Expense Types
                                    </td>
                                </tr>
                            </table>--%>
                        <div class="mainConBody">
                            <table style="width: 99.8%; margin: -2px 0px 0px 1px;" cellpadding="3" cellspacing="2" class="searchbg">
                                <tr style="height: 25px; vertical-align: middle">
                                    <td style="width: 1%;"></td>
                                    <td style="width: 25%; font-size: 22px; color: White;">Expense Types
                                    </td>
                                    <td style="width: 5%">
                                        <div style="text-align: right;">
                                          
                                        </div>
                                    </td>
                                    <td style="width: 15%; color: White;" align="right">Search
                                    </td>
                                    <td style="width: 21%" class="NewBox">
                                        <asp:TextBox ID="txtSearch" runat="server" SkinID="skinTxtBoxSearch"></asp:TextBox>
                                    </td>
                                    <td style="width: 21%" class="NewBox">
                                        <div style="width: 160px; font-family: 'Trebuchet MS';">
                                            <asp:DropDownList ID="ddCriteria" runat="server" Width="154px" Height="23px" BackColor="White" Style="text-align: center; border: 1px solid White">
                                                <asp:ListItem Value="0">All</asp:ListItem>
                                                <asp:ListItem Value="LedgerName">Expense Name</asp:ListItem>
                                                <asp:ListItem Value="AliasName">Alias Name</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                    <td style="width: 21%" class="tblLeftNoPad">
                                        <asp:Button ID="btnSearch" onkeyup="EnableDisableButton(this,'BtnClearFilter')" runat="server" Text="" CssClass="ButtonSearch6" EnableTheming="false" />
                                    </td>
                                    <td style="width: 15%" class="tblLeftNoPad">
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

                        <asp:Panel runat="server" ID="popUp" Style="width: 50%">
                            <div id="contentPopUp">
                                <table style="width: 100%;" align="center">
                                    <tr style="width: 100%">
                                        <td style="width: 100%">
                                            <asp:FormView ID="frmViewAdd" runat="server" Width="100%" DataSourceID="frmSource" OnModeChanged="frmViewAdd_ModeChanged" OnDataBound="frmViewAdd_DataBound"
                                                DataKeyNames="ID" OnItemCommand="frmViewAdd_ItemCommand" DefaultMode="Edit"
                                                OnItemCreated="frmViewAdd_ItemCreated" Visible="False" OnItemInserting="frmViewAdd_ItemInserting"
                                                OnItemUpdating="frmViewAdd_ItemUpdating" EmptyDataText="No Records" OnItemInserted="frmViewAdd_ItemInserted"
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
                                                                <td colspan="5" class="headerPopUp">Expenses Information
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 15px">
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width: 40%">Expense Name *
                                                                    <asp:RequiredFieldValidator ID="rvLdgrName" runat="server" ControlToValidate="txtLdgrName"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Please enter Expense Name. It cannot be left blank.">*</asp:RequiredFieldValidator>
                                                                    <cc1:FilteredTextBoxExtender ID="rvLdgrNameAddFilteredTextBoxExtender123" runat="server" FilterType="LowercaseLetters, UppercaseLetters,Numbers"
                                                                        TargetControlID="txtLdgrName" />
                                                                </td>
                                                                <td class="ControlTextBox3" style="width: 25%">
                                                                    <asp:TextBox ID="txtLdgrName"  runat="server" Text='<%# Bind("Expensehead") %>' SkinID="skinTxtBoxGrid"
                                                                        TabIndex="1"></asp:TextBox>
                                                                </td>

                                                                
                                                                <td style="width: 35%"></td>
                                                            </tr>
                                                            <tr style="height: 3px">
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width: 20%">Alias Name
                                                                    <%--<asp:RequiredFieldValidator ID="rvAliasName" runat="server" ControlToValidate="txtAliasName"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Alias is mandatory">*</asp:RequiredFieldValidator>--%>
                                                                </td>
                                                                <td class="ControlTextBox3" style="width: 25%">
                                                                    <asp:TextBox ID="txtAliasName" runat="server"  Text='<%# Bind("AliasName") %>' SkinID="skinTxtBoxGrid"
                                                                        TabIndex="4"></asp:TextBox>
                                                                </td>
                                                                 </tr>
                                                            <tr id="trid" runat="server" visible="false">
                                                                <td class="ControlLabel" style="width: 20%">Opening Balance *
                                                                    <asp:CompareValidator ID="cvOpenBal" runat="server" ControlToValidate="txtOpenBal"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Open Balance should be numeric value"
                                                                        Operator="DataTypeCheck" Type="Double">*</asp:CompareValidator>
                                                                   <%-- <asp:RequiredFieldValidator ID="rvOpenBal" runat="server" ControlToValidate="txtOpenBal"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Please enter Opening Balance. It cannot be left blank.">*</asp:RequiredFieldValidator>
                                                                    <cc1:FilteredTextBoxExtender ID="OBvalid" runat="server" FilterType="Numbers" TargetControlID="txtOpenBal" />--%>
                                                                
                                                                    <asp:TextBox ID="txtOpenBal" runat="server"  Style="border: 1px solid #e7e7e7" Width="60%" BackColor="#e7e7e7"
                                                                        TabIndex="2"></asp:TextBox>
                                                                    <asp:DropDownList ID="ddCRDR" runat="server" Width="55px" BackColor="#e7e7e7" Style="border: 1px solid Gray" Height="26px" CssClass="drpDownListMedium"
                                                                        TabIndex="3">
                                                                        <asp:ListItem Text="CR" Value="CR"></asp:ListItem>
                                                                        <asp:ListItem Text="DR" Value="DR"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                Mode of Contact *
                                                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="drpModeofContact"
                                                                        Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="Mode of Contact is mandatory"
                                                                        Operator="GreaterThan" ValueToCompare="0" />
                                                                
                                                                    <asp:DropDownList ID="drpModeofContact" TabIndex="10" Width="100%" CssClass="drpDownListMedium" BackColor="#e7e7e7" Style="border: 1px solid #e7e7e7" Height="26px"
                                                                        runat="server"  AutoPostBack="false" >
                                                                        <%--<asp:ListItem Text="Select Mode of Contact" Value="0"></asp:ListItem>--%>
                                                                        <asp:ListItem Text="SMS" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="Email" Value="2"></asp:ListItem>
                                                                        <asp:ListItem Text="Paper" Value="3" Selected="True"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>

                                                            </tr>
                                                            <tr runat="server" visible="false">
                                                                <td class="ControlLabel" style="width: 20%">Email Id
                                                                </td>
                                                                <td class="ControlTextBox3" style="width: 25%">
                                                                    <asp:TextBox ID="txtEmailId" TabIndex="8" runat="server" 
                                                                        SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                Mobile
                                                                
                                                                    <asp:TextBox ID="txtMobile" MaxLength="10" TabIndex="13" runat="server" 
                                                                        SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td style="width: 25%">
                                                                    <asp:DropDownList Visible="false" ID="drpunuse" TabIndex="10" AutoPostBack="false" runat="server" BackColor="#e7e7e7" OnDataBound="drpunuse_DataBound" CssClass="drpDownListMedium"
                                                                        Width="100%" Style="border: 1px solid #e7e7e7" Height="26px" SelectedValue='<%# Bind("IsActive") %>'>
                                                                        <asp:ListItem Text="NO" Value="NO" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                
                                                                    <asp:TextBox ID="txtdueDate" Visible="false" Enabled="false" MaxLength="10" TabIndex="13" Width="150%" BackColor="#e7e7e7" runat="server" 
                                                                        SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="calBillDate" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="btnBillDate" TargetControlID="txtdueDate">
                                                                    </cc1:CalendarExtender>
                                                                </td>
                                                                <td style="width: 14%; visibility:hidden" align="left">
                                                                    <asp:ImageButton ID="btnBillDate" runat="server" CausesValidation="False" ImageUrl="App_Themes/NewTheme/images/cal.gif" Width="20px" />
                                                                </td>
                                                            </tr>
                                                            <%--<tr>
                                                                <td colspan="4">
                                                                    
                                                                </td>
                                                            </tr>--%>
                                                            <%--<tr style="height:3px">
                                                                                    </tr>--%>
                                                            <%--<tr style="height:5px">
                                                                <td>
                                                                    
                                                                </td>
                                                                <td>
                                                                    
                                                                </td>
                                                            </tr> 
                                                            <tr style="height:3px">
                                                                                    </tr>--%>
                                                            <tr>
                                                                <td></td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddAccGroup" DataSourceID="srcGroupInfo" CssClass="drpDownListMedium" BackColor="#90c9fc"
                                                                        runat="server"  DataTextField="GroupName"
                                                                        DataValueField="GroupID" Width="98%" AppendDataBoundItems="True" Visible="False">
                                                                    </asp:DropDownList>
                                                            </tr>
                                                            <tr>
                                                                <td></td>
                                                                <td style="visibility:hidden">
                                                                    <asp:TextBox ID="txtContName" runat="server" 
                                                                        SkinID="skinTxtBoxGrid" TabIndex="3" Visible="False"></asp:TextBox>
                                                                
                                                                
                                                                    <asp:DropDownList ID="drpLedgerCat" Width="99%" EnableTheming="false" TabIndex="10" Height="24px" BackColor="#90c9fc" CssClass="drpDownListMedium"
                                                                        runat="server" Visible="False">
                                                                        <asp:ListItem Text="Select Category" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <%--<tr style="height:3px">
                                                                                    </tr>--%>
                                                            <tr>
                                                                <td></td>
                                                                <td>
                                                                    <asp:DropDownList ID="drpIncharge" Width="99%" EnableTheming="false" TabIndex="11" BackColor="#90c9fc"
                                                                        AppendDataBoundItems="true" runat="server" CssClass="drpDownListMedium" DataSourceID="empSrc" Height="24px"
                                                                        DataTextField="empFirstName" DataValueField="empno" Visible="False">
                                                                        <asp:ListItem Text="Select Executive" Value="0" Selected="True"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                
                                                                    <asp:TextBox ID="txtTin" TabIndex="12" runat="server" Width="150%" BackColor="#90c9fc" 
                                                                        SkinID="skinTxtBoxGrid" Visible="False"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txtAdd1" TabIndex="6" runat="server" 
                                                                        SkinID="skinTxtBoxGrid" Visible="False"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtChequeName" TabIndex="15" runat="server" Width="150%" BackColor="#90c9fc" 
                                                                        SkinID="skinTxtBoxGrid" Visible="False"></asp:TextBox>
                                                                
                                                                    <asp:TextBox ID="txtPhone" TabIndex="9" runat="server" 
                                                                        SkinID="skinTxtBoxGrid" Visible="False"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <%--<tr style="height:3px">
                                                                                    </tr>--%>
                                                            <tr>
                                                                <td>
                                                                    <asp:DropDownList ID="drpdc" TabIndex="10" AutoPostBack="false" runat="server" BackColor="#90c9fc" CssClass="drpDownListMedium"
                                                                        Width="100%" Style="border: 1px solid #90c9fc" Height="26px"  Visible="False">
                                                                        <asp:ListItem Text="NO" Value="NO" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtAdd2" TabIndex="7" runat="server" 
                                                                        SkinID="skinTxtBoxGrid" Visible="False"></asp:TextBox>
                                                                
                                                                    <asp:TextBox ID="txtAdd3" TabIndex="8" runat="server" 
                                                                        SkinID="skinTxtBoxGrid" Visible="False"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:DropDownList ID="drpPaymentmade" TabIndex="10" AutoPostBack="false" runat="server" BackColor="#90c9fc"  CssClass="drpDownListMedium"
                                                                        Width="100%" Style="border: 1px solid #90c9fc" Height="26px" Visible="false" >
                                                                        <asp:ListItem Text="NO" Value="NO" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="drpIntTrans" TabIndex="10" AutoPostBack="false" runat="server" BackColor="#90c9fc" CssClass="drpDownListMedium"
                                                                        Width="100%" Style="border: 1px solid #90c9fc" Height="26px" Visible="false" >
                                                                        <asp:ListItem Text="NO" Value="NO" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4">
                                                                    <table style="width: 100%">
                                                                        <tr>
                                                                            <td align="right" style="width: 15%"></td>
                                                                            <td align="right" style="width: 30%">
                                                                                <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update"
                                                                                    CssClass="Updatebutton1231" EnableTheming="false" SkinID="skinBtnSave" 
                                                                                    OnClick="UpdateButton_Click"></asp:Button>
                                                                            </td>
                                                                            <td align="center" style="width: 30%">
                                                                                <asp:Button ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                    CssClass="cancelbutton6" EnableTheming="false" SkinID="skinBtnCancel" OnClick="UpdateCancelButton_Click"></asp:Button>
                                                                            </td>
                                                                            <td align="right" style="width: 15%"></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <table cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <asp:ObjectDataSource ID="srcGroupInfo" runat="server" SelectMethod="ListGroupInfo"
                                                                    TypeName="BusinessLogic" OldValuesParameterFormatString="original_{0}">
                                                                    <SelectParameters>
                                                                        <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                    </SelectParameters>
                                                                </asp:ObjectDataSource>
                                                                <td>
                                                                    <asp:ValidationSummary ID="valSum" DisplayMode="BulletList" ShowMessageBox="true"
                                                                        ShowSummary="false" HeaderText="" Font-Names="'Trebuchet MS'"
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
                                                                <td colspan="5" class="headerPopUp">Expenses Information
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 15px">
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width: 40%">Expense Name *
                                                                    <asp:RequiredFieldValidator ID="rvLdgrNameAdd" runat="server" ControlToValidate="txtLdgrNameAdd"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Please enter Expense Name. It cannot be left blank.">*</asp:RequiredFieldValidator>
                                                                    <cc1:FilteredTextBoxExtender ID="rvLdgrNameAddFilteredTextBoxExtender2" runat="server" FilterType="LowercaseLetters, UppercaseLetters,Numbers"
                                                                        TargetControlID="txtLdgrNameAdd" />
                                                                </td>
                                                                <td class="ControlTextBox3" style="width: 25%">
                                                                    <asp:TextBox ID="txtLdgrNameAdd" runat="server" Text='<%# Bind("Expensehead") %>'
                                                                        SkinID="skinTxtBoxGrid" TabIndex="1"></asp:TextBox>
                                                                </td>

                                                                
                                                                <td style="width: 35%"></td>
                                                            </tr>
                                                            <tr style="height: 3px">
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width: 40%">Alias Name
                                                                    <%--<asp:RequiredFieldValidator ID="rvAliasNameAdd" runat="server" ControlToValidate="txtAliasNameAdd"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Alias is mandatory">*</asp:RequiredFieldValidator>--%>
                                                                </td>
                                                                <td style="width: 25%" class="ControlTextBox3">
                                                                    <asp:TextBox ID="txtAliasNameAdd" runat="server" Text='<%# Bind("AliasName") %>'
                                                                        SkinID="skinTxtBoxGrid" TabIndex="4"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 35%"></td>
                                                            </tr>
                                                            <tr runat="server" visible="false">
                                                                <td class="ControlLabel" style="width: 20%">Opening Balance *
                                                                    <%--<asp:CompareValidator ID="cvOpenBalAdd" runat="server" ControlToValidate="txtOpenBalAdd"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Open Balance should be numeric value"
                                                                        Operator="DataTypeCheck" Type="Double">*</asp:CompareValidator>
                                                                    <asp:RequiredFieldValidator ID="rvOpenBalAdd" runat="server" ControlToValidate="txtOpenBalAdd"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Please enter Opening Balance. It cannot be left blank.">*</asp:RequiredFieldValidator>
                                                                    <cc1:FilteredTextBoxExtender ID="OBvalidAdd" runat="server" FilterType="Numbers"
                                                                        TargetControlID="txtOpenBalAdd" />--%>
                                                                </td>
                                                                <td class="ControlNumberBox3" style="width: 25%">
                                                                    <asp:TextBox ID="txtOpenBalAdd" runat="server" Text="0"
                                                                        Style="border: 1px solid #e7e7e7" Width="60%" TabIndex="2" BackColor="#e7e7e7"></asp:TextBox>
                                                                    <asp:DropDownList ID="ddCRDRAdd" runat="server" Width="55px" BackColor="#e7e7e7" Style="border: 1px solid Gray" Height="26px" CssClass="drpDownListMedium" SelectedValue='<%# Bind("DRORCR") %>'
                                                                        TabIndex="3">
                                                                        <asp:ListItem Text="CR" Value="CR"></asp:ListItem>
                                                                        <asp:ListItem Text="DR" Value="DR"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                Mode of Contact *
                                                                    <%--<asp:CompareValidator ID="CompareValidator213" runat="server" ControlToValidate="drpModeofContactAdd"
                                                                        Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="Mode of Contact is mandatory"
                                                                        Operator="GreaterThan" ValueToCompare="0" />--%>
                                                                
                                                                    <asp:DropDownList ID="drpModeofContactAdd" TabIndex="10" Width="100%" AutoPostBack="false" OnDataBound="drpModeofContactAdd_DataBound" CssClass="drpDownListMedium" BackColor="#e7e7e7" Style="border: 1px solid #e7e7e7" Height="26px"
                                                                        runat="server" SelectedValue='<%# Bind("ModeofContact") %>'>
                                                                        <%--<asp:ListItem Text="Select Mode of Contact" Value="0" ></asp:ListItem>--%>
                                                                        <asp:ListItem Text="SMS" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="Email" Value="2"></asp:ListItem>
                                                                        <asp:ListItem Text="Paper" Value="3" Selected="True"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr runat="server" visible="false">
                                                                <td class="ControlLabel" style="width: 20%">Email Id
                                                                </td>
                                                                <td class="ControlTextBox3" style="width: 25%">
                                                                    <asp:TextBox ID="txtEmailIdAdd" MaxLength="10" TabIndex="13" Width="150%" BackColor="#e7e7e7" runat="server" Text='<%# Bind("EmailId") %>'
                                                                        SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                Mobile
                                                                
                                                                    <asp:TextBox ID="txtMobileAdd" MaxLength="10" TabIndex="13" Width="150%" BackColor="#e7e7e7" runat="server" Text='<%# Bind("Mobile") %>'
                                                                        SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                               
                                                                <td style="width: 25%">
                                                                    <asp:DropDownList ID="drpunuseAdd" Visible="false" TabIndex="10" AutoPostBack="false" runat="server" BackColor="#e7e7e7" OnDataBound="drpunuseAdd_DataBound" CssClass="drpDownListMedium"
                                                                        Width="100%" Style="border: 1px solid #e7e7e7" Height="26px" SelectedValue='<%# Bind("unuse") %>'>
                                                                        <asp:ListItem Text="NO" Value="NO"></asp:ListItem>
                                                                        <asp:ListItem Text="YES" Value="YES" Selected="True"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                               
                                                                    <asp:TextBox ID="txtdueDateadd" Visible="false" Enabled="false" MaxLength="10" TabIndex="13" Width="150%" BackColor="#e7e7e7" runat="server" Text='<%# Bind("OpDueDate") %>'
                                                                        SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="calBillDate1" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="btnBillDate1" TargetControlID="txtdueDateadd">
                                                                    </cc1:CalendarExtender>
                                                                </td>
                                                                <td style="width: 14%; visibility:hidden" align="left">
                                                                    <asp:ImageButton ID="btnBillDate1" runat="server" CausesValidation="False" ImageUrl="App_Themes/NewTheme/images/cal.gif" Width="20px" />
                                                                </td>
                                                            </tr>
                                                            <%--<tr style="height:3px">
                                                                                    </tr>--%>
                                                            <tr runat="server" visible="false">
                                                                <td></td>
                                                                
                                                                <td>
                                                                    <asp:DropDownList ID="ddAccGroupAdd" DataSourceID="srcGroupInfoAdd" CssClass="drpDownListMedium" BackColor="#90c9fc"
                                                                        runat="server" SelectedValue='<%# Bind("GroupID") %>' DataTextField="GroupName"
                                                                        DataValueField="GroupID" Width="98%" AppendDataBoundItems="True" Visible="False">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr runat="server" visible="false">
                                                                <td></td>
                                                                <td>
                                                                    <asp:TextBox ID="txtContNameAdd" runat="server" Text='<%# Bind("ContactName") %>'
                                                                        SkinID="skinTxtBoxGrid" TabIndex="3" Visible="False"></asp:TextBox>
                                                                
                                                                    <asp:DropDownList ID="drpLedgerCatAdd" Width="99%" EnableTheming="false" TabIndex="10" BackColor="#90c9fc" Style="border: 1px solid #90c9fc" Height="26px" CssClass="drpDownListMedium"
                                                                        runat="server" OnDataBound="drpLedgerCatAdd_DataBound" Visible="False">
                                                                        <asp:ListItem Text="Select Category" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td></td>
                                                                <td>
                                                                    <asp:DropDownList ID="drpInchargeAdd" Width="99%" EnableTheming="false" TabIndex="11" BackColor="#90c9fc"
                                                                        AppendDataBoundItems="true" runat="server" CssClass="drpDownListMedium" DataSourceID="empSrcAdd"
                                                                        DataTextField="empFirstName" DataValueField="empno" SelectedValue='<%# Bind("ExecutiveIncharge") %>' Visible="False">
                                                                        <asp:ListItem Text="Select Executive" Value="0" Selected="True"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                
                                                                    <asp:TextBox ID="txtChequeNameAdd" TabIndex="15" runat="server" Width="150%" BackColor="#90c9fc" Text='<%# Bind("ChequeName") %>'
                                                                        SkinID="skinTxtBoxGrid" Visible="False"></asp:TextBox>
                                                                
                                                                    <asp:TextBox ID="txtTinAdd" TabIndex="12" runat="server" Width="150%" BackColor="#90c9fc" Text='<%# Bind("TINnumber") %>'
                                                                        SkinID="skinTxtBoxGrid" Visible="False"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                
                                                                <td>
                                                                    <asp:TextBox ID="txtAdd1Add" TabIndex="6" runat="server" Text='<%# Bind("Add1") %>'
                                                                        SkinID="skinTxtBoxGrid" Visible="False"></asp:TextBox>
                                                                
                                                                    <asp:DropDownList ID="drpdcAdd" TabIndex="10" AutoPostBack="false" runat="server" BackColor="#90c9fc" CssClass="drpDownListMedium"
                                                                        Width="100%" Style="border: 1px solid #90c9fc" Height="26px" SelectedValue='<%# Bind("dc") %>' Visible="False">
                                                                        <asp:ListItem Text="NO" Value="NO" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <%--<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Custom, Numbers"
                                                                        TargetControlID="txtPhoneAdd" ValidChars="+" />--%>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtPhoneAdd" TabIndex="9" runat="server" Text='<%# Bind("Phone") %>'
                                                                        SkinID="skinTxtBoxGrid" Visible="False"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td></td>
                                                                <td>
                                                                    <asp:TextBox ID="txtAdd2Add" TabIndex="7" runat="server" Text='<%# Bind("Add2") %>'
                                                                        SkinID="skinTxtBoxGrid" Visible="False"></asp:TextBox>
                                                                
                                                                    <asp:TextBox ID="txtAdd3Add" TabIndex="8" runat="server" Text='<%# Bind("Add3") %>'
                                                                        SkinID="skinTxtBoxGrid" Visible="False"></asp:TextBox>
                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <td>
                                                                    <asp:DropDownList ID="drpPaymentmadeAdd" TabIndex="10" AutoPostBack="false" runat="server" BackColor="#90c9fc" OnDataBound="drpPaymentmadeAdd_DataBound" CssClass="drpDownListMedium"
                                                                        Width="100%" Style="border: 1px solid #90c9fc" Height="26px" SelectedValue='<%# Bind("Paymentmade") %>' Visible="false">
                                                                        <asp:ListItem Text="NO" Value="NO" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="drpIntTransAdd" TabIndex="10" AutoPostBack="false" runat="server" BackColor="#90c9fc" CssClass="drpDownListMedium"
                                                                        Width="100%" Style="border: 1px solid #90c9fc" Height="26px" Visible="false" SelectedValue='<%# Bind("Inttrans") %>'>
                                                                        <asp:ListItem Text="NO" Value="NO" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="5">
                                                                    <table style="width: 100%">
                                                                        <tr>
                                                                            <td align="right" style="width: 15%"></td>
                                                                            <td align="right" style="width: 30%">
                                                                                <asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                                                                    CssClass="savebutton1231" EnableTheming="false" SkinID="skinBtnSave"
                                                                                    OnClick="InsertButton_Click"></asp:Button>
                                                                            </td>
                                                                            <td align="center" style="width: 30%">
                                                                                <asp:Button ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                    CssClass="cancelbutton6" EnableTheming="false" SkinID="skinBtnCancel" OnClick="InsertCancelButton_Click"></asp:Button>
                                                                            </td>
                                                                            <td align="right" style="width: 15%"></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>

                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <table cellspacing="0">
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:ValidationSummary ID="valSumAdd" DisplayMode="BulletList" ShowMessageBox="true"
                                                                    ShowSummary="false" HeaderText="" Font-Names="'Trebuchet MS'"
                                                                    Font-Size="12" runat="server" />
                                                            </td>
                                                            <td colspan="3">
                                                                <asp:ObjectDataSource ID="srcGroupInfoAdd" runat="server" SelectMethod="ListGroupInfoExp"
                                                                    TypeName="BusinessLogic" OldValuesParameterFormatString="original_{0}">
                                                                    <SelectParameters>
                                                                        <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                    </SelectParameters>
                                                                </asp:ObjectDataSource>
                                                            </td>
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
                <tr style="width: 100%;">
                    <td style="width: 100%;">
                        <table width="100%" style="margin: -3px 0px 0px 0px;">
                            <tr style="width: 100%">
                                <td>
                                    <div class="mainGridHold" id="searchGrid">
                                        <asp:GridView ID="GrdViewLedger" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                            OnRowCreated="GrdViewLedger_RowCreated" Width="100.4%" DataSourceID="GridSource"
                                            AllowPaging="True" DataKeyNames="ID" EmptyDataText="No Expense Data Found."
                                            OnRowCommand="GrdViewLedger_RowCommand" OnRowDataBound="GrdViewLedger_RowDataBound" OnRowDeleting="GrdViewLedger_RowDeleting"
                                            OnRowDeleted="GrdViewLedger_RowDeleted" CssClass="someClass">
                                            <HeaderStyle Height="30px" HorizontalAlign="Center" Font-Bold="true" BackColor="#cccccc" BorderColor="Gray" Font-Size="Small" />
                                            <RowStyle Font-Bold="true" HorizontalAlign="Center" Height="30px" Font-Size="Small" ForeColor="#0567AE" />
                                            <Columns>
                                                <asp:BoundField DataField="Expensehead" HeaderText="Expense Name" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="AliasName" HeaderText="Alias Name" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="GroupName" HeaderText="Group Name" HeaderStyle-BorderColor="Gray" />
                                                <%--<asp:BoundField DataField="IsActive" HeaderText="IsActive" HeaderStyle-BorderColor="Gray" />--%>
                                             <%--   <asp:TemplateField HeaderText="Opening Balance" HeaderStyle-BorderColor="Gray">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBalance" runat="server"></asp:Label>
                                                        <asp:HiddenField ID="DRORCR" runat="server" Value='<%# Bind("DRORCR") %>' />
                                                        <asp:HiddenField ID="OpenBalance" runat="server" Value='<%# Bind("OpenBalance") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                               <%-- <asp:BoundField DataField="Phone" HeaderText="Phone No." Visible="false" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="LedgerCategory" HeaderText="Category" Visible="false" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="TinNumber" HeaderText="TIN#" Visible="false" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="Branchcode" HeaderText="Branch Code" HeaderStyle-BorderColor="Gray" />--%>
                                                <asp:TemplateField ItemStyle-CssClass="command" HeaderText="Edit" ItemStyle-Width="50px" HeaderStyle-BorderColor="Gray"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Select" />
                                                        <asp:ImageButton ID="btnEditDisabled" Enabled="false" SkinID="editDisable" runat="Server"></asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="command" HeaderText="Delete" HeaderStyle-Width="50px" HeaderStyle-BorderColor="Gray"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to Delete this Expense?"
                                                            runat="server">
                                                        </cc1:ConfirmButtonExtender>
                                                        <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" CommandName="Delete"></asp:ImageButton>
                                                        <asp:ImageButton ID="lnkBDisabled" Enabled="false" SkinID="deleteDisable" runat="Server"></asp:ImageButton>
                                                        <asp:HiddenField ID="ldgID" runat="server" Value='<%# Bind("ID") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerTemplate>
                                                <table style="border-color: white">
                                                    <tr style="border-color: white">
                                                        <td style="border-color: white">Goto Page
                                                        </td>
                                                        <td style="border-color: white">
                                                            <asp:DropDownList ID="ddlPageSelector" BackColor="#e7e7e7" runat="server" AutoPostBack="true" Width="65px" Height="23px" Style="border: 1px solid blue">
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
                <tr style="width: 100%;">
                    <td style="width: 918px" align="left">
                        <asp:ObjectDataSource ID="GridSource" runat="server" SelectMethod="ListExpenseInfo"
                            TypeName="BusinessLogic" DeleteMethod="DeleteExpense" OnDeleting="GridSource_Deleting">
                            <DeleteParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                <asp:Parameter Name="ID" Type="Int32" />
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
                        <asp:ObjectDataSource ID="frmSource" runat="server" SelectMethod="GetExpenseInfoForId"
                            TypeName="BusinessLogic" OnUpdating="frmSource_Updating" OnInserting="frmSource_Inserting"
                            InsertMethod="InsertExpenseInfo" UpdateMethod="UpdateExpenseInformation">
                            <UpdateParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                <asp:Parameter Name="ID" Type="Int32" />
                                <asp:Parameter Name="Expensehead" Type="String" />
                                <asp:Parameter Name="AliasName" Type="String" />
                                <asp:Parameter Name="GroupID" Type="Int32" />
                                <asp:Parameter Name="Username" Type="String" />
                                 <asp:Parameter Name="IsActive" Type="String" />               
                            </UpdateParameters>
                            <SelectParameters>
                                <asp:ControlParameter ControlID="GrdViewLedger" Name="ID" PropertyName="SelectedValue"
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
                            </InsertParameters>
                        </asp:ObjectDataSource>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%"></td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <table width="100%">
        <tr>
            <td>
                <table style="width: 100%">
                    <tr>
                        <td style="width: 20%">

                        </td>
                        <td style="width: 15%">
                              <asp:Panel ID="pnlSearch" runat="server" Width="100px">
                                                <asp:Button ID="lnkBtnAdd" runat="server" OnClick="lnkBtnAdd_Click" CssClass="ButtonAdd66"
                                                    EnableTheming="false" Width="80px" Text=""></asp:Button>
                                            </asp:Panel>
                        </td>
                        <td style="width: 15%">
                            <asp:Button ID="BlkAdd" runat="server" OnClientClick="window.open('BulkAdditionExpense.aspx','billSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=250,width=500,left=425,top=220, scrollbars=yes');" CssClass="bulkaddition"
                                EnableTheming="false" Text=""></asp:Button>
                        </td>
                        <td style="width: 15%">
                            <asp:Button ID="btnExportToExcel" runat="server" CssClass="exportexl6" OnClientClick="window.open('ReportExcelExpenses.aspx ','billSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=310,width=500,left=425,top=220, scrollbars=yes');"
                                EnableTheming="false"></asp:Button>
                        </td>
                        <td style="width: 35%"></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
