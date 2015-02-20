<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportXLMissingDC.aspx.cs" Inherits="ReportXLMissingDC" %>

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
                <table style="border: 1px solid blue; background-color:White;" width="350px">
                    <%--<tr class="mainConHd">
                        <td colspan="4">
                            <span>Stock Ledger Report</span>
                        </td>
                    </tr>--%>
                    <%--<tr class="subHeadFont">
                        <td colspan="3">
                            <table>
                                <tr>
                                    <td>
                                        Missing DC Report
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>--%>
                    <tr class="subHeadFont">
                        <td colspan="3" class="headerPopUp">
                            Missing DC Report
                        </td>
                    </tr>
                    
                    <tr>
                        <td>
                            <table style="width:460px; height: 100%">
                                <tr style="height:6px">
                                        </tr>
                                <tr>
                                    <td class="ControlLabel2" style="width:35%;">
                                        Start Date
                                    </td>
                                    <td style="width:35%;" class="ControlTextBox3">
                                            <asp:TextBox ID="txtSrtDate" runat="server"  AutoPostBack="True" BackColor = "#90c9fc" SkinID="skinTxtBoxGrid"
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
                                <tr>
                                    <td class="ControlLabel2" style="width:35%;">
                                        End Date
                                    </td>
                                    <td style="width:35%;" class="ControlTextBox3">
                                            <asp:TextBox ID="txtEdDate" runat="server"  AutoPostBack="True" BackColor = "#90c9fc" SkinID="skinTxtBoxGrid"
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
                                <tr style="height:6px">
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
                                        <td style="width:10%">
                                            
                                             
                                        </td>
                                        <td style="width:40%" align="center">
                                             <asp:Button ID="btnxls" runat="server" CssClass="exportexl6" 
                                                  EnableTheming="false" OnClick="btnxls_Click" Width="152px" />
                                        </td>
                                        <td style="width:25%">
                                            <asp:Button ID="btngriddata" runat="server" CssClass="generatebutton" 
                                                  EnableTheming="false" OnClick="btngriddata_Click" Width="152px" Visible="False" />
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
                                            AutoGenerateColumns="false" PrintPageSize="47" AllowPrintPaging="true" Width="850px"
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
                                                <%--<asp:BoundField ItemStyle-HorizontalAlign="Left" DataField="Closing" HeaderText="Closing" />--%>

                                                <%--<asp:TemplateField HeaderText="Debit" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDebit" Text='<%# Eval("Debit","{0:f2}") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Credit" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCredit" Text='<%# Eval("Credit","{0:f2}") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Balance" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBalance" runat="server" CssClass="lblFont" Font-Bold="true" ForeColor="Blue"
                                                            Text="0.00"> </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
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



