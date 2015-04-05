<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductSalesBill.aspx.cs"
    Inherits="ProductSalesBill" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit"
    TagPrefix="cc1" %>

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
        function CallPrint() {

            var strid;
            var printType = document.getElementById("PrintDropDownList");
            if (printType[printType.selectedIndex].value == "1") {

                //document.getElementById("divPrint").visible = "true";
                //document.getElementById("divPrintEx").visible = "false";

                strid = 'divPrint';
            }
            else if (printType[printType.selectedIndex].value == "2") {

                //document.getElementById("divPrint").visible = "false";
                //document.getElementById("divPrintEx").visible = "true";

                strid = 'divPrintEx';
            }

            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('', '', 'letf=0,top=0,width=700,height=400,toolbar=0,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();

        }

    </script>

    <style type="text/css">
        .auto-style6 {
            width: 118px;
            padding: 3px 3px 3px 10px;
            font-weight: bold;
        }

        .auto-style7 {
            width: 238px;
        }

        .auto-style8 {
            width: 245px;
            padding-right: 3px;
        }

        .auto-style12 {
            width: 114px;
        }

        .auto-style16 {
            width: 227px;
            color: #00007a;
        }

        .auto-style18 {
            width: 245px;
        }

        .auto-style19 {
            width: 133px;
        }

        .auto-style20 {
            width: 222px;
            padding-left: 5px;
        }

        .auto-style21 {
            width: 700px;
            padding: 3px 3px 3px 10px;
        }

        .list li {
            /*display: inline;*/
            /*list-style: none;*/
            /*list-style-type: disc;*/
            display: inline-block;
        }

        .auto-style22 {
            width: 150px;
        }

        .auto-style23 {
            width: 400px;
            padding: 3px 3px 3px 10px;
            text-align: right;
        }

        .headerGrid {
            padding: 3px 3px 3px 3px;
        }

        .itemGrid {
            padding: 3px 3px 3px 3px;
            font-weight: bold;
        }

        .auto-style24 {
            width: 59px;
        }

        .auto-style26 {
            width: 123px;
            padding: 3px 3px 3px 10px;
            text-align: right;
        }

        .auto-style32 {
            width: 110px;
            padding: 2px 2px 2px 2px;
        }

        .auto-style33 {
            width: 118px;
            padding: 3px 3px 3px 10px;
            font-weight: bold;
            font-size: 12px;
        }

        .auto-styleDiv {
            padding: 3px 3px 3px 3px;
        }
    </style>

</head>
<body style="font-family: 'Trebuchet MS'; font-size: 14px;">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="6000">
        </asp:ScriptManager>
        <div style="width: 100%; text-align: center" align="center">
            <br />
            <input type="button" value="Print " id="Button1" runat="Server" class="printButton"
                onclick="javascript: CallPrint()" />&nbsp;
        <asp:Button ID="btnBack" Text="Back" runat="server" class="printButton" OnClick="btnBack_Click" />
            <br />
            <div id="Div2" runat="server" align="center">
                <table width="600px" border="0" cellpadding="2" cellspacing="0" style="font-family: 'Trebuchet MS'; font-size: 11px;">
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
                                        <td style="width: 10%">Copy
                                        </td>
                                        <td style="width: 40%">&nbsp;&nbsp;<asp:TextBox ID="txtCopy" runat="server" Text="Customer Copy" CssClass="cssTextBox"
                                            Width="120px"></asp:TextBox>
                                        </td>
                                        <td style="width: 20%">
                                            <asp:Label ID="lblDivisions" runat="server" Text="Division"></asp:Label>
                                        </td>
                                        <td style="width: 30%">
                                            <div id="divDiv" runat="server" style="width: 158px; font-family: 'Trebuchet MS';">
                                                <asp:DropDownList ID="ddDivsions" runat="server" DataTextField="DivisionName" BackColor="#90c9fc" DataValueField="DivisionID" Style="border: 1px solid Blue" Height="24px"
                                                    Width="100%" AppendDataBoundItems="True" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddDivsions_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True" Value="0" style="background-color: #90c9fc">Select Division</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Print Type :</td>
                                        <td>
                                            <asp:DropDownList ID="PrintDropDownList" runat="server" BackColor="#90c9fc" Style="border: 1px solid Blue" Height="24px"
                                                Width="100%" OnSelectedIndexChanged="PrintDropDownList_SelectedIndexChanged" AutoPostBack="True">
                                                <asp:ListItem Selected="True" Value="1" style="background-color: #90c9fc">New Bill</asp:ListItem>
                                                <asp:ListItem Value="2" style="background-color: #90c9fc">Pre Printed Bill</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </div>

                            <table width="600px" cellpadding="2" cellspacing="2" align="center">
                                <tr>
                                    <td colspan="2" style="text-align: center">
                                        <asp:Button Text="Enter" ID="btnCopy" runat="server" OnClick="btnCopy_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <br />

            <div id="divPrint" runat="server" style="font-family: 'Trebuchet MS'; font-size: 11px;" align="center">

                <div>
                    <div id="A4FORMAT" runat="server" visible="false" align="center">
                        <div id="dvHeader" runat="server" align="center">
                            <table width="700px" border="0" cellpadding="2" cellspacing="0" class="lblFont" style="font-family: 'Trebuchet MS'; font-size: 14px;">
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel23" runat="server" UpdateMode="Always">
                                            <ContentTemplate>
                                                <table style="font-family: 'Trebuchet MS'; font-size: 14px; width: 700px;">
                                                    <tr>
                                                        <td class="auto-style12">
                                                            <asp:Image ID="Image4" runat="server" ImageUrl="~/img/Benit-Icon.png" Width="120px" Height="114px" />
                                                        </td>
                                                        <td valign="top" align="left" class="auto-style24">

                                                            <table border="0" style="font-family: Verdana, Geneva, Tahoma, sans-serif; width: 210px; height: 90px;">

                                                                <tr id="tr1" runat="server">
                                                                    <td align="center" class="auto-style16" style="font-weight: 600; font-size: 25px;">
                                                                        <asp:Label ID="Label4" Text="Benit & CO" runat="server" />
                                                                    </td>
                                                                </tr>

                                                                <tr id="trTINCST" runat="server">
                                                                    <td align="center" class="auto-style16" style="font-weight: 400; font-size: 18px;">
                                                                        <asp:Label ID="Label10" Text="Tax Invoice" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr id="trCST" runat="server">
                                                                    <td align="center" class="auto-style16" style="font-weight: 400; font-size: 16px;">
                                                                        <asp:Label ID="Label11" Text="Customer Copy" runat="server" />
                                                                    </td>

                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="auto-style16" style="font-weight: 400; font-size: 16px;">
                                                                        <asp:Label ID="Label12" Text="Original" runat="server" />
                                                                    </td>
                                                                </tr>
                                                            </table>

                                                        </td>

                                                        <td align="right" width="150px" valign="top">

                                                            <asp:Panel BackColor="#c0c0c0" Height="95px" 
                                                                runat="server" ID="MainPanel" Width="100%" HorizontalAlign="Center" Style="margin-left: 0px">
                                                                <table style="font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif; font-weight: bold; text-align: left; font-size: 12px;">
                                                                    <tr>
                                                                        <td class="auto-style20">
                                                                            <asp:Label ID="lblCompany" runat="server"></asp:Label>,
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="auto-style20">
                                                                            <asp:Label ID="lblAddress" runat="server"></asp:Label>,
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="auto-style20">
                                                                            <asp:Label ID="lblCity" runat="server" />
                                                                            -
                                                                    <asp:Label ID="lblPincode" runat="server"></asp:Label>,
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="auto-style20">
                                                                            <asp:Label ID="lblState" runat="server"> </asp:Label>,
                                                                        </td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td class="auto-style20">Mobile:
                                                                            <asp:Label ID="lblPhn" runat="server"> </asp:Label>
                                                                        </td>
                                                                    </tr>

                                                                </table>
                                                            </asp:Panel>
                                                            <cc1:RoundedCornersExtender Corners="All" Radius="10" TargetControlID="MainPanel"
                                                                ID="RoundedCornersExtender1" runat="server">
                                                            </cc1:RoundedCornersExtender>

                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                            <table border="0" cellpadding="2" cellspacing="0" class="lblFont" style="font-family: 'Trebuchet MS'; font-size: 14px; margin-left: 0px; height: 121px; width: 682px;"
                                align="center">

                                <tr>
                                    <td class="auto-style8">
                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Always">
                                            <ContentTemplate>
                                                <table border="0" cellpadding="2" cellspacing="0" style="font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; font-size: 12px; width: 230px;">

                                                    <tr style=" text-align: left; color:black; font-size: 14px;">
                                                        <td style="padding-left: 5px; padding-bottom: 5px; padding-top: 5px;"><strong>Billing Address: </strong></td>
                                                    </tr>

                                                    <tr runat="server">

                                                        <td align="left" class="auto-style6" style="font-weight: bold;">
                                                            <asp:Label ID="lblSupplierName" runat="server"></asp:Label>
                                                        </td>

                                                    </tr>

                                                    <tr runat="server">

                                                        <td align="left" class="auto-style6" style="font-weight: bold;">
                                                            <asp:Label ID="lblSupplierCmpnyName" runat="server"></asp:Label>
                                                        </td>

                                                    </tr>

                                                    <tr runat="server">

                                                        <td align="left" class="auto-style6" style="font-weight: bold;">
                                                            <asp:Label ID="lblSupplierAddr1" runat="server"></asp:Label>
                                                        </td>

                                                    </tr>

                                                    <tr runat="server">

                                                        <td align="left" class="auto-style6" style="font-weight: bold;">
                                                            <asp:Label ID="lblSupplierAddr2" runat="server"></asp:Label>
                                                        </td>

                                                    </tr>

                                                    <tr runat="server">

                                                        <td align="left" class="auto-style6" style="font-weight: bold;">
                                                            <asp:Label ID="lblSupplierPhn" runat="server"></asp:Label>
                                                        </td>

                                                    </tr>

                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>

                                    <td class="auto-style18">
                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Always">
                                            <ContentTemplate>
                                                <table border="0" cellpadding="2" cellspacing="0" style="font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; font-size: 12px; width: 230px;">

                                                    <tr style=" text-align: left; color: black; font-size: 14px;">
                                                        <td style="padding-left: 5px; padding-bottom: 5px; padding-top: 5px;"><strong>Delivery Address: </strong></td>
                                                    </tr>

                                                    <tr runat="server">

                                                        <td align="left" class="auto-style6" style="font-weight: bold;">
                                                            <asp:Label ID="lblShipToName" runat="server"></asp:Label>
                                                        </td>

                                                    </tr>

                                                    <tr runat="server">

                                                        <td align="left" class="auto-style6" style="font-weight: bold;">
                                                            <asp:Label ID="lblShipToCmpnyName" runat="server"></asp:Label>
                                                        </td>

                                                    </tr>

                                                    <tr runat="server">

                                                        <td align="left" class="auto-style6" style="font-weight: bold;">
                                                            <asp:Label ID="lblShipToAddr1" runat="server"></asp:Label>
                                                        </td>

                                                    </tr>

                                                    <tr runat="server">

                                                        <td align="left" class="auto-style6" style="font-weight: bold;">
                                                            <asp:Label ID="lblShipToAddr2" runat="server"></asp:Label>
                                                        </td>

                                                    </tr>

                                                    <tr runat="server">

                                                        <td align="left" class="auto-style6" style="font-weight: bold;">
                                                            <asp:Label ID="lblShipToPhn" runat="server"></asp:Label>
                                                        </td>

                                                    </tr>

                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>

                                    <td class="auto-style7">
                                        <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Always">
                                            <ContentTemplate>
                                                <table border="0" cellpadding="2" cellspacing="0" style="font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; font-size: 12px; width: 228px; height: 121px;">

                                                    <tr>
                                                        <td class="auto-style26">Date: </td>
                                                        <td class="auto-style33" style="font-weight: bold; border: 2px solid #999999">
                                                            <asp:Label ID="lblBillDate" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td class="auto-style26">Invoice #: </td>
                                                        <td class="auto-style33" style="font-weight: bold; border-bottom: 2px solid #999999; border-left: 2px solid #999999; border-right: 2px solid #999999;">
                                                            <asp:Label ID="lblInvoice" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td class="auto-style26">Customer ID:&nbsp; </td>
                                                        <td class="auto-style33" style="font-weight: bold; border-bottom: 2px solid #999999; border-left: 2px solid #999999; border-right: 2px solid #999999;">
                                                            <asp:Label ID="lblCustomerID" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td class="auto-style26">Purchase Order #: </td>
                                                        <td class="auto-style33" style="font-weight: bold; border-bottom: 2px solid #999999; border-left: 2px solid #999999; border-right: 2px solid #999999;">
                                                            <asp:Label ID="lblPurchaseOrder" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td class="auto-style26">Payment Due Date: </td>
                                                        <td class="auto-style33" style="font-weight: bold; border-bottom: 2px solid #999999; border-left: 2px solid #999999; border-right: 2px solid #999999;">
                                                            <asp:Label ID="lblPaymentDue" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>

                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>

                                </tr>
                            </table>
                            <%--End Header--%>
                        </div>
                        <%-- Start Product Details --%>
                        <div id="dvFormat" runat="server" align="center" style="padding-top: 5px;">
                            <table cellpadding="2" cellspacing="0" class="lblFont" style="font-family: 'Trebuchet MS'; font-size: 14px; width: 698px;">
                                <tr>
                                    <td>
                                        <table style="font-family: 'Trebuchet MS'; font-size: 14px;">
                                            <tr>
                                                <td>
                                                    <wc:ReportGridView runat="server" BorderWidth="1" ID="gvGeneral" ShowHeaderWhenEmpty="true"
                                                        GridLines="Both" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                                        PrintPageSize="45" AllowPrintPaging="true" Visible="false" Width="100%" OnRowDataBound="gvGeneral_RowDataBound"
                                                        AlternatingRowStyle-BackColor="#999999"
                                                        Style="font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; font-weight: bold; font-size: 12px; border: 2px solid #999999">
                                                        <PageHeaderTemplate>
                                                            <br />
                                                            <br />
                                                        </PageHeaderTemplate>
                                                        <Columns>
                                                            <asp:BoundField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" DataField="SalesPerson"
                                                                HeaderText="Sales Person" HeaderStyle-ForeColor="Black"
                                                                ItemStyle-CssClass="itemGrid" HeaderStyle-CssClass="headerGrid" />
                                                            <asp:BoundField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="77px" DataFormatString="{0:f2}"
                                                                DataField="ShippingMethod" HeaderText="Shipping Method" Visible="true" HeaderStyle-ForeColor="Black"
                                                                ItemStyle-CssClass="itemGrid" HeaderStyle-CssClass="headerGrid" />
                                                            <asp:BoundField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="55px" DataField="ShippingTerms"
                                                                HeaderText="Shipping Terms" HeaderStyle-ForeColor="Black"
                                                                ItemStyle-CssClass="itemGrid" HeaderStyle-CssClass="headerGrid" />
                                                            <asp:BoundField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px" DataField="PaymentMode"
                                                                HeaderText="Payment Mode" Visible="true" HeaderStyle-ForeColor="Black"
                                                                ItemStyle-CssClass="itemGrid" HeaderStyle-CssClass="headerGrid" />
                                                            <asp:BoundField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="37px"
                                                                DataField="DueDate" HeaderText="Due Date" HeaderStyle-ForeColor="Black"
                                                                ItemStyle-CssClass="itemGrid" HeaderStyle-CssClass="headerGrid" />
                                                            <asp:BoundField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="45px" DataField="DeliveryDate"
                                                                HeaderText="Delivery Date" HeaderStyle-ForeColor="Black"
                                                                ItemStyle-CssClass="itemGrid" HeaderStyle-CssClass="headerGrid" />
                                                        </Columns>
                                                        <PagerTemplate>
                                                        </PagerTemplate>
                                                        <PageFooterTemplate>
                                                            <br />
                                                        </PageFooterTemplate>
                                                    </wc:ReportGridView>
                                                    <%--KRISHNAVELU 12 - JULY - 2010 --%>

                                                    <wc:ReportGridView runat="server" ID="gvItem" CssClass="left" AlternatingRowStyle-BackColor="#cccccc"
                                                        GridLines="Both" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                                        PrintPageSize="45" AllowPrintPaging="true" Visible="false" Width="100%" OnRowDataBound="gvItem_RowDataBound"
                                                        Style="font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; font-weight: bold; font-size: 12px; border: 2px solid #999999">
                                                        <PageHeaderTemplate>

                                                            <br />
                                                        </PageHeaderTemplate>
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="#" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderColor="#666666"
                                                                HeaderStyle-ForeColor="Black">
                                                                <ItemTemplate>
                                                                    <%# ((GridViewRow)Container).RowIndex + 1%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:BoundField ItemStyle-HorizontalAlign="Left" ItemStyle-BorderColor="#666666" ItemStyle-Width="290px" DataField="ProductItem"
                                                                HeaderText="Item" HeaderStyle-ForeColor="Black" ItemStyle-CssClass="itemGrid" HeaderStyle-CssClass="headerGrid" />
                                                            <%-- <asp:BoundField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="240px" DataFormatString="{0:f2}" ItemStyle-BorderColor="#666666"
                                                                DataField="ProductDesc" HeaderText="Description" HeaderStyle-BackColor="#003366" HeaderStyle-ForeColor="White" ItemStyle-CssClass="itemGrid" HeaderStyle-CssClass="headerGrid" />--%>
                                                            <asp:BoundField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px" DataField="Qty" ItemStyle-BorderColor="#666666"
                                                                HeaderText="Qty" HeaderStyle-ForeColor="Black" ItemStyle-CssClass="itemGrid" HeaderStyle-CssClass="headerGrid" />
                                                            <asp:BoundField ItemStyle-HorizontalAlign="Right" ItemStyle-Width="80px" DataField="Rate" ItemStyle-BorderColor="#666666"
                                                                HeaderText="Unit Price" HeaderStyle-ForeColor="Black" ItemStyle-CssClass="itemGrid" HeaderStyle-CssClass="headerGrid" />                                                        
                                                            <asp:BoundField Visible="true" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="65px" DataFormatString="{0:f2}" ItemStyle-BorderColor="#666666"
                                                                DataField="VAT" HeaderText="VAT %" HeaderStyle-ForeColor="Black" ItemStyle-CssClass="itemGrid" HeaderStyle-CssClass="headerGrid" />
                                                            <asp:BoundField Visible="true" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="90px" DataFormatString="{0:f2}" ItemStyle-BorderColor="#666666"
                                                                DataField="VATAmount" HeaderText="VAT Amount" HeaderStyle-ForeColor="Black" ItemStyle-CssClass="itemGrid" HeaderStyle-CssClass="headerGrid" />
                                                            <asp:BoundField Visible="true" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="70px" DataFormatString="{0:f2}" ItemStyle-BorderColor="#666666"
                                                                DataField="Discount" HeaderText="Discount" HeaderStyle-ForeColor="Black" ItemStyle-CssClass="itemGrid" HeaderStyle-CssClass="headerGrid" />

                                                            <asp:BoundField ItemStyle-HorizontalAlign="Right" ItemStyle-Width="125px" DataFormatString="{0:f2}" ItemStyle-BorderColor="#666666"
                                                                DataField="Amount" HeaderText="Total Price" HeaderStyle-ForeColor="Black" ItemStyle-CssClass="itemGrid" HeaderStyle-CssClass="headerGrid" />

                                                        </Columns>
                                                        <PagerTemplate>
                                                        </PagerTemplate>
                                                        <PageFooterTemplate>
                                                            <br />
                                                        </PageFooterTemplate>
                                                    </wc:ReportGridView>

                                                    <div id="PaymentMode">

                                                        <div class="auto-styleDiv" runat="server" align="left">
                                                            Payment Mode :
                                                            <asp:Label Font-Bold="true" ID="lblPayMode" runat="server"></asp:Label>
                                                        </div>

                                                        <div id="divMultiPayment" class="auto-styleDiv" runat="server" style="text-align: right" align="center">
                                                            <wc:ReportGridView ID="GrdViewReceipt" runat="server" CssClass="left" Width="100%"
                                                                GridLines="Both" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                                                Style="font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; font-weight: bold; font-size: 12px; border: 2px solid #999999"
                                                                PrintPageSize="45" AllowPrintPaging="true">
                                                                <EmptyDataRowStyle CssClass="GrdContent" />
                                                                <Columns>
                                                                    <asp:BoundField DataField="Debi" HeaderText="Bank Name / Cash" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="left"
                                                                        ItemStyle-BorderColor="#666666" HeaderStyle-ForeColor="Black" ItemStyle-CssClass="itemGrid" HeaderStyle-CssClass="headerGrid" />
                                                                    <asp:BoundField DataField="ChequeNo" HeaderText="CHEQUE" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="left"
                                                                        ItemStyle-BorderColor="#666666" HeaderStyle-ForeColor="Black" ItemStyle-CssClass="itemGrid" HeaderStyle-CssClass="headerGrid" />
                                                                    <asp:BoundField DataField="AMOUNT" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"
                                                                        HeaderText="Amount" HeaderStyle-Wrap="false" DataFormatString="{0:f2}"
                                                                        ItemStyle-BorderColor="#666666" HeaderStyle-ForeColor="Black" ItemStyle-CssClass="itemGrid" HeaderStyle-CssClass="headerGrid" />


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

                                                        <div id="divBankPaymode" class="auto-styleDiv" runat="server" align="center">
                                                            <table cellspacing="0" style="text-align: left; font-family: 'Trebuchet MS'; font-size: 11px; border: 2px solid #999999; " align="center"> 
                                                                <tr>
                                                                    <td style="white-space: normal; vertical-align: top">Cheque / Credit Card No. :
                                                                    </td>
                                                                    <td style="vertical-align: top">
                                                                        <div id="divCreditCardNo" runat="server">
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="vertical-align: top">Bank Name
                                                                    </td>
                                                                    <td style="vertical-align: top">
                                                                        <div id="divBankName" runat="server">
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>

                                                    <div id="divFooter" class="auto-styleDiv" runat="server">
                                                        <div id="dvAmount" runat="server">
                                                            <table border="0" cellpadding="2" cellspacing="0" style="font-family: 'Trebuchet MS'; width: 689px;">

                                                                <tr>
                                                                    <td class="auto-style19">
                                                                        <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Always">
                                                                            <ContentTemplate>
                                                                                <table border="0" cellpadding="2" cellspacing="0" style="font-weight: bold; font-size: 12px; width: 330px; font-family: 'Trebuchet MS';">

                                                                                    <tr style=" color: black; font-size: medium;">
                                                                                        <td style="padding-left: 10px; padding-bottom: 2px; padding-top: 2px;"><strong>For Service and Demo, Contact: </strong></td>
                                                                                    </tr>

                                                                                    <tr runat="server">

                                                                                        <td align="center" class="auto-style6" style="font-size: 14px; font-weight: bold; border-left: 2px solid #999999; border-right: 2px solid #999999; border-top: 2px solid #999999;">
                                                                                            <asp:Label ID="lblCustName" runat="server"></asp:Label>
                                                                                        </td>

                                                                                    </tr>

                                                                                    <tr runat="server">

                                                                                        <td style="border-left: 2px solid #999999; border-right: 2px solid #999999; border-bottom: 2px solid #999999;">
                                                                                            <table style="width: 325px; font-size: 14px; font-weight: bold;">
                                                                                                <tr>
                                                                                                    <td align="Left" class="auto-style32">Mobile
                                                                                                    </td>
                                                                                                    <td>
                                                                                                       : <asp:Label ID="lblCustPhn" runat="server"></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td align="Left" class="auto-style32">Email
                                                                                                    </td>
                                                                                                    <td>
                                                                                                       : <asp:Label ID="lblCustMailID" runat="server"></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td align="Left" class="auto-style32">Contact Timings
                                                                                                    </td>
                                                                                                    <td>
                                                                                                       : <asp:Label ID="lblCustTiming" runat="server"></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>

                                                                                </table>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </td>

                                                                    <td class="auto-style22 " style="padding-left: 140px;">
                                                                        <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Always">
                                                                            <ContentTemplate>
                                                                                <table border="0" cellpadding="2" cellspacing="0" style="font-family: 'Trebuchet MS'; font-size: 13px; width: 224px;">

                                                                                    <tr>
                                                                                        <td class="auto-style23">Subtotal </td>
                                                                                        <td class="auto-style6" style="font-weight: bold; text-align: right;">
                                                                                            <asp:Label ID="lblSubTotal" runat="server"></asp:Label>
                                                                                        </td>
                                                                                    </tr>

                                                                                    <%-- <tr>
                                                                                        <td class="auto-style23">Sales Tax Rate </td>
                                                                                        <td class="auto-style6" style="text-align: right;">
                                                                                            <asp:Label ID="lblSalesTaxRate" runat="server"></asp:Label>
                                                                                        </td>
                                                                                    </tr>--%>

                                                                                    <tr>
                                                                                        <td class="auto-style23">Sales Tax&nbsp; </td>
                                                                                        <td class="auto-style6" style="font-weight: bold; text-align: right;">
                                                                                            <asp:Label ID="lblSalesTax" runat="server"></asp:Label>
                                                                                        </td>
                                                                                    </tr>

                                                                                    <tr>
                                                                                        <td class="auto-style23">Discount</td>
                                                                                        <td class="auto-style6" style="font-weight: bold; text-align: right;">
                                                                                            <asp:Label ID="lblDiscount" runat="server"></asp:Label>
                                                                                        </td>
                                                                                    </tr>

                                                                                    <tr>
                                                                                        <td class="auto-style23">Total</td>
                                                                                        <td class="auto-style6" style="font-weight: bold; text-align: right;">
                                                                                            <asp:Label ID="lblTotal" runat="server"></asp:Label>
                                                                                        </td>
                                                                                    </tr>

                                                                                </table>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </td>

                                                                </tr>
                                                            </table>
                                                        </div>

                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="dvFooter" runat="server" align="center">
                            <table width="700px" border="0" cellpadding="2" cellspacing="0" style="font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; font-size: 12px;">
                                <tr>
                                    <td style="text-align: center; font-weight: bold;" class="auto-style21">Thank you for your business with us!
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style21" style=" padding-left:25px; text-align: left; font-weight: bold;">Make all Cheques payable to J&J Traders. Cheque payments are subject to realization.
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style21" style="font-weight: bold; text-align: left; border-bottom-style: solid; border-bottom-width: thick; border-bottom-color: #808080">

                                        <ul class="list">
                                            <li style="display: inline-block;">Goods once sold cannot be taken back.</li>
                                            <li style="display: inline-block;">All disputes subject to Madurai jurisdiction.</li>
                                        </ul>                                       
                                    </td>
                                </tr>
                            </table>
                        </div>

                    </div>

                </div>
            </div>

            <div id="divPrintEx" runat="server" visible="false" style="font-family: 'Trebuchet MS'; font-size: 11px;" align="center">

                <div>
                    <div id="A4FORMATEx" runat="server" align="center">
                        <div id="dvHeaderEx" runat="server" align="center">
                            <table width="700px" border="0" cellpadding="2" cellspacing="0" class="lblFont" style="font-family: 'Trebuchet MS'; font-size: 14px;">
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel1Ex" runat="server" UpdateMode="Always">
                                            <ContentTemplate>
                                                <table style="font-family: 'Trebuchet MS'; font-size: 14px; width: 700px;">
                                                    <tr>
                                                        <td class="auto-style12">
                                                            <asp:Image Visible="false" ID="ImageEx" runat="server" ImageUrl="~/img/Benit-Icon.png" Width="120px" Height="114px" />
                                                        </td>
                                                        <td valign="top" align="left" class="auto-style24">

                                                            <table border="0" style="font-family: Verdana, Geneva, Tahoma, sans-serif; width: 210px; height: 90px;">
                                                                <tr runat="server">
                                                                    <td align="center" class="auto-style16" style="color:white; font-weight: 600; font-size: 25px;">
                                                                        <asp:Label ID="Label1" Text="Tax Invoice" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr runat="server">
                                                                    <td align="center" class="auto-style16" style="color:white; font-weight: 400; font-size: 18px;">
                                                                        <asp:Label ID="Label2" Text="Customer Copy" runat="server" />
                                                                    </td>

                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="auto-style16" style="color:white; font-weight: 400; font-size: 18px;">
                                                                        <asp:Label ID="Label3" Text="Original" runat="server" />
                                                                    </td>
                                                                </tr>
                                                            </table>

                                                        </td>

                                                        <td align="right" width="150px" valign="top">

                                                            <asp:Panel Height="95px" ForeColor="Black"
                                                                runat="server" ID="MainPanelEx" Width="100%" HorizontalAlign="Center" Style="margin-left: 0px">
                                                                <table style="font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif; font-weight: bold; text-align: left; font-size: 12px;">
                                                                    <tr>
                                                                        <td class="auto-style20">
                                                                            <asp:Label ID="lblCompanyEx" runat="server"></asp:Label>,
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="auto-style20">
                                                                            <asp:Label ID="lblAddressEx" runat="server"></asp:Label>,
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="auto-style20">
                                                                            <asp:Label ID="lblCityEx" runat="server" />
                                                                            -
                                                                    <asp:Label ID="lblPincodeEx" runat="server"></asp:Label>,
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="auto-style20">
                                                                            <asp:Label ID="lblStateEx" runat="server"> </asp:Label>,
                                                                        </td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td class="auto-style20">Mobile:
                                                                            <asp:Label ID="lblPhnEx" runat="server"> </asp:Label>
                                                                        </td>
                                                                    </tr>

                                                                </table>
                                                            </asp:Panel>
                                                            <cc1:RoundedCornersExtender Corners="All" Radius="10" TargetControlID="MainPanelEx"
                                                                ID="RoundedCornersExtender2" runat="server">
                                                            </cc1:RoundedCornersExtender>

                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                            <table border="0" cellpadding="2" cellspacing="0" class="lblFont" style="font-family: 'Trebuchet MS'; font-size: 14px; margin-left: 0px; height: 121px; width: 682px;"
                                align="center">

                                <tr>
                                    <td class="auto-style8">
                                        <asp:UpdatePanel ID="UpdatePanel2Ex" runat="server" UpdateMode="Always">
                                            <ContentTemplate>
                                                <table border="0" cellpadding="2" cellspacing="0" style="font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; font-size: 12px; width: 230px;">

                                                    <tr style="text-align: left; color: #FFFFFF; font-size: 14px;">
                                                        <td style="padding-left: 5px; padding-bottom: 5px; padding-top: 5px;"><strong>Billing Address: </strong></td>
                                                    </tr>

                                                    <tr runat="server">

                                                        <td align="left" class="auto-style6" style="font-weight: bold;">
                                                            <asp:Label ID="lblSupplierNameEx" runat="server"></asp:Label>
                                                        </td>

                                                    </tr>

                                                    <tr runat="server">

                                                        <td align="left" class="auto-style6" style="font-weight: bold;">
                                                            <asp:Label ID="lblSupplierCmpnyNameEx" runat="server"></asp:Label>
                                                        </td>

                                                    </tr>

                                                    <tr runat="server">

                                                        <td align="left" class="auto-style6" style="font-weight: bold;">
                                                            <asp:Label ID="lblSupplierAddr1Ex" runat="server"></asp:Label>
                                                        </td>

                                                    </tr>

                                                    <tr runat="server">

                                                        <td align="left" class="auto-style6" style="font-weight: bold;">
                                                            <asp:Label ID="lblSupplierAddr2Ex" runat="server"></asp:Label>
                                                        </td>

                                                    </tr>

                                                    <tr runat="server">

                                                        <td align="left" class="auto-style6" style="font-weight: bold;">
                                                            <asp:Label ID="lblSupplierPhnEx" runat="server"></asp:Label>
                                                        </td>

                                                    </tr>

                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>

                                    <td class="auto-style18">
                                        <asp:UpdatePanel ID="UpdatePanel3Ex" runat="server" UpdateMode="Always">
                                            <ContentTemplate>
                                                <table border="0" cellpadding="2" cellspacing="0" style="font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; font-size: 12px; width: 230px;">

                                                    <tr style=" text-align: left; color: #FFFFFF; font-size: 14px;">
                                                        <td style="padding-left: 5px; padding-bottom: 5px; padding-top: 5px;"><strong>Delivery Address: </strong></td>
                                                    </tr>

                                                    <tr runat="server">

                                                        <td align="left" class="auto-style6" style="font-weight: bold;">
                                                            <asp:Label ID="lblShipToNameEx" runat="server"></asp:Label>
                                                        </td>

                                                    </tr>

                                                    <tr runat="server">

                                                        <td align="left" class="auto-style6" style="font-weight: bold;">
                                                            <asp:Label ID="lblShipToCmpnyNameEx" runat="server"></asp:Label>
                                                        </td>

                                                    </tr>

                                                    <tr runat="server">

                                                        <td align="left" class="auto-style6" style="font-weight: bold;">
                                                            <asp:Label ID="lblShipToAddr1Ex" runat="server"></asp:Label>
                                                        </td>

                                                    </tr>

                                                    <tr runat="server">

                                                        <td align="left" class="auto-style6" style="font-weight: bold;">
                                                            <asp:Label ID="lblShipToAddr2Ex" runat="server"></asp:Label>
                                                        </td>

                                                    </tr>

                                                    <tr runat="server">

                                                        <td align="left" class="auto-style6" style="font-weight: bold;">
                                                            <asp:Label ID="lblShipToPhnEx" runat="server"></asp:Label>
                                                        </td>

                                                    </tr>

                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>

                                    <td class="auto-style7">
                                        <asp:UpdatePanel ID="UpdatePanel9Ex" runat="server" UpdateMode="Always">
                                            <ContentTemplate>
                                                <table border="0" cellpadding="2" cellspacing="0" style="font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; font-size: 12px; width: 228px; height: 121px;">

                                                    <tr>
                                                        <td style="color:white;" class="auto-style26">Date: </td>
                                                        <td class="auto-style33" style="font-weight: bold;">
                                                            <asp:Label ID="lblBillDateEx" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td style="color:white;" class="auto-style26">Invoice #: </td>
                                                        <td class="auto-style33" style="font-weight: bold;">
                                                            <asp:Label ID="lblInvoiceEx" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td style="color:white;" class="auto-style26">Customer ID:&nbsp; </td>
                                                        <td class="auto-style33" style="font-weight: bold; ">
                                                            <asp:Label ID="lblCustomerIDEx" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td style="color:white;" class="auto-style26">Purchase Order #: </td>
                                                        <td class="auto-style33" style="font-weight: bold;">
                                                            <asp:Label ID="lblPurchaseOrderEx" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td style="color:white;" class="auto-style26">Payment Due Date: </td>
                                                        <td class="auto-style33" style="font-weight: bold; ">
                                                            <asp:Label ID="lblPaymentDueEx" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>

                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>

                                </tr>
                            </table>
                            <%--End Header--%>
                        </div>
                        <%-- Start Product Details --%>
                        <div id="dvFormatEx" runat="server" align="center" style="padding-top: 5px;">
                            <table cellpadding="2" cellspacing="0" class="lblFont" style="font-family: 'Trebuchet MS'; font-size: 14px; width: 698px;">
                                <tr>
                                    <td>
                                        <table style="font-family: 'Trebuchet MS'; font-size: 14px;">
                                            <tr>
                                                <td>
                                                    <wc:ReportGridView runat="server" BorderWidth="1" ID="gvGeneralEx" ShowHeaderWhenEmpty="true"
                                                        GridLines="Both" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                                        PrintPageSize="45" AllowPrintPaging="true" Visible="false" Width="694px" OnRowDataBound="gvGeneralEx_RowDataBound"                                                        
                                                        Style="font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; font-weight: bold; font-size: 12px; border: 2px solid White">
                                                        <PageHeaderTemplate>
                                                            <br />
                                                            <br />
                                                        </PageHeaderTemplate>
                                                        <Columns>
                                                            <asp:BoundField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" DataField="SalesPerson"
                                                                HeaderText="Sales Person" HeaderStyle-ForeColor="White" HeaderStyle-CssClass="headerGrid"
                                                                ItemStyle-CssClass="itemGrid" ItemStyle-BorderColor="White" HeaderStyle-BorderColor="White" />
                                                            <asp:BoundField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px" DataFormatString="{0:f2}"
                                                                DataField="ShippingMethod" HeaderText="Shipping Method" Visible="true" HeaderStyle-ForeColor="White"
                                                                ItemStyle-CssClass="itemGrid" HeaderStyle-CssClass="headerGrid" ItemStyle-BorderColor="White" HeaderStyle-BorderColor="White" />
                                                            <asp:BoundField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px" DataField="ShippingTerms"
                                                                HeaderText="Shipping Terms" HeaderStyle-ForeColor="White"
                                                                ItemStyle-CssClass="itemGrid" HeaderStyle-CssClass="headerGrid" ItemStyle-BorderColor="White" HeaderStyle-BorderColor="White" />
                                                            <asp:BoundField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px" DataField="PaymentMode"
                                                                HeaderText="Payment Mode" Visible="true" HeaderStyle-ForeColor="White"
                                                                ItemStyle-CssClass="itemGrid" HeaderStyle-CssClass="headerGrid" ItemStyle-BorderColor="White" HeaderStyle-BorderColor="White" />
                                                            <asp:BoundField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="35px"
                                                                DataField="DueDate" HeaderText="Due Date" HeaderStyle-ForeColor="White"
                                                                ItemStyle-CssClass="itemGrid" HeaderStyle-CssClass="headerGrid" ItemStyle-BorderColor="White" HeaderStyle-BorderColor="White" />
                                                            <asp:BoundField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="45px" DataField="DeliveryDate"
                                                                HeaderText="Delivery Date" HeaderStyle-ForeColor="White"
                                                                ItemStyle-CssClass="itemGrid" HeaderStyle-CssClass="headerGrid" ItemStyle-BorderColor="White" HeaderStyle-BorderColor="White" />
                                                        </Columns>
                                                        <PagerTemplate>
                                                        </PagerTemplate>
                                                        <PageFooterTemplate>
                                                            <br />
                                                        </PageFooterTemplate>
                                                    </wc:ReportGridView>


                                                    <wc:ReportGridView runat="server" ID="gvItemEx" CssClass="left"
                                                        GridLines="Both" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                                        PrintPageSize="45" AllowPrintPaging="true" Visible="false" Width="694px" OnRowDataBound="gvItemEx_RowDataBound"
                                                        Style="font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; font-weight: bold; font-size: 12px; border: 2px solid White ">
                                                        <PageHeaderTemplate>

                                                            <br />
                                                        </PageHeaderTemplate>
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="#" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderColor="White" HeaderStyle-BorderColor="White"
                                                               HeaderStyle-ForeColor="White">
                                                                <ItemTemplate>
                                                                    <%# ((GridViewRow)Container).RowIndex + 1%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-BorderColor="White" ItemStyle-BorderColor="White" ItemStyle-Width="100px" DataField="ProductName"
                                                                HeaderText="Item" HeaderStyle-ForeColor="White" ItemStyle-CssClass="itemGrid" HeaderStyle-CssClass="headerGrid" />
                                                            <asp:BoundField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="240px" DataFormatString="{0:f2}" ItemStyle-BorderColor="White" HeaderStyle-BorderColor="White"
                                                                DataField="ProductDesc" HeaderText="Description" HeaderStyle-ForeColor="White" ItemStyle-CssClass="itemGrid" HeaderStyle-CssClass="headerGrid" />
                                                            <asp:BoundField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px" DataField="Qty" ItemStyle-BorderColor="White" HeaderStyle-BorderColor="White"
                                                                HeaderText="Qty" HeaderStyle-ForeColor="White" ItemStyle-CssClass="itemGrid" HeaderStyle-CssClass="headerGrid" />
                                                            <asp:BoundField ItemStyle-HorizontalAlign="Right" ItemStyle-Width="80px" DataField="Rate" ItemStyle-BorderColor="White" HeaderStyle-BorderColor="White"
                                                                HeaderText="Unit Price" HeaderStyle-ForeColor="White" ItemStyle-CssClass="itemGrid" HeaderStyle-CssClass="headerGrid" />
                                                            <asp:BoundField ItemStyle-HorizontalAlign="Right" ItemStyle-Width="90px" DataFormatString="{0:f2}" ItemStyle-BorderColor="White" HeaderStyle-BorderColor="White"
                                                                DataField="Amount" HeaderText="Total Price" HeaderStyle-ForeColor="White" ItemStyle-CssClass="itemGrid" HeaderStyle-CssClass="headerGrid" />
                                                            <asp:BoundField Visible="false" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="90px" DataFormatString="{0:f2}" ItemStyle-BorderColor="White" HeaderStyle-BorderColor="White"
                                                                DataField="VAT" HeaderText="VAT" HeaderStyle-ForeColor="White" ItemStyle-CssClass="itemGrid" HeaderStyle-CssClass="headerGrid" />
                                                            <asp:BoundField Visible="false" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="90px" DataFormatString="{0:f2}" ItemStyle-BorderColor="White" HeaderStyle-BorderColor="White"
                                                                DataField="VATAmount" HeaderText="VAT Amount" HeaderStyle-ForeColor="White" ItemStyle-CssClass="itemGrid" HeaderStyle-CssClass="headerGrid" />
                                                            <asp:BoundField Visible="false" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="90px" DataFormatString="{0:f2}" ItemStyle-BorderColor="White" HeaderStyle-BorderColor="White"
                                                                DataField="Discount" HeaderText="Discount" HeaderStyle-ForeColor="White" ItemStyle-CssClass="itemGrid" HeaderStyle-CssClass="headerGrid" />
                                                        </Columns>
                                                        <PagerTemplate>
                                                        </PagerTemplate>
                                                        <PageFooterTemplate>
                                                            <br />
                                                        </PageFooterTemplate>
                                                    </wc:ReportGridView>

                                                     <div id="PaymentModeEx">

                                                        <div class="auto-styleDiv" runat="server" align="left">
                                                            <asp:Label runat="server" ForeColor="White" Text="Payment Mode"></asp:Label>
                                                            <asp:Label Font-Bold="true" ID="lblPayModeEx" runat="server"></asp:Label>
                                                        </div>

                                                        <div id="divMultiPaymentEx" class="auto-styleDiv" runat="server" style="text-align: right" align="center">
                                                            <wc:ReportGridView ID="GrdViewReceiptEx" runat="server" CssClass="left" Width="100%"
                                                                GridLines="Both" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                                                Style="font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; font-weight: bold; font-size: 12px;border: 2px solid White "
                                                                PrintPageSize="45" AllowPrintPaging="true">
                                                                <EmptyDataRowStyle CssClass="GrdContent" />
                                                                <Columns>
                                                                    <asp:BoundField DataField="Debi" HeaderText="Bank Name / Cash" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="left"
                                                                        HeaderStyle-ForeColor="Black" ItemStyle-CssClass="itemGrid" HeaderStyle-CssClass="headerGrid"
                                                                        ItemStyle-BorderColor="White" HeaderStyle-BorderColor="White" />
                                                                    <asp:BoundField DataField="ChequeNo" HeaderText="CHEQUE" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="left"
                                                                        HeaderStyle-ForeColor="Black" ItemStyle-CssClass="itemGrid" HeaderStyle-CssClass="headerGrid" 
                                                                        ItemStyle-BorderColor="White" HeaderStyle-BorderColor="White"/>
                                                                    <asp:BoundField DataField="AMOUNT" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"
                                                                        HeaderText="Amount" HeaderStyle-Wrap="false" HeaderStyle-ForeColor="Black" ItemStyle-CssClass="itemGrid" HeaderStyle-CssClass="headerGrid" 
                                                                        ItemStyle-BorderColor="White" HeaderStyle-BorderColor="White"/>

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

                                                        <div id="divBankPaymodeEx" class="auto-styleDiv" runat="server" align="center">
                                                            <table cellspacing="0" style="text-align: left; font-family: 'Trebuchet MS'; font-size: 11px; border: 2px solid #999999; " align="center"> 
                                                                <tr>
                                                                    <td style="color:white; white-space: normal; vertical-align: top">Cheque / Credit Card No. :
                                                                    </td>
                                                                    <td style="vertical-align: top">
                                                                        <div id="divCreditCardNoEx" runat="server">
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style=" color:white; vertical-align: top">Bank Name
                                                                    </td>
                                                                    <td style="vertical-align: top">
                                                                        <div id="divBankNameEx" runat="server">
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>

                                                    <div id="divFooterEx" runat="server">
                                                        <div id="Div3" runat="server">
                                                            <table border="0" cellpadding="2" cellspacing="0" style="font-family: 'Trebuchet MS'; width: 689px;">

                                                                <tr>
                                                                    <td class="auto-style19">
                                                                        <asp:UpdatePanel ID="UpdatePanel10Ex" runat="server" UpdateMode="Always">
                                                                            <ContentTemplate>
                                                                                <table border="0" cellpadding="2" cellspacing="0" style="font-weight: bold; font-size: 12px; width: 330px; font-family: 'Trebuchet MS';">

                                                                                    <tr style=" color: white; font-size: medium;">
                                                                                        <td style="padding-left: 10px; padding-bottom: 2px; padding-top: 2px;"><strong>For Service and Demo, Contact: </strong></td>
                                                                                    </tr>

                                                                                    <tr runat="server">

                                                                                        <td align="center" class="auto-style6" style="font-weight:">
                                                                                            <asp:Label ID="lblCustNameEx" runat="server"></asp:Label>
                                                                                        </td>

                                                                                    </tr>

                                                                                    <tr runat="server">

                                                                                        <td style="">
                                                                                            <table style="width: 325px; font-size: 12px; font-weight: bold;">
                                                                                                <tr>
                                                                                                    <td style="color:white;" align="right" class="auto-style32">Mobile:
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblCustPhnEx" runat="server"></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="color:white;" align="right" class="auto-style32">Email:
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblCustMailIDEx" runat="server"></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="color:white;" align="right" class="auto-style32">Contact Timings:
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblCustTimingEx" runat="server"></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>

                                                                                </table>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </td>

                                                                    <td class="auto-style22 " style="padding-left: 140px;">
                                                                        <asp:UpdatePanel ID="UpdatePanel11Ex" runat="server" UpdateMode="Always">
                                                                            <ContentTemplate>
                                                                                <table border="0" cellpadding="2" cellspacing="0" style="font-family: 'Trebuchet MS'; font-size: 13px; width: 224px;">

                                                                                    <tr>
                                                                                        <td class="auto-style23" style="color:white;">Subtotal </td>
                                                                                        <td class="auto-style6" style="font-weight: bold; text-align: right;">
                                                                                            <asp:Label ID="lblSubTotalEx" runat="server"></asp:Label>
                                                                                        </td>
                                                                                    </tr>

                                                                                    <tr>
                                                                                        <td class="auto-style23" style="color:white;">Sales Tax @
                                                                                            <asp:Label ID="lblSalesTaxRateEx" runat="server"></asp:Label></td>
                                                                                        <td class="auto-style6" style="font-weight: bold; text-align: right;">
                                                                                            <asp:Label ID="lblSalesTaxEx" runat="server"></asp:Label>
                                                                                        </td>
                                                                                    </tr>

                                                                                    <tr>
                                                                                        <td class="auto-style23" style="color:white;">Discount</td>
                                                                                        <td class="auto-style6" style="font-weight: bold; text-align: right;">
                                                                                            <asp:Label ID="lblDiscountEx" runat="server"></asp:Label>
                                                                                        </td>
                                                                                    </tr>

                                                                                    <tr>
                                                                                        <td class="auto-style23" style="color:white;">Total</td>
                                                                                        <td class="auto-style6" style="font-weight: bold; text-align: right;">
                                                                                            <asp:Label ID="lblTotalEx" runat="server"></asp:Label>
                                                                                        </td>
                                                                                    </tr>

                                                                                </table>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </td>

                                                                </tr>
                                                            </table>
                                                        </div>

                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="dvFooterEx" runat="server" align="center">
                            <table width="700px" border="0" cellpadding="2" cellspacing="0" style="font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; font-size: 12px;">
                                <tr>
                                    <td style=" color:white; text-align: center; font-weight: bold;" class="auto-style21">Thank you for your business with us!
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style21" style="color:white; text-align: left; font-weight: bold;">Make all Cheques payable to J&J Traders. Cheque payments are subject to realization.
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style21" style="font-weight: bold; text-align: left; color:white; ">

                                        <ul class="list">
                                            <li style="display: inline-block;">Goods once sold cannot be taken back.</li>
                                            <li style="display: inline-block;">All disputes subject to Madurai jurisdiction.</li>
                                        </ul>

                                        <br />

                                    </td>
                                </tr>
                            </table>
                        </div>

                    </div>

                </div>
            </div>

        </div>

        <br />
        <br />

    </form>
</body>
</html>
