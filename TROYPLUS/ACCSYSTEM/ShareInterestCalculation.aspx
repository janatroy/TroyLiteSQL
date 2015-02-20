<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="ShareInterestCalculation.aspx.cs" Inherits="ShareInterestCalculation"
    Title="Others > Share Interest Calculation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">

    <table style="width: 100%">
        <tr style="width: 100%">
            <td style="width: 100%">
                
                    <%--<div class="mainConHd">
                        <table cellspacing="0" cellpadding="0" border="0">
                            <tr valign="middle">
                                <td>
                                    <span>Share Interest Calculation</span>
                                </td>
                            </tr>
                        </table>
                    </div>--%>
                    <table class="mainConHd" style="width: 994px;">
                        <tr valign="middle">
                            <td style="font-size: 20px;">
                                Share Interest Calculation
                            </td>
                        </tr>
                    </table>
            </td>
        </tr>
    </table>
    <table cellspacing="1" cellpadding="1" width="100%">
        <tr style="height:8px">
        </tr>
        <tr>
            <td align="right" width="20%" class="ControlLabel">
                Select Share Holder A/c
            </td>
            <td class="ControlDrpBorder" style="width:10%">
                <asp:DropDownList ID="cmbShareHolder" AppendDataBoundItems="true" BackColor = "#90c9fc" CssClass="drpDownListMedium" width="100%"
                    runat="server" AutoPostBack="true" DataValueField="LedgerID" DataTextField="LedgerName" style="border: 1px solid #90c9fc" height="26px"
                    OnSelectedIndexChanged="cmbShareHolder_SelectedIndexChanged">
                    <asp:ListItem Text="Select Share Holder A/C" style="background-color: #90c9fc" Value="0"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="ControlLabel" style="width:15%">
                <asp:RequiredFieldValidator CssClass="lblFont" ID="reqShareHolder" Text="Please Select a Share Holder"
                    InitialValue="0" ControlToValidate="cmbShareHolder" ValidationGroup="interestval"
                    runat="server" />
                Amount
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cmbShareHolder" EventName="SelectedIndexChanged" />
                    </Triggers>
                    <ContentTemplate>
                        <asp:Label ID="lblAmount" runat="server" CssClass="lblFont" Style="color: royalblue;
                            font-weight: bold;"></asp:Label>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td style="width:10%">
            </td>
        </tr>
        <tr>
            <td align="right" style="width:20%" class="ControlLabel">
                Start Date
            </td>
            <td align="left" width="10%" class="ControlTextBox3">
                <asp:TextBox ID="txtStartDate" runat="server" CssClass="cssTextBox" Width="100px"
                    MaxLength="10" />
            </td>
            <td align="left" style="width:15%">
                <script type="text/javascript" language="JavaScript">
                    new tcal({ 'formname': 'aspnetForm', 'controlname': 'ctl00$cplhControlPanel$txtStartDate' });</script>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtStartDate"
                    Display="Dynamic" ErrorMessage="Please Enter Start Date" CssClass="lblFont" ValidationGroup="interestval"></asp:RequiredFieldValidator>
            </td>
            <td style="width:10%">
                
            </td>
        </tr>
        <tr>
            <td align="right" class="ControlLabel" style="width:20%">
                End Date
            </td>
            <td align="left" class="ControlTextBox3" style="width:10%">
                <asp:TextBox ID="txtEndDate" runat="server" CssClass="cssTextBox" Width="100px" MaxLength="10" />
            </td>
            <td align="left" style="width:15%">
                <script type="text/javascript" language="JavaScript">
                    new tcal({ 'formname': 'aspnetForm', 'controlname': 'ctl00$cplhControlPanel$txtEndDate' });</script>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEndDate"
                    Display="Dynamic" CssClass="lblFont" ErrorMessage="Please Enter The End Date"
                    ValidationGroup="interestval"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" CssClass="lblFont" ControlToCompare="txtStartDate"
                    ControlToValidate="txtEndDate" Display="Dynamic" ErrorMessage="Start Date Should Be Less Than the End Date"
                    ValidationGroup="interestval" Operator="GreaterThanEqual" SetFocusOnError="True"
                    Type="Date"></asp:CompareValidator>
            </td>
            <td align="left" style="width:10%">
                
            </td>
        </tr>
        <tr>
            <td style="width: 20%" class="ControlLabel">
                Interest (%)
            </td>
            <td class="ControlTextBox3" style="width:10%">
                <asp:TextBox ID="txtInterest" runat="server" CssClass="cssTextBox" Width="100px"
                    MaxLength="10" />
                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender41" runat="server" TargetControlID="txtInterest"
                    FilterType="Custom, Numbers" ValidChars="." />
            </td>
            <td style="width:15%">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtInterest"
                    Display="Dynamic" CssClass="lblFont" ErrorMessage="Please Enter Interest(%)"
                    ValidationGroup="interestval"></asp:RequiredFieldValidator>
            </td>
            <td style="width:10%">
            </td>
        </tr>
        <tr style="height:8px">
        </tr>
        <tr>
        <td>
        </td>
            <td align="center">
                <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" Text="Calculate Interest"
                    CssClass="cssDropDown" ValidationGroup="interestval" />&nbsp;
            </td>
            <td>
            </td>
            <td></td>
        </tr>
        <tr>
            <td colspan="4">
            </td>
        </tr>
    </table>
    <div id="dvResult" runat="server" visible="false">
        <table width="100%" cellpadding="2" cellspacing="2" style="border: 1px solid silver;
            font-size: 11px; font-family: 'Trebuchet MS';">
            <tr>
                <td style="background-image: url('App_Themes/DefaultTheme/Images/head.png'); color: White;
                    background-repeat: repeat-x">
                    Name of the Share Holder
                </td>
                <td style="background-image: url('App_Themes/DefaultTheme/Images/head.png'); color: White;
                    background-repeat: repeat-x">
                    Interest (%)
                </td>
                <td style="background-image: url('App_Themes/DefaultTheme/Images/head.png'); color: White;
                    background-repeat: repeat-x">
                    No. of Months
                </td>
                <td style="background-image: url('App_Themes/DefaultTheme/Images/head.png'); color: White;
                    background-repeat: repeat-x">
                    Amount
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblShare" runat="server" CssClass="lblFont" Style="color: royalblue;
                        font-weight: bold;"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblInterest" runat="server" CssClass="lblFont" Style="color: royalblue;
                        font-weight: bold;"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblDate" runat="server" CssClass="lblFont" Style="color: royalblue;
                        font-weight: bold;"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblInterestAmount" runat="server" CssClass="lblFont" Style="color: royalblue;
                        font-weight: bold;"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
