<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="StockAgeingReport.aspx.cs" Inherits="StockAgeingReport"  Title="Stock Ageing Report" %>

<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div align="center">
    <br />
    <div id="div1" runat="server">
        <table style="border: 1px solid blue; background-color:White;" width="650px">
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
                <td colspan="5" class="headerPopUp">
                    Stock Ageing Report
                </td>
            </tr>
            <tr style="height:6px">
                    
            </tr>
            <tr>
                <td colspan="5">
                    <table style="width:100%">
                        <tr>
                            <td  style="width:15%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                                Brand
                            </td>
                            <td style="text-align: left; width:20%" class="ControlDrpBorder">
                                <asp:DropDownList ID="cmbBrand" runat="server"  style="border: 1px solid #e7e7e7" BackColor = "#e7e7e7" height="26px" Width="100%" CssClass="drpDownListMedium">
                                </asp:DropDownList>
                            </td>
                            <td  style="width:10%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                                Category
                            </td>
                            <td style="text-align: left; width:20%" class="ControlDrpBorder">
                                <asp:DropDownList ID="cmbCategory" runat="server" AutoPostBack="true"  BackColor = "#e7e7e7" style="border: 1px solid #e7e7e7" height="26px" Width="100%"
                                    CssClass="textbox">
                                    <asp:ListItem Selected="True" Value="0" style="background-color: #bce1fe">Select Category</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr style="height: 2px;"/> 
                        <tr>
                            <td  style="width:15%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                                Model
                            </td>
                            <td style="text-align: left; width:20%" class="ControlDrpBorder">
                                <asp:DropDownList ID="cmbModel" runat="server" AutoPostBack="true" BackColor = "#e7e7e7" style="border: 1px solid #e7e7e7" height="26px" Width="100%"
                                     CssClass="drpDownListMedium">
                                    <asp:ListItem Selected="True" Value="0" style="background-color: #bce1fe">Select Category</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td  style="width:10%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                                Product
                            </td>
                            <td style="text-align: left; width:20%" class="ControlDrpBorder">
                                <asp:DropDownList TabIndex="1" ID="cmbProduct"  CssClass="drpDownListMedium" BackColor = "#e7e7e7" style="border: 1px solid #e7e7e7" height="26px" Width="100%" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr style="height: 2px;"/> 
                        <tr style="display: none">
                            <td  style="width:15%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                                Start Date
                            </td>
                            <td class="ControlTextBox3" style="width:20%">
                                <asp:TextBox ID="txtStartDate" runat="server" style="border: 1px solid #e7e7e7" CssClass="textbox" MaxLength="10" BackColor = "#e7e7e7" />
                            </td>
                            <td  style="width:10%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                                End Date
                            </td>
                            <td class="ControlTextBox3" style="width:20%">
                                <asp:TextBox ID="txtEndDate" CssClass="textbox" runat="server" style="border: 1px solid #e7e7e7" MaxLength="10" BackColor = "#e7e7e7" />
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr style="height: 2px;"/> 
                        <tr>
                            <td  style="width:15%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                                Duration (Days)
                            </td>
                            <td align="left" class="ControlTextBox3" style="width:20%">
                                <asp:TextBox ID="txtDuration" runat="server" BackColor = "#e7e7e7" style="border: 1px solid #e7e7e7"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDuration"
                                    ErrorMessage="EnterNumber" ValidationGroup="btnAgeing">*</asp:RequiredFieldValidator>
                            </td>
                            <td  style="width:10%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                                No. of Columns
                            </td>
                            <td align="left" class="ControlTextBox3" style="width:20%">
                                <asp:TextBox ID="txtColumns" runat="server" BackColor = "#e7e7e7" style="border: 1px solid #e7e7e7"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtColumns"
                                    ErrorMessage="EnterNumber" ValidationGroup="btnAgeing">*</asp:RequiredFieldValidator>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr style="height: 2px;"/> 
                        <tr>
                            <td  style="width:15%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                                First Level
                            </td>
                            <td style="text-align: left; width:20%" class="ControlDrpBorder">
                                <asp:DropDownList ID="ddlFirstLvl" runat="server"  CssClass="drpDownListMedium" BackColor = "#e7e7e7" style="border: 1px solid #e7e7e7" height="26px" Width="100%" AppendDataBoundItems="True">
                                </asp:DropDownList>
                            </td>
                            <td  style="width:10%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                                Second Level
                            </td>
                            <td style="text-align: left; width:20%" class="ControlDrpBorder">
                                <asp:DropDownList ID="ddlSecondLvl" runat="server"  CssClass="drpDownListMedium" BackColor = "#e7e7e7" style="border: 1px solid #e7e7e7" height="26px" Width="100%" AppendDataBoundItems="True">
                                </asp:DropDownList>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr style="height: 2px;"/> 
                        <tr>
                            <td  style="width:15%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                                Third Level
                            </td>
                            <td style="text-align: left ;width:20%" class="ControlDrpBorder">
                                <asp:DropDownList ID="ddlThirdLvl" runat="server"  CssClass="drpDownListMedium" BackColor = "#e7e7e7" style="border: 1px solid #e7e7e7" height="26px" Width="100%" AppendDataBoundItems="True">
                                </asp:DropDownList>
                            </td>
                            <td  style="width:20%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                                Fourth Level
                            </td>
                            <td style="text-align: left ;width:20%" class="ControlDrpBorder">
                                <asp:DropDownList ID="ddlFourthLvl" runat="server"  CssClass="drpDownListMedium"  BackColor = "#e7e7e7" style="border: 1px solid #e7e7e7" height="26px" Width="100%" AppendDataBoundItems="True">
                                </asp:DropDownList>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr style="height: 2px;"/> 
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
                    <table style="width:100%">
                        <tr>
                            <td style="width:30%;" >
                            </td>
                            <td style="width:20%;">
                                <asp:Button ID="btnstockageing" runat="server" OnClick="btnstockageing_Click" CssClass="NewReport6"
                                    ValidationGroup="btnAgeing" EnableTheming="false"/>
                            </td>
                            <td  style="width:20%;">
                                <asp:Button ID="btnExcel" runat="server" OnClick="btnExcel_Click" CssClass="exportexl6"
                                    EnableTheming="false" ValidationGroup="btnAgeing" />
                            </td>
                            <td style="width:30%;">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="style1" colspan="4">
                </td>
            </tr>
            </table>
            </div>
            <div runat="server" id="divmain" visible="false">
                <table width="600px">
                    <tr>
                        <td colspan="3">
                            <table width="100%">
                                <tr>
                                    <td style="width:31%">

                                    </td>
                                    <td style="width:19%">
                                        <input type="button" value="" id="Button1" runat="Server" onclick="javascript:CallPrint('divPrint')"
                                            class="printbutton6" />
                                    </td>
                                    <td style="width:19%">
                                        <asp:Button ID="btndet" CssClass="GoBack" EnableTheming="false" runat="server"
                                            OnClick="btndet_Click" />
                                    </td>
                                    <td style="width:31%">

                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <br />
                <div id="divPrint" style="font-family: 'Trebuchet MS'; font-size: 11px; width: 100%"
                    runat="server">
                    <table width="600px" border="0" style="font-family: Trebuchet MS; font-size: 14px;">
                        <tr>
                            <td width="140px" align="left">
                                <asp:Label runat="server" CssClass="label" ID="Label16">TIN#:</asp:Label>
                                <asp:Label ID="lblTNGST" runat="server" CssClass="label" Font-Bold="True"></asp:Label>
                            </td>
                            <td align="center" width="320px" style="font-size: 20px;">
                                <asp:Label ID="lblCompany" runat="server" CssClass="label" Font-Bold="True"></asp:Label>
                            </td>
                            <td width="140px" align="left">
                                <asp:Label runat="server" CssClass="label" ID="Label17">Ph:</asp:Label>
                                <asp:Label ID="lblPhone" runat="server" CssClass="label" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Label runat="server" CssClass="label" ID="Label18">GST#:</asp:Label>
                                <asp:Label ID="lblGSTno" runat="server" CssClass="label" Font-Bold="True"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lblAddress" runat="server" CssClass="label"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label runat="server" CssClass="label" ID="Label19">Date:</asp:Label>
                                <asp:Label ID="lblBillDate" runat="server" CssClass="label" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td align="center">
                                <asp:Label ID="lblCity" runat="server" CssClass="label"></asp:Label>
                                ---<asp:Label ID="lblPinCode" runat="server" CssClass="label"></asp:Label>
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
                                <asp:Label ID="lblState" runat="server" CssClass="label"></asp:Label>
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
                </div>
                &nbsp;
            
        <%--<asp:GridView ID="gvOuts" runat="server" BackColor="White" BorderColor="White" BorderStyle="Ridge"
            BorderWidth="2px" CellPadding="3" CellSpacing="1" Font-Size="Small" ShowFooter="True"
            OnRowDataBound="gvOuts_RowDataBound" AutoGenerateColumns="True">
            <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
            <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
            <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
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
        </asp:GridView>--%>
        <wc:ReportGridView runat="server" BorderWidth="1" ID="gvOuts" GridLines="Both"
                                AlternatingRowStyle-CssClass="even" AutoGenerateColumns="true"
                                AllowPrintPaging="true" EmptyDataText="No Data Found" SkinID="gridview" CssClass="someClass"
                                Width="100%" OnRowDataBound="gvOuts_RowDataBound">
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
    Total :
    <asp:Label ID="lblTotalStockaging" runat="server" Text="0"></asp:Label>
    </div>
    </div>

</asp:Content>
