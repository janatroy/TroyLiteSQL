<%@ Page Title="" Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="ManualSalesBook.aspx.cs" Inherits="ManualSalesBook" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <script language="javascript" type="text/javascript">

        function pageLoad() {
            //  get the behavior associated with the tab control
            var tabContainer = $find('ctl00_cplhControlPanel_frmViewAdd_tablInsert');

            if (tabContainer == null)
                tabContainer = $find('ctl00_cplhControlPanel_frmViewAdd_tabEdit');

            if (tabContainer != null) {
                //  get all of the tabs from the container
                var tabs = tabContainer.get_tabs();

                //  loop through each of the tabs and attach a handler to
                //  the tab header's mouseover event
                for (var i = 0; i < tabs.length; i++) {
                    var tab = tabs[i];

                    $addHandler(
                tab.get_headerTab(),
                'mouseover',
                Function.createDelegate(tab, function () {
                    tabContainer.set_activeTab(this);
                }
            ));
                }
            }
        }

        function PrintItem(ID) {
            window.showModalDialog('./PrintPayment.aspx?ID=' + ID, self, 'dialogWidth:700px;dialogHeight:430px;status:no;dialogHide:yes;unadorned:yes;');
        }
 
    </script>
    <style id="Style1" runat="server">
        .fancy-green .ajax__tab_header
        {
            background: url(App_Themes/NewTheme/Images/green_bg_Tab.gif) repeat-x;
            cursor: pointer;
        }
        .fancy-green .ajax__tab_hover .ajax__tab_outer, .fancy-green .ajax__tab_active .ajax__tab_outer
        {
            background: url(App_Themes/NewTheme/Images/green_left_Tab.gif) no-repeat left top;
        }
        .fancy-green .ajax__tab_hover .ajax__tab_inner, .fancy-green .ajax__tab_active .ajax__tab_inner
        {
            background: url(App_Themes/NewTheme/Images/green_right_Tab.gif) no-repeat right top;
        }
        .fancy .ajax__tab_header
        {
            font-size: 13px;
            font-weight: bold;
            color: #000;
            font-family: sans-serif;
        }
        .fancy .ajax__tab_active .ajax__tab_outer, .fancy .ajax__tab_header .ajax__tab_outer, .fancy .ajax__tab_hover .ajax__tab_outer
        {
            height: 46px;
        }
        .fancy .ajax__tab_active .ajax__tab_inner, .fancy .ajax__tab_header .ajax__tab_inner, .fancy .ajax__tab_hover .ajax__tab_inner
        {
            height: 46px;
            margin-left: 16px; /* offset the width of the left image */
        }
        .fancy .ajax__tab_active .ajax__tab_tab, .fancy .ajax__tab_hover .ajax__tab_tab, .fancy .ajax__tab_header .ajax__tab_tab
        {
            margin: 16px 16px 0px 0px;
        }
        .fancy .ajax__tab_hover .ajax__tab_tab, .fancy .ajax__tab_active .ajax__tab_tab
        {
            color: #fff;
        }
        .fancy .ajax__tab_body
        {
            font-family: Arial;
            font-size: 10pt;
            border-top: 0;
            border: 1px solid #999999;
            padding: 8px;
            background-color: #ffffff;
        }
    </style>
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
                                    <td style="width: 2%;">
                                    </td>
                                    <td style="width: 24%; font-size: 22px; color: White;">
                                        Manual Sales Book
                                    </td>
                                    <td style="width: 16%">
                                        <div style="text-align: right;">
                                            <asp:Panel ID="pnlSearch" runat="server" Width="100px">
                                                <asp:Button ID="lnkBtnAdd" runat="server" OnClick="lnkBtnAdd_Click" CssClass="ButtonAdd66"
                                                    EnableTheming="false" Width="80px" Text=""></asp:Button>
                                            </asp:Panel>
                                        </div>
                                    </td>
                                    <td style="width: 13%; color: White;" align="right">
                                        Search
                                    </td>
                                    <td style="width: 20%" class="NewBox">
                                        <asp:TextBox ID="txtSearch" runat="server" SkinID="skinTxtBoxSearch"></asp:TextBox>
                                    </td>
                                    <td style="width: 20%" class="NewBox">
                                        <div style="width: 160px; font-family: 'Trebuchet MS';">
                                            <asp:DropDownList ID="ddCriteria" runat="server" Width="154px" BackColor="White" Height="23px" Style="text-align: center; border: 1px solid White">
                                                <asp:ListItem Value="BookName">Book Name</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                    <td style="width: 22%" class="tblLeftNoPad">
                                        <asp:Button ID="btnSearch" runat="server" Text="" EnableTheming="false" CssClass="ButtonSearch6"
                                            OnClick="btnSearch_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <input id="dummy" type="button" style="display: none" runat="server" />
                        <input id="Button1" type="button" style="display: none" runat="server" />
                        <input id="dummy1" type="button" style="display: none" runat="server" />
                        <input id="Button2" type="button" style="display: none" runat="server" />
                        <input id="dummy2" type="button" style="display: none" runat="server" />
                        <input id="Button3" type="button" style="display: none" runat="server" />
                        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                            CancelControlID="Button1" DynamicServicePath="" Enabled="True" PopupControlID="popUp"
                            TargetControlID="dummy">
                        </cc1:ModalPopupExtender>
                        <cc1:ModalPopupExtender ID="ModalPopupAddDamaged" runat="server" BackgroundCssClass="modalBackground"
                            CancelControlID="Button2" DynamicServicePath="" Enabled="True" PopupControlID="popUpAddDamagedLeaf"
                            TargetControlID="dummy1">
                        </cc1:ModalPopupExtender>
                        <cc1:ModalPopupExtender ID="ModalPopupUnused" runat="server" BackgroundCssClass="modalBackground"
                            CancelControlID="Button3" DynamicServicePath="" Enabled="True" PopupControlID="popUpUnused"
                            TargetControlID="dummy2">
                        </cc1:ModalPopupExtender>
                        <asp:Panel runat="server" ID="popUpUnUsed" Style="width: 40%">
                            <div id="div1" runat="server" style="text-align: left">
                                <table class="tblLeft" cellpadding="0" cellspacing="0" style="border: 0px solid #5078B3;
                                    background-color: #fff; color: #000; text-align: left" width="100%">
                                    <tr>
                                        <td align="left">
                                            <div class="divArea" style="text-align: left">
                                                <table cellpadding="1" cellspacing="3" style="border: 1px solid #86b2d1; width: 100%;">
                                                    <tr>
                                                        <td>
                                                            <cc1:TabContainer ID="TabContainer2" runat="server" Width="100%" ActiveTabIndex="0"
                                                                CssClass="fancy fancy-green">
                                                                <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="Unused Leaf Details">
                                                                    <ContentTemplate>
                                                                        <table style="width: 600px; border: 0px solid #86b2d1; vertical-align: text-top;
                                                                            text-align: left" cellpadding="1" cellspacing="1">
                                                                            <tr>
                                                                                <td class="ControlLabel" >
                                                                        <asp:GridView ID="gridViewUnUsedLeafs" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                                            OnRowCreated="gridViewUnUsedLeafs_RowCreated" Width="100.3%" CssClass="someClass" PageSize="10"
                                                                            AllowPaging="True" DataKeyNames="LeafNo" EmptyDataText="No Unsed Leafs found for this book!">
                                                                            <EmptyDataRowStyle CssClass="GrdContent" />
                                                                            <Columns>
                                                                                <asp:BoundField DataField="BookName" HeaderText="Book Name" HeaderStyle-BorderColor="Gray"
                                                                                    HeaderStyle-Wrap="false" />
                                                                                <asp:BoundField DataField="LeafNo" HeaderText="Leaf No" HeaderStyle-BorderColor="Gray"
                                                                                    HeaderStyle-Wrap="false" />
                                                                            </Columns>
                                                                            <PagerTemplate>
                                                                                <table style="border-color: white">
                                                                                    <tr style="border-color: white">
                                                                                        <td style="border-color: white">
                                                                                            Goto Page
                                                                                        </td>
                                                                                        <td style="border-color: white">
                                                                                            <asp:DropDownList ID="ddlPageSelector" runat="server" Width="65px" Style="border: 1px solid blue">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td style="border-color: white; width: 5px">
                                                                                        </td>
                                                                                        <td style="border-color: white">
                                                                                            <asp:Button Text="" CommandName="Page" CommandArgument="First" runat="server" CssClass="NewFirst"
                                                                                                EnableTheming="false" Width="22px" Height="18px" ID="btnFirst" />
                                                                                        </td>
                                                                                        <td style="border-color: white">
                                                                                            <asp:Button Text="" CommandName="Page" CommandArgument="Prev" runat="server" CssClass="NewPrev"
                                                                                                EnableTheming="false" Width="22px" Height="18px" ID="btnPrevious" />
                                                                                        </td>
                                                                                        <td style="border-color: white">
                                                                                            <asp:Button Text="" CommandName="Page" CommandArgument="Next" runat="server" CssClass="NewNext"
                                                                                                EnableTheming="false" Width="22px" Height="18px" ID="btnNext" />
                                                                                        </td>
                                                                                        <td style="border-color: white">
                                                                                            <asp:Button Text="" CommandName="Page" CommandArgument="Last" runat="server" CssClass="NewLast"
                                                                                                EnableTheming="false" Width="22px" Height="18px" ID="btnLast" />
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </PagerTemplate>
                                                                        </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="text-align: center">
                                                                                    <asp:Button ID="btnCancelUnused" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                        CssClass="cancelbutton6" EnableTheming="false" SkinID="skinBtnCancel" OnClick="btnCancelUnused_Click">
                                                                                    </asp:Button>
                                                                                </td>

                                                                            </tr>
                                                                        </table>
                                                                    </ContentTemplate>
                                                                </cc1:TabPanel>
                                                            </cc1:TabContainer>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="popUpAddDamagedLeaf" Style="width: 40%">
                            <div id="divDamangedLeaf" runat="server" style="text-align: left">
                                <table class="tblLeft" cellpadding="0" cellspacing="0" style="border: 0px solid #5078B3;
                                    background-color: #fff; color: #000; text-align: left" width="100%">
                                    <tr>
                                        <td align="left">
                                            <div class="divArea" style="text-align: left">
                                                <table cellpadding="1" cellspacing="3" style="border: 1px solid #86b2d1; width: 100%;">
                                                    <tr>
                                                        <td>
                                                            <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" ActiveTabIndex="0"
                                                                CssClass="fancy fancy-green">
                                                                <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="Damaged Leaf Details">
                                                                    <ContentTemplate>
                                                                        <table style="width: 600px; border: 0px solid #86b2d1; vertical-align: text-top;
                                                                            text-align: left" cellpadding="1" cellspacing="1">
                                                                            <tr>
                                                                                <td class="ControlLabel" style="width: 30%;">
                                                                                    Leaf No: *
                                                                                    <asp:RequiredFieldValidator ID="rvLeaf" runat="server" ControlToValidate="txtDamagedLeaf" ValidationGroup="addDMG"
                                                                                        ErrorMessage="Leaf No is mandatory" Text="*" />
                                                                                </td>
                                                                                <td class="ControlDrpBorder" style="width: 40%;">
                                                                                    <asp:TextBox ID="txtDamagedLeaf" runat="server" SkinID="skinTxtBoxGrid" TabIndex="2"></asp:TextBox>
                                                                                </td>
                                                                                <td style="width: 30%; text-align: center">
                                                                                    <asp:Button ID="btnSaveDamagedLeaf" runat="server" CausesValidation="True" CommandName="Insert"
                                                                                        CssClass="Updatebutton1231" EnableTheming="false" SkinID="skinBtnSave" OnClick="SaveDamangedLeafButton_Click">
                                                                                    </asp:Button>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="ControlLabel" style="width: 20%;">
                                                                                    Comments: *
                                                                                    <asp:RequiredFieldValidator ID="rvComments" runat="server" ControlToValidate="txtComments" ValidationGroup="addDMG"
                                                                                        ErrorMessage="Comments is mandatory" Text="*" />
                                                                                </td>
                                                                                <td style="width: 40%;" class="ControlDrpBorder">
                                                                                    <asp:TextBox ID="txtComments" runat="server" BackColor="#e7e7e7" CssClass="cssTextBox"
                                                                                        Height="23px" ValidationGroup="grpDetails" Width="120px"></asp:TextBox>
                                                                                </td>
                                                                                <td style="width: 30%; text-align: center">
                                                                                    <asp:Button ID="btnCancelSaveComments" runat="server" CausesValidation="False" CommandName="Cancel" ValidationGroup="addDMG"
                                                                                        CssClass="cancelbutton6" EnableTheming="false" SkinID="skinBtnCancel" OnClick="btnCancelDamangedLeaf_Click">
                                                                                    </asp:Button>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </ContentTemplate>
                                                                </cc1:TabPanel>
                                                            </cc1:TabContainer>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="popUp" Style="width: 50%">
                            <div id="contentPopUp">
                                <table cellpadding="2" cellspacing="2" style="border: 1px solid blue; background-color: #fff;
                                    color: #000;" width="100%">
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlVisitDetails" runat="server" Visible="false">
                                                <div>
                                                    <table cellpadding="2" cellspacing="1" style="border: 0px solid blue;" width="100%">
                                                        <tr>
                                                            <tr>
                                                                <td colspan="4">
                                                                    <table class="headerPopUp" width="100%">
                                                                        <tr>
                                                                            <td>
                                                                                Book Information
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 5px">
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 10%">
                                                                </td>
                                                                <td style="width: 25%" class="ControlLabel">
                                                                    Book Name *
                                                                    <asp:RequiredFieldValidator ID="rvAliasNameAdd" runat="server" ControlToValidate="txtBookNameAdd" ValidationGroup="editBook"
                                                                        Display="Dynamic" ErrorMessage="Book Name is mandatory">*</asp:RequiredFieldValidator>
                                                                </td>
                                                                <td style="width: 25%;" class="ControlDrpBorder">
                                                                    <asp:TextBox ID="txtBookNameAdd" runat="server" Text='<%# Bind("BookName") %>' SkinID="skinTxtBoxGrid"
                                                                        TabIndex="2"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 33%">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 10%">
                                                                </td>
                                                                <td style="width: 25%" class="ControlLabel">
                                                                    Book No From *
                                                                    <asp:RequiredFieldValidator ID="rvOpenBalAdd" runat="server" ControlToValidate="txtFromNoAdd" ValidationGroup="editBook"
                                                                        Display="Dynamic" ErrorMessage="Book No. From is mandatory">*</asp:RequiredFieldValidator>
                                                                </td>
                                                                <td style="width: 25%" class="ControlDrpBorder">
                                                                    <asp:TextBox ID="txtFromNoAdd" runat="server" Text='<%# Bind("BookFrom") %>' SkinID="skinTxtBoxGrid"
                                                                        TabIndex="3"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 33%">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 10%">
                                                                </td>
                                                                <td style="width: 25%;" class="ControlLabel">
                                                                    Book To *
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtBookToAdd" ValidationGroup="editBook"
                                                                        Display="Dynamic" ErrorMessage="Book To is mandatory">*</asp:RequiredFieldValidator>
                                                                    <asp:CompareValidator ID="cvBook" runat="server" Display="Dynamic" ErrorMessage="To Book cannot be less than From Book No." ValidationGroup="editBook"
                                                                        ControlToCompare="txtFromNoAdd" ControlToValidate="txtBookToAdd" Operator="GreaterThan">*</asp:CompareValidator>
                                                                </td>
                                                                <td style="width: 25%" class="ControlDrpBorder">
                                                                    <asp:TextBox ID="txtBookToAdd" runat="server" Text='<%# Bind("BookTo") %>' SkinID="skinTxtBoxGrid"
                                                                        TabIndex="4"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 33%">
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 5px">
                                                            </tr>
                                                            <tr>
                                                                <td align="center" style="width: 100%" colspan="4">
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td style="width: 30%">
                                                                            </td>
                                                                            <td align="center" style="width: 18%">
                                                                                <asp:Button ID="CancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                    CssClass="cancelbutton6" EnableTheming="false" SkinID="skinBtnCancel" OnClick="UpdateCancelButton_Click">
                                                                                </asp:Button>
                                                                            </td>
                                                                            <td align="center" style="width: 18%">
                                                                                <asp:Button ID="SaveButton" runat="server" CommandName="Update" SkinID="skinBtnSave" ValidationGroup="editBook"
                                                                                    CssClass="savebutton1231" EnableTheming="false" OnClick="SaveButton_Click"></asp:Button>
                                                                                <asp:Button ID="UpdateButton" runat="server" CommandName="Update" SkinID="skinBtnSave" ValidationGroup="editBook"
                                                                                    CssClass="Updatebutton1231" EnableTheming="false" OnClick="UpdateButton_Click">
                                                                                </asp:Button>
                                                                            </td>
                                                                            <td style="width: 30%">
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 10%">
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
                        <table width="100%" style="margin: -6px 0px 0px 0px;">
                            <tr style="width: 100%">
                                <td>
                                    <asp:HiddenField ID="hdVisitID" runat="server" Value="0" />
                                    <div class="mainGridHold" id="searchGrid">
                                        <asp:GridView ID="GrdViewManualBook" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                            OnRowCreated="GrdViewManualBook_RowCreated" Width="100.3%" DataSourceID="GridSource"
                                            CssClass="someClass" AllowPaging="True" DataKeyNames="BookId" EmptyDataText="No Cheque Book found!"
                                            OnRowCommand="GrdViewManualBook_RowCommand" OnRowDataBound="GrdViewManualBook_RowDataBound"
                                            OnSelectedIndexChanged="GrdViewManualBook_SelectedIndexChanged" OnRowDeleting="GrdViewManualBook_RowDeleting"
                                            OnRowDeleted="GrdViewManualBook_RowDeleted">
                                            <EmptyDataRowStyle CssClass="GrdContent" />
                                            
                                            <HeaderStyle Height="30px" HorizontalAlign="Center" Font-Bold="true" BackColor="#cccccc" BorderColor="Gray" Font-Size="Small" />
                                            <RowStyle Font-Bold="true" HorizontalAlign="Center" Height="30px" Font-Size="Small" ForeColor="#0567AE" />
                                            <Columns>
                                                <asp:BoundField DataField="BookId" HeaderText="BookId" HeaderStyle-BorderColor="Gray"
                                                    Visible="false" HeaderStyle-Wrap="false" />
                                                <asp:BoundField DataField="BookName" HeaderText="BookName" HeaderStyle-BorderColor="Gray"
                                                    HeaderStyle-Wrap="false" />
                                                <asp:BoundField DataField="BookFrom" HeaderText="Book From" HeaderStyle-BorderColor="Gray"
                                                    HeaderStyle-Wrap="false" />
                                                <asp:BoundField DataField="BookTo" HeaderText="Book To" HeaderStyle-BorderColor="Gray"
                                                    HeaderStyle-Wrap="false" />
                                                <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="50px" HeaderText="Unused Leafs"
                                                    HeaderStyle-BorderColor="Gray">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnShowUnusedLeafs" runat="server" CausesValidation="false"
                                                            SkinID="edit" CommandName="Select" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="50px" HeaderText="Add Damaged Leaf"
                                                    HeaderStyle-BorderColor="Gray">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnAddDamagedLeaf" runat="server" CausesValidation="false" SkinID="edit"
                                                            CommandName="Select" />
                                                        <asp:ImageButton ID="btnAddDamagedLeafDisabled" Enabled="false" SkinID="editDisable"
                                                            runat="Server"></asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="50px" HeaderText="Edit"
                                                    HeaderStyle-BorderColor="Gray">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" SkinID="edit"
                                                            CommandName="Select" />
                                                        <asp:ImageButton ID="btnEditDisabled" Enabled="false" SkinID="editDisable" runat="Server">
                                                        </asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="command" HeaderText="Delete" HeaderStyle-Width="50px"
                                                    HeaderStyle-BorderColor="Gray" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to Delete this Book Details?"
                                                            runat="server">
                                                        </cc1:ConfirmButtonExtender>
                                                        <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" CommandName="Delete"></asp:ImageButton>
                                                        <asp:ImageButton ID="lnkBDisabled" Enabled="false" SkinID="deleteDisable" runat="Server">
                                                        </asp:ImageButton>
                                                        <asp:HiddenField ID="ldgID" runat="server" Value='<%# Bind("BookId") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataRowStyle CssClass="GrdContent" />
                                            <PagerTemplate>
                                                <table style="border-color: white">
                                                    <tr style="border-color: white">
                                                        <td style="border-color: white">
                                                            Goto Page
                                                        </td>
                                                        <td style="border-color: white">
                                                            <asp:DropDownList ID="ddlPageSelector" runat="server" Width="65px" Style="border: 1px solid blue">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="border-color: white; width: 5px">
                                                        </td>
                                                        <td style="border-color: white">
                                                            <asp:Button Text="" CommandName="Page" CommandArgument="First" runat="server" CssClass="NewFirst"
                                                                EnableTheming="false" Width="22px" Height="18px" ID="btnFirst" />
                                                        </td>
                                                        <td style="border-color: white">
                                                            <asp:Button Text="" CommandName="Page" CommandArgument="Prev" runat="server" CssClass="NewPrev"
                                                                EnableTheming="false" Width="22px" Height="18px" ID="btnPrevious" />
                                                        </td>
                                                        <td style="border-color: white">
                                                            <asp:Button Text="" CommandName="Page" CommandArgument="Next" runat="server" CssClass="NewNext"
                                                                EnableTheming="false" Width="22px" Height="18px" ID="btnNext" />
                                                        </td>
                                                        <td style="border-color: white">
                                                            <asp:Button Text="" CommandName="Page" CommandArgument="Last" runat="server" CssClass="NewLast"
                                                                EnableTheming="false" Width="22px" Height="18px" ID="btnLast" />
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
                        <asp:ObjectDataSource ID="GridSource" runat="server" SelectMethod="ListManualSalesBookInfo"
                            TypeName="BusinessLogic" DeleteMethod="DeleteManualSaleBook" OnDeleting="GridSource_Deleting">
                            <DeleteParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                <asp:Parameter Name="BookId" Type="Int32" />
                                <asp:Parameter Name="Username" Type="String" />
                                <asp:Parameter Name="Types" Type="String" />
                            </DeleteParameters>
                        </asp:ObjectDataSource>
                        <asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="true" ShowSummary="false" />
                        <asp:HiddenField ID="hdDataSource" runat="server" />
                        <asp:HiddenField ID="hdBookID" runat="server" Value="0" />
                        <asp:HiddenField ID="hdCustomerID" runat="server" Value="0" />
                        <asp:HiddenField ID="hdDueDate" runat="server" Value="" />
                        <asp:HiddenField ID="hdRefNumber" runat="server" Value="" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
