<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/OutStandingMasterReport1.aspx.cs"
    Inherits="OutStandingMasterReport1" %>

<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <title></title>
    <link href="App_Themes/DefaultTheme/calendar.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="Scripts/calendar_eu.js"></script>
    <link href="App_Themes/NewTheme/base.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/DefaultTheme/DefaultTheme.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <div id="div1" align="center" runat="server">
            <table style="width: 620px; border:1px solid blue; background-color:White;">
                <%--<tr class="subHeadFont">
                    <td colspan="5">
                        <table>
                            <tr>
                                <td>
                                    Outstanding Ageing Report
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>--%>

                <tr>
                    <td colspan="4" class="subHeadFont2">
                        Outstanding Ageing Report
                    </td>
                </tr>

                <%--<tr>
                    <td colspan="4" align="center">
                        <strong>
                            <asp:Label ID="Label8" runat="server" Text="Outstanding Report" CssClass="td" Font-Bold="True" Font-Size="Medium"></asp:Label></strong>
                    </td>
                </tr>--%>
                <tr style="height:6px">

                </tr>
                <tr>
                    <td  style="width:25%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                        <asp:Label ID="Label20" runat="server" CssClass="label" Text="Brand"></asp:Label>
                    </td>
                    <td class="ControlDrpBorder"  style="width:25%">
                        <asp:DropDownList ID="cmbBrand" runat="server"  CssClass="drpDownListMedium" BackColor = "#90c9fc" Width="100%"  style="border: 1px solid #90c9fc" height="26px">
                        </asp:DropDownList>
                    </td>
                    <td  style="width:20%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                        <asp:Label ID="Label21" runat="server" CssClass="label" Text="Category"></asp:Label>
                    </td>
                    <td  class="ControlDrpBorder" style="width:25%">
                        <asp:DropDownList ID="cmbCategory" runat="server" AutoPostBack="true"  CssClass="drpDownListMedium" BackColor = "#90c9fc" Width="100%"  style="border: 1px solid #90c9fc" height="26px">
                            <asp:ListItem Selected="True" Value="0" style="background-color: #bce1fe">Select Category</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="width:5%">
                        
                    </td>
                </tr>
                <tr>
                    <td  style="width:25%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                        <asp:Label ID="Label22" runat="server"  Text="Model"></asp:Label>
                    </td>
                    <td  class="ControlDrpBorder" style="width:25%">
                        <asp:DropDownList ID="cmbModel" runat="server" AutoPostBack="true" CssClass="drpDownListMedium" BackColor = "#90c9fc" Width="100%"  style="border: 1px solid #90c9fc" height="26px">
                            <asp:ListItem Selected="True" Value="0" style="background-color: #bce1fe">Select Category</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td  style="width:20%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                        <asp:Label ID="Label1" runat="server" Text="Product" ></asp:Label>
                    </td>
                    <td  class="ControlDrpBorder" style="width:25%">
                        <asp:DropDownList TabIndex="1" ID="cmbProduct"  CssClass="drpDownListMedium" BackColor = "#90c9fc" runat="server" Width="100%"  style="border: 1px solid #90c9fc" height="26px">
                        </asp:DropDownList>
                    </td>
                    <td style="width:5%">
                        
                    </td>
                </tr>
                <tr>
                    <td  style="width:25%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                        <asp:Label ID="Label7" runat="server" Text="Duration(Days)" ></asp:Label>
                    </td>
                    <td  class="ControlTextBox3" style="width:25%">
                        <asp:TextBox ID="txtDuration" runat="server" Width="100px" CssClass="cssTextBox"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDuration"
                            ErrorMessage="EnterNumber" ValidationGroup="btnRpt">*</asp:RequiredFieldValidator>
                    </td>
                    <td  style="width:20%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                        <asp:Label ID="Label9" runat="server" Text="No.of Columns" ></asp:Label>
                    </td>
                    <td  class="ControlTextBox3" style="width:25%">
                        <asp:TextBox ID="txtColumns" runat="server" Width="100px" CssClass="cssTextBox"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtColumns"
                            ErrorMessage="EnterNumber" ValidationGroup="btnRpt">*</asp:RequiredFieldValidator>
                    </td>
                    <td style="width:5%">
                        
                    </td>
                </tr>
                <tr>
                    <td  style="width:25%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                        <asp:Label ID="Label25" runat="server" Text="First Level" CssClass="label"></asp:Label>
                    </td>
                    <td  class="ControlDrpBorder" style="width:25%">
                        <asp:DropDownList ID="ddlFirstLvl" runat="server"  CssClass="drpDownListMedium" BackColor = "#90c9fc" AppendDataBoundItems="True" Width="100%"  style="border: 1px solid #90c9fc" height="26px">
                        </asp:DropDownList>
                    </td>
                    <td  style="width:20%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                        <asp:Label ID="Label23" runat="server" Text="Second Level"></asp:Label>
                    </td>
                    <td  class="ControlDrpBorder" style="width:25%">
                        <asp:DropDownList ID="ddlSecondLvl" runat="server" CssClass="drpDownListMedium" BackColor = "#90c9fc" AppendDataBoundItems="True" Width="100%" style="border: 1px solid #90c9fc" height="26px">
                        </asp:DropDownList>
                    </td>
                    <td style="width:5%">
                        
                    </td>
                </tr>
                <tr>
                    <td  style="width:25%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                        <asp:Label ID="Label26" runat="server" Text="Third Level"></asp:Label>
                    </td>
                    <td class="ControlDrpBorder"  style="width:25%">
                        <asp:DropDownList ID="ddlThirdLvl" runat="server"  CssClass="drpDownListMedium" BackColor = "#90c9fc" AppendDataBoundItems="True" Width="100%"  style="border: 1px solid #90c9fc" height="26px">
                        </asp:DropDownList>
                    </td>
                    <td  style="width:20%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                        <asp:Label ID="Label24" runat="server" Text="Fourth Level"></asp:Label>
                    </td>
                    <td  class="ControlDrpBorder" style="width:25%">
                        <asp:DropDownList ID="ddlFourthLvl" runat="server"  CssClass="drpDownListMedium" BackColor = "#90c9fc" AppendDataBoundItems="True" Width="100%"  style="border: 1px solid #90c9fc" height="26px">
                        </asp:DropDownList>
                    </td>
                    <td style="width:5%">
                        
                    </td>
                </tr>
                <tr>
                    <td  style="width:25%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                        <asp:Label ID="Label28" runat="server" Text="Fifth Level"></asp:Label>
                    </td>
                    <td  class="ControlDrpBorder" style="width:25%">
                        <asp:DropDownList ID="ddlFifthLvl" runat="server"  CssClass="drpDownListMedium" BackColor = "#90c9fc" AppendDataBoundItems="True" Width="100%"  style="border: 1px solid #90c9fc" height="26px">
                        </asp:DropDownList>
                    </td>
                    <td style="width:20%">
                    </td>
                    <td style="width:25%">
                    </td>
                    <td style="width:5%">
                        
                    </td>
                </tr>
                 <tr style="height:5px">

                </tr>
                <tr>
                    <td class="style1" colspan="5">
                        <asp:Label ID="lblDate" runat="server" CssClass="label"></asp:Label>
                    </td>
                    <%--<td align="left" style="width:20%">
                        <asp:CheckBox ID="chkboxpay" runat="server" Text="Without payment" style="color:Black" Font-Names="arial" Font-Size="11px" AutoPostBack="true" Checked="True">
                        </asp:CheckBox>
                    </td>--%>
                </tr>
                <tr>
                    <td colspan="5">
                        <table width="100%">
                            <tr>
                                <td style="width:30%">
                                </td>

                                <td style="width:20%">
                                    <asp:Button ID="btnOutstand" runat="server" OnClick="btnOutstand_Click" CssClass="NewReport6" EnableTheming="false"
                                        ValidationGroup="btnRpt" />
                                </td>
                                <td style="width:20%">
                                    <asp:Button ID="btnExcel" runat="server" OnClick="btnExcel_Click" CssClass="exportexl6" EnableTheming="false"
                                        ValidationGroup="btnRpt" />
                                </td>
                                <td style="width:30%">
                                    
                                </td>
                                <td>
                                
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                </table>
                </div>
                
                <table style="width: 620px;"  align="center">
                <tr>
                    <td colspan="4">
                        <table style="width:100%">
                            <tr>
                                <td style="width:42%">

                                </td>
                                <td style="width:15%">
                                    <input type="button" value=" " id="Button1" runat="Server" onclick="javascript:CallPrint('divPrint')"
                                        class="printbutton6" visible="False" />
                                </td>
                                <td style="width:10%">
                                    <asp:Button ID="btndet" CssClass="GoBack" EnableTheming="false" runat="server"
                                        OnClick="btndet_Click" Visible="False" />
                                </td>
                                <td style="width:33%">

                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <br />
                <tr>
                    <td class="style1" colspan="4">
                        <div id="divPrint" style="font-family: 'Trebuchet MS'; font-size: 11px; width: 100%"
                            runat="server" visible="False" align="center">
                            <table width="90%" border="0" style="font-family: Trebuchet MS; font-size: 14px;">
                                <tr>
                                    <td width="140px" align="left">
                                        <asp:Label runat="server" ID="Label16">TIN#:</asp:Label>
                                        <asp:Label ID="lblTNGST" runat="server" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td align="center" width="320px" style="font-size: 20px;">
                                        <asp:Label ID="lblCompany" runat="server" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td width="140px" align="left">
                                        <asp:Label runat="server" ID="Label17">Ph:</asp:Label>
                                        <asp:Label ID="lblPhone" runat="server" Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:Label runat="server" ID="Label18">GST#:</asp:Label>
                                        <asp:Label ID="lblGSTno" runat="server" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lblAddress" runat="server" CssClass="label"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:Label runat="server" ID="Label19">Date:</asp:Label>
                                        <asp:Label ID="lblBillDate" runat="server" Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lblCity" runat="server"></asp:Label>
                                        ---<asp:Label ID="lblPinCode" runat="server"></asp:Label>
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
                                        <asp:Label ID="lblState" runat="server"></asp:Label>
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
                            <wc:ReportGridView runat="server" BorderWidth="1" ID="gvReport" GridLines="Both"
                                AlternatingRowStyle-CssClass="even" AutoGenerateColumns="true"
                                AllowPrintPaging="true" EmptyDataText="No Data Found" SkinID="gridview" CssClass="someClass"
                                Width="100%" OnRowDataBound="gvReport_RowDataBound">
                                <RowStyle CssClass="dataRow" />
                                <SelectedRowStyle CssClass="SelectdataRow" />
                                <AlternatingRowStyle CssClass="altRow" />
                                <EmptyDataRowStyle CssClass="HeadataRow" Font-Bold="true" />
                                <HeaderStyle CssClass="HeadataRow" Wrap="false" BorderStyle="Solid" BorderColor="Gray" BorderWidth="1px" />
                                <FooterStyle CssClass="dataRow" />
                                <PagerStyle CssClass="footer-row allPad" VerticalAlign="Middle" HorizontalAlign="Left" />
                                <PageHeaderTemplate>
                                    <br />
                                    <br />
                                </PageHeaderTemplate>
                                <Columns>
                                </Columns>
                                <PagerTemplate>
                                </PagerTemplate>
                                <PageFooterTemplate>
                                    <br />
                                </PageFooterTemplate>
                            </wc:ReportGridView>
                            <br />
                            <table width="100%" cellpadding="0px" cellspacing="0px" style="font-family: 'Trebuchet MS';
                font-size: 15px;" border="0px">
                <tr>
                    <td width="65px">
                    </td>
                    <td width="85px">
                    </td>
                    <td width="110px">
                    </td>
                    <td width="110px">
                    </td>
                    <td width="110px">
                    </td>
                    <td width="110px">
                    </td>
                    <td colspan="2" align="right">
                        <b>Total :</b> <b>
                            <asp:Label ID="lblTotalOutstanding" runat="server" Text="0"></asp:Label></b>
                    </td>
                </tr>
            </table>
            <table style="font-family: 'Trebuchet MS' ">
                <%--<tr>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="WithOut Payment" Font-Bold="True" Font-Size="Medium" Visible="False"></asp:Label>
                    </td>
                </tr>--%>
                <wc:ReportGridView runat="server" BorderWidth="1" ID="gvLedger" SkinID="gridview" CssClass="someClass"
                    GridLines="Both" AlternatingRowStyle-CssClass="even" HeaderStyle-HorizontalAlign="Left"
                    AutoGenerateColumns="false" PrintPageSize="47" AllowPrintPaging="true" Width="875px"
                    OnRowDataBound="gvLedger_RowDataBound" EmptyDataText="No Data Found ">

                    <RowStyle CssClass="dataRow" />
                    <SelectedRowStyle CssClass="SelectdataRow" />
                    <AlternatingRowStyle CssClass="altRow" />
                    <EmptyDataRowStyle CssClass="HeadataRow" Font-Bold="true" />
                    <HeaderStyle CssClass="HeadataRow" Wrap="false" />
                    <FooterStyle CssClass="dataRow" />
                    <PagerStyle CssClass="footer-row allPad" VerticalAlign="Middle" HorizontalAlign="Left" />

    <%--
                    <HeaderStyle CssClass="ReportHeadataRow" />
                    <RowStyle CssClass="ReportdataRow" />
                    <AlternatingRowStyle CssClass="ReportAltdataRow" />
                    <FooterStyle CssClass="ReportFooterRow" />--%>

                    <PageHeaderTemplate>
                        <br />
                        <br />
                    </PageHeaderTemplate>
                    <Columns>
                        <asp:BoundField ItemStyle-HorizontalAlign="Left" DataField="Customer" HeaderText="Ledger Name"  HeaderStyle-BorderColor="Blue"/>
                        <asp:BoundField ItemStyle-HorizontalAlign="Left" DataField="Total" HeaderText="Total"  HeaderStyle-BorderColor="Blue"/>
                        <%--<asp:BoundField ItemStyle-HorizontalAlign="Left" DataField="OpenBalanceDR" HeaderText="OpenBal(Dr)" />
                        <asp:BoundField ItemStyle-HorizontalAlign="Left" DataField="OpenBalanceCR" HeaderText="OpenBal(Cr)" />
                        <asp:BoundField ItemStyle-HorizontalAlign="Left" DataField="balance" HeaderText="Balance" />--%>
                         <%--<asp:TemplateField HeaderText="Total" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblBalancee" runat="server" CssClass="lblFont" Font-Bold="true" ForeColor="Blue"
                                    Text="0.00"> </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="OpenBal(Cr)" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblBalanceee" runat="server" CssClass="lblFont" Font-Bold="true" ForeColor="Blue"
                                    Text="0.00"> </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="OpenBal(Dr)" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblBalanceeee" runat="server" CssClass="lblFont" Font-Bold="true" ForeColor="Blue"
                                    Text="0.00"> </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <%--<asp:TemplateField HeaderText="Balance" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblBalance" runat="server" CssClass="lblFont" Font-Bold="true" ForeColor="Blue"
                                    Text="0.00"> </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                    </Columns>
                    <PagerTemplate>
                    </PagerTemplate>
                    <PageFooterTemplate>
                    </PageFooterTemplate>
                </wc:ReportGridView>
                <tr>
                    <td>
                        <b><asp:Label ID="Label6" runat="server" Text="Total : " Visible="False" Font-Bold="True" Font-Size="Medium"></asp:Label></b> 
                    </td>
                    <td>
                        <b>
                            <asp:Label ID="Label2" runat="server" Visible="False" Font-Size="Medium" Font-Bold="True"></asp:Label></b>
                    </td>
                </tr>
            </table>
            <%--<table width="100%" cellpadding="0px" cellspacing="0px" style="font-family: 'Trebuchet MS';
                font-size: 15px;" border="0px">
                
            </table>--%>
            <table style="font-family: 'Trebuchet MS' ">
                <tr>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text="With Payment" Font-Bold="True" Font-Size="Medium" Visible="False"></asp:Label>
                    </td>
                </tr>
                <wc:ReportGridView runat="server" BorderWidth="1" ID="GVPay" SkinID="gridview" CssClass="someClass"
                    GridLines="Both" AlternatingRowStyle-CssClass="even" HeaderStyle-HorizontalAlign="Left"
                    AutoGenerateColumns="false" PrintPageSize="47" AllowPrintPaging="true" Width="875px"
                    OnRowDataBound="GVPay_RowDataBound">
                    <RowStyle CssClass="dataRow" />
                    <SelectedRowStyle CssClass="SelectdataRow" />
                    <AlternatingRowStyle CssClass="altRow" />
                    <EmptyDataRowStyle CssClass="HeadataRow" Font-Bold="true" />
                    <HeaderStyle CssClass="HeadataRow" Wrap="false" />
                    <FooterStyle CssClass="dataRow" />
                    <PagerStyle CssClass="footer-row allPad" VerticalAlign="Middle" HorizontalAlign="Left" />
                    <PageHeaderTemplate>
                        <br />
                        <br />
                    </PageHeaderTemplate>
                    <Columns>
                        <asp:BoundField ItemStyle-HorizontalAlign="Left" DataField="Ledgername" HeaderText="Ledgername"  HeaderStyle-BorderColor="Blue"/>
                        <asp:BoundField ItemStyle-HorizontalAlign="Left" DataField="Total" HeaderText="Total"  HeaderStyle-BorderColor="Blue"/>
                        <asp:BoundField ItemStyle-HorizontalAlign="Left" DataField="Pay" HeaderText="Payment"  HeaderStyle-BorderColor="Blue"/>
                        <asp:BoundField ItemStyle-HorizontalAlign="Left" DataField="Rec" HeaderText="Receipt"  HeaderStyle-BorderColor="Blue"/>
                        <asp:BoundField ItemStyle-HorizontalAlign="Left" DataField="OpenBalanceDR" HeaderText="OpenBal(Dr)"  HeaderStyle-BorderColor="Blue"/>
                        <asp:BoundField ItemStyle-HorizontalAlign="Left" DataField="OpenBalanceCR" HeaderText="OpenBal(Cr)"  HeaderStyle-BorderColor="Blue"/>
                        <asp:BoundField ItemStyle-HorizontalAlign="Left" DataField="balance" HeaderText="Balance" HeaderStyle-BorderColor="Blue" />
                     </Columns>
                    <PagerTemplate>
                    </PagerTemplate>
                    <PageFooterTemplate>
                    </PageFooterTemplate>
                </wc:ReportGridView>
                <tr>
                    <td>
                        <b><asp:Label ID="Label10" runat="server" Text="Balance : " Visible="False" Font-Size="Medium" Font-Bold="True"></asp:Label></b> 
                    </td>
                    <td>
                        <b><asp:Label ID="Label3" runat="server" Text="0" Visible="False" Font-Size="Medium" Font-Bold="True"></asp:Label></b>
                    </td>
                </tr>
            </table>
                        </div>
                        
                    </td>
                </tr>
                <tr>
                    <td class="style1" colspan="4">
                    </td>
                </tr>
            </table>
            <%--            <asp:GridView ID="gvReport" runat="server" BackColor="White" BorderColor="White"
                BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" Font-Size="Small"
                ShowFooter="True" OnRowDataBound="gvReport_RowDataBound">
                <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
            </asp:GridView>--%>
            <br />
            
            <%--<table width="600px" cellpadding="0px" cellspacing="0px" style="font-family: 'Trebuchet MS';
                font-size: 15px;" border="0px">
                <tr style="height:10px">
                </tr>
            </table>--%>
        
    </div>
    </form>
</body>
</html>
