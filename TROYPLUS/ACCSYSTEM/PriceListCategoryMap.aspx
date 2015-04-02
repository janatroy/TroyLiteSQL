<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="PriceListCategoryMap.aspx.cs" Inherits="PriceListCategoryMap" Title="Sales > Mapping Price List" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <script language="javascript" type="text/javascript">


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
                                            <span>Brand Master</span>
                                        </td>
                                    </tr>
                                </table>
                            </div>--%>
                        <%--<table class="mainConHd" style="width: 994px;">
                                <tr valign="middle">
                                    <td style="font-size: 20px;">
                                        Category Master
                                    </td>
                                </tr>
                            </table>--%>
                        <div class="mainConBody">
                            <table style="width: 99.8%; margin: -2px 0px 0px 1px;" cellpadding="3" cellspacing="2" class="searchbg">
                                <tr style="height: 25px; vertical-align: middle">
                                    <td style="width: 1%;"></td>
                                    <td style="width: 35%; font-size: 22px; color: White;">Customer Category
                                    </td>
                                 
                                    <td style="width: 10%; color: White;" align="right">Search
                                    </td>
                                    <td style="width: 20%" class="NewBox">
                                        <asp:TextBox ID="txtSearch" runat="server" SkinID="skinTxtBoxSearch"></asp:TextBox>
                                    </td>
                                    <td style="width: 20%" class="NewBox">
                                        <div style="width: 160px; font-family: 'Trebuchet MS';">
                                            <asp:DropDownList ID="ddCriteria" runat="server" Width="153px" Height="23px" BackColor="White" Style="text-align: center; border: 1px solid White">
                                                <asp:ListItem Value="All">All</asp:ListItem>
                                                <asp:ListItem Value="PriceListName">PriceList Name</asp:ListItem>
                                                <asp:ListItem Value="CusCategoryName">Customer Category</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                    <td style="width: 16%" class="tblLeftNoPad">
                                        <asp:Button ID="btnSearch" onkeyup="EnableDisableButton(this,'BtnClearFilter')" runat="server" Text="" CssClass="ButtonSearch6" EnableTheming="false" />
                                    </td>
                                    <td style="width: 2%" class="tblLeftNoPad">
                                        <asp:Button ID="BtnClearFilter" runat="server"  Visible="true" OnClick="BtnClearFilter_Click" EnableTheming="false" Text="" CssClass="ClearFilter6" />
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
                                                OnItemUpdating="frmViewAdd_ItemUpdating" EmptyDataText="No Records Found" OnItemInserted="frmViewAdd_ItemInserted"
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
                                                                <td colspan="4" class="headerPopUp">Customer Category
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 5px">
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width: 40%">Customer Category *
                                                                    <%--<asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="drpCustomerCategory"
                                                                        Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="Customer Category is mandatory"
                                                                        Operator="GreaterThan" ValueToCompare="0" />--%>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="drpCustomerCategory"
                                                                        Display="Dynamic" ErrorMessage="Please enter Customer Category. It Cannot be left blank ">*</asp:RequiredFieldValidator>
                                                                      <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="LowercaseLetters, UppercaseLetters,Numbers,Custom" ValidChars=" " TargetControlID="drpCustomerCategory" />
                                                                </td>
                                                                <td class="ControlDrpBorder" style="width: 30%">
                                                                    <%--<asp:DropDownList ID="drpCustomerCategory" runat="server" OnDataBound="drpCustomerCategory_DataBound" AppendDataBoundItems="true" BackColor="#e7e7e7" CssClass="drpDownListMedium" DataTextField="CusCategory_Name" DataValueField="CusCategory_Value" DataSourceID="srccuscat" height="26px" style="border: 1px solid #e7e7e7" TabIndex="2" ValidationGroup="salesval" Width="100%">
                                                                        <asp:ListItem style="background-color: #e7e7e7" Text="Select Customer Category" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>--%>
                                                                    <asp:TextBox ID="drpCustomerCategory" runat="server" Text='<%# Bind("CustomerCategory_Name") %>'
                                                                        SkinID="skinTxtBoxGrid" TabIndex="1"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 30%">
                                                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("CusCategory_ID") %>'
                                                                        Visible="false"></asp:TextBox>
                                                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("CustomerCategory_Name") %>'
                                                                        Visible="false"></asp:TextBox>
                                                                </td>
                                                                <td></td>

                                                            </tr>
                                                            <tr style="height: 3px">
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width: 40%">Price List Name *
                                                                    <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="drpPriceList"
                                                                        Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="Please Select Price List. It cannot be left blank "
                                                                        Operator="GreaterThan" ValueToCompare="0" />
                                                                </td>
                                                                <td class="ControlDrpBorder" style="width: 30%">
                                                                    <asp:DropDownList ID="drpPriceList" runat="server" AppendDataBoundItems="true" BackColor="#e7e7e7" SelectedValue='<%# Bind("PriceList_Id") %>' CssClass="drpDownListMedium" DataTextField="PriceName" DataValueField="Id" Height="26px" DataSourceID="srclistpricelist" Style="border: 1px solid #e7e7e7" TabIndex="2" ValidationGroup="salesval" Width="100%">
                                                                        <asp:ListItem style="background-color: #e7e7e7" Text="Select Price List" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>

                                                                <td style="width: 30%"></td>
                                                                <td></td>
                                                            </tr>

                                                            <tr style="height: 10px">
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4">
                                                                    <table style="width: 100%">
                                                                        <tr>
                                                                            <td style="width: 20%">
                                                                                <asp:ObjectDataSource ID="srclistpricelist" runat="server" SelectMethod="ListPriceList"
                                                                                    TypeName="BusinessLogic">
                                                                                    <SelectParameters>
                                                                                        <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                                    </SelectParameters>
                                                                                </asp:ObjectDataSource>
                                                                                <asp:ObjectDataSource ID="srccuscat" runat="server" SelectMethod="ListCusCategory"
                                                                                    TypeName="BusinessLogic">
                                                                                    <SelectParameters>
                                                                                        <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                                    </SelectParameters>
                                                                                </asp:ObjectDataSource>
                                                                            </td>
                                                                            <td align="center" style="width: 30%">
                                                                                <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update"
                                                                                    CssClass="Updatebutton1231" EnableTheming="false" SkinID="skinBtnSave"
                                                                                    OnClick="UpdateButton_Click"></asp:Button>
                                                                            </td>
                                                                            <td align="center" style="width: 30%">
                                                                                <asp:Button ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                    CssClass="cancelbutton6" EnableTheming="false" SkinID="skinBtnCancel" OnClick="UpdateCancelButton_Click"></asp:Button>
                                                                            </td>
                                                                            <td style="width: 20%"></td>
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
                                                                <td>
                                                                    <asp:ValidationSummary ID="valSum" DisplayMode="BulletList" ShowMessageBox="true"
                                                                        ShowSummary="false" HeaderText=" " Font-Names="'Trebuchet MS'"
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
                                                                <td colspan="4" class="headerPopUp">Customer Category
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 5px">
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width: 40%">Customer Category *
                                                                    <%--<asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="drpCustomerCategoryAdd"
                                                                        Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="Customer Category is mandatory"
                                                                        Operator="GreaterThan" ValueToCompare="0" />--%>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="drpCustomerCategoryAdd"
                                                                        Display="Dynamic" ErrorMessage="Please enter Customer Category. It cannot be left blank">*</asp:RequiredFieldValidator>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="LowercaseLetters, UppercaseLetters,Numbers,Custom" ValidChars=" " TargetControlID="drpCustomerCategoryAdd" />
                                                                </td>
                                                                <td class="ControlDrpBorder" style="width: 30%">
                                                                    <%--<asp:DropDownList ID="drpCustomerCategoryAdd" runat="server" AppendDataBoundItems="true" OnDataBound="drpCustomerCategoryAdd_DataBound" BackColor="#e7e7e7" CssClass="drpDownListMedium" DataTextField="CusCategory_Name" DataValueField="CusCategory_Value" height="26px" DataSourceID="srccuscatadd" style="border: 1px solid #e7e7e7" TabIndex="2" ValidationGroup="salesval" Width="100%">
                                                                        <asp:ListItem style="background-color: #e7e7e7" Text="Select Customer Category" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>--%>
                                                                    <asp:TextBox ID="drpCustomerCategoryAdd" runat="server"
                                                                        SkinID="skinTxtBoxGrid" TabIndex="1"></asp:TextBox>
                                                                </td>

                                                                <td style="width: 30%"></td>
                                                                <td></td>
                                                            </tr>

                                                            <tr style="height: 3px">
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width: 40%">Price List Name *
                                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="drpPriceListAdd"
                                                                        Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="Please Select Price List. It cannot be left blank"
                                                                        Operator="GreaterThan" ValueToCompare="0" />
                                                                </td>
                                                                <td class="ControlDrpBorder" style="width: 30%">
                                                                    <asp:DropDownList ID="drpPriceListAdd" runat="server" AppendDataBoundItems="true" OnDataBound="drpPriceListAdd_DataBound" BackColor="#e7e7e7" CssClass="drpDownListMedium" DataTextField="PriceName" DataValueField="Id" Height="26px" DataSourceID="srclistpricelistadd" Style="border: 1px solid #e7e7e7" TabIndex="2" ValidationGroup="salesval" Width="100%">
                                                                        <asp:ListItem style="background-color: #e7e7e7" Text="Select Price List" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>

                                                                <td style="width: 30%"></td>
                                                                <td></td>
                                                            </tr>
                                                            <tr style="height: 10px">
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4">
                                                                    <table style="width: 100%">
                                                                        <tr>
                                                                            <td style="width: 20%"></td>
                                                                            <td align="center" style="width: 30%">
                                                                                <asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                                                                    CssClass="savebutton1231" EnableTheming="false" SkinID="skinBtnSave"
                                                                                    OnClick="InsertButton_Click"></asp:Button>
                                                                            </td>
                                                                            <td align="center" style="width: 30%">
                                                                                <asp:Button ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                    CssClass="cancelbutton6" EnableTheming="false" SkinID="skinBtnCancel" OnClick="InsertCancelButton_Click"></asp:Button>
                                                                            </td>
                                                                            <td style="width: 20%"></td>
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
                                                                    ShowSummary="false" HeaderText=" " Font-Names="'Trebuchet MS'"
                                                                    Font-Size="12" runat="server" />
                                                                <asp:ObjectDataSource ID="srclistpricelistadd" runat="server" SelectMethod="ListPriceList"
                                                                    TypeName="BusinessLogic">
                                                                    <SelectParameters>
                                                                        <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                    </SelectParameters>
                                                                </asp:ObjectDataSource>

                                                            </td>
                                                            <td colspan="2">
                                                                <asp:ObjectDataSource ID="srcGroupInfoAdd" runat="server" SelectMethod="ListGroupInfoExp"
                                                                    TypeName="BusinessLogic" OldValuesParameterFormatString="original_{0}">
                                                                    <SelectParameters>
                                                                        <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                    </SelectParameters>
                                                                </asp:ObjectDataSource>
                                                                <asp:ObjectDataSource ID="srccuscatadd" runat="server" SelectMethod="ListCusCategory"
                                                                    TypeName="BusinessLogic">
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
                        <table width="100%" style="margin: -5px 0px 0px 0px;">
                            <tr style="width: 100%">
                                <td>
                                    <div class="mainGridHold" id="searchGrid">
                                        <asp:GridView ID="GrdViewLedger" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                            OnRowCreated="GrdViewLedger_RowCreated" Width="100.4%" DataSourceID="GridSource" CssClass="someClass"
                                            AllowPaging="True" DataKeyNames="Id" EmptyDataText="No Mapping Found."
                                            OnRowCommand="GrdViewLedger_RowCommand" OnRowDataBound="GrdViewLedger_RowDataBound" OnRowDeleting="GrdViewLedger_RowDeleting"
                                            OnRowDeleted="GrdViewLedger_RowDeleted">
                                            <HeaderStyle Height="30px" HorizontalAlign="Center" Font-Bold="true" BackColor="#cccccc" BorderColor="Gray" Font-Size="Small" />
                                            <RowStyle Font-Bold="true" HorizontalAlign="Center" Height="30px" Font-Size="Small" ForeColor="#0567AE" />
                                            <Columns>
                                                <asp:BoundField DataField="CustomerCategory_Name" HeaderText="Customer Category Name" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="200px" />
                                                <asp:BoundField DataField="PriceList_Name" HeaderText="Price List Name" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="200px" />
                                                <asp:BoundField DataField="CusCategory_ID" HeaderText="CusCategory_ID" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="200px" Visible="false" />
                                                <asp:TemplateField ItemStyle-CssClass="command" HeaderText="Edit" ItemStyle-Width="50px" HeaderStyle-BorderColor="Gray"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Select" />
                                                        <asp:ImageButton ID="btnEditDisabled" Enabled="false" SkinID="editDisable" runat="Server"></asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="command" HeaderText="Delete" HeaderStyle-Width="50px" HeaderStyle-BorderColor="Gray"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to Delete this Mapping?"
                                                            runat="server">
                                                        </cc1:ConfirmButtonExtender>
                                                        <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" CommandName="Delete"></asp:ImageButton>
                                                        <asp:ImageButton ID="lnkBDisabled" Enabled="false" SkinID="deleteDisable" runat="Server"></asp:ImageButton>
                                                        <asp:HiddenField ID="ldgID" runat="server" Value='<%# Bind("Id") %>' />
                                                        <asp:HiddenField ID="HiddenField2" runat="server" Value='<%# Bind("CusCategory_Name") %>' />
                                                        <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Bind("Id") %>' />
                                                        <asp:HiddenField ID="ldgIDDD" runat="server" Value='<%# Bind("CusCategory_ID") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerTemplate>
                                                <table style="border-color: white">
                                                    <tr style="border-color: white">
                                                        <td style="border-color: white">Goto Page
                                                        </td>
                                                        <td style="border-color: white">
                                                            <asp:DropDownList ID="ddlPageSelector" runat="server" AutoPostBack="true" Width="65px" Style="border: 1px solid blue" BackColor="#e7e7e7">
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
                <tr style="width: 100%;">
                    <td style="width: 918px" align="left">
                        <asp:ObjectDataSource ID="GridSource" runat="server" SelectMethod="ListMappingInfo"
                            TypeName="BusinessLogic" DeleteMethod="DeleteMapping" OnDeleting="GridSource_Deleting">
                            <DeleteParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                <asp:Parameter Name="ID" Type="Int32" />
                                <asp:Parameter Name="Username" Type="String" />
                                <asp:Parameter Name="CusCategory_Name" Type="String" />
                            </DeleteParameters>
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="empSrc" runat="server" SelectMethod="ListExecutive" TypeName="BusinessLogic">
                            <SelectParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="empSrcAdd" runat="server" SelectMethod="ListExecutive"
                            TypeName="BusinessLogic">
                            <SelectParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="frmSource" runat="server" SelectMethod="GetMappingInfoForId"
                            TypeName="BusinessLogic" OnUpdating="frmSource_Updating" OnInserting="frmSource_Inserting"
                            InsertMethod="InsertMappingInfo" UpdateMethod="UpdateMappingInfo">
                            <UpdateParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                <asp:Parameter Name="ID" Type="Int32" />
                                <asp:Parameter Name="PriceList_Id" Type="Int32" />
                                <asp:Parameter Name="PriceList_Name" Type="String" />
                                <asp:Parameter Name="CustomerCategory_Value" Type="String" />
                                <asp:Parameter Name="CustomerCategory_Name" Type="String" />
                                <asp:Parameter Name="Username" Type="String" />
                                <asp:Parameter Name="CusCategory_ID" Type="Int32" />
                            </UpdateParameters>
                            <SelectParameters>
                                <asp:ControlParameter ControlID="GrdViewLedger" Name="ID" PropertyName="SelectedValue"
                                    Type="String" />
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                            </SelectParameters>
                            <InsertParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                <asp:Parameter Name="PriceList_Id" Type="Int32" />
                                <asp:Parameter Name="PriceList_Name" Type="String" />
                                <asp:Parameter Name="CustomerCategory_Value" Type="String" />
                                <asp:Parameter Name="CustomerCategory_Name" Type="String" />
                                <asp:Parameter Name="Username" Type="String" />
                            </InsertParameters>
                        </asp:ObjectDataSource>
                    </td>
                </tr>
                <tr style="width: 100%;" align="center">
                     <td>
                          <table align="center" style="width: 100%">
                            <tr>
                                <td style="width: 35%"></td>
                                   <td style="width: 30%" align="center" >
                                      
                                            <asp:Panel ID="pnlSearch" runat="server" Width="100px">
                                                <asp:Button ID="lnkBtnAdd" runat="server" OnClick="lnkBtnAdd_Click" CssClass="ButtonAdd66"
                                                    EnableTheming="false" Width="80px" Text=""></asp:Button>
                                            </asp:Panel>
                                        
                                    </td>
                                <td style="width: 35%"></td>
                                </tr>
                                </table>

                     </td>

                </tr>
                <tr runat="server" visible="false" id="rowid">
                    <td>
                        <table align="center" style="width: 100%">
                            <tr>
                                <td style="width: 35%"></td>
                                <td style="width: 15%">
                                    <asp:Button ID="BlkAdd" runat="server" OnClientClick="window.open('BulkAdditionCategory.aspx','billSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=250,width=500,left=425,top=220, scrollbars=yes');" CssClass="bulkaddition"
                                        EnableTheming="false" Text=""></asp:Button>
                                </td>
                                <td style="width: 15%">
                                    <asp:Button ID="btnExportToExcel" runat="server" CssClass="exportexl6" OnClientClick="window.open('ReportExcelCategory.aspx ','billSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=310,width=500,left=425,top=220, scrollbars=yes');"
                                        EnableTheming="false"></asp:Button>
                                </td>
                                <td style="width: 35%"></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
