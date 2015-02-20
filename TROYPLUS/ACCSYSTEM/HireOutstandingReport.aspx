<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HireOutstandingReport.aspx.cs"
    Inherits="HireOutstandingReport" %>

<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <title>Hire Outstanding Report</title>
    <link href="App_Themes/DefaultTheme/calendar.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/DefaultTheme/DefaultTheme.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="Scripts/calendar_eu.js"></script>
    <script type="text/javascript" language="JavaScript">

        function CallPrint(strid) {
            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('', '', 'letf=0,top=0,width=600,height=400,toolbar=0,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();

        }
    </script>
</head>
<body style="font-family: 'Trebuchet MS'; font-size: 11px;">
    <form id="form1" runat="server">
    <br />

    <div align="center" id="div1" runat="server">
        <table cellpadding="1" cellspacing="2" border="0" style="border: 1px solid blue; background-color:White;
            text-align: left; width:450px">
            <tr>
                <td colspan="4" class="subHeadFont2">
                    Hire Outstanding Report
                </td>
            </tr>
            <tr style="height:6px">

                </tr>
                <tr>
                <td class="ControlLabel2" style="width:35%;">
                    As on Date
                </td>
                <td style="width:35%;" class="ControlTextBox3">
                        <asp:TextBox ID="txtSrtDate" runat="server" BackColor = "#90c9fc" SkinID="skinTxtBoxGrid"
                            Width="100px"
                            MaxLength="10" TabIndex="1"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                            PopupButtonID="btnSrtDate" TargetControlID="txtSrtDate" Enabled="True">
                        </cc1:CalendarExtender>
                </td>
                <td align="left" style="width:15%;">
                        <asp:ImageButton ID="btnSrtDate" ImageUrl="App_Themes/NewTheme/images/cal.gif"
                            CausesValidation="False" Width="20px" runat="server" />    
                </td>
                <td>
                </td>
            </tr>
            <%--<tr>
                <td class="ControlLabel2" style="width:35%;">
                    End Date
                </td>
                <td style="width:35%;" class="ControlTextBox3">
                        <asp:TextBox ID="txtEdDate" runat="server"  BackColor = "#90c9fc" SkinID="skinTxtBoxGrid"
                            Width="100px"
                            MaxLength="10" TabIndex="1"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                            PopupButtonID="btnEdDate" TargetControlID="txtEdDate" Enabled="True">
                        </cc1:CalendarExtender>
                </td>
                <td align="left" style="width:15%;">
                        <asp:ImageButton ID="btnEdDate" ImageUrl="App_Themes/NewTheme/images/cal.gif"
                            CausesValidation="False" Width="20px" runat="server" />    
                </td>
                <td>
                </td>
            </tr>--%>
            <%--<tr>
                <td style="width:35%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                    Ledger Name
                </td>
                <td align="left" style="width:35%" class="ControlDrpBorder">
                    <asp:DropDownList ID="drpLedgerName" runat="server" Width="100%" DataTextField="GroupName" CssClass="drpDownListMedium" BackColor = "#90c9fc"
                        DataValueField="GroupID"  style="border: 1px solid #90c9fc" height="26px">
                    </asp:DropDownList>
                </td>
                <td style="width:15%">
                </td>
            </tr>--%>
            <%--<tr runat="server" id="hmg" visible="false">
                <td style="width:35%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                    Option
                </td>
                <td align="left" style="width:35%" class="ControlDrpBorder">
                    <asp:RadioButtonList ID="opttype" align="left" runat="server"
                        Width="100%" >
                        <asp:ListItem Text="All"></asp:ListItem>
                        <asp:ListItem  Selected="True" Text="Without DC/INT TRANS"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td style="width:15%">
                </td>
            </tr>--%>
            <tr style="height:6px">

            </tr>
            <tr>
                <td colspan="4">
                    <table width="100%">
                        <tr>
                            <td style="width:40%">

                            </td>
                            <td style="width:20%">
                                <asp:Button ID="btnReport" CssClass="NewReport6" EnableTheming="false" runat="server"
                                    OnClick="btnReport_Click" />
                            </td>
                            
                            <td style="width:10%">
                                <asp:Button ID="btndetails" CssClass="exportexl6" EnableTheming="false" runat="server"
                                    OnClick="btndetails_Click" Visible="false" />
                                    <asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="true" ShowSummary="false" />
                            </td>
                            <td style="width:30%">

                            </td>
                         </tr>
                    </table>
                </td>
             </tr>
             <tr>
                <td style="width:30%">
                    
                </td>
                <td style="width:35%">
                    
                                
                </td>
             </tr>
             
        </table>
    </div>
    <div id="OutPanel" runat="server" visible="false" align="center">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />
        <asp:ScriptManagerProxy runat="server" ID="ScriptManagerProxy1" />
        &nbsp;
        <table width="600px">
            <tr>
                <td colspan="4">
                    <table width="100%">
                        <tr>
                            <td style="width:40%">

                            </td>
                            <td style="width:20%">
                                <input type="button" id="Button1" runat="Server" onclick="javascript:CallPrint('divPrint')"
                                    class="printbutton6" />
                            </td>
                            <td style="width:10%">
                                <asp:Button ID="btndet" CssClass="GoBack" EnableTheming="false" runat="server"
                                    OnClick="btndet_Click" Visible="False" />
                            </td>
                            <td style="width:30%">

                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div id="divPrint" style="font-family: 'Trebuchet MS'; font-size: 11px;">
            <br />
            
            <table width="700px" border="0" style="font-family: Trebuchet MS; font-size: 14px;">
                <tr>
                    <td width="140px" align="left">
                        TIN#:
                        <asp:Label ID="lblTNGST" runat="server"></asp:Label>
                    </td>
                    <td align="center" width="420px" style="font-size: 20px;">
                        <asp:Label ID="lblCompany" runat="server"></asp:Label>
                    </td>
                    <td width="140px" align="left">
                        Ph:
                        <asp:Label ID="lblPhone" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        GST#:
                        <asp:Label ID="lblGSTno" runat="server"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:Label ID="lblAddress" runat="server"></asp:Label>
                    </td>
                    <td align="left">
                        Date:
                        <asp:Label ID="lblBillDate" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td align="center">
                        <asp:Label ID="lblCity" runat="server" />
                        -
                        <asp:Label ID="lblPincode" runat="server"></asp:Label>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td align="center">
                        <asp:Label ID="lblState" runat="server"> </asp:Label>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td align="center">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <br />
                        <h5>
                            Outstanding Report For
                            <asp:Label ID="lblSundry" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
            <wc:ReportGridView runat="server" BorderWidth="1" ID="gvLedger" CssClass="gridview"
                GridLines="Both" AlternatingRowStyle-CssClass="even" HeaderStyle-HorizontalAlign="Left"
                AutoGenerateColumns="false" AllowPrintPaging="true" Width="700px"
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
                    <asp:BoundField ItemStyle-HorizontalAlign="Left" DataField="Ledgername" HeaderText="Particulars" />
                    <%--<asp:BoundField ItemStyle-HorizontalAlign="Left" DataField="Mobile" HeaderText="Mobile" />--%>
                    <asp:TemplateField HeaderText="Debit" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
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
                    </asp:TemplateField>
                    <asp:BoundField ItemStyle-HorizontalAlign="Left" DataField="Mobile" HeaderText="Mobile" />
                </Columns>
                <PagerTemplate>
                </PagerTemplate>
                <PageFooterTemplate>
                </PageFooterTemplate>
            </wc:ReportGridView>
            <br />
            <br />
            <!-- Start Outstanding Report March 16 -->
            <table width="600px" border="0" cellpadding="0" cellspacing="2" style="font-family: 'Trebuchet MS';
                font-size: 11px;">
                <tr>
                    <td width="220px" align="right">
                        <b></b>
                    </td>
                    <td width="100px" align="right">
                        <hr />
                        <asp:Label ID="lblDebitSum" runat="server"></asp:Label><hr />
                    </td>
                    <td width="100px" align="right">
                        <hr />
                        <asp:Label ID="lblCreditSum" runat="server"></asp:Label><hr />
                    </td>
                    <td width="80px" align="right">
                        <b></b>
                    </td>
                </tr>
                <tr>
                    <td width="220px" align="right">
                        <b></b>
                    </td>
                    <td align="right" width="100px">
                        <hr />
                        <asp:Label ID="lblDebitDiff" runat="server"></asp:Label><hr />
                    </td>
                    <td align="right" width="100px">
                        <hr />
                        <asp:Label ID="lblCreditDiff" runat="server"></asp:Label><hr />
                    </td>
                    <td align="right" width="80px">
                        <b></b>
                    </td>
                </tr>
            </table>
            <!-- End Outstanding Report March 16 -->
            <br />
            <br />
        </div>
    </div>
    </form>
</body>
</html>
