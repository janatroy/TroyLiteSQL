<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="DashBoard.aspx.cs" Inherits="_DashBoard" Title="Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <script type="text/javascript">
        function PrintItem(branchcode) {
            // alert(branchcode);
            window.showModalDialog('./SalesdailyReport1.aspx?Req=N&BranchCode=' + branchcode, self, 'dialogWidth:1000px;dialogHeight:500px;status:no;dialogHide:yes;unadorned:yes;');
        }
        function PrintItem2(branchcode) {
            // alert(branchcode);
            window.showModalDialog('./SalesdailyReport2.aspx?Req=N&BranchCode=' + branchcode, self, 'dialogWidth:1000px;dialogHeight:500px;status:no;dialogHide:yes;unadorned:yes;');
        }

        function PrintItem1(branchcode) {
            // alert(branchcode);
            window.showModalDialog('./PurchasedailyReport1.aspx?Req=N&BranchCode=' + branchcode, self, 'dialogWidth:1000px;dialogHeight:500px;status:no;dialogHide:yes;unadorned:yes;');
        }


        // window.location.href = window.location;

    </script>
   <%-- <html>
        <head> 
            <meta http-equiv="refresh" content="10;URL=dashboard.aspx" />
            </head>
         
        </html>--%>
    <div style="vertical-align: top; text-align: left">
        <table width="100%" id="tblWarning" runat="server" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <div class="mainConBody">
                        <div>
                            <table cellspacing="2px" cellpadding="3px" border="0" width="99.8%" style="margin: -2px 0px 0px 1px;"
                                class="searchbg1">
                                <tr>
                                    <td style="width: 45%">
                                       
                                    </td>
                                    <td style="width: 50%; font-size: 17px; font-weight: 800; color: white;">Sales - Real-time Summary
                                    </td>
                                    <td style="width: 2%"></td>
                                    <td style="width: 15%; color: white;" align="right"></td>
                                    <td style="width: 17%" class="NewBox"></td>
                                    <td style="width: 5%"></td>
                                    <td style="width: 18%" class="NewBox">
                                        <div style="width: 160px; font-family: 'Trebuchet MS';">
                                        </div>
                                    </td>
                                    <td style="width: 13%; text-align: left"></td>
                                    <td style="width: 20%">
                                         <asp:Button ID="dpshow" runat="server"  Text="show-DP" Visible="false" OnClick="dpshow_Click" 
                                            EnableTheming="false" ForeColor="white" BackColor="#ff9900" />
                                    </td>
                                    <td style="width: 10%">
                                         <asp:Button ID="btnSearch" runat="server" Text="Refresh"  OnClick="btnSearch_Click"
                                            EnableTheming="false" ForeColor="white" BackColor="#ff9900" />
                                    </td>
                                </tr>
                                
                            </table>
                        </div>
                    </div>

                </td>
            </tr>
        </table>
        <%-- <table cellspacing="2px" cellpadding="3px" border="0" width="99.8%" style="margin: -2px 0px 0px 1px;">
                                <tr>
                                    <td style="width: 4%"></td>
                                     <td style=" border-left:solid 2px black; border-right:solid 2px black; width: 20%" align="center"> Today Sales</td>
                                    <td style=" border-right:solid 2px black; width: 17%;" align="center">
                                        Monthly sales
                                    </td>
                                    <td style="  border-right:solid 2px black; width: 20%" align="center">
                                        Annual Sales
                                    </td>
                                   

                                </tr>
                                     </table>--%>


        <table width="100%" style="margin: -5px 0px 0px 0px;">
            <tr style="width: 100%">
                <td>
                    <div id="div1" runat="server" style="height: 240px; overflow: scroll">
                        <asp:GridView ID="GrdWME" runat="server" DataKeyNames="BranchCode" AutoGenerateColumns="False"
                            HeaderStyle-HorizontalAlign="Center" RowStyle-HorizontalAlign="Center" Width="100%" CssClass="someClass"
                            OnRowCreated="GrdWME_RowCreated"
                            OnSelectedIndexChanged="GrdWME_SelectedIndexChanged" EmptyDataText="No Project Details found."
                            OnRowDataBound="GrdView_RowDataBound" OnRowCommand="GrdWME_RowCommand">
                            <HeaderStyle Height="30px" HorizontalAlign="Center" Font-Bold="true" BackColor="#cccccc" BorderColor="Gray" Font-Size="12px" />
                            <RowStyle Font-Bold="true" HorizontalAlign="Center" Height="22px" Font-Size="15px" CssClass="GrdItemForecolor" ForeColor="#414141" />
                            <Columns>
                                <asp:BoundField DataField="BranchCode" HeaderText="Branch" HeaderStyle-Width="15px" />
                                <asp:TemplateField Visible="false" FooterStyle-Font-Bold="True" HeaderStyle-HorizontalAlign="Left" HeaderText="Branch Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="20%">
                                    <ItemTemplate>
                                        <%--<asp:TextBox ID="txtPriceName" runat="server" Width="90%"  Text='<%# Eval("PriceName")%>' Enabled="false" Height="26px"
                                                                                                                        ></asp:TextBox>--%>
                                        <div style="margin-left: auto; margin-right: auto; text-align: left;">
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Branchname")%>' Font-Bold="true">
                                                                                                                           

                                            </asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%-- <asp:BoundField DataField="Branchname" HeaderText="Branchname" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="15px" />--%>
                                <asp:ButtonField DataTextField="DailySales" ItemStyle-ForeColor="Blue" CommandName="dailysales" HeaderText="Today's sales" HeaderStyle-Width="12px" ItemStyle-Font-Size="12px"/>      
                                <asp:BoundField DataField="Todaysalesquantity" HeaderText="Today's salesQty" HeaderStyle-Width="12px"  ItemStyle-Font-Size="12px"/>         
                                 <asp:BoundField DataField="DailySalesforgp" HeaderText="Today Mgnt.prft" HeaderStyle-Width="12px"  ItemStyle-Font-Size="12px"/>
                                <asp:BoundField DataField="DailySalesfordp" HeaderText="Today Branch.Prft" HeaderStyle-Width="12px"  ItemStyle-Font-Size="12px"/>
                                 
                                <asp:ButtonField DataTextField="MonthlySales" ItemStyle-ForeColor="Blue" CommandName="monthlysales" HeaderText="Monthly sales" HeaderStyle-Width="12px"  ItemStyle-Font-Size="12px"/>
                                 <asp:BoundField DataField="monthlysalesquantity" HeaderText="Monthly salesQty" HeaderStyle-Width="12px"  ItemStyle-Font-Size="12px"/>
                                
                                 <asp:BoundField DataField="montlySalesforgp" HeaderText="Monthly Mgnt.prft" HeaderStyle-Width="12px"  ItemStyle-Font-Size="12px"/>
                                 <asp:BoundField DataField="montlySalesfordp" HeaderText="Montly Branch.prft" HeaderStyle-Width="12px"  ItemStyle-Font-Size="12px"/>

                                 <asp:ButtonField DataTextField="AnnualSales" ItemStyle-ForeColor="Blue" CommandName="annualsales"  HeaderText="Annual sales" HeaderStyle-Width="12px"  ItemStyle-Font-Size="12px"/>      
                                <asp:BoundField DataField="Annualsalesquantity" HeaderText="Annual salesQty" HeaderStyle-Width="12px"  ItemStyle-Font-Size="12px"/>         
                                 <asp:BoundField DataField="AnnualSalesforgp" HeaderText="Annual Mgnt.prft" HeaderStyle-Width="12px"  ItemStyle-Font-Size="12px"/>
                                <asp:BoundField DataField="AnnualSalesfordp" HeaderText="Annual Branch.Prft" HeaderStyle-Width="12px"  ItemStyle-Font-Size="12px"/>
                               
                                <asp:TemplateField Visible="false" ItemStyle-CssClass="command" HeaderStyle-Width="50px" HeaderText="View Detailed report" HeaderStyle-BorderColor="Gray"
                                    ItemStyle-HorizontalAlign="Center">
                                    <%-- <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Select" />
                                                        <asp:ImageButton ID="btnEditDisabled" Enabled="false" SkinID="editDisable" runat="Server"></asp:ImageButton>
                                                    </ItemTemplate>--%>
                                    <ItemTemplate>
                                        <%--<a href='<%# DataBinder.Eval(Container, "DataItem.Billno", "javascript:PrintItem({0});") %>'>--%>

                                        <a href='<%#String.Format("javascript:PrintItem(&#39;{0}&#39;)",  Eval("BranchCode")) %>'>
                                            <asp:Image runat="server" ID="lnkprint" alt="Print" border="0" src="App_Themes/DefaultTheme/Images/PrintIcon_btn.png" />
                                        </a>
                                        <%-- <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Select" />--%>
                                        <%--<asp:ImageButton ID="btnEditDisabled" Visible="false" Enabled="false" SkinID="search" runat="Server"></asp:ImageButton>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>

                        </asp:GridView>
                        
                         <asp:GridView ID="GridViewdp" runat="server" DataKeyNames="BranchCode" AutoGenerateColumns="False"
                            HeaderStyle-HorizontalAlign="Center" RowStyle-HorizontalAlign="Center" Width="100%" CssClass="someClass"
                            OnRowCreated="GrdWME_RowCreated"
                            OnSelectedIndexChanged="GridViewdp_SelectedIndexChanged" EmptyDataText="No Project Details found."
                            OnRowDataBound="GridViewdp_RowDataBound" OnRowCommand="GridViewdp_RowCommand">
                           <HeaderStyle Height="30px" HorizontalAlign="Center" Font-Bold="true" BackColor="#cccccc" BorderColor="Gray" Font-Size="12px" />
                            <RowStyle Font-Bold="true" HorizontalAlign="Center" Height="22px" Font-Size="15px" CssClass="GrdItemForecolor" ForeColor="#414141" />
                            <Columns>
                                <asp:BoundField DataField="BranchCode" HeaderText="Branch" HeaderStyle-Width="15px" />
                                <asp:TemplateField Visible="false" FooterStyle-Font-Bold="True" HeaderStyle-HorizontalAlign="Left" HeaderText="Branch Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="20%">
                                    <ItemTemplate>
                                        <%--<asp:TextBox ID="txtPriceName" runat="server" Width="90%"  Text='<%# Eval("PriceName")%>' Enabled="false" Height="26px"
                                                                                                                        ></asp:TextBox>--%>
                                        <div style="margin-left: auto; margin-right: auto; text-align: left;">
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Branchname")%>' Font-Bold="true">
                                                                                                                           

                                            </asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%-- <asp:BoundField DataField="Branchname" HeaderText="Branchname" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="15px" />--%>
                                <asp:ButtonField DataTextField="DailySales" CommandName="dailysales" ItemStyle-ForeColor="Blue" HeaderText="Today's sales" HeaderStyle-Width="12px" />
                                 <asp:BoundField DataField="Todaysalesquantity" HeaderText="Today's salesQty" HeaderStyle-Width="12px" />                           
                               <asp:BoundField DataField="DailySalesfordp" HeaderText="Today Branch.Prft" HeaderStyle-Width="12px" />
                               
                                <asp:ButtonField DataTextField="MonthlySales" CommandName="monthlysales" ItemStyle-ForeColor="Blue" HeaderText="Monthly sales" HeaderStyle-Width="12px" />
                                <asp:BoundField DataField="monthlysalesquantity" HeaderText="Monthly salesQty" HeaderStyle-Width="12px" />
                                <asp:BoundField DataField="montlySalesfordp" HeaderText="Montly Branch.prft" HeaderStyle-Width="12px" />
                                
                                   <asp:ButtonField DataTextField="AnnualSales" ItemStyle-ForeColor="Blue" CommandName="annualsales"  HeaderText="Annual sales" HeaderStyle-Width="12px" />      
                                <asp:BoundField DataField="Annualsalesquantity" HeaderText="Annual salesQty" HeaderStyle-Width="12px" />      
                                <asp:BoundField DataField="AnnualSalesfordp" HeaderText="Annual Branch.Prft" HeaderStyle-Width="12px" />

                                <asp:TemplateField ItemStyle-CssClass="command" Visible="false"  HeaderStyle-Width="50px" HeaderText="View Detailed report" HeaderStyle-BorderColor="Gray"
                                    ItemStyle-HorizontalAlign="Center">
                                    <%-- <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Select" />
                                                        <asp:ImageButton ID="btnEditDisabled" Enabled="false" SkinID="editDisable" runat="Server"></asp:ImageButton>
                                                    </ItemTemplate>--%>
                                    <ItemTemplate>
                                        <%--<a href='<%# DataBinder.Eval(Container, "DataItem.Billno", "javascript:PrintItem({0});") %>'>--%>

                                        <a href='<%#String.Format("javascript:PrintItem2(&#39;{0}&#39;)",  Eval("BranchCode")) %>'>
                                            <asp:Image runat="server" ID="lnkprint" alt="Print" border="0" src="App_Themes/DefaultTheme/Images/PrintIcon_btn.png" />
                                        </a>
                                        <%-- <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Select" />--%>
                                        <%--<asp:ImageButton ID="btnEditDisabled" Visible="false" Enabled="false" SkinID="search" runat="Server"></asp:ImageButton>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>

                        </asp:GridView>
                    </div>

                </td>

            </tr>

        </table>




        <table width="100%" id="Table1" runat="server" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <div class="mainConBody">
                        <div>
                            <table cellspacing="2px" cellpadding="3px" border="0" width="99.8%" style="margin: -2px 0px 0px 1px;"
                                class="searchbg1">
                                <tr>
                                    <td style="width: 45%">
                                         
                                    </td>
                                    <td style="width: 50%; font-size: 17px; font-weight: 800; color: white;">Purchases - Real-time Summary
                                    </td>
                                    <td style="width: 2%"></td>
                                    <td style="width: 15%; color: white;" align="right"></td>
                                    <td style="width: 17%" class="NewBox"></td>
                                    <td style="width: 5%"></td>
                                    <td style="width: 18%" class="NewBox">
                                        <div style="width: 160px; font-family: 'Trebuchet MS';">
                                        </div>
                                    </td>
                                    <td style="width: 13%; text-align: left"></td>
                                    <td style="width: 20%"></td>
                                    <td style="width: 10%">
                                         <asp:Button ID="Button1" runat="server" Visible="false" Text="Refresh" OnClick="Button1_Click"
                                            EnableTheming="false" ForeColor="white" BackColor="#ff9900" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>

                </td>
            </tr>
        </table>
        <table width="100%" style="margin: -5px 0px 0px 0px;">
            <tr style="width: 100%">
                <td>
                    <div id="div2" runat="server" style="height: 240px; overflow: scroll">
                        <asp:GridView ID="grdpurchase" runat="server" DataKeyNames="BranchCode" AutoGenerateColumns="False"
                            HeaderStyle-HorizontalAlign="Center" RowStyle-HorizontalAlign="Center" Width="100%" CssClass="someClass"
                            OnRowCreated="grdpurchase_RowCreated"
                            OnSelectedIndexChanged="grdpurchase_SelectedIndexChanged" EmptyDataText="No Project Details found."
                            OnRowDataBound="grdpurchase_RowDataBound" OnRowCommand="grdpurchase_RowCommand">
                            <HeaderStyle Height="30px" HorizontalAlign="Center" Font-Bold="true" BackColor="#cccccc" BorderColor="Gray" Font-Size="12px" />

                            <RowStyle Font-Bold="true" HorizontalAlign="Center" Height="22px" Font-Size="15px" CssClass="GrdItemForecolor" ForeColor="#414141" />
                            <Columns>
                                <asp:BoundField DataField="BranchCode" HeaderText="Branch" HeaderStyle-Width="15px" />
                                <asp:TemplateField Visible="false"  FooterStyle-Font-Bold="True" HeaderStyle-HorizontalAlign="Left" HeaderText="Branch Name" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="20%">
                                    <ItemTemplate>
                                        <%--<asp:TextBox ID="txtPriceName" runat="server" Width="90%"  Text='<%# Eval("PriceName")%>' Enabled="false" Height="26px"
                                                                                                                        ></asp:TextBox>--%>
                                        <div style="margin-left: auto; margin-right: auto; text-align: left;">
                                            <asp:Label ID="Label12" runat="server" Text='<%# Eval("Branchname")%>' Font-Bold="true">

                                            </asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%-- <asp:BoundField DataField="Branchname" HeaderText="Branchname" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="15px" />--%>
                                <asp:ButtonField DataTextField="DailyPuchase" ItemStyle-ForeColor="Blue" CommandName="dailypurchase" HeaderText="Today's purchases" HeaderStyle-Width="12px" ItemStyle-Font-Size="12px" />
                                <asp:BoundField DataField="TodayPuchasequantity" HeaderText="Today's purchasesQty" HeaderStyle-Width="12px" ItemStyle-Font-Size="12px" />
                                <asp:ButtonField DataTextField="MonthlyPuchase" ItemStyle-ForeColor="Blue" CommandName="monthlypurchase" HeaderText="Monthly purchases" HeaderStyle-Width="12px" ItemStyle-Font-Size="12px" />
                                <asp:BoundField DataField="monthlyPuchasequantity" HeaderText="Monthly purchasesQty" HeaderStyle-Width="12px" ItemStyle-Font-Size="12px" />
                                <asp:ButtonField DataTextField="AnnualPuchase" ItemStyle-ForeColor="Blue" CommandName="annualpurchase" HeaderText="Annual purchases" HeaderStyle-Width="12px" ItemStyle-Font-Size="12px" />
                                <asp:BoundField DataField="AnnualPuchasequantity" HeaderText="Annual purchasesQty" HeaderStyle-Width="12px" ItemStyle-Font-Size="12px" />
                                <asp:TemplateField ItemStyle-CssClass="command" Visible="false" HeaderStyle-Width="50px" HeaderText="View Detailed report" HeaderStyle-BorderColor="Gray"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%--<a href='<%# DataBinder.Eval(Container, "DataItem.Billno", "javascript:PrintItem({0});") %>'>--%>

                                        <a href='<%#String.Format("javascript:PrintItem1(&#39;{0}&#39;)",  Eval("BranchCode")) %>'>
                                            <asp:Image runat="server" ID="lnkprint1" alt="Print" border="0" src="App_Themes/DefaultTheme/Images/PrintIcon_btn.png" />
                                        </a>
                                        <%-- <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Select" />--%>
                                        <%--<asp:ImageButton ID="btnEditDisabled" Visible="false" Enabled="false" SkinID="search" runat="Server"></asp:ImageButton>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>

                        </asp:GridView>
                    </div>

                </td>

            </tr>

        </table>
    </div>
</asp:Content>
