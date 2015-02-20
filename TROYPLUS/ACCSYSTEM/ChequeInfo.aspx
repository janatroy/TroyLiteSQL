<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="ChequeInfo.aspx.cs" Inherits="ChequeInfo" Title="Accounts > Cheque Information" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <script language="javascript" type="text/javascript">

//        function Mobile_Validator() {
//            var ctrMobile = document.getElementById('ctl00_cplhControlPanel_frmViewAdd_txtMobile');

//            if (ctrMobile == null)
//                ctrMobile = document.getElementById('ctl00_cplhControlPanel_frmViewAdd_txtMobileAdd');

//            var txtMobile = ctrMobile.value;

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

    </script>
    <asp:UpdatePanel ID="UpdatePanel16" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <table style="width: 100%">
                <tr style="width: 100%">
                    <td style="width: 100%">
                        
                            <%--<div class="mainConHd">
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tr valign="middle">
                                        <td>
                                            <span>Cheque Information</span>
                                        </td>
                                    </tr>
                                </table>
                            </div>--%>
                            <table class="mainConHd" style="width: 994px;">
                                <tr valign="middle">
                                    <td style="font-size: 20px;">
                                        Cheque Information
                                    </td>
                                </tr>
                            </table>
                            <div class="mainConBody">
                                <table style="width: 100%;" cellpadding="3" cellspacing="2" class="searchbg">
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
                                        <td style="width: 10%" align="center">
                                            Search
                                        </td>
                                        <td style="width: 22%" class="cssTextBoxbg">
                                            <asp:TextBox ID="txtSearch" runat="server" SkinID="skinTxtBoxSearch"></asp:TextBox>
                                        </td>
                                        <td style="width: 22%" class="Box">
                                            <div style="width: 150px; font-family: 'Trebuchet MS';">
                                                <asp:DropDownList ID="ddCriteria" runat="server" Width="155px" Height="23px" style="text-align:center;border:1px solid White ">
                                                    <asp:ListItem Value="0" style="background-color: White">All</asp:ListItem>
                                                    <asp:ListItem Value="BankName">Bank Name</asp:ListItem>
                                                    <%--<asp:ListItem Value="AccountNo">Account No</asp:ListItem>--%>
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
                        
                              <asp:Panel runat="server" ID="popUp" Style="width: 40%">
                                 <div id="contentPopUp">
                                    <table style="width: 100%;" align="center">
                                        <tr style="width: 100%">
                                            <td style="width: 100%">
                                            <asp:FormView ID="frmViewAdd" runat="server" Width="100%" DataSourceID="frmSource"
                                                DataKeyNames="ChequeBookID" OnItemCommand="frmViewAdd_ItemCommand" DefaultMode="Edit"
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
                                                                <td colspan="4" class="headerPopUp">
                                                                    Cheque Information
                                                                </td>
                                                            </tr>
                                                            <tr style="height:5px">
                                                            </tr> 
                                                            <tr>
                                                                <td class="ControlLabel" style="width:40%">
                                                                    Bank Name *
                                                                    <asp:RequiredFieldValidator ID="rvLdgrName" runat="server" ControlToValidate="ddBankName"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Bank Name is mandatory">*</asp:RequiredFieldValidator>
                                                                </td>
                                                                <td class="ControlDrpBorder" style="width:30%">
                                                                   <asp:DropDownList ID="ddBankName" DataSourceID="srcGroupInfo" OnDataBound="ddBankName_DataBound" CssClass="drpDownListMedium" BackColor = "#90c9fc"
                                                                        runat="server" SelectedValue='<%# Bind("BankID") %>' DataTextField="LedgerName"
                                                                        DataValueField="LedgerID" Width="100%" AppendDataBoundItems="True" style="border: 1px solid #90c9fc" >
                                                                     </asp:DropDownList>
                                                                </td>
                                                                
                                                                <td style="width:30%">
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr style="height:3px">
                                                                                    </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width:40%">
                                                                    Account No *
                                                                    <asp:RequiredFieldValidator ID="rvAliasName" runat="server" ControlToValidate="txtAccNo"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Account No is mandatory">*</asp:RequiredFieldValidator>
                                                                </td>
                                                                <td class="ControlTextBox3" style="width:30%">
                                                                    <asp:TextBox ID="txtAccNo" runat="server" Text='<%# Bind("AccountNo") %>' SkinID="skinTxtBoxGrid"
                                                                        TabIndex="4"></asp:TextBox>
                                                                </td>
                                                                <td style="width:30%">
                                                                </td>
                                                                <td>
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr style="height:3px">
                                                                                    </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width:40%">
                                                                    Cheque No From *
                                                                    <asp:RequiredFieldValidator ID="rvOpenBal" runat="server" ControlToValidate="txtFromNo"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="From Cheque No is mandatory">*</asp:RequiredFieldValidator>
                                                                    
                                                                </td>
                                                                <td class="ControlTextBox3" style="width:30%">
                                                                    <asp:TextBox ID="txtFromNo" runat="server" SkinID="skinTxtBoxGrid"  Text='<%# Bind("FromChequeNo") %>' style="border: 1px solid #90c9fc" BackColor = "#90c9fc"
                                                                        TabIndex="2"></asp:TextBox>
                                                                </td>
                                                                <td style="width:30%">
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            
                                                            <tr style="height:3px">
                                                                                    </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width:40%">
                                                                    Cheque No To *
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtToNo"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="To Cheque No is mandatory">*</asp:RequiredFieldValidator>
                                                                    
                                                                </td>
                                                                <td class="ControlTextBox3" style="width:30%">
                                                                    <asp:TextBox ID="txtToNo" runat="server" SkinID="skinTxtBoxGrid"  Text='<%# Bind("ToChequeNo") %>' style="border: 1px solid #90c9fc" BackColor = "#90c9fc"
                                                                        TabIndex="2"></asp:TextBox>
                                                                </td>
                                                                <td style="width:30%">
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td align="right">
                                                                    <asp:Button ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                        CssClass="cancelbutton" EnableTheming="false" SkinID="skinBtnCancel" OnClick="UpdateCancelButton_Click">
                                                                    </asp:Button>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update"
                                                                        CssClass="savebutton" EnableTheming="false" SkinID="skinBtnSave"
                                                                        OnClick="UpdateButton_Click"></asp:Button>
                                                                    
                                                                </td>
                                                                
                                                                <td>
                                                                </td>
                                                                
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <table cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <asp:ObjectDataSource ID="srcGroupInfo" runat="server" SelectMethod="ListBanks"
                                                                    TypeName="BusinessLogic" OldValuesParameterFormatString="original_{0}">
                                                                    <SelectParameters>
                                                                        <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                    </SelectParameters>
                                                                </asp:ObjectDataSource>
                                                                <td>
                                                                    <asp:ValidationSummary ID="valSum" DisplayMode="BulletList" ShowMessageBox="true"
                                                                        ShowSummary="false" HeaderText="Validation Messages" Font-Names="'Trebuchet MS'"
                                                                        Font-Size="12" runat="server" />
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
                                                                <td colspan="4" class="headerPopUp">
                                                                    Cheque Book Information
                                                                </td>
                                                            </tr>
                                                            <tr style="height:5px"> 
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width:40%">
                                                                    Bank Name *
                                                                    <asp:RequiredFieldValidator ID="rvLdgrNameAdd" runat="server" ControlToValidate="ddBankNameAdd"
                                                                        Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="Bank Name is mandatory"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td class="ControlDrpBorder" style="width:30%">
                                                                    <asp:DropDownList ID="ddBankNameAdd" DataSourceID="srcGroupInfoAdd" CssClass="drpDownListMedium" BackColor = "#90c9fc"
                                                                        runat="server" SelectedValue='<%# Bind("BankID") %>' DataTextField="LedgerName"  style="border: 1px solid #90c9fc" height="26px" 
                                                                        DataValueField="LedgerID" Width="98%" AppendDataBoundItems="True" TabIndex="1">
                                                                        <asp:ListItem Selected="True" style="background-color: #90c9fc" Value="0">Select Bank</asp:ListItem>
                                                                     </asp:DropDownList>
                                                                </td>
                                                                
                                                                <td style="width:30%">
                                                                    
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr style="height:3px">
                                                                                    </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width:40%">
                                                                    Account No *
                                                                    <asp:RequiredFieldValidator ID="rvAliasNameAdd" runat="server" ControlToValidate="txtAccNoAdd"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Account No is mandatory">*</asp:RequiredFieldValidator>
                                                                </td>
                                                                <td class="ControlTextBox3" style="width:30%">
                                                                    <asp:TextBox ID="txtAccNoAdd" runat="server" Text='<%# Bind("AccountNo") %>'
                                                                        SkinID="skinTxtBoxGrid" TabIndex="2"></asp:TextBox>
                                                                </td>
                                                                
                                                                <td style="width:30%">

                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr style="height:3px">
                                                                                    </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width:40%">
                                                                    Cheque No From *
                                                                    <asp:RequiredFieldValidator ID="rvOpenBalAdd" runat="server" ControlToValidate="txtFromNoAdd"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Cheque From No is mandatory">*</asp:RequiredFieldValidator>
                                                                    
                                                                </td>
                                                                <td class="ControlTextBox3" style="width:30%">
                                                                    <asp:TextBox ID="txtFromNoAdd" runat="server" Text='<%# Bind("FromChequeNo") %>'
                                                                          style="border: 1px solid #90c9fc" SkinID="skinTxtBoxGrid"  TabIndex="3"  BackColor = "#90c9fc"></asp:TextBox>
                                                                </td>
                                                                <td style="width:30%">

                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr style="height:3px">
                                                                                    </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width:40%">
                                                                    Cheque No To *
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtToNoAdd"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Cheque To No is mandatory">*</asp:RequiredFieldValidator>
                                                                </td>
                                                                <td class="ControlTextBox3" style="width:30%">
                                                                    <asp:TextBox ID="txtToNoAdd" runat="server" Text='<%# Bind("ToChequeNo") %>'
                                                                          style="border: 1px solid #90c9fc" SkinID="skinTxtBoxGrid"  TabIndex="4"  BackColor = "#90c9fc"></asp:TextBox>
                                                                </td>
                                                                <td style="width:30%">

                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr style="height:3px">
                                                                                    </tr>
                                                            
                                                            <tr>
                                                                <td>
                                                                    
                                                                </td>
                                                                <td align="right">
                                                                    <asp:Button ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                        CssClass="cancelbutton" EnableTheming="false" SkinID="skinBtnCancel" OnClick="InsertCancelButton_Click">
                                                                    </asp:Button>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                                                        CssClass="savebutton" EnableTheming="false" SkinID="skinBtnSave"
                                                                        OnClick="InsertButton_Click"></asp:Button>
                                                                        
                                                                </td>
                                                                
                                                                <td>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <table cellspacing="0">
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:ValidationSummary ID="valSumAdd" DisplayMode="BulletList" ShowMessageBox="true"
                                                                    ShowSummary="false" HeaderText="Validation Messages" Font-Names="'Trebuchet MS'"
                                                                    Font-Size="12" runat="server" />
                                                            </td>
                                                            <td colspan="2">
                                                                <asp:ObjectDataSource ID="srcGroupInfoAdd" runat="server" SelectMethod="ListBanks"
                                                                    TypeName="BusinessLogic" OldValuesParameterFormatString="original_{0}">
                                                                    <SelectParameters>
                                                                        <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                    </SelectParameters>
                                                                </asp:ObjectDataSource>
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
                <tr style="width: 100%;">
                    <td style="width: 100%;">
                        <table width="100%" style="margin: -3px 0px 0px 0px;">
                                    <tr style="width: 100%">
                                        <td>
                        <div class="mainGridHold" id="searchGrid">
                            <asp:GridView ID="GrdViewLedger" CssClass="someClass" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                OnRowCreated="GrdViewLedger_RowCreated" Width="99.9%" DataSourceID="GridSource"
                                AllowPaging="True" DataKeyNames="ChequeBookID" EmptyDataText="No Cheque Data Found."
                                OnRowCommand="GrdViewLedger_RowCommand" OnRowDataBound="GrdViewLedger_RowDataBound" OnRowDeleting="GrdViewLedger_RowDeleting"
                                OnRowDeleted="GrdViewLedger_RowDeleted">
                                <Columns>
                                    <asp:BoundField DataField="FromChequeNo" HeaderText="From Cheque No"  HeaderStyle-BorderColor="Blue"/>
                                    <asp:BoundField DataField="ToChequeNo" HeaderText="To Cheque No"  HeaderStyle-BorderColor="Blue"/>
                                    <asp:BoundField DataField="Ledgername" HeaderText="Bank Name" HeaderStyle-BorderColor="Blue" />
                                    <asp:BoundField DataField="AccountNo" HeaderText="Account No" HeaderStyle-BorderColor="Blue"/>
                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderText="Edit" ItemStyle-Width="50px" HeaderStyle-BorderColor="Blue"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Select" />
                                            <asp:ImageButton ID="btnEditDisabled" Enabled="false" SkinID="editDisable" runat="Server">
                                            </asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderText="Delete" HeaderStyle-Width="50px" HeaderStyle-BorderColor="Blue"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to Delete this Cheque Book Details?"
                                                runat="server">
                                            </cc1:ConfirmButtonExtender>
                                            <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" CommandName="Delete"></asp:ImageButton>
                                            <asp:ImageButton ID="lnkBDisabled" Enabled="false" SkinID="deleteDisable" runat="Server">
                                            </asp:ImageButton>
                                            <asp:HiddenField ID="ldgID" runat="server" Value='<%# Bind("ChequeBookID") %>' />
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
                                                <asp:DropDownList ID="ddlPageSelector" runat="server" AutoPostBack="true" width="65px" style="border:1px solid blue">
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
                <tr style="width:100%;">
                    <td style="width: 918px" align="left">
                        <asp:ObjectDataSource ID="GridSource" runat="server" SelectMethod="ListChequeInfo"
                            TypeName="BusinessLogic" DeleteMethod="DeleteCheque" OnDeleting="GridSource_Deleting">
                            <DeleteParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                <asp:Parameter Name="ChequeBookId" Type="Int32" />
                                <asp:Parameter Name="Username" Type="String" />
                                <asp:Parameter Name="Types" Type="String" />
                            </DeleteParameters>
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="frmSource" runat="server" SelectMethod="GetChequeInfoForId"
                            TypeName="BusinessLogic" OnUpdating="frmSource_Updating" OnInserting="frmSource_Inserting"
                            InsertMethod="InsertCheque" UpdateMethod="UpdateCheque">
                            <UpdateParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                <asp:Parameter Name="ChequeBookID" Type="Int32" />
                                <asp:Parameter Name="BankName" Type="String" />
                                <asp:Parameter Name="AccountNo" Type="String" />
                                <asp:Parameter Name="BankID" Type="Int32" />
                                <asp:Parameter Name="FromChequeNo" Type="String" />
                                <asp:Parameter Name="ToChequeNo" Type="String" />
                                <asp:Parameter Name="Username" Type="String" />
                                <asp:Parameter Name="Types" Type="String" />
                            </UpdateParameters>
                            <SelectParameters>
                                <asp:ControlParameter ControlID="GrdViewLedger" Name="ChequeBookId" PropertyName="SelectedValue"
                                    Type="String" />
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                            </SelectParameters>
                            <InsertParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                <asp:Parameter Name="BankName" Type="String" />
                                <asp:Parameter Name="AccountNo" Type="String" />
                                <asp:Parameter Name="BankID" Type="Int32" />
                                <asp:Parameter Name="FromChequeNo" Type="String" />
                                <asp:Parameter Name="ToChequeNo" Type="String" />
                                <asp:Parameter Name="Username" Type="String" />
                                <asp:Parameter Name="Types" Type="String" />
                            </InsertParameters>
                        </asp:ObjectDataSource>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%" align="right">
                        
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div align="center">
    <table>
    <tr>
        <td >
                        <asp:Button ID="UnusedLeaf" runat="server"
            CssClass="UnusedLeaf" EnableTheming="false" CausesValidation="false"
            OnClick="UnusedLeaf_Click"></asp:Button>
       </td>
        <td >
            <asp:Button ID="DamageLeaf" runat="server"
            CssClass="DamageLeaf" EnableTheming="false"
            OnClick="DamageLeaf_Click"></asp:Button>
        </td>
        </tr>
            </table>
    </div>
</asp:Content>
