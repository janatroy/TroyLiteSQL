<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="BookCashEntry.aspx.cs" Inherits="BookCashEntry" Title="Cash Entry" %>

<%@ Register Src="~/SMS/UserControls/errordisplay.ascx" TagName="errordisplay" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
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
                Book *
                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddBook"
                    ErrorMessage="Book is Mandatory" Operator="GreaterThan" ValueToCompare="0"></asp:CompareValidator>
            </td>
            <td style="width: 25%">
                <asp:DropDownList ID="ddBook" runat="server" AppendDataBoundItems="True" DataSourceID="srcBook"
                    SkinID="skinDdlBox" DataTextField="BookName" DataValueField="BookID" AutoPostBack="true"
                    Width="100%" OnSelectedIndexChanged="ddBook_SelectedIndexChanged">
                    <asp:ListItem Value="0"> -- Select Book -- </asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="LMSLeftColumnColor" style="width: 25%">
                Bill No. :
                <asp:RequiredFieldValidator ID="rvBillNo" runat="server" ControlToValidate="txtBillNo"
                    ErrorMessage="Bill No. is Mandatory"></asp:RequiredFieldValidator>
            </td>
            <td style="width: 25%">
                <asp:TextBox ID="txtBillNo" runat="server" Enabled="false" SkinID="skinTxtBox"></asp:TextBox>
            </td>
            <td style="width: 25%">
                &nbsp;
            </td>
        </tr>
        <tr style="width: 100%">
            <td class="LMSLeftColumnColor" style="width: 25%">
                Customer Code *
                <asp:RequiredFieldValidator ID="rvCustCode" runat="server" ControlToValidate="txtCustCode"
                    ErrorMessage="Customer Code is Mandatory"></asp:RequiredFieldValidator>
            </td>
            <td style="width: 25%">
                <asp:TextBox ID="txtCustCode" runat="server" SkinID="skinTxtBox"></asp:TextBox>
            </td>
            <td class="LMSLeftColumnColor" style="width: 25%">
                Area *
                <asp:CompareValidator ID="cvArea" runat="server" ControlToValidate="ddArea"
                    ErrorMessage="Area is Mandatory" Operator="GreaterThan" ValueToCompare="0"></asp:CompareValidator>
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
                Amount *
                <asp:RequiredFieldValidator ID="rvAmnt" runat="server" ControlToValidate="txtAmount"
                    ErrorMessage="Amount is Mandatory"></asp:RequiredFieldValidator>
                <asp:CompareValidator runat="server" ID="cmpAmount" ControlToValidate="txtAmount"
                    Type="Integer" Operator="DataTypeCheck" ErrorMessage="Amount should be Interger"></asp:CompareValidator>
            </td>
            <td style="width: 25%">
                <asp:TextBox ID="txtAmount" runat="server" SkinID="skinTxtBox"></asp:TextBox>
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
                Discount :
            </td>
            <td style="width: 25%">
                <asp:TextBox ID="txtDiscount" runat="server" SkinID="skinTxtBox"></asp:TextBox>
            </td>
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
            </td>
        </tr>
        <tr style="width: 100%">
            <td style="width: 25%">
            </td>
            <td style="width: 25%">
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
            <td colspan="2" rowspan="2">
                <asp:Panel ID="Panel1" runat="server">
                    <table>
                        <tr>
                            <td class="item alignLeft" style="width: 50%">
                                Book Total Amount :
                            </td>
                            <td class="item alignLeft" style="width: 50%">
                                <asp:Label ID="lblTotalAmount" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="item alignLeft" style="width: 50%">
                                Total cash Enteted:
                            </td>
                            <td class="item alignLeft" style="width: 50%">
                                <asp:Label ID="lblCashEnteted" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="item alignLeft" style="width: 50%">
                                Total Remaining:
                            </td>
                            <td class="item alignLeft" style="width: 50%">
                                <asp:Label ID="lblCashRem" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
            <td style="width: 25%" align="center">
                &nbsp;
                <asp:SqlDataSource ID="srcBook" runat="server" SelectCommand="SELECT BookID,BookName FROM tblBook Where BookStatus='Open' Order By BookName ASC "
                    ProviderName="System.Data.OleDb"></asp:SqlDataSource>
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
