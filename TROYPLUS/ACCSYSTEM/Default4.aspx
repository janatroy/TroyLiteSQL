<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default4.aspx.cs" Inherits="Default4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
        <cc1:TabContainer ID="tabs2" runat="server" Width="100%" ActiveTabIndex="2">
            <cc1:TabPanel ID="tabMaster" runat="server" HeaderText="Sales Details">
                <HeaderTemplate>
                    Details1
                </HeaderTemplate>
                <ContentTemplate>
                    <asp:CheckBox ID="chk" runat="server" />

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
