<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LedgerReport1.aspx.cs" Inherits="LedgerReport1" %>

<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <title>Ledger Report</title>
    <link href="App_Themes/DefaultTheme/calendar.css" rel="stylesheet" type="text/css" />
    <link rel="Stylesheet" href="App_Themes/DefaultTheme/DefaultTheme.css" />
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
    <form id="form1" method="post" runat="server">
    <br />
    <div align="center" id="div1" runat="server">
        <table cellpadding="1" cellspacing="2" width="450px" border="0" style="border: 1px solid blue; background-color:White;
            text-align: left">
            <tr>
                <td class="subHeadFont2" colspan="4">
                    Ledger Report
                </td>
            </tr>
            <tr style="height:6px">
            </tr>
            <tr>
                <td style="width:35%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                    Start Date
                </td>
                <td class="ControlTextBox3" style="width:25%">
                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="cssTextBox" Width="100px"
                        MaxLength="10" />
                </td>
                <td style="width:10%">
                    <script type="text/javascript" language="JavaScript">
                        new tcal({ 'formname': 'form1', 'controlname': 'txtStartDate' });</script>
                </td>
                <td style="width:30%">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtStartDate"
                        Display="None" ErrorMessage="Please Enter Start Date" CssClass="lblFont"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td  style="width:30%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                    End Date
                </td>
                <td class="ControlTextBox3" style="width:25%">
                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="cssTextBox" Width="100px" MaxLength="10" />
                </td>
                <td style="width:10%">
                    <script type="text/javascript" language="JavaScript">
                        new tcal({ 'formname': 'form1', 'controlname': 'txtEndDate' });</script>
                </td>
                <td style="width:15%">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEndDate"
                        Display="None" ErrorMessage="Please Enter The End Date" CssClass="lblFont"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtStartDate"
                        ControlToValidate="txtEndDate" Display="None" ErrorMessage="Start Date Should Be Less Than the End Date"
                        CssClass="lblFont" Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td  style="width:30%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                    Heading
                </td>
                <td align="left" class="ControlDrpBorder" style="width:25%">
                    <asp:DropDownList ID="drpHeading" runat="server" Width="100%" DataTextField="Heading"
                        AppendDataBoundItems="true" AutoPostBack="true" DataValueField="HeadingID" CssClass="drpDownListMedium" BackColor = "#90c9fc"
                        OnSelectedIndexChanged="drpHeading_SelectedIndexChanged" style="border: 1px solid #90c9fc" height="26px">
                        <asp:ListItem Text=" -- All -- " Value="0"></asp:ListItem>
                    </asp:DropDownList>
                    
                </td>
                <td style="width:15%">
                    </td>
                    <td style="width:10%">
                    </td>
            </tr>
            <tr>
                <td  style="width:30%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                    Ledger Group
                </td>
                <td align="left" class="ControlDrpBorder" style="width:25%">
                    <asp:DropDownList ID="drpGroup" runat="server" Width="100%" DataTextField="GroupName"
                        AppendDataBoundItems="true" AutoPostBack="true" DataValueField="GroupID" CssClass="drpDownListMedium" BackColor = "#90c9fc"
                        OnSelectedIndexChanged="drpGroupName_SelectedIndexChanged" style="border: 1px solid #90c9fc" height="26px">
                        <asp:ListItem Text=" -- All -- " Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="width:10%">
                    </td>
                    <td style="width:15%">
                    </td>
            </tr>
            <tr>
                <td  style="width:30%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                    Ledger Name
                </td>
                <td align="left" class="ControlDrpBorder" style="width:25%">
                    <asp:DropDownList ID="drpLedgerName" runat="server" Width="100%" DataTextField="LedgerName" CssClass="drpDownListMedium" BackColor = "#90c9fc"
                        AppendDataBoundItems="true" AutoPostBack="true" DataValueField="LedgerID"  style="border: 1px solid #90c9fc" height="26px"
                        OnSelectedIndexChanged="drpLedgerName_SelectedIndexChanged">
                        <asp:ListItem Text=" -- All -- " Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="width:15%">
                    </td>
                    <td style="width:10%">
                    </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td align="left">
                    <div id="dvSalesPurchase" runat="server" visible="false">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lblSP" runat="server" CssClass="lblFont"></asp:Label>
                                    <asp:CheckBox ID="chkSP" runat="server" />
                                </td>
                                <td>
                                    <asp:Label ID="lblSPR" runat="server" CssClass="lblFont"></asp:Label>
                                    <asp:CheckBox ID="chkSPR" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td  style="width:30%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                    Display Order
                    <br />
                    (For Date)
                </td>
                <td align="left" class="ControlDrpBorder" style="width:25%">
                    <asp:DropDownList ID="drpOrder" style="border: 1px solid #90c9fc" height="26px" runat="server" Width="100%" CssClass="drpDownListMedium" BackColor = "#90c9fc">
                        <asp:ListItem Value="0">Increasing</asp:ListItem>
                        <asp:ListItem Value="1">Decreasing</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="width:10%">
                    </td>
                    <td style="width:15%">
                    </td>
            </tr>
            
            <tr>
                <td style="width:25%">
                    <asp:CheckBox ID="chkSummary" runat="server" Text="Summary" />
                </td>
                <td colspan="3">
                    <table width="75%">
                        <tr>
                            <td style="width:20%">

                            </td>
                            <td style="width:19%">
                                <asp:Button ID="btnReport" CssClass="NewReport6" EnableTheming="false" runat="server"
                                OnClick="btnReport_Click"  />
                            </td>
                            <td style="width:19%">
                                <asp:Button ID="btndetails" CssClass="exportexl6" EnableTheming="false" runat="server"
                                    OnClick="btndetails_Click" />
                            </td>
                            <td style="width:17%">
                                <asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="true" ShowSummary="false" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <%--<tr>
                
                <td colspan="3">
                    <table width="100%">
                        <tr>
                            <td style="width:25%">
                                    
                            </td>
                            <td style="width:25%">
                                <input type="button" id="Button1" runat="Server" onclick="javascript:CallPrint('divPrint')"
                                class="printbutton6" visible="False" />
                            </td>
                            <td align="left" style="width:20%">
                                 
                            </td>
                         </tr>
                     </table>
                 </td>
            </tr>--%>
        </table>
    </div>
    <div id="LedgerPanel" runat="server" visible="false"  align="center">
        &nbsp;
        <table width="600px">
            <tr>
                <td colspan="4">
                    <table width="100%">
                        <tr>
                            <td style="width:40%">

                            </td>
                            <td style="width:20%">
                                <input type="button" value="" id="Button2" runat="Server" onclick="javascript:CallPrint('divPrint')"
                                class="printbutton6"  />
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
        <br />
        <div id="divPrint" style="font-family: 'Trebuchet MS'; font-size: 11px;"  align="center">
            
            <table width="600px" border="0" style="font-family: Trebuchet MS; font-size: 14px;">
                <tr>
                    <td width="140px" align="left">
                        TIN#:
                        <asp:Label ID="lblTNGST" runat="server"></asp:Label>
                    </td>
                    <td align="center" width="320px" style="font-size: 20px;">
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
                    <td colspan="3">
                        <br />
                        <h5>
                            Ledger Of
                            <asp:Label ID="lblLedger" runat="server"></asp:Label>
                            <br />
                            Date From
                            <asp:Label ID="lblStartDate" runat="server"> </asp:Label>
                            To
                            <asp:Label ID="lblEndDate" runat="server"> </asp:Label></h5>
                    </td>
                </tr>
            </table>
            <br />
            <div id="divDetails" runat="server" visible="false">
                <wc:ReportGridView runat="server" BorderWidth="1" ID="gvLedger" AutoGenerateColumns="false"
                    AllowPrintPaging="true" Width="700px" Style="font-family: 'Trebuchet MS';
                    font-size: 11px;" OnRowDataBound="gvLedger_RowDataBound">
                    <HeaderStyle CssClass="ReportHeadataRow" />
                    <RowStyle CssClass="ReportdataRow" />
                    <AlternatingRowStyle CssClass="ReportAltdataRow" />
                    <FooterStyle CssClass="ReportFooterRow" />
                    <PageHeaderTemplate>
                        <br />
                        <br />
                    </PageHeaderTemplate>
                    <Columns>
                        <asp:BoundField ItemStyle-Width="5%" DataField="Date" HeaderText="Date" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField ItemStyle-Width="35%" DataField="Particulars" HeaderText="Particulars" />
                        <asp:BoundField ItemStyle-Width="15%" DataField="VoucherType" HeaderText="Voucher Type" />
                        <asp:BoundField ItemStyle-Width="12%" DataField="Debit" HeaderStyle-HorizontalAlign="Right"
                            HeaderText="Debit" DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField ItemStyle-Width="12%" DataField="Credit" HeaderStyle-HorizontalAlign="Right"
                            HeaderText="Credit" DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="Right" />
                        <asp:TemplateField ItemStyle-Width="18%" HeaderText="Balance" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblBalance" runat="server" CssClass="lblFont" Font-Bold="true" ForeColor="Blue"
                                    Text="0.00"> </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField ItemStyle-Width="10%" DataField="LedgerID" Visible="false" />
                        <%--<asp:TemplateField >
                <ItemTemplate >
                <asp:Label ID="obDr" Visible="false" runat="server"></asp:Label>
                </ItemTemplate> 
                </asp:TemplateField> 
               <asp:TemplateField >
                <ItemTemplate >
                <asp:Label ID="obCr" Visible="false"  runat="server"></asp:Label>
                </ItemTemplate> 
                </asp:TemplateField> --%>
                    </Columns>
                    <PagerTemplate>
                    </PagerTemplate>
                    <PageFooterTemplate>
                    </PageFooterTemplate>
                </wc:ReportGridView>
            </div>
            <br />
            <div id="divSummary" runat="server" visible="false">
                <br />
                <b>Summary :</b>
                <br />
                <wc:ReportGridView runat="server" BorderWidth="1" ID="gvSummary" CssClass="gridview"
                    GridLines="Both" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                    AllowPrintPaging="true" Width="700px" Style="font-family: 'Trebuchet MS';
                    font-size: 11px;" OnRowDataBound="gvSummary_RowDataBound">
                    <HeaderStyle CssClass="ReportHeadataRow" />
                    <RowStyle CssClass="ReportdataRow" />
                    <AlternatingRowStyle CssClass="ReportAltdataRow" />
                    <FooterStyle CssClass="ReportFooterRow" />
                    <PageHeaderTemplate>
                        <br />
                        <br />
                    </PageHeaderTemplate>
                    <Columns>
                        <asp:BoundField ItemStyle-Width="15%" DataField="Ledger" HeaderText="Ledger" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField ItemStyle-Width="12%" DataField="Debit" HeaderStyle-HorizontalAlign="Right"
                            HeaderText="Debit" DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField ItemStyle-Width="12%" DataField="Credit" HeaderStyle-HorizontalAlign="Right"
                            HeaderText="Credit" DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="Right" />
                        <asp:TemplateField ItemStyle-Width="10%" HeaderText="Balance" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblBalance" runat="server" CssClass="lblFont" Font-Bold="true" ForeColor="Blue"
                                    Text="0.00"> </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField ItemStyle-Width="10%" DataField="LedgerID" Visible="false" />
                    </Columns>
                    <PagerTemplate>
                    </PagerTemplate>
                    <PageFooterTemplate>
                    </PageFooterTemplate>
                </wc:ReportGridView>
            </div>
            <!-- Start Ledger Report March 16 -->
            <table width="600px" border="0" cellspacing="0" cellpadding="1" style="font-family: 'Trebuchet MS';
                font-size: 11px;">
                <tr>
                    <td width="280px" align="right">
                        <b>Opening Balance :</b>
                    </td>
                    <td width="80px">
                        &nbsp;
                    </td>
                    <td width="80px" align="right">
                        <hr />
                        <asp:Label ID="lblOBDR" runat="server"></asp:Label><hr />
                    </td>
                    <td width="80px" align="right">
                        <hr />
                        <asp:Label ID="lblOBCR" runat="server"></asp:Label><hr />
                    </td>
                    <td width="80px">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <b>Total :</b>
                    </td>
                    <td width="80px">
                        &nbsp;
                    </td>
                    <td align="right">
                        <hr />
                        <asp:Label ID="lblDebitSum" runat="server"></asp:Label><hr />
                    </td>
                    <td align="right">
                        <hr />
                        <asp:Label ID="lblCreditSum" runat="server"></asp:Label><hr />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <b>Current Balance :</b>
                    </td>
                    <td width="80px">
                        &nbsp;
                    </td>
                    <td align="right">
                        <hr />
                        <asp:Label ID="lblDebitDiff" runat="server"></asp:Label><hr />
                    </td>
                    <td align="right">
                        <hr />
                        <asp:Label ID="lblCreditDiff" runat="server"></asp:Label><hr />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <b>Closing Balance :</b>
                    </td>
                    <td width="80px">
                        &nbsp;
                    </td>
                    <td align="right">
                        <hr />
                        <asp:Label ID="lblClosDr" runat="server"></asp:Label><hr />
                    </td>
                    <td align="right">
                        <hr />
                        <asp:Label ID="lblClosCr" runat="server"></asp:Label><hr />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
            <!-- End Ledger Report March 16 -->
            <br />
            <br />
        </div>
    </div>
    </form>
</body>
</html>
