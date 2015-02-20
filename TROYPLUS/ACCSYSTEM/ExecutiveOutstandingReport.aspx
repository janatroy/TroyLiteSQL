<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExecutiveOutstandingReport.aspx.cs"
    Inherits="ExecutiveOutstandingReport" %>

<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>Executive Outstanding Report</title>
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
        function PrintItem(ID, Customername) {
            //alert(Customername);
            window.showModalDialog('BillCustomerView.aspx?ID=' + ID + '&cname=' + escape(Customername), self, 'dialogWidth:700px;dialogHeight:430px;status:no;dialogHide:yes;unadorned:yes;');
            //window.open('BillCustomerView.aspx?ID=' + ID + '&cname=' + Customername ,'','letf=0,top=0,width=600,height=400,toolbar=0,scrollbars=1,status=0');
            //alert('BillCustomerView.aspx?ID=' + ID + '&cname=' + Customername );
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
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 700px">
        <br />
        <br />
        <table cellpadding="2" cellspacing="4" width="700px" border="0" style="border: 1px solid silver;
            text-align: left">
            <tr>
                <td colspan="3" class="subHeadFont">
                    Outstanding Executive Report
                </td>
            </tr>
        </table>
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
        </table>
        <br />
        <br />
        <div style="display: none;">
            <table cellpadding="2" cellspacing="4" width="100%" border="0" style="border: 1px solid silver;
                background-image: url('App_Themes/DefaultTheme/Images/bluebg.jpg');">
                <tr>
                    <td align="center" style="background-image: url('App_Themes/DefaultTheme/Images/bgReportheader.jpg');
                        color: White; background-repeat: repeat-x; font-size: 11px; font-weight: bold;">
                        Outstanding View
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:DropDownList ID="drpView" runat="server" CssClass="lblFont">
                            <asp:ListItem>Both Outstanding</asp:ListItem>
                            <asp:ListItem>Customer Outstanding</asp:ListItem>
                            <asp:ListItem>Company Outstanding</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="center" class="lblFont">
                        <b>- Both Outstanding (We can view both Customer Outstanding and Company Outstanding)
                            <br />
                            - Customer Outstanding (We can view only Customer Outstanding)
                            <br />
                            - Company Outstanding (We can view only Company Outstanding) </b>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="btnReport" SkinID="skinButtonBig" runat="server" Width="200px" OnClick="btnReport_Click"
                            Text="View Report" />
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <asp:GridView ShowFooter="true" Style="font-family: 'Trebuchet MS'; font-size: 11px;"
            EmptyDataText="No Executives Found" Width="100%" ID="gvExecutive" GridLines="Both"
            SkinID="gridview" CssClass="gridview" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="False"
            runat="server" OnRowDataBound="gvExecutive_RowDataBound" ForeColor="#333333">
            <Columns>
                <asp:TemplateField ItemStyle-Width="80%" HeaderText="Executive Name" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <br />
                        <a href="javascript:switchViews('dv<%# Eval("Empno") %>', 'imdiv<%# Eval("Empno") %>');"
                            style="text-decoration: none;">
                            <img id="imdiv<%# Eval("Empno") %>" alt="Show" border="0" src="App_Themes/DefaultTheme/Images/plus.gif" />
                        </a>
                        <%# Eval("EmpFirstName") %>
                        <div id="dv<%# Eval("Empno") %>" style="display: none; position: relative; left: 25px;">
                            <asp:GridView ShowFooter="true" Style="font-family: 'Trebuchet MS'; font-size: 11px;"
                                EmptyDataText="No Dealers Found" Width="100%" ID="gvDealer" GridLines="Both"
                                SkinID="gridview" CssClass="gridview" AlternatingRowStyle-CssClass="even" OnRowDataBound="gvDealer_RowDataBound"
                                AutoGenerateColumns="False" runat="server" ForeColor="#333333">
                                <Columns>
                                    <asp:TemplateField HeaderText="Opening Balance" ItemStyle-Width="9%" ItemStyle-VerticalAlign="Top"
                                        HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOB" Text='<%# Eval("OB","{0:f2}") %> ' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer Need to Pay" ItemStyle-Width="30%" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <a href="javascript:switchViews('dv<%# Eval("LedgerID") %>', 'imdiv<%# Eval("LedgerID") %>');"
                                                style="text-decoration: none;">
                                                <img id="imdiv<%# Eval("LedgerID") %>" alt="Show" border="0" src="App_Themes/DefaultTheme/Images/plus.gif" />
                                            </a>
                                            <div id="dv<%# Eval("LedgerID") %>" style="display: none; position: relative; left: 25px;">
                                                <asp:GridView ShowFooter="true" Style="font-family: 'Trebuchet MS'; font-size: 11px;"
                                                    EmptyDataText="No Outstanding  Found" OnRowDataBound="gvCredit_RowDataBound"
                                                    Width="100%" ID="gvCredit" GridLines="Both" SkinID="gridview" CssClass="gridview"
                                                    AlternatingRowStyle-CssClass="even" AutoGenerateColumns="False" runat="server"
                                                    ForeColor="#333333">
                                                    <Columns>
                                                        <asp:BoundField ItemStyle-HorizontalAlign="Left" DataField="Billno" HeaderText="Bill No." />
                                                        <asp:BoundField ItemStyle-HorizontalAlign="Left" DataField="BillDate" HeaderText="Bill Date"
                                                            DataFormatString="{0:dd/MM/yyyy}" />
                                                        <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderText="Amount" HeaderStyle-HorizontalAlign="Right"
                                                            ItemStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDebit" Text='<%# Eval("Amount","{0:f2}") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblT" Style="font-family: 'Trebuchet MS'; font-size: 11px;" runat="server"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount Received From Customer" ItemStyle-Width="30%"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <a href="javascript:switchViews('dvr<%# Eval("LedgerID") %>', 'imdivr<%# Eval("LedgerID") %>');"
                                                style="text-decoration: none;">
                                                <img id="imdivr<%# Eval("LedgerID") %>" alt="Show" border="0" src="App_Themes/DefaultTheme/Images/plus.gif" />
                                            </a>
                                            <div id="dvr<%# Eval("LedgerID") %>" style="display: none; position: relative; left: 25px;">
                                                <asp:GridView ShowFooter="true" Style="font-family: 'Trebuchet MS'; font-size: 11px;"
                                                    OnRowDataBound="gvDebit_RowDataBound" EmptyDataText="No Outstanding  Found" Width="100%"
                                                    ID="gvDebit" GridLines="Both" SkinID="gridview" CssClass="gridview" AlternatingRowStyle-CssClass="even"
                                                    AutoGenerateColumns="False" runat="server" ForeColor="#333333">
                                                    <Columns>
                                                        <asp:BoundField ItemStyle-HorizontalAlign="Left" DataField="Transno" HeaderText="Trans. No." />
                                                        <asp:BoundField ItemStyle-HorizontalAlign="Left" DataField="TransDate" HeaderText="Bill Date"
                                                            DataFormatString="{0:dd/MM/yyyy}" />
                                                        <asp:BoundField ItemStyle-HorizontalAlign="Left" DataField="VoucherType" HeaderText="Voucher" />
                                                        <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderText="Amount" HeaderStyle-HorizontalAlign="Right"
                                                            ItemStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDebit" Text='<%# Eval("Amount","{0:f2}") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblT" Style="font-family: 'Trebuchet MS'; font-size: 11px;" runat="server"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField ItemStyle-HorizontalAlign="Left" DataField="Ledgername" HeaderText="Particulars"
                                        ItemStyle-Width="15%" />
                                    <asp:TemplateField HeaderText="Customer Pending to Company" FooterStyle-HorizontalAlign="Right"
                                        ItemStyle-VerticalAlign="Top" ItemStyle-Width="8%" HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDebit" Text='<%# Eval("Debit","{0:f2}") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblTDebit" runat="server" Style="font-family: 'Trebuchet MS'; font-size: 11px;"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Company Pending to Customer" FooterStyle-HorizontalAlign="Right"
                                        ItemStyle-VerticalAlign="Top" ItemStyle-Width="8%" HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCredit" Text='<%# Eval("Credit","{0:f2}") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblTCredit" Style="font-family: 'Trebuchet MS'; font-size: 11px;"
                                                runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-VerticalAlign="Top" FooterStyle-HorizontalAlign="Right"
                    HeaderText="Total Outstanding" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblTotal" runat="server" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="lblTotEx" Style="font-family: 'Trebuchet MS'; font-size: 11px;" runat="server"></asp:Label>
                    </FooterTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
