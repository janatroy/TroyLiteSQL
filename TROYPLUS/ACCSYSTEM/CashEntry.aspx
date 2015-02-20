<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="CashEntry.aspx.cs" Inherits="CashEntry" Title="Cash Entries" %>

<%@ Register Src="~/SMS/UserControls/errordisplay.ascx" TagName="errordisplay" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <br />
    <table width="100%">
        <tr>
            <td colspan="5" align="left">
                <uc1:errordisplay ID="errorDisplay" runat="server" />
            </td>
        </tr>
        <tr style="width: 100%">
            <td class="item alignLeft" colspan="5">
                * represents mandatory fields
            </td>
        </tr>
        <tr style="width: 100%">
            <td class="SectionHeader" colspan="5">
                <span>Cash Details </span>
            </td>
        </tr>
        <tr style="width: 100%">
            <td class="LMSLeftColumnColor" style="width: 25%">
                Customer Code *
                <asp:RequiredFieldValidator ID="rvCustCode" runat="server" ControlToValidate="txtCustCode"
                    ErrorMessage="Customer Code is Mandatory" Display="Dynamic">*</asp:RequiredFieldValidator>
            </td>
            <td style="width: 25%">
                <asp:TextBox ID="txtCustCode" runat="server" SkinID="skinTxtBox"></asp:TextBox>
            </td>
            <td class="LMSLeftColumnColor" style="width: 25%">
                Area *
                <asp:CompareValidator ID="cvArea" runat="server" ControlToValidate="ddArea"
                    ErrorMessage="Area is Mandatory" Operator="GreaterThan" ValueToCompare="0">*</asp:CompareValidator>
            </td>
            <td style="width: 25%">
                <asp:DropDownList ID="ddArea" runat="server" AppendDataBoundItems="True" DataSourceID="srcArea"
                    SkinID="skinDdlBox" DataTextField="area" DataValueField="area" Width="100%">
                    <asp:ListItem Value="0"> -- Select Area -- </asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="width: 25%">
                <asp:ImageButton ID="btnDetails" runat="server" Text="Find" CausesValidation="false"
                    ImageUrl="~/App_Themes/DefaultTheme/Images/Icon_View.gif" OnClick="btnDetails_Click" />
            </td>
        </tr>
        <tr style="width: 100%">
            <td class="LMSLeftColumnColor" style="width: 25%">
                Customer Name
            </td>
            <td style="width: 25%">
                <asp:TextBox ID="txtCustName" runat="server" Enabled="false" SkinID="skinTxtBox"></asp:TextBox>
            </td>
            <td class="LMSLeftColumnColor" style="width: 25%">
                Balance :
            </td>
            <td style="width: 25%">
                <asp:TextBox ID="txtCustBalance" runat="server" Enabled="false" SkinID="skinTxtBox"></asp:TextBox>
            </td>
            <td style="width: 25%">
            </td>
        </tr>
        <tr style="width: 100%">
            <td class="LMSLeftColumnColor" style="width: 25%">
                Bill No. :
                <asp:RequiredFieldValidator ID="rvBillNo" runat="server" ControlToValidate="txtBillNo"
                    ErrorMessage="Bill No. is Mandatory" Display="Dynamic">*</asp:RequiredFieldValidator>
            </td>
            <td class="lmsrightcolumncolor" style="width: 25%">
                <asp:TextBox ID="txtBillNo" runat="server" SkinID="skinTxtBox"></asp:TextBox>
            </td>
            <td class="LMSLeftColumnColor" style="width: 25%">
                Amount *
                <asp:RequiredFieldValidator ID="rvAmnt" runat="server" ControlToValidate="txtAmount"
                    ErrorMessage="Amount is Mandatory" Display="Dynamic">*</asp:RequiredFieldValidator>
            </td>
            <td class="lmsrightcolumncolor" style="width: 25%">
                <asp:TextBox ID="txtAmount" runat="server" SkinID="skinTxtBox"></asp:TextBox>
            </td>
            <td style="width: 25%">
            </td>
        </tr>
        <tr style="width: 100%">
            <td class="LMSLeftColumnColor" style="width: 25%">
                Discount :
            </td>
            <td style="width: 25%">
                <asp:TextBox ID="txtDiscount" runat="server" SkinID="skinTxtBox"></asp:TextBox>
            </td>
            <td class="LMSLeftColumnColor" style="width: 25%">
                Date Paid *
                <asp:CompareValidator ControlToValidate="txtDatePaid" Operator="DataTypeCheck" Type="Date"
                    ErrorMessage="Please enter a valid date(dd/MM/yyyy)" runat="server" ID="cmpValtxtDate"></asp:CompareValidator>
            </td>
            <td style="width: 25%">
                <asp:TextBox ID="txtDatePaid" runat="server" SkinID="skinTxtBox"></asp:TextBox>
            </td>
            <td style="width: 25%">
            </td>
        </tr>
        <tr style="width: 100%">
            <td class="LMSLeftColumnColor" style="width: 25%">
                Reason :&nbsp;
                <asp:CompareValidator ID="cvReason" runat="server" ControlToValidate="ddReason" ErrorMessage="Please select the reason for the discount provided."
                    Operator="GreaterThan" ValueToCompare="0" Enabled="False">*</asp:CompareValidator>
            </td>
            <td style="width: 25%">
                <asp:DropDownList ID="ddReason" runat="server" AppendDataBoundItems="True" SkinID="skinDdlBox"
                    DataSourceID="srcReason" DataTextField="reason" DataValueField="reason" Width="100%">
                    <asp:ListItem Value="0"> -- Select Reason --</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="width: 25%">
                &nbsp;
            </td>
            <td style="width: 25%">
                &nbsp;
            </td>
            <td style="width: 25%">
            </td>
        </tr>
        <tr style="width: 100%">
            <td colspan="5" style="height: 16px">
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
                    SkinID="skinButtonMedium" />&nbsp;
                <asp:Button ID="lnkBtnSave" runat="server" Text="Save" SkinID="skinButtonMedium"
                    OnClick="lnkBtnSave_Click" />
                &nbsp;
            </td>
            <td style="width: 25%">
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
                    ProviderName="System.Data.OleDb"></asp:SqlDataSource>
            </td>
            <td align="center" style="width: 25%">
                <asp:SqlDataSource ID="srcReason" runat="server" SelectCommand="SELECT reason FROM ReasonMaster"
                    EnableCaching="True" ProviderName="System.Data.OleDb"></asp:SqlDataSource>
            </td>
            <td style="width: 25%">
            </td>
        </tr>
        <tr style="width: 100%">
            <td colspan="5" style="height: 16px">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
