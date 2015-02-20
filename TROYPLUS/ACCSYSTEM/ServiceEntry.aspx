<%@ Page Title="Service Entry" Language="C#" MasterPageFile="~/PageMaster.master"
    AutoEventWireup="true" CodeFile="ServiceEntry.aspx.cs" Inherits="ServiceEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel16" runat="server" UpdateMode="Always">
        <ContentTemplate>
            
            <table style="width: 100%">
                <tr style="width: 100%">
                    <td style="width: 100%">
                        
                            <%--<div class="mainConHd">
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tr valign="middle">
                                        <td>
                                            <span>Service Contracts</span>
                                        </td>
                                    </tr>
                                </table>
                            </div>--%>
                            <%--<table class="mainConHd" style="width: 994px;">
                                <tr valign="middle">
                                    <td style="font-size: 20px;">
                                        Service Contracts
                                    </td>
                                </tr>
                            </table>--%>
                            <div class="mainConBody">
                                <table style="width: 100%; margin: -1px 0px 0px 1px;" cellpadding="3" cellspacing="2" class="searchbg">
                                    <tr style="height: 25px; vertical-align: middle">
                                        <td style="width: 23%; font-size: 22px; color: #000000;">
                                            Service Contracts
                                        </td>
                                        <td style="width: 15%">
                                            <div style="text-align: right;">
                                                <asp:Panel ID="pnlSearch" runat="server" Width="100px">
                                                    <asp:Button ID="lnkBtnAdd" runat="server" OnClick="lnkBtnAdd_Click" CssClass="ButtonAdd66"
                                                        EnableTheming="false" Width="80px" Text=""></asp:Button>
                                                </asp:Panel>
                                            </div>
                                        </td>
                                        <td style="width: 15%; color: #000080;" align="right">
                                            Search
                                        </td>
                                        <td style="width: 18%" class="Box1">
                                            <asp:TextBox ID="txtSearch" runat="server" SkinID="skinTxtBoxSearch"></asp:TextBox>
                                        </td>
                                        <td style="width: 18%" class="Box1">
                                            <div style="width: 150px; font-family: 'Trebuchet MS';">
                                                <asp:DropDownList ID="ddCriteria" runat="server"  Width="155px" Height="23px" BackColor="#BBCAFB" style="text-align:center;border:1px solid #BBCAFB ">
                                                    <asp:ListItem Value="RefNumber">Ref. No.</asp:ListItem>
                                                    <asp:ListItem Value="Details">Details</asp:ListItem>
                                                    <asp:ListItem Value="Ledger">Customer / Dealer</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </td>
                                        <td style="width: 22%" class="tblLeftNoPad">
                                            <asp:Button ID="btnSearch" runat="server" Text=""  CssClass="ButtonSearch6" EnableTheming="false" OnClick="btnSearch_Click" />
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
                        <asp:Panel runat="server" ID="popUp" Style="width: 60%">
                            <div id="contentPopUp">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                                    <ContentTemplate>
                                        <table class="tblLeft" cellpadding="0" cellspacing="0" style="border: 0px solid #5078B3;
                                            background-color: #fff; color: #000;" width="100%">
                                            <tr>
                                                <td>
                                                    <asp:FormView ID="frmViewAdd" runat="server" Width="100%" DataSourceID="frmSource"
                                                        OnItemInserting="frmViewAdd_ItemInserting" DefaultMode="Edit" DataKeyNames="ServiceID"
                                                        Visible="False" OnItemUpdated="frmViewAdd_ItemUpdated" OnItemCreated="frmViewAdd_ItemCreated"
                                                        OnItemInserted="frmViewAdd_ItemInserted" OnItemUpdating="frmViewAdd_ItemUpdating">
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
                                                                            Service Entry
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="height:5px">
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="ControlLabel" style="width:20%">
                                                                            Ref. No.
                                                                            <asp:RequiredFieldValidator ID="rvRefNo" ErrorMessage="Customer / Dealer is mandatory"
                                                                                InitialValue="0" EnableClientScript="true" Text="*" ControlToValidate="txtRefNo"
                                                                                runat="server" Display="Dynamic" />
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtRefNo"
                                                                                FilterType="Numbers" />
                                                                        </td>
                                                                        <td class="ControlTextBox3" style="width:25%">
                                                                            <asp:TextBox ID="txtRefNo" runat="server" Text='<%# Bind("RefNumber") %>' Enabled="false"
                                                                                Width="95%" CssClass="cssTextBox"></asp:TextBox>
                                                                        </td>
                                                                        <td class="ControlLabel" style="width:15%">
                                                                            Customer *
                                                                            <asp:CompareValidator ID="cvCustomer" runat="server" ControlToValidate="drpCustomer"
                                                                                Display="Dynamic" ErrorMessage="Customer / Dealer is Mandatory" Operator="GreaterThan"
                                                                                Text="*" ValueToCompare="0"></asp:CompareValidator>
                                                                        </td>
                                                                        <td class="ControlDrpBorder" style="width:25%">
                                                                            <asp:DropDownList ID="drpCustomer" runat="server" OnDataBound="ComboBox2_DataBound" CssClass="drpDownListMedium" BackColor = "#90c9fc"
                                                                                AutoPostBack="False" DataSourceID="srcCreditorDebitor" Enabled="false" DataValueField="LedgerID" Width="100%" style="border:1px solid #90c9fc" height="26px"
                                                                                DataTextField="LedgerName" AppendDataBoundItems="true">
                                                                                <asp:ListItem style="background-color: #90c9fc" Text="Select Customer" Value="0"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr  style="height:3px">
                                                            </tr>
                                                                    <tr>
                                                                        <td class="ControlLabel" style="width:20%">
                                                                            Frequency
                                                                        </td>
                                                                        <td class="ControlDrpBorder" style="width:25%">
                                                                            <asp:DropDownList ID="drpFrequency" TabIndex="4" AppendDataBoundItems="True" CssClass="drpDownListMedium" BackColor = "#90c9fc"
                                                                                Width="100%" runat="server" AutoPostBack="false" SelectedValue='<%# Bind("Frequency") %>' style="border:1px solid #90c9fc" height="26px"
                                                                                ValidationGroup="salesval">
                                                                                <asp:ListItem Text="Monthly" Value="1"></asp:ListItem>
                                                                                <asp:ListItem Text="Quarterly" Value="3"></asp:ListItem>
                                                                                <asp:ListItem Text="Annually" Value="12"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td class="ControlLabel" style="width:15%">
                                                                            Start Date *
                                                                            <asp:RequiredFieldValidator ID="rvStartDate" runat="server" ControlToValidate="txtStartDate"
                                                                                Display="Dynamic" EnableClientScript="true" ErrorMessage="StartDate is mandatory">*</asp:RequiredFieldValidator>
                                                                        </td>
                                                                        <td class="ControlTextBox3" style="width:25%">
                                                                            <asp:TextBox ID="txtStartDate" CssClass="cssTextBox" runat="server" Text='<%# Bind("StartDate","{0:dd/MM/yyyy}") %>'
                                                                                Width="100px" MaxLength="10"></asp:TextBox>
                                                                            <cc1:CalendarExtender ID="calExtender3" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                                PopupButtonID="btnDate3" PopupPosition="BottomLeft" TargetControlID="txtStartDate">
                                                                            </cc1:CalendarExtender>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:ImageButton ID="btnDate3" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                                Width="20px" runat="server" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr  style="height:3px">
                                                            </tr>
                                                                    <tr>
                                                                        <td class="ControlLabel" style="width:20%">
                                                                            Amount *
                                                                            <asp:RequiredFieldValidator ID="rvAmount" runat="server" ControlToValidate="txtAmount"
                                                                                ErrorMessage="Amount is mandatory" Display="Dynamic" EnableClientScript="True">*</asp:RequiredFieldValidator>
                                                                            <cc1:FilteredTextBoxExtender ID="fltAmt" runat="server" TargetControlID="txtAmount"
                                                                                ValidChars="." FilterType="Numbers, Custom" />
                                                                        </td>
                                                                        <td class="ControlTextBox3" style="width:25%">
                                                                            <asp:TextBox ID="txtAmount" runat="server" Text='<%# Bind("Amount") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                        </td>
                                                                        <td class="ControlLabel" style="width:15%">
                                                                            End Date *
                                                                            <asp:RequiredFieldValidator ID="rvEndDate" runat="server" ControlToValidate="txtEndDate"
                                                                                Display="Dynamic" EnableClientScript="true" ErrorMessage="EndDate is mandatory">*</asp:RequiredFieldValidator>
                                                                        </td>
                                                                        <td class="ControlTextBox3" style="width:25%">
                                                                            <asp:TextBox ID="txtEndDate" CssClass="cssTextBox" runat="server" Text='<%# Bind("EndDate","{0:dd/MM/yyyy}") %>'
                                                                                Width="100px" MaxLength="10"></asp:TextBox>
                                                                            <cc1:CalendarExtender ID="calEndDate" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                                PopupButtonID="imgBtnEndDate" PopupPosition="BottomLeft" TargetControlID="txtEndDate">
                                                                            </cc1:CalendarExtender>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:ImageButton ID="imgBtnEndDate" ImageUrl="App_Themes/NewTheme/images/cal.gif"
                                                                                CausesValidation="false" Width="20px" runat="server" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr  style="height:3px">
                                                            </tr>
                                                                    <tr>
                                                                        <td class="ControlLabel" style="width:20%">
                                                                            Details *
                                                                            <asp:RequiredFieldValidator ID="rvDetails" runat="server" ControlToValidate="txtDetials"
                                                                                Display="Dynamic" EnableClientScript="true" ErrorMessage="Details is mandatory">*</asp:RequiredFieldValidator>
                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtDetials"
                                                                                Display="Dynamic" ErrorMessage="Please limit to 255 characters or less." Text="*"
                                                                                ValidationExpression="[\s\S]{1,255}"></asp:RegularExpressionValidator>
                                                                        </td>
                                                                        <td class="ControlTextBox3" style="width:20%">
                                                                            <asp:TextBox ID="txtDetials" runat="server" TextMode="MultiLine" Height="50px" MaxLength="100"
                                                                                Text='<%# Bind("Details") %>' CssClass="cssTextBox" Width="99%"></asp:TextBox>
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
                                                                        <td align="center">
                                                                            <asp:Button ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                CssClass="cancelbutton6" EnableTheming="false" SkinID="skinBtnCancel" OnClick="UpdateCancelButton_Click">
                                                                            </asp:Button>
                                                                        </td>
                                                                        <td align="right">
                                                                            <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update"
                                                                                CssClass="Updatebutton1231" EnableTheming="false" SkinID="skinBtnSave" OnClick="UpdateButton_Click">
                                                                            </asp:Button>
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
                                                                        <asp:ObjectDataSource ID="srcCreditorDebitor" runat="server" SelectMethod="ListCustomersDealers"
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
                                                        <InsertItemTemplate>
                                                            <div class="divArea">
                                                                <table cellpadding="1" cellspacing="1" style="border: 1px solid #86b2d1; width: 100%;">
                                                                    <tr>
                                                                        <td colspan="5" class="headerPopUp">
                                                                            Service Entry
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="height:5px">
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="ControlLabel" style="width:20%">
                                                                            Ref. No.
                                                                            <asp:RequiredFieldValidator ID="rvRefNoAdd" ErrorMessage="Customer / Dealer is mandatory"
                                                                                InitialValue="0" EnableClientScript="true" Text="*" ControlToValidate="txtRefNoAdd"
                                                                                runat="server" Display="Dynamic" />
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExt1" runat="server" TargetControlID="txtRefNoAdd"
                                                                                FilterType="Numbers" />
                                                                        </td>
                                                                        <td class="ControlTextBox3" style="width:25%">
                                                                            <asp:TextBox ID="txtRefNoAdd" runat="server" Text='<%# Bind("RefNumber") %>' Width="95%"
                                                                                CssClass="cssTextBox"></asp:TextBox>
                                                                        </td>
                                                                        <td class="ControlLabel" style="width:15%">
                                                                            Customer *
                                                                            <asp:CompareValidator ID="cvCustomerAdd" runat="server" ControlToValidate="drpCustomerAdd"
                                                                                Display="Dynamic" ErrorMessage="Customer / Dealer is Mandatory" Operator="GreaterThan"
                                                                                Text="*" ValueToCompare="0"></asp:CompareValidator>
                                                                        </td>
                                                                        <td class="ControlDrpBorder" style="width:25%">
                                                                            <asp:DropDownList ID="drpCustomerAdd" runat="server" AutoPostBack="False" CssClass="drpDownListMedium" BackColor = "#90c9fc"
                                                                                DataSourceID="srcCreditorDebitorAdd" DataValueField="LedgerID" DataTextField="LedgerName" Width="100%" style="border:1px solid #90c9fc" height="26px"
                                                                                AppendDataBoundItems="true">
                                                                                <asp:ListItem style="background-color: #90c9fc" Text="Select Customer" Value="0"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr  style="height:3px">
                                                            </tr>
                                                                    <tr>
                                                                        <td class="ControlLabel" style="width:20%">
                                                                            Frequency
                                                                        </td>
                                                                        <td align="left" class="ControlDrpBorder" style="width:25%">
                                                                            <asp:DropDownList ID="drpFrequencyAdd" TabIndex="4" AppendDataBoundItems="True" CssClass="drpDownListMedium" BackColor = "#90c9fc"
                                                                                Width="100%" runat="server" AutoPostBack="false" SelectedValue='<%# Bind("Frequency") %>' style="border:1px solid #90c9fc" height="26px"
                                                                                ValidationGroup="salesval">
                                                                                <asp:ListItem Text="Monthly" Value="1"></asp:ListItem>
                                                                                <asp:ListItem Text="Quarterly" Value="3"></asp:ListItem>
                                                                                <asp:ListItem Text="Annually" Value="12"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td class="ControlLabel" style="width:15%">
                                                                            Start Date *
                                                                            <asp:RequiredFieldValidator ID="rvStartDateAdd" runat="server" ControlToValidate="txtStartDateAdd"
                                                                                Display="Dynamic" EnableClientScript="true" ErrorMessage="StartDate is mandatory">*</asp:RequiredFieldValidator>
                                                                        </td>
                                                                        <td class="ControlTextBox3" style="width:25%">
                                                                            <asp:TextBox ID="txtStartDateAdd" CssClass="cssTextBox" runat="server" Text='<%# Bind("StartDate","{0:dd/MM/yyyy}") %>'
                                                                                Width="100px" MaxLength="10"></asp:TextBox>
                                                                            <cc1:CalendarExtender ID="calStartDateAdd" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                                PopupButtonID="btnDate3" PopupPosition="BottomLeft" TargetControlID="txtStartDateAdd">
                                                                            </cc1:CalendarExtender>
                                                                        </td>
                                                                        <td align="left" style="width:10%">
                                                                            <asp:ImageButton ID="btnDate3" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                                Width="20px" runat="server" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr  style="height:3px">
                                                            </tr>
                                                                    <tr>
                                                                        <td class="ControlLabel" style="width:20%">
                                                                            Amount *
                                                                            <asp:RequiredFieldValidator ID="rvAmountAdd" runat="server" ControlToValidate="txtAmountAdd"
                                                                                ErrorMessage="Amount is mandatory" Display="Dynamic" EnableClientScript="True">*</asp:RequiredFieldValidator>
                                                                            <cc1:FilteredTextBoxExtender ID="fltAmtAdd" runat="server" TargetControlID="txtAmountAdd"
                                                                                ValidChars="." FilterType="Numbers,Custom" />
                                                                        </td>
                                                                        <td class="ControlTextBox3" style="width:25%">
                                                                            <asp:TextBox ID="txtAmountAdd" runat="server" Text='<%# Bind("Amount") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                        </td>
                                                                        <td class="ControlLabel" style="width:15%">
                                                                            End Date *
                                                                            <asp:RequiredFieldValidator ID="rvEndDateAdd" runat="server" ControlToValidate="txtEndDateAdd"
                                                                                Display="Dynamic" EnableClientScript="true" ErrorMessage="EndDate is mandatory">*</asp:RequiredFieldValidator>
                                                                        </td>
                                                                        <td class="ControlTextBox3" style="width:25%">
                                                                            <asp:TextBox ID="txtEndDateAdd" CssClass="cssTextBox" runat="server" Text='<%# Bind("EndDate","{0:dd/MM/yyyy}") %>'
                                                                                Width="100px" MaxLength="10"></asp:TextBox>
                                                                            <cc1:CalendarExtender ID="calEndDateAdd" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                                PopupButtonID="imgEndDateAdd" PopupPosition="BottomLeft" TargetControlID="txtEndDateAdd">
                                                                            </cc1:CalendarExtender>
                                                                        </td>
                                                                        <td align="left" style="width:10%">
                                                                            <asp:ImageButton ID="imgEndDateAdd" ImageUrl="App_Themes/NewTheme/images/cal.gif"
                                                                                CausesValidation="false" Width="20px" runat="server" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr  style="height:3px">
                                                            </tr>
                                                                    <tr>
                                                                        <td class="ControlLabel" style="width:20%">
                                                                            Details *
                                                                            <asp:RequiredFieldValidator ID="rvDetailsAdd" runat="server" ControlToValidate="txtDetialsAdd"
                                                                                Display="Dynamic" EnableClientScript="true" ErrorMessage="Details is mandatory">*</asp:RequiredFieldValidator>
                                                                            <asp:RegularExpressionValidator ID="rvdetailAdd" runat="server" ControlToValidate="txtDetialsAdd"
                                                                                Display="Dynamic" ErrorMessage="Please limit to 255 characters or less." Text="*"
                                                                                ValidationExpression="[\s\S]{1,255}"></asp:RegularExpressionValidator>
                                                                        </td>
                                                                        <td class="ControlTextBox3" style="width:25%">
                                                                            <asp:TextBox ID="txtDetialsAdd" runat="server" TextMode="MultiLine" Height="50px"
                                                                                MaxLength="100" Text='<%# Bind("Details") %>' CssClass="cssTextBox" Width="99%"></asp:TextBox>
                                                                        </td>
                                                                        <td style="width:15%">
                                                                        </td>
                                                                        <td style="width:25%">
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
                                                                            <asp:Button ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                CssClass="cancelbutton6" EnableTheming="false" SkinID="skinBtnCancel" OnClick="InsertCancelButton_Click">
                                                                            </asp:Button>
                                                                        </td>
                                                                        <td align="right">
                                                                            <asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                                                                CssClass="savebutton1231" EnableTheming="false" SkinID="skinBtnSave" OnClick="InsertButton_Click">
                                                                            </asp:Button>
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
                                                                        <asp:ObjectDataSource ID="srcCreditorDebitorAdd" runat="server" SelectMethod="ListCustomersDealers"
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
                                                        </InsertItemTemplate>
                                                    </asp:FormView>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </asp:Panel>
                    </td>
                </tr>
                <tr style="width: 100%">
                    <td style="width: 100%;">
                        <table width="100%" style="margin: -3px 0px 0px 0px;">
                                <tr style="width: 100%">
                                    <td>
                        <div class="mainGridHold" id="searchGrid">
                            <asp:GridView ID="GrdViewSerEntry" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                OnRowCreated="GrdViewSerEntry_RowCreated" Width="99.9%" DataSourceID="GridSource" CssClass="someClass"
                                AllowPaging="True" DataKeyNames="ServiceID" EmptyDataText="No Service Entry found!"
                                OnRowCommand="GrdViewSerEntry_RowCommand" OnRowDataBound="GrdViewSerEntry_RowDataBound"
                                OnSelectedIndexChanged="GrdViewSerEntry_SelectedIndexChanged" OnRowDeleting="GrdViewSerEntry_RowDeleting"
                                OnRowDeleted="GrdViewSerEntry_RowDeleted">
                                <EmptyDataRowStyle CssClass="GrdContent" />
                                <Columns>
                                    <asp:BoundField DataField="ServiceID" HeaderText="Service ID" HeaderStyle-Wrap="false"  HeaderStyle-BorderColor="Gray"/>
                                    <asp:BoundField DataField="RefNumber" HeaderText="Ref. No." HeaderStyle-Wrap="false"  HeaderStyle-BorderColor="Gray"/>
                                    <asp:BoundField DataField="LedgerName" HeaderText="Customer / Dealer" HeaderStyle-Wrap="false"  HeaderStyle-BorderColor="Gray"/>
                                    <asp:BoundField DataField="Frequency" HeaderText="Frequency" HeaderStyle-Wrap="false"  HeaderStyle-BorderColor="Gray"/>
                                    <asp:BoundField DataField="StartDate" HeaderText="Start Date" DataFormatString="{0:dd/MM/yyyy}"  HeaderStyle-BorderColor="Gray"/>
                                    <asp:BoundField DataField="EndDate" HeaderText="End Date" DataFormatString="{0:dd/MM/yyyy}"   HeaderStyle-BorderColor="Gray"/>
                                    <asp:BoundField DataField="Amount" HeaderText="Amount"   HeaderStyle-BorderColor="Gray" />
                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderText="Edit" HeaderStyle-Width="50px"  HeaderStyle-BorderColor="Gray">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Select" />
                                            <asp:ImageButton ID="btnEditDisabled" Enabled="false" SkinID="editDisable" runat="Server"></asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderText="Delete" HeaderStyle-Width="50px" HeaderStyle-BorderColor="Gray">
                                        <ItemTemplate>
                                            <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to Delete this Service Entry?"
                                                runat="server">
                                            </cc1:ConfirmButtonExtender>
                                            <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" CommandName="Delete"></asp:ImageButton>
                                            <asp:ImageButton ID="lnkBDisabled" Enabled="false" SkinID="deleteDisable" runat="Server"></asp:ImageButton>
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
                <tr width="100%">
                    <td style="width: 918px" align="left">
                        <asp:ObjectDataSource ID="srcDebitors" runat="server" SelectMethod="ListSundryDebtors"
                            TypeName="BusinessLogic">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="hdDataSource" Name="connection" Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="frmSource" runat="server" SelectMethod="GetServiceEntryForId"
                            TypeName="BusinessLogic" InsertMethod="InsertServiceEntry" OnUpdating="frmSource_Updating"
                            OnInserting="frmSource_Inserting" UpdateMethod="UpdateServiceEntry" OnInserted="frmSource_Inserted"
                            OnUpdated="frmSource_Updated">
                            <UpdateParameters>
                                <asp:Parameter Name="ServiceID" Type="Int32" />
                                <asp:Parameter Name="RefNumber" Type="String" />
                                <asp:Parameter Name="CustomerID" Type="Int32" />
                                <asp:Parameter Name="Details" Type="String" />
                                <asp:Parameter Name="Amount" Type="Double" />
                                <asp:Parameter Name="Frequency" Type="Int32" />
                                <asp:Parameter Name="StartDate" Type="DateTime" />
                                <asp:Parameter Name="EndDate" Type="DateTime" />
                                <asp:CookieParameter CookieName="Company" Type="String" Name="connection" />
                            </UpdateParameters>
                            <SelectParameters>
                                <asp:ControlParameter ControlID="GrdViewSerEntry" Name="ServiceID" PropertyName="SelectedValue"
                                    Type="Int32" />
                                <asp:CookieParameter CookieName="Company" Type="String" Name="connection" />
                            </SelectParameters>
                            <InsertParameters>
                                <asp:Parameter Name="RefNumber" Type="String" />
                                <asp:Parameter Name="CustomerID" Type="Int32" />
                                <asp:Parameter Name="Details" Type="String" />
                                <asp:Parameter Name="Amount" Type="Double" />
                                <asp:Parameter Name="Frequency" Type="Int32" />
                                <asp:Parameter Name="StartDate" Type="DateTime" />
                                <asp:Parameter Name="EndDate" Type="DateTime" />
                                <asp:CookieParameter CookieName="Company" Type="String" Name="connection" />
                            </InsertParameters>
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="GridSource" runat="server" SelectMethod="ListServiceEntries"
                            TypeName="BusinessLogic" DeleteMethod="DeleteServiceEntry" OnDeleting="GridSource_Deleting">
                            <DeleteParameters>
                                <asp:Parameter Name="ServiceID" Type="Int32" />
                                <asp:CookieParameter CookieName="Company" Type="String" Name="connection" />
                            </DeleteParameters>
                        </asp:ObjectDataSource>
                        <asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="true" ShowSummary="false" />
                        <asp:HiddenField ID="hdDataSource" runat="server" />
                        <asp:HiddenField ID="hdServiceEntry" runat="server" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
