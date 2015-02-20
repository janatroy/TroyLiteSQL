<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintPayment.aspx.cs" Inherits="PrintPayment" %>

<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Print Preview - Payment Report</title>
    <script type="text/javascript">
        function CallPrint(strid) {
            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('');
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
        }
        function unl() {
            document.form1.submit();
        }

    </script>
</head>
<body style="font-size: 14px; font-family: 'Trebuchet MS';" onbeforeunload="unl()">
    <form id="form1" runat="server">
    <asp:ScriptManager runat="server" ID="scr"></asp:ScriptManager>
    <br />
    <div style="text-align: center" align="center">
        <input type="button" value="Print " id="btnPrint" runat="Server" onclick="javascript:CallPrint('divPrint')"
              class="button" />&nbsp;&nbsp;&nbsp;
         <%--<asp:Button ID="btnBack" Text="Back" runat="server" class="printButton" OnClick="btnBack_Click" /><br />--%>
         <%--asp:Button ID="Button1" value="Back " type="button"   runat="server" class="button" OnClick="btnBack_Click" />--%>
         <%--<input type="button" value="Back " id="btnBack" runat="Server" onclick="btnBack_Click" class="button" />--%>
            <table style="width:100%" align="center">
                <tr>
                    <td style="width:30%">

                    </td>
                    <td style="width:15%" class="ControlLabel">
                        
                    </td>
                    <td style="width:17%">
                        <asp:UpdatePanel ID="UpdatePanel21" runat="server" UpdateMode="Conditional">
                             <ContentTemplate>
                                <asp:DropDownList ID="cmdBill" Width="100%" AppendDataBoundItems="True" CssClass="drpDownListMedium" OnSelectedIndexChanged="cmdBill_SelectedIndexChanged"
                                     runat="server" DataTextField="Billformat" AutoPostBack="true" DataValueField="Billformat" ValidationGroup="product" style="border: 1px solid #90C9FC" height="25px">
                                     <%--<asp:ListItem Text="Select Billformat" Value="0"></asp:ListItem>--%>
                                </asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="cmdBill" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <%--<td style="width:50%">
                        <input type="button" value="Export To Excel " id="Button1" runat="Server" class="button" />
                    </td>--%>
                </tr>
            </table>

        <div id="divPrint" style="font-family: 'Trebuchet MS'; font-size: 15px;" align="center">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div id="GENREALFORMAT" visible="false" runat="server" align="center">
                        <table width="850px" border="0" cellpadding="0" cellspacing="0" style="font-family: 'Trebuchet MS';
                            font-size: 11px;">
                            <tr>
                                <td width="140px" align="left">
                                    TIN#:
                                    <asp:Label ID="lblTNGST" runat="server"></asp:Label>
                                </td>
                                <td align="center" width="320px">
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
                                    <hr />
                                </td>
                                <td align="center">
                                    <hr />
                                </td>
                                <td>
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="text-align: left">
                                    <br />
                                    <h5>
                                        Payment<br />
                                    </h5>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table width="600" border="0" cellpadding="0" cellspacing="2" style="font-family: 'Trebuchet MS';
                            font-size: 11px;">
                            <tr>
                                <td style="width: 30%" align="left">
                                    &nbsp;
                                </td>
                                <td style="width: 30%" align="left">
                                    &nbsp;
                                </td>
                                <td style="width: 30%">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%" align="left">
                                    Reference No
                                </td>
                                <td style="width: 30%" align="left">
                                    <asp:Label ID="lblRefNo" runat="server"></asp:Label>
                                </td>
                                <td style="width: 30%">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%" align="left">
                                    Paid To
                                </td>
                                <td style="width: 30%" align="left">
                                    <asp:Label ID="lblPayedTo" runat="server"></asp:Label>
                                </td>
                                <td style="width: 30%">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%" align="left">
                                    Amount
                                </td>
                                <td style="width: 30%" align="left">
                                    <asp:Label ID="lblAmount" runat="server"></asp:Label>
                                </td>
                                <td style="width: 30%">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%" align="left">
                                    Payment Mode
                                </td>
                                <td style="width: 30%" align="left">
                                    <asp:Label ID="lblPayMode" runat="server"></asp:Label>
                                </td>
                                <td style="width: 30%">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr id="rowBank" runat="server">
                                <td style="width: 30%" align="left">
                                    Bank Name
                                </td>
                                <td style="width: 30%" align="left">
                                    <asp:Label ID="lblBank" runat="server"></asp:Label>
                                </td>
                                <td style="width: 30%">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr id="rowCheque" runat="server">
                                <td style="width: 30%" align="left">
                                    Cheque No.
                                </td>
                                <td style="width: 30%" align="left">
                                    <asp:Label ID="lblCheque" runat="server"></asp:Label>
                                </td>
                                <td style="width: 30%">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%" align="left">
                                    Narration
                                </td>
                                <td style="width: 30%" align="left">
                                    <asp:Label ID="lblNarration" runat="server"></asp:Label>
                                </td>
                                <td style="width: 30%">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%" align="left">
                                    Payment Date
                                </td>
                                <td style="width: 30%" align="left">
                                    <asp:Label ID="lblPayDate" runat="server"></asp:Label>
                                </td>
                                <td style="width: 30%">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%" align="left">
                                    In words
                                </td>
                                <td style="width: 30%" align="left">
                                    <asp:Label ID="lblinwords" runat="server"></asp:Label>
                                </td>
                                <td style="width: 30%">
                                    &nbsp;
                                </td>
                            </tr>
                             <tr>
                                <td style="width: 30%" align="left">
                                    &nbsp;
                                </td>
                                <td style="width: 30%" align="left">
                                    &nbsp;
                                </td>
                                <td style="width: 30%">
                                    &nbsp;
                                </td>
                            </tr>
                             <tr>
                                <td style="width: 30%" align="left">
                                    Authorised By
                                </td>
                                <td style="width: 30%" align="left">
                            
                                </td>
                                <td style="width: 30%">
                                    Receiver's Sign
                                </td>
                            </tr>
                        </table>
                        <br />
                        <br />
                    </div>
                    <div id="PREPRINTEDFORMAT2" visible="false" runat="server" align="center">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" style="font-family:'Times New Roman';
                            font-size: 16px; font-weight: bold; ">
                            <tr>
                                <td style="width: 25%" align="left">
                            
                                </td>
                                <td style="width: 67%" align="left">
                                       
                                </td>
                                <td style="width: 15%">
                                    &nbsp;
                                </td>
                                
                            </tr>

                            <tr>
                                <td style="width: 4%" align="left">
                            
                                </td>
                                <td style="width: 60%" align="left">
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                                <td style="width: 15%">
                                    &nbsp;
                                </td>
                            </tr>
                            
                            <tr>
                                <td style="width: 4%" align="left">
                            
                                </td>
                                <td style="width: 60%" align="left">
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                                <td style="width: 15%">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 4%" align="left">
                            
                                </td>
                                <td style="width: 67%" align="left">
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                                <td style="width: 15%">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 4%" align="left">
                            
                                </td>
                                <td style="width: 67%" align="left">
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                                <td style="width: 15%">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr  style="height:5px">
                            </tr>
                            <tr>
                                <td style="width: 4%" align="left">
                            
                                </td>
                                <td style="width: 67%" align="left">
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                                <td style="width: 15%">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 4%" align="left">
                            
                                </td>
                                <td style="width: 67%" align="left">
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                                <td style="width: 15%">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 4%" align="left">
                            
                                </td>
                                <td style="width: 67%" align="left">
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                                <td style="width: 15%">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 4%" align="left">
                            
                                </td>
                                <td style="width: 67%" align="left">
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                                <td style="width: 15%">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 4%" align="left">
                            
                                </td>
                                <td style="width: 67%" align="left">
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                                <td style="width: 15%">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 4%" align="left">
                            
                                </td>
                                <td style="width: 67%" align="left">
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                                <td style="width: 15%">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 4%" align="left">
                            
                                </td>
                                <td style="width: 67%" align="left">
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                                <td style="width: 15%">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 4%" align="left">
                            
                                </td>
                                <td style="width: 67%" align="left">
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                                <td style="width: 15%">
                                    <asp:Label ID="lblPayDatePP" runat="server"></asp:Label>
                                </td>
                            </tr>
                            
                            <tr>
                                <td style="width: 4%" align="left">
                            
                                </td>
                                <td style="width: 67%" align="left">
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                                <td style="width: 15%">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 4%" align="left">
                            
                                </td>
                                <td style="width: 60%" align="left">
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                                <td style="width: 15%">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 4%" align="left">
                            
                                </td>
                                <td style="width: 67%" align="left">
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblPayedToPP" runat="server"></asp:Label>
                                </td>
                                <td style="width: 15%">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <table style="width:100%">
                                        <tr style="height:7px">
                                        </tr>
                                        <tr>
                                            <td style="width: 25%" align="left">
                            
                                            </td>
                                            <td style="width: 67%" align="left">
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblAmttxtPP" runat="server"></asp:Label>
                                            </td>
                                            <td style="width: 15%">
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                
                            </tr>
                           <%--<tr>
                                <td style="width: 4%" align="left">
                            
                                </td>
                                <td style="width: 60%" align="left">
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                                <td style="width: 15%">
                                    &nbsp;
                                </td>
                            </tr>--%>
                            <tr  style="height:5px">
                            </tr>
                            <tr id="Tr1" runat="server">
                                <td style="width: 4%" align="left">
                            
                                </td>
                                <td style="width: 60%" align="left">
                            
                                </td>
                                <td style="width: 15%">
                                    <asp:Label ID="lblAmountPP" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr id="Tr2" runat="server">
                                <td style="width: 4%" align="left">
                            
                                </td>
                                <td style="width: 60%" align="left">
                            
                                </td>
                                <td style="width: 15%">
                                    &nbsp;
                                </td>
                            </tr>
                            
                        </table>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cmdBill" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    </form>
</body>
</html>
