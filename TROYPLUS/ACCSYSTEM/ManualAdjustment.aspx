<%@ Page Title="Accounts > Manual Adjustment" Language="C#" MasterPageFile="~/PageMaster.master"
    AutoEventWireup="true" CodeFile="ManualAdjustment.aspx.cs" Inherits="ManualAdjustment" %>

<%@ Register Assembly="RealWorld.Grids" Namespace="RealWorld.Grids" TagPrefix="rwg" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <script language="javascript" type="text/javascript">

        /*@cc_on@*/
        /*@if (@_win32 && @_jscript_version>=5)

        function window.confirm(str) {
            execScript('n = msgbox("' + str + '","4132")', "vbscript");
            return (n == 6);
        }

        @end@*/


        function OnKeyPress(args) {
            if (args.keyCode == Sys.UI.Key.esc) {
                $find("ctl00_cplhControlPanel_ModalPopupExtender2").hide();
            }
        }

        $("#ctl00_cplhControlPanel_UpdateCancelButton").live("click", function () {
            $find("ctl00_cplhControlPanel_ModalPopupExtender2").hide();
        });


//        function CheckPendingBill() {
//            if (document.getElementById('ctl00_cplhControlPanel_hdPendingCount').value != '0') {
//                var rv = confirm("Unadjusted Invoice found, Do you still want to continue?");

//                if (rv == true) {
//                    return true;
//                }
//                else {
//                    return window.event.returnValue = false;
//                }
//            }
//        }

//        function ConfirmSMS() {
//            if (Page_IsValid) {
//                var confSMS = document.getElementById('ctl00_cplhControlPanel_hdSMS').value;

//                var confSMSRequired = document.getElementById('ctl00_cplhControlPanel_hdSMSRequired').value;

//                var txtMobile = document.getElementById('ctl00_cplhControlPanel_txtMobile');

//                if (txtMobile == null)
//                    txtMobile = document.getElementById('ctl00_cplhControlPanel_txtMobile');

//                if (txtMobile != null) {
//                    if (txtMobile.value != "") {

//                        if (confSMSRequired == "YES") {
//                            var rv = confirm("Do you want to send SMS to Customer?");

//                            if (rv == true) {
//                                document.getElementById('ctl00_cplhControlPanel_hdSMS').value = "YES";
//                                return false;
//                            }
//                            else {
//                                document.getElementById('ctl00_cplhControlPanel_hdSMS').value = "NO";
//                                return false;
//                            }
//                        }
//                    }
//                }
//            }
//        }

//        function Validate() {
//            var txtAmount = document.getElementById('ctl00_cplhControlPanel_txtAmount').value;

//            if (txtAmount == "") {
//                alert('Please enter the Receipt Amount before Adding BillNo');
//                return true;
//            }

//            var e = document.getElementById("ctl00_cplhControlPanel_ddReceivedFrom");

//            var strUser = e.options[e.selectedIndex].value;

//            if (strUser == "0") {
//                alert('Please Select the Customer before Adding Bills');
//                return true;
//            }

//        }

//        function EditMobile_Validator() {
//            var txtMobile = document.getElementById('ctl00_cplhControlPanel_txtMobile').value;

//            if (txtMobile.length > 0) {
//                if (txtMobile.length != 10) {
//                    alert("Customer Mobile Number should be minimum of 10 digits.");
//                    Page_IsValid = false;
//                    return window.event.returnValue = false;
//                }

//                if (txtMobile.charAt(0) == "0") {
//                    alert("Customer Mobile should not start with Zero. Please remove Zero and try again.");
//                    Page_IsValid = false;
//                    return window.event.returnValue = false;
//                }
//            }
//            else {
//                Page_IsValid = true;
//            }
//        }

//        function AddMobile_Validator() {
//            var txtMobile = document.getElementById('ctl00_cplhControlPanel_txtMobile').value;

//            if (txtMobile.length > 0) {
//                if (txtMobile.length != 10) {
//                    alert("Customer Mobile Number should be minimum of 10 digits.");
//                    Page_IsValid = false;
//                    return window.event.returnValue = false;
//                }

