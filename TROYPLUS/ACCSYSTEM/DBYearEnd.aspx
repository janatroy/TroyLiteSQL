<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="DBYearEnd.aspx.cs" Inherits="DBYearEnd" Title="Administration > Year End Updation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <table style="width: 100%">
        <tr style="width: 100%">
            <td style="width: 100%">
                <div class="mainConBody">
                    <table style="width: 100.3%; margin: -3px 0px 0px 2px;" cellpadding="3" cellspacing="2" class="searchbg">
                        <tr style=" vertical-align: middle">
                            <td style="width: 23%; font-size: 20px; color: #000000;" >
                                Year End Updation
                            </td>
                            <td style="width: 10%">
                   
                            </td>
                            <td style="width: 5% ;color: #000080;" align="right">
                   
                            </td>
                            <td style="width: 15%">
                    
                            </td>
                            <td style="width: 4%;color: #000080;" align="right">
                    
                            </td>
                            <td style="width: 14%">
                    
                            </td>
                            <td style="width: 10%; text-align: left">
                    
                            </td>
                        </tr>
                    </table>
                </div>
    <div class="lblFont" style="border: solid 1px black; width: 100.1%; background-color: aliceblue; margin: 0px 0px 0px 2px;
        font-weight: bold; color: Red;">
        Attention : This screen is used to create a New financial year account. New transacation
        will be created from now. This need to be used carefully need to be done by the approval
        of administrator.
    </div>
    <br />
    <asp:Label ID="lblMsg" runat="server" Style="color: Red; font-weight: bold;" class="lblFont"></asp:Label>
    <br />
    <br />
    <%--<asp:RequiredFieldValidator CssClass="lblFont" ID="reqDBname" Text=" * - Old Database name is mandatory"
        ControlToValidate="txtName" runat="server" ValidationGroup="cmpval" />--%>
    <table align="center" width="50%" style="border: 3px solid black; margin: 0 0 0 0px"
                        cellpadding="1" cellspacing="1">
        <%--<tr>
            <td>
                <span class="lblFont">Provide the old database name :</span>&nbsp;
                <asp:TextBox ID="txtName" CssClass="TxtBoxSales" Width="100px" runat="server" MaxLength="100"
                    ValidationGroup="cmpval" />
            </td>
        </tr>--%>
        <tr style="height: 17px;">
        
        </tr>
        <tr>
            <td width="8%">
                
            </td>
            <td width="34%">
                <asp:Button ID="btnAccount" SkinID="skinButtonCol2" runat="server" OnClick="btnAccount_Click"
                    Text="Refresh the New Account" ValidationGroup="cmpval" Width="100%" /> 
            </td>
            <td width="8%">
                
            </td>
        </tr>
        <tr runat="server" visible="false">
            <td width="8%">
                
            </td>
            <td width="34%">
                <asp:Button ID="Button1" SkinID="skinButtonCol2" runat="server" OnClick="userupd_Click"
                    Text="User Updation" ValidationGroup="cmpval" Width="100%" />
            </td>
            <td width="8%">    
                
            </td>
        </tr>
        <tr runat="server" visible="false">
            <td width="8%">
                
            </td>
            <td width="34%">
                <asp:Button ID="masupd" SkinID="skinButtonCol2" runat="server" OnClick="masupd_Click"
                    Text="Master Updation" ValidationGroup="cmpval" Width="100%" />
            </td>
            <td width="8%">    
                
            </td>
        </tr>
        <tr>
            <td width="8%">
                
            </td>
            <td width="34%">
                <asp:Button ID="stkupd" SkinID="skinButtonCol2" runat="server" OnClick="stkupd_Click"
                    Text="Stock Updation" ValidationGroup="cmpval" Width="100%" />
            </td>
            <td width="8%">    
                
            </td>
        </tr>
        <tr>
            <td width="8%">
                
            </td>
            <td width="34%">
                <asp:Button ID="opbalupd" SkinID="skinButtonCol2" runat="server" OnClick="opbalupd_Click"
                    Text="Opening Balance Updation" ValidationGroup="cmpval" Width="100%" />
            </td>
            <td width="8%">    
                
            </td>
        </tr>
	<tr runat="server" visible="false">
            <td width="8%">
                
            </td>
            <td width="34%">
                <asp:Button ID="Button2" SkinID="skinButtonCol2" runat="server" OnClick="cash_Click"
                    Text="Balance Updation" ValidationGroup="cmpval" Width="100%" />
            </td>
            <td width="8%">    
                
            </td>
        </tr>
        <tr style="height: 17px;">
        
        </tr>
        <%--<tr>
            <td width="5%">
                
            </td>
            <td width="34%">
                <asp:Button ID="Button1" SkinID="skinButtonCol2" runat="server" OnClick="btnCompress_Click"
                    Text="Compress The DB" ValidationGroup="cmpval" Width="100%" />
            </td>
            <td width="8%">    
                
            </td>
        </tr>--%>
    </table>
    </td>
    </tr>
    </table>
</asp:Content>
