<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="Config.aspx.cs" Inherits="Config" Title="Administration > Users Lock" %>

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
                                    <td style="width: 45%; font-size: 22px; color: White;">Define Recipients of Email And SMS
                                    </td>
                                    <td style="width: 1%">
                                        <div style="text-align: right;">
                                            <asp:Panel ID="pnlSearch" runat="server">
                                            </asp:Panel>



                                        </div>

                                    </td>
                                    <td style="width: 15%; color: White;" align="right">Screen Name
                                    </td>
                                    <td style="width: 20%" class="NewBox">
                                        <asp:TextBox ID="txtScreenName" runat="server" SkinID="skinTxtBoxSearch"></asp:TextBox>
                                    </td>
                                    <td class="tblLeftNoPad" style="width: 15%">
                                        <asp:Button ID="lnkBtnSearchId" runat="server" OnClick="lnkBtnSearch_Click" Text=""
                                            ToolTip="Click here to submit" CssClass="ButtonSearch6" EnableTheming="false" TabIndex="3" />
                                    </td>
                                    <td style="width: 15%" class="tblLeftNoPad">
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
                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" Font-Names="'Trebuchet MS'" Font-Size="12pt"
                                                HeaderText="Validation Messages" ShowMessageBox="true" ShowSummary="false" ValidationGroup="contact" />
                        <asp:Panel ID="purchasePanel" runat="server" Style="width: 80%; display: none">
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
                                                                        <td>Define Recipients of Email And SMS
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="5">
                                                                <table cellpadding="0" cellspacing="0"
                                                                    width="100%">
                                                                    <tr style="height: 5px">
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="5">
                                                                            <table width="100%">
                                                                                <tr style="height: 5%">
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 10%"></td>
                                                                                    <td class="ControlLabelproject" style="width: 15%">Name of Screen *
                                                                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="drpScreenName"
                                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Screen Name is Mandatory"  ValidationGroup="contact"
                                                                                        Operator="GreaterThan" ValueToCompare="0">*</asp:CompareValidator>
                                                                                    </td>
                                                                                    <td class="ControlDrpBorder" style="width: 18%">
                                                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                                                            <ContentTemplate>
                                                                                                <asp:DropDownList ID="drpScreenName" runat="server" AutoPostBack="True" Width="100%" DataSourceID="srcScreens" CssClass="drpDownListMedium" BackColor="#e7e7e7"
                                                                                                    DataValueField="Sno" OnSelectedIndexChanged="drpScreenName_SelectedIndexChanged" Style="border: 1px solid #e7e7e7" Height="26px"
                                                                                                    DataTextField="ScreenName" AppendDataBoundItems="True">
                                                                                                    <asp:ListItem Text="Select Screen Name" Value="0"></asp:ListItem>
                                                                                                </asp:DropDownList>
                                                                                            </ContentTemplate>
                                                                                            <Triggers>
                                                                                                <asp:AsyncPostBackTrigger ControlID="drpScreenNo" EventName="SelectedIndexChanged" />
                                                                                            </Triggers>
                                                                                        </asp:UpdatePanel>

                                                                                    </td>
                                                                                    <td class="ControlLabelproject" style="width: 24%">Screen Number *
                                                                                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="drpScreenNo"
                                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Screen No is Mandatory" ValidationGroup="contact"
                                                                                        Operator="GreaterThan" ValueToCompare="0">*</asp:CompareValidator>
                                                                                    </td>
                                                                                    <td class="ControlDrpBorder" style="width: 22%">
                                                                                        <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                                                                                            <ContentTemplate>
                                                                                                <asp:DropDownList ID="drpScreenNo" runat="server" AutoPostBack="True" Width="100%" DataSourceID="srcScreens" CssClass="drpDownListMedium" BackColor="#e7e7e7"
                                                                                                    DataValueField="Sno" OnSelectedIndexChanged="drpScreenNo_SelectedIndexChanged" Style="border: 1px solid #e7e7e7" Height="26px"
                                                                                                    DataTextField="ScreenNo" AppendDataBoundItems="True">
                                                                                                    <asp:ListItem Text="Select Screen No" Value="0"></asp:ListItem>
                                                                                                </asp:DropDownList>
                                                                                            </ContentTemplate>
                                                                                            <Triggers>
                                                                                                <asp:AsyncPostBackTrigger ControlID="drpScreenName" EventName="SelectedIndexChanged" />
                                                                                            </Triggers>
                                                                                        </asp:UpdatePanel>
                                                                                    </td>
                                                                                    <td style="width: 12%"></td>
                                                                                </tr>

                                                                                <tr style="height: 2px">
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 10%"></td>

                                                                                    <td class="ControlLabelproject" style="width: 15%">Email Content *
                                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator123" runat="server" ControlToValidate="txtEmailContent"
                                                                                                                        ErrorMessage="Email Content is mandatory" ValidationGroup="contact">*</asp:RequiredFieldValidator>
                                                                                    </td>
                                                                                    <td class="ControlTextBox3" style="width: 18%">
                                                                                        <asp:TextBox ID="txtEmailContent" runat="server" Text='<%# Bind("EmailContent") %>' TextMode="MultiLine" Height="40px" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                    </td>
                                                                                    <td class="ControlLabelproject" style="width: 24%">SMS Content *
                                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtSMSContent"
                                                                                                                        ErrorMessage="SMS Content is mandatory" ValidationGroup="contact">*</asp:RequiredFieldValidator>
                                                                                    </td>
                                                                                    <td class="ControlTextBox3" style="width: 22%">
                                                                                        <asp:TextBox ID="txtSMSContent" runat="server" Text='<%# Bind("SMSContent") %>' TextMode="MultiLine" Height="40px" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                    </td>
                                                                                    <td style="width: 12%"></td>
                                                                                </tr>

                                                                                <tr style="height: 2px">
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 10%"></td>
                                                                                    <td class="ControlLabelproject" style="width: 15%">Email Subject *
                                                                                                                <asp:RequiredFieldValidator ID="rvModel" runat="server" ControlToValidate="txtEmailSubject"
                                                                                                                        ErrorMessage="Email Subject is mandatory" ValidationGroup="contact">*</asp:RequiredFieldValidator>
                                                                                    </td>
                                                                                    <td class="ControlTextBox3" style="width: 18%">
                                                                                        <asp:TextBox ID="txtEmailSubject" runat="server" Text='<%# Bind("EmailSubject") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                    </td>

                                                                                    <td style="width: 23%"></td>
                                                                                </tr>
                                                                                <tr style="height: 2px">
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="6">
                                                                                        <table style="width: 100%">
                                                                                            <tr>
                                                                                                <td style="background-color: #cccccc; border: 1px solid gray">

                                                                                                    <asp:Label ID="Label2" runat="server" Text="Replaceable Recipients for Email and SMS" Font-Bold="true" ForeColor="Black" Font-Size="Medium">

                                                                                                    </asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="6">
                                                                                        <table style="width: 100%; border: 1px solid gray">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <div id="div2" runat="server" style="height: 180px; overflow: scroll">

                                                                                                        <asp:GridView ID="GridView1" AutoGenerateColumns="False" ShowFooter="True"
                                                                                                            OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting" GridLines="None" SaveButtonID="SaveButton" runat="server"
                                                                                                            Width="100%">
                                                                                                            <%--<RowStyle CssClass="dataRow" />
                                                                                                <SelectedRowStyle CssClass="SelectdataRow" />
                                                                                                <AlternatingRowStyle CssClass="altRow" />
                                                                                                <EmptyDataRowStyle CssClass="HeadataRow" Font-Bold="true" />
                                                                                                <HeaderStyle CssClass="HeadataRow" Wrap="false" />
                                                                                                <FooterStyle CssClass="dataRow" />--%>
                                                                                                            <HeaderStyle Height="30px" HorizontalAlign="Center" />
                                                                                                            <RowStyle HorizontalAlign="Center" Height="30px" />
                                                                                                            <Columns>
                                                                                                                <asp:TemplateField HeaderText="" HeaderStyle-Width="19%" HeaderStyle-Font-Size="Small">
                                                                                                                    <ItemTemplate>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:BoundField DataField="RowNumber" HeaderText="#" HeaderStyle-Width="5%" />
                                                                                                                <asp:TemplateField HeaderText="" HeaderStyle-Width="1%" HeaderStyle-Font-Size="Small">
                                                                                                                    <ItemTemplate>
                                                                                                                        <%--<asp:DropDownList ID="drpType" Width="98%" runat="server" style="border:1px solid #cccccc" ForeColor="#006699" Height="24px" BackColor="White" Font-Bold="true" AutoPostBack="true" OnSelectedIndexChanged="drpType_SelectedIndexChanged">
                                                                                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                                                                                <asp:ListItem Text="Internal" Value="1"></asp:ListItem>
                                                                                                                <asp:ListItem Text="External" Value="2"></asp:ListItem>
                                                                                                            </asp:DropDownList>--%>
                                                                                                                        <asp:RadioButtonList ID="drpType1" runat="server" Visible="false"
                                                                                                                            Style="border: 1px solid #cccccc" ForeColor="Black" Width="98%" Font-Size="Small" RepeatDirection="Horizontal" Height="24px" BackColor="#cccccc" Font-Bold="true">
                                                                                                                            <asp:ListItem Text="Staff" Value="1"></asp:ListItem>
                                                                                                                            <asp:ListItem Text="OutSider" Value="2"></asp:ListItem>
                                                                                                                        </asp:RadioButtonList>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                
                                                                                                                <%--<asp:TemplateField HeaderText="#" HeaderStyle-Width="5%" HeaderStyle-Font-Size="Small">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lbl123" runat="server" Text='<%# Eval("RowNumber")%>'>
                                                                                                            </asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>--%>
                                                                                                                
                                                                                                                <asp:TemplateField HeaderText="Replacement Name" HeaderStyle-ForeColor="Black" HeaderStyle-Font-Size="Small" HeaderStyle-Width="20%">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:DropDownList ID="drpName1" Width="98%" Style="border: 1px solid #cccccc" ForeColor="#006699" Height="24px" runat="server" AppendDataBoundItems="true" BackColor="White" Font-Bold="true">
                                                                                                                            <asp:ListItem Text="Select Replacement Name" Value="0"></asp:ListItem>
                                                                                                                            <asp:ListItem Text="Customer" Value="1"></asp:ListItem>
                                                                                                                            <asp:ListItem Text="Supplier" Value="2"></asp:ListItem>
                                                                                                                            <asp:ListItem Text="Bank" Value="3"></asp:ListItem>
                                                                                                                            <asp:ListItem Text="Expense" Value="4"></asp:ListItem>
                                                                                                                            <asp:ListItem Text="Ledger" Value="5"></asp:ListItem>
                                                                                                                            <asp:ListItem Text="Sales Executive" Value="6"></asp:ListItem>
                                                                                                                        </asp:DropDownList>
                                                                                                                    </ItemTemplate>
                                                                                                                    <FooterStyle HorizontalAlign="left" />
                                                                                                                    <FooterTemplate>
                                                                                                                        <asp:Button ID="ButtonAdd2" runat="server" AutoPostback="false" EnableTheming="false"
                                                                                                                            Text="Add New" OnClick="ButtonAdd2_Click" Width="100%" />
                                                                                                                    </FooterTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="5%" HeaderStyle-Font-Size="Small" HeaderText="SMS" HeaderStyle-BorderColor="Gray">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:CheckBox ID="chkboxMobile1" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px"></asp:CheckBox>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="5%" HeaderStyle-Font-Size="Small" HeaderText="Email" HeaderStyle-BorderColor="Gray">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:CheckBox ID="chkboxEmail1" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px"></asp:CheckBox>
                                                                                                                    </ItemTemplate>

                                                                                                                </asp:TemplateField>
                                                                                                                <asp:CommandField ShowDeleteButton="True" ButtonType="Button" ItemStyle-HorizontalAlign="Left" />
                                                                                                                <asp:TemplateField HeaderText="" HeaderStyle-Width="15%" HeaderStyle-Font-Size="Small">
                                                                                                                    <ItemTemplate>
                                                                                                                    </ItemTemplate>

                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField HeaderText="" HeaderStyle-Width="21%" HeaderStyle-Font-Size="Small">
                                                                                                                    <ItemTemplate>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>

                                                                                                                <%--<asp:TemplateField HeaderText="" HeaderStyle-Width="5%" HeaderStyle-Font-Size="Small">
                                                                                                        <ItemTemplate>
                                                                                                           
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>--%>
                                                                                                            </Columns>
                                                                                                        </asp:GridView>

                                                                                                    </div>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>

                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="6">
                                                                                        <table style="width: 100%">
                                                                                            <tr>
                                                                                                <td style="background-color: #cccccc; border: 1px solid gray">

                                                                                                    <asp:Label ID="Label1" runat="server" Text="Recipients of Email and SMS" Font-Bold="true" ForeColor="Black" Font-Size="Medium">

                                                                                                    </asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="6">
                                                                                        <div id="div" runat="server" style="height: 180px; border: 1px solid Gray; overflow: scroll">

                                                                                            <asp:GridView ID="GrdViewItem" AutoGenerateColumns="False" ShowFooter="True"
                                                                                                OnRowDataBound="GrdViewItem_RowDataBound" OnRowDeleting="GrdViewItem_RowDeleting" GridLines="None" SaveButtonID="SaveButton" runat="server"
                                                                                                Width="100%">
                                                                                                <%--<RowStyle CssClass="dataRow" />
                                                                                                <SelectedRowStyle CssClass="SelectdataRow" />
                                                                                                <AlternatingRowStyle CssClass="altRow" />
                                                                                                <EmptyDataRowStyle CssClass="HeadataRow" Font-Bold="true" />
                                                                                                <HeaderStyle CssClass="HeadataRow" Wrap="false" />
                                                                                                <FooterStyle CssClass="dataRow" />--%>
                                                                                                <HeaderStyle Height="30px" HorizontalAlign="Center" />
                                                                                                <RowStyle HorizontalAlign="Center" Height="30px" />
                                                                                                <Columns>
                                                                                                    <asp:BoundField DataField="RowNumber" HeaderText="#" HeaderStyle-Width="5%" />
                                                                                                    <asp:TemplateField HeaderText="Origin of Recipient" HeaderStyle-Width="20%" HeaderStyle-Font-Size="Small">
                                                                                                        <ItemTemplate>
                                                                                                            <%--<asp:DropDownList ID="drpType" Width="98%" runat="server" style="border:1px solid #cccccc" ForeColor="#006699" Height="24px" BackColor="White" Font-Bold="true" AutoPostBack="true" OnSelectedIndexChanged="drpType_SelectedIndexChanged">
                                                                                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                                                                                <asp:ListItem Text="Internal" Value="1"></asp:ListItem>
                                                                                                                <asp:ListItem Text="External" Value="2"></asp:ListItem>
                                                                                                            </asp:DropDownList>--%>
                                                                                                            <asp:RadioButtonList ID="drpType" runat="server"
                                                                                                                Style="border: 1px solid #cccccc" ForeColor="Black" Width="98%" Font-Size="Small" RepeatDirection="Horizontal" Height="24px" BackColor="#cccccc" Font-Bold="true" AutoPostBack="true" OnSelectedIndexChanged="drpType_SelectedIndexChanged">
                                                                                                                <asp:ListItem Text="Staff" Value="1"></asp:ListItem>
                                                                                                                <asp:ListItem Text="Outsider" Value="2"></asp:ListItem>
                                                                                                            </asp:RadioButtonList>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Name of Person" HeaderStyle-ForeColor="Black" HeaderStyle-Font-Size="Small" HeaderStyle-Width="20%">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:DropDownList ID="drpName" Width="98%" Style="border: 1px solid #cccccc" ForeColor="#006699" Height="24px" runat="server" DataTextField="empFirstName" DataValueField="empno" AppendDataBoundItems="true" BackColor="White" Font-Bold="true" AutoPostBack="true" OnSelectedIndexChanged="drpName_SelectedIndexChanged">
                                                                                                            </asp:DropDownList>
                                                                                                            <asp:TextBox ID="txtName" runat="server" Text="" Width="98%" Height="21px" BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1px" ForeColor="#006699" Font-Bold="true" Visible="false"></asp:TextBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="5%" HeaderStyle-Font-Size="Small" HeaderText="SMS" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxMobile" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px"></asp:CheckBox>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="5%" HeaderStyle-Font-Size="Small" HeaderText="Email" HeaderStyle-BorderColor="Gray">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkboxEmail" runat="server" Style="color: Black" Text="" Font-Names="arial" Font-Size="11px"></asp:CheckBox>
                                                                                                        </ItemTemplate>

                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Mobile Number" HeaderStyle-Width="15%" HeaderStyle-Font-Size="Small" ItemStyle-HorizontalAlign="Left">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblMobileNo" runat="server" Width="96%" Height="21px" BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1px" ForeColor="#006699" Font-Bold="true">
                                                                                                            </asp:Label>
                                                                                                            <asp:TextBox ID="txtMobileNo" runat="server" Text="" Width="96%" Height="21px" BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1px" ForeColor="#006699" Font-Bold="true" Visible="false"></asp:TextBox>
                                                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender312" runat="server" TargetControlID="txtMobileNo"
                                                                                                                                    ValidChars="." FilterType="Numbers, Custom" />
                                                                                                        </ItemTemplate>
                                                                                                        <FooterStyle HorizontalAlign="left" />
                                                                                                        <FooterTemplate>
                                                                                                            <asp:Button ID="ButtonAdd1" runat="server" AutoPostback="false" EnableTheming="false"
                                                                                                                Text="Add New" OnClick="ButtonAdd1_Click" Width="100%" />
                                                                                                        </FooterTemplate>
                                                                                                    </asp:TemplateField>

                                                                                                    <asp:TemplateField HeaderText="Email Id" HeaderStyle-Width="21%" HeaderStyle-Font-Size="Small" ItemStyle-HorizontalAlign="Left">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblEmailId" runat="server" Width="96%" Height="21px" BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1px" ForeColor="#006699" Font-Bold="true">

                                                                                                            </asp:Label>
                                                                                                            <asp:TextBox ID="txtEmailId" runat="server" Text="" Width="96%" Height="21px" BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1px" ForeColor="#006699" Font-Bold="true" Visible="false"></asp:TextBox>
                                                                                                        </ItemTemplate>

                                                                                                    </asp:TemplateField>

                                                                                                    <asp:CommandField ShowDeleteButton="True" ButtonType="Button" />
                                                                                                    <%--<asp:TemplateField HeaderText="" HeaderStyle-Width="5%" HeaderStyle-Font-Size="Small">
                                                                                                        <ItemTemplate>
                                                                                                           
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>--%>
                                                                                                </Columns>
                                                                                            </asp:GridView>

                                                                                        </div>
                                                                                    </td>

                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td colspan="4">
                                                                            <table width="100%">
                                                                                <tr>
                                                                                    <td style="width: 34%"></td>
                                                                                    <td style="width: 16%">
                                                                                        <asp:Button ID="cmdSave" runat="server" Text="" CssClass="savebutton1231" CausesValidation="true"
                                                                                            EnableTheming="false" OnClick="cmdSave_Click" SkinID="skinBtnSave" ValidationGroup="contact"/>
                                                                                        <asp:Button ID="UpdateButton" runat="server" SkinID="skinBtnSave" CausesValidation="true" ValidationGroup="contact"
                                                                                            OnClick="UpdateButton_Click" CssClass="Updatebutton1231" EnableTheming="false"></asp:Button>
                                                                                    </td>
                                                                                    <td style="width: 16%">
                                                                                        <asp:Button ID="btnCancel" runat="server" Text="" CssClass="cancelbutton6" EnableTheming="false"
                                                                                            Visible="true" OnClick="cmdCancel_Click" SkinID="skinBtnCancel" CausesValidation="False" />
                                                                                    </td>
                                                                                    <td style="width: 34%">
                                                                                        <asp:ObjectDataSource ID="srcScreens" runat="server" SelectMethod="ListScreens"
                                                                                            TypeName="BusinessLogic">
                                                                                            <SelectParameters>
                                                                                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                                            </SelectParameters>
                                                                                        </asp:ObjectDataSource>
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
                                        <asp:GridView ID="GrdViewCust" DataKeyNames="Id" GridLines="Both" Width="100.3%" OnRowDataBound="GrdViewCust_RowDataBound" EmptyDataText="No Screens Found"
                                            runat="server" AutoGenerateColumns="False" OnRowCreated="GrdViewCust_RowCreated" OnSelectedIndexChanged="GrdViewCust_SelectedIndexChanged"
                                            AllowPaging="true" CssClass="someClass" OnRowDeleting="GrdViewCust_RowDeleting">
                                            <RowStyle BorderWidth="1" HorizontalAlign="Center" />
                                            <HeaderStyle Height="30px" HorizontalAlign="Center" Font-Bold="true" BackColor="#cccccc" BorderColor="Gray" Font-Size="Small" />
                                            <RowStyle Font-Bold="true" HorizontalAlign="Center" Height="30px" Font-Size="Small" ForeColor="#0567AE" />
                                            <Columns>
                                                <asp:BoundField DataField="RowNumber" HeaderText="#" HeaderStyle-Width="5%" />
                                                <asp:BoundField DataField="ScreenNo" HeaderText="Screen No" HeaderStyle-BorderColor="Gray"></asp:BoundField>
                                                <asp:BoundField DataField="ScreenName" HeaderText="Screen Name" HeaderStyle-BorderColor="Gray" ItemStyle-HorizontalAlign="left"></asp:BoundField>
                                                <asp:BoundField DataField="EmailSubject" HeaderText="Email Subject" HeaderStyle-BorderColor="Gray" ItemStyle-HorizontalAlign="left"></asp:BoundField>
                                                <asp:BoundField DataField="EmailContent" HeaderText="Email Content" HeaderStyle-BorderColor="Gray" ItemStyle-HorizontalAlign="left"></asp:BoundField>
                                                <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="50px" HeaderText="Edit" HeaderStyle-BorderColor="Gray">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Select" />
                                                        <asp:ImageButton ID="btnEditDisabled" Enabled="false" SkinID="editDisable" runat="Server"></asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-BorderColor="Gray"
                                                    HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" OnClientClick="return confirm('Are you sure you want to delete the Screen?');"
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
                                    <%--<asp:ObjectDataSource ID="GridSource" runat="server" SelectMethod="GetScreen"
                                        TypeName="BusinessLogic">
                                       
                                    </asp:ObjectDataSource>--%>
                                </td>
                            </tr>

                        </table>
                    </td>
                </tr>
                <tr>
                    <td width="100%">
                        <table width="100%">
                            <tr>
                                <td width="40%">
                                    
                                </td>
                                <td width="20%">
                                    <asp:Button ID="lnkBtnAdd" runat="server" OnClick="lnkBtnAdd_Click" CssClass="ButtonAdd66" CausesValidation="false"
                                        EnableTheming="false" Width="80px" Text=""></asp:Button>
                                </td>
                                <td width="40%">
                                    
                                </td>
                            </tr>
                        </table>
                    </td>

                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
