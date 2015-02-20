<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="CommissionMngmt.aspx.cs" Inherits="CommissionMngmt"
    Title="Customer > Commission Management" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
  
    <script language="javascript" type="text/javascript">
//        function pageLoad() {
//            
//            //  get the behavior associated with the tab control
//            var tabContainer = $find('ctl00_cplhControlPanel_tabs2');

//            

//            //if($('#ctl00_cplhControlPanel_tabs2_tabMaster_cmdCancelProduct') != null)
//            //$addHandler($('#ctl00_cplhControlPanel_tabs2_tabMaster_cmdCancelProduct'), "onclick", OnProductCancel);
//            //var popup = $find('ctl00_cplhControlPanel_tabs2_tabMaster_ModalPopupExtender1'); 
//            //popup.add_shown(SetzIndex); 

//            if (tabContainer != null) {
//                //  get all of the tabs from the container
//                var tabs = tabContainer.get_tabs();

//                //  loop through each of the tabs and attach a handler to
//                //  the tab header's mouseover event
//                for (var i = 0; i < tabs.length; i++) {
//                    var tab = tabs[i];

//                    $addHandler(
//                tab.get_headerTab(),
//                'mouseover',
//                Function.createDelegate(tab, function () {
//                    tabContainer.set_activeTab(this);
//                }
//            ));
//                }
//            }
//        }

