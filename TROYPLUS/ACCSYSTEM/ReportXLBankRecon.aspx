<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportXLBankRecon.aspx.cs" Inherits="ReportXLBankRecon" %>

<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Bank Reconciliation Report</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <%--<link href="App_Themes/DefaultTheme/DefaultTheme.css" rel="stylesheet" type="text/css" />--%>
    <link href="App_Themes/DefaultTheme/calendar.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="Scripts/calendar_eu.js"></script>

</head>
<body style="font-family: 'Trebuchet MS'; font-size: 11px;">
    <form id="form1" runat="server">
    <asp:ScriptManager runat="server" ID="scr"></asp:ScriptManager>
        <div align="center">
        <br />

            <asp:HiddenField ID="hdStock" runat="server" />
                <table style="border: 1px solid blue; background-color:White;" width="460px">
                    <%--<tr style="height:5px">

                    </tr>--%>
                    <%--<tr class="subHeadFont">
                        <td colspan="3">
                            <table>
                                <tr>
                                    <td>
                                        Bank Reconciliation Report
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr style="height:5px">

                    </tr>--%>
                    <tr>
                        <td colspan="3" class="headerPopUp">
                            Bank Reconciliation Report
                        </td>
                    </tr>
                    
                <tr style="height:7px">

                </tr>
                    <%--<tr>
                        <td class="ControlLabel2" style="width:35%;">
                            Date
                        </td>
                        <td style="width:35%;" class="ControlTextBox3">
                            <asp:TextBox ID="txtSrtDate" runat="server"  AutoPostBack="True" BackColor = "#90c9fc" SkinID="skinTxtBoxGrid"
                                Width="100px"
                                MaxLength="10" TabIndex="1"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                PopupButtonID="btnSrtDate" TargetControlID="txtSrtDate" Enabled="True">
                            </cc1:CalendarExtender>
                        </td>
                        <td align="left" style="width:30%;">
                            <asp:ImageButton ID="btnSrtDate" ImageUrl="App_Themes/NewTheme/images/cal.gif"
                                CausesValidation="False" Width="20px" runat="server" />    
                        </td>
                                    
                               
                                <%--<tr>
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
                                </tr>--%>
                              
                      <%--</tr>--%>
                      <tr>
                        <td class="ControlLabel2" style="width:30%">
                            Option
                        </td>
                        <td class="ControlTextBox3"  style="width:40%">
                            <asp:RadioButtonList ID="opnbank" runat="server" style="font-size:14px" 
                                RepeatDirection="Horizontal" BackColor="#90C9FC" Height="35px" OnSelectedIndexChanged="opnbank_SelectedIndexChanged" AutoPostBack="True">
                                <asp:ListItem Selected="True">Bank</asp:ListItem>
                                <asp:ListItem>Customer</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                                                                                                    
                        <td align="left" style="width:30%">
                                                                                                        
                        </td>
                    </tr>
                    <tr>
                        <td class="ControlLabel2" style="width:30%">
                            Bank
                        </td>
                        <td class="ControlDrpBorder"  style="width:35%">
                            <asp:DropDownList ID="ddlbank" AppendDataBoundItems="true" style="border: 1px solid #90C9FC" runat="server" Height="25px" Width="100%"  CssClass="drpDownListMedium" BackColor = "#90C9FC" DataTextField="LedgerName"  DataValueField="LedgerID">
                            </asp:DropDownList>
                        </td>
                                                                                                    
                        <td align="left" style="width:30%">
                                                                                                        
                        </td>
                    </tr>
                    <tr>
                        <td class="ControlLabel2" style="width:30%">
                            Customer
                        </td>
                        <td class="ControlDrpBorder"  style="width:35%">
                            <asp:DropDownList ID="ddlCustomer" AppendDataBoundItems="true" style="border: 1px solid #90C9FC" runat="server" Height="25px" Width="100%"  CssClass="drpDownListMedium" BackColor = "#90C9FC" DataTextField="LedgerName"  DataValueField="LedgerID">
                            </asp:DropDownList>
                        </td>
                                                                                                    
                        <td align="left" style="width:30%">
                                                                                                        
                        </td>
                    </tr>  
                    <tr>
                        <td class="ControlLabel2" style="width:30%">
                            List Option
                        </td>
                        <td class="ControlTextBox3"  style="width:35%" align="left">
                            <asp:RadioButtonList ID="btnlist" runat="server" Width="100%" style="font-size:12px" 
                                BackColor="#90C9FC" Height="45px">
                                <asp:ListItem Selected="True">All</asp:ListItem>
                                <asp:ListItem>Pending</asp:ListItem>
                                <asp:ListItem>Reconciliated</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                                                                                                    
                        <td align="left" style="width:30%">
                                                                                                        
                        </td>
                    </tr>                                
                    <tr>  
                        <td colspan="3">                                    
                            <table width="100%">
                                <tr>
                                    <td style="width:40%">
                                    </td>
                                    <td style="width:20%" align="center">
                                        <asp:Button ID="btnxls" runat="server" CssClass="exportexl6" 
                                            EnableTheming="false" OnClick="btnxls_Click" Width="152px" />
                                    </td>
                                    <td style="width:40%">
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



