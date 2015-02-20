<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="CompanyInfo.aspx.cs" Inherits="CompanyInfo" Title="Administration > General Settings" %>

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
                    <table style="width: 100.3%;margin: -3px 0px 0px 2px;" cellpadding="2px" cellspacing="2px" class="searchbg">
                        <tr>
                            <td style="width: 1%">
                            </td>
                            <td style="width: 25%; font-size: 22px; color: #000000;" >
                                 Basic Settings
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
                <table style="text-align: left; border: 0px solid #5078B3; margin: -6px 0px 0px 0px; padding-left:3px; width: 980px" cellpadding="0"
                    cellspacing="0">
                    <tr>
                        <td>
                            <div align="center" style="width: 980px; margin: -6px 0px 0px 0px; text-align: left">
                                <cc1:TabContainer ID="tabs2" runat="server" ActiveTabIndex="0" CssClass="fancy fancy-green">
                                    <cc1:TabPanel ID="tabMaster" runat="server" HeaderText="Company Details">
                                        <ContentTemplate>
                                            <div style="text-align: left;">
                                                <table style="width: 977px; font-size: 11px; font-family: 'Trebuchet MS';" cellpadding="1"
                                                    cellspacing="1">
                                                    <tr>
                                                        <td style="width: 25%" class="ControlLabel">
                                                            <asp:RequiredFieldValidator ID="rqCompany" ForeColor="Red" ValidationGroup="compInfo"
                                                                ErrorMessage="Company Name is mandatory" Font-Bold="true" runat="server" Text="*"
                                                                ControlToValidate="txtCompanyName"></asp:RequiredFieldValidator>
                                                            Company
                                                        </td>
                                                        <td style="width: 25%" class="ControlTextBox3">
                                                            <asp:TextBox ID="txtCompanyName" runat="server" Width="300px" CssClass="cssTextBox"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 15%" class="ControlLabel">
                                                            <asp:RegularExpressionValidator ID="rgEmail" runat="server" ErrorMessage="Wrong Email Format"
                                                                ValidationGroup="compInfo" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                                ControlToValidate="txtEmail"></asp:RegularExpressionValidator>
                                                            Email
                                                        </td>
                                                        <td style="width: 20%" class="ControlTextBox3">
                                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="cssTextBox" Width="300px" Height="16px"></asp:TextBox>
                                                        </td>
                                                        
                                                        <td style="width: 20%">
                                                        </td>
                                                    </tr>
                                                    <tr style="height:2px">
                                                                                               </tr>
                                                    <%--<tr>
                                                        <td style="width: 25%; text-align: right" class="ControlLabel">
                                                            
                                                        </td>
                                                        <td style="width: 25%" class="ControlTextBox3">
                                                            
                                                        <td style="width: 15%">
                                                        </td>
                                                        <td style="width: 20%">
                                                        </td>
                                                        <td style="width: 15%">
                                                        </td>
                                                    </tr>--%>
                                                    <tr>
                                                        <td style="width: 25%" class="ControlLabel">
                                                            Address
                                                        </td>
                                                        <td style="width: 25%" class="ControlTextBox3">
                                                            <asp:TextBox TextMode="MultiLine" ID="txtAddress" runat="server" CssClass="cssTextBox"
                                                                Width="300px" Height="46px"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 15%" class="ControlLabel">
                                                            <asp:RequiredFieldValidator ID="rvCity" ForeColor="Red" ValidationGroup="compInfo"
                                                                ErrorMessage="City is mandatory" Font-Bold="true" runat="server" Text="*" ControlToValidate="txtCity"></asp:RequiredFieldValidator>
                                                            City
                                                        </td>
                                                        <td style="width: 20%" class="ControlTextBox3">
                                                            <asp:TextBox ID="txtCity" runat="server" CssClass="cssTextBox" Width="300px" Height="16px"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 15%">
                                                        </td>
                                                    </tr>
                                                    <tr style="height:2px">
                                                                                               </tr>
                                                    <%--<tr>
                                                        <td style="width: 25%" class="ControlLabel">
                                                            
                                                        </td>
                                                        <td style="width: 25%" valign="top" class="ControlTextBox3">
                                                            
                                                        </td>
                                                        <td style="width: 15%">
                                                        </td>
                                                        <td style="width: 20%">
                                                        </td>
                                                        <td style="width: 15%">
                                                        </td>
                                                    </tr>--%>
                                                    <tr>
                                                        <td style="width: 25%" class="ControlLabel">
                                                            <asp:RequiredFieldValidator ID="rvPin" ForeColor="Red" ValidationGroup="compInfo"
                                                                ErrorMessage="Pincode is mandatory" Font-Bold="true" runat="server" Text="*"
                                                                ControlToValidate="txtPincode"></asp:RequiredFieldValidator>
                                                            Pincode
                                                        </td>
                                                        <td style="width: 25%" class="ControlTextBox3">
                                                            <asp:TextBox ID="txtPincode" runat="server" CssClass="cssTextBox" Width="300px" Height="16px"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 15%" class="ControlLabel">
                                                            <asp:RequiredFieldValidator ID="rvState" ForeColor="Red" ValidationGroup="compInfo"
                                                                ErrorMessage="State is mandatory" Font-Bold="true" runat="server" Text="*" ControlToValidate="txtState"></asp:RequiredFieldValidator>
                                                            State
                                                        </td>
                                                        <td style="width: 20%" class="ControlTextBox3">
                                                            <asp:TextBox ID="txtState" runat="server" CssClass="cssTextBox" Width="300px" Height="16px"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 15%">
                                                        </td>
                                                    </tr>
                                                    <tr style="height:2px">
                                                                                               </tr>
                                                    <%--<tr>
                                                        <td style="width: 25%" class="ControlLabel">
                                                            
                                                        </td>
                                                        <td style="width: 25%" class="ControlTextBox3">
                                                            
                                                        </td>
                                                        <td style="width: 15%">
                                                        </td>
                                                        <td style="width: 20%">
                                                        </td>
                                                        <td style="width: 15%">
                                                        </td>
                                                    </tr>--%>
                                                    <tr>
                                                        <td style="width: 25%" class="ControlLabel">
                                                            Phone
                                                        </td>
                                                        <td style="width: 25%" class="ControlTextBox3">
                                                            <asp:TextBox ID="txtPhone" runat="server" CssClass="cssTextBox" Width="300px" Height="16px"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 15%" class="ControlLabel">
                                                            FAX
                                                        </td>
                                                        <td style="width: 20%" class="ControlTextBox3">
                                                            <asp:TextBox ID="txtFAX" runat="server" CssClass="cssTextBox" Width="300px" Height="16px"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 15%">
                                                        </td>
                                                    </tr>
                                                    <tr style="height:2px">
                                                                                               </tr>
                                                    <%--<tr>
                                                        <td style="width: 25%" class="ControlLabel">
                                                            
                                                        </td>
                                                        <td style="width: 25%" class="ControlTextBox3">
                                                            
                                                        </td>
                                                        <td style="width: 15%">
                                                        </td>
                                                        <td style="width: 20%">
                                                        </td>
                                                        <td style="width: 15%">
                                                        </td>
                                                    </tr>--%>
                                                    <tr>
                                                        <td style="width: 25%" class="ControlLabel">
                                                            CST No.
                                                        </td>
                                                        <td style="width: 25%" class="ControlTextBox3">
                                                            <asp:TextBox ID="txtCST" runat="server" CssClass="cssTextBox" Width="300px" Height="16px"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 15%" class="ControlLabel">
                                                            Logo.
                                                        </td>
                                                        <td style="width: 20%" class="ControlTextBox3">
                                                            <asp:FileUpload ID="fileuploadimages" runat="server"  BackColor="#e7e7e7" style="border: 1px solid #e7e7e7;"/>
                                                        </td>
                                                        <td style="width: 15%">
                                                        </td>
                                                    </tr>
                                                    <tr style="height:2px">
                                                                                               </tr>
                                                    <tr>
                                                        <td style="width: 25%" class="ControlLabel">
                                                            TIN No.
                                                        </td>
                                                        <td style="width: 25%" class="ControlTextBox3">
                                                            <asp:TextBox ID="txtTin" runat="server" CssClass="cssTextBox" Width="300px" Height="16px"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 15%" >
                                                            
                                                        </td>
                                                        <td style="width: 20%" >
                                                            
                                                        </td>
                                                        <td style="width: 15%">
                                                        </td>
                                                    </tr>
                                                    <tr style="height:2px">
                                                                                               </tr>
                                                    <tr>
                                                        <td style="width: 25%">
                                                            
                                                        </td>
                                                        <td style="width: 25%">
                                                            
                                                        </td>
                                                        <td style="width: 15%" align="center">
                                                            <asp:Button ID="btnSave" runat="server" SkinID="skinBtnSave" ValidationGroup="compInfo"
                                                                CssClass="savebutton1231" EnableTheming="false" OnClick="btnCompanyInfo_Click" />
                                                            <asp:ValidationSummary ID="valSum" DisplayMode="BulletList" ShowMessageBox="true"
                                                                ShowSummary="false" HeaderText="Validation Messages" ValidationGroup="compInfo"
                                                                Font-Names="'Trebuchet MS'" Font-Size="12" runat="server" />
                                                        </td>
                                                        <td style="width: 20%">
                                                        </td>
                                                        <td style="width: 15%">
                                                        </td>
                                                    </tr>
                                                    <%--<tr>
                                            <td colspan="4" align="center">
                                                <%--<hr />--%>
                                                    <%--</td>
                                        </tr>--%>
                                                </table>
                                            </div>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel ID="TabPanel4" runat="server" HeaderText="Company Divisions">
                                        <ContentTemplate>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                                                <ContentTemplate>
                                                    <div style="text-align: left;">
                                                        <table style="width: 977px" cellpadding="1" cellspacing="1">
                                                            <tr>
                                                                <td style="text-align: left; width: 977px">
                                                                    <asp:Panel ID="pnlDivsion" runat="server" Visible="false">
                                                                        <div style="text-align: left; width: 977px">
                                                                            <table style="width: 977px; border: 0px solid #86b2d1" cellpadding="1" cellspacing="1">
                                                                                <tr style="height: 5px">
                                                                                </tr>
                                                                                <tr style="height: 20px">
                                                                                    <td style="width: 20%" class="ControlLabel">
                                                                                        <asp:RequiredFieldValidator ValidationGroup="Save" ID="rq" runat="server" ErrorMessage="Division is mandatory"
                                                                                            Text="*" ControlToValidate="txtDivision"></asp:RequiredFieldValidator>
                                                                                        Division *
                                                                                    </td>
                                                                                    <td style="width: 20%" class="ControlTextBox3">
                                                                                        <asp:TextBox ID="txtDivision" MaxLength="50" runat="server" SkinID="skinTxtBox" TabIndex="1"></asp:TextBox>
                                                                                    </td>
                                                                                    <td style="width: 10%" class="ControlLabel">
                                                                                        PIN No.
                                                                                    </td>
                                                                                    <td style="width: 20%" class="ControlTextBox3">
                                                                                        <asp:TextBox ID="txtDivPinNo" TabIndex="6" MaxLength="10" runat="server" SkinID="skinTxtBox" />
                                                                                    </td>
                                                                                    <td style="width: 10%">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr style="height:2px">
                                                                                               </tr>
                                                                                <tr style="vertical-align: bottom">
                                                                                    <td style="width: 20%" class="ControlLabel">
                                                                                        Street
                                                                                    </td>
                                                                                    <td style="width: 20%; vertical-align: middle" class="ControlTextBox3">
                                                                                        <asp:TextBox ID="txtDivAddress" TabIndex="2" MaxLength="255" runat="server" SkinID="skinTxtBox" />
                                                                                    </td>
                                                                                    <td style="width: 10%" class="ControlLabel">
                                                                                        Phone
                                                                                    </td>
                                                                                    <td style="width: 20%" class="ControlTextBox3">
                                                                                        <asp:TextBox ID="txtDivPhoneNo" TabIndex="7" runat="server" MaxLength="10" SkinID="skinTxtBox"></asp:TextBox>
                                                                                    </td>
                                                                                    <td style="width: 15%">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr style="height:2px">
                                                                                               </tr>
                                                                                <tr>
                                                                                    <td style="width: 20%" class="ControlLabel">
                                                                                        <asp:RequiredFieldValidator ValidationGroup="Save" ID="RequiredFieldValidator5" runat="server"
                                                                                            Text="*" ErrorMessage="City is mandatory" ControlToValidate="txtDivCity"></asp:RequiredFieldValidator>
                                                                                        City *
                                                                                    </td>
                                                                                    <td style="width: 20%" class="ControlTextBox3">
                                                                                        <asp:TextBox ID="txtDivCity" TabIndex="3" MaxLength="50" runat="server" SkinID="skinTxtBox" />
                                                                                    </td>
                                                                                    <td style="width: 10%" class="ControlLabel">
                                                                                        Fax
                                                                                    </td>
                                                                                    <td style="width: 20%" class="ControlTextBox3">
                                                                                        <asp:TextBox ID="txtDivFax" TabIndex="8" MaxLength="10" runat="server" SkinID="skinTxtBox"> </asp:TextBox>
                                                                                    </td>
                                                                                    <td style="width: 15%">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr style="height:2px">
                                                                                               </tr>
                                                                                <tr runat="server" id="rowremarks">
                                                                                    <td style="width: 20%" class="ControlLabel">
                                                                                        State
                                                                                    </td>
                                                                                    <td style="width: 20%" class="ControlTextBox3">
                                                                                        <asp:TextBox ID="txtDivState" TabIndex="4" MaxLength="50" runat="server" SkinID="skinTxtBox" />
                                                                                    </td>
                                                                                    <td style="width: 10%" class="ControlLabel">
                                                                                        Email
                                                                                    </td>
                                                                                    <td style="width: 20%" class="ControlTextBox3">
                                                                                        <asp:TextBox ID="txtDivEmail" TabIndex="9" MaxLength="50" runat="server" SkinID="skinTxtBox" />
                                                                                    </td>
                                                                                    <td style="width: 15%">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr style="height:2px">
                                                                                               </tr>
                                                                                <tr runat="server" id="Tr1">
                                                                                    <td style="width: 20%" class="ControlLabel">
                                                                                        TIN No.
                                                                                    </td>
                                                                                    <td style="width: 20%" class="ControlTextBox3">
                                                                                        <asp:TextBox ID="txtDivTinNo" TabIndex="5" runat="server" MaxLength="10" SkinID="skinTxtBox" />
                                                                                    </td>
                                                                                    <td style="width: 10%" class="ControlLabel">
                                                                                        GST No.
                                                                                    </td>
                                                                                    <td style="width: 20%" class="ControlTextBox3">
                                                                                        <asp:TextBox ID="txtDivGSTNo" TabIndex="10" MaxLength="10" runat="server" SkinID="skinTxtBox" />
                                                                                    </td>
                                                                                    <td style="width: 15%">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr style="height: 5px">
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                    </td>
                                                                                    <td align="right">                                                                                        
                                                                                        <asp:Button ID="btnDivSave" ValidationGroup="Save" runat="server" SkinID="skinBtnSave"
                                                                                            CssClass="savebutton1231" EnableTheming="false" OnClick="btnDivSave_Click" />
                                                                                        <asp:Button ID="btnDivUpdate" runat="server" ValidationGroup="Save" CssClass="Updatebutton1231"
                                                                                            EnableTheming="false" OnClick="btnDivUpdate_Click" />
                                                                                    </td>
                                                                                    <td align="right">
                                                                                        <asp:Button ID="btnDivCancel" runat="server" SkinID="skinBtnCancel" CssClass="cancelbutton6"
                                                                                            EnableTheming="false" OnClick="btnDivCancel_Click" />
                                                                                    </td>
                                                                                    <td>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                
                                                            </tr>
                                                            <tr style="width: 977px">
                                                                <td style="width: 977px">
                                                                    <div class="mainGridHold" id="searchGrid" style="width: 977px" align="center">
                                                                        <asp:GridView ID="GrdDiv" CssClass="someClass" runat="server" AllowSorting="True"
                                                                            AutoGenerateColumns="False" Width="90%" AllowPaging="True" OnPageIndexChanging="GrdDiv_PageIndexChanging"
                                                                            OnDataBound="GrdDiv_DataBound" OnRowCreated="GrdDiv_RowCreated" DataKeyNames="DivisionID"
                                                                            PageSize="10" EmptyDataText="No Divisions found." OnSelectedIndexChanged="GrdDiv_SelectedIndexChanged"
                                                                            OnRowDeleting="GrdDiv_RowDeleting">
                                                                            <EmptyDataRowStyle CssClass="GrdHeaderbgClr" Font-Bold="true" Height="25px" />
                                                                              <HeaderStyle Height="30px" HorizontalAlign="Center" Font-Bold="true" BackColor="#cccccc" BorderColor="Gray" Font-Size="Small" />
                                                                             <RowStyle Font-Bold="true" HorizontalAlign="Center" Height="30px" Font-Size="Small" ForeColor="#0567AE" />
                                                                            <Columns>
                                                                                <asp:BoundField DataField="DivisionName" HeaderText="Division" HeaderStyle-BorderColor="Gray" />
                                                                                <asp:BoundField DataField="Address" HeaderText="Address" HeaderStyle-BorderColor="Gray" />
                                                                                <asp:BoundField DataField="City" HeaderText="City" HeaderStyle-BorderColor="Gray" />
                                                                                <asp:BoundField DataField="Phone" HeaderText="Phone" HeaderStyle-BorderColor="Gray" />
                                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="command"
                                                                                    HeaderStyle-BorderColor="Gray" HeaderStyle-Width="35px" HeaderText="Edit">
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Select" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="command" HeaderText="Delete"
                                                                                    HeaderStyle-BorderColor="Gray" HeaderStyle-Width="35px">
                                                                                    <ItemTemplate>
                                                                                        <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to Delete this Division?"
                                                                                            runat="server">
                                                                                        </cc1:ConfirmButtonExtender>
                                                                                        <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" CommandName="Delete"></asp:ImageButton>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <PagerTemplate>
                                                                                Goto Page
                                                                                <asp:DropDownList ID="ddlPageSelector" runat="server" AutoPostBack="true" Width="55px"
                                                                                    OnSelectedIndexChanged="ddlPageSelector_SelectedIndexChanged">
                                                                                </asp:DropDownList>
                                                                                <asp:Button Text="First" CommandName="Page" CommandArgument="First" runat="server"
                                                                                    ID="btnFirst" />
                                                                                <asp:Button Text="Previous" CommandName="Page" CommandArgument="Prev" runat="server"
                                                                                    ID="btnPrevious" />
                                                                                <asp:Button Text="Next" CommandName="Page" CommandArgument="Next" runat="server"
                                                                                    ID="btnNext" />
                                                                                <asp:Button Text="Last" CommandName="Page" CommandArgument="Last" runat="server"
                                                                                    ID="btnLast" />
                                                                            </PagerTemplate>
                                                                        </asp:GridView>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table width="100%">
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
                                    <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="App Configuration">
                                        <ContentTemplate>
                                            <div style="text-align: left">
                                                <table style="width: 977px; font-size: 11px; font-family: 'Trebuchet MS';" cellpadding="2"
                                                    cellspacing="1">
                                                    <tr style="height: 10px">
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%" class="ControlLabel">
                                                            Auto Date Lock
                                                        </td>
                                                        <td style="width: 20%" class="ControlTextBox3">
                                                            <asp:RadioButtonList ID="dpautolock" runat="server" RepeatDirection="Horizontal">
                                                                <asp:ListItem Selected="True" Text="YES"></asp:ListItem>
                                                                <asp:ListItem Text="NO"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                        <td style="width: 15%" class="ControlLabel">
                                                            Purchase Entry Date Enable
                                                        </td>
                                                        <td style="width: 20%" class="ControlTextBox3">
                                                            <asp:RadioButtonList ID="rdvoudateenable" runat="server" RepeatDirection="Horizontal">
                                                                <asp:ListItem Selected="True" Text="YES"></asp:ListItem>
                                                                <asp:ListItem Text="NO"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                    </tr>
                                                    <tr style="height:2px">
                                                                                               </tr>
                                                    <tr>
                                                        <td style="width: 25%" class="ControlLabel">
                                                            Enable Vat% in S And P
                                                        </td>
                                                        <td style="width: 20%" class="ControlTextBox3">
                                                            <asp:RadioButtonList ID="ddenablevat" runat="server" RepeatDirection="Horizontal">
                                                                <asp:ListItem Selected="True" Text="YES"></asp:ListItem>
                                                                <asp:ListItem Text="NO"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                        <td style="width: 15%" class="ControlLabel">
                                                            Save Log Details
                                                        </td>
                                                        <td style="width: 20%" class="ControlTextBox3">
                                                            <asp:RadioButtonList ID="dpsavelog" runat="server" RepeatDirection="Horizontal">
                                                                <asp:ListItem Selected="True" Text="YES"></asp:ListItem>
                                                                <asp:ListItem Text="NO"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                    </tr>
                                                    <tr style="height:2px">
                                                                                               </tr>
                                                    <tr>
                                                        <td style="width: 25%" class="ControlLabel">
                                                            Hide Obsolute Products
                                                        </td>
                                                        <td style="width: 20%" class="ControlDrpBorder">
                                                            <asp:DropDownList ID="dpobsolute" Width="100%" CssClass="drpDownListMedium" BackColor="#e7e7e7"
                                                                runat="server" Style="border: 1px solid #e7e7e7" Height="26px">
                                                                <asp:ListItem Text="YES"></asp:ListItem>
                                                                <asp:ListItem Selected="True" Text="NO"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="width: 15%" class="ControlLabel">
                                                            Billing Method
                                                        </td>
                                                        <td style="width: 20%" class="ControlDrpBorder">
                                                            <%--<div style="border-width: 1px; border-color: #90c9fc; border-style: solid; width: 170px;">--%>
                                                            <asp:DropDownList ID="DropDownList1" Width="100%" CssClass="drpDownListMedium" BackColor="#e7e7e7"
                                                                runat="server" Style="border: 1px solid #e7e7e7" Height="26px">
                                                                <asp:ListItem Selected="True" Text="VAT EXCLUSIVE"></asp:ListItem>
                                                                <asp:ListItem Text="VAT INCLUSIVE"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <%--</div>--%>
                                                        </td>
                                                    </tr>
                                                    <tr style="height:2px">
                                                                                               </tr>
                                                    <tr>
                                                        <td style="width: 25%" class="ControlLabel">
                                                            <%--RoundOff With FixedTotal--%>
                                                            Deviation Price List
                                                        </td>
                                                        <td style="width: 20%" class="ControlDrpBorder">
                                                            <asp:DropDownList ID="dproundoff" Width="100%" CssClass="drpDownListMedium" BackColor="#e7e7e7"
                                                                runat="server" Style="border: 1px solid #e7e7e7" Height="26px" Visible="false">
                                                                <asp:ListItem Text="YES"></asp:ListItem>
                                                                <asp:ListItem Selected="True" Text="NO"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddPriceList" Width="100%" AppendDataBoundItems="True" CssClass="drpDownListMedium"
                                                                BackColor="#e7e7e7" runat="server" DataTextField="PRICEname" DataValueField="PRICEname"
                                                                ValidationGroup="product" Style="border: 1px solid #e7e7e7" Height="26px">
                                                                <asp:ListItem Text="Select PriceList" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="width: 15%" class="ControlLabel">
                                                            Sales BillNo Series
                                                        </td>
                                                        <td style="width: 20%" class="ControlDrpBorder">
                                                            <asp:DropDownList ID="dpsalesbillno" Width="100%" CssClass="drpDownListMedium" BackColor="#e7e7e7"
                                                                runat="server" Style="border: 1px solid #e7e7e7" Height="26px">
                                                                <asp:ListItem Text="YES"></asp:ListItem>
                                                                <asp:ListItem Selected="True" Text="NO"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <%--<tr style="height:2px">
                                                                                               </tr>--%>
                                                    <tr id="rowextra" runat="server" style="display:none">
                                                        <td style="width: 25%" class="ControlLabel">
                                                            Supplier TinNo mandatory
                                                        </td>
                                                        <td style="width: 20%" class="ControlTextBox3">
                                                            <%--<div style="border-width: 1px; border-color: #bce1fe; border-style: solid; width: 170px;">--%>
                                                            <asp:RadioButtonList ID="rdomacaddress" runat="server" RepeatDirection="Horizontal" Visible="False">
                                                                <asp:ListItem Text="YES"></asp:ListItem>
                                                                <asp:ListItem Selected="True" Text="NO"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                            <asp:RadioButtonList ID="rdotinnomandatory" runat="server" RepeatDirection="Horizontal">
                                                                <asp:ListItem Text="YES"></asp:ListItem>
                                                                <asp:ListItem Selected="True" Text="NO"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                            <%--</div>--%>
                                                        </td>
                                                        <td style="width: 15%" class="ControlLabel">
                                                            BLIT Required
                                                        </td>
                                                        <td style="width: 20%" class="ControlTextBox3">
                                                           <%--<div style="border-width: 1px; border-color: #bce1fe; border-style: solid; width: 170px;">--%>
                                                            <asp:RadioButtonList ID="rdoBLIT" runat="server" RepeatDirection="Horizontal">
                                                                <asp:ListItem Text="YES"></asp:ListItem>
                                                                <asp:ListItem Selected="True" Text="NO"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                            <%--</div>--%>
                                                        </td>
                                                    </tr>
                                                    <tr style="height:2px">
                                                                                               </tr>
                                                    <tr>
                                                        <td style="width: 25%" class="ControlLabel">
                                                           Update Opening Balance
                                                        </td>
                                                        <td style="width: 20%" class="ControlTextBox3">
                                                            <asp:RadioButtonList ID="RadioButtonOpening" runat="server" RepeatDirection="Horizontal">
                                                                <asp:ListItem Selected="True" Text="YES"></asp:ListItem>
                                                                <asp:ListItem Text="NO"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                        <td style="width: 15%" class="ControlLabel">
                                                            Enable Sales Discount
                                                        </td>
                                                        <td style="width: 20%" class="ControlTextBox3">
                                                            <asp:RadioButtonList ID="RadioButtonDiscount" runat="server" RepeatDirection="Horizontal">
                                                                <asp:ListItem Selected="True" Text="YES"></asp:ListItem>
                                                                <asp:ListItem Text="NO"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                    </tr>
                                                    <tr style="height:2px">
                                                                                               </tr>
                                                    <tr>
                                                        <td style="width: 25%" class="ControlLabel">
                                                            MAC Blocking
                                                        </td>
                                                        <td style="width: 20%" class="ControlTextBox3">
                                                            <%--<div style="border-width: 1px; border-color: #bce1fe; border-style: solid; width: 170px;">--%>
                                                            <asp:RadioButtonList ID="rdIPBlock" AutoPostBack="True" runat="server" RepeatDirection="Horizontal"
                                                                OnSelectedIndexChanged="rdIPBlock_SelectedIndexChanged">
                                                                <asp:ListItem Selected="True" Text="YES"></asp:ListItem>
                                                                <asp:ListItem Text="NO"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                            <%--</div>--%>
                                                        </td>
                                                        <td style="width: 15%" class="ControlLabel">
                                                            Bill Format
                                                        </td>
                                                        <td style="width: 20%" class="ControlDrpBorder">
                                                            <%--<div style="border-width: 1px; border-color: #90c9fc; border-style: solid; width: 170px;">--%>
                                                            <asp:DropDownList ID="cmdBill" Width="100%" AppendDataBoundItems="True" CssClass="drpDownListMedium"
                                                                BackColor="#e7e7e7" runat="server" DataTextField="Billformat" DataValueField="Billformat"
                                                                ValidationGroup="product" Style="border: 1px solid #e7e7e7" Height="26px">
                                                                <asp:ListItem Text="Select Billformat" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <%--</div>--%>
                                                        </td>
                                                    </tr>
                                                    <tr style="height:2px">
                                                                                               </tr>
                                                    <tr>
                                                        <td style="width: 25%" class="ControlLabel">
                                                            Currency
                                                        </td>
                                                        <td style="width: 20%" class="ControlDrpBorder">
                                                            <%--<div style="border-width: 1px; border-color: #90c9fc; border-style: solid; width: 170px;">--%>
                                                            <asp:DropDownList ID="ddCurrency" Width="100%" AppendDataBoundItems="True" CssClass="drpDownListMedium"
                                                                BackColor="#e7e7e7" runat="server" Style="border: 1px solid #e7e7e7" Height="26px">
                                                                <asp:ListItem Text="India Rupee(INR)" Value="INR"></asp:ListItem>
                                                                <asp:ListItem Text="British Pound(£)" Value="GBP"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <%--</div>--%>
                                                        </td>
                                                        <td style="width: 15%" class="ControlLabel">
                                                            Stock Edit
                                                        </td>
                                                        <td style="width: 20%" class="ControlTextBox3">
                                                            <%--<div style="border-width: 1px; border-color: #bce1fe; border-style: solid; width: 170px;">--%>
                                                            <asp:RadioButtonList ID="rdoStockEdit" runat="server" RepeatDirection="Horizontal">
                                                                <asp:ListItem Text="YES"></asp:ListItem>
                                                                <asp:ListItem Selected="True" Text="NO"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                            <%--</div>--%>
                                                        </td>
                                                    </tr>
                                                    <tr style="height:2px">
                                                                                               </tr>
                                                    <tr>
                                                        <td style="width: 25%" class="ControlLabel">
                                                            Barcode
                                                        </td>
                                                        <td style="width: 20%" class="ControlTextBox3">
                                                            <%--<div style="border-width: 1px; border-color: #bce1fe; border-style: solid; width: 170px;">--%>
                                                            <asp:RadioButtonList ID="rdoBarcode" runat="server" RepeatDirection="Horizontal">
                                                                <asp:ListItem Text="YES"></asp:ListItem>
                                                                <asp:ListItem Selected="True" Text="NO"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                            <%--</div>--%>
                                                        </td>
                                                        <td style="width: 15%" class="ControlLabel">
                                                            SMS Required
                                                        </td>
                                                        <td style="width: 20%" class="ControlTextBox3">
                                                            <%--<div style="border-width: 1px; border-color: #bce1fe; border-style: solid; width: 170px;">--%>
                                                            <asp:RadioButtonList ID="rdoSMSRqrd" runat="server" RepeatDirection="Horizontal">
                                                                <asp:ListItem Text="YES"></asp:ListItem>
                                                                <asp:ListItem Selected="True" Text="NO"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                            <%--</div>--%>
                                                        </td>
                                                    </tr>
                                                    <tr style="height:2px">
                                                                                               </tr>
                                                    <tr>
                                                        <td style="width: 25%" class="ControlLabel">
                                                            Email Required
                                                        </td>
                                                        <td style="width: 20%" class="ControlTextBox3">
                                                            <%--<div style="border-width: 1px; border-color: #bce1fe; border-style: solid; width: 170px;">--%>
                                                            <asp:RadioButtonList ID="rdoemailrequired" runat="server" RepeatDirection="Horizontal">
                                                                <asp:ListItem Text="YES"></asp:ListItem>
                                                                <asp:ListItem Selected="True" Text="NO"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                            <%--</div>--%>
                                                        </td>
                                                        <td style="width: 15%" class="ControlLabel">
                                                            Owner Mobile
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender45" runat="server" FilterType="Numbers"
                                                                TargetControlID="txtMobile" />
                                                        </td>
                                                        <td style="width: 20%" class="ControlTextBox3">
                                                            <asp:TextBox ID="txtMobile" MaxLength="10" runat="server" CssClass="cssTextBox" Width="165px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr style="height:2px">
                                                                                               </tr>
                                                    <tr>
                                                        <td style="width: 25%" class="ControlLabel">
                                                            VAT Amount
                                                        </td>
                                                        <td style="width: 20%" class="ControlTextBox3">
                                                            <asp:TextBox ID="txtVATAmount" MaxLength="10" runat="server" CssClass="cssTextBox"
                                                                Width="165px"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 15%" class="ControlLabel">
                                                            VAT Recon Date
                                                        </td>
                                                        <td style="width: 20%" class="ControlTextBox3">
                                                            <asp:TextBox ID="txtVATReconDate" Enabled="false" runat="server" CssClass="cssTextBox" Width="125px"
                                                                Height="14px" MaxLength="10"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="calBillDate" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                PopupButtonID="btnBillDate" PopupPosition="BottomLeft" TargetControlID="txtVATReconDate">
                                                            </cc1:CalendarExtender>
                                                        </td>
                                                        <td style="width:10" align="left">
                                                            <asp:ImageButton ID="btnBillDate" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                Width="20px" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr style="height:2px">
                                                                                               </tr>
                                                    <tr>
                                                        <td style="width: 25%" class="ControlLabel">
                                                            Licence
                                                        </td>
                                                        <td style="width: 20%" class="ControlTextBox3">
                                                            <asp:Label ID="Label1" ForeColor="Black" runat="server"></asp:Label>
                                                        </td>
                                                         <td style="width: 15%" class="ControlLabel">
                                                            Password Expiry Day
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                                                TargetControlID="txtExpDay" />
                                                        </td>
                                                        <td style="width: 20%" class="ControlTextBox3">
                                                            <asp:TextBox ID="txtExpDay" MaxLength="10" runat="server" CssClass="cssTextBox" Width="165px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 5px">
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td align="center">
                                                            <%--<hr />--%>
                                                            <asp:Button ID="Button1" ValidationGroup="Save" runat="server" SkinID="skinBtnSave"
                                                                CssClass="savebutton1231" EnableTheming="false" OnClick="btnSave_Click" />
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel ID="TabPanel5" runat="server" HeaderText="Configuration Details">
                                        <ContentTemplate>
                                            <div style="text-align: left">
                                                <table style="font-size: 11px; font-family: 'Trebuchet MS'; width: 977px;" cellpadding="1"
                                                    cellspacing="1">
                                                    <tr style="height: 10px">
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%" class="ControlLabel">
                                                            Item Tracking
                                                        </td>
                                                        <td style="width: 20%" class="ControlTextBox3">
                                                            <%--<div style="border-width: 1px; border-color: #bce1fe; border-style: solid; width: 170px;">--%>
                                                            <asp:RadioButtonList ID="rdQtyReturn" runat="server" RepeatDirection="Horizontal">
                                                                <asp:ListItem Selected="True" Text="YES"></asp:ListItem>
                                                                <asp:ListItem Text="NO"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                        <td style="width: 15%" class="ControlLabel">
                                                            <asp:RequiredFieldValidator ValidationGroup="Save" ID="RequiredFieldValidator1" Font-Bold="True"
                                                                runat="server" Text="*" ControlToValidate="txtDate"></asp:RequiredFieldValidator>
                                                            Item Tracking Start Date
                                                        </td>
                                                        <td style="width: 20%" class="ControlTextBox3">
                                                            <asp:TextBox ID="txtDate" runat="server" Enabled="false" CssClass="cssTextBox" Width="125px" MaxLength="10"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="ImgITDate"
                                                                TargetControlID="txtDate" Enabled="True">
                                                            </cc1:CalendarExtender>
                                                        </td>
                                                        <td style="height: 10%" align="left">
                                                            <asp:ImageButton ID="ImgITDate" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="False"
                                                                Width="20px" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr style="height:2px">
                                                                                               </tr>
                                                    <tr>
                                                        <td style="width: 25%" class="ControlLabel">
                                                            Item Tracking Itemcode
                                                        </td>
                                                        <td style="width: 20%" class="ControlDrpBorder">
                                                            <div style="border-width: 1px; border-color: #e7e7e7; border-style: solid;">
                                                                <asp:DropDownList ID="cmbProdAdd" Width="100%" AppendDataBoundItems="True" CssClass="drpDownListMedium"
                                                                    BackColor="#e7e7e7" runat="server" DataTextField="ProductName" DataValueField="ItemCode"
                                                                    ValidationGroup="product" Style="border: 1px solid #e7e7e7" Height="26px">
                                                                    <asp:ListItem Text=" -- Please Select -- " Value="0"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </td>
                                                        <td style="width: 15%" class="ControlLabel">
                                                            Discount Type
                                                        </td>
                                                        <td style="width: 20%" class="ControlDrpBorder">
                                                            <div style="border-width: 1px; border-color: #e7e7e7; border-style: solid;">
                                                                <asp:DropDownList ID="ddDiscType" Width="100%" AppendDataBoundItems="True" CssClass="drpDownListMedium"
                                                                    BackColor="#e7e7e7" runat="server" Style="border: 1px solid #e7e7e7" Height="26px">
                                                                    <asp:ListItem Text="In Percentage(%)" Value="PERCENTAGE"></asp:ListItem>
                                                                    <asp:ListItem Text="In Rupees(INR)" Value="RUPEE"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr style="height:2px">
                                                                                               </tr>
                                                    <tr>
                                                        <td style="width: 25%" class="ControlLabel">
                                                            Allow Credit Exceed
                                                        </td>
                                                        <td style="width: 20%" class="ControlTextBox3">
                                                            <%--<div style="border-width: 1px; border-color: #bce1fe; border-style: solid; width: 170px;">--%>
                                                            <asp:RadioButtonList ID="rdoExceedCreditLimit" runat="server" RepeatDirection="Horizontal">
                                                                <asp:ListItem Selected="True" Text="YES"></asp:ListItem>
                                                                <asp:ListItem Text="NO"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                            <%--</div>--%>
                                                        </td>
                                                        <td style="width: 15%" class="ControlLabel">
                                                            Dealer Required
                                                        </td>
                                                        <td style="width: 20%" class="ControlTextBox3">
                                                            <%--<div style="border-width: 1px; border-color: #bce1fe; border-style: solid; width: 170px;">--%>
                                                            <asp:RadioButtonList ID="rdDealer" runat="server" RepeatDirection="Horizontal">
                                                                <asp:ListItem Selected="True" Text="YES"></asp:ListItem>
                                                                <asp:ListItem Text="NO"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                            <%--</div>--%>
                                                        </td>
                                                    </tr>
                                                    <tr style="height:15px">
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td align="center">
                                                            <%--<hr />--%>
                                                            <asp:Button ID="Button2" ValidationGroup="Save" runat="server" SkinID="skinBtnSave"
                                                                CssClass="savebutton1231" EnableTheming="false" OnClick="btnSave_Click" />
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="MAC Addresses">
                                        <ContentTemplate>
                                            <div style="width: 100%" align="center">
                                                <table style="width: 955px; border: 0px solid #5078B3;" cellpadding="3"
                                                    cellspacing="1">
                                                    <tr>
                                                        <td style="width: 35%;">
                                                        </td>
                                                        <td style="width: 20%;" class="ControlTextBox3">
                                                            <asp:TextBox ID="txtAddIP" runat="server" CssClass="cssTextBox" Width="100px"></asp:TextBox>&nbsp;&nbsp;
                                                            <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="True" ShowSummary="False" HeaderText="Validation Messages"
                                                                 Font-Names="'Trebuchet MS'" Font-Size="12pt" runat="server" ValidationGroup="AddIP" />
                                                            <asp:RequiredFieldValidator ValidationGroup="AddIP" ID="RequiredFieldValidator2"
                                                                    ForeColor="Red" runat="server" Text="*" ErrorMessage="IP Address is Required"  ControlToValidate="txtAddIP"></asp:RequiredFieldValidator>
                                                                
                                                        </td>
                                                        <td style="width: 20%" class="tblLeft">
                                                            <asp:Button ID="btnAddIP" ValidationGroup="AddIP" Width="100px" runat="server" Text="Add MAC" SkinID="skinButtonCol2"
                                                                OnClick="btnAddIP_Click" />
                                                        </td>
                                                        <td style="width: 25%; text-align: right">
                                                        </td>
                                                    </tr>
                                                    <tr style="height:6px">

                                                    </tr>
                                                    <tr>
                                                        <td colspan="4"  style="width: 955px;">
                                                            
                                                            <div style="width: 100%;" align="center">
                                                                <asp:GridView ID="grdViewIP" CssClass="someClass" runat="server" EmptyDataText="No Data found!"
                                                                    DataKeyNames="IP" AutoGenerateColumns="False" AllowPaging="True" Width="50%"
                                                                    OnRowCreated="grdViewIP_RowCreated" OnRowDeleted="grdViewIP_RowDeleted" DataSourceID="grdSource">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="IP" HeaderText="MAC Address" HeaderStyle-BorderColor="Gray">
                                                                            <ItemStyle Width="70%" Wrap="False" />
                                                                        </asp:BoundField>
                                                                        <asp:TemplateField ItemStyle-CssClass="command" ItemStyle-HorizontalAlign="Center"
                                                                            HeaderStyle-BorderColor="Gray" HeaderText="Delete">
                                                                            <ItemTemplate>
                                                                                <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to Delete this MAC Address?"
                                                                                    runat="server">
                                                                                </cc1:ConfirmButtonExtender>
                                                                                <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" CommandName="Delete"></asp:ImageButton>
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
                                                                                    <asp:DropDownList ID="ddlPageSelector" style="border:1px solid blue"  Width="75px"  height="23px" BackColor="#BBCAFB" runat="server" AutoPostBack="true">
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
                                                            <asp:ObjectDataSource ID="grdSource" runat="server" DeleteMethod="DelIPAddresses"
                                                                SelectMethod="GetIPAddresses" TypeName="BusinessLogic">
                                                                <SelectParameters>
                                                                    <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                </SelectParameters>
                                                                <DeleteParameters>
                                                                    <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                    <asp:Parameter Name="IP" Type="String" />
                                                                </DeleteParameters>
                                                            </asp:ObjectDataSource>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="Unit Measurement">
                                        <ContentTemplate>
                                            <div style="width: 100%" align="center">
                                                <table style="width: 977px; border: 0px solid #5078B3;" align="center" cellpadding="3"
                                                    cellspacing="3">
                                                    <tr>
                                                        <td>
                                                            <div style="width: 100%" align="center">
                                                              
                                                                <asp:GridView ID="GrdUnitMnt" runat="server" CssClass="someClass" DataSourceID="srcGridView"
                                                                    AutoGenerateColumns="False" OnRowCreated="GrdUnitMnt_RowCreated" Width="50%"
                                                                    PageSize="5" EmptyDataText="No Units Found" Style="font-family: 'Trebuchet MS';
                                                                    font-size: 11px;" OnRowCommand="GrdUnitMnt_RowCommand" AllowPaging="True" DataKeyNames="ID"
                                                                    EnableViewState="False" OnRowUpdating="GrdUnitMnt_RowUpdating" OnRowUpdated="GrdUnitMnt_RowUpdated">
                                                                      <HeaderStyle Height="30px" HorizontalAlign="Center" Font-Bold="true" BackColor="#cccccc" BorderColor="Gray" Font-Size="Small" />
                                                                     <RowStyle Font-Bold="true" HorizontalAlign="Center" Height="30px" Font-Size="Small" ForeColor="#0567AE" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Unit" HeaderStyle-BorderColor="Gray">
                                                                            <ItemStyle Width="80%" HorizontalAlign="Left" />
                                                                            <FooterStyle Width="80%" HorizontalAlign="Left" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblUnit" runat="server" Text='<%# Bind("Unit") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <asp:TextBox ID="txtUnit" runat="server" Text='<%# Bind("Unit") %>' CssClass="cssTextBox"
                                                                                    Width="95%"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="rvDescr" runat="server" ControlToValidate="txtUnit"
                                                                                    Display="Dynamic" EnableClientScript="False" ErrorMessage="Unit is mandatory">*</asp:RequiredFieldValidator>
                                                                            </EditItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:TextBox ID="txtAddUnit" runat="server" CssClass="cssTextBox" Width="95%"></asp:TextBox><asp:RequiredFieldValidator
                                                                                    ID="rvAddDescr" runat="server" ControlToValidate="txtAddUnit" Display="Dynamic"
                                                                                    EnableClientScript="true" ErrorMessage="Unit is mandatory">*</asp:RequiredFieldValidator>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Edit" HeaderStyle-BorderColor="Gray">
                                                                            <ItemStyle Width="20%" HorizontalAlign="Center" />
                                                                            <FooterStyle Width="20%" HorizontalAlign="Center" />
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
                                                                                    <asp:DropDownList ID="ddlPageSelector" runat="server" AutoPostBack="true" SkinID="skinPagerDdlBox2">
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
                                                                    SelectMethod="GetUnitData" TypeName="BusinessLogic">
                                                                    <SelectParameters>
                                                                        <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                    </SelectParameters>
                                                                </asp:ObjectDataSource>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table width="100%">
                                                    <tr>
                                                        <td align="center">
                                                              <asp:Button ID="lnkBtnAdd" runat="server" OnClick="lnkBtnAdd_Click" CssClass="ButtonAdd66"
                                                                    Text="" EnableTheming="false"></asp:Button>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel ID="tabTransporters" runat="server" HeaderText="Transporters">
                                        <ContentTemplate>
                                            <asp:UpdatePanel ID="UpdatePanel23" runat="server" UpdateMode="Always">
                                                <ContentTemplate>
                                                    <div style="width: 100%" align="center">
                                                        <table style="width: 977px; border: 0px solid #5078B3;" align="center" cellpadding="3"
                                                            cellspacing="3">
                                                            <tr>
                                                                <td>
                                                                    <div style="width: 100%" align="center">
                                                                      
                                                                        <asp:GridView ID="GrdTransporter" runat="server" DataSourceID="srcGridTransporter"
                                                                            AutoGenerateColumns="False" OnRowCreated="GrdTransporter_RowCreated" Width="50%"
                                                                            CssClass="someClass" PageSize="5" Style="font-family: 'Trebuchet MS'; font-size: 11px;"
                                                                            OnRowCommand="GrdTransporter_RowCommand" AllowPaging="True" DataKeyNames="TransporterID"
                                                                            OnRowUpdating="GrdTransporter_RowUpdating" EnableViewState="False" OnRowUpdated="GrdTransporter_RowUpdated"
                                                                            OnDataBound="GrdTransporter_DataBound">
                                                                              <HeaderStyle Height="30px" HorizontalAlign="Center" Font-Bold="true" BackColor="#cccccc" BorderColor="Gray" Font-Size="Small" />
                                                                              <RowStyle Font-Bold="true" HorizontalAlign="Center" Height="30px" Font-Size="Small" ForeColor="#0567AE" />
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Transporter" HeaderStyle-BorderColor="Gray">
                                                                                    <ItemStyle Width="80%" HorizontalAlign="Left" />
                                                                                    <FooterStyle Width="80%" HorizontalAlign="Left" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblTransporter" runat="server" Text='<%# Bind("Transporter") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <EditItemTemplate>
                                                                                        <asp:TextBox ID="txtTransporter" runat="server" Text='<%# Bind("Transporter") %>'
                                                                                            CssClass="cssTextBox" Width="95%"></asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ID="rvTransporter" runat="server" ControlToValidate="txtTransporter"
                                                                                            Display="Dynamic" EnableClientScript="False" ErrorMessage="Transporter is mandatory">*</asp:RequiredFieldValidator>
                                                                                    </EditItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:TextBox ID="txtAddTransporter" runat="server" CssClass="cssTextBox" Width="95%"></asp:TextBox><asp:RequiredFieldValidator
                                                                                            ID="rvAddTransporter" runat="server" ControlToValidate="txtAddTransporter" Display="Dynamic"
                                                                                            EnableClientScript="true" ErrorMessage="Transporter is mandatory">*</asp:RequiredFieldValidator>
                                                                                    </FooterTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Edit" HeaderStyle-BorderColor="Gray">
                                                                                    <ItemStyle Width="20%" HorizontalAlign="Center" />
                                                                                    <FooterStyle Width="20%" HorizontalAlign="Center" />
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
                                                                                <table>
                                                                                    <tr>
                                                                                        <td>
                                                                                            Goto Page
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="ddlPageSelector" runat="server" AutoPostBack="true" SkinID="skinPagerDdlBox2">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td style="width: 5px">
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Button Text="" CommandName="Page" CommandArgument="First" runat="server" CssClass="NewFirst"
                                                                                                EnableTheming="false" Width="22px" Height="18px" ID="btnFirst" />
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Button Text="" CommandName="Page" CommandArgument="Prev" runat="server" CssClass="NewPrev"
                                                                                                EnableTheming="false" Width="22px" Height="18px" ID="btnPrevious" />
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Button Text="" CommandName="Page" CommandArgument="Next" runat="server" CssClass="NewNext"
                                                                                                EnableTheming="false" Width="22px" Height="18px" ID="btnNext" />
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Button Text="" CommandName="Page" CommandArgument="Last" runat="server" CssClass="NewLast"
                                                                                                EnableTheming="false" Width="22px" Height="18px" ID="btnLast" />
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </PagerTemplate>
                                                                        </asp:GridView>
                                                                        <asp:ObjectDataSource ID="srcGridTransporter" runat="server" InsertMethod="InsertUnitRecord"
                                                                            SelectMethod="GetTransporterData" TypeName="BusinessLogic">
                                                                            <SelectParameters>
                                                                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                            </SelectParameters>
                                                                        </asp:ObjectDataSource>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table width="100%">
                                                            <tr>
                                                                <td align="center">
                                                                      <asp:Button ID="lnkBtnAddTransporter" runat="server" OnClick="lnkBtnAddTransporter_Click"
                                                                            EnableTheming="false" CssClass="ButtonAdd66" Text=""></asp:Button>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel ID="TabPanel6" runat="server" HeaderText="Posting Method">
                                        <ContentTemplate>
                                            <div style="width: 100%" align="center">
                                                <table style="width: 955px; border: 0px solid #5078B3;" align="center" cellpadding="3"
                                                            cellspacing="3">
                                                    <tr>
                                                        <td colspan="4">
                                                            <div class="lblFont" style="border: solid 1px black; background-color: aliceblue;
                                                                font-weight: bold; color: Red;">
                                                                Attention : By Default all ledger will post as Receipt while doing Multiple payment in Sales. If you need to 
                                                                Post the ledger as Journal then need to make configuration here.
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr style="height:10px">

                                                    </tr>
                                                    <tr>
                                                        <td style="width: 35%;">
                                                        </td>
                                                        <td style="width: 20%;" class="ControlDrpBorder">
                                                            <asp:DropDownList ID="ddBank1" runat="server" AppendDataBoundItems="true" DataTextField="LedgerName" BackColor = "#e7e7e7" CssClass="drpDownListMedium"
                                                                DataValueField="LedgerID" Width="100%" style="border: 1px solid #e7e7e7" height="26px"
                                                                TabIndex="12" ValidationGroup="AddPosting" >
                                                                <asp:ListItem style="background-color: #e7e7e7" Text="Select Ledger" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            
                                                        </td>
                                                        <td style="width: 20%" class="tblLeft">
                                                            <asp:Button ID="btnAddPostLedger" ValidationGroup="AddPosting" Width="100px" runat="server" Text="Add Ledger" SkinID="skinButtonCol2"
                                                                OnClick="btnAddPostLedger_Click" />
                                                            <asp:RequiredFieldValidator ValidationGroup="AddPosting" ID="RequiredFieldValidator3"
                                                                    ForeColor="Red" runat="server" Text="*" ErrorMessage="Ledger is Required"  ControlToValidate="ddBank1"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td style="width: 25%; text-align: right">
                                                        </td>
                                                    </tr>
                                                    <tr style="height:6px">

                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <div style="width: 100%;" align="center">
                                                                <asp:GridView ID="GridPosting" CssClass="someClass" runat="server" EmptyDataText="No Ledger found!"
                                                                    DataKeyNames="Ledger" AutoGenerateColumns="False" AllowPaging="True" Width="50%"
                                                                    OnRowCreated="GridPosting_RowCreated" OnRowDeleted="GridPosting_RowDeleted" DataSourceID="sourceposting">
                                                                      <HeaderStyle Height="30px" HorizontalAlign="Center" Font-Bold="true" BackColor="#cccccc" BorderColor="Gray" Font-Size="Small" />
                                                                    <RowStyle Font-Bold="true" HorizontalAlign="Center" Height="30px" Font-Size="Small" ForeColor="#0567AE" />
                                                                    <Columns>
                                                                        <asp:BoundField DataField="Ledger" HeaderText="Ledger" HeaderStyle-BorderColor="Gray">
                                                                            <ItemStyle Width="70%" Wrap="False" />
                                                                        </asp:BoundField>
                                                                        <asp:TemplateField ItemStyle-CssClass="command" ItemStyle-HorizontalAlign="Center"
                                                                            HeaderStyle-BorderColor="Gray" HeaderText="Delete">
                                                                            <ItemTemplate>
                                                                                <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to Delete this Ledger?"
                                                                                    runat="server">
                                                                                </cc1:ConfirmButtonExtender>
                                                                                <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" CommandName="Delete"></asp:ImageButton>
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
                                                                                    <asp:DropDownList ID="ddlPageSelector" style="border:1px solid blue"  Width="75px"  height="23px" BackColor="#BBCAFB" runat="server" AutoPostBack="true">
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
                                                                <asp:ObjectDataSource ID="sourceposting" runat="server" DeleteMethod="DelPostingLedger"
                                                                    SelectMethod="GetPostingLedger" TypeName="BusinessLogic">
                                                                    <SelectParameters>
                                                                        <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                    </SelectParameters>
                                                                    <DeleteParameters>
                                                                        <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                        <asp:Parameter Name="Ledger" Type="String" />
                                                                    </DeleteParameters>
                                                                </asp:ObjectDataSource>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
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
