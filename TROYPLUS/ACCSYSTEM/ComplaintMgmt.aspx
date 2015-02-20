<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="ComplaintMgmt.aspx.cs" Inherits="ComplaintMgmt" Title="Complaint > Complaint" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <script language="javascript" type="text/javascript">

        function PrintItem(ID) {
            window.showModalDialog('PrintJournal.aspx?ID=' + ID, self, 'dialogWidth:700px;dialogHeight:430px;status:no;dialogHide:yes;unadorned:yes;');
        }

    </script>
    <asp:UpdatePanel ID="UpdatePanel16" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <table style="width: 100%;" align="center">
                <tr style="width: 100%">
                    <td style="width: 100%">
                        <div class="mainConDiv" id="IdmainConDiv">
                            <%--<div class="mainConHd">
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tr valign="middle">
                                        <td>
                                            <span>Complaint</span>
                                        </td>
                                    </tr>
                                </table>
                            </div>--%>
                            <%--<table class="mainConHd" style="width: 994px;">
                                <tr valign="middle">
                                    <td style="font-size: 20px;">
                                         Complaint
                                    </td>
                                </tr>
                            </table>--%>
                            <div class="mainConBody">
                                <table style="width: 100%;margin: -1px 0px 0px 1px;" cellpadding="2px" cellspacing="2px" class="searchbg">
                                    <tr>
                                        <td style="width: 1%">
                                        </td>
                                        <td style="width: 25%; font-size: 20px; color: #000000;" >
                                             Complaint
                                         </td>
                                        <td style="width: 14%">
                                            
                                        </td>
                                        <td style="width: 10%; color: #000000;" align="right">
                                            
                                        </td>
                                        <td style="width: 19%">
                                            
                                        </td>
                                        <td style="width: 18%">
                                            
                                        </td>
                                        
                                     </tr>
                                </table>
                            </div>
                            <div class="mainConBody">
                                <div class="shadow1">
                                    &nbsp;
                                </div>
                                <div>
                                    <table style="width: 100%;" cellpadding="3" cellspacing="2" style="border: 0px solid #5078B3">
                                        <tr>
                                            <td style="width: 22%">
                                                <div style="text-align: right;">
                                                    <asp:Panel ID="pnlSearch" runat="server" Width="100px">
                                                        <asp:Button ID="lnkBtnAdd" runat="server" OnClick="lnkBtnAdd_Click" CssClass="ButtonAdd"
                                                            EnableTheming="false" Width="80px" Text="Add New"></asp:Button>
                                                    </asp:Panel>
                                                </div>
                                            </td>
                                            <td style="width: 10%" align="center">
                                                Search
                                            </td>
                                            <td style="width: 22%">
                                                <asp:TextBox ID="txtSearch" runat="server" CssClass="cssTextBox" Width="92%"></asp:TextBox>
                                            </td>
                                            <td style="width: 22%">
                                                <div style="border-width: 1px; border-color: #bce1fe; border-style: solid; width: 150px;
                                                    font-family: 'Trebuchet MS';">
                                                    <asp:DropDownList ID="ddCriteria" runat="server" CssClass="cssDD" Width="99%">
                                                        <asp:ListItem Value="Customer">Customer</asp:ListItem>
                                                        <asp:ListItem Value="ComplaintDetails">Complaint Details</asp:ListItem>
                                                        <asp:ListItem Value="Status">Status</asp:ListItem>
                                                        <asp:ListItem Value="AssignedTo">Assigned To</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </td>
                                            <td style="width: 8%;" class="tblRight">
                                                Active :
                                            </td>
                                            <td style="width: 6%" align="left">
                                                <asp:CheckBox ID="chkActive" runat="server" Checked="true" />
                                            </td>
                                            <td style="width: 25%" class="tblLeftNoPad">
                                                <asp:Button ID="btnSearch" runat="server" Text="Search" SkinID="skinBtnSearch" OnClick="cmdSearch_Click" />
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
                                                DefaultMode="Edit" DataKeyNames="ComplaintID" Visible="False" OnItemUpdated="frmViewAdd_ItemUpdated"
                                                OnItemCreated="frmViewAdd_ItemCreated" OnItemInserted="frmViewAdd_ItemInserted">
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
                                                                <tr>
                                                                    <td colspan="4">
                                                                        <table class="headerPopUp" style="border: 1px solid #86b2d1" width="100%">
                                                                            <tr>
                                                                                <td>
                                                                                    Complaint Management
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 25%; height: 34px;" class="leftCol">
                                                                        Customer *
                                                                        <asp:CompareValidator ID="cvCustomer" runat="server" ControlToValidate="drpCustomer"
                                                                            ValidationGroup="edit" Display="Dynamic" ErrorMessage="Customer is Mandatory"
                                                                            Operator="GreaterThan" Text="*" ValueToCompare="0"></asp:CompareValidator>
                                                                    </td>
                                                                    <td style="width: 25%">
                                                                        <div style="border-width: 1px; border-color: #bce1fe; border-style: solid; width: 99%;
                                                                            font-family: 'Trebuchet MS';">
                                                                            <asp:DropDownList TabIndex="1" ID="drpCustomer" AppendDataBoundItems="true" SkinID="skinDdlBox"
                                                                                DataSourceID="srcDebitors" OnDataBound="drpCustomer_DataBound" runat="server"
                                                                                AutoPostBack="false" DataValueField="LedgerID" DataTextField="LedgerName" ValidationGroup="edit">
                                                                                <asp:ListItem style="background-color: #bce1fe" Text="Select Customer" Value="0"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </td>
                                                                    <td style="width: 25%" class="leftCol">
                                                                        Complaint Date *
                                                                        <asp:RequiredFieldValidator ID="rvStock" runat="server" ValidationGroup="edit" ControlToValidate="txtComplaintDate"
                                                                            Display="Dynamic" EnableClientScript="true" ErrorMessage="Date is mandatory">*</asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <td style="width: 25%">
                                                                        <asp:TextBox ID="txtComplaintDate" CssClass="cssTextBox" runat="server" ValidationGroup="editVal"
                                                                            Text='<%# Bind("ComplaintDate","{0:dd/MM/yyyy}") %>' Width="100px" MaxLength="10"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="calExt3" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                            PopupButtonID="btnD3" PopupPosition="BottomLeft" TargetControlID="txtComplaintDate">
                                                                        </cc1:CalendarExtender>
                                                                        <asp:ImageButton ID="btnD3" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false" Width="20px"
                                                                            runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 25%" class="leftCol">
                                                                        Assigned To:
                                                                        <asp:RequiredFieldValidator ID="reqAssignedTo" ErrorMessage="Assigned To is mandatory"
                                                                            InitialValue="0" EnableClientScript="true" Text="*" ControlToValidate="drpAssignedTo"
                                                                            runat="server" ValidationGroup="edit" Display="Dynamic" />
                                                                    </td>
                                                                    <td style="width: 25%">
                                                                        <div style="border-width: 1px; border-color: #bce1fe; border-style: solid; width: 99%;
                                                                            font-family: 'Trebuchet MS';">
                                                                            <asp:DropDownList ID="drpAssignedTo" TabIndex="4" Enabled="True" AppendDataBoundItems="true"
                                                                                OnDataBound="drpAssignedTo_DataBound" DataSourceID="srcAssinedTo" runat="server"
                                                                                SkinID="skinDdlBox" DataTextField="empFirstName" DataValueField="empno">
                                                                                <asp:ListItem Text="Select Employee" style="background-color: #bce1fe" Value="0"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </td>
                                                                    <td style="width: 25%" class="leftCol">
                                                                        Status:
                                                                    </td>
                                                                    </td>
                                                                    <td style="width: 25%">
                                                                        <div style="border-width: 1px; border-color: #bce1fe; border-style: solid; width: 99%;
                                                                            font-family: 'Trebuchet MS';">
                                                                            <asp:DropDownList ID="drpStatus" runat="server" SkinID="skinDdlBox" AutoPostBack="False"
                                                                                SelectedValue='<%# Bind("ComplaintStatus") %>' AppendDataBoundItems="true">
                                                                                <asp:ListItem Text="In Progress" Value="In Progress"></asp:ListItem>
                                                                                <asp:ListItem Text="Resolved" Value="Resolved"></asp:ListItem>
                                                                                <asp:ListItem Text="Closed" Value="Closed"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr class="tblLeft" valign="top">
                                                                    <td style="width: 25%;" class="leftCol">
                                                                        Is Billed:
                                                                    </td>
                                                                    <td style="width: 25%;">
                                                                        <div style="border-width: 1px; border-color: #bce1fe; border-style: solid; width: 99%;
                                                                            font-family: 'Trebuchet MS';">
                                                                            <asp:DropDownList ID="drpBilled" runat="server" SkinID="skinDdlBox" AutoPostBack="False"
                                                                                SelectedValue='<%# Bind("IsBilled") %>' AppendDataBoundItems="true">
                                                                                <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                                                <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </td>
                                                                    <td style="width: 25%">
                                                                    </td>
                                                                    <td style="width: 25%">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 25%; vertical-align: text-top" class="leftCol">
                                                                        Details *
                                                                        <asp:RequiredFieldValidator ID="rvDetails" runat="server" ControlToValidate="txtComplaintDetials"
                                                                            ValidationGroup="edit" Display="Dynamic" EnableClientScript="true" ErrorMessage="Details is mandatory">*</asp:RequiredFieldValidator>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtComplaintDetials"
                                                                            Display="Dynamic" ErrorMessage="Please limit to 255 characters or less." ValidationGroup="edit"
                                                                            Text="*" ValidationExpression="[\s\S]{1,255}"></asp:RegularExpressionValidator>
                                                                    </td>
                                                                    <td style="width: 25%" colspan="3">
                                                                        <asp:TextBox ID="txtComplaintDetials" ValidationGroup="edit" runat="server" TextMode="MultiLine"
                                                                            Height="50px" MaxLength="100" Text='<%# Bind("ComplaintDetails") %>' CssClass="cssTextBox"
                                                                            Width="99%"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" style="width: 25%" colspan="2">
                                                                        <table cellspacing="0">
                                                                            <tr width="100%">
                                                                                <td style="width: 67px">
                                                                                    <asp:Button ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                        SkinID="skinBtnCancel" Text="Cancel" OnClick="UpdateCancelButton_Click"></asp:Button>
                                                                                </td>
                                                                                <td>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update"
                                                                                        ValidationGroup="edit" SkinID="skinBtnSave" Text="Update" OnClick="UpdateButton_Click">
                                                                                    </asp:Button>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td style="width: 25%">
                                                                    </td>
                                                                    <td style="width: 25%">
                                                                    </td>
                                                                </tr>
                                                        </table>
                                                        <asp:ValidationSummary ID="valSum" DisplayMode="BulletList" ShowMessageBox="true"
                                                            ShowSummary="false" HeaderText="Validation Messages" ValidationGroup="edit" Font-Names="'Trebuchet MS'"
                                                            Font-Size="12" runat="server" />
                                                    </div>
                                                </EditItemTemplate>
                                                <InsertItemTemplate>
                                                    <div class="divArea">
                                                        <table class="tblLeft" cellpadding="3" cellspacing="3" style="border: 1px solid #5078B3;"
                                                            width="100%">
                                                            <tr>
                                                                <tr>
                                                                    <td colspan="4">
                                                                        <table class="headerPopUp" style="border: 1px solid #86b2d1" width="100%">
                                                                            <tr>
                                                                                <td>
                                                                                    Complaint Management
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <tr>
                                                                        <td colspan="4">
                                                                        </td>
                                                                    </tr>
                                                                    <td style="width: 25%; height: 34px;" class="leftCol">
                                                                        Customer *
                                                                        <asp:CompareValidator ID="cvCustomerAdd" runat="server" ControlToValidate="drpCustomerAdd"
                                                                            ValidationGroup="add" Display="Dynamic" ErrorMessage="Customer is Mandatory"
                                                                            Operator="GreaterThan" Text="*" ValueToCompare="0"></asp:CompareValidator>
                                                                    </td>
                                                                    <td style="width: 25%">
                                                                        <div style="border-width: 1px; border-color: #bce1fe; border-style: solid; width: 99%;
                                                                            font-family: 'Trebuchet MS';">
                                                                            <asp:DropDownList TabIndex="1" ID="drpCustomerAdd" AppendDataBoundItems="true" SkinID="skinDdlBox"
                                                                                DataSourceID="srcDebitors" runat="server" AutoPostBack="false" DataValueField="LedgerID"
                                                                                DataTextField="LedgerName" ValidationGroup="edit">
                                                                                <asp:ListItem style="background-color: #bce1fe" Text="Select Customer" Value="0"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </td>
                                                                    <td style="width: 25%" class="leftCol">
                                                                        Complaint Date *
                                                                        <asp:RequiredFieldValidator ID="rvStockAdd" runat="server" ValidationGroup="add"
                                                                            ControlToValidate="txtComplaintDateAdd" Display="Dynamic" EnableClientScript="true"
                                                                            ErrorMessage="Date is mandatory">*</asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <td style="width: 25%">
                                                                        <asp:TextBox ID="txtComplaintDateAdd" CssClass="cssTextBox" runat="server" ValidationGroup="editVal"
                                                                            Text='<%# Bind("ComplaintDate","{0:dd/MM/yyyy}") %>' Width="100px" MaxLength="10"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="calEx3" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                            PopupButtonID="btnDate3" PopupPosition="BottomLeft" TargetControlID="txtComplaintDateAdd">
                                                                        </cc1:CalendarExtender>
                                                                        <asp:ImageButton ID="btnDate3" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                            Width="20px" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 25%" class="leftCol">
                                                                        Assigned To:
                                                                        <asp:RequiredFieldValidator ID="reqAssignedToAdd" ErrorMessage="Assigned To is mandatory"
                                                                            InitialValue="0" EnableClientScript="true" Text="*" ControlToValidate="drpAssignedToAdd"
                                                                            runat="server" ValidationGroup="add" Display="Dynamic" />
                                                                    </td>
                                                                    <td style="width: 25%">
                                                                        <div style="border-width: 1px; border-color: #bce1fe; border-style: solid; width: 99%;
                                                                            font-family: 'Trebuchet MS';">
                                                                            <asp:DropDownList ID="drpAssignedToAdd" TabIndex="4" Enabled="True" AppendDataBoundItems="true"
                                                                                DataSourceID="srcAssinedTo" runat="server" SkinID="skinDdlBox" DataTextField="empFirstName"
                                                                                DataValueField="empno">
                                                                                <asp:ListItem Text="Assigned To" style="background-color: #bce1fe" Value="0"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </td>
                                                                    <td style="width: 25%" class="leftCol">
                                                                        Status:
                                                                    </td>
                                                                    </td>
                                                                    <td style="width: 25%">
                                                                        <div style="border-width: 1px; border-color: #bce1fe; border-style: solid; width: 99%;
                                                                            font-family: 'Trebuchet MS';">
                                                                            <asp:DropDownList ID="drpStatusAdd" runat="server" SkinID="skinDdlBox" AutoPostBack="False"
                                                                                AppendDataBoundItems="true">
                                                                                <asp:ListItem Text="In Progress" Value="In Progress"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr class="tblLeft">
                                                                    <td style="width: 25%;" class="leftCol">
                                                                        Is Billed:
                                                                    </td>
                                                                    <td style="width: 25%;">
                                                                        <div style="border-width: 1px; border-color: #bce1fe; border-style: solid; width: 99%;
                                                                            font-family: 'Trebuchet MS';">
                                                                            <asp:DropDownList ID="drpBilledAdd" runat="server" SkinID="skinDdlBox" AutoPostBack="False"
                                                                                AppendDataBoundItems="true">
                                                                                <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                                                <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </td>
                                                                    <td style="width: 25%">
                                                                    </td>
                                                                    <td style="width: 25%">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 25%; vertical-align: text-top" class="leftCol">
                                                                        Details *
                                                                        <asp:RequiredFieldValidator ID="rvDetailsAdd" runat="server" ControlToValidate="txtComplaintDetialsAdd"
                                                                            ValidationGroup="add" Display="Dynamic" EnableClientScript="true" ErrorMessage="Details is mandatory">*</asp:RequiredFieldValidator>
                                                                        <asp:RegularExpressionValidator ID="RegExpr2" runat="server" ControlToValidate="txtComplaintDetialsAdd"
                                                                            Display="Dynamic" ErrorMessage="Please limit to 255 characters or less." ValidationGroup="add"
                                                                            Text="*" ValidationExpression="[\s\S]{1,255}"></asp:RegularExpressionValidator>
                                                                    </td>
                                                                    <td style="width: 75%" colspan="3">
                                                                        <asp:TextBox ID="txtComplaintDetialsAdd" ValidationGroup="editVal" runat="server"
                                                                            TextMode="MultiLine" Height="50px" MaxLength="1000" CssClass="cssTextBox" Width="99%"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 25%" colspan="2" align="left">
                                                                        <table cellspacing="0" cellpadding="0">
                                                                            <tr>
                                                                                <td style="width: 67px">
                                                                                    <asp:Button ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                        SkinID="skinBtnCancel" Text="Cancel" OnClick="InsertCancelButton_Click"></asp:Button>
                                                                                </td>
                                                                                <td style="height: 26px">
                                                                                </td>
                                                                                <td style="height: 26px">
                                                                                    <asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                                                                        ValidationGroup="add" SkinID="skinBtnSave" Text="Save" OnClick="InsertButton_Click">
                                                                                    </asp:Button>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td style="width: 25%">
                                                                    </td>
                                                                    <td style="width: 25%">
                                                                    </td>
                                                                </tr>
                                                        </table>
                                                        <asp:ValidationSummary ID="valSumAdd" DisplayMode="BulletList" ShowMessageBox="true"
                                                            ShowSummary="false" HeaderText="Validation Messages" ValidationGroup="add" Font-Names="'Trebuchet MS'"
                                                            Font-Size="12" runat="server" />
                                                    </div>
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
                    <td style="width: 100%;">
                        <div class="mainGridHold" id="searchGrid">
                            <br />
                            <asp:GridView ID="GrdViewComplaint" runat="server" AutoGenerateColumns="False" Width="100%"
                                DataKeyNames="ComplaintID" AllowPaging="True" EmptyDataText="No Complaints found!"
                                OnRowCreated="GrdViewComplaint_RowCreated" OnSelectedIndexChanged="GrdViewComplaint_SelectedIndexChanged"
                                OnRowCommand="GrdViewComplaint_RowCommand" OnPageIndexChanging="GrdViewComplaint_PageIndexChanging"
                                OnRowDeleting="GrdViewComplaint_RowDeleting" OnSorting="GrdViewComplaint_Sorting"
                                OnRowDataBound="GrdViewComplaint_RowDataBound" CssClass="someClass">
                                <EmptyDataRowStyle CssClass="GrdContent" />
                                <Columns>
                                    <asp:BoundField DataField="ComplaintID" HeaderText="Complaint ID" HeaderStyle-Wrap="false"  HeaderStyle-BorderColor="Blue"/>
                                    <asp:BoundField DataField="ComplaintDate" HeaderText="Date" HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Blue"
                                        DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField DataField="ComplaintDetails" HeaderText="Details" HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Blue"
                                        Visible="false" />
                                    <asp:BoundField DataField="CustomerName" HeaderText="Customer" HeaderStyle-Wrap="false"  HeaderStyle-BorderColor="Blue"/>
                                    <asp:BoundField DataField="ComplaintStatus" HeaderText="Status"  HeaderStyle-BorderColor="Blue"/>
                                    <asp:BoundField DataField="AssignedTo" HeaderText="AssignedTo"  HeaderStyle-BorderColor="Blue"/>
                                    <asp:BoundField DataField="IsBilled" HeaderText="Is Billed"  HeaderStyle-BorderColor="Blue"/>
                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="50px" HeaderText="Edit" HeaderStyle-BorderColor="Blue">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Select" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="50px" HeaderText="Delete" HeaderStyle-BorderColor="Blue">
                                        <ItemTemplate>
                                            <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to Delete this Complaint?"
                                                runat="server">
                                            </cc1:ConfirmButtonExtender>
                                            <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" CommandName="Delete"></asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
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
                                                <asp:DropDownList ID="ddlPageSelector" runat="server" OnSelectedIndexChanged="ddlPageSelector_SelectedIndexChanged"
                                                    AutoPostBack="true" SkinID="skinPagerDdlBox">
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
                <tr width="100%">
                    <td style="width: 918px" align="left">
                        <asp:ObjectDataSource ID="srcAssinedTo" runat="server" SelectMethod="ListExecutive"
                            TypeName="BusinessLogic">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="hdDataSource" Name="connection" Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="srcDebitors" runat="server" SelectMethod="ListSundryDebtors"
                            TypeName="BusinessLogic">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="hdDataSource" Name="connection" Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="frmSource" runat="server" SelectMethod="GetComplaintForId"
                            TypeName="BusinessLogic" InsertMethod="InsertComplaint" OnUpdating="frmSource_Updating"
                            OnInserting="frmSource_Inserting" UpdateMethod="UpdateComplaint" OnInserted="frmSource_Inserted"
                            OnUpdated="frmSource_Updated">
                            <UpdateParameters>
                                <asp:Parameter Name="ComplaintID" Type="Int32" />
                                <asp:Parameter Name="ComplaintDate" Type="DateTime" />
                                <asp:Parameter Name="CustomerID" Type="Int32" />
                                <asp:Parameter Name="ComplaintStatus" Type="String" />
                                <asp:Parameter Name="ComplaintDetails" Type="String" />
                                <asp:Parameter Name="AssignedTo" Type="Int32" />
                                <asp:Parameter Name="IsBilled" Type="String" />
                                <asp:Parameter Name="sPath" Type="String" />
                            </UpdateParameters>
                            <SelectParameters>
                                <asp:ControlParameter ControlID="GrdViewComplaint" Name="ComplaintID" PropertyName="SelectedValue"
                                    Type="Int32" />
                                <asp:ControlParameter ControlID="hdDataSource" Name="ConStr" Type="String" />
                            </SelectParameters>
                            <InsertParameters>
                                <asp:Parameter Name="ComplaintDate" Type="DateTime" />
                                <asp:Parameter Name="CustomerID" Type="Int32" />
                                <asp:Parameter Name="ComplaintStatus" Type="String" />
                                <asp:Parameter Name="AssignedTo" Type="Int32" />
                                <asp:Parameter Name="ComplaintDetails" Type="String" />
                                <asp:Parameter Name="IsBilled" Type="String" />
                                <asp:Parameter Name="sPath" Type="String" />
                                <asp:Parameter Name="ComplaintID" Type="Int32" Direction="Output" />
                            </InsertParameters>
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="GridSource" runat="server" SelectMethod="ListComplaints"
                            TypeName="BusinessLogic" DeleteMethod="DeleteComplaint" OnDeleting="GridSource_Deleting">
                            <DeleteParameters>
                                <asp:Parameter Name="ComplaintID" Type="Int32" />
                                <asp:Parameter Name="sPath" Type="String" />
                            </DeleteParameters>
                        </asp:ObjectDataSource>
                        <asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="true" ShowSummary="false" />
                        <asp:HiddenField ID="hdDataSource" runat="server" />
                        <asp:HiddenField ID="hdJournal" runat="server" />
                    </td>
                </tr>
            </table>
            <br />
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
