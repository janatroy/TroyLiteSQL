<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true" CodeFile="TimeSheet.aspx.cs" Inherits="TimeSheet" Title="Human Resource > Timesheet Entry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <script type="text/javascript">

        function pageLoad() {
            var tabContainer = $find('<%=tbMain.ClientID%>');
            var tabs = tabContainer.get_tabs();

            for (var i = 0; i < tabs.length; i++) {
                var tab = tabs[i];

                $addHandler(tab.get_headerTab(), 'mouseover', Function.createDelegate(tab, function () { tabContainer.set_activeTab(this); }))
            }
        }

        function ActiveTabChanged() {
            var tabClickButton = document.getElementById('<%=tabClickButton.ClientID%>');
            if (tabClickButton != null) {
                tabClickButton.click();
            }
        }

        function setHeight(txtDesc, rowIdx, col) {
            var objGridView = document.getElementById('<%=gridWeeklyTimesheets.ClientID%>');

            var txtHeight = objGridView.rows[rowIdx].cells[col].children[0].scrollHeight;

            for (j = 1; j <= 7; j++) {
                objGridView.rows[rowIdx].cells[j].children[0].style["height"] = txtHeight + "px";
            }

            // }
        }

        function setTextblur(txtDesc, rowIdx, colCount) {
            var objGridView = document.getElementById('<%=gridWeeklyTimesheets.ClientID%>');

            // No. of rows & columns iteration.
            for (i = 1; i <= 9 ; i++) {
                for (j = 1; j <= 7; j++) {
                    objGridView.rows[i].cells[j].children[0].style["width"] = "161px";
                    objGridView.rows[i].cells[j].children[0].style["height"] = "30px";
                }
            }
        }

        var GridView;
        var cellValue
        var gridViewCtlId;

        var gridViewCtl = null;
        var curSelRow = null;

        var curRowIdx = -1;
        var v;

        function updateValue(field, rowIdx, col) {
            var ColCount = 7;
            var rowtotal = 9;
            var cellValue = 0;

            var currValue = 0;
            var j = 0;

            var objGridView = document.getElementById('<%=gridWeeklyTimesheets.ClientID%>');

            for (j = 1; j <= rowtotal; j++) {

                for (k = 1; k <= ColCount; k++) {
                    if (k == col) {
                        objGridView.rows[j].cells[col].children[0].style["width"] = '380px';
                    } else { objGridView.rows[j].cells[k].children[0].style["width"] = '124px'; }

                }
            }

            var txtHeight = objGridView.rows[rowIdx].cells[1].children[0].scrollHeight;

            for (j = 2; j <= ColCount; j++) {
                var temp = objGridView.rows[rowIdx].cells[j].children[0].scrollHeight;
                if (temp > txtHeight)
                    txtHeight = temp;
            }

            if (txtHeight >= 130)
                txtHeight = 130;
            if (objGridView.rows[rowIdx].cells[col].children[0].scrollHeight >= 130)
                txtHeight = objGridView.rows[rowIdx].cells[col].children[0].scrollHeight;

            for (j = 1; j <= ColCount; j++) {
                objGridView.rows[rowIdx].cells[j].children[0].style["height"] = txtHeight + "px";
            }
        }

        function IsSubmitted(strApproved, strMode) {

            if (strApproved == "Submitted") {
                alert("Submitted record cannot be " + strMode + "!");
                return false;
            }
            else
                return true;
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

    <asp:UpdatePanel ID="UpdatePanelPage" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:ValidationSummary ID="VSContact" runat="server" Font-Names="'Trebuchet MS'" Font-Size="12pt"
                HeaderText="Validation Messages" ShowMessageBox="true" ShowSummary="true" ValidationGroup="Save" />
            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                CancelControlID="Button1" DynamicServicePath="" Enabled="True" PopupControlID="pnsTime"
                TargetControlID="dummy" RepositionMode="RepositionOnWindowScroll">
            </cc1:ModalPopupExtender>
            <input id="dummy" type="button" style="display: none" runat="server" />
            <input id="Button1" type="button" style="display: none" runat="server" />

            <asp:HiddenField ID="hdTse" runat="server" Value="0" />
            <div align="left" style="vertical-align: top; margin: -3px 0px 0px 5px; width: 99.5%" class="mainConBody">
                <cc1:TabContainer ID="tbMain" Visible="false" OnClientActiveTabChanged="ActiveTabChanged" AutoPostBack="false" runat="server" CssClass="fancy fancy-green">
                    <cc1:TabPanel ID="tblMaster1" runat="server" Width="976px" HeaderText="Daily - Time Sheet Entry">
                        <ContentTemplate>
                            <table style="vertical-align: top;" width="976px">
                                <tr>
                                    <td valign="top">
                                        <asp:Panel runat="server" ID="pnlTimesheetEntryList" Width="100%">
                                            <table width="100%" style="margin: -1px 0px 0px 1px;" cellpadding="3" cellspacing="2" class="searchbg">
                                                <tr style="vertical-align: middle">
                                                    <td style="width: 25%">
                                                        <div style="text-align: right;">
                                                           
                                                        </div>
                                                    </td>
                                                    <td style="width: 14%; color: white;" align="right">Search
                                                    </td>
                                                    <td style="width: 15%" class="NewBox">
                                                        <asp:TextBox ID="txtTimeSheetValueField" runat="server" SkinID="skinTxtBoxSearch" MaxLength="50" />

                                                    </td>
                                                    <td id="tdCalendar" style="width: 4%; color: #000080;" align="left" runat="server">
                                                        <script type="text/javascript" language="JavaScript">                                                    new tcal({ 'formname': 'aspnetForm', 'controlname': GettxtBoxName('txtTimeSheetValueField') });</script>
                                                    </td>
                                                    <td style="width: 14%; text-align: left;" class="NewBox">
                                                        <asp:DropDownList ID="ddlTimeSheetKeyField" runat="server" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlTimeSheetKeyField_SelectedIndexChanged" BackColor="White" Style="text-align: center; border: 1px solid white; margin: 1px; height: 25px; width: 150px;"
                                                            EnableTheming="False">
                                                            <asp:ListItem>-- All --</asp:ListItem>
                                                            <asp:ListItem Value="DateRange:string">Date Range</asp:ListItem>
                                                            <asp:ListItem Value="DateSubmitted:datetime">Date Submitted</asp:ListItem>
                                                            <asp:ListItem Value="Pendingwith:string">Pending with</asp:ListItem>
                                                            <asp:ListItem Value="Approved:string">Status</asp:ListItem>
                                                            <asp:ListItem Value="Rejectreason:string">Reject reason</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="width: 3%; text-align: left;"></td>
                                                    <td style="width: 10%; text-align: left">

                                                        <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click"
                                                            CssClass="ButtonSearch6" EnableTheming="False" />
                                                    </td>
                                                    <td style="width: 20%" class="tblLeftNoPad">
                                                        <asp:Button ID="BtnClearFilter" runat="server" OnClick="BtnClearFilter_Click" EnableTheming="false" Text="" CssClass="ClearFilter6" />
                                                    </td>
                                                </tr>
                                                <tr id="ErrMessage">
                                                    <td colspan="7">
                                                        <asp:Label ID="txtErrorMessage" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="7">
                                                        <table width="100%" style="margin: -1px 0px 0px 0px;">
                                                            <tr>
                                                                <td>
                                                                    <asp:GridView ID="GrdTse" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                                        Width="117%" AllowPaging="True" OnPageIndexChanging="GrdTse_PageIndexChanging" CssClass="someClass" OnRowDataBound="GrdTse_RowDataBound"
                                                                        OnRowCreated="GrdTse_RowCreated" DataKeyNames="Empno" EmptyDataText="No Time Sheet Entry Details found for the search criteria"
                                                                        OnSelectedIndexChanged="GrdTse_SelectedIndexChanged" PageSize="2">
                                                                        <EmptyDataRowStyle CssClass="GrdContent" />
                                                                        <Columns>
                                                                            <asp:BoundField DataField="DateRange" HeaderText="Date range" DataFormatString="{0:dd/MM/yyyy}">
                                                                                <HeaderStyle BorderColor="Gray" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="DateSubmitted" HeaderText="Date submitted" DataFormatString="{0:dd/MM/yyyy}">
                                                                                <HeaderStyle BorderColor="Gray" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="Pendingwith" HeaderText="Pending with">
                                                                                <HeaderStyle BorderColor="Gray" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="Approved" HeaderText="Status">
                                                                                <HeaderStyle BorderColor="Gray" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="Rejectreason" HeaderText="Reason">
                                                                                <HeaderStyle BorderColor="Gray" />
                                                                            </asp:BoundField>
                                                                            <asp:TemplateField HeaderText="Edit">
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" OnClick="btnEdit_Click" CommandName="Select" />
                                                                                    <asp:ImageButton ID="btnEditDisabled" Enabled="false" SkinID="editDisable" runat="Server"></asp:ImageButton>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle BorderColor="Gray" Width="50px" />
                                                                                <ItemStyle CssClass="command" HorizontalAlign="Center" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Delete">
                                                                                <ItemTemplate>
                                                                                    <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to Delete this Time Sheet Entry?"
                                                                                        runat="server">
                                                                                    </cc1:ConfirmButtonExtender>
                                                                                    <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" OnClick="lnkB_Click" CommandName="Delete"></asp:ImageButton>
                                                                                    <asp:ImageButton ID="lnkBDisabled" Enabled="false" SkinID="deleteDisable" runat="Server"></asp:ImageButton>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle BorderColor="Gray" Width="50px" />
                                                                                <ItemStyle CssClass="command" HorizontalAlign="Center" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <PagerTemplate>
                                                                            <table style="border-color: white">
                                                                                <tr style="border-color: white">
                                                                                    <td style="border-color: white">Goto Page
                                                                                    </td>
                                                                                    <td style="border-color: white">
                                                                                        <asp:DropDownList ID="ddlPageSelector" runat="server" AutoPostBack="true" BackColor="#BBCAFB" Width="65px" Style="border: 1px solid blue"
                                                                                            OnSelectedIndexChanged="ddlPageSelector_SelectedIndexChanged">
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
                                                                        <RowStyle ForeColor="Black" />
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="tblMaster2" runat="server" Width="976px" HeaderText="Time Sheet Approval">
                        <ContentTemplate>
                            <asp:Panel runat="server" ID="pnlTimesheetApprove" Width="100%">
                                <table width="976px" style="margin: -1px 0px 0px 1px;" cellpadding="3" cellspacing="2" class="searchbg">
                                    <tr style="vertical-align: middle">
                                        <td style="width: 10%">
                                            <div style="text-align: right;">
                                            </div>
                                        </td>
                                        <td style="width: 5%; color: white;" align="right">Search
                                        </td>

                                        <td style="width: 15%" class="NewBox">
                                            <asp:TextBox ID="txtTimeSheetApproval" runat="server" SkinID="skinTxtBoxSearch" MaxLength="50" />
                                        </td>
                                        <td style="width: 4%; color: #000080;" align="left">
                                            <script type="text/javascript" language="JavaScript">                                                    new tcal({ 'formname': 'aspnetForm', 'controlname': GettxtBoxName('txtTimeSheetApproval') });</script>
                                        </td>
                                        <td style="width: 14%; text-align: left;" class="NewBox">
                                            <asp:DropDownList ID="ddlTimeSheetApproval" runat="server" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlTimeSheetApproval_SelectedIndexChanged" BackColor="White" Style="text-align: center; border: 1px solid white; padding: 1px; margin: 1px; height: 23px; width: 150px;"
                                                EnableTheming="False" Visible="True">
                                                <asp:ListItem>-- All --</asp:ListItem>
                                                <asp:ListItem Value="EmployeeName:string">Employee Name</asp:ListItem>
                                                <asp:ListItem Value="DateRange:string">Date Range</asp:ListItem>
                                                <asp:ListItem Value="Approved:string">Status</asp:ListItem>
                                                <asp:ListItem Value="Rejectreason:string">Reject reason</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td id="td1" style="width: 3%; text-align: left;" runat="server"></td>
                                        <td style="width: 10%; text-align: left">

                                            <asp:Button ID="btnApprovalSearch" runat="server" OnClick="btnApprovalSearch_Click"
                                                CssClass="ButtonSearch6" EnableTheming="False" />
                                        </td>
                                        <td style="width: 20%" class="tblLeftNoPad">
                                            <asp:Button ID="BtnClrFilter" runat="server" OnClick="BtnClrFilter_Click" EnableTheming="false" Text="" CssClass="ClearFilter6" />
                                        </td>
                                    </tr>
                                    <tr id="tblMaster2ErrMessage">
                                        <td colspan="7">
                                            <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td colspan="7">
                                            <table width="100%" style="margin: -1px 0px 0px 0px;">
                                                <tr>
                                                    <td>
                                                        <asp:GridView ID="GrdSubTSe" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                            Width="120%" AllowPaging="True" CssClass="someClass"
                                                            EmptyDataText="No Subordinate Details for Approval" OnRowDataBound="GrdSubTSe_RowDataBound">
                                                            <%--OnPageIndexChanging="" OnRowCreated=""  OnSelectedIndexChanged="" OnRowDeleting="" OnRowDataBound=""--%>
                                                            <EmptyDataRowStyle CssClass="GrdContent" />
                                                            <HeaderStyle Height="30px" HorizontalAlign="Center" Font-Bold="true" BackColor="#cccccc" BorderColor="Gray" Font-Size="Small" />
                                                            <RowStyle Font-Bold="true" HorizontalAlign="Center" Height="30px" Font-Size="Small" ForeColor="#0567AE" />
                                                            <Columns>
                                                                <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name">
                                                                    <HeaderStyle BorderColor="Gray" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="DateRange" HeaderText="Date range">
                                                                    <HeaderStyle BorderColor="Gray" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Approved" HeaderText="Status">
                                                                    <HeaderStyle BorderColor="Gray" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Rejectreason" HeaderText="Reason">
                                                                    <HeaderStyle BorderColor="Gray" />
                                                                </asp:BoundField>

                                                                <asp:TemplateField HeaderText="Appr">
                                                                    <ItemTemplate>
                                                                        <%--<cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to Delete this Time Sheet Entry?"
                                                                                        runat="server">
                                                                                    </cc1:ConfirmButtonExtender>--%>
                                                                        <asp:ImageButton ID="lnkAppr" SkinID="Appr" runat="Server" CommandName="DateRange" OnClick="lnkApproveLineItem_Click"></asp:ImageButton>
                                                                        <asp:ImageButton ID="lnkApprDisabled" Enabled="false" SkinID="ApprDisable" runat="Server"></asp:ImageButton>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle BorderColor="Gray" Width="50px" />
                                                                    <ItemStyle CssClass="command" HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <PagerTemplate>
                                                                <%-- <table style="border-color: white">
                                                                                <tr style="border-color: white">
                                                                                    <td style="border-color: white">Goto Page
                                                                                    </td>
                                                                                    <td style="border-color: white">
                                                                                        <asp:DropDownList ID="tabMaster2ddlPageSelector" runat="server" AutoPostBack="true" BackColor="#BBCAFB" Width="65px" Style="border: 1px solid blue"
                                                                                            OnSelectedIndexChanged="">
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
                                                                            </table>--%>
                                                            </PagerTemplate>
                                                            <RowStyle ForeColor="Black" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </ContentTemplate>
                    </cc1:TabPanel>
                </cc1:TabContainer>

                <asp:Panel ID="pnsTime" runat="server" BackColor="White" Style="height: 590px; width: 90%; display: none" ScrollBars="Vertical">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table id="tableWeeklyTimesheets" cellpadding="3" cellspacing="2">
                                <tr id="pnsTse" runat="server">
                                    <td style="vertical-align: top; width: 70px" runat="server">
                                        <asp:Calendar ID="WeeklyCalendar" OnSelectionChanged="WeeklyCalendar_SelectionChanged" Height="100px" Width="70px" runat="server" SelectionMode="DayWeek" ShowGridLines="True"></asp:Calendar>
                                    </td>
                                    <td width="100%" style="vertical-align: top;">
                                        <table width="100%" cellpadding="1" cellspacing="1">
                                            <tr>
                                                <td id="tdTimeSheetTitle" style="vertical-align: top; width: 100%; height: 30px; font-size: 24px; font-weight: 150; color: white; background-color: black; text-align: center;" runat="server">TimeSheet entry
                                                </td>
                                            </tr>
                                            <tr>
                                                <td id="tdSubmitted" style="width: 100%; height: 35px; text-align: center; vertical-align: middle;" runat="server">
                                                    <asp:Label ID="lblTimeSheetErrorMessage" runat="server" Text="" ForeColor="Red" Font-Bold="True" Font-Size="Larger"></asp:Label>
                                                </td>
                                            </tr>
                                            <asp:Panel ID="Rowrejectreason" runat="server" Style="display: none">
                                                <tr>
                                                    <td id="tdReject" style="width: 100%; height: 35px; text-align: center; vertical-align: middle;" runat="server">Enter Reject Reason if any :
                                                    <asp:TextBox ID="txtReject" runat="server"></asp:TextBox>
                                                        <asp:HiddenField ID="hndEmpno" runat="server" />
                                                    </td>
                                                </tr>
                                            </asp:Panel>
                                            <asp:Panel ID="Employeerow" runat="server" Style="display: block">
                                                <tr>
                                                    <td valign="top">Employee Name:
                                                        <asp:Label ID="lblEmployee" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                            </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:GridView ID="gridWeeklyTimesheets" CellPadding="5" CellSpacing="5" runat="server" OnInit="gridWeeklyTimesheets_Init" AutoGenerateColumns="False"></asp:GridView>
                                    </td>
                                </tr>
                            <tr>
                                <td style="width: 100%" colspan="2">
                                    <asp:Panel ID="pnlTimeSheetEntry" Width="100%" runat="server">
                                        <table width="40%">
                                            <tr>
                                                <td width="25px">
                                                    <asp:Button ID="btnSave" ValidationGroup="Save" runat="server" CssClass="savebutton1231"
                                                        EnableTheming="False" SkinID="skinBtnSave" OnClick="btnSave_Click" AccessKey="s" /></td>
                                                <td width="25px">
                                                    <asp:Button ID="btnCancel" runat="server" CssClass="cancelbutton6" EnableTheming="False" SkinID="skinBtnCancel" OnClick="btnCancel_Click" /></td>
                                                <td width="25px">
                                                    <asp:Button ID="btnSubmit" runat="server" ValidationGroup="Save" EnableTheming="False" CssClass="AddGetRefNos6"
                                                        OnClick="btnSubmit_Click" AccessKey="S" /></td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <%-- <asp:Button ID="btnUpdate" runat="server" ValidationGroup="Save"
                                                EnableTheming="False" SkinID="skinBtnSave" OnClick="btnUpdate_Click" Enabled="False" AccessKey="s" />--%>

                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" width="100%">
                                    <asp:Panel ID="pnlTimeSheetApproval" Width="100%" runat="server">
                                        <table width="40%">
                                            <tr>
                                                <td width="25px">
                                                    <asp:Button ID="btnApprove" runat="server" Text="Approve" Width="120px"
                                                        OnClick="btnApprove_Click" AccessKey="a" /></td>
                                                <td width="25px">
                                                    <asp:Button ID="btnReject" runat="server" Text="Reject" Width="120px"
                                                        OnClick="btnReject_Click" AccessKey="r" /></td>
                                                <td width="25px"></td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
            </div>
            <div style="visibility: hidden">
                <asp:Button ID="tabClickButton" runat="server" OnClick="tbMain_ActiveTabChanged" />
            </div>
            <table width="100%">
                <tr>
                    <td align="center">
                         <asp:Panel ID="pnlSearch" runat="server" Width="100px">
                                                                <asp:Button ID="lnkBtnAdd" runat="server" OnClick="lnkBtnAdd_Click" CssClass="ButtonAdd66"
                                                                    EnableTheming="False" Width="80px"></asp:Button>
                                                            </asp:Panel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
