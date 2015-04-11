﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BulkAddition.aspx.cs" Inherits="BulkAddition" %>

<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <title>Bulk Addition</title>
    <link href="App_Themes/DefaultTheme/calendar.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/DefaultTheme/DefaultTheme.css" rel="stylesheet" type="text/css" />
  
    <script language="javascript" type="text/javascript" src="Scripts/calendar_eu.js"></script>
    <script type="text/javascript" language="JavaScript">
        function CallPrint(strid) {
            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('', '', 'letf=0,top=0,width=600,height=400,toolbar=0,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();

        }
        function switchViews(obj, imG) {
            var div = document.getElementById(obj);
            var img = document.getElementById(imG);

            if (div.style.display == "none") {
                div.style.display = "inline";


                img.src = "App_Themes/DefaultTheme/Images/minus.gif";

            }
            else {
                div.style.display = "none";
                img.src = "App_Themes/DefaultTheme/Images/plus.gif";

            }
        }
    </script>
</head>
<body style="font-family: 'Trebuchet MS'; font-size: 11px; text-align: center">
    <form id="form1" runat="server">
    <br />
    <br />
    <div id="div1" runat="server" align="center">
        <table cellpadding="1" cellspacing="2" width="450px"
            style="border: 1px solid blue;">
            <tr class="headerPopUp">
                <td colspan="4">
                    Bulk Products Addition
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <table style="width:100%">
                        <tr style="height:15px">

                        </tr>
                        <tr>
                            <td  style="width:30%">

                            </td>
                            <td  style="width:35%">
                                <div>
                                    <%--<asp:FileUpload ID="FileUpload1" runat="server" />
                                    <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" AccessKey="s" />
                                    <br />--%>
                                    <%--<asp:Label ID="Label1" runat="server" Text="Has Header ?"></asp:Label>--%>
                                    <%--<asp:RadioButtonList ID="rbHDR" runat="server">
                                        <asp:ListItem Text = "Yes" Value = "Yes" Selected = "True" ></asp:ListItem>
                                        <asp:ListItem Text = "No" Value = "No"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:GridView ID="GridView1" runat="server" AllowPaging = "true"  >
                                    </asp:GridView>--%>

                                    <asp:FileUpload ID="FileUpload1" runat="server" />
                                    
                                    <asp:GridView ID="GridView1" runat="server">
                                    </asp:GridView>
                                </div>
                            </td>
                            <td  style="width:35%">

                            </td>
                        </tr>
                        <tr style="height:6px">

                        </tr>
                        <tr>
                            <td colspan="4">
                                <table style="width:100%">
                                    <tr>
                                        <td style="width:30%">
                                        
                                        </td>
                                        <td style="width:35%" align="center">
                                           <%-- <asp:Button ID="btnUpload" runat="server" Height="21px" CssClass="Uploadbutton1231" Width="100px" onclick="btnUpload_Click"/>--%>
                                              <asp:Button ID="btnUpload" OnClick="btnUpload_Click" Font-Bold="True" runat="server" CssClass="Uploadbutton1231"
                                                        EnableTheming="false" ForeColor="White" Width="80px" />
                                        </td>
                                        <td style="width:35%">
                                        
                                        </td>
                                    </tr>
                                    <tr style="height:10px">

                                    </tr>
                                    
                                </table>
                            </td>
                        </tr>
                        
                    </table>
                </td>
            </tr>
            <tr style="height:10px">

            </tr>
            <tr>
                <td colspan="4">
                    <table style="width:100%">
                        <tr>
                            <td style="width:50%">
                                        
                            </td>
                            <td style="width:50%">
                             <%--   <asp:Button ID="Button2" runat="server" CssClass="Uploadbutton12312" SkinID="skinButtonCol2" Height="23px"   onclick="btnFormat_Click" Width="200px" />        --%>
                                      <asp:Button ID="Button2" OnClick="btnFormat_Click" Font-Bold="True" runat="server" CssClass="Uploadbutton12312"
                                                        EnableTheming="false" ForeColor="White" Width="250px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
                
        </table>
        
    </div>
    
    </form>
</body>
</html>
