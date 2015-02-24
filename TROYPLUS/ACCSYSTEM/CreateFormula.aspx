<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="CreateFormula.aspx.cs" Inherits="CreateFormula" Title="Manufacture > Define Product speification" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <style id="Style1" runat="server">
        
        
        .fancy-green .ajax__tab_header
        {
	        background: url(App_Themes/NewTheme/Images/green_bg_Tab.gif) repeat-x;
	        cursor:pointer;
        }
        .fancy-green .ajax__tab_hover .ajax__tab_outer, .fancy-green .ajax__tab_active .ajax__tab_outer
        {
	        background: url(App_Themes/NewTheme/Images/green_left_Tab.gif) no-repeat left top;
        }
        .fancy-green .ajax__tab_hover .ajax__tab_inner, .fancy-green .ajax__tab_active .ajax__tab_inner
        {
	        background: url(App_Themes/NewTheme/Images/green_right_Tab.gif) no-repeat right top;
        }
        .fancy .ajax__tab_header
        {
	        font-size: 13px;
	        font-weight: bold;
	        color: #000;
	        font-family: sans-serif;
        }
        .fancy .ajax__tab_active .ajax__tab_outer, .fancy .ajax__tab_header .ajax__tab_outer, .fancy .ajax__tab_hover .ajax__tab_outer
        {
	        height: 46px;
        }
        .fancy .ajax__tab_active .ajax__tab_inner, .fancy .ajax__tab_header .ajax__tab_inner, .fancy .ajax__tab_hover .ajax__tab_inner
        {
	        height: 46px;
	        margin-left: 16px; /* offset the width of the left image */
        }
        .fancy .ajax__tab_active .ajax__tab_tab, .fancy .ajax__tab_hover .ajax__tab_tab, .fancy .ajax__tab_header .ajax__tab_tab
        {
	        margin: 16px 16px 0px 0px;
        }
        .fancy .ajax__tab_hover .ajax__tab_tab, .fancy .ajax__tab_active .ajax__tab_tab
        {
	        color: #fff;
        }
        .fancy .ajax__tab_body
        {
	        font-family: Arial;
	        font-size: 10pt;
	        border-top: 0;
	        border:1px solid #999999;
	        padding: 8px;
	        background-color: #ffffff;
        }
        
    </style>
    <asp:UpdatePanel ID="UpdatePanel16" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            
            <table style="width: 100%">
                <tr style="width: 100%">
                    <td style="width: 100%">
                    
                            <%--<div class="mainConHd">
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tr valign="middle">
                                        <td>
                                            <span>Inventory Manufacturing / Transfer Definitions</span>
                                        </td>
                                    </tr>
                                </table>
                            </div>--%>
                            <%--<table class="mainConHd" style="width: 994px;">
                                <tr valign="middle">
                                    <td style="font-size: 20px;">
                                         Inventory Manufacturing / Transfer Definitions
                                    </td>
                                </tr>
                            </table>--%>
                            <div class="mainConBody">
                                <table style="width: 99.8%;margin: -1px 0px 0px 1px;" cellpadding="3" cellspacing="2" class="searchbg">
                                    <tr style=" vertical-align: middle">
                                       
                                        <td style="width: 52%; font-size: 22px; color: white;" >
                                           Specification of Products
                                        </td>
                                        <td style="width: 13%">
                                            <div style="text-align: right;">
                                              
                                            </div>
                                        </td>
                                        <td style="width: 7%; color: white;" align="right">
                                            Search
                                        </td>
                                        <td style="width: 18%" class="NewBox">
                                            <asp:TextBox ID="txtSearch" runat="server" SkinID="skinTxtBoxSearch"></asp:TextBox>
                                        </td>
                                        <td style="width: 18%" class="tblLeft">
                                            <asp:Button ValidationGroup="search" ID="btnSearch" OnClick="btnSearch_Click" runat="server"
                                                Text="" EnableTheming="false" CssClass="ButtonSearch6" />
                                        </td>
                                     <%--    <td style="width: 16%" class="tblLeftNoPad">
                                        <asp:Button ID="BtnClearFilter" runat="server" OnClick="BtnClearFilter_Click" EnableTheming="false" Text="" CssClass="ClearFilter6" />
                                    </td>--%>
                                        <%--<td style="width: 25%" class="tblLeftNoPad">--%>
                                            <%--<asp:RequiredFieldValidator ValidationGroup="search" ID="rqSearchBill" runat="server"
                                                Text="Search Box is Empty" ControlToValidate="txtSearch"></asp:RequiredFieldValidator>--%>
                                        <%--</td>--%>
                                    </tr>
                                </table>
                            </div>
                        
                        <div style="width: 100%;   text-align: left">
                            <asp:HiddenField ID="hdMode" runat="server" Value="New" />
                            <input id="dummy" type="button" style="display: none" runat="server" />
                            <input id="Button2" type="button" style="display: none" runat="server" />
                            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                                CancelControlID="Button2" DynamicServicePath="" Enabled="True" PopupControlID="popUp"
                                TargetControlID="dummy">
                            </cc1:ModalPopupExtender>
                            <asp:Panel runat="server" ID="popUp" Style="width: 85%; background-color: White;
                                display: none">
                                <asp:UpdatePanel ID="UpdatePanelFormula" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div>
                                            <div id="contentPopUp">
                                                <table class="tblLeft"  style="border: 1px solid #5078B3; height:250px; " width="100%">
                                                    <tr>
                                                        <td colspan="4">
                                                            <table class="headerPopUp"   width="900px">
                                                                <tr>
                                                                    <td>
                                                                  <asp:Label ID="heading" runat="server">
                                                                      </asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <table class="tblLeft" style="border: 0px solid #5078B3; background-color: #fff;
                                                                color: #000;">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Panel ID="prodPanel" runat="server" Visible="False">
                                                                            <table class="tblLeft" width="1100px" style="border: 0px solid #5078B3; background-color: #fff;
                                                                                color: #000;">
                                                                                <tr style="height:5px">
                                                                                                    </tr>
                                                                                <tr style="height:5px">
                                                                                                    </tr>
                                                                                <tr style="height:5px">
                                                                                                    </tr>
                                                                                <tr>
                                                                                    <td class="ControlLabelproject1" style="width: 28%">
                                                                                        Name of Product to be Manufactured
                                                                                    </td>
                                                                                    <td style="width: 2%">
                                                                                        
                                                                                    </td>
                                                                                    <td style="width: 20%" class="ControlTextBox3">
                                                                                        <asp:TextBox ValidationGroup="salesval" Style="text-transform: uppercase" ID="txtFormulaName"
                                                                                            runat="server" CssClass="cssTextBox" Width="95%" MaxLength="150"></asp:TextBox>
                                                                                    </td>
                                                                                    <td style="width: 5%">
                                                                                        
                                                                                    </td>
                                                                                    <td style="width: 25%; text-align: left">
                                                                                        <asp:Button ID="Button1" Style="width: 200px; height:23px;" ValidationGroup="salesval" runat="server" Text="Define Specification"  Font-Bold="true" Font-Size="15px" BackColor="Wheat"  EnableTheming="false" 
                                                                                            SkinID="skinBtnAddProduct" OnClick="Button1_Click" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr style="height:5px">
                                                                                                    </tr>
                                                                                <tr style="height:5px">
                                                                                                    </tr>
                                                                                <tr style="height:5px">
                                                                                                    </tr>
                                                                               
                                                                            </table>
                                                                        </asp:Panel>
                                                                        <asp:Panel ID="salesPanel" runat="server" Visible="False">
                                                                            <table  width="100%" cellpadding="4"   style=" font-size:12px; font-weight:bold; border: 1px solid #5078B3" ;>
                                                                                <tr class="HeadataRow">
                                                                                    <td width="8%" style="font-size:14px; background:#cccccc; font-weight:bold;">
                                                                                        Component ID
                                                                                    </td>
                                                                                    <td width="25%" style="font-size:14px; background:#cccccc; font-weight:bold;">
                                                                                        Component Name
                                                                                    </td>
                                                                                    <td width="30%" style="font-size:14px; background:#cccccc; font-weight:bold;">
                                                                                        Description
                                                                                    </td>
                                                                                    <td width="5%" style="font-size:14px; background:#cccccc; font-weight:bold;">
                                                                                        Qty.
                                                                                    </td>
                                                                                    <td width="5%" style="font-size:14px; background:#cccccc; font-weight:bold;">
                                                                                      Types of Component
                                                                                    </td>
                                                                                      <td width="7%" style="font-size:14px; background:#cccccc; font-weight:bold;">
                                                                                        Unit
                                                                                    </td>

                                                                                </tr>
                                                                                <tr valign="middle">
                                                                                    <td class="ControlDrpBorder">
                                                                                        <div style="font-family: 'Trebuchet MS';">
                                                                                            <asp:DropDownList ID="cmbProdAdd" Width="100%" AppendDataBoundItems="True" CssClass="drpDownListMedium"
                                                                                                runat="server" AutoPostBack="True" DataTextField="ProductName" DataValueField="ItemCode" BackColor = "#cccccc" height="25px"
                                                                                                OnSelectedIndexChanged="cmbProdAdd_SelectedIndexChanged">
                                                                                                <asp:ListItem style="background-color: #cccccc" Text="Select Component" Value="0"></asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </div>
                                                                                    </td>
                                                                                    <td valign="top" class="ControlTextBox3">
                                                                                        <asp:TextBox ID="lblProdNameAdd" runat="server" CssClass="cssTextBoxmanu" Width="98%"
                                                                                            ReadOnly="true"></asp:TextBox>
                                                                                    </td>
                                                                                    <td valign="top" class="ControlTextBox3">
                                                                                        <asp:TextBox ID="lblProdDescAdd" runat="server" CssClass="cssTextBoxmanu" Width="98%"
                                                                                            ReadOnly="true"></asp:TextBox>
                                                                                    </td>
                                                                                    <td  class="ControlTextBox3">
                                                                                         <cc1:FilteredTextBoxExtender ID="OBvalid" runat="server" FilterType="Numbers" TargetControlID="txtQtyAdd" />
                                                                                        <asp:TextBox ID="txtQtyAdd" runat="server" Width="98%" CssClass="cssTextBoxmanu" ValidationGroup="product"></asp:TextBox>
                                                                                    </td>
                                                                                    <td valign="top" class="ControlTextBox30">
                                                                                       
                                                                                            <asp:DropDownList ID="ddType"  runat="server" CssClass="drpDownListMedium"  BackColor = "#cccccc" Width="98%" height="25px" style="border:1px solid black">
                                                                                                <asp:ListItem Text="Raw Material" Value="IN"></asp:ListItem>
                                                                                                <asp:ListItem Text="Product" Value="OUT"></asp:ListItem>
                                                                                                  <asp:ListItem Text="By-Product" Value="OUT"></asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                       
                                                                                    </td>
                                                                                    <td valign="top" class="ControlTextBox30">
                                                                                        <div style="border-width: 1px; border-color: #bce1fe; border-style: solid; width: 100%;
                                                                                            font-family: 'Trebuchet MS';">
                                                                                            <asp:DropDownList ID="ddUnit" runat="server"  DataTextField="Unit" DataValueField="Unit" DataSourceID="srcUnitMntAdd" CssClass="drpDownListMedium"  BackColor = "#cccccc" height="25px" style="border:1px solid black">
                                                                                               
                                                                                            </asp:DropDownList>
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:RequiredFieldValidator ID="rv1" ValidationGroup="product" ErrorMessage="Please Select Component ID.It Cannot be left Blank"
                                                                                            Text="*" InitialValue="0" ControlToValidate="cmbProdAdd" runat="server" />
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="ErrMsg" runat="server" CssClass="errorMsg"></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:CompareValidator ID="cvQty" runat="server" ControlToValidate="txtQtyAdd" ValueToCompare="0"
                                                                                            Type="Double" Operator="GreaterThan" Text="*" ErrorMessage="Qty. should be Greater 0"
                                                                                            Display="Dynamic" SetFocusOnError="True" />
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:RequiredFieldValidator ValidationGroup="product" ID="rqQty" runat="server" Display="Dynamic"
                                                                                            ControlToValidate="txtQtyAdd" Text="*" ErrorMessage="Please Enter Qty.It Cannot be left Blank"></asp:RequiredFieldValidator>
                                                                                    </td>
                                                                                    <td></td>
                                                                                    <td align="right">
                                                                                        <asp:HiddenField ID="hdStock" runat="server" />
                                                                                        <asp:Button ID="cmdSaveProduct" Style="width: 100px;" runat="server" Text="Enter" Font-Bold="true" Font-Size="12px" BackColor="Wheat"
                                                                                            EnableTheming="false" SkinID="skinBtnAddProduct" OnClick="cmdSaveProduct_Click"
                                                                                            ValidationGroup="product" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </asp:Panel>
                                                                        <asp:ValidationSummary ID="valSum" DisplayMode="BulletList" ShowMessageBox="true"
                                                                            ShowSummary="false" ValidationGroup="product" HeaderText=" "
                                                                            Font-Names="'Trebuchet MS'" Font-Size="12" runat="server" />
                                                                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="ListProducts"
                                                                            TypeName="BusinessLogic"></asp:ObjectDataSource>
                                                                             <asp:ObjectDataSource ID="srcUnitMntAdd" runat="server" SelectMethod="ListMeasurementUnits"
                                                                                                                TypeName="BusinessLogic" OldValuesParameterFormatString="original_{0}">
                                                                                                                <SelectParameters>
                                                                                                                    <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                                                                </SelectParameters>
                                                                                                            </asp:ObjectDataSource>
                                                                       
                                                                        <asp:Panel ID="PanelItems"  Height="50%" runat="server" Visible="False">
                                                                            <asp:GridView ID="GrdViewItems" AutoGenerateColumns="false" runat="server" AllowSorting="True" Width="100%"  DataKeyNames="FormulaID"
                                                                                AllowPaging="True" OnRowEditing="GrdViewItems_RowEditing" OnRowCancelingEdit="GrdViewItems_RowCancelingEdit"
                                                                                EmptyDataText="No Product Added. Please add products by Clicking Define Specification." 
                                                                                OnPageIndexChanging="GrdViewItems_PageIndexChanging" OnRowDataBound="GrdViewItems_RowDataBound"
                                                                                OnRowUpdating="GrdViewItems_RowUpdating" OnRowDeleting="GrdViewItems_RowDeleting"
                                                                                OnRowCreated="GrdViewItems_RowCreated" CssClass="someClass" >
                                                                                    <HeaderStyle Height="30px" HorizontalAlign="Center" Font-Bold="true" BackColor="#cccccc" BorderColor="Gray" Font-Size="14px"/>
                                                                                   <RowStyle Font-Bold="true" HorizontalAlign="Center" Height="30px" Font-Size="Small" ForeColor="#0567AE"/>
                                                                                <EmptyDataRowStyle Font-Bold="false" /> 
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="Component ID"  HeaderStyle-BackColor="#cccccc"  HeaderStyle-Font-Bold="true">
                                                                                        <ItemTemplate>
                                                                                            <%# Eval("itemcode")%>
                                                                                        </ItemTemplate>
                                                                                        <EditItemTemplate>
                                                                                            <asp:Label runat="server"  ID="lblCode" Text='<%# Eval("itemcode")%>'></asp:Label>
                                                                                        </EditItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Component Name" HeaderStyle-BorderColor="Gray">
                                                                                        <ItemTemplate>
                                                                                            <%# Eval("ProductName")%>
                                                                                        </ItemTemplate>
                                                                                        <EditItemTemplate>
                                                                                            <asp:Label runat="server" ID="lblProdname" Text='<%# Eval("ProductName")%>'></asp:Label>
                                                                                        </EditItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Description" HeaderStyle-BorderColor="Gray">
                                                                                        <ItemTemplate>
                                                                                            <%# Eval("ProductDesc")%>
                                                                                        </ItemTemplate>
                                                                                        <EditItemTemplate>
                                                                                            <asp:Label runat="server" ID="lblDesc" Text='<%# Eval("ProductDesc")%>'></asp:Label>
                                                                                        </EditItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Qty." HeaderStyle-BorderColor="Gray">
                                                                                        <ItemTemplate>
                                                                                            <%# Eval("Qty")%>
                                                                                        </ItemTemplate>
                                                                                        <EditItemTemplate>
                                                                                            <asp:TextBox ID="txtQty" runat="server" Text='<%# Eval("Qty") %>' CssClass="cssTextBox"
                                                                                                Width="60px"></asp:TextBox>
                                                                                            <asp:RequiredFieldValidator ValidationGroup="editVal" runat="server" ID="rvq" ControlToValidate="txtQty"
                                                                                                ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                                            <asp:CompareValidator ID="cvQty" runat="Server" ControlToValidate="txtQty" ValueToCompare="0"
                                                                                                Type="Double" Operator="GreaterThanEqual" ErrorMessage="Please enter a number greater than equal to 0"
                                                                                                Display="Dynamic" SetFocusOnError="True" />
                                                                                        </EditItemTemplate>
                                                                                        <ItemStyle Width="80px" />
                                                                                        <FooterTemplate>
                                                                                        </FooterTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Type of component" HeaderStyle-BorderColor="Gray">
                                                                                        <ItemTemplate>
                                                                                            <%# Eval("InOut")%>
                                                                                        </ItemTemplate>
                                                                                        <EditItemTemplate>
                                                                                            <asp:Label runat="server" ID="lblInOut" Text='<%# Eval("InOut")%>'></asp:Label>
                                                                                        </EditItemTemplate>
                                                                                        <FooterTemplate>
                                                                                        </FooterTemplate>
                                                                                    </asp:TemplateField>
                                                                                     <asp:TemplateField HeaderText="Unit" HeaderStyle-BorderColor="Gray">
                                                                                        <ItemTemplate>
                                                                                            <%# Eval("Unit_Of_Measure")%>
                                                                                        </ItemTemplate>
                                                                                        <EditItemTemplate>
                                                                                            <asp:Label runat="server" ID="lblunit" Text='<%# Eval("Unit_Of_Measure")%>'></asp:Label>
                                                                                        </EditItemTemplate>
                                                                                        <FooterTemplate>
                                                                                        </FooterTemplate>
                                                                                    </asp:TemplateField>
                                                                                    
                                                                                    <asp:TemplateField HeaderText="Edit" HeaderStyle-BorderColor="Gray">
                                                                                        <ItemStyle Width="7%" HorizontalAlign="Center" />
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CausesValidation="False"
                                                                                                CommandName="Edit" />
                                                                                        </ItemTemplate>
                                                                                        <EditItemTemplate>
                                                                                            <asp:ImageButton ID="lbUpdate" runat="server" CausesValidation="true" ValidationGroup="bills"
                                                                                                CommandName="Update" Text="Update" SkinID="GridUpdate"></asp:ImageButton>
                                                                                            <asp:ImageButton ID="lbCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                                Text="Cancel" SkinID="GridCancel"></asp:ImageButton>
                                                                                        </EditItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Delete" HeaderStyle-BorderColor="Gray">
                                                                                        <ItemTemplate>
                                                                                            <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure you want to delete this product?"
                                                                                                runat="server">
                                                                                            </cc1:ConfirmButtonExtender>
                                                                                            <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" CommandName="Delete"></asp:ImageButton>
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle Width="4%" />
                                                                                        <ItemStyle HorizontalAlign="Center" CssClass="command" />
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                                <PagerTemplate>
                                                                                    Goto Page
                                                                                    <asp:DropDownList ID="ddlPageSelector" runat="server" AutoPostBack="true" SkinID="skinPagerDdlBox">
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
                                                                        </asp:Panel>
                                                                         
                                                                        <div style="width: 100%; text-align: center">
                                                                            <asp:Panel ID="PanelCmd" runat="server" Visible="False">
                                                                                <table style="width: 100%">
                                                                                    <tr title="Componenet Id" style="height:5px">
                                                                                                    </tr>
                                                                                    <tr style="height:5px">
                                                                                                    </tr>
                                                                                    <tr style="height:5px">
                                                                                                    </tr>
                                                                                    <td style="width: 10%">      
                                                                                                        </td>
                                                                                 
                                                                                    <tr style="width: 100%">
                                                                                        <td style="width:40%">
                                                                                        </td>
                                                                                        <td style="width:10%" align="right">                                                                                           
                                                                                             <asp:Button ID="cmdSave" runat="server" ValidationGroup="salesval" CssClass="savebutton1231"
                                                                                                EnableTheming="false" OnClick="cmdSave_Click" SkinID="skinBtnSave" />
                                                                                            <asp:Button ID="cmdUpdate" runat="server" ValidationGroup="salesval" CssClass="Updatebutton1231"
                                                                                                EnableTheming="false" OnClick="cmdUpdate_Click" SkinID="skinBtnSave" />
                                                                                        </td>
                                                                                        <td style="width:10%">
                                                                                            <asp:Button ID="cmdCancel" runat="server" CssClass="cancelbutton6" EnableTheming="false"
                                                                                                OnClick="cmdCancel_Click" SkinID="skinBtnCancel" />
                                                                                        </td>
                                                                                        <td style="width:40%"></td>
                                                                                       <tr style="height:5px">
                                                                                                    </tr>
                                                                                        <tr style="height:5px">
                                                                                                    </tr>
                                                                                        <tr style="height:5px">
                                                                                                    </tr>

                                                                                    </tr>
                                                                                </table>
                                                                            </asp:Panel>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>
                            <asp:Panel ID="PanelBill" runat="server">
                            <table width="100%" style="margin: -4px 0px 0px 0px;">
                                <tr style="width: 100%">
                                    <td>
                                <div style="margin: -2px 0px 0px 0px;">
                                    <asp:GridView ID="GridViewFormula" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                        Width="100%" AllowPaging="True" CssClass="someClass" DataKeyNames="FormulaName" EmptyDataText="No Stock Management Definition found."
                                        OnPageIndexChanging="GridViewFormula_PageIndexChanging" OnSelectedIndexChanged="GridViewFormula_SelectedIndexChanged"
                                        OnRowCreated="GridViewFormula_RowCreated" OnRowDataBound="GridViewFormula_RowDataBound">
                                        <EmptyDataRowStyle CssClass="GrdContent" />


                                          <HeaderStyle Height="30px" HorizontalAlign="Center" Font-Bold="true" BackColor="#cccccc" BorderColor="Gray" Font-Size="15px" />
                                            <RowStyle Font-Bold="true"  HorizontalAlign="Center" Height="30px" Font-Size="15px" ForeColor="#0567AE" />
                                        


                                        <Columns>
                                             <asp:BoundField DataField="Row" HeaderText="#"  HeaderStyle-BorderColor="Gray"/>
                                            <asp:BoundField DataField="FormulaName" HeaderText="Name of Product" ItemStyle-HorizontalAlign="Left"  HeaderStyle-BorderColor="Gray"/>
                                            <asp:TemplateField ItemStyle-CssClass="command"  ItemStyle-Width="10%" HeaderText="Edit" HeaderStyle-BorderColor="Gray"
                                                ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnEdit" runat="server" ToolTip="View Details" SkinID="edit"
                                                        CommandName="Select" />
                                                    <asp:ImageButton ID="btnEditDisabled" Enabled="false" SkinID="editDisable" runat="Server"></asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataRowStyle CssClass="GrdContent" />
                                        <PagerTemplate>
                                            <table style=" border-color:white">
                                                <tr style=" border-color:white">
                                                    <td style=" border-color:white">
                                                        Goto Page
                                                    </td>
                                                    <td style=" border-color:white">
                                                        <asp:DropDownList ID="ddlPageSelector" runat="server"  BackColor="#e7e7e7" AutoPostBack="true" Width="65px" OnSelectedIndexChanged="ddlPageSelector_SelectedIndexChanged" style="border:1px solid blue">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style=" border-color:white;Width:5px">
                                            
                                                    </td>
                                                    <td style=" border-color:white">
                                                        <asp:Button Text="" CommandName="Page" CommandArgument="First" runat="server" CssClass="NewFirst" EnableTheming="false" Width="22px" Height="18px"
                                                            ID="btnFirst" />
                                                    </td>
                                                    <td style=" border-color:white">
                                                        <asp:Button Text="" CommandName="Page" CommandArgument="Prev" runat="server" CssClass="NewPrev" EnableTheming="false" Width="22px" Height="18px"
                                                            ID="btnPrevious" />
                                                    </td>
                                                    <td style=" border-color:white">
                                                        <asp:Button Text="" CommandName="Page" CommandArgument="Next" runat="server" CssClass="NewNext" EnableTheming="false" Width="22px" Height="18px"
                                                            ID="btnNext" />
                                                    </td>
                                                    <td style=" border-color:white">
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
                            </asp:Panel>
                            <asp:HiddenField ID="hdTempId" runat="server" Value="0" />
                            <asp:HiddenField ID="hdFilename" runat="server" Value="0" />
                            <asp:HiddenField ID="hdFormula" runat="server" Value="0" />
                            <asp:HiddenField ID="hdSMSRequired" runat="server" Value="NO" />
                            <asp:HiddenField ID="hdEmailRequired" runat="server" Value="NO" />
                        </div>
                    </td>
                </tr>
                  <tr>
                                <td>
                                    <center>
                                  <%--  <table align="center" style="width: 100%">
                                        <tr>
                                             <td style="width: 25%"></td>
                                             <td style="width: 25%"></td>
                                             <td style="width: 25%"></td>
                                             <td style="width: 25%"></td>

                                        </tr>--%>
                                       <%-- <tr>
                                            <td style="width: 40%"></td>
                      
                                             <td style="width: 20% " align="center">--%>
                                                  <asp:Panel ID="pnlSearch" runat="server" Width="100px">
                                                    <asp:Button ID="lnkBtnAdd" runat="server" OnClick="lnkBtnAdd_Click"
                                                        EnableTheming="false" Width="130px" Font-Bold="true" Font-Size="Larger" Text="Add New Product"></asp:Button>
                                                </asp:Panel>
                                           <%-- </td>--%>
                                            <%-- <td style="width: 5%" align="left">
                                               
                                            </td>--%>
                                         <%--   <td style="width: 40%"></td>
                                           
                                        </tr>
                                      
                                    </table>--%>
                                        </center>
                                </td>
                            </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
