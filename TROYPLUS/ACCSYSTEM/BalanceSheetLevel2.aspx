<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="BalanceSheetLevel2.aspx.cs" Inherits="BalanceSheetLevel2" Title="Untitled Page" %>

<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <table width="100%" height="80%" cellpadding="3" cellspacing="3" style="height: 80%;
        border: 1px solid black; background-color: ALiceblue;">
        <tr>
            <td class="lblFont">
                <h3>
                    Balance Sheet Groups List -
                    <asp:Label ID="HeadingName" runat="server" CssClass="accordionHeaderSelected"></asp:Label>&nbsp;<a
                        href="javascript:history.go(-1)" style="text-decoration: none">[Go Back]</a></h3>
            </td>
        </tr>
        <tr>
            <!-- Liablity Side  ->
<td valign="top">
<wc:ReportGridView runat="server" BorderWidth="0" ID="gvGroup" GridLines="None"  AlternatingRowStyle-CssClass="even"  
                     AutoGenerateColumns="false" PrintPageSize="23" AllowPrintPaging="true"  CssClass="lblFont"  
                Width="50%" CellPadding="2"   style="font-family:'Trebuchet MS'; font-size:11px;  " OnRowDataBound="gvGroup_RowDataBound" ShowFooter="false" ShowHeader="false"  FooterStyle-CssClass="lblFont" >
                <PageHeaderTemplate>
                <br />
                 
               
                <br />
            </PageHeaderTemplate>
            
            <Columns>

               
            <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                <a style="text-decoration:none" href='TrailLedgerView.aspx?GroupName=<%# Eval("GroupName") %>&GroupID=<%# Eval("GroupID") %>'><asp:Label style="font-family:'Trebuchet MS'; font-size:11px;  " ID="lblparticulars" runat="server" Text = '<%# Eval("GroupName") %>' /></a>
                </ItemTemplate> 
            </asp:TemplateField> 
            <asp:TemplateField  ItemStyle-HorizontalAlign="Right"  >
                <ItemTemplate>
                    <asp:Label style="font-family:'Trebuchet MS'; font-size:11px;  " ID="lblSum" runat="server" Text = '<%# Eval("sum","{0:f2}") %>' />
                </ItemTemplate> 
            </asp:TemplateField> 
            
            
                
            
               
               
            </Columns>            
            <PagerTemplate>
        
            </PagerTemplate>
            <PageFooterTemplate>
               
                <br />
                      
    
               
            </PageFooterTemplate>
        </wc:ReportGridView>
</td>


</tr>

<tr>
         
        <td  align="right" ><asp:Label CssClass="tblLeft" ID="lblDebitTotal" runat="server"></asp:Label></td>
    
      
    </tr>
</table>
</asp:Content>

