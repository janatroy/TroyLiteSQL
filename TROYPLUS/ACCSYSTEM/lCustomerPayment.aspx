<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="CustomerPayment.aspx.cs" Inherits="CustomerPayment" Title="Cusotmer > Customer Payment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <script language="javascript" type="text/javascript">

    function pageLoad(){
        //  get the behavior associated with the tab control
        var tabContainer = $find('ctl00_cplhControlPanel_frmViewAdd_tablInsert');
       
        if(tabContainer == null)
            tabContainer = $find('ctl00_cplhControlPanel_frmViewAdd_tabEdit');
        
        if(tabContainer != null)
        {
        //  get all of the tabs from the container
        var tabs = tabContainer.get_tabs();
        
        //  loop through each of the tabs and attach a handler to
        //  the tab header's mouseover event
        for(var i = 0; i < tabs.length; i++){
            var tab = tabs[i];
            
            $addHandler(
                tab.get_headerTab(),
                'mouseover',
                Function.createDelegate(tab, function(){
                    tabContainer.set_activeTab(this);             
                }
            ));
        }
        }
    }

function PrintItem(ID) 
{ 
  window.showModalDialog('./PrintPayment.aspx?ID=' + ID ,self,'dialogWidth:700px;dialogHeight:430px;status:no;dialogHide:yes;unadorned:yes;') ;
}

function OnKeyPress(args) {
    if (args.keyCode == Sys.UI.Key.esc) {
        $find("ctl00_cplhControlPanel_ModalPopupExtender1").hide();
    }
}

$("#ctl00_cplhControlPanel_frmViewAdd_tablInsert_tabInsMain_InsertCancelButton").live("click", function () {
    $find("ctl00_cplhControlPanel_ModalPopupExtender1").hide();
});

$("#ctl00_cplhControlPanel_frmViewAdd_tabEdit_tabEditMain_UpdateCancelButton").live("click", function () {
    $find("ctl00_cplhControlPanel_ModalPopupExtender1").hide();
});

function AdvancedTest(id) 
{ 
       
       var panel = document.getElementById('ctl00_cplhControlPanel_frmViewAdd_tabEdit_tabEditMain_tblBank');   
       var adv = document.getElementById('ctl00_cplhControlPanel_hidAdvancedState') ; 
       var grd = document.getElementById("<%= frmViewAdd.ClientID %>");  
       
       var rdoArray = grd.getElementsByTagName("input");  
       
       
       for(i=0;i<=rdoArray.length-1;i++)  
        {  
            //alert(rdoArray[i].type);
            if(rdoArray[i].type == 'radio')
            {
                
                if( rdoArray[i].value == 'Cash' && rdoArray[i].checked == true)
                {
                    panel.className = "hidden" ; 
                    adv.value = panel.className ; 
                }
                else if (rdoArray[i].value == 'Cheque' && rdoArray[i].checked == true) 
                { 
                    panel.className = "AdvancedSearch" ; 
                    adv.value = panel.className ; 
                }        
                
            }
        }  

} 

function AdvancedAdd(id) 
{ 
       
       var panel = document.getElementById('ctl00_cplhControlPanel_frmViewAdd_tablInsert_tabInsMain_tblBankAdd');   
       var adv = document.getElementById('ctl00_cplhControlPanel_hidAdvancedState') ; 
       var grd = document.getElementById("<%= frmViewAdd.ClientID %>");  
       
       var rdoArray = grd.getElementsByTagName("input");  
       
       
       for(i=0;i<=rdoArray.length-1;i++)  
        {  
            //alert(rdoArray[i].type);
            if(rdoArray[i].type == 'radio')
            {
                
                if( rdoArray[i].value == 'Cash' && rdoArray[i].checked == true)
                {
                    panel.className = "hidden" ; 
                    adv.value = panel.className ; 
                }
                else if (rdoArray[i].value == 'Cheque' && rdoArray[i].checked == true) 
                { 
                    panel.className = "AdvancedSearch" ; 
                    adv.value = panel.className ; 
                }        
                
            }
        }  
} 

