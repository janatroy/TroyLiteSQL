<%@ Page Title="Banking > Cheque book" Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="ChequeBook.aspx.cs" Inherits="ChequeBook" %>

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
        window.onload = function Showalert() {

            var txt = document.getElementById("<%= txtSearch.ClientID %>");
            var btn = document.getElementById("<%= BtnClearFilter.ClientID %>");
            if (txt.value == "") {
                // alert(txt.value);
                btn.style.visibility = "hidden";
                // when the window is loaded, hide the button if the textbox is empty
            }

        }

        function EnableDisableButton(sender, target) {
            var first = document.getElementById('<%=txtSearch.ClientID %>');

            if (sender.value.length >= 1 && first.value.length >= 1) {
                document.getElementById('<%=BtnClearFilter.ClientID %>').style.visibility = "visible";

            }

            if (sender.value.length < 1 && first.value.length < 1) {

                document.getElementById('<%=BtnClearFilter.ClientID %>').style.visibility = "Hidden";
            }
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
                                        Cheque Book Information
                                    </td>
                                </tr>
                            </table>--%>
                        <div class="mainConBody">
                            <table style="width: 99.8%; margin: -2px 0px 0px 1px;" cellpadding="3" cellspacing="2" class="searchbg">
                                <tr style="height: 25px; vertical-align: middle">
                                    <td style="width: 2%;"></td>
                                    <td style="width: 18%; font-size: 22px; color: White;">Cheque Book
                                    </td>
                                    <td style="width: 16%">
                                        <div style="text-align: right;">
                                        
                                        </div>
                                    </td>
                                    <td style="width: 13%; color: White;" align="right">Search
                                    </td>
                                    <td style="width: 20%" class="NewBox">
                                        <asp:TextBox ID="txtSearch" runat="server" SkinID="skinTxtBoxSearch"></asp:TextBox>
                                    </td>
                                    <td style="width: 20%" class="NewBox">
                                        <div style="width: 160px; font-family: 'Trebuchet MS';">
                                            <asp:DropDownList ID="ddCriteria" runat="server" Width="154px" BackColor="White" Height="23px" Style="text-align: center; border: 1px solid White">
                                                <asp:ListItem Value="BankName">Bank Name</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                    <td style="width: 22%" class="tblLeftNoPad">
                                        <asp:Button ID="btnSearch" onkeyup="EnableDisableButton(this,'BtnClearFilter')" runat="server" Text="" EnableTheming="false" CssClass="ButtonSearch6" OnClick="btnSearch_Click" />
                                    </td>
                                    <td style="width: 20%" class="tblLeftNoPad">
                                        <asp:Button ID="BtnClearFilter" runat="server" OnClick="BtnClearFilter_Click" EnableTheming="false" Text="" CssClass="ClearFilter6" />
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
                        <asp:Panel runat="server" ID="popUp" Style="width: 50%">
                            <div id="contentPopUp">
                                <table cellpadding="2" cellspacing="2" style="border: 1px solid blue; background-color: #fff; color: #000;"
                                    width="100%">
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlVisitDetails" runat="server" Visible="false">
                                                <div>
                                                    <table cellpadding="2" cellspacing="1" style="border: 0px solid blue;"
                                                        width="100%">
                                                        <tr>
                                                            <tr>
                                                                <td colspan="4">
                                                                    <table class="headerPopUp" width="100%">
                                                                        <tr>
                                                                            <td>Cheque Book Information
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 5px">
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 15%"></td>
                                                                <td style="width: 25%;" class="ControlLabel">Bank Name *
                                                                    <%--<asp:RequiredFieldValidator ID="rvLdgrName" runat="server" ControlToValidate="ddBankName"
                                                                        Display="Dynamic" ErrorMessage="Bank Name is mandatory">*</asp:RequiredFieldValidator>--%>
                                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddBankName"
                                                                        Display="Dynamic" ErrorMessage="Bank Name is Mandatory" Operator="GreaterThan"
                                                                        ValueToCompare="0">*</asp:CompareValidator>
                                                                </td>
                                                                <td style="width: 25%" class="ControlDrpBorder">
                                                                    <asp:DropDownList ID="ddBankName" CssClass="drpDownListMedium" BackColor="#e7e7e7"
                                                                        runat="server" DataTextField="LedgerName"
                                                                        DataValueField="LedgerID" Width="100%" AppendDataBoundItems="True" Style="border: 1px solid #e7e7e7">
                                                                        <asp:ListItem Text="Select Bank" style="background-color: #e7e7e7" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>

                                                                <td style="width: 35%"></td>
                                                            </tr>
                                                            <tr style="height: 2px">
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 10%"></td>
                                                                <td style="width: 25%" class="ControlLabel">Account No *
                                                                    <asp:RequiredFieldValidator ID="rvAliasNameAdd" runat="server" ControlToValidate="txtAccNoAdd"
                                                                        Display="Dynamic" ErrorMessage="Account No is mandatory">*</asp:RequiredFieldValidator>
                                                                </td>
                                                                <td style="width: 25%;" class="ControlDrpBorder">
                                                                    <asp:TextBox ID="txtAccNoAdd" runat="server" Text='<%# Bind("AccountNo") %>'
                                                                        SkinID="skinTxtBoxGrid" TabIndex="2"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 33%"></td>
                                                            </tr>
                                                            <tr style="height: 2px">
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 10%"></td>
                                                                <td style="width: 25%" class="ControlLabel">Cheque No From *
                                                                    <asp:RequiredFieldValidator ID="rvOpenBalAdd" runat="server" ControlToValidate="txtFromNoAdd"
                                                                        Display="Dynamic" ErrorMessage="Cheque From No is mandatory">*</asp:RequiredFieldValidator>
                                                                </td>
                                                                <td style="width: 25%" class="ControlTextBox3">
                                                                    <asp:TextBox ID="txtFromNoAdd" runat="server" Text='<%# Bind("FromChequeNo") %>'
                                                                        Style="border: 1px solid #e7e7e7" SkinID="skinTxtBoxGrid" TabIndex="3" BackColor="#e7e7e7"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 33%"></td>
                                                            </tr>
                                                            <tr style="height: 2px">
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 10%"></td>
                                                                <td style="width: 25%;" class="ControlLabel">Cheque No To *
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtToNoAdd"
                                                                        Display="Dynamic" ErrorMessage="Cheque To No is mandatory">*</asp:RequiredFieldValidator>
                                                                </td>
                                                                <td style="width: 25%" class="ControlTextBox3">
                                                                    <asp:TextBox ID="txtToNoAdd" runat="server" Text='<%# Bind("ToChequeNo") %>'
                                                                        Style="border: 1px solid #e7e7e7" SkinID="skinTxtBoxGrid" TabIndex="4" BackColor="#e7e7e7"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 33%"></td>
                                                            </tr>
                                                            <tr style="height: 5px">
                                                            </tr>
                                                            <tr>
                                                                <td align="center" style="width: 100%" colspan="4">
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td style="width: 30%"></td>
                                                                            <td align="center" style="width: 18%">
                                                                                <asp:Button ID="SaveButton" runat="server" CommandName="Update" SkinID="skinBtnSave"
                                                                                    CssClass="savebutton1231" EnableTheming="false" OnClick="SaveButton_Click"></asp:Button>
                                                                                <asp:Button ID="UpdateButton" runat="server" CommandName="Update" SkinID="skinBtnSave"
                                                                                    CssClass="Updatebutton1231" EnableTheming="false" OnClick="UpdateButton_Click"></asp:Button>
                                                                            </td>
                                                                            <td align="center" style="width: 18%">
                                                                                <asp:Button ID="CancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                    CssClass="cancelbutton6" EnableTheming="false" SkinID="skinBtnCancel" OnClick="UpdateCancelButton_Click"></asp:Button>
                                                                            </td>
                                                                            <td style="width: 30%"></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 10%">
                                                                    <asp:ObjectDataSource ID="srcCreditorDebitor" runat="server" SelectMethod="ListCreditorDebitorSuppliers"
                                                                        TypeName="BusinessLogic">
                                                                        <SelectParameters>
                                                                            <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                        </SelectParameters>
                                                                    </asp:ObjectDataSource>
                                                                </td>
                                                                <td style="width: 25%"></td>
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
                        <table width="100%" style="margin: -4px 0px 0px 0px;">
                            <tr style="width: 100%">
                                <td>
                                    <asp:HiddenField ID="hdVisitID" runat="server" Value="0" />
                                    <div class="mainGridHold" id="searchGrid">
                                        <asp:GridView ID="GrdViewSerVisit" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                            OnRowCreated="GrdViewSerVisit_RowCreated" Width="100.4%" DataSourceID="GridSource" CssClass="someClass"
                                            AllowPaging="True" DataKeyNames="ChequeBookID" EmptyDataText="No Cheque Book found!"
                                            OnRowCommand="GrdViewSerVisit_RowCommand" OnRowDataBound="GrdViewSerVisit_RowDataBound"
                                            OnSelectedIndexChanged="GrdViewSerVisit_SelectedIndexChanged" OnRowDeleting="GrdViewSerVisit_RowDeleting"
                                            OnRowDeleted="GrdViewSerVisit_RowDeleted">
                                            <EmptyDataRowStyle CssClass="GrdContent" />
                                            <HeaderStyle Height="30px" HorizontalAlign="Center" Font-Bold="true" BackColor="#cccccc" BorderColor="Gray" Font-Size="Small" />
                                            <RowStyle Font-Bold="true" HorizontalAlign="Center" Height="30px" Font-Size="Small" ForeColor="#0567AE" />
                                            <Columns>
                                                <asp:BoundField DataField="FromChequeNo" HeaderText="From Cheque No" HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                <asp:BoundField DataField="ToChequeNo" HeaderText="To Cheque No" HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                <asp:BoundField DataField="Ledgername" HeaderText="Bank Name" HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                <asp:BoundField DataField="AccountNo" HeaderText="Account No" HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="50px" HeaderText="Edit" HeaderStyle-BorderColor="Gray">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" SkinID="edit"
                                                            CommandName="Select" />
                                                        <asp:ImageButton ID="btnEditDisabled" Enabled="false" SkinID="editDisable" runat="Server"></asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="command" HeaderText="Delete" HeaderStyle-Width="50px" HeaderStyle-BorderColor="Gray"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to Delete this Cheque Book Details?"
                                                            runat="server">
                                                        </cc1:ConfirmButtonExtender>
                                                        <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" CommandName="Delete"></asp:ImageButton>
                                                        <asp:ImageButton ID="lnkBDisabled" Enabled="false" SkinID="deleteDisable" runat="Server"></asp:ImageButton>
                                                        <asp:HiddenField ID="ldgID" runat="server" Value='<%# Bind("ChequeBookID") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataRowStyle CssClass="GrdContent" />
                                            <PagerTemplate>
                                                <table style="border-color: white">
                                                    <tr style="border-color: white">
                                                        <td style="border-color: white">Goto Page
                                                        </td>
                                                        <td style="border-color: white">
                                                            <asp:DropDownList ID="ddlPageSelector" runat="server" Width="65px" Style="border: 1px solid blue" BackColor="#e7e7e7">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="border-color: white; width: 5px"></td>
                                                        <td style="border-color: white">
                                                            <asp:Button Text="" CommandName="Page" CommandArgument="First" runat="server" CssClass="NewFirst" EnableTheming="false" Width="22px" Height="18px"
                                                                ID="btnFirst" />
                                                        </td>
                                                        <td style="border-color: white">
                                                            <asp:Button Text="" CommandName="Page" CommandArgument="Prev" runat="server" CssClass="NewPrev" EnableTheming="false" Width="22px" Height="18px"
                                                                ID="btnPrevious" />
                                                        </td>
                                                        <td style="border-color: white">
                                                            <asp:Button Text="" CommandName="Page" CommandArgument="Next" runat="server" CssClass="NewNext" EnableTheming="false" Width="22px" Height="18px"
                                                                ID="btnNext" />
                                                        </td>
                                                        <td style="border-color: white">
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
                <tr style="width: 100%">
                    <td style="width: 918px" align="left">
                        <asp:ObjectDataSource ID="srcDebitors" runat="server" SelectMethod="ListSundryDebtors"
                            TypeName="BusinessLogic">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="hdDataSource" Name="connection" Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="GridSource" runat="server" SelectMethod="ListChequeInfo"
                            TypeName="BusinessLogic" DeleteMethod="DeleteCheque" OnDeleting="GridSource_Deleting">
                            <DeleteParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                <asp:Parameter Name="ChequeBookId" Type="Int32" />
                                <asp:Parameter Name="Username" Type="String" />
                                <asp:Parameter Name="Types" Type="String" />
                            </DeleteParameters>
                        </asp:ObjectDataSource>
                        <asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="true" ShowSummary="false" />
                        <asp:HiddenField ID="hdDataSource" runat="server" />
                        <asp:HiddenField ID="hdServiceID" runat="server" Value="" />
                        <asp:HiddenField ID="hdCustomerID" runat="server" Value="0" />
                        <asp:HiddenField ID="hdDueDate" runat="server" Value="" />
                        <asp:HiddenField ID="hdRefNumber" runat="server" Value="" />
                        <asp:HiddenField ID="hdSMSRequired" runat="server" Value="NO" />
                        <asp:HiddenField ID="hdEmailRequired" runat="server" Value="NO" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div align="center">
        <table>
            <tr>
                <td style="width:20%">
                    </td>
                <td style="width:20%">
                        <asp:Panel ID="pnlSearch" runat="server" Width="100px">
                                                <asp:Button ID="lnkBtnAdd" runat="server" OnClick="lnkBtnAdd_Click" CssClass="ButtonAdd66"
                                                    EnableTheming="false" Width="80px" Text=""></asp:Button>
                                            </asp:Panel>
                </td>
                <td style="width:20%">
                    <asp:Button ID="UnusedLeaf" runat="server"
                        CssClass="UnusedLeaf6" EnableTheming="false" CausesValidation="false"
                        OnClick="UnusedLeaf_Click"></asp:Button>
                </td>
                <td style="width:20%">
                    <asp:Button ID="DamageLeaf" runat="server"
                        CssClass="DamageLeaf6" EnableTheming="false"
                        OnClick="DamageLeaf_Click"></asp:Button>
                </td>
                <td style="width:20%">
                    </td>
            </tr>
        </table>
    </div>
</asp:Content>
