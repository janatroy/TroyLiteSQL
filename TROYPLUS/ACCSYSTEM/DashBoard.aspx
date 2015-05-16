<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="DashBoard.aspx.cs" Inherits="_DashBoard" Title="Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <div style="vertical-align: top; text-align: left">
        <table width="80%" id="tblWarning" runat="server" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                  <%--  <div id="divWarning" runat="server" class="infoWarning">
                        
                    </div>--%>
                </td>
            </tr>
        </table>
        <table width="100%" cellpadding="2" cellspacing="2">
            <tr align="center">
                
                <td>
                    <asp:Image ID="imgTrial" runat="server" ImageUrl="~/App_Themes/DefaultTheme/Images/dashboard.png" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
