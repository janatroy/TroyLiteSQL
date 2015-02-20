<%@ Page Title="" Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="ServiceMGTEntry.aspx.cs" Inherits="ServiceMGTEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <script language="javascript" type="text/javascript">
        function OpenWindow() {
            var ddLedger = document.getElementById('ctl00_cplhControlPanel_drpCustomer');
            var iLedger = ddLedger.options[ddLedger.selectedIndex].text;
            window.open('Service.aspx?ID=' + iLedger, '', "height=400, width=700,resizable=yes, toolbar =no");
            return false;
        }
    </script>
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
                                        Service Visits
                                    </td>
                                </tr>
                            </table>--%>
                            <div class="mainConBody">
                                <table style="width: 100%; margin: -1px 0px 0px 1px;" cellpadding="3" cellspacing="2" class="searchbg">
                                    <tr style="height: 25px; vertical-align: middle">
                                        <td style="width: 2%">
                                        </td>
                                        <td style="width: 23%; font-size: 22px; color: #000000;" >
                                                Service Visits
                                        </td>
                                        <td style="width: 12%">
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
                                            <div style="width: 160px; font-family: 'Trebuchet MS';">
                                                <asp:DropDownList ID="ddCriteria" runat="server" Width="154px" Height="23px" BackColor="#BBCAFB" style="text-align:center;border:1px solid #BBCAFB ">
                                                    <asp:ListItem Value="RefNumber">Ref. No.</asp:ListItem>
                                                    <asp:ListItem Value="Details">Details</asp:ListItem>
                                                    <asp:ListItem Value="Ledger">Customer / Dealer</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </td>
                                        <td style="width: 25%" class="tblLeftNoPad">
                                            <asp:Button ID="btnSearch" runat="server" Text=""  CssClass="ButtonSearch6" EnableTheming="false" OnClick="btnSearch_Click" />
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
                                            <asp:Panel ID="pnlVisitDetails" runat="server" Visible="false">
                                                <div class="divArea">
                                                    <table class="tblLeft" cellpadding="0" cellspacing="1" style="border: 1px solid #5078B3;"
                                                        width="100%">
                                                        <tr>
                                                            <tr>
                                                                <td colspan="5">
                                                                    <table class="headerPopUp" style="border: 1px solid #86b2d1" width="100%">
                                                                        <tr>
                                                                            <td>
                                                                                Service Visit
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 20%" class="ControlLabel">
                                                                    Visited
                                                                </td>
                                                                <td style="width: 25%" class="ControlTextBox3">
                                                                    <asp:CheckBox Text="Yes" ID="chkVisited" Checked="True" runat="server" />
                                                                </td>
                                                                <td style="width: 20%" class="ControlLabel">
                                                                    Search Services
                                                                </td>
                                                                <td style="width: 25%" class="ControlTextBox3">
                                                                    <asp:ImageButton ID="btnSearchService" runat="server" OnClientClick="Javascript:return OpenWindow();"
                                                                        CausesValidation="False" SkinID="searchBig" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 20%;" class="ControlLabel">
                                                                    Customer / Dealer *
                                                                    <asp:CompareValidator ID="cvCustomer" runat="server" ControlToValidate="drpCustomer"
                                                                        Display="Dynamic" ErrorMessage="Customer / Dealer is Mandatory" Operator="GreaterThan"
                                                                        Text="*" ValueToCompare="0"></asp:CompareValidator>
                                                                </td>
                                                                <td style="width: 25%" class="ControlDrpBorder">
                                                                    <asp:DropDownList ID="drpCustomer" runat="server" DataValueField="LedgerID" CssClass="drpDownListMedium" BackColor = "#90c9fc"
                                                                        DataTextField="LedgerName" AppendDataBoundItems="True" Width="100%" style="border:1px solid #90c9fc" height="26px">
                                                                        <asp:ListItem Text="Select Cust/Dealer" style="background-color: #90c9fc" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="width: 20%" class="ControlLabel">
                                                                    Ref. No. *
                                                                    <asp:RequiredFieldValidator ID="rvRefNo" runat="server" ControlToValidate="txtRefNo"
                                                                        Display="Dynamic" ErrorMessage="Ref. No. is mandatory">*</asp:RequiredFieldValidator>
                                                                </td>
                                                                <td style="width: 25%" class="ControlTextBox3">
                                                                    <asp:TextBox ID="txtRefNo" runat="server" Text='<%# Bind("RefNumber") %>' Enabled="False"
                                                                        SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <%--<tr style="height:5px">
                                                                <%--<td colspan="4">--%>
                                                                    <%--<hr />--%>
                                                                <%--</td>--%>
                                                            <%--</tr>--%>
                                                            <tr>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 20%" class="ControlLabel">
                                                                    Payment Mode
                                                                </td>
                                                                <td style="width: 25%;" class="ControlDrpBorder">
                                                                    <asp:DropDownList ID="drpPaymode" TabIndex="4" AppendDataBoundItems="True" CssClass="drpDownListMedium" BackColor = "#90c9fc"
                                                                        Width="100%" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpPaymode_SelectedIndexChanged" style="border:1px solid #90c9fc" height="26px"
                                                                        ValidationGroup="salesval">
                                                                        <asp:ListItem Text="Cash" Value="1" Selected="true"></asp:ListItem>
                                                                        <asp:ListItem Text="Bank / Credit Card" Value="2"></asp:ListItem>
                                                                        <asp:ListItem Text="Credit" Value="3"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="width: 20%" class="ControlLabel">
                                                                    Due Date *
                                                                    <asp:RequiredFieldValidator ID="rvDueDate" runat="server" ControlToValidate="txtDueDate"
                                                                        Display="Dynamic" ErrorMessage="DueDate is mandatory">*</asp:RequiredFieldValidator>
                                                                </td>
                                                                <td style="width: 25%" class="ControlTextBox3">
                                                                    <asp:TextBox ID="txtDueDate" CssClass="cssTextBox" runat="server" ReadOnly="true"
                                                                        Text='<%# Bind("DueDate","{0:dd/MM/yyyy}") %>' Width="100px" MaxLength="10"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="calExt3" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                        PopupButtonID="btnD3" PopupPosition="BottomLeft" TargetControlID="txtDueDate">
                                                                    </cc1:CalendarExtender>
                                                                </td>
                                                                <td>
                                                                    <asp:ImageButton ID="btnD3" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                        Width="20px" runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                            </tr>
                                                            <tr class="tblLeft" valign="top">
                                                                <td style="width: 20%" class="ControlLabel">
                                                                    Amount *
                                                                    <asp:RequiredFieldValidator ID="rvAmount" runat="server" ControlToValidate="txtAmount"
                                                                        ErrorMessage="Amount is mandatory" Display="Dynamic">*</asp:RequiredFieldValidator>
                                                                    <cc1:FilteredTextBoxExtender ID="fltAmt" runat="server" TargetControlID="txtAmount"
                                                                        ValidChars="." FilterType="Custom, Numbers" Enabled="True" />
                                                                </td>
                                                                <td style="width: 25%" class="ControlTextBox3">
                                                                    <asp:TextBox ID="txtAmount" runat="server" Text='<%# Bind("Amount") %>' SkinID="skinTxtBoxGrid"
                                                                        Width="99%"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 20%" class="ControlLabel">
                                                                    Visit Date *
                                                                    <asp:RequiredFieldValidator ID="rvVisitDate" runat="server" ControlToValidate="txtVisitDate"
                                                                        Display="Dynamic" ErrorMessage="EndDate is mandatory">*</asp:RequiredFieldValidator>
                                                                    <asp:RangeValidator ID="myRangeValidator" runat="server" ControlToValidate="txtVisitDate"
                                                                        ErrorMessage="Visit date cannot be future date." Text="*" Type="Date"></asp:RangeValidator>
                                                                </td>
                                                                <td style="width: 25%" class="ControlTextBox3">
                                                                    <asp:TextBox ID="txtVisitDate" CssClass="cssTextBox" runat="server" Text='<%# Bind("VisitDate","{0:dd/MM/yyyy}") %>'
                                                                        Width="100px" MaxLength="10"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                        PopupButtonID="ImageButton1" PopupPosition="BottomLeft" TargetControlID="txtVisitDate">
                                                                    </cc1:CalendarExtender>
                                                                </td>
                                                                <td>
                                                                    <asp:ImageButton ID="ImageButton1" ImageUrl="App_Themes/NewTheme/images/cal.gif"
                                                                        CausesValidation="false" Width="20px" runat="server" />
                                                                </td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td colspan="4" style="width: 25%">
                                                                    <asp:UpdatePanel ID="bankPanel" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:Panel ID="pnlBank" runat="Server" Visible="false">
                                                                                <table cellpadding="3" cellspacing="1" class="FntSales" width="100%">
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 20%">
                                                                                            Cheque / Card No. *
                                                                                            <asp:RequiredFieldValidator ID="rvCredit" runat="server" ControlToValidate="txtCreditCardNo"
                                                                                                Enabled="false" ErrorMessage="Cheque\Card number is mandatory" Text="*" ValidationGroup="salesval" />
                                                                                        </td>
                                                                                        <td style="width: 25%" class="ControlTextBox3">
                                                                                            <asp:TextBox ID="txtCreditCardNo" TabIndex="6" runat="server" MaxLength="20" SkinID="skinTxtBoxGridBank"
                                                                                                ValidationGroup="salesval"></asp:TextBox>
                                                                                        </td>
                                                                                        <td class="ControlLabel" style="width: 20%">
                                                                                            Bank Name *
                                                                                            <asp:RequiredFieldValidator ID="rvbank" runat="server" ControlToValidate="drpBankName"
                                                                                                Enabled="false" ErrorMessage="Bankname is mandatory" InitialValue="0" Text="*"
                                                                                                ValidationGroup="salesval" />
                                                                                        </td>
                                                                                        <td style="width: 25%" class="ControlDrpBorder">
                                                                                            <asp:DropDownList ID="drpBankName" TabIndex="7" runat="server" AppendDataBoundItems="true" CssClass="drpDownListMedium" BackColor = "#90c9fc"
                                                                                                DataTextField="LedgerName" DataValueField="LedgerID" Width="98%" style="border:1px solid #90c9fc" height="26px"
                                                                                                ValidationGroup="salesval">
                                                                                                <asp:ListItem style="background-color: #90c9fc" Text="Select Bank" Value="0"></asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 25%">
                                                                                        </td>
                                                                                        <td colspan="3" style="width: 25%">
                                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" FilterType="Numbers"
                                                                                                TargetControlID="txtCreditCardNo" />
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </asp:Panel>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="drpPaymode" EventName="SelectedIndexChanged" />
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 20%; vertical-align: text-top" class="ControlLabel">
                                                                    Visit Details *
                                                                    <asp:RequiredFieldValidator ID="rvDetails" runat="server" ControlToValidate="txtDetials"
                                                                        Display="Dynamic" ErrorMessage="Details is mandatory">*</asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtDetials"
                                                                        Display="Dynamic" ErrorMessage="Please limit to 255 characters or less." Text="*"
                                                                        ValidationExpression="[\s\S]{1,255}"></asp:RegularExpressionValidator>
                                                                </td>
                                                                <td style="width: 25%" colspan="3" class="ControlTextBox3">
                                                                    <asp:TextBox ID="txtDetials" runat="server" TextMode="MultiLine" Height="50px" MaxLength="240"
                                                                        Text='<%# Bind("Details") %>' CssClass="cssTextBox" Width="99%"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 100%" colspan="4">
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td style="width: 32%">
                                                                            </td>
                                                                            <td  style="width: 18%">
                                                                                <asp:Button ID="CancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                    CssClass="cancelbutton6" EnableTheming="false" SkinID="skinBtnCancel" OnClick="UpdateCancelButton_Click">
                                                                                </asp:Button>
                                                                            </td>
                                                                            <td  style="width: 18%">
                                                                                <asp:Button ID="SaveButton" runat="server" CommandName="Update" SkinID="skinBtnSave"
                                                                                    CssClass="savebutton1231" EnableTheming="false" OnClick="SaveButton_Click"></asp:Button>
                                                                                <asp:Button ID="UpdateButton" runat="server" CommandName="Update" SkinID="skinBtnSave"
                                                                                    CssClass="Updatebutton1231" EnableTheming="false" OnClick="UpdateButton_Click"></asp:Button>
                                                                            </td>
                                                                            <td style="width: 32%">
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 25%">
                                                                    <asp:ObjectDataSource ID="srcCreditorDebitor" runat="server" SelectMethod="ListCreditorDebitorSuppliers"
                                                                        TypeName="BusinessLogic">
                                                                        <SelectParameters>
                                                                            <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                        </SelectParameters>
                                                                    </asp:ObjectDataSource>
                                                                </td>
                                                                <td style="width: 25%">
                                                                </td>
                                                            </tr>
                                                    </table>
                                                </div>
                                            </asp:Panel>
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
                                AllowPaging="True" DataKeyNames="VisitID" EmptyDataText="No Service Visit found!"
                                OnRowCommand="GrdViewSerVisit_RowCommand" OnRowDataBound="GrdViewSerVisit_RowDataBound"
                                OnSelectedIndexChanged="GrdViewSerVisit_SelectedIndexChanged" OnRowDeleting="GrdViewSerVisit_RowDeleting"
                                OnRowDeleted="GrdViewSerVisit_RowDeleted">
                                <EmptyDataRowStyle CssClass="GrdContent" />
                                <Columns>
                                    <asp:BoundField DataField="ServiceID" HeaderText="Service ID" HeaderStyle-Wrap="false"  HeaderStyle-BorderColor="Gray"/>
                                    <asp:BoundField DataField="RefNumber" HeaderText="Ref. No." HeaderStyle-Wrap="false"  HeaderStyle-BorderColor="Gray"/>
                                    <asp:BoundField DataField="LedgerName" HeaderText="Customer / Dealer" HeaderStyle-Wrap="false"  HeaderStyle-BorderColor="Gray"/>
                                    <asp:BoundField DataField="DueDate" HeaderText="Due Date" DataFormatString="{0:dd/MM/yyyy}"  HeaderStyle-BorderColor="Gray"/>
                                    <asp:BoundField DataField="VisitDate" HeaderText="Visit Date" DataFormatString="{0:dd/MM/yyyy}"  HeaderStyle-BorderColor="Gray"/>
                                    <asp:BoundField DataField="Visited" HeaderText="Visited"  HeaderStyle-BorderColor="Gray"/>
                                    <asp:BoundField DataField="Amount" HeaderText="Amount"  HeaderStyle-BorderColor="Gray"/>
                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="50px" HeaderText="Edit" HeaderStyle-BorderColor="Gray">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" SkinID="edit"
                                                CommandName="Select" />
                                            <asp:ImageButton ID="btnEditDisabled" Enabled="false" SkinID="editDisable" runat="Server"></asp:ImageButton>
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
                <tr style="width:100%">
                    <td style="width: 918px" align="left">
                        <asp:ObjectDataSource ID="srcDebitors" runat="server" SelectMethod="ListSundryDebtors"
                            TypeName="BusinessLogic">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="hdDataSource" Name="connection" Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="GridSource" runat="server" SelectMethod="ListServiceVisits"
                            TypeName="BusinessLogic" DeleteMethod="DeleteServiceEntry" OnDeleting="GridSource_Deleting">
                            <DeleteParameters>
                                <asp:Parameter Name="VisitID" Type="Int32" />
                                <asp:CookieParameter CookieName="Company" Type="String" Name="connection" />
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
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
