<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportXLStockLevel.aspx.cs" Inherits="ReportXLStockLevel" %>

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
                            Stock Level Report
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
                                <tr>
                                    <td class="ControlLabel2" style="width:35%;">
                                        Date
                                    </td>
                                    <td style="width:35%;" class="ControlTextBox3">
                                            <asp:TextBox ID="txtStartDate" Enabled="false" runat="server" cssclass="cssTextBox" AutoPostBack="True" BackColor = "#e7e7e7"
                                                Width="100%"
                                                MaxLength="10" TabIndex="1"></asp:TextBox>
                                            <cc1:CalendarExtender ID="calStartDate" runat="server" Format="dd/MM/yyyy"
                                                PopupButtonID="btnStartDate" TargetControlID="txtStartDate" Enabled="True">
                                            </cc1:CalendarExtender>
                                    </td>
                                    <td align="left" style="width:35%;">
                                            <asp:ImageButton ID="btnStartDate" ImageUrl="App_Themes/NewTheme/images/cal.gif"
                                                CausesValidation="False" Width="20px" runat="server" />    
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr style="height: 2px;"/>
                                <tr>
                                    <td class="ControlLabel2" style="width:35%;">
                                        Branch
                                    </td>
                                    <td style="width:35%;" class="ControlDrpBorder">
                                        <asp:DropDownList ID="drpBranchAdd" TabIndex="10" Width="100%" DataTextField="BranchName" DataValueField="Branchcode" CssClass="drpDownListMedium" AppendDataBoundItems="true" BackColor="#e7e7e7" Style="border: 1px solid #e7e7e7" Height="26px"
                                                                                                        runat="server">
                                                                                                    </asp:DropDownList>
                                    </td>
                                    <td align="left" style="width:35%;">
                                            
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr style="height: 2px;"/>
                                <tr>
                                    <td class="ControlLabel2" style="width:35%;">
                                        Price List
                                    </td>
                                    <td style="width:35%;" align="left" class="ControlDrpBorder">
                                         <div style="overflow: scroll; height: 150px;" align="left">
                                              <asp:CheckBoxList ID="CheckBoxList1" runat="server" AutoPostBack="true" SelectionMode="Multiple" AppendDataBoundItems="true" DataTextField="PriceName" DataValueField="PriceName" OnSelectedIndexChanged="lst_SelectedIndexChanged_1">
                                                  <asp:ListItem Text="All" Value="0" />
                                     </asp:CheckBoxList>
                                        <asp:CheckBoxList ID="lstPricelist" runat="server" RepeatDirection="Vertical" SelectionMode="Multiple" AppendDataBoundItems="true" DataTextField = "PriceName" DataValueField = "PriceName">
                                                <%--<asp:ListItem Text="All" Value="0" />--%>
                                            </asp:CheckBoxList>
                                            </div>
                                    </td>
                                    <td align="left" style="width:35%;">
                                            
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr style="height: 2px;"/>
                                <tr>
                                    <td class="ControlLabel2" style="width:35%;">
                                        Range
                                    </td>
                                    <td style="width:35%;" class="ControlDrpBorder">
                                        <asp:DropDownList ID="cmbtrange" runat="server" AppendDataBoundItems="true" AutoCompleteMode="Suggest"
                                            Width="100%" CssClass="drpDownListMedium" BackColor = "#e7e7e7"
                                            style="border: 1px solid #e7e7e7" height="26px">
                                            <asp:ListItem Text="All" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="Below" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Above" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Equal" Value="3"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="left" style="width:35%;">
                                            
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr style="height: 2px;"/>
                                <tr>
                                    <td class="ControlLabel2" style="width:35%;">
                                        Option
                                    </td>
                                    <td style="width:35%;" class="ControlDrpBorder">
                                         <asp:DropDownList ID="cmbtoption" runat="server" AppendDataBoundItems="true" AutoCompleteMode="Suggest"
                                            Width="100%" CssClass="drpDownListMedium" BackColor = "#e7e7e7" 
                                            style="border: 1px solid #e7e7e7" height="26px">
                                            <asp:ListItem Text="Category" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Brand" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Product" Value="3"></asp:ListItem>
                                        </asp:DropDownList>
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
                                        <td style="width:10%">
                                        </td>
                                        <td style="width:10%">
                                            
                                        </td>
                                        <td style="width:20%">
                                             <asp:Button ID="btnxls" runat="server" CssClass="exportexl6" 
                                                  EnableTheming="false"  Width="152px" OnClick="btnxls_Click"/>
                                        </td>
                                        <td style="width:20%">
                                              <asp:Button ID="btnReport" EnableTheming="false" runat="server" CssClass="NewReport6"
                                            Width="120px" OnClick="btnReport_Click" />
                                        </td>
                                         <td style="width:10%">
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



