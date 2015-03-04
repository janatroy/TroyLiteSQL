<%@ Page Title="Inventory > Opening Stock" Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="OpeningStock.aspx.cs" Inherits="OpeningStock" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <script language="javascript" type="text/javascript">
        function OpenWindow() {
            var ddLedger = document.getElementById('ctl00_cplhControlPanel_drpCustomer');
            var iLedger = ddLedger.options[ddLedger.selectedIndex].text;
            window.open('Service.aspx?ID=' + iLedger, '', "height=400, width=700,resizable=yes, toolbar =no");
            return false;
        }
        window.onload = function Showalert() {

            var txt = document.getElementById("<%= txtSearch.ClientID %>");
            var btn = document.getElementById("<%= BtnClearFilter.ClientID %>");
            if (txt.value == "") {
                // alert(txt.value);
                btn.style.visibility = "hidden";
                // when the window is loaded, hide the button if the textbox is empty
            }

        }

        function EnableDisableButton(sender, target) {
            var first = document.getElementById('<%=txtSearch.ClientID %>');

            if (sender.value.length >= 1 && first.value.length >= 1) {
                document.getElementById('<%=BtnClearFilter.ClientID %>').style.visibility = "visible";

            }

            if (sender.value.length < 1 && first.value.length < 1) {

                document.getElementById('<%=BtnClearFilter.ClientID %>').style.visibility = "Hidden";
            }
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel16" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <table style="width: 100%">
                <tr style="width: 100%">
                    <td style="width: 100%">

                        <%--<div class="mainConHd">
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tr valign="middle">
                                        <td>
                                            <span>Service Visits</span>
                                        </td>
                                    </tr>
                                </table>
                            </div>--%>
                        <%--<table class="mainConHd" style="width: 994px;">
                                <tr valign="middle">
                                    <td style="font-size: 20px;">
                                        Opening Stock
                                    </td>
                                </tr>
                            </table>--%>
                        <div class="mainConBody">
                            <table style="width: 99.8%; margin: -2px 0px 0px 1px;" cellpadding="3" cellspacing="2" class="searchbg">
                                <tr style="height: 25px; vertical-align: middle">
                                    <td style="width: 2%;"></td>
                                    <td style="width: 20%; font-size: 22px; color: White;">Opening Stock
                                    </td>
                                    <td style="width: 16%">
                                        <div style="text-align: right;">
                                         
                                        </div>
                                    </td>
                                    <td style="width: 15%; color: White;" align="right">Search
                                    </td>
                                    <td style="width: 18%" class="NewBox">
                                        <asp:TextBox ID="txtSearch" runat="server" SkinID="skinTxtBoxSearch"></asp:TextBox>
                                    </td>
                                    <td style="width: 18%" class="NewBox">
                                        <div style="width: 160px; font-family: 'Trebuchet MS';">
                                            <asp:DropDownList ID="ddCriteria" runat="server" Width="154px" BackColor="White" Height="23px" Style="text-align: center; border: 1px solid White">
                                                <asp:ListItem Value="ItemCode">Product Code</asp:ListItem>
                                                <asp:ListItem Value="ProductName">Product Name</asp:ListItem>
                                                <asp:ListItem Value="Model">Model</asp:ListItem>
                                                <asp:ListItem Value="Brand">Brand</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                    <td style="width: 22%" class="tblLeftNoPad">
                                        <asp:Button ID="btnSearch" onkeyup="EnableDisableButton(this,'BtnClearFilter')" runat="server" Text="" CssClass="ButtonSearch6" EnableTheming="false" OnClick="btnSearch_Click" />
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
                        <asp:Panel runat="server" ID="popUp" Style="width: 54%">
                            <div id="contentPopUp">
                                <table cellpadding="2" cellspacing="2" style="border: 1px solid blue; background-color: #fff; color: #000;"
                                    width="100%">
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlVisitDetails" runat="server" Visible="false">
                                                <div>
                                                    <table cellpadding="2" cellspacing="1" style="border: 0px solid blue;"
                                                        width="100%">
                                                        <tr>
                                                            <tr>
                                                                <td colspan="5">
                                                                    <table class="headerPopUp" width="100%">
                                                                        <tr>
                                                                            <td>Opening Stock
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 5px">
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width: 25%;">Category *
                                                                     <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="cmbCategory"
                                                                         Display="Dynamic" ErrorMessage="Please Select Category. It cannot be left blank." Operator="GreaterThan"
                                                                         Text="*" ValueToCompare="0"></asp:CompareValidator>
                                                                </td>
                                                                <td class="ControlDrpBorder" style="width: 25%;">
                                                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always">
                                                                        <ContentTemplate>
                                                                            <asp:DropDownList ID="cmbCategory" runat="server" AutoPostBack="true" BackColor="#e7e7e7"
                                                                                Width="100%" OnSelectedIndexChanged="LoadProducts" AppendDataBoundItems="True" Style="border: 1px solid #e7e7e7" Height="26px" CssClass="drpDownListMedium">
                                                                                <asp:ListItem Selected="True" Value="0" style="background-color: #e7e7e7">Select Category</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </ContentTemplate>
                                                                        <%--<Triggers>
                                                                                                        <asp:AsyncPostBackTrigger ControlID="cmbCategory" EventName="SelectedIndexChanged" />
                                                                                                        </Triggers>--%>
                                                                    </asp:UpdatePanel>

                                                                </td>
                                                                <td class="ControlLabel" style="width: 15%;">Brand *
                                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="cmbBrand"
                                                                        Display="Dynamic" ErrorMessage="Please Select Brand.It cannot be let blank." Operator="GreaterThan"
                                                                        Text="*" ValueToCompare="0"></asp:CompareValidator>
                                                                </td>
                                                                <td class="ControlDrpBorder" style="width: 25%">
                                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                                                                        <ContentTemplate>
                                                                            <asp:DropDownList ID="cmbBrand" runat="server" Width="100%" AutoPostBack="true" BackColor="#e7e7e7" Style="border: 1px solid #e7e7e7" Height="26px" CssClass="drpDownListMedium"
                                                                                OnSelectedIndexChanged="LoadForBrand" AppendDataBoundItems="True">
                                                                                <asp:ListItem Selected="True" Value="0" style="background-color: #e7e7e7">Select Brand</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </ContentTemplate>
                                                                        <%--<Triggers>
                                                                                                        <asp:AsyncPostBackTrigger ControlID="cmbCategory" EventName="SelectedIndexChanged" />
                                                                                                        </Triggers>--%>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                                <td style="width: 10%"></td>
                                                            </tr>
                                                            <tr style="height: 2px">
                                                            </tr>
                                                            <tr>

                                                                <td class="ControlLabel" style="width: 25%;">Product Name
                                                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="cmbProdName"
                                                                        Display="Dynamic" ErrorMessage="Please Select Product Name. It cannot be let blank." Operator="GreaterThan"
                                                                        Text="*" ValueToCompare="0"></asp:CompareValidator>
                                                                </td>
                                                                <td class="ControlDrpBorder" style="width: 25%;">
                                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                                                                        <ContentTemplate>
                                                                            <asp:DropDownList ID="cmbProdName" runat="server" Width="100%" BackColor="#e7e7e7" Style="border: 1px solid #e7e7e7" Height="26px" CssClass="drpDownListMedium"
                                                                                AutoPostBack="true" OnSelectedIndexChanged="LoadForProductName" AppendDataBoundItems="True">
                                                                                <asp:ListItem Selected="True" Value="0" style="background-color: #e7e7e7">Select Product</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </ContentTemplate>
                                                                        <%--<Triggers>
                                                                                                        <asp:AsyncPostBackTrigger ControlID="cmbCategory" EventName="SelectedIndexChanged" />
                                                                                                        </Triggers>--%>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                                <td class="ControlLabel" style="width: 15%;">Model *
                                                                    <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="cmbModel"
                                                                        Display="Dynamic" ErrorMessage="Please Select Model. It cannot be let blank." Operator="GreaterThan"
                                                                        Text="*" ValueToCompare="0"></asp:CompareValidator>
                                                                </td>
                                                                <td class="ControlDrpBorder" style="width: 25%;">
                                                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Always">
                                                                        <ContentTemplate>
                                                                            <asp:DropDownList ID="cmbModel" runat="server" OnSelectedIndexChanged="LoadForModel" BackColor="#e7e7e7" Style="border: 1px solid #e7e7e7" Height="26px" CssClass="drpDownListMedium"
                                                                                AutoPostBack="true" Width="100%" AppendDataBoundItems="True">
                                                                                <asp:ListItem Selected="True" Value="0" style="background-color: #e7e7e7">Select Model</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </ContentTemplate>
                                                                        <%--<Triggers>
                                                                                                        <asp:AsyncPostBackTrigger ControlID="cmbCategory" EventName="SelectedIndexChanged" />
                                                                                                        </Triggers>--%>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                                <td style="width: 10%"></td>
                                                            </tr>
                                                            <tr style="height: 2px">
                                                            </tr>
                                                            <tr>

                                                                <td class="ControlLabel" style="width: 25%;">Product Code *
                                                                    <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="cmbProdAdd"
                                                                        Display="Dynamic" ErrorMessage="Please Select Product Code. It cannot be let blank." Operator="GreaterThan"
                                                                        Text="*" ValueToCompare="0"></asp:CompareValidator>
                                                                </td>
                                                                <td class="ControlDrpBorder" style="width: 25%">
                                                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Always">
                                                                        <ContentTemplate>
                                                                            <asp:DropDownList ID="cmbProdAdd" runat="server" AppendDataBoundItems="true" AutoPostBack="true" BackColor="#e7e7e7"
                                                                                DataTextField="ProductName" DataValueField="ItemCode" OnSelectedIndexChanged="LoadForProduct" Style="border: 1px solid #e7e7e7" Height="26px" CssClass="drpDownListMedium"
                                                                                ValidationGroup="product" Width="100%">
                                                                                <asp:ListItem style="background-color: #e7e7e7;" Text="Select Product Code" Value="0"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </ContentTemplate>
                                                                        <%--<Triggers>
                                                                                                        <asp:AsyncPostBackTrigger ControlID="cmbCategory" EventName="SelectedIndexChanged" />
                                                                                                        </Triggers>--%>
                                                                    </asp:UpdatePanel>

                                                                </td>
                                                                <td style="width: 15%" class="ControlLabel">Opening Stock *
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtOpeningStock"
                                                                        Display="Dynamic" ErrorMessage="Please enter Opening Stock. It cannot be let blank">*</asp:RequiredFieldValidator>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Custom, Numbers"
                                                                        TargetControlID="txtOpeningStock" ValidChars="+" />
                                                                </td>
                                                                <td style="width: 25%" class="ControlTextBox3">
                                                                    <asp:TextBox ID="txtOpeningStock" runat="server" BackColor="#e7e7e7" SkinID="skinTxtBoxGrid"
                                                                        Width="100px"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 10%"></td>
                                                            </tr>
                                                            <tr style="height: 2px">
                                                            </tr>
                                                            <tr id="rowstk" visible="false" runat="server">

                                                                <td class="ControlLabel" style="width: 25%;">Current Stock
                                                                </td>
                                                                <td class="ControlDrpBorder" style="width: 25%">
                                                                    <asp:TextBox ID="txtCurrentStock" runat="server" BackColor="#e7e7e7" SkinID="skinTxtBoxGrid"
                                                                        Width="100px"
                                                                        Enabled="False"></asp:TextBox>

                                                                </td>
                                                                <td class="ControlLabel" style="width: 15%">To be adjusted *
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtadjusted"
                                                                        Display="Dynamic" ErrorMessage="Please enter Stock to be adjusted. It cannot be let blank.">*</asp:RequiredFieldValidator>
                                                                    <%--<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom, Numbers"
                                                                        TargetControlID="txtadjusted" ValidChars="+" />--%>
                                                                </td>
                                                                <td style="width: 25%" class="ControlTextBox3">
                                                                    <asp:TextBox ID="txtadjusted" runat="server" BackColor="#e7e7e7" SkinID="skinTxtBoxGrid"
                                                                        Width="100px"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 10%"></td>
                                                            </tr>
                                                            <tr style="height: 2px">
                                                            </tr>
                                                            <tr>

                                                                <td class="ControlLabel" style="width: 25%;">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDueDate"
                                                                        Display="Dynamic" ErrorMessage="please select OpeningStock Due Date. It cannot be let blank.">*</asp:RequiredFieldValidator>
                                                                    Opening Due Date *
                                                                </td>
                                                                <td class="ControlTextBox3" style="width: 25%">
                                                                    <asp:TextBox ID="txtDueDate" Enabled="false" runat="server" BackColor="#e7e7e7" SkinID="skinTxtBoxGrid"
                                                                        Width="100px"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="calExtender3" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                        PopupButtonID="btnDate3" PopupPosition="BottomLeft" TargetControlID="txtDueDate">
                                                                    </cc1:CalendarExtender>
                                                                </td>
                                                                <td style="width: 15%" align="left">
                                                                    <asp:ImageButton ID="btnDate3" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                        Width="20px" runat="server" />
                                                                </td>
                                                                <td style="width: 25%"></td>
                                                                <td style="width: 10%"></td>
                                                            </tr>

                                                            <tr style="height: 8px">
                                                            </tr>
                                                            <tr>
                                                                <td align="center" style="width: 100%" colspan="5">
                                                                    <table style="width: 100%">
                                                                        <tr>
                                                                            <td style="width: 30%"></td>
                                                                            <td align="center" style="width: 18%">
                                                                                <asp:Button ID="SaveButton" runat="server" CommandName="Update" SkinID="skinBtnSave" AccessKey="s"
                                                                                    CssClass="savebutton1231" EnableTheming="false" OnClick="SaveButton_Click"></asp:Button>
                                                                                <asp:Button ID="UpdateButton" runat="server" CommandName="Update" SkinID="skinBtnSave" AccessKey="u"
                                                                                    CssClass="Updatebutton1231" EnableTheming="false" OnClick="UpdateButton_Click"></asp:Button>
                                                                            </td>
                                                                            <td align="center" style="width: 18%">

                                                                                <asp:Button ID="CancelButton" runat="server" CausesValidation="False" CommandName="Cancel" AccessKey="c"
                                                                                    CssClass="cancelbutton6" EnableTheming="false" SkinID="skinBtnCancel" OnClick="UpdateCancelButton_Click"></asp:Button>
                                                                            </td>
                                                                            <td style="width: 30%"></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                    </table>
                                                </div>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                    </td>
                </tr>
                <tr style="width: 100%">
                    <td style="width: 100%;">
                        <table width="100%" style="margin: -6px 0px 0px 0px;">
                            <tr style="width: 100%">
                                <td>
                                    <asp:HiddenField ID="hdVisitID" runat="server" Value="0" />
                                    <div class="mainGridHold" id="searchGrid">
                                        <asp:GridView ID="GrdViewSerVisit" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                            OnRowCreated="GrdViewSerVisit_RowCreated" Width="100.4%" DataSourceID="GridSource" CssClass="someClass"
                                            AllowPaging="True" DataKeyNames="ItemCode" EmptyDataText="No Opening Stock found!"
                                            OnRowCommand="GrdViewSerVisit_RowCommand" OnRowDataBound="GrdViewSerVisit_RowDataBound"
                                            OnSelectedIndexChanged="GrdViewSerVisit_SelectedIndexChanged" OnRowDeleting="GrdViewSerVisit_RowDeleting"
                                            OnRowDeleted="GrdViewSerVisit_RowDeleted">
                                            <EmptyDataRowStyle CssClass="GrdContent" />
                                            <HeaderStyle Height="30px" HorizontalAlign="Center" Font-Bold="true" BackColor="#cccccc" BorderColor="Gray" Font-Size="Small" />
                                            <RowStyle Font-Bold="true" HorizontalAlign="Center" Height="30px" Font-Size="Small" ForeColor="#0567AE" />
                                            <Columns>
                                                <asp:BoundField DataField="ItemCode" HeaderText="Product Code" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="ProductName" HeaderText="Product Name" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="Model" HeaderText="Model" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="CategoryName" HeaderText="Category" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="ProductDesc" HeaderText="Brand" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="OpeningStock" HeaderText="Opening Stock" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="Stock" HeaderText="Current Stock" HeaderStyle-BorderColor="Gray" />
                                                <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="50px" HeaderText="Edit" HeaderStyle-BorderColor="Gray">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" SkinID="edit"
                                                            CommandName="Select" />
                                                        <asp:ImageButton ID="btnEditDisabled" Enabled="false" SkinID="editDisable" runat="Server"></asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="command" HeaderText="Delete" HeaderStyle-Width="50px" HeaderStyle-BorderColor="Gray"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to Delete this Opening Stock Details?"
                                                            runat="server">
                                                        </cc1:ConfirmButtonExtender>
                                                        <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" CommandName="Delete"></asp:ImageButton>
                                                        <asp:ImageButton ID="lnkBDisabled" Enabled="false" SkinID="deleteDisable" runat="Server"></asp:ImageButton>
                                                        <asp:HiddenField ID="ldgID" runat="server" Value='<%# Bind("ItemCode") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataRowStyle CssClass="GrdContent" />
                                            <PagerTemplate>
                                                <table style="border-color: white">
                                                    <tr style="border-color: white">
                                                        <td style="border-color: white">Goto Page
                                                        </td>
                                                        <td style="border-color: white">
                                                            <asp:DropDownList ID="ddlPageSelector" runat="server" Width="65px" BackColor="#e7e7e7" Style="border: 1px solid blue">
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
                <tr style="width: 100%">
                    <td style="width: 918px" align="left">
                        <asp:ObjectDataSource ID="GridSource" runat="server" SelectMethod="ListOpeningProductStock"
                            TypeName="BusinessLogic" DeleteMethod="DeleteOpeningStock" OnDeleting="GridSource_Deleting">
                            <DeleteParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                <asp:Parameter Name="ItemCode" Type="String" />
                                <asp:Parameter Name="Username" Type="String" />
                            </DeleteParameters>
                        </asp:ObjectDataSource>
                        <asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="true" ShowSummary="false" />
                        <asp:HiddenField ID="hdDataSource" runat="server" />
                        <asp:HiddenField ID="hdServiceID" runat="server" Value="" />
                        <asp:HiddenField ID="hdCustomerID" runat="server" Value="0" />
                        <asp:HiddenField ID="hdDueDate" runat="server" Value="" />
                        <asp:HiddenField ID="hdRefNumber" runat="server" Value="" />
                        <asp:HiddenField ID="hdSMSRequired" runat="server" Value="NO" />
                        <asp:HiddenField ID="hdEmailRequired" runat="server" Value="NO" />
                    </td>
                </tr>
                <tr align="center">
                    <table style="width: 100%">
                        <tr>
                             <td style="width: 20%">
                                 </td>
                            <td style="width: 15%">
                                   <asp:Panel ID="pnlSearch" runat="server" Width="100px">
                                                <asp:Button ID="lnkBtnAdd" runat="server" OnClick="lnkBtnAdd_Click" CssClass="ButtonAdd66"
                                                    EnableTheming="false" Width="80px" Text=""></asp:Button>
                                            </asp:Panel>
                            </td>
                            <td style="width: 15%">
                                <asp:Button ID="Button2" runat="server" CssClass="bulkaddition" OnClientClick="window.open('OpeningBulkAddition.aspx','billSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=310,width=500,left=425,top=220, scrollbars=yes');"
                                    EnableTheming="false"></asp:Button>
                            </td>
                            <td style="width: 15%">
                                <asp:Button ID="btnExportToExcel" runat="server" CssClass="exportexl6" OnClientClick="window.open('ReportExcelOpeningStock.aspx','billSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=310,width=500,left=425,top=220, scrollbars=yes');"
                                    EnableTheming="false"></asp:Button>
                            </td>
                            <td style="width: 35%"></td>
                        </tr>
                    </table>

                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
