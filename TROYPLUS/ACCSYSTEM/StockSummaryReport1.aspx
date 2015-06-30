<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StockSummaryReport1.aspx.cs"
    Inherits="StockSummaryReport1" %>

<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <title>Stock Summary Report</title>
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
<body style="font-family: 'Trebuchet MS'; font-size: 11px;">
    <form id="form1" runat="server">
    <div  align="center">
        <br />
        <div runat="server" id="div1">
        <table cellpadding="0" cellspacing="1" border="0" style="border: 1px solid Blue; background-color:White;
            width: 500px">
            <%--<tr>--%>
                <%--<td colspan="3" class="mainConHd">
                <span>Sales Bill Summary Report</span>
          
                </td>--%>
                <%--<td colspan="4">
                    <table class="headerPopUp">
                        <tr>
                            <td>
                                <span>Sales Bill Summary Report</span>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>--%>
            <tr>
                <td colspan="4" class="subHeadFont2">
                    Stock  Summary Report
                </td>
            </tr>
            <tr style="height:6px">
                    
                    </tr>
            <tr>
                <td style="width:25%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">     
                    Start Date
                </td>
                <td align="left"  style="width:30%" class="ControlTextBox3">
                    <asp:TextBox ID="txtStartDate" CssClass="cssTextBox" Width="100px" MaxLength="10"
                        runat="server" />
                    &nbsp; &nbsp; &nbsp;
                </td>
                <td align="left" style="width:15%">
                    <script type="text/javascript" language="JavaScript" style="float: left;">
                        new tcal({ 'formname': 'form1', 'controlname': 'txtStartDate' });</script>
                </td>
                <td align="left"  style="width:5%">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtStartDate"
                        Display="None" CssClass="lblFont" ErrorMessage="Please Enter Start Date"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="width:25%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                    End Date
                </td>
                <td align="left" class="ControlTextBox3"  style="width:30%">
                    <asp:TextBox ID="txtEndDate" CssClass="cssTextBox" Width="100px" MaxLength="10" runat="server" />
                    &nbsp; &nbsp;
                </td>
                <td align="left" style="width:15%">
                    <script type="text/javascript" language="JavaScript">
                        new tcal({ 'formname': 'form1', 'controlname': 'txtEndDate' });</script>
                </td>
                <td align="left" style="width:5%">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEndDate"
                        Display="None" ErrorMessage="Please Enter The End Date" CssClass="lblFont"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtStartDate"
                        ControlToValidate="txtEndDate" Display="None" ErrorMessage="Start Date Should Be Less Than the End Date"
                        CssClass="lblFont" Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td style="width:25%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                    Data Display Mode
                </td>
                <td align="left"  style="width:30%" class="ControlDrpBorder">
                    <asp:DropDownList TabIndex="1" ID="cmbDisplayCat" Width="100%"  CssClass="drpDownListMedium" BackColor = "#90c9fc"
                        runat="server" style="border: 1px solid #90c9fc" height="26px">
                        <asp:ListItem Text="Daywise" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Categorywise" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Brandwise" Value="3"></asp:ListItem>
                        <asp:ListItem Text="Modelwise" Value="4"></asp:ListItem>
                        <asp:ListItem Text="Billwise" Value="5"></asp:ListItem>
                        <asp:ListItem Text="Customerwise" Value="6"></asp:ListItem>
                        <asp:ListItem Text="Executivewise" Value="7"></asp:ListItem>
                        <asp:ListItem Text="Itemwise" Value="8"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="left" style="width:15%">
                    <asp:RequiredFieldValidator CssClass="lblFont" ID="reqSuppllier" Text="Display Mode is mandatory"
                        InitialValue="0" ControlToValidate="cmbDisplayCat" runat="server" />
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width:25%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                    Item Display Mode
                </td>
                <td align="left"  style="width:30%" class="ControlDrpBorder">
                    <asp:DropDownList TabIndex="1" ID="cmbDisplayItem" Width="100%"  CssClass="drpDownListMedium" BackColor = "#90c9fc"
                        runat="server" style="border: 1px solid #90c9fc" height="26px">
                        <asp:ListItem Text="Brandwise" Value="ProductDesc"></asp:ListItem>
                        <asp:ListItem Text="Modelwise" Value="Model"></asp:ListItem>
                        <asp:ListItem Text="Billwise" Value="BillNo"></asp:ListItem>
                        <asp:ListItem Text="Customerwise" Value="CustomerName"></asp:ListItem>
                        <asp:ListItem Text="Itemwise" Value="ItemCode"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="left" style="width:15%">
                    <asp:RequiredFieldValidator CssClass="lblFont" ID="RequiredFieldValidator3" Text="Display Mode is mandatory"
                        InitialValue="0" ControlToValidate="cmbDisplayCat" runat="server" />
                </td>
                <td>
                
                </td>
            </tr>
            <tr>
                <td  style="width:25%">
                </td>
                <td align="left" style="width:30%" class="ControlTextBox3">
                    <asp:RadioButton GroupName="1" ID="chkPurReturn" name="1" runat="server" />
                    Purchase Return
                    <br />
                    <asp:RadioButton ID="chkIntTrans" GroupName="1" runat="server" />
                    Internal Transfer
                    <br />
                    <asp:RadioButton ID="chkDelNote" GroupName="1" runat="server" />
                    Delivery Note
                    <br />
                </td>
                <td align="left">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <table width="100%">
                        <tr>
                            <td style="width:31%">
                            
                            </td>
                            <td style="width:19%">
                                <asp:Button ID="btnReport" runat="server" CssClass="NewReport6" EnableTheming="false"
                                    OnClick="btnReport_Click" Text="" />
                            </td>
                            <td style="width:19%">
                                <asp:Button ID="btnxls" runat="server" CssClass="exportexl6" 
                                     EnableTheming="false" OnClick="btnxls_Click" />
                            </td>
                            <td style="width:31%">
                            
                            </td>
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
                        <td colspan="4">
                            <table width="100%">
                                <tr>
                                    <td style="width:40%">

                                    </td>
                                    <td style="width:20%">
                                        <input type="button" value=" " class="printbutton6" id="Button1" runat="Server" onclick="javascript:CallPrint('divPrint')"
                                            style="padding-left: 25px;" />
                                        <asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="true" ShowSummary="false" />
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
        <div id="divPrint" runat="server" visible="false" style="font-family: 'Trebuchet MS';
            font-size: 11px;">
            <center>
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
                </table>
                <h5>
                    SALES SUMMARY REPORT</h5>
            </center>
            <wc:ReportGridView runat="server" BorderWidth="1" ID="gvMain" GridLines="Both" AlternatingRowStyle-CssClass="even"
                AutoGenerateColumns="false" AllowPaging="false" AllowPrintPaging="true"
                Width="80%" EmptyDataText="No Rows Found." CellPadding="2" OnRowDataBound="gvMain_RowDataBound"
                ShowFooter="True" ShowHeader="True">
                <HeaderStyle CssClass="ReportHeadataRow" />
                <RowStyle CssClass="ReportdataRow" />
                <AlternatingRowStyle CssClass="ReportAltdataRow" />
                <FooterStyle CssClass="ReportFooterRow" />
                <EmptyDataRowStyle CssClass="HeadataRow" Font-Bold="true" Height="25px" />
                <Columns>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <div style="white-space: nowrap; width: auto;">
                                <a href="javascript:switchViews('dv<%# Eval("LinkName") %>', 'imdiv<%# Eval("LinkName") %>');"
                                    style="text-decoration: none;">
                                    <img id="imdiv<%# Eval("LinkName") %>" alt="Show" border="0" src="App_Themes/DefaultTheme/Images/plus.gif" />
                                </a>
                                <asp:Label ID="lblLink" runat="server" Text='<%# Eval("LinkName") %>'></asp:Label>
                                <asp:Label ID="lblProductName" runat="server" Text=''></asp:Label>
                                <br />
                                <div id="dv<%# Eval("LinkName") %>" style="display: none; position: relative; left: 1px;">
                                    <wc:ReportGridView runat="server" BorderWidth="1" ID="gvSecond" GridLines="Both"
                                        AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false" ShowFooter="true"
                                        Width="70%" Style="font-family: 'Trebuchet MS'; font-size: 11px;" OnRowDataBound="gvSecond_RowDataBound">
                                        <FooterStyle CssClass="ReportFooterRow" />
                                        <HeaderStyle CssClass="ReportHeadataRow" />
                                        <RowStyle CssClass="ReportdataRow" />
                                        <AlternatingRowStyle CssClass="ReportAltdataRow" />
                                        <PageHeaderTemplate>
                                            <br />
                                            <br />
                                        </PageHeaderTemplate>
                                        <Columns>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <b>
                                                        <%# Eval("GroupItem") %>
                                                    </b>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                                                ItemStyle-Font-Size="XX-Small" HeaderText="Sales Rate">
                                                <ItemTemplate>
                                                    <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblRate" runat="server"
                                                        Text='<%# Eval("SRate","{0:f2}") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                                                ItemStyle-Font-Size="XX-Small" HeaderText="Qty">
                                                <ItemTemplate>
                                                    <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblSQty" runat="server"
                                                        Text='<%# Eval("Quantity","{0:f2}") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                                                HeaderText="Net Rate" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblNetRate"
                                                        runat="server" Text='<%# Eval("NetRate","{0:f2}") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                                                HeaderText="Discount Rate" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblDiscountRate"
                                                        runat="server" Text='<%# Eval("ActualDiscount","{0:f2}") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                                                HeaderText="VAT Rate" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblVatRate"
                                                        runat="server" Text='<%# Eval("ActualVat","{0:f2}") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                                                HeaderText="CST Rate" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblCSTRate"
                                                        runat="server" Text='<%# Eval("ActualCST","{0:f2}") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                                                HeaderText="Freight" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblFreightRate"
                                                        runat="server" Text='<%# Eval("SumFreight","{0:f2}") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                                                HeaderText="Loading / Unloading" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblLURate" runat="server"
                                                        Text='<%# Eval("Loading","{0:f2}") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                                                HeaderText="Total" ItemStyle-Width="20%">
                                                <ItemTemplate>
                                                    <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblTotal" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </wc:ReportGridView>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                        HeaderText="Net Rate">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblNetRate"
                                runat="server" Text='<%# Eval("NetRate","{0:f2}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                        HeaderText="Discount Rate">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblDiscountRate"
                                runat="server" Text='<%# Eval("ActualDiscount","{0:f2}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                        HeaderText="VAT Rate">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblVatRate"
                                runat="server" Text='<%# Eval("ActualVat","{0:f2}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                        HeaderText="CST Rate">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblCSTRate"
                                runat="server" Text='<%# Eval("ActualCST","{0:f2}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                        HeaderText="Freight">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblFreightRate"
                                runat="server" Text='' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                        HeaderText="Loading / Unloading">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblLURate" runat="server"
                                Text='' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                        HeaderText="Total">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblTotal" runat="server" />
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
