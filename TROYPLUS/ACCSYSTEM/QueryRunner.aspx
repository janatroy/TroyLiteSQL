<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="QueryRunner.aspx.cs" Inherits="QueryRunner" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>



    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Stock Report</title>
        <%--<script language="javascript" type="text/javascript">

            function pageLoad() {
                //  get the behavior associated with the tab control
                var tabContainer = $find('ctl00_cplhControlPanel_tabContol');

//                if (tabContainer == null)
//                    tabContainer = $find('ctl00_cplhControlPanel_tabPanelMaster');

                if (tabContainer != null) {
                    //  get all of the tabs from the container
                    var tabs = tabContainer.get_tabs();

                    //  loop through each of the tabs and attach a handler to
                    //  the tab header's mouseover event
                    for (var i = 0; i < tabs.length; i++) {
                        var tab = tabs[i];

                        $addHandler(
                tab.get_headerTab(),
                'mouseover',
                Function.createDelegate(tab, function () {
                    tabContainer.set_activeTab(this);
                }
            ));
                    }
                }
            }

    </script>--%>
    </head>

    <body>
     
    <form id="form1" runat="server" style="width:auto; height:auto; color:Black">

     <asp:ScriptManager runat="server" ID="scr"></asp:ScriptManager>
     <div style=" width:100%; text-align: left">
        <div id="Div1" style="width:100%;">
                    <table style="width: 100%;" align="center">
                        <tr style="width: 100%">
                            <td style="width: 100%">
                                <table style="text-align: left; border:0px solid silver" width="100%">
                                    <tr style="height:3px">
                                    </tr>
                                    <tr>
                                        <td colspan="5" class="subHeadFont">
                                             REPORT RUNNER
                                        </td>
                                    </tr>
                                    <tr style="height:2px">
                                    </tr>
                                    <tr>
                                        <td colspan="5" style="width: 100%">
                                           <table width="100%" cellpadding="1" cellspacing="1" >                  
                                                <tr style="height:3px">
                                                </tr>
                                                <tr>
                                                    <td colspan="5" style="width: 100%">
                                                        <table style="width: 100%">
                                                            <tr>
                                                                <td style="width: 5%">
                                                                </td>
                           
                                                                <td style="width: 90%;" align="right">
                                                                        <asp:Button ID="BtnGenerateReport" runat="server" 
                                                                                                Width="110px" ValidationGroup="sendSMS" EnableTheming="false"
                                                                                                CssClass="generatebutton" 
                                                                        TabIndex="7" Visible="False" />
                                                                </td>
                                                                <td style="width: 20%;" align="left">
                                                                    <asp:Button ID="btncancel2" runat="server" EnableTheming="False" 
                                                                        CssClass="cancelbutton" OnClientClick="window.close();"/>
                                                                        
                                                                </td>
                                                            </tr>
                                                            <tr style="height:5px">
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width: 5%">
                                                                    
                                                                </td>
                                                                <td style="width:90%">
                                                                    1. Stock Consistency Check
                                                                </td>
                                                                <td style="width:5%">
                                                                           
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width: 5%">
                                                                    
                                                                </td>
                                                                <td style="width:90%">
                                                                    <asp:TextBox ID="txtQuery" BorderColor="Blue" Width="100%" BorderStyle="Solid" BorderWidth="1px"  runat="server" TextMode="MultiLine" Height="150px" BackColor = "#90C9FC" ></asp:TextBox>   
                                                                </td>
                                                                <td style="width: 20%;">
                                                                    <asp:Button ID="Button1" runat="server" 
                                                                        Width="110px" ValidationGroup="sendSMS" EnableTheming="false"
                                                                        CssClass="generatebutton" 
                                                                        OnClick="Button1_Click" TabIndex="7" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width: 5%">
                                                                    
                                                                </td>
                                                                <td style="width:90%">
                                                                    2. Missing Ledger in sales
                                                                </td>
                                                                <td style="width:5%">
                                                                           
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width: 5%">
                                                                   
                                                                </td>
                                                                <td style="width:90%">
                                                                    <asp:TextBox ID="txtmissingledsales" BorderColor="Blue" Width="100%" BorderStyle="Solid" BorderWidth="1px"  runat="server" TextMode="MultiLine" Height="50px" BackColor = "#90C9FC" ></asp:TextBox>   
                                                                </td>
                                                                <td style="width: 20%;">
                                                                    <asp:Button ID="Button2" runat="server" 
                                                                        Width="110px" ValidationGroup="sendSMS" EnableTheming="false"
                                                                        CssClass="generatebutton" 
                                                                        OnClick="Button2_Click" TabIndex="7" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width: 5%">
                                                                    
                                                                </td>
                                                                <td style="width:90%">
                                                                    3. Missing Ledger in Purchase
                                                                </td>
                                                                <td style="width:5%">
                                                                           
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width: 5%">
                                                                   
                                                                </td>
                                                                <td style="width:90%">
                                                                    <asp:TextBox ID="TextBox1" BorderColor="Blue" Width="100%" BorderStyle="Solid" BorderWidth="1px"  runat="server" TextMode="MultiLine" Height="50px" BackColor = "#90C9FC" ></asp:TextBox>   
                                                                </td>
                                                                <td style="width: 20%;">
                                                                    <asp:Button ID="Button3" runat="server" 
                                                                        Width="110px" ValidationGroup="sendSMS" EnableTheming="false"
                                                                        CssClass="generatebutton" 
                                                                        OnClick="Button3_Click" TabIndex="7" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width: 5%">
                                                                    
                                                                </td>
                                                                <td style="width:90%">
                                                                    4. Missing Ledger in Receipt
                                                                </td>
                                                                <td style="width:5%">
                                                                           
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width: 5%">
                                                                   
                                                                </td>
                                                                <td style="width:90%">
                                                                    <asp:TextBox ID="TextBox2" BorderColor="Blue" Width="100%" BorderStyle="Solid" BorderWidth="1px"  runat="server" TextMode="MultiLine" Height="50px" BackColor = "#90C9FC" ></asp:TextBox>   
                                                                </td>
                                                                <td style="width: 20%;">
                                                                    <asp:Button ID="Button4" runat="server" 
                                                                        Width="110px" ValidationGroup="sendSMS" EnableTheming="false"
                                                                        CssClass="generatebutton" 
                                                                        OnClick="Button4_Click" TabIndex="7" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width: 5%">
                                                                    
                                                                </td>
                                                                <td style="width:90%">
                                                                    5. Missing Ledger in Payment
                                                                </td>
                                                                <td style="width:5%">
                                                                           
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width: 5%">
                                                                   
                                                                </td>
                                                                <td style="width:90%">
                                                                    <asp:TextBox ID="TextBox3" BorderColor="Blue" Width="100%" BorderStyle="Solid" BorderWidth="1px"  runat="server" TextMode="MultiLine" Height="50px" BackColor = "#90C9FC" ></asp:TextBox>   
                                                                </td>
                                                                <td style="width: 20%;">
                                                                    <asp:Button ID="Button5" runat="server" 
                                                                        Width="110px" ValidationGroup="sendSMS" EnableTheming="false"
                                                                        CssClass="generatebutton" 
                                                                        OnClick="Button5_Click" TabIndex="7" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width: 5%">
                                                                    
                                                                </td>
                                                                <td style="width:90%">
                                                                    6. Missing Ledger in Credit Debit Note
                                                                </td>
                                                                <td style="width:5%">
                                                                           
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width: 5%">
                                                                   
                                                                </td>
                                                                <td style="width:90%">
                                                                    <asp:TextBox ID="TextBox4" BorderColor="Blue" Width="100%" BorderStyle="Solid" BorderWidth="1px"  runat="server" TextMode="MultiLine" Height="50px" BackColor = "#90C9FC" ></asp:TextBox>   
                                                                </td>
                                                                <td style="width: 20%;">
                                                                    <asp:Button ID="Button6" runat="server" 
                                                                        Width="110px" ValidationGroup="sendSMS" EnableTheming="false"
                                                                        CssClass="generatebutton" 
                                                                        OnClick="Button6_Click" TabIndex="7" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width: 5%">
                                                                    
                                                                </td>
                                                                <td style="width:90%">
                                                                    7. Missing Ledger in Daybook (Credit)
                                                                </td>
                                                                <td style="width:5%">
                                                                           
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width: 5%">
                                                                   
                                                                </td>
                                                                <td style="width:90%">
                                                                    <asp:TextBox ID="TextBox5" BorderColor="Blue" Width="100%" BorderStyle="Solid" BorderWidth="1px"  runat="server" TextMode="MultiLine" Height="50px" BackColor = "#90C9FC" ></asp:TextBox>   
                                                                </td>
                                                                <td style="width: 20%;">
                                                                    <asp:Button ID="Button7" runat="server" 
                                                                        Width="110px" ValidationGroup="sendSMS" EnableTheming="false"
                                                                        CssClass="generatebutton" 
                                                                        OnClick="Button7_Click" TabIndex="7" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width: 5%">
                                                                    
                                                                </td>
                                                                <td style="width:90%">
                                                                    8. Missing Ledger in Daybook (Debit)
                                                                </td>
                                                                <td style="width:5%">
                                                                           
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width: 5%">
                                                                   
                                                                </td>
                                                                <td style="width:90%">
                                                                    <asp:TextBox ID="TextBox6" BorderColor="Blue" Width="100%" BorderStyle="Solid" BorderWidth="1px"  runat="server" TextMode="MultiLine" Height="50px" BackColor = "#90C9FC" ></asp:TextBox>   
                                                                </td>
                                                                <td style="width: 20%;">
                                                                    <asp:Button ID="Button8" runat="server" 
                                                                        Width="110px" ValidationGroup="sendSMS" EnableTheming="false"
                                                                        CssClass="generatebutton" 
                                                                        OnClick="Button8_Click" TabIndex="7" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width: 5%">
                                                                    
                                                                </td>
                                                                <td style="width:90%">
                                                                    9. Missing sales in Daybook
                                                                </td>
                                                                <td style="width:5%">
                                                                           
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width: 5%">
                                                                   
                                                                </td>
                                                                <td style="width:90%">
                                                                    <asp:TextBox ID="TextBox7" BorderColor="Blue" Width="100%" BorderStyle="Solid" BorderWidth="1px"  runat="server" TextMode="MultiLine" Height="50px" BackColor = "#90C9FC" ></asp:TextBox>   
                                                                </td>
                                                                <td style="width: 20%;">
                                                                    <asp:Button ID="Button9" runat="server" 
                                                                        Width="110px" ValidationGroup="sendSMS" EnableTheming="false"
                                                                        CssClass="generatebutton" 
                                                                        OnClick="Button9_Click" TabIndex="7" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width: 5%">
                                                                    
                                                                </td>
                                                                <td style="width:90%">
                                                                    10. Missing Purchase in Daybook
                                                                </td>
                                                                <td style="width:5%">
                                                                           
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width: 5%">
                                                                   
                                                                </td>
                                                                <td style="width:90%">
                                                                    <asp:TextBox ID="TextBox8" BorderColor="Blue" Width="100%" BorderStyle="Solid" BorderWidth="1px"  runat="server" TextMode="MultiLine" Height="50px" BackColor = "#90C9FC" ></asp:TextBox>   
                                                                </td>
                                                                <td style="width: 20%;">
                                                                    <asp:Button ID="Button10" runat="server" 
                                                                        Width="110px" ValidationGroup="sendSMS" EnableTheming="false"
                                                                        CssClass="generatebutton" 
                                                                        OnClick="Button10_Click" TabIndex="7" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width: 5%">
                                                                    
                                                                </td>
                                                                <td style="width:90%">
                                                                    11. Missing Receipt in Daybook
                                                                </td>
                                                                <td style="width:5%">
                                                                           
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width: 5%">
                                                                   
                                                                </td>
                                                                <td style="width:90%">
                                                                    <asp:TextBox ID="TextBox9" BorderColor="Blue" Width="100%" BorderStyle="Solid" BorderWidth="1px"  runat="server" TextMode="MultiLine" Height="50px" BackColor = "#90C9FC" ></asp:TextBox>   
                                                                </td>
                                                                <td style="width: 20%;">
                                                                    <asp:Button ID="Button11" runat="server" 
                                                                        Width="110px" ValidationGroup="sendSMS" EnableTheming="false"
                                                                        CssClass="generatebutton" 
                                                                        OnClick="Button11_Click" TabIndex="7" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width: 5%">
                                                                    
                                                                </td>
                                                                <td style="width:90%">
                                                                    12. Missing Payment in Daybook
                                                                </td>
                                                                <td style="width:5%">
                                                                           
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width: 5%">
                                                                   
                                                                </td>
                                                                <td style="width:90%">
                                                                    <asp:TextBox ID="TextBox10" BorderColor="Blue" Width="100%" BorderStyle="Solid" BorderWidth="1px"  runat="server" TextMode="MultiLine" Height="50px" BackColor = "#90C9FC" ></asp:TextBox>   
                                                                </td>
                                                                <td style="width: 20%;">
                                                                    <asp:Button ID="Button12" runat="server" 
                                                                        Width="110px" ValidationGroup="sendSMS" EnableTheming="false"
                                                                        CssClass="generatebutton" 
                                                                        OnClick="Button12_Click" TabIndex="7" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width: 5%">
                                                                    
                                                                </td>
                                                                <td style="width:90%">
                                                                    13. Missing CreditDebitnote in Daybook
                                                                </td>
                                                                <td style="width:5%">
                                                                           
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width: 5%">
                                                                   
                                                                </td>
                                                                <td style="width:90%">
                                                                    <asp:TextBox ID="TextBox11" BorderColor="Blue" Width="100%" BorderStyle="Solid" BorderWidth="1px"  runat="server" TextMode="MultiLine" Height="50px" BackColor = "#90C9FC" ></asp:TextBox>   
                                                                </td>
                                                                <td style="width: 20%;">
                                                                    <asp:Button ID="Button13" runat="server" 
                                                                        Width="110px" ValidationGroup="sendSMS" EnableTheming="false"
                                                                        CssClass="generatebutton" 
                                                                        OnClick="Button13_Click" TabIndex="7" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width: 5%">
                                                                    
                                                                </td>
                                                                <td style="width:90%">
                                                                    14. Missing Journel in Sales
                                                                </td>
                                                                <td style="width:5%">
                                                                           
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width: 5%">
                                                                   
                                                                </td>
                                                                <td style="width:90%">
                                                                    <asp:TextBox ID="TextBox12" BorderColor="Blue" Width="100%" BorderStyle="Solid" BorderWidth="1px"  runat="server" TextMode="MultiLine" Height="50px" BackColor = "#90C9FC" ></asp:TextBox>   
                                                                </td>
                                                                <td style="width: 20%;">
                                                                    <asp:Button ID="Button14" runat="server" 
                                                                        Width="110px" ValidationGroup="sendSMS" EnableTheming="false"
                                                                        CssClass="generatebutton" 
                                                                        OnClick="Button14_Click" TabIndex="7" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width: 5%">
                                                                    
                                                                </td>
                                                                <td style="width:90%">
                                                                    15. Missing Journel in Purchase
                                                                </td>
                                                                <td style="width:5%">
                                                                           
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width: 5%">
                                                                   
                                                                </td>
                                                                <td style="width:90%">
                                                                    <asp:TextBox ID="TextBox13" BorderColor="Blue" Width="100%" BorderStyle="Solid" BorderWidth="1px"  runat="server" TextMode="MultiLine" Height="50px" BackColor = "#90C9FC" ></asp:TextBox>   
                                                                </td>
                                                                <td style="width: 20%;">
                                                                    <asp:Button ID="Button15" runat="server" 
                                                                        Width="110px" ValidationGroup="sendSMS" EnableTheming="false"
                                                                        CssClass="generatebutton" 
                                                                        OnClick="Button15_Click" TabIndex="7" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width: 5%">
                                                                    
                                                                </td>
                                                                <td style="width:90%">
                                                                    16. Missing Journel in Receipt
                                                                </td>
                                                                <td style="width:5%">
                                                                           
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width: 5%">
                                                                   
                                                                </td>
                                                                <td style="width:90%">
                                                                    <asp:TextBox ID="TextBox14" BorderColor="Blue" Width="100%" BorderStyle="Solid" BorderWidth="1px"  runat="server" TextMode="MultiLine" Height="50px" BackColor = "#90C9FC" ></asp:TextBox>   
                                                                </td>
                                                                <td style="width: 20%;">
                                                                    <asp:Button ID="Button16" runat="server" 
                                                                        Width="110px" ValidationGroup="sendSMS" EnableTheming="false"
                                                                        CssClass="generatebutton" 
                                                                        OnClick="Button16_Click" TabIndex="7" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width: 5%">
                                                                    
                                                                </td>
                                                                <td style="width:90%">
                                                                    17. Missing Journel in Payment
                                                                </td>
                                                                <td style="width:5%">
                                                                           
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width: 5%">
                                                                   
                                                                </td>
                                                                <td style="width:90%">
                                                                    <asp:TextBox ID="TextBox15" BorderColor="Blue" Width="100%" BorderStyle="Solid" BorderWidth="1px"  runat="server" TextMode="MultiLine" Height="50px" BackColor = "#90C9FC" ></asp:TextBox>   
                                                                </td>
                                                                <td style="width: 20%;">
                                                                    <asp:Button ID="Button17" runat="server" 
                                                                        Width="110px" ValidationGroup="sendSMS" EnableTheming="false"
                                                                        CssClass="generatebutton" 
                                                                        OnClick="Button17_Click" TabIndex="7" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width: 5%">
                                                                    
                                                                </td>
                                                                <td style="width:90%">
                                                                    18. Missing Journel in CreditDebitnote
                                                                </td>
                                                                <td style="width:5%">
                                                                           
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width: 5%">
                                                                   
                                                                </td>
                                                                <td style="width:90%">
                                                                    <asp:TextBox ID="TextBox16" BorderColor="Blue" Width="100%" BorderStyle="Solid" BorderWidth="1px"  runat="server" TextMode="MultiLine" Height="50px" BackColor = "#90C9FC" ></asp:TextBox>   
                                                                </td>
                                                                <td style="width: 20%;">
                                                                    <asp:Button ID="Button18" runat="server" 
                                                                        Width="110px" ValidationGroup="sendSMS" EnableTheming="false"
                                                                        CssClass="generatebutton" 
                                                                        OnClick="Button18_Click" TabIndex="7" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width: 5%">
                                                                    
                                                                </td>
                                                                <td style="width:90%">
                                                                    19. Negative Stock
                                                                </td>
                                                                <td style="width:5%">
                                                                           
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width: 5%">
                                                                   
                                                                </td>
                                                                <td style="width:90%">
                                                                    <asp:TextBox ID="TextBox17" BorderColor="Blue" Width="100%" BorderStyle="Solid" BorderWidth="1px"  runat="server" TextMode="MultiLine" Height="50px" BackColor = "#90C9FC" ></asp:TextBox>   
                                                                </td>
                                                                <td style="width: 20%;">
                                                                    <asp:Button ID="Button19" runat="server" 
                                                                        Width="110px" ValidationGroup="sendSMS" EnableTheming="false"
                                                                        CssClass="generatebutton" 
                                                                        OnClick="Button19_Click" TabIndex="7" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr style="height:5px">
                                                    <td>
                                                        <wc:ReportGridView runat="server" BorderWidth="1" ID="gvProducts" AutoGenerateColumns="false"
                                                            Style="font-family: 'Trebuchet MS'; font-size: 11px;" HeaderStyle-HorizontalAlign="Left"
                                                            CssClass="gridview" AllowPrintPaging="true" Width="600px" EmptyDataText="No data found!"
                                                            GridLines="Both" AlternatingRowStyle-CssClass="even">
                                                            <HeaderStyle CssClass="ReportHeadataRow" />
                                                            <RowStyle CssClass="ReportdataRow" />
                                                            <AlternatingRowStyle CssClass="ReportdataRow" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Item Code">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("ItemCode") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Product Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("ProductName") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Stock">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDesc" runat="server" Text='<%# Eval("Stock") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="QuantityMismatch">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStock" runat="server" Text='<%# Eval("QuantityMismatch") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </wc:ReportGridView>
                                                    </td>
                                                </tr>
                                                <tr style="height:5px">
                                                    <td>
                                                        <wc:ReportGridView runat="server" BorderWidth="1" ID="ReportGridView1" AutoGenerateColumns="false"
                                                            Style="font-family: 'Trebuchet MS'; font-size: 11px;" HeaderStyle-HorizontalAlign="Left"
                                                            CssClass="gridview" AllowPrintPaging="true"  EmptyDataText="No data found!" Width="600px"
                                                            GridLines="Both" AlternatingRowStyle-CssClass="even">
                                                            <HeaderStyle CssClass="ReportHeadataRow" />
                                                            <RowStyle CssClass="ReportdataRow" />
                                                            <AlternatingRowStyle CssClass="ReportdataRow" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="creditorid">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("creditorid") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="transno">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("transno") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="transdate">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDesc" runat="server" Text='<%# Eval("transdate") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="narration">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStock1" runat="server" Text='<%# Eval("narration") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="vouchertype">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStock2" runat="server" Text='<%# Eval("vouchertype") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                           </Columns>
                                                        </wc:ReportGridView>
                                                    </td>
                                                </tr>
                                                <tr style="height:5px">
                                                    <td>
                                                        <wc:ReportGridView runat="server" BorderWidth="1" ID="ReportGridView2" AutoGenerateColumns="false"
                                                            Style="font-family: 'Trebuchet MS'; font-size: 11px;" HeaderStyle-HorizontalAlign="Left"
                                                            CssClass="gridview"  EmptyDataText="No data found!" AllowPrintPaging="true" Width="600px"
                                                            GridLines="Both" AlternatingRowStyle-CssClass="even">
                                                            <HeaderStyle CssClass="ReportHeadataRow" />
                                                            <RowStyle CssClass="ReportdataRow" />
                                                            <AlternatingRowStyle CssClass="ReportdataRow" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="debtorid">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("debtorid") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="transno">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("transno") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="transdate">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDesc" runat="server" Text='<%# Eval("transdate") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="narration">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStock1" runat="server" Text='<%# Eval("narration") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="vouchertype">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStock2" runat="server" Text='<%# Eval("vouchertype") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </wc:ReportGridView>
                                                    </td>
                                                </tr>
                                                <tr style="height:5px">
                                                    <td>
                                                        <wc:ReportGridView runat="server" BorderWidth="1" ID="ReportGridView3" AutoGenerateColumns="false"
                                                            Style="font-family: 'Trebuchet MS'; font-size: 11px;"  EmptyDataText="No data found!" HeaderStyle-HorizontalAlign="Left"
                                                            CssClass="gridview" AllowPrintPaging="true" Width="600px"
                                                            GridLines="Both" AlternatingRowStyle-CssClass="even">
                                                            <HeaderStyle CssClass="ReportHeadataRow" />
                                                            <RowStyle CssClass="ReportdataRow" />
                                                            <AlternatingRowStyle CssClass="ReportdataRow" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="billno">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("billno") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="billdate">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("billdate") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="customername">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDesc" runat="server" Text='<%# Eval("customername") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="journalid">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStock1" runat="server" Text='<%# Eval("journalid") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="customerid">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStock2" runat="server" Text='<%# Eval("customerid") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </wc:ReportGridView>
                                                    </td>
                                                </tr>
                                                <tr style="height:5px">
                                                    <td>
                                                        <wc:ReportGridView runat="server" BorderWidth="1" ID="ReportGridView4" AutoGenerateColumns="false"
                                                            Style="font-family: 'Trebuchet MS'; font-size: 11px;" HeaderStyle-HorizontalAlign="Left"
                                                            CssClass="gridview" AllowPrintPaging="true"  EmptyDataText="No data found!" Width="600px"
                                                            GridLines="Both" AlternatingRowStyle-CssClass="even">
                                                            <HeaderStyle CssClass="ReportHeadataRow" />
                                                            <RowStyle CssClass="ReportdataRow" />
                                                            <AlternatingRowStyle CssClass="ReportdataRow" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="billno">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("billno") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="billdate">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("billdate") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="supplierid">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDesc" runat="server" Text='<%# Eval("supplierid") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="journalid">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStock1" runat="server" Text='<%# Eval("journalid") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="voucherno">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStock2" runat="server" Text='<%# Eval("purchaseid") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </wc:ReportGridView>
                                                    </td>
                                                </tr>
                                                <tr style="height:5px">
                                                    <td>
                                                        <wc:ReportGridView runat="server" BorderWidth="1" ID="ReportGridView5" AutoGenerateColumns="false"
                                                            Style="font-family: 'Trebuchet MS'; font-size: 11px;" HeaderStyle-HorizontalAlign="Left"
                                                            CssClass="gridview" AllowPrintPaging="true" Width="600px"
                                                            GridLines="Both" AlternatingRowStyle-CssClass="even" EmptyDataText="No Data found">
                                                            <HeaderStyle CssClass="ReportHeadataRow" />
                                                            <RowStyle CssClass="ReportdataRow" />
                                                            <AlternatingRowStyle CssClass="ReportdataRow" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="receiptno">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("receiptno") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="creditorid">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("creditorid") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="paymode">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDesc" runat="server" Text='<%# Eval("paymode") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="journalid">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStock1" runat="server" Text='<%# Eval("journalid") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                            </Columns>
                                                        </wc:ReportGridView>
                                                    </td>
                                                </tr>
                                                <tr style="height:5px">
                                                    <td>
                                                        <wc:ReportGridView runat="server" BorderWidth="1" ID="ReportGridView6" AutoGenerateColumns="false"
                                                            Style="font-family: 'Trebuchet MS'; font-size: 11px;" HeaderStyle-HorizontalAlign="Left"
                                                            CssClass="gridview" AllowPrintPaging="true" Width="600px"
                                                            GridLines="Both" AlternatingRowStyle-CssClass="even" EmptyDataText="No Data found">
                                                            <HeaderStyle CssClass="ReportHeadataRow" />
                                                            <RowStyle CssClass="ReportdataRow" />
                                                            <AlternatingRowStyle CssClass="ReportdataRow" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="paymode">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("paymode") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="debtorid">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("debtorid") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="voucherno">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDesc" runat="server" Text='<%# Eval("voucherno") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="journalid">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStock1" runat="server" Text='<%# Eval("journalid") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="billno">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStock123" runat="server" Text='<%# Eval("billno") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </wc:ReportGridView>
                                                    </td>
                                                </tr>
                                                <tr style="height:5px">
                                                    <td>
                                                        <wc:ReportGridView runat="server" BorderWidth="1" ID="ReportGridView7" AutoGenerateColumns="false"
                                                            Style="font-family: 'Trebuchet MS'; font-size: 11px;" HeaderStyle-HorizontalAlign="Left"
                                                            CssClass="gridview" AllowPrintPaging="true" Width="600px"
                                                            GridLines="Both" AlternatingRowStyle-CssClass="even" EmptyDataText="No Data found">
                                                            <HeaderStyle CssClass="ReportHeadataRow" />
                                                            <RowStyle CssClass="ReportdataRow" />
                                                            <AlternatingRowStyle CssClass="ReportdataRow" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="cdtype">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("cdtype") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="refno">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("refno") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="amount">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDesc" runat="server" Text='<%# Eval("amount") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="notedate">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStock1" runat="server" Text='<%# Eval("notedate") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="ledgerid">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStock123" runat="server" Text='<%# Eval("ledgerid") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="transno">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStock132" runat="server" Text='<%# Eval("transno") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </wc:ReportGridView>
                                                    </td>
                                                </tr>
                                                <tr style="height:5px">
                                                    <td>
                                                        <wc:ReportGridView runat="server" BorderWidth="1" ID="ReportGridView8" AutoGenerateColumns="false"
                                                            Style="font-family: 'Trebuchet MS'; font-size: 11px;" HeaderStyle-HorizontalAlign="Left"
                                                            CssClass="gridview" AllowPrintPaging="true" Width="600px"
                                                            GridLines="Both" AlternatingRowStyle-CssClass="even" EmptyDataText="No Data found">
                                                            <HeaderStyle CssClass="ReportHeadataRow" />
                                                            <RowStyle CssClass="ReportdataRow" />
                                                            <AlternatingRowStyle CssClass="ReportdataRow" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="transno">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("transno") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="creditorid">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("creditorid") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="transdate">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDesc" runat="server" Text='<%# Eval("transdate") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="debtorid">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStock1" runat="server" Text='<%# Eval("debtorid") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="amount">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStock123" runat="server" Text='<%# Eval("amount") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="chequeno">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStock111" runat="server" Text='<%# Eval("chequeno") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="narration">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStock122" runat="server" Text='<%# Eval("narration") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="vouchertype">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStock333" runat="server" Text='<%# Eval("vouchertype") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="refno">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStock222" runat="server" Text='<%# Eval("refno") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </wc:ReportGridView>
                                                    </td>
                                                </tr>
                                                <tr style="height:5px">
                                                    <td>
                                                        <wc:ReportGridView runat="server" BorderWidth="1" ID="ReportGridView9" AutoGenerateColumns="false"
                                                            Style="font-family: 'Trebuchet MS'; font-size: 11px;" HeaderStyle-HorizontalAlign="Left"
                                                            CssClass="gridview" AllowPrintPaging="true" Width="600px"
                                                            GridLines="Both" AlternatingRowStyle-CssClass="even" EmptyDataText="No Data found">
                                                            <HeaderStyle CssClass="ReportHeadataRow" />
                                                            <RowStyle CssClass="ReportdataRow" />
                                                            <AlternatingRowStyle CssClass="ReportdataRow" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="transno">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("transno") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="creditorid">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("creditorid") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="transdate">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDesc" runat="server" Text='<%# Eval("transdate") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="debtorid">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStock1" runat="server" Text='<%# Eval("debtorid") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="amount">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStock123" runat="server" Text='<%# Eval("amount") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="chequeno">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStock111" runat="server" Text='<%# Eval("chequeno") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="narration">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStock122" runat="server" Text='<%# Eval("narration") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="vouchertype">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStock333" runat="server" Text='<%# Eval("vouchertype") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="refno">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStock222" runat="server" Text='<%# Eval("refno") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                            </Columns>
                                                        </wc:ReportGridView>
                                                    </td>
                                                </tr>
                                                <tr style="height:5px">
                                                    <td>
                                                        <wc:ReportGridView runat="server" BorderWidth="1" ID="ReportGridView10" AutoGenerateColumns="false"
                                                            Style="font-family: 'Trebuchet MS'; font-size: 11px;" HeaderStyle-HorizontalAlign="Left"
                                                            CssClass="gridview" AllowPrintPaging="true" Width="600px"
                                                            GridLines="Both" AlternatingRowStyle-CssClass="even" EmptyDataText="No Data found">
                                                            <HeaderStyle CssClass="ReportHeadataRow" />
                                                            <RowStyle CssClass="ReportdataRow" />
                                                            <AlternatingRowStyle CssClass="ReportdataRow" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="transno">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("transno") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="creditorid">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("creditorid") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="transdate">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDesc" runat="server" Text='<%# Eval("transdate") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="debtorid">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStock1" runat="server" Text='<%# Eval("debtorid") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="amount">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStock123" runat="server" Text='<%# Eval("amount") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="chequeno">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStock111" runat="server" Text='<%# Eval("chequeno") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="narration">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStock122" runat="server" Text='<%# Eval("narration") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="vouchertype">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStock333" runat="server" Text='<%# Eval("vouchertype") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="refno">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStock222" runat="server" Text='<%# Eval("refno") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                            </Columns>
                                                        </wc:ReportGridView>
                                                    </td>
                                                </tr>
                                                <tr style="height:5px">
                                                    <td>
                                                        <wc:ReportGridView runat="server" BorderWidth="1" ID="ReportGridView11" AutoGenerateColumns="false"
                                                            Style="font-family: 'Trebuchet MS'; font-size: 11px;" HeaderStyle-HorizontalAlign="Left"
                                                            CssClass="gridview" AllowPrintPaging="true" Width="600px"
                                                            GridLines="Both" AlternatingRowStyle-CssClass="even" EmptyDataText="No Data found">
                                                            <HeaderStyle CssClass="ReportHeadataRow" />
                                                            <RowStyle CssClass="ReportdataRow" />
                                                            <AlternatingRowStyle CssClass="ReportdataRow" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="transno">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("transno") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="creditorid">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("creditorid") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="transdate">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDesc" runat="server" Text='<%# Eval("transdate") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="debtorid">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStock1" runat="server" Text='<%# Eval("debtorid") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="amount">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStock123" runat="server" Text='<%# Eval("amount") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="chequeno">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStock111" runat="server" Text='<%# Eval("chequeno") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="narration">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStock122" runat="server" Text='<%# Eval("narration") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="vouchertype">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStock333" runat="server" Text='<%# Eval("vouchertype") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="refno">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStock222" runat="server" Text='<%# Eval("refno") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                            </Columns>
                                                        </wc:ReportGridView>
                                                    </td>
                                                </tr>
                                                <tr style="height:5px">
                                                    <td>
                                                        <wc:ReportGridView runat="server" BorderWidth="1" ID="ReportGridView12" AutoGenerateColumns="false"
                                                            Style="font-family: 'Trebuchet MS'; font-size: 11px;" HeaderStyle-HorizontalAlign="Left"
                                                            CssClass="gridview" AllowPrintPaging="true" Width="600px"
                                                            GridLines="Both" AlternatingRowStyle-CssClass="even" EmptyDataText="No Data found">
                                                            <HeaderStyle CssClass="ReportHeadataRow" />
                                                            <RowStyle CssClass="ReportdataRow" />
                                                            <AlternatingRowStyle CssClass="ReportdataRow" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="transno">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("transno") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="creditorid">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("creditorid") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="transdate">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDesc" runat="server" Text='<%# Eval("transdate") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="debtorid">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStock1" runat="server" Text='<%# Eval("debtorid") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="amount">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStock123" runat="server" Text='<%# Eval("amount") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="chequeno">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStock111" runat="server" Text='<%# Eval("chequeno") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="narration">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStock122" runat="server" Text='<%# Eval("narration") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="vouchertype">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStock333" runat="server" Text='<%# Eval("vouchertype") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="refno">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStock222" runat="server" Text='<%# Eval("refno") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                            </Columns>
                                                        </wc:ReportGridView>
                                                    </td>
                                                </tr>
                                                <tr style="height:5px">
                                                    <td>
                                                        <wc:ReportGridView runat="server" BorderWidth="1" ID="ReportGridView13" AutoGenerateColumns="false"
                                                            Style="font-family: 'Trebuchet MS'; font-size: 11px;" HeaderStyle-HorizontalAlign="Left"
                                                            CssClass="gridview" AllowPrintPaging="true" Width="600px"
                                                            GridLines="Both" AlternatingRowStyle-CssClass="even" EmptyDataText="No Data found">
                                                            <HeaderStyle CssClass="ReportHeadataRow" />
                                                            <RowStyle CssClass="ReportdataRow" />
                                                            <AlternatingRowStyle CssClass="ReportdataRow" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="billno">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("billno") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="billdate">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("billdate") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="customername">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDesc" runat="server" Text='<%# Eval("customername") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="journalid">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStock1" runat="server" Text='<%# Eval("journalid") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="customerid">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStock123" runat="server" Text='<%# Eval("customerid") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                              </Columns>
                                                        </wc:ReportGridView>
                                                    </td>
                                                </tr>
                                                <tr style="height:5px">
                                                    <td>
                                                        <wc:ReportGridView runat="server" BorderWidth="1" ID="ReportGridView14" AutoGenerateColumns="false"
                                                            Style="font-family: 'Trebuchet MS'; font-size: 11px;" HeaderStyle-HorizontalAlign="Left"
                                                            CssClass="gridview" AllowPrintPaging="true" Width="600px"
                                                            GridLines="Both" AlternatingRowStyle-CssClass="even" EmptyDataText="No Data found">
                                                            <HeaderStyle CssClass="ReportHeadataRow" />
                                                            <RowStyle CssClass="ReportdataRow" />
                                                            <AlternatingRowStyle CssClass="ReportdataRow" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="billno">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("billno") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="billdate">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("billdate") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="supplierid">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDesc" runat="server" Text='<%# Eval("supplierid") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="voucherno">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStock1" runat="server" Text='<%# Eval("purchaseid") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="journalid">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStock123" runat="server" Text='<%# Eval("journalid") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                    
                                                            </Columns>
                                                        </wc:ReportGridView>
                                                    </td>
                                                </tr>
                                                <tr style="height:5px">
                                                    <td>
                                                        <wc:ReportGridView runat="server" BorderWidth="1" ID="ReportGridView15" AutoGenerateColumns="false"
                                                            Style="font-family: 'Trebuchet MS'; font-size: 11px;" HeaderStyle-HorizontalAlign="Left"
                                                            CssClass="gridview" AllowPrintPaging="true" Width="600px"
                                                            GridLines="Both" AlternatingRowStyle-CssClass="even" EmptyDataText="No Data found">
                                                            <HeaderStyle CssClass="ReportHeadataRow" />
                                                            <RowStyle CssClass="ReportdataRow" />
                                                            <AlternatingRowStyle CssClass="ReportdataRow" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="receiptno">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("receiptno") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="creditorid">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("creditorid") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="journalid">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDesc" runat="server" Text='<%# Eval("journalid") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="paymode">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStock1" runat="server" Text='<%# Eval("paymode") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                
                                                            </Columns>
                                                        </wc:ReportGridView>
                                                    </td>
                                                </tr>
                                                <tr style="height:5px">
                                                    <td>
                                                        <wc:ReportGridView runat="server" BorderWidth="1" ID="ReportGridView16" AutoGenerateColumns="false"
                                                            Style="font-family: 'Trebuchet MS'; font-size: 11px;" HeaderStyle-HorizontalAlign="Left"
                                                            CssClass="gridview" AllowPrintPaging="true" Width="600px"
                                                            GridLines="Both" AlternatingRowStyle-CssClass="even" EmptyDataText="No Data found">
                                                            <HeaderStyle CssClass="ReportHeadataRow" />
                                                            <RowStyle CssClass="ReportdataRow" />
                                                            <AlternatingRowStyle CssClass="ReportdataRow" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="voucherno">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("voucherno") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="billno">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("billno") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="journalid">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDesc" runat="server" Text='<%# Eval("journalid") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="paymode">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStock1" runat="server" Text='<%# Eval("paymode") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="debtorid">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStock123" runat="server" Text='<%# Eval("debtorid") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                            </Columns>
                                                        </wc:ReportGridView>
                                                    </td>
                                                </tr>
                                                <tr style="height:5px">
                                                    <td>
                                                        <wc:ReportGridView runat="server" BorderWidth="1" ID="ReportGridView17" AutoGenerateColumns="false"
                                                            Style="font-family: 'Trebuchet MS'; font-size: 11px;" HeaderStyle-HorizontalAlign="Left"
                                                            CssClass="gridview" AllowPrintPaging="true" Width="600px"
                                                            GridLines="Both" AlternatingRowStyle-CssClass="even" EmptyDataText="No Data found">
                                                            <HeaderStyle CssClass="ReportHeadataRow" />
                                                            <RowStyle CssClass="ReportdataRow" />
                                                            <AlternatingRowStyle CssClass="ReportdataRow" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="cdtype">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("cdtype") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="refno">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("refno") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="amount">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDesc" runat="server" Text='<%# Eval("amount") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="ledgerid">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStock1" runat="server" Text='<%# Eval("ledgerid") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="notedate">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStock123" runat="server" Text='<%# Eval("notedate") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="transno">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStock132" runat="server" Text='<%# Eval("transno") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </wc:ReportGridView>
                                                    </td>
                                                </tr>
                                                <tr style="height:5px">
                                                    <td>
                                                        <wc:ReportGridView runat="server" BorderWidth="1" ID="ReportGridView18" AutoGenerateColumns="false"
                                                            Style="font-family: 'Trebuchet MS'; font-size: 11px;" HeaderStyle-HorizontalAlign="Left"
                                                            CssClass="gridview" AllowPrintPaging="true" Width="600px"
                                                            GridLines="Both" AlternatingRowStyle-CssClass="even" EmptyDataText="No Data found">
                                                            <HeaderStyle CssClass="ReportHeadataRow" />
                                                            <RowStyle CssClass="ReportdataRow" />
                                                            <AlternatingRowStyle CssClass="ReportdataRow" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="ItemCode">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("ItemCode") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="ProductName">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("ProductName") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Model">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDesc" runat="server" Text='<%# Eval("Model") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Categoryid">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStock1" runat="server" Text='<%# Eval("Categoryid") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Brand">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStock123" runat="server" Text='<%# Eval("productdesc") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="stock">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStock132" runat="server" Text='<%# Eval("stock") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </wc:ReportGridView>
                                                    </td>
                                                </tr>
                                           </table>
                                      </td>
                                 </tr>
                             </table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
