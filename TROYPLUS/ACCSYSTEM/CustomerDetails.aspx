<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="CustomerDetails.aspx.cs" Inherits="CustomerDetails" Title="Customer Details" %>

<%@ Register Src="~/SMS/UserControls/errordisplay.ascx" TagName="errorDisplay" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <br />
    <table width="100%">
        <tr>
            <td colspan="4" align="left">
                <uc1:errorDisplay ID="errorDisplay" runat="server" />
            </td>
        </tr>
        <tr style="width: 100%">
            <td class="item alignLeft" colspan="4">
                * represents mandatory fields
            </td>
        </tr>
        <tr style="width: 100%">
            <td class="SectionHeader" colspan="4">
                <span>Customer Details</span>
            </td>
        </tr>
        <tr style="width: 100%">
            <td class="LMSLeftColumnColor" style="width: 25%">
                Customer Name *
                <asp:RequiredFieldValidator ID="rvCustName" runat="server" ControlToValidate="txtCustName"
                    ErrorMessage="Customer Name is Mandatory" Display="Dynamic">*</asp:RequiredFieldValidator>
                &nbsp;
            </td>
            <td class="lmsrightcolumncolor" style="width: 25%">
                <asp:TextBox ID="txtCustName" runat="server" SkinID="skinTxtBox"></asp:TextBox>
            </td>
            <td class="LMSLeftColumnColor" style="width: 25%">
                Area *
                <asp:CompareValidator ID="cvArea" runat="server" ControlToValidate="ddArea" ErrorMessage="Area is Mandatory"
                    Operator="GreaterThan" ValueToCompare="0">*</asp:CompareValidator>
            </td>
            <td class="lmsrightcolumncolor" style="width: 25%">
                <asp:DropDownList ID="ddArea" runat="server" AppendDataBoundItems="True" Width="100%"
                    DataSourceID="srcArea" DataTextField="area" SkinID="skinDdlBox" DataValueField="area">
                    <asp:ListItem Value="0"> -- Select Area -- </asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr style="width: 100%">
            <td class="LMSLeftColumnColor" style="width: 25%">
                Cust Code :
            </td>
            <td class="lmsrightcolumncolor" style="width: 25%">
                <asp:TextBox ID="txtCustCode" runat="server" SkinID="skinTxtBox" ReadOnly="True"
                    Enabled="False"></asp:TextBox>
            </td>
            <td class="LMSLeftColumnColor" style="width: 25%">
                Category *
                <asp:CompareValidator ID="cvCategory" runat="server" ControlToValidate="ddCategory"
                    ErrorMessage="Category is Mandatory" Operator="GreaterThan" ValueToCompare="0">*</asp:CompareValidator>
            </td>
            <td class="lmsrightcolumncolor" style="width: 25%">
                <asp:DropDownList ID="ddCategory" runat="server" SkinID="skinDdlBox" AppendDataBoundItems="True"
                    Width="100%">
                    <asp:ListItem Value="0"> -- Select Category -- </asp:ListItem>
                    <asp:ListItem>DC</asp:ListItem>
                    <asp:ListItem>RC</asp:ListItem>
                    <asp:ListItem>NC</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr style="width: 100%">
            <td class="LMSLeftColumnColor" style="width: 25%">
                Door No.
            </td>
            <td class="lmsrightcolumncolor" style="width: 25%">
                <asp:TextBox ID="txtDoorNo" runat="server" SkinID="skinTxtBox"></asp:TextBox>
            </td>
            <td class="LMSLeftColumnColor" style="width: 25%">
                Address 1 *
                <asp:RequiredFieldValidator ID="rvAdd1" runat="server" ControlToValidate="txtAdd1"
                    EnableClientScript="true" ErrorMessage="Address 1 is Mandatory" Display="Dynamic">*</asp:RequiredFieldValidator>
            </td>
            <td class="lmsrightcolumncolor" style="width: 25%">
                <asp:TextBox ID="txtAdd1" runat="server" SkinID="skinTxtBox"></asp:TextBox>
            </td>
        </tr>
        <tr style="width: 100%">
            <td class="LMSLeftColumnColor" style="width: 25%">
                Address 2:
            </td>
            <td class="lmsrightcolumncolor" style="width: 25%">
                <asp:TextBox ID="txtAdd2" runat="server" SkinID="skinTxtBox"></asp:TextBox>
            </td>
            <td class="LMSLeftColumnColor" style="width: 25%">
                Place:
            </td>
            <td class="lmsrightcolumncolor" style="width: 25%">
                <asp:TextBox ID="txtPlace" runat="server" SkinID="skinTxtBox"></asp:TextBox>
            </td>
        </tr>
        <tr style="width: 100%">
            <td class="LMSLeftColumnColor" style="width: 25%">
                Phone No.
            </td>
            <td style="width: 25%">
                <asp:TextBox ID="txtPhoneNo" runat="server" SkinID="skinTxtBox"></asp:TextBox>
            </td>
            <td class="LMSLeftColumnColor" style="width: 25%">
                Effective Date:
            </td>
            <td style="width: 25%">
                <table cellpadding="0" cellspacing="0" style="width: 100%; text-align: left">
                    <tr>
                        <td style="width: 88%">
                            <asp:TextBox ID="txtEffDate" runat="server" SkinID="skinTxtBox"></asp:TextBox>
                        </td>
                        <td style="width: 12%; text-align: center">
                            <script language="JavaScript">                                new tcal({ 'formname': 'aspnetForm', 'controlname': 'ctl00$cplhControlPanel$txtEffDate' });</script>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="width: 100%">
            <td class="LMSLeftColumnColor" style="width: 25%">
                Installation Charge *
                <asp:RequiredFieldValidator ID="rvInstChrge" runat="server"
                    ControlToValidate="txtInstCrge" EnableClientScript="true" ErrorMessage="Installation Charge is Mandatory"
                    Display="Dynamic">*</asp:RequiredFieldValidator>
            </td>
            <td class="lmsrightcolumncolor" style="width: 25%">
                <asp:TextBox ID="txtInstCrge" runat="server" SkinID="skinTxtBox"></asp:TextBox>
            </td>
            <td class="LMSLeftColumnColor" style="width: 25%">
                Monthly Charge *
                <asp:RequiredFieldValidator ID="rvMnthlyCrge" runat="server" ControlToValidate="txtMnthCrge"
                    EnableClientScript="true" ErrorMessage="Monthly Charge is Mandatory" Display="Dynamic">*</asp:RequiredFieldValidator>
            </td>
            <td class="lmsrightcolumncolor" style="width: 25%">
                <asp:TextBox ID="txtMnthCrge" runat="server" SkinID="skinTxtBox"></asp:TextBox>
            </td>
        </tr>
        <tr style="width: 100%">
            <td class="LMSLeftColumnColor" style="width: 25%">
                Prevalied:
            </td>
            <td class="lmsrightcolumncolor alignLeft" style="width: 5%">
                <asp:CheckBox ID="chkPrev" runat="server" CssClass="CheckBox" />
            </td>
            <td class="LMSLeftColumnColor" style="width: 25%">
                Balance :
            </td>
            <td class="lmsrightcolumncolor" style="width: 25%">
                <asp:TextBox ID="txtBalance" runat="server" SkinID="skinTxtBox" ReadOnly="True" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr style="width: 100%">
            <td colspan="4" style="height: 16px">
            </td>
        </tr>
        <tr style="width: 100%">
            <td style="width: 25%">
                &nbsp;
            </td>
            <td style="width: 25%" align="center">
                &nbsp;
            </td>
            <td style="width: 25%" align="center">
                &nbsp;
            </td>
            <td style="width: 25%" align="right">
                <asp:Button ID="lnkBtncancel" runat="server" Text="Cancel" CausesValidation="false"
                    OnClick="lnkBtncancel_Click" CssClass="Button" />&nbsp;
                <asp:Button ID="lnkBtnSave" runat="server" Text="Save    " OnClick="lnkBtnSave_Click"
                    CssClass="Button" />
            </td>
        </tr>
        <tr style="width: 100%">
            <td style="width: 25%; text-align: left">
                <asp:Button ID="btnCashHistory" runat="server" SkinID="skinButtonBig" Text="Cash History"
                    OnClick="btnCashHistory_Click" CausesValidation="false" Font-Bold="True" Font-Size="9px"
                    Height="17px" Width="100%" />
            </td>
            <td align="center" style="width: 25%">
                &nbsp;
            </td>
            <td align="center" style="width: 25%">
                &nbsp;
            </td>
            <td align="center" style="width: 25%">
                <asp:SqlDataSource ID="srcArea" runat="server" SelectCommand="SELECT [area] FROM [AreaMaster]"
                    EnableCaching="True" ProviderName="System.Data.OleDb"></asp:SqlDataSource>
            </td>
        </tr>
        <tr style="width: 100%">
            <td colspan="4" style="height: 16px">
            </td>
        </tr>
    </table>
</asp:Content>
