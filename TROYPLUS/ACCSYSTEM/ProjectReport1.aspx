<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProjectReport1.aspx.cs" Inherits="ProjectReport1" %>


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
<body style="font-family: 'Trebuchet MS'; font-size: 18px;">
    <form id="form1" method="post" runat="server">
        <br />
        <div id="div1" visible="false" runat="server">
            <table style="border: 1px solid blue; background-color: White;" width="700px">
                <%--<tr>
                <td colspan="5">
                    
                        <table cellspacing="0" cellpadding="0" border="0"  class="headerPopUp">
                            <tr valign="middle">
                                <td>
                                    <span>Stock Ageing Report</span>
                                </td>
                            </tr>
                        </table>
                    
                </td>--%>
                <%--<td colspan="4" class="mainConHd" style="text-align:center; vertical-align:middle">
                    <span>Stock Ageing Report</span>
                </td>--%>
                <%--</tr>--%>
                <tr>
                    <td colspan="5" class="headerPopUp">Project Report
                    </td>
                </tr>
                <tr style="height: 6px">
                </tr>
                <tr>
                    <td colspan="5">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 25%; font-family: 'ARIAL'; font-size: 11px; font-weight: normal; color: #000000; text-align: right; text-decoration: none; padding-right: 5px; padding-left: 5px; padding-top: 5px;" height="27px">Select the Employee
                                </td>
                                <td style="text-align: left; width: 20%" class="ControlDrpBorder">

                                    <asp:DropDownList ID="drpEmployee" TabIndex="3" Enabled="True" EnableTheming="false" AppendDataBoundItems="true" CssClass="drpDownListMedium"
                                        runat="server" Width="100%" DataTextField="empFirstName" BackColor="#e7e7e7" Style="border: 1px solid #e7e7e7" Height="26px"
                                        DataValueField="empno">
                                        <%--<asp:ListItem Text="Select Employee" Value="0"></asp:ListItem>--%>
                                    </asp:DropDownList>


                                </td>
                                <td style="width: 25%; font-family: 'ARIAL'; font-size: 11px; font-weight: normal; color: #000000; text-align: right; text-decoration: none; padding-right: 5px; padding-left: 5px; padding-top: 5px;" height="27px">Select the Project
                                </td>
                                <td style="text-align: left; width: 20%" class="ControlDrpBorder">

                                    <asp:DropDownList ID="drpproject" runat="server" DataTextField="Project_Name" DataValueField="Project_Id" BackColor="#e7e7e7" Style="border: 1px solid #e7e7e7" Height="26px" Width="100%"
                                        CssClass="textbox">
                                        <%--<asp:ListItem Selected="True" Value="0" style="background-color: #bce1fe">Select Project</asp:ListItem>--%>
                                    </asp:DropDownList>

                                </td>
                                <td style="width: 10%"></td>
                            </tr>
                            <tr style="height: 2px;" />
                            <tr>
                                <td style="width: 25%; font-family: 'ARIAL'; font-size: 11px; font-weight: normal; color: #000000; text-align: right; text-decoration: none; padding-right: 5px; padding-left: 5px; padding-top: 5px;" height="27px">Blocked Task
                                </td>
                                <td style="text-align: left; width: 20%" class="ControlDrpBorder">

                                    <asp:RadioButtonList ID="radblocktask" runat="server">
                                        <asp:ListItem Text="NO" Value="NO" Selected="true"></asp:ListItem>
                                        <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                    </asp:RadioButtonList>

                                </td>
                                <td style="width: 25%; font-family: 'ARIAL'; font-size: 11px; font-weight: normal; color: #000000; text-align: right; text-decoration: none; padding-right: 5px; padding-left: 5px; padding-top: 5px;" height="27px">Task Status
                                </td>
                                <td style="text-align: left; width: 20%" class="ControlDrpBorder">
                                    <asp:DropDownList ID="drpTaskStatus" TabIndex="5" Enabled="True" EnableTheming="false" AppendDataBoundItems="true" CssClass="drpDownListMedium"
                                        runat="server" Width="100%" DataTextField="Task_Status_Name" BackColor="#e7e7e7" Style="border: 1px solid #e7e7e7" Height="26px"
                                        DataValueField="Task_Status_Id">
                                        <asp:ListItem Text="Select Task Status" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 10%"></td>
                            </tr>

                            <tr style="height: 2px;" />
                            <tr>
                                <td style="width: 25%; font-family: 'ARIAL'; font-size: 11px; font-weight: normal; color: #000000; text-align: right; text-decoration: none; padding-right: 5px; padding-left: 5px; padding-top: 5px;" height="27px">Select task
                                </td>
                                <td style="text-align: left; width: 20%" class="ControlDrpBorder">

                                    <asp:DropDownList ID="drptask" runat="server" DataTextField="Task_Name" DataValueField="Task_Id" CssClass="drpDownListMedium" BackColor="#e7e7e7" Style="border: 1px solid #e7e7e7" Height="26px" Width="100%" AppendDataBoundItems="True">
                                    </asp:DropDownList>

                                </td>
                                <td style="width: 25%; font-family: 'ARIAL'; font-size: 11px; font-weight: normal; color: #000000; text-align: right; text-decoration: none; padding-right: 5px; padding-left: 5px; padding-top: 5px;" height="27px">Select Dependency Task
                                </td>
                                <td style="text-align: left; width: 20%" class="ControlDrpBorder">

                                    <asp:DropDownList ID="drpdependencytask" runat="server" DataTextField="Task_Name" DataValueField="Task_Id" CssClass="drpDownListMedium" BackColor="#e7e7e7" Style="border: 1px solid #e7e7e7" Height="26px" Width="100%" AppendDataBoundItems="True">
                                    </asp:DropDownList>

                                </td>
                                <td style="width: 10%"></td>
                            </tr>
                            <tr style="height: 2px;" />

                            <tr>
                                <td colspan="4">
                                    <asp:Label ID="lblDate" runat="server" CssClass="label"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>

                </tr>

                <tr>
                    <td colspan="4">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 30%;"></td>
                                <td style="width: 20%;">
                                    <asp:Button ID="btnstockageing" runat="server" CssClass="NewReport6"
                                        ValidationGroup="btnAgeing" EnableTheming="false" />
                                </td>
                                <td style="width: 20%;">
                                    <asp:Button ID="btnExcel" runat="server" CssClass="exportexl6"
                                        EnableTheming="false" ValidationGroup="btnAgeing" />
                                </td>
                                <td style="width: 30%;"></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="style1" colspan="4"></td>
                </tr>
            </table>
        </div>
        <div runat="server" id="divmain" visible="true">
            <table width="600px">
                <tr>
                    <td colspan="3">
                        <table width="100%">
                            <tr>
                                <td style="width: 45%"></td>
                                <td style="width: 19%">
                                    <input type="button" value="" id="Button1" runat="Server" onclick="javascript: CallPrint('divPrint')"
                                        class="printbutton6" />
                                </td>
                                <td style="width: 2%">
                                    <asp:Button ID="btndet" Visible="false" CssClass="GoBack" EnableTheming="false" runat="server" />
                                </td>
                                <td style="width: 31%"></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            
              <table width="600px" runat="server"  cellpadding="2" border="0" style="font-family: Trebuchet MS; font-size: 14px; align-items:center;">
                    <tr>
                        <td width="140px" align="left">TIN#:
                        <asp:Label ID="lblTNGST" runat="server"></asp:Label>
                        </td>
                        <td align="center" width="320px" style="font-size: 20px;">
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
                        <td colspan="3">
                            <br />
                            <h5>
                            <asp:Label ID="lblproject" runat="server"></asp:Label>
                                <br />
                               
                            <asp:Label ID="lblStartDate" runat="server"> </asp:Label>
                               
                            <asp:Label ID="lblEndDate" runat="server"> </asp:Label></h5>
                        </td>
                    </tr>
                </table>
              
            <br />
            <div id="divPrint" align="center" visible="false"  style="font-family: 'Trebuchet MS'; font-size: 18px; width: 100%"
                runat="server">
              
                <br />
                <br />
            
            </div>
            &nbsp;
            
    
          
            <br />
        </div>
    </form>
</body>
</html>

