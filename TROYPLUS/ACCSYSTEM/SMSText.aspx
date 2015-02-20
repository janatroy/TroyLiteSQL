<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="SMSText.aspx.cs" Inherits="SMSText" Title="SMS Draft Information" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">

    <table style="width: 100%">
        <tr style="width: 100%">
            <td style="width: 100%">
                   <%--<div class="mainConHd">
                        <table cellspacing="0" cellpadding="0" border="0">
                            <tr valign="middle">
                                <td>
                                    <span>Text Message Definitions</span>
                                </td>
                            </tr>
                        </table>
                    </div>--%>
                    <%--<table class="mainConHd" style="width: 994px;">
                        <tr valign="middle">
                            <td style="font-size: 20px;">
                                Text Message Definitions
                            </td>
                        </tr>
                    </table>--%>
                    <div class="mainConBody">
                        <table style="width: 100.3%;margin: -3px 0px 0px 2px;" cellpadding="3" cellspacing="2" class="searchbg">
                            <tr style="height: 25px; vertical-align: middle">
                                <td style="width: 2%;"></td>
                                            <td style="width: 30%; font-size: 22px; color: #000000;" >
                                                Text Message Definitions
                                            </td>
                                            <td style="width: 16%">
                                    <div style="text-align: left;">
                                        <%--<asp:Panel ID="pnlSearch" runat="server" Width="100px">--%>
                                            <asp:Button ID="lnkBtnAdd" runat="server" OnClick="lnkBtnAdd_Click" CssClass="ButtonAdd66"
                                                EnableTheming="false" Text=""></asp:Button>
                                        <%--</asp:Panel>--%>
                                    </div>
                                </td>
                                <td style="width: 10%; color: #000000;" align="right">
                                        <%--<div style="text-align: left;">
                                            <asp:Panel ID="Panel1" runat="server" Width="20px">--%>
                                                Search
                                            <%--</asp:Panel> 
                                        </div>--%>
                                    </td>
                                    <td style="width: 20%" class="Box1" >
                                        <%--<div style="text-align: left;">
                                            <asp:Panel ID="Panel2" runat="server" Width="100px">--%>
                                                <asp:TextBox ID="txtSearch" runat="server" SkinID="skinTxtBoxSearch" 
                                                ontextchanged="txtSearch_TextChanged"></asp:TextBox>
                                            <%--</asp:Panel> 
                                        </div>--%>
                                    </td>
                                    <td style="width: 20%;"  class="Box1">
                                        <div style="width: 160px; font-family: 'Trebuchet MS';">
                                            <%--<asp:Panel ID="Panel3" runat="server" Width="80px">--%>
                                                <asp:DropDownList ID="ddCriteria" runat="server" BackColor="#BBCAFB"  Width="150px" Height="23px" style="text-align:center;border:1px solid #BBCAFB "
                                                AutoPostBack="false" 
                                                onselectedindexchanged="ddCriteria_SelectedIndexChanged">
                                                <asp:ListItem Value="Type">Type</asp:ListItem>
                                                <asp:ListItem Value="SMSText">SMS Text</asp:ListItem>
                                                </asp:DropDownList>
                                            <%--</asp:Panel> --%>
                                        </div>
                                    </td>
                                    <td style="width: 20%" class="tblLeftNoPad" >
                                        <%--<div style="text-align: left;">
                                            <asp:Panel ID="Panel4" runat="server" Width="100px">--%>
                                                <asp:Button ID="btnSearch" runat="server" Text="" CssClass="ButtonSearch6" EnableTheming="false"
                                                    onclick="btnSearch_Click" />
                                            <%--</asp:Panel> 
                                        </div>--%>
                                    </td>
                            </tr>
                        </table>
                    </div>
                   
            <%--<table width="100%">
                                <tr style="width: 100%">
                                    <td>--%>
                
                    
                    <table width="100%" style="margin: -2px 0px 0px 0px;">
                        <tr style="width: 100%">
                            <td>
                            <div align="center" style="margin: -2px 0px 0px 0px;">
                                <asp:GridView ID="GrdSMSText" runat="server" DataSourceID="srcGridView" AutoGenerateColumns="False"
                                    OnRowCreated="GrdSMSText_RowCreated" Width="100.8%" PageSize="7" OnRowCancelingEdit="GrdSMSText_RowCancelingEdit"
                                    OnRowCommand="GrdSMSText_RowCommand" EmptyDataText="No SMS Text found!" OnRowDataBound="GrdSMSText_RowDataBound"
                                    AllowPaging="True" DataKeyNames="ID" OnDataBound="GrdSMSText_DataBound" OnRowUpdating="GrdSMSText_RowUpdating"
                                    EnableViewState="False" OnPreRender="GrdSMSText_PreRender" OnRowUpdated="GrdSMSText_RowUpdated" CssClass="someClass">
                                    <EditRowStyle VerticalAlign="Top" />
                                    <FooterStyle VerticalAlign="Top" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Type" HeaderStyle-BorderColor="Gray">
                                            <ItemStyle Width="150px" HorizontalAlign="Left" />
                                            <FooterStyle Width="150px" HorizontalAlign="Left" />
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtSMSType" runat="server" Width="140px" Text='<%# Bind("SMSType") %>'
                                                    CssClass="cssTextBox" EnableTheming="false"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rvSMSType" runat="server" ControlToValidate="txtSMSType"
                                                    Display="Dynamic" ErrorMessage="Type is mandatory">*</asp:RequiredFieldValidator>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSMSType" runat="server" Text='<%# Bind("SMSType") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtAddSMSType" runat="server" Width="140px" CssClass="cssTextBox"
                                                    EnableTheming="false"></asp:TextBox><asp:RequiredFieldValidator ID="rvAddSMSType"
                                                        runat="server" ControlToValidate="txtAddSMSType" Display="Dynamic" EnableClientScript="true"
                                                        ErrorMessage="Type is mandatory">*</asp:RequiredFieldValidator>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SMS Text" HeaderStyle-BorderColor="Gray">
                                            <ItemStyle Width="460px" HorizontalAlign="Left" />
                                            <FooterStyle Width="460px" HorizontalAlign="Left" />
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtSMSText" runat="server" Width="97%" TextMode="MultiLine" Height="30px"
                                                    Text='<%# Bind("SMSText") %>' CssClass="cssTextBox" EnableTheming="false"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rvSMSText" runat="server" ControlToValidate="txtSMSText"
                                                    Display="Dynamic" ErrorMessage="SMS Text is mandatory">*</asp:RequiredFieldValidator>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSMSText" runat="server" Text='<%# Bind("SMSText") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtAddSMSText" runat="server" TextMode="MultiLine" Height="30px"
                                                    Width="97%" CssClass="cssTextBox" EnableTheming="false"></asp:TextBox><asp:RequiredFieldValidator
                                                        ID="rvAddSMSText" runat="server" ControlToValidate="txtAddSMSText" Display="Dynamic"
                                                        EnableClientScript="true" ErrorMessage="SMS Text is mandatory">*</asp:RequiredFieldValidator>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit" HeaderStyle-BorderColor="Gray">
                                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                                            <FooterStyle Width="50px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Edit" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:ImageButton ID="lbUpdate" runat="server" CausesValidation="True" CommandName="Update"
                                                    Text="Update" SkinID="GridUpdate"></asp:ImageButton>
                                                <asp:ImageButton ID="lbCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                                    Text="Cancel" SkinID="GridCancel"></asp:ImageButton>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:ImageButton ID="lbInsert" runat="server" SkinID="GridUpdate" CommandName="Insert"
                                                    Text="Save"></asp:ImageButton>
                                                <asp:ImageButton ID="lblInsCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                                    SkinID="GridCancel" Text="Cancel"></asp:ImageButton>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="command" HeaderText="Delete" HeaderStyle-Width="50px" HeaderStyle-BorderColor="Gray">
                                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to Delete this Message Type?"
                                                    runat="server">
                                                </cc1:ConfirmButtonExtender>
                                                <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" CommandName="Delete"></asp:ImageButton>
                                                <asp:HiddenField ID="ldgID" runat="server" Value='<%# Bind("ID") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerTemplate>
                                        Goto Page
                                        <asp:DropDownList ID="ddlPageSelector" runat="server" AutoPostBack="true" Width="65px" style="border:1px solid blue">
                                        </asp:DropDownList>
                                        <asp:Button Text="First" CommandName="Page" CommandArgument="First" runat="server"
                                            ID="btnFirst" />
                                        <asp:Button Text="Previous" CommandName="Page" CommandArgument="Prev" runat="server"
                                            ID="btnPrevious" />
                                        <asp:Button Text="Next" CommandName="Page" CommandArgument="Next" runat="server"
                                            ID="btnNext" />
                                        <asp:Button Text="Last" CommandName="Page" CommandArgument="Last" runat="server"
                                            ID="btnLast" />
                                    </PagerTemplate>
                                </asp:GridView>
                                <asp:ValidationSummary ID="valSum" DisplayMode="BulletList" ShowMessageBox="true"
                                    ShowSummary="false" HeaderText="Validation Messages" Font-Names="'Trebuchet MS'"
                                    Font-Size="12" runat="server" />
                                    </div>
                            </td>
                        </tr>
                        <tr style="height:10%; width:100%">
                            <td style="width: 100%" align="left">
                                <asp:ObjectDataSource ID="srcGridView" runat="server" InsertMethod="InsertAllRecord"
                                    SelectMethod="GetSMSTextData" TypeName="BusinessLogic" OnUpdating="srcGridView_Updating"
                                    DeleteMethod="DeleteSMSText" OnDeleting="GridSource_Deleting">
                                    <SelectParameters>
                                        <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                    </SelectParameters>
                                    <DeleteParameters>
                                        <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                        <asp:Parameter Name="ID" Type="Int32" />
                                    </DeleteParameters>
                                </asp:ObjectDataSource>
                            </td>
                        </tr>
                        <tr style="width:100%">
                            <td align="left" style="width: 100%">
                            </td>
                        </tr>
                    </table>
 
                <%--</td>
                </tr>
                </table>--%>
            </td>
        </tr>
    </table>
</asp:Content>
