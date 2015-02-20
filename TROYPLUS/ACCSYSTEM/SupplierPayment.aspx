<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="SupplierPayment.aspx.cs" Inherits="SupplierPayment" Title="Supplier > Supplier Payment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
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

                    $addHandler(tab.get_headerTab(), 'mouseover', Function.createDelegate(tab, function () {
                        tabContainer.set_activeTab(this);
                    }));
                }
            }
        }

        function PrintItem(ID) {
            window.showModalDialog('./PrintPayment.aspx?ID=' + ID, self, 'dialogWidth:700px;dialogHeight:430px;status:no;dialogHide:yes;unadorned:yes;');
        }

        function AdvancedTest(id) {
            var panel = document.getElementById('ctl00_cplhControlPanel_frmViewAdd_tabEdit_tabEditMain_tblBank');
            var adv = document.getElementById('ctl00_cplhControlPanel_hidAdvancedState');
            var grd = document.getElementById("<%= frmViewAdd.ClientID %>");

            var rdoArray = grd.getElementsByTagName("input");

            for (i = 0; i <= rdoArray.length - 1; i++) {
                if (rdoArray[i].type == 'radio') {
                    if (rdoArray[i].value == 'Cash' && rdoArray[i].checked == true) {
                        panel.className = "hidden";
                        adv.value = panel.className;
                    }
                    else if (rdoArray[i].value == 'Cheque' && rdoArray[i].checked == true) {
                        panel.className = "AdvancedSearch";
                        adv.value = panel.className;
                    }
                }
            }
        }

        function AdvancedAdd(id) {
            var panel = document.getElementById('ctl00_cplhControlPanel_frmViewAdd_tablInsert_tabInsMain_tblBankAdd');
            var adv = document.getElementById('ctl00_cplhControlPanel_hidAdvancedState');
            var grd = document.getElementById("<%= frmViewAdd.ClientID %>");

            var rdoArray = grd.getElementsByTagName("input");

            for (i = 0; i <= rdoArray.length - 1; i++) {
                if (rdoArray[i].type == 'radio') {
                    if (rdoArray[i].value == 'Cash' && rdoArray[i].checked == true) {
                        panel.className = "hidden";
                        adv.value = panel.className;
                    }
                    else if (rdoArray[i].value == 'Cheque' && rdoArray[i].checked == true) {
                        panel.className = "AdvancedSearch";
                        adv.value = panel.className;
                    }
                }
            }
        }

        function Advanced() {
            var table = document.getElementById('tblBank');
            var adv = document.getElementById('ctl00_cplhControlPanel_hidAdvancedState');

            var tr = table.getElementsByTagName('tr');

            for (i = 0; i < tr.length; i++) {
                if (tr[i].className == "AdvancedSearch") {
                    tr[i].className = "hidden";
                    adv.value = tr[i].className;
                }
                else if (tr[i].className == "hidden") {
                    tr[i].className = "AdvancedSearch";
                    adv.value = tr[i].className;
                }
            }
        } 
    </script>
    <asp:UpdatePanel ID="UpdatePanel16" runat="server" UpdateMode="Always">
        <ContentTemplate>
        
            <table style="width: 100%">
                <tr style="width: 100%">
                    <td style="width: 100%">
                    
                            <div class="mainConHd">
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tr valign="middle">
                                        <td>
                                            <span>Supplier Payments</span>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="mainConBody">
                                <table style="width: 100%;" cellpadding="3" cellspacing="0" class="searchbg">
                                    <tr style="height: 25px; vertical-align: middle">
                                        <td style="width: 10%">

                                        </td>
                                        <td style="width: 15%">
                                            <div style="text-align: right;">
                                                <asp:Panel ID="pnlSearch" runat="server" Width="100px">
                                                    <asp:Button ID="lnkBtnAdd" runat="server" OnClick="lnkBtnAdd_Click" CssClass="ButtonAdd"
                                                        EnableTheming="false" Width="80px" Text="Add New"></asp:Button>
                                                </asp:Panel>
                                            </div>
                                        </td>
                                        <td style="width: 10%" align="right">
                                            Search
                                            <asp:RequiredFieldValidator ID="rvSearch" runat="server" ControlToValidate="txtSearch"
                                                Display="Dynamic" EnableClientScript="False" Enabled="false">Search is mandatory</asp:RequiredFieldValidator>
                                        </td>
                                        <td style="width: 22%" class="NewBox">
                                            <asp:TextBox ID="txtSearch" runat="server" SkinID="skinTxtBoxSearch"></asp:TextBox>
                                        </td>
                                        <td style="width: 22%" class="NewBox">
                                            <div style="width: 160px; font-family: 'Trebuchet MS';">
                                                <asp:DropDownList ID="ddCriteria" runat="server" Width="154px" Height="23px" style="text-align:center;border:1px solid White ">
                                                    <asp:ListItem Value="TransNo">Trans. No.</asp:ListItem>
                                                    <asp:ListItem Value="RefNo">Ref. No.</asp:ListItem>
                                                    <asp:ListItem Value="TransDate">Transaction Date</asp:ListItem>
                                                    <asp:ListItem Value="LedgerName">Paid To</asp:ListItem>
                                                    <asp:ListItem Value="Narration">Narration</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </td>
                                        <td style="width: 25%" class="tblLeftNoPad">
                                            <asp:Button ID="btnSearch" runat="server" Text="Search" SkinID="skinBtnSearch" />
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
                        <asp:Panel runat="server" ID="popUp" Style="width: 60%">
                            <div id="contentPopUp">
                                <table class="tblLeft" cellpadding="0" cellspacing="0" style="border: 0px solid #5078B3;
                                    background-color: #fff; color: #000;" width="100%">
                                    <tr>
                                        <td>
                                            <asp:FormView ID="frmViewAdd" runat="server" Width="100%" DataSourceID="frmSource"
                                                OnItemCommand="frmViewAdd_ItemCommand" DefaultMode="Edit" DataKeyNames="TransNo"
                                                OnItemUpdated="frmViewAdd_ItemUpdated" OnItemCreated="frmViewAdd_ItemCreated"
                                                Visible="False" OnItemInserting="frmViewAdd_ItemInserting" EmptyDataText="No Records"
                                                OnItemInserted="frmViewAdd_ItemInserted">
                                                <RowStyle HorizontalAlign="left" CssClass="GrdContent allPad" VerticalAlign="Middle"
                                                    BorderColor="#cccccc" Height="20px" />
                                                <EditRowStyle HorizontalAlign="left" CssClass="GrdAlternateColor allPad" BorderColor="#cccccc"
                                                    VerticalAlign="middle" Height="20px" />
                                                <HeaderStyle HorizontalAlign="left" CssClass="GrdHeaderbgClr GrdHdrContent allPad"
                                                    Height="25px" BorderColor="#cccccc" VerticalAlign="Middle" />
                                                <EditItemTemplate>
                                                    <div class="divArea">
                                                        <table cellpadding="1" cellspacing="3" style="border: 1px solid #86b2d1; width: 100%;">
                                                            <tr>
                                                                <td colspan="4" class="headerPopUp">
                                                                    Supplier Payment
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4" align="left">
                                                                    <cc1:TabContainer ID="tabEdit" runat="server" Width="100%" ActiveTabIndex="0">
                                                                        <cc1:TabPanel ID="tabEditMain" runat="server" HeaderText="Payment Details">
                                                                            <ContentTemplate>
                                                                                <table style="width: 800px; border: 0px solid #86b2d1; vertical-align: text-top" align="center" cellpadding="3"
                                                                                    cellspacing="1">
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width:25%">
                                                                                            Ref. No. *
                                                                                            <asp:RequiredFieldValidator ID="rvRefNo" runat="server" ControlToValidate="txtRefNo"
                                                                                                Text="*" ErrorMessage="Ref. No. is mandatory" CssClass="rfv" Display="Dynamic"
                                                                                                EnableClientScript="True"></asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width:25%">
                                                                                            <asp:TextBox ID="txtRefNo" runat="server" Text='<%# Bind("RefNo") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                        </td>
                                                                                        <td class="ControlLabel" style="width:15%">
                                                                                            <asp:RequiredFieldValidator ID="rvStock" runat="server" ControlToValidate="txtTransDate"
                                                                                                Text="*" ErrorMessage="Ref Date is mandatory" Display="Dynamic" EnableClientScript="True"></asp:RequiredFieldValidator>
                                                                                            <asp:CompareValidator ControlToValidate="txtTransDate" Operator="DataTypeCheck" Type="Date"
                                                                                                Text="*" ErrorMessage="Please enter a valid date" runat="server" ID="cmpValtxtDate"></asp:CompareValidator>
                                                                                            <asp:RangeValidator ID="myRangeValidator" runat="server" ControlToValidate="txtTransDate"
                                                                                                ErrorMessage="Payment date cannot be future date." Text="*" Type="Date"></asp:RangeValidator>
                                                                                            Date *
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width:25%">
                                                                                            <asp:TextBox ID="txtTransDate" runat="server" Text='<%# Bind("TransDate","{0:dd/MM/yyyy}") %>'
                                                                                                CssClass="cssTextBox" Width="100px"></asp:TextBox>
                                                                                            <cc1:CalendarExtender ID="calExt3" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                                                PopupButtonID="btnDate3" PopupPosition="BottomLeft" TargetControlID="txtTransDate">
                                                                                            </cc1:CalendarExtender>
                                                                                            <td>
                                                                                                <asp:ImageButton ID="btnDate3" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                                                    Width="20px" runat="server" />
                                                                                            </td>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr style="height:3px">
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width:25%">
                                                                                            Paid To *
                                                                                            <asp:CompareValidator ID="cvPayedTo" runat="server" ControlToValidate="ComboBox2"
                                                                                                Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="Paid To is Mandatory"
                                                                                                Operator="GreaterThan" ValueToCompare="0"></asp:CompareValidator>
                                                                                        </td>
                                                                                        <td class="ControlDrpBorder" style="width:25%">
                                                                                            <asp:DropDownList ID="ComboBox2" runat="server" BackColor = "#90c9fc" CssClass="drpDownListMedium" width="100%"  style="border: 1px solid #90c9fc" height="26px"  AutoPostBack="False"
                                                                                                DataSourceID="srcCreditorDebitor" DataValueField="LedgerID" DataTextField="LedgerName"
                                                                                                AppendDataBoundItems="true" OnDataBound="ComboBox2_DataBound">
                                                                                                <asp:ListItem Text="Select Supplier" Value="0"></asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td class="ControlLabel" style="width:15%">
                                                                                            Amount *
                                                                                            <asp:RequiredFieldValidator ID="rvModel" runat="server" ControlToValidate="txtAmount"
                                                                                                ErrorMessage="Amount is mandatory" Display="Dynamic" EnableClientScript="True">*</asp:RequiredFieldValidator>
                                                                                            <cc1:FilteredTextBoxExtender ID="fltAmt" runat="server" TargetControlID="txtAmount"
                                                                                                ValidChars="." FilterType="Numbers, Custom" />
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width:25%">
                                                                                            <asp:TextBox ID="txtAmount" runat="server" Text='<%# Bind("Amount") %>' Width="100%"
                                                                                                SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr style="height:3px">
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width:25%">
                                                                                            Payment Made By *
                                                                                            <asp:RequiredFieldValidator ID="rvBDate" runat="server" ControlToValidate="chkPayTo"
                                                                                                Display="Dynamic" EnableClientScript="True" ErrorMessage="Item Name is mandatory.">*</asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width:25%">
                                                                                            <asp:RadioButtonList ID="chkPayTo" onclick="javascript:AdvancedTest(this.id);" runat="server"
                                                                                                AutoPostBack="false" Width="100%" OnDataBound="chkPayTo_DataBound" OnSelectedIndexChanged="chkPayTo_SelectedIndexChanged">
                                                                                                <asp:ListItem Text="Cash" Selected="true"></asp:ListItem>
                                                                                                <asp:ListItem Text="Cheque"></asp:ListItem>
                                                                                            </asp:RadioButtonList>
                                                                                        </td>
                                                                                        <td class="ControlLabel" style="width:15%">
                                                                                            Narration *
                                                                                            <asp:RequiredFieldValidator ID="rvNarration" runat="server" ControlToValidate="txtNarration"
                                                                                                ErrorMessage="Narration is mandatory" Display="Dynamic" EnableClientScript="True">*</asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width:25%">
                                                                                            <asp:TextBox ID="txtNarration" runat="server" Height="30px" TextMode="MultiLine"
                                                                                                Width="96%" Text='<%# Bind("Narration") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr style="height:3px">
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td colspan="4">
                                                                                            <asp:Panel ID="PanelBank" runat="server">
                                                                                                <table width="100%" id="tblBank" cellpadding="3" cellspacing="3" runat="server">
                                                                                                    <tr>
                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="ControlLabel" style="width:25%">
                                                                                                            Bank Name *
                                                                                                            <asp:CompareValidator ID="cvBank" runat="server" ControlToValidate="ddBanks" Display="Dynamic"
                                                                                                                EnableClientScript="false" ErrorMessage="Phase is Mandatory" Operator="GreaterThan"
                                                                                                                ValueToCompare="0">*</asp:CompareValidator>
                                                                                                        </td>
                                                                                                        <td class="ControlDrpBorder" style="width:25%">
                                                                                                            <asp:DropDownList ID="ddBanks" runat="server" OnDataBound="ddBanks_DataBound" DataSourceID="srcBanks" style="border: 1px solid #90c9fc" height="26px" 
                                                                                                                DataTextField="LedgerName" DataValueField="LedgerID" AppendDataBoundItems="True"
                                                                                                                BackColor = "#90c9fc" CssClass="drpDownListMedium" width="100%" >
                                                                                                                <asp:ListItem Selected="True" Value="0">-- Please Select --</asp:ListItem>
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                        <td class="ControlLabel" style="width:15.3%">
                                                                                                            Cheque No. *
                                                                                                            <asp:RequiredFieldValidator ID="rvChequeNo" runat="server" ControlToValidate="txtChequeNo"
                                                                                                                ErrorMessage="Cheque No. is mandatory" Display="Dynamic" EnableClientScript="false">*</asp:RequiredFieldValidator>
                                                                                                        </td>
                                                                                                        <td class="ControlTextBox3" style="width:25%">
                                                                                                            <asp:TextBox ID="txtChequeNo" runat="server" Text='<%# Bind("ChequeNo") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </asp:Panel>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </ContentTemplate>
                                                                        </cc1:TabPanel>
                                                                        <cc1:TabPanel ID="tabEditAddTab" runat="server" HeaderText="Additional Details">
                                                                            <ContentTemplate>
                                                                                <table align="center" cellpadding="0" cellspacing="0" style="border: 0px solid #5078B3;
                                                                                    width: 800px">
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 35%;">
                                                                                            Against Bill No.
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 25%;">
                                                                                            <asp:TextBox ID="txtBill" runat="server" Text='<%# Bind("BillNo") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                        </td>
                                                                                        <td style="width: 15%;">
                                                                                        </td>
                                                                                        <td style="width: 25%;">
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </ContentTemplate>
                                                                        </cc1:TabPanel>
                                                                    </cc1:TabContainer>
                                                                    <asp:ValidationSummary ID="valSum" DisplayMode="BulletList" ShowMessageBox="true"
                                                                        ShowSummary="false" HeaderText="Validation Messages" Font-Names="'Trebuchet MS'"
                                                                        Font-Size="12" runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr style="height:5px">
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 25%;">
                                                                </td>
                                                                <td align="right">
                                                                    <asp:Button ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                        CssClass="cancelbutton" EnableTheming="false" SkinID="skinBtnCancel" OnClick="UpdateCancelButton_Click">
                                                                    </asp:Button>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update"
                                                                        CssClass="savebutton" EnableTheming="false" SkinID="skinBtnSave" OnClick="UpdateButton_Click">
                                                                    </asp:Button>
                                                                </td>
                                                                <td style="width: 25%;">
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <table cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <asp:ObjectDataSource ID="srcBanks" runat="server" SelectMethod="ListBanks" TypeName="BusinessLogic">
                                                                    <SelectParameters>
                                                                        <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                    </SelectParameters>
                                                                </asp:ObjectDataSource>
                                                                <td>
                                                                    <asp:ObjectDataSource ID="srcCreditorDebitor" runat="server" SelectMethod="ListSundryCreditors"
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
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                                <InsertItemTemplate>
                                                    <div class="divArea">
                                                        <table cellpadding="0" cellspacing="2" style="border: 1px solid #86b2d1; width: 100%;">
                                                            <tr>
                                                                <td colspan="4" class="headerPopUp">
                                                                    Supplier Payment
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4" align="left">
                                                                    <cc1:TabContainer ID="tablInsert" runat="server" Width="100%" ActiveTabIndex="0">
                                                                        <cc1:TabPanel ID="tabInsMain" runat="server" HeaderText="Payment Details">
                                                                            <ContentTemplate>
                                                                                <table style="width: 800px;border: 0px solid #86b2d1; vertical-align: text-top" align="center" cellpadding="0"
                                                                                    cellspacing="1">
                                                                                    <%--<tr>
                                                                                        <td style="width:25%" class="ControlLabel">
                                                                                        </td>
                                                                                        <td style="width:25.2%">
                                                                                        </td>
                                                                                        <td style="width:15%" class="ControlLabel">
                                                                                        </td>
                                                                                        <td style="width:25%" class="ControlLabel">
                                                                                            
                                                                                        </td>
                                                                                    </tr>--%>
                                                                                    <%--<tr>
                                                                                        <td class="ControlLabel" style="width:25%">
                                                                                            
                                                                                        </td>
                                                                                        <td style="width:25%">
                                                                                            <asp:CheckBox ID="chkboxAll" runat="server" AutoPostBack="True" OnCheckedChanged="chkboxAll_CheckedChanged" Text="Copy from Old Entry" Font-Size="12px">
                                                                                            </asp:CheckBox>
                                                                                        </td>
                                                                                        <td colspan="2">
                                                                                            <table style="width:100%">
                                                                                                <asp:Panel ID="Panelcopy" runat="server" Visible="False">
                                                                                                    <tr>
                                                                                                        <td class="ControlLabel" style="width:15%">
                                                                                                            Old Ref No
                                                                                                        </td>
                                                                                                        <td class="ControlDrpBorder" style="width:25%">
                                                                                                            <asp:UpdatePanel ID="UpdatePanel21" runat="server" UpdateMode="Conditional">
                                                                                                                <ContentTemplate>
                                                                                                                    <asp:DropDownList ID="combooldrefno" runat="server"  style="border: 1px solid #90c9fc" height="26px"  CssClass="drpDownListMedium" BackColor = "#90c9fc" AutoPostBack="true"
                                                                                                                        DataValueField="TransNo" DataTextField="RefNo" Width="100%" DataSourceID="srcOldRefno"
                                                                                                                        AppendDataBoundItems="true" OnSelectedIndexChanged="combooldrefno_SelectedIndexChanged">
                                                                                                                        <asp:ListItem style="background-color: #90c9fc" Text="Select Ref No" Value="0"></asp:ListItem>
                                                                                                                    </asp:DropDownList>
                                                                                                                    </ContentTemplate>
                                                                                                                <Triggers>
                                                                                                                    <asp:AsyncPostBackTrigger ControlID="combooldrefno" EventName="SelectedIndexChanged" />
                                                                                                                </Triggers>
                                                                                                            </asp:UpdatePanel>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                
                                                                                                </asp:Panel>
                                                                                            </table>
                                                                                        </td>                      
                                                                                    </tr>--%>
                                                                                    <%--<tr style="height:3px">
                                                                                    </tr>--%>
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width:25%">
                                                                                            Ref. No. *
                                                                                            <asp:RequiredFieldValidator ID="rvRefNoAdd" runat="server" ControlToValidate="txtRefNoAdd"
                                                                                                ErrorMessage="Ref. No. is mandatory" Display="Dynamic" EnableClientScript="True">*</asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width:25%">
                                                                                            <asp:TextBox ID="txtRefNoAdd" runat="server" Text='<%# Bind("RefNo") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                        </td>
                                                                                        <td class="ControlLabel" style="width:15%">
                                                                                            <asp:RequiredFieldValidator ID="rvStockAdd" runat="server" ControlToValidate="txtTransDateAdd"
                                                                                                ErrorMessage="Trasaction Date is mandatory" Display="Dynamic" EnableClientScript="True">*</asp:RequiredFieldValidator>
                                                                                            <asp:CompareValidator ControlToValidate="txtTransDateAdd" Operator="DataTypeCheck"
                                                                                                Type="Date" ErrorMessage="Please enter a valid date" runat="server" ID="cmpValtxtDateAdd">*</asp:CompareValidator>
                                                                                            <asp:RangeValidator ID="myRangeValidatorAdd" runat="server" ControlToValidate="txtTransDateAdd"
                                                                                                ErrorMessage="Payment date cannot be future date." Text="*" Type="Date"></asp:RangeValidator>
                                                                                            Date *
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width:25%">
                                                                                            <asp:TextBox ID="txtTransDateAdd" runat="server" Text='<%# Bind("TransDate","{0:dd/MM/yyyy}") %>'
                                                                                                CssClass="cssTextBox" Width="100px"></asp:TextBox>
                                                                                            <cc1:CalendarExtender ID="calExtender3" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                                                PopupButtonID="btnDateAdd" PopupPosition="BottomLeft" TargetControlID="txtTransDateAdd">
                                                                                            </cc1:CalendarExtender>
                                                                                            <td>
                                                                                                <asp:ImageButton ID="btnDateAdd" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                                                    Width="20px" runat="server" />
                                                                                            </td>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr style="height:3px">
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width:25%">
                                                                                            Paid To *
                                                                                            <asp:CompareValidator ID="cvPayedToAdd" runat="server" ControlToValidate="ComboBox2Add"
                                                                                                Display="Dynamic" EnableClientScript="True" ErrorMessage="Paid To is Mandatory"
                                                                                                Operator="GreaterThan" ValueToCompare="0">*</asp:CompareValidator>
                                                                                        </td>
                                                                                        <td class="ControlDrpBorder" style="width:25%">
                                                                                            <asp:DropDownList ID="ComboBox2Add" runat="server" AutoPostBack="False"  style="border: 1px solid #90c9fc" height="26px"  BackColor = "#90c9fc" CssClass="drpDownListMedium" width="100%" 
                                                                                                DataSourceID="srcCreditorDebitorAdd" DataValueField="LedgerID" DataTextField="LedgerName"
                                                                                                AppendDataBoundItems="true">
                                                                                                <asp:ListItem Text="Select Supplier" Value="0"></asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td class="ControlLabel" style="width:15%">
                                                                                            Amount *
                                                                                            <asp:RequiredFieldValidator ID="rvModelAdd" runat="server" ControlToValidate="txtAmountAdd"
                                                                                                ErrorMessage="Amount is mandatory" Display="Dynamic" EnableClientScript="True">*</asp:RequiredFieldValidator>
                                                                                            <cc1:FilteredTextBoxExtender ID="fltAmtAdd" runat="server" TargetControlID="txtAmountAdd"
                                                                                                ValidChars="." FilterType="Numbers, Custom" />
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width:25%">
                                                                                            <asp:TextBox ID="txtAmountAdd" runat="server" Text='<%# Bind("Amount") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr style="height:3px">
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width:25%">
                                                                                            Payment Made By *
                                                                                            <asp:RequiredFieldValidator ID="rvBDateAdd" runat="server" ControlToValidate="chkPayToAdd"
                                                                                                Display="Dynamic" EnableClientScript="True" ErrorMessage="Item Name is mandatory.">*</asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width:25%">
                                                                                            <asp:RadioButtonList ID="chkPayToAdd" runat="server" OnDataBound="chkPayToAdd_DataBound"
                                                                                                onclick="javascript:AdvancedAdd(this.id);" AutoPostBack="false" Width="100%"
                                                                                                OnSelectedIndexChanged="chkPayToAdd_SelectedIndexChanged">
                                                                                                <asp:ListItem Text="Cash" Selected="true"></asp:ListItem>
                                                                                                <asp:ListItem Text="Cheque"></asp:ListItem>
                                                                                            </asp:RadioButtonList>
                                                                                        </td>
                                                                                        <td class="ControlLabel" style="width:15%">
                                                                                            Narration *
                                                                                            <asp:RequiredFieldValidator ID="rvNarrationAdd" runat="server" ControlToValidate="txtNarrationAdd"
                                                                                                ErrorMessage="Narration is mandatory" Display="Dynamic" EnableClientScript="True">*</asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width:25%">
                                                                                            <asp:TextBox ID="txtNarrationAdd" runat="server" Height="30px" TextMode="MultiLine"
                                                                                                Text='<%# Bind("Narration") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr style="height:1px">
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td colspan="4">
                                                                                            <asp:Panel ID="PanelBankAdd" runat="server">
                                                                                                <table width="100%" id="tblBankAdd" cellpadding="0" cellspacing="1" runat="server">
                                                                                                    <tr>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="ControlLabel" style="width:25%">
                                                                                                            <asp:CompareValidator ID="cvBankAdd" runat="server" ControlToValidate="ddBanksAdd"
                                                                                                                Display="Dynamic" EnableClientScript="false" ErrorMessage="Bank is Mandatory"
                                                                                                                Operator="GreaterThan" ValueToCompare="0">*</asp:CompareValidator>
                                                                                                            Bank Name *
                                                                                                        </td>
                                                                                                        <td class="ControlDrpBorder" style="width:25%">
                                                                                                            <asp:DropDownList ID="ddBanksAdd" runat="server" SelectedValue='<%# Bind("CreditorID") %>'
                                                                                                                BackColor = "#90c9fc"  style="border: 1px solid #90c9fc" height="26px"  width="100%" CssClass="drpDownListMedium"  DataSourceID="srcBanksAdd" DataTextField="LedgerName" DataValueField="LedgerID"
                                                                                                                AppendDataBoundItems="True">
                                                                                                                <asp:ListItem Selected="True" Value="0">Select Bank</asp:ListItem>
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                        <td class="ControlLabel" style="width:15.4%">
                                                                                                            <asp:RequiredFieldValidator ID="rvChequeNoAdd" runat="server" ControlToValidate="txtChequeNoAdd"
                                                                                                                ErrorMessage="Cheque No. is mandatory" Display="Dynamic" EnableClientScript="false">*</asp:RequiredFieldValidator>
                                                                                                            Cheque No. *
                                                                                                        </td>
                                                                                                        <td class="ControlTextBox3" style="width:25%">
                                                                                                            <asp:TextBox ID="txtChequeNoAdd" runat="server" Text='<%# Bind("ChequeNo") %>' SkinID="skinTxtBoxGridBank"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </asp:Panel>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </ContentTemplate>
                                                                        </cc1:TabPanel>
                                                                        <cc1:TabPanel ID="tabInsAddTab" runat="server" HeaderText="Additional Details">
                                                                            <ContentTemplate>
                                                                                <table align="center" cellpadding="3" cellspacing="5" style="border: 0px solid #5078B3;
                                                                                    width: 800px;">
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width:35%">
                                                                                            Against Bill No.
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width:25%">
                                                                                            <asp:TextBox ID="txtBillAdd" runat="server" Text='<%# Bind("BillNo") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                        </td>
                                                                                        <td style="width: 15%;">
                                                                                        </td>
                                                                                        <td style="width: 25%;">
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </ContentTemplate>
                                                                        </cc1:TabPanel>
                                                                    </cc1:TabContainer>
                                                                    <asp:ValidationSummary ID="valSum" DisplayMode="BulletList" ShowMessageBox="true"
                                                                        ShowSummary="false" HeaderText="Validation Messages" Font-Names="'Trebuchet MS'"
                                                                        Font-Size="12" runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr style="height:5px">
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4">
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td align="right" style="width: 15%;">
                                                                            </td>
                                                                            <td align="right" style="width: 25%;">
                                                                                <asp:Button ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                    CssClass="cancelbutton" EnableTheming="false" SkinID="skinBtnCancel" OnClick="InsertCancelButton_Click">
                                                                                </asp:Button>
                                                                            </td>
                                                                            <td align="center" style="width: 20%;">
                                                                                <asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                                                                    CssClass="savebutton" EnableTheming="false" SkinID="skinBtnSave" OnClick="InsertButton_Click">
                                                                                </asp:Button>
                                                                            </td>
                                                                            <td align="left" style="width: 40%;">
                                                                                <asp:Button ID="GetRefNo" runat="server" OnClick="GetRefNo_Click" CssClass="AddGetRefNo" CausesValidation="False"
                                                                                    EnableTheming="false" Text=""></asp:Button>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <table cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <asp:ObjectDataSource ID="srcCreditorDebitorAdd" runat="server" SelectMethod="ListSundryCreditors"
                                                                    TypeName="BusinessLogic">
                                                                    <SelectParameters>
                                                                        <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                    </SelectParameters>
                                                                </asp:ObjectDataSource>
                                                            </td>
                                                            <td>
                                                                <asp:ObjectDataSource ID="srcBanksAdd" runat="server" SelectMethod="ListBanks" TypeName="BusinessLogic">
                                                                    <SelectParameters>
                                                                        <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                    </SelectParameters>
                                                                </asp:ObjectDataSource>
                                                            </td>
                                                            <td>
                                                                <asp:ObjectDataSource ID="srcOldRefno" runat="server" SelectMethod="ListSupplierPaymentsRef"
                                                                    TypeName="BusinessLogic">
                                                                    <SelectParameters>
                                                                        <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                    </SelectParameters>
                                                                </asp:ObjectDataSource>
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </InsertItemTemplate>
                                            </asp:FormView>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div>
                                <table>
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
                                            <asp:Panel ID="pnlContact" runat="server" Width="700px" CssClass="modalPopup">
                                                <asp:UpdatePanel ID="updatePnlContact" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Panel ID="pnlContactItems" CssClass="pnlPopUp" runat="server">
                                                            <div id="Div1">
                                                                <table class="tblLeft" cellpadding="3" cellspacing="2" width="100%">
                                                                    <tr>
                                                                        <td style="width:5px;">
                                                                        </td>
                                                                        <td>
                                                                            <table class="headerPopUp" width="100%">
                                                                                <tr>
                                                                                    <td>
                                                                                        Get RefNo
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td style="width:5px;">
                                                                        </td>
                                                                    </tr>
                                                                                                                
                                                                    <tr>
                                                                        <td style="width:5px;">
                                                                            <asp:HiddenField ID="hdCurrentRow" runat="server" Value="0" />
                                                                        </td>
                                                                        <td>
                                                                            <table style="width: 100%; border: 1px solid #86b2d1" cellpadding="3" cellspacing="1">
                                                                                <tr style="height:1px">
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 32%" class="ControlLabel">
                                                                                        <asp:RequiredFieldValidator ID="rvDate" runat="server" ControlToValidate="txtContactedDate"
                                                                                                                                        ErrorMessage="RefNo is mandatory" Text="*" ValidationGroup="contact" />
                                                                                        Old RefNo
                                                                                    </td>
                                                                                    <td style="width: 27%" class="ControlTextBox3">
                                                                                        <asp:TextBox ID="txtContactedDate" runat="server" CssClass="cssTextBox" ValidationGroup="contact" Width="70px" Height="23px" BackColor = "#90c9fc" ></asp:TextBox>
                                                                                    </td>
                                                                                    <td style="width: 35%" class="ControlLabel">
                                                                                        
                                                                                    </td>
                                                                                    
                                                                                </tr>
                                                                                                                            
                                                                            </table>
                                                                        </td>
                                                                        <td style="width:5px;">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width:5px;">
                                                                        </td>
                                                                        <td>
                                                                            <table class="tblLeft" cellpadding="1" cellspacing="2"
                                                                                width="100%">
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        <table style="border: 1px solid #86b2d1">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Panel ID="Panel2" runat="server" Width="120px" Height="32px">
                                                                                                        <asp:Button ID="cmdCancelContact" runat="server" CssClass="CloseWindow" Height="45px" OnClick="cmdCancelContact_Click" CausesValidation="false"
                                                                                                            EnableTheming="false"/>
                                                                                                    </asp:Panel>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Panel ID="Panel3" runat="server" Width="120px" Height="32px">
                                                                                                        <asp:Button ID="cmdSaveContact" runat="server" CssClass="AddGetRefNos" 
                                                                                                            EnableTheming="false" OnClick="cmdSaveContact_Click" Text="" Height="45px" 
                                                                                                            ValidationGroup="contact" />
                                                                                                                                                    
                                                                                                        <asp:Button ID="cmdUpdateContact" runat="server" CssClass="UpGetRefNos" 
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
                                                                        <td style="width:5px;">
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
                            </div>
                        </asp:Panel>
                    </td>
                </tr>
                <tr style="width: 100%">
                    <td style="width: 100%">
                    <table width="100%" style="margin: -3px 0px 0px 0px;">
                                <tr style="width: 100%">
                                    <td>
                        <div class="mainGridHold" id="searchGrid">
                            <asp:GridView ID="GrdViewPayment" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                OnRowCreated="GrdViewPayment_RowCreated" Width="99.9%" DataSourceID="GridSource"
                                AllowPaging="True" DataKeyNames="TransNo" EmptyDataText="No Supplier Payments found!"
                                OnRowCommand="GrdViewPayment_RowCommand" OnRowDataBound="GrdViewPayment_RowDataBound"
                                OnSelectedIndexChanged="GrdViewPayment_SelectedIndexChanged" OnRowDeleting="GrdViewPayment_RowDeleting"
                                OnRowDeleted="GrdViewPayment_RowDeleted">
                                <EmptyDataRowStyle CssClass="GrdContent" />
                                <Columns>
                                    <asp:BoundField DataField="TransNo" HeaderStyle-Wrap="false" HeaderText="Trans. No." />
                                    <asp:BoundField DataField="RefNo" HeaderStyle-Wrap="false" HeaderText="Ref. No." />
                                    <asp:BoundField DataField="TransDate" HeaderStyle-Wrap="false" HeaderText="Trans. Date"
                                        DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField DataField="Debi" HeaderText="Paid To" HeaderStyle-Wrap="false" />
                                    <asp:BoundField DataField="LedgerName" HeaderText="Bank Name / Cash" HeaderStyle-Wrap="false" />
                                    <asp:BoundField DataField="Amount" HeaderText="Amount" HeaderStyle-Wrap="false" />
                                    <asp:BoundField DataField="Narration" HeaderText="Narration" HeaderStyle-Wrap="false" />
                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderText="Edit" HeaderStyle-Width="50px">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Select" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderText="Delete" HeaderStyle-Width="50px">
                                        <ItemTemplate>
                                            <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to Delete this Payment?"
                                                runat="server">
                                            </cc1:ConfirmButtonExtender>
                                            <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" CommandName="Delete"></asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="50px" HeaderText="Print">
                                        <ItemTemplate>
                                            <a href='<%# DataBinder.Eval(Container, "DataItem.TransNo", "javascript:PrintItem({0});") %>'>
                                                <img alt="Print" border="0" src="App_Themes/DefaultTheme/Images/Print.png">
                                            </a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerTemplate>
                                    <table>
                                        <tr>
                                            <td>
                                                Goto Page
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlPageSelector" runat="server" AutoPostBack="true" style="border:1px solid blue" Width="65px">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="Width:5px">
                                            
                                            </td>
                                            <td>
                                                <asp:Button Text="" CommandName="Page" CommandArgument="First" runat="server" CssClass="NewFirst" EnableTheming="false" Width="22px" Height="18px"
                                                    ID="btnFirst" />
                                            </td>
                                            <td>
                                                <asp:Button Text="" CommandName="Page" CommandArgument="Prev" runat="server" CssClass="NewPrev" EnableTheming="false" Width="22px" Height="18px"
                                                    ID="btnPrevious" />
                                            </td>
                                            <td>
                                                <asp:Button Text="" CommandName="Page" CommandArgument="Next" runat="server" CssClass="NewNext" EnableTheming="false" Width="22px" Height="18px"
                                                    ID="btnNext" />
                                            </td>
                                            <td>
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
                <tr>
                    <td style="width: 918px" align="left">
                        <asp:ObjectDataSource ID="GridSource" runat="server" SelectMethod="ListSupplierPayments"
                            TypeName="BusinessLogic" DeleteMethod="DeletePayment" OnDeleting="GridSource_Deleting">
                            <DeleteParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                <asp:Parameter Name="TransNo" Type="Int32" />
                                <asp:Parameter Name="requireValidation" Type="Boolean" DefaultValue="true" />
                                <asp:Parameter Name="Username" Type="String" />
                            </DeleteParameters>
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="frmSource" runat="server" SelectMethod="GetPaymentForId"
                            TypeName="BusinessLogic" InsertMethod="InsertPayment" OnUpdating="frmSource_Updating"
                            OnInserting="frmSource_Inserting" UpdateMethod="UpdatePaymentSupp" OnInserted="frmSource_Inserted"
                            OnUpdated="frmSource_Updated">
                            <UpdateParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                <asp:Parameter Name="TransNo" Type="Int32" />
                                <asp:Parameter Name="RefNo" Type="String" />
                                <asp:Parameter Name="TransDate" Type="DateTime" />
                                <asp:Parameter Name="DebitorID" Type="Int32" />
                                <asp:Parameter Name="CreditorID" Type="Int32" />
                                <asp:Parameter Name="Amount" Type="Double" />
                                <asp:Parameter Name="Narration" Type="String" />
                                <asp:Parameter Name="VoucherType" Type="String" />
                                <asp:Parameter Name="ChequeNo" Type="String" />
                                <asp:Parameter Name="Paymode" Type="String" />
                                <asp:Parameter Name="Billno" Type="String" />
                                <asp:Parameter Name="NewTransNo" Type="Int32" Direction="Output" />
                            </UpdateParameters>
                            <SelectParameters>
                                <asp:ControlParameter ControlID="GrdViewPayment" Name="TransNo" PropertyName="SelectedValue"
                                    Type="Int32" />
                                <asp:CookieParameter CookieName="Company" Type="String" Name="connection" />
                            </SelectParameters>
                            <InsertParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                <asp:Parameter Name="RefNo" Type="String" />
                                <asp:Parameter Name="TransDate" Type="DateTime" />
                                <asp:Parameter Name="DebitorID" Type="Int32" />
                                <asp:Parameter Name="CreditorID" Type="Int32" />
                                <asp:Parameter Name="Amount" Type="Double" />
                                <asp:Parameter Name="Narration" Type="String" />
                                <asp:Parameter Name="VoucherType" Type="String" />
                                <asp:Parameter Name="ChequeNo" Type="String" />
                                <asp:Parameter Name="PaymentMode" Type="String" />
                                <asp:Parameter Name="NewTransNo" Type="Int32" Direction="Output" />
                            </InsertParameters>
                        </asp:ObjectDataSource>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%">
                        <asp:HiddenField ID="hdPayment" runat="server" />
                    </td>
                </tr>
            </table>
            <input type="hidden" id="hidAdvancedState" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
