<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportExcelFormulaExecution1.aspx.cs" Inherits="ReportExcelFormulaExecution1" %>

<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <title>Stock Report</title>
    <link href="App_Themes/DefaultTheme/DefaultTheme.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/DefaultTheme/calendar.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="Scripts/calendar_eu.js"></script>
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
    <div style="text-align: center; min-height: 400px">
        <div align="center">
            <br />
            
        </div>
        <div id="divPr" runat="server" align="center" visible="false">
            <table width="700px">
                <tr>
                    <td colspan="4">
                        <table width="100%">
                            <tr>
                                <td style="width:44%">

                                </td>
                                <td style="width:19%">
                                    <input type="button" id="Button3" runat="Server" onclick="javascript:CallPrint('divPrint')"
                                    class="printbutton6" style="padding-left: 25px;"/>
                                </td>
                                <td style="width:5%">
                                    <asp:Button ID="btndet" CssClass="GoBack" EnableTheming="false" runat="server"
                                        OnClick="btndet_Click" Visible="False" />
                                </td>
                                <td style="width:31%">

                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <br />
        <div class="fontName" align="center" id="divPrint" runat="server" visible="false" style="font-family: 'Trebuchet MS';
            font-size: 11px;">
            
            
            <table width="700px" border="0" style="font-family: Trebuchet MS; font-size: 14px;">
                <tr>
                    <td width="140px" align="left">
                        TIN#:
                        <asp:Label ID="lblTNGST" runat="server"></asp:Label>
                    </td>
                    <td align="center" width="320px" style="font-size: 20px;">
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
                       <%-- <h5>--%>
                           Specification of Products
                            <%--<asp:Label ID="lblHeadDate" runat="server"> </asp:Label></h5>--%>
                    </td>
                </tr>
            </table>
            <div style="width: 700px" runat="server"  align="center">
                <wc:ReportGridView runat="server"  BorderWidth="1" ID="Grdformula" GridLines="Both"
                    AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                    AllowPrintPaging="true" Width="100%" Style="font-family: 'Trebuchet MS'; font-size: 11px;"
                    DataKeyNames="FormulaName"  OnRowDataBound="GridView1_RowDataBound">
                    <HeaderStyle CssClass="ReportHeadataRow" />
                    <RowStyle CssClass="ReportdataRow" />
                    <AlternatingRowStyle CssClass="ReportAltdataRow" />
                     <Columns>
                          <asp:BoundField DataField="FormulaName" HeaderText="Product Name" HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Green" />
                           <asp:BoundField DataField="ItemCode" HeaderText="ItemCode" HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Green" />
                           <asp:BoundField DataField="Qty" HeaderText="Qty" HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Green" />
                           <asp:BoundField DataField="InOut" HeaderText="InOut" HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Green" />
                           <asp:BoundField DataField="ProductName" HeaderText="Product Name" HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Green" />
                           <asp:BoundField DataField="ProductDesc" HeaderText="Product desc" HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Green" />
                         <asp:BoundField DataField="Model" HeaderText="Model" HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Green" />
                           <asp:BoundField DataField="Stock" HeaderText="Current Stock" HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Green" />
                           <asp:BoundField DataField="IsReleased" HeaderText="Released" HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Green" />
                          <asp:BoundField DataField="CDate" HeaderText="Date" HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Green" />
                         <asp:BoundField DataField="Comments" HeaderText="comment" HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Green" />
                          <asp:BoundField DataField="IsReleased" HeaderText="Released" HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Green" />
                         
                           <asp:BoundField DataField="BranchCode" HeaderText="Branch Code" HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Green" />
                                                    
                                                            <%--<asp:Label ID="lblformulaname" runat="server" Text='<%# Eval("FormulaName") %>' />--%>
                                                     
                                                 
                                                         <%--   <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("ItemCode") %>' />--%>
                                                     
                                                   
                                                         <%--   <asp:Label ID="lblQty" runat="server" Text='<%# Eval("Qty") %>' />--%>
                                                      
                                                  
                                                        <%--    <asp:Label ID="lblInOut" runat="server" Text='<%# Eval("InOut") %>' />--%>
                                                       
                                                  
                                                           <%-- <asp:Label ID="lblUnitOfMeasure" runat="server" Text='<%# Eval("Unit_Of_Measure") %>' />
                                                    
                                                 
                                                            <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("ProductName") %>' />
                                                      
                                                 
                                                            <asp:Label ID="lblProductDesc" runat="server" Text='<%# Eval("ProductDesc") %>' />
                                                       
                                                 
                                                            <asp:Label ID="lblStock" runat="server" Text='<%# Eval("Stock") %>' />
                                                       
                                                   
                                                            <asp:Label ID="lblBranchName" runat="server" Text='<%# Eval("BranchName") %>' />--%>
                                                      
                                                  
                                                </Columns>
                </wc:ReportGridView>
                <br />
                <div style="text-align: right; visibility:hidden">
                    <b><span style="font-family: 'Trebuchet MS'; font-size: 11px;">Grand Total : </span>
                    </b>
                    <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblGrandTotal"
                        runat="server" Font-Bold="true" />
                </div>
            </div>
        </div>
                </div>
        <br />
        <br />
        <br />
    </div>
    </form>
</body>
</html>

