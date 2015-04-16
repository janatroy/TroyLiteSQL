<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BulkPriceUpd.aspx.cs" Inherits="BulkPriceUpd" %>

<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <title>Bulk Product Updation</title>
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
                <td colspan="3">
                    Bulk Price Updation
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <table style="width:100%">
                        <tr style="height:15px">

                        </tr>
                        <tr>
                            <td  style="width:30%">
                                File
                            </td>
                            <td  style="width:35%" class="ControlTextBox3">
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

                                    <asp:FileUpload ID="FileUpload1" runat="server"  BackColor="#e7e7e7" style="border: 1px solid #e7e7e7;"/>
                                    
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
                            <td style="width:30%">
                                 To Update
                                <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="drpPriceList"
                                                                        Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="Price List is mandatory"
                                                                        Operator="GreaterThan" ValueToCompare="0" />
                            </td>
                            <td style="width:35%" align="left" class="ControlDrpBorder">
                                <asp:DropDownList ID="drpPriceList" runat="server" AppendDataBoundItems="true" BackColor="#e7e7e7" CssClass="drpDownListMedium" DataTextField="PriceName" DataValueField="Id" height="26px" DataSourceID="srclistpricelist" style="border: 1px solid #e7e7e7" Width="100%">
                                                                        <asp:ListItem style="background-color: #e7e7e7" Text="Select Price List" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>
                            </td>
                            <td style="width:35%">
                            
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <table style="width:100%">
                                    <tr>
                                        <td style="width:30%">
                                        
                                        </td>
                                        <td style="width:35%" align="center">
                                            <asp:Button ID="btnUpload" runat="server" Height="21px"  SkinID="skinButtonCol2" Text="Upload" Width="100px" onclick="btnUpload_Click"/>
                                        </td>
                                        <td style="width:35%">
                                            <asp:ObjectDataSource ID="srclistpricelist" runat="server" SelectMethod="ListPriceList"
                                                                    TypeName="BusinessLogic">
                                                                    <SelectParameters>
                                                                        <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                    </SelectParameters>
                                                                </asp:ObjectDataSource>
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
                <td colspan="3">
                    <table style="width:100%">
                        <tr>
                            <td style="width:50%">
                                        
                            </td>
                            <td style="width:50%">
                                <asp:Button ID="Button2" runat="server" SkinID="skinButtonCol2" Text="Download Sample Excel Format" Height="23px" CausesValidation="false"   onclick="btnFormat_Click" Width="200px" />        
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
