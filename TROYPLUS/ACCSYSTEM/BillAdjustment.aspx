<%@ Page Title="Accounts > Manual Clearance" Language="C#" MasterPageFile="~/PageMaster.master"
    AutoEventWireup="true" CodeFile="BillAdjustment.aspx.cs" Inherits="BillAdjustment" %>

<%@ Register Assembly="RealWorld.Grids" Namespace="RealWorld.Grids" TagPrefix="rwg" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <script language="javascript" type="text/javascript">

//        $(document).ready(function () {
//            var width = new Array();
//            var table = $("table[id*=GrdViewReceipt]");
//            table.find("th").each(function (i) {
//                width[i] = $(this).width();
//            });
//            headerRow = table.find("tr:first");
//            headerRow.find("th").each(function (i) {
//                $(this).width(width[i]);
//            });
//            var header = table.clone();
//            header.empty();
//            header.append(headerRow);
//            header.css("width", width);
//            $("#container").before(header);
//            var footer = table.clone();
//            footer.empty();
//            footer.append(table.find("tr:last"));
//            table.find("tr:first td").each(function (i) {
//                $(this).width(width[i]);
//            });
//            footer.find("td").each(function (i) {
//                $(this).width(width[i]);
//            });
//            $("#container").height(300);
//            $("#container").width(table.width() + 20);
//            $("#container").append(table);
//            $("#container").after(footer);
//        });


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


        function switchViews(obj, imG) {
            var div = document.getElementById(obj);
            var img = document.getElementById(imG);

            if (div.style.display == "none") {
                div.style.display = "inline";


                img.src = "App_Themes/DefaultTheme/Images/minus.gif";

            }
            else {
                div.style.display = "none";
                img.src = "App_Themes/DefaultTheme/Images/plus.gif";

            }
        }

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


//        function PrintItem(ID) {
//            window.showModalDialog('./PrintReceipt.aspx?ID=' + ID, self, 'dialogWidth:700px;dialogHeight:430px;status:no;dialogHide:yes;unadorned:yes;');
//        }

//        function ShowCreditSales() {
//            //return window.open('./ShowSalesBills.aspx', self, 'dialogWidth:800px;dialogHeight:350px;scroll:on;status:no;dialogHide:yes;unadorned:no;');
//            window.open("ShowSalesBills.aspx", "TROY", "toolbar=no,menubar=no,resizable=yes,status=yes,height=450px,width=700px,scrollbars=yes");
//        }

//        function AdvancedTest(id) {

//            var panel = document.getElementById('ctl00_cplhControlPanel_tblBank');
//            var adv = document.getElementById('ctl00_cplhControlPanel_hidAdvancedState');

//            var rdoArray = document.getElementsByTagName("input");


//            for (i = 0; i <= rdoArray.length - 1; i++) {
//                //alert(rdoArray[i].type);
//                if (rdoArray[i].type == 'radio') {

//                    if (rdoArray[i].value == 'Cash' && rdoArray[i].checked == true) {
//                        panel.className = "hidden";
//                        adv.value = panel.className;
//                    }
//                    else if (rdoArray[i].value == 'Cheque' && rdoArray[i].checked == true) {
//                        panel.className = "AdvancedSearch";
//                        adv.value = panel.className;
//                    }

//                }
//            }
//        }

//        function AdvancedAdd(id) {

//            var panel = document.getElementById('ctl00_cplhControlPanel_tblBankAdd');
//            var adv = document.getElementById('ctl00_cplhControlPanel_hidAdvancedState');

//            var rdoArray = document.getElementsByTagName("input");


//            for (i = 0; i <= rdoArray.length - 1; i++) {
//                //alert(rdoArray[i].type);
//                if (rdoArray[i].type == 'radio') {

//                    if (rdoArray[i].value == 'Cash' && rdoArray[i].checked == true) {
//                        panel.className = "hidden";
//                        adv.value = panel.className;
//                    }
//                    else if (rdoArray[i].value == 'Cheque' && rdoArray[i].checked == true) {
//                        panel.className = "AdvancedSearch";
//                        adv.value = panel.className;
//                    }

