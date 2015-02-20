<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalesPurCompReport.aspx.cs" Inherits="SalesPurCompReport"
    Title="Sales Purchase Comparision Report" %>

<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <title>Sales Purchase Comparision</title>
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
    <%--<link rel="Stylesheet" href="App_Themes/DefaultTheme/DefaultTheme.css" />--%>
    <!-- March 17  -->
    <%--<link rel="stylesheet" href="App_Themes/DefaultTheme/calendar.css" />--%>
    <%--<script type="text/javascript" language="JavaScript" src="Scripts/calendar_eu.js"></script>--%>
    <!-- March 17  -->
</head>
<body style="font-family: 'Trebuchet MS'; font-size: 11px;">
    <form id="form1" runat="server" method="post">
    <div style="min-height: 300px" align="center">
        <br />
        <div  align="center">
        <table cellpadding="2" cellspacing="2" border="0" width="480px"  style="border: 1px solid blue; background-color:White;">
            <tr>
                <td colspan="4" class="subHeadFont">
                    Sales Purchase Comparision
                </td>
            </tr>
            <tr>
                <td class="ControlLabel" style="width:30%">
                    Start Date
                </td>
                <td class="ControlTextBox3" style="width:25%">
                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="cssTextBox" Width="100px"
                        MaxLength="10" />
                </td>
                <td align="left" style="width:10%">
                    <script type="text/javascript" language="JavaScript">
                        new tcal({ 'formname': 'form1', 'controlname': 'txtStartDate' });</script>
                </td>
                <td align="left" style="width:20%">
                    <asp:RequiredFieldValidator class="lblFont" CssClass="lblFont" ID="RequiredFieldValidator1"
                        runat="server" ControlToValidate="txtStartDate" Display="None" ErrorMessage="Please Enter Start Date"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="ControlLabel" style="width:30%">
                    End Date
                </td>
                <td class="ControlTextBox3" style="width:25%">
                    <asp:TextBox ID="txtEndDate" CssClass="cssTextBox" runat="server" Width="100px" MaxLength="10" />
                </td>
                <td align="left" style="width:10%">
                    <script type="text/javascript" language="JavaScript">
                        new tcal({ 'formname': 'form1', 'controlname': 'txtEndDate' });</script>
                </td>
                <td align="left" style="width:20%">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEndDate"
                        Display="None" CssClass="lblFont" ErrorMessage="Please Enter The End Date"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" CssClass="lblFont" ControlToCompare="txtStartDate"
                        ControlToValidate="txtEndDate" Display="None" ErrorMessage="Start Date Should Be Less Than the End Date"
                        Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <table width="100%">
                        <tr>
                            <td style="width: 25%;">
                            </td>
                            <td style="width: 20%;">
                                <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" CssClass="generatebutton"
                                    EnableTheming="false" />
                    
                    
                            </td>
                            <td style="width: 25%;">
                                <input type="button" id="Button2" runat="Server" onclick="javascript:CallPrint('divPrint')"
                                    class="printbutton" />
                            </td>
                            <td style="width: 20%;">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <%--<tr>
                <td colspan="4">
                    <table width="100%">
                        <tr>
                            <td style="width:33%">
                            </td>
                            <td style="width:15%">
                                <asp:Button ID="btndetails" CssClass="exportExcel" EnableTheming="false" runat="server"
                                    OnClick="btndetails_Click" />
                            </td>
                            <td style="width:30%">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>--%>
        </table>
        <%--<asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="true" ShowSummary="false" />--%>
        </div>
        <br />
        <div id="dvBalanceSheet" visible="false" style="font-family: 'Trebuchet MS'; width: 700px;
            font-size: 11px;" runat="Server">
            <div id="divPrint" style="font-family: 'Trebuchet MS'; font-size: 11px;">
                <%--<table width="700px" border="0" style="font-family: Trebuchet MS; font-size: 14px;">
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
                </table>--%>
                <table width="450px" border="0" cellpadding="1" cellspacing="2" style="font-family: 'Trebuchet MS';
                    font-size: 11px;">
                    <tr>
                        <td colspan="2">
                            <br />
                            <h5>
                                Balance Sheet Report From
                                <asp:Label ID="lblStartDate" runat="server"> </asp:Label>
                                To
                                <asp:Label ID="lblEndDate" runat="server"> </asp:Label></h5>
                        </td>
                    </tr>
                </table>

                <table width="100%" style="height:80%" cellpadding="3" cellspacing="3" style="border: 1px solid black;">
                    <tr style="height:5%">
                        <td>
                            <b>Liability</b>
                        </td>
                        <td>
                            <b>Asset</b>
                        </td>
                    </tr>
                    <tr style="height:100%">
                        <%--<Liablity Side  ->--%>
                        <td valign="top">
                            <wc:ReportGridView runat="server" BorderWidth="0" ID="gvLiabilityBalance" GridLines="None"
                                AutoGenerateColumns="false"
                                AllowPrintPaging="true" Width="100%" CellPadding="2" DataKeyNames="HeadingID"
                                Style="font-family: 'Trebuchet MS'; font-size: 11px;" OnRowDataBound="gvLiabilityBalance_RowDataBound"
                                ShowFooter="false" ShowHeader="false">
                                <PageHeaderTemplate>
                                    <br />
                                    <br />
                                </PageHeaderTemplate>
                                <Columns>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="75%">
                                        <ItemTemplate>
                                            <a href="javascript:switchViews('div<%# Eval("HeadingID") %>', 'imgdiv<%# Eval("HeadingID") %>');"
                                                style="text-decoration: none;">
                                                <img id="imgdiv<%# Eval("HeadingID") %>" alt="Show" border="0" />
                                            </a>
                                            <%--<a style="text-decoration:none" href='BalanceSheetLevel2.aspx?HeadingName=<%# Eval("HeadingName") %>&HeadingID=<%# Eval("HeadingID") %>'><asp:Label style="font-family:'Trebuchet MS'; font-size:11px;  " ID="lblparticulars" runat="server" Text = '<%# Eval("HeadingName") %>' /></a>--%>
                                            <%# Eval("HeadingName") %>
                                            <br />
                                            <div id="div<%# Eval("HeadingID") %>" style="display: none; position: relative; left: 25px;">
                                                <wc:ReportGridView runat="server" BorderWidth="0" ID="gvLiaGroup" GridLines="None"
                                                    AutoGenerateColumns="false"
                                                    AllowPrintPaging="true" CssClass="lblFont" Width="100%" CellPadding="2" DataKeyNames="GroupID"
                                                    Style="font-family: 'Trebuchet MS'; font-size: 11px;" OnRowDataBound="gvLiaGroup_RowDataBound"
                                                    ShowFooter="false" ShowHeader="false">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <a href="javascript:switchViews('dv<%# Eval("GroupID") %>', 'imdiv<%# Eval("GroupID") %>');"
                                                                    style="text-decoration: none;">
                                                                    <img id="imdiv<%# Eval("GroupID") %>" alt="Show" border="0" />
                                                                </a>
                                                                <%# Eval("GroupName") %>
                                                                <br />
                                                                <div id="dv<%# Eval("GroupID") %>" style="display: none; position: relative; left: 25px;">
                                                                    <wc:ReportGridView runat="server" BorderWidth="1" ID="gvLiaLedger"
                                                                        GridLines="Both" AutoGenerateColumns="false"
                                                                        Width="100%" Style="font-family: 'Trebuchet MS'; font-size: 11px;">
                                                                                                            
                                                                        <PageHeaderTemplate>
                                                                            <br />
                                                                            <br />
                                                                        </PageHeaderTemplate>
                                                                        <Columns>
                                                                            <asp:BoundField DataField="Folionumber" ItemStyle-HorizontalAlign="Left" HeaderText="L.FNO" />
                                                                            <asp:BoundField DataField="LedgerName" ItemStyle-HorizontalAlign="Left" HeaderText="Ledger Name" />
                                                                            <asp:BoundField DataField="Debit" DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="right"
                                                                                HeaderText="Debit" />
                                                                            <asp:BoundField DataField="Credit" DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="right"
                                                                                HeaderText="Credit" />
                                                                        </Columns>
                                                                    </wc:ReportGridView>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top">
                                                            <ItemTemplate>
                                                                <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblSum" runat="server"
                                                                    Text='<%# Eval("sum","{0:f2}") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </wc:ReportGridView>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                                        ItemStyle-Width="25%">
                                        <ItemTemplate>
                                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblSum" runat="server"
                                                Text='<%# Eval("sum","{0:f2}") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerTemplate>
                                </PagerTemplate>
                                <PageFooterTemplate>
                                    <br />
                                </PageFooterTemplate>
                            </wc:ReportGridView>
                        </td>
                        <%--< Asset Side  ->--%>
                        <td valign="top">
                            <wc:ReportGridView runat="server" BorderWidth="0" ID="gvAssetBalance" GridLines="None"
                                AutoGenerateColumns="false" DataKeyNames="HeadingID"
                                PrintPageSize="23" AllowPrintPaging="true" CssClass="lblFont" Width="100%" CellPadding="2"
                                Style="font-family: 'Trebuchet MS'; font-size: 11px;" OnRowDataBound="gvAssetBalance_RowDataBound"
                                ShowFooter="false" ShowHeader="false">
                                <PageHeaderTemplate>
                                    <br />
                                    <br />
                                </PageHeaderTemplate>
                                <Columns>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <a href="('diva<%# Eval("HeadingID") %>', 'imgadiv<%# Eval("HeadingID") %>');"
                                                style="text-decoration: none;">
                                                <img id="imgadiv<%# Eval("HeadingID") %>" alt="Show" border="0" />
                                            </a>
                                            <%--<a style="text-decoration:none" href='BalanceSheetLevel2.aspx?HeadingName=<%# Eval("HeadingName") %>&HeadingID=<%# Eval("HeadingID") %>'><asp:Label style="font-family:'Trebuchet MS'; font-size:11px;  " ID="lblparticulars" runat="server" Text = '<%# Eval("HeadingName") %>' /></a>--%>
                                            <%# Eval("HeadingName") %>
                                            <br />
                                            <div id="diva<%# Eval("HeadingID") %>" style="display: none; position: relative;
                                                left: 25px;">
                                                <wc:ReportGridView runat="server" BorderWidth="0" ID="gvAssetGroup" GridLines="None"
                                                    AutoGenerateColumns="false" PrintPageSize="23"
                                                    AllowPrintPaging="true" Width="100%" CellPadding="2" DataKeyNames="GroupID"
                                                    Style="font-family: 'Trebuchet MS'; font-size: 11px;" OnRowDataBound="gvAssetGroup_RowDataBound"
                                                    ShowFooter="false" ShowHeader="false" >
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <a href="('dva<%# Eval("GroupID") %>', 'imadiv<%# Eval("GroupID") %>');"
                                                                    style="text-decoration: none;">
                                                                    <img id="imadiv<%# Eval("GroupID") %>" alt="Show" border="0" />
                                                                </a>
                                                                <%# Eval("GroupName") %>
                                                                <br />
                                                                <div id="dva<%# Eval("GroupID") %>" style="display: none; position: relative; left: 25px;">
                                                                    <wc:ReportGridView runat="server" BorderWidth="1" ID="gvAssetLedger"
                                                                        GridLines="Both" AutoGenerateColumns="false"
                                                                        Width="100%" Style="font-family: 'Trebuchet MS'; font-size: 11px;" DataKeyNames="LedgerID">
                                                                        <PageHeaderTemplate>
                                                                            <br />
                                                                            <br />
                                                                        </PageHeaderTemplate>
                                                                        <Columns>
                                                                            <asp:BoundField DataField="Folionumber" ItemStyle-HorizontalAlign="Left" HeaderText="L.FNO" />
                                                                            <asp:BoundField DataField="LedgerName" ItemStyle-HorizontalAlign="Left" HeaderText="Ledger Name" />
                                                                            <asp:BoundField DataField="Debit" ItemStyle-HorizontalAlign="right" DataFormatString="{0:F2}"
                                                                                HeaderText="Debit" />
                                                                            <asp:BoundField DataField="Credit" ItemStyle-HorizontalAlign="right" DataFormatString="{0:F2}"
                                                                                HeaderText="Credit" />
                                                                        </Columns>
                                                                    </wc:ReportGridView>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top">
                                                            <ItemTemplate>
                                                                <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblSum" runat="server"
                                                                    Text='<%# Eval("sum","{0:f2}") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </wc:ReportGridView>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top">
                                        <ItemTemplate>
                                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblSum" runat="server"
                                                Text='<%# Eval("sum","{0:f2}") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerTemplate>
                                </PagerTemplate>
                                <PageFooterTemplate>
                                    <br />
                                </PageFooterTemplate>
                            </wc:ReportGridView>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="left">
                            <a href="ProfitAndLossReport.aspx">Profit & Loss Account</a>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="right">
                            <div id="pnlLib" visible="false" runat="server">
                                <i>Difference in Opening Balance : </i>&nbsp;<asp:Label ID="lblLib" runat="server"
                                    ></asp:Label></div>
                            &nbsp;
                        </td>
                        <td valign="top" align="right">
                            <div id="pnlAst" visible="false" runat="server">
                                <i>Difference in Opening Balance : </i>&nbsp;<asp:Label ID="lblAst" runat="server"
                                   ></asp:Label></div>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="lblCreditTotal" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblDebitTotal" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
