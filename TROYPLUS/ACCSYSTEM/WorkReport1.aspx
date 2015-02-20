<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WorkReport1.aspx.cs" Inherits="WorkReport1" %>

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
    <style type="text/css">
        .style1
        {
            background-color: #E3E4FA;
            text-align: left;
            color: #736F6E;
            font: 10px 'Trebuchet MS' ,sans-serif;
            vertical-align: middle;
            width: 359px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center" style="min-height: 300px">
        <br />
        <div runat="server" id="div1">
        <table cellpadding="2" cellspacing="2" width="450px" border="0" style="border: 1px solid blue; background-color:White;">
            <tr>
                <td class="subHeadFont2" colspan="5" rowspan="1">
                    Work Report
                </td>
            </tr>
            <tr style="height:7px">
                                                                                </tr>
            <tr>
                <td class="ControlLabel2" style="width:40%">
                    Executive Name
                </td>
                <td class="ControlDrpBorder"  style="width:25%">
                    <asp:DropDownList ID="drpsIncharge" TabIndex="4" Enabled="True" AppendDataBoundItems="true" style="border: 1px solid #90c9fc" height="26px"
                        runat="server" Width="100%" CssClass="drpDownListMedium" BackColor = "#90c9fc"  DataTextField="empFirstName"
                        DataValueField="empno">
                        <asp:ListItem Text="ALL" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="ControlLabel" style="width:35%">
                    
                </td>
            </tr>
            <tr>
                <td class="ControlLabel2"  style="width:40%">
                    Status
                </td>
                <td  style="width:25%" class="ControlDrpBorder">
                    <asp:DropDownList ID="drpsStatus" runat="server" Width="100%" style="border: 1px solid #90c9fc" height="26px" CssClass="drpDownListMedium" BackColor = "#90c9fc" >
                        <asp:ListItem Value="">ALL</asp:ListItem>
                        <asp:ListItem Value="Open">Open</asp:ListItem>
                        <asp:ListItem Value="Resolved">Resolved</asp:ListItem>
                        <asp:ListItem Value="Closed">Closed</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="ControlLabel" style="width:35%">
                    
                </td>
            </tr>
            
            <tr >
                <td class="ControlLabel2" style="width:40%">
                    Expected Work Start Date
                </td>
                <td class="ControlTextBox3"  style="width:25%">
                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="cssTextBox" Width="99px"
                        MaxLength="10" />
                </td>
                <td  style="width:35%" align="left">
                    <script type="text/javascript" language="JavaScript">
                        new tcal({ 'formname': 'form1', 'controlname': 'txtStartDate' });</script>
                </td>
            </tr>
            <tr >
                <td class="ControlLabel2" style="width:40%">
                    Expected Work End Date
                </td>
                <td class="ControlTextBox3"  style="width:25%">
                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="cssTextBox" Width="100px" MaxLength="10" />
                </td>
                <td  style="width:35%" align="left">
                    <script type="text/javascript" language="JavaScript">
                        new tcal({ 'formname': 'form1', 'controlname': 'txtEndDate' });</script>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="5">
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtStartDate"
                        ControlToValidate="txtEndDate" Display="None" ErrorMessage="Start Date Should Be Less Than the End Date"
                        CssClass="lblFont" Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
                </td>
            </tr>
            <tr style="height:7px">
                                                                                </tr>
            
            <tr>
                <td colspan="5">
                    <table width="100%">
                        <tr>
                            <td  style="width:40%">
                            </td>
                            <td align="center"  style="width:20%">
                                <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" CssClass="NewReport6"
                                EnableTheming="false" />
                            </td>    
                            <td  style="width:10%">
                                <asp:Button ID="Button1" runat="server" OnClick="btnRep_Click" CssClass="exportexl6"
                                EnableTheming="false" />
                            </td>
                            <td  style="width:30%">
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
                                        <input type="button" value=" " class="printbutton6" id="Button2" runat="Server" onclick="javascript:CallPrint('divPrint')"
                                            style="padding-left: 25px;" />
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
            <br />
            <wc:ReportGridView runat="server" BorderWidth="1" ID="gvWME" CssClass="gridview"
                GridLines="Both" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                AllowPrintPaging="true" Width="600px" OnRowDataBound="gvTSE_RowDataBound">
                <HeaderStyle CssClass="ReportHeadataRow" />
                <RowStyle CssClass="ReportdataRow" />
                <AlternatingRowStyle CssClass="ReportAltdataRow" />
                <FooterStyle CssClass="ReportFooterRow" />
                <PageHeaderTemplate>
                    <br />
                    <br />
                </PageHeaderTemplate>
                <Columns>
                    <asp:TemplateField HeaderText=" WorkID " ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label Visible="true" ID="lblWorkId" runat="server" Text='<%# Eval("WorkID")%>' />
                        </ItemTemplate>
                        <ItemStyle Width="10%"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Creation Date" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label Visible="true" ID="lblcreationdate" runat="server" Text='<%# Eval("CreationDate","{0:dd/MM/yyyy}")%>' />
                        </ItemTemplate>
                        <ItemStyle Width="10%"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Expected Work Start Date" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label Visible="true" ID="lblExpWrkStartdate" runat="server" Text='<%# Eval("ExpWrkStartDate","{0:dd/MM/yyyy}")%>' />
                        </ItemTemplate>
                        <ItemStyle Width="10%"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Expected Work End Date" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label Visible="true" ID="lblExpWrkEndDate" runat="server" Text='<%# Eval("ExpWrkEndDate","{0:dd/MM/yyyy}")%>' />
                        </ItemTemplate>
                        <ItemStyle Width="10%"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Employee Name" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label Visible="true" ID="lblempName" runat="server" Text='<%# Eval("empFirstName")%>' />
                        </ItemTemplate>
                        <ItemStyle Width="10%"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="WorkDetails" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label Visible="true" ID="lblWorkDetails" runat="server" Text='<%# Eval("Workdetails")%>' />
                        </ItemTemplate>
                        <ItemStyle Width="10%"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Actual Start Date" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label Visible="true" ID="lblActStartDate" runat="server" Text='<%# Eval("ActStartDate","{0:dd/MM/yyyy}")%>' />
                        </ItemTemplate>
                        <ItemStyle Width="10%"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Actual End Date" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label Visible="true" ID="lblActEndDate" runat="server" Text='<%# Eval("ActEndDate","{0:dd/MM/yyyy}")%>' />
                        </ItemTemplate>
                        <ItemStyle Width="10%"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Resolution Detail" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label Visible="true" ID="lblResDet" runat="server" Text='<%# Eval("Resolutiondet")%>' />
                        </ItemTemplate>
                        <ItemStyle Width="10%"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Work Status" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label Visible="true" ID="lblwrkStatus" runat="server" Text='<%# Eval("WorkStatus")%>' />
                        </ItemTemplate>
                        <ItemStyle Width="10%"></ItemStyle>
                    </asp:TemplateField>
                </Columns>
                <PagerTemplate>
                </PagerTemplate>
                <PageFooterTemplate>
                </PageFooterTemplate>
            </wc:ReportGridView>
        </div>
        </div>
    </div>
    </form>
</body>
</html>
