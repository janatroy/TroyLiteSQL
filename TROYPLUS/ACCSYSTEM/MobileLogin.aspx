<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MobileLogin.aspx.cs" Inherits="MobileLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mobile Login</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <link href="App_Themes/DefaultTheme/DefaultTheme.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/NewTheme/base.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/DefaultTheme/ComboBox.css" rel="Stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">

        //window.onload=checkWindow; 

        function checkWindow() {
            if (window.name != "TROY") {
                window.open("MobileLogin.aspx?Type=Demo", "TROY", "toolbar=no,menubar=no,resizable=yes,status=yes,height=800,width=1080,scrollbars=yes");
                window.opener = top;
                window.open('', '_parent', '');
                window.parent.close(); //window.open("../close.htm", "_self"); 
            }
        }         
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <br />
    <br />
    <br />
    <div style="font-size: small; text-align: center">
        <div class="brdClrLogin" style="width: 90%; text-align: center" align="center">
            <table cellpadding="4" cellspacing="0" border="0px" width="100%" align="center">
                <tr valign="middle" class="HeaderbgLogin">
                    <td width="100%" colspan="3" align="center">
                        Login
                    </td>
                </tr>
                <tr height="30px" valign="center">
                    <td align="right" width="25%">
                        User Name
                        <asp:RequiredFieldValidator ID="reqtxtLogin" runat="server" ControlToValidate="txtLogin"
                            Display="Dynamic" ErrorMessage="UserName Required" SetFocusOnError="true" CssClass="errorMsg">*</asp:RequiredFieldValidator>
                    </td>
                    <td width="3%">
                        &nbsp;
                    </td>
                    <td width="72%" align="left">
                        <asp:TextBox ID="txtLogin" runat="server" TabIndex="1" CssClass="cssTextBox" Font-Size="Medium"
                            Height="25px" MaxLength="50" Width="99%"></asp:TextBox>
                    </td>
                </tr>
                <tr height="30px" valign="center">
                    <td align="right" width="25%">
                        Password
                        <asp:RequiredFieldValidator ID="reqtxtPassword" runat="server" ControlToValidate="txtPassword"
                            Display="Dynamic" ErrorMessage="Password Required" SetFocusOnError="true" CssClass="errorMsg">*</asp:RequiredFieldValidator>
                    </td>
                    <td width="3%">
                        &nbsp;
                    </td>
                    <td width="72%" align="left">
                        <asp:TextBox ID="txtPassword" runat="server" TabIndex="2" CssClass="cssTextBox" Font-Size="Medium"
                            Height="25px" Width="99%" TextMode="Password" MaxLength="15"></asp:TextBox>
                    </td>
                </tr>
                <tr height="30px" valign="center">
                    <td align="right" width="25%">
                        Company Code<asp:RequiredFieldValidator ID="rvCompany" runat="server" ControlToValidate="txtCompany"
                            Display="Dynamic" ErrorMessage="Company Code Required" SetFocusOnError="true"
                            CssClass="errorMsg">*</asp:RequiredFieldValidator>
                    </td>
                    <td width="3%">
                        &nbsp;
                    </td>
                    <td width="72%" align="left">
                        <asp:TextBox ID="txtCompany" runat="server" TabIndex="3" Width="99%" Height="25px"
                            Font-Size="Medium" CssClass="cssTextBox upper" MaxLength="50"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" width="25%">
                    </td>
                    <td width="3%">
                    </td>
                    <td width="72%" align="left">
                        <table cellspacing="2px" cellpadding="0px" border="0">
                            <tr valign="middle">
                                <td>
                                    <asp:CheckBox ID="chkRemember" runat="server" TabIndex="4" Style="margin-left: -3px;" />
                                </td>
                                <td>
                                    &nbsp;&nbsp;
                                </td>
                                <td>
                                    Remember Me
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="right" width="25%">
                    </td>
                    <td width="3%">
                    </td>
                    <td width="72%" align="left">
                        <asp:Button ID="btnLogin" runat="server" TabIndex="5" EnableTheming="false" OnClick="btnLogin_Click"
                            CssClass="loginBtn" Text="Login" />
                        <asp:Button ID="BtnReset" CssClass="loginBtn" EnableTheming="false" CausesValidation="false"
                            runat="server" Text="Reset" OnClientClick="resetLogin();" />
                        <br />
                    </td>
                </tr>
            </table>
            <br />
        </div>
    </div>
    </form>
</body>
</html>
