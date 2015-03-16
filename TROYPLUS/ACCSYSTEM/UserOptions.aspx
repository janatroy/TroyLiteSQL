<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="UserOptions.aspx.cs" Inherits="UserOptions" Title="Administration > Users Lock" %>

<%@ Register Assembly="RealWorld.Grids" Namespace="RealWorld.Grids" TagPrefix="rwg" %>
<%@ Register Namespace="UrlNameSpace" TagPrefix="uc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <script language="javascript" type="text/javascript">
        function pageLoad() {
            //  get the behavior associated with the tab control
            var tabContainer = $find('ctl00_cplhControlPanel_tabContol');

            if (tabContainer == null)
                tabContainer = $find('ctl00_cplhControlPanel_tabCustomer');

            if (tabContainer == null)
                tabContainer = $find('ctl00_cplhControlPanel_tabSupplier');

            if (tabContainer == null)
                tabContainer = $find('ctl00_cplhControlPanel_TabBANKING');

            if (tabContainer == null)
                tabContainer = $find('ctl00_cplhControlPanel_TabEXPENSES');

            if (tabContainer == null)
                tabContainer = $find('ctl00_cplhControlPanel_TabINVENTORY');

            if (tabContainer == null)
                tabContainer = $find('ctl00_cplhControlPanel_TabACCOUNTS');

            if (tabContainer == null)
                tabContainer = $find('ctl00_cplhControlPanel_TabRESOURCE');

            if (tabContainer == null)
                tabContainer = $find('ctl00_cplhControlPanel_TabSERVICE');

            if (tabContainer == null)
                tabContainer = $find('ctl00_cplhControlPanel_TabOTHER');

            if (tabContainer == null)
                tabContainer = $find('ctl00_cplhControlPanel_TabREPORT');

            if (tabContainer == null)
                tabContainer = $find('ctl00_cplhControlPanel_TabSECURITY');

            if (tabContainer != null) {
                //  get all of the tabs from the container
                var tabs = tabContainer.get_tabs();

                //  loop through each of the tabs and attach a handler to
                //  the tab header's mouseover event
                for (var i = 0; i < tabs.length; i++) {
                    var tab = tabs[i];

                    $addHandler(tab.get_headerTab(), 'mouseover', Function.createDelegate(tab, function () {
                        tabContainer.set_activeTab(this);
                    }));
                }
            }
        }


        window.onload = function Showalert() {

            var txt = document.getElementById("<%= txtUserName.ClientID %>");
            var btn = document.getElementById("<%= BtnClearFilter.ClientID %>");
            if (txt.value == "") {
                // alert(txt.value);
                btn.style.visibility = "hidden";
                // when the window is loaded, hide the button if the textbox is empty
            }

        }

        function EnableDisableButton(sender, target) {
            var first = document.getElementById('<%=txtUserName.ClientID %>');

            if (sender.value.length >= 1 && first.value.length >= 1) {
                document.getElementById('<%=BtnClearFilter.ClientID %>').style.visibility = "visible";

            }

            if (sender.value.length < 1 && first.value.length < 1) {

                document.getElementById('<%=BtnClearFilter.ClientID %>').style.visibility = "Hidden";
            }
        }

    </script>

    <style id="Style1" runat="server">
        .fancy-green .ajax__tab_header {
            background: url(App_Themes/NewTheme/Images/green_bg_Tab.gif) repeat-x;
            cursor: pointer;
        }

        .fancy-green .ajax__tab_hover .ajax__tab_outer, .fancy-green .ajax__tab_active .ajax__tab_outer {
            background: url(App_Themes/NewTheme/Images/green_left_Tab.gif) no-repeat left top;
        }

        .fancy-green .ajax__tab_hover .ajax__tab_inner, .fancy-green .ajax__tab_active .ajax__tab_inner {
            background: url(App_Themes/NewTheme/Images/green_right_Tab.gif) no-repeat right top;
        }

        .fancy .ajax__tab_header {
            font-size: 13px;
            font-weight: bold;
            color: #000;
            font-family: sans-serif;
        }

            .fancy .ajax__tab_active .ajax__tab_outer, .fancy .ajax__tab_header .ajax__tab_outer, .fancy .ajax__tab_hover .ajax__tab_outer {
                height: 46px;
            }

            .fancy .ajax__tab_active .ajax__tab_inner, .fancy .ajax__tab_header .ajax__tab_inner, .fancy .ajax__tab_hover .ajax__tab_inner {
                height: 46px;
                margin-left: 16px; /* offset the width of the left image */
            }

            .fancy .ajax__tab_active .ajax__tab_tab, .fancy .ajax__tab_hover .ajax__tab_tab, .fancy .ajax__tab_header .ajax__tab_tab {
                margin: 16px 16px 0px 0px;
            }

        .fancy .ajax__tab_hover .ajax__tab_tab, .fancy .ajax__tab_active .ajax__tab_tab {
            color: #fff;
        }

        .fancy .ajax__tab_body {
            font-family: Arial;
            font-size: 10pt;
            border-top: 0;
            border: 1px solid #999999;
            padding: 8px;
            background-color: #ffffff;
        }
    </style>

    <asp:UpdatePanel ID="UpdatePanelPage" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table style="width: 100%">
                <tr style="width: 100%">
                    <td style="width: 100%">
                        <%--<table class="mainConHd" style="width: 994px;">
                    <tr valign="middle">
                        <td style="font-size: 20px;">
                            Users And Options
                        </td>
                    </tr>
                </table>--%>
                        <div class="mainConBody">
                            <table style="width: 99.8%; margin: -2px 0px 0px 1px;" cellpadding="3" cellspacing="2" class="searchbg">
                                <tr style="height: 25px; vertical-align: middle">
                                    <td style="width: 2%;"></td>
                                    <td style="width: 15%; font-size: 22px; color: White;">USER
                                    </td>
                                    <td style="width: 28%">
                                        <div style="text-align: right;">
                                            <asp:Panel ID="pnlSearch" runat="server" Width="100px">
                                                <asp:Button ID="lnkBtnAdd" runat="server" OnClick="lnkBtnAdd_Click" CssClass="ButtonAdd66"
                                                    EnableTheming="false" Width="80px" Text=""></asp:Button>
                                            </asp:Panel>
                                        </div>
                                    </td>
                                    <td style="width: 20%; color: White;" align="right">User Name
                                    </td>
                                    <td style="width: 20%" class="NewBox">
                                        <asp:TextBox ID="txtUserName" runat="server" SkinID="skinTxtBoxSearch"></asp:TextBox>
                                    </td>
                                    <td colspan="2" class="tblLeftNoPad" style="width: 20%">
                                        <asp:Button ID="lnkBtnSearchId" onkeyup="EnableDisableButton(this,'BtnClearFilter')" runat="server" OnClick="lnkBtnSearch_Click" Text=""
                                            ToolTip="Click here to submit" CssClass="ButtonSearch6" EnableTheming="false" TabIndex="3" />
                                    </td>
                                    <td style="width: 20%" class="tblLeftNoPad">
                                        <asp:Button ID="BtnClearFilter" runat="server" OnClick="BtnClearFilter_Click" EnableTheming="false" Text="" CssClass="ClearFilter6" />
                                    </td>
                                </tr>
                            </table>
                        </div>


                        <input id="dummyPurchase" type="button" style="display: none" runat="server" />
                        <input id="BtnPopUpCancel1" type="button" style="display: none" runat="server" />
                        <cc1:ModalPopupExtender ID="ModalPopupGet" runat="server" BackgroundCssClass="modalBackground"
                            CancelControlID="BtnPopUpCancel1" DynamicServicePath="" Enabled="True" PopupControlID="purchasePanel"
                            RepositionMode="RepositionOnWindowResizeAndScroll" TargetControlID="dummyPurchase">
                        </cc1:ModalPopupExtender>
                        <asp:Panel ID="purchasePanel" runat="server" Style="width: 100%; display: none">
                            <asp:UpdatePanel ID="updatePnlPurchase" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div id="Div1" style="background-color: White;">
                                        <table style="width: 100%;" align="center">
                                            <tr style="width: 100%">
                                                <td style="width: 100%">
                                                    <table style="text-align: left; border: 1px solid Gray;" width="100%" cellpadding="2" cellspacing="2">
                                                        <tr>
                                                            <td colspan="5">
                                                                <table class="headerPopUp" width="100%">
                                                                    <tr>
                                                                        <td>User
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="5">
                                                                <table cellpadding="2" cellspacing="2"
                                                                    width="100%">
                                                                    <tr style="height: 5px">
                                                                        <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="BulletList" ShowMessageBox="true"
                                                                            ShowSummary="false" ValidationGroup="product" HeaderText="Validation Messages"
                                                                            Font-Names="'Trebuchet MS'" Font-Size="12" runat="server" />
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="5">
                                                                            <table width="100%">
                                                                                <tr>
                                                                                    <td class="ControlLabel" style="width: 28%">UserName *
                                                                                    <asp:RequiredFieldValidator ID="rvUserName" runat="server" ErrorMessage="User Name is mandatory" Display="Dynamic" ValidationGroup="product" ControlToValidate="txtUser"
                                                                                        Text="*"></asp:RequiredFieldValidator>
                                                                                    </td>
                                                                                    <td class="ControlTextBox3" style="width: 18%">
                                                                                        <asp:TextBox ID="txtUser" runat="server" SkinID="skinTxtBoxGrid" ValidationGroup="product"></asp:TextBox>
                                                                                    </td>
                                                                                    <td class="ControlLabel" style="width: 15%">Email
                                                                                    </td>
                                                                                    <td class="ControlTextBox3" style="width: 18%">
                                                                                        <asp:TextBox ID="txtEmail" runat="server" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                    </td>
                                                                                    <td style="width: 22%">
                                                                                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                                                                            <ContentTemplate>
                                                                                                <asp:CheckBox ID="chkboxdatelock" runat="server" Text="Date Lock" Font-Size="15px" AutoPostBack="true" Visible="False" />
                                                                                                <asp:CheckBox runat="server" ID="chkAccLocked" Visible="False" />
                                                                                                <asp:CheckBox ID="chkhidedeviation" runat="server" Text="Allow Deviation Checks" Font-Size="15px" AutoPostBack="true" />
                                                                                            </ContentTemplate>

                                                                                        </asp:UpdatePanel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr style="height: 2px">
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="ControlLabel" style="width: 25%">Password *
                                                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Password is mandatory" Display="Dynamic"  ValidationGroup="product" ControlToValidate="txtpassword"
                                                                                        Text="*"></asp:RequiredFieldValidator>--%>
                                                                                    </td>
                                                                                    <td class="ControlTextBox3" style="width: 18%">
                                                                                        <asp:TextBox ID="txtpassword" runat="server" SkinID="skinTxtBoxGrid" ValidationGroup="product" TextMode="Password"></asp:TextBox>
                                                                                    </td>
                                                                                    <td class="ControlLabel" style="width: 15%">Confirm Password *
                                                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Confirm Password is mandatory" Display="Dynamic"  ValidationGroup="product" ControlToValidate="txtconfirmpassword"
                                                                                        Text="*"></asp:RequiredFieldValidator>
                                                                                    <asp:CompareValidator ID="cmpPassword" runat="server" ControlToValidate="txtconfirmpassword" ValidationGroup="product"
                                                                                        ControlToCompare="txtpassword" Display="Dynamic" Operator="Equal" 
                                                                                        ErrorMessage="New password and Confirm password dosent match.">*</asp:CompareValidator>--%>
                                                                                    </td>
                                                                                    <td class="ControlTextBox3" style="width: 18%">
                                                                                        <asp:TextBox ID="txtconfirmpassword" runat="server" SkinID="skinTxtBoxGrid" TextMode="Password"></asp:TextBox>
                                                                                    </td>
                                                                                    <td style="width: 22%">
                                                                                        <asp:Label ID="lbloption" runat="server" Visible="False" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr style="height: 2px">
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="ControlLabel" style="width: 25%">Executive
                                                                                    </td>
                                                                                    <td class="ControlDrpBorder" style="width: 18%">
                                                                                        <asp:DropDownList ID="drpIncharge" TabIndex="5" Enabled="True" EnableTheming="false" AppendDataBoundItems="true" CssClass="drpDownListMedium"
                                                                                            runat="server" Width="100%" DataTextField="empFirstName" BackColor="#e7e7e7" Style="border: 1px solid #e7e7e7" Height="26px"
                                                                                            DataValueField="empno">
                                                                                            <asp:ListItem Text="Select Executive" Value="0"></asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td class="ControlLabel" style="width: 15%">User Group
                                                                                    </td>
                                                                                    <td style="width: 18%" class="ControlTextBox3">
                                                                                        <asp:TextBox ID="txtUserGroup" runat="server" SkinID="skinTxtBoxGrid" ValidationGroup="product"></asp:TextBox>
                                                                                    </td>
                                                                                    <td style="width: 22%"></td>
                                                                                </tr>
                                                                                <tr style="height: 2px">
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="ControlLabel" style="width: 25%">Branch  *
                                                                                    </td>
                                                                                    <td class="ControlDrpBorder" style="width: 18%">
                                                                                        <asp:DropDownList ID="drpBranch" TabIndex="5" Enabled="True" EnableTheming="false" AppendDataBoundItems="true" CssClass="drpDownListMedium"
                                                                                            runat="server" Width="100%" DataTextField="BranchName" BackColor="#e7e7e7" Style="border: 1px solid #e7e7e7" Height="26px"
                                                                                            DataValueField="Branchcode">
                                                                                            <asp:ListItem Text="Select Branch" Value="0"></asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td style="width: 15%">
                                                                                        <asp:UpdatePanel runat="server" ID="upBranch">
                                                                                            <ContentTemplate>
                                                                                                <asp:CheckBox ID="chkBranch" runat="server" Text="All Branch" Font-Size="15px" AutoPostBack="true" />
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </td>
                                                                                    <td style="width: 18%"></td>
                                                                                    <td style="width: 22%"></td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="height: 5px">
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="5">
                                                                            <div id="div" runat="server" style="height: 307px; overflow: scroll">
                                                                                <cc1:TabContainer ID="tabContol" runat="server" Width="100%" ActiveTabIndex="0" CssClass="fancy fancy-green">
                                                                                    <cc1:TabPanel ID="tabCustomer" runat="server" HeaderText="Customer">
                                                                                        <ContentTemplate>
                                                                                            <rwg:BulkEditGridView ID="GrdViewItem" AutoGenerateColumns="False" BorderWidth="1px"
                                                                                                BorderStyle="Solid" GridLines="Both" SaveButtonID="SaveButton" runat="server" CssClass="someClass"
                                                                                                Width="1057px">
                                                                                                <RowStyle CssClass="dataRow" />
                                                                                                <SelectedRowStyle CssClass="SelectdataRow" />
                                                                                                <AlternatingRowStyle CssClass="altRow" />
                                                                                                <EmptyDataRowStyle CssClass="HeadataRow" Font-Bold="true" />
                                                                                                <HeaderStyle CssClass="HeadataRow" Wrap="false" />
                                                                                                <FooterStyle CssClass="dataRow" />
                                                                                                <Columns>
                                                                                                    <asp:BoundField DataField="Area" HeaderText="Area" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                                                                    <asp:BoundField DataField="Section" HeaderText="Section" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                                                                    <asp:BoundField DataField="RoleDesc" HeaderText="Screen Name" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                                                                    <asp:BoundField DataField="Role" HeaderText="Roles" HeaderStyle-Wrap="false" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" />
                                                                                                    <asp:TemplateField Visible="false">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblDebtorID" runat="server" Text='<%# Eval("OrderNo")%>' />
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="Add" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxAdd" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Add") %>'></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="Edit" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxEdit" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Edit") %>'></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="Delete" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxDel" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Delete") %>'></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="View" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxView" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Views") %>'></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                </Columns>
                                                                                            </rwg:BulkEditGridView>
                                                                                        </ContentTemplate>
                                                                                    </cc1:TabPanel>
                                                                                    <cc1:TabPanel ID="tabSupplier" runat="server" HeaderText="Supplier">
                                                                                        <ContentTemplate>
                                                                                            <rwg:BulkEditGridView ID="GridSupplier" AutoGenerateColumns="False" BorderWidth="1px"
                                                                                                BorderStyle="Solid" GridLines="Both" SaveButtonID="SaveButton" runat="server" CssClass="someClass"
                                                                                                Width="1057px">
                                                                                                <RowStyle CssClass="dataRow" />
                                                                                                <SelectedRowStyle CssClass="SelectdataRow" />
                                                                                                <AlternatingRowStyle CssClass="altRow" />
                                                                                                <EmptyDataRowStyle CssClass="HeadataRow" Font-Bold="true" />
                                                                                                <HeaderStyle CssClass="HeadataRow" Wrap="false" />
                                                                                                <FooterStyle CssClass="dataRow" />
                                                                                                <Columns>
                                                                                                    <asp:BoundField DataField="Area" HeaderText="Area" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                                                                    <asp:BoundField DataField="Section" HeaderText="Section" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                                                                    <asp:BoundField DataField="RoleDesc" HeaderText="Screen Name" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                                                                    <asp:BoundField DataField="Role" HeaderText="Roles" HeaderStyle-Wrap="false" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" />
                                                                                                    <asp:TemplateField Visible="false">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblDebtorID" runat="server" Text='<%# Eval("OrderNo")%>' />
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="Add" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxAdd" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Add") %>'></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="Edit" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxEdit" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Edit") %>'></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="Delete" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxDel" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Delete") %>'></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="View" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxView" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Views") %>'></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                </Columns>
                                                                                            </rwg:BulkEditGridView>
                                                                                        </ContentTemplate>
                                                                                    </cc1:TabPanel>
                                                                                    <cc1:TabPanel ID="TabBANKING" runat="server" HeaderText="Banking">
                                                                                        <ContentTemplate>
                                                                                            <rwg:BulkEditGridView ID="GridBANKING" AutoGenerateColumns="False" BorderWidth="1px"
                                                                                                BorderStyle="Solid" GridLines="Both" SaveButtonID="SaveButton" runat="server" CssClass="someClass"
                                                                                                Width="1057px">
                                                                                                <RowStyle CssClass="dataRow" />
                                                                                                <SelectedRowStyle CssClass="SelectdataRow" />
                                                                                                <AlternatingRowStyle CssClass="altRow" />
                                                                                                <EmptyDataRowStyle CssClass="HeadataRow" Font-Bold="true" />
                                                                                                <HeaderStyle CssClass="HeadataRow" Wrap="false" />
                                                                                                <FooterStyle CssClass="dataRow" />
                                                                                                <Columns>
                                                                                                    <asp:BoundField DataField="Area" HeaderText="Area" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                                                                    <asp:BoundField DataField="Section" HeaderText="Section" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                                                                    <asp:BoundField DataField="RoleDesc" HeaderText="Screen Name" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                                                                    <asp:BoundField DataField="Role" HeaderText="Roles" HeaderStyle-Wrap="false" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" />
                                                                                                    <asp:TemplateField Visible="false">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblDebtorID" runat="server" Text='<%# Eval("OrderNo")%>' />
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="Add" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxAdd" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Add") %>'></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="Edit" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxEdit" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Edit") %>'></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="Delete" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxDel" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Delete") %>'></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="View" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxView" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Views") %>'></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                </Columns>
                                                                                            </rwg:BulkEditGridView>
                                                                                        </ContentTemplate>
                                                                                    </cc1:TabPanel>
                                                                                    <cc1:TabPanel ID="TabEXPENSES" runat="server" HeaderText="Expenses">
                                                                                        <ContentTemplate>
                                                                                            <rwg:BulkEditGridView ID="GridEXPENSES" AutoGenerateColumns="False" BorderWidth="1px"
                                                                                                BorderStyle="Solid" GridLines="Both" SaveButtonID="SaveButton" runat="server" CssClass="someClass"
                                                                                                Width="1057px">
                                                                                                <RowStyle CssClass="dataRow" />
                                                                                                <SelectedRowStyle CssClass="SelectdataRow" />
                                                                                                <AlternatingRowStyle CssClass="altRow" />
                                                                                                <EmptyDataRowStyle CssClass="HeadataRow" Font-Bold="true" />
                                                                                                <HeaderStyle CssClass="HeadataRow" Wrap="false" />
                                                                                                <FooterStyle CssClass="dataRow" />
                                                                                                <Columns>
                                                                                                    <asp:BoundField DataField="Area" HeaderText="Area" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                                                                    <asp:BoundField DataField="Section" HeaderText="Section" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                                                                    <asp:BoundField DataField="RoleDesc" HeaderText="Screen Name" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                                                                    <asp:BoundField DataField="Role" HeaderText="Roles" HeaderStyle-Wrap="false" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" />
                                                                                                    <asp:TemplateField Visible="false">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblDebtorID" runat="server" Text='<%# Eval("OrderNo")%>' />
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="Add" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxAdd" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Add") %>'></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="Edit" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxEdit" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Edit") %>'></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="Delete" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxDel" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Delete") %>'></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="View" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxView" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Views") %>'></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                </Columns>
                                                                                            </rwg:BulkEditGridView>
                                                                                        </ContentTemplate>
                                                                                    </cc1:TabPanel>
                                                                                    <cc1:TabPanel ID="TabINVENTORY" runat="server" HeaderText="Inventory">
                                                                                        <ContentTemplate>
                                                                                            <rwg:BulkEditGridView ID="GridINVENTORY" AutoGenerateColumns="False" BorderWidth="1px"
                                                                                                BorderStyle="Solid" GridLines="Both" SaveButtonID="SaveButton" runat="server" CssClass="someClass"
                                                                                                Width="1057px">
                                                                                                <RowStyle CssClass="dataRow" />
                                                                                                <SelectedRowStyle CssClass="SelectdataRow" />
                                                                                                <AlternatingRowStyle CssClass="altRow" />
                                                                                                <EmptyDataRowStyle CssClass="HeadataRow" Font-Bold="true" />
                                                                                                <HeaderStyle CssClass="HeadataRow" Wrap="false" />
                                                                                                <FooterStyle CssClass="dataRow" />
                                                                                                <Columns>
                                                                                                    <asp:BoundField DataField="Area" HeaderText="Area" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                                                                    <asp:BoundField DataField="Section" HeaderText="Section" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                                                                    <asp:BoundField DataField="RoleDesc" HeaderText="Screen Name" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                                                                    <asp:BoundField DataField="Role" HeaderText="Roles" HeaderStyle-Wrap="false" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" />
                                                                                                    <asp:TemplateField Visible="false">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblDebtorID" runat="server" Text='<%# Eval("OrderNo")%>' />
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="Add" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxAdd" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Add") %>'></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="Edit" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxEdit" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Edit") %>'></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="Delete" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxDel" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Delete") %>'></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="View" HeaderStyle-BorderColor="blue">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxView" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Views") %>'></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                </Columns>
                                                                                            </rwg:BulkEditGridView>
                                                                                        </ContentTemplate>
                                                                                    </cc1:TabPanel>
                                                                                    <cc1:TabPanel ID="TabACCOUNTS" runat="server" HeaderText="Accounts">
                                                                                        <ContentTemplate>
                                                                                            <rwg:BulkEditGridView ID="GridACCOUNTS" AutoGenerateColumns="False" BorderWidth="1px"
                                                                                                BorderStyle="Solid" GridLines="Both" SaveButtonID="SaveButton" runat="server" CssClass="someClass"
                                                                                                Width="1057px">
                                                                                                <RowStyle CssClass="dataRow" />
                                                                                                <SelectedRowStyle CssClass="SelectdataRow" />
                                                                                                <AlternatingRowStyle CssClass="altRow" />
                                                                                                <EmptyDataRowStyle CssClass="HeadataRow" Font-Bold="true" />
                                                                                                <HeaderStyle CssClass="HeadataRow" Wrap="false" />
                                                                                                <FooterStyle CssClass="dataRow" />
                                                                                                <Columns>
                                                                                                    <asp:BoundField DataField="Area" HeaderText="Area" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                                                                    <asp:BoundField DataField="Section" HeaderText="Section" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                                                                    <asp:BoundField DataField="RoleDesc" HeaderText="Screen Name" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                                                                    <asp:BoundField DataField="Role" HeaderText="Roles" HeaderStyle-Wrap="false" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" />
                                                                                                    <asp:TemplateField Visible="false">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblDebtorID" runat="server" Text='<%# Eval("OrderNo")%>' />
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="Add" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxAdd" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Add") %>'></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="Edit" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxEdit" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Edit") %>'></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="Delete" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxDel" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Delete") %>'></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="View" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxView" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Views") %>'></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                </Columns>
                                                                                            </rwg:BulkEditGridView>
                                                                                        </ContentTemplate>
                                                                                    </cc1:TabPanel>
                                                                                    <cc1:TabPanel ID="TabRESOURCE" runat="server" HeaderText="Resource">
                                                                                        <ContentTemplate>
                                                                                            <rwg:BulkEditGridView ID="GridRESOURCE" AutoGenerateColumns="False" BorderWidth="1px"
                                                                                                BorderStyle="Solid" GridLines="Both" SaveButtonID="SaveButton" runat="server" CssClass="someClass"
                                                                                                Width="1057px">
                                                                                                <RowStyle CssClass="dataRow" />
                                                                                                <SelectedRowStyle CssClass="SelectdataRow" />
                                                                                                <AlternatingRowStyle CssClass="altRow" />
                                                                                                <EmptyDataRowStyle CssClass="HeadataRow" Font-Bold="true" />
                                                                                                <HeaderStyle CssClass="HeadataRow" Wrap="false" />
                                                                                                <FooterStyle CssClass="dataRow" />
                                                                                                <Columns>
                                                                                                    <asp:BoundField DataField="Area" HeaderText="Area" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                                                                    <asp:BoundField DataField="Section" HeaderText="Section" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                                                                    <asp:BoundField DataField="RoleDesc" HeaderText="Screen Name" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                                                                    <asp:BoundField DataField="Role" HeaderText="Roles" HeaderStyle-Wrap="false" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" />
                                                                                                    <asp:TemplateField Visible="false">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblDebtorID" runat="server" Text='<%# Eval("OrderNo")%>' />
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="Add" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxAdd" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Add") %>'></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="Edit" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxEdit" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Edit") %>'></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="Delete" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxDel" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Delete") %>'></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="View" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxView" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Views") %>'></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                </Columns>
                                                                                            </rwg:BulkEditGridView>
                                                                                        </ContentTemplate>
                                                                                    </cc1:TabPanel>
                                                                                    <cc1:TabPanel ID="TabSERVICE" runat="server" HeaderText="Service">
                                                                                        <ContentTemplate>
                                                                                            <rwg:BulkEditGridView ID="GridSERVICE" AutoGenerateColumns="False" BorderWidth="1px"
                                                                                                BorderStyle="Solid" GridLines="Both" SaveButtonID="SaveButton" runat="server" CssClass="someClass"
                                                                                                Width="1057px">
                                                                                                <RowStyle CssClass="dataRow" />
                                                                                                <SelectedRowStyle CssClass="SelectdataRow" />
                                                                                                <AlternatingRowStyle CssClass="altRow" />
                                                                                                <EmptyDataRowStyle CssClass="HeadataRow" Font-Bold="true" />
                                                                                                <HeaderStyle CssClass="HeadataRow" Wrap="false" />
                                                                                                <FooterStyle CssClass="dataRow" />
                                                                                                <Columns>
                                                                                                    <asp:BoundField DataField="Area" HeaderText="Area" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                                                                    <asp:BoundField DataField="Section" HeaderText="Section" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                                                                    <asp:BoundField DataField="RoleDesc" HeaderText="Screen Name" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                                                                    <asp:BoundField DataField="Role" HeaderText="Roles" HeaderStyle-Wrap="false" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" />
                                                                                                    <asp:TemplateField Visible="false">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblDebtorID" runat="server" Text='<%# Eval("OrderNo")%>' />
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="Add" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxAdd" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Add") %>'></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="Edit" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxEdit" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Edit") %>'></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="Delete" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxDel" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Delete") %>'></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="View" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxView" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Views") %>'></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                </Columns>
                                                                                            </rwg:BulkEditGridView>
                                                                                        </ContentTemplate>
                                                                                    </cc1:TabPanel>
                                                                                    <cc1:TabPanel ID="TabOTHER" runat="server" HeaderText="Other">
                                                                                        <ContentTemplate>
                                                                                            <rwg:BulkEditGridView ID="GridOTHER" AutoGenerateColumns="False" BorderWidth="1px"
                                                                                                BorderStyle="Solid" GridLines="Both" SaveButtonID="SaveButton" runat="server" CssClass="someClass"
                                                                                                Width="1057px">
                                                                                                <RowStyle CssClass="dataRow" />
                                                                                                <SelectedRowStyle CssClass="SelectdataRow" />
                                                                                                <AlternatingRowStyle CssClass="altRow" />
                                                                                                <EmptyDataRowStyle CssClass="HeadataRow" Font-Bold="true" />
                                                                                                <HeaderStyle CssClass="HeadataRow" Wrap="false" />
                                                                                                <FooterStyle CssClass="dataRow" />
                                                                                                <Columns>
                                                                                                    <asp:BoundField DataField="Area" HeaderText="Area" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                                                                    <asp:BoundField DataField="Section" HeaderText="Section" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                                                                    <asp:BoundField DataField="RoleDesc" HeaderText="Screen Name" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                                                                    <asp:BoundField DataField="Role" HeaderText="Roles" HeaderStyle-Wrap="false" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" />
                                                                                                    <asp:TemplateField Visible="false">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblDebtorID" runat="server" Text='<%# Eval("OrderNo")%>' />
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="Add" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxAdd" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Add") %>'></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="Edit" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxEdit" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Edit") %>'></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="Delete" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxDel" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Delete") %>'></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="View" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxView" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Views") %>'></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                </Columns>
                                                                                            </rwg:BulkEditGridView>
                                                                                        </ContentTemplate>
                                                                                    </cc1:TabPanel>
                                                                                    <cc1:TabPanel ID="TabREPORT" runat="server" HeaderText="Report">
                                                                                        <ContentTemplate>
                                                                                            <rwg:BulkEditGridView ID="GridREPORT" AutoGenerateColumns="False" BorderWidth="1px"
                                                                                                BorderStyle="Solid" GridLines="Both" SaveButtonID="SaveButton" runat="server" CssClass="someClass"
                                                                                                Width="1057px">
                                                                                                <RowStyle CssClass="dataRow" />
                                                                                                <SelectedRowStyle CssClass="SelectdataRow" />
                                                                                                <AlternatingRowStyle CssClass="altRow" />
                                                                                                <EmptyDataRowStyle CssClass="HeadataRow" Font-Bold="true" />
                                                                                                <HeaderStyle CssClass="HeadataRow" Wrap="false" />
                                                                                                <FooterStyle CssClass="dataRow" />
                                                                                                <Columns>
                                                                                                    <asp:BoundField DataField="Area" HeaderText="Area" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                                                                    <asp:BoundField DataField="Section" HeaderText="Section" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                                                                    <asp:BoundField DataField="RoleDesc" HeaderText="Screen Name" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                                                                    <asp:BoundField DataField="Role" HeaderText="Roles" HeaderStyle-Wrap="false" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" />
                                                                                                    <asp:TemplateField Visible="false">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblDebtorID" runat="server" Text='<%# Eval("OrderNo")%>' />
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="Add" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxAdd" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Add") %>' Enabled="False"></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="Edit" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxEdit" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Edit") %>' Enabled="False"></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="Delete" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxDel" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Delete") %>' Enabled="False"></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="View" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxView" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Views") %>'></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                </Columns>
                                                                                            </rwg:BulkEditGridView>
                                                                                        </ContentTemplate>
                                                                                    </cc1:TabPanel>
                                                                                    <cc1:TabPanel ID="TabSECURITY" runat="server" HeaderText="Security">
                                                                                        <ContentTemplate>
                                                                                            <rwg:BulkEditGridView ID="GridSECURITY" AutoGenerateColumns="False" BorderWidth="1px"
                                                                                                BorderStyle="Solid" GridLines="Both" SaveButtonID="SaveButton" runat="server" CssClass="someClass"
                                                                                                Width="1057px">
                                                                                                <RowStyle CssClass="dataRow" />
                                                                                                <SelectedRowStyle CssClass="SelectdataRow" />
                                                                                                <AlternatingRowStyle CssClass="altRow" />
                                                                                                <EmptyDataRowStyle CssClass="HeadataRow" Font-Bold="true" />
                                                                                                <HeaderStyle CssClass="HeadataRow" Wrap="false" />
                                                                                                <FooterStyle CssClass="dataRow" />
                                                                                                <Columns>
                                                                                                    <asp:BoundField DataField="Area" HeaderText="Area" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                                                                    <asp:BoundField DataField="Section" HeaderText="Section" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                                                                    <asp:BoundField DataField="RoleDesc" HeaderText="Screen Name" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                                                                    <asp:BoundField DataField="Role" HeaderText="Roles" HeaderStyle-Wrap="false" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" />
                                                                                                    <asp:TemplateField Visible="false">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblDebtorID" runat="server" Text='<%# Eval("OrderNo")%>' />
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="Add" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxAdd" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Add") %>'></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="Edit" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxEdit" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Edit") %>'></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="Delete" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxDel" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Delete") %>'></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="View" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxView" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Views") %>'></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                </Columns>
                                                                                            </rwg:BulkEditGridView>
                                                                                        </ContentTemplate>
                                                                                    </cc1:TabPanel>


                                                                                        <cc1:TabPanel ID="tabmanufacture" runat="server" HeaderText="Manufacture">
                                                                                        <ContentTemplate>
                                                                                            <rwg:BulkEditGridView ID="GridMANUFACTURE" AutoGenerateColumns="False" BorderWidth="1px"
                                                                                                BorderStyle="Solid" GridLines="Both" SaveButtonID="SaveButton" runat="server" CssClass="someClass"
                                                                                                Width="1057px">
                                                                                                <RowStyle CssClass="dataRow" />
                                                                                                <SelectedRowStyle CssClass="SelectdataRow" />
                                                                                                <AlternatingRowStyle CssClass="altRow" />
                                                                                                <EmptyDataRowStyle CssClass="HeadataRow" Font-Bold="true" />
                                                                                                <HeaderStyle CssClass="HeadataRow" Wrap="false" />
                                                                                                <FooterStyle CssClass="dataRow" />
                                                                                                <Columns>
                                                                                                    <asp:BoundField DataField="Area" HeaderText="Area" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                                                                    <asp:BoundField DataField="Section" HeaderText="Section" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                                                                    <asp:BoundField DataField="RoleDesc" HeaderText="Screen Name" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                                                                    <asp:BoundField DataField="Role" HeaderText="Roles" HeaderStyle-Wrap="false" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" />
                                                                                                    <asp:TemplateField Visible="false">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblDebtorID" runat="server" Text='<%# Eval("OrderNo")%>' />
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="Add" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxAdd" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Add") %>'></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="Edit" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxEdit" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Edit") %>'></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="Delete" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxDel" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Delete") %>'></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="View" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxView" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Views") %>' Enabled="false"></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                </Columns>
                                                                                            </rwg:BulkEditGridView>
                                                                                        </ContentTemplate>
                                                                                    </cc1:TabPanel>

                                                                                      <cc1:TabPanel ID="TabLead" runat="server" HeaderText="Lead">
                                                                                        <ContentTemplate>
                                                                                            <rwg:BulkEditGridView ID="GridLead" AutoGenerateColumns="False" BorderWidth="1px"
                                                                                                BorderStyle="Solid" GridLines="Both" SaveButtonID="SaveButton" runat="server" CssClass="someClass"
                                                                                                Width="1057px">
                                                                                                <RowStyle CssClass="dataRow" />
                                                                                                <SelectedRowStyle CssClass="SelectdataRow" />
                                                                                                <AlternatingRowStyle CssClass="altRow" />
                                                                                                <EmptyDataRowStyle CssClass="HeadataRow" Font-Bold="true" />
                                                                                                <HeaderStyle CssClass="HeadataRow" Wrap="false" />
                                                                                                <FooterStyle CssClass="dataRow" />
                                                                                                <Columns>
                                                                                                    <asp:BoundField DataField="Area" HeaderText="Area" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                                                                    <asp:BoundField DataField="Section" HeaderText="Section" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                                                                    <asp:BoundField DataField="RoleDesc" HeaderText="Screen Name" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                                                                    <asp:BoundField DataField="Role" HeaderText="Roles" HeaderStyle-Wrap="false" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" />
                                                                                                    <asp:TemplateField Visible="false">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblDebtorID" runat="server" Text='<%# Eval("OrderNo")%>' />
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="Add" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxAdd" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Add") %>'></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="Edit" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxEdit" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Edit") %>'></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="Delete" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxDel" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Delete") %>'></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="View" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxView" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Views") %>' Enabled="false"></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                </Columns>
                                                                                            </rwg:BulkEditGridView>
                                                                                        </ContentTemplate>
                                                                                    </cc1:TabPanel>



                                                                                    <cc1:TabPanel ID="TabProject" runat="server" HeaderText="Project">
                                                                                        <ContentTemplate>
                                                                                            <rwg:BulkEditGridView ID="GridPROJECT" AutoGenerateColumns="False" BorderWidth="1px"
                                                                                                BorderStyle="Solid" GridLines="Both" SaveButtonID="SaveButton" runat="server" CssClass="someClass"
                                                                                                Width="1057px">
                                                                                                <RowStyle CssClass="dataRow" />
                                                                                                <SelectedRowStyle CssClass="SelectdataRow" />
                                                                                                <AlternatingRowStyle CssClass="altRow" />
                                                                                                <EmptyDataRowStyle CssClass="HeadataRow" Font-Bold="true" />
                                                                                                <HeaderStyle CssClass="HeadataRow" Wrap="false" />
                                                                                                <FooterStyle CssClass="dataRow" />
                                                                                                <Columns>
                                                                                                    <asp:BoundField DataField="Area" HeaderText="Area" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                                                                    <asp:BoundField DataField="Section" HeaderText="Section" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                                                                    <asp:BoundField DataField="RoleDesc" HeaderText="Screen Name" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                                                                    <asp:BoundField DataField="Role" HeaderText="Roles" HeaderStyle-Wrap="false" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" />
                                                                                                    <asp:TemplateField Visible="false">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblDebtorID" runat="server" Text='<%# Eval("OrderNo")%>' />
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="Add" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxAdd" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Add") %>'></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="Edit" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxEdit" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Edit") %>'></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="Delete" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxDel" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Delete") %>'></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="View" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxView" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Views") %>' Enabled="false"></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                </Columns>
                                                                                            </rwg:BulkEditGridView>
                                                                                        </ContentTemplate>
                                                                                    </cc1:TabPanel>

                                                                                    <cc1:TabPanel ID="TabConfif" runat="server" HeaderText="Config">
                                                                                        <ContentTemplate>
                                                                                            <rwg:BulkEditGridView ID="GridConfig" AutoGenerateColumns="False" BorderWidth="1px"
                                                                                                BorderStyle="Solid" GridLines="Both" SaveButtonID="SaveButton" runat="server" CssClass="someClass"
                                                                                                Width="1057px">
                                                                                                <RowStyle CssClass="dataRow" />
                                                                                                <SelectedRowStyle CssClass="SelectdataRow" />
                                                                                                <AlternatingRowStyle CssClass="altRow" />
                                                                                                <EmptyDataRowStyle CssClass="HeadataRow" Font-Bold="true" />
                                                                                                <HeaderStyle CssClass="HeadataRow" Wrap="false" />
                                                                                                <FooterStyle CssClass="dataRow" />
                                                                                                <Columns>
                                                                                                    <asp:BoundField DataField="Area" HeaderText="Area" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                                                                    <asp:BoundField DataField="Section" HeaderText="Section" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                                                                    <asp:BoundField DataField="RoleDesc" HeaderText="Screen Name" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                                                                    <asp:BoundField DataField="Role" HeaderText="Roles" HeaderStyle-Wrap="false" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="Gray" />
                                                                                                    <asp:TemplateField Visible="false">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblDebtorID" runat="server" Text='<%# Eval("OrderNo")%>' />
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="Add" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxAdd" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Add") %>'></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="Edit" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxEdit" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Edit") %>' Enabled="False"></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="Delete" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxDel" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Delete") %>' Enabled="False"></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="View" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxView" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Views") %>' Enabled="False"></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                </Columns>
                                                                                            </rwg:BulkEditGridView>
                                                                                        </ContentTemplate>
                                                                                    </cc1:TabPanel>
                                                                                </cc1:TabContainer>
                                                                            </div>
                                                                        </td>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4">
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td style="width: 34%"></td>
                                                                        <td style="width: 16%">
                                                                            <asp:Button ID="cmdSave" runat="server" Text="" CssClass="savebutton1231"
                                                                                EnableTheming="false" OnClick="cmdSave_Click" SkinID="skinBtnSave" ValidationGroup="product" />
                                                                        </td>
                                                                        <td style="width: 16%">
                                                                            <asp:Button ID="btnCancel" runat="server" Text="" CssClass="cancelbutton6" EnableTheming="false"
                                                                                Visible="true" OnClick="cmdCancel_Click" SkinID="skinBtnCancel" CausesValidation="False" />
                                                                        </td>
                                                                        <td style="width: 34%">
                                                                            <asp:HiddenField ID="hdSMSRequired" runat="server" Value="NO" />
                                                                            <asp:HiddenField ID="hdEmailRequired" runat="server" Value="NO" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>


                    </td>
                </tr>
                <tr style="text-align: left">
                    <td width="100%">
                        <table width="100%" style="margin: -6px 0px 0px 0px;">
                            <tr style="width: 100%">
                                <td>
                                    <div class="mainGridHold" id="searchGrid" align="center">
                                        <asp:GridView ID="GrdViewCust" DataKeyNames="username" GridLines="Both" Width="100.3%" OnRowDataBound="GrdViewCust_RowDataBound"
                                            runat="server" AutoGenerateColumns="False" DataSourceID="GridSource" OnRowCreated="GrdViewCust_RowCreated" OnSelectedIndexChanged="GrdViewCust_SelectedIndexChanged"
                                            AllowPaging="true" CssClass="someClass" OnRowDeleting="GrdViewCust_RowDeleting">
                                            <RowStyle BorderWidth="1" HorizontalAlign="Center" />
                                            <HeaderStyle Height="30px" HorizontalAlign="Center" Font-Bold="true" BackColor="#cccccc" BorderColor="Gray" Font-Size="Small" />
                                            <RowStyle Font-Bold="true" HorizontalAlign="Center" Height="30px" Font-Size="Small" ForeColor="#0567AE" />
                                            <Columns>
                                                <asp:BoundField DataField="username" HeaderText="User" HeaderStyle-BorderColor="Gray"></asp:BoundField>
                                                <asp:BoundField DataField="Email" HeaderText="Email" HeaderStyle-BorderColor="Gray"></asp:BoundField>
                                                <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="50px" HeaderText="Edit" HeaderStyle-BorderColor="Gray">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Select" />
                                                        <asp:ImageButton ID="btnEditDisabled" Enabled="false" SkinID="editDisable" runat="Server"></asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-BorderColor="Gray"
                                                    HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" OnClientClick="return confirm('Are you sure you want to delete the User?');"
                                                            CommandName="Delete"></asp:ImageButton>
                                                        <asp:ImageButton ID="lnkBDisabled" Enabled="false" SkinID="deleteDisable" runat="Server"></asp:ImageButton>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderStyle-BorderColor="Gray">
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerTemplate>
                                                <table style="border-color: white">
                                                    <tr style="border-color: white">
                                                        <td style="border-color: white">Goto Page
                                                        </td>
                                                        <td style="border-color: white">
                                                            <asp:DropDownList ID="ddlPageSelector" runat="server" AutoPostBack="true" Width="65px" Style="border: 1px solid blue" BackColor="#e7e7e7" Height="23px">
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
                                    <asp:ObjectDataSource ID="GridSource" runat="server" DeleteMethod="DeleteUserOptions" SelectMethod="ListUsers"
                                        TypeName="BusinessLogic" OnDeleting="GridSource_Deleting">
                                        <DeleteParameters>
                                            <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                            <asp:ControlParameter ControlID="GrdViewCust" Name="username" Type="String" />
                                        </DeleteParameters>
                                    </asp:ObjectDataSource>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
