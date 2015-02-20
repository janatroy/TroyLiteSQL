<%@ Page Language="C#" MasterPageFile="~/SimplePage.master" AutoEventWireup="true"
    CodeFile="Login.aspx.cs" Inherits="Login" Title="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server" Visible="false">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <script language="javascript" type="text/javascript">

        function resetLogin() {
            document.getElementById('ctl00_cplhControlPanel_txtLogin').value = "";
            document.getElementById('ctl00_cplhControlPanel_txtPassword').value = "";
            document.getElementById('ctl00_cplhControlPanel_txtCompany').value = "";
            document.getElementById('ctl00_cplhControlPanel_chkRemember').checked = false;
        }

        function invokeMeMaster() {
            var chkPostBack = '<%= Page.IsPostBack ? "true" : "false" %>';

            if (chkPostBack == 'false') {

                alert('For Demo Login. Please enter UserName: admin, Password: admin123, CompanyCode: DEMO');
            }

        }

        //window.onload = function() { invokeMeMaster(); };
        //window.onload=checkWindow;

        function checkWindow() {

            //            var left = (screen.width / 2) - (w / 2);
            //            var top = (screen.height / 2) - (h / 2);

            var w = screen.width - 100;
            var h = 805;

            if (window.name == "") {
                window.open("Login.aspx", "TROY", "toolbar=no,menubar=no,resizable=no,status=yes,height=" + h + ",width=" + w + ",scrollbars=yes,copyhistory=no");
                window.opener = top;
                window.open('', '_parent', '');
                window.parent.close();
                //window.open("close.htm", "_self"); 
            }
        }         
        
    </script>
    <div class="loginDiv">
        <!-- Login Div -->
        <div class="loginInner">
            <div class="loginCnt">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td align="left" valign="top" class="fieldname">
                            <asp:Label ID="lbUsername" runat="server" Text="User Name" Font-Bold="True" ForeColor="Black" Font-Size="Small"></asp:Label>
                            <asp:RequiredFieldValidator ID="reqtxtLogin" runat="server" ControlToValidate="txtLogin"
                                Display="Dynamic" ErrorMessage="UserName Required" SetFocusOnError="true" CssClass="errorMsg">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" class="textfieldbg">
                            <asp:TextBox ID="txtLogin" runat="server" TabIndex="1" CssClass="loginText" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" class="fieldname">
                            <asp:Label ID="Label1" runat="server" Text="Password" Font-Bold="True" ForeColor="Black" Font-Size="Small"></asp:Label>
                            <asp:RequiredFieldValidator ID="reqtxtPassword" runat="server" ControlToValidate="txtPassword"
                                Display="Dynamic" ErrorMessage="Password Required" SetFocusOnError="true" CssClass="errorMsg">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" class="textfieldbg">
                            <asp:TextBox ID="txtPassword" runat="server" TabIndex="2" CssClass="loginText" TextMode="Password"
                                MaxLength="15"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" class="fieldname">
                        <asp:Label ID="Label2" runat="server" Text="Company Code" Font-Bold="True" ForeColor="Black" Font-Size="Small"></asp:Label>
                            <asp:RequiredFieldValidator ID="rvCompany" runat="server" ControlToValidate="txtCompany"
                                Display="Dynamic" ErrorMessage="Company Code Required" SetFocusOnError="true"
                                CssClass="errorMsg">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" class="textfieldbg">
                            <asp:TextBox ID="txtCompany" runat="server" TabIndex="3" CssClass="loginText upper"
                                MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top">
                            <table cellspacing="2px" cellpadding="0px" border="0">
                                <tr valign="middle">
                                    <td width="2%">
                                        &nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkRemember" runat="server" TabIndex="4" Style="margin-left: 10px;" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td class=" rememberme" style="width:90%">
                                        Remember Me
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top">
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <table width="100%" cellspacing="0" cellpadding="0" border="0" style="margin-top: -15px;">
                    <tr height="30px" valign="center">
                        <td width="20%">
                        </td>
                        <td width="30%">
                            
                        </td>
                        <td width="30%" >
                        </td>
                        <td width="20%">
                            
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">
                        </td>
                        <td width="30%">
                        </td>
                        <td width="30%" >
                        </td>
                        <td width="20%">
                            
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">
                            
                        </td>
                        <td width="30%">
                            <asp:Button ID="btnLogin" runat="server" TabIndex="5" EnableTheming="false" OnClick="btnLogin_Click"
                                CssClass="loginbuttonbg" Text="Login" />
                        </td>
                        <td width="30%">
                            <asp:Button ID="BtnReset" CssClass="loginbuttonbg" EnableTheming="false" CausesValidation="false"
                                runat="server" Text="Reset" OnClientClick="resetLogin();" />
                        </td>
                        <td width="20%">
                            
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
