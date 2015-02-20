<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="QtyReturns.aspx.cs" Inherits="QtyReturns" Title="Others > Qty. Returns" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel16" runat="server" UpdateMode="Always">
        <ContentTemplate>
        
            <table style="width: 100%;" align="center">
                <tr style="width: 100%">
                    <td style="width: 100%">
                            <%--<table class="mainConHd" style="width: 994px;">
                                <tr valign="middle">
                                    <td style="font-size: 20px;">
                                        Item Tracking
                                    </td>
                                </tr>
                            </table>--%>
                            <%--<div class="mainConHd">
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tr valign="middle">
                                        <td>
                                            <span>Item Tracking</span>
                                        </td>
                                    </tr>
                                </table>
                            </div>--%>
                            <div class="mainConBody">
                                <table style="width: 100%; margin: -1px 0px 0px 1px;" cellpadding="3" cellspacing="2" class="searchbg">
                                    <tr style="height: 25px; vertical-align: middle">
                                        <td style="width: 2%;"></td>
                                        <td style="width: 18%; font-size: 22px; color: #000000;" >
                                            Item Tracking
                                        </td>
                                        <td style="width: 15%">
                                            <div style="text-align: right;">
                                                <asp:Panel ID="pnlSearch" runat="server" Width="100px">
                                                    <asp:Button ID="lnkBtnAdd" runat="server" OnClick="lnkBtnAdd_Click" CssClass="ButtonAdd66"
                                                        EnableTheming="false" Width="80px" Text=""></asp:Button>
                                                </asp:Panel>
                                            </div>
                                        </td>
                                        <td style="width: 13%; color: #000000;" align="right">
                                            Search
                                        </td>
                                        <td style="width: 19%" class="Box1">
                                            <div style="width: 160px; font-family: 'Trebuchet MS';">
                                                <asp:DropDownList ID="ddSearcLedger" runat="server" AutoPostBack="False" Width="154px"
                                                    DataSourceID="srcCreditorDebitor" DataValueField="LedgerID" DataTextField="LedgerName" BackColor="#BBCAFB" CssClass="drpDownListMedium" style="border: 1px solid #BBCAFB"
                                                    AppendDataBoundItems="true">
                                                    <asp:ListItem Text="Select Ledger" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </td>
                                        <td style="width: 19%;" class="tblLeftNoPad">
                                            <asp:Button ID="btnSearch" runat="server" Text="" OnClick="btnSearch_Click"
                                                 EnableTheming="false" CssClass="ButtonSearch6"  />
                                        </td>
                                        <td style="width: 28%; text-align: left">
                                            <a onclick="window.open('QtyReturnReport.aspx ','Summary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=700,width=900,left=10,top=10, scrollbars=yes');"
                                                id="lnkye" href="javascript:__doPostBack('ctl00$cplhControlPanel$lnkJM','')">Click
                                                to view Report</a>
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
                        <asp:Panel runat="server" ID="popUp" Style="width: 55%">
                            <div id="contentPopUp">
                                <table class="tblLeft" cellpadding="0" cellspacing="0" style="border: 0px solid #5078B3;
                                    background-color: #fff; color: #000;" width="100%">
                                    <tr>
                                        <td>
                                            <asp:FormView ID="frmViewAdd" runat="server" Width="100%" DataSourceID="frmSource"
                                                DataKeyNames="ReturnID" OnItemCommand="frmViewAdd_ItemCommand" DefaultMode="Edit"
                                                OnItemCreated="frmViewAdd_ItemCreated" Visible="False" OnItemInserting="frmViewAdd_ItemInserting"
                                                OnItemUpdating="frmViewAdd_ItemUpdating" EmptyDataText="No Records" OnItemInserted="frmViewAdd_ItemInserted"
                                                OnItemUpdated="frmViewAdd_ItemUpdated">
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
                                                                    Item Tracking
                                                                </td>
                                                            </tr>
                                                            <tr style="height:5px">
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width:15%">
                                                                    Ledger Name *
                                                                    <asp:CompareValidator ID="cvLedger" runat="server" ControlToValidate="cmbLedger"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Ledger is Mandatory"
                                                                        Operator="GreaterThan" ValueToCompare="0"></asp:CompareValidator>
                                                                </td>
                                                                <td align="left" style="width:25%" class="ControlDrpBorder">
                                                                    <asp:DropDownList ID="cmbLedger" runat="server" AutoPostBack="False"  CssClass="drpDownListMedium" BackColor="#90c9fc" Width="100%"
                                                                        DataSourceID="srcCreditorDebitor" DataValueField="LedgerID" DataTextField="LedgerName" style="border: 1px solid #90c9fc" height="26px"
                                                                        AppendDataBoundItems="true" OnDataBound="cmbLedger_DataBound">
                                                                        <asp:ListItem Text="Select Ledger" style="background-color: #bce1fe" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td class="ControlLabel" style="width:15%">
                                                                    Date Returned *
                                                                    <asp:RequiredFieldValidator ID="rvDateRet" runat="server" ControlToValidate="txtDateEntered"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Date Returned is mandatory"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td class="ControlTextBox3" style="width:15%">
                                                                    <asp:TextBox ID="txtDateEntered" runat="server" Text='<%# Bind("DateEntered","{0:dd/MM/yyyy}") %>'
                                                                        Width="100px" CssClass="cssTextBox"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="calExtd3" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                        PopupButtonID="btnD3" PopupPosition="BottomLeft" TargetControlID="txtDateEntered">
                                                                    </cc1:CalendarExtender>
                                                                </td>
                                                                <td align="left" style="width:10%">
                                                                    <asp:ImageButton ID="btnD3" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                        Width="20px" runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr  style="height:3px">
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width:15%">
                                                                    Qty. *
                                                                    <asp:RequiredFieldValidator ID="rvQty" runat="server" ControlToValidate="txtQty"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Qty. is mandatory"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td class="ControlTextBox3" style="width:25%">
                                                                    <asp:TextBox ID="txtQty" runat="server" Text='<%# Bind("Qty") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                </td>
                                                                <td class="ControlLabel" style="width:15%">
                                                                    Comments *
                                                                    <asp:RequiredFieldValidator ID="rvcmnts" runat="server" ControlToValidate="txtComments"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Comments is mandatory"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td class="ControlTextBox3" style="width:25%">
                                                                    <asp:TextBox ID="txtComments" runat="server" Text='<%# Bind("Comments") %>' SkinID="skinTxtBoxGrid"
                                                                        Height="35px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <td style="width:10%">
                                                            </td>
                                                            <tr style="height:6px">
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4">
                                                                    <table style="width: 100%;">
                                                                        <tr>    
                                                                             <td style="width:30%">
                                                                             </td>
                                                                             <td style="width:20%">
                                                                                <asp:Button ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                    CssClass="cancelbutton6" EnableTheming="false" SkinID="skinBtnCancel" OnClick="UpdateCancelButton_Click">
                                                                                </asp:Button>
                                                                            </td>
                                                                            <td style="width:20%">
                                                                                <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update"
                                                                                    CssClass="Updatebutton1231" EnableTheming="false" SkinID="skinBtnSave" OnClick="UpdateButton_Click">
                                                                                </asp:Button>
                                                                            </td>
                                                                            <td style="width:30%">
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
                                                            </td>
                                                            <td>
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
                                                        <table cellpadding="1" cellspacing="1" style="border: 1px solid #86b2d1; width: 100%;">
                                                            <tr>
                                                                <td colspan="5" class="headerPopUp">
                                                                    Item Tracking
                                                                </td>
                                                            </tr>
                                                            <tr style="height:5px">
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width:15%">
                                                                    Ledger Name *
                                                                    <asp:CompareValidator ID="cvLedgerAdd" runat="server" ControlToValidate="cmbLedgerAdd"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Ledger is Mandatory"
                                                                        Operator="GreaterThan" ValueToCompare="0"></asp:CompareValidator>
                                                                </td>
                                                                <td align="left" class="ControlDrpBorder" style="width:25%">
                                                                    <asp:DropDownList ID="cmbLedgerAdd" runat="server" CssClass="drpDownListMedium" BackColor="#90c9fc" Width="100%" AutoPostBack="False"
                                                                        DataSourceID="srcCreditorDebitor" DataValueField="LedgerID" DataTextField="LedgerName" style="border: 1px solid #90c9fc" height="26px"
                                                                        AppendDataBoundItems="true" OnDataBound="cmbLedger_DataBound">
                                                                        <asp:ListItem Text="Select Ledger" style="background-color: #90c9fc" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td class="ControlLabel" style="width:15%">
                                                                    Date Returned *
                                                                    <asp:RequiredFieldValidator ID="rvDateRetAdd" runat="server" ControlToValidate="txtDateEnteredAdd"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Date Returned is mandatory"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td class="ControlTextBox3" style="width:25%">
                                                                    <asp:TextBox ID="txtDateEnteredAdd" runat="server" Text='<%# Bind("DateEntered","{0:dd/MM/yyyy}") %>'
                                                                        Width="100px" CssClass="cssTextBox"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="calExt3" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                        PopupButtonID="btnDt3" PopupPosition="BottomLeft" TargetControlID="txtDateEnteredAdd">
                                                                    </cc1:CalendarExtender>
                                                                </td>
                                                                <td style="width:10%" align="left"> 
                                                                    <asp:ImageButton ID="btnDt3" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                        Width="20px" runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr  style="height:3px">
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width:15%">
                                                                    Qty. *
                                                                    <asp:RequiredFieldValidator ID="rvQtyAdd" runat="server" ControlToValidate="txtQtyAdd"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Qty. is mandatory"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td class="ControlTextBox3" style="width:25%">
                                                                    <asp:TextBox ID="txtQtyAdd" runat="server" Text='<%# Bind("Qty") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                </td>
                                                                <td class="ControlLabel" style="width:15%">
                                                                    Comments *
                                                                    <asp:RequiredFieldValidator ID="rvcmntsAdd" runat="server" ControlToValidate="txtCommentsAdd"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Comments is mandatory"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td class="ControlTextBox3" style="width:25%">
                                                                    <asp:TextBox ID="txtCommentsAdd" runat="server" Height="35px" Text='<%# Bind("Comments") %>'
                                                                        SkinID="skinTxtBoxGrid" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr style="height:7px">
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4">
                                                                    <table style="width: 100%;">
                                                                        <tr>    
                                                                             <td style="width:30%">
                                                                             </td>
                                                                             <td style="width:20%">
                                                                                <asp:Button ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                    CssClass="cancelbutton6" EnableTheming="false" SkinID="skinBtnCancel" OnClick="InsertCancelButton_Click">
                                                                                </asp:Button>
                                                                            </td>
                                                                            <td style="width:20%">
                                                                                <asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                                                                    CssClass="savebutton1231" EnableTheming="false" SkinID="skinBtnSave" OnClick="InsertButton_Click">
                                                                                </asp:Button>
                                                                            </td>
                                                                            <td  style="width:30%">
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
                                                                <asp:ObjectDataSource ID="srcGroupInfoAdd" runat="server" SelectMethod="ListGroupInfo"
                                                                    TypeName="BusinessLogic" OldValuesParameterFormatString="original_{0}">
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
                                                        </tr>
                                                    </table>
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
                    <td style="width: 100%">
                    <table width="100%" style="margin: -3px 0px 0px 0px;">
                                <tr style="width: 100%">
                                    <td>
                        <asp:GridView ID="GrdViewReturns" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                            OnRowCreated="GrdViewReturns_RowCreated" Width="100.1%" DataSourceID="GridSource"
                            AllowPaging="True" DataKeyNames="ReturnID" EmptyDataText="No Data Found. Please try again."
                            OnRowCommand="GrdViewReturns_RowCommand" OnRowDataBound="GrdViewReturns_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="LedgerName" HeaderText="Ledger Name" />
                                <asp:BoundField DataField="DateEntered" HeaderText="Date Returned" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="Qty" HeaderText="Qty." />
                                <asp:BoundField DataField="Comments" HeaderText="Comments" />
                                <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="50px" HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Select" />
                                        <asp:ImageButton ID="btnEditDisabled" Enabled="false" SkinID="editDisable" runat="Server"></asp:ImageButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="50px" HeaderText="Delete">
                                    <ItemTemplate>
                                        <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to Delete this Item?"
                                            runat="server">
                                        </cc1:ConfirmButtonExtender>
                                        <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" CommandName="Delete"></asp:ImageButton>
                                        <asp:ImageButton ID="lnkBDisabled" Enabled="false" SkinID="deleteDisable" runat="Server"></asp:ImageButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerTemplate>
                                <tble>
                                    <tr>
                                        <td>
                                            Goto Page
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlPageSelector" runat="server" AutoPostBack="true" Width="65px" style="border:1px solid blue">
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
                                </tble>
                            </PagerTemplate>
                        </asp:GridView>
                        </td>
                        </tr>
                        </table>
                    </td>
                </tr>
                <tr style="width:100%">
                    <td style="width: 918px" align="left">
                        <asp:ObjectDataSource ID="GridSource" runat="server" DeleteMethod="DeleteQtyReturns"
                            SelectMethod="ListQtyReturns" TypeName="BusinessLogic">
                            <DeleteParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                <asp:Parameter Name="ReturnID" Type="Int32" />
                            </DeleteParameters>
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="empSrc" runat="server" SelectMethod="ListExecutive" TypeName="BusinessLogic">
                            <SelectParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="frmSource" runat="server" SelectMethod="GetQtyReturnForId"
                            TypeName="BusinessLogic" OnUpdating="frmSource_Updating" OnInserting="frmSource_Inserting"
                            InsertMethod="InsertQtyReturns" UpdateMethod="UpdateQtyReturns">
                            <UpdateParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                <asp:Parameter Name="ReturnID" Type="Int32" />
                                <asp:Parameter Name="LedgerID" Type="Int32" />
                                <asp:Parameter Name="DateEntered" Type="String" />
                                <asp:Parameter Name="Qty" Type="Double" />
                                <asp:Parameter Name="Comments" Type="String" />
                            </UpdateParameters>
                            <SelectParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                <asp:ControlParameter ControlID="GrdViewReturns" Name="retunID" PropertyName="SelectedValue"
                                    Type="Int32" />
                            </SelectParameters>
                            <InsertParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                <asp:Parameter Name="LedgerID" Type="Int32" />
                                <asp:Parameter Name="DateEntered" Type="String" />
                                <asp:Parameter Name="Qty" Type="Int32" />
                                <asp:Parameter Name="Comments" Type="String" />
                            </InsertParameters>
                        </asp:ObjectDataSource>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%">
                        <asp:ObjectDataSource ID="srcCreditorDebitor" runat="server" SelectMethod="ListCreditorDebitor"
                            TypeName="BusinessLogic">
                            <SelectParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
