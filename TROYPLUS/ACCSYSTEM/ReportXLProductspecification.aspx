<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportXLProductspecification.aspx.cs" Inherits="ReportXLProductspecification" %>

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
                <table style="border: 1px solid blue; background-color:White;" width="400px">
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
                           Product Specification Report
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
                            <table style="width:460px; height: 100%">
                                <tr style="height: 2px;"/>
                                <tr>
                                    <td class="ControlLabel2" style="width:35%;">
                                        Select Specification
                                    </td>
                                    <td style="width:35%;" class="ControlDrpBorder">
                                         <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                                                                                <ContentTemplate>
                                        <asp:DropDownList ID="drpproduct" OnSelectedIndexChanged="drpproduct_SelectedIndexChanged"  AutoPostBack="true" runat="server" AppendDataBoundItems="true"
                                            Width="100%" CssClass="drpDownListMedium" DataTextField="FormulaName" DataValueField="FormulaName" BackColor = "#e7e7e7"
                                            style="border: 1px solid #e7e7e7" height="26px">
                                        </asp:DropDownList>
                                                                                                        </ContentTemplate>
                                                                                            </asp:UpdatePanel>
                                    </td>
                                    <td align="left" style="width:35%;">
                                            
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr style="height: 2px;"/>
                                <tr style="visibility:hidden">
                                    <td class="ControlLabel2" style="width:35%;">
                                       Product
                                    </td>
                                    <td style="width:35%;" class="ControlDrpBorder">
                                          <asp:UpdatePanel ID="UpdatePanel123" runat="server" UpdateMode="Conditional">
                                                                                                <ContentTemplate>
                                         <asp:DropDownList ID="drpoption" runat="server" 
                                            Width="100%" CssClass="drpDownListMedium" DataTextField="FormulaName" DataValueField="FormulaName" BackColor = "#e7e7e7" 
                                            style="border: 1px solid #e7e7e7" height="26px">
                                           </asp:DropDownList>
                                                                                                     </ContentTemplate>
                                                                                            </asp:UpdatePanel>
                                    </td>
                                    <td align="left" style="width:35%;">
                                             
                                    </td>
                                    <td>
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
                                        <td style="width:10%">
                                            
                                        </td>
                                        <td style="width:20%">
                                             <asp:Button ID="btnxls" runat="server" CssClass="exportexl6" 
                                                  EnableTheming="false"  Width="152px" OnClick="btnxls_Click"/>
                                        </td>
                                        <td style="width:30%">
                                            <asp:Button ID="btngriddata" runat="server" Visible="false" CssClass="generatebutton" 
                                                  EnableTheming="false" Width="152px" />
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
