<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmployeePayroll.aspx.cs" Inherits="EmployeePayroll" MasterPageFile="~/PageMaster.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
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
                                                AutoPostBack="true" OnSelectedIndexChanged="ddl_SelectedIndexChanged">
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
                                                        AutoPostBack="true" OnSelectedIndexChanged="ddl_SelectedIndexChanged">
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
                                                    <asp:Button ID="btnQueuePayroll" runat="server" Text="Initiate Payroll" Enabled="false" EnableTheming="false" OnClick="btnQueuePayroll_Click" />
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
                                                <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" Visible="true" />
                                                <asp:BoundField DataField="Role" HeaderText="Role" Visible="true" />
                                                <asp:BoundField DataField="DateOfJoining" HeaderText="Date of Joining" Visible="true" />
                                                <asp:BoundField DataField="PayrollDate" HeaderText="PayrollDate" Visible="false" />
                                                <asp:BoundField DataField="Payments" HeaderText="Payments" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="Deductions" HeaderText="Deductions" HeaderStyle-BorderColor="Gray" NullDisplayText="NA" />
                                                <asp:BoundField DataField="OtherAllowance" HeaderText="Other Allowance" HeaderStyle-BorderColor="Gray" NullDisplayText="NA" />
                                                <asp:BoundField DataField="OtherDeductions" HeaderText="Other Deductions" HeaderStyle-BorderColor="Gray" NullDisplayText="NA" />
                                                <asp:BoundField DataField="LossOfPayDays" HeaderText="LOP Days" HeaderStyle-BorderColor="Gray" NullDisplayText="NA" />
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
                            <tr style="width: 100%">
                                <td>
                                    <br></br>    
                                </td>
                            </tr>
                            <tr style="width: 100%" align="center">
                                <td>
                                    <asp:Button ID="btnExportToExcel" runat="server" Text="Export To Excel" SkinID="skinBtnExcel" EnableTheming="false" OnClick="btnExportToExcel_Click" Visible="false" />
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
                            <div id="contentPopUp" class="divBody" style="border-color: green; border-width: 2px; background-color: silver; width: 100%">
                                <table style="vertical-align: text-top; border: 1px solid #FFFFFF;"
                                    align="center" cellspacing="2" cellpadding="3" width="100%">
                                    <tr>
                                        <td style="width: 100%">
                                            <div class="divArea" style="overflow-y: scroll">
                                                <table cellpadding="1" cellspacing="1" style="border: 1px solid #86b2d1; width: 100%;">
                                                    <tr>
                                                        <td colspan="3" class="headerPopUp">Payroll Generation Log
                                                            
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <asp:GridView ID="grdViewPayrollLog" runat="server" AutoGenerateColumns="false">
                                                                <Columns>
                                                                    <asp:BoundField HeaderText="Employee Name" DataField="EmpFirstName" NullDisplayText="NA" ItemStyle-HorizontalAlign="Left" />
                                                                    <asp:BoundField HeaderText="Log Info" DataField="Message" NullDisplayText="NA" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="true" />
                                                                </Columns>
                                                            </asp:GridView>
                                                            <asp:TextBox ID="txtLogMessage" runat="server" ReadOnly="true" Text="" TextMode="MultiLine" Width="100%" Height="300px" Visible="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Button ID="btnClosePopup" runat="server" EnableTheming="false" UseSubmitBehavior="true" Text="Ok" Width="100px" />
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
                <tr>
                    <td style="width: 100%">
                        <input id="FakeTargetBtn2" type="button" style="display: none" runat="server" />

                        <cc1:ModalPopupExtender ID="ModalPopupPayrollGeneration" runat="server" BackgroundCssClass="modalBackground"
                            CancelControlID="btnClosePaycomponentPopup" Enabled="True" PopupControlID="pnlPayrollGenerationPopup"
                            TargetControlID="FakeTargetBtn2">
                        </cc1:ModalPopupExtender>
                        <asp:Panel runat="server" ID="pnlPayrollGenerationPopup" Visible="true">

                            <div id="PayrollGenerationContentPopUp" class="divBody" style="border-color: green; border-width: 2px; background-color: silver">

                                <table style="width: 700px; vertical-align: text-top; border: 1px solid #FFFFFF;"
                                    align="center" cellspacing="2" cellpadding="3">
                                    <tr>
                                        <td style="width: 100%">
                                            <div class="divArea" style="overflow-y: scroll">
                                                <table cellpadding="1" cellspacing="1" style="border: 1px solid white; width: 100%;">
                                                    <tr>
                                                        <td colspan="3" class="headerPopUp">Generate Payroll
                                                            
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" align="left">
                                                            <asp:Label ID="lblPayrollGenerationMsg" CssClass="lblBox" runat="server" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3">
                                                            <asp:UpdatePanel ID="updPnlPayrollGeneration" runat="server" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <table>
                                                                        <tr>
                                                                            <td style="width: 40%">
                                                                                <asp:CheckBox ID="chkBoxEnableFileUpload" AutoPostBack="true" runat="server" Text="Upload additional pay component" ToolTip="Enable or Disable Pay Component Upload Option" Checked="false"
                                                                                    OnCheckedChanged="chkBoxEnableFileUpload_CheckedChanged" />
                                                                            </td>
                                                                            <td style="width: 40%" align="left">
                                                                                <asp:FileUpload ID="fileUploadpayComponent" runat="server" Enabled="false" Width="100%" />
                                                                            </td>
                                                                            <td style="width: 20%">
                                                                                <asp:HiddenField ID="hdnfFileUploadStatus" runat="server" Value="" />

                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="3">
                                                                                <br></br>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 40%"></td>
                                                                            <td style="width: 40%; padding-right: 25px;" align="right">
                                                                                <asp:Button ID="btnClosePaycomponentPopup" runat="server" EnableTheming="false" UseSubmitBehavior="true" Text="Cancel" />
                                                                            </td>
                                                                            <td style="width: 20%; padding-left: 25px;" align="left">
                                                                                <asp:Button ID="btnInitiatePayroll" runat="server" EnableTheming="false" UseSubmitBehavior="true" Text="Generate Payroll" OnClick="btnInitiatePayroll_Click" />

                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:PostBackTrigger ControlID="btnInitiatePayroll" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                    </tr>

                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExportToExcel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
