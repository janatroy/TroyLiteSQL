<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TimeSheetReports1.aspx.cs"
    Inherits="TimeSheetReports1" %>

<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Time Sheet Report</title>
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
</head>
<body>
    <form id="form1" runat="server">
    <br />
    <div align="center">
    <div runat="server" id="div1">
        <table cellpadding="2" cellspacing="2" width="450px" border="0" style="border: 1px solid blue; background-color:White;
            ">
            <tr>
                <td class="subHeadFont2" colspan="4" rowspan="1">
                    Time Sheet Report
                </td>
            </tr>
            <tr>
                <td class="ControlLabel2" style="width:40%">
                    Start Date
                </td>
                <td class="ControlTextBox3"  style="width:25%">
                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="cssTextBox" Width="99px"
                        MaxLength="10" />
                </td>
                <td  style="width:15%">
                    <script type="text/javascript" language="JavaScript">
                        new tcal({ 'formname': 'form1', 'controlname': 'txtStartDate' });</script>
                </td>
                <td align="left"  style="width:20%">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtStartDate"
                        Display="None" ErrorMessage="Please Enter Start Date" CssClass="lblFont"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="ControlLabel2"  style="width:40%">
                    End Date
                </td>
                <td class="ControlTextBox3"  style="width:25%">
                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="cssTextBox" Width="100px" MaxLength="10" />
                </td>
                <td style="width:15%">
                    <script type="text/javascript" language="JavaScript">
                        new tcal({ 'formname': 'form1', 'controlname': 'txtEndDate' });</script>
                </td>
                <td align="left" style="width:20%">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEndDate"
                        Display="None" ErrorMessage="Please Enter The End Date" CssClass="lblFont"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtStartDate"
                        ControlToValidate="txtEndDate" Display="None" ErrorMessage="Start Date Should Be Less Than the End Date"
                        CssClass="lblFont" Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="ControlLabel2" style="width:40%">
                    Executive Name
                </td>
                <td class="ControlDrpBorder" style="width:25%">
                    <asp:DropDownList ID="drpIncharge" TabIndex="4" Enabled="True" AppendDataBoundItems="true" style="border: 1px solid #90c9fc" height="26px"
                        runat="server" Width="100%"  CssClass="drpDownListMedium" BackColor = "#90c9fc" DataTextField="empFirstName"
                        DataValueField="empno">
                        <asp:ListItem Text="ALL" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td  style="width:15%">
                </td>
                <td  style="width:20%">
                </td>
            </tr>
            <tr>
                <td class="ControlLabel2" style="width:40%">
                    Approved
                </td>
                <td class="ControlDrpBorder" style="width:25%">
                    <asp:DropDownList ID="drpsApproved" runat="server" Width="100%" style="border: 1px solid #90c9fc" height="26px"  CssClass="drpDownListMedium" BackColor = "#90c9fc"
                        AppendDataBoundItems="True" EnableTheming="False">
                        <asp:ListItem Value="">ALL</asp:ListItem>
                        <asp:ListItem Value="NO">No</asp:ListItem>
                        <asp:ListItem Value="YES">Yes</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td  style="width:15%">
                </td>
                <td  style="width:20%">
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <table width="100%">
                        <tr>
                            <td width="30%">
                                
                            </td>
                            <td width="20%">
                                 <asp:Button ID="Button2" runat="server" OnClick="btnRep_Click" CssClass="NewReport6"
                                    EnableTheming="false" />
                            </td>
                            <td width="20%">
                                <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" CssClass="exportexl6"
                                    EnableTheming="false" />
                            </td>
                            
                            <td width="30%">
                                <asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="true" ShowSummary="false" />
                            </td>
                        </tr>
                    </table>
                </td>
             </tr>
        </table>
        </div>
        <div id="divmain" runat="server" visible="false">
            <table width="700px">
                    <tr>
                        <td colspan="4">
                            <table width="100%">
                                <tr>
                                    <td style="width:40%">

                                    </td>
                                    <td style="width:20%">
                                        <input type="button" class="printbutton6" id="Button1" runat="Server" onclick="javascript:CallPrint('divPrint')" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" />
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
        <div id="divPrint" style="font-family: 'Trebuchet MS'; font-size: 11px;" runat="server">
            
            <wc:ReportGridView runat="server" BorderWidth="1" ID="gvTSE" CssClass="gridview"
                GridLines="Both" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                PrintPageSize="30" AllowPrintPaging="true" Width="90%" Style="font-family: 'Trebuchet MS';
                font-size: 11px;" OnRowDataBound="gvTSE_RowDataBound">
                <HeaderStyle CssClass="ReportHeadataRow" />
                <RowStyle CssClass="ReportdataRow" />
                <AlternatingRowStyle CssClass="ReportAltdataRow" />
                <FooterStyle CssClass="ReportFooterRow" />
                <PageHeaderTemplate>
                    <br />
                    <br />
                </PageHeaderTemplate>
                <Columns>
                    <asp:TemplateField HeaderText=" Date " ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label Visible="true" ID="lblTSEDate" runat="server" Text='<%# Eval("TSDate","{0:dd/MM/yyyy}")%>' />
                        </ItemTemplate>
                        <ItemStyle Width="5%"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Employee Name" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label Visible="true" ID="lblEmpNo" runat="server" Text='<%# Eval("empFirstName")%>' />
                        </ItemTemplate>
                        <ItemStyle Width="5%"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Before 8" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label Visible="true" ID="lblbefore8" runat="server" Text='<%# Eval("before8")%>' />
                        </ItemTemplate>
                        <ItemStyle Width="5%"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="8 TO 9" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label Visible="true" ID="lbl8to9" runat="server" Text='<%# Eval("8to9")%>' />
                        </ItemTemplate>
                        <ItemStyle Width="5%"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="9 TO 10" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label Visible="true" ID="lbl9to10" runat="server" Text='<%# Eval("9to10")%>' />
                        </ItemTemplate>
                        <ItemStyle Width="5%"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="10 TO 11" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label Visible="true" ID="lbl10to11" runat="server" Text='<%# Eval("10to11")%>' />
                        </ItemTemplate>
                        <ItemStyle Width="5%"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="11 TO 12" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label Visible="true" ID="lbl11to12" runat="server" Text='<%# Eval("11to12")%>' />
                        </ItemTemplate>
                        <ItemStyle Width="5%"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="12 TO 1" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label Visible="true" ID="lbl12to1" runat="server" Text='<%# Eval("12to1")%>' />
                        </ItemTemplate>
                        <ItemStyle Width="5%"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="1 TO 2" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label Visible="true" ID="lbl1pmto2" runat="server" Text='<%# Eval("1pmto2")%>' />
                        </ItemTemplate>
                        <ItemStyle Width="5%"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="2 TO 3" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label Visible="true" ID="lbl2pmto3" runat="server" Text='<%# Eval("2pmto3")%>' />
                        </ItemTemplate>
                        <ItemStyle Width="5%"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="3 TO 4" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label Visible="true" ID="lbl3pmto4" runat="server" Text='<%# Eval("3pmto4")%>' />
                        </ItemTemplate>
                        <ItemStyle Width="5%"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="4 TO 5" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label Visible="true" ID="lbl4pmto5" runat="server" Text='<%# Eval("4pmto5")%>' />
                        </ItemTemplate>
                        <ItemStyle Width="5%"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="5 TO 6" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label Visible="true" ID="lbl5pmto6" runat="server" Text='<%# Eval("5pmto6")%>' />
                        </ItemTemplate>
                        <ItemStyle Width="5%"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="6 TO 7" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label Visible="true" ID="lbl6pmto7" runat="server" Text='<%# Eval("6pmto7")%>' />
                        </ItemTemplate>
                        <ItemStyle Width="5%"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="7 TO 8" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label Visible="true" ID="lbl7pmto8" runat="server" Text='<%# Eval("7pmto8")%>' />
                        </ItemTemplate>
                        <ItemStyle Width="5%"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="8 TO 9" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label Visible="true" ID="lbl8pmto9" runat="server" Text='<%# Eval("8pmto9")%>' />
                        </ItemTemplate>
                        <ItemStyle Width="5%"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="9 TO 10" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label Visible="true" ID="lbl9pmto10" runat="server" Text='<%# Eval("9pmto10")%>' />
                        </ItemTemplate>
                        <ItemStyle Width="5%"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="After 10" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label Visible="true" ID="lblafter10" runat="server" Text='<%# Eval("after10")%>' />
                        </ItemTemplate>
                        <ItemStyle Width="5%"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Approved" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label Visible="true" ID="lblApproved" runat="server" Text='<%# Eval("Approved")%>' />
                        </ItemTemplate>
                        <ItemStyle Width="5%"></ItemStyle>
                    </asp:TemplateField>
                </Columns>
                <PagerTemplate>
                </PagerTemplate>
                <PageFooterTemplate>
                </PageFooterTemplate>
                <AlternatingRowStyle CssClass="even"></AlternatingRowStyle>
            </wc:ReportGridView>
        </div>
        </div>
    </div>
    </form>
</body>
</html>
