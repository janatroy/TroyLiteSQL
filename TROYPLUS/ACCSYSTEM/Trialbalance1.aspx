<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Trialbalance1.aspx.cs" Inherits="Trialbalance1"
    Title="Trial Balance Report" %>

<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Trailbalance</title>
    <script language="javascript" type="text/javascript">
        function CallPrint(strid) {
            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('', '', 'letf=0,top=0,width=600,height=400,toolbar=0,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();

        }
        function switchViews(obj, imG) {
            var div = document.getElementById(obj);
            var img = document.getElementById(imG);

            if (div.style.display == "none") {
                div.style.display = "inline";


                img.src = "App_Themes/DefaultTheme/Images/minus.gif";

            }
            else {
                div.style.display = "none";
                img.src = "App_Themes/DefaultTheme/Images/plus.gif";

            }
        }

    </script>
    <link rel="Stylesheet" href="App_Themes/DefaultTheme/DefaultTheme.css" />
    <link rel="Stylesheet" href="App_Themes/NewTheme/base.css" />
    <link rel="stylesheet" href="App_Themes/DefaultTheme/calendar.css" />
    <script type="text/javascript" language="JavaScript" src="Scripts/calendar_eu.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <br />
        <div id="Div1"
            runat="Server">
            <table cellpadding="1" cellspacing="2" width="450px" border="0" style="border: 1px solid blue; background-color:White;">
                <tr>
                    <td colspan="4" class="subHeadFont2">
                        Trail Balance Report
                    </td>
                </tr>
                <tr style="height:6px">
                
                </tr>
                <tr>
                    <td style="width:35%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                        Start Date
                    </td>
                    <td width="25%" class="ControlTextBox3">
                        <asp:TextBox ID="txtStartDate" runat="server" CssClass="cssTextBox"
                            MaxLength="10" />
                    </td>
                    <td width="10%">
                        <script type="text/javascript" language="JavaScript">
                            new tcal({ 'formname': 'form1', 'controlname': 'txtStartDate' });</script>
                    </td>
                    <td align="left" width="20%">
                        <asp:RequiredFieldValidator class="lblFont" CssClass="lblFont" ID="RequiredFieldValidator1"
                            runat="server" ControlToValidate="txtStartDate" Display="None" ErrorMessage="Please Enter Start Date"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td  style="width:35%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                        End Date
                    </td>
                    <td  width="25%" class="ControlTextBox3">
                        <asp:TextBox ID="txtEndDate" CssClass="cssTextBox" runat="server" MaxLength="10" />
                    </td>
                    <td width="10%">
                        <script type="text/javascript" language="JavaScript">
                            new tcal({ 'formname': 'form1', 'controlname': 'txtEndDate' });</script>
                    </td>
                    <td align="left" width="20%">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEndDate"
                            Display="None" CssClass="lblFont" ErrorMessage="Please Enter The End Date"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" CssClass="lblFont" ControlToCompare="txtStartDate"
                            ControlToValidate="txtEndDate" Display="None" ErrorMessage="Start Date Should Be Less Than the End Date"
                            Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
                    </td>
                </tr>
                <tr style="height:10px">
                
                </tr>
                <tr>
                    <td colspan="4">
                        <table width="100%">
                            <tr>
                                <td style="width:30%">
                                </td>
                                <td style="width:20%">
                                    <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" CssClass="NewReport6"
                                        EnableTheming="false"  />
                                </td>
                                <td style="width:20%">
                                    <asp:Button ID="btndetails" CssClass="exportexl6" EnableTheming="false" runat="server"
                                        OnClick="btndetails_Click" />
                                </td>
                                <td style="width:30%">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                
            </table>
        </div>
        <asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="true" ShowSummary="false" />
        
        <div id="dvTrail" visible="false" style="font-family: 'Trebuchet MS'; font-size: 11px;"
            runat="Server">
            <table width="700px">
                <tr>
                    <td style="width: 40%;">
                    </td>
                    <td width="20%">
                        <input type="button" id="Button2" runat="Server" onclick="javascript:CallPrint('divPrint')"
                            class="printbutton6" />
                        </td>
                    <td width="20%">
                        <asp:Button ID="btndet" CssClass="GoBack" EnableTheming="false" runat="server"
                                OnClick="btndet_Click" Visible="False" />
                    </td>
                    <td style="width: 30%;">
                    </td>
                </tr>
            </table>
            <br />
            <div id="divPrint" style="font-family: 'Trebuchet MS'; font-size: 11px;">
                
                <table align="center" width="700px" border="0" style="font-family: Trebuchet MS; font-size: 14px;">
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
                </table>
                <table width="700px" cellpadding="3" cellspacing="3">
                    <tr>
                        <td class="lblFont">
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <wc:ReportGridView runat="server" BorderWidth="1" ID="gvTrailBalance" AutoGenerateColumns="false"
                                AllowPrintPaging="true" Width="700px" Style="font-family: 'Trebuchet MS';
                                font-size: 11px;" DataKeyNames="GroupID" OnRowDataBound="gvTrailBalance_RowDataBound"
                                ShowFooter="true" FooterStyle-CssClass="lblFont">
                                <HeaderStyle CssClass="ReportHeadataRow" />
                                <RowStyle CssClass="ReportdataRow" />
                                <AlternatingRowStyle CssClass="ReportAltdataRow" />
                                <FooterStyle CssClass="ReportFooterRow" />
                                <PageHeaderTemplate>
                                    <br />
                                    <br />
                                </PageHeaderTemplate>
                                <Columns>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="80%">
                                        <ItemTemplate>
                                            <a href="javascript:switchViews('dv<%# Eval("GroupID") %>', 'imdiv<%# Eval("GroupID") %>');"
                                                style="text-decoration: none;">
                                                <img id="imdiv<%# Eval("GroupID") %>" alt="Show" border="0" src="App_Themes/DefaultTheme/Images/plus.gif" />
                                            </a>
                                            <%# Eval("GroupName") %>
                                            <div id="dv<%# Eval("GroupID") %>" style="display: none; position: relative; left: 25px;">
                                                <wc:ReportGridView runat="server" BorderWidth="1" ID="gvLiaLedger" CssClass="gridview"
                                                    GridLines="Both" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                                    Width="70%" Style="font-family: 'Trebuchet MS'; font-size: 11px;" DataKeyNames="LedgerID">
                                                    <HeaderStyle CssClass="ReportHeadataRow" />
                                                    <RowStyle CssClass="ReportdataRow" />
                                                    <AlternatingRowStyle CssClass="ReportAltdataRow" />
                                                    <FooterStyle CssClass="ReportFooterRow" />
                                                    <PageHeaderTemplate>
                                                        <br />
                                                        <br />
                                                    </PageHeaderTemplate>
                                                    <Columns>
                                                        <asp:BoundField DataField="Folionumber" ItemStyle-HorizontalAlign="Left" HeaderText="L.FNO" />
                                                        <asp:BoundField DataField="LedgerName" ItemStyle-HorizontalAlign="Left" HeaderText="Ledger Name" />
                                                        <asp:BoundField DataField="Debit" DataFormatString="{0:F2}" ItemStyle-HorizontalAlign="right"
                                                            HeaderText="Debit" />
                                                        <asp:BoundField DataField="Credit" DataFormatString="{0:F2}" ItemStyle-HorizontalAlign="right"
                                                            HeaderText="Credit" />
                                                    </Columns>
                                                </wc:ReportGridView>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Debit" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblDebit" runat="server"
                                                Text='<%# Eval("Debit","{0:f2}") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Credit" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblCredit" runat="server"
                                                Text='<%# Eval("Credit","{0:f2}") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerTemplate>
                                </PagerTemplate>
                                <PageFooterTemplate>
                                    <br />
                                </PageFooterTemplate>
                            </wc:ReportGridView>
                            <%--<h5>Total Outstading Amount:</h5>--%>
                        </td>
                    </tr>
                </table>
                <table width="700px" cellpadding="3" cellspacing="3" class="lblFont">
                    <tr>
                        <td width="80%">
                            &nbsp;
                        </td>
                        <td width="10%" align="right">
                            <asp:Label CssClass="tblLeft" ID="lblDebitTotal" runat="server"></asp:Label>
                        </td>
                        <td width="10%" align="right">
                            <asp:Label CssClass="tblLeft" ID="lblCreditTotal" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
