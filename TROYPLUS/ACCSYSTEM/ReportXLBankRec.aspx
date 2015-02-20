<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportXLBankRec.aspx.cs" Inherits="ReportXLBankRec" %>

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
                    <tr>
                        <td style="width:35%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                            Start Date
                        </td>
                        <td width="40%" class="ControlTextBox3">
                            <asp:TextBox ID="txtStartDate" runat="server" CssClass="cssTextBox" Width="100px"
                                MaxLength="10" />
                        </td>
                        <td align="left" width="25%">
                            <script type="text/javascript" language="JavaScript">                        new tcal({ 'formname': 'form1', 'controlname': 'txtStartDate' });</script>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtStartDate"
                                Display="None" ErrorMessage="Please Enter Start Date" CssClass="lblFont"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td  style="width:35%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                            End Date
                        </td>
                        <td align="left" class="ControlTextBox3" width="40%">
                            <asp:TextBox ID="txtEndDate" runat="server" CssClass="cssTextBox" Width="100px" MaxLength="10" />
                        </td>
                        <td align="left">
                            <script type="text/javascript" language="JavaScript">                        new tcal({ 'formname': 'form1', 'controlname': 'txtEndDate' });</script>
                            <%--<a href="javascript:NewCal('txtEndDate','ddmmyyyy',false,24)"><img src="cal.gif" width="16" height="16" border="0" alt="Pick a date"></a>--%>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEndDate"
                                Display="None" ErrorMessage="Please Enter The End Date" CssClass="lblFont"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtStartDate"
                                ControlToValidate="txtEndDate" Display="None" ErrorMessage="Start Date Should Be Less Than the End Date"
                                CssClass="lblFont" Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:35%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                            Option
                        </td>
                        <td align="left" class="ControlTextBox3" width="40%">
                            <asp:RadioButtonList ID="optionmethod" runat="server" style="font-size:14px" align="center"
                                    BackColor="#90C9FC" Height="35px">
                                    <asp:ListItem Selected="True" Value="ReconciliatedDate">Reconciliated Date</asp:ListItem>
                                    <asp:ListItem Value="TransDate">Trans Date</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td align="left">

                        </td>
                    </tr>
                    <tr>  
                        <td colspan="3">                                    
                            <table width="100%">
                                <tr>
                                    <td style="width:45%">
                                    </td>
                                    <td style="width:20%" align="center">
                                        <asp:Button ID="btnxls" runat="server" CssClass="exportexl6" 
                                            EnableTheming="false" OnClick="btnxls_Click" />
                                    </td>
                                    <td style="width:35%">
                                        <asp:Button ID="btngriddata" runat="server" CssClass="generatebutton" 
                                                EnableTheming="false" OnClick="btngriddata_Click" Visible="False" />
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