//        function PrintItem(ID) {
//            window.showModalDialog('./PrintProductCommission.aspx?Req=N&SID=' + ID, self, 'dialogWidth:800px;dialogHeight:530px;status:no;dialogHide:yes;unadorned:no;');
//        }
        function PrintItem(ID) {

            window.showModalDialog('./ProductCommPurchaseBill.aspx?Req=N&RT=NO&SID=' + ID, self, 'dialogWidth:800px;dialogHeight:530px;status:no;dialogHide:yes;unadorned:yes;');

        }

    </script>

    <style id="Style1" runat="server">
         .someClass td
        {
            font-size: 12px;
            border : 1px solid Gray ;
        }
    </style>

    <asp:UpdatePanel ID="UpdatePnlMaster" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            
            <table style="width: 100%;" align="center" >
                <tr style="width: 100%">
                    <td style="width:100%" valign="middle">
                            <%--<div class="mainConHd">
                               <span>Sales</span>
                            </div>--%>
                            <%--<table class="mainConHd" style="width: 994px;">
                                <tr valign="middle">
                                    <td style="font-size: 20px;">
                                        Commission Management
                                    </td>
                                </tr>
                            </table>--%>
                            <div class="mainConBody">
                                <div>
                                    <table cellspacing="2px" cellpadding="3px" border="0" width="99.8%" style="margin: -1px 0px 0px 1px;"
                                        class="searchbg">
                                        <tr style="vertical-align: middle">
                                           <td style="width: 2%">
                                            </td>
                                            <td style="width: 45%; font-size: 22px; color: white;" >
                                                Commission Management
                                            </td>
                                            <td style="width: 15%">
                                                <div style="text-align: right;">
                                                    <asp:Panel ID="pnlSearch" runat="server" Width="100px">
                                                        <asp:Button ID="lnkBtnAdd" runat="server" OnClick="lnkBtnAdd_Click" CssClass="ButtonAdd66"
                                                            EnableTheming="false" Width="80px" Text=""></asp:Button>
                                                    </asp:Panel>
                                                </div>
                                            </td>
                                            <td style="width: 5%" align="center">
                                                
                                            </td>
                                            <td style="width: 20%; color: white;" align="right">
                                                Commission No.
                                            </td>
                                            <td style="width: 20%; text-align: center" class="NewBox">
                                                <asp:TextBox ValidationGroup="search" ID="txtBillnoSrc" runat="server" CssClass="cssTextBox"
                                                    Width="92%" MaxLength="8"></asp:TextBox>
                                            </td>
                                            <td style="width: 1%" align="center">
                                                
                                            </td>
                                            <td style="width: 19%">
                                                <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" Text=""
                                                    CssClass="ButtonSearch6" EnableTheming="false" ForeColor="White" />
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender221" runat="server" TargetControlID="txtBillnoSrc"
                                                    FilterType="Numbers" />
                                            </td>
                                            <td style="width: 5%; text-align: left">
                                                
                                                    <asp:TextBox ValidationGroup="search" ID="txtTransNo" runat="server" MaxLength="8"
                                                    CssClass="cssTextBox" Width="92%" Visible="False"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtTransNo"
                                                    FilterType="Numbers" />
                                            </td>
                                             <td style="width: 16%" class="tblLeftNoPad">
                                        <asp:Button ID="Button1" runat="server" OnClick="BtnClearFilter_Click" EnableTheming="false" Text="" CssClass="ClearFilter6" />
                                    </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        
                        
                        <asp:ValidationSummary ID="valSum" DisplayMode="BulletList" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="product" HeaderText="Validation Messages"
                            Font-Names="'Trebuchet MS'" Font-Size="12" runat="server" />
                        <input id="dummySales" type="button" style="display: none" runat="server" />
                        <input id="BtnPopUpCancel" type="button" style="display: none" runat="server" />
                        <cc1:ModalPopupExtender ID="ModalPopupSales" runat="server" BackgroundCssClass="modalBackground"
                            RepositionMode="RepositionOnWindowResizeAndScroll" CancelControlID="BtnPopUpCancel"
                            DynamicServicePath="" Enabled="True" PopupControlID="pnlSalesForm" TargetControlID="dummySales">
                        </cc1:ModalPopupExtender>
                        <asp:Panel ID="pnlSalesForm" runat="server" Style="width: 61%; display: none">
                            <asp:UpdatePanel ID="updatePnlSales" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div id="Div1" class="divArea">
                                        <table style="width: 100%;" align="center">
                                            <tr style="width: 100%">
                                                <td style="width: 100%">
                                                    <table style="text-align: left; border:1px solid Gray" width="100%" cellpadding="3" cellspacing="5">
                                                        <tr>
                                                            <td>
                                                                <table class="headerPopUp" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            Commission Details
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="5">
                                                                
                                                                            <table cellpadding="1" cellspacing="1" width="100%">
                                                                                <tr>
                                                                                    <td>
                                                                                        <table cellpadding="1" cellspacing="1" width="730px">
                                                                                            <tr>
                                                                                                <td colspan="5">
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="ControlLabel" style="width: 25%;">
                                                                                                    Bill No.
                                                                                                </td>
                                                                                                <td class="ControlTextBox3" style="width: 24%;">
                                                                                                    <asp:Label ID="lblBillNo" runat="server" height="25px" BackColor = "#e7e7e7" 
                                                                                                        Width="100px" ></asp:Label>
                                                                                                    <asp:DropDownList ID="ddSeriesType" runat="server" AppendDataBoundItems="True" 
                                                                                                        SkinID="skinDdlBox"  BackColor = "#e7e7e7" height="25px"
                                                                                                        Width="100%" TabIndex="0">
                                                                                                    </asp:DropDownList>
                                                                                                </td>
                                                                                                <td class="ControlLabel" style="width: 15%;">
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtBillDate"
                                                                                                        CssClass="lblFont" Display="Dynamic" ErrorMessage="BillDate is mandatory" Text="*"
                                                                                                        ValidationGroup="salesval"></asp:RequiredFieldValidator>
                                                                                                    <asp:RangeValidator ID="mrBillDate" runat="server" ControlToValidate="txtBillDate"
                                                                                                        ErrorMessage="Bill date cannot be future date." ValidationGroup="salesval" Text="*"
                                                                                                        Type="Date"></asp:RangeValidator>
                                                                                                     Date *
                                                                                                </td>
                                                                                                <td  class="ControlTextBox3" style="width: 24%;">
                                                                                                    <asp:TextBox ID="txtBillDate" runat="server"  AutoPostBack="True"  
                                                                                                                    BackColor = "#e7e7e7" height="23px"
                                                                                                                    OnTextChanged="txtBillDate_TextChanged" MaxLength="10" TabIndex="1" ValidationGroup="salesval"
                                                                                                                    CssClass="cssTextBox"></asp:TextBox>
                                                                                                    <cc1:CalendarExtender ID="calBillDate" runat="server" Format="dd/MM/yyyy"
                                                                                                                    PopupButtonID="btnBillDate" TargetControlID="txtBillDate" Enabled="True">
                                                                                                    </cc1:CalendarExtender>
                                                                                                              
                                                                                                </td>
                                                                                                <td align="left" style="width: 15%;" >
                                                                                                    <asp:ImageButton ID="btnBillDate" ImageUrl="App_Themes/NewTheme/images/cal.gif"
                                                                                                                            CausesValidation="False" Width="20px" runat="server" />   
                                                                                                </td>
                                                                                                
                                                                                            </tr>
                                                                                            <tr style="height:2px">
                                                                                                                            </tr>
                                                                                            <tr>
                                                                                            <td class="ControlLabel" style="width: 25%;">
                                                                                                Supplier Name *
                                                                                                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="dpsupplier"
                                                                                                    Display="Dynamic" ErrorMessage="Please Select Supplier!!" Operator="GreaterThan"
                                                                                                    Text="*" ValidationGroup="salesval" ValueToCompare="0"></asp:CompareValidator>
                                                                                            </td>
                                                                                            <td  style="width: 24%;" class="ControlDrpBorder">
                                                                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                                                                                    <ContentTemplate>
                                                                                                        <asp:DropDownList ID="dpsupplier" runat="server" AppendDataBoundItems="true" AutoPostBack="true" DataTextField="LedgerName" CssClass="drpDownListMedium" style="border: 1px solid #e7e7e7" height="26px" BackColor = "#e7e7e7" DataValueField="LedgerID" OnSelectedIndexChanged="dpsupplier_SelectedIndexChanged"
                                                                                                            Width="100%" TabIndex="2" ValidationGroup="salesval">
                                                                                                            <asp:ListItem style="background-color: #e7e7e7" Text="Select Customer" Value="0"></asp:ListItem>
                                                                                                        </asp:DropDownList>
                                                                                                    </ContentTemplate>
                                                                                                    <Triggers>
                                                                                                        <asp:AsyncPostBackTrigger ControlID="tabs2$TabPanel1$drpPurchaseReturn" EventName="SelectedIndexChanged" />
                                                                                                    </Triggers>
                                                                                                </asp:UpdatePanel>
                                                                                            </td>
                                                                                            <td class="ControlLabel" style="width: 15%;">
                                                                                                Supplier PayMode
                                                                                            </td>
                                                                                            <td style="width: 24%;" class="ControlDrpBorder">
                                                                                                <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                                                                                                    <ContentTemplate >
                                                                                                        <asp:DropDownList ID="drpSuppPaymode" runat="server" AppendDataBoundItems="True" style="text-align:center; border:1px solid #e7e7e7" height="26px" BackColor = "#e7e7e7" AutoPostBack="True" Width="100%" CssClass="drpDownListMedium"
                                                                                                            OnSelectedIndexChanged="drpSuppPaymode_SelectedIndexChanged"
                                                                                                            TabIndex="3" ValidationGroup="salesval">
                                                                                                            <asp:ListItem Text="Cash" Value="1"></asp:ListItem>
                                                                                                            <asp:ListItem Text="Bank / Credit Card" Value="2"></asp:ListItem>
                                                                                                            <asp:ListItem Text="Credit" Value="3"></asp:ListItem>
                                                                                                        </asp:DropDownList>
                                                                                                    </ContentTemplate>
                                                                                                </asp:UpdatePanel>       
                                                                                            </td>
                                                                                            <td style="width: 15%;">
                                                                                            </td>
                                                                                        </tr>
                                                                                            
                                                                                        <tr>
                                                                                            <td colspan="5">
                                                                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                                                                    <ContentTemplate>
                                                                                                
                                                                                                        <asp:Panel ID="Pnlsuppbank" runat="Server" Visible="false">
                                                                                                            <table cellpadding="1" cellspacing="1" width="100%">
                                                                                                                <tr style="height:2px">
                                                                                                                            </tr>
                                                                                                                <tr>
                                                                                                                    <td class="ControlLabel" style="width: 25%;">
                                                                                                                        <asp:RequiredFieldValidator ID="rvsuppcredit" runat="server" ControlToValidate="txtsuppcard"
                                                                                                                            Enabled="false" ErrorMessage="Cheque\Card number is mandatory" Text="*" ValidationGroup="salesval" />
                                                                                                                        Cheque / Credit Card No.*
                                                                                                                    </td>
                                                                                                                    <td class="ControlTextBox3" style="width: 24%;">
                                                                                                                        <asp:TextBox ID="txtsuppcard" runat="server" Height="20px" CssClass="cssTextBox" 
                                                                                                                            Width="100px" TabIndex="4" ValidationGroup="salesval"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                    <td class="ControlLabel" style="width: 15.4%;">
                                                                                                                        <asp:RequiredFieldValidator ID="rvsuppbank" runat="server" ControlToValidate="dpbank1"
                                                                                                                            Enabled="false" ErrorMessage="Bankname is mandatory" InitialValue="0" Text="*"
                                                                                                                            ValidationGroup="salesval" />
                                                                                                                        Bank Name*
                                                                                                                    </td>
                                                                                                                    <td class="ControlDrpBorder" style="width: 24%;">
                                                                                                                        <asp:DropDownList ID="dpbank1" runat="server" AppendDataBoundItems="true" DataTextField="LedgerName" Width="100%" style="border: 1px solid #e7e7e7" height="26px" CssClass="drpDownListMedium" BackColor = "#e7e7e7" 
                                                                                                                            DataValueField="LedgerID" TabIndex="5" ValidationGroup="salesval">
                                                                                                                            <asp:ListItem style="background-color: #e7e7e7" Text="Select Bank" Value="0"></asp:ListItem>
                                                                                                                        </asp:DropDownList>
                                                                                                                    </td>
                                                                                                                    <td style="width: 15%;">
                                                                                                                    </td>
                                                                                                            
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="Numbers"
                                                                                                                            TargetControlID="txtsuppcard" />
                                                                                                        </asp:Panel>
                                                                                                    </ContentTemplate>
                                                                                                    <Triggers>
                                                                                                        <asp:AsyncPostBackTrigger ControlID="drpSuppPaymode" EventName="SelectedIndexChanged" />
                                                                                                    </Triggers>
                                                                                                </asp:UpdatePanel>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr style="height:2px">
                                                                                                                            </tr>
                                                                                <tr>
                                                                                    <td style="width: 25%" class="ControlLabel">
                                                                                        Freight
                                                                                    </td>
                                                                                    <td style="width: 24%" class="ControlTextBox3">
                                                                                        <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
                                                                                                    <ContentTemplate >
                                                                                        <asp:TextBox ID="txtFreight" SkinID="skinTxtBox" TabIndex="10" runat="server" ValidationGroup="salesval"  BackColor = "#e7e7e7" width="200px"
                                                                                            Text="0" AutoPostBack="True" OnTextChanged="txtFreight_TextChanged"></asp:TextBox>
                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txtFreight"
                                                                                            FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                                                                            </ContentTemplate>
                                                                                                </asp:UpdatePanel> 
                                                                                    </td>
                                                                                    <td class="ControlLabel" style="width: 15%;">
                                                                                                Freight PayMode
                                                                                            </td>
                                                                                            <td style="width: 24%;" class="ControlDrpBorder">
                                                                                                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                                                                                    <ContentTemplate >
                                                                                                        <asp:DropDownList ID="dpfreightpaymode" runat="server" AppendDataBoundItems="True" style="text-align:center; border:1px solid #e7e7e7" height="26px" BackColor = "#e7e7e7" AutoPostBack="True" Width="100%" CssClass="drpDownListMedium"
                                                                                                            OnSelectedIndexChanged="dpfreightpaymode_SelectedIndexChanged"
                                                                                                            TabIndex="11" ValidationGroup="salesval">
                                                                                                            <asp:ListItem Text="Cash" Value="1"></asp:ListItem>
                                                                                                            <asp:ListItem Text="Bank / Credit Card" Value="2"></asp:ListItem>
                                                                                                        </asp:DropDownList>
                                                                                                    </ContentTemplate>
                                                                                                </asp:UpdatePanel>       
                                                                                            </td>
                                                                                    
                                                                                    <td style="width: 15%">
                                                                                        
                                                                                    </td>
                                                                                </tr>
                                                                               
                                                                                <tr>
                                                                                            <td colspan="5">
                                                                                                <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                                                                                    <ContentTemplate>
                                                                                                
                                                                                                        <asp:Panel ID="Panel4" runat="Server" Visible="false">
                                                                                                            <table cellpadding="1" cellspacing="1" width="100%">
                                                                                                                <tr style="height:2px">
                                                                                                                            </tr>
                                                                                                                <tr>
                                                                                                                    <td class="ControlLabel" style="width: 25%;">
                                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtfreightcardno"
                                                                                                                            Enabled="false" ErrorMessage="Cheque\Card number is mandatory" Text="*" ValidationGroup="salesval" />
                                                                                                                        Cheque / Credit Card No.*
                                                                                                                    </td>
                                                                                                                    <td class="ControlTextBox3" style="width: 24%;">
                                                                                                                        <asp:TextBox ID="txtfreightcardno" runat="server" Height="20px" CssClass="cssTextBox" 
                                                                                                                            Width="100px" TabIndex="12" ValidationGroup="salesval"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                    <td class="ControlLabel" style="width: 15.4%;">
                                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="dpfreightbank"
                                                                                                                            Enabled="false" ErrorMessage="Bankname is mandatory" InitialValue="0" Text="*"
                                                                                                                            ValidationGroup="salesval" />
                                                                                                                        Bank Name*
                                                                                                                    </td>
                                                                                                                    <td class="ControlDrpBorder" style="width: 24%;">
                                                                                                                        <asp:DropDownList ID="dpfreightbank" runat="server" AppendDataBoundItems="true" DataTextField="LedgerName" Width="100%" style="border: 1px solid #e7e7e7" height="26px" CssClass="drpDownListMedium" BackColor = "#e7e7e7" 
                                                                                                                            DataValueField="LedgerID" TabIndex="13" ValidationGroup="salesval">
                                                                                                                            <asp:ListItem style="background-color: #e7e7e7" Text="Select Bank" Value="0"></asp:ListItem>
                                                                                                                        </asp:DropDownList>
                                                                                                                    </td>
                                                                                                                    <td style="width: 15%;">
                                                                                                                    </td>
                                                                                                            
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers"
                                                                                                                            TargetControlID="txtfreightcardno" />
                                                                                                        </asp:Panel>
                                                                                                    </ContentTemplate>
                                                                                                    <Triggers>
                                                                                                        <asp:AsyncPostBackTrigger ControlID="dpfreightpaymode" EventName="SelectedIndexChanged" />
                                                                                                    </Triggers>
                                                                                                </asp:UpdatePanel>
                                                                                            </td>
                                                                                        </tr>
                                                                                            <tr style="height:2px">
                                                                                                                            </tr>
                                                                                <tr>
                                                                                    <td style="width: 25%" class="ControlLabel">
                                                                                        Loading / Unloading
                                                                                    </td>
                                                                                    <td style="width: 24%" class="ControlTextBox3">
                                                                                    <asp:UpdatePanel ID="UpdatePanel12" runat="server" UpdateMode="Conditional">
                                                                                                    <ContentTemplate >
                                                                                            <asp:TextBox ID="txtLU" SkinID="skinTxtBox" TabIndex="14" runat="server" ValidationGroup="salesval"  BackColor = "#e7e7e7" width="200px"
                                                                                                Text="0" AutoPostBack="True" OnTextChanged="txtLU_TextChanged"></asp:TextBox>
                                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="txtLU"
                                                                                                FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                                                                     </ContentTemplate>
                                                                                                </asp:UpdatePanel> 
                                                                                    </td>
                                                                                    <td class="ControlLabel" style="width: 15%;">
                                                                                                Loading / Unloading PayMode
                                                                                            </td>
                                                                                            <td style="width: 24%;" class="ControlDrpBorder">
                                                                                                <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                                                                                    <ContentTemplate >
                                                                                                        <asp:DropDownList ID="dplupaymode" runat="server" AppendDataBoundItems="True" style="text-align:center; border:1px solid #e7e7e7" height="26px" BackColor = "#e7e7e7" AutoPostBack="True" Width="100%" CssClass="drpDownListMedium"
                                                                                                            OnSelectedIndexChanged="dplupaymode_SelectedIndexChanged"
                                                                                                            TabIndex="15" ValidationGroup="salesval">
                                                                                                            <asp:ListItem Text="Cash" Value="1"></asp:ListItem>
                                                                                                            <asp:ListItem Text="Bank / Credit Card" Value="2"></asp:ListItem>
                                                                                                        </asp:DropDownList>
                                                                                                    </ContentTemplate>
                                                                                                </asp:UpdatePanel>       
                                                                                            </td>
                                                                                    
                                                                                    <td style="width: 15%">
                                                                                        
                                                                                    </td>
                                                                                </tr>
                                                                              
                                                                                <tr>
                                                                                            <td colspan="5">
                                                                                                <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                                                                                                    <ContentTemplate>
                                                                                                
                                                                                                        <asp:Panel ID="Panel5" runat="Server" Visible="false">
                                                                                                            <table cellpadding="1" cellspacing="1" width="100%">
                                                                                                                <tr style="height:2px">
                                                                                                                            </tr>
                                                                                                                <tr>
                                                                                                                    <td class="ControlLabel" style="width: 25%;">
                                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtlucardno"
                                                                                                                            Enabled="false" ErrorMessage="Cheque\Card number is mandatory" Text="*" ValidationGroup="salesval" />
                                                                                                                        Cheque / Credit Card No.*
                                                                                                                    </td>
                                                                                                                    <td class="ControlTextBox3" style="width: 24%;">
                                                                                                                        <asp:TextBox ID="txtlucardno" runat="server" Height="20px" CssClass="cssTextBox" 
                                                                                                                            Width="100px" TabIndex="16" ValidationGroup="salesval"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                    <td class="ControlLabel" style="width: 15.4%;">
                                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="dplubank"
                                                                                                                            Enabled="false" ErrorMessage="Bankname is mandatory" InitialValue="0" Text="*"
                                                                                                                            ValidationGroup="salesval" />
                                                                                                                        Bank Name*
                                                                                                                    </td>
                                                                                                                    <td class="ControlDrpBorder" style="width: 24%;">
                                                                                                                        <asp:DropDownList ID="dplubank" runat="server" AppendDataBoundItems="true" DataTextField="LedgerName" Width="100%" style="border: 1px solid #e7e7e7" height="26px" CssClass="drpDownListMedium" BackColor = "#e7e7e7" 
                                                                                                                            DataValueField="LedgerID" TabIndex="17" ValidationGroup="salesval">
                                                                                                                            <asp:ListItem style="background-color: #e7e7e7" Text="Select Bank" Value="0"></asp:ListItem>
                                                                                                                        </asp:DropDownList>
                                                                                                                    </td>
                                                                                                                    <td style="width: 15%;">
                                                                                                                    </td>
                                                                                                            
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers"
                                                                                                                            TargetControlID="txtlucardno" />
                                                                                                        </asp:Panel>
                                                                                                    </ContentTemplate>
                                                                                                    <Triggers>
                                                                                                        <asp:AsyncPostBackTrigger ControlID="dplupaymode" EventName="SelectedIndexChanged" />
                                                                                                    </Triggers>
                                                                                                </asp:UpdatePanel>
                                                                                            </td>
                                                                                        </tr>
                                                                                            <tr style="height:2px">
                                                                                                                            </tr>
                                                                                <tr>
                                                                                    <td style="width: 25%" class="ControlLabel">
                                                                                       
                                                                                    Commission value *
                                                                                    </td>
                                                                                    <td style="width: 24%" class="ControlTextBox3">
                                                                                        <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional" >
                                                                                        <ContentTemplate>
                                                                                            
                                                                                            <asp:TextBox ID="txtcommissionvalue" runat="server" AutoPostBack="True" TabIndex="18" OnTextChanged="txtcommissionvalue_TextChanged" Width="70px" Height="23px" style="border:1px solid #e7e7e7" BackColor = "#e7e7e7" 
                                                                                            ></asp:TextBox>
                                                                                            <asp:Label ID="txtcommissionval" runat="server" Width="50px" Visible="False"></asp:Label>
                                                                                            <asp:CheckBox ID="chkboxAll" runat="server" Text="A" Visible="false">
                                                                        </asp:CheckBox>
                                                                                        </ContentTemplate>
                                                                                        <Triggers>
                                                                                                        <asp:AsyncPostBackTrigger ControlID="txtcommissionvalue" EventName="TextChanged" />
                                                                                                    </Triggers>
                                                                                        </asp:UpdatePanel>
                                                                                        
                                                                                    </td>
                                                                                    <td style="width: 15%" class="ControlLabel">
                                                                                        
                                                                                                    
                                                                                        Remarks
                                                                                    </td>
                                                                                    <td class="ControlTextBox3" style="width: 24%">
                                                                                        <asp:TextBox ID="txtremarks" runat="server" CssClass="cssTextBox" TabIndex="19" Width="70px" Height="23px" BackColor = "#e7e7e7" 
                                                                                            ></asp:TextBox>
                                                                                       
                                                                                    </td>
                                                                                    <td style="width:15%;">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="left" colspan="4">
                                                                                        <cc1:ModalPopupExtender ID="ModalPopupProduct" runat="server" BackgroundCssClass="modalBackground"
                                                                                            CancelControlID="CancelPopUp" DynamicServicePath="" Enabled="True" PopupControlID="pnlPopup"
                                                                                            TargetControlID="ShowPopUp">
                                                                                        </cc1:ModalPopupExtender>
                                                                                        <input id="ShowPopUp" type="button" style="display: none" runat="server" />
                                                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;<input ID="CancelPopUp" runat="server" style="display: none" 
                                                                                            type="button" /> </input>
                                                                                        </input>
                                                                                        &nbsp;<asp:ValidationSummary ID="VS" runat="server" Font-Names="'Trebuchet MS'" Font-Size="12pt"
                                                                                            HeaderText="Validation Messages" ShowMessageBox="True" ShowSummary="False" ValidationGroup="salesval" />
                                                                                        <asp:Panel ID="pnlPopup" runat="server" Width="61%" CssClass="modalPopup">
                                                                                            <asp:UpdatePanel ID="updatePnlProduct" runat="server" UpdateMode="Conditional">
                                                                                                <ContentTemplate>
                                                                                                    <asp:Panel ID="pnlItems" CssClass="pnlPopUp" runat="server">
                                                                                                        <div id="contentPopUp">
                                                                                                            <table class="tblLeft" cellpadding="1" cellspacing="1" width="100%">
                                                                                                                <tr>
                                                                                                                    <td style="width:2px;">
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <table class="headerPopUp" width="100%">
                                                                                                                            <tr>
                                                                                                                                <td>
                                                                                                                                    Product Details
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                        </table>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td style="width:2px;">
                                                                                                                    </td>
                                                                                                                    <td >
                                                                                                                        <table cellpadding="1" cellspacing="1" width="100%" align="center" style="border:1px solid blue">
                                                                                                                            <tr style="height:3px">
                                                                                                                            </tr>
                                                                                                                            <tr>
                                                                                                                               <td class="ControlLabel" style="width: 25%;">
                                                                                                                                    Customer Name *
                                                                                                                                    <asp:CompareValidator ID="cvCustomer" runat="server" ControlToValidate="cmbCustomer"
                                                                                                                                        Display="Dynamic" ErrorMessage="Please Select Customer!!" Operator="GreaterThan"
                                                                                                                                        Text="*" ValidationGroup="product" ValueToCompare="0"></asp:CompareValidator>
                                                                                                                                </td>
                                                                                                                                <td  style="width: 24%;" class="ControlDrpBorder">

                                                                                                                                            <asp:DropDownList ID="cmbCustomer" runat="server" AppendDataBoundItems="true" DataTextField="LedgerName" CssClass="drpDownListMedium" style="border: 1px solid #e7e7e7" height="26px" BackColor = "#e7e7e7" DataValueField="LedgerID"
                                                                                                                                                Width="100%" TabIndex="1" ValidationGroup="product">
                                                                                                                                                <asp:ListItem style="background-color: #e7e7e7" Text="Select Customer" Value="0"></asp:ListItem>
                                                                                                                                            </asp:DropDownList>
                                                                                                                                        
                                                                                                                                            <asp:TextBox ID="txtOtherCusName" runat="server" SkinID="skinTxtBox" TabIndex="1" BackColor = "#e7e7e7" height="10px" 
                                                                                                                                                ValidationGroup="product" Visible="False"></asp:TextBox>
                                                                                                                                            <asp:Label ID="lblledgerCategory" runat="server"  Visible="False" CssClass="lblFont" Style="color: royalblue;
                                                                                                                                                font-weight: normal; font-size: smaller"></asp:Label>
                                                                                                                                        
                                                                                                                                </td>
                                                                                                                                <td class="ControlLabel" style="width: 15%;">
                                                                                                                                    Selling PayMode
                                                                                                                                </td>
                                                                                                                                <td style="width: 24%;" class="ControlDrpBorder">
                                                                                                                                    <asp:UpdatePanel ID="UpdatePanelPayMode" runat="server" UpdateMode="Conditional">
                                                                                                                                        <ContentTemplate >
                                                                                                                                            <asp:DropDownList ID="drpPaymode" runat="server" AppendDataBoundItems="True" style="text-align:center; border:1px solid #e7e7e7" height="26px" BackColor = "#e7e7e7" AutoPostBack="True" Width="100%" CssClass="drpDownListMedium"
                                                                                                                                                OnSelectedIndexChanged="drpPaymode_SelectedIndexChanged"
                                                                                                                                                TabIndex="2" ValidationGroup="product">
                                                                                                                                                <asp:ListItem Text="Cash" Value="1"></asp:ListItem>
                                                                                                                                                <asp:ListItem Text="Bank / Credit Card" Value="2"></asp:ListItem>
                                                                                                                                                <asp:ListItem Text="Credit" Value="3"></asp:ListItem>
                                                                                                                                            </asp:DropDownList>
                                                                                                                                        </ContentTemplate>
                                                                                                                                    </asp:UpdatePanel>       
                                                                                                                                </td>
                                                                                                                                <td style="width: 15%;">
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                            <tr style="height:3px">
                                                                                                                            </tr>
                                                                                                                            <tr>
                                                                                                                                <td colspan="5">
                                                                                                                                    <asp:UpdatePanel ID="bankPanel" runat="server" UpdateMode="Conditional">
                                                                                                                                        <ContentTemplate>
                                                                                                
                                                                                                                                            <asp:Panel ID="pnlBank" runat="Server" Visible="false">
                                                                                                                                                <table cellpadding="1" cellspacing="1" width="100%">
                                                                                                                                                    <tr>
                                                                                                                                                        <td class="ControlLabel" style="width: 25%;">
                                                                                                                                                            <asp:RequiredFieldValidator ID="rvCredit" runat="server" ControlToValidate="txtCreditCardNo"
                                                                                                                                                                Enabled="false" ErrorMessage="Cheque\Card number is mandatory" Text="*" ValidationGroup="product" />
                                                                                                                                                            Cheque / Credit Card No.*
                                                                                                                                                        </td>
                                                                                                                                                        <td class="ControlTextBox3" style="width: 24%;">
                                                                                                                                                            <asp:TextBox ID="txtCreditCardNo" runat="server" Height="20px" CssClass="cssTextBox" 
                                                                                                                                                                Width="100px" TabIndex="3" ValidationGroup="product"></asp:TextBox>
                                                                                                                                                        </td>
                                                                                                                                                        <td class="ControlLabel" style="width: 15.4%;">
                                                                                                                                                            <asp:RequiredFieldValidator ID="rvbank" runat="server" ControlToValidate="drpBankName"
                                                                                                                                                                Enabled="false" ErrorMessage="Bankname is mandatory" InitialValue="0" Text="*"
                                                                                                                                                                ValidationGroup="product" />
                                                                                                                                                            Bank Name*
                                                                                                                                                        </td>
                                                                                                                                                        <td class="ControlDrpBorder" style="width: 24%;">
                                                                                                                                                            <asp:DropDownList ID="drpBankName" runat="server" AppendDataBoundItems="true" DataTextField="LedgerName" Width="100%" style="border: 1px solid #e7e7e7" height="26px" CssClass="drpDownListMedium" BackColor = "#e7e7e7" 
                                                                                                                                                                DataValueField="LedgerID" TabIndex="4" ValidationGroup="product">
                                                                                                                                                                <asp:ListItem style="background-color: #e7e7e7" Text="Select Bank" Value="0"></asp:ListItem>
                                                                                                                                                            </asp:DropDownList>
                                                                                                                                                        </td>
                                                                                                                                                        <td style="width: 15%;">
                                                                                                                                                        </td>
                                                                                                            
                                                                                                                                                    </tr>
                                                                                                                                                </table>
                                                                                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" FilterType="Numbers"
                                                                                                                                                                TargetControlID="txtCreditCardNo" />
                                                                                                                                            </asp:Panel>
                                                                                                                                        </ContentTemplate>
                                                                                                                                        <Triggers>
                                                                                                                                            <asp:AsyncPostBackTrigger ControlID="drpPaymode" EventName="SelectedIndexChanged" />
                                                                                                                                        </Triggers>
                                                                                                                                    </asp:UpdatePanel>
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                            <tr style="height:3px">
                                                                                                                            </tr>
                                                                                                                            <tr>
                                                                                                                                <td colspan="5"  style="border-bottom: 1px solid #e7e7e7;  width:80%">
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                            <tr style="height:3px">
                                                                                                                            </tr>
                                                                                                                            <tr id="rowBarcode" runat="server">
                                                                                                                                <td class="ControlLabel" style="width:25%;">
                                                                                                                                    Barcode
                                                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtBarcode"
                                                                                                                                        CssC0lass="lblFont" Text="BarCode is mandatory" ValidationGroup="lookUp" />
                                                                                                                                </td>
                                                                                                                                <td class="ControlTextBox3" style="width:24%;">
                                                                                                                                    <asp:TextBox ID="txtBarcode" runat="server" CssClass="cssTextBox" Width="80px" SkinID="skinTxtBox" />
                                                                                                                                </td>
                                                                                                                                <td  style="width:15%;">
                                                                                                                                    <asp:Button ID="cmdBarcode" runat="server" SkinID="skinBtnMedium" Text="Lookup Product"
                                                                                                                                        ValidationGroup="lookUp" />
                                                                                                                                </td>
                                                                                                                                
                                                                                                                                <td style="width: 24%"></td>
                                                                                                                                <td style="width: 15%;">
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                            <tr style="height:3px">
                                                                                                                            </tr>
                                                                                                                            <tr>
                                                                                                                                <td class="ControlLabel"  style="width:25%;">
                                                                                                                                    Category *
                                                                                                                                    <asp:CompareValidator ID="cvCatergory" runat="server" ControlToValidate="cmbCategory"
                                                                                                                                        Display="Dynamic" ErrorMessage="Category is Mandatory" Operator="GreaterThan"
                                                                                                                                        Text="*" ValidationGroup="product" ValueToCompare="0"></asp:CompareValidator>
                                                                                                                                </td>
                                                                                                                                <td class="ControlDrpBorder" style="width:24%;"> 
                                                                                                                                    <asp:UpdatePanel ID="UpdatePanel13" runat="server" UpdateMode="Always">
                                                                                                                                        <ContentTemplate >
                                                                                                                                        <asp:DropDownList ID="cmbCategory" runat="server" AutoPostBack="true" BackColor = "#e7e7e7"
                                                                                                                                            Width="100%" OnSelectedIndexChanged="LoadProducts" AppendDataBoundItems="True" style="border: 1px solid #e7e7e7" height="26px" CssClass="drpDownListMedium">
                                                                                                                                            <asp:ListItem Selected="True" Value="0" style="background-color: #e7e7e7">Select Category</asp:ListItem>
                                                                                                                                        </asp:DropDownList>
                                                                                                                                    </ContentTemplate>
                                                                                                                                        
                                                                                                                                    </asp:UpdatePanel>
                                                                                                                                </td>
                                                                                                                                <td class="ControlLabel"  style="width:15%;">
                                                                                                                                    Product Code
                                                                                                                                </td>
                                                                                                                                <td class="ControlDrpBorder" style="width: 24%">
                                                                                                                                    <asp:UpdatePanel ID="UpdatePanel14" runat="server" UpdateMode="Always">
                                                                                                                                        <ContentTemplate >
                                                                                                                                        <asp:DropDownList ID="cmbProdAdd" runat="server" AppendDataBoundItems="true" AutoPostBack="true" BackColor = "#e7e7e7"
                                                                                                                                            DataTextField="ProductName" DataValueField="ItemCode" OnSelectedIndexChanged="LoadForProduct" style="border: 1px solid #e7e7e7" height="26px" CssClass="drpDownListMedium"
                                                                                                                                            ValidationGroup="product" Width="100%">
                                                                                                                                            <asp:ListItem style="background-color: #e7e7e7;" Text="Select Product" Value="0"></asp:ListItem>
                                                                                                                                        </asp:DropDownList>
                                                                                                                                    </ContentTemplate>
                                                                                                                                        
                                                                                                                                    </asp:UpdatePanel>
                                                                                                                                </td>
                                                                                                                                <td  style="width: 15%"></td>
                                                                                                                            </tr>
                                                                                                                            <tr style="height:3px">
                                                                                                                            </tr>
                                                                                                                           <tr>
                                                                                                                                <td class="ControlLabel" style="width:25%;">
                                                                                                                                    Product Name
                                                                                                                                </td>
                                                                                                                                <td class="ControlDrpBorder" style="width:24%;">
                                                                                                                                    <asp:UpdatePanel ID="UpdatePanel15" runat="server" UpdateMode="Always">
                                                                                                                                        <ContentTemplate >
                                                                                                                                        <asp:DropDownList ID="cmbProdName" runat="server" Width="100%" BackColor = "#e7e7e7" style="border: 1px solid #e7e7e7" height="26px" CssClass="drpDownListMedium"
                                                                                                                                            AutoPostBack="true" OnSelectedIndexChanged="LoadForProductName" AppendDataBoundItems="True">
                                                                                                                                            <asp:ListItem Selected="True" Value="0" style="background-color: #e7e7e7">Select Product</asp:ListItem>
                                                                                                                                        </asp:DropDownList>
                                                                                                                                        <asp:TextBox ID="lblProdDescAdd" runat="server" CssClass="cssTextBox" ReadOnly="true"
                                                                                                                                            Visible="false" Width="196px"></asp:TextBox>
                                                                                                                                   </ContentTemplate>
                                                                                                                                        
                                                                                                                                    </asp:UpdatePanel>
                                                                                                                                </td>
                                                                                                                                <td class="ControlLabel" style="width:15%;">
                                                                                                                                    Brand
                                                                                                                                </td>
                                                                                                                                <td class="ControlDrpBorder" style="width: 24%">
                                                                                                                                <asp:UpdatePanel ID="UpdatePanel16" runat="server" UpdateMode="Always">
                                                                                                                                        <ContentTemplate >
                                                                                                                                     <asp:DropDownList ID="cmbBrand" runat="server" Width="100%" AutoPostBack="true" BackColor = "#e7e7e7"  style="border: 1px solid #e7e7e7" height="26px" CssClass="drpDownListMedium"
                                                                                                                                            OnSelectedIndexChanged="LoadForBrand" AppendDataBoundItems="True">
                                                                                                                                            <asp:ListItem Selected="True" Value="0" style="background-color: #e7e7e7">Select Category</asp:ListItem>
                                                                                                                                     </asp:DropDownList>
                                                                                                                                     </ContentTemplate>
                                                                                                                                        
                                                                                                                                    </asp:UpdatePanel>
                                                                                                                                </td>
                                                                                                                                <td  style="width: 15%"></td>
                                                                                                                            </tr>
                                                                                                                            <tr style="height:3px">
                                                                                                                            </tr>
                                                                                                                            <tr>
                                                                                                                                <td class="ControlLabel" style="width:25%;">
                                                                                                                                    Model
                                                                                                                                </td>
                                                                                                                                <td class="ControlDrpBorder" style="width:24%;">
                                                                                                                                 <asp:UpdatePanel ID="UpdatePanel17" runat="server" UpdateMode="Always">
                                                                                                                                        <ContentTemplate >
                                                                                                                                        <asp:DropDownList ID="cmbModel" runat="server" OnSelectedIndexChanged="LoadForModel" BackColor = "#e7e7e7" style="border: 1px solid #e7e7e7" height="26px" CssClass="drpDownListMedium"
                                                                                                                                            AutoPostBack="true" Width="100%" AppendDataBoundItems="True">
                                                                                                                                            <asp:ListItem Selected="True" Value="0" style="background-color: #e7e7e7">Select Category</asp:ListItem>
                                                                                                                                        </asp:DropDownList>
                                                                                                                         </ContentTemplate>
                                                                                                                                        
                                                                                                                                    </asp:UpdatePanel>
                                                                                                                                </td>
                                                                                                                                <td class="ControlLabel" style="width:15%;">
                                                                                                                                    
                                                                                                                                </td>
                                                                                                                                <td  style="width: 24%">
                                                                                                                                    <asp:TextBox ID="txtstock" runat="server" CssClass="cssTextBox" ValidationGroup="product" Width="70px" Height="23px" BackColor = "#e7e7e7" 
                                                                                                                                            Visible="False"></asp:TextBox>
                                                                                                                                    
                                                                                                                                </td>
                                                                                                                                <td  style="width: 15%"></td>
                                                                                                                            </tr>
                                                                                                                            <tr style="height:3px">
                                                                                                                            </tr>
                                                                                                                            <tr>
                                                                                                                                <td style="width: 25%" class="ControlLabel">
                                                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtRateAdd"
                                                                                                                                        ErrorMessage="Product Rate is mandatory" Text="*" ValidationGroup="product" />
                                                                                                                                Rate
                                                                                                                                </td>
                                                                                                                                <td style="width: 24%" class="ControlTextBox3">
                                                                                                                                <asp:UpdatePanel ID="UpdatePanel18" runat="server" UpdateMode="Always">
                                                                                                                                        <ContentTemplate >
                                                                                                                                    <asp:TextBox ID="txtRateAdd" runat="server" CssClass="cssTextBox" ValidationGroup="product" Width="70px" Height="23px" BackColor = "#e7e7e7" 
                                                                                                                                        ></asp:TextBox>
                                                                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender41" runat="server" FilterType="Custom, Numbers"
                                                                                                                                        TargetControlID="txtRateAdd" ValidChars="." />
                                                                                                                                        </ContentTemplate>
                                                                                                                                        </asp:UpdatePanel>
                                                                                                                                </td>
                                                                                                                                
                                                                                                                                <td style="width: 15%" class="ControlLabel">
                                                                                                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtQtyAdd"
                                                                                                                                        Display="Dynamic" ErrorMessage="Product Qty. must be greater than Zero" Operator="GreaterThan"
                                                                                                                                        Text="*" ValidationGroup="product" ValueToCompare="0"></asp:CompareValidator>
                                                                                                                                    <asp:RequiredFieldValidator ID="rvQty" runat="server" ControlToValidate="txtQtyAdd"
                                                                                                                                        ErrorMessage="Qty. is mandatory" Text="*" ValidationGroup="product" />
                                                                                                                                Qty. *
                                                                                                                                </td>
                                                                                                                                <td style="width: 24%" class="ControlTextBox3">
                                                                                                                                    <asp:TextBox ID="txtQtyAdd" runat="server" CssClass="cssTextBox" Text="0" ValidationGroup="product" Width="70px" Height="23px" BackColor = "#e7e7e7" 
                                                                                                                                       ></asp:TextBox>
                                                                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom, Numbers"
                                                                                                                                        TargetControlID="txtQtyAdd" ValidChars="." />
                                                                                                                                </td>
                                                                                                                                
                                                                                                                                <td style="width:15%;">
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                            <tr style="height:3px">
                                                                                                                            </tr>
                                                                                                                            <tr>
                                                                                                                                <td colspan="5"  style="border-bottom: 1px solid #90c9fc;  width:80%">
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                            <tr style="height:3px">
                                                                                                                            </tr>
                                                                                                                            <tr>
                                                                                                                                <td align="center" colspan="5">
                                                                                                                                    <table style="width:100%;">
                                                                                                                                        <tr>
                                                                                                                                            <td style="width:26%;">
                                                                                                                                            </td>
                                                                                                                                            <td style="width:13%;">                                                                                                                                               
                                                                                                                                                 <asp:Panel ID="Panel3" runat="server" Width="120px" Height="32px">
                                                                                                                                                    <asp:Button ID="cmdSaveProduct" runat="server" CssClass="AddProd6" 
                                                                                                                                                        EnableTheming="false" OnClick="cmdSaveProduct_Click" Text="" Height="45px" 
                                                                                                                                                        ValidationGroup="product" />
                                                                                                                                                    <asp:Button ID="cmdUpdateProduct" runat="server" CssClass="UpdateProd6" 
                                                                                                                                                        Enabled="false" EnableTheming="false" Height="45px" 
                                                                                                                                                        OnClick="cmdUpdateProduct_Click" Text="" ValidationGroup="product" 
                                                                                                                                                        Width="45px" />
                                                                                                                                                </asp:Panel>
                                                                                                                                            </td>
                                                                                                                                            <td style="width:13%;">
                                                                                                                                                <asp:Panel ID="Panel2" runat="server" Width="120px" Height="32px">
                                                                                                                                                    <asp:Button ID="cmdCancelProduct" runat="server" CssClass="CloseWindow6" Height="45px"  CausesValidation="false"
                                                                                                                                                        EnableTheming="false" OnClick="cmdCancelProduct_Click"/>
                                                                                                                                        
                                                                                                                                                </asp:Panel>
                                                                                                                                            </td>
                                                                                                                                            <td style="width:13%;">
                                                                                                                                                <asp:Panel ID="Panel1" runat="server" >
                                                                                                                                                    <asp:Button ID="BtnClearFilter" runat="server" OnClick="btnClearFilter_Click" EnableTheming="false" CssClass="ClearFilter6"
                                                                                                                                                        Visible="false"  Text="" />
                                                                                                                                                </asp:Panel>
                                                                                                                                            </td>
                                                                                                                                            <td style="width:21%;">
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                    </table>
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                        </table>
                                                                                                                    </td>
                                                                                                                    <td>
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
                                                                                <tr>
                                                                                    <td colspan="4">
                                                                                        <asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="Conditional">
                                                                                            <ContentTemplate>
                                                                                                <asp:HiddenField ID="hdnDisplay" runat="server" Value="N" />
                                                                                                <asp:HiddenField ID="hdStock" runat="server" Value="0" />
                                                                                                <asp:HiddenField ID="hdTotalAmt" runat="server" Value="0" />
                                                                                                <asp:HiddenField ID="hdsales" runat="server" Value="0" />
                                                                                                <asp:HiddenField ID="hdSeries" runat="server" Value="0" />
                                                                                                <asp:HiddenField ID="hdContact" runat="server" />
                                                                                                <asp:HiddenField ID="hdCreditSMS" runat="server" Value="NO" />
                                                                                                <asp:HiddenField ID="hdCustCreditLimit" runat="server" Value="0" />
                                                                                                <asp:HiddenField ID="hdAllowSales" runat="server" Value="NO" />
                                                                                                <asp:HiddenField ID="hdPrevSalesTotal" runat="server" Value="0" />
                                                                                                <asp:HiddenField ID="hdBalance" runat="server" Value="0" />
                                                                                                <asp:HiddenField ID="hdPrevMode" runat="server" Value="0" />
                                                                                                <asp:HiddenField ID="hdCREDITEXD" runat="server" Value="NO" />
                                                                                                <asp:HiddenField ID="hdDaysLimit" runat="server" Value="NO" />
                                                                                                <asp:HiddenField ID="hdCurrentRow" runat="server" Value="0" />
                                                                                                <asp:HiddenField ID="hdCurrRole" runat="server" />
                                                                                                <asp:HiddenField ID="hdOpr" runat="server" />
                                                                                                <asp:HiddenField ID="hdEditQty" runat="server" Value="0" />
                                                                                            </ContentTemplate>
                                                                                            <Triggers>
                                                                                                <asp:AsyncPostBackTrigger ControlID="dpsupplier" EventName="SelectedIndexChanged" />
                                                                                                <asp:AsyncPostBackTrigger ControlID="cmdSaveProduct" EventName="Click" />
                                                                                            </Triggers>
                                                                                        </asp:UpdatePanel>
                                                                                    </td>
                                                                                </tr>

                                                                            </table>
                                                                            </td> </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="4">
                                                                                    <br/>
                                                                                    <asp:UpdatePanel ID="UpdatePanelSalesItems" runat="server" UpdateMode="Conditional">
                                                                                        <ContentTemplate>
                                                                                            <asp:Panel ID="pnlProduct" runat="server">
                                                                                                <table cellpadding="0" cellspacing="1" style="border: 1px Solid #86b2d1; min-height: 50px"
                                                                                                    width="96%">
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <asp:GridView ID="GrdViewItems" runat="server" AllowPaging="True" BorderWidth="1px" CssClass="someClass"
                                                                                                                DataKeyNames="Roles" EmptyDataText="No Items added." OnRowDataBound="GrdViewItems_RowDataBound"
                                                                                                                ShowFooter="false" OnRowDeleting="GrdViewItems_RowDeleting" OnSelectedIndexChanged="GrdViewItems_SelectedIndexChanged"
                                                                                                                Width="100%">
                                                                                                                <RowStyle Font-Bold="false" />
                                                                                                                <FooterStyle Font-Bold="true" Wrap="false" />
                                                                                                                <Columns>
                                                                                                                    <asp:BoundField DataField="itemcode" HeaderText="Product"  HeaderStyle-BorderColor="Gray"/>
                                                                                                                    <asp:BoundField DataField="ProductName" HeaderText="Description"  HeaderStyle-BorderColor="Gray"/>
                                                                                                                    <asp:BoundField DataField="ProductDesc" HeaderText="Brand" Visible="false"  HeaderStyle-BorderColor="Gray"/>
                                                                                                                    
                                                                                                                    <asp:BoundField DataField="Rate" HeaderStyle-Width="50px" HeaderText="Rate"  HeaderStyle-BorderColor="Gray"/>
                                                                                                                    <asp:BoundField DataField="Qty" HeaderStyle-Width="50px" HeaderText="Qty."  HeaderStyle-BorderColor="Gray"/>
                                                                                                                    <asp:BoundField DataField="ExecCharge" HeaderStyle-Width="70px" Visible="false" HeaderText="Exec Comm"  HeaderStyle-BorderColor="Gray"/>
                                                                                                                    <asp:BoundField DataField="Measure_Unit" HeaderText="Unit" Visible="false"  HeaderStyle-BorderColor="Gray"/>
                                                                                                                    <asp:BoundField DataField="Discount" HeaderStyle-Width="60px" Visible="false" DataFormatString="{0:F2}" HeaderStyle-BorderColor="Gray"
                                                                                                                        HeaderText="Disc(%)" />
                                                                                                                    <asp:BoundField DataField="Vat" HeaderStyle-Width="60px" HeaderText="VAT(%)"  Visible="false" HeaderStyle-BorderColor="Gray"/>
                                                                                                                    <asp:BoundField DataField="CST" HeaderStyle-Width="60px" HeaderText="CST(%)"  Visible="false" HeaderStyle-BorderColor="Gray"/>

                                                                                                                    <asp:BoundField DataField="CustomerName" HeaderText="CustomerName" HeaderStyle-BorderColor="Gray"/>
                                                                                                                    <asp:BoundField DataField="CustomerID" HeaderText="CustomerID"  Visible="false" HeaderStyle-BorderColor="Gray"/>
                                                                                                                    <asp:BoundField DataField="OtherCusName" HeaderText="OtherCusName" Visible="false"  HeaderStyle-BorderColor="Gray"/>
                                                                                                                    <asp:BoundField DataField="SellingPaymode" HeaderText="Paymode"  HeaderStyle-BorderColor="Gray"/>
                                                                                                                    <asp:BoundField DataField="CardNo" HeaderText="CardNo" HeaderStyle-BorderColor="Gray"/>
                                                                                                                    <asp:BoundField DataField="BankName" HeaderText="BankName" HeaderStyle-BorderColor="Gray"/>
                                                                                                                    <asp:BoundField DataField="BankID" HeaderText="BankID" Visible="false" HeaderStyle-BorderColor="Gray"/>

                                                                                                                    <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Total" HeaderStyle-BorderColor="Gray">
                                                                                                                        <ItemTemplate>
                                                                                                                             <asp:Label ID="lbltotal" runat="server" Text='<%# GetTotal(Convert.ToDouble(Eval("Qty").ToString()), Convert.ToDouble(Eval("rate").ToString()))%>'></asp:Label>
                                                                                                                            <%--<asp:Label ID="lbltotal" runat="server" Text='<%# GetTotal(Eval("Qty").ToString(), Eval("rate").ToString(), Eval("discount").ToString(), Eval("vat").ToString(), Eval("CST").ToString())%>'></asp:Label>--%>
                                                                                                                        </ItemTemplate>
                                                                                                                        <FooterTemplate>
                                                                                                                            <asp:Label ID="lbltotalSummary" runat="server" Text=""></asp:Label>
                                                                                                                        </FooterTemplate>
                                                                                                                        <FooterStyle />
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderStyle-Width="30px" ItemStyle-CssClass="command" HeaderStyle-BorderColor="Gray">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:ImageButton ID="btnEdit" runat="server" CommandName="Select" SkinID="edit" />
                                                                                                                        </ItemTemplate>
                                                                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderStyle-Width="30px" ItemStyle-CssClass="command" HeaderStyle-BorderColor="Gray">
                                                                                                                        <ItemTemplate>
                                                                                                                            <cc1:ConfirmButtonExtender ID="CnrfmDel" runat="server" ConfirmText="Are you sure to delete this product from Sales?"
                                                                                                                                TargetControlID="lnkB">
                                                                                                                            </cc1:ConfirmButtonExtender>
                                                                                                                            <asp:ImageButton ID="lnkB" runat="Server" CommandName="Delete" SkinID="delete" />
                                                                                                                        </ItemTemplate>
                                                                                                                        <HeaderStyle Width="4%" />
                                                                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                                                                    </asp:TemplateField>
                                                                                                                </Columns>
                                                                                                            </asp:GridView>
                                                                                                            <asp:GridView ID="GrdViewEmptyItems" runat="server" AllowPaging="True" BorderWidth="1px" CssClass="someClass"
                                                                                                                GridLines="Both" EnableTheming="false" AutoGenerateColumns="false" ShowFooter="false"
                                                                                                                Width="100%">
                                                                                                                <RowStyle CssClass="dataRow" BorderStyle="Solid" BorderColor="Black" BorderWidth="1px" />
                                                                                                                <SelectedRowStyle CssClass="SelectdataRow" Font-Bold="true" />
                                                                                                                <AlternatingRowStyle CssClass="altRow" />
                                                                                                                <EmptyDataRowStyle CssClass="HeadataRow" Font-Bold="true" Height="25px" />
                                                                                                                <HeaderStyle Height="25px" CssClass="HeadataRow" Font-Bold="true" />
                                                                                                                <FooterStyle Height="27px" CssClass="dataRow" />
                                                                                                                <Columns>
                                                                                                                    <asp:BoundField DataField="itemcode" HeaderText="Product"  HeaderStyle-BorderColor="Gray"/>
                                                                                                                    <asp:BoundField DataField="ProductName" HeaderText="Description"  HeaderStyle-BorderColor="Gray"/>
                                                                                                                    <asp:BoundField DataField="ProductDesc" HeaderText="Brand" Visible="false"  HeaderStyle-BorderColor="Gray"/>
                                                                                                                    <asp:BoundField DataField="Rate" HeaderStyle-Width="50px" HeaderText="Rate"  HeaderStyle-BorderColor="Gray"/>
                                                                                                                    <asp:BoundField DataField="Qty" HeaderStyle-Width="50px" HeaderText="Qty."  HeaderStyle-BorderColor="Gray"/>
                                                                                                                    <asp:BoundField DataField="ExecCharge" HeaderStyle-Width="70px" HeaderText="Exec Comm"  HeaderStyle-BorderColor="Gray" Visible="False" />
                                                                                                                    <asp:BoundField DataField="Measure_Unit" HeaderText="Unit" Visible="false"  HeaderStyle-BorderColor="Gray"/>
                                                                                                                    <asp:BoundField DataField="Discount" HeaderStyle-Width="60px" DataFormatString="{0:F2}"
                                                                                                                        HeaderText="Disc(%)"  HeaderStyle-BorderColor="Gray" Visible="False" />
                                                                                                                    <asp:BoundField DataField="Vat" HeaderStyle-Width="60px" HeaderText="VAT(%)"  HeaderStyle-BorderColor="Gray" Visible="False" />
                                                                                                                    <asp:BoundField DataField="CST" HeaderStyle-Width="60px" HeaderText="CST(%)"  HeaderStyle-BorderColor="Gray" Visible="False" />
                                                                                                                    <asp:BoundField DataField="CustomerName" HeaderText="CustomerName" HeaderStyle-BorderColor="Gray" Visible="False"/>
                                                                                                                    <asp:BoundField DataField="Customer ID" HeaderText="CustomerID"  HeaderStyle-BorderColor="Gray" Visible="False"/>
                                                                                                                    <asp:BoundField DataField="OtherCusName" HeaderText="OtherCusName" Visible="false"  HeaderStyle-BorderColor="Gray"/>
                                                                                                                    <asp:BoundField DataField="SellingPaymode" HeaderText="SellingPaymode" Visible="false"  HeaderStyle-BorderColor="Gray"/>
                                                                                                                    <asp:BoundField DataField="CardNo" HeaderText="CardNo" Visible="false"  HeaderStyle-BorderColor="Gray"/>
                                                                                                                    <asp:BoundField DataField="BankName" HeaderText="BankName" Visible="false"  HeaderStyle-BorderColor="Gray"/>
                                                                                                                </Columns>
                                                                                                            </asp:GridView>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </asp:Panel>
                                                                                        </ContentTemplate>
                                                                                        <Triggers>
                                                                                            <asp:AsyncPostBackTrigger ControlID="cmdSaveProduct" EventName="Click" />
                                                                                            <asp:AsyncPostBackTrigger ControlID="cmdUpdateProduct" EventName="Click" />
                                                                                            <asp:AsyncPostBackTrigger ControlID="GrdViewItems" />
                                                                                            <asp:AsyncPostBackTrigger ControlID="cmdUpdate" />
                                                                                        </Triggers>
                                                                                    </asp:UpdatePanel>
                                                                                    <br>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="4">
                                                                                    <br />
                                                                                    <asp:UpdatePanel ID="UpdatePanelTotalSummary" runat="server" UpdateMode="Conditional" >
                                                                                        <ContentTemplate>
                                                                                            <table width="100%">
                                                                                                <tr>
                                                                                                    <td style="width:11px">
                                                                                                    </td>
                                                                                                    <td class="uploadingbg312">
                                                                                                        <div>
                                                                                                            <div>
                                                                                                                <div>
                                                                                                                    <table border="0" cellpadding="0px" cellspacing="5px" style="margin: 0px auto;">
                                                                                                                        <tr style="display: none" visible="false">
                                                                                                                            <td align="left">
                                                                                                                                <asp:Label ID="lblDispTotal" runat="server" CssClass="item"></asp:Label>
                                                                                                                            </td>
                                                                                                                            <td width="1px">
                                                                                                                            </td>
                                                                                                                            <td align="left">
                                                                                                                                <asp:Label ID="lblTotalSum" runat="server" CssClass="item" Font-Bold="true"></asp:Label>
                                                                                                                                <br />
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        
                                                                                                                        <tr>
                                                                                                                            <td align="left">
                                                                                                                                <asp:Label ID="lblDispLoad" runat="server" CssClass="item"></asp:Label>
                                                                                                                            </td>
                                                                                                                            <td width="1px">
                                                                                                                            </td>
                                                                                                                            <td align="right">
                                                                                                                                <asp:Label ID="lblFreight" runat="server" CssClass="item" Font-Bold="true"></asp:Label>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                            <td align="left">
                                                                                                                                <asp:Label ID="lblsalesname" runat="server" CssClass="item"></asp:Label>
                                                                                                                            </t>
                                                                                                                            <td width="1px">
                                                                                                                            </td>
                                                                                                                            <td align="right">
                                                                                                                                <asp:Label ID="lblsales" runat="server" CssClass="item" Font-Bold="true" Text="0"></asp:Label>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                            <td align="left">
                                                                                                                                <asp:Label ID="lblDispGrandTtl" runat="server" CssClass="item"></asp:Label>
                                                                                                                            </td>
                                                                                                                            <td width="1px">
                                                                                                                            </td>
                                                                                                                            <td align="right">
                                                                                                                                <asp:Label ID="lblNet" runat="server" CssClass="item" Font-Bold="true" Text="0"></asp:Label>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                    </table>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:TextBox ID="txtCommissionPercent" runat="server" Width="60px" Visible="false"
                                                                                                                        ></asp:TextBox>
                                                                                                        <asp:TextBox ID="txt" runat="server" Width="60px" Visible="false"
                                                                                                                        Text="0"></asp:TextBox>
                                                                                                    </td>
                                                                                                    <td style="text-align: right">
                                                                                                        <div style="text-align: right">
                                                                                                            <asp:Panel ID="PanelCmd" runat="server">
                                                                                                                <table>
                                                                                                                   <tr>
                                                                                                                        
                                                                                                                        <td style="padding: 1px">
                                                                                                                            <asp:Button ID="CmdProd" runat="server" Text="" SkinID="skinBtnGeneral"
                                                                                                                                   onclick="cmdprod_click" EnableTheming="false" CssClass="Newproductbutton6"  
                                                                                                                                   Width="28px" Height="27px" Visible="False" />           
                                                                                                                        </td>

                                                                                                                        <td style="padding: 1px">
                                                                                                                            <asp:Button ID="AddNewProd" runat="server" Text="" SkinID="skinBtnAddProduct" CssClass="addproductbutton6"
                                                                                                                                EnableTheming="false" OnClick="lnkAddProduct_Click" />
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:Button ID="cmdPrint" runat="server" Enabled="false" OnClick="cmdPrint_Click"
                                                                                                                                SkinID="skinBtnPrint" Text="" CssClass="printbutton6" EnableTheming="false" ValidationGroup="salesval" />
                                                                                                                        </td>
                                                                                                                        <td style="width: 6px">
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        
                                                                                                                        <td style="padding: 1px">
                                                                                                                            
                                                                                                                                <asp:Button ID="CmdCat" runat="server" Text="" SkinID="skinBtnGeneral"
                                                                                                                                    OnClick="cmdcat_click"  EnableTheming="false" CssClass="NewCustomerbutton"  
                                                                                                                                    Width="28px" Height="27px" Visible="False" />                 
                                                                                                                                
                                                                                                                        </td>
                                                                                                                        <td style="padding: 1px">
                                                                                                                            <asp:Button ID="cmdUpdate" runat="server" Enabled="false" OnClick="cmdUpdate_Click"
                                                                                                                                SkinID="skinBtnSave"
                                                                                                                                CssClass="Updatebutton1231" EnableTheming="false" Text="" ValidationGroup="salesval" />
                                                                                                                            <asp:Button ID="cmdSave" runat="server" OnClick="cmdSave_Click"
                                                                                                                                SkinID="skinBtnSave" Text="" ValidationGroup="salesval" CssClass="savebutton1231"
                                                                                                                                EnableTheming="false" />
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:Button ID="cmdCancel" runat="server" CausesValidation="false" Enabled="false"
                                                                                                                                OnClick="cmdCancel_Click" SkinID="skinBtnCancel" Text="" CssClass="cancelbutton6"
                                                                                                                                EnableTheming="false" />
                                                                                                                        </td>
                                                                                                                        <td style="width: 6px">
                                                                                                                        </td>                                
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                             </asp:Panel>
                                                                                                        </div>
                                                                                                    </td>
                                                                                                    <td style="width: 5px">
                                                                                                    </td>
                                                                                                    
                                                                                                </tr>
                                                                                            </table>
                                                                                         
                                                                                        <%--<br />--%>
                                                                                        </ContentTemplate>
                                                                                        <Triggers>
                                                                                            <asp:AsyncPostBackTrigger ControlID="cmdSave" EventName="Click" />
                                                                                            <asp:AsyncPostBackTrigger ControlID="cmdCancel" EventName="Click" />
                                                                                            <asp:AsyncPostBackTrigger ControlID="CmdProd" EventName="Click" />
                                                                                            <asp:AsyncPostBackTrigger ControlID="CmdCat" EventName="Click" />
                                                                                            <asp:AsyncPostBackTrigger ControlID="cmdUpdateProduct" EventName="Click" />
                                                                                            <asp:AsyncPostBackTrigger ControlID="cmdSaveProduct" EventName="Click" />
                                                                                        </Triggers>
                                                                                    </asp:UpdatePanel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="4">
                                                                                    <asp:Panel ID="errPanel" runat="server" Visible="False">
                                                                                        <table cellpadding="3" cellspacing="3" class="tblLeft" width="100%">
                                                                                            <tr>
                                                                                                <td class="SalesHeader" colspan="2">
                                                                                                    Exception !!!
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td width="15%">
                                                                                                    Error Message:
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="ErrMsg" runat="server" CssClass="errorMsg"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td colspan="2">
                                                                                                    PLEASE TAKE THE SCREENSHOT AND SEND IT TO ADMINISTRATOR FOR INVESTIGATION
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </asp:Panel>
                                                                                </td>
                                                                            </tr>
                                                                            </table>
                                                                  <cc1:TabContainer ID="tabs2" runat="server" Width="100%" ActiveTabIndex="0" Visible="False">
                                                                    <cc1:TabPanel ID="tabMaster" runat="server" HeaderText="Sales Details" Visible="False">
                                                                        <HeaderTemplate>
                                                                            
                                                                        </HeaderTemplate>
                                                                        <ContentTemplate>
                                                                        </ContentTemplate>
                                                                    </cc1:TabPanel>
                                                                    <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="Additional Details" Visible="False">
                                                                        <ContentTemplate>
                                                                            <table class="tblLeft" width="735px" cellpadding="3" cellspacing="1">
                                                                            <tr style="height:5px">
                                                                            </tr>
                                                                                
                                                                                <tr>
                                                                                    <td class="ControlLabel" style="width: 20%" >
                                                                                        
                                                                                    </td>
                                                                                    <td  style="width: 25%">
                                                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                                                            <ContentTemplate>
                                                                                                <asp:DropDownList ID="drpIncharge" TabIndex="8" Enabled="True" AppendDataBoundItems="true" style="border: 1px solid #90c9fc" height="26px" CssClass="drpDownListMedium" BackColor = "#90c9fc"
                                                                                                    Width="100%" runat="server" DataTextField="empFirstName"
                                                                                                    DataValueField="empno" Visible="False">
                                                                                                    <asp:ListItem Text="Select Executive" Value="0"></asp:ListItem>
                                                                                                </asp:DropDownList>
                                                                                            </ContentTemplate>
                                                                                            <%--<Triggers>
                                                                                                <asp:AsyncPostBackTrigger ControlID="tabs2$tabMaster$cmbCustomer" EventName="SelectedIndexChanged" />
                                                                                            </Triggers>--%>
                                                                                        </asp:UpdatePanel>
                                                                                    </td>
                                                                                    <td class="ControlLabel" style="width: 20%">
                                                                                        
                                                                                    </td>
                                                                                    <td  style="width: 25%">
                                                                                        <asp:DropDownList ID="drpIntTrans" TabIndex="10" AutoPostBack="false" runat="server" BackColor = "#90c9fc" CssClass="drpDownListMedium"
                                                                                            Width="100%" style="border: 1px solid #90c9fc" height="26px" Visible="False">
                                                                                            <asp:ListItem Text="NO" Value="NO" Selected="True"></asp:ListItem>
                                                                                            <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td style="width: 15%">
                                                                                        
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="ControlLabel" style="width: 20%">
                                                                                        
                                                                                    </td>
                                                                                    <td  style="width: 25%" >
                                                                                        <asp:UpdatePanel ID="UpdatePanelPReturn" runat="server" UpdateMode="Conditional">
                                                                                            <ContentTemplate>
                                                                                                <asp:DropDownList ID="drpPurchaseReturn" TabIndex="8" AutoPostBack="True" runat="server" BackColor = "#90c9fc" style="border: 1px solid #90c9fc" height="26px" CssClass="drpDownListMedium"
                                                                                                    Width="100%" OnSelectedIndexChanged="drpPurchaseReturn_SelectedIndexChanged"
                                                                                                    Visible="False">
                                                                                                    <asp:ListItem Text="NO" Value="NO" Selected="True"></asp:ListItem>
                                                                                                    <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                                                                                </asp:DropDownList>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </td>
                                                                                    <td style="width: 20%" class="ControlLabel">
                                                                                        
                                                                                    </td>
                                                                                    <td style="width: 25%">
                                                                                        <asp:DropDownList ID="ddDeliveryNote" TabIndex="10" AutoPostBack="false" runat="server" BackColor = "#90c9fc" style="border: 1px solid #90c9fc" height="26px" CssClass="drpDownListMedium" 
                                                                                            Width="100%" Visible="False">
                                                                                            <asp:ListItem Text="NO" Value="NO" Selected="True"></asp:ListItem>
                                                                                            <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td style="width: 15%">
                                                                                        
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="5">
                                                                                        <asp:UpdatePanel ID="UpdatePanel22" runat="server" UpdateMode="Conditional">
                                                                                            <ContentTemplate>
                                                                                                <table runat="server" id="rowReason" cellpadding="3" cellspacing="1" width="100%">
                                                                                                    <tr>
                                                                                                        <td class="ControlLabel" style="width: 20%">
                                                                                                            
                                                                                                        </td>
                                                                                                        <td style="width: 25%">
                                                                                                            <asp:TextBox ID="txtPRReason" CssClass="cssTextBox" TabIndex="11" runat="server" BackColor = "#90c9fc" Height="23px" 
                                                                                                                TextMode="MultiLine" MaxLength="200" Visible="False"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td style="width: 20%">

                                                                                                        </td>
                                                                                                        <td style="width: 25%">

                                                                                                        </td>
                                                                                                        <td style="width: 15%">

                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </ContentTemplate>
                                                                                            <Triggers>
                                                                                                <asp:AsyncPostBackTrigger ControlID="drpPurchaseReturn" EventName="SelectedIndexChanged" />
                                                                                            </Triggers>
                                                                                        </asp:UpdatePanel>
                                                                                    </td>
                                                                                    
                                                                                    <%--<td style="width: 35%">
                                                                                        
                                                                                    </td>--%>
                                                                                </tr>

                                                                                <tr  id="rowTotal" runat="server" visible="False">
                                                                                                                    <td style="width:2px;">
                                                                                                                        
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <table style="width: 100%;" cellpadding="2" cellspacing="2">
                                                                                                                             <tr>
                                                                                                                                <td style="width: 17%" class="ControlLabel">
                                                                                                                                    Total MRP
                                                                                                                                </td>
                                                                                                                                <td style="width: 28%" class="ControlTextBox3">
                                                                                                                                    <asp:TextBox ID="txttotal" runat="server" AutoPostBack="True" CssClass="cssTextBox" Width="70px" Height="23px" BackColor = "#90c9fc" 
                                                                                                                                        ></asp:TextBox>
                                                                                                                                </td>
                                                                                                                                <td style="width: 16.7%" class="ControlLabel">
                                                                                                                                    Sub Total
                                                                                                                                </td>
                                                                                                                                <td style="width: 28.4%" class="ControlTextBox3">
                                                                                                                                    <asp:TextBox ID="txtsubtot" runat="server" AutoPostBack="True" CssClass="cssTextBox" Width="70px" Height="23px" BackColor = "#90c9fc" 
                                                                                                                                        ></asp:TextBox>
                                                                                                                                </td>
                                                                                                                                <td style="width:17.6%;">
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                            <tr id="rowTotal1" runat="server">
                                                                                                                                <td style="width: 17%" class="ControlLabel">
                                                                                                                                    
                                                                                                                                Vat Amount
                                                                                                                                </td>
                                                                                                                                <td style="width: 28%" class="ControlTextBox3">
                                                                                                                                    <asp:TextBox ID="TextBox1" runat="server" AutoPostBack="True" CssClass="cssTextBox" Width="70px" Height="23px" BackColor = "#90c9fc" 
                                                                                                                                        ></asp:TextBox>
                                                                                                                                    
                                                                                                                                     
                                                                                                                                </td>
                                                                                                                                <td style="width: 16.7%" class="ControlLabel">
                                                                                                                                    
                                                                                                                                </td>
                                                                                                                                <td style="width: 28.4%" align="right">
                                                                                                                                   <asp:Button ID="BtnGet" runat="server" OnClick="BtnGet_Click" Text="" EnableTheming="false" CssClass="LoadData" 
                                                                                                                                                            />
                                                                                                                                </td>
                                                                                                                                <td style="width:17.6%;">
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                            <tr style="height:3px">
                                                                                                                            </tr>
                                                                                                                        </table>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td style="width:2px;">
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <table style="width: 100%;" cellpadding="3" cellspacing="2">
                                                                                                                            
                                                                                                                            
                                                                                                                            <tr id="rowexec" visible="false">
                                                                                                                                <td style="width: 16.7%" class="ControlLabel">
                                                                                                                                    
                                                                                                                                </td>
                                                                                                                                <td  style="width: 28.4%">
                                                                                                                                    <asp:TextBox ID="txtExecCharge" runat="server" CssClass="cssTextBox" Text="0" Width="70px" Height="23px" BackColor = "#90c9fc" 
                                                                                                                                       Visible="False"></asp:TextBox>
                                                                                                                                </td>
                                                                                                                                <td style="width: 16.7%" class="ControlLabel">
                                                                                                                                </td>
                                                                                                                                <td style="width: 28.4%">
                                                                                                                                    <asp:TextBox ID="lblDisAdd" runat="server" CssClass="cssTextBox" Text="0" Width="70px" Height="23px" BackColor = "#90c9fc" 
                                                                                                                                        Visible="False"></asp:TextBox>
                                                                                                                                </td>
                                                                                                                                <td style="width:17.6%;">
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                            <tr id="rowexec1" visible="false">
                                                                                                                                <td style="width: 17%" class="ControlLabel">
                                                                                                                                </td>
                                                                                                                                <td style="width: 28%">
                                                                                                                                    <asp:TextBox ID="lblVATAdd" runat="server" CssClass="cssTextBox" Text="0" Width="70px" Height="23px" BackColor = "#90c9fc" 
                                                                                                                                        Visible="False"></asp:TextBox>
                                                                                                                                </td>
                                                                                                                                <td style="width: 16.7%" class="ControlLabel">
                                                                                                                                    </td>
                                                                                                                                <td style="width: 28.4%">
                                                                                                                                    <asp:TextBox ID="lblCSTAdd" runat="server" CssClass="cssTextBox" Text="0" Width="70px" Height="23px" BackColor = "#90c9fc" 
                                                                                                                                        Visible="False"></asp:TextBox>
                                                                                                                                    </td>
                                                                                                                                <td style="width:17.6%;">
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                            <tr style="height:4px">
                                                                                                                            </tr>
                                                                                                                        </table>
                                                                                                                    </td>
                                                                                                                <td>
                                                                                                                </td>
                                                                                                                </tr>
                                                                                <tr style="height:3px">
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="6" style="text-align: center">
                                                                                        <asp:UpdatePanel ID="UpdatePanelMP" runat="server" UpdateMode="Conditional" >
                                                                                            <ContentTemplate>
                                                                                                <div runat="server" id="divMultiPayment" visible="false"  class="customer-paymentsNew">
                                                                                                    <div style="width: 100%; text-align: center" id="divAddMPayments" runat="server">
                                                                                                        <table width="100%" cellpadding="3" cellspacing="1">
                                                                                                            <tr style="height:25px">
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td style="width: 13%">
                                                                                                                </td>
                                                                                                                <td style="width: 25%; text-align: center" class="ControlLabel">
                                                                                                                    Bank
                                                                                                                </td>
                                                                                                                <td style="width: 25%; text-align: center" class="ControlLabel">
                                                                                                                    Cheque/CreditCard
                                                                                                                </td>
                                                                                                                <td style="width: 25%; text-align: center" class="ControlLabel">
                                                                                                                    Amount
                                                                                                                </td>
                                                                                                                <td style="width: 5%">
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td style="width: 13%">
                                                                                                                </td>
                                                                                                                <td class="ControlDrp3" style="width: 25%">
                                                                                                                    <asp:DropDownList ID="ddBank1" runat="server" AppendDataBoundItems="true" DataTextField="LedgerName" BackColor = "#90c9fc" CssClass="drpDownListMedium"
                                                                                                                        onchange="OnBankselectedChange(1);" DataValueField="LedgerID" Width="100%" style="border: 1px solid #90c9fc" height="26px"
                                                                                                                        TabIndex="12" ValidationGroup="salesval">
                                                                                                                        <asp:ListItem style="background-color: #90c9fc" Text="Select Bank" Value="0"></asp:ListItem>
                                                                                                                    </asp:DropDownList>
                                                                                                                </td>
                                                                                                                <td class="ControlDrp3" style="width: 25%">
                                                                                                                    <asp:TextBox ID="txtCCard1" runat="server" MaxLength="20" CssClass="cssTextBox" Width="97%"
                                                                                                                        TabIndex="13"></asp:TextBox>
                                                                                                                    <cc1:FilteredTextBoxExtender ID="dtCard1" runat="server" TargetControlID="txtCCard1"
                                                                                                                        FilterType="Numbers" Enabled="True" />
                                                                                                                </td>
                                                                                                                <td class="ControlDrp3" style="width: 25%">
                                                                                                                    <asp:TextBox ID="txtAmount1" runat="server" MaxLength="20" CssClass="cssTextBox"
                                                                                                                        Width="97%" OnTextChanged="txtRAmount_TextChanged" AutoPostBack="true" TabIndex="14"></asp:TextBox>
                                                                                                                    <cc1:FilteredTextBoxExtender ID="ftAmt1" runat="server" TargetControlID="txtAmount1"
                                                                                                                        FilterType="Custom,Numbers" Enabled="True" ValidChars="." />
                                                                                                                </td>
                                                                                                                <td style="width: 5%">
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td style="width: 13%">
                                                                                                                </td>
                                                                                                                <td class="ControlDrp3" style="width: 25%">
                                                                                                                    <asp:DropDownList ID="ddBank2" runat="server" AppendDataBoundItems="true" DataTextField="LedgerName" BackColor = "#90c9fc" CssClass="drpDownListMedium"
                                                                                                                        onchange="OnBankselectedChange(2);" DataValueField="LedgerID" Width="100%" style="border: 1px solid #90c9fc" height="26px"
                                                                                                                        TabIndex="15" ValidationGroup="salesval">
                                                                                                                        <asp:ListItem style="background-color: #90c9fc" Text="Select Bank" Value="0"></asp:ListItem>
                                                                                                                    </asp:DropDownList>
                                                                                                                </td>
                                                                                                                <td class="ControlDrp3" style="width: 25%">
                                                                                                                    <asp:TextBox ID="txtCCard2" runat="server" MaxLength="20" CssClass="cssTextBox" Width="97%"
                                                                                                                        TabIndex="16"></asp:TextBox>
                                                                                                                    <cc1:FilteredTextBoxExtender ID="ftCard2" runat="server" TargetControlID="txtCCard2"
                                                                                                                        FilterType="Numbers" Enabled="True" />
                                                                                                                </td>
                                                                                                                <td class="ControlDrp3" style="width: 25%">
                                                                                                                    <asp:TextBox ID="txtAmount2" runat="server" MaxLength="20" CssClass="cssTextBox"
                                                                                                                        Width="97%" OnTextChanged="txtRAmount_TextChanged" AutoPostBack="true" TabIndex="17"></asp:TextBox>
                                                                                                                    <cc1:FilteredTextBoxExtender ID="ftAmt2" runat="server" TargetControlID="txtAmount2"
                                                                                                                        FilterType="Custom,Numbers" Enabled="True" ValidChars="." />
                                                                                                                    <asp:CompareValidator ID="cvAmount2" runat="server" ControlToValidate="txtAmount2"
                                                                                                                        Display="Dynamic" ErrorMessage="Amount must be greater than Zero" Operator="GreaterThan"
                                                                                                                        Text="*" ValidationGroup="salesval" ValueToCompare="0"></asp:CompareValidator>
                                                                                                                </td>
                                                                                                                <td style="width: 5%">
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td style="width: 13%">
                                                                                                                </td>
                                                                                                                <td class="ControlDrp3" style="width: 25%">
                                                                                                                    <asp:DropDownList ID="ddBank3" runat="server" AppendDataBoundItems="true" DataTextField="LedgerName" BackColor = "#90c9fc" CssClass="drpDownListMedium"
                                                                                                                        onchange="OnBankselectedChange(3);" DataValueField="LedgerID" Width="100%" style="border: 1px solid #90c9fc" height="26px"
                                                                                                                        TabIndex="12" ValidationGroup="salesval">
                                                                                                                        <asp:ListItem style="background-color: #90c9fc" Text="Select Bank" Value="0"></asp:ListItem>
                                                                                                                    </asp:DropDownList>
                                                                                                                </td>
                                                                                                                <td class="ControlDrp3" style="width: 25%">
                                                                                                                    <asp:TextBox ID="txtCCard3" runat="server" MaxLength="20" CssClass="cssTextBox" Width="97%"
                                                                                                                        TabIndex="13"></asp:TextBox>
                                                                                                                    <cc1:FilteredTextBoxExtender ID="ftCard3" runat="server" TargetControlID="txtCCard3"
                                                                                                                        FilterType="Numbers" Enabled="True" />
                                                                                                                </td>
                                                                                                                <td class="ControlDrp3" style="width: 25%">
                                                                                                                    <asp:TextBox ID="txtAmount3" runat="server" MaxLength="20" CssClass="cssTextBox"
                                                                                                                        Width="97%" OnTextChanged="txtRAmount_TextChanged" AutoPostBack="true" TabIndex="14"></asp:TextBox>
                                                                                                                    <cc1:FilteredTextBoxExtender ID="ftAmount3" runat="server" TargetControlID="txtAmount3"
                                                                                                                        FilterType="Custom,Numbers" Enabled="True" ValidChars="." />
                                                                                                                </td>
                                                                                                                <td style="width: 5%">
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td style="width: 13%">
                                                                                                                </td>
                                                                                                                <td style="width: 25%">
                                                                                                                </td>
                                                                                                                <td style="width: 25%; text-align: center" class="ControlLabel">
                                                                                                                    Cash Amount
                                                                                                                </td>
                                                                                                                <td style="width: 25%" class="ControlDrp3">
                                                                                                                    <asp:TextBox ID="txtCashAmount" CssClass="cssTextBox" Width="97%" TabIndex="18" runat="server"
                                                                                                                        OnTextChanged="txtRAmount_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                                                                    <cc1:FilteredTextBoxExtender ID="ftCash" runat="server" TargetControlID="txtCashAmount"
                                                                                                                        FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                                                                                                </td>
                                                                                                                <td style="width: 5%">
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td style="width: 13%">
                                                                                                                </td>
                                                                                                                <td style="width: 25%">
                                                                                                                </td>
                                                                                                                <td style="width: 25%; text-align: center">
                                                                                                                </td>
                                                                                                                <td style="width: 25%; font-weight: bold; display: none" class="tblLeft allPad">
                                                                                                                    <asp:Label ID="lblReceivedTotal" runat="server"></asp:Label>
                                                                                                                </td>
                                                                                                                <td style="width: 5%">
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </div>
                                                                                                    <div style=" text-align: center" id="divListMPayments" runat="server" align="center">
                                                                                                        <asp:GridView ID="GrdViewReceipt" runat="server" AllowSorting="false" AutoGenerateColumns="False"
                                                                                                            OnRowCreated="GrdViewReceipt_RowCreated" Width="99%" AllowPaging="True" DataKeyNames="TransNo"
                                                                                                            EmptyDataText="No Customer Receipts found!">
                                                                                                            <EmptyDataRowStyle CssClass="GrdContent" />
                                                                                                            <Columns>
                                                                                                                <asp:BoundField DataField="TransNo" HeaderText="Trans. No." HeaderStyle-Wrap="false" />
                                                                                                                <asp:BoundField DataField="RefNo" HeaderText="Ref. No." HeaderStyle-Wrap="false" />
                                                                                                                <asp:BoundField DataField="TransDate" HeaderText="Transaction Date" HeaderStyle-Wrap="false"
                                                                                                                    DataFormatString="{0:dd/MM/yyyy}" />
                                                                                                                <asp:BoundField DataField="Debi" HeaderText="Bank Name / Cash" HeaderStyle-Wrap="false" />
                                                                                                                <asp:BoundField DataField="Amount" HeaderText="Amount" HeaderStyle-Wrap="false" />
                                                                                                                <asp:BoundField DataField="Narration" HeaderText="Narration" HeaderStyle-Wrap="false" />
                                                                                                            </Columns>
                                                                                                            <PagerTemplate>
                                                                                                                Goto Page
                                                                                                                <asp:DropDownList ID="ddlPageSelector" runat="server" AutoPostBack="true" SkinID="skinPagerDdlBox">
                                                                                                                </asp:DropDownList>
                                                                                                                <asp:Button Text="First" CommandName="Page" CommandArgument="First" runat="server"
                                                                                                                    ID="btnFirst" />
                                                                                                                <asp:Button Text="Previous" CommandName="Page" CommandArgument="Prev" runat="server"
                                                                                                                    ID="btnPrevious" />
                                                                                                                <asp:Button Text="Next" CommandName="Page" CommandArgument="Next" runat="server"
                                                                                                                    ID="btnNext" />
                                                                                                                <asp:Button Text="Last" CommandName="Page" CommandArgument="Last" runat="server"
                                                                                                                    ID="btnLast" />
                                                                                                            </PagerTemplate>
                                                                                                        </asp:GridView>
                                                                                                        <br />
                                                                                                    </div>
                                                                                                    <br />
                                                                                                </div>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
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
                    </td>
                </tr>
                <tr style="width: 100%">
                    <td style="width: 100%;">
                        <asp:Panel ID="PanelBill" Direction="LeftToRight" runat="server">
                            <table width="100%" style="margin: -6px 0px 0px 0px;">
                                <tr style="width: 100%">
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanelSalesGrid" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:GridView ID="GrdViewCommission" runat="server" AllowSorting="false" AutoGenerateColumns="False"
                                                    Width="100%" DataKeyNames="CommissionNo" AllowPaging="True" EmptyDataText="No Commission Details Found"
                                                    OnRowCreated="GrdViewCommission_RowCreated" OnRowDataBound="GrdViewCommission_RowDataBound"
                                                    OnSelectedIndexChanged="GrdViewCommission_SelectedIndexChanged" OnPageIndexChanging="GrdViewCommission_PageIndexChanging"
                                                    OnRowDeleting="GrdViewCommission_RowDeleting" CssClass="someClass">
                                                    <Columns>
                                                        <asp:BoundField DataField="CommissionNo" HeaderText="CommissionNo" SortExpression="Commission No"
                                                            HeaderStyle-Wrap="false"  HeaderStyle-BorderColor="Gray"/>
                                                        <asp:BoundField DataField="CommissionDate" SortExpression="Commission Date" HeaderText="Commission Date"  HeaderStyle-BorderColor="Gray"/>
                                                        <asp:BoundField DataField="CustomerName" SortExpression="Customer Name" HeaderText="Customer Name"  HeaderStyle-BorderColor="Gray" Visible="False" />
                                                        <asp:BoundField DataField="subname" SortExpression="Supplier Name" HeaderText="Supplier Name"  HeaderStyle-BorderColor="Gray"/>
                                                        <asp:BoundField DataField="freight" SortExpression="freight" HeaderText="Freight"  HeaderStyle-BorderColor="Gray"/>
                                                        <asp:BoundField DataField="loadunload" SortExpression="loadunload" HeaderText="LoadUnload"  HeaderStyle-BorderColor="Gray"/>
                                                        <asp:BoundField DataField="Comissionvalue" SortExpression="Commission value" HeaderText="Commission Value"  HeaderStyle-BorderColor="Gray"/>
                                                        <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="45px" HeaderText="Edit" HeaderStyle-BorderColor="Gray">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Select" />
                                                                <asp:ImageButton ID="btnEditDisabled" Enabled="false" SkinID="editDisable" runat="Server"></asp:ImageButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-Width="45px" HeaderText="Print" HeaderStyle-BorderColor="Gray">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <a href='<%# DataBinder.Eval(Container, "DataItem.CommissionNo", "javascript:PrintItem({0});") %>'>
                                                                    <img alt="Print" border="0" src="App_Themes/DefaultTheme/Images/Print1.png">
                                                                </a>
                                                                <asp:ImageButton ID="btnViewDisabled" Enabled="false" SkinID="search" runat="Server"></asp:ImageButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="45px" HeaderText="Delete" HeaderStyle-BorderColor="Gray">
                                                            <ItemStyle Width="45px" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to Delete this Commission Entry?"
                                                                    runat="server">
                                                                </cc1:ConfirmButtonExtender>
                                                                <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" CommandName="Delete"></asp:ImageButton>
                                                                <asp:ImageButton ID="lnkBDisabled" Enabled="false" SkinID="deleteDisable" runat="Server"></asp:ImageButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerTemplate>
                                                        <table style=" border-color:white">
                                                        <tr style="height:1px">
                                                        </tr>
                                                            <tr style=" border-color:white">
                                                                <td style=" border-color:white">
                                                                    Goto Page
                                                                </td> 
                                                                <td style=" border-color:white">
                                                                    <asp:DropDownList ID="ddlPageSelector" OnSelectedIndexChanged="ddlPageSelector_SelectedIndexChanged"
                                                                        runat="server" AutoPostBack="true" Width="70px" style="border:1px solid blue" height="23px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style=" border-color:white; Width:5px">
                                            
                                                                </td>
                                                                <td style=" border-color:white">
                                                                    <asp:Button Text="" CommandName="Page" CommandArgument="First" runat="server" CssClass="NewFirst" EnableTheming="false"
                                                                        ID="btnFirst" Width="22px" Height="18px"/>
                                                                </td>
                                                                <td style=" border-color:white">
                                                                    <asp:Button Text="" CommandName="Page" CommandArgument="Prev" runat="server" CssClass="NewPrev" EnableTheming="false"
                                                                        ID="btnPrevious" Width="22px" Height="18px"/>
                                                                </td>
                                                                <td style=" border-color:white">
                                                                    <asp:Button Text="" CommandName="Page" CommandArgument="Next" runat="server" CssClass="NewNext" EnableTheming="false"
                                                                        ID="btnNext" Width="22px" Height="18px"/>
                                                                </td>
                                                                <td style=" border-color:white">
                                                                    <asp:Button Text="" CommandName="Page" CommandArgument="Last" runat="server" CssClass="NewLast" EnableTheming="false"
                                                                        ID="btnLast" Width="22px" Height="18px"/>
                                                                </td>
                                                                <td style=" border-color:white;Width:63%">
                                            
                                                                </td>
                                                                <td style=" border-color:white">
                                                                    <asp:Button ID="Button2" runat="server" CssClass="exportExcel" OnClientClick="window.open('ReportXlCommission.aspx','CSTSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=310,width=450,left=350,top=100, scrollbars=yes');"
                                                                           EnableTheming="false" Width="130px"></asp:Button>
                                                                </td>
                                                            </tr>
                                                            <tr style="height:1px">
                                                            </tr>
                                                        </table>
                                                    </PagerTemplate>
                                                </asp:GridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </asp:Panel>
                    </td>
                </tr>
                <tr style="width: 100%">
                    <td style="width: 100%">
                        <asp:HiddenField ID="hdSMS" runat="server" Value="NO" />
                        <asp:HiddenField ID="hdSMSRequired" runat="server" Value="NO" />
                        <asp:Button ID="cmdDelete" runat="server" Enabled="false" Visible="false" SkinID="skinBtnDelete"
                            Text="Delete" ValidationGroup="salesval" />
                        <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="cmdDelete" ConfirmText="Are you sure you want to Delete this Sales?"
                            runat="server">
                        </cc1:ConfirmButtonExtender>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <table align="center">
        <tr>
            <td >
                <asp:Button ID="btnComison" runat="server"
                CssClass="exportexl6" EnableTheming="false" CausesValidation="false"
                 OnClientClick="window.open('ReportXlCommission.aspx','CSTSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=310,width=500,left=425,top=220, scrollbars=yes');"></asp:Button>
           </td>
        </tr>
     </table>
</asp:Content>