//                if (txtMobile.charAt(0) == "0") {
//                    alert("Customer Mobile should not start with Zero. Please remove Zero and try again.");
//                    Page_IsValid = false;
//                    return window.event.returnValue = false;
//                }
//            }
//            else {
//                Page_IsValid = true;
//            }
//        }


        function PrintItem(ID) {
            window.showModalDialog('./PrintReceipt.aspx?ID=' + ID, self, 'dialogWidth:700px;dialogHeight:430px;status:no;dialogHide:yes;unadorned:yes;');
        }

        function ShowCreditSales() {
            //return window.open('./ShowSalesBills.aspx', self, 'dialogWidth:800px;dialogHeight:350px;scroll:on;status:no;dialogHide:yes;unadorned:no;');
            window.open("ShowSalesBills.aspx", "TROY", "toolbar=no,menubar=no,resizable=yes,status=yes,height=450px,width=700px,scrollbars=yes");
        }

        function AdvancedTest(id) {

            var panel = document.getElementById('ctl00_cplhControlPanel_tblBank');
            var adv = document.getElementById('ctl00_cplhControlPanel_hidAdvancedState');

            var rdoArray = document.getElementsByTagName("input");


            for (i = 0; i <= rdoArray.length - 1; i++) {
                //alert(rdoArray[i].type);
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

            var panel = document.getElementById('ctl00_cplhControlPanel_tblBankAdd');
            var adv = document.getElementById('ctl00_cplhControlPanel_hidAdvancedState');

            var rdoArray = document.getElementsByTagName("input");


            for (i = 0; i <= rdoArray.length - 1; i++) {
                //alert(rdoArray[i].type);
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
    </script>
    <asp:UpdatePanel ID="UpdatePanelPage" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        
            <table style="width: 100%;" align="center">
                <tr style="width: 100%">
                    <td style="width: 100%" valign="middle">
                        
                            <%--<div class="mainConHd">
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tr valign="middle">
                                        <td>
                                            <span>Customer Receipts</span>
                                        </td>
                                    </tr>
                                </table>
                            </div>--%>
                            <table class="mainConHd" style="width: 994px;">
                                <tr valign="middle">
                                    <td style="font-size: 20px;">
                                        Manual Clearance
                                    </td>
                                </tr>
                            </table>
                            <div class="mainConBody">
                                
                                    <table cellspacing="2px" cellpadding="3px" border="0" width="100%"
                                        class="searchbg">
                                        <tr style="height: 25px">
                                            <td style="width: 15%" align="right">
                                                Option
                                            </td>
                                            
                                            <td style="width: 22%" class="Box">
                                                <div style="width: 160px; font-family: 'Trebuchet MS';">
                                                <asp:DropDownList ID="ddCriteria" runat="server" Width="154px" Height="23px" AutoPostBack="True" OnSelectedIndexChanged="ddCriteria_SelectedIndexChanged" style="text-align:center;border:1px solid White ">
                                                    <asp:ListItem Value="FullyCleared">Fully Cleared</asp:ListItem>
                                                    <asp:ListItem Value="PartiallyCleared" Selected="True">Partially Cleared</asp:ListItem>
                                                    <asp:ListItem Value="NotCleared">Not Cleared</asp:ListItem>
                                                </asp:DropDownList>
                                                </div>
                                            </td>
                                            <td style="width: 22%" class="Box">
                                            <div style="width: 160px; font-family: 'Trebuchet MS';">
                                                <asp:DropDownList ID="ddoption" runat="server" Width="154px" Height="23px" AutoPostBack="True" OnSelectedIndexChanged="ddoption_SelectedIndexChanged" style="text-align:center;border:1px solid White ">
                                                    <asp:ListItem Value="Customer" Selected="True">Customer</asp:ListItem>
                                                    <asp:ListItem Value="Supplier">Supplier</asp:ListItem>
                                                </asp:DropDownList>
                                                </div>
                                            </td>
                                            <td style="width: 22%" class="Box">
                                            <div style="width: 160px; font-family: 'Trebuchet MS';">
                                               <asp:DropDownList ID="ddReceivedFrom" runat="server" AutoPostBack="True" Width="154px" CssClass="drpDownListMedium" style="text-align:center;border:1px solid White "
                                                    DataValueField="customerID" OnSelectedIndexChanged="ComboBox2_SelectedIndexChanged" height="23px" 
                                                    DataTextField="customerName" AppendDataBoundItems="True">
                                                </asp:DropDownList>
                                                </div>
                                            </td>
                                            <td style="width: 28%; text-align: left">
                                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                                                    SkinID="skinBtnSearch" />
                                            </td>
                                        </tr>
                                    </table>
                                
                            </div>
                        
                        <input id="dummy" type="button" style="display: none" runat="server" />
                        <input id="Button1" type="button" style="display: none" runat="server" />
                        <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
                            CancelControlID="Button1" DynamicServicePath="" Enabled="True" PopupControlID="popUp"
                            TargetControlID="dummy">
                        </cc1:ModalPopupExtender>
                        <asp:Panel runat="server" ID="popUp" Style="width: 60%; display: none">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div id="contentPopUp">
                                        <asp:Panel ID="pnlEdit" runat="server">
                                            <table class="tblLeft" cellpadding="0" cellspacing="0" style="border: 0px solid #5078B3;
                                                background-color: #fff; color: #000;" width="100%">
                                                <tr>
                                                    <td>
                                                        <div class="divArea">
                                                            <table class="tblLeft" cellpadding="3" cellspacing="0" style="padding-left: 5px;"
                                                                width="100%">
                                                                <tr>
                                                                    <td colspan="4">
                                                                        <table class="headerPopUp">
                                                                            <tr>
                                                                                <td>
                                                                                    Manual Clearance
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <div style="text-align: left">
                                                                            <table style="width: 100%;border: 0px solid #86b2d1" align="center" cellpadding="3" cellspacing="2">
                                                                                
                                                                                
                                                                                <tr>
                                                                                    <td colspan="4">
                                                                                        <div runat="server" style="height:200px; border:1px solid blue; overflow:scroll">
                                                                                            <asp:Panel ID="pnlPopup" BackColor="White" BorderStyle="Solid" BorderColor="#bce1fe"
                                                                                                 runat="server">
                                                                                                <%--<asp:GridView ID="GrdViewSales" runat="server" AllowSorting="True" AutoGenerateColumns="False" CssClass="someClass" OnRowCreated="GrdViewSales_RowCreated" OnRowDataBound="GrdViewSales_RowDataBound"
                                                                                                    Width="100%" DataKeyNames="Billno" AllowPaging="True" OnPageIndexChanging="GrdViewSales_PageIndexChanging" EmptyDataText="No Credit Sales found." PageSize="6">
                                                                                                    <EmptyDataRowStyle CssClass="GrdContent" />--%>
                                                                                                <rwg:BulkEditGridView ID="GrdViewSales" AutoGenerateColumns="False" BorderWidth="1px"
                                                                                                BorderStyle="Solid" GridLines="Both" SaveButtonID="SaveButton" runat="server" CssClass="someClass"
                                                                                                Width="100%" DataKeyNames="BillNo" OnSelectedIndexChanged="GrdViewSales_SelectedIndexChanged"  OnRowDataBound="GrdViewSales_RowDataBound" EmptyDataText="No Credit Sales found.">
                                                                                                <RowStyle CssClass="dataRow" />
                                                                                                <SelectedRowStyle CssClass="SelectdataRow" />
                                                                                                <AlternatingRowStyle CssClass="altRow" />
                                                                                                <EmptyDataRowStyle CssClass="HeadataRow" Font-Bold="true" />
                                                                                                <HeaderStyle CssClass="HeadataRow" Wrap="false" />
                                                                                                <FooterStyle CssClass="dataRow" />
                                                                                                    <Columns>
                                                                                                        <asp:BoundField DataField="Billno" HeaderText="Bill No."  HeaderStyle-BorderColor="Blue" ReadOnly="true" ApplyFormatInEditMode="false"/>
                                                                                                        <asp:BoundField DataField="BillDate" HeaderText="Bill Date" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-BorderColor="Blue" ReadOnly="true" ApplyFormatInEditMode="false"/>
                                                                                                        <asp:BoundField DataField="CustomerName" HeaderText="CustomerName"  HeaderStyle-BorderColor="Blue" ReadOnly="true" ApplyFormatInEditMode="false"/>
                                                                                                        <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="{0:F2}"  HeaderStyle-BorderColor="Blue" ReadOnly="true" ApplyFormatInEditMode="false"/>
                                                                                                        <asp:BoundField DataField="pay" HeaderText="To Be Paid" DataFormatString="{0:F2}"  HeaderStyle-BorderColor="Blue" ReadOnly="true" ApplyFormatInEditMode="false"/>
                                                                                                        <asp:TemplateField ItemStyle-CssClass="command" ItemStyle-Width="50px" HeaderText="Show" HeaderStyle-BorderColor="blue">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:ImageButton ID="btnSelect" runat="server" SkinID="GridForward" ToolTip="Click To Get Bill Details"
                                                                                                                    CommandName="Select" />
                                                                                                            </ItemTemplate>
                                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                                        </asp:TemplateField>
                                                                                                    </Columns>
                                                                                                </rwg:BulkEditGridView>
                                                                                                </asp:Panel>
                                                                                            </div>
                                                                                        </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="4">
                                                                                        <table style="width:100%">
                                                                                            <tr>
                                                                                                <td colspan="3" style="width:80%">
                                                                                                    <div id="Div1" runat="server" style="height:160px; border:1px solid blue; overflow:scroll">
                                                                                                    <asp:Panel ID="Panel1" BackColor="White" CssClass="someClass"
                                                                                                        runat="server" >
                                                                                                        <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                                                                                                            <ContentTemplate>
                                                                                                                <asp:GridView ID="GrdBills" runat="server" AutoGenerateColumns="False" OnRowCreated="GrdBills_RowCreated"
                                                                                                                    Width="100%" PageSize="7" OnRowEditing="EditBill" OnRowCommand="GrdBills_RowCommand" CssClass="someClass"
                                                                                                                    OnRowDataBound="GrdBills_RowDataBound" AllowPaging="True" OnRowUpdating="GrdBills_RowUpdating"
                                                                                                                    OnRowCancelingEdit="GrdBillsCancelEdit" OnRowDeleting="GrdBills_RowDeleting"
                                                                                                                    EmptyDataText="No Bills Added." OnRowUpdated="GrdBills_RowUpdated">
                                                                                                                    <Columns>
                                                                                                                        <asp:TemplateField HeaderText="Bill No." HeaderStyle-BorderColor="Blue">
                                                                                                                            <ItemStyle Width="35%" />
                                                                                                                            <FooterStyle Width="35%" />
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label ID="lblBillNo" runat="server" Text='<%# Bind("BillNo") %>'></asp:Label>
                                                                                                                                <%--<asp:DropDownList ID="txtAddBillNo" Width="100%" AppendDataBoundItems="True" CssClass="drpDownListMedium"
                                                                                                                                    runat="server" DataTextField="BillNo" DataValueField="BillNo" BackColor = "#90C9FC" height="25px">
                                                                                                                                </asp:DropDownList>--%>
                                                                                                                            </ItemTemplate>
                                                                                                                            <EditItemTemplate>
                                                                                                                                <%--<asp:TextBox ID="txtBillNo" runat="server" Text='<%# Bind("BillNo") %>' CssClass=""
                                                                                                                                    Width="95%"></asp:TextBox>--%>
                                                                                                                                <asp:DropDownList ID="txtBillNo" Width="100%" AppendDataBoundItems="True" CssClass="drpDownListMedium"
                                                                                                                                    runat="server" DataTextField="BillNo" DataValueField="BillNo" BackColor = "#90C9FC" height="25px">
                                                                                                                                </asp:DropDownList>
                                                                                                                                <%--<asp:RequiredFieldValidator ID="rvBillNo" runat="server" ControlToValidate="txtBillNo"
                                                                                                                                    ValidationGroup="bills" Display="Dynamic" EnableClientScript="False" ErrorMessage="BillNo is mandatory">*</asp:RequiredFieldValidator>--%>
                                                                                                                            </EditItemTemplate>
                                                                                                                            <FooterTemplate>
                                                                                                                                <%--<asp:TextBox ID="txtAddBillNo" runat="server" CssClass="" Width="95%"></asp:TextBox>--%>
                                                                                                                                <asp:DropDownList ID="txtAddBillNo" Width="100%" AppendDataBoundItems="True" CssClass="drpDownListMedium"
                                                                                                                                    runat="server" DataTextField="BillNo" DataValueField="BillNo" BackColor = "#90C9FC" height="25px">
                                                                                                                                </asp:DropDownList>
                                                                                                                                <%--<asp:RequiredFieldValidator ID="rvAddBillNo" runat="server" ControlToValidate="txtAddBillNo"
                                                                                                                                    ValidationGroup="bills" Display="Dynamic" EnableClientScript="true" ErrorMessage="BillNo is mandatory">*</asp:RequiredFieldValidator>--%>
                                                                                                                            </FooterTemplate>
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField HeaderText="Amount" HeaderStyle-BorderColor="Blue">
                                                                                                                            <ItemStyle Width="35%" />
                                                                                                                            <FooterStyle Width="35%" />
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label ID="lblBillAmount" runat="server" Text='<%# Bind("Amount") %>'></asp:Label>
                                                                                                                            </ItemTemplate>
                                                                                                                            <EditItemTemplate>
                                                                                                                                <asp:TextBox ID="txtBillAmount" runat="server" Text='<%# Bind("Amount") %>' CssClass=""
                                                                                                                                    Width="93%"></asp:TextBox>
                                                                                                                                <asp:RequiredFieldValidator ID="rvDescr" runat="server" ControlToValidate="txtBillAmount"
                                                                                                                                    ValidationGroup="bills" Display="Dynamic" EnableClientScript="False" ErrorMessage="Amount is mandatory">*</asp:RequiredFieldValidator>
                                                                                                                                <asp:CompareValidator ID="cvBillAmount" runat="server" ControlToValidate="txtBillAmount"
                                                                                                                                    Display="Dynamic" ErrorMessage="Amount must be greater than Zero" Operator="GreaterThan"
                                                                                                                                    Text="*" ValidationGroup="bills" ValueToCompare="0"></asp:CompareValidator>
                                                                                                                            </EditItemTemplate>
                                                                                                                            <FooterTemplate>
                                                                                                                                <asp:TextBox ID="txtAddBillAmount" runat="server" CssClass="" Width="93%"></asp:TextBox>
                                                                                                                                <asp:RequiredFieldValidator ID="rvAddBillAmt" runat="server" ControlToValidate="txtAddBillAmount"
                                                                                                                                    ValidationGroup="bills" Display="Dynamic" EnableClientScript="true" ErrorMessage="Amount is mandatory">*</asp:RequiredFieldValidator>
                                                                                                                                <asp:CompareValidator ID="cvAddBillAmount" runat="server" ControlToValidate="txtAddBillAmount"
                                                                                                                                    Display="Dynamic" ErrorMessage="Amount must be greater than Zero" Operator="GreaterThan"
                                                                                                                                    Text="*" ValidationGroup="bills" ValueToCompare="0"></asp:CompareValidator>
                                                                                                                            </FooterTemplate>
                                                                                                                        </asp:TemplateField>
                                                                                                                        <%--<asp:BoundField DataField="pay" HeaderText="To Be Paid" HeaderStyle-Wrap="false"  HeaderStyle-BorderColor="Blue"/>--%>
                                                                                                                        <asp:TemplateField HeaderText="Edit" HeaderStyle-BorderColor="Blue">
                                                                                                                            <ItemStyle Width="20%" HorizontalAlign="Center" />
                                                                                                                            <FooterStyle Width="20%" HorizontalAlign="Center" />
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CausesValidation="False"
                                                                                                                                    CommandName="Edit" />
                                                                                                                            </ItemTemplate>
                                                                                                                            <EditItemTemplate>
                                                                                                                                <asp:ImageButton ID="lbUpdate" runat="server" CausesValidation="true" ValidationGroup="bills"
                                                                                                                                    CommandName="Update" Text="Update" SkinID="GridUpdate"></asp:ImageButton>
                                                                                                                                <asp:ImageButton ID="lbCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                                                                    Text="Cancel" SkinID="GridCancel"></asp:ImageButton>
                                                                                                                            </EditItemTemplate>
                                                                                                                            <FooterTemplate>
                                                                                                                                <asp:ImageButton ID="lbInsert" runat="server" CommandName="Insert" Text="Save" ValidationGroup="bills"
                                                                                                                                    SkinID="GridUpdate"></asp:ImageButton>
                                                                                                                                <asp:ImageButton ID="lblInsCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                                                                    Text="Cancel" SkinID="GridCancel"></asp:ImageButton>
                                                                                                                            </FooterTemplate>
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField ItemStyle-CssClass="command" HeaderText="Delete" HeaderStyle-BorderColor="Blue">
                                                                                                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                                                                                            <FooterStyle Width="10%" HorizontalAlign="Center" />
                                                                                                                            <ItemTemplate>
                                                                                                                                <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to Delete this Bill?"
                                                                                                                                    runat="server">
                                                                                                                                </cc1:ConfirmButtonExtender>
                                                                                                                                <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" CommandName="Delete" CausesValidation="false">
                                                                                                                                </asp:ImageButton>
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>
                                                                                                                        <%--<asp:TemplateField HeaderText="ReceiptNo" HeaderStyle-BorderColor="Blue">
                                                                                                                            <ItemStyle Width="35%" />
                                                                                                                            <FooterStyle Width="35%" />
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label ID="lblReceiptNo" runat="server" Text="0"></asp:Label>
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>--%>
                                                                                                                    </Columns>
                                                                                                                    <PagerTemplate>
                                                                                                                        <table style=" border-color:white">
                                                                                                                            <tr style=" border-color:white">
                                                                                                                                <td style=" border-color:white">
                                                                                                                                    Goto Page
                                                                                                                                </td>
                                                                                                                                <td style=" border-color:white">
                                                                                                                                    <asp:DropDownList ID="ddlPageSelector" runat="server" AutoPostBack="true" SkinID="skinPagerDdlBox2" style="border:1px solid blue">
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
                                                                                                                <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="True" ValidationGroup="bills"
                                                                                                                    ShowSummary="False" HeaderText="Validation Messages" Font-Names="'Trebuchet MS'"
                                                                                                                    Font-Size="12pt" runat="server" />
                                                                                                            </ContentTemplate>
                                                                                                            <Triggers>
                                                                                                                <asp:AsyncPostBackTrigger ControlID="lnkAddBills" EventName="Click" />
                                                                                                            </Triggers>
                                                                                                        </asp:UpdatePanel>
                                                                                                        </asp:Panel>
                                                                                                        </div>
                                                                                                </td>
                                                                                                <td style="width:20%">
                                                                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                                                    <ContentTemplate>
                                                                                                        <asp:Button ID="lnkAddBills" Text="" CausesValidation="False" runat="server" CssClass="addbillbutton"
                                                                                                            EnableTheming="false" OnClick="lnkAddBills_Click" SkinID="skinBtnUpload" Width="80px"
                                                                                                            />
                                                                                                    </ContentTemplate>
                                                                                                    <Triggers>
                                                                                                        <asp:AsyncPostBackTrigger ControlID="GrdBills" EventName="SelectedIndexChanged" />
                                                                                                    </Triggers>
                                                                                                </asp:UpdatePanel>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                        
                                                                                        
                                                                                    </td>
                                                                                </tr>
                                                                                <tr style="height: 30px; text-align: center">
                                                                                    <td colspan="4">
                                                                                        <table width="100%" cellspacing="0" style="table-layout: fixed">
                                                                                            <tr>
                                                                                                <td style="width: 30%">
                                                                                                </td>
                                                                                                <td style="width: 18%">
                                                                                                    <asp:Button ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                                        OnClick="UpdateCancelButton_Click" SkinID="skinBtnCancel" CssClass="cancelbutton"
                                                                                                        EnableTheming="false"></asp:Button>
                                                                                                </td>
                                                                                                <td style="width: 18%">
                                                                                                    <asp:Button ID="UpdateButton" runat="server" SkinID="skinBtnSave"
                                                                                                        OnClick="UpdateButton_Click" CssClass="savebutton" EnableTheming="false"></asp:Button>
                                                                                                    <asp:Button ID="SaveButton" runat="server" SkinID="skinBtnSave"
                                                                                                        OnClick="SaveButton_Click" CssClass="savebutton" EnableTheming="false"></asp:Button>
                                                                                                </td>
                                                                                                <td style="width: 30%">
                                                                                                    
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
                                                                                                <td>
                                                                                                </td>
                                                                                                <td>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr id="row" visible="false">
                                                                                    <td  colspan="4">
                                                                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                                                                                            Width="100%" PageSize="7" CssClass="someClass"
                                                                                            AllowPaging="True"
                                                                                            
                                                                                            EmptyDataText="No Bills Added." Visible="False">
                                                                                            <Columns>
                                                                                                <%--<asp:TemplateField HeaderText="Bill No." HeaderStyle-BorderColor="Blue">
                                                                                                    <ItemStyle Width="35%" />
                                                                                                    <FooterStyle Width="35%" />
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblBillNo" runat="server" Text='<%# Bind("BillNo") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    
                                                                                                </asp:TemplateField>--%>
                                                                                                <asp:BoundField DataField="BillNo" HeaderText="BillNo" HeaderStyle-BorderColor="blue" ReadOnly="true" ApplyFormatInEditMode="false"  HeaderStyle-Width="50px"/>
                                                                                                <asp:BoundField DataField="Amount" HeaderText="Amount" HeaderStyle-BorderColor="blue" ReadOnly="true" ApplyFormatInEditMode="false"  HeaderStyle-Width="50px"/>
                                                                                                <%--<asp:TemplateField HeaderText="Amount" HeaderStyle-BorderColor="Blue">
                                                                                                    <ItemStyle Width="35%" />
                                                                                                    <FooterStyle Width="35%" />
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblBillAmount" runat="server" Text='<%# Bind("Amount") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Edit" HeaderStyle-BorderColor="Blue">
                                                                                                    <ItemStyle Width="20%" HorizontalAlign="Center" />
                                                                                                    <FooterStyle Width="20%" HorizontalAlign="Center" />
                                                                                                    <ItemTemplate>
                                                                                                        <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CausesValidation="False"
                                                                                                            CommandName="Edit" />
                                                                                                    </ItemTemplate>
                                                                                                    <EditItemTemplate>
                                                                                                        <asp:ImageButton ID="lbUpdate" runat="server" CausesValidation="true" ValidationGroup="bills"
                                                                                                            CommandName="Update" Text="Update" SkinID="GridUpdate"></asp:ImageButton>
                                                                                                        <asp:ImageButton ID="lbCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                                            Text="Cancel" SkinID="GridCancel"></asp:ImageButton>
                                                                                                    </EditItemTemplate>
                                                                                                    <FooterTemplate>
                                                                                                        <asp:ImageButton ID="lbInsert" runat="server" CommandName="Insert" Text="Save" ValidationGroup="bills"
                                                                                                            SkinID="GridUpdate"></asp:ImageButton>
                                                                                                        <asp:ImageButton ID="lblInsCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                                            Text="Cancel" SkinID="GridCancel"></asp:ImageButton>
                                                                                                    </FooterTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField ItemStyle-CssClass="command" HeaderText="Delete" HeaderStyle-BorderColor="Blue">
                                                                                                    <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                                                                    <FooterStyle Width="10%" HorizontalAlign="Center" />
                                                                                                    <ItemTemplate>
                                                                                                        <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to Delete this Bill?"
                                                                                                            runat="server">
                                                                                                        </cc1:ConfirmButtonExtender>
                                                                                                        <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" CommandName="Delete" CausesValidation="false">
                                                                                                        </asp:ImageButton>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>--%>
                                                                                            </Columns>
                                                                                            <PagerTemplate>
                                                                                                <table style=" border-color:white">
                                                                                                    <tr style=" border-color:white">
                                                                                                        <td style=" border-color:white">
                                                                                                            Goto Page
                                                                                                        </td>
                                                                                                        <td style=" border-color:white">
                                                                                                            <asp:DropDownList ID="ddlPageSelector" runat="server" AutoPostBack="true" SkinID="skinPagerDdlBox2" style="border:1px solid blue">
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
                                                                                        <asp:Label runat="server" ID="Error" ForeColor="Red"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                            
                                                                            
                                                                        </div>
                                                                        
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
                <tr style="width: 100%;  margin: -4px 0px 0px 0px;">
                    <td style="width: 100%">
                        <table width="100%" style="margin: -3px 0px 0px 0px;">
                            <tr style="width: 100%">
                                <td>
                        <div class="mainGridHold" id="searchGrid">
                            <asp:GridView ID="GrdViewReceipt" runat="server" AllowSorting="false" AutoGenerateColumns="False"
                                OnRowCreated="GrdViewReceipt_RowCreated" Width="99.8%"
                                AllowPaging="True" DataKeyNames="TransNo" EmptyDataText="No Date found!"
                                OnRowCommand="GrdViewReceipt_RowCommand" OnRowDataBound="GrdViewReceipt_RowDataBound"
                                OnSelectedIndexChanged="GrdViewReceipt_SelectedIndexChanged" OnRowDeleting="GrdViewReceipt_RowDeleting"
                                OnRowDeleted="GrdViewReceipt_RowDeleted" CssClass="someClass">
                                <EmptyDataRowStyle CssClass="GrdContent" />
                                <Columns>
                                    <asp:BoundField DataField="TransNo" HeaderText="Trans. No." HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Blue" />
                                    <asp:BoundField DataField="RefNo" HeaderText="Ref. No." HeaderStyle-Wrap="false"  HeaderStyle-BorderColor="Blue"/>
                                    <asp:BoundField DataField="TransDate" HeaderText="Transaction Date" HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Blue"
                                        DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField DataField="LedgerName" HeaderText="Received From" HeaderStyle-Wrap="false"  HeaderStyle-BorderColor="Blue"/>
                                    <asp:BoundField DataField="Debi" HeaderText="Bank Name / Cash" HeaderStyle-Wrap="false"  HeaderStyle-BorderColor="Blue"/>
                                    <asp:BoundField DataField="Amount" HeaderText="Amount" HeaderStyle-Wrap="false"  HeaderStyle-BorderColor="Blue"/>
                                    <asp:BoundField DataField="Narration" HeaderText="Narration" Visible="false" HeaderStyle-Wrap="false"  HeaderStyle-BorderColor="Blue"/>
                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="50px" HeaderText="Edit" HeaderStyle-BorderColor="Blue">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEdit" CausesValidation="false" runat="server" SkinID="edit"
                                                CommandName="Select" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="50px" HeaderText="Delete" HeaderStyle-BorderColor="Blue">
                                        <ItemTemplate>
                                            <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to Delete this Receipt?"
                                                runat="server">
                                            </cc1:ConfirmButtonExtender>
                                            <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" CommandName="Delete"></asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="50px" HeaderText="Print" HeaderStyle-BorderColor="Blue">
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
                                    <table style=" border-color:white">
                                        <tr style=" border-color:white">
                                            <td style=" border-color:white">
                                                Goto Page
                                            </td>
                                            <td style=" border-color:white">
                                                <asp:DropDownList ID="ddlPageSelector" style="border:1px solid blue" runat="server" AutoPostBack="true" SkinID="skinPagerDdlBox2">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="Width:5px; border-color:white">
                                            
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
                    <td align="left">
                        <%--<asp:ObjectDataSource ID="GridSource" runat="server"
                            TypeName="BusinessLogic" DeleteMethod="DeleteCustReceipt" OnDeleting="GridSource_Deleting">
                            <DeleteParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                <asp:Parameter Name="TransNo" Type="Int32" />
                                <asp:Parameter Name="requireValidation" Type="Boolean" DefaultValue="true" />
                                <asp:Parameter Name="Username" Type="String" />
                            </DeleteParameters>
                        </asp:ObjectDataSource>--%>
                    </td>
                </tr>
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
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
