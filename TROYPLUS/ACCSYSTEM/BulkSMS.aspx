<%@ Page Title="" Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="BulkSMS.aspx.cs" Inherits="BulkSMS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <div style="text-align: left">
        <asp:UpdatePanel ID="UpdatePanel16" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <cc1:TabContainer ID="tabContol" runat="server" Width="80%" ActiveTabIndex="0">
                    <cc1:TabPanel ID="tabPanelMaster" runat="server" HeaderText="Adhoc SMS">
                        <ContentTemplate>
                            <table width="100%" style="border: 0px solid #5078B3">
                                <tr style="width: 100%">
                                    <td class="tblLeft" style="width: 40%">
                                        Recipients
                                    </td>
                                    <td style="width: 30%; text-align: left">
                                        <div style="border-width: 1px; border-color: #bce1fe; border-style: solid; width: 200px;
                                            font-family: 'Trebuchet MS'; text-align: center;">
                                            <asp:DropDownList TabIndex="1" ID="cmbCustomer" AppendDataBoundItems="true" CssClass="cssDropDown"
                                                Width="200px" runat="server" AutoPostBack="true" DataValueField="LedgerID" DataTextField="LedgerName"
                                                ValidationGroup="salesval">
                                                <asp:ListItem Text="All Customers" Value="ALLCUST"></asp:ListItem>
                                                <asp:ListItem Text="All Dealers" Value="ALLDEL"></asp:ListItem>
                                                <asp:ListItem Text="All Dealers/Customers" Value="ALLDELSUPP"></asp:ListItem>
                                                <asp:ListItem Text="All Supplier/Dealers/Customers" Value="ALLSDC"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                    <td style="width: 30%; text-align: left">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tblLeft" style="width: 40%">
                                        SMS Type:
                                    </td>
                                    <td style="text-align: left" colspan="2" style="width: 200px">
                                        <asp:RadioButtonList runat="server" ID="rdoSMSType" Width="200px" AutoPostBack="true"
                                            CssClass="cssTextBox" RepeatDirection="Vertical" OnSelectedIndexChanged="ChangeRdo">
                                            <asp:ListItem Text="Debt Recovery" Value="DEBT"></asp:ListItem>
                                            <asp:ListItem Text="General Message" Value="NORM" Selected="True"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr id="RowSMSTemplate" runat="server">
                                    <td class="tblLeft" style="width: 30%">
                                        SMS Draft:
                                    </td>
                                    <td style="text-align: left; width: 30%">
                                        <div style="border-width: 1px; border-color: #bce1fe; border-style: solid; width: 200px;
                                            font-family: 'Trebuchet MS';">
                                            <asp:DropDownList TabIndex="1" ID="ddSMSTemplate" AppendDataBoundItems="true" CssClass="cssDropDown"
                                                DataSourceID="srcSMSText" Width="200px" runat="server" AutoPostBack="true" DataValueField="SMSType"
                                                OnSelectedIndexChanged="ddSMSTemplate_SelectedIndexChanged" DataTextField="SMSType">
                                                <asp:ListItem style="background-color: #bce1fe" Text="Select Draft SMS" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                    <td style="width: 30%; text-align: left">
                                    </td>
                                </tr>
                                <tr id="RowOustanding" runat="server">
                                    <td colspan="3" style="text-align: left">
                                        <table width="100%" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td style="width: 40%" class="tblLeft">
                                                    Outstanding :
                                                </td>
                                                <td style="width: 30%; text-align: left" align="left" valign="top">
                                                    <table cellpadding="0" cellspacing="0" width="100%">
                                                        <tr style="text-align: left">
                                                            <td>
                                                                <asp:DropDownList ID="ddOper" runat="server" Style="height: 19px" CssClass="cssDropDown"
                                                                    Width="59px">
                                                                    <asp:ListItem Value="&gt;=">&gt;=</asp:ListItem>
                                                                    <asp:ListItem Value="&gt;">&gt;</asp:ListItem>
                                                                    <asp:ListItem Value="=">=</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                &nbsp;<asp:TextBox ID="txtOpenBal" runat="server" CssClass="cssTextBox" Width="130px"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 30%; text-align: left">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 30%; text-align: left">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr style="width: 100%" id="RowText" runat="server">
                                    <td class="tblLeft" style="width: 40%">
                                        Text<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="sendSMS"
                                            runat="server" ControlToValidate="txtMessage" EnableClientScript="true" ErrorMessage="Text is mandatory."
                                            Display="Dynamic">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td colspan="3" style="width: 60%; text-align: left">
                                        <asp:TextBox ID="txtMessage" runat="server" Width="98%" CssClass="cssTextBox" TextMode="MultiLine"
                                            Height="60px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tblLeft" style="width: 25%">
                                        <asp:ObjectDataSource ID="srcSMSText" runat="server" SelectMethod="ListSMSText" TypeName="BusinessLogic">
                                            <SelectParameters>
                                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                    </td>
                                    <td class="lmsrightcolumncolor" colspan="3" style="width: 75%; text-align: left">
                                        <asp:Button ID="BtnSendSMS" runat="server" Text="Send SMS" Width="75px" ValidationGroup="sendSMS"
                                            CssClass="Button" OnClick="BtnSendSMS_Click" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </cc1:TabPanel>
                </cc1:TabContainer>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
