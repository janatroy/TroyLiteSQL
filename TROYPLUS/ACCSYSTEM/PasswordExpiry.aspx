<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true" EnableEventValidation="false" Title="Password Expiry"
    CodeFile="PasswordExpiry.aspx.cs" Inherits="Password_Expiry" %>

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
            width: 98.5%;
        }
    </style>

    <table style="width: 100%">
        <tr style="width: 100%">
            <td style="width: 99%;">

                <div class="mainConBody">
                    <table style="width: 99.7%;margin: 1px 0px 0px 2px;" cellpadding="2px" cellspacing="2px" class="searchbg">
                        <tr style="vertical-align: middle">
                            <td style="width: 1%"></td>
                            <td style="width: 38%; font-size: 22px; color: #000000;">Your Password has been expired
                            </td>
                            <td style="width: 14%"></td>
                            <td style="width: 10%; color: #000000;" align="right"></td>
                            <td style="width: 19%"></td>
                            <td style="width: 18%"></td>

                        </tr>
                    </table>
                </div>
                <table style="text-align: left; border: 0px solid #5078B3; padding-left: 3px; width: 980px" cellpadding="0"
                    cellspacing="0">
                    <tr>
                        <td>
                            <div align="center" style="width: 992px; text-align: left">
                                <cc1:TabContainer ID="tabs2" runat="server" ActiveTabIndex="0" CssClass="fancy fancy-green">
                                    <cc1:TabPanel ID="tabMaster" runat="server" HeaderText="Change Password">
                                        <ContentTemplate>
                                            <div style="text-align: left;">
                                                <table style="width: 960px; font-size: 11px; font-family: 'Trebuchet MS';" cellpadding="3"
                                                    cellspacing="1">
                                                    <tr>
                                                        <%--<asp:HiddenField ID="SettingsID" Visible="false" runat="server"></asp:HiddenField>--%>

                                                        <td style="width: 25%" class="ControlLabel">Old Password :
                                                        </td>
                                                        <td style="width: 25%" class="ControlTextBox3">
                                                            <asp:TextBox ID="txtOldPwd" TextMode="Password" runat="server" Width="300px" CssClass="cssTextBox"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="reqOldPwd" ForeColor="Red" ValidationGroup="adminInfo"
                                                                ErrorMessage="Old Password is mandatory" Font-Bold="true" runat="server" Text="*"
                                                                ControlToValidate="txtOldPwd"></asp:RequiredFieldValidator>
                                                        </td>

                                                        <td style="width: 25%"></td>
                                                        <td style="width: 25%"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%;" class="ControlLabel">New Passowrd :
                                                        </td>
                                                        <td style="width: 25%" class="ControlTextBox3">
                                                            <asp:TextBox ID="txtNewPwd" TextMode="Password" runat="server" CssClass="cssTextBox"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="ReqNewPwd" ForeColor="Red" ValidationGroup="adminInfo"
                                                                ErrorMessage="New Password is mandatory" Font-Bold="true" runat="server" Text="*"
                                                                ControlToValidate="txtNewPwd"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td style="width: 25%"></td>
                                                        <td style="width: 25%"></td>
                                                    </tr>

                                                    <tr>
                                                        <td style="width: 25%" class="ControlLabel">Confirm Password :
                                                        </td>
                                                        <td style="width: 25%" class="ControlTextBox3">
                                                            <asp:TextBox TextMode="Password" ID="txtConPwd" runat="server" CssClass="cssTextBox"
                                                                Width="300px" Height="16px"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ForeColor="Red" ValidationGroup="adminInfo"
                                                                ErrorMessage="Confirm Password is mandatory" Font-Bold="true" runat="server" Text="*"
                                                                ControlToValidate="txtConPwd"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td style="width: 25%"></td>
                                                        <td style="width: 25%"></td>
                                                    </tr>

                                                    <tr>
                                                        <td style="height: 10px"></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td style="width: 30%;"></td>
                                                                    <td style="width: 5%;">
                                                                        <asp:Button ID="btnPwdSave" runat="server" SkinID="skinBtnSave" ValidationGroup="adminInfo"
                                                                            CssClass="Updatebutton1231" EnableTheming="false" OnClick="btnPwdSave_Click" />
                                                                    </td>
                                                                    <td style="width: 5%;">
                                                                        <asp:Button ID="btnPwdCancel" runat="server" CausesValidation="False"
                                                                            CssClass="cancelbutton6" EnableTheming="false" SkinID="skinBtnCancel" OnClick="btnPwdCancel_Click"></asp:Button>
                                                                        <asp:ValidationSummary ID="valSum" DisplayMode="BulletList" ShowMessageBox="true"
                                                                            ShowSummary="false" HeaderText="Validation Messages" ValidationGroup="adminInfo"
                                                                            Font-Names="'Trebuchet MS'" Font-Size="12" runat="server" />
                                                                    </td>
                                                                    <td style="width: 60%;"></td>
                                                                </tr>
                                                            </table>
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
                                </cc1:TabContainer>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

</asp:Content>