<!-- 
function Advanced() 
{ 
        var table = document.getElementById('tblBank'); 
        var adv = document.getElementById('ctl00_cplhControlPanel_hidAdvancedState') ; 
        
        var tr = table.getElementsByTagName('tr') ; 
        
        for (i = 0; i < tr.length; i++) 
        { 
                if (tr[i].className == "AdvancedSearch") 
                { 
                        tr[i].className = "hidden" ; 
                        adv.value = tr[i].className ; 
                } 
                else if (tr[i].className == "hidden") 
                { 
                        tr[i].className = "AdvancedSearch" ; 
                        adv.value = tr[i].className ; 
                }                               
        } 
} 
//--> 
    </script>
    <asp:UpdatePanel ID="UpdatePanel16" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <table style="width: 100%">
                <tr style="width: 100%">
                    <td style="width: 100%">
                        <div class="mainConDiv" id="IdmainConDiv">
                            <div class="mainConHd">
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tr valign="middle">
                                        <td>
                                            <span>Customer Payment </span>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="mainConBody">
                                <div class="shadow1">
                                    &nbsp;
                                </div>
                                <div>
                                    <table cellspacing="2px" cellpadding="3px" border="0" width="100%" style="margin: 0px auto;">
                                        <tr style="height: 25px">
                                            <td style="width: 22%">
                                                <div style="text-align: right;">
                                                    <asp:Panel ID="pnlSearch" runat="server" Width="100px">
                                                        <asp:Button ID="lnkBtnAdd" runat="server" OnClick="lnkBtnAdd_Click" CssClass="ButtonAdd"
                                                            EnableTheming="false" Width="80px" Text="Add New"></asp:Button>
                                                    </asp:Panel>
                                                </div>
                                            </td>
                                            <td style="width: 10%" align="center">
                                                Search Text :
                                                <asp:RequiredFieldValidator ID="rvSearch" runat="server" ControlToValidate="txtSearch"
                                                    Display="Dynamic" EnableClientScript="False" Enabled="false">Search is mandatory</asp:RequiredFieldValidator>
                                            </td>
                                            <td style="width: 22%">
                                                <asp:TextBox ID="txtSearch" runat="server" SkinID="skinTxtBoxSearch"></asp:TextBox>
                                            </td>
                                            <td style="width: 22%">
                                                <div style="border-width: 1px; border-color: #bce1fe; border-style: solid; width: 160px;
                                                    font-family: 'Trebuchet MS';">
                                                    <asp:DropDownList ID="ddCriteria" runat="server" CssClass="cssDropDown" Width="99%">
                                                        <%--<asp:ListItem Value="All" style="background-color: #bce1fe">All</asp:ListItem>--%>
                                                        <asp:ListItem Value="TransNo">Trans No</asp:ListItem>
                                                        <asp:ListItem Value="RefNo">Ref No</asp:ListItem>
                                                        <asp:ListItem Value="TransDate">Transaction Date</asp:ListItem>
                                                        <asp:ListItem Value="LedgerName">Paid To</asp:ListItem>
                                                        <asp:ListItem Value="Narration">Narration</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </td>
                                            <td style="width: 25%; text-align: left">
                                                <asp:Button ID="btnSearch" runat="server" Text="Search" SkinID="skinBtnSearch" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
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
                                                        <table class="tblLeft" cellpadding="3" cellspacing="3" style="border: 1px solid #5078B3;"
                                                            width="100%">
                                                            <tr>
                                                                <td colspan="4">
                                                                    <table class="headerPopUp" style="border: 1px solid #86b2d1" width="100%">
                                                                        <tr>
                                                                            <td>
                                                                                Customer Payment
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <div style="text-align: left">
                                                                        <cc1:TabContainer ID="tabEdit" runat="server" Width="100%" ActiveTabIndex="0">
                                                                            <cc1:TabPanel ID="tabEditMain" runat="server" HeaderText="Payment Details">
                                                                                <ContentTemplate>
                                                                                    <table style="width: 100%; vertical-align: text-top" align="center" cellpadding="3"
                                                                                        cellspacing="3" style="border: 0px solid #86b2d1;">
                                                                                        <tr>
                                                                                            <td colspan="4">
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr style="height: 30px" class="tblLeft">
                                                                                            <td style="width: 25%" class="tblLeft leftCol">
                                                                                                Ref No: *
                                                                                                <asp:RequiredFieldValidator ID="rvRefNo" runat="server" ControlToValidate="txtRefNo"
                                                                                                    Text="*" ErrorMessage="Ref No is mandatory" CssClass="rfv" Display="Dynamic"
                                                                                                    EnableClientScript="True"></asp:RequiredFieldValidator>
                                                                                            </td>
                                                                                            <td style="width: 25%">
                                                                                                <asp:TextBox ID="txtRefNo" runat="server" Text='<%# Bind("RefNo") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                            </td>
                                                                                            <td style="width: 25%" class="tblLeft leftCol">
                                                                                                Date: *
                                                                                                <asp:RequiredFieldValidator ID="rvStock" runat="server" ControlToValidate="txtTransDate"
                                                                                                    Text="*" ErrorMessage="Ref Date is mandatory" Display="Dynamic" EnableClientScript="True"></asp:RequiredFieldValidator>
                                                                                                <asp:CompareValidator ControlToValidate="txtTransDate" Operator="DataTypeCheck" Type="Date"
                                                                                                    Text="*" ErrorMessage="Please enter a valid date" runat="server" ID="cmpValtxtDate"></asp:CompareValidator>
                                                                                                <asp:RangeValidator ID="myRangeValidator" runat="server" ControlToValidate="txtTransDate"
                                                                                                    ErrorMessage="Payment date cannot be future date." Text="*" Type="Date"></asp:RangeValidator>
                                                                                            </td>
                                                                                            <td style="width: 25%">
                                                                                                <asp:TextBox ID="txtTransDate" runat="server" Text='<%# Bind("TransDate","{0:dd/MM/yyyy}") %>'
                                                                                                    CssClass="cssTextBox" Width="100px"></asp:TextBox>
                                                                                                <cc1:CalendarExtender ID="calExtender3" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                                                    PopupButtonID="btnDate3" PopupPosition="BottomLeft" TargetControlID="txtTransDate">
                                                                                                </cc1:CalendarExtender>
                                                                                                <asp:ImageButton ID="btnDate3" ImageUrl="~/img/cal.gif" CausesValidation="false"
                                                                                                    Width="20px" runat="server" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr style="height: 30px" class="tblLeft">
                                                                                            <td style="width: 25%" class="tblLeft leftCol">
                                                                                                Paid To: *
                                                                                                <asp:CompareValidator ID="cvPayedTo" runat="server" ControlToValidate="ComboBox2"
                                                                                                    Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="Paid To is Mandatory"
                                                                                                    Operator="GreaterThan" ValueToCompare="0"></asp:CompareValidator>
                                                                                            </td>
                                                                                            <td style="width: 25%">
                                                                                                <div style="border-width: 1px; border-color: #bce1fe; border-style: solid; width: 100%;
                                                                                                    font-family: 'Trebuchet MS';">
                                                                                                    <asp:DropDownList ID="ComboBox2" runat="server" SkinID="skinDdlBox" AutoPostBack="False"
                                                                                                        DataSourceID="srcCreditorDebitor" DataValueField="LedgerID" DataTextField="LedgerName"
                                                                                                        AppendDataBoundItems="true" OnDataBound="ComboBox2_DataBound">
                                                                                                        <asp:ListItem style="background-color: #bce1fe" Text="Select Ledger" Value="0"></asp:ListItem>
                                                                                                    </asp:DropDownList>
                                                                                                </div>
                                                                                            </td>
                                                                                            <td style="width: 25%" class="tblLeft leftCol">
                                                                                                Amount: *<asp:RequiredFieldValidator ID="rvModel" runat="server" ControlToValidate="txtAmount"
                                                                                                    ErrorMessage="Amount is mandatory" Display="Dynamic" EnableClientScript="True">*</asp:RequiredFieldValidator>
                                                                                                <cc1:FilteredTextBoxExtender ID="fltAmt" runat="server" TargetControlID="txtAmount"
                                                                                                    ValidChars="." FilterType="Numbers, Custom" />
                                                                                            </td>
                                                                                            <td style="width: 25%">
                                                                                                <asp:TextBox ID="txtAmount" runat="server" Text='<%# Bind("Amount") %>' Width="100%"
                                                                                                    SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr style="height: 30px" class="tblLeft">
                                                                                            <td style="width: 25%; height: 34px;" class="tblLeft leftCol">
                                                                                                Payment Made By: *<asp:RequiredFieldValidator ID="rvBDate" runat="server" ControlToValidate="chkPayTo"
                                                                                                    Display="Dynamic" EnableClientScript="True" ErrorMessage="Item Name is mandatory.">*</asp:RequiredFieldValidator>
                                                                                            </td>
                                                                                            <td style="width: 25%; height: 34px;">
                                                                                                <asp:RadioButtonList ID="chkPayTo" onclick="javascript:AdvancedTest(this.id);" runat="server"
                                                                                                    AutoPostBack="false" Width="100%" OnDataBound="chkPayTo_DataBound" OnSelectedIndexChanged="chkPayTo_SelectedIndexChanged">
                                                                                                    <asp:ListItem Text="Cash" Selected="true"></asp:ListItem>
                                                                                                    <asp:ListItem Text="Cheque"></asp:ListItem>
                                                                                                </asp:RadioButtonList>
                                                                                            </td>
                                                                                            <td style="width: 25%" class="tblLeft leftCol">
                                                                                                Narration: *<asp:RequiredFieldValidator ID="rvNarration" runat="server" ControlToValidate="txtNarration"
                                                                                                    ErrorMessage="Narration is mandatory" Display="Dynamic" EnableClientScript="True">*</asp:RequiredFieldValidator>
                                                                                            </td>
                                                                                            <td style="width: 25%">
                                                                                                <asp:TextBox ID="txtNarration" runat="server" Height="30px" TextMode="MultiLine"
                                                                                                    Text='<%# Bind("Narration") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan="4" width="100%">
                                                                                                <asp:Panel ID="PanelBank" runat="server">
                                                                                                    <table width="100%" id="tblBank" cellpadding="0" cellspacing="0" runat="server">
                                                                                                        <tr>
                                                                                                            <td style="width: 25%;" class="tblLeft leftCol">
                                                                                                                Bank Name: *<asp:CompareValidator ID="cvBank" runat="server" ControlToValidate="ddBanks"
                                                                                                                    Display="Dynamic" EnableClientScript="false" ErrorMessage="Phase is Mandatory"
                                                                                                                    Operator="GreaterThan" ValueToCompare="0">*</asp:CompareValidator>
                                                                                                            </td>
                                                                                                            <td style="width: 25%">
                                                                                                                <div style="border-width: 1px; border-color: #bce1fe; border-style: solid; width: 100%;
                                                                                                                    font-family: 'Trebuchet MS';">
                                                                                                                    <asp:DropDownList ID="ddBanks" runat="server" OnDataBound="ddBanks_DataBound" DataSourceID="srcBanks"
                                                                                                                        DataTextField="LedgerName" DataValueField="LedgerID" AppendDataBoundItems="True"
                                                                                                                        SkinID="skinDdlBoxBank">
                                                                                                                        <asp:ListItem Selected="True" style="background-color: #bce1fe" Value="0">Select Bank</asp:ListItem>
                                                                                                                    </asp:DropDownList>
                                                                                                                </div>
                                                                                                            </td>
                                                                                                            <td style="width: 25%" class="tblLeft leftCol">
                                                                                                                Cheque No: *
                                                                                                                <asp:RequiredFieldValidator ID="rvChequeNo" runat="server" ControlToValidate="txtChequeNo"
                                                                                                                    ErrorMessage="Cheque No is mandatory" Display="Dynamic" EnableClientScript="false">*</asp:RequiredFieldValidator>
                                                                                                            </td>
                                                                                                            <td style="width: 25%;" class="tblLeftNoPad">
                                                                                                                <asp:TextBox ID="txtChequeNo" runat="server" Text='<%# Bind("ChequeNo") %>' SkinID="skinTxtBoxGridBank"></asp:TextBox>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </asp:Panel>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr style="height: 30px; text-align: center">
                                                                                            <td align="center" valign="bottom" colspan="4">
                                                                                                <table cellspacing="0">
                                                                                                    <tr width="100%">
                                                                                                        <td style="width: 67px" align="right">
                                                                                                            <asp:Button ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                                                SkinID="skinBtnCancel" Text="Cancel" OnClick="UpdateCancelButton_Click"></asp:Button>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                        </td>
                                                                                                        <td align="left">
                                                                                                            <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update"
                                                                                                                SkinID="skinBtnSave" Text="Update" OnClick="UpdateButton_Click"></asp:Button>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                            <td style="width: 25%">
                                                                                                <asp:ObjectDataSource ID="srcBanks" runat="server" SelectMethod="ListBanks" TypeName="BusinessLogic">
                                                                                                    <SelectParameters>
                                                                                                        <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                                                    </SelectParameters>
                                                                                                </asp:ObjectDataSource>
                                                                                            </td>
                                                                                            <td style="width: 25%">
                                                                                                <asp:ObjectDataSource ID="srcCreditorDebitor" runat="server" SelectMethod="ListSundryDebitors"
                                                                                                    TypeName="BusinessLogic">
                                                                                                    <SelectParameters>
                                                                                                        <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                                                    </SelectParameters>
                                                                                                </asp:ObjectDataSource>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </ContentTemplate>
                                                                            </cc1:TabPanel>
                                                                            <cc1:TabPanel ID="tabEditAddTab" runat="server" HeaderText="Additional Details">
                                                                                <ContentTemplate>
                                                                                    <table align="center" cellpadding="3" cellspacing="5" style="border: 0px solid #5078B3;
                                                                                        width: 100%;">
                                                                                        <tr style="height: 30px" class="tblLeft">
                                                                                            <td style="width: 25%" class="tblLeft leftCol">
                                                                                                Against Bill no:
                                                                                            </td>
                                                                                            <td style="width: 25%">
                                                                                                <asp:TextBox ID="txtBill" runat="server" Text='<%# Bind("BillNo") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                            </td>
                                                                                            <td style="width: 25%">
                                                                                            </td>
                                                                                            <td style="width: 25%">
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </ContentTemplate>
                                                                            </cc1:TabPanel>
                                                                        </cc1:TabContainer>
                                                                        <asp:ValidationSummary ID="valSum" DisplayMode="BulletList" ShowMessageBox="true"
                                                                            ShowSummary="false" HeaderText="Validation Messages" Font-Names="'Trebuchet MS'"
                                                                            Font-Size="12" runat="server" />
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                                <InsertItemTemplate>
                                                    <div class="divArea">
                                                        <table class="tblLeft" cellpadding="3" cellspacing="3" style="border: 1px solid #5078B3;"
                                                            width="100%">
                                                            <tr>
                                                                <td colspan="4">
                                                                    <table class="headerPopUp" style="border: 1px solid #86b2d1" width="100%">
                                                                        <tr>
                                                                            <td>
                                                                                Customer Payment
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <div style="text-align: left">
                                                                        <cc1:TabContainer ID="tablInsert" runat="server" Width="100%" ActiveTabIndex="0">
                                                                            <cc1:TabPanel ID="tabInsMain" runat="server" HeaderText="Payment Details">
                                                                                <ContentTemplate>
                                                                                    <table style="width: 100%; vertical-align: text-top" align="center" cellpadding="3"
                                                                                        cellspacing="3" style="border: 0px solid #86b2d1;">
                                                                                        <tr>
                                                                                            <td colspan="4">
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr style="height: 30px" class="tblLeft">
                                                                                            <td style="width: 25%" class="tblLeft leftCol">
                                                                                                Ref No: *
                                                                                                <asp:RequiredFieldValidator ID="rvRefNoAdd" runat="server" ControlToValidate="txtRefNoAdd"
                                                                                                    ErrorMessage="RefNo is mandatory" Display="Dynamic" EnableClientScript="True">*</asp:RequiredFieldValidator>
                                                                                            </td>
                                                                                            <td style="width: 25%">
                                                                                                <asp:TextBox ID="txtRefNoAdd" runat="server" Text='<%# Bind("RefNo") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                            </td>
                                                                                            <td style="width: 25%" class="tblLeft leftCol">
                                                                                                Date: *
                                                                                                <asp:RequiredFieldValidator ID="rvStockAdd" runat="server" ControlToValidate="txtTransDateAdd"
                                                                                                    ErrorMessage="Trasaction Date is mandatory" Display="Dynamic" EnableClientScript="True">*</asp:RequiredFieldValidator>
                                                                                                <asp:CompareValidator ControlToValidate="txtTransDateAdd" Operator="DataTypeCheck"
                                                                                                    Type="Date" ErrorMessage="Please enter a valid date" runat="server" ID="cmpValtxtDateAdd">*</asp:CompareValidator>
                                                                                                <asp:RangeValidator ID="myRangeValidatorAdd" runat="server" ControlToValidate="txtTransDateAdd"
                                                                                                    ErrorMessage="Payment date cannot be future date." Text="*" Type="Date"></asp:RangeValidator>
                                                                                            </td>
                                                                                            <td style="width: 25%">
                                                                                                <asp:TextBox ID="txtTransDateAdd" runat="server"  Text='<%# Bind("TransDate","{0:dd/MM/yyyy}") %>'
                                                                                                    CssClass="cssTextBox" Width="100px"></asp:TextBox>
                                                                                                <cc1:CalendarExtender ID="calExt3" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                                                    PopupButtonID="btnDateAdd" PopupPosition="BottomLeft" TargetControlID="txtTransDateAdd">
                                                                                                </cc1:CalendarExtender>
                                                                                                <asp:ImageButton ID="btnDateAdd" ImageUrl="~/img/cal.gif" CausesValidation="false"
                                                                                                    Width="20px" runat="server" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr style="height: 30px" class="tblLeft">
                                                                                            <td style="width: 25%" class="tblLeft leftCol">
                                                                                                Paid To: *
                                                                                                <asp:CompareValidator ID="cvPayedToAdd" runat="server" ControlToValidate="ComboBox2Add"
                                                                                                    Display="Dynamic" EnableClientScript="True" ErrorMessage="Paid To is Mandatory"
                                                                                                    Operator="GreaterThan" ValueToCompare="0">*</asp:CompareValidator>
                                                                                            </td>
                                                                                            <td style="width: 25%">
                                                                                                <div style="border-width: 1px; border-color: #bce1fe; border-style: solid; width: 100%;
                                                                                                    font-family: 'Trebuchet MS';">
                                                                                                    <asp:DropDownList ID="ComboBox2Add" runat="server" SkinID="skinDdlBox" AutoPostBack="False"
                                                                                                        DataSourceID="srcCreditorDebitorAdd" DataValueField="LedgerID" DataTextField="LedgerName"
                                                                                                        AppendDataBoundItems="true">
                                                                                                        <asp:ListItem style="background-color: #bce1fe" Text="Select Ledger" Value="0"></asp:ListItem>
                                                                                                    </asp:DropDownList>
                                                                                                </div>
                                                                                            </td>
                                                                                            <td style="width: 25%" class="tblLeft leftCol">
                                                                                                Amount: *<asp:RequiredFieldValidator ID="rvModelAdd" runat="server" ControlToValidate="txtAmountAdd"
                                                                                                    ErrorMessage="Amount is mandatory" Display="Dynamic" EnableClientScript="True">*</asp:RequiredFieldValidator>
                                                                                                <cc1:FilteredTextBoxExtender ID="fltAmtAdd" runat="server" TargetControlID="txtAmountAdd"
                                                                                                    ValidChars="." FilterType="Numbers, Custom" />
                                                                                            </td>
                                                                                            <td style="width: 25%">
                                                                                                <asp:TextBox ID="txtAmountAdd" runat="server" Text='<%# Bind("Amount") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="tblLeft">
                                                                                            <td style="width: 25%;" class="tblLeft leftCol">
                                                                                                Payment Made By: *<asp:RequiredFieldValidator ID="rvBDateAdd" runat="server" ControlToValidate="chkPayToAdd"
                                                                                                    Display="Dynamic" EnableClientScript="True" ErrorMessage="Item Name is mandatory.">*</asp:RequiredFieldValidator>
                                                                                            </td>
                                                                                            <td style="width: 25%; text-align: left">
                                                                                                <asp:RadioButtonList ID="chkPayToAdd" runat="server" OnDataBound="chkPayToAdd_DataBound"
                                                                                                    onclick="javascript:AdvancedAdd(this.id);" AutoPostBack="false" Width="100%"
                                                                                                    OnSelectedIndexChanged="chkPayToAdd_SelectedIndexChanged">
                                                                                                    <asp:ListItem Text="Cash" Selected="true"></asp:ListItem>
                                                                                                    <asp:ListItem Text="Cheque"></asp:ListItem>
                                                                                                </asp:RadioButtonList>
                                                                                            </td>
                                                                                            <td style="width: 25%" class="tblLeft leftCol">
                                                                                                Narration: *<asp:RequiredFieldValidator ID="rvNarrationAdd" runat="server" ControlToValidate="txtNarrationAdd"
                                                                                                    ErrorMessage="Narration is mandatory" Display="Dynamic" EnableClientScript="True">*</asp:RequiredFieldValidator>
                                                                                            </td>
                                                                                            <td style="width: 25%">
                                                                                                <asp:TextBox ID="txtNarrationAdd" runat="server" Height="30px" TextMode="MultiLine"
                                                                                                    Text='<%# Bind("Narration") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <td colspan="4" width="100%">
                                                                                            <asp:Panel ID="PanelBankAdd" runat="server">
                                                                                                <table width="100%" id="tblBankAdd" cellpadding="0" cellspacing="0" runat="server">
                                                                                                    <tr>
                                                                                                        <td style="width: 25%;" class="tblLeft leftCol">
                                                                                                            Bank Name: *
                                                                                                            <asp:CompareValidator ID="cvBankAdd" runat="server" ControlToValidate="ddBanksAdd"
                                                                                                                Display="Dynamic" EnableClientScript="false" ErrorMessage="Bank is Mandatory"
                                                                                                                Operator="GreaterThan" ValueToCompare="0">*</asp:CompareValidator>
                                                                                                        </td>
                                                                                                        <td style="width: 25%;">
                                                                                                            <div style="border-width: 1px; border-color: #bce1fe; border-style: solid; width: 100%;
                                                                                                                font-family: 'Trebuchet MS';">
                                                                                                                <asp:DropDownList ID="ddBanksAdd" runat="server" SelectedValue='<%# Bind("CreditorID") %>'
                                                                                                                    SkinID="skinDdlBoxBank" DataSourceID="srcBanksAdd" DataTextField="LedgerName"
                                                                                                                    DataValueField="LedgerID" AppendDataBoundItems="True">
                                                                                                                    <asp:ListItem Selected="True" style="background-color: #bce1fe" Value="0">Select Bank</asp:ListItem>
                                                                                                                </asp:DropDownList>
                                                                                                            </div>
                                                                                                        </td>
                                                                                                        <td style="width: 25%" class="tblLeft leftCol">
                                                                                                            Cheque No: *
                                                                                                            <asp:RequiredFieldValidator ID="rvChequeNoAdd" runat="server" ControlToValidate="txtChequeNoAdd"
                                                                                                                ErrorMessage="ChequeNo is mandatory" Display="Dynamic" EnableClientScript="false">*</asp:RequiredFieldValidator>
                                                                                                        </td>
                                                                                                        <td style="width: 25%;" class="tblLeftNoPad">
                                                                                                            <asp:TextBox ID="txtChequeNoAdd" runat="server" Text='<%# Bind("ChequeNo") %>' SkinID="skinTxtBoxGridBank"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </asp:Panel>
                                                                                        </td>
                                                                                        <tr style="height: 30px">
                                                                                            <td align="center" style="width: 100%" colspan="4">
                                                                                                <table cellspacing="0" cellpadding="0">
                                                                                                    <tr width="100%">
                                                                                                        <td style="height: 26px" align="right">
                                                                                                            <asp:Button ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                                                SkinID="skinBtnCancel" Text="Cancel" OnClick="InsertCancelButton_Click"></asp:Button>
                                                                                                        </td>
                                                                                                        <td style="height: 26px">
                                                                                                        </td>
                                                                                                        <td style="height: 26px" align="left">
                                                                                                            <asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                                                                                                SkinID="skinBtnSave" Text="Save" OnClick="InsertButton_Click"></asp:Button>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                            <td style="width: 25%">
                                                                                                <asp:ObjectDataSource ID="srcCreditorDebitorAdd" runat="server" SelectMethod="ListSundryDebitors"
                                                                                                    TypeName="BusinessLogic">
                                                                                                    <SelectParameters>
                                                                                                        <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                                                    </SelectParameters>
                                                                                                </asp:ObjectDataSource>
                                                                                            </td>
                                                                                            <td style="width: 25%">
                                                                                                <asp:ObjectDataSource ID="srcBanksAdd" runat="server" SelectMethod="ListBanks" TypeName="BusinessLogic">
                                                                                                    <SelectParameters>
                                                                                                        <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                                                    </SelectParameters>
                                                                                                </asp:ObjectDataSource>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </ContentTemplate>
                                                                            </cc1:TabPanel>
                                                                            <cc1:TabPanel ID="tabInsAddTab" runat="server" HeaderText="Additional Details">
                                                                                <ContentTemplate>
                                                                                    <table align="center" cellpadding="3" cellspacing="5" style="border: 0px solid #5078B3;
                                                                                        width: 100%;">
                                                                                        <tr style="height: 30px" class="tblLeft">
                                                                                            <td style="width: 25%" class="tblLeft leftCol">
                                                                                                Against Bill no:
                                                                                            </td>
                                                                                            <td style="width: 25%">
                                                                                                <asp:TextBox ID="txtBillAdd" runat="server" Text='<%# Bind("BillNo") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                            </td>
                                                                                            <td style="width: 25%">
                                                                                            </td>
                                                                                            <td style="width: 25%">
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </ContentTemplate>
                                                                            </cc1:TabPanel>
                                                                        </cc1:TabContainer>
                                                                        <asp:ValidationSummary ID="valSum" DisplayMode="BulletList" ShowMessageBox="true"
                                                                            ShowSummary="false" HeaderText="Validation Messages" Font-Names="'Trebuchet MS'"
                                                                            Font-Size="12" runat="server" />
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4">
                                                                    <asp:UpdatePanel ID="UP1" runat="server" UpdateMode="Conditional">
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="txtTransDateAdd" EventName="TextChanged" />
                                                                        </Triggers>
                                                                        <ContentTemplate>
                                                                            <asp:HiddenField ID="hddatecheck" runat="server" Value="0" />
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
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
                        <br />
                        <div class="mainGridHold" id="searchGrid">
                            <asp:GridView ID="GrdViewPayment" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                OnRowCreated="GrdViewPayment_RowCreated" Width="100%" DataSourceID="GridSource"
                                AllowPaging="True" DataKeyNames="TransNo" EmptyDataText="No Customer/Dealer Payments found!"
                                OnRowCommand="GrdViewPayment_RowCommand" OnRowDataBound="GrdViewPayment_RowDataBound"
                                OnSelectedIndexChanged="GrdViewPayment_SelectedIndexChanged" OnRowDeleting="GrdViewPayment_RowDeleting"
                                OnRowDeleted="GrdViewPayment_RowDeleted">
                                <EmptyDataRowStyle CssClass="GrdContent" />
                                <Columns>
                                    <asp:BoundField DataField="TransNo" HeaderStyle-Wrap="false" HeaderText="Trans No" />
                                    <asp:BoundField DataField="RefNo" HeaderStyle-Wrap="false" HeaderText="Ref No" />
                                    <asp:BoundField DataField="TransDate" HeaderStyle-Wrap="false" HeaderText="Trans Date"
                                        DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField DataField="Debi" HeaderText="Paid To" HeaderStyle-Wrap="false" />
                                    <asp:BoundField DataField="LedgerName" HeaderText="BankName/Cash" HeaderStyle-Wrap="false" />
                                    <asp:BoundField DataField="Amount" HeaderText="Amount" HeaderStyle-Wrap="false" />
                                    <asp:BoundField DataField="Narration" HeaderText="Narration" Visible="false" HeaderStyle-Wrap="false" />
                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="50px" HeaderText="Edit">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Select" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="50px" HeaderText="Delete">
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
                        </div>
                    </td>
                </tr>
                <tr width="100%">
                    <td style="width: 918px" align="left">
                        <asp:ObjectDataSource ID="GridSource" runat="server" SelectMethod="ListCustomerPayments"
                            TypeName="BusinessLogic" DeleteMethod="DeletePayment" OnDeleting="GridSource_Deleting">
                            <DeleteParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                <asp:Parameter Name="TransNo" Type="Int32" />
                                <asp:Parameter Name="requireValidation" Type="Boolean" DefaultValue="true" />
                            </DeleteParameters>
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="frmSource" runat="server" SelectMethod="GetPaymentForId"
                            TypeName="BusinessLogic" InsertMethod="InsertPayment" OnUpdating="frmSource_Updating"
                            OnInserting="frmSource_Inserting" UpdateMethod="UpdateCustPayment" OnInserted="frmSource_Inserted"
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
                                <asp:Parameter Name="Username" Type="String" />
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
                        <asp:HiddenField ID="hdEmailRequired" runat="server" Value="NO" />
                        <asp:HiddenField ID="hdSMSRequired" runat="server" Value="NO" />
                    </td>
                </tr>
            </table>
            <input type="hidden" id="hidAdvancedState" runat="server" />
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