//                }
//            }
//        }
    </script>
    <asp:UpdatePanel ID="UpdatePanelPage" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        
            <table style="width: 100%;" align="center">
                <tr style="width: 100%">
                    <td style="width: 100%" valign="middle">
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
                                            <td style="width: 10%" align="center">
                                                Search Text
                                            </td>
                                            <td style="width: 22%" class="cssTextBoxbg">
                                                <asp:TextBox ID="txtSearch" runat="server" CssClass="cssTextBox" Width="92%"></asp:TextBox>
                                            </td>
                                            <td style="width: 22%" class="Box">
                                                <div style="width: 160px; font-family: 'Trebuchet MS';">
                                                    <asp:DropDownList ID="ddCriteria" runat="server" Width="154px" Height="23px" style="text-align:center;border:1px solid White ">
                                                        <%--<asp:ListItem Value="0" style="background-color: #bce1fe">All</asp:ListItem>--%>
                                                        <asp:ListItem Value="TransNo">Trans. No.</asp:ListItem>
                                                        <asp:ListItem Value="RefNo">Ref. No.</asp:ListItem>
                                                        <asp:ListItem Value="TransDate">Transaction Date</asp:ListItem>
                                                        <asp:ListItem Value="LedgerName">Received From</asp:ListItem>
                                                        <asp:ListItem Value="Narration">Narration</asp:ListItem>
                                                    </asp:DropDownList>
                                            </td>
                                            <td style="width: 28%; text-align: left">
                                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                                                    SkinID="skinBtnSearch" />
                                            </td>
                                        </tr>
                                    </table>
                                
                            </div>
                            <cc1:ModalPopupExtender ID="ModalPopupMethod" runat="server" BackgroundCssClass="modalBackground"
                                                CancelControlID="CancelPopUpMethod" DynamicServicePath="" Enabled="True" PopupControlID="pnlMethod"
                                                TargetControlID="ShowPopUpMethod">
                                            </cc1:ModalPopupExtender>
                                            <input id="ShowPopUpMethod" type="button" style="display: none" runat="server" />
                                            <input ID="CancelPopUpMethod" runat="server" style="display: none" 
                                                type="button" /> </input>
                                            </input>
                                            <asp:ValidationSummary ID="VSContact" runat="server" Font-Names="'Trebuchet MS'" Font-Size="12pt"
                                                HeaderText="Validation Messages" ShowMessageBox="true" ShowSummary="true" ValidationGroup="contact" />
                                            <asp:Panel ID="pnlMethod" runat="server" style="width:50%; display: none">
                                                <asp:UpdatePanel ID="updatePnlMethod" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                    <asp:Panel ID="pnlContactItems" CssClass="pnlPopUp" runat="server">
                                                        <div id="Div2" class="divArea">
                                                            <table cellpadding="3" cellspacing="2" style="width:100%" align="center">
                                                                <tr style="width:100%">
                                                                    <td style="width: 100%">
                                                                        <table style="text-align: left; width:100%; border: 1px solid Blue;" cellpadding="3" cellspacing="2">
                                                                            <tr>
                                                                                <td>
                                                                                    <table class="headerPopUp" width="100%">
                                                                                        <tr>
                                                                                            <td>
                                                                                                Select Option
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                                
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <table style="width: 100%;" cellpadding="3" cellspacing="1">
                                                                                        <tr style="height:10px">
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 34%">
                                                                                        
                                                                                            </td>
                                                                                            <td style="width: 30%;" class="ControlTextBox3">
                                                                                                <asp:RadioButtonList ID="optionmethod" runat="server" style="font-size:14px"
                                                                                                        RepeatDirection="Horizontal" BackColor="#90C9FC" Height="35px">
                                                                                                        <asp:ListItem Selected="True">Customer</asp:ListItem>
                                                                                                        <asp:ListItem>Supplier</asp:ListItem>
                                                                                                </asp:RadioButtonList>
                                                                                            </td>
                                                                                            <td style="width: 30%">
                                                                                        
                                                                                            </td>
                                                                                    
                                                                                        </tr>
                                                                                        <tr style="height:7px">
                                                                                        </tr>                                            
                                                                                    </table>
                                                                                </td>
                                                                                
                                                                            </tr>
                                                                            <tr>
                                                                                
                                                                                <td>
                                                                                    <table cellpadding="1" cellspacing="2"
                                                                                        width="100%">
                                                                                        <tr>
                                                                                            <td align="center">
                                                                                                <table>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:Panel ID="Panel4" runat="server" Width="120px">
                                                                                                                <asp:Button ID="cmdCancelMet" runat="server" CssClass="cancelbutton" OnClick="cmdCancelMet_Click" CausesValidation="false"
                                                                                                                    EnableTheming="false"/>
                                                                                                            </asp:Panel>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:Panel ID="Panel5" runat="server" Width="120px">
                                                                                                                <asp:Button ID="cmdMet" runat="server" CssClass="Start" 
                                                                                                                    EnableTheming="false" OnClick="cmdMet_Click" Text=""
                                                                                                                    ValidationGroup="contact" />
                                                                                                            </asp:Panel>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                                                       
                                                                                                </table>
                                                                                            </td>
                                                                                            
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                                
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            <tr>
                                                                <td>
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
                                                                            <tr style="height:10%">
                                                                            </tr>
                                                                                <tr>
                                                                                    <td class="ControlLabel" style="width: 16%">
                                                                                        <asp:Label ID="Label1" runat="server"
                                                                                                        Width="120px" Text="Received Date *" ></asp:Label>
                                                                                        <asp:RequiredFieldValidator ID="rvStock" runat="server" ControlToValidate="txtTransDate"
                                                                                            ErrorMessage="Trasaction Date is mandatory" Display="Dynamic">*</asp:RequiredFieldValidator>
                                                                                        <asp:CompareValidator ControlToValidate="txtTransDate" Operator="DataTypeCheck" Type="Date"
                                                                                            ErrorMessage="Please enter a valid date" runat="server" ID="cmpValtxtDate">*</asp:CompareValidator>
                                                                                        <asp:RangeValidator ID="myRangeValidator" runat="server" ControlToValidate="txtTransDate"
                                                                                            ErrorMessage="Payment date cannot be future date." Text="*" Type="Date"></asp:RangeValidator>
                                                                                    </td>
                                                                                    <td class="ControlTextBox3" style="width: 25%">
                                                                                        <asp:TextBox ID="txtTransDate" runat="server" Text='<%# Bind("TransDate","{0:dd/MM/yyyy}") %>'
                                                                                            CssClass="cssTextBox" Width="100px"></asp:TextBox>
                                                                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                                                        <cc1:CalendarExtender ID="calExtender3" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                                            PopupButtonID="btnDate3" PopupPosition="BottomLeft" TargetControlID="txtTransDate">
                                                                                        </cc1:CalendarExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:ImageButton ID="btnDate3" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                                            Width="20px" runat="server" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr >
                                                                                    <td class="ControlLabel" style="width: 24%">
                                                                                        <asp:Label ID="Label2" runat="server"
                                                                                                        Width="120px" Text="Select Customer *" ></asp:Label>
                                                                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddledger"
                                                                                            Display="Dynamic" ErrorMessage="Ledger is Mandatory" Operator="GreaterThan"
                                                                                            ValueToCompare="0">*</asp:CompareValidator>
                                                                                    </td>
                                                                                    <td class="ControlDrpBorder" style="width: 25%">
                                                                                        <asp:DropDownList ID="ddledger" runat="server" AutoPostBack="True" Width="100%" CssClass="drpDownListMedium" BackColor = "#90c9fc"
                                                                                            DataValueField="LedgerID" OnSelectedIndexChanged="ComboBox2_SelectedIndexChanged" style="border: 1px solid #90c9fc" height="26px" 
                                                                                            DataTextField="LedgerName" AppendDataBoundItems="True">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr >
                                                                                    <td class="ControlLabel" style="width: 24%">
                                                                                        <asp:Label ID="Label3" runat="server"
                                                                                                        Width="120px" Text="Select Option *" ></asp:Label>
                                                                                    </td>
                                                                                    <td class="ControlDrpBorder" style="width: 25%">
                                                                                        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" Width="100%" CssClass="drpDownListMedium" BackColor = "#90c9fc"
                                                                                            style="border: 1px solid #90c9fc" height="26px" 
                                                                                            AppendDataBoundItems="True">
                                                                                            <asp:ListItem Selected="True">Fully Cleared</asp:ListItem>
                                                                                            <asp:ListItem>Partially Cleared</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr  style="height: 10px">
                                                                                    

                                                                                </tr>
                                                                                <tr id="rowdetails" runat="server" visible="false">
                                                                                    <td colspan="4">
                                                                                        <div style="width: 700px" align="center">
                                                                                            <rwg:BulkEditGridView ID="GrdViewSales" AutoGenerateColumns="False" BorderWidth="1px"
                                                                                                BorderStyle="Solid" GridLines="Both" SaveButtonID="SaveButton" runat="server" CssClass="someClass"
                                                                                                Width="100%" DataKeyNames="BillNo" OnRowDataBound="GrdViewSales_RowDataBound">
                                                                                                <RowStyle CssClass="dataRow" />
                                                                                                <SelectedRowStyle CssClass="SelectdataRow" />
                                                                                                <AlternatingRowStyle CssClass="altRow" />
                                                                                                <EmptyDataRowStyle CssClass="HeadataRow" Font-Bold="true" />
                                                                                                <HeaderStyle CssClass="HeadataRow" Wrap="false" />
                                                                                                <FooterStyle CssClass="dataRow" />
                                                                                                <Columns>
                                                                                                    <asp:TemplateField HeaderText="Bill Detail View" ItemStyle-HorizontalAlign="Left">
                                                                                                        <ItemTemplate>
                                                                                                            <a href="javascript:switchViews('div<%# Eval("BillNo") %>', 'imgdiv<%# Eval("BillNo") %>');"
                                                                                                                style="text-decoration: none;">
                                                                                                                <img id="imgdiv<%# Eval("BillNo") %>" alt="Show" border="0" src="App_Themes/DefaultTheme/Images/plus.gif" />
                                                                                                            </a>
                                                                                                            <%# Eval("BillNo")%>
                                                                                                            <br />
                                                                                                            <div id="div<%# Eval("BillNo") %>" style="display: none; position: relative;
                                                                                                                left: 25px;">
                                                                                                             
                                                                                                            </div>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:BoundField DataField="BillDate" HeaderText="BillDate" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-Wrap="false" />
                                                                                                    <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-Wrap="false" />
                                                                                                    <asp:BoundField DataField="Amount" HeaderText="Amount" HeaderStyle-Wrap="false" ReadOnly="true" ApplyFormatInEditMode="false"/>
                                                                                                </Columns>
                                                                                            </rwg:BulkEditGridView>
                                                                                            
                                                                                        
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="4"> 
                                                                                        <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                                                                                            <ContentTemplate>
                                                                                                <asp:GridView ID="GrdViewDetails" runat="server" AutoGenerateColumns="False" OnRowCreated="GrdViewDetails_RowCreated"
                                                                                                    Width="110%" PageSize="7" OnRowEditing="EditBill" OnRowCommand="GrdViewDetails_RowCommand" CssClass="someClass"
                                                                                                    OnRowDataBound="GrdViewDetails_RowDataBound" AllowPaging="True" OnRowUpdating="GrdViewDetails_RowUpdating"
                                                                                                    OnRowCancelingEdit="GrdBillsCancelEdit" OnRowDeleting="GrdViewDetails_RowDeleting"
                                                                                                    EmptyDataText="No Bills Added." OnRowUpdated="GrdViewDetails_RowUpdated">
                                                                                                    <Columns>
                                                                                                        <asp:BoundField DataField="ReceiptNo" HeaderText="ReceiptNo" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-Wrap="false" />
                                                                                                        <asp:TemplateField HeaderText="Bill No." HeaderStyle-BorderColor="Blue">
                                                                                                            <ItemStyle Width="35%" />
                                                                                                            <FooterStyle Width="35%" />
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="lblBillNo" runat="server" Text='<%# Bind("BillNo") %>'></asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                            <EditItemTemplate>
                                                                                                                <asp:TextBox ID="txtBillNo" runat="server" Text='<%# Bind("BillNo") %>' CssClass=""
                                                                                                                    Width="95%"></asp:TextBox>
                                                                                                                <asp:RequiredFieldValidator ID="rvBillNo" runat="server" ControlToValidate="txtBillNo"
                                                                                                                    ValidationGroup="bills" Display="Dynamic" EnableClientScript="False" ErrorMessage="BillNo is mandatory">*</asp:RequiredFieldValidator>
                                                                                                            </EditItemTemplate>
                                                                                                            <FooterTemplate>
                                                                                                                <asp:TextBox ID="txtAddBillNo" runat="server" CssClass="" Width="95%"></asp:TextBox>
                                                                                                                <asp:RequiredFieldValidator ID="rvAddBillNo" runat="server" ControlToValidate="txtAddBillNo"
                                                                                                                    ValidationGroup="bills" Display="Dynamic" EnableClientScript="true" ErrorMessage="BillNo is mandatory">*</asp:RequiredFieldValidator>
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
                                                                                    </td>
                                                                                </tr>
                                                                                
                                                                                <tr  style="height: 10px">
                                                                                </tr>
                                                                                <tr style="height: 30px; text-align: center">
                                                                                    <td colspan="6">
                                                                                        <table width="100%" cellspacing="0" style="table-layout: fixed">
                                                                                            <tr>
                                                                                                <td style="width: 15%">
                                                                                                </td>
                                                                                                <td style="width: 18%">
                                                                                                    
                                                                                                </td>
                                                                                                <td style="width: 18%">
                                                                                                    <asp:Button ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                                        OnClick="UpdateCancelButton_Click" SkinID="skinBtnCancel" CssClass="cancelbutton"
                                                                                                        EnableTheming="false"></asp:Button>
                                                                                                </td>
                                                                                                <td style="width: 18%">
                                                                                                    
                                                                                                    <asp:Button ID="UpdateButton" runat="server" SkinID="skinBtnSave" OnClientClick="javascript:CheckPendingBill();EditMobile_Validator();ConfirmSMS();"
                                                                                                        OnClick="UpdateButton_Click" CssClass="savebutton" EnableTheming="false"></asp:Button>
                                                                                                    <asp:Button ID="SaveButton" runat="server" SkinID="skinBtnSave" OnClientClick="javascript:CheckPendingBill();EditMobile_Validator();ConfirmSMS();"
                                                                                                        OnClick="SaveButton_Click" CssClass="savebutton" EnableTheming="false"></asp:Button>
                                                                                                </td>
                                                                                                <td style="width: 18%">
                                                                                                   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                                                        <ContentTemplate>
                                                                                                            <asp:Button ID="lnkAddBills" Text="" CausesValidation="False" runat="server" CssClass="addbillbutton"
                                                                                                                EnableTheming="false" OnClick="lnkAddBills_Click" SkinID="skinBtnUpload" Width="80px"
                                                                                                                />
                                                                                                        </ContentTemplate>
                                                                                                        <Triggers>
                                                                                                            <asp:AsyncPostBackTrigger ControlID="GrdViewDetails" EventName="SelectedIndexChanged" />
                                                                                                        </Triggers>
                                                                                                    </asp:UpdatePanel>
                                                                                    
                                                                                                    <input id="ShowPopUp" type="button" class="pendingbillbutton" Visible="false" style="width: 110px;
                                                                                                        vertical-align: middle" runat="server" value="" />
                                                                                                    <asp:Button ID="BtnSales" Text="" CausesValidation="False" Visible="False" OnClick="ShowPendingSales_Click"
                                                                                                        runat="server" CssClass="Button" Width="120px" />
                                                                                                </td>
                                                                                                <td style="width: 15%">
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
                                                                                <tr>
                                                                                    <td>
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
                        </table>
                        </div>
                        </asp:Panel>
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
                                OnRowCreated="GrdViewReceipt_RowCreated" Width="99.8%" DataSourceID="GridSource"
                                AllowPaging="True" DataKeyNames="TransNo" EmptyDataText="No Receipts found!"
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
                        <asp:ObjectDataSource ID="GridSource" runat="server" SelectMethod="ListReceiptsCustomers"
                            TypeName="BusinessLogic" DeleteMethod="DeleteCustReceipt" OnDeleting="GridSource_Deleting">
                            <DeleteParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                <asp:Parameter Name="TransNo" Type="Int32" />
                                <asp:Parameter Name="requireValidation" Type="Boolean" DefaultValue="true" />
                            </DeleteParameters>
                        </asp:ObjectDataSource>
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
