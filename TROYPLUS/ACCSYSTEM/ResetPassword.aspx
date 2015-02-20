<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="ResetPassword.aspx.cs" Inherits="ResetPassword" Title="Administration > Reset Password" %>

<%@ Register Src="~/SMS/UserControls/errordisplay.ascx" TagName="errorDisplay" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <script language="javascript" type="text/javascript" src="Scripts/JScript.js"></script>

    <table style="width: 100%">
        <tr style="width: 100%">
            <td style="width: 100%">

                <%--<div class="mainConHd">
                        <table cellspacing="0" cellpadding="0" border="0">
                            <tr valign="middle">
                                <td>
                                    <span>Change Password</span>
                                </td>
                            </tr>
                        </table>
                    </div>--%>

                <%-- <table class="mainConHd" style="width: 994px;">
                    <tr valign="middle">
                        <td style="font-size: 20px;">
                            Change Password
                        </td>
                    </tr>
                </table>--%>
                <div class="mainConBody">
                    <div>
                        <table cellspacing="2px" cellpadding="3px" border="0" width="100%" style="margin: -3px 0px 0px 2px;"
                            class="searchbg">
                            <tr>
                                <td style="width: 2%"></td>
                                <td style="width: 20%; font-size: 22px; color: #000000;">Reset Password
                                </td>
                                <td style="width: 13%"></td>
                                <td style="width: 5%; color: #000080;" align="right"></td>
                                <td style="width: 17%"></td>

                                <td style="width: 18%"></td>
                                <td style="width: 13%; text-align: left"></td>
                                <td style="width: 25%"></td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="mainConDiv" id="IdmainConDiv" align="center">
                    <br /><br /><br />
                    <table align="center" width="50%" style="border: 1px solid #86b2d1" cellpadding="5"
                        cellspacing="1">
                        <tr style="height: 5px">
                        </tr>
                        <tr>
                            <td class="ControlLabel" style="width: 20%">
                                <asp:RequiredFieldValidator ID="rvbank" runat="server" ControlToValidate="drpUserName" Display="Dynamic" EnableClientScript="False"  ErrorMessage="User Name is mandatory" InitialValue="0" Text="*"  />
                                User Name*
                            </td>
                            <td style="width: 25%" class="ControlTextBox3">
                                <asp:DropDownList ID="drpUserName" runat="server" AppendDataBoundItems="true" BackColor="#e7e7e7" CssClass="drpDownListMedium" DataTextField="UserName" DataValueField="UserName" Height="26px" Style="border: 1px solid #e7e7e7" TabIndex="1" Width="313px">
                                    <asp:ListItem style="background-color: #e7e7e7" Text="Select User Name" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="width: 10%"></td>
                        </tr>
                         <tr style="height: 5px">
                        </tr>
                        <tr>
                            <td class="ControlLabel" style="width: 20%">Reset Password *
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNewPass"
                                    Display="Dynamic" EnableClientScript="False" ErrorMessage="Enter Reset password">*</asp:RequiredFieldValidator>
                            </td>
                            <td style="width: 25%" class="ControlTextBox3">
                                <asp:TextBox ID="txtNewPass" runat="server" TextMode="Password" SkinID="skinTxtBox" TabIndex="2"
                                    Width="98%"></asp:TextBox>
                            </td>
                            <td style="width: 10%"></td>
                        </tr>

                        <tr style="height: 5px">
                        </tr>
                        <tr>
                            <td colspan="3">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 25%"></td>
                                        <td style="width: 25%">
                                            <asp:Button ID="lnkBtnSave" runat="server" CssClass="savebutton1231" EnableTheming="false" TabIndex="3"
                                                OnClick="lnkBtnSave_Click" Width="150px" />
                                        </td>
                                        <td style="width: 30%">
                                            <asp:Button ID="lnkBtncancel" runat="server" CssClass="cancelbutton6" EnableTheming="false" TabIndex="4"
                                                SkinID="skinBtnCancel" CausesValidation="false" DESIGNTIMEDRAGDROP="679" />
                                        </td>
                                        <td style="width: 20%"></td>
                                    </tr>

                                </table>
                            </td>
                        </tr>
                        <%--<tr style="height:5px">
                        </tr>--%>
                    </table>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
