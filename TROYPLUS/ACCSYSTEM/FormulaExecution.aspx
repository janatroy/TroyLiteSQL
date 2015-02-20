<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="FormulaExecution.aspx.cs" Inherits="FormulaExecution" Title="Manufacture > Process Manufacturing" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajX" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <script language="javascript" type="text/javascript">

        /*@cc_on@*/
        /*@if (@_win32 && @_jscript_version>=5)

        function window.confirm(str) {
            execScript('n = msgbox("' + str + '","4132")', "vbscript");
            return (n == 6);
        }

        @end@*/

        function ConfirmHold() {
            if (Page_IsValid) {

                var rv = confirm("Do you want to Hold this Stock Processing? Please click Yes to Hold or else No to Release.");

                if (rv == true) {
                    document.getElementById('ctl00_cplhControlPanel_hdStockHold').value = "Y";
                    return true;
                }
                else {
                    document.getElementById('ctl00_cplhControlPanel_hdStockHold').value = "N";
                    return true;
                }


            }
        }


    </script>

    <style id="Style1" runat="server">
        .fancy-green .ajax__tab_header {
            background: url(App_Themes/NewTheme/Images/green_bg_Tab.gif) repeat-x;
            cursor: pointer;
        }

        .fancy-green .ajax__tab_hover .ajax__tab_outer, .fancy-green .ajax__tab_active .ajax__tab_outer {
            background: url(App_Themes/NewTheme/Images/green_left_Tab.gif) no-repeat left top;
        }

        .fancy-green .ajax__tab_hover .ajax__tab_inner, .fancy-green .ajax__tab_active .ajax__tab_inner {
            background: url(App_Themes/NewTheme/Images/green_right_Tab.gif) no-repeat right top;
        }

        .fancy .ajax__tab_header {
            font-size: 13px;
            font-weight: bold;
            color: #000;
            font-family: sans-serif;
        }

            .fancy .ajax__tab_active .ajax__tab_outer, .fancy .ajax__tab_header .ajax__tab_outer, .fancy .ajax__tab_hover .ajax__tab_outer {
                height: 46px;
            }

            .fancy .ajax__tab_active .ajax__tab_inner, .fancy .ajax__tab_header .ajax__tab_inner, .fancy .ajax__tab_hover .ajax__tab_inner {
                height: 46px;
                margin-left: 16px; /* offset the width of the left image */
            }

            .fancy .ajax__tab_active .ajax__tab_tab, .fancy .ajax__tab_hover .ajax__tab_tab, .fancy .ajax__tab_header .ajax__tab_tab {
                margin: 16px 16px 0px 0px;
            }

        .fancy .ajax__tab_hover .ajax__tab_tab, .fancy .ajax__tab_active .ajax__tab_tab {
            color: #fff;
        }

        .fancy .ajax__tab_body {
            font-family: Arial;
            font-size: 10pt;
            border-top: 0;
            border: 1px solid #999999;
            padding: 8px;
            background-color: #ffffff;
        }
    </style>

    <asp:UpdatePanel ID="UpdatePanel16" runat="server" UpdateMode="Always">
        <ContentTemplate>

            <table style="width: 100%">
                <tr style="width: 100%">
                    <td style="width: 100%">

                        <%--<div class="mainConHd">
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tr valign="middle">
                                        <td>
                                            <span>Inventory Manufacturing / Transfers</span>
                                        </td>
                                    </tr>
                                </table>
                            </div>--%>
                        <%--<table class="mainConHd" style="width: 994px;">
                                <tr valign="middle">
                                    <td style="font-size: 20px;">
                                        Inventory Manufacturing / Transfers
                                    </td>
                                </tr>
                            </table>--%>
                        <div class="mainConBody">
                            <table style="width: 99.8%; margin: -2px 0px 0px 1px;" cellpadding="3" cellspacing="2" class="searchbg">
                                <tr style="height: 25px; vertical-align: middle">
                                    <%--<td style="width: 2%;"></td>--%>
                                    <td style="width: 25%; font-size: 22px; color: white;">Process Manufacturing 
                                    </td>
                                    <td style="width: 2%">
                                        <div style="text-align: right;">
                                        </div>
                                    </td>
                                    <%--<td  style="width: 5%; color: #000000;" align="right">
                                            Start Date
                                        </td>--%>
                                    <td style="width: 15%" class="NewBox">
                                        <asp:TextBox ID="txtStartDate" Enabled="false" CssClass="cssTextBox" Width="80px" runat="server"
                                            MaxLength="10" ValidationGroup="salesval">
                                        </asp:TextBox>




                                    </td>
                                    <td style="width: 2%;">
                                        <script type="text/javascript" language="JavaScript">
                                            new tcal({ 'formname': 'aspnetForm', 'controlname': GettxtBoxName('txtStartDate') });
                                        </script>
                                    </td>
                                    <td style="width: 15%" class="NewBox">
                                        <asp:TextBox ID="txtEndDate" Enabled="false" CssClass="cssTextBox" Width="80px" runat="server" MaxLength="10"
                                            ValidationGroup="salesval"></asp:TextBox>




                                    </td>
                                    <td style="width: 2%;">
                                        <script type="text/javascript" language="JavaScript">
                                            new tcal({ 'formname': 'aspnetForm', 'controlname': GettxtBoxName('txtEndDate') });
                                        </script>
                                    </td>
                                    <td style="width: 6%; color: #000000;">
                                        <asp:CheckBox ID="rdoIsPros" runat="server" Text="Processed" />
                                    </td>
                                    <td style="width: 8%; text-align: left">
                                        <asp:Button ValidationGroup="search" ID="btnSearch" OnClick="btnSearch_Click" runat="server"
                                            Text="" EnableTheming="false" CssClass="ButtonSearch6" />
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
                        <asp:Panel runat="server" ID="popUp" Style="width: 62%; background-color: White; display: none">
                            <div id="contentPopUp">
                                <table style="width: 100%; border: 1px solid #86b2d1" align="left" cellpadding="3" cellspacing="5">
                                    <tr>
                                        <td colspan="4">
                                            <table class="headerPopUp" style="border: 1px solid #86b2d1" width="750px">
                                                <tr>
                                                    <td>Process Products
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr style="width: 100%;">
                                        <td style="width: 100%">
                                            <div style="text-align: center">
                                                <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Size="15px" Width="90%" CssClass="info" SkinID="skinHistoryMsg"></asp:Label>
                                            </div>
                                            <asp:Panel ID="PanelTemplatesList" runat="server" Visible="false" Style="text-align: center">
                                                <asp:GridView ID="GridViewTemplates" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                    Width="814px" AllowPaging="True" DataKeyNames="FormulaName" EmptyDataText="No Stock Definations found. Please add new Stock Mgmt Defination."
                                                    OnPageIndexChanging="GridViewTemplates_PageIndexChanging" OnSelectedIndexChanged="GridViewTemplates_SelectedIndexChanged"
                                                    PageSize="7" OnRowCreated="GridViewTemplates_RowCreated" OnRowDataBound="GridViewTemplates_RowDataBound" CssClass="someClass">
                                                    <EmptyDataRowStyle CssClass="GrdContent" />
                                                    <Columns>
                                                        <asp:BoundField DataField="FormulaName" HeaderText="Process Products" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="15px" HeaderStyle-BackColor="#cccccc" ItemStyle-ForeColor="#0567AE" ItemStyle-Font-Bold="true" ItemStyle-Font-Size="13px" ItemStyle-Height="20px" HeaderStyle-BorderColor="Gray" />
                                                        <asp:CommandField ShowSelectButton="True" ControlStyle-ForeColor="#3464cc" SelectText="Process this Product" ControlStyle-Font-Size="small" HeaderStyle-BackColor="#cccccc" HeaderStyle-BorderColor="Gray">
                                                            <ItemStyle Width="20%" HorizontalAlign="Center" />
                                                            <ControlStyle ForeColor="#3464CC"></ControlStyle>
                                                        </asp:CommandField>
                                                    </Columns>
                                                    <EmptyDataRowStyle CssClass="GrdContent" />
                                                    <PagerTemplate>
                                                        <table>
                                                            <tr>
                                                                <td>Goto Page
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlPageSelector" runat="server" AutoPostBack="true" Width="65px" OnSelectedIndexChanged="ddlPageSelector_SelectedIndexChanged" Style="border: 1px solid blue">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="width: 5px"></td>
                                                                <td>
                                                                    <asp:Button CommandName="Page" CommandArgument="First" runat="server" CssClass="NewFirst" EnableTheming="false" Width="22px" Height="18px"
                                                                        ID="btnFirst" />
                                                                </td>
                                                                <td>
                                                                    <asp:Button CommandName="Page" CommandArgument="Prev" runat="server" CssClass="NewPrev" EnableTheming="false" Width="22px" Height="18px"
                                                                        ID="btnPrevious" />
                                                                </td>
                                                                <td>
                                                                    <asp:Button CommandName="Page" CommandArgument="Next" runat="server" CssClass="NewNext" EnableTheming="false" Width="22px" Height="18px"
                                                                        ID="btnNext" />
                                                                </td>
                                                                <td>
                                                                    <asp:Button CommandName="Page" CommandArgument="Last" runat="server" CssClass="NewLast" EnableTheming="false" Width="22px" Height="18px"
                                                                        ID="btnLast" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </PagerTemplate>
                                                </asp:GridView>
                                                <div align="center">
                                                    <asp:Button ID="BtnDefnBack" Width="80px" runat="server" CssClass="GoBack" OnClick="cmdCancel_Click"
                                                        EnableTheming="false" />
                                                </div>
                                            </asp:Panel>
                                            <br />
                                            <asp:Panel ID="PanelTemplateGrids" runat="server" Visible="false">
                                                <div style="text-align: left">
                                                    <cc1:TabContainer ID="tabs2" runat="server" Width="100%" ActiveTabIndex="0" CssClass="fancy fancy-green">
                                                        <cc1:TabPanel ID="tabMaster" runat="server" HeaderText="Process Products">
                                                            <ContentTemplate>
                                                                <table style="width: 800px; border: 0px solid #86b2d1" align="left" cellpadding="0"
                                                                    cellspacing="0" class="accordionContent">
                                                                    <tr class="tblLeft" style="width: 100%">
                                                                        <td style="width: 100%" colspan="5">
                                                                            <table width="100%">
                                                                                <tr>
                                                                                    <td style="width: 25%" class="ControlLabelproject1">Name of Product 
                                                                                    </td>
                                                                                    <td style="width: 20%" class="ControlTextBox3">
                                                                                        <asp:Label ID="lblFormula" runat="server" Font-Bold="True"></asp:Label>
                                                                                    </td>
                                                                                    <td style="width: 20%" class="ControlLabelproject1">
                                                                                        <asp:CompareValidator ControlToValidate="txtDate" Operator="DataTypeCheck" Type="Date"
                                                                                            ValidationGroup="FormulaInfo" ErrorMessage="Please Select a valid date(dd/MM/yyyy)"
                                                                                            runat="server" ID="cmpValtxtDate"></asp:CompareValidator>
                                                                                        <asp:RequiredFieldValidator ValidationGroup="FormulaInfo" ID="rqDate" runat="server"
                                                                                            Display="Dynamic" ControlToValidate="txtDate" ErrorMessage="Mandatory Field"></asp:RequiredFieldValidator>
                                                                                        <%--<asp:Label ID="Label1" runat="server" Text="Date *" Width="100px"></asp:Label>--%>
                                                                                        Date *
                                                                                    </td>
                                                                                    <td style="width: 17%;" class="ControlTextBox3">
                                                                                        <asp:TextBox ID="txtDate" Enabled="false" runat="server" CssClass="cssTextBox" Width="80px"></asp:TextBox>

                                                                                        <cc1:CalendarExtender ID="calExtender3" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                                            PopupButtonID="btnDate3" PopupPosition="BottomLeft" TargetControlID="txtDate">
                                                                                        </cc1:CalendarExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:ImageButton ID="btnDate3" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                                            Width="20px" runat="server" />
                                                                                    </td>

                                                                                    <td align="left" style="width: 10%"></td>
                                                                                </tr>
                                                                                <tr class="tblLeft">
                                                                                    <td style="width: 25%" class="ControlLabelproject1">Processing Completed?
                                                                                    </td>
                                                                                    <td style="width: 20%;" class="ControlTextBox3">
                                                                                        <asp:RadioButtonList ID="rdComplete" runat="server" RepeatDirection="Horizontal">
                                                                                            <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                                                                            <asp:ListItem Text="No" Selected="True" Value="N"></asp:ListItem>
                                                                                        </asp:RadioButtonList>
                                                                                    </td>
                                                                                    <td style="width: 20%" class="ControlLabelproject1">Comments
                                                                                    </td>
                                                                                    <td style="width: 17%;" class="ControlTextBox3">
                                                                                        <asp:TextBox ID="txtComments" runat="server" Width="50px" MaxLength="250" Height="30px"
                                                                                            CssClass="cssTextBox" TextMode="MultiLine"></asp:TextBox>
                                                                                    </td>
                                                                                    <td style="width: 10%"></td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="tblLeft">
                                                                        <td>
                                                                            <table width="100%">
                                                                                <tr>
                                                                                    <td style="width: 100%; font-size: larger; font-weight: bold">Raw Materials
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 100%">
                                                                                        <asp:GridView ID="grdIn" runat="server" Width="100%" AutoGenerateColumns="False" CssClass="someClass"
                                                                                            AllowPaging="True" OnRowCreated="grdIn_RowCreated" DataKeyNames="ID" EmptyDataText="No Input Products to Found."
                                                                                            OnDataBound="grdIn_DataBound" OnRowDataBound="grdIn_RowDataBound">
                                                                                            <EmptyDataRowStyle Font-Bold="False" />

                                                                                            <HeaderStyle Height="30px" Width="20%" HorizontalAlign="Center" Font-Bold="true" BackColor="#cccccc" BorderColor="Gray" Font-Size="15px" />
                                                                                            <RowStyle Font-Bold="true" HorizontalAlign="Center" Height="30px" Font-Size="15px" ForeColor="#0567AE" />

                                                                                            <Columns>
                                                                                                <asp:BoundField HeaderText="Component ID" ReadOnly="True" DataField="itemCode" HeaderStyle-Width="20%" HeaderStyle-BorderColor="Gray" />

                                                                                                <asp:BoundField HeaderText="Description" ReadOnly="True" HeaderStyle-Width="20%" DataField="ProductDesc" HeaderStyle-BorderColor="Gray" />

                                                                                                <asp:BoundField HeaderText="Component Name" ReadOnly="True" HeaderStyle-Width="20%" DataField="ProductName" HeaderStyle-BorderColor="Gray" />

                                                                                                <asp:TemplateField HeaderText="Qty." HeaderStyle-BorderColor="Gray">
                                                                                                    <ItemStyle Width="20%" />
                                                                                                    <ItemTemplate>
                                                                                                        <br />
                                                                                                        <asp:TextBox ID="txtQty" runat="server" Text='<%# Bind("Qty") %>' CssClass="cssTextBox"
                                                                                                            Width="50px"></asp:TextBox>
                                                                                                        <asp:HiddenField ID="lblID" runat="Server" Value='<%# Bind("ID") %>' />
                                                                                                        <asp:HiddenField ID="hdStockLimlt" runat="Server" Value='<%# Bind("Stock") %>' />
                                                                                                        <asp:RequiredFieldValidator ValidationGroup="FormulaInfo" ID="rqQty" runat="server"
                                                                                                            Display="Dynamic" ControlToValidate="txtQty" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                                                        <br />
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:BoundField HeaderText="Stock Limit" HeaderStyle-Width="20%" ReadOnly="True" DataField="Stock" HeaderStyle-BorderColor="Gray" />
                                                                                            </Columns>
                                                                                            <PagerTemplate>
                                                                                                <table style="border-color: white">
                                                                                                    <tr style="border-color: white">
                                                                                                        <td style="border-color: white">Goto Page
                                                                                                        </td>
                                                                                                        <td style="border-color: white">
                                                                                                            <asp:DropDownList ID="ddlPageSelector" runat="server" AutoPostBack="true" Width="65px" Style="border: 1px solid blue" BackColor="#BBCAFB">
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
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 100%; font-size: larger; font-weight: bold">Product
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 100%">
                                                                                        <asp:GridView ID="grdOut" runat="server" Width="100%" AutoGenerateColumns="False" CssClass="someClass"
                                                                                            OnRowDataBound="grdOut_RowDataBound" AllowPaging="True" OnRowCreated="grdOut_RowCreated"
                                                                                            DataKeyNames="ID" EmptyDataText="No Input Products to Found.">
                                                                                            <EmptyDataRowStyle Font-Bold="False" />

                                                                                            <HeaderStyle Height="30px" HorizontalAlign="Center" Font-Bold="true" BackColor="#cccccc" BorderColor="Gray" Font-Size="15px" />
                                                                                            <RowStyle Font-Bold="true" HorizontalAlign="Center" Height="30px" Font-Size="15px" ForeColor="#0567AE" />

                                                                                            <Columns>

                                                                                                <asp:BoundField HeaderText="Component ID" HeaderStyle-Width="20%" ReadOnly="True" DataField="itemCode" HeaderStyle-BorderColor="Gray" />
                                                                                                <asp:BoundField HeaderText="Description" HeaderStyle-Width="20%" ReadOnly="True" DataField="ProductDesc" HeaderStyle-BorderColor="Gray" />
                                                                                                <asp:BoundField HeaderText="Component Name" ReadOnly="True" HeaderStyle-Width="20%" DataField="ProductName" HeaderStyle-BorderColor="Gray" />

                                                                                                <asp:TemplateField HeaderText="Qty." HeaderStyle-Width="20%" HeaderStyle-BorderColor="Gray">

                                                                                                    <ItemStyle Width="20%" />
                                                                                                    <ItemTemplate>
                                                                                                        <br />
                                                                                                        <asp:TextBox ID="txtQty" runat="server" Text='<%# Bind("Qty") %>' CssClass="cssTextBox"
                                                                                                            Width="50px"></asp:TextBox>
                                                                                                        <asp:HiddenField ID="hdStockLimlt" runat="Server" Value='<%# Bind("Stock") %>' />
                                                                                                        <asp:RequiredFieldValidator ValidationGroup="FormulaInfo" ID="rqQty" runat="server"
                                                                                                            Display="Dynamic" ControlToValidate="txtQty" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                                                        <asp:HiddenField ID="lblOut" runat="Server" Value='<%# Bind("ID") %>' />
                                                                                                        <asp:CompareValidator ControlToValidate="txtQty" Operator="GreaterThanEqual" Type="Double"
                                                                                                            ValidationGroup="FormulaInfo" ValueToCompare="0" ErrorMessage="Invalid" runat="server"
                                                                                                            ID="cmpValtxtDate"></asp:CompareValidator>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:BoundField HeaderText="  " ReadOnly="True" HeaderStyle-Width="20%" HeaderStyle-BorderColor="Gray" />
                                                                                            </Columns>
                                                                                            <PagerTemplate>
                                                                                                <table style="border-color: white">
                                                                                                    <tr style="border-color: white">
                                                                                                        <td style="border-color: white">Goto Page
                                                                                                        </td>
                                                                                                        <td style="border-color: white">
                                                                                                            <asp:DropDownList ID="ddlPageSelector" runat="server" AutoPostBack="true" Width="65px" Style="border: 1px solid blue" BackColor="#BBCAFB">
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
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>

                                                                    <tr style="width: 100%;">
                                                                        <td style="width: 100%;">

                                                                            <table style="width: 100%;">
                                                                                <tr>
                                                                                    <td style="width: 25%"></td>
                                                                                    <td style="width: 25%" align="right">
                                                                                        <asp:Button ID="cmdSave" runat="server" ValidationGroup="FormulaInfo" CssClass="savebutton1231" EnableTheming="false"
                                                                                            OnClick="cmdSave_Click" />
                                                                                    </td>
                                                                                    <td style="width: 25%">
                                                                                        <asp:Panel ID="PanelCmd" runat="server" Visible="False">
                                                                                            <asp:Button ID="cmdCancel" Width="80px" runat="server" CssClass="GoBack" OnClick="cmdCancel_Click"
                                                                                                EnableTheming="false" />
                                                                                        </asp:Panel>
                                                                                    </td>
                                                                                    <td style="width: 25%"></td>
                                                                                </tr>
                                                                            </table>

                                                                        </td>
                                                                    </tr>
                                                                    <tr align="center">
                                                                        <td style="width: 100%" colspan="3">
                                                                            <asp:Label runat="server" ID="Error" ForeColor="Red"></asp:Label>
                                                                            <asp:ValidationSummary ID="valSum" ShowMessageBox="True" ShowSummary="False" HeaderText=" "
                                                                                Font-Names="'Trebuchet MS'" runat="server" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </cc1:TabPanel>
                                                    </cc1:TabContainer>
                                                </div>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                        <br />
                        <asp:Panel ID="PanelProductsList" runat="server" Visible="false">
                            <table width="100%" style="margin: -4px 0px 0px 0px;">
                                <tr style="width: 100%">
                                    <td>
                                        <div style="text-align: left; margin: -2px 0px 0px 0px;">
                                            <asp:GridView ID="GridViewProducts" runat="server" Width="100%" DataKeyNames="CompID" AutoGenerateColumns="false"
                                                EmptyDataText="No Stock Processing found." OnPageIndexChanging="GridViewProducts_PageIndexChanging"
                                                OnSelectedIndexChanged="GridViewProducts_SelectedIndexChanged" OnRowCreated="GridViewProducts_RowCreated"
                                                OnRowDataBound="GridViewProducts_RowDataBound" OnRowDeleting="GridViewProducts_RowDeleting" CssClass="someClass">
                                                <HeaderStyle Height="30px" HorizontalAlign="Center" Font-Bold="true" BackColor="#cccccc" BorderColor="Gray" Font-Size="Small" />
                                                <RowStyle Font-Bold="true" HorizontalAlign="Center" Height="30px" Font-Size="Small" ForeColor="#0567AE" />
                                                <Columns>
                                                    <asp:BoundField DataField="CDate" HeaderText="Date Created" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-BorderColor="Gray" />
                                                    <asp:BoundField DataField="FormulaName" HeaderText="Definition" HeaderStyle-BorderColor="Gray" />
                                                    <asp:BoundField DataField="IsReleased" HeaderText="Processed" HeaderStyle-BorderColor="Gray" />
                                                    <asp:TemplateField ItemStyle-CssClass="command" ItemStyle-Width="50px" HeaderText="Process" HeaderStyle-BorderColor="Gray">
                                                        <ItemTemplate>
                                                            <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="btnRelease" ConfirmText="Are you sure to complete the processing?"
                                                                runat="server">
                                                            </cc1:ConfirmButtonExtender>
                                                            <asp:ImageButton ID="btnRelease" runat="server" SkinID="GridRelease" ToolTip="Click here to complete the processing."
                                                                CommandName="Delete" />
                                                            <asp:HiddenField ID="hdcompID" runat="server" Value='<%# Bind("CompID") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="command" ItemStyle-Width="50px" HeaderText="View" HeaderStyle-BorderColor="Gray">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnSelect" runat="server" SkinID="GridForward" ToolTip="Click here to view the Details."
                                                                CommandName="Select" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
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
                                <tr>
                                    <td>
                                        <table align="center" style="width: 100%">
                                            <tr>
                                                <td style="width: 35%"></td>

                                                <td style="width: 15%" align="center">

                                                    <asp:Button ID="lnkBtnAdd" runat="server" OnClick="lnkBtnAdd_Click"
                                                        EnableTheming="false" Width="220px" BackColor="WhiteSmoke" Font-Size="12px" Font-Bold="true" Text="Process New Product/Component"></asp:Button>


                                                </td>
                                                <td style="width: 10%" align="right">
                                                    <asp:Button ID="btnExportToExcel" runat="server" CssClass="exportexl6forproject" OnClientClick="window.open('ReportExcelFormulaExecution.aspx','billSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=310,width=500,left=425,top=220, scrollbars=yes');"
                                                        EnableTheming="false"></asp:Button>
                                                </td>
                                                <td style="width: 35%"></td>

                                            </tr>

                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:HiddenField ID="hdStockHold" runat="server" Value="N" />
                        <asp:HiddenField ID="hdSMSRequired" runat="server" Value="NO" />
                        <asp:HiddenField ID="hdEmailRequired" runat="server" Value="NO" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
