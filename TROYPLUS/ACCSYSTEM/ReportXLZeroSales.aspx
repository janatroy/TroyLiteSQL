<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportXLZeroSales.aspx.cs" Inherits="ReportXLZeroSales" %>

<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Stock Ledger Report</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <%--<link href="App_Themes/DefaultTheme/DefaultTheme.css" rel="stylesheet" type="text/css" />--%>
    <link href="App_Themes/DefaultTheme/calendar.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="Scripts/calendar_eu.js"></script>

</head>
<body style="font-family: 'Trebuchet MS'; font-size: 11px;">
    <form id="form1" runat="server">
    <asp:ScriptManager runat="server" ID="scr"></asp:ScriptManager>
    <br />
        <div align="center">
            <asp:HiddenField ID="hdStock" runat="server" />
                <table style="border: 1px solid blue; background-color:White;" width="400px">
                    <%--<tr class="mainConHd">
                        <td colspan="4">
                            <span>Stock Ledger Report</span>
                        </td>
                    </tr>--%>
                    <tr>
                        
                                    <td colspan="3" class="headerPopUp">
                                        Zero / 1 Rupee Value Sales Report
                                    </td>
                                
                    </tr>
                    <tr style="height:6px">
                    
                    </tr>
                    <tr>
                        <td>
                            <table style="width:460px; height: 100%">
                                <tr>
                                    <td class="ControlLabel2" style="width:35%;">
                                        Start Date
                                    </td>
                                    <td style="width:35%;" class="ControlTextBox3">
                                            <asp:TextBox ID="txtSrtDate" runat="server" AutoPostBack="True" BackColor = "#e7e7e7" SkinID="skinTxtBoxGrid"
                                                Width="100px"
                                                MaxLength="10" TabIndex="1"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                PopupButtonID="btnSrtDate" TargetControlID="txtSrtDate" Enabled="True">
                                            </cc1:CalendarExtender>
                                    </td>
                                    <td align="left" style="width:35%;">
                                            <asp:ImageButton ID="btnSrtDate" ImageUrl="App_Themes/NewTheme/images/cal.gif"
                                                CausesValidation="False" Width="20px" runat="server" />    
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr style="height: 2px;"/> 
                                <tr>
                                    <td class="ControlLabel2" style="width:35%;">
                                        End Date
                                    </td>
                                    <td style="width:35%;" class="ControlTextBox3">
                                            <asp:TextBox ID="txtEdDate" runat="server" AutoPostBack="True" BackColor = "#e7e7e7" SkinID="skinTxtBoxGrid"
                                                Width="100px"
                                                MaxLength="10" TabIndex="1"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                PopupButtonID="btnEdDate" TargetControlID="txtEdDate" Enabled="True">
                                            </cc1:CalendarExtender>
                                    </td>
                                    <td align="left" style="width:35%;">
                                            <asp:ImageButton ID="btnEdDate" ImageUrl="App_Themes/NewTheme/images/cal.gif"
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
                                         <asp:DropDownList ID="drpBranch" runat="server" AutoPostBack="true" AppendDataBoundItems="True" BackColor="#E7E7E7" CssClass="drpDownListMedium" EnableTheming="False" Height="26px" Style="border: 1px solid #e7e7e7" Width="100%" DataTextField="BranchName" DataValueField="Branchcode">
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
                                        Value
                                    </td>
                                    <td style="width:35%;" class="ControlTextBox3">
                                        <asp:TextBox ID="txtvalue" runat="server"  AutoPostBack="True" BackColor = "#e7e7e7" SkinID="skinTxtBoxGrid"
                                            Width="100px"
                                            MaxLength="10" TabIndex="1"></asp:TextBox>
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
                                    <td style="width:35%;" class="ControlTextBox3" align="left">
                                        <asp:RadioButtonList ID="chkoption" runat="server">
                                            <asp:ListItem Text="Only Value Details" Selected="true"></asp:ListItem>
                                            <asp:ListItem Text="Whole Bill Details"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td>
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
                                       
                                        <td style="width:20%">
                                             <asp:Button ID="btnxls" runat="server" CssClass="exportexl6" 
                                                  EnableTheming="false" OnClick="btnxls_Click" Width="152px" />
                                        </td>
                                        <td style="width:20%">
                                            <asp:Button ID="btngriddata" runat="server" CssClass="NewReport6" 
                                                  EnableTheming="false" OnClick="btngriddata_Click" Width="152px" />
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



