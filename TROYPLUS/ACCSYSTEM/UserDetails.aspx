<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="UserDetails.aspx.cs" Inherits="UserDetails" Title="User Details" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <script type="text/javascript">

        function pageLoad() {
            //  get the behavior associated with the tab control
            var tabContainer = $find('ctl00_cplhControlPanel_tabEditContol_tabEditProdMaster_tabs2');
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

    <table style="width: 950px">
        <tr>
        <td style="width: 10px">
        </td>
            <td>
    
    <div style="text-align: center;">
        <cc1:TabContainer ID="tabEditContol" runat="server" Width="980px" ActiveTabIndex="0">
            <cc1:TabPanel ID="tabEditProdMaster" runat="server" HeaderText="User Details">
                <ContentTemplate>
                    <%--<div class="mainConDiv" id="IdmainConDiv">--%>
                        <div class="mainConHd" style="Width:100%">
                            <table class="headerPopUp" style="width: 100%;" cellspacing="0" cellpadding="0" border="0">
                                <tr style="height:5px">

                                </tr>
                                <tr>
                                    <td colspan="4" align="center" valign="middle">
                                        User Details
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="mainConBody" style="Width:100%">
                            <table style="Width:100%">
                                <tr style="height:5px">

                                </tr>
                                <tr>
                                    <td class="ControlLabel" style="width: 20%">
                                        User Name *
                                        <asp:RequiredFieldValidator ID="rvUserName" runat="server" ControlToValidate="txtUserName"
                                            Display="Dynamic">User Name is mandatory</asp:RequiredFieldValidator>
                                    </td>
                                    <td class="ControlTextBox3" style="width: 15%">
                                        <asp:TextBox ID="txtUserName" runat="server" CssClass="cssTextBox" Width="100px" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td class="ControlLabel" style="width: 5%">
                                        Email
                                    </td>
                                    <td class="ControlTextBox3" style="width: 15%">
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="cssTextBox" Width="100px"></asp:TextBox>
                                    </td>
                                    <td style="width:15%" align="left">
                                        &nbsp; <asp:CheckBox ID="chkboxdatelock" runat="server" Text="Date Lock" Font-Size="15px" AutoPostBack="true" />
                                    </td>
                                </tr>
                                <tr style="height:5px">

                                </tr>
                            </table>
                            <table>
                               <tr>
                                    <td style="width: 25%">
                                    </td>
                                    <td style="width: 25%">
                                        <asp:CheckBox runat="server" ID="chkAccLocked" Visible="False" />
                                    </td>
                                    <td style="width: 25%">
                                    </td>
                                    <td style="width: 25%">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        
                                        <cc1:TabContainer ID="tabs2" runat="server" Width="100%" BehaviorID="tabTest">
                                            <cc1:TabPanel ID="tabMaster" runat="server" HeaderText="Master Access">
                                                <ContentTemplate>
                                                    <div style="text-align: left; width: 920px">
                                                        <asp:CheckBoxList ID="chckMaster" RepeatColumns="3" CellPadding="5" runat="server"
                                                            Width="100%">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </ContentTemplate>
                                            </cc1:TabPanel>
                                            <cc1:TabPanel ID="tabBilling" runat="server" HeaderText="Billing Access">
                                                <ContentTemplate>
                                                    <div style="text-align: left; width: 920px">
                                                        <asp:CheckBoxList ID="chkBilling" RepeatColumns="3" Width="100%" CellPadding="5"
                                                            RepeatLayout="Table" runat="server">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </ContentTemplate>
                                            </cc1:TabPanel>
                                            <cc1:TabPanel ID="tabRestMgmt" runat="server" RepeatLayout="Table" HeaderText="Resource Management">
                                                <ContentTemplate>
                                                    <div style="text-align: left; width: 920px">
                                                        <asp:CheckBoxList ID="chkResMgmt" runat="server" Width="100%" CellPadding="3" RepeatColumns="3"
                                                            RepeatLayout="Table">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </ContentTemplate>
                                            </cc1:TabPanel>
                                            <cc1:TabPanel ID="tabReports" runat="server" RepeatLayout="Table" HeaderText="Reports Access">
                                                <ContentTemplate>
                                                    <div style="text-align: left; width: 920px">
                                                        <asp:CheckBoxList ID="chkReport" runat="server" Width="100%" CellPadding="3" RepeatColumns="3"
                                                            RepeatLayout="Table" Height="250px">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </ContentTemplate>
                                            </cc1:TabPanel>
                                        </cc1:TabContainer>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 25%">
                                    </td>
                                    <td style="width: 25%" align="right">
                                        <asp:Button ID="lnkBtncancel" runat="server" CausesValidation="False" CssClass="cancelbutton"
                                            EnableTheming="false" SkinID="skinBtnCancel" OnClick="lnkBtncancel_Click" />
                                    </td>
                                    <td style="width: 25%">
                                        <asp:Button ID="lnkBtnSave" runat="server" CssClass="savebutton" EnableTheming="false"
                                            SkinID="skinBtnSave" OnClick="lnkBtnSave_Click" />
                                    </td>
                                    <td style="width: 25%">
                                    </td>
                                </tr>
                            </table>
                        </div>
                    <%--</div>--%>
                </ContentTemplate>
            </cc1:TabPanel>
        </cc1:TabContainer>
    </div>
    </td>
    </tr>
    </table>

    <br />
</asp:Content>
