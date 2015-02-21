<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="LeadReference.aspx.cs" Inherits="LeadReference" Title="Sale Leads  > Lead Reference " %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <script language="javascript" type="text/javascript">

        //        function Mobile_Validator() {
        //            var ctrMobile = document.getElementById('ctl00_cplhControlPanel_frmViewAdd_txtMobile');

        //            if (ctrMobile == null)
        //                ctrMobile = document.getElementById('ctl00_cplhControlPanel_frmViewAdd_txtMobileAdd');

        //            var txtMobile = ctrMobile.value;

        //            if (txtMobile.length > 0) {
        //                if (txtMobile.length != 10) {
        //                    alert("Customer Mobile Number should be minimum of 10 digits.");
        //                    Page_IsValid = false;
        //                    return window.event.returnValue = false;
        //                }

        //                if (txtMobile.charAt(0) == "0") {
        //                    alert("Customer Mobile should not start with Zero. Please remove Zero and try again.");
        //                    Page_IsValid = false;
        //                    return window.event.returnValue = false;
        //                }
        //            }
        //            else {
        //                Page_IsValid = true;
        //            }
        //        }

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
                                            <span>Lead Reference</span>
                                        </td>
                                    </tr>
                                </table>
                            </div>--%>
                        <%--<table class="mainConHd" style="width: 994px;">
                                <tr valign="middle">
                                    <td style="font-size: 20px;">
                                       Lead Reference
                                    </td>
                                </tr>
                            </table>--%>
                        <div class="mainConBody">
                            <table style="width: 99.8%; margin: -1px 0px 0px 1px;" cellpadding="3" cellspacing="2" class="searchbg">
                                <tr style="height: 25px; vertical-align: middle">
                                    <td style="width: 2%"></td>
                                    <td style="width: 35%; font-size: 22px; color: white;">Defined Drop-down Lists
                                    </td>
                                    <td style="width: 12%">
                                        <%--  <div style="text-align: right;">
                                            <asp:Panel ID="pnlSearch" runat="server" Width="100px">
                                                <asp:Button ID="lnkBtnAdd" runat="server" OnClick="lnkBtnAdd_Click" CssClass="ButtonAdd66"
                                                    EnableTheming="false" Width="80px" Text=""></asp:Button>
                                            </asp:Panel>
                                        </div>--%>
                                    </td>
                                    <td style="width: 12%; color: white;" align="right">Search
                                    </td>
                                    <td style="width: 19%" class="NewBox">
                                        <asp:TextBox ID="txtSearch" runat="server" SkinID="skinTxtBoxSearch"></asp:TextBox>
                                    </td>
                                    <td style="width: 19%" class="NewBox">
                                        <div style="width: 150px; font-family: 'Trebuchet MS';">
                                            <asp:DropDownList ID="ddCriteria" runat="server" Width="155px" Height="23px" BackColor="White" Style="text-align: center; border: 1px solid white">
                                                <asp:ListItem Value="0">All</asp:ListItem>
                                                <asp:ListItem Value="ActivityName">Activity Name</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                    <td style="width: 20%" class="tblLeftNoPad">
                                        <asp:Button ID="btnSearch" runat="server" Text="" CssClass="ButtonSearch6" EnableTheming="false" />
                                    </td>
                                    <td style="width: 16%" class="tblLeftNoPad">
                                        <asp:Button ID="BtnClearFilter" runat="server" OnClick="BtnClearFilter_Click" EnableTheming="false" Text="" CssClass="ClearFilter6" />
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <input id="dummy" type="button" style="display: none" runat="server" />
                        <input id="Button1" type="button" style="display: none" runat="server" />
                        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                            CancelControlID="Button1" DynamicServicePath="" Enabled="True" PopupControlID="popUp"
                            TargetControlID="dummy">
                        </cc1:ModalPopupExtender>

                        <asp:Panel runat="server" ID="popUp" Style="width: 40%">
                            <div id="contentPopUp">
                                <table style="width: 100%;" align="center">
                                    <tr style="width: 100%">
                                        <td style="width: 100%">
                                            <asp:FormView ID="frmViewAdd" runat="server" Width="100%" DataSourceID="frmSource"
                                                DataKeyNames="ID" OnItemCommand="frmViewAdd_ItemCommand" DefaultMode="Edit"
                                                OnItemCreated="frmViewAdd_ItemCreated" Visible="False" OnItemInserting="frmViewAdd_ItemInserting"
                                                OnItemUpdating="frmViewAdd_ItemUpdating" EmptyDataText="No Records" OnItemInserted="frmViewAdd_ItemInserted"
                                                OnItemUpdated="frmViewAdd_ItemUpdated">
                                                <RowStyle HorizontalAlign="left" CssClass="GrdContent allPad" VerticalAlign="Middle"
                                                    BorderColor="#cccccc" Height="20px" />
                                                <EditRowStyle HorizontalAlign="left" CssClass="GrdAlternateColor allPad" BorderColor="#cccccc"
                                                    VerticalAlign="middle" Height="20px" />
                                                <HeaderStyle HorizontalAlign="left" CssClass="GrdHeaderbgClr GrdHdrContent allPad"
                                                    Height="25px" BorderColor="#cccccc" VerticalAlign="Middle" />
                                                <EditItemTemplate>
                                                    <div class="divArea">
                                                        <table cellpadding="1" cellspacing="1" style="border: 1px solid #86b2d1; width: 100%;">
                                                            <tr>
                                                                <td colspan="4" class="headerPopUp">Update Drop-down List
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 5px">
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabelproject" style="width: 40%">Name of Drop-down field *
                                                                    <asp:RequiredFieldValidator ID="rvOpenBal" runat="server" ControlToValidate="ddTypedd"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Reference Type is mandatory">*</asp:RequiredFieldValidator>

                                                                </td>
                                                                <td class="ControlDrpBorder" style="width: 30%">
                                                                    <asp:DropDownList ID="ddTypedd" runat="server" Width="100%" BackColor="#e7e7e7" Style="border: 1px solid #e7e7e7" Height="26px" CssClass="drpDownListMedium" SelectedValue='<%# Bind("TypeID") %>'
                                                                        DataSourceID="srcReferenceType" DataTextField="ReferenceType" DataValueField="TypeID" TabIndex="2">
                                                                        <%-- <asp:ListItem Text="Activity Name" Value="Activity Name"></asp:ListItem>--%>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="width: 30%"></td>
                                                                <td></td>
                                                            </tr>

                                                            <tr style="height: 3px">
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabelproject" style="width: 40%">Value of Drop-down field *
                                                                    <asp:RequiredFieldValidator ID="rvLdgrName" runat="server" ControlToValidate="txtLdgrName"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Text Value is mandatory">*</asp:RequiredFieldValidator>
                                                                </td>
                                                                <td class="ControlTextBox3" style="width: 30%">
                                                                    <asp:TextBox ID="txtLdgrName" runat="server" Text='<%# Bind("TextValue") %>' SkinID="skinTxtBoxGrid"
                                                                        TabIndex="1"></asp:TextBox>
                                                                </td>

                                                                <td style="width: 30%"></td>
                                                                <td></td>
                                                            </tr>
                                                            <tr style="height: 6px">
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4">
                                                                    <table style="width: 100%">
                                                                        <tr>
                                                                            <td style="width: 22%" align="center"></td>
                                                                            <td style="width: 28%" align="center">
                                                                                <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update"
                                                                                    CssClass="Updatebutton1231" EnableTheming="false" SkinID="skinBtnSave"
                                                                                    OnClick="UpdateButton_Click"></asp:Button>
                                                                            </td>
                                                                            <td style="width: 28%" align="center">
                                                                                <asp:Button ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                    CssClass="cancelbutton6" EnableTheming="false" SkinID="skinBtnCancel" OnClick="UpdateCancelButton_Click"></asp:Button>
                                                                            </td>
                                                                            <td style="width: 22%"></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <table cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <asp:ObjectDataSource ID="srcGroupInfo" runat="server" SelectMethod="ListGroupInfo"
                                                                    TypeName="BusinessLogic" OldValuesParameterFormatString="original_{0}">
                                                                    <SelectParameters>
                                                                        <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                    </SelectParameters>
                                                                </asp:ObjectDataSource>

                                                                <asp:ObjectDataSource ID="srcReferenceType" runat="server" SelectMethod="ListReferenceType"
                                                                    TypeName="BusinessLogic" OldValuesParameterFormatString="original_{0}">
                                                                    <SelectParameters>
                                                                        <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                    </SelectParameters>
                                                                </asp:ObjectDataSource>

                                                                <td>
                                                                    <asp:ValidationSummary ID="valSum" DisplayMode="BulletList" ShowMessageBox="true"
                                                                        ShowSummary="false" HeaderText="Validation Messages" Font-Names="'Trebuchet MS'"
                                                                        Font-Size="12" runat="server" />
                                                                </td>
                                                                <td></td>
                                                                <td></td>
                                                        </tr>
                                                    </table>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                                <InsertItemTemplate>
                                                    <div class="divArea">
                                                        <table cellpadding="1" cellspacing="1" style="border: 1px solid #86b2d1; width: 100%;">
                                                            <tr>
                                                                <td colspan="4" class="headerPopUp">Add New Drop-down List
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 5px">
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabelproject" style="width: 40%">Name of Drop-down field *
                                                                    <asp:RequiredFieldValidator ID="rvOpenBalAdd" runat="server" ControlToValidate="ddType"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Reference Type is mandatory">*</asp:RequiredFieldValidator>

                                                                </td>
                                                                <td class="ControlDrpBorder" style="width: 30%">
                                                                    <asp:DropDownList ID="ddType" runat="server" Width="100%" BackColor="#e7e7e7" Style="border: 1px solid #e7e7e7" Height="26px" CssClass="drpDownListMedium" SelectedValue='<%# Bind("TypeID") %>'
                                                                        DataSourceID="srcReferenceTypeAdd" DataTextField="ReferenceType" DataValueField="TypeID" TabIndex="2">
                                                                        <%--<asp:ListItem Text="Activity Name" Value="Activity Name"></asp:ListItem>--%>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="width: 30%"></td>
                                                                <td></td>
                                                            </tr>
                                                            <tr style="height: 3px">
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabelproject" style="width: 40%">Value of Drop-down field *
                                                                    <asp:RequiredFieldValidator ID="rvLdgrNameAdd" runat="server" ControlToValidate="txtLdgrNameAdd"
                                                                        Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="Text ValueName is mandatory"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td class="ControlTextBox3" style="width: 30%">
                                                                    <asp:TextBox ID="txtLdgrNameAdd" runat="server" Text='<%# Bind("TextValue") %>'
                                                                        SkinID="skinTxtBoxGrid" TabIndex="1"></asp:TextBox>
                                                                </td>

                                                                <td style="width: 30%"></td>
                                                                <td></td>
                                                            </tr>                                                           
                                                            <tr style="height: 6px">
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4">
                                                                    <table style="width: 100%">
                                                                        <tr>
                                                                            <td style="width: 22%" align="center"></td>
                                                                            <td style="width: 28%" align="center">
                                                                                <asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                                                                    CssClass="savebutton1231" EnableTheming="false" SkinID="skinBtnSave"
                                                                                    OnClick="InsertButton_Click"></asp:Button>
                                                                            </td>
                                                                            <td style="width: 28%" align="center">
                                                                                <asp:Button ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                    CssClass="cancelbutton6" EnableTheming="false" SkinID="skinBtnCancel" OnClick="InsertCancelButton_Click"></asp:Button>
                                                                            </td>
                                                                            <td style="width: 22%" align="center"></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>

                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <table cellspacing="0">
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:ValidationSummary ID="valSumAdd" DisplayMode="BulletList" ShowMessageBox="true"
                                                                    ShowSummary="false" HeaderText="Validation Messages" Font-Names="'Trebuchet MS'"
                                                                    Font-Size="12" runat="server" />
                                                            </td>
                                                            <td colspan="2">
                                                                <asp:ObjectDataSource ID="srcGroupInfoAdd" runat="server" SelectMethod="ListGroupInfoExp"
                                                                    TypeName="BusinessLogic" OldValuesParameterFormatString="original_{0}">
                                                                    <SelectParameters>
                                                                        <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                    </SelectParameters>
                                                                </asp:ObjectDataSource>
                                                            </td>
                                                            <td colspan="2">
                                                                <asp:ObjectDataSource ID="srcReferenceTypeAdd" runat="server" SelectMethod="ListReferenceType"
                                                                    TypeName="BusinessLogic" OldValuesParameterFormatString="original_{0}">
                                                                    <SelectParameters>
                                                                        <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                    </SelectParameters>
                                                                </asp:ObjectDataSource>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </InsertItemTemplate>
                                            </asp:FormView>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                    </td>
                </tr>
                <tr style="width: 100%;">
                    <td style="width: 100%;">
                        <table width="100%" style="margin: -4px 0px 0px 0px;">
                            <tr style="width: 100%">
                                <td>
                                    <div class="mainGridHold" id="searchGrid">
                                        <asp:GridView ID="GrdViewLedger" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                            OnRowCreated="GrdViewLedger_RowCreated" Width="100.4%" DataSourceID="GridSource" CssClass="someClass"
                                            AllowPaging="True" DataKeyNames="ID" EmptyDataText="No Reference Data Found."
                                            OnRowCommand="GrdViewLedger_RowCommand" OnRowDataBound="GrdViewLedger_RowDataBound" OnRowDeleting="GrdViewLedger_RowDeleting"
                                            OnRowDeleted="GrdViewLedger_RowDeleted">

                                            <HeaderStyle Height="30px" HorizontalAlign="Center" Font-Bold="true" BackColor="#cccccc" BorderColor="Gray" Font-Size="15px" />
                                            <RowStyle Font-Bold="true" HorizontalAlign="Center" Height="30px" Font-Size="15px" ForeColor="#0567AE" />

                                            <Columns>
                                                <asp:BoundField DataField="Row" HeaderText="#" HeaderStyle-HorizontalAlign="Center" ItemStyle-Font-Size="15px" HeaderStyle-Height="20px" ItemStyle-HorizontalAlign="Center" ItemStyle-Height="30px" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="#0567AE" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="Small" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="75px" />
                                                <asp:BoundField DataField="TypeName" HeaderText="Name of Drop-down field" ItemStyle-Font-Size="15px" HeaderStyle-Height="20px" ItemStyle-HorizontalAlign="Left" ItemStyle-Height="30px" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="#0567AE" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="Small" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="690px" />
                                                <asp:BoundField DataField="TextValue" HeaderText="Values of Drop-down field" ItemStyle-Font-Size="15px" HeaderStyle-Height="20px" ItemStyle-HorizontalAlign="Left" ItemStyle-Height="30px" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="#0567AE" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="Small" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="690px" />
                                                <asp:TemplateField ItemStyle-CssClass="command" HeaderText="Edit" ItemStyle-Width="50px" ItemStyle-Font-Size="15px" HeaderStyle-BorderColor="Gray"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Select" />
                                                        <asp:ImageButton ID="btnEditDisabled" Enabled="false" SkinID="editDisable" runat="Server"></asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="command" HeaderText="Delete" ItemStyle-Font-Size="15px" HeaderStyle-Width="50px" HeaderStyle-BorderColor="Gray"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to Delete this Reference?"
                                                            runat="server">
                                                        </cc1:ConfirmButtonExtender>
                                                        <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" CommandName="Delete"></asp:ImageButton>
                                                        <asp:ImageButton ID="lnkBDisabled" Enabled="false" SkinID="deleteDisable" runat="Server"></asp:ImageButton>
                                                        <asp:HiddenField ID="ldgID" runat="server" Value='<%# Bind("ID") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerTemplate>
                                                <table style="border-color: white">
                                                    <tr style="border-color: white">
                                                        <td style="border-color: white">Goto Page
                                                        </td>
                                                        <td style="border-color: white">
                                                            <asp:DropDownList ID="ddlPageSelector" runat="server" AutoPostBack="true" Width="65px" BackColor="#e7e7e7" Style="border: 1px solid blue">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="width: 5px; border-color: white"></td>
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
                <tr style="width: 100%;">
                    <td style="width: 918px" align="left">
                        <asp:ObjectDataSource ID="GridSource" runat="server" SelectMethod="ListReferenceInfo"
                            TypeName="LeadBusinessLogic" DeleteMethod="DeleteReference" OnDeleting="GridSource_Deleting">
                            <DeleteParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                <asp:Parameter Name="ID" Type="Int32" />
                            </DeleteParameters>
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="frmSource" runat="server" SelectMethod="GetReferenceForId"
                            TypeName="LeadBusinessLogic" OnUpdating="frmSource_Updating" OnInserting="frmSource_Inserting"
                            InsertMethod="InsertReference" UpdateMethod="UpdateReference">
                            <UpdateParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                <asp:Parameter Name="ID" Type="Int32" />
                                <asp:Parameter Name="TextValue" Type="String" />
                                <asp:Parameter Name="TypeName" Type="String" />
                                <asp:Parameter Name="Types" Type="String" />
                                <asp:Parameter Name="TypeID" Type="Int32" />
                            </UpdateParameters>
                            <SelectParameters>
                                <asp:ControlParameter ControlID="GrdViewLedger" Name="ID" PropertyName="SelectedValue"
                                    Type="String" />
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                            </SelectParameters>
                            <InsertParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                <asp:Parameter Name="TextValue" Type="String" />
                                <asp:Parameter Name="TypeName" Type="String" />
                                <asp:Parameter Name="Types" Type="String" />
                                <asp:Parameter Name="TypeID" Type="Int32" />
                            </InsertParameters>
                        </asp:ObjectDataSource>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%"></td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div align="center">
        <asp:Button ID="lnkBtnAdd" runat="server" OnClick="lnkBtnAdd_Click" CssClass="ButtonAdd66"
            EnableTheming="false" Width="80px" Text=""></asp:Button>
    </div>
</asp:Content>
