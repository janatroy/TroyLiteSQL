<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintProductSalesBill.aspx.cs"
    Inherits="PrintProductSalesBill" %>

<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Sales Bill</title>
    <base target="_self">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <link rel="Stylesheet" href="App_Themes/DefaultTheme/DefaultTheme.css" />
    <script type="text/javascript">
        function CallPrint(strid) {
            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('', '', 'letf=0,top=0,width=700,height=400,toolbar=0,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();

        }

    </script>
</head>
<body style="font-family: 'Trebuchet MS'; font-size: 14px;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="6000">
    </asp:ScriptManager>
    <div style="width: 100%; text-align: center" align="center">
        <br />
        <input type="button" value="Print " id="Button1" runat="Server" class="printButton"
            onclick="javascript:CallPrint('divPrint')" />&nbsp;
        <asp:Button ID="btnBack" Text="Back" runat="server" class="printButton" OnClick="btnBack_Click" /><br />
        <br />
        <div id="Div2" runat="server" align="center">
        <table width="600px" border="0" cellpadding="2" cellspacing="0" style="font-family: 'Trebuchet MS';
            font-size: 11px;">
            <tr>
                <td>
                    <div id="Div1" runat="server" align="center">
                    <table width="600px" cellpadding="2" cellspacing="2">
                        <tr>
                            <td colspan="4">
                                <b>Sales Bill Details Entry</b>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 10%">
                                Copy
                            </td>
                            <td style="width: 40%" >
                                &nbsp;&nbsp;<asp:TextBox ID="txtCopy" runat="server" Text="Customer Copy" CssClass="cssTextBox"
                                    Width="120px"></asp:TextBox>
                            </td>
                            <td style="width: 20%">
                                <asp:Label ID="lblDivisions" runat="server" Text="Division"></asp:Label>
                            </td>
                            <td style="width: 30%">
                                <div id="divDiv" runat="server" style="width: 158px; font-family: 'Trebuchet MS';">
                                    <asp:DropDownList ID="ddDivsions" runat="server" DataTextField="DivisionName" BackColor="#90c9fc" DataValueField="DivisionID" style="border: 1px solid Blue" height="24px"
                                        Width="100%" AppendDataBoundItems="True" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddDivsions_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Value="0" style="background-color: #90c9fc">Select Division</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </td>
                        </tr>
                    </table>
                    </div>
                    <div id="dvPF2" runat="server" visible="false" align="center">
                        <table width="40%" cellpadding="2" cellspacing="2" align="center">
                            <tr>
                                <td>
                                    Boxes
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="txtBoxes" runat="server" Text="0" CssClass="cssTextBox" Width="120px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Lorry
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="txtLorryName" runat="server" Text="" CssClass="cssTextBox" Width="120px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Remarks
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="txtRemarks" runat="server" Text="" CssClass="cssTextBox" Width="120px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <table width="600px" cellpadding="2" cellspacing="2" align="center">
                        <tr>
                            <td colspan="2">
                                <asp:Button Text="Enter" ID="btnCopy" runat="server" OnClick="btnCopy_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        </div>
        <br />
        <div id="divPrint" style="font-family: 'Trebuchet MS'; font-size: 11px;" align="center">
            <%--Start Header--%>
            <%--<table width="600px">
                <tr>
                    <td align="right">
                        <asp:Image ID="Image1" runat="server"/>
                    </td>
                </tr>
            </table>--%>
            
            <center>
                <asp:Label Font-Bold="true" ID="lblBillCopy" Style="font-family: 'Trebuchet MS';
                    font-size: 14px;" runat="server" CssClass="lblFont"></asp:Label>
            </center>
            <center>
                <asp:Label ID="lblDuplicatecopy" Style="font-family: 'Trebuchet MS';
                    font-size: 12px;" runat="server" CssClass="lblFont"></asp:Label>
            </center>
            <div id="GENREALFORMAT" visible="false" runat="server" align="center" >
                <table style="background-image: url('App_Themes/NewTheme/Images/water.png'); background-position:center; background-repeat:no-repeat">
                        <tr>
                            <td>
                <div id="dvHeader600" runat="server" align="center">
                    
                    <table width="600px" border="0" cellpadding="2" cellspacing="0" class="lblFont" style="font-family: 'Trebuchet MS';
                        font-size: 11px; border: 1px solid black;">
                        <tr>
                            <td style="border-right: 1px solid black;">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                                    <ContentTemplate>
                                        <table width="300px" border="0" cellpadding="2" cellspacing="0" style="font-family: 'Trebuchet MS';
                                            font-size: 11px;">
                                            <tr id="trTINCST600" runat="server">
                                                <td align="left">
                                                    TIN
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lblTNGST600" runat="server"></asp:Label>
                                                </td>
                                                <td align="right">
                                                    BillNo :
                                                    <asp:Label ID="lblBillno600" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr id="trTINCST1600" runat="server" visible="false">
                                                <td align="left">
                                                    BillNo : &nbsp;
                                                    <asp:Label ID="lblBillNo1600" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                </td>
                                                <td align="left">
                                                </td>
                                                <td align="right">
                                                </td>
                                            </tr>
                                            <tr id="trCST600" runat="server">
                                                <td align="left">
                                                    CST#
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align="left" colspan="2">
                                                    <asp:Label ID="lblGSTno600" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="left" colspan="4" width="400px">
                                                    <table style="font-family: 'Trebuchet MS'; font-size: 11px;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label Font-Bold="true" ID="lblCompany600" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblAddress600" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblCity600" runat="server" />
                                                                -
                                                                <asp:Label ID="lblPincode600" runat="server"></asp:Label>
                                                                PH:
                                                                <asp:Label ID="lblPhone600" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblState600" runat="server"> </asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td width="301px" align="left">
                                <table width="300px" border="0" cellpadding="2" cellspacing="0" style="font-family: 'Trebuchet MS';
                                    font-size: 11px;">
                                    <tr id="trVATTAX600" runat="server">
                                        <td valign="top" style="background-color: Black; color: White;">
                                            <div id="divInvoiceType" runat="server">
                                                VAT TAX INVOICE</div>
                                            <asp:Label ID="lblHeader600" runat="server" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td valign="top" align="right">
                                            Date:
                                            <asp:Label ID="lblBillDate600" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" valign="top" align="right">
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="left" valign="top">
                                            <table border="0" style="font-family: 'Trebuchet MS'; font-size: 11px;">
                                                <tr>
                                                    <td>
                                                        Customer :&nbsp;<asp:Label ID="lblSupplier600" runat="server" Font-Bold="true" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblSuppAdd1600" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblSuppAdd2600" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblSuppAdd3600" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Ph :
                                                        <asp:Label ID="lblSuppPh600" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        TIN :
                                                        <asp:Label ID="lblCustTIN600" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <%--End Header--%>
                </div>
                <div id="dvJandJFormatHeader" runat="server" visible="false" align="center">
                    <table width="600px" border="0" cellpadding="2" cellspacing="0" class="lblFont" style="font-family: 'Trebuchet MS';
                        font-size: 11px; border: 0px solid black;" align="center">
                        <tr>
                            <td width="60%" rowspan="2" style="padding-left: 15px; text-align: left">
                                <table border="0" style="font-family: 'Trebuchet MS'; font-size: 11px;">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lbljjSupplier600" runat="server" Font-Bold="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lbljjSuppAdd1600" runat="server"></asp:Label>,<asp:Label ID="lbljjSuppAdd2600"
                                                runat="server"></asp:Label>,<asp:Label ID="lbljjSuppAdd3600" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Ph :
                                            <asp:Label ID="lbljjSuppPh600" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            TIN :
                                            <asp:Label ID="lbljjCustTIN600" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="40%" align="left">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td width="50px">
                                        </td>
                                        <td>
                                            <asp:Label ID="lbljjBillno" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td width="40%" align="left">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td width="50px">
                                        </td>
                                        <td>
                                            <asp:Label ID="lbljjBillDate" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="dvBody600" runat="server" align="center">
                    <table width="600px" border="0" cellpadding="2" cellspacing="0" style="font-family: 'Trebuchet MS';
                        font-size: 11px; border: 1px solid black;" align="center">
                        <tr>
                            <td>
                                <wc:ReportGridView runat="server" BorderWidth="1" ID="gvGeneral600" CssClass="left"
                                    GridLines="Both" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                    PrintPageSize="45" AllowPrintPaging="true" Visible="false" Width="600px" OnRowDataBound="gvGeneral600_RowDataBound"
                                    Style="font-family: 'Trebuchet MS'; font-size: 11px;">
                                    <PageHeaderTemplate>
                                        <br />
                                        <br />
                                    </PageHeaderTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="SI.No">
                                            <ItemTemplate>
                                                <%# ((GridViewRow)Container).RowIndex + 1%>
                                            </ItemTemplate>
                                            <ItemStyle Width="50px" HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:BoundField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="360px" DataField="Particulars"
                                            HeaderText="PARTICULARS" HeaderStyle-HorizontalAlign="left"/>
                                        <asp:BoundField ItemStyle-HorizontalAlign="right" ItemStyle-Width="50px" DataFormatString="{0:f2}"
                                            DataField="NetRate" HeaderText="NETRATE" Visible="false" />
                                        <asp:BoundField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px" DataFormatString="{0:f2}"
                                            HeaderStyle-HorizontalAlign="left" DataField="Rate" HeaderText="RATE" />
                                        <asp:BoundField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px" DataField="Qty"
                                            HeaderStyle-HorizontalAlign="left" HeaderText="QTY" />
                                        <asp:BoundField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px" DataField="Unit"
                                            HeaderText="" Visible="false" />
                                        <asp:BoundField ItemStyle-HorizontalAlign="right" ItemStyle-Width="70px" DataFormatString="{0:f2}"
                                            DataField="Amount" HeaderText="AMOUNT" HeaderStyle-HorizontalAlign="right" />
                                    </Columns>
                                    <PagerTemplate>
                                    </PagerTemplate>
                                    <PageFooterTemplate>
                                        <br />
                                    </PageFooterTemplate>
                                </wc:ReportGridView>
                                <wc:ReportGridView runat="server" BorderWidth="1" ID="gvJJFormat" CssClass="left"
                                    GridLines="Both" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                    PrintPageSize="45" AllowPrintPaging="true" Visible="false" Width="600px" OnRowDataBound="gvGeneral600_RowDataBound"
                                    Style="font-family: 'Trebuchet MS'; font-size: 11px;">
                                    <PageHeaderTemplate>
                                        <br />
                                        <br />
                                    </PageHeaderTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="SI.No">
                                            <ItemTemplate>
                                                <%# ((GridViewRow)Container).RowIndex + 1%>
                                            </ItemTemplate>
                                            <ItemStyle Width="50px" HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:BoundField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="260px" DataField="Particulars"
                                            HeaderText="PARTICULARS" HeaderStyle-HorizontalAlign="left"/>
                                        <asp:BoundField ItemStyle-HorizontalAlign="right" ItemStyle-Width="50px" DataFormatString="{0:f2}"
                                            DataField="NetRate" HeaderText="NETRATE" Visible="false" />
                                        <asp:BoundField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px" DataFormatString="{0:f2}"
                                            HeaderStyle-HorizontalAlign="left" DataField="Rate" HeaderText="RATE" />
                                        <asp:BoundField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px" DataField="Qty"
                                            HeaderStyle-HorizontalAlign="left" HeaderText="QTY" />
                                        <asp:BoundField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px" DataField="Unit"
                                            HeaderText="" Visible="false" />
                                        <asp:BoundField ItemStyle-HorizontalAlign="right" ItemStyle-Width="100px" DataFormatString="{0:f2}"
                                            DataField="Amount" HeaderText="AMOUNT" HeaderStyle-HorizontalAlign="right" />
                                    </Columns>
                                    <PagerTemplate>
                                    </PagerTemplate>
                                    <PageFooterTemplate>
                                        <br />
                                    </PageFooterTemplate>
                                </wc:ReportGridView>
                                <wc:ReportGridView runat="server" BorderWidth="1" ID="gvViswas" CssClass="left" GridLines="Both"
                                    AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false" PrintPageSize="10"
                                    AllowPrintPaging="true" Visible="false" Width="600px" CellPadding="2" CellSpacing="0"
                                    OnRowDataBound="gvVishwas_RowDataBound" Style="font-family: 'Trebuchet MS'; font-size: 11px;">
                                    <PageHeaderTemplate>
                                        <br />
                                        <br />
                                    </PageHeaderTemplate>
                                    <Columns>
                                        <asp:BoundField ItemStyle-HorizontalAlign="right" ItemStyle-Width="25px" DataFormatString="{0:f2}"
                                            DataField="Rate" HeaderText="Rate" />
                                        <asp:BoundField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="410px" DataField="Particulars"
                                            HeaderText="Particulars" />
                                        <asp:BoundField ItemStyle-HorizontalAlign="right" ItemStyle-Width="40px" DataFormatString="{0:f2}"
                                            DataField="NetRate" HeaderText="NetRate" />
                                        <asp:BoundField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25px" DataField="Bundles"
                                            HeaderText="No. of Bundles" />
                                        <asp:BoundField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25px" DataField="Rods"
                                            HeaderText="No. of Rods" />
                                        <asp:BoundField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25px" DataField="Qty"
                                            HeaderText="Weight" />
                                        <asp:BoundField ItemStyle-HorizontalAlign="right" ItemStyle-Width="50px" DataFormatString="{0:f2}"
                                            DataField="Amount" HeaderText="Amount" />
                                    </Columns>
                                    <PagerTemplate>
                                    </PagerTemplate>
                                    <PageFooterTemplate>
                                        <br />
                                    </PageFooterTemplate>
                                </wc:ReportGridView>
                                <wc:ReportGridView runat="server" BorderWidth="1" ID="gvGenNoUnitRate" CssClass="left"
                                    GridLines="Both" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                    PrintPageSize="45" AllowPrintPaging="true" Visible="false" Width="600px" OnRowDataBound="gvGenNoUnitRate_RowDataBound"
                                    Style="font-family: 'Trebuchet MS'; font-size: 11px;">
                                    <PageHeaderTemplate>
                                        <br />
                                        <br />
                                    </PageHeaderTemplate>
                                    <HeaderStyle Height="30px" VerticalAlign="Middle" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="SI.No">
                                            <ItemTemplate>
                                                <%# ((GridViewRow)Container).RowIndex + 1%>
                                            </ItemTemplate>
                                            <ItemStyle Width="50px" HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:BoundField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="360px" DataField="Particulars"
                                            HeaderText="PARTICULARS" />
                                        <asp:BoundField ItemStyle-HorizontalAlign="right" ItemStyle-Width="50px" DataFormatString="{0:f2}"
                                            DataField="NetRate" HeaderText="NETRATE" Visible="false" />
                                        <asp:BoundField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px" DataField="Qty"
                                            HeaderStyle-HorizontalAlign="left" HeaderText="QTY" />
                                        <asp:BoundField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px" DataField="Unit"
                                            HeaderText="" Visible="false" />
                                        <asp:BoundField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px" DataFormatString="{0:f2}"
                                            HeaderStyle-HorizontalAlign="left" DataField="Rate" HeaderText="RATE" Visible="false" />
                                        <asp:BoundField ItemStyle-HorizontalAlign="right" ItemStyle-Width="70px" DataFormatString="{0:f2}"
                                            DataField="Amount" HeaderText="AMOUNT" HeaderStyle-HorizontalAlign="right" />
                                    </Columns>
                                    <PagerTemplate>
                                    </PagerTemplate>
                                    <PageFooterTemplate>
                                        <br />
                                    </PageFooterTemplate>
                                </wc:ReportGridView>
                                <wc:ReportGridView runat="server" BorderWidth="1" ID="gvVishwasCement" CssClass="left"
                                    GridLines="Both" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                    PrintPageSize="45" AllowPrintPaging="true" Visible="false" Width="600px" OnRowDataBound="gvVishwasCement_RowDataBound"
                                    Style="font-family: 'Trebuchet MS'; font-size: 11px;">
                                    <PageHeaderTemplate>
                                        <br />
                                        <br />
                                    </PageHeaderTemplate>
                                    <Columns>
                                        <asp:BoundField ItemStyle-HorizontalAlign="right" ItemStyle-Width="40px" DataFormatString="{0:f2}"
                                            DataField="Rate" HeaderText="Rate" />
                                        <asp:BoundField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="410px" DataField="Particulars"
                                            HeaderText="Particulars" />
                                        <asp:BoundField ItemStyle-HorizontalAlign="right" ItemStyle-Width="40px" DataFormatString="{0:f2}"
                                            DataField="NetRate" HeaderText="NetRate" />
                                        <asp:BoundField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40px" DataField="Qty"
                                            HeaderText="No of Bags" />
                                        <asp:BoundField ItemStyle-HorizontalAlign="right" ItemStyle-Width="70px" DataFormatString="{0:f2}"
                                            DataField="Amount" HeaderText="Amount" />
                                    </Columns>
                                    <PagerTemplate>
                                    </PagerTemplate>
                                    <PageFooterTemplate>
                                        <br />
                                    </PageFooterTemplate>
                                </wc:ReportGridView>
                                <div id="dvTotal600" runat="server">
                                    <div id="dvAmount600" runat="server">
                                        <table width="600px" border="0" cellpadding="2" cellspacing="0" style="font-family: 'Trebuchet MS';
                                            font-size: 11px;">
                                            <tr>
                                                <td width="340px">
                                                    &nbsp;
                                                </td>
                                                <td align="left" width="189px">
                                                    Total
                                                    <asp:Label ID="lblCurrTotal600" runat="server" CssClass="lblFont"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align="right" width="70px">
                                                    <asp:Label ID="lblAmt600" CssClass="lblFont" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div id="dvDiscountTotal600" runat="server" visible="false" align="center">
                                        <table width="600px" border="0" cellpadding="2" cellspacing="0" style="font-family: 'Trebuchet MS';
                                            font-size: 11px;">
                                            <tr>
                                                <td width="340px">
                                                    &nbsp;
                                                </td>
                                                <td align="left" width="189px">
                                                    Discount
                                                    <asp:Label ID="lblCurrDisp600" runat="server" CssClass="lblFont"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align="right" width="70px">
                                                    <asp:Label ID="lblGrandDiscount600" CssClass="lblFont" Text="0" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div id="dvVatTotal600" runat="server" visible="false" align="center">
                                        <table width="600px" border="0" cellpadding="2" cellspacing="0" style="font-family: 'Trebuchet MS';
                                            font-size: 11px;">
                                            <tr>
                                                <td width="340px">
                                                    &nbsp;
                                                </td>
                                                <td align="left" width="189px">
                                                    VAT &nbsp;<asp:Label ID="lblVatDisplay600" runat="server" CssClass="lblFont"></asp:Label>&nbsp;
                                                    <asp:Label ID="lblCurrVAT600" runat="server" CssClass="lblFont"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align="right" width="70px">
                                                    <asp:Label ID="lblGrandVat600" CssClass="lblFont" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div id="dvFrgTotal600" runat="server" visible="false" align="center">
                                        <table width="600px" border="0" cellpadding="2" cellspacing="0" style="font-family: 'Trebuchet MS';
                                            font-size: 11px;">
                                            <tr>
                                                <td width="343px">
                                                    &nbsp;
                                                </td>
                                                <td align="left" width="186px">
                                                    Loading / Unloading / Freight
                                                    <asp:Label ID="lblCurrLoad600" runat="server" CssClass="lblFont"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align="right" width="70px">
                                                    <asp:Label ID="lblFg600" CssClass="lblFont" Text="0" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div id="dvCSTTotal600" runat="server" visible="false" align="center">
                                        <table width="600px" border="0" cellpadding="2" cellspacing="0" style="font-family: 'Trebuchet MS';
                                            font-size: 11px;">
                                            <tr>
                                                <td width="340px">
                                                    &nbsp;
                                                </td>
                                                <td align="left" width="189px">
                                                    CST &nbsp;<asp:Label ID="lblCSTDisplay600" runat="server" CssClass="lblFont"></asp:Label>&nbsp;
                                                    <asp:Label ID="lblCurrCST600" runat="server" CssClass="lblFont"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align="right" width="70px">
                                                    <asp:Label ID="lblGrandCst600" CssClass="lblFont" Text="0" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <table width="600px" border="0" cellpadding="2" cellspacing="0" style="font-family: 'Trebuchet MS';
                                        font-size: 11px;" align="center">
                                        <tr>
                                            <td align="left" width="340px">
                                                <asp:Label ID="lblCurrRs600" runat="server" CssClass="lblFont" />
                                            </td>
                                            <td align="left" width="189px">
                                                GRAND TOTAL
                                                <asp:Label ID="lblCurrGrandTTL600" runat="server"></asp:Label>
                                            </td>
                                            <td width="1px">
                                            </td>
                                            <td align="right" width="70px" style="border-top: 1px solid black; border-bottom: 1px solid black;">
                                                <asp:Label ID="lblNetTotal600" CssClass="lblFont" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div style="text-align: left" align="center">
                                    <div id="paymode" runat="server" align="left">
                                    </div>
                                    <br />
                                    <div id="divMultiPayment" runat="server" style="text-align: right" align="center">
                                        <wc:ReportGridView ID="GrdViewReceipt" runat="server" CssClass="left" Width="100%"
                                            GridLines="Both" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                            Style="font-family: 'Trebuchet MS'; font-size: 11px;" PrintPageSize="45" AllowPrintPaging="true">
                                            <EmptyDataRowStyle CssClass="GrdContent" />
                                            <Columns>
                                                <asp:BoundField DataField="Debi" HeaderText="Bank Name / Cash" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="left"/>
                                                <asp:BoundField DataField="ChequeNo" HeaderText="CHEQUE" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="left"/>
                                                <asp:BoundField DataField="AMOUNT" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"
                                                    HeaderText="Amount" HeaderStyle-Wrap="false" />
                                            </Columns>
                                            <PagerTemplate>
                                                Goto Page
                                                <asp:DropDownList ID="ddlPageSelector" runat="server" AutoPostBack="true" SkinID="skinPagerDdlBox">
                                                </asp:DropDownList>
                                                <asp:Button Text="First" CommandName="Page" CommandArgument="First" runat="server"
                                                    ID="btnFirst" />
                                                <asp:Button Text="Previous" CommandName="Page" CommandArgument="Prev" runat="server"
                                                    ID="btnPrevious" />
                                                <asp:Button Text="Next" CommandName="Page" CommandArgument="Next" runat="server"
                                                    ID="btnNext" />
                                                <asp:Button Text="Last" CommandName="Page" CommandArgument="Last" runat="server"
                                                    ID="btnLast" />
                                            </PagerTemplate>
                                        </wc:ReportGridView>
                                    </div>
                                    <div id="divBankPaymode" runat="server" align="center">
                                        <table cellspacing="0" style="text-align: left; font-family: 'Trebuchet MS'; font-size: 11px;" align="center">
                                            <tr>
                                                <td style="white-space: normal; vertical-align: top">
                                                    Cheque / Credit Card No. :
                                                </td>
                                                <td style="vertical-align: top">
                                                    <div id="divCreditCardNo" runat="server">
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top">
                                                    Bank Name
                                                </td>
                                                <td style="vertical-align: top">
                                                    <div id="divBankName" runat="server">
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="dvFooter600" runat="server" align="center">
                    <table width="600px" border="0" cellpadding="2" cellspacing="0" style="font-family: 'Trebuchet MS';
                        font-size: 11px; border-right: 1px solid black; border-left: 1px solid black;
                        border-bottom: 1px solid black; border-top: 1px solid black;" align="center">
                        <tr>
                            <td>
                                <table width="600px" border="0" cellpadding="2" cellspacing="0" style="font-family: 'Trebuchet MS';
                                    font-size: 11px;">
                                    <tr>
                                        <td width="350px" align="left">
                                            <div id="divNotes" runat="server">
                                            </div>
                                        </td>
                                        <td align="center" width="250px">
                                            For
                                            <asp:Label Font-Bold="true" ID="lblSignCompany600" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <hr />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            Benit & Co
                                        </td>
                                        <td align="left">
                                            Despatched From : <asp:Label Font-Bold="true" ID="lbldespatched" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="rowexec" runat="server">
                                        <td align="left">
                                            For Service and Demo contact Customer Care
                                        </td>
                                        <td align="left">
                                            Executive : <asp:Label Font-Bold="true" ID="lblexecutive" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="rownarration" runat="server">
                                        <td align="left">
                                            
                                        </td>
                                        <td align="left">
                                            Narration : <asp:Label Font-Bold="true" ID="lblnarration" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            89400 09090, 97879 70707<br /> 
                                            customercare@benitco.com<br />
                                            <asp:Label Font-Bold="true" ID="Label1" runat="server" Text="Customer Care working hours"></asp:Label> : Mon To Sat 10AM to 6PM.<br />
                                        </td>
                                        <td align="left" style="font-size: :xx-small;">
                                            <div id="viswasfooter600" visible="false" runat="server">
                                                X Our Liability ceases as soon as the goods leave our place.<br />
                                                X Weight recorded at our weighbridge is final.<br />
                                                X All disputes subject to Aruppukottai jurisdiction.<br />
                                            </div>
                                            <div id="dvGeneral600" visible="false" runat="server">
                                                X Goods once sold cannot be taken back.<br />
                                                X All disputes subject to madurai jurisdiction.<br />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                </td>
                </tr>
                </table>
            </div>
            <%--Start PPF2 footer--%>
            <div id="PREPRINTEDFORMAT2" visible="false" runat="server" align="center">
                <div id="dvHeaderPF2" runat="server" visible="false" align="center">
                    <br />
                    <table border="0" width="700px" cellpadding="2" cellspacing="0" style="font-family: 'Trebuchet MS';
                        font-size: 11px;" align="center">
                        <tr>
                            <td>
                            </td>
                            <td>
                                <table width="100%" border="0" style="font-family: 'Trebuchet MS'; font-size: 14px;">
                                    <tr>
                                        <td>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblTinNumberPF2" runat="server" CssClass="lblFont"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblCSTNumberPF2" runat="server" CssClass="lblFont"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" height="40px">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="left" width="350px">
                                <table border="0" style="font-family: 'Trebuchet MS'; font-size: 14px;">
                                    <tr>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblSupplierPF2" runat="server" Font-Bold="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblSuppAdd1PF2" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblSuppAdd2PF2" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblSuppAdd3PF2" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblSuppPhPF2" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;<asp:Label ID="lblSuppTin"
                                                runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table width="100%" border="0" style="font-family: 'Trebuchet MS'; font-size: 14px;">
                                    <tr>
                                        <td width="50%">
                                        </td>
                                        <td align="right" style="font-weight: bold">
                                            <table cellpadding="0" cellspacing="0" style="font-family: 'Trebuchet MS'; font-size: 14px;">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblBillnoPF2" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblBillCopyF2" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblBillDatePF2" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblBoxes" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblLorry" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divFooterPF2" runat="server" visible="false" align="center">
                    <table width="700px" border="0" cellpadding="2" cellspacing="0" style="font-family: 'Trebuchet MS';
                        font-size: 11px; border: 0px solid black;">
                        <tr>
                            <td>
                                <br />
                                <br />
                                <wc:ReportGridView runat="server" BorderWidth="0" ID="gvGeneralPF2" CssClass="left"
                                    GridLines="Both" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                    PrintPageSize="45" ShowHeader="false" AllowPrintPaging="true" Visible="false"
                                    Width="690px" BorderColor="Transparent" OnRowDataBound="gvGeneralPF2_RowDataBound"
                                    Style="font-family: 'Trebuchet MS'; font-size: 14px;">
                                    <PageHeaderTemplate>
                                        <br />
                                        <br />
                                    </PageHeaderTemplate>
                                    <Columns>
                                        <asp:BoundField ItemStyle-HorizontalAlign="left" ItemStyle-Width="80px" DataFormatString="{0:f2}"
                                            DataField="Rate" HeaderText="Rate" />
                                        <asp:BoundField ItemStyle-HorizontalAlign="left" ItemStyle-Width="480px" DataField="Particulars"
                                            HeaderText="Description" />
                                        <asp:BoundField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" DataField="Qty"
                                            HeaderText="Qty" />
                                        <asp:BoundField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40px" DataField="Unit"
                                            Visible="false" HeaderText="" />
                                        <asp:TemplateField ItemStyle-HorizontalAlign="right" ItemStyle-Width="110px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAmtt" runat="server" Text='<%#Eval("Amount")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerTemplate>
                                    </PagerTemplate>
                                    <PageFooterTemplate>
                                        <br />
                                        <br />
                                    </PageFooterTemplate>
                                </wc:ReportGridView>
                                <br />
                                <asp:Label ID="lblRemarks" runat="server"></asp:Label>
                                <table width="700px" border="0" cellpadding="2" cellspacing="0" style="font-family: 'Trebuchet MS';
                                    font-size: 12px;">
                                    <tr>
                                        <td align="left" width="340px">
                                        </td>
                                        <td align="left" width="189px">
                                        </td>
                                        <td align="right" width="120px">
                                            <asp:Label ID="lblTotalPf2" CssClass="lblFont" runat="server" Style="font-family: 'Trebuchet MS';
                                                font-size: 14px; font-weight: Bold"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <%--End PPF2 footer--%>
            <div id="A4FORMAT" runat="server" visible="false" align="center">
                <div id="dvHeader" runat="server" align="center">
                    <table width="700px" border="0" cellpadding="2" cellspacing="0" class="lblFont" style="font-family: 'Trebuchet MS';
                        font-size: 14px; border: 1px solid black;">
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel23" runat="server" UpdateMode="Always">
                                    <ContentTemplate>
                                        <table width="700px" style="font-family: 'Trebuchet MS'; font-size: 14px;">
                                            <tr>
                                                <td width="125px">
                                                </td>
                                                <td valign="top" align="center" width="450px">
                                                    <table style="font-family: 'Trebuchet MS'; text-align: center; font-size: 14px;">
                                                        <tr>
                                                            <td style="font-size: 22px; text-transform: uppercase">
                                                                <asp:Label Font-Bold="true" ID="lblCompany" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblAddress" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblCity" runat="server" />
                                                                -
                                                                <asp:Label ID="lblPincode" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblState" runat="server"> </asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr id="trVATTAX" runat="server">
                                                            <td valign="top" style="background-color: Black; color: White;">
                                                                <b>TAX INVOICE</b><asp:Label ID="lblHeader" runat="server" Font-Bold="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td align="right" width="125px" valign="top">
                                                    <table border="0" style="font-family: 'Trebuchet MS'; font-size: 14px;">
                                                        <tr id="trTINCST" runat="server">
                                                            <td align="left">
                                                                TIN
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td align="left">
                                                                <asp:Label ID="lblTNGST" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr id="trCST" runat="server">
                                                            <td align="left">
                                                                CST
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td align="left">
                                                                <asp:Label ID="lblGSTno" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                Ph
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td align="left">
                                                                <asp:Label ID="lblPhone" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                    <table width="700px" border="0" cellpadding="2" cellspacing="0" class="lblFont" style="font-family: 'Trebuchet MS';
                        font-size: 14px; border-top: 1px solid black; border-right: 1px solid black;
                        border-left: 1px solid black; border-bottom: 0px solid black;" align="center">
                        <tr>
                            <td style="text-align: left">
                                To
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="left" valign="top">
                                <table border="0" style="font-family: 'Trebuchet MS'; font-size: 14px; width: 700px">
                                    <tr>
                                        <td style="padding-left: 15px" width="400px">
                                            <asp:Label ID="lblSupplier" runat="server" Font-Bold="true" />
                                        </td>
                                        <td width="100px">
                                        </td>
                                        <td width="200px">
                                            <table border="0" cellpadding="0" cellspacing="0" style="font-family: 'Trebuchet MS';
                                                font-size: 14px;">
                                                <tr>
                                                    <td width="50px">
                                                        BillNo
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblBillno" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <%--<tr id="trTINCST1" runat="server" visible="false">
                                            <td align="left">
                                             BillNo : &nbsp;
                                             <asp:Label ID="lblBillNo1" runat="server"></asp:Label>
                                            </td>
                                            <td></td>
                                            <td>
                                            </td>
                                        </tr>  --%>
                                    <tr>
                                        <td style="padding-left: 15px">
                                            <asp:Label ID="lblSuppAdd1" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <table border="0" cellpadding="0" cellspacing="0" style="font-family: 'Trebuchet MS';
                                                font-size: 14px;">
                                                <tr>
                                                    <td width="50px">
                                                        Date
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblBillDate" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 15px">
                                            <asp:Label ID="lblSuppAdd2" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <table border="0" cellpadding="0" cellspacing="0" style="font-family: 'Trebuchet MS';
                                                font-size: 14px;">
                                                <tr>
                                                    <td width="50px">
                                                        TIN
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCustTIN" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 15px">
                                            <asp:Label ID="lblSuppAdd3" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <table border="0" cellpadding="0" cellspacing="0" style="font-family: 'Trebuchet MS';
                                                font-size: 14px;">
                                                <tr>
                                                    <td width="50px">
                                                        Ph
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblSuppPh" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <%--End Header--%>
                </div>
                <%-- Start Product Details --%>
                <div id="dvFormat" runat="server" align="center">
                    <table width="702px" cellpadding="2" cellspacing="0" class="lblFont" style="font-family: 'Trebuchet MS';
                        font-size: 14px; border-top: 1px solid black; border-right: 1px solid black;
                        border-left: 1px solid black; border-bottom: 1px solid black;">
                        <tr>
                            <td>
                                <table style="font-family: 'Trebuchet MS'; font-size: 14px;">
                                    <tr>
                                        <td>
                                            <wc:ReportGridView runat="server" BorderWidth="1" ID="gvGeneral" CssClass="left"
                                                GridLines="Both" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                                PrintPageSize="45" AllowPrintPaging="true" Visible="false" Width="694px" OnRowDataBound="gvGeneral_RowDataBound"
                                                Style="font-family: 'Trebuchet MS'; font-size: 14px;">
                                                <PageHeaderTemplate>
                                                    <br />
                                                    <br />
                                                </PageHeaderTemplate>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SI.No">
                                                        <ItemTemplate>
                                                            <%# ((GridViewRow)Container).RowIndex + 1%>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="50px" HorizontalAlign="left" />
                                                        <HeaderStyle HorizontalAlign="left" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="360px" DataField="Particulars"
                                                        HeaderText="PARTICULARS" />
                                                    <asp:BoundField ItemStyle-HorizontalAlign="right" ItemStyle-Width="50px" DataFormatString="{0:f2}"
                                                        DataField="NetRate" HeaderText="NETRATE" Visible="false" />
                                                    <asp:BoundField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px" DataField="Qty"
                                                        HeaderStyle-HorizontalAlign="left" HeaderText="QTY" />
                                                    <asp:BoundField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px" DataField="Unit"
                                                        HeaderText="" Visible="false" />
                                                    <asp:BoundField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px" DataFormatString="{0:f2}"
                                                        HeaderStyle-HorizontalAlign="left" DataField="Rate" HeaderText="RATE" />
                                                    <asp:BoundField ItemStyle-HorizontalAlign="right" ItemStyle-Width="70px" DataFormatString="{0:f2}"
                                                        DataField="Amount" HeaderText="AMOUNT" HeaderStyle-HorizontalAlign="right" />
                                                </Columns>
                                                <PagerTemplate>
                                                </PagerTemplate>
                                                <PageFooterTemplate>
                                                    <br />
                                                </PageFooterTemplate>
                                            </wc:ReportGridView>
                                            <%--KRISHNAVELU 12 - JULY - 2010 --%>
                                            <wc:ReportGridView runat="server" BorderWidth="1" ID="gvFormat3" CssClass="left"
                                                GridLines="Both" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                                PrintPageSize="45" AllowPrintPaging="true" Visible="false" Width="694px" OnRowDataBound="gvFormat3_RowDataBound"
                                                Style="font-family: 'Trebuchet MS'; font-size: 14px;">
                                                <PageHeaderTemplate>
                                                    <br />
                                                    <br />
                                                </PageHeaderTemplate>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SI.No">
                                                        <ItemTemplate>
                                                            <%# ((GridViewRow)Container).RowIndex + 1%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="410px" DataField="Particulars"
                                                        HeaderText="Particulars" />
                                                    <asp:BoundField ItemStyle-HorizontalAlign="right" ItemStyle-Width="40px" DataFormatString="{0:f2}"
                                                        DataField="NetRate" HeaderText="NetRate" Visible="false" />
                                                    <asp:BoundField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40px" DataField="Qty"
                                                        HeaderText="Qty" />
                                                    <asp:BoundField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40px" DataField="Unit"
                                                        HeaderText="" />
                                                    <asp:BoundField ItemStyle-HorizontalAlign="right" ItemStyle-Width="40px" DataFormatString="{0:f2}"
                                                        DataField="Rate" HeaderText="Rate" />
                                                    <asp:BoundField ItemStyle-HorizontalAlign="right" ItemStyle-Width="40px" DataFormatString="{0:f2}"
                                                        DataField="Discount" HeaderText="Dis" />
                                                    <asp:BoundField ItemStyle-HorizontalAlign="right" ItemStyle-Width="40px" DataFormatString="{0:f2}"
                                                        DataField="VAT" HeaderText="VAT" />
                                                    <asp:BoundField ItemStyle-HorizontalAlign="right" ItemStyle-Width="40px" DataFormatString="{0:f2}"
                                                        DataField="CST" HeaderText="CST" />
                                                    <asp:BoundField ItemStyle-HorizontalAlign="right" ItemStyle-Width="70px" DataFormatString="{0:f2}"
                                                        DataField="Amount" HeaderText="Amount" />
                                                </Columns>
                                                <PagerTemplate>
                                                </PagerTemplate>
                                                <PageFooterTemplate>
                                                    <br />
                                                </PageFooterTemplate>
                                            </wc:ReportGridView>
                                            <div id="divFooter" runat="server">
                                                <div id="dvAmount" runat="server">
                                                    <table border="0" width="694px" cellpadding="2" cellspacing="0" style="font-family: 'Trebuchet MS';
                                                        font-size: 14px;">
                                                        <tr>
                                                            <td width="340px">
                                                                &nbsp;
                                                            </td>
                                                            <td align="left" width="189px">
                                                                Total
                                                                <asp:Label ID="lblCurrTotal" runat="server" CssClass="lblFont"></asp:Label>
                                                            </td>
                                                            <td width="1px">
                                                                :
                                                            </td>
                                                            <td align="right" width="70px">
                                                                <asp:Label ID="lblAmt" CssClass="lblFont" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div id="dvDiscountTotal" runat="server" visible="false">
                                                    <table border="0" width="694px" cellpadding="2" cellspacing="0" style="font-family: 'Trebuchet MS';
                                                        font-size: 14px;">
                                                        <tr>
                                                            <td width="340px">
                                                                &nbsp;
                                                            </td>
                                                            <td align="left" width="189px">
                                                                Discount
                                                                <asp:Label ID="lblCurrDisp" runat="server" CssClass="lblFont"></asp:Label>
                                                            </td>
                                                            <td width="1px">
                                                                :
                                                            </td>
                                                            <td align="right" width="70px">
                                                                <asp:Label ID="lblGrandDiscount" CssClass="lblFont" Text="0" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div id="dvVatTotal" runat="server" visible="false">
                                                    <table width="694px" border="0" cellpadding="2" cellspacing="0" style="font-family: 'Trebuchet MS';
                                                        font-size: 14px;">
                                                        <tr>
                                                            <td width="340px">
                                                                &nbsp;
                                                            </td>
                                                            <td align="left" width="189px">
                                                                VAT &nbsp;<asp:Label ID="lblVatDisplay" runat="server" CssClass="lblFont"></asp:Label>&nbsp;
                                                                <asp:Label ID="lblCurrVAT" runat="server" CssClass="lblFont"></asp:Label>
                                                            </td>
                                                            <td width="1px">
                                                                :
                                                            </td>
                                                            <td align="right" width="70px">
                                                                <asp:Label ID="lblGrandVat" CssClass="lblFont" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div id="dvFrgTotal" runat="server" visible="false">
                                                    <table width="694px" border="0" cellpadding="2" cellspacing="0" style="font-family: 'Trebuchet MS';
                                                        font-size: 14px;">
                                                        <tr>
                                                            <td width="343px">
                                                                &nbsp;
                                                            </td>
                                                            <td align="left" width="186px">
                                                                Loading / Unloading / Freight
                                                                <asp:Label ID="lblCurrLoad" runat="server" CssClass="lblFont"></asp:Label>
                                                            </td>
                                                            <td width="1px">
                                                                :
                                                            </td>
                                                            <td align="right" width="70px">
                                                                <asp:Label ID="lblFg" CssClass="lblFont" Text="0" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div id="dvCSTTotal" runat="server" visible="false">
                                                    <table width="694px" border="0" cellpadding="2" cellspacing="0" style="font-family: 'Trebuchet MS';
                                                        font-size: 14px;">
                                                        <tr>
                                                            <td width="340px">
                                                                &nbsp;
                                                            </td>
                                                            <td align="left" width="189px">
                                                                CST &nbsp;<asp:Label ID="lblCSTDisplay" runat="server" CssClass="lblFont"></asp:Label>&nbsp;
                                                                <asp:Label ID="lblCurrCST" runat="server" CssClass="lblFont"></asp:Label>
                                                            </td>
                                                            <td width="1px">
                                                                :
                                                            </td>
                                                            <td align="right" width="70px">
                                                                <asp:Label ID="lblGrandCst" CssClass="lblFont" Text="0" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <table width="694px" border="0" cellpadding="2" cellspacing="0" style="font-family: 'Trebuchet MS';
                                                    font-size: 14px;">
                                                    <tr>
                                                        <td align="left" width="340px">
                                                            <asp:Label ID="lblCurrRs" runat="server" CssClass="lblFont" />
                                                        </td>
                                                        <td align="left" width="189px">
                                                            GRAND TOTAL
                                                            <asp:Label ID="lblCurrGrandTTL" runat="server"></asp:Label>
                                                        </td>
                                                        <td width="1px">
                                                        </td>
                                                        <td align="right" width="70px" style="border-top: 1px solid black; border-bottom: 1px solid black;">
                                                            <asp:Label ID="lblNetTotal" CssClass="lblFont" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <%-- End Product Details --%>
                <%--Start Footer--%>
                <div id="dvFooter" runat="server" align="center">
                    <table width="700px" border="0" cellpadding="2" cellspacing="0" style="font-family: 'Trebuchet MS';
                        font-size: 14px; border-right: 1px solid black; border-left: 1px solid black;
                        border-bottom: 1px solid black; border-top: 1px solid black;">
                        <tr>
                            <td>
                                <table width="700px" border="0" cellpadding="2" cellspacing="0" style="font-family: 'Trebuchet MS';
                                    font-size: 14px;">
                                    <tr>
                                        <td width="350px">
                                            &nbsp;<br />
                                            <br />
                                            <br />
                                        </td>
                                        <td align="center" width="250px">
                                            For
                                            <asp:Label Font-Bold="true" ID="lblSignCompany" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="left" style="font-size: :xx-small;">
                                            <div id="viswasfooter" visible="false" runat="server">
                                                X Our Liability ceases as soon as the goods leave our place.<br />
                                                X Weight recorded at our weighbridge is final.<br />
                                                X All disputes subject to Aruppukottai jurisdiction.<br />
                                            </div>
                                            <div id="dvGeneral" visible="false" runat="server">
                                                X Goods once sold cannot be taken back.<br />
                                                X All disputes subject to madurai jurisdiction.<br />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <%--End Footer--%>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
