<%@ Page Title="" Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true" CodeFile="EmployeeAttendance.aspx.cs" Inherits="Attendance_EmployeeAttendance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
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
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table style="width: 100%">
                <tr style="width: 100%">
                    <td style="width: 100%">
                        <div class="mainConBody">
                            <table style="width: 100%; margin: -1px 0px 0px 1px;" cellpadding="3" cellspacing="2" class="searchbg">
                                <tr style="height: 25px; vertical-align: middle">
                                    <td style="width: 2%"></td>
                                    <td style="width: 16%; font-size: 22px; color: #000000;">Attendance
                                    </td>
                                    <td style="width: 14%">
                                        <div style="text-align: right;">
                                            <asp:Panel ID="pnlSearch" runat="server" Width="100px">
                                                <asp:Button ID="lnkBtnAddAttendance" runat="server" CssClass="ButtonAdd66"
                                                    EnableTheming="false" Width="80px" Text="" OnClick="lnkBtnAddAttendance_Click"></asp:Button>
                                            </asp:Panel>
                                        </div>
                                    </td>
                                    <td style="width: 13%; color: #000080;" align="right"></td>
                                    <td style="width: 20%; color: #000080;" align="right">Filter by year:
                                        <asp:TextBox ID="txtSearchInput" runat="server" SkinID="skinTxtBoxSearch" Visible="false"></asp:TextBox>
                                    </td>
                                    <td style="width: 20%">
                                        <div style="width: 160px; font-family: 'Trebuchet MS';">
                                            <asp:DropDownList ID="ddlSearchCriteria" runat="server" Width="154px" BackColor="#BBCAFB" Height="23px"
                                                Style="text-align: center; border: 1px solid #BBCAFB" Visible="true" DataTextField="AttendanceYear" DataValueField="AttendanceYear"
                                                OnSelectedIndexChanged="ddlSearchCriteria_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="All" Text="All" Selected="True"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                    <td style="width: 17%" class="tblLeftNoPad">
                                        <asp:Button ID="btnFilterAttendance" runat="server" Text="" CssClass="ButtonSearch6" EnableTheming="false" ForeColor="White" Visible="false" />
                                    </td>

                                    <td style="width: 20%" class="tblLeftNoPad">
                                        <asp:Button ID="btnClearFilter" runat="server" EnableTheming="false" Text="" CssClass="ClearFilter6" Visible="false" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr style="width: 100%">
                    <td style="width: 100%">
                        <table width="100%" style="margin: -3px 0px 0px 0px;">
                            <tr style="width: 100%">
                                <td>
                                    <div class="mainGridHold" id="searchGrid">
                                        <asp:GridView ID="grdViewAttendanceSummary" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                            Width="99.9%" AllowPaging="True" DataKeyNames="AttendanceId"
                                            EmptyDataText="No Attendance Data Found." OnRowCommand="grdViewAttendanceSummary_RowCommand" Font-Names="Trebuchet MS" CssClass="someClass">
                                            <Columns>
                                                <asp:BoundField DataField="AttendanceID" HeaderText="AttendanceID" Visible="false" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="AttendanceYear" HeaderText="Year" Visible="false" />
                                                <asp:BoundField DataField="AttendanceMonthId" HeaderText="Month" Visible="false" />
                                                <asp:BoundField DataField="Period" HeaderText="Month" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="DateSubmitted" HeaderText="Date submitted" HeaderStyle-BorderColor="Gray" NullDisplayText="NA" />
                                                <asp:BoundField DataField="Approver" HeaderText="Approver" HeaderStyle-BorderColor="Gray" />
                                                <asp:TemplateField ItemStyle-CssClass="command" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderColor="Gray"
                                                    HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="EditRecord" CommandArgument='<%#Eval("AttendanceID").ToString()+":"+Eval("AttendanceYear").ToString()+":"+Eval("AttendanceMonthId").ToString()+":"+Eval("Status").ToString()%> ' />
                                                        <asp:ImageButton ID="btnEditDisabled" Enabled="false" SkinID="editDisable" runat="Server"></asp:ImageButton>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="command" Width="50px"></ItemStyle>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerTemplate>
                                                <table style="border-color: white">
                                                    <tr style="border-color: white">
                                                        <td style="border-color: white">Goto Page
                                                        </td>
                                                        <td style="border-color: white">
                                                            <asp:DropDownList ID="ddlPageSelector" runat="server" AutoPostBack="true" Style="border: 1px solid blue" BackColor="#BBCAFB" Width="75px" Height="25px">
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
                        <asp:ObjectDataSource ID="AttendanceSummaryGridSource" runat="server" SelectMethod="GetAttendanceSummary"
                            TypeName="BusinessLogic"></asp:ObjectDataSource>
                    </td>
                </tr>
                <tr>
                    <td>
                        <input id="FakeCancelBtn" type="button" style="display: none" runat="server" />
                        <input id="FakeTargetBtn" type="button" style="display: none" runat="server" />
                        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                            CancelControlID="FakeCancelBtn" Enabled="True" PopupControlID="AttendanceDetailPopUp"
                            TargetControlID="FakeTargetBtn" X="100" Y="100">
                        </cc1:ModalPopupExtender>
                        <asp:Panel runat="server" ID="AttendanceDetailPopUp" Style="width: 85%">
                            <div id="contentPopUp" class="divBody">
                                <table style="width: 100%;" align="center">
                                    <tr>
                                        <td class="headerPopUp">Mark Attendance
                                        </td>
                                    </tr>
                                    <tr style="width: 100%">
                                        <td style="width: 100%">
                                            <div id="divGridContainer" style="overflow: scroll; width: 100%">
                                                <table width="100%" style="margin: 5px;">
                                                    <tr style="width: 100%; height: 25px;">
                                                        <td colspan="5" style="text-align: left;">
                                                            <asp:Label ID="lblStatus" runat="server" Text="" Font-Bold="true" ForeColor="Red" Font-Size="Large">

                                                            </asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="width: 100%; height: 25px;">
                                                        <td class="btnBts-dark">Colors Legend:
                                                        </td>
                                                        <td class="btnBts-default">Present
                                                        </td>
                                                        <td class="btnBts-warning">Leave
                                                        </td>
                                                        <td class="btnBts-info">Holiday
                                                        </td>
                                                        <td class="btnBts-success">Week Off
                                                        </td>
                                                    </tr>
                                                    <tr style="width: 100%">
                                                        <td colspan="5">
                                                            <div class="mainGridHold" id="attendanceDetailGrid">
                                                                <asp:HiddenField ID="hdnfIsNewAttendance" runat="server" Value="" />
                                                                <asp:HiddenField ID="hdnfIsGridLoaded" runat="server" Value="" />
                                                                <asp:HiddenField ID="hdnfAttendanceID" runat="server" Value="" />
                                                                <asp:HiddenField ID="hdnfAttendanceYear" runat="server" Value="" />
                                                                <asp:HiddenField ID="hdnfAttendanceMonth" runat="server" Value="" />
                                                                <asp:UpdatePanel runat="server" ID="updPnlAttendanceDeailsGrid" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <asp:GridView ID="GridViewAttendanceDetail" runat="server"
                                                                            Width="99.9%" AllowPaging="false" EmptyDataText="No Reportees Found." AutoGenerateColumns="false"
                                                                            DataKeyNames="EmployeeNo"
                                                                            Font-Names="Trebuchet MS" Visible="false" CssClass="someClass" OnRowDataBound="GridViewAttendanceDetail_RowDataBound">
                                                                            <Columns>
                                                                                <asp:BoundField HeaderText="EmployeeNo" AccessibleHeaderText="EmployeeNo" DataField="EmployeeNo" ReadOnly="true" Visible="false" />
                                                                                <asp:BoundField HeaderText="Employee" AccessibleHeaderText="Employee" DataField="Employee" ReadOnly="true" />
                                                                                <asp:TemplateField ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                                                                                    HeaderText="Day1" AccessibleHeaderText="Day1">
                                                                                    <ItemTemplate>
                                                                                        <asp:Button runat="server" CssClass="btnBts btnBts-default" CommandArgument='<%#Eval("EmployeeNo").ToString()+"//"+Eval("Employee").ToString()+"//"+ Eval("Day1")+"//1"%>' ID="Button1" OnClick="ToggleAttendance_Click" Text='<%#Eval("Day1") %>' UseSubmitBehavior="false" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                                                                                    HeaderText="Day2" AccessibleHeaderText="Day2">
                                                                                    <ItemTemplate>
                                                                                        <asp:Button runat="server" CssClass="btnBts btnBts-default" CommandArgument='<%#Eval("EmployeeNo").ToString()+"//"+Eval("Employee").ToString()+"//"+ Eval("Day2")+"//2" %>' ID="Button2" OnClick="ToggleAttendance_Click" Text='<%#Eval("Day2") %>' UseSubmitBehavior="false" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                                                                                    HeaderText="Day3" AccessibleHeaderText="Day3">
                                                                                    <ItemTemplate>
                                                                                        <asp:Button runat="server" CssClass="btnBts btnBts-default" CommandArgument='<%#Eval("EmployeeNo").ToString()+"//"+Eval("Employee").ToString()+"//"+ Eval("Day3")+"//3" %>' ID="Button3" OnClick="ToggleAttendance_Click" Text='<%#Eval("Day3") %>' UseSubmitBehavior="false" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                                                                                    HeaderText="Day4" AccessibleHeaderText="Day4">
                                                                                    <ItemTemplate>
                                                                                        <asp:Button runat="server" CssClass="btnBts btnBts-default" CommandArgument='<%#Eval("EmployeeNo").ToString()+"//"+Eval("Employee").ToString()+"//"+ Eval("Day4")+"//4" %>' ID="Button4" OnClick="ToggleAttendance_Click" Text='<%#Eval("Day4") %>' UseSubmitBehavior="false" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                                                                                    HeaderText="Day5" AccessibleHeaderText="Day5">
                                                                                    <ItemTemplate>
                                                                                        <asp:Button runat="server" CssClass="btnBts btnBts-default" CommandArgument='<%#Eval("EmployeeNo").ToString()+"//"+Eval("Employee").ToString()+"//"+ Eval("Day5")+"//5" %>' ID="Button5" OnClick="ToggleAttendance_Click" Text='<%#Eval("Day5") %>' UseSubmitBehavior="false" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                                                                                    HeaderText="Day6" AccessibleHeaderText="Day6">
                                                                                    <ItemTemplate>
                                                                                        <asp:Button runat="server" CssClass="btnBts btnBts-default" CommandArgument='<%#Eval("EmployeeNo").ToString()+"//"+Eval("Employee").ToString()+"//"+ Eval("Day6")+"//6" %>' ID="Button6" OnClick="ToggleAttendance_Click" Text='<%#Eval("Day6") %>' UseSubmitBehavior="false" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                                                                                    HeaderText="Day7" AccessibleHeaderText="Day7">
                                                                                    <ItemTemplate>
                                                                                        <asp:Button runat="server" CssClass="btnBts btnBts-default" CommandArgument='<%#Eval("EmployeeNo").ToString()+"//"+Eval("Employee").ToString()+"//"+ Eval("Day7")+"//7" %>' ID="Button7" OnClick="ToggleAttendance_Click" Text='<%#Eval("Day7") %>' UseSubmitBehavior="false" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                                                                                    HeaderText="Day8" AccessibleHeaderText="Day8">
                                                                                    <ItemTemplate>
                                                                                        <asp:Button runat="server" CssClass="btnBts btnBts-default" CommandArgument='<%#Eval("EmployeeNo").ToString()+"//"+Eval("Employee").ToString()+"//"+ Eval("Day8")+"//8" %>' ID="Button8" OnClick="ToggleAttendance_Click" Text='<%#Eval("Day8") %>' UseSubmitBehavior="false" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                                                                                    HeaderText="Day9" AccessibleHeaderText="Day9">
                                                                                    <ItemTemplate>
                                                                                        <asp:Button runat="server" CssClass="btnBts btnBts-default" CommandArgument='<%#Eval("EmployeeNo").ToString()+"//"+Eval("Employee").ToString()+"//"+ Eval("Day9")+"//9" %>' ID="Button9" OnClick="ToggleAttendance_Click" Text='<%#Eval("Day9") %>' UseSubmitBehavior="false" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                                                                                    HeaderText="Day10" AccessibleHeaderText="Day10">
                                                                                    <ItemTemplate>
                                                                                        <asp:Button runat="server" CssClass="btnBts btnBts-default" CommandArgument='<%#Eval("EmployeeNo").ToString()+"//"+Eval("Employee").ToString()+"//"+ Eval("Day10")+"//10" %>' ID="Button10" OnClick="ToggleAttendance_Click" Text='<%#Eval("Day10") %>' UseSubmitBehavior="false" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                                                                                    HeaderText="Day11" AccessibleHeaderText="Day11">
                                                                                    <ItemTemplate>
                                                                                        <asp:Button runat="server" CssClass="btnBts btnBts-default" CommandArgument='<%#Eval("EmployeeNo").ToString()+"//"+Eval("Employee").ToString()+"//"+ Eval("Day11")+"//11" %>' ID="Button11" OnClick="ToggleAttendance_Click" Text='<%#Eval("Day11") %>' UseSubmitBehavior="false" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                                                                                    HeaderText="Day12" AccessibleHeaderText="Day12">
                                                                                    <ItemTemplate>
                                                                                        <asp:Button runat="server" CssClass="btnBts btnBts-default" CommandArgument='<%#Eval("EmployeeNo").ToString()+"//"+Eval("Employee").ToString()+"//"+ Eval("Day12")+"//12" %>' ID="Button12" OnClick="ToggleAttendance_Click" Text='<%#Eval("Day12") %>' UseSubmitBehavior="false" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                                                                                    HeaderText="Day13" AccessibleHeaderText="Day13">
                                                                                    <ItemTemplate>
                                                                                        <asp:Button runat="server" CssClass="btnBts btnBts-default" CommandArgument='<%#Eval("EmployeeNo").ToString()+"//"+Eval("Employee").ToString()+"//"+ Eval("Day13")+"//13" %>' ID="Button13" OnClick="ToggleAttendance_Click" Text='<%#Eval("Day13") %>' UseSubmitBehavior="false" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                                                                                    HeaderText="Day14" AccessibleHeaderText="Day14">
                                                                                    <ItemTemplate>
                                                                                        <asp:Button runat="server" CssClass="btnBts btnBts-default" CommandArgument='<%#Eval("EmployeeNo").ToString()+"//"+Eval("Employee").ToString()+"//"+ Eval("Day14")+"//14" %>' ID="Button14" OnClick="ToggleAttendance_Click" Text='<%#Eval("Day14") %>' UseSubmitBehavior="false" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                                                                                    HeaderText="Day15" AccessibleHeaderText="Day15">
                                                                                    <ItemTemplate>
                                                                                        <asp:Button runat="server" CssClass="btnBts btnBts-default" CommandArgument='<%#Eval("EmployeeNo").ToString()+"//"+Eval("Employee").ToString()+"//"+ Eval("Day15")+"//15" %>' ID="Button15" OnClick="ToggleAttendance_Click" Text='<%#Eval("Day15") %>' UseSubmitBehavior="false" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                                                                                    HeaderText="Day16" AccessibleHeaderText="Day16">
                                                                                    <ItemTemplate>
                                                                                        <asp:Button runat="server" CssClass="btnBts btnBts-default" CommandArgument='<%#Eval("EmployeeNo").ToString()+"//"+Eval("Employee").ToString()+"//"+ Eval("Day16")+"//16" %>' ID="Button16" OnClick="ToggleAttendance_Click" Text='<%#Eval("Day16") %>' UseSubmitBehavior="false" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                                                                                    HeaderText="Day17" AccessibleHeaderText="Day17">
                                                                                    <ItemTemplate>
                                                                                        <asp:Button runat="server" CssClass="btnBts btnBts-default" CommandArgument='<%#Eval("EmployeeNo").ToString()+"//"+Eval("Employee").ToString()+"//"+ Eval("Day17")+"//17" %>' ID="Button17" OnClick="ToggleAttendance_Click" Text='<%#Eval("Day17") %>' UseSubmitBehavior="false" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                                                                                    HeaderText="Day18" AccessibleHeaderText="Day18">
                                                                                    <ItemTemplate>
                                                                                        <asp:Button runat="server" CssClass="btnBts btnBts-default" CommandArgument='<%#Eval("EmployeeNo").ToString()+"//"+Eval("Employee").ToString()+"//"+ Eval("Day18")+"//18" %>' ID="Button18" OnClick="ToggleAttendance_Click" Text='<%#Eval("Day18") %>' UseSubmitBehavior="false" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                                                                                    HeaderText="Day19" AccessibleHeaderText="Day19">
                                                                                    <ItemTemplate>
                                                                                        <asp:Button runat="server" CssClass="btnBts btnBts-default" CommandArgument='<%#Eval("EmployeeNo").ToString()+"//"+Eval("Employee").ToString()+"//"+ Eval("Day19")+"//19" %>' ID="Button19" OnClick="ToggleAttendance_Click" Text='<%#Eval("Day19") %>' UseSubmitBehavior="false" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                                                                                    HeaderText="Day20" AccessibleHeaderText="Day20">
                                                                                    <ItemTemplate>
                                                                                        <asp:Button runat="server" CssClass="btnBts btnBts-default" CommandArgument='<%#Eval("EmployeeNo").ToString()+"//"+Eval("Employee").ToString()+"//"+ Eval("Day20")+"//20" %>' ID="Button20" OnClick="ToggleAttendance_Click" Text='<%#Eval("Day20") %>' UseSubmitBehavior="false" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                                                                                    HeaderText="Day21" AccessibleHeaderText="Day21">
                                                                                    <ItemTemplate>
                                                                                        <asp:Button runat="server" CssClass="btnBts btnBts-default" CommandArgument='<%#Eval("EmployeeNo").ToString()+"//"+Eval("Employee").ToString()+"//"+ Eval("Day21")+"//21" %>' ID="Button21" OnClick="ToggleAttendance_Click" Text='<%#Eval("Day21") %>' UseSubmitBehavior="false" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                                                                                    HeaderText="Day22" AccessibleHeaderText="Day22">
                                                                                    <ItemTemplate>
                                                                                        <asp:Button runat="server" CssClass="btnBts btnBts-default" CommandArgument='<%#Eval("EmployeeNo").ToString()+"//"+Eval("Employee").ToString()+"//"+ Eval("Day22")+"//22" %>' ID="Button22" OnClick="ToggleAttendance_Click" Text='<%#Eval("Day22") %>' UseSubmitBehavior="false" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                                                                                    HeaderText="Day23" AccessibleHeaderText="Day23">
                                                                                    <ItemTemplate>
                                                                                        <asp:Button runat="server" CssClass="btnBts btnBts-default" CommandArgument='<%#Eval("EmployeeNo").ToString()+"//"+Eval("Employee").ToString()+"//"+ Eval("Day23")+"//23" %>' ID="Button23" OnClick="ToggleAttendance_Click" Text='<%#Eval("Day23") %>' UseSubmitBehavior="false" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                                                                                    HeaderText="Day24" AccessibleHeaderText="Day24">
                                                                                    <ItemTemplate>
                                                                                        <asp:Button runat="server" CssClass="btnBts btnBts-default" CommandArgument='<%#Eval("EmployeeNo").ToString()+"//"+Eval("Employee").ToString()+"//"+ Eval("Day24")+"//24" %>' ID="Button24" OnClick="ToggleAttendance_Click" Text='<%#Eval("Day24") %>' UseSubmitBehavior="false" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                                                                                    HeaderText="Day25" AccessibleHeaderText="Day25">
                                                                                    <ItemTemplate>
                                                                                        <asp:Button runat="server" CssClass="btnBts btnBts-default" CommandArgument='<%#Eval("EmployeeNo").ToString()+"//"+Eval("Employee").ToString()+"//"+ Eval("Day25")+"//25" %>' ID="Button25" OnClick="ToggleAttendance_Click" Text='<%#Eval("Day25") %>' UseSubmitBehavior="false" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                                                                                    HeaderText="Day26" AccessibleHeaderText="Day26">
                                                                                    <ItemTemplate>
                                                                                        <asp:Button runat="server" CssClass="btnBts btnBts-default" CommandArgument='<%#Eval("EmployeeNo").ToString()+"//"+Eval("Employee").ToString()+"//"+ Eval("Day26")+"//26" %>' ID="Button26" OnClick="ToggleAttendance_Click" Text='<%#Eval("Day26") %>' UseSubmitBehavior="false" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                                                                                    HeaderText="Day27" AccessibleHeaderText="Day27">
                                                                                    <ItemTemplate>
                                                                                        <asp:Button runat="server" CssClass="btnBts btnBts-default" CommandArgument='<%#Eval("EmployeeNo").ToString()+"//"+Eval("Employee").ToString()+"//"+ Eval("Day27")+"//27" %>' ID="Button27" OnClick="ToggleAttendance_Click" Text='<%#Eval("Day27") %>' UseSubmitBehavior="false" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                                                                                    HeaderText="Day28" AccessibleHeaderText="Day28">
                                                                                    <ItemTemplate>
                                                                                        <asp:Button runat="server" CssClass="btnBts btnBts-default" CommandArgument='<%#Eval("EmployeeNo").ToString()+"//"+Eval("Employee").ToString()+"//"+ Eval("Day28")+"//28" %>' ID="Button28" OnClick="ToggleAttendance_Click" Text='<%#Eval("Day28") %>' UseSubmitBehavior="false" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                                                                                    HeaderText="Day29" AccessibleHeaderText="Day29">
                                                                                    <ItemTemplate>
                                                                                        <asp:Button runat="server" CssClass="btnBts btnBts-default" CommandArgument='<%#Eval("EmployeeNo").ToString()+"//"+Eval("Employee").ToString()+"//"+ Eval("Day29")+"//29" %>' ID="Button29" OnClick="ToggleAttendance_Click" Text='<%#Eval("Day29") %>' UseSubmitBehavior="false" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                                                                                    HeaderText="Day30" AccessibleHeaderText="Day30">
                                                                                    <ItemTemplate>
                                                                                        <asp:Button runat="server" CssClass="btnBts btnBts-default" CommandArgument='<%#Eval("EmployeeNo").ToString()+"//"+Eval("Employee").ToString()+"//"+ Eval("Day30")+"//30" %>' ID="Button30" OnClick="ToggleAttendance_Click" Text='<%#Eval("Day30") %>' UseSubmitBehavior="false" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                                                                                    HeaderText="Day31" AccessibleHeaderText="Day31">
                                                                                    <ItemTemplate>
                                                                                        <asp:Button runat="server" CssClass="btnBts btnBts-default" CommandArgument='<%#Eval("EmployeeNo").ToString()+"//"+Eval("Employee").ToString()+"//"+ Eval("Day31")+"//31" %>' ID="Button31" OnClick="ToggleAttendance_Click" Text='<%#Eval("Day31") %>' UseSubmitBehavior="false" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblEmployeeNo" runat="server" Text='<%#Eval("EmployeeNo")%>' Visible="false"></asp:Label>
                                                                                    </ItemTemplate>

                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left; height: 25px; color: red;" colspan="5">
                                                            <asp:Label ID="lblUserHint" runat="server" Text="Click the cell to toggle the attendance entry." ForeColor="Red">

                                                            </asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table width="100%" align="left">
                                                <tr>
                                                    <td align="right" style="width: 20%;"></td>
                                                    <td style="width: 20%;">
                                                        <asp:Button ID="btnCancelPopup" runat="server" CausesValidation="False"
                                                            CssClass="cancelbutton6" EnableTheming="false" SkinID="skinBtnCancel"></asp:Button>
                                                    </td>
                                                    <td style="width: 20%;">
                                                        <asp:Button ID="btnSaveAttendance" runat="server" CausesValidation="True"
                                                            CssClass="savebutton1231" EnableTheming="false" SkinID="skinBtnSave" OnClick="btnSaveAttendance_Click"></asp:Button>
                                                    </td>
                                                    <td style="width: 20%;">
                                                        <asp:Button ID="btnSubmitAttendance" runat="server" Visible="true" CausesValidation="True"
                                                            CssClass="AddGetRefNos6" EnableTheming="false" SkinID="skinBtnSave" OnClick="btnSubmitAttendance_Click"></asp:Button>
                                                    </td>
                                                    <td style="width: 20%;"></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <input id="btnAutoApplyLeavePopupTarget" type="button" style="display: none" runat="server" />
                                            <cc1:ModalPopupExtender ID="ModalPopupAutoApplyLeave" runat="server" PopupControlID="AutoApplyLeavePopUp"
                                                BackgroundCssClass="modalBackground" TargetControlID="btnAutoApplyLeavePopupTarget" CancelControlID="btnCancelAutoApplyLeave">
                                            </cc1:ModalPopupExtender>
                                            <asp:Panel runat="server" ID="AutoApplyLeavePopUp">
                                                <div id="AutoApplyLeavePopUpContent" class="divBody" style="border-color: green; border-width: 2px; background-color: silver">

                                                    <table style="width: 770px; vertical-align: text-top; border: 1px solid #FFFFFF;"
                                                        align="center" cellspacing="2" cellpadding="3">
                                                        <tr>
                                                            <td class="headerPopUp">Auto Apply Leave</td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div id="divMsgContainer" style="overflow: scroll; width: 100%; height:400px;">
                                                                    <asp:Label ID="lblAutoApplyLeaveMsg" runat="server" Text="" Width="100%" Height="100%"></asp:Label>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table width="100%" align="left">
                                                                    <tr>
                                                                        <td align="right" style="width: 20%;"></td>
                                                                        <td style="width: 20%;">
                                                                            
                                                                        </td>
                                                                        <td style="width: 20%;">
                                                                            <asp:Button ID="btnCancelAutoApplyLeave" runat="server" CausesValidation="False"
                                                                                EnableTheming="false" Text="Ok"></asp:Button>
                                                                        </td>
                                                                        <td style="width: 20%;">
                                                                            
                                                                        </td>
                                                                        <td style="width: 20%;"></td>
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
                <tr>
                    <td>
                        <input id="btnFakeModalTarget" type="button" style="display: none" runat="server" />
                        <cc1:ModalPopupExtender ID="CompOffModalPopupExtender" runat="server" PopupControlID="pnlCompOffContentPopUp"
                            BackgroundCssClass="modalBackground" TargetControlID="btnFakeModalTarget" CancelControlID="btnCancelCompOff">
                        </cc1:ModalPopupExtender>
                        <asp:Panel runat="server" ID="pnlCompOffContentPopUp">
                            <div id="CompOffContentPopUp" class="divBody" style="border-color: green; border-width: 2px; background-color: silver">

                                <table style="width: 770px; vertical-align: text-top; border: 0px solid #86b2d1;"
                                    align="center" cellspacing="2" cellpadding="3">
                                    <tr>
                                        <td class="headerPopUp">Rota/Comp-Off Approval
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:UpdatePanel ID="updPnlRotaCompOff" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:Panel runat="server" ID="optionsPanel">
                                                        <asp:RadioButton ID="rbtnRota" runat="server" GroupName="options" Checked="true" Text="Rota" ToolTip="Provide Rota Week-Off" AutoPostBack="true" CausesValidation="false" OnCheckedChanged="rbtnRotaCompOff_CheckedChanged" />
                                                        <asp:RadioButton ID="rbtnCompOff" runat="server" GroupName="options" Checked="false" Text="Comp-Off" ToolTip="Provide Comp-Off" AutoPostBack="true" CausesValidation="false" OnCheckedChanged="rbtnRotaCompOff_CheckedChanged" />
                                                        <asp:HiddenField ID="hdnfRequestDayInfo" runat="server" Value="" />
                                                        <asp:HiddenField ID="hdnfRequestSenderId" runat="server" Value="" />
                                                        <asp:HiddenField ID="hdnfCompOffRotaEmployeeNo" runat="server" Value="" />
                                                    </asp:Panel>
                                                    <asp:Panel ID="controlsPanel" runat="server">
                                                        <div id="divRotaContaiiner" style="width: 100%;" runat="server" visible="true">
                                                            <table width="100%" style="margin: 5px;">
                                                                <tr style="width: 100%; height: 25px;">
                                                                    <td>Please choose the alternative week-off day for the employee
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlRotaAlternativeDays" runat="server" Style="border: 1px solid blue" BackColor="#BBCAFB" Width="175px" Height="25px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <div id="divCompOffContainer" style="width: 100%;" runat="server" visible="false">
                                                            <table width="100%" style="margin: 5px;">
                                                                <tr style="width: 100%; height: 25px;">
                                                                    <td>Please enter the reason for providing the Compensatory Off
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="txtCompOffReason" runat="server"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </asp:Panel>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr style="width: 100%">
                                        <td style="width: 100%"></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>--%>
                                            <table width="100%" align="left">
                                                <tr>
                                                    <td align="right" style="width: 20%;"></td>
                                                    <td style="width: 20%;">
                                                        <asp:Button ID="btnCancelCompOff" runat="server" CausesValidation="False"
                                                            Text="Cancel" EnableTheming="false" OnClick="btnCancelCompOff_Click"></asp:Button>
                                                    </td>
                                                    <td style="width: 20%;">
                                                        <asp:Button ID="btnApproveCompOff" runat="server" CausesValidation="True"
                                                            Text="Approve & Save" ToolTip="Approve and save attendance." EnableTheming="false" OnClick="btnApproveCompOff_Click"></asp:Button>
                                                    </td>
                                                    <td style="width: 20%;" colspan="2"></td>
                                                </tr>
                                            </table>
                                            <%--</ContentTemplate>
                                            </asp:UpdatePanel>--%>
                                        </td>
                                    </tr>
                                </table>

                            </div>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

