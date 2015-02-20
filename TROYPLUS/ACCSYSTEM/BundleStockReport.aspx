<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BundleStockReport.aspx.cs"
    Inherits="BundleStockReport" %>

<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">

    
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Stock Report</title>
    <link rel="Stylesheet" href="StyleSheet.css" />
    <script type="text/javascript">
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
<body>
    <form id="form1" runat="server">
    <div>
        <br />
        <br />
        <input type="button" value="Print " id="Button1" runat="Server" onclick="javascript:CallPrint('divPrint')"
            class="button" />&nbsp;
    </div>
    <br />
    <div class="fontName" id="divPrint" style="font-family: 'Trebuchet MS'; font-size: 11px;">
        <br />
        <table width="600px" border="0" style="font-family: 'Trebuchet MS'; font-size: 11px;">
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
                    &nbsp;
                </td>
                <td align="center">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="3" align="center">
                    <br />
                    <h5>
                        <wc:ReportGridView runat="server" BorderWidth="1" ID="gvCategory" CssClass="gridview"
                            GridLines="Both" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                            PrintPageSize="10" AllowPrintPaging="true" Width="600px" Style="font-family: 'Trebuchet MS';
                            font-size: 11px;" DataKeyNames="CategoryID" OnRowDataBound="GridView1_RowDataBound">
                            <Columns>
                                <%--<asp:TemplateField ItemStyle-VerticalAlign="Top" HeaderText="Category Name">
    <ItemTemplate>
     <asp:Label style="font-family:'Trebuchet MS'; font-size:11px;  " ID="lblCategory" runat="server" Text = '<%# Eval("CategoryName") %>' />
    </ItemTemplate> 
    </asp:TemplateField>--%>
                                <%-- <asp:TemplateField>
    <ItemTemplate>
     <asp:Label Visible="false"  ID="lblCategoryID" runat="server" Text = '<%# Eval("CategoryID") %>' />
    </ItemTemplate> 
    </asp:TemplateField> --%>
                                <asp:TemplateField HeaderText="Category Product View" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <a href="javascript:switchViews('div<%# Eval("CategoryName") %>', 'imgdiv<%# Eval("CategoryName") %>');"
                                            style="text-decoration: none;">
                                            <img id="imgdiv<%# Eval("CategoryName") %>" alt="Show" border="0" src="App_Themes/DefaultTheme/Images/plus.gif" />
                                        </a>
                                        <%--<a style="text-decoration:none" href='BalanceSheetLevel2.aspx?HeadingName=<%# Eval("HeadingName") %>&HeadingID=<%# Eval("HeadingID") %>'><asp:Label style="font-family:'Trebuchet MS'; font-size:11px;  " ID="lblparticulars" runat="server" Text = '<%# Eval("HeadingName") %>' /></a>--%>
                                        <%# Eval("CategoryName") %>
                                        <br />
                                        <div id="div<%# Eval("CategoryName") %>" style="display: none; position: relative;
                                            left: 25px;">
                                            <wc:ReportGridView runat="server" BorderWidth="1" ID="gvProducts" ShowFooter="true"
                                                AutoGenerateColumns="false" PrintPageSize="23" AllowPrintPaging="true" Width="90%"
                                                OnRowDataBound="GridView2_RowDataBound" Style="font-family: 'Trebuchet MS'; font-size: 11px;">
                                                <FooterStyle HorizontalAlign="Right" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Item Code" ItemStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("ItemCode") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Product Name" ItemStyle-Width="70px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("ProductName") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Model" ItemStyle-Width="80px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblModel" runat="server" Text='<%# Eval("Model") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description" ItemStyle-Width="120px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDesc" runat="server" Text='<%# Eval("ProductDesc") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Qty" ItemStyle-Width="30px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStock" runat="server" Text='<%# Eval("Stock") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Bundle Qty" ItemStyle-Width="30px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBunStock" runat="server" Text='<%# Eval("BundleQty") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Coir" ItemStyle-Width="30px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCoir" runat="server" Text='<%# Eval("Coir") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Bundle" ItemStyle-Width="30px">
                                                        <ItemTemplate>
                                                            <%--  <asp:Label ID="lblBudnle" runat="server" Text = '<%# Eval("Bundle") %>' />       --%>
                                                            <a onclick="window.open('BundleDet.aspx?ItemCode=<%# Eval("ItemCode") %> ','Summary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=1000,width=900,left=10,top=10, scrollbars=yes');"
                                                                id="ctl00_cplhControlPanel_lnkSummreports" style="text-decoration: underline;">
                                                                <%# Eval("Bundle") %></a>
                                                            <%--  <asp:LinkButton ID="LinkButton1"  runat="server" CommandArgument=<%# Eval("ItemCode") %> onclick="LinkButton1_Click"  ><%# Eval("Bundle") %></asp:LinkButton> %> --%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--  <asp:TemplateField HeaderText="Unit" ItemStyle-Width="30px">  
                           <ItemTemplate>
                             <asp:Label ID="lblUnit" runat="server" Text = '<%# Eval("Unit") %>' />       
                            </ItemTemplate>
                      </asp:TemplateField> --%>
                                                    <asp:TemplateField HeaderText="Rate" ItemStyle-Width="30px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRate" runat="server" Text='<%# Eval("Rate") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAmount" runat="server" ItemStyle-Width="40px" />
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblTotal" runat="server" Font-Bold="true" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </wc:ReportGridView>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount Summary" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblTotal" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <%--  <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
       <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />--%>
                            <%--  <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" />--%>
                        </wc:ReportGridView>
                        Stock List As On
                        <asp:Label ID="lblHeadDate" runat="server"> </asp:Label></h5>
                </td>
            </tr>
        </table>
        <br />
        <b><span style="font-family: 'Trebuchet MS'; font-size: 11px;">Grand Total : </span>
        </b>
        <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblGrandTotal"
            runat="server" Font-Bold="true" />
    </div>
    </form>
</body>
</html>
