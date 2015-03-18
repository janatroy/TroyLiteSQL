<%--<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ReportExcelFormulaExecution.aspx.cs" Inherits="ReportExcelFormulaExecution" Title="Formula Execution Report Page" %>--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportExcelFormulaExecution.aspx.cs" Inherits="ReportExcelFormulaExecution" %>
<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Stock Level Report</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <%--<link href="App_Themes/DefaultTheme/DefaultTheme.css" rel="stylesheet" type="text/css" />--%>
    <link href="App_Themes/DefaultTheme/calendar.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="Scripts/calendar_eu.js"></script>

    <script language="javascript" type="text/javascript">
        function showMacAddress() {
            var obj = new ActiveXObject("WbemScripting.SWbemLocator");
            var s = obj.ConnectServer(".");
            var properties = s.ExecQuery("SELECT * FROM Win32_NetworkAdapterConfiguration");
            var e = new Enumerator(properties);
            var output;
            output = '<table border="0" cellPadding="5px" cellSpacing="1px" bgColor="#CCCCCC">';
            output = output + '<tr bgColor="#EAEAEA"><td>Caption</td><td>MACAddress</td></tr>';
            while (!e.atEnd()) {
                e.moveNext();
                var p = e.item();
                if (!p) continue;
                output = output + '<tr bgColor="#FFFFFF">';
                output = output + '<td>' + p.Caption; +'</td>';
                output = output + '<td>' + p.MACAddress + '</td>';
                output = output + '</tr>';
            }
            output = output + '</table>';
            document.getElementById("box").innerHTML = output;
        }
    </script>

