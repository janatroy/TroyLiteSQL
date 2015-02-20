<%@ Page Title="Others > Bilty" Language="C#" MasterPageFile="~/PageMaster.master"
    AutoEventWireup="true" CodeFile="BiltPurchase.aspx.cs" Inherits="BiltPurchase" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <script language="javascript" type="text/javascript">

        function PrintItem(ID) {
            window.showModalDialog('./PrintPayment.aspx?ID=' + ID, self, 'dialogWidth:700px;dialogHeight:430px;status:no;dialogHide:yes;unadorned:yes;');
        }


        function AdvancedTest(id) {

            var panel = document.getElementById('ctl00_cplhControlPanel_frmViewAdd_tblBank');
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


        function ValidateAdd() {
            var chalanNo = document.getElementById('ctl00_cplhControlPanel_frmViewAdd_txtChalanNoAdd');
            var biltyNo = document.getElementById('ctl00_cplhControlPanel_frmViewAdd_txtBiltiNoAdd');

            if (Page_IsValid) {
                if (chalanNo.value.toUpperCase() == biltyNo.value.toUpperCase()) {
                    alert("Challan No. and Bilty No. cannot be same. Please try again.");
                    return window.event.returnValue = false;
                }
            }
        }

        function ValidateUpdate() {
            var chalanNo = document.getElementById('ctl00_cplhControlPanel_frmViewAdd_txtChalanNo');
            var biltyNo = document.getElementById('ctl00_cplhControlPanel_frmViewAdd_txtBiltiNo');

            if (Page_IsValid) {
                if (chalanNo.value.toUpperCase() == biltyNo.value.toUpperCase()) {
                    alert("Challan No. and Bilty No. cannot be same. Please try again.");
                    return window.event.returnValue = false;
                }
            }
        }

        function AdvancedAdd(id) {

            var panel = document.getElementById('ctl00_cplhControlPanel_frmViewAdd_tblBankAdd');
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
    <asp:UpdatePanel ID="UpdatePanel16" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        
            <table style="width: 100%; vertical-align: top" align="center">
                <tr style="width: 100%">
                    <td style="width: 100%">
                        
                            <%--<div class="mainConHd">
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tr valign="middle">
                                        <td>
                                            <span>Bilty</span>
                                        </td>
                                    </tr>
                                </table>
                            </div>--%>
                            <%--<table class="mainConHd" style="width: 994px;">
                                <tr valign="middle">
                                    <td style="font-size: 20px;">
                                        Bilty
                                    </td>
                                </tr>
                            </table>--%>
                            <div class="mainConBody">
                                <table style="width: 100%; margin: -1px 0px 0px 1px;" cellpadding="3" cellspacing="2" class="searchbg">
                                    <tr style="height: 25px; vertical-align: middle">
                                        <td style="width: 2%;"></td>
                                        <td style="width: 18%; font-size: 22px; color: #000000;" >
                                            Bilty
                                        </td>
                                        <td style="width: 16%">
                                            <div style="text-align: right;">
                                                <asp:Panel ID="pnlSearch" runat="server" Width="100px">
                                                    <asp:Button ID="lnkBtnAdd" runat="server" OnClick="lnkBtnAdd_Click" CssClass="ButtonAdd66"
                                                        EnableTheming="false" Width="80px" Text=""></asp:Button>
                                                </asp:Panel>
                                            </div>
                                        </td>
                                        <td style="width: 13%; color: #000000;" align="right">
                                            Search
                                            <asp:RequiredFieldValidator ID="rvSearch" runat="server" ControlToValidate="txtSearch"
                                                Display="Dynamic" EnableClientScript="False" Enabled="false">Search is mandatory</asp:RequiredFieldValidator>
                                        </td>
                                        <td style="width: 20%" class="Box1">
                                            <asp:TextBox ID="txtSearch" runat="server" SkinID="skinTxtBoxSearch"></asp:TextBox>
                                        </td>
                                        <td style="width: 20%" class="Box1">
                                            <div style="width: 150px; font-family: 'Trebuchet MS';">
                                                <asp:DropDownList ID="ddCriteria" runat="server" Width="155px" BackColor="#BBCAFB" Height="23px" style="text-align:center;border:1px solid #BBCAFB ">
                                                    <asp:ListItem Value="ChalanNo">Challan No.</asp:ListItem>
                                                    <asp:ListItem Value="BiltiNo">Bilty No.</asp:ListItem>
                                                    <asp:ListItem Value="Transporter">Transporter</asp:ListItem>
                                                    <asp:ListItem Value="LedgerName">Supplier</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </td>
                                        <td style="width: 22%" class="tblLeftNoPad">
                                            <asp:Button ID="btnSearch" runat="server" Text=""  EnableTheming="false" CssClass="ButtonSearch6" SkinID="skinBtnSearch" />
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
                            <asp:UpdatePanel ID="updatePnlBilty" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div id="contentPopUp">
                                        <table style="width: 100%;" align="center">
                                            <tr style="width: 100%">
                                                <td style="width: 100%">
                                                    <asp:FormView ID="frmViewAdd" runat="server" Width="100%" DataSourceID="frmSource"
                                                        OnItemCommand="frmViewAdd_ItemCommand" DefaultMode="Edit" DataKeyNames="ID" OnItemUpdated="frmViewAdd_ItemUpdated"
                                                        OnItemCreated="frmViewAdd_ItemCreated" Visible="False" OnItemInserting="frmViewAdd_ItemInserting"
                                                        EmptyDataText="No Records" OnItemInserted="frmViewAdd_ItemInserted">
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
                                                                        <td colspan="5" class="headerPopUp">
                                                                            Bilty Details
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="height:5px">

                                                                    </tr>
                                                                    <tr>
                                                                        <td class="ControlLabel" style="width:15%">
                                                                            Challan No. *
                                                                            <asp:RequiredFieldValidator ID="rvChalanNo" runat="server" ControlToValidate="txtChalanNo"
                                                                                ErrorMessage="ChallanNo is mandatory" Display="Dynamic" EnableClientScript="True">*</asp:RequiredFieldValidator>
                                                                        </td>
                                                                        <td class="ControlTextBox3" style="width:25%">
                                                                            <asp:TextBox ID="txtChalanNo" runat="server" Text='<%# Bind("ChalanNo") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                        </td>
                                                                        <td class="ControlLabel" style="width:15%">
                                                                            Bilty No. *
                                                                            <asp:RequiredFieldValidator ID="rvBilti" runat="server" ControlToValidate="txtBiltiNo"
                                                                                ErrorMessage="Bilty No. is mandatory" Display="Dynamic" EnableClientScript="True">*</asp:RequiredFieldValidator>
                                                                        </td>
                                                                        <td class="ControlTextBox3" style="width:25%">
                                                                            <asp:TextBox ID="txtBiltiNo" runat="server" Text='<%# Bind("BiltiNo") %>' SkinID="skinTxtBoxGrid"
                                                                                Width="100px"></asp:TextBox>
                                                                        </td>
                                                                        <td style="width:10%">
                                                                        </td>
                                                                    </tr>
                                                                    <tr  style="height:3px">
                                                            </tr>
                                                                    <tr>
                                                                        <td class="ControlLabel" style="width:15%">
                                                                            Supplier
                                                                        </td>
                                                                        <td align="left" class="ControlDrpBorder" style="width:25%">
                                                                            <asp:DropDownList ID="ddSupplier" runat="server" CssClass="drpDownListMedium" BackColor = "#90c9fc" OnDataBound="ddSupplier_DataBound"
                                                                                AutoPostBack="False" DataSourceID="srcCreditorDebitor" DataValueField="LedgerID" Width="100%" style="border: 1px solid #90c9fc" height="26px"
                                                                                DataTextField="LedgerName" AppendDataBoundItems="true">
                                                                                <asp:ListItem Text="Select Supplier" style="background-color: #90c9fc" Value="0"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td class="ControlLabel" style="width:15%">
                                                                            Transporter
                                                                        </td>
                                                                        <td align="left" class="ControlDrpBorder" style="width:25%">
                                                                            <asp:DropDownList ID="ddTransporters" runat="server" CssClass="drpDownListMedium" BackColor = "#90c9fc" OnDataBound="ddTransporters_DataBound"
                                                                                AutoPostBack="False" DataSourceID="srcTransporters" DataValueField="TransporterID" Width="100%" style="border: 1px solid #90c9fc" height="26px"
                                                                                DataTextField="Transporter" AppendDataBoundItems="true">
                                                                                <asp:ListItem Text="Select Transporter" style="background-color: #90c9fc" Value="0"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td style="width:10%">
                                                                        </td>
                                                                    </tr>
                                                                    <tr  style="height:3px">
                                                            </tr>
                                                                    <tr>
                                                                        <td class="ControlLabel" style="width:15%">
                                                                            Qty. *
                                                                            <asp:RequiredFieldValidator ID="rvQty" runat="server" ControlToValidate="txtQty"
                                                                                ErrorMessage="Qty. is mandatory" Display="Dynamic" EnableClientScript="True">*</asp:RequiredFieldValidator>
                                                                            <cc1:FilteredTextBoxExtender ID="fltQty" runat="server" TargetControlID="txtQty"
                                                                                ValidChars="." FilterType="Numbers, Custom" />
                                                                        </td>
                                                                        <td class="ControlNumberBox3" style="width:25%">
                                                                            <asp:TextBox ID="txtQty" runat="server" Text='<%# Bind("Quantity") %>' CssClass="cssTextBox"
                                                                                Width="90%"></asp:TextBox>
                                                                        </td>
                                                                        <td class="ControlLabel" style="width:15%">
                                                                            Unit
                                                                        </td>
                                                                        <td align="left" class="ControlDrpBorder" style="width:25%">
                                                                            <asp:DropDownList ID="drpMeasure" runat="server" DataTextField="Unit" DataValueField="Unit" Width="100%" style="border: 1px solid #90c9fc" height="26px"
                                                                                 CssClass="drpDownListMedium" BackColor = "#90c9fc" DataSourceID="srcUnitMnt" AppendDataBoundItems="True" OnDataBound="drpMeasure_DataBound">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td style="width:10%">
                                                                        </td>
                                                                    </tr>
                                                                    <tr  style="height:3px">
                                                            </tr>
                                                                    <tr>
                                                                        <td class="ControlLabel" style="width: 15%">
                                                                            <asp:RequiredFieldValidator ID="rvReceiptDate" runat="server" ControlToValidate="txtReceiptDate"
                                                                                Text="*" ErrorMessage="Receipt Date is mandatory" Display="Dynamic" EnableClientScript="True"></asp:RequiredFieldValidator>
                                                                            <asp:CompareValidator ControlToValidate="txtReceiptDate" Operator="DataTypeCheck"
                                                                                Type="Date" Text="*" ErrorMessage="Please enter a valid date" runat="server"
                                                                                ID="cmpValtxtDate"></asp:CompareValidator>
                                                                            <asp:RangeValidator ID="myRangeValidator" runat="server" ControlToValidate="txtReceiptDate"
                                                                                ErrorMessage="Receipt Date cannot be future date." Text="*" Type="Date"></asp:RangeValidator>
                                                                            Receipt Date*
                                                                        </td>
                                                                        <td class="ControlNumberBox3" style="width: 25%">
                                                                            <asp:TextBox ID="txtReceiptDate" runat="server" Text='<%# Bind("ReceiptDate","{0:dd/MM/yyyy}") %>'
                                                                                CssClass="cssTextBox" Width="100px"></asp:TextBox>
                                                                            <cc1:CalendarExtender ID="calBillDate" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                                PopupButtonID="btnBillDat" PopupPosition="BottomLeft" TargetControlID="txtReceiptDate">
                                                                            </cc1:CalendarExtender>
                                                                        </td>
                                                                        <td style="width: 10%;" align="left">
                                                                            <asp:ImageButton ID="btnBillDat" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                                Width="20px" runat="server" />
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="height:5px">
                                                                        
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                        </td>
                                                                        <td align="right">
                                                                            <asp:Button ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                CssClass="cancelbutton6" EnableTheming="false" SkinID="skinBtnCancel" OnClick="UpdateCancelButton_Click">
                                                                            </asp:Button>
                                                                        </td>
                                                                        <td align="right">
                                                                            <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update"
                                                                                CssClass="Updatebutton1231" EnableTheming="false" OnClientClick="javascript:ValidateUpdate();"
                                                                                SkinID="skinBtnSave" OnClick="UpdateButton_Click"></asp:Button>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                            <table cellspacing="0">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label runat="server" ID="lblError" ForeColor="Red"></asp:Label>
                                                                        <asp:ObjectDataSource ID="srcCreditorDebitor" runat="server" SelectMethod="ListCreditorDebitor"
                                                                            TypeName="BusinessLogic">
                                                                            <SelectParameters>
                                                                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                            </SelectParameters>
                                                                        </asp:ObjectDataSource>
                                                                        <asp:ObjectDataSource ID="srcTransporters" runat="server" SelectMethod="ListTransporters"
                                                                            TypeName="BusinessLogic">
                                                                            <SelectParameters>
                                                                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                            </SelectParameters>
                                                                        </asp:ObjectDataSource>
                                                                        <asp:ObjectDataSource ID="srcUnitMnt" runat="server" SelectMethod="ListMeasurementUnits"
                                                                            TypeName="BusinessLogic" OldValuesParameterFormatString="original_{0}">
                                                                            <SelectParameters>
                                                                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                            </SelectParameters>
                                                                        </asp:ObjectDataSource>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                        <asp:ValidationSummary ID="valSum" DisplayMode="BulletList" ShowMessageBox="true"
                                                                            ShowSummary="false" HeaderText="Validation Messages" Font-Names="'Trebuchet MS'"
                                                                            Font-Size="12" runat="server" />
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
                                                                <table cellpadding="1" cellspacing="1" style="border: 1px solid #86b2d1; width: 100%;">
                                                                    <tr>
                                                                        <td colspan="5" class="headerPopUp">
                                                                            Bilty Details
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="height:5px">
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="ControlLabel" style="width:15%">
                                                                            Challan No. *
                                                                            <asp:RequiredFieldValidator ID="rvChalanNoAdd" runat="server" ControlToValidate="txtChalanNoAdd"
                                                                                ErrorMessage="ChallanNo is mandatory" Display="Dynamic" EnableClientScript="True">*</asp:RequiredFieldValidator>
                                                                        </td>
                                                                        <td class="ControlTextBox3" style="width:25%">
                                                                            <asp:TextBox ID="txtChalanNoAdd" runat="server" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                        </td>
                                                                        <td class="ControlLabel" style="width:15%">
                                                                            Bilty No. *
                                                                            <asp:RequiredFieldValidator ID="rvBiltiAdd" runat="server" ControlToValidate="txtBiltiNoAdd"
                                                                                ErrorMessage="Bilty No. is mandatory" Display="Dynamic" EnableClientScript="True">*</asp:RequiredFieldValidator>
                                                                        </td>
                                                                        <td class="ControlTextBox3" style="width:25%">
                                                                            <asp:TextBox ID="txtBiltiNoAdd" runat="server" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                        </td>
                                                                        <td style="width:10%">
                                                                        </td>
                                                                    </tr>
                                                                    <tr  style="height:3px">
                                                            </tr>
                                                                    <tr>
                                                                        <td class="ControlLabel" style="width:15%">
                                                                            Supplier
                                                                        </td>
                                                                        <td class="ControlDrpBorder" align="left" style="width:25%">
                                                                            <asp:DropDownList ID="ddSupplierAdd" runat="server" CssClass="drpDownListMedium" BackColor = "#90c9fc" AutoPostBack="False"
                                                                                DataSourceID="srcCreditorsAdd" DataValueField="LedgerID" DataTextField="LedgerName" Width="100%" style="border: 1px solid #90c9fc" height="26px"
                                                                                AppendDataBoundItems="true">
                                                                                <asp:ListItem Text="Select Supplier" style="background-color: #90c9fc" Value="0"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td class="ControlLabel" style="width:15%">
                                                                            Transporter
                                                                        </td>
                                                                        <td align="left" class="ControlDrpBorder" style="width:25%">
                                                                            <asp:DropDownList ID="ddTransportersAdd" runat="server" CssClass="drpDownListMedium" BackColor = "#90c9fc" AutoPostBack="False"
                                                                                DataSourceID="srcTransportersAdd" DataValueField="TransporterID" DataTextField="Transporter" Width="100%" style="border: 1px solid #90c9fc" height="26px"
                                                                                AppendDataBoundItems="true">
                                                                                <asp:ListItem Text="Select Transporter" style="background-color: #90c9fc" Value="0"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                    <tr  style="height:3px">
                                                            </tr>
                                                                    <tr>
                                                                        <td class="ControlLabel" style="width:15%">
                                                                            Qty. *
                                                                            <asp:RequiredFieldValidator ID="rvQtyAdd" runat="server" ControlToValidate="txtQtyAdd"
                                                                                ErrorMessage="Qty. is mandatory" Display="Dynamic" EnableClientScript="True">*</asp:RequiredFieldValidator>
                                                                            <cc1:FilteredTextBoxExtender ID="fltAdd" runat="server" TargetControlID="txtQtyAdd"
                                                                                ValidChars="." FilterType="Numbers, Custom" />
                                                                        </td>
                                                                        <td class="ControlNumberBox3" style="width:25%">
                                                                            <asp:TextBox ID="txtQtyAdd" runat="server" CssClass="cssTextBox" Width="90%"></asp:TextBox>
                                                                        </td>
                                                                        <td class="ControlLabel" style="width:15%">
                                                                            Unit
                                                                        </td>
                                                                        <td align="left" class="ControlDrpBorder" style="width:25%">
                                                                            <asp:DropDownList ID="drpMeasureAdd" runat="server" DataTextField="Unit" DataValueField="Unit" Width="100%"
                                                                                CssClass="drpDownListMedium" BackColor = "#90c9fc" DataSourceID="srcUnitMntAdd" AppendDataBoundItems="True" style="border: 1px solid #90c9fc" height="26px"
                                                                                OnDataBound="drpMeasureAdd_DataBound">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td style="width:10%">
                                                                        </td>
                                                                    </tr>
                                                                    <tr  style="height:3px">
                                                            </tr>
                                                                    <tr>
                                                                        <td class="ControlLabel" style="width: 15%">
                                                                            <asp:RequiredFieldValidator ID="rvReceiptDateAdd" runat="server" ControlToValidate="txtReceiptDateAdd"
                                                                                Text="*" ErrorMessage="Receipt Date is mandatory" Display="Dynamic" EnableClientScript="True"></asp:RequiredFieldValidator>
                                                                            <asp:CompareValidator ControlToValidate="txtReceiptDateAdd" Operator="DataTypeCheck"
                                                                                Type="Date" Text="*" ErrorMessage="Please enter a valid date" runat="server"
                                                                                ID="cmpValtxtDateAdd"></asp:CompareValidator>
                                                                            <asp:RangeValidator ID="myRangeValidatorAdd" runat="server" ControlToValidate="txtReceiptDateAdd"
                                                                                ErrorMessage="Receipt Date cannot be future date." Text="*" Type="Date"></asp:RangeValidator>
                                                                            Receipt Date*
                                                                        </td>
                                                                        <td class="ControlNumberBox3" style="width: 25%">
                                                                            <asp:TextBox ID="txtReceiptDateAdd" runat="server" CssClass="cssTextBox" Width="100px"></asp:TextBox>
                                                                            <cc1:CalendarExtender ID="calBillDate" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                                PopupButtonID="btnBillDateAdd" PopupPosition="BottomLeft" TargetControlID="txtReceiptDateAdd">
                                                                            </cc1:CalendarExtender>
                                                                        </td>
                                                                        <td style="width: 10%;" align="left">
                                                                            <asp:ImageButton ID="btnBillDateAdd" ImageUrl="App_Themes/NewTheme/images/cal.gif"
                                                                                CausesValidation="false" Width="20px" runat="server" />
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="height:6px">
                                                                        
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="5">
                                                                            <table style="width: 100%;">
                                                                                <tr>
                                                                                    <td style="width: 30%;">
                                                                                    </td>
                                                                                    <td align="center" style="width: 18%;">
                                                                                        <asp:Button ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                            CssClass="cancelbutton6" EnableTheming="false" SkinID="skinBtnCancel" OnClick="InsertCancelButton_Click">
                                                                                        </asp:Button>
                                                                                    </td>
                                                                                    <td align="center" style="width: 18%;">
                                                                                        <asp:Button ID="InsertButton" runat="server" CausesValidation="True" OnClientClick="javascript:ValidateAdd();"
                                                                                            CssClass="savebutton1231" EnableTheming="false" CommandName="Insert" SkinID="skinBtnSave"
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
                                                            <table cellspacing="0">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label runat="server" ID="lblError" ForeColor="Red"></asp:Label>
                                                                        <asp:ObjectDataSource ID="srcCreditorsAdd" runat="server" SelectMethod="ListSundryCreditors"
                                                                            TypeName="BusinessLogic">
                                                                            <SelectParameters>
                                                                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                            </SelectParameters>
                                                                        </asp:ObjectDataSource>
                                                                        <asp:ObjectDataSource ID="srcUnitMntAdd" runat="server" SelectMethod="ListMeasurementUnits"
                                                                            TypeName="BusinessLogic" OldValuesParameterFormatString="original_{0}">
                                                                            <SelectParameters>
                                                                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                            </SelectParameters>
                                                                        </asp:ObjectDataSource>
                                                                        <asp:ObjectDataSource ID="srcTransportersAdd" runat="server" SelectMethod="ListTransporters"
                                                                            TypeName="BusinessLogic">
                                                                            <SelectParameters>
                                                                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                            </SelectParameters>
                                                                        </asp:ObjectDataSource>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                        <asp:ValidationSummary ID="valSum" DisplayMode="BulletList" ShowMessageBox="true"
                                                                            ShowSummary="false" HeaderText="Validation Messages" Font-Names="'Trebuchet MS'"
                                                                            Font-Size="12" runat="server" />
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
                            <asp:GridView ID="GrdViewBilit" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                OnRowCreated="GrdViewBilit_RowCreated" Width="99.9%" DataSourceID="GridSource" CssClass="someClass"
                                AllowPaging="True" DataKeyNames="ID" EmptyDataText="No Bilty Entry Found" OnRowCommand="GrdViewBilit_RowCommand"
                                OnRowDataBound="GrdViewBilit_RowDataBound" OnSelectedIndexChanged="GrdViewBilit_SelectedIndexChanged"
                                OnRowDeleting="GrdViewBilit_RowDeleting" OnRowDeleted="GrdViewBilit_RowDeleted">
                                <EmptyDataRowStyle CssClass="GrdContent" />
                                <Columns>
                                    <asp:BoundField DataField="ChalanNo" HeaderStyle-Wrap="false" HeaderText="Challan No."  HeaderStyle-BorderColor="blue"/>
                                    <asp:BoundField DataField="BiltiNo" HeaderStyle-Wrap="false" HeaderText="Bilty No."  HeaderStyle-BorderColor="blue"/>
                                    <asp:BoundField DataField="LedgerName" HeaderStyle-Wrap="false" HeaderText="Supplier"  HeaderStyle-BorderColor="blue"/>
                                    <asp:BoundField DataField="Transporter" HeaderText="Transporter" HeaderStyle-Wrap="false"  HeaderStyle-BorderColor="blue"/>
                                    <asp:BoundField DataField="ReceiptDate" HeaderText="Receipt Date" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-BorderColor="blue"
                                        HeaderStyle-Wrap="false" />
                                    <asp:BoundField DataField="Quantity" HeaderText="Qty." HeaderStyle-Wrap="false"  HeaderStyle-BorderColor="blue"/>
                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="50px" HeaderText="Edit" HeaderStyle-BorderColor="blue">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Select" />
                                            <asp:ImageButton ID="btnEditDisabled" Enabled="false" SkinID="editDisable" runat="Server"></asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="50px" HeaderText="Delete" HeaderStyle-BorderColor="blue">
                                        <ItemTemplate>
                                            <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to Delete this Bilty?"
                                                runat="server">
                                            </cc1:ConfirmButtonExtender>
                                            <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" CommandName="Delete"></asp:ImageButton>
                                            <asp:ImageButton ID="lnkBDisabled" Enabled="false" SkinID="deleteDisable" runat="Server"></asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="25px" Visible="false" HeaderStyle-BorderColor="blue">
                                        <ItemTemplate>
                                            <a href='<%# DataBinder.Eval(Container, "DataItem.ID", "javascript:PrintItem({0});") %>'>
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
                                    <table style=" border-color:white">
                                        <tr style=" border-color:white">
                                            <td style=" border-color:white">
                                                Goto Page
                                            </td>
                                            <td style=" border-color:white">
                                                <asp:DropDownList ID="ddlPageSelector" runat="server" AutoPostBack="true" Width="65px" style="border:1px solid blue">
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
                <tr width="100%">
                    <td style="width: 918px" align="left">
                        <asp:ObjectDataSource ID="GridSource" runat="server" SelectMethod="ListBiltis" TypeName="BusinessLogic"
                            DeleteMethod="DeleteBilit" OnDeleting="GridSource_Deleting">
                            <DeleteParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                <asp:Parameter Name="ID" Type="Int32" />
                            </DeleteParameters>
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="frmSource" runat="server" SelectMethod="GetBiltForId" TypeName="BusinessLogic"
                            InsertMethod="InsertBilt" OnUpdating="frmSource_Updating" OnInserting="frmSource_Inserting"
                            UpdateMethod="UpdateBilt" OnInserted="frmSource_Inserted" OnUpdated="frmSource_Updated">
                            <UpdateParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                <asp:Parameter Name="ChalanNo" Type="String" />
                                <asp:Parameter Name="BiltiNo" Type="string" />
                                <asp:Parameter Name="SupplierID" Type="Int32" />
                                <asp:Parameter Name="TransporterID" Type="Int32" />
                                <asp:Parameter Name="Quantity" Type="Double" />
                                <asp:Parameter Name="ReceiptDate" Type="DateTime" />
                                <asp:Parameter Name="ID" Type="Int32" />
                                <asp:Parameter Name="QtyMeasure" Type="string" />
                            </UpdateParameters>
                            <SelectParameters>
                                <asp:ControlParameter ControlID="GrdViewBilit" Name="ID" PropertyName="SelectedValue"
                                    Type="Int32" />
                                <asp:CookieParameter CookieName="Company" Type="String" Name="connection" />
                            </SelectParameters>
                            <InsertParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                <asp:Parameter Name="ChalanNo" Type="String" />
                                <asp:Parameter Name="BiltiNo" Type="string" />
                                <asp:Parameter Name="SupplierID" Type="Int32" />
                                <asp:Parameter Name="TransporterID" Type="Int32" />
                                <asp:Parameter Name="Quantity" Type="Double" />
                                <asp:Parameter Name="ReceiptDate" Type="DateTime" />
                                <asp:Parameter Name="QtyMeasure" Type="string" />
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
