<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportXLAbsolute.aspx.cs" Inherits="ReportXLAbsolute" %>

<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Absolute Items</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <%--<link href="App_Themes/DefaultTheme/DefaultTheme.css" rel="stylesheet" type="text/css" />--%>
    <link href="App_Themes/DefaultTheme/calendar.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="Scripts/calendar_eu.js"></script>

</head>
<body style="font-family: 'Trebuchet MS'; font-size: 11px;">
    <form id="form1" runat="server">
    <asp:ScriptManager runat="server" ID="scr"></asp:ScriptManager>
        <div align="center">
            <asp:HiddenField ID="hdStock" runat="server" />
                <table>
                    <tr style="height:3px">
                        
                    </tr>
                </table>
                <table style="border: 1px solid blue; background-color:White;" width="450px">
                    <%--<tr class="mainConHd">
                        <td colspan="4">
                            <span>Stock Ledger Report</span>
                        </td>
                    </tr>--%>
                    <%--<tr style="height:1px">
                    
                    </tr>--%>
                    <%--<tr class="subHeadFont">
                        <td colspan="3">
                            <table>
                                <tr>
                                    <td>
                                        Obsolute Item List
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>--%>
                    <tr>
                        <td colspan="3" class="headerPopUp">
                            Obsolete Item List
                        </td>
                    </tr>
                    <tr style="height:6px">
                    
                    </tr>
                    <tr>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td style="width:25%">
                                    
                                    </td>
                                    <td style="width:25%">
                                        Branch
                                    </td>
                                    <td style="width:25%" class="ControlDrpBorder">
                                        
                                        <asp:DropDownList ID="drpBranchAdd" TabIndex="10" Width="100%" DataTextField="BranchName" DataValueField="Branchcode" CssClass="drpDownListMedium" AppendDataBoundItems="true" BackColor="#e7e7e7" Style="border: 1px solid #e7e7e7" Height="26px"
                                                                                                        runat="server">
                                                                                                    </asp:DropDownList>
                                            
                                    </td>
                                    <td style="width:25%">
                                            
                                    </td>
                                   
                                </tr>
                            </table>
                        </td>
                    </tr>  
                    <tr>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td style="width:15%">
                                    
                                    </td>
                                    <td style="width:60%" class="ControlTextBox3" align="left">
                                        <asp:RadioButtonList ID="chkoption" runat="server"  Width="80%"
                                            >
                                            <asp:ListItem Text="All ItemList" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Obsolete ItemList"></asp:ListItem>
                                            <asp:ListItem Text="Other Than Obsolete"></asp:ListItem>
                                        </asp:RadioButtonList>      
                                    </td>
                                    <td style="width:10%">
                                            
                                    </td>
                                   
                                </tr>
                            </table>
                        </td>
                    </tr>   
                    <tr style="height:1px">
                    
                    </tr>
                    
                    <tr>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td style="width:15%">
                                    
                                    </td>
                                    <td style="width:60%" class="ControlTextBox3">
                                        <div style="overflow-y: scroll; height: 150px;" align="left">
                                             <asp:CheckBoxList ID="CheckBoxList1" runat="server" AutoPostBack="true" SelectionMode="Multiple" AppendDataBoundItems="true" DataTextField="PriceName" DataValueField="PriceName" OnSelectedIndexChanged="lst_SelectedIndexChanged_1">
                                                  <asp:ListItem Text="All" Value="0" />
                                     </asp:CheckBoxList>
                                        <asp:CheckBoxList ID="lstPricelist" runat="server" RepeatDirection="Vertical" SelectionMode="Multiple" AppendDataBoundItems="true" DataTextField = "PriceName" DataValueField = "PriceName">
                                                <%--<asp:ListItem Text="All" Value="0" />--%>
                                            </asp:CheckBoxList>
                                            </div>
                                        <%--<asp:CheckBoxList runat="server" ID="chkopt" Width="80%">
                                           <asp:ListItem Text="With MRP" Selected="False"></asp:ListItem>
                                           <asp:ListItem Text="With DP" Selected="False"></asp:ListItem>
                                           <asp:ListItem Text="With NLC" Selected="False"></asp:ListItem>
                                        </asp:CheckBoxList>   --%> 
                                    </td>
                                    <td style="width:10%">
                                            
                                    </td>
                                   
                                </tr>
                            </table>
                        </td>
                    </tr>   
                    <tr style="height:6px">
                    
                    </tr>
                    <tr>  
                        <td>                                    
                            <table width="100%">
                                <tr>
                                    <td style="width:25%">
                                    </td>
                                    <td style="width:25%">
                                            <asp:Button ID="btnxls" runat="server" CssClass="exportexl6" CausesValidation="true" 
                                                EnableTheming="false" OnClick="btnxls_Click" Width="152px" />
                                        <asp:CheckBox ID="chkboxMRP" Visible="false" runat="server" Text="With MRP" style="color:Black" Font-Names="arial" Font-Size="11px" AutoPostBack="true"/>
                                        <asp:CheckBox ID="chkboxDP" Visible="false" runat="server" Text="With DP" style="color:Black" Font-Names="arial" Font-Size="11px" AutoPostBack="true"/>
                                        <asp:CheckBox ID="chkboxNLC" Visible="false" runat="server" Text="With NLC" style="color:Black" Font-Names="arial" Font-Size="11px" AutoPostBack="true"/>
                                             
                                    </td>
                                    <td style="width:20%">
                                             <asp:Button ID="btnReport" EnableTheming="false" runat="server" CssClass="NewReport6"
                                            Width="120px" OnClick="btnReport_Click" />
                                         <asp:CheckBox ID="CheckBox12" Visible="false" runat="server" Text="With MRP" style="color:Black" Font-Names="arial" Font-Size="11px" AutoPostBack="true"/>
                                        <asp:CheckBox ID="CheckBox22" Visible="false" runat="server" Text="With DP" style="color:Black" Font-Names="arial" Font-Size="11px" AutoPostBack="true"/>
                                        <asp:CheckBox ID="CheckBox32" Visible="false" runat="server" Text="With NLC" style="color:Black" Font-Names="arial" Font-Size="11px" AutoPostBack="true"/>
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
                                            AutoGenerateColumns="true" AllowPrintPaging="true" Width="850px"
                                            OnRowDataBound="gvLedger_RowDataBound">
                                            <HeaderStyle CssClass="ReportHeadataRow" />
                                            <RowStyle CssClass="ReportdataRow" />
                                            <AlternatingRowStyle CssClass="ReportAltdataRow" />
                                            <FooterStyle CssClass="ReportFooterRow" />
                                                                                        
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



