<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="TimeSheetApproval.aspx.cs" Inherits="TimeSheetApproval" Title="Resource > Timesheet Approval" %>

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


    <asp:UpdatePanel ID="UpdatePanelPage" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        

            <table style="width: 100%">
                <tr style="width: 100%">
                    <td style="width: 100%">
                    
                                <%--<div class="mainConHd">
                                    <table cellspacing="0" cellpadding="0" border="0">
                                        <tr valign="middle">
                                            <td>
                                                <span>Resource Timesheet Approvals</span>
                                            </td>
                                        </tr>
                                    </table>
                                </div>--%>
                                <%--<table class="mainConHd" style="width: 994px;">
                                <tr valign="middle">
                                    <td style="font-size: 20px;">
                                        Resource Timesheet Approvals
                                    </td>
                                </tr>
                            </table>--%>
                                <div class="mainConBody">
                                    <table style="width: 99.8%; margin: -1px 0px 0px 1px;" cellpadding="3" cellspacing="2" class="searchbg">
                                        <tr style=" vertical-align: middle">
                                            
                                            <td style="width: 55%; font-size: 15px; color: white;" >
                                                Timesheet Approvals
                                            </td>
                                            <td style="width: 1%">
                                                <div style="text-align: right;">
                                                    <asp:Panel ID="Panel2" runat="server">
                                                        <asp:Button ID="lnkBtnAdd" runat="server" OnClick="lnkBtnAdd_Click" CssClass="ButtonAdd66"
                                                            EnableTheming="false" Width="50px" Text=""></asp:Button>
                                                    </asp:Panel>
                                                </div>
                                            </td>
                                            <td style="width: 19% ;color: white;" align="right">
                                                Emp No.
                                            </td>
                                            <td style="width: 5%" class="NewBox">
                                                <asp:TextBox ID="txtSEmpno" runat="server" SkinID="skinTxtBoxSearch"></asp:TextBox>
                                            </td>
                                            <td style="width: 25%; color: white;" align="right">
                                                Date Of TSE
                                            </td>
                                            <td style="width: 5%" class="NewBox">
                                                <asp:TextBox ID="txtSDate" runat="server" SkinID="skinTxtBoxSearch" MaxLength="10" />
                                                <%--<script type="text/javascript" language="JavaScript">
                                                    new tcal({ 'formname': 'aspnetForm', 'controlname': 'ctl00_cplhControlPanel_AccordionPane1_content_txtSDate' });
                                                </script>--%>

                                                

                                            </td>
                                            <td style="width: 3%">
                                            <script type="text/javascript" language="JavaScript">                                                new tcal({ 'formname': 'aspnetForm', 'controlname': GettxtBoxName('txtSDate') });</script>
                                            </td>
                                            <td style="width: 10%; text-align: left">
                                                <asp:Button ID="btnSearch" runat="server" Text="" OnClick="btnSearch_Click"
                                                    CssClass="ButtonSearch6" EnableTheming="false" />
                                                <asp:DropDownList ID="drpsApproved" runat="server" Width="54%" AppendDataBoundItems="True"
                                                    EnableTheming="False" Height="16px" Visible="False">
                                                    <asp:ListItem Value=""> -- All -- </asp:ListItem>
                                                    <asp:ListItem Value="NO">No</asp:ListItem>
                                                    <asp:ListItem Value="YES">Yes</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                             <td style="width: 10%" class="tblLeftNoPad">
                                            <asp:Button ID="BtnClearFilter" runat="server"  OnClick="BtnClearFilter_Click"  EnableTheming="false" Text="" CssClass="ClearFilter6" />
                                        </td>
                                        </tr>
                                    </table>
                                </div>
                            
                        <asp:HiddenField ID="hdTse" runat="server" Value="0" />
                        <asp:Panel ID="Panel1" runat="server">
                            
                        </asp:Panel>
                        <div align="left">
                            <asp:ValidationSummary ID="VSContact" runat="server" Font-Names="'Trebuchet MS'" Font-Size="12pt"
                                                HeaderText="Validation Messages" ShowMessageBox="true" ShowSummary="true" ValidationGroup="Save" />
                            <input id="dummy" type="button" style="display: none" runat="server" />
                            <input id="Button1" type="button" style="display: none" runat="server" />
                            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                                CancelControlID="Button1" DynamicServicePath="" Enabled="True" PopupControlID="popUp"
                                TargetControlID="dummy">
                            </cc1:ModalPopupExtender>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Panel runat="server" ID="popUp" Style="width: 62%; background-color: White;
                                        display: none">
                                        <div id="contentPopUp">
                                            <table class="tblLeft" cellpadding="0" cellspacing="0" style="border: 0px solid #5078B3;
                                                background-color: #fff; color: #000;" width="100%">
                                                <tr>
                                                    <td>
                                                        <div class="divArea">
                                                            <table class="tblLeft" cellpadding="0" cellspacing="3" style="border: 1px solid #5078B3;"
                                                                width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <table class="headerPopUp" style="border: 1px solid #86b2d1" width="100%">
                                                                            <tr>
                                                                                <td>
                                                                                    Time Sheet Details
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <cc1:TabContainer ID="tbMain" Visible="false" runat="server" Width="100%" CssClass="fancy fancy-green">
                                                                            <cc1:TabPanel ID="tabMaster" runat="server" HeaderText="Time Sheet Details">
                                                                                <ContentTemplate>
                                                                                    <div align="left">
                                                                                    <asp:Panel ID="pnsTime" runat="server">
                                                                                        <table style="width: 760px" cellpadding="3" cellspacing="2">
                                                                                            <tr id="pnsTse" runat="server" visible="False">
                                                                                                <td style="width: 20%" class="ControlLabel">
                                                                                                    
                                                                                                    Emp. No. *
                                                                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Text="*" ErrorMessage="Employee No. is mandatory" ControlToValidate="drpIncharge"
                                                                                                        ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                                                                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="drpIncharge" ErrorMessage="Employee is mandatory" Display="Dynamic"
                                                                                                            ValueToCompare="0" Operator="GreaterThan" Text="*" ValidationGroup="Save"></asp:CompareValidator>
                                                                                                </td>
                                                                                                <td style="width: 30%" class="ControlDrpBorder">
                                                                                                    <asp:DropDownList ID="drpIncharge" TabIndex="4" Enabled="True" AppendDataBoundItems="true" CssClass="drpDownListMedium"  BackColor = "#e7e7e7"
                                                                                                        runat="server" Width="100%" DataTextField="empFirstName" style="border: 1px solid #e7e7e7" height="26px"
                                                                                                        DataValueField="empno">
                                                                                                        <asp:ListItem Text="Select Executive" style="background-color: #e7e7e7" Value="0"></asp:ListItem>
                                                                                                    </asp:DropDownList>
                                                                                                </td>
                                                                                                <td style="width: 15%" class="ControlLabel">
                                                                                                    
                                                                                                    Date *
                                                                                                    <asp:RequiredFieldValidator ValidationGroup="Save" ID="RequiredFieldValidator2" runat="server" ErrorMessage="Date is mandatory"
                                                                                                        Text="*" ControlToValidate="txtDate"></asp:RequiredFieldValidator>
                                                                                                </td>
                                                                                                <td style="width: 30%" class="ControlTextBox3">
                                                                                                    <asp:TextBox ID="txtDate" Width="100px" MaxLength="10" runat="server" Height="24px" CssClass="cssTextBox" />
                                                                                                    <cc1:CalendarExtender ID="calExtender3" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                                                                        PopupButtonID="btnDate3" PopupPosition="BottomLeft" TargetControlID="txtDate">
                                                                                                                    </cc1:CalendarExtender>
                                                                                                </td>
                                                                                                <td align="left" style="width: 10%">
                                                                                                    <asp:ImageButton ID="btnDate3" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                                                        Width="20px" runat="server" />
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr style="height:45px">
                                                                                                <td style="width: 20%" class="ControlLabel">
                                                                                                    Before 8
                                                                                                </td>
                                                                                                <td style="width: 25%" class="ControlTextBox66">
                                                                                                    <asp:TextBox ID="txtBefore8" runat="server" Width="98%"  style="height:41px;overflow: hidden;padding: 0px;font-family: 'Trebuchet MS'; font-size: 13px; background-color:#e7e7e7"
                                                                                                        TextMode="MultiLine"></asp:TextBox>
                                                                                                </td>
                                                                                                <td style="width: 15%" class="ControlLabel">
                                                                                                    8 To 9
                                                                                                </td>
                                                                                                <td style="width: 25%" class="ControlTextBox66">
                                                                                                    <asp:TextBox ID="txt8to9" runat="server"  Width="98%"  style="height:41px;overflow: hidden;padding: 0px;font-family: 'Trebuchet MS'; font-size: 13px; background-color:#e7e7e7"
                                                                                                        TextMode="MultiLine"></asp:TextBox>
                                                                                                </td>
                                                                                                <td style="width:10%">
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr style="height:45px">
                                                                                                <td class="ControlLabel" style="width: 20%">
                                                                                                    9 To 10
                                                                                                </td>
                                                                                                <td style="width: 25%" class="ControlTextBox66">
                                                                                                    <asp:TextBox ID="txt9to10" runat="server"  Width="98%" style="height:41px;overflow: hidden;padding: 0px;font-family: 'Trebuchet MS'; font-size: 13px; background-color:#e7e7e7"
                                                                                                        TextMode="MultiLine"> </asp:TextBox>
                                                                                                </td>
                                                                                                <td class="ControlLabel" style="width: 15%">
                                                                                                    10 To 11
                                                                                                </td>
                                                                                                <td style="width: 25%" class="ControlTextBox66">
                                                                                                    <asp:TextBox ID="txt10to11" runat="server"  Width="98%"  style="height:41px;overflow: hidden;padding: 0px;font-family: 'Trebuchet MS'; font-size: 13px; background-color:#e7e7e7"
                                                                                                        TextMode="MultiLine"> </asp:TextBox>
                                                                                                </td>
                                                                                                <td style="width:10%">
                                                                                                </td>
                                                                                            </tr>
                                                                                            
                                                                                            <tr style="height:45px">
                                                                                                <td class="ControlLabel" style="width: 20%">
                                                                                                    11 To 12
                                                                                                </td>
                                                                                                <td style="width: 25%" class="ControlTextBox66">
                                                                                                    <asp:TextBox ID="txt11to12" runat="server" Width="98%"  style="height:41px;overflow: hidden;padding: 0px;font-family: 'Trebuchet MS'; font-size: 13px; background-color:#e7e7e7"
                                                                                                        TextMode="MultiLine"> </asp:TextBox>
                                                                                                </td>
                                                                                                <td class="ControlLabel" style="width: 15%">
                                                                                                    12 to 1
                                                                                                </td>
                                                                                                <td style="width: 25%" class="ControlTextBox66">
                                                                                                    <asp:TextBox ID="txt12to1" runat="server"  Width="98%"  style="height:41px;overflow: hidden;padding: 0px;font-family: 'Trebuchet MS'; font-size: 13px; background-color:#e7e7e7"
                                                                                                        TextMode="MultiLine"> </asp:TextBox>
                                                                                                </td>
                                                                                                <td style="width:10%">
                                                                                                </td>
                                                                                            </tr>
                                                                                            
                                                                                            <tr style="height:45px">
                                                                                                <td class="ControlLabel" style="width: 20%">
                                                                                                    1 To 2
                                                                                                </td>
                                                                                                <td style="width: 25%" class="ControlTextBox66">
                                                                                                    <asp:TextBox ID="txtPM1to2" runat="server"  Width="98%"  style="height:41px;overflow: hidden;padding: 0px;font-family: 'Trebuchet MS'; font-size: 13px; background-color:#e7e7e7"
                                                                                                        TextMode="MultiLine"> </asp:TextBox>
                                                                                                </td>
                                                                                                <td class="ControlLabel" style="width: 15%">
                                                                                                    2 To 3
                                                                                                </td>
                                                                                                <td style="width: 25%" class="ControlTextBox66">
                                                                                                    <asp:TextBox ID="txtPM2to3" runat="server"  Width="98%"  style="height:41px;overflow: hidden;padding: 0px;font-family: 'Trebuchet MS'; font-size: 13px; background-color:#e7e7e7"
                                                                                                        TextMode="MultiLine"> </asp:TextBox>
                                                                                                </td>
                                                                                                <td style="width:10%">
                                                                                                </td>
                                                                                            </tr>
                                                                                            
                                                                                            <tr style="height:45px">
                                                                                                <td class="ControlLabel" style="width: 20%">
                                                                                                    3 To 4
                                                                                                </td>
                                                                                                <td style="width: 25%" class="ControlTextBox66">
                                                                                                    <asp:TextBox ID="txtPM3to4" runat="server"  Width="98%"  style="height:41px;overflow: hidden;padding: 0px;font-family: 'Trebuchet MS'; font-size: 13px; background-color:#e7e7e7"
                                                                                                        TextMode="MultiLine"> </asp:TextBox>
                                                                                                </td>
                                                                                                <td class="ControlLabel" style="width: 15%">
                                                                                                    4 TO 5
                                                                                                </td>
                                                                                                <td style="width: 25%" class="ControlTextBox66">
                                                                                                    <asp:TextBox ID="txtPM4to5" runat="server"  Width="98%"  style="height:41px;overflow: hidden;padding: 0px;font-family: 'Trebuchet MS'; font-size: 13px; background-color:#e7e7e7"
                                                                                                        TextMode="MultiLine"> </asp:TextBox>
                                                                                                </td>
                                                                                                <td style="width:10%">
                                                                                                </td>
                                                                                            </tr>
                                                                                            
                                                                                            <tr style="height:45px">
                                                                                                <td class="ControlLabel" style="width: 20%">
                                                                                                    5 TO 6
                                                                                                </td>
                                                                                                <td style="width: 25%" class="ControlTextBox66">
                                                                                                    <asp:TextBox ID="txtPM5to6" runat="server"  Width="98%"  style="height:41px;overflow: hidden;padding: 0px;font-family: 'Trebuchet MS'; font-size: 13px; background-color:#e7e7e7"
                                                                                                        TextMode="MultiLine"> </asp:TextBox>
                                                                                                </td>
                                                                                                <td class="ControlLabel" style="width: 15%">
                                                                                                    6 To 7
                                                                                                </td>
                                                                                                <td style="width: 25%" class="ControlTextBox66">
                                                                                                    <asp:TextBox ID="txtPM6to7" runat="server"  Width="98%"  style="height:41px;overflow: hidden;padding: 0px;font-family: 'Trebuchet MS'; font-size: 13px; background-color:#e7e7e7"
                                                                                                        TextMode="MultiLine"> </asp:TextBox>
                                                                                                </td>
                                                                                                <td style="width:10%">
                                                                                                </td>
                                                                                            </tr>
                                                                                            
                                                                                            <tr style="height:45px">
                                                                                                <td class="ControlLabel" style="width: 20%">
                                                                                                    7 To 8
                                                                                                </td>
                                                                                                <td style="width: 25%" class="ControlTextBox66">
                                                                                                    <asp:TextBox ID="txtPM7to8" runat="server"  Width="98%"  style="height:41px;overflow: hidden;padding: 0px;font-family: 'Trebuchet MS'; font-size: 13px; background-color:#e7e7e7"
                                                                                                        TextMode="MultiLine"> </asp:TextBox>
                                                                                                </td>
                                                                                                <td class="ControlLabel" style="width: 15%">
                                                                                                    8 To 9
                                                                                                </td>
                                                                                                <td style="width: 25%" class="ControlTextBox66">
                                                                                                    <asp:TextBox ID="txtPM8to9" runat="server"  Width="98%"  style="height:41px;overflow: hidden;padding: 0px;font-family: 'Trebuchet MS'; font-size: 13px; background-color:#e7e7e7"
                                                                                                        TextMode="MultiLine"> </asp:TextBox>
                                                                                                </td>
                                                                                                <td style="width:10%">
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr style="height:45px">
                                                                                                <td class="ControlLabel" style="width: 20%">
                                                                                                    9 To 10
                                                                                                </td>
                                                                                                <td style="width: 25%" class="ControlTextBox66">
                                                                                                    <asp:TextBox ID="txtPM9to10" runat="server"  Width="98%"  style="height:41px;overflow: hidden;padding: 0px;font-family: 'Trebuchet MS'; font-size: 13px; background-color:#e7e7e7"
                                                                                                        TextMode="MultiLine"> </asp:TextBox>
                                                                                                </td>
                                                                                                <td class="ControlLabel" style="width: 15%">
                                                                                                    After 10
                                                                                                </td>
                                                                                                <td style="width: 25%" class="ControlTextBox66">
                                                                                                    <asp:TextBox ID="txtPMafter10" runat="server"  Width="98%"  style="height:41px;overflow: hidden;padding: 0px;font-family: 'Trebuchet MS'; font-size: 13px; background-color:#e7e7e7"
                                                                                                        TextMode="MultiLine"> </asp:TextBox>
                                                                                                </td>
                                                                                                <td style="width:10%">
                                                                                                </td>
                                                                                            </tr>
                                                                                            
                                                                                            <tr>
                                                                                                <td class="ControlLabel" style="width: 20%">
                                                                                                    Approved
                                                                                                </td>
                                                                                                <td style="width: 25%" class="ControlDrpBorder">
                                                                                                    <asp:DropDownList ID="drpapproved" runat="server" width="100%"  CssClass="drpDownListMedium" BackColor = "#e7e7e7" AppendDataBoundItems="True" style="border: 1px solid #e7e7e7" height="26px">
                                                                                                        <asp:ListItem Value="NO">No</asp:ListItem>
                                                                                                        <asp:ListItem Value="YES">Yes</asp:ListItem>
                                                                                                    </asp:DropDownList>
                                                                                                </td>
                                                                                                <td class="tblLeft" style="width: 15%">
                                                                                                </td>
                                                                                                <td class="tblLeft" style="width: 25%">
                                                                                                </td>
                                                                                                <td style="width:10%">
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </asp:Panel>
                                                                                    </div>
                                                                                </ContentTemplate>
                                                                            </cc1:TabPanel>
                                                                        </cc1:TabContainer>
                                                                        <asp:Panel ID="Panel3" runat="server">
                                                                            <table style="width: 100%;" align="center" cellpadding="3" cellspacing="5">
                                                                                <tr>
                                                                                    <td style="width: 25%;">
                                                                                    </td>
                                                                                    <td align="right" style="width: 25%;">                                                                                       
                                                                                         <asp:Button ID="btnSave" ValidationGroup="Save" runat="server" CssClass="savebutton1231"
                                                                                            EnableTheming="false" SkinID="skinBtnSave" OnClick="btnSave_Click" />
                                                                                        <asp:Button ID="btnUpdate" runat="server" ValidationGroup="Save" CssClass="Updatebutton1231"
                                                                                            EnableTheming="false" SkinID="skinBtnSave" OnClick="btnUpdate_Click" Enabled="false" />
                                                                                    </td>
                                                                                    <td align="left" style="width: 25%;">
                                                                                        <asp:Button ID="btnCancel" runat="server" CssClass="cancelbutton6" EnableTheming="false"
                                                                                            SkinID="skinBtnCancel" OnClick="btnCancel_Click" Enabled="false" />
                                                                                    </td>
                                                                                    <td style="width: 25%;">
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </asp:Panel>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <asp:Panel ID="pnsApprov" runat="server" Visible="False">
                            <table style="width: 100%;" align="center" cellpadding="3" cellspacing="5" class="SalesBg">
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="pnsSave" runat="server" Visible="False">
                            <table style="width: 100%;" align="center" cellpadding="3" cellspacing="5" class="SalesBg">
                            </table>
                        </asp:Panel>
                        <table width="100%" style="margin: -1px 0px 0px 0px;">
                                <tr style="width: 100%">
                                    <td>
                        <asp:GridView ID="GrdTse" runat="server" AllowSorting="True" AutoGenerateColumns="False" CssClass="someClass"
                            Width="100.3%" AllowPaging="True" OnPageIndexChanging="GrdTse_PageIndexChanging" OnRowDataBound="GrdTse_RowDataBound"
                            OnRowCreated="GrdTse_RowCreated" DataKeyNames="Empno" PageSize="7" EmptyDataText="No Time Sheet Entry Details found for the search criteria"
                            OnSelectedIndexChanged="GrdTse_SelectedIndexChanged" OnRowDeleting="GrdTse_RowDeleting">
                            <EmptyDataRowStyle CssClass="GrdContent" />
                            <Columns>
                                <asp:BoundField DataField="TSDate" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}"  HeaderStyle-BorderColor="Gray"/>
                                <asp:BoundField DataField="empno" HeaderText="Emp No"  HeaderStyle-BorderColor="Gray"/>
                                <asp:BoundField DataField="Approved" HeaderText="Approved"  HeaderStyle-BorderColor="Gray"/>
                                <asp:TemplateField ItemStyle-CssClass="command" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderColor="Gray"
                                    HeaderStyle-Width="50px" HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Select" />
                                        <asp:ImageButton ID="btnEditDisabled" Enabled="false" SkinID="editDisable" runat="Server"></asp:ImageButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-CssClass="command" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderColor="Gray"
                                    HeaderStyle-Width="50px" HeaderText="Delete">
                                    <ItemTemplate>
                                        <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to Delete this Time Sheet Entry?"
                                            runat="server">
                                        </cc1:ConfirmButtonExtender>
                                        <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" CommandName="Delete"></asp:ImageButton>
                                        <asp:ImageButton ID="lnkBDisabled" Enabled="false" SkinID="deleteDisable" runat="Server"></asp:ImageButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerTemplate>
                                <table style=" border-color:white">
                                    <tr style=" border-color:white">
                                        <td style=" border-color:white">
                                            Goto Page
                                        </td>
                                        <td style=" border-color:white">
                                            <asp:DropDownList ID="ddlPageSelector" runat="server" AutoPostBack="true" Width="65px" style="border:1px solid blue"
                                                OnSelectedIndexChanged="ddlPageSelector_SelectedIndexChanged">
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
                        </td>
                        </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