</head>
<body style="font-family: 'Trebuchet MS'; font-size: 11px;">
    <form id="form1" runat="server">
    <asp:ScriptManager runat="server" ID="scr"></asp:ScriptManager>
        <div align="center">
            <asp:HiddenField ID="hdStock" runat="server" />
            <br />
                <table style="border: 1px solid blue; background-color:White;" width="450px">
                    <%--<tr style="height:1px">
                    
                    </tr>--%>
                    <%--<tr class="subHeadFont">
                        <td colspan="3">
                            <table>
                                <tr>
                                    <td>
                                        Stock Level Report
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>--%>
                    <tr class="subHeadFont">
                        <td colspan="3" class="headerPopUp">
                           Product Manufacturing report
                        </td>
                            
                    </tr>
                    <tr style="height:6px">
                        <td>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="Label2" runat="server"
                                 />
                        </td>
                    </tr>
                    
                    <tr>
                        <td>
                            <table style="width:570px;">
                                <tr style="height: 2px;"/>
                                <tr>
                                    <td class="ControlLabel2" style="width:20%;">
                                    <%--     <asp:RequiredFieldValidator ValidationGroup="reportandexcel" ID="RequiredFieldValidator2" runat="server"
                                                                                                Text="*" ErrorMessage="Please select Product. It cannot be left blank. " ControlToValidate="txtProjectCode"></asp:RequiredFieldValidator>--%>
                                        Select Product
                                    </td>
                                    <td style="width:15%;" class="ControlDrpBorder">
                                         <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                                                                                <ContentTemplate>
                                        <asp:DropDownList ID="drpproduct"   AutoPostBack="true" runat="server" AppendDataBoundItems="true"
                                            Width="100%" CssClass="drpDownListMedium" DataTextField="FormulaName" DataValueField="FormulaName" BackColor = "#e7e7e7"
                                            style="border: 1px solid #e7e7e7" height="26px">
                                        </asp:DropDownList>
                                                                                                        </ContentTemplate>
                                                                                            </asp:UpdatePanel>
                                    </td>
                                    <td  style="width:5%;">
                                            
                                    </td>
                                     <td class="ControlLabel2" style="width:20%;">
                                        Product Process?
                                    </td>
                                    <td style="width:15%;" class="ControlDrpBorder">
                                            <asp:CheckBox  ID="rdoIsPros" runat="server" Text="Processed" />
                                    </td>
                                    <td  style="width:5%;">
                                            
                                    </td>
                                </tr>
                                <tr style="height: 2px;"/>
                                <tr>
                                    <td class="ControlLabel2" style="width:20%;">
                                        <asp:RequiredFieldValidator ValidationGroup="reportandexcel" ID="RequiredFieldValidator1" runat="server"
                                                                                                Text="*" ErrorMessage="Please select Start date. It cannot be left blank. " ControlToValidate="txtStartDate"></asp:RequiredFieldValidator>
                                       Start date
                                    </td>
                                      <td style="width: 15%;" class="ControlTextBox3">
                                                                                        <asp:TextBox ID="txtStartDate"  runat="server" CssClass="cssTextBox" Width="80px"></asp:TextBox>

                                                                                        <cc1:CalendarExtender ID="calExtender3" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                                            PopupButtonID="btnDate3" PopupPosition="BottomLeft" TargetControlID="txtStartDate">
                                                                                        </cc1:CalendarExtender>
                                                                                    </td>
                                                                                    <td style="width: 5%;">
                                                                                        <asp:ImageButton ID="btnDate3" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                                            Width="20px" runat="server" />
                                                                                    </td>
                                    <td class="ControlLabel2" style="width:20%;">
                                        <asp:RequiredFieldValidator ValidationGroup="reportandexcel" ID="RequiredFieldValidator3" runat="server"
                                                                                                Text="*" ErrorMessage="Please select End Date. It cannot be left blank. " ControlToValidate="txtEndDate"></asp:RequiredFieldValidator>
                                       End date
                                    </td>
                                      <td style="width: 15%;" class="ControlTextBox3">
                                                                                        <asp:TextBox ID="txtEndDate"  runat="server" CssClass="cssTextBox" Width="80px"></asp:TextBox>

                                                                                        <cc1:CalendarExtender ID="CalExtender1" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                                            PopupButtonID="btnDate4" PopupPosition="BottomLeft" TargetControlID="txtEndDate">
                                                                                        </cc1:CalendarExtender>
                                                                                    </td>
                                                                                    <td style="width: 5%;">
                                                                                        <asp:ImageButton ID="btnDate4" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                                            Width="20px" runat="server" />
                                                                                    </td>
                                </tr>
                                <tr style="height: 2px;"/>
                                <tr>
                                     <td class="ControlLabel2" style="width:20%;">
                                         Select In/Out categories
                                    </td>
                                      <td style="width:15%;" class="ControlDrpBorder">
                                       
                                        <asp:DropDownList ID="drpinout"  runat="server" AppendDataBoundItems="true"
                                            Width="100%" CssClass="drpDownListMedium" DataTextField="FormulaName" DataValueField="FormulaName" BackColor = "#e7e7e7"
                                            style="border: 1px solid #e7e7e7" height="26px">

                                             <asp:ListItem Text="All"  Value="All"></asp:ListItem>
                                             <asp:ListItem Text="Raw Material"  Value="Raw Material"></asp:ListItem>
                                             <asp:ListItem Text="Product"  Value="Product"></asp:ListItem>
                                                                                               
                                                                                            
                                        </asp:DropDownList>
                                    </td>
                                    <td  style="width:5%;">
                                            
                                    </td>
                                    <td class="ControlLabel2" style="width:20%;">
                                        Select Branch
                                        <asp:RequiredFieldValidator ValidationGroup="reportandexcel" ID="RequiredFieldValidator4" runat="server"
                                                                                                Text="*" ErrorMessage="Please select any one Branch in the Dropdown List. " ControlToValidate="drpBranch"></asp:RequiredFieldValidator>
                                    </td>
                                      <td style="width:15%;" class="ControlDrpBorder">
                                       
                                        <asp:DropDownList ID="drpBranch"  runat="server" AppendDataBoundItems="true"
                                            Width="100%" CssClass="drpDownListMedium" DataTextField="BranchName" DataValueField="Branchcode" BackColor = "#e7e7e7"
                                            style="border: 1px solid #e7e7e7" height="26px">
                                        </asp:DropDownList>
                                    </td>
                                    <td  style="width:5%;">
                                            
                                    </td>
                                </tr>
                                
                              </table>
                          </td>
                      </tr>    
                      <tr style="height:3px">
                    
                      </tr>                                  
                       <tr>  
                            <td>                                    
                                <table width="100%">
                                    <tr>
                                        <td style="width:25%">
                                        </td>
                                        <td style="width: 19%">
                                        <asp:Button ID="btnReport" EnableTheming="false" runat="server" CssClass="NewReport6"
                                            Width="120px" OnClick="btnReport_Click" ValidationGroup="reportandexcel" />
                                    </td>
                                        <td style="width:20%">
                                             <asp:Button ID="btnxls" runat="server" CssClass="exportexl6" 
                                                  EnableTheming="false"  Width="152px" OnClick="btnxls_Click" ValidationGroup="reportandexcel"/>
                                        </td>
                                        <td style="width:30%">
                                           <%-- <asp:Button ID="btngriddata" runat="server" Visible="false" CssClass="generatebutton" 
                                                  EnableTheming="false" Width="152px" />--%>
                                        </td>
                                    </tr>
                            </table>
                        </td>
                    </tr>
                    </table>
                                                                                          
                            <table width="100%">
                                <tr>
                                    <td>
                                       <wc:ReportGridView runat="server" BorderWidth="1" ID="gvLedger" CssClass="gridview"
                                            GridLines="Both" AlternatingRowStyle-CssClass="even" HeaderStyle-HorizontalAlign="Left"
                                            AutoGenerateColumns="false" AllowPrintPaging="true" Width="850px"
                                            >
                                            <HeaderStyle CssClass="ReportHeadataRow" />
                                            <RowStyle CssClass="ReportdataRow" />
                                            <AlternatingRowStyle CssClass="ReportAltdataRow" />
                                            <FooterStyle CssClass="ReportFooterRow" />
                                            <PageHeaderTemplate>
                                                <br />
                                                <br />
                                            </PageHeaderTemplate>
                                            <Columns>
                                                <asp:BoundField ItemStyle-HorizontalAlign="Left" DataField="ProductName" HeaderText="ProductName" />
                                                <asp:BoundField ItemStyle-HorizontalAlign="Left" DataField="Brand" HeaderText="Brand" />
                                                <asp:BoundField ItemStyle-HorizontalAlign="Left" DataField="Model" HeaderText="Model" />
                                                <asp:BoundField ItemStyle-HorizontalAlign="Left" DataField="Opening" HeaderText="Opening" />
                                                <asp:BoundField ItemStyle-HorizontalAlign="Left" DataField="Purchase" HeaderText="Purchase" />
                                                <asp:BoundField ItemStyle-HorizontalAlign="Left" DataField="Sales" HeaderText="Sales" />
                                            </Columns>
                                            <PagerTemplate>
                                            </PagerTemplate>
                                            <PageFooterTemplate>
                                            </PageFooterTemplate>
                                        </wc:ReportGridView>
                                    </td>
                                </tr>
                        </table>

             
        </div>
    </form>
</body>
</html>

