<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BillSummaryReport.aspx.cs"
    Inherits="BillSummaryReport" %>

<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CST VAT Report</title>
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
    <div class="lblFont">
        <br />
        <table cellpadding="2" cellspacing="4" width="70%" border="0" style="border: 1px solid silver;
            background-image: url('App_Themes/DefaultTheme/Images/bluebg.jpg');">
            <tr>
                <td colspan="3" style="background-image: url('App_Themes/DefaultTheme/Images/bgReportheader.jpg');
                    color: White; background-repeat: repeat-x; font-size: 11px; font-weight: bold;">
                    Sales Bill Summary Report
                </td>
            </tr>
            <tr>
                <td align="right" width="20%" class="lblFont">
                    <b>Start Date:</b>
                </td>
                <td align="left" width="30%">
                    <asp:TextBox ID="txtStartDate" CssClass="lblFont" Width="100px" MaxLength="10" runat="server" />
                    <script type="text/javascript" language="JavaScript">                        new tcal({ 'formname': 'form1', 'controlname': 'txtStartDate' });</script>
                </td>
                <td align="left" width="50%">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtStartDate"
                        Display="None" CssClass="lblFont" ErrorMessage="Please Enter Start Date"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right" class="lblFont">
                    <b>End Date:</b>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtEndDate" CssClass="lblFont" Width="100px" MaxLength="10" runat="server" />
                    <script type="text/javascript" language="JavaScript">                        new tcal({ 'formname': 'form1', 'controlname': 'txtEndDate' });</script>
                </td>
                <td align="left">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEndDate"
                        Display="None" ErrorMessage="Please Enter The End Date" CssClass="lblFont"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtStartDate"
                        ControlToValidate="txtEndDate" Display="None" ErrorMessage="Start Date Should Be Less Than the End Date"
                        CssClass="lblFont" Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td colspan="2" align="left">
                    <asp:Button ID="btnReport" SkinID="skinButtonBig" runat="server" CssClass="btnStyle"
                        OnClick="btnReport_Click" Text="Generate Report" />&nbsp;<input type="button" value="Print "
                            id="Button1" runat="Server" onclick="javascript:CallPrint('divPrint')" class="button" />&nbsp;
                    <asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="true" ShowSummary="false" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Label ID="lblErr" runat="server" CssClass="errorMsg"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <div id="divPrint" style="font-family: 'Trebuchet MS'; font-size: 11px;">
            <wc:ReportGridView runat="server" BorderWidth="1" ID="gvMain" GridLines="Both" AlternatingRowStyle-CssClass="even"
                AutoGenerateColumns="false" PrintPageSize="23" AllowPrintPaging="true" CssClass="gridview"
                Width="650px" CellPadding="2" Style="font-family: 'Trebuchet MS'; font-size: 11px;"
                OnRowDataBound="gvMain_RowDataBound" ShowFooter="True" ShowHeader="True" FooterStyle-CssClass="lblFont">
                <Columns>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <a href="javascript:switchViews('dv', 'imdiv');" style="text-decoration: none;">
                                <img id="imdiv" alt="Show" border="0" src="App_Themes/DefaultTheme/Images/plus.gif" />
                            </a>Customer Level
                            <br />
                            <div id="dv" style="display: none; position: relative; left: 25px;">
                                <wc:ReportGridView runat="server" BorderWidth="1" ID="gvSecond" CssClass="gridview"
                                    GridLines="Both" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                    DataKeyNames="CustomerID" Width="90%" Style="font-family: 'Trebuchet MS'; font-size: 11px;"
                                    OnRowDataBound="gvSecond_RowDataBound">
                                    <PageHeaderTemplate>
                                        <br />
                                        <br />
                                    </PageHeaderTemplate>
                                    <Columns>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <%-- <a href="javascript:switchViews('dv<%# Eval("CustomerID") %>', 'imdiv<%# Eval("CustomerID") %>');" style="text-decoration:none; ">
                                                                    <img id="imdiv<%# Eval("CustomerID") %>" alt="Show" border="0" src="App_Themes/DefaultTheme/Images/plus.gif" />
                                                                </a>--%>
                                                <a href="javascript:PrintItem(<%# Eval("CustomerID") %>,'<%# Eval("CustomerName")%>')">
                                                    <b>
                                                        <%# Eval("CustomerName") %>
                                                    </b></a>
                                                <div id="div<%# Eval("CustomerID") %>" style="display: none; position: relative;
                                                    left: 25px;">
                                                    <%-- <wc:ReportGridView runat="server" BorderWidth="1" ID="gvThird" CssClass="gridview" GridLines="Both" AlternatingRowStyle-CssClass="even"  
                                                                      AutoGenerateColumns="false"  Width="100%"  style="font-family:'Trebuchet MS'; font-size:11px;  "   >
                                                                        <PageHeaderTemplate>
                                                                            <br />
                                                                            <br />
                                                                        </PageHeaderTemplate>
                                                                        <Columns>
                                                                                <asp:TemplateField  ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top" HeaderText="Net Rate" >
                                                                                    <ItemTemplate>
                                                                                        <asp:Label style="font-family:'Trebuchet MS'; font-size:11px;  " ID="lblBillno" runat="server" Text = '<%# Eval("Billno","{0:f2}") %>' />
                                                                                    </ItemTemplate> 
                                                                                </asp:TemplateField> 
                                                                                <asp:TemplateField  ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top" HeaderText="Net Rate" >
                                                                                    <ItemTemplate>
                                                                                        <asp:Label style="font-family:'Trebuchet MS'; font-size:11px;  " ID="lblNetRate" runat="server" Text = '<%# Eval("SalesRate","{0:f2}") %>' />
                                                                                    </ItemTemplate> 
                                                                                </asp:TemplateField> 
                                                                                <asp:TemplateField  ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top" HeaderText="Discount Rate">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label style="font-family:'Trebuchet MS'; font-size:11px;  " ID="lblDiscountRate" runat="server" Text = '<%# Eval("SalesDiscount","{0:f2}") %>' />
                                                                                    </ItemTemplate> 
                                                                                </asp:TemplateField>
                                                                                
                                                                                <asp:TemplateField  ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top" HeaderText="VAT Rate">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label style="font-family:'Trebuchet MS'; font-size:11px;  " ID="lblVatRate" runat="server" Text = '<%# Eval("SumVat","{0:f2}") %>' />
                                                                                    </ItemTemplate> 
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField  ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top" HeaderText="CST Rate">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label style="font-family:'Trebuchet MS'; font-size:11px;  " ID="lblCSTRate" runat="server" Text = '<%# Eval("SumCST","{0:f2}") %>' />
                                                                                    </ItemTemplate> 
                                                                                </asp:TemplateField> 
                                                                                <asp:TemplateField  ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top" HeaderText="Freight">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label style="font-family:'Trebuchet MS'; font-size:11px;  " ID="lblFreightRate" runat="server" Text = '<%# Eval("SumFreight","{0:f2}") %>' />
                                                                                    </ItemTemplate> 
                                                                                </asp:TemplateField> 
                                                                                <asp:TemplateField  ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top" HeaderText="Loading / Unloading">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label style="font-family:'Trebuchet MS'; font-size:11px;  " ID="lblLURate" runat="server" Text = '<%# Eval("Loading","{0:f2}") %>' />
                                                                                    </ItemTemplate> 
                                                                                </asp:TemplateField> 
                                                                           </Columns>
                                                                     </wc:ReportGridView> --%>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                                            HeaderText="Net Rate">
                                            <ItemTemplate>
                                                <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblNetRate"
                                                    runat="server" Text='<%# Eval("SalesRate","{0:f2}") %>' />
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
                                                    runat="server" Text='<%# Eval("SumFreight","{0:f2}") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                                            HeaderText="Loading / Unloading">
                                            <ItemTemplate>
                                                <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblLURate" runat="server"
                                                    Text='<%# Eval("Loading","{0:f2}") %>' />
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
                                runat="server" Text='<%# Eval("SumFreight","{0:f2}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                        HeaderText="Loading / Unloading">
                        <ItemTemplate>
                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblLURate" runat="server"
                                Text='<%# Eval("Loading","{0:f2}") %>' />
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
    </form>
</body>
</html>
