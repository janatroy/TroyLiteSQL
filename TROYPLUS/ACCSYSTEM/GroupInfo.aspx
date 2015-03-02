<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="~/GroupInfo.aspx.cs" Inherits="GroupInfo" Title="Financials > Group Information" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
     <script language="javascript" type="text/javascript">

         window.onload = function Showalert() {

             var txt = document.getElementById("<%= txtSearch.ClientID %>")
              <%--  var btn = document.getElementById("<%= BtnClearFilter.ClientID %>");--%>
                if (txt.value == "") {
                    // alert(txt.value);
                    btn.style.visibility = "hidden";
                    // when the window is loaded, hide the button if the textbox is empty
                }

            }

            function EnableDisableButton(sender, target) {
                var first = document.getElementById('<%=txtSearch.ClientID %>');

                if (sender.value.length >= 1 && first.value.length >= 1) {
                   <%-- document.getElementById('<%=BtnClearFilter.ClientID %>').style.visibility = "visible";--%>

                }

                if (sender.value.length < 1 && first.value.length < 1) {

                  <%--  document.getElementById('<%=BtnClearFilter.ClientID %>').style.visibility = "Hidden";--%>
                }
            }
        </script>
    <asp:UpdatePanel ID="UpdatePanel16" runat="server" UpdateMode="Always">

       
        <ContentTemplate>

            <table style="width: 100%">
                <tr style="width: 100%">
                    <td style="width: 100%">
                        <%--<div class="mainConHd">
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tr valign="middle">
                                        <td>
                                            <span>Account Groups</span>
                                        </td>
                                    </tr>
                                </table>
                            </div>--%>
                        <%--<table class="mainConHd" style="width: 994px;">
                                <tr valign="middle">
                                    <td style="font-size: 20px;">
                                        Account Groups
                                    </td>
                                </tr>
                            </table>--%>
                        <div class="mainConBody">
                            <table style="width: 99.7%; margin: -1px 0px 0px 2px;" cellpadding="3" cellspacing="2" class="searchbg">
                                <tr style="height: 25px; vertical-align: middle">
                                    <td style="width: 2%;"></td>
                                    <td style="width: 20%; font-size: 22px; color: White;">Account Groups
                                    </td>
                                    <td style="width: 14%">
                                        <div style="text-align: left;">
                                         
                                        </div>
                                    </td>

                                    <td style="width: 13%; color: White;" align="right">Search
                                    </td>
                                    <td style="width: 19%" class="NewBox">
                                        <div style="text-align: left;">
                                            <asp:Panel ID="Panel2" runat="server" Width="100px">
                                                <asp:TextBox ID="txtSearch" runat="server"
                                                    OnTextChanged="txtSearch_TextChanged" CssClass="cssTextBox"></asp:TextBox>
                                            </asp:Panel>
                                        </div>
                                    </td>
                                    <td style="width: 19%;" class="NewBox">
                                        <div style="text-align: left; width: 160px">
                                            <asp:Panel ID="Panel3" runat="server" Width="160px">
                                                <asp:DropDownList ID="ddCriteria" runat="server" Width="155px" Height="23px" BackColor="White" Style="text-align: center; border: 1px solid White"
                                                    AutoPostBack="false"
                                                    OnSelectedIndexChanged="ddCriteria_SelectedIndexChanged">
                                                    <asp:ListItem Value="GroupName">Group Name</asp:ListItem>
                                                </asp:DropDownList>
                                            </asp:Panel>
                                        </div>
                                    </td>
                                    <td style="width: 19%" class="tblLeftNoPad">
                                        <div style="text-align: left;">
                                            <asp:Panel ID="Panel4" runat="server" Width="100px">
                                                <asp:Button ID="btnSearch" onkeyup="EnableDisableButton(this,'BtnClearFilter')" runat="server" Text="" CssClass="ButtonSearch6" EnableTheming="false"
                                                    OnClick="btnSearch_Click" />
                                            </asp:Panel>
                                        </div>
                                    </td>
                                </tr>
                            </table>

                            <input id="dummy" type="button" style="display: none" runat="server" />
                            <input id="Button1" type="button" style="display: none" runat="server" />
                            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                                CancelControlID="Button1" DynamicServicePath="" Enabled="True" PopupControlID="popUp"
                                TargetControlID="dummy">
                            </cc1:ModalPopupExtender>
                            <asp:Panel runat="server" ID="popUp" Style="width: 55%">
                                <div id="contentPopUp">
                                    <table style="width: 100%;" align="center">
                                        <tr style="text-align: left" align="center">
                                            <div id="Div1" align="center">
                                                <td align="center">
                                                    <asp:FormView ID="frmViewDetails" runat="server" Width="60%" DataKeyNames="GroupID"
                                                        DefaultMode="Edit" DataSourceID="frmSource" OnItemUpdated="frmViewDetails_ItemUpdated"
                                                        OnItemInserted="frmViewDetails_ItemInserted" OnItemCommand="frmViewDetails_ItemCommand"
                                                        OnItemInserting="frmViewDetails_ItemInserting">
                                                        <RowStyle HorizontalAlign="left" CssClass="GrdContent allPad" VerticalAlign="Middle"
                                                            BorderColor="#cccccc" Height="20px" />
                                                        <EditRowStyle HorizontalAlign="left" CssClass="GrdAlternateColor allPad" BorderColor="#cccccc"
                                                            VerticalAlign="middle" Height="20px" />
                                                        <HeaderStyle HorizontalAlign="left" CssClass="GrdHeaderbgClr GrdHdrContent allPad"
                                                            Height="25px" BorderColor="#cccccc" VerticalAlign="Middle" />
                                                        <EditItemTemplate>
                                                            <div class="divArea" align="center">
                                                                <table cellpadding="1" cellspacing="1" style="width: 100%;">
                                                                    <tr>
                                                                        <td colspan="4" class="headerPopUp">Group
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="height: 10px">
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="ControlLabel" style="width: 33%">Group Name
                                                                        <asp:RequiredFieldValidator ID="rvEEstMnds" runat="server" ControlToValidate="txtGroupName"
                                                                            Display="Dynamic" EnableClientScript="true" ErrorMessage="Estimated Mandays is Mandatory">*</asp:RequiredFieldValidator>
                                                                        </td>
                                                                        <td class="ControlTextBox3" style="width: 40%">
                                                                            <asp:TextBox ID="txtGroupName" runat="server" Text='<%# Bind("GroupName") %>' Width="100%"
                                                                                SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                        </td>
                                                                        <td style="width: 15%"></td>
                                                                    </tr>
                                                                    <tr style="height: 2px">
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="ControlLabel" style="width: 33%">Heading
                                                                        <asp:CompareValidator ID="cvHeading" runat="server" ControlToValidate="ddHeading"
                                                                            Display="Dynamic" EnableClientScript="true" ErrorMessage="Please select the Item"
                                                                            Operator="GreaterThan" ValueToCompare="0">*</asp:CompareValidator>
                                                                        </td>
                                                                        <td class="ControlDrpBorder" style="width: 40%">
                                                                            <asp:DropDownList ID="ddHeading" DataSourceID="srcHeading" runat="server" CssClass="drpDownListMedium" BackColor="#e7e7e7"
                                                                                SelectedValue='<%# Bind("HeadingID") %>' DataTextField="Heading" DataValueField="HeadingID" Style="border: 1px solid #e7e7e7" Height="26px"
                                                                                Width="100%" AppendDataBoundItems="True">
                                                                                <asp:ListItem Selected="True" style="background-color: #e7e7e7" Value="0">Select Heading</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="height: 10px">
                                                                    </tr>
                                                                    <tr>

                                                                        <td colspan="4">
                                                                            <table style="width: 100%">
                                                                                <tr>
                                                                                    <td align="center" style="width: 15%"></td>
                                                                                    <td style="width: 30%">
                                                                                        <asp:Button ID="Button1" runat="server" SkinID="skinBtnSave" CausesValidation="True"
                                                                                            CssClass="Updatebutton1231" EnableTheming="false" CommandName="Update" OnClick="UpdateButton_Click"></asp:Button>
                                                                                    </td>
                                                                                    <td style="width: 30%">
                                                                                        <asp:Button ID="UpdateCancelButton" runat="server" CommandName="Cancel" CssClass="cancelbutton6"
                                                                                            EnableTheming="false" SkinID="skinBtnCancel" OnClick="UpdateCancelButton_Click"></asp:Button>
                                                                                    </td>
                                                                                    <td align="center" style="width: 15%"></td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>

                                                                        <%--<td align="right">
                                                                
                                                                    </td>
                                                                    <td align="left">
                                                                
                                                                    </td>
                                                                    <td>
                                                                    </td>--%>
                                                                    </tr>
                                                                    <tr style="height: 10px">
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                            <table cellspacing="0">
                                                                <tr>
                                                                    <td>
                                                                        <asp:ObjectDataSource ID="srcHeading" runat="server" SelectMethod="ListHeading" TypeName="BusinessLogic">
                                                                            <SelectParameters>
                                                                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                            </SelectParameters>
                                                                        </asp:ObjectDataSource>
                                                                    </td>
                                                                    <td>
                                                                        <asp:ValidationSummary ID="VSEdit" DisplayMode="BulletList" ShowMessageBox="true"
                                                                            ShowSummary="false" HeaderText="Validation Messages" Font-Names="'Trebuchet MS'"
                                                                            Font-Size="12" runat="server" />
                                                                    </td>
                                                                    <td></td>
                                                                    <td></td>
                                                                </tr>
                                                            </table>
                                                        </EditItemTemplate>
                                                        <InsertItemTemplate>
                                                            <div id="Div5" align="center">
                                                                <table cellpadding="2" cellspacing="2" style="border: 1px solid White; width: 100%;" align="center">
                                                                    <tr>
                                                                        <td colspan="4" class="headerPopUp">Group
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="height: 10px">
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="ControlLabel" style="width: 33%">Group Name
                                                                        <asp:RequiredFieldValidator ID="rvIgroupAdd" runat="server" ControlToValidate="txtIGroupAdd"
                                                                            Display="Dynamic" EnableClientScript="true" ErrorMessage="Group Name is Mandatory">*</asp:RequiredFieldValidator>
                                                                        </td>
                                                                        <td class="ControlTextBox3" style="width: 40%">
                                                                            <asp:TextBox ID="txtIGroupAdd" runat="server" Text='<%# Bind("GroupName") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                        </td>
                                                                        <td style="width: 15%"></td>
                                                                    </tr>

                                                                    <tr style="height: 2px">
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="ControlLabel" style="width: 33%">Heading
                                                                        <asp:CompareValidator ID="cvHeadingAdd" runat="server" ControlToValidate="ddIHeadingAdd"
                                                                            ErrorMessage="Heading is Mandatory" Operator="GreaterThan" ValueToCompare="0"
                                                                            EnableClientScript="true">*</asp:CompareValidator>
                                                                        </td>
                                                                        <td class="ControlDrpBorder" style="width: 40%">
                                                                            <asp:DropDownList ID="ddIHeadingAdd" DataSourceID="srcHeadingAdd" runat="server" CssClass="drpDownListMedium" BackColor="#e7e7e7"
                                                                                DataTextField="Heading" DataValueField="HeadingID" SelectedValue='<%# Bind("HeadingID") %>' Style="border: 1px solid #e7e7e7" Height="26px"
                                                                                Width="100%" AppendDataBoundItems="True">
                                                                                <asp:ListItem Selected="True" style="background-color: #e7e7e7" Value="0">Select Heading</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td style="width: 15%"></td>
                                                                    </tr>
                                                                    <tr style="height: 10px">
                                                                        <td colspan="4"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="4">
                                                                            <table style="width: 100%">
                                                                                <tr>
                                                                                    <td align="center" style="width: 20%"></td>
                                                                                    <td align="center" style="width: 30%">
                                                                                        <asp:Button ID="InsertButton" runat="server" SkinID="skinBtnSave" CausesValidation="True"
                                                                                            CssClass="savebutton1231" EnableTheming="false" CommandName="Insert" OnClick="InsertButton_Click"></asp:Button>
                                                                                    </td>
                                                                                    <td align="center" style="width: 30%">
                                                                                        <asp:Button ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                            CssClass="cancelbutton6" EnableTheming="false" SkinID="skinBtnCancel" OnClick="InsertCancelButton_Click"></asp:Button>
                                                                                    </td>
                                                                                    <td align="center" style="width: 20%"></td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="height: 10px">
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                            <table cellspacing="0">
                                                                <tr>
                                                                    <td>
                                                                        <asp:ObjectDataSource ID="srcHeadingAdd" runat="server" SelectMethod="ListHeading"
                                                                            TypeName="BusinessLogic">
                                                                            <SelectParameters>
                                                                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                            </SelectParameters>
                                                                        </asp:ObjectDataSource>
                                                                    </td>
                                                                    <td>
                                                                        <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="BulletList" ShowMessageBox="true"
                                                                            ShowSummary="false" HeaderText="Validation Messages" Font-Names="'Trebuchet MS'"
                                                                            Font-Size="12" runat="server" />
                                                                    </td>
                                                                    <td></td>
                                                                    <td></td>
                                                                </tr>
                                                            </table>
                                                        </InsertItemTemplate>
                                                    </asp:FormView>
                                                </td>
                                            </div>
                                        </tr>
                                    </table>
                                </div>
                            </asp:Panel>
                    </td>
                </tr>
                <tr style="width: 100%;">
                    <td>
                        <table width="100%" style="margin: -3px 0px 0px 1px;">
                            <tr>
                                <td>
                                    <div id="Div5">
                                        <asp:GridView ID="grdViewGroup" runat="server" EmptyDataText="No Group found!" DataKeyNames="GroupID"
                                            AutoGenerateColumns="False" AllowPaging="True" GridLines="Horizontal" Width="100%"
                                            OnRowCreated="grdGroup_RowCreated" HeaderStyle-HorizontalAlign="Center" DataSourceID="grdSource"
                                            OnRowCommand="grdViewGroup_RowCommand" OnRowDataBound="grdViewGroup_RowDataBound" CssClass="someClass">
                                            <HeaderStyle Height="30px" HorizontalAlign="Center" Font-Bold="true" BackColor="#cccccc" BorderColor="Gray" Font-Size="Small" />
                                            <RowStyle Font-Bold="true" HorizontalAlign="Center" Height="30px" Font-Size="Small" ForeColor="#0567AE" />
                                            <Columns>
                                                <asp:BoundField DataField="GroupName" HeaderText="Group Name" HeaderStyle-BorderColor="Gray">
                                                    <ItemStyle Width="50%" Wrap="False" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Heading" HeaderText="Heading" HeaderStyle-BorderColor="Gray">
                                                    <ItemStyle Wrap="true" Width="40%" />
                                                </asp:BoundField>
                                                <asp:TemplateField ItemStyle-CssClass="command" HeaderText="Edit" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderColor="Gray">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEditDisabled" Enabled="false" SkinID="editDisable" runat="Server"></asp:ImageButton>
                                                        <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Select" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerTemplate>
                                                <table style="border-color: white">
                                                    <tr style="border-color: white">
                                                        <td style="border-color: white">Goto Page
                                                        </td>
                                                        <td style="border-color: white">
                                                            <asp:DropDownList ID="ddlPageSelector" runat="server" Width="65px" BackColor="#BBCAFB" Style="border: 1px solid blue" AutoPostBack="true">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="border-color: white; width: 5px"></td>
                                                        <td style="border-color: white">
                                                            <asp:Button Text="" CommandName="Page" CommandArgument="First" runat="server" CssClass="NewFirst" EnableTheming="false" Width="22px" Height="18px"
                                                                ID="btnFirst" />
                                                        </td>
                                                        <td style="border-color: white">
                                                            <asp:Button Text="" CommandName="Page" CommandArgument="Prev" runat="server" CssClass="NewPrev" EnableTheming="false" Width="22px" Height="18px"
                                                                ID="btnPrevious" />
                                                        </td>
                                                        <td style="border-color: white">
                                                            <asp:Button Text="" CommandName="Page" CommandArgument="Next" runat="server" CssClass="NewNext" EnableTheming="false" Width="22px" Height="18px"
                                                                ID="btnNext" />
                                                        </td>
                                                        <td style="border-color: white">
                                                            <asp:Button Text="" CommandName="Page" CommandArgument="Last" runat="server" CssClass="NewLast" EnableTheming="false" Width="22px" Height="18px"
                                                                ID="btnLast" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </PagerTemplate>
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:ObjectDataSource ID="grdSource" runat="server" SelectMethod="ListGroupInfo"
                            TypeName="BusinessLogic">
                            <SelectParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="frmSource" runat="server" SelectMethod="GetGroupInfoForId"
                            TypeName="BusinessLogic" UpdateMethod="UpdateGroupInfo" InsertMethod="InsertGroupInfo"
                            OnInserting="frmSource_Inserting" OnUpdating="frmSource_Updating">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="grdViewGroup" Name="GroupID" PropertyName="SelectedValue"
                                    Type="Int32" />
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                            </SelectParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="GroupId" Type="Int32" />
                                <asp:Parameter Name="HeadingId" Type="Int32" />
                                <asp:Parameter Name="GroupName" Type="String" />
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                            </UpdateParameters>
                            <InsertParameters>
                                <asp:Parameter Name="GroupId" Type="Int32" />
                                <asp:Parameter Name="HeadingId" Type="Int32" />
                                <asp:Parameter Name="GroupName" Type="String" />
                                <asp:Parameter Name="Order" Type="Int32" />
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                            </InsertParameters>
                        </asp:ObjectDataSource>

                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                           <asp:Panel ID="pnlSearch" runat="server" Width="100px">
                                                <asp:Button ID="lnkBtnAddGroup" runat="server" OnClick="lnkBtnAddGroup_Click" CssClass="ButtonAdd66"
                                                    EnableTheming="false" Width="80px" Text=""></asp:Button>
                                            </asp:Panel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
