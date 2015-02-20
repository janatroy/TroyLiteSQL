<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StockReport1.aspx.cs" Inherits="StockReport1" %>

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
        <div align="center" id="div1" runat="server">
        <table cellpadding="1" cellspacing="2" width="450px" border="0" style="border: 1px solid blue; background-color:White;">
            <%--<tr class="subHeadFont">
                <td colspan="4">
                    Stock Report
                </td>
            </tr>--%>
            <tr>
                <td colspan="4" class="subHeadFont2">
                    Stock Report
                </td>
            </tr>
            <tr style="height:6px">
                    
            </tr>
            <tr>
                <td colspan="4">
                    <table  style="width:100%">
                        <tr>
                            <td style="width:45%; font-family:'ARIAL';font-size:11px;font-weight:normal; color: #000000;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                                Date
                            </td>
                            <td style="width:20%"  align="left" class="ControlTextBox3">
                                <asp:TextBox ID="txtStartDate" runat="server" CssClass="cssTextBox" Width="100px"
                                    MaxLength="10" />
                    
                            </td>
                            <td style="width:35%" align="left" >
                                <script type="text/javascript" language="JavaScript">                    new tcal({ 'formname': 'form1', 'controlname': 'txtStartDate' });</script>
                                <%--<a href="javascript:NewCal('txtStartDate','ddmmyyyy',false,24)"><img src="cal.gif" width="16" height="16" border="0" alt="Pick a date"></a>--%>
                                <asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="true" ShowSummary="false" />
                                <asp:Label ID="err" runat="server" Text="" ForeColor="Red" Font-Bold="true" class="lblFont"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtStartDate"
                                    Display="None" CssClass="lblFont" ErrorMessage="Please Enter Start Date"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="height:6px">
                    
            </tr>
            <tr>
                <td colspan="4">
                    <table  style="width:100%">
                        <tr>
                            <td style="width:31%">
                                
                                
                            </td>
                            <td style="width:19%">  
                                <asp:Button ID="btnReport" EnableTheming="false" runat="server" CssClass="NewReport6"
                                    Width="120px" OnClick="btnReport_Click"  />
                            </td>
                            <td style="width:19%">
                               <asp:Button ID="btnxls" runat="server" CssClass="exportexl6" 
                                    EnableTheming="false" OnClick="btnxls_Click" Width="153px" />
                            </td>
                            <td style="width:31%">
                                
                                
                            </td>
                        </tr>
                    </table>
                 </td>
             </tr>
            
             
        </table>
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
                        <h5>
                            Stock List As On
                            <asp:Label ID="lblHeadDate" runat="server"> </asp:Label></h5>
                    </td>
                </tr>
            </table>
            <div style="width: 700px" align="center">
                <wc:ReportGridView runat="server" BorderWidth="1" ID="gvCategory" GridLines="Both"
                    AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                    AllowPrintPaging="true" Width="100%" Style="font-family: 'Trebuchet MS'; font-size: 11px;"
                    DataKeyNames="CategoryID" OnRowDataBound="GridView1_RowDataBound">
                    <HeaderStyle CssClass="ReportHeadataRow" />
                    <RowStyle CssClass="ReportdataRow" />
                    <AlternatingRowStyle CssClass="ReportAltdataRow" />
                    <Columns>
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
                                        AutoGenerateColumns="false" AllowPrintPaging="true" Width="90%"
                                        OnRowDataBound="GridView2_RowDataBound" Style="font-family: 'Trebuchet MS'; font-size: 11px;">
                                        <HeaderStyle CssClass="ReportHeadataRow" />
                                        <RowStyle CssClass="ReportdataRow" />
                                        <AlternatingRowStyle CssClass="ReportAltdataRow" />
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
                                            <asp:TemplateField HeaderText="Brand" ItemStyle-Width="120px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDesc" runat="server" Text='<%# Eval("ProductDesc") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Qty" ItemStyle-Width="30px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStock" runat="server" Text='<%# Eval("Stock") %>' />
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
                </wc:ReportGridView>
                <br />
                <div style="text-align: right">
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
