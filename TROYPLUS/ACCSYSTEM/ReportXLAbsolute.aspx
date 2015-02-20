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
                                    <td style="width:60%" class="ControlTextBox3" align="left">
                                        <asp:CheckBox ID="chkboxMRP" runat="server" Text="With MRP" style="color:Black" Font-Names="arial" Font-Size="11px" AutoPostBack="true"/>
                                        <asp:CheckBox ID="chkboxDP" runat="server" Text="With DP" style="color:Black" Font-Names="arial" Font-Size="11px" AutoPostBack="true"/>
                                        <asp:CheckBox ID="chkboxNLC" runat="server" Text="With NLC" style="color:Black" Font-Names="arial" Font-Size="11px" AutoPostBack="true"/>
                                        
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
                                            <asp:Button ID="btnxls" runat="server" CssClass="exportexl6" 
                                                EnableTheming="false" OnClick="btnxls_Click" Width="152px" />
                                             
                                    </td>
                                    <td style="width:20%">
                                            
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
                                            OnRowDataBound="gvLedger_RowDataBound">
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



