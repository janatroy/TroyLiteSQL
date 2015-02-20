<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LeadManagementReport.aspx.cs"
    Inherits="PurchaseSummaryReport1" %>

<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <title>Lead Management Report</title>
    <link href="App_Themes/DefaultTheme/calendar.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="Scripts/calendar_eu.js"></script>
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

    </script>
    <link rel="Stylesheet" href="App_Themes/DefaultTheme/DefaultTheme.css" />
</head>
<body style="font-family: 'Trebuchet MS'; font-size: 11px;">
    <form id="form1" runat="server">
        <div align="center">
            <br />
            <div id="div1" runat="server">

                <table cellpadding="0" cellspacing="1" border="0" style="border: 1px solid Blue; background-color: White; width: 50%;">

                    <tr>
                        <td colspan="3" class="subHeadFont2">Lead Management Report
                        </td>
                    </tr>
                    <tr style="height: 6px">
                    </tr>
                    <tr>
                        <td style="width: 25%; font-family: 'ARIAL'; font-size: 11px; font-weight: normal; color: #000000; text-align: right; text-decoration: none; padding-right: 5px; padding-left: 5px; padding-top: 5px;" height="27px">Start Date
                        </td>
                        <td align="left" style="width: 25%" class="ControlTextBox3">
                            <asp:TextBox ID="txtStartDate" CssClass="cssTextBox" MaxLength="10"
                                runat="server" Width="100px" />

                        </td>
                        <td align="left" style="width: 15%">
                            <script type="text/javascript" language="JavaScript">
                                new tcal({ 'formname': 'form1', 'controlname': 'txtStartDate' });</script>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtStartDate"
                                Display="None" CssClass="lblFont" ErrorMessage="Please Enter Start Date"></asp:RequiredFieldValidator>
                        </td>

                    </tr>
                    <tr>
                        <td style="width: 25%; font-family: 'ARIAL'; font-size: 11px; font-weight: normal; color: #000000; text-align: right; text-decoration: none; padding-right: 5px; padding-left: 5px; padding-top: 5px;" height="27px">End Date
                        </td>
                        <td align="left" width="25%" class="ControlTextBox3">
                            <asp:TextBox ID="txtEndDate" CssClass="cssTextBox" MaxLength="10" runat="server" Width="100px" />

                        </td>
                        <td align="left" style="width: 15%">
                            <script type="text/javascript" language="JavaScript">
                                new tcal({ 'formname': 'form1', 'controlname': 'txtEndDate' });</script>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEndDate"
                                Display="None" ErrorMessage="Please Enter The End Date" CssClass="lblFont"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtStartDate"
                                ControlToValidate="txtEndDate" Display="None" ErrorMessage="Start Date Should Be Less Than the End Date"
                                CssClass="lblFont" Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
                        </td>

                    </tr> 
                    <tr>
                        <td colspan="3">
                            <table width="100%">
                                <tr>
                                    <td style="width: 31%"></td>
                                    <td style="width: 20%">
                                        <asp:Button ID="btnReport" runat="server" CssClass="NewReport6" EnableTheming="false" />

                                    </td>

                                    <td style="width: 20%">
                                        <asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="true" ShowSummary="false" />
                                        <asp:Button ID="Button2" runat="server" CssClass="exportexl6"
                                            EnableTheming="false" />
                                    </td>
                                    <td style="width: 25%"></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="lblErr" runat="server" CssClass="errorMsg"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divmain" runat="server" visible="false">
                <table width="700px">
                    <tr>
                        <td colspan="3">
                            <table width="100%">
                                <tr>
                                    <td style="width: 40%"></td>
                                    <td style="width: 20%">
                                        <input type="button" id="Button1" runat="Server" onclick="javascript: CallPrint('divPrint')"
                                            class="printbutton6" style="padding-left: 25px;" />
                                    </td>
                                    <td style="width: 10%">
                                        <asp:Button ID="btndet" CssClass="GoBack" EnableTheming="false" runat="server"
                                            Visible="False" />
                                    </td>
                                    <td style="width: 30%"></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <br />
                <div id="divPrint" runat="server" visible="false" style="font-family: 'Trebuchet MS'; font-size: 11px;">


                    <table width="700px" border="0" style="font-family: Trebuchet MS; font-size: 14px;">
                        <tr>
                            <td width="140px" align="left">TIN#:
                            <asp:Label ID="lblTNGST" runat="server"></asp:Label>
                            </td>
                            <td align="center" width="420px" style="font-size: 20px;">
                                <asp:Label ID="lblCompany" runat="server"></asp:Label>
                            </td>
                            <td width="140px" align="left">Ph:
                            <asp:Label ID="lblPhone" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">GST#:
                            <asp:Label ID="lblGSTno" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lblAddress" runat="server"></asp:Label>
                            </td>
                            <td align="left">Date:
                            <asp:Label ID="lblBillDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                            <td align="center">
                                <asp:Label ID="lblCity" runat="server" />
                                -
                            <asp:Label ID="lblPincode" runat="server"></asp:Label>
                            </td>
                            <td>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                            <td align="center">
                                <asp:Label ID="lblState" runat="server"> </asp:Label>
                            </td>
                            <td>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                            <td align="center">&nbsp;
                            </td>
                            <td>&nbsp;
                            </td>
                        </tr>
                    </table>
                    <wc:ReportGridView runat="server" BorderWidth="1" ID="gvMain" GridLines="Both" AlternatingRowStyle-CssClass="even"
                        AutoGenerateColumns="false" AllowPrintPaging="true" Width="80%"
                        EmptyDataText="No Rows Found." CellPadding="2" Style="font-family: 'Trebuchet MS'; font-size: 11px;"
                        ShowFooter="True" ShowHeader="True"
                        FooterStyle-CssClass="lblFont">
                        <FooterStyle CssClass="ReportFooterRow" Font-Bold="true" />
                        <HeaderStyle CssClass="ReportHeadataRow" />
                        <RowStyle CssClass="ReportdataRow" />
                        <AlternatingRowStyle CssClass="ReportAltdataRow" />
                        <EmptyDataRowStyle CssClass="HeadataRow" Font-Bold="true" Height="25px" />
                        <Columns>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                                HeaderText="Lead No">
                                <ItemTemplate>
                                    <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblNetRate"
                                        runat="server" Text='<%# Eval("Lead_No") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top"
                                HeaderText="Lead Name">
                                <ItemTemplate>
                                    <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblNetRate"
                                        runat="server" Text='<%# Eval("Lead_Name") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top"
                                HeaderText="Customer Name">
                                <ItemTemplate>
                                    <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblNetRate"
                                        runat="server" Text='<%# Eval("BP_Name") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top"
                                HeaderText="Address">
                                <ItemTemplate>
                                    <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblNetRate"
                                        runat="server" Text='<%# Eval("Address") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top"
                                HeaderText="Mobile">
                                <ItemTemplate>
                                    <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblNetRate"
                                        runat="server" Text='<%# Eval("Mobile") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top"
                                HeaderText="Telephone">
                                <ItemTemplate>
                                    <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblNetRate"
                                        runat="server" Text='<%# Eval("Telephone") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top"
                                HeaderText="Record Status">
                                <ItemTemplate>
                                    <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblNetRate"
                                        runat="server" Text='<%# Eval("Doc_Status") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top"
                                HeaderText="Closing Date">
                                <ItemTemplate>
                                    <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblNetRate"
                                        runat="server" Text='<%# ProcessMyDataItem(Eval("Closing_Date", "{0:d}")) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top"
                                HeaderText="Employee Name">
                                <ItemTemplate>
                                    <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblNetRate"
                                        runat="server" Text='<%# Eval("Emp_Name") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top"
                                HeaderText="Start Date">
                                <ItemTemplate>
                                    <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblNetRate"
                                        runat="server" Text='<%# Eval("Start_Date", "{0:d}") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top"
                                HeaderText="Lead Status">
                                <ItemTemplate>
                                    <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblNetRate"
                                        runat="server" Text='<%# Eval("Lead_Status") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top"
                                HeaderText="Contact Name">
                                <ItemTemplate>
                                    <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblNetRate"
                                        runat="server" Text='<%# Eval("Contact_Name") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top"
                                HeaderText="Predicted Closing date">
                                <ItemTemplate>
                                    <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblNetRate"
                                        runat="server" Text='<%# Eval("Predicted_Closing_Date", "{0:d}") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top"
                                HeaderText="Competitor Name">
                                <ItemTemplate>
                                    <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblNetRate"
                                        runat="server" Text='<%# Eval("Competitor_Name") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top"
                                HeaderText="Lead Activity">
                                <ItemTemplate>
                                    <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblNetRate"
                                        runat="server" Text='<%# Eval("Activity_Name") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top"
                                HeaderText="Lead Activity Date">
                                <ItemTemplate>
                                    <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblNetRate"
                                        runat="server" Text='<%# Eval("Activity_Date", "{0:d}") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top"
                                HeaderText="Lead Location">
                                <ItemTemplate>
                                    <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblNetRate"
                                        runat="server" Text='<%# Eval("Activity_Location") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top"
                                HeaderText="Follow-up Activity">
                                <ItemTemplate>
                                    <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblNetRate"
                                        runat="server" Text='<%# Eval("Next_Activity") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top"
                                HeaderText="Follow-up Activity Date">
                                <ItemTemplate>
                                    <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblNetRate"
                                        runat="server" Text='<%# Eval("NextActivity_Date", "{0:d}") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </wc:ReportGridView>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
