<%@ Page Title="" Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="HirePurchaseNew.aspx.cs" Inherits="HirePurchaseNew"  %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <script src="Scripts/JScriptSales.js" type="text/javascript">

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

    <asp:UpdatePanel ID="UpdatePanel16" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        
            <table style="width: 100%">
                <tr style="width: 100%">
                    <td style="width: 100%">
                    
                            <%--<div class="mainConHd">
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tr valign="middle">
                                        <td>
                                            <span>Service Visits</span>
                                        </td>
                                    </tr>
                                </table>
                            </div>--%>
                            <%--<table class="mainConHd" style="width: 994px;">
                                <tr valign="middle">
                                    <td style="font-size: 20px;">
                                        Opening Stock
                                    </td>
                                </tr>
                            </table>--%>
                            <div class="mainConBody">
                                <table style="width: 100%;margin: -1px 0px 0px 1px;" cellpadding="3" cellspacing="2" class="searchbg">
                                    <tr style="height: 25px; vertical-align: middle">
                                        <td style="width: 2%;"></td>
                                            <td style="width: 20%; font-size: 22px; color: #000000;" >
                                                Hire Purchase
                                            </td>
                                            <td style="width: 16%">
                                            <div style="text-align: right;">
                                                <asp:Panel ID="pnlSearch" runat="server" Width="100px">
                                                    <asp:Button ID="lnkBtnAdd" runat="server" OnClick="lnkBtnAdd_Click" CssClass="ButtonAdd66"
                                                        EnableTheming="false" Width="80px" Text=""></asp:Button>
                                                </asp:Panel>
                                            </div>
                                        </td>
                                        <td style="width: 15%; color: #000000;" align="right">

                                                Search
                                            </td>
                                        <td style="width: 18%" class="Box1">
                                            <asp:TextBox ID="txtSearch" runat="server" SkinID="skinTxtBoxSearch"></asp:TextBox>
                                        </td>
                                        <td style="width: 18%" class="Box1">
                                            <div style="width: 160px; font-family: 'Trebuchet MS';">
                                                <asp:DropDownList ID="ddCriteria" runat="server" Width="154px" BackColor="#BBCAFB" Height="23px" style="text-align:center;border:1px solid #BBCAFB ">
                                                    <asp:ListItem Value="0">All</asp:ListItem>
                                                    <asp:ListItem Value="BillNo">BillNo</asp:ListItem>
                                                    <asp:ListItem Value="BillDate">Bill Date</asp:ListItem>
                                                    <asp:ListItem Value="CustomerName">Customer Name</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </td>
                                        <td style="width: 22%" class="tblLeftNoPad">
                                            <asp:Button ID="btnSearch" runat="server" Text=""  CssClass="ButtonSearch6" EnableTheming="false" OnClick="btnSearch_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        
                        <asp:ValidationSummary ID="VS" runat="server" Font-Names="'Trebuchet MS'" 
                        Font-Size="12pt" HeaderText="Validation Messages" ShowMessageBox="True" 
                        ShowSummary="False" ValidationGroup="salesval" />
                        <input id="dummy" type="button" style="display: none" runat="server" />
                        <input id="Button1" type="button" style="display: none" runat="server" />
                        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                            CancelControlID="Button1" DynamicServicePath="" Enabled="True" PopupControlID="popUp"
                            TargetControlID="dummy">
                        </cc1:ModalPopupExtender>
                        <asp:Panel runat="server" ID="popUp" Style="width: 60%">
                            <div id="contentPopUp">
                                <table cellpadding="2" cellspacing="2" style="border: 1px solid blue;
                                    background-color: #fff; color: #000;" width="100%">
                                    <tr>
                                        <td>
                                            
                                                    <table cellpadding="2" cellspacing="1" style="border: 0px solid blue;"
                                                        width="100%">
                                                        
                                                            <tr>
                                                                <td colspan="5">
                                                                    <table class="headerPopUp" width="100%">
                                                                        <tr>
                                                                            <td>
                                                                                Hire Purchase
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="5">
                                                                    <cc1:TabContainer ID="tabs2" runat="server" Width="100%" ActiveTabIndex="0" CssClass="fancy fancy-green">
                                                                        <cc1:TabPanel ID="tabMaster" runat="server" HeaderText="Hire Purchase Details">
                                                                            <HeaderTemplate>
                                                                                <div>
                                                                                        <table> 
                                                                                            <tr><td> <b> Hire Purchase Details </b></td></tr>
                                                                                        </table>
                                                                                    </div>
                                                                            </HeaderTemplate>
                                                                            <ContentTemplate>
                                                                            <div>
                                                                                <table style="width: 750px; border: 0px solid #86b2d1" align="center" cellpadding="3" cellspacing="2">
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 20%;">
                                                                                            BillNo.
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 24%;">
                                                                                            <asp:Label ID="lblBillNo" runat="server" BackColor="#90C9FC" Height="25px" 
                                                                                                Width="100px"></asp:Label>
                                                                                         </td>
                                                                                         <td class="ControlLabel" style="width: 20%;">
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                                                                                ControlToValidate="txtBillDate" CssClass="lblFont" Display="Dynamic" 
                                                                                                ErrorMessage="BillDate is mandatory" Text="*" ></asp:RequiredFieldValidator>
                                                                                            <asp:RangeValidator ID="mrBillDate" runat="server" 
                                                                                                ControlToValidate="txtBillDate" ErrorMessage="Bill date cannot be future date." 
                                                                                                Text="*" Type="Date" ></asp:RangeValidator>
                                                                                            Date *
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 24%;">
                                                                                            <asp:TextBox ID="txtBillDate" runat="server" AutoPostBack="True" 
                                                                                                BackColor="#90C9FC" CssClass="cssTextBox" Height="23px" MaxLength="10" 
                                                                                                OnTextChanged="txtBillDate_TextChanged" TabIndex="1" ></asp:TextBox>
                                                                                            <cc1:CalendarExtender ID="calBillDate" runat="server" Enabled="True" 
                                                                                                Format="dd/MM/yyyy"
                                                                                                PopupButtonID="btnBillDate" TargetControlID="txtBillDate">
                                                                                            </cc1:CalendarExtender>
                                                                                        </td>
                                                                                        <td align="left" style="width: 5%;">
                                                                                            <asp:ImageButton ID="btnBillDate" runat="server" CausesValidation="False" 
                                                                                                ImageUrl="App_Themes/NewTheme/images/cal.gif" Width="20px" />
                                                                                        </td>
                                                                                 </tr>
                                                                                 <tr>
                                                                                    <td class="ControlLabel" style="width: 25%;">
                                                                                        Name *
                                                                                        <asp:CompareValidator ID="cvCustomer" runat="server" 
                                                                                            ControlToValidate="cmbCustomer" Display="Dynamic" 
                                                                                            ErrorMessage="Please Select Customer!!" Operator="GreaterThan" Text="*" 
                                                                                            ValidationGroup="salesval" ValueToCompare="0"></asp:CompareValidator>
                                                                                    </td>
                                                                                    <td class="ControlDrpBorder" style="width: 24%;">
                                                                                        <asp:UpdatePanel ID="UpdatePanel21" runat="server" UpdateMode="Conditional">
                                                                                            <ContentTemplate>
                                                                                                <asp:DropDownList ID="cmbCustomer" runat="server" AppendDataBoundItems="true" 
                                                                                                    AutoPostBack="true" BackColor="#90c9fc" CssClass="drpDownListMedium" 
                                                                                                    DataTextField="LedgerName" DataValueField="LedgerID" height="26px" 
                                                                                                    OnSelectedIndexChanged="cmbCustomer_SelectedIndexChanged" 
                                                                                                    style="border: 1px solid #90c9fc" TabIndex="2" ValidationGroup="salesval" 
                                                                                                    Width="100%">
                                                                                                    <asp:ListItem style="background-color: #90c9fc" Text="Select Customer" Value="0"></asp:ListItem>
                                                                                                </asp:DropDownList>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                        <asp:UpdatePanel ID="UpdatePanel20" runat="server" UpdateMode="Conditional">
                                                                                            <ContentTemplate>
                                                                                                <asp:TextBox ID="txtOtherCusName" runat="server" BackColor="#90c9fc" 
                                                                                                    height="10px" SkinID="skinTxtBox" TabIndex="2" ValidationGroup="salesval" 
                                                                                                    Visible="False"></asp:TextBox>
                                                                                                <asp:Label ID="lblledgerCategory" runat="server" CssClass="lblFont" Style="color: royalblue; font-weight : normal; font-size: smaller"></asp:Label>
                                                                                                </ContentTemplate>
                                                                                            <Triggers>
                                                                                                <asp:AsyncPostBackTrigger ControlID="cmbCustomer" 
                                                                                                    EventName="SelectedIndexChanged" />
                                                                                            </Triggers>
                                                                                        </asp:UpdatePanel>
                                                                                    </td>
                                                                                    <td class="ControlLabel" style="width: 15%;">
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" 
                                                                                            ControlToValidate="txtDueDate" CssClass="lblFont" Display="Dynamic" 
                                                                                            ErrorMessage="Due Date is mandatory" Text="*" ValidationGroup="salesval"></asp:RequiredFieldValidator>
                                                                                        <%--<asp:RangeValidator ID="mrduedate" runat="server" 
                                                                                            ControlToValidate="txtDueDate" ErrorMessage="Bill date cannot be future date." 
                                                                                            Text="*" Type="Date" ValidationGroup="salesval"></asp:RangeValidator>--%>
                                                                                            Start Due Date *
                                                                                    </td>
                                                                                    <td class="ControlTextBox3" style="width: 24%;">
                                                                                        <asp:TextBox ID="txtduedate" runat="server"
                                                                                            BackColor="#90C9FC" CssClass="cssTextBox" Height="23px" MaxLength="10" 
                                                                                            TabIndex="3" ValidationGroup="salesval"></asp:TextBox>
                                                                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" 
                                                                                            Format="dd/MM/yyyy" 
                                                                                            PopupButtonID="ImageButton1" TargetControlID="txtDueDate">
                                                                                        </cc1:CalendarExtender>
                                                                                    </td>
                                                                                    <td align="left" style="width: 15%;">
                                                                                        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" 
                                                                                            ImageUrl="App_Themes/NewTheme/images/cal.gif" Width="20px" />
                                                                                    </td>
                                                                                </tr>                                                                
                                                                                <tr>
                                                                                    <td class="ControlLabel" style="width: 25%;" valign="middle">
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                                                                                            ControlToValidate="txtpuramt" CssClass="lblFont" Display="Dynamic" 
                                                                                            ErrorMessage="Despatched From is mandatory" Text="*" ValidationGroup="salesval"></asp:RequiredFieldValidator>
                                                                                        Purchase Amount *
                                                                                    </td>
                                                                                    <td class="ControlTextBox3" style="width: 24%;">
                                                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                                                            <ContentTemplate>
                                                                                                <asp:TextBox ID="txtpuramt" runat="server" BackColor="#90C9FC" 
                                                                                                    MaxLength="10" SkinID="skinTxtBoxGrid" TabIndex="4" Width="200px" AutoPostBack="True" OnTextChanged="txtpuramt_TextChanged"></asp:TextBox>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </td>
                                                                                    <td class="ControlLabel" style="width: 15%;">
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                                                                            ControlToValidate="Txtinipay" CssClass="lblFont" Display="Dynamic" 
                                                                                            ErrorMessage="Loan Amount is mandatory" Text="*" ValidationGroup="salesval"></asp:RequiredFieldValidator>
                                                                                        Initial Payment *
                                                                                    </td>
                                                                                    <td class="ControlTextBox3" style="width: 24%">
                                                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                                                            <ContentTemplate>
                                                                                                    <asp:TextBox ID="txtinipay" runat="server" BackColor="#90C9FC" 
                                                                                                    CssClass="cssTextBox" MaxLength="200" SkinID="skinTxtBox" AutoPostBack="True" OnTextChanged="txtinipay_TextChanged" TabIndex="5" 
                                                                                                    Width="500px"></asp:TextBox>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </td>
                                                                                 </tr>
                                                                                 <tr>
                                                                                    <td class="ControlLabel" style="width: 15%;">
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                                                                            ControlToValidate="Txtlnamt" CssClass="lblFont" Display="Dynamic" 
                                                                                            ErrorMessage="Loan Amount is mandatory" Text="*" ValidationGroup="salesval"></asp:RequiredFieldValidator>
                                                                                        Loan Amount *
                                                                                    </td>

                                                                                    <td class="ControlTextBox3" style="width: 24%">
                                                                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                                                                           <ContentTemplate>
                                                                                                <asp:TextBox ID="Txtlnamt" runat="server" BackColor="#90C9FC"
                                                                                                    CssClass="cssTextBox" MaxLength="200" SkinID="skinTxtBox" TabIndex="6" 
                                                                                                    Width="500px"></asp:TextBox>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </td>
                                                                                        <td class="ControlLabel" style="width: 15%;">
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                                                                                            ControlToValidate="txtdocchr" CssClass="lblFont" Display="Dynamic" 
                                                                                            ErrorMessage="Document Charges is mandatory" Text="*" ValidationGroup="salesval"></asp:RequiredFieldValidator>
                                                                                        Document Charges *
                                                                                    </td>
                                                                                                                    
                                                                                    <td class="ControlTextBox3" style="width: 24%">
                                                                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                                                                           <ContentTemplate>
                                                                                                    <asp:TextBox ID="txtdocchr" runat="server" BackColor="#90C9FC" AutoPostBack="True" OnTextChanged="txtdocchr_TextChanged"
                                                                                                        CssClass="cssTextBox" MaxLength="200" SkinID="skinTxtBox" TabIndex="7" 
                                                                                                        Width="500px"></asp:TextBox>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </td>
                                                                                    </tr>
                                                                                        <tr>
                                                                                    <td class="ControlLabel" style="width: 15%;">
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                                                                                            ControlToValidate="txtintamt" CssClass="lblFont" Display="Dynamic" 
                                                                                            ErrorMessage="Interest Amount is mandatory" Text="*" ValidationGroup="salesval"></asp:RequiredFieldValidator>
                                                                                        Interest Amount *
                                                                                    </td>

                                                                                    <td class="ControlTextBox3" style="width: 24%">
                                                                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                                                                           <ContentTemplate>
                                                                                                <asp:TextBox ID="txtintamt" runat="server" BackColor="#90C9FC" AutoPostBack="True" OnTextChanged="txtintamt_TextChanged"
                                                                                                    CssClass="cssTextBox" MaxLength="200" SkinID="skinTxtBox" TabIndex="8" 
                                                                                                    Width="500px"></asp:TextBox>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </td>
                                                                                    <td class="ControlLabel" style="width: 15%;">
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                                                                            ControlToValidate="txtfinpay" CssClass="lblFont" Display="Dynamic" 
                                                                                            ErrorMessage="Final Payment is mandatory" Text="*" ValidationGroup="salesval"></asp:RequiredFieldValidator>
                                                                                        Final Payment *
                                                                                    </td>

                                                                                    <td class="ControlTextBox3" style="width: 24%">
                                                                                        <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                                                                                           <ContentTemplate>
                                                                                                <asp:TextBox ID="txtfinpay" runat="server" BackColor="#90C9FC"  
                                                                                                CssClass="cssTextBox" MaxLength="200" SkinID="skinTxtBox" TabIndex="9" 
                                                                                                Width="500px"></asp:TextBox>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="ControlLabel" style="width: 15%;">
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                                                            ControlToValidate="txtnoinst1" CssClass="lblFont" Display="Dynamic" 
                                                                                            ErrorMessage="no of installment is mandatory" Text="*" ValidationGroup="salesval"></asp:RequiredFieldValidator>
                                                                                        No Of Installment *
                                                                                    </td>

                                                                                    <td class="ControlTextBox3" style="width: 24%">
                                                                                        <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                                                                           <ContentTemplate>
                                                                                                <asp:TextBox ID="txtnoinst1" runat="server" BackColor="#90C9FC" AutoPostBack="True" OnTextChanged="txtnoinst1_TextChanged" 
                                                                                                CssClass="cssTextBox" MaxLength="200" SkinID="skinTxtBox" TabIndex="10" 
                                                                                                    Width="500px"></asp:TextBox>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </td>
                                                                                                                   
                                                                                    <td class="ControlLabel" style="width: 15%;">
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" 
                                                                                            ControlToValidate="txteach" CssClass="lblFont" Display="Dynamic" 
                                                                                            ErrorMessage="Each Month Payment is mandatory" Text="*" ValidationGroup="salesval"></asp:RequiredFieldValidator>
                                                                                        Each Month Payment *
                                                                                    </td>

                                                                                    <td class="ControlTextBox3" style="width: 24%">
                                                                                        <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                                                                                            <ContentTemplate>
                                                                                                <asp:TextBox ID="Txteach" runat="server" BackColor="#90C9FC" 
                                                                                                CssClass="cssTextBox" MaxLength="200" SkinID="skinTxtBox" TabIndex="11" 
                                                                                                Width="500px"></asp:TextBox>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                   </td>
                                                                               </tr>
                                                                               <tr>
                                                                                    <td class="ControlLabel" style="width: 15%;">
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" 
                                                                                            ControlToValidate="txtdatepay" CssClass="lblFont" Display="Dynamic" 
                                                                                            ErrorMessage="no of installment is mandatory" Text="*" ValidationGroup="salesval"></asp:RequiredFieldValidator>
                                                                                        Date Of Payment *
                                                                                    </td>
                                                                                                                    
                                                                                    <td class="ControlTextBox3" style="width: 24%">
                                                                                        <asp:TextBox ID="txtdatepay" runat="server" BackColor="#90C9FC" 
                                                                                            CssClass="cssTextBox" MaxLength="200" SkinID="skinTxtBox" TabIndex="12" 
                                                                                            Width="500px"></asp:TextBox>
                                                                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" 
                                                                                                Format="dd/MM/yyyy" 
                                                                                                PopupButtonID="ImageButton2" TargetControlID="txtdatepay">
                                                                                        </cc1:CalendarExtender>
                                                                                    </td>
                                                                                    <td class="ControlLabel" style="width: 25%;">
                                                                                        <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" 
                                                                                                    ImageUrl="App_Themes/NewTheme/images/cal.gif" Width="20px"  align="left"/>

                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" 
                                                                                            ControlToValidate="txtothers" CssClass="lblFont" Display="Dynamic" 
                                                                                            ErrorMessage="Each Month Payment is mandatory" Text="*" ValidationGroup="salesval"></asp:RequiredFieldValidator>
                                                                                        <asp:Label ID="Label1" runat="server" align="right" 
                                                                                                Width="100px" Text="Others"></asp:Label>
                                                                                    </td>
                                                                                        <td class="ControlTextBox3" style="width: 24%">
                                                                                        <asp:TextBox ID="txtothers" runat="server" BackColor="#90C9FC" 
                                                                                            CssClass="cssTextBox" MaxLength="200" SkinID="skinTxtBox" TabIndex="13" 
                                                                                            Width="500px"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="4">
                                                                                    
                                                                                    </td>
                                                                                </tr>
                                                                                <tr style="height:8px">
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </cc1:TabPanel>
                                                            </cc1:TabContainer>
                                                        </td>
                                                     </tr>
                                                     
                                                     <tr>
                                                        <td align="center" style="width: 100%" colspan="5">
                                                            <table style="width:100%">
                                                                <tr>
                                                                    <td style="width:30%">
                                                                    </td>
                                                                    <td align="center" style="width:18%">
                                                                        <asp:Button ID="CancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                            CssClass="cancelbutton6" EnableTheming="false" SkinID="skinBtnCancel" OnClick="UpdateCancelButton_Click">
                                                                        </asp:Button>
                                                                    </td>
                                                                    <td align="center" style="width:18%">
                                                                        <asp:Button ID="SaveButton" runat="server" CommandName="Update" SkinID="skinBtnSave" ValidationGroup="salesval"
                                                                            CssClass="savebutton1231" EnableTheming="false" OnClick="SaveButton_Click"></asp:Button>
                                                                        <asp:Button ID="UpdateButton" runat="server" CommandName="Update" SkinID="skinBtnSave" ValidationGroup="salesval"
                                                                            CssClass="Updatebutton1231" EnableTheming="false" OnClick="UpdateButton_Click"></asp:Button>
                                                                    </td>
                                                                    <td style="width:30%">
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                            
                                                    </table>
                                                
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                    </td>
                </tr>
                <tr style="width: 100%">
                    <td style="width: 100%;">
                    <table width="100%" style="margin: -3px 0px 0px 0px;">
                                <tr style="width: 100%">
                                    <td>
                        <asp:HiddenField ID="hdVisitID" runat="server" Value="0" />
                        <div class="mainGridHold" id="searchGrid">
                            <asp:GridView ID="GrdViewSerVisit" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                OnRowCreated="GrdViewSerVisit_RowCreated" Width="99.9%" DataSourceID="GridSource" CssClass="someClass"
                                AllowPaging="True" DataKeyNames="slno" EmptyDataText="No Hire Purchase found!"
                                OnRowCommand="GrdViewSerVisit_RowCommand" OnRowDataBound="GrdViewSerVisit_RowDataBound"
                                OnSelectedIndexChanged="GrdViewSerVisit_SelectedIndexChanged" OnRowDeleting="GrdViewSerVisit_RowDeleting"
                                OnRowDeleted="GrdViewSerVisit_RowDeleted">
                                <EmptyDataRowStyle CssClass="GrdContent" />
                                <Columns>
                                    <asp:BoundField DataField="SlNo" HeaderText="BillNo"  HeaderStyle-BorderColor="Gray"/>
                                    <asp:BoundField DataField="BillDate" HeaderText="BillDate"  HeaderStyle-BorderColor="Gray"/>
                                    <asp:BoundField DataField="CustomerName" HeaderText="CustomerName"  HeaderStyle-BorderColor="Gray"/>
                                    <asp:BoundField DataField="puramt" HeaderText="Purchase Amount"  HeaderStyle-BorderColor="Gray"/>
                                    <asp:BoundField DataField="noinst" HeaderText="No of Inst"  HeaderStyle-BorderColor="Gray"/>
                                    <asp:BoundField DataField="paydate" HeaderText="Payment Date"  HeaderStyle-BorderColor="Gray"/>
                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="50px" HeaderText="Edit" HeaderStyle-BorderColor="Gray">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" SkinID="edit"
                                                CommandName="Select" />
                                            <asp:ImageButton ID="btnEditDisabled" Enabled="false" SkinID="editDisable" runat="Server">
                                            </asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderText="Delete" HeaderStyle-Width="50px" HeaderStyle-BorderColor="Gray"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to Delete this Hire Purchase Details?"
                                                runat="server">
                                            </cc1:ConfirmButtonExtender>
                                            <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" CommandName="Delete"></asp:ImageButton>
                                            <asp:ImageButton ID="lnkBDisabled" Enabled="false" SkinID="deleteDisable" runat="Server">
                                            </asp:ImageButton>
                                            <asp:HiddenField ID="ldgID" runat="server" Value='<%# Bind("SlNo") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataRowStyle CssClass="GrdContent" />
                                <PagerTemplate>
                                    <table style=" border-color:white">
                                        <tr style=" border-color:white">
                                            <td style=" border-color:white">
                                                Goto Page
                                            </td>
                                            <td style=" border-color:white">
                                                <asp:DropDownList ID="ddlPageSelector" runat="server" Width="65px" style="border:1px solid blue">
                                                </asp:DropDownList>
                                            </td>
                                            <td style=" border-color:white;Width:5px">
                                            
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
                        </td>
                        </tr>
                        </table>
                    </td>
                </tr>
                <tr style="width:100%">
                    <td style="width: 918px" align="left">
                        <asp:ObjectDataSource ID="GridSource" runat="server" SelectMethod="GetHireList"
                            TypeName="BusinessLogic" DeleteMethod="DeleteHirePurchase" OnDeleting="GridSource_Deleting">
                            <DeleteParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                <asp:Parameter Name="Slno" Type="Int32" />
                                <asp:Parameter Name="usernam" Type="String" />
                            </DeleteParameters>
                        </asp:ObjectDataSource>
                        <asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="true" ShowSummary="false" />
                        <asp:HiddenField ID="hdDataSource" runat="server" />
                        <asp:HiddenField ID="hdServiceID" runat="server" Value="" />
                        <asp:HiddenField ID="hdCustomerID" runat="server" Value="0" />
                        <asp:HiddenField ID="hdDueDate" runat="server" Value="" />
                        <asp:HiddenField ID="hdRefNumber" runat="server" Value="" />
                    </td>
                </tr>
                <tr align="center">
                    <td >
                        <asp:Button ID="btnExportToExcel" runat="server" CssClass="exportexl6" OnClientClick="window.open('ReportExcelHirePurchase.aspx','billSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=310,width=500,left=425,top=220, scrollbars=yes');"
                                                EnableTheming="false"></asp:Button>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
