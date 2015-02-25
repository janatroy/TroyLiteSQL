<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="DamageCheque.aspx.cs" Inherits="DamageCheque" Title="Accounts > Damaged Cheque Leaf" %>

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
                            <%--<table class="mainConHd" style="width: 994px;">
                                <tr valign="middle">
                                    <td style="font-size: 20px;">
                                        Damaged Cheque Leaf
                                    </td>
                                </tr>
                            </table>--%>
                            <div class="mainConBody">
                                <table style="width: 100%; margin: -1px 0px 0px 1px;" cellpadding="3" cellspacing="2" class="searchbg">
                                    <tr style="height: 25px; vertical-align: middle">
                                        <td style="width: 1%;"></td>
                                        <td style="width: 25%; font-size: 22px; color: White;" >
                                            Damaged Cheque Leaf
                                        </td>
                                        <td style="width: 16%">
                                           
                                        </td>
                                        <td style="width: 13%; color: White;" align="right">
                                            Search
                                        </td>
                                        <td style="width: 19%" class="NewBox">
                                            <asp:TextBox ID="txtSearch" runat="server" SkinID="skinTxtBoxSearch"></asp:TextBox>
                                        </td>
                                        <td style="width: 19%" class="NewBox">
                                            <div style="width: 160px; font-family: 'Trebuchet MS';">
                                                <asp:DropDownList ID="ddCriteria" runat="server" Width="155px" BackColor="White" Height="23px" style="text-align:center;border:1px solid White ">
                                                    <asp:ListItem Value="0">All</asp:ListItem>
                                                    <asp:ListItem Value="BankName">Bank Name</asp:ListItem>
                                                    <asp:ListItem Value="ChequeNo">Cheque No</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </td>
                                        <td style="width: 20%" class="tblLeftNoPad">
                                            <asp:Button ID="btnSearch" runat="server" Text="" EnableTheming="false" CssClass="ButtonSearch6" />
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
                                                                    Damaged Cheque Leaf
                                                                </td>
                                                            </tr>
                                                            <tr style="height:5px">
                                                            </tr> 
                                                            <tr>
                                                                <td class="ControlLabel" style="width:40%">
                                                                    Bank Name *
                                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddBankName"
                                                                                                                    Display="Dynamic" ErrorMessage="Bank Name is Mandatory" Operator="GreaterThan"
                                                                                                                    ValueToCompare="0">*</asp:CompareValidator>
                                                                </td>
                                                                <td class="ControlDrpBorder" style="width:30%">
                                                                   <asp:DropDownList ID="ddBankName" DataSourceID="srcGroupInfo" OnDataBound="ddBankName_DataBound" CssClass="drpDownListMedium" BackColor = "#e7e7e7"
                                                                        runat="server" SelectedValue='<%# Bind("BankID") %>' DataTextField="LedgerName"
                                                                        DataValueField="LedgerID" Width="100%" AppendDataBoundItems="True" style="border: 1px solid #e7e7e7" >
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
                                                                    Cheque No
                                                                    <asp:RequiredFieldValidator ID="rvOpenBal" runat="server" ControlToValidate="txtFromNo"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Cheque No is mandatory">*</asp:RequiredFieldValidator>
                                                                    
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
                                                            <tr>
                                                                <table style="width:100%">
                                                                    <tr>
                                                                        <td style="width:25%">
                                
                                                                        </td>
                                                                        <td  style="width:25%">
                                                                            <asp:Button ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                CssClass="cancelbutton6" EnableTheming="false" SkinID="skinBtnCancel" OnClick="UpdateCancelButton_Click">
                                                                            </asp:Button>
                                                                        </td>
                                                                        <td  style="width:25%">
                                                                            <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update"
                                                                                CssClass="Updatebutton1231" EnableTheming="false" SkinID="skinBtnSave"
                                                                                OnClick="UpdateButton_Click"></asp:Button>
                                                                    
                                                                        </td>
                                                                
                                                                        <td style="width:25%">
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                
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
                                                                    Damaged Cheque Leaf
                                                                </td>
                                                            </tr>
                                                            <tr style="height:5px"> 
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width:40%">
                                                                    Bank Name *
                                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddBankNameAdd"
                                                                                                                    Display="Dynamic" ErrorMessage="Bank Name is Mandatory" Operator="GreaterThan"
                                                                                                                    ValueToCompare="0">*</asp:CompareValidator>
                                                                </td>
                                                                <td class="ControlDrpBorder" style="width:30%">
                                                                    <asp:DropDownList ID="ddBankNameAdd" DataSourceID="srcGroupInfoAdd" CssClass="drpDownListMedium" BackColor = "#e7e7e7"
                                                                        runat="server" SelectedValue='<%# Bind("BankID") %>' DataTextField="LedgerName"  style="border: 1px solid #e7e7e7" height="26px" 
                                                                        DataValueField="LedgerID" Width="98%" AppendDataBoundItems="True" TabIndex="1">
                                                                        <asp:ListItem Selected="True" style="background-color: #e7e7e7" Value="0">Select Bank</asp:ListItem>
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
                                                                    Cheque No
                                                                    <asp:RequiredFieldValidator ID="rvOpenBalAdd" runat="server" ControlToValidate="txtFromNoAdd"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Cheque No is mandatory">*</asp:RequiredFieldValidator>
                                                                    
                                                                </td>
                                                                <td class="ControlTextBox3" style="width:30%">
                                                                    <asp:TextBox ID="txtFromNoAdd" runat="server" Text='<%# Bind("ChequeNo") %>'
                                                                          style="border: 1px solid #e7e7e7" SkinID="skinTxtBoxGrid"  TabIndex="3"  BackColor = "#e7e7e7"></asp:TextBox>
                                                                </td>
                                                                <td style="width:30%">

                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr style="height:3px">
                                                                                    </tr>
                                                            
                                                            <tr>
                                                                <table style="width:100%">
                                                                    <tr>
                                                                        <td style="width:25%">
                                
                                                                        </td>
                                                                        <td  style="width:25%">
                                                                            <asp:Button ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                CssClass="cancelbutton6" EnableTheming="false" SkinID="skinBtnCancel" OnClick="InsertCancelButton_Click">
                                                                            </asp:Button>
                                                                        </td>
                                                                        <td  style="width:25%">
                                                                            <asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                                                                CssClass="savebutton1231" EnableTheming="false" SkinID="skinBtnSave"
                                                                                OnClick="InsertButton_Click"></asp:Button>
                                                                        
                                                                        </td>
                                                                
                                                                        <td style="width:25%">
                                                                        </td>
                                                                    </tr>
                                                                </table>
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
                                AllowPaging="True" DataKeyNames="ChequeID" EmptyDataText="No Cheque Data Found."
                                OnRowCommand="GrdViewLedger_RowCommand" OnRowDataBound="GrdViewLedger_RowDataBound" OnRowDeleting="GrdViewLedger_RowDeleting"
                                OnRowDeleted="GrdViewLedger_RowDeleted">
                                <Columns>
                                    <asp:BoundField DataField="ChequeNo" HeaderText="Cheque No"  HeaderStyle-BorderColor="Gray"/>
                                    <asp:BoundField DataField="ledgername" HeaderText="Bank Name" HeaderStyle-BorderColor="Gray" />
                                    <%--<asp:TemplateField ItemStyle-CssClass="command" HeaderText="Edit" ItemStyle-Width="50px" HeaderStyle-BorderColor="Blue"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Select" />
                                            <asp:ImageButton ID="btnEditDisabled" Enabled="false" SkinID="editDisable" runat="Server">
                                            </asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderText="Delete" HeaderStyle-Width="50px" HeaderStyle-BorderColor="Gray"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to Delete this Damaged Cheque Leaf Details?"
                                                runat="server">
                                            </cc1:ConfirmButtonExtender>
                                            <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" CommandName="Delete"></asp:ImageButton>
                                            <asp:ImageButton ID="lnkBDisabled" Enabled="false" SkinID="deleteDisable" runat="Server">
                                            </asp:ImageButton>
                                            <asp:HiddenField ID="ldgID" runat="server" Value='<%# Bind("ChequeID") %>' />
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
                        <asp:ObjectDataSource ID="GridSource" runat="server" SelectMethod="ListDamageChequeInfo"
                            TypeName="BusinessLogic" DeleteMethod="DeleteDamageCheque" OnDeleting="GridSource_Deleting">
                            <DeleteParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                <asp:Parameter Name="ChequeId" Type="Int32" />
                                <asp:Parameter Name="Username" Type="String" />
                            </DeleteParameters>
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="frmSource" runat="server" SelectMethod="GetDamageChequeId"
                            TypeName="BusinessLogic" OnInserting="frmSource_Inserting"
                            InsertMethod="InsertDamageCheque">
                            <%--<UpdateParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                <asp:Parameter Name="ChequeID" Type="Int32" />
                                <asp:Parameter Name="BankName" Type="String" />
                                <asp:Parameter Name="BankID" Type="Int32" />
                                <asp:Parameter Name="ChequeNo" Type="String" />
                                <asp:Parameter Name="Username" Type="String" />
                            </UpdateParameters>--%>
                            <SelectParameters>
                                <asp:ControlParameter ControlID="GrdViewLedger" Name="ChequeId" PropertyName="SelectedValue"
                                    Type="String" />
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                            </SelectParameters>
                            <InsertParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                <asp:Parameter Name="BankName" Type="String" />
                                <asp:Parameter Name="BankID" Type="Int32" />
                                <asp:Parameter Name="ChequeNo" Type="String" />
                                <asp:Parameter Name="Username" Type="String" />
                            </InsertParameters>
                        </asp:ObjectDataSource>
                        <asp:HiddenField ID="hdSMSRequired" runat="server" Value="NO" />
                        <asp:HiddenField ID="hdEmailRequired" runat="server" Value="NO" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <table style="width:100%">
                            <tr>
                                 <td style="width:20%">
                                
                                </td>
                                <td style="width:15%" align="left">
                            
                                                    <asp:Button ID="lnkBtnAdd" runat="server" OnClick="lnkBtnAdd_Click" CssClass="ButtonAdd66"
                                                        EnableTheming="false" Width="80px" Text=""></asp:Button>
                                              
                                           
                                </td>
                                <td style="width:15%">
                                    <asp:Button ID="btnpur" runat="server"
                                    CssClass="exportexl6" EnableTheming="false" CausesValidation="false"
                                     OnClientClick="window.open('ReportExcelDamagedLeaf.aspx','CSTSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=250,width=500,left=425,top=220, scrollbars=yes');"></asp:Button>
                               </td>
                                <td style="width:15%">
                                    <asp:Button ID="CancelButton" CssClass="GoBack" EnableTheming="false"
                                            runat="server" CausesValidation="False" CommandName="" SkinID="skinBtnCancel"
                                            OnClick="CancelButton_Click"></asp:Button>
                                </td>
                                <td style="width:35%">
                                
                                </td>
                            </tr>
                        </table>
                    </td>
                    
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
