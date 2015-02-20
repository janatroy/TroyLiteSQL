<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="EmailSMSConfig.aspx.cs" Inherits="EmailSMSConfig" Title="Administration > Email/SMS Config" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <script type="text/javascript">

        function pageLoad() {
            //  get the behavior associated with the tab control
            var tabContainer = $find('ctl00_cplhControlPanel_tabs2');
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
    
    </script>

       <style id="Style1" runat="server">
        
        
        .fancy-green .ajax__tab_header
        {
	        background: url(App_Themes/NewTheme/Images/green_bg_Tab.gif) repeat-x;
	        cursor:pointer;
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
	        border:1px solid #999999;
	        padding: 8px;
	        background-color: #ffffff;
        }
        
    </style>

    <table style="width: 100%">
        <tr style="width: 100%">
            <td style="width: 100%;">
                <%--<table style="width: 100%">
                    <tr style="width: 100%">
                        <td style="width: 100%">
                            <%--<table class="mainConHd" style="width: 994px;">
                                <tr valign="middle">
                                    <td style="font-size: 20px;">
                                        Basic Settings
                                    </td>
                                </tr>
                                <tr valign="middle">

                                </tr>
                            </table>--%>
                            <%--<table class="mainConHd" style="width: 994px;">
                                <tr valign="middle">
                                    <td style="font-size: 20px;">
                                        Basic Settings
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>--%>
                <%--<table class="mainConHd" style="width: 994px;">
                    <tr valign="middle">
                        <td style="font-size: 20px;">
                            Basic Settings
                        </td>
                    </tr>
                </table>--%>
                <div class="mainConBody">
                    <table style="width: 99.8%;margin: 0px 0px 0px 0px;" cellpadding="2px" cellspacing="2px" class="searchbg">
                        <tr>
                            <td style="width: 1%">
                            </td>
                            <td style="width: 25%; font-size: 22px; color: White;" >
                                 Email / SMS Config
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
                <table style="text-align: left; border: 0px solid #5078B3; margin: 0px 0px 0px 0px; padding-left:3px; width: 990px" cellpadding="0"
                    cellspacing="0">
                    <tr>
                        <td>
                            <div align="center" style="width: 990px; margin: 0px 0px 0px 0px; text-align: left">
                                <cc1:TabContainer ID="tabs2" runat="server" ActiveTabIndex="0" CssClass="fancy fancy-green">
                                    <cc1:TabPanel ID="tabMaster" runat="server" HeaderText="Screen Master">
                                        <ContentTemplate>
                                            <div style="width: 100%" align="center">
                                                <table style="width: 977px; border: 0px solid #5078B3;" align="center" cellpadding="3"
                                                    cellspacing="3">
                                                    <tr>
                                                        <td>
                                                            <div style="width: 100%" align="center">
                                                                
                                                                <asp:GridView ID="GrdScreen" runat="server" CssClass="someClass" DataSourceID="srcGridView"
                                                                    AutoGenerateColumns="False" OnRowCreated="GrdScreen_RowCreated" Width="98%"
                                                                    PageSize="5" EmptyDataText="No Screens Found" Style="font-family: 'Trebuchet MS';
                                                                    font-size: 11px;" OnRowCommand="GrdScreen_RowCommand" AllowPaging="True" DataKeyNames="ScreenID"
                                                                    EnableViewState="False" OnRowUpdating="GrdScreen_RowUpdating" OnRowUpdated="GrdScreen_RowUpdated">
                                                                      <HeaderStyle Height="30px" HorizontalAlign="Center" Font-Bold="true" BackColor="#cccccc" BorderColor="Gray" Font-Size="Small" />
                                                                       <RowStyle Font-Bold="true" HorizontalAlign="Center" Height="30px" Font-Size="Small" ForeColor="#0567AE" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Screen No" HeaderStyle-BorderColor="Gray">
                                                                            <ItemStyle Width="5%" HorizontalAlign="Left" />
                                                                            <FooterStyle Width="5%" HorizontalAlign="Left" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblScreenNo" runat="server" Text='<%# Bind("ScreenNo") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <asp:TextBox ID="txtScreenNo" runat="server" Text='<%# Bind("ScreenNo") %>' CssClass="cssTextBox"
                                                                                    Width="5%"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="rvDescr" runat="server" ControlToValidate="txtScreenNo"
                                                                                    Display="Dynamic" EnableClientScript="False" ErrorMessage="ScreenNo is mandatory">*</asp:RequiredFieldValidator>
                                                                            </EditItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:TextBox ID="txtAddScreenNo" runat="server" CssClass="cssTextBox" Width="15%"></asp:TextBox><asp:RequiredFieldValidator
                                                                                    ID="rvAdd" runat="server" ControlToValidate="txtAddScreenNo" Display="Dynamic"
                                                                                    EnableClientScript="true" ErrorMessage="ScreenNo is mandatory">*</asp:RequiredFieldValidator>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Screen Name" HeaderStyle-BorderColor="Gray">
                                                                            <ItemStyle Width="25%" HorizontalAlign="Left" />
                                                                            <FooterStyle Width="25%" HorizontalAlign="Left" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblScreenName" runat="server" Text='<%# Bind("ScreenName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <asp:TextBox ID="txtScreenName" runat="server" Text='<%# Bind("ScreenName") %>' CssClass="cssTextBox"
                                                                                    Width="25%"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="rvD" runat="server" ControlToValidate="txtScreenName"
                                                                                    Display="Dynamic" EnableClientScript="False" ErrorMessage="ScreenName is mandatory">*</asp:RequiredFieldValidator>
                                                                            </EditItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:TextBox ID="txtAddScreenName" runat="server" CssClass="cssTextBox" Width="25%"></asp:TextBox><asp:RequiredFieldValidator
                                                                                    ID="rvAddDescr" runat="server" ControlToValidate="txtAddScreenName" Display="Dynamic"
                                                                                    EnableClientScript="true" ErrorMessage="ScreenName is mandatory">*</asp:RequiredFieldValidator>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Subject" HeaderStyle-BorderColor="Gray">
                                                                            <ItemStyle Width="25%" HorizontalAlign="Left" />
                                                                            <FooterStyle Width="25%" HorizontalAlign="Left" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSubject" runat="server" Text='<%# Bind("Subject") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <asp:TextBox ID="txtSubject" runat="server" Text='<%# Bind("Subject") %>' CssClass="cssTextBox"
                                                                                    Width="25%"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="rvDes" runat="server" ControlToValidate="txtSubject"
                                                                                    Display="Dynamic" EnableClientScript="False" ErrorMessage="Subject is mandatory">*</asp:RequiredFieldValidator>
                                                                            </EditItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:TextBox ID="txtAddSubject" runat="server" CssClass="cssTextBox" Width="25%"></asp:TextBox><asp:RequiredFieldValidator
                                                                                    ID="rvAd" runat="server" ControlToValidate="txtAddSubject" Display="Dynamic"
                                                                                    EnableClientScript="true" ErrorMessage="Subject is mandatory">*</asp:RequiredFieldValidator>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Content" HeaderStyle-BorderColor="Gray">
                                                                            <ItemStyle Width="50%" HorizontalAlign="Left" />
                                                                            <FooterStyle Width="50%" HorizontalAlign="Left" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblContent" runat="server" Text='<%# Bind("Content") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <asp:TextBox ID="txtContent" runat="server" Text='<%# Bind("Content") %>' CssClass="cssTextBox"
                                                                                    Width="50%"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="rvDes1" runat="server" ControlToValidate="txtContent"
                                                                                    Display="Dynamic" EnableClientScript="False" ErrorMessage="Content is mandatory">*</asp:RequiredFieldValidator>
                                                                            </EditItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:TextBox ID="txtAddContent" runat="server" CssClass="cssTextBox" Width="50%"></asp:TextBox><asp:RequiredFieldValidator
                                                                                    ID="rvAd1" runat="server" ControlToValidate="txtAddContent" Display="Dynamic"
                                                                                    EnableClientScript="true" ErrorMessage="Content is mandatory">*</asp:RequiredFieldValidator>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Edit" HeaderStyle-BorderColor="Gray">
                                                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                                            <FooterStyle Width="10%" HorizontalAlign="Center" />
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Edit" />
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <asp:ImageButton ID="lbUpdate" runat="server" CausesValidation="True" CommandName="Update"
                                                                                    Text="Update" SkinID="GridUpdate"></asp:ImageButton>
                                                                                <asp:ImageButton ID="lbCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                    Text="Cancel" SkinID="GridCancel"></asp:ImageButton>
                                                                            </EditItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:ImageButton ID="lbInsert" runat="server" CommandName="Insert" Text="Save" SkinID="GridUpdate">
                                                                                </asp:ImageButton>
                                                                                <asp:ImageButton ID="lblInsCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                    Text="Cancel" SkinID="GridCancel"></asp:ImageButton>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <PagerTemplate>
                                                                        <table style="border-color: white">
                                                                            <tr style="border-color: white">
                                                                                <td style="border-color: white">
                                                                                    Goto Page
                                                                                </td>
                                                                                <td style="border-color: white">
                                                                                    <asp:DropDownList ID="ddlPageSelector" runat="server" AutoPostBack="true" SkinID="skinPagerDdlBox2" BackColor="#e7e7e7">
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
                                                                <asp:ObjectDataSource ID="srcGridView" runat="server" InsertMethod="InsertUnitRecord"
                                                                    SelectMethod="GetScreen" TypeName="BusinessLogic">
                                                                    <SelectParameters>
                                                                        <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                    </SelectParameters>
                                                                </asp:ObjectDataSource>
                                                                <asp:Button ID="lnkBtnAdd" runat="server" OnClick="lnkBtnAdd_Click" CssClass="ButtonAdd66"
                                                                    Text="" EnableTheming="false"></asp:Button>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel ID="TabPanel4" runat="server" HeaderText="Email Config">
                                        <ContentTemplate>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                                                <ContentTemplate>
                                                    <div style="text-align: left;">
                                                        <table style="width: 977px" cellpadding="1" cellspacing="1">
                                                            <tr>
                                                                <td style="text-align: left; width: 977px">
                                                                    <asp:ValidationSummary ID="Save" runat="server" Font-Names="'Trebuchet MS'" Font-Size="12pt"
                                                                    HeaderText="Validation Messages" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Save" />
                                                                    <asp:Panel ID="pnlDivsion" runat="server" Visible="false">
                                                                        <div style="text-align: left; width: 977px">
                                                                            <table style="width: 977px; border: 0px solid #86b2d1" cellpadding="1" cellspacing="1">
                                                                                <tr style="height: 5px">
                                                                                </tr>
                                                                                <tr style="height: 20px">
                                                                                    <td style="width: 25%">
                                                                                    </td>
                                                                                    <td style="width: 20%" class="ControlLabel">
                                                                                        <asp:RequiredFieldValidator ValidationGroup="Save" ID="rq" runat="server" ErrorMessage="Screen No is mandatory"
                                                                                            Text="*" ControlToValidate="txtScreenNumber"></asp:RequiredFieldValidator>
                                                                                        Screen No *
                                                                                    </td>
                                                                                    <td style="width: 20%" class="ControlTextBox3">
                                                                                        <asp:TextBox ID="txtScreenNumber" MaxLength="50" runat="server" SkinID="skinTxtBox" TabIndex="1"></asp:TextBox>
                                                                                    </td>
                                                                                    <td style="width: 35%">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 25%">
                                                                                    </td>
                                                                                    <td style="width: 20%" class="ControlLabel">
                                                                                        <asp:RequiredFieldValidator ValidationGroup="Save" ID="RequiredFieldValidator1" runat="server" ErrorMessage="Email is mandatory"
                                                                                            Text="*" ControlToValidate="txtEmail"></asp:RequiredFieldValidator>
                                                                                        <asp:CompareValidator ID="cvModeOfContact" runat="server" ControlToValidate="cmbCustomer"
                                                                                                    ValidationGroup="Save" ErrorMessage="Please Select Email Id" Operator="GreaterThan"
                                                                                                    Text="*" ValueToCompare="0"></asp:CompareValidator>
                                                                                        Email Id *
                                                                                    </td>
                                                                                    <td style="width: 20%" class="ControlDrpBorder">
                                                                                        <asp:UpdatePanel ID="UpdatePanel21" runat="server" UpdateMode="Conditional">
                                                                                            <ContentTemplate>
                                                                                                <asp:DropDownList ID="cmbCustomer" runat="server" AppendDataBoundItems="true" AutoPostBack="true" BackColor="#e7e7e7" CssClass="drpDownListMedium" height="26px" style="border: 1px solid #e7e7e7" TabIndex="3" Width="100%">
                                                                                                    <asp:ListItem style="background-color: #e7e7e7" Text="Select" Value="0"></asp:ListItem>
                                                                                                    <asp:ListItem Text="Customer" Value="Customer"></asp:ListItem>
                                                                                                    <asp:ListItem Text="Vendor" Value="Vendor"></asp:ListItem>
                                                                                                    <asp:ListItem Text="Expense" Value="Expense"></asp:ListItem>
                                                                                                    <asp:ListItem Text="Bank" Value="Bank"></asp:ListItem>
                                                                                                    <asp:ListItem Text="Ledger" Value="Ledger"></asp:ListItem>
                                                                                                 </asp:DropDownList>
                                                                                               </ContentTemplate>
                                                                                           </asp:UpdatePanel>
                                                                                        <asp:TextBox ID="txtEmail" TabIndex="2" runat="server" SkinID="skinTxtBox" visible="false"/>
                                                                                    </td>
                                                                                    <td style="width: 30%">
                                                                                        <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
                                                                                             <ContentTemplate>
                                                                                                 <asp:CheckBox runat="server" ID="chk" OnCheckedChanged="chk_CheckedChanged" AutoPostBack="true"/>
                                                                                             </ContentTemplate>
                                                                                       </asp:UpdatePanel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 25%">
                                                                                    </td>
                                                                                    <td style="width: 20%" class="ControlLabel">
                                                                                        <asp:CompareValidator ID="CompareValidator213" runat="server" ControlToValidate="drpActive"
                                                                                            Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="Active is mandatory"
                                                                                            Operator="GreaterThan" ValueToCompare="0" />
                                                                                        Active *
                                                                                    </td>
                                                                                    <td style="width: 20%" class="ControlDrpBorder">
                                                                                        <asp:DropDownList ID="drpActive" TabIndex="3" AutoPostBack="false" runat="server" BackColor = "#e7e7e7" CssClass="drpDownListMedium"
                                                                                            Width="100%" style="border: 1px solid #e7e7e7" height="26px">
                                                                                            <asp:ListItem Text="YES" Value="YES" Selected="True"></asp:ListItem>
                                                                                            <asp:ListItem Text="NO" Value="NO"></asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td style="width: 30%">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr style="height: 10px">

                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 25%">
                                                                                    </td>
                                                                                    <td align="right" style="width: 20%">                                                                                       
                                                                                         <asp:Button ID="btnDivSave" ValidationGroup="Save" runat="server" SkinID="skinBtnSave"
                                                                                            CssClass="savebutton1231" EnableTheming="false" OnClick="btnDivSave_Click" />
                                                                                        <asp:Button ID="btnDivUpdate" runat="server" ValidationGroup="Save" CssClass="Updatebutton1231"
                                                                                            EnableTheming="false" OnClick="btnDivUpdate_Click" />
                                                                                    </td>
                                                                                    <td align="center" style="width: 20%">
                                                                                        <asp:Button ID="btnDivCancel" runat="server" SkinID="skinBtnCancel" CssClass="cancelbutton6"
                                                                                            EnableTheming="false" OnClick="btnDivCancel_Click" />
                                                                                    </td>
                                                                                    <td style="width: 30%">
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            
                                                            <tr style="width: 977px">
                                                                <td style="width: 977px">
                                                                    <div class="mainGridHold" id="searchGrid" style="width: 977px" align="center">
                                                                        <asp:GridView ID="GrdDiv" CssClass="someClass" runat="server" AllowSorting="True"
                                                                            AutoGenerateColumns="False" Width="90%" AllowPaging="True" OnPageIndexChanging="GrdDiv_PageIndexChanging"
                                                                            OnDataBound="GrdDiv_DataBound" OnRowCreated="GrdDiv_RowCreated" DataKeyNames="ID"
                                                                            PageSize="8" EmptyDataText="No Email Config found." OnSelectedIndexChanged="GrdDiv_SelectedIndexChanged"
                                                                            OnRowDeleting="GrdDiv_RowDeleting">
                                                                            <EmptyDataRowStyle CssClass="GrdHeaderbgClr" Font-Bold="true" Height="25px" />
                                                                              <HeaderStyle Height="30px" HorizontalAlign="Center" Font-Bold="true" BackColor="#cccccc" BorderColor="Gray" Font-Size="Small" />
                                                                               <RowStyle Font-Bold="true" HorizontalAlign="Center" Height="30px" Font-Size="Small" ForeColor="#0567AE" />
                                                                            <Columns>
                                                                                <asp:BoundField DataField="ScreenNo" HeaderText="Screen No" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="15%" />
                                                                                <asp:BoundField DataField="EmailID" HeaderText="Email ID" HeaderStyle-BorderColor="Gray"  HeaderStyle-Width="50%" />
                                                                                <asp:BoundField DataField="Active" HeaderText="Active" HeaderStyle-BorderColor="Gray"  HeaderStyle-Width="20%" />
                                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="command"
                                                                                    HeaderStyle-BorderColor="Gray" HeaderStyle-Width="15%" HeaderText="Edit">
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Select" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <%--<asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="command" HeaderText="Delete"
                                                                                    HeaderStyle-BorderColor="Gray" HeaderStyle-Width="35px">
                                                                                    <ItemTemplate>
                                                                                        <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to Delete this Email Config?"
                                                                                            runat="server">
                                                                                        </cc1:ConfirmButtonExtender>
                                                                                        <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" CommandName="Delete"></asp:ImageButton>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>--%>
                                                                            </Columns>
                                                                            <PagerTemplate>
                                                                                <table style="border-color: white">
                                                                                    <tr style="border-color: white">
                                                                                        <td style="border-color: white">
                                                                                            Goto Page
                                                                                        </td>
                                                                                        <td style="border-color: white">
                                                                                            <asp:DropDownList ID="ddlPageSelector" runat="server" AutoPostBack="true" SkinID="skinPagerDdlBox2" BackColor="#e7e7e7">
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
                                                            <tr>
                                                                <td style="width: 955px" align="center">
                                                                    <asp:Button ID="BtnAddDivision" runat="server" OnClick="BtnAddDivision_Click" CssClass="ButtonAdd66"
                                                                        Text="" EnableTheming="false"></asp:Button>
                                                                    <asp:HiddenField ID="hdDivision" runat="server" Value="0" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="SMS Config">
                                        <ContentTemplate>
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                                                <ContentTemplate>
                                                    <div style="text-align: left;">
                                                        <table style="width: 970px" cellpadding="1" cellspacing="1">
                                                            <tr>
                                                                <td style="text-align: left; width: 970px">
                                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" Font-Names="'Trebuchet MS'" Font-Size="12pt"
                                                                    HeaderText="Validation Messages" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Save1" />
                                                                    <asp:Panel ID="Panel1" runat="server" Visible="false">
                                                                        <div style="text-align: left; width: 977px">
                                                                            <table style="width: 970px; border: 0px solid #86b2d1" cellpadding="1" cellspacing="1">
                                                                                <tr style="height: 5px">
                                                                                </tr>
                                                                                <tr style="height: 20px">
                                                                                    <td style="width: 25%">
                                                                                    </td>
                                                                                    <td style="width: 20%" class="ControlLabel">
                                                                                        <asp:RequiredFieldValidator ValidationGroup="Save1" ID="RequiredFieldValidator2" runat="server" ErrorMessage="Screen No is mandatory"
                                                                                            Text="*" ControlToValidate="TextBox1"></asp:RequiredFieldValidator>
                                                                                        Screen No *
                                                                                    </td>
                                                                                    <td style="width: 20%" class="ControlTextBox3">
                                                                                        <asp:TextBox ID="TextBox1" MaxLength="50" runat="server" SkinID="skinTxtBox" TabIndex="1"></asp:TextBox>
                                                                                    </td>
                                                                                    <td style="width: 35%">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 25%">
                                                                                    </td>
                                                                                    <td style="width: 20%" class="ControlLabel">
                                                                                        <asp:RequiredFieldValidator ValidationGroup="Save1" ID="RequiredFieldValidator3" runat="server" ErrorMessage="Email is mandatory"
                                                                                            Text="*" ControlToValidate="TextBox2"></asp:RequiredFieldValidator>
                                                                                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="DropDownList2"
                                                                                                    ValidationGroup="Save1" ErrorMessage="Please Select Email Id" Operator="GreaterThan"
                                                                                                    Text="*" ValueToCompare="0"></asp:CompareValidator>
                                                                                        Email Id *
                                                                                    </td>
                                                                                    <td style="width: 20%" class="ControlDrpBorder">
                                                                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                                                                            <ContentTemplate>
                                                                                                <asp:DropDownList ID="DropDownList2" runat="server" AppendDataBoundItems="true" AutoPostBack="true" BackColor="#e7e7e7" CssClass="drpDownListMedium" height="26px" style="border: 1px solid #e7e7e7" TabIndex="2" Width="100%">
                                                                                                    <asp:ListItem style="background-color: #e7e7e7" Text="Select" Value="0"></asp:ListItem>
                                                                                                    <asp:ListItem Text="Customer" Value="Customer"></asp:ListItem>
                                                                                                    <asp:ListItem Text="Vendor" Value="Vendor"></asp:ListItem>
                                                                                                    <asp:ListItem Text="Expense" Value="Expense"></asp:ListItem>
                                                                                                    <asp:ListItem Text="Bank" Value="Bank"></asp:ListItem>
                                                                                                    <asp:ListItem Text="Ledger" Value="Ledger"></asp:ListItem>
                                                                                                 </asp:DropDownList>
                                                                                               </ContentTemplate>
                                                                                           </asp:UpdatePanel>
                                                                                        <asp:TextBox ID="TextBox2" TabIndex="2" MaxLength="10" runat="server" SkinID="skinTxtBox" Visible="false" />
                                                                                    </td>
                                                                                    <td style="width: 30%">
                                                                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                                                                             <ContentTemplate>
                                                                                                 <asp:CheckBox runat="server" ID="CheckBox1" OnCheckedChanged="CheckBox1_CheckedChanged" AutoPostBack="true"/>
                                                                                             </ContentTemplate>
                                                                                       </asp:UpdatePanel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 25%">
                                                                                    </td>
                                                                                    <td style="width: 20%" class="ControlLabel">
                                                                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="DropDownList1"
                                                                                            Text="*" ValidationGroup="Save1" EnableClientScript="True" ErrorMessage="Active is mandatory"
                                                                                            Operator="GreaterThan" ValueToCompare="0" />
                                                                                        Active *
                                                                                    </td>
                                                                                    <td style="width: 20%" class="ControlDrpBorder">
                                                                                        <asp:DropDownList ID="DropDownList1" TabIndex="3" AutoPostBack="false" runat="server" BackColor = "#e7e7e7" CssClass="drpDownListMedium"
                                                                                            Width="100%" style="border: 1px solid #e7e7e7" height="26px">
                                                                                            <asp:ListItem Text="YES" Value="YES" Selected="True"></asp:ListItem>
                                                                                            <asp:ListItem Text="NO" Value="NO"></asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td style="width: 30%">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr style="height: 10px">

                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 25%">
                                                                                    </td>
                                                                                    <td align="right" style="width: 20%">                                                                                        
                                                                                        <asp:Button ID="btnConfigSave" ValidationGroup="Save1" runat="server" SkinID="skinBtnSave"
                                                                                            CssClass="savebutton1231" EnableTheming="false" OnClick="btnConfigSave_Click" />
                                                                                        <asp:Button ID="btnConfigUpdate" runat="server" ValidationGroup="Save1" CssClass="Updatebutton1231"
                                                                                            EnableTheming="false" OnClick="btnConfigUpdate_Click" />
                                                                                    </td>
                                                                                    <td align="center" style="width: 20%">
                                                                                        <asp:Button ID="btnConfigCancel" runat="server" SkinID="skinBtnCancel" CssClass="cancelbutton6"
                                                                                            EnableTheming="false" OnClick="btnConfigCancel_Click" />
                                                                                    </td>
                                                                                    <td style="width: 30%">
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            
                                                            <tr style="width: 977px">
                                                                <td style="width: 977px">
                                                                    <div class="mainGridHold" id="searchGrid1" style="width: 977px" align="center">
                                                                        <asp:GridView ID="GridView1" CssClass="someClass" runat="server" AllowSorting="True"
                                                                            AutoGenerateColumns="False" Width="90%" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging"
                                                                            OnDataBound="GridView1_DataBound" OnRowCreated="GridView1_RowCreated" DataKeyNames="ID"
                                                                            PageSize="8" EmptyDataText="No SMS Config found." OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                                                                            OnRowDeleting="GridView1_RowDeleting">
                                                                            <EmptyDataRowStyle CssClass="GrdHeaderbgClr" Font-Bold="true" Height="25px" />
                                                                              <HeaderStyle Height="30px" HorizontalAlign="Center" Font-Bold="true" BackColor="#cccccc" BorderColor="Gray" Font-Size="Small" />
                                                                             <RowStyle Font-Bold="true" HorizontalAlign="Center" Height="30px" Font-Size="Small" ForeColor="#0567AE" />
                                                                            <Columns>
                                                                                <asp:BoundField DataField="ScreenNo" HeaderText="Screen No" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="15%" />
                                                                                <asp:BoundField DataField="EmailID" HeaderText="Email ID" HeaderStyle-BorderColor="Gray"  HeaderStyle-Width="50%" />
                                                                                <asp:BoundField DataField="Active" HeaderText="Active" HeaderStyle-BorderColor="Gray"  HeaderStyle-Width="20%" />
                                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="command"
                                                                                    HeaderStyle-BorderColor="Gray" HeaderStyle-Width="15%" HeaderText="Edit">
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Select" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <%--<asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="command" HeaderText="Delete"
                                                                                    HeaderStyle-BorderColor="Gray" HeaderStyle-Width="35px">
                                                                                    <ItemTemplate>
                                                                                        <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to Delete this Email Config?"
                                                                                            runat="server">
                                                                                        </cc1:ConfirmButtonExtender>
                                                                                        <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" CommandName="Delete"></asp:ImageButton>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>--%>
                                                                            </Columns>
                                                                            <PagerTemplate>
                                                                                <table style="border-color: white">
                                                                                    <tr style="border-color: white">
                                                                                        <td style="border-color: white">
                                                                                            Goto Page
                                                                                        </td>
                                                                                        <td style="border-color: white">
                                                                                            <asp:DropDownList ID="ddlPageSelector" runat="server" AutoPostBack="true" SkinID="skinPagerDdlBox2" BackColor="#e7e7e7">
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
                                                            <tr>
                                                                <td style="width: 955px" align="center">
                                                                    <asp:Button ID="BtnAddConfig" runat="server" OnClick="BtnAddConfig_Click" CssClass="ButtonAdd66"
                                                                        Text="" EnableTheming="false"></asp:Button>
                                                                    <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                </cc1:TabContainer>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
