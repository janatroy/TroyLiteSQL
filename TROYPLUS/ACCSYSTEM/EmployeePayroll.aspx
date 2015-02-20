<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmployeePayroll.aspx.cs" Inherits="EmployeePayroll" MasterPageFile="~/PageMaster.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table style="width: 100%">
                <tr style="width: 100%">
                    <td style="width: 100%">
                        <div class="mainConBody">
                            <table style="width: 100%; margin: -1px 0px 0px 1px;" cellpadding="3" cellspacing="2" class="searchbg">
                                <tr style="height: 25px; vertical-align: middle">
                                    <td style="width: 5%"></td>
                                    <td style="width: 20%; font-size: 22px; color: #000000;">Initiate Payroll
                                    </td>
                                    <td style="width: 60%;"></td>
                                    <td style="width: 15%;"></td>
                                </tr>
                                <tr style="height: 25px; vertical-align: middle">
                                    <td style="width: 5%"></td>
                                    <td style="width: 20%; color: black;">Choose payroll year:</td>
                                    <td style="width: 60%;">
                                        <div style="width: 160px; font-family: 'Trebuchet MS';">
                                            <asp:DropDownList ID="ddlYear" runat="server" Width="154px" BackColor="#BBCAFB" Height="23px"
                                                Style="text-align: center; border: 1px solid #BBCAFB" Visible="true" DataTextField="MonthName" DataValueField="MonthId"
                                                AutoPostBack="true">
                                                <asp:ListItem Text="2014" Value="2014" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="2015" Value="2015"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                    <td style="width: 15%;"></td>
                                </tr>

                                <tr style="height: 25px; vertical-align: middle">
                                    <td style="width: 5%"></td>
                                    <td style="width: 20%; color: black;">Choose payroll month:</td>
                                    <td style="width: 60%;" align="left">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:DropDownList ID="ddlMonth" runat="server" Width="154px" BackColor="#BBCAFB" Height="23px"
                                                        Style="text-align: center; border: 1px solid #BBCAFB" Visible="true" DataTextField="MonthName" DataValueField="MonthId"
                                                        AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="width: 10px;"></td>
                                                <td>
                                                    <asp:Button ID="btnSearchpayroll" runat="server" Text="Search Payroll" EnableTheming="false" OnClick="btnSearchpayroll_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 15%;" align="left"></td>
                                </tr>
                                <tr style="height: 25px; vertical-align: middle">
                                    <td style="width: 5%"></td>
                                    <td style="width: 20%"></td>
                                    <td style="width: 60%;">
                                        <div style="text-align: left;">
                                            <asp:Label ID="lblPayrollStatus" runat="server" Text="" ForeColor="Black"></asp:Label>
                                            <asp:HiddenField ID="hdfPayrollId" runat="server" Value="" />
                                        </div>
                                    </td>
                                    <td style="width: 15%;"></td>
                                </tr>

                                <tr style="height: 25px; vertical-align: middle">
                                    <td style="width: 5%"></td>
                                    <td style="width: 20%"></td>
                                    <td style="width: 60%;" align="left">
                                        <table>
                                            <tr>
                                                <td style="width: 155px;">
                                                    <asp:Button ID="btnQueuePayroll" runat="server" Text="Generate Payroll" Enabled="false" EnableTheming="false" OnClick="btnQueuePayroll_Click" />
                                                </td>
                                                <td style="width: 10px;"></td>

                                                <td>
                                                    <asp:Button ID="btnViewLog" runat="server" Text="View Log" Enabled="true" OnClick="btnViewLog_Click" Visible="false" EnableTheming="false" />
                                                    <asp:Button ID="btnViewPayslips" runat="server" Text="View Payslip" Enabled="false" EnableTheming="false" OnClick="btnViewPayslips_Click" />
                                                </td>
                                            </tr>
                                        </table>

                                    </td>
                                    <td style="width: 15%;"></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr style="width: 100%; height: 100%">
                    <td style="width: 100%">
                        <table width="100%" style="margin: -3px 0px 0px 0px;">
                            <tr style="width: 100%">
                                <td>
                                    <div class="mainGridHold" id="searchGrid">
                                        <asp:GridView ID="grdViewPaySlipInfo" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                            Width="99.9%" AllowPaging="true" DataKeyNames="PayslipId" OnPageIndexChanging="grdViewPaySlipInfo_PageIndexChanging"
                                            OnRowCreated="grdViewPaySlipInfo_RowCreated"
                                            EmptyDataText="No payslip associated with this payroll." Font-Names="Trebuchet MS" CssClass="someClass">
                                            <Columns>
                                                <asp:BoundField DataField="PayslipId" HeaderText="PayslipId" Visible="false" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="EmpFirstName" HeaderText="Employee Name" Visible="true" />
                                                <asp:BoundField DataField="EmpDesig" HeaderText="Designation" Visible="true" />
                                                <asp:BoundField DataField="EmpDOJ" HeaderText="Date of Joining" Visible="true" />
                                                <asp:BoundField DataField="PayrollDate" HeaderText="PayrollDate" Visible="false" />
                                                <asp:BoundField DataField="Payments" HeaderText="Payments" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="Deductions" HeaderText="Deductions" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="TotalPayable" HeaderText="Total Salary" HeaderStyle-BorderColor="Gray" NullDisplayText="NA" />
                                            </Columns>
                                            <PagerTemplate>
                                                <table style="border-color: white">
                                                    <tr style="border-color: white">
                                                        <td style="border-color: white">Goto Page
                                                        </td>
                                                        <td style="border-color: white">
                                                            <asp:DropDownList ID="ddlPageSelector" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSelector_SelectedIndexChanged" Style="border: 1px solid blue" BackColor="#BBCAFB" Width="75px" Height="25px">
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
                <tr style="width: 100%; height: 100%">
                    <td style="width: 100%">
                        <input id="FakeCancelBtn" type="button" style="display: none" runat="server" />
                        <input id="FakeTargetBtn" type="button" style="display: none" runat="server" />
                        <cc1:ModalPopupExtender ID="modelPopupLogViewer" runat="server" BackgroundCssClass="modalBackground"
                            CancelControlID="FakeCancelBtn" Enabled="True" PopupControlID="LogDetailPopUp"
                            TargetControlID="FakeTargetBtn">
                        </cc1:ModalPopupExtender>
                        <asp:Panel runat="server" ID="LogDetailPopUp" Style="width: 60%" Visible="true">
                            <div id="contentPopUp">
                                <table style="width: 100%;" align="center">
                                    <tr style="width: 100%">
                                        <td style="width: 100%">
                                            <div class="divArea" style="overflow-y:scroll">
                                                <table cellpadding="1" cellspacing="1" style="border: 1px solid #86b2d1; width: 100%;">
                                                    <tr>
                                                        <td colspan="3" class="headerPopUp">Payroll Generation Log
                                                            
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:GridView ID="grdViewPayrollLog" runat="server" AutoGenerateColumns="false">
                                                                <Columns>
                                                                    <asp:BoundField HeaderText="Employee Name" DataField="EmpFirstName" NullDisplayText="NA" ItemStyle-HorizontalAlign="Left" />
                                                                    <asp:BoundField HeaderText="Log Info" DataField="Message" NullDisplayText="NA" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="true" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Button ID="btnClosePopup" runat="server" EnableTheming="false" UseSubmitBehavior="true" Text="Ok" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
