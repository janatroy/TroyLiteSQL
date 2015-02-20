<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="Stock" Title="Stock Page" %>

<%--<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>--%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">

   
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    
        
    <style type="text/css">
        .style4
        {
        }
        .style21
        {
            height: 31px;
            font-weight: bold;
        }
        .style23
        {
            width: 236px;
            height: 28px;
            font-weight: bold;
        }
        .style24
        {
            height: 36px;
            font-weight: bold;
        }
        .style25
        {
            width: 236px;
            height: 36px;
            font-weight: bold;
        }
        .style35
        {
            width: 222px;
            height: 28px;
        }
        .style36
        {
            width: 222px;
            height: 36px;
        }
        .style40
        {
            width: 222px;
        }
        .style42
        {
            font-family: "Courier New", Courier, monospace;
            font-size: small;
            font-weight: 700;
            color: #003366;
        }
        .style45
        {
            width: 222px;
            font-family: Verdana;
            font-size: x-small;
            height: 25px;
        }
        .style47
        {
            width: 313px;
            font-weight: bold;
        }
        .style49
        {
            width: 236px;
            height: 21px;
            font-weight: bold;
        }
        .style50
        {
            width: 222px;
            height: 21px;
        }
        .style52
        {
            height: 21px;
            font-weight: bold;
        }
        .style53
        {
            height: 17px;
            font-weight: bold;
        }
        .style54
        {
            width: 236px;
            height: 17px;
            font-weight: bold;
        }
        .style55
        {
            width: 222px;
            height: 17px;
        }
        .style58
        {
            width: 236px;
            height: 19px;
            font-weight: bold;
        }
        .style59
        {
            width: 222px;
            height: 19px;
        }
        .style61
        {
            height: 19px;
            font-weight: bold;
        }
        .style62
        {
            height: 28px;
            font-weight: bold;
        }
        .style63
        {
            height: 23px;
        }
        .style64
        {
            height: 25px;
            font-weight: bold;
        }
        .style65
        {
            font-family: Verdana;
            font-size: x-small;
            height: 25px;
            font-weight: bold;
        }
        .style68
        {
            height: 23px;
            width: 313px;
            font-weight: bold;
        }
        .style69
        {
            font-family: "Courier New", Courier, monospace;
            font-size: small;
            color: #003366;
        }
        .style71
        {
            height: 31px;
            font-family: "Courier New", Courier, monospace;
            font-size: small;
            font-weight: bold;
            color: #003366;
        }
        .style73
        {
            height: 23px;
            font-weight: bold;
        }
        .style74
        {
            font-weight: bold;
        }
        .style75
        {
            font-family: "Courier New", Courier, monospace;
            font-size: small;
            color: #003366;
            font-weight: bold;
        }
        .style78
        {
            height: 23px;
            width: 238px;
        }
        .style79
        {
            width: 238px;
        }
    </style>
