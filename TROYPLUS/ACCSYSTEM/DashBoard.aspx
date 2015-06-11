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
                                    <td style="width: 50%; font-size: 17px; font-weight: 800; color: white;">Today's Sales - Real-time Summary
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
                                         <asp:Button ID="dpshow" runat="server" Text="show-DP" Visible="false" OnClick="dpshow_Click" 
                                            EnableTheming="false" ForeColor="white" BackColor="#ff9900" />
                                    </td>
                                    <td style="width: 10%">
                                         <asp:Button ID="btnSearch" runat="server" Text="Refresh" Visible="false" OnClick="btnSearch_Click"
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
                    <div id="div1" runat="server" style="height: 240px; overflow: scroll">
                        <asp:GridView ID="GrdWME" runat="server" DataKeyNames="BranchCode" AutoGenerateColumns="False"
                            HeaderStyle-HorizontalAlign="Center" RowStyle-HorizontalAlign="Center" Width="100%" CssClass="someClass"
                            OnRowCreated="GrdWME_RowCreated"
                            OnSelectedIndexChanged="GrdWME_SelectedIndexChanged" EmptyDataText="No Project Details found."
                            OnRowDataBound="GrdView_RowDataBound">
                            <HeaderStyle Height="30px" HorizontalAlign="Center" Font-Bold="true" BackColor="#cccccc" BorderColor="Gray" Font-Size="15px" />
                            <RowStyle Font-Bold="true" HorizontalAlign="Center" Height="22px" Font-Size="15px" CssClass="GrdItemForecolor" ForeColor="#414141" />
                            <Columns>
                                <asp:BoundField DataField="BranchCode" HeaderText="Branch" HeaderStyle-Width="15px" />
                                <asp:TemplateField FooterStyle-Font-Bold="True" HeaderStyle-HorizontalAlign="Left" HeaderText="Branch Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="20%">
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
                                <asp:BoundField DataField="DailySales" HeaderText="Today's sales" HeaderStyle-Width="15px" />               
                                 <asp:BoundField DataField="DailySalesforgp" HeaderText="NLC-Gp" HeaderStyle-Width="15px" />
                                 <asp:BoundField DataField="Todaysalesquantity" HeaderText="Today's salesQty" HeaderStyle-Width="15px" />
                                <asp:BoundField DataField="MonthlySales" HeaderText="Monthly sales" HeaderStyle-Width="15px" />
                                
                                 <asp:BoundField DataField="montlySalesforgp" HeaderText="NLC-GP" HeaderStyle-Width="15px" />
                                <asp:BoundField DataField="monthlysalesquantity" HeaderText="Monthly salesQty" HeaderStyle-Width="15px" />
                                <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="50px" HeaderText="View Detailed report" HeaderStyle-BorderColor="Gray"
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
                            OnRowDataBound="GridViewdp_RowDataBound">
                            <HeaderStyle Height="30px" HorizontalAlign="Center" Font-Bold="true" BackColor="#cccccc" BorderColor="Gray" Font-Size="15px" />
                            <RowStyle Font-Bold="true" HorizontalAlign="Center" Height="22px" Font-Size="15px" CssClass="GrdItemForecolor" ForeColor="#414141" />
                            <Columns>
                                <asp:BoundField DataField="BranchCode" HeaderText="Branch" HeaderStyle-Width="15px" />
                                <asp:TemplateField FooterStyle-Font-Bold="True" HeaderStyle-HorizontalAlign="Left" HeaderText="Branch Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="20%">
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
                                <asp:BoundField DataField="DailySales" HeaderText="Today's sales" HeaderStyle-Width="15px" />
                                
                                <asp:BoundField DataField="DailySalesforgp" HeaderText="DP-Gp" HeaderStyle-Width="15px" />
                                <asp:BoundField DataField="Todaysalesquantity" HeaderText="Today's salesQty" HeaderStyle-Width="15px" />
                                <asp:BoundField DataField="MonthlySales" HeaderText="Monthly sales" HeaderStyle-Width="15px" />
                                
                                <asp:BoundField DataField="montlySalesforgp" HeaderText="DP-GP" HeaderStyle-Width="15px" />
                                <asp:BoundField DataField="monthlysalesquantity" HeaderText="Monthly salesQty" HeaderStyle-Width="15px" />
                                <asp:TemplateField ItemStyle-CssClass="command"  HeaderStyle-Width="50px" HeaderText="View Detailed report" HeaderStyle-BorderColor="Gray"
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
                                    <td style="width: 50%; font-size: 17px; font-weight: 800; color: white;">Today's Purchases - Real-time Summary
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
                                         <asp:Button ID="Button1" runat="server" Text="Refresh" Visible="false" OnClick="Button1_Click"
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
                            OnRowDataBound="grdpurchase_RowDataBound">
                            <HeaderStyle Height="30px" HorizontalAlign="Center" Font-Bold="true" BackColor="#cccccc" BorderColor="Gray" Font-Size="15px" />
                            <RowStyle Font-Bold="true" HorizontalAlign="Center" Height="22px" Font-Size="15px" CssClass="GrdItemForecolor" ForeColor="#414141" />
                            <Columns>
                                <asp:BoundField DataField="BranchCode" HeaderText="Branch" HeaderStyle-Width="15px" />
                                <asp:TemplateField FooterStyle-Font-Bold="True" HeaderStyle-HorizontalAlign="Left" HeaderText="Branch Name" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="20%">
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
                                <asp:BoundField DataField="DailyPuchase" HeaderText="Today's purchase" HeaderStyle-Width="15px" />
                                <asp:BoundField DataField="TodayPuchasequantity" HeaderText="Today's purchaseQty" HeaderStyle-Width="15px" />
                                <asp:BoundField DataField="MonthlyPuchase" HeaderText="Monthly purchase" HeaderStyle-Width="15px" />
                                <asp:BoundField DataField="monthlyPuchasequantity" HeaderText="Monthly purchaseQty" HeaderStyle-Width="15px" />
                                <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="50px" HeaderText="View Detailed report" HeaderStyle-BorderColor="Gray"
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
