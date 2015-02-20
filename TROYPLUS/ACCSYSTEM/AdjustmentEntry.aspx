<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="AdjustmentEntry.aspx.cs" Inherits="AdjustmentEntry" Title="Adjustment Entries" %>

<%@ Register Src="~/SMS/UserControls/errordisplay.ascx" TagName="errordisplay" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <br />
    <table width="100%">
        <tr>
            <td colspan="4" align="left">
                <uc1:errordisplay ID="errorDisplay" runat="server" />
            </td>
        </tr>
        <tr style="width: 100%">
            <td class="item alignLeft" colspan="4">
              * represents mandatory fields
            </td>
        </tr>
        <tr style="width: 100%">
            <td class="SectionHeader" colspan="4">
                <span>Adjustments </span>
            </td>
        </tr>
        <tr style="width: 100%">
            <td class="LMSLeftColumnColor" style="width: 25%">
                Customer Code *
                <asp:RequiredFieldValidator ID="rvCustCode" runat="server" ControlToValidate="txtCustCode"
                    ErrorMessage="Customer Code is Mandatory" Display="Dynamic">*</asp:RequiredFieldValidator>
            </td>
            <td class="lmsrightcolumncolor" style="width: 25%">
                <asp:TextBox ID="txtCustCode" runat="server" SkinID="skinTxtBox"></asp:TextBox>
            </td>
            <td class="LMSLeftColumnColor" style="width: 25%">
                Area *
                <asp:CompareValidator ID="cvArea" runat="server" ControlToValidate="ddArea"
                    ErrorMessage="Area is Mandatory" Operator="GreaterThan" ValueToCompare="0">*</asp:CompareValidator>
            </td>
            <td class="lmsrightcolumncolor" style="width: 25%">
                <asp:DropDownList ID="ddArea" runat="server" SkinID="skinDdlBox" AppendDataBoundItems="True"
                    DataSourceID="srcArea" DataTextField="area" DataValueField="area" Width="100%">
                    <asp:ListItem Value="0">Please Select Area</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr style="width: 100%">
            <td class="LMSLeftColumnColor" style="width: 25%">
                Amount *
                <asp:RequiredFieldValidator ID="rvAmnt" runat="server" ControlToValidate="txtAmount"
                    ErrorMessage="Amount is Mandatory" Display="Dynamic">*</asp:RequiredFieldValidator>
            </td>
            <td class="lmsrightcolumncolor" style="width: 25%">
                <asp:TextBox ID="txtAmount" runat="server" SkinID="skinTxtBox"></asp:TextBox>
            </td>
            <td class="LMSLeftColumnColor" style="width: 25%">
                Date Entered:
            </td>
            <td class="lmsrightcolumncolor" style="width: 25%">
                <asp:TextBox ID="txtDateEntered" runat="server" SkinID="skinTxtBox"></asp:TextBox>
            </td>
        </tr>
        <tr style="width: 100%">
            <td class="LMSLeftColumnColor" style="width: 25%">
                Reason *
                <asp:CompareValidator ID="cvReason" runat="server" ControlToValidate="ddReason"
                    ErrorMessage="Please select the reason for the discount provided." Operator="GreaterThan"
                    ValueToCompare="0">*</asp:CompareValidator>
            </td>
            <td class="lmsrightcolumncolor" style="width: 25%">
                <asp:DropDownList ID="ddReason" runat="server" SkinID="skinDdlBox" AppendDataBoundItems="True"
                    DataSourceID="srcReason" DataTextField="reason" DataValueField="reason" Width="100%">
                    <asp:ListItem Value="0">Please Select Reason</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="lmsrightcolumncolor" style="width: 25%">
                &nbsp;
            </td>
            <td class="lmsrightcolumncolor" style="width: 25%">
                &nbsp;
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
                <asp:Button ID="lnkBtncancel" runat="server" Text="Cancel" CssClass="Button" OnClick="lnkBtncancel_Click" />&nbsp;
                <asp:Button ID="lnkBtnSave" runat="server" Text="Save    " CssClass="Button" OnClick="lnkBtnSave_Click" />
                &nbsp;
            </td>
        </tr>
        <tr style="width: 100%">
            <td style="width: 25%">
                &nbsp;
            </td>
            <td align="center" style="width: 25%">
                &nbsp;
            </td>
            <td align="center" style="width: 25%">
                <asp:SqlDataSource ID="srcArea" runat="server" SelectCommand="SELECT [area] FROM [AreaMaster]"
                    EnableCaching="True" ProviderName="System.Data.OleDb"></asp:SqlDataSource>
            </td>
            <td align="center" style="width: 25%">
                <asp:SqlDataSource ID="srcReason" runat="server" SelectCommand="SELECT [reason] FROM [AdjustmentReason]"
                    EnableCaching="True" ProviderName="System.Data.OleDb"></asp:SqlDataSource>
            </td>
        </tr>
        <tr style="width: 100%">
            <td colspan="4" style="height: 16px">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