</head>
<body style="height: 619px; width: 815px;">
    <form id="form1" runat="server">
    <div align="center" 
        style="width: 805px; font-family: 'Courier New', Courier, monospace; font-size: large; font-weight: 700;">
    
        <br />
        <asp:Label ID="Label2" runat="server" Text="STOCK REPORT"></asp:Label>
        <br />
    
    </div>
    <div style="height: 419px; width: 882px">
        <table style="width: 430px; height: 388px">
            <tr>
                <td>
                    <asp:DropDownList ID="DropDownList1" runat="server" DataTextField="empFirstName"
            DataValueField="empno" Height="80px" Width="349px">
            <asp:ListItem Text="Select Executive" Value="0"></asp:ListItem>
        </asp:DropDownList>
    
                </td>
            </tr>
            <tr>
                <td align="center" class="style74" colspan="4">
                    <asp:Label ID="Label1" runat="server" Text="Select Fields To Be Viewed" 
                        CssClass="style69"></asp:Label>
                </td>
                <td align="center" class="style74" colspan="2">
                    <asp:Label ID="Label23" runat="server" CssClass="style69" 
                        Text="Filter by Particular Category"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style62" colspan="2">
                    <asp:CheckBox ID="chkboxCategory" runat="server" Text="Category" 
                        CssClass="style69" AutoPostBack="True" />
                </td>
                <td class="style23" colspan="2">
                    <asp:CheckBox ID="chkboxStock" runat="server" Text="Quantity" 
                        CssClass="style69" AutoPostBack="True" />
                </td>
                <td class="style23">
                    <asp:Label ID="Label30" runat="server" CssClass="style69" Text="Category"></asp:Label>
                </td>
                <td class="style35">                  
                    <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="true" 
                        Height="28px" CssClass="drpDownListMedium" BackColor = "#90c9fc" Width="75%" >
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style61" colspan="2">
                    <asp:CheckBox ID="chkboxBrand" runat="server" Text="Brand" CssClass="style69" 
                        AutoPostBack="True" />
                </td>
                <td class="style58" colspan="2">
                    <asp:CheckBox ID="chkboxBuyrate" runat="server" Text="BuyRate" 
                        CssClass="style69" AutoPostBack="True" />
                </td>
                <td class="style58">
                    <asp:Label ID="Label31" runat="server" CssClass="style69" Text="Brand"></asp:Label>
                </td>
                <td class="style59">
                    <asp:DropDownList ID="ddlBrand" runat="server" AutoPostBack="true" 
                        Height="28px" CssClass="drpDownListMedium" BackColor = "#90c9fc" Width="75%" >
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style24" colspan="2">
                    <asp:CheckBox ID="chkboxModel" runat="server" Text="Model" CssClass="style69" 
                        AutoPostBack="True" />
                </td>
                <td class="style25" colspan="2">
                    <asp:CheckBox ID="chkboxRate" runat="server" Text="Rate" CssClass="style69" 
                        AutoPostBack="True" />
                </td>
                <td class="style25">
                    <asp:Label ID="Label32" runat="server" CssClass="style69" Text="Product"></asp:Label>
                </td>
                <td class="style36">
                    <asp:DropDownList ID="ddlPrdctNme" runat="server" AutoPostBack="true" 
                        Height="28px" CssClass="drpDownListMedium" BackColor = "#90c9fc" 
                        Width="75%" >
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style53" colspan="2">
                    <asp:CheckBox ID="chkboxProduct" runat="server" Text="Product" 
                        CssClass="style69" AutoPostBack="True" />
                </td>
                <td class="style54" colspan="2">
                    <asp:CheckBox ID="chkboxVat" runat="server" Text="VAT" CssClass="style69" 
                        AutoPostBack="True" />
                </td>
                <td class="style54">
                    <asp:Label ID="Label33" runat="server" CssClass="style69" Text="ItemCode"></asp:Label>
                </td>
                <td class="style55">
                    <asp:DropDownList ID="ddlItemCode" runat="server" AutoPostBack="true" 
                        Height="28px" CssClass="drpDownListMedium" BackColor = "#90c9fc" Width="75%" >
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style52" colspan="2">
                    <asp:CheckBox ID="chkboxItemCode" runat="server" Text="ItemCode" 
                        CssClass="style69" AutoPostBack="True" />
                </td>
                <td class="style49" colspan="2">
                    <asp:CheckBox ID="chkboxNlc" runat="server" Text="NLC" CssClass="style69" 
                        AutoPostBack="True" />
                </td>
                <td class="style49">
                    <asp:Label ID="Label34" runat="server" CssClass="style69" Text="Model"></asp:Label>
                </td>
                <td class="style50">
                    <asp:DropDownList ID="ddlMdl" runat="server" AutoPostBack="true" 
                        Height="28px" CssClass="drpDownListMedium" BackColor = "#90c9fc" 
                        Width="75%" >
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="center" class="style64" colspan="4">
                    <asp:CheckBox ID="chkboxAll" runat="server" Text="Select ALL" 
                        AutoPostBack="true" CssClass="style69" 
                        oncheckedchanged="AllChecked"/>
                </td>
                <td align="left" class="style65">
                    <asp:Label ID="Label35" runat="server" CssClass="style69" Text="Quantity"></asp:Label>
                    </td>
                <td align="left" class="style45">
                    <asp:DropDownList ID="ddlStock" runat="server" AutoPostBack="true" 
                        Height="28px" CssClass="drpDownListMedium" BackColor = "#90c9fc" 
                        Width="60px" >
                                                                        <asp:ListItem>All</asp:ListItem>
                        <asp:ListItem Value="GT 0">= 0</asp:ListItem>
                        <asp:ListItem Value="GT < StockValues">&lt;Value</asp:ListItem>
                        <asp:ListItem Value="GT > StockValues">&gt;Value</asp:ListItem>
                        <asp:ListItem Value="GT > 0">&gt;0</asp:ListItem>


                    </asp:DropDownList>
                    <asp:TextBox ID="Stocktxtbox" runat="server" CssClass="style42" Width="63px"></asp:TextBox>
                    </td>
            </tr>
            <tr>
                <td align="left" class="style21" colspan="2">
                    <asp:Label ID="Label22" runat="server" Text="GroupBy Particular Category" 
                        CssClass="style69"></asp:Label>
                </td>
                <td align="left" class="style21" colspan="2">
                    <asp:Label ID="Label29" runat="server" CssClass="style69" 
                        Text="Order by Particular Category"></asp:Label>
                </td>
                <td align="left" class="style71" colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center" class="style47">
                    <asp:Label ID="Label14" runat="server" Text="First Level" CssClass="style69"></asp:Label>
                </td>
                <td align="center" class="style79">
                    <asp:DropDownList ID="ddlfirstlvl" runat="server" AutoPostBack="true" 
                        Height="28px" CssClass="drpDownListMedium" BackColor = "#90c9fc" 
                        Width="137px" 
                        
                        style="font-family: 'Courier New', Courier, monospace; font-size: small; font-weight: bold; color: #003366" >
                    </asp:DropDownList>
                </td>
                <td align="center" class="style74">
                    <asp:Label ID="Label24" runat="server" CssClass="style69" Text="First Level"></asp:Label>
                </td>
                <td align="center">
                    <asp:DropDownList ID="odlfirstlvl" runat="server" AutoPostBack="true" 
                        Height="28px" CssClass="drpDownListMedium" BackColor = "#90c9fc" 
                        Width="153px" 
                        
                        
                        style="font-family: 'Courier New', Courier, monospace; font-size: small; font-weight: bold; color: #003366" >
                    </asp:DropDownList>
                </td>
                <td align="center">
                    <asp:Label ID="Label37" runat="server" CssClass="style75" Text="NLC"></asp:Label>
                </td>
                <td align="left" class="style40">
                    <asp:DropDownList ID="ddlNlc" runat="server" AutoPostBack="true" Height="28px" 
                        CssClass="drpDownListMedium" BackColor = "#90c9fc" Width="60px" >
                                       <asp:ListItem>All</asp:ListItem>
                        <asp:ListItem Value="GT < Nlcs ">&lt;Value</asp:ListItem>
                        <asp:ListItem Value="GT > Nlcs">&gt;Value</asp:ListItem>
                        <asp:ListItem Value="GT = Nlcs">=Value</asp:ListItem>

                    </asp:DropDownList>
                    <asp:TextBox ID="Nlctxtbox" runat="server" CssClass="style42" Height="22px" 
                        Width="62px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="center" class="style47">
                    <asp:Label ID="Label15" runat="server" Text="Second Level" CssClass="style69"></asp:Label>
                </td>
                <td align="center" class="style79">
                    <asp:DropDownList ID="ddlsecondlvl" runat="server" AutoPostBack="true" 
                        Height="23px" CssClass="drpDownListMedium" BackColor = "#90c9fc" 
                        Width="136px" 
                        
                        style="font-family: 'Courier New', Courier, monospace; font-size: small; font-weight: bold; color: #003366" >
                    </asp:DropDownList>
                </td>
                <td align="center" class="style74">
                    <asp:Label ID="Label25" runat="server" CssClass="style69" Text="Second Level"></asp:Label>
                </td>
                <td align="center">
                    <asp:DropDownList ID="odlsecondlvl" runat="server" AutoPostBack="true" 
                        Height="29px" CssClass="drpDownListMedium" BackColor = "#90c9fc" 
                        Width="153px" 
                        
                        
                        style="font-family: 'Courier New', Courier, monospace; font-size: small; font-weight: bold; color: #003366" >
                    </asp:DropDownList>
                </td>
                <td align="center">
                    <asp:Label ID="Label38" runat="server" CssClass="style75" Text="Rate"></asp:Label>
                </td>
                <td align="left" class="style40">
                    <asp:DropDownList ID="ddlRate" runat="server" AutoPostBack="true" Height="28px" 
                        CssClass="drpDownListMedium" BackColor = "#90c9fc" Width="60px" >
                                        <asp:ListItem>All</asp:ListItem>
                        <asp:ListItem Value="GT < Rated">&lt;Value</asp:ListItem>
                        <asp:ListItem Value="GT > Rated">&gt;Value</asp:ListItem>
                        <asp:ListItem Value="GT = Rated">=Value</asp:ListItem>

                    </asp:DropDownList>
                    <asp:TextBox ID="Ratetxtbox" runat="server" CssClass="style42" Width="61px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="center" class="style47">
                    
                    <asp:Label ID="Label18" runat="server" Text="Third Level" CssClass="style69"></asp:Label>
                </td>
                <td align="center" class="style79">
                    <asp:DropDownList ID="ddlthirdlvl" runat="server" AutoPostBack="true" 
                        Height="24px" CssClass="drpDownListMedium" BackColor = "#90c9fc" 
                        Width="138px" 
                        
                        style="font-family: 'Courier New', Courier, monospace; font-size: small; font-weight: bold; color: #003366" >
                    </asp:DropDownList>
                </td>
                <td align="center" class="style74">
                    <asp:Label ID="Label26" runat="server" CssClass="style69" Text="Third Level"></asp:Label>
                </td>
                <td align="center">
                    <asp:DropDownList ID="odlthirdlvl" runat="server" AutoPostBack="true" 
                        Height="28px" CssClass="drpDownListMedium" BackColor = "#90c9fc" 
                        Width="151px" 
                        
                        
                        style="font-family: 'Courier New', Courier, monospace; font-size: small; font-weight: bold; color: #003366" >
                    </asp:DropDownList>
                </td>
                <td align="center">
                    <asp:Label ID="Label39" runat="server" CssClass="style75" Text="BuyRate"></asp:Label>
                </td>
                <td align="left" class="style40">
                    <asp:DropDownList ID="ddlBuyrate" runat="server" AutoPostBack="true" 
                        Height="28px" CssClass="drpDownListMedium" BackColor = "#90c9fc" 
                        Width="60px" >
                                        <asp:ListItem>All</asp:ListItem>
                        <asp:ListItem Value="GT < Buyrates">&lt;Value</asp:ListItem>
                        <asp:ListItem Value="GT > Buyrates">&gt;Value</asp:ListItem>
                        <asp:ListItem Value="GT = Buyrates">=Value</asp:ListItem>

                    </asp:DropDownList>
                    <asp:TextBox ID="buyratetxtbox" runat="server" CssClass="style42" 
                        Width="62px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="center" class="style47">
                    <asp:Label ID="Label19" runat="server" Text="Fourth Level" CssClass="style69"></asp:Label>
                </td>
                <td align="center" class="style79">
                    <asp:DropDownList ID="ddlfourlvl" runat="server" AutoPostBack="true" 
                        Height="25px" CssClass="drpDownListMedium" BackColor = "#90c9fc" 
                        Width="139px" 
                        
                        style="font-family: 'Courier New', Courier, monospace; font-size: small; font-weight: bold; color: #003366" >
                    </asp:DropDownList>
                </td>
                <td align="center" class="style74">
                    <asp:Label ID="Label27" runat="server" CssClass="style69" Text="Fourth Level"></asp:Label>
                </td>
                <td align="center">
                    <asp:DropDownList ID="odlfourlvl" runat="server" AutoPostBack="true" 
                        Height="31px" CssClass="drpDownListMedium" BackColor = "#90c9fc" 
                        Width="150px" 
                        
                        
                        style="font-family: 'Courier New', Courier, monospace; font-size: small; font-weight: bold; color: #003366" >
                    </asp:DropDownList>
                </td>
                <td align="center">
                    <asp:Label ID="Label36" runat="server" CssClass="style75" Text="VAT"></asp:Label>
                </td>
                <td align="left" class="style40">
                    <asp:DropDownList ID="ddlVat" runat="server" AutoPostBack="true" Height="28px" 
                        CssClass="drpDownListMedium" BackColor = "#90c9fc" Width="60px" >
                                        <asp:ListItem>All</asp:ListItem>
                        <asp:ListItem Value="GT < Vats">&lt;Value</asp:ListItem>
                        <asp:ListItem Value="GT > Vats">&gt;Value</asp:ListItem>
                        <asp:ListItem Value="GT = Vats">=Value</asp:ListItem>

                    </asp:DropDownList>
                    <asp:TextBox ID="Vattxtbox" runat="server" CssClass="style42" Width="63px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" class="style68">
                    <asp:Label ID="Label16" runat="server" Text="Fifth Level" CssClass="style69"></asp:Label>
                </td>
                <td align="center" class="style78">
                    <asp:DropDownList ID="ddlfifthlvl" runat="server" AutoPostBack="true" 
                        Height="24px" CssClass="drpDownListMedium" BackColor = "#90c9fc" 
                        Width="137px" 
                        
                        style="font-family: 'Courier New', Courier, monospace; font-size: small; font-weight: bold; color: #003366" >
                    </asp:DropDownList>
                </td>
                <td align="center" class="style73">
                    <asp:Label ID="Label28" runat="server" CssClass="style69" Text="Fifth Level"></asp:Label>
                </td>
                <td align="center" class="style63">
                    <asp:DropDownList ID="odlfifthlvl" runat="server" AutoPostBack="true" 
                        Height="31px" CssClass="drpDownListMedium" BackColor = "#90c9fc" 
                        Width="148px" 
                        
                        
                        style="font-family: 'Courier New', Courier, monospace; font-size: small; font-weight: bold; color: #003366" >
                    </asp:DropDownList>
                </td>
                <td align="right" class="style73">
                    <asp:Label ID="lblMsg" runat="server" Text="Label" CssClass="style69"></asp:Label>
                </td>
                <td align="right" class="style63">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center" class="style47">
                    <asp:Label ID="Label40" runat="server" CssClass="style69" Text="Name "></asp:Label>
                </td>
                <td align="center" class="style79">
                    <asp:TextBox ID="TextBox6" runat="server" CssClass="style75"></asp:TextBox>
                </td>
                <td align="center" class="style4">
                    <asp:Button ID="Button1" runat="server" CssClass="style75" 
                        Text="Save Selection" onclick="Button1_Click" />
                </td>
                <td align="center">
                    <asp:Button ID="Button2" runat="server" CssClass="style75" 
                        Text="Retrive Selection" onclick="Button2_Click" />
                </td>
                <td align="center">
                    <asp:Label ID="Label41" runat="server" CssClass="style75" Text="Date"></asp:Label>
                </td>
                <td align="center" class="style40">
                    <asp:TextBox ID="txtStartDate" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="6">
                    <asp:Button ID="btnxls" runat="server" CssClass="style42" 
                        Text="Export to Excel" onclick="btnxls_Click" />
                </td>

            </tr>
            </table>

        <table>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns= "false">

        <Columns>
        <asp:TemplateField>
        <HeaderTemplate>name</HeaderTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
        <HeaderTemplate>chkboxCategory</HeaderTemplate>
        
        </asp:TemplateField>
        <asp:TemplateField>
        <HeaderTemplate>chkboxBrand</HeaderTemplate>  
        </asp:TemplateField>
        <asp:TemplateField>
        <HeaderTemplate>chkboxProduct</HeaderTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
       <HeaderTemplate>chkboxModel</HeaderTemplate>
        
        </asp:TemplateField>
         <asp:TemplateField>
       <HeaderTemplate>chkboxItemcode</HeaderTemplate>
        
        </asp:TemplateField>
        
         <asp:TemplateField>
       <HeaderTemplate>chkboxstock</HeaderTemplate>
        
        </asp:TemplateField>
         <asp:TemplateField>
       <HeaderTemplate>chkboxnlc</HeaderTemplate>
        
        </asp:TemplateField>
         <asp:TemplateField>
       <HeaderTemplate>chkboxvat</HeaderTemplate>
        
        </asp:TemplateField>
         <asp:TemplateField>
       <HeaderTemplate>chkboxbuyrate</HeaderTemplate>
        
        </asp:TemplateField>
         <asp:TemplateField>
       <HeaderTemplate>chkboxrate</HeaderTemplate>
        
        </asp:TemplateField>
         <asp:TemplateField>
       <HeaderTemplate>chkboxall</HeaderTemplate>
        
        </asp:TemplateField>
         <asp:TemplateField>
       <HeaderTemplate>dbrand</HeaderTemplate>
        
        </asp:TemplateField>
         <asp:TemplateField>
       <HeaderTemplate>dcat</HeaderTemplate>
        
        </asp:TemplateField>
         <asp:TemplateField>
       <HeaderTemplate>dprod</HeaderTemplate>
        
        </asp:TemplateField>
         <asp:TemplateField>
       <HeaderTemplate>ditem</HeaderTemplate>
        
        </asp:TemplateField>
         <asp:TemplateField>
       <HeaderTemplate>dstock</HeaderTemplate>
        
        </asp:TemplateField>
         <asp:TemplateField>
       <HeaderTemplate>dnlc</HeaderTemplate>
        
        </asp:TemplateField>
       
         <asp:TemplateField>
       <HeaderTemplate>dvat</HeaderTemplate>
        
        </asp:TemplateField>
         <asp:TemplateField>
       <HeaderTemplate>dbuyrate</HeaderTemplate>
        
        </asp:TemplateField>
         <asp:TemplateField>
       <HeaderTemplate>drate</HeaderTemplate>
        
        </asp:TemplateField>
         <asp:TemplateField>
       <HeaderTemplate>dfirst</HeaderTemplate>
        </asp:TemplateField>
         <asp:TemplateField>
       <HeaderTemplate>dsecond</HeaderTemplate>
        
        </asp:TemplateField>
         <asp:TemplateField>
       <HeaderTemplate>dthird</HeaderTemplate>
        
        </asp:TemplateField>
         <asp:TemplateField>
       <HeaderTemplate>dfour</HeaderTemplate>
        
        </asp:TemplateField>
         <asp:TemplateField>
       <HeaderTemplate>dfifth</HeaderTemplate>

       </asp:TemplateField>
         <asp:TemplateField>
       <HeaderTemplate>odfirst</HeaderTemplate>
        </asp:TemplateField>
         <asp:TemplateField>
       <HeaderTemplate>odsecond</HeaderTemplate>
        
        </asp:TemplateField>
         <asp:TemplateField>
       <HeaderTemplate>odthird</HeaderTemplate>
        
        </asp:TemplateField>
         <asp:TemplateField>
       <HeaderTemplate>odfour</HeaderTemplate>
        
        </asp:TemplateField>
         <asp:TemplateField>
       <HeaderTemplate>odfifth</HeaderTemplate>
       </asp:TemplateField>
        </Columns>
                    </asp:GridView>
        </table>

        
    <%--cssDropDown--%>
<%--    border:solid 3px #90c9fc;
    height:24px;
    text-align:center;
    vertical-align:middle;
    color:#000000;
    font:normal 11px 'Trebuchet MS';--%>


    </div>
    </form>
</body>
</html>
