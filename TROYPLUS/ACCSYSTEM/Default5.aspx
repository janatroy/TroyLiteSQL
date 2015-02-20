<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default5.aspx.cs" Inherits="Default5" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>



<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" ID="scr"></asp:ScriptManager>
    <div>
    


    <cc1:TabContainer ID="tabs2" runat="server" Width="59%" ActiveTabIndex="0" 
            Height="372px">
            <cc1:TabPanel ID="tabMaster" runat="server" HeaderText="Sales Details">
                <HeaderTemplate>
                    Details1
                
                </HeaderTemplate>
                
                <ContentTemplate>
               
                    <div style="height: 235px; width: 357px">
                        <table align="center" style="width: 100%; height: 337px;">
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:Label ID="Label1" runat="server" 
                                        style="font-size: small; font-weight: 700" Text="Select Fields To be Generated"></asp:Label>

                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="CheckBox1" runat="server" Text="Category" />

                                    
                                </td>
                                <td>
                                    <asp:CheckBox ID="CheckBox8" runat="server" Text="Quantity" />

                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="CheckBox4" runat="server" Text="Brand" />

                                </td>
                                <td>
                                    <asp:CheckBox ID="CheckBox3" runat="server" Text="VAT" />

                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="CheckBox7" runat="server" Text="Product" />

                                </td>
                                <td>
                                    <asp:CheckBox ID="CheckBox6" runat="server" Text="NLC" />

                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="CheckBox2" runat="server" Text="Model" />

                                </td>
                                <td>
                                    <asp:CheckBox ID="CheckBox10" runat="server" Text="BuyRate" />

                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="CheckBox5" runat="server" Text="ItemCode" />

                                </td>
                                <td>
                                    <asp:CheckBox ID="CheckBox9" runat="server" Text="Rate" />

                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:CheckBox ID="chkboxAll" runat="server" Text="Select All" />

                                </td>
                            </tr>
                        </table>
                    </div>
               
                
</ContentTemplate>
            
</cc1:TabPanel>
            <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="Sales Details">
                <HeaderTemplate>
                    Details2
                
</HeaderTemplate>
                
<ContentTemplate>

                
</ContentTemplate>
            
</cc1:TabPanel>
            <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="Sales Details">
                <HeaderTemplate>
                    Details3
                
</HeaderTemplate>
                
<ContentTemplate>
                
</ContentTemplate>
            
</cc1:TabPanel>
            <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="Sales Details">
                <HeaderTemplate>
                    Details4
                
</HeaderTemplate>
                
<ContentTemplate>
                
</ContentTemplate>
            
</cc1:TabPanel>
            <cc1:TabPanel ID="TabPanel4" runat="server" HeaderText="Sales Details">
                <HeaderTemplate>
                    Details5
                
</HeaderTemplate>
                
<ContentTemplate>
                
</ContentTemplate>
            
</cc1:TabPanel>
        </cc1:TabContainer> 
 







    </div>
    </form>
</body>
</html>
