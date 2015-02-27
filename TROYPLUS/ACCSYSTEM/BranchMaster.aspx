<%@ Page Title="Inventory > PriceList" Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="BranchMaster.aspx.cs" Inherits="BranchMaster"  %>


<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <script language="javascript" type="text/javascript">
        function OpenWindow() {
            var ddLedger = document.getElementById('ctl00_cplhControlPanel_drpCustomer');
            var iLedger = ddLedger.options[ddLedger.selectedIndex].text;
            window.open('Service.aspx?ID=' + iLedger, '', "height=400, width=700,resizable=yes, toolbar =no");
            return false;
        }

        function CheckDate() {            
            alert('Please search for a product and edit the prices');
                   
        }

    </script>
    <asp:UpdatePanel ID="UpdatePanel16" runat="server" UpdateMode="Always">
        <ContentTemplate>
        
            <table style="width: 100%">
                <tr style="width: 100%">
                    <td style="width: 100%">
                    
                            <%--<div class="mainConHd">
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tr valign="middle">
                                        <td>
                                            <span>Service Visits</span>
                                        </td>
                                    </tr>
                                </table>
                            </div>--%>
                            <%--<table class="mainConHd" style="width: 994px;">
                                <tr valign="middle">
                                    <td style="font-size: 20px;">
                                        Cheque Book Information
                                    </td>
                                </tr>
                            </table>--%>
                            <div class="mainConBody">
                                <table style="width: 99.8%; margin: -2px 0px 0px 1px;" cellpadding="3" cellspacing="2" class="searchbg">
                                    <tr style="height: 25px; vertical-align: middle">
                                        <td style="width: 1%;"></td>
                                        <td style="width: 25%; font-size: 22px; color: White;" >
                                            Branch Master
                                        </td>
                                        <td style="width: 10%">
                                            
                                        </td>
                                        <td style="width: 8%; color: White;" align="right">
                                            Search
                                        </td>
                                        <td style="width: 18%" class="NewBox">
                                            <asp:TextBox ID="txtSearch" runat="server" SkinID="skinTxtBoxSearch"></asp:TextBox>
                                        </td>
                                        <td style="width: 18%" class="NewBox">
                                            <div style="width: 160px; font-family: 'Trebuchet MS';">
                                                <asp:DropDownList ID="ddCriteria" runat="server" Width="154px" BackColor="White" Height="23px" style="text-align:center;border:1px solid White ">
                                                    <asp:ListItem Value="BranchName">Branch Name</asp:ListItem>
                                                    <asp:ListItem Value="BranchCode">Branch Code</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </td>
                                        <td style="width: 17%" class="tblLeftNoPad">
                                            <asp:Button ID="btnSearch" runat="server" Text="" EnableTheming="false" CssClass="ButtonSearch6" OnClick="btnSearch_Click" />
                                        </td>
                                         <td style="width: 15%" class="tblLeftNoPad">
                                            <asp:Button ID="BtnClearFilter" runat="server"  OnClick="BtnClearFilter_Click"  EnableTheming="false" Text="" CssClass="ClearFilter6" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        
                        <input id="dummy" type="button" style="display: none" runat="server" />
                        <input id="Button1" type="button" style="display: none" runat="server" />
                        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                            CancelControlID="Button1" DynamicServicePath="" Enabled="True" PopupControlID="popUp"
                            TargetControlID="dummy">
                        </cc1:ModalPopupExtender>
                        <asp:Panel runat="server" ID="popUp" Style="width: 50%">
                            <div id="contentPopUp" class="divArea">
                                <table cellpadding="1" cellspacing="2" style="border: 1px solid blue;
                                    background-color: #fff; color: #000;" width="100%">
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlVisitDetails" runat="server" Visible="false">
                                                <div>
                                                    <table cellpadding="1" cellspacing="1" style="border: 0px solid blue;"
                                                        width="100%">
                                                        <tr>
                                                            <tr>
                                                                <td colspan="4">
                                                                    <table class="headerPopUp" width="100%">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Title1" runat="server">

                                                                                </asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            
                                                            <tr style="height:2px">
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 10%">
                                                                    
                                                                </td>
                                                                <td style="width: 35%" class="ControlLabelNew">
                                                                    Branch Code *
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtBranchcode"
                                                                        Display="Dynamic" ErrorMessage="Branch code is mandatory">*</asp:RequiredFieldValidator>
                                                                </td>
                                                                <td style="width: 30%;" class="ControlDrpBorder">
                                                                    <asp:TextBox ID="txtBranchcode" runat="server" CssClass="cssTextBox"
                                                                        TabIndex="2"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 30%" >
                                                                    
                                                                </td>
                                                            </tr>
                                                            <tr style="height:2px">
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 10%">
                                                                    
                                                                </td>
                                                                <td style="width: 35%" class="ControlLabelNew">
                                                                    Branch Name *
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtBranchName"
                                                                        Display="Dynamic" ErrorMessage="Branch Name is mandatory">*</asp:RequiredFieldValidator>
                                                                </td>
                                                                <td style="width: 25%;" class="ControlDrpBorder">
                                                                    <asp:TextBox ID="txtBranchName" runat="server" CssClass="cssTextBox"
                                                                        TabIndex="2"></asp:TextBox>       
                                                                        
                                                                </td>
                                                                <td style="width: 30%" >
                                                                    
                                                                </td>
                                                            </tr>
                                                            
                                                            <tr style="height:2px">
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 10%">
                                                                    
                                                                </td>
                                                                <td style="width: 35%" class="ControlLabelNew">
                                                                    Branch Address 1 *
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtBranchAddress1"
                                                                        Display="Dynamic" ErrorMessage="Branch Address is mandatory">*</asp:RequiredFieldValidator>
                                                                </td>
                                                                <td style="width: 25%;" class="ControlDrpBorder">
                                                                    <asp:TextBox ID="txtBranchAddress1" runat="server" CssClass="cssTextBox"
                                                                        TabIndex="2"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 30%" >
                                                                    
                                                                </td>
                                                            </tr>
                                                            <tr style="height:2px">
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 10%">
                                                                    
                                                                </td>
                                                                <td style="width: 35%" class="ControlLabelNew">
                                                                    Branch Address 2 *
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtBranchAddress2"
                                                                        Display="Dynamic" ErrorMessage="Branch Address is mandatory">*</asp:RequiredFieldValidator>
                                                                </td>
                                                                <td style="width: 25%;" class="ControlDrpBorder">
                                                                    <asp:TextBox ID="txtBranchAddress2" runat="server" CssClass="cssTextBox"
                                                                        TabIndex="2"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 30%" >
                                                                    
                                                                </td>
                                                            </tr>
                                                            <tr style="height:2px">
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 10%">
                                                                    
                                                                </td>
                                                                <td style="width: 35%" class="ControlLabelNew">
                                                                    Branch Address 3
                                                                </td>
                                                                <td style="width: 25%;" class="ControlDrpBorder">
                                                                    <asp:TextBox ID="txtBranchAddress3" runat="server" CssClass="cssTextBox"
                                                                        TabIndex="2"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 30%" >
                                                                    
                                                                </td>
                                                            </tr>
                                                            <tr style="height:2px">
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 10%">
                                                                    
                                                                </td>
                                                                <td style="width: 35%" class="ControlLabelNew">
                                                                    Branch Location *
                                                                    <asp:RequiredFieldValidator ID="rvAliasNameAdd" runat="server" ControlToValidate="txtBranchLocation"
                                                                        Display="Dynamic" ErrorMessage="Branch Location is mandatory">*</asp:RequiredFieldValidator>
                                                                </td>
                                                                <td style="width: 25%;" class="ControlDrpBorder">
                                                                    <asp:TextBox ID="txtBranchLocation" runat="server" CssClass="cssTextBox"
                                                                        TabIndex="2"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 30%" >
                                                                    
                                                                </td>
                                                            </tr>
                                                            <tr style="height:2px">
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 10%">
                                                                    
                                                                </td>
                                                                <td style="width: 35%" class="ControlLabelNew">
                                                                    Is Active *
                                                                   <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtBranchAddress"
                                                                        Display="Dynamic" ErrorMessage="Branch Address is mandatory">*</asp:RequiredFieldValidator>--%>
                                                                </td>
                                                                <td style="width: 25%;" class="ControlDrpBorder">
                                                                    <asp:DropDownList ID="drpIsActive" TabIndex="10" AutoPostBack="false" runat="server" BackColor="#e7e7e7" CssClass="drpDownListMedium"
                                                                        Width="100%" Style="border: 1px solid #e7e7e7" Height="26px">
                                                                        <asp:ListItem Text="YES" Value="YES" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Text="NO" Value="NO"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="width: 30%" >
                                                                    
                                                                </td>
                                                            </tr>
                                                            <tr style="height:10px">
                                                            </tr>
                                                            <tr>
                                                                <td align="center" style="width: 100%" colspan="4">
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td style="width: 30%">
                                                                            </td>
                                                                            <td align="center" style="width: 18%">   
                                                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                                                                                    <ContentTemplate>                                                                            
                                                                                        <asp:Button ID="SaveButton" runat="server" CommandName="Update" SkinID="skinBtnSave"
                                                                                            CssClass="savebutton1231" EnableTheming="false" OnClick="SaveButton_Click"></asp:Button>
                                                                                        <asp:Button ID="UpdateButton" runat="server" CommandName="Update" SkinID="skinBtnSave"
                                                                                            CssClass="Updatebutton1231" EnableTheming="false" OnClick="UpdateButton_Click"></asp:Button>
                                                                                    </ContentTemplate>    
                                                                                    
                                                                                </asp:UpdatePanel>
                                                                            </td>
                                                                            <td align="center" style="width: 18%">
                                                                                 <asp:Button ID="CancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                    CssClass="cancelbutton6" EnableTheming="false" SkinID="skinBtnCancel" OnClick="UpdateCancelButton_Click">
                                                                                </asp:Button>
                                                                            </td>
                                                                            <td style="width: 30%">
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 10%">
                                                                    <asp:ObjectDataSource ID="srcCreditorDebitor" runat="server" SelectMethod="ListCreditorDebitorSuppliers"
                                                                        TypeName="BusinessLogic">
                                                                        <SelectParameters>
                                                                            <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                        </SelectParameters>
                                                                    </asp:ObjectDataSource>
                                                                </td>
                                                                <td style="width: 25%">
                                                                </td>
                                                            </tr>
                                                    </table>
                                                </div>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                    </td>
                </tr>
                <tr style="width: 100%">
                    <td style="width: 100%;">
                    <table width="100%" style="margin: -4px 0px 0px 0px;">
                                <tr style="width: 100%">
                                    <td>
                        <asp:HiddenField ID="hdVisitID" runat="server" Value="0" />
                        <div class="mainGridHold" id="searchGrid">
                            <asp:GridView ID="GrdViewSerVisit" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                OnRowCreated="GrdViewSerVisit_RowCreated" Width="100.4%" DataSourceID="GridSource" CssClass="someClass"
                                AllowPaging="True" DataKeyNames="BranchId" EmptyDataText="No Branch found!"  OnPageIndexChanging="GrdViewSerVisit_PageIndexChanging"
                                OnRowCommand="GrdViewSerVisit_RowCommand" OnRowDataBound="GrdViewSerVisit_RowDataBound"
                                OnSelectedIndexChanged="GrdViewSerVisit_SelectedIndexChanged" OnRowDeleting="GrdViewSerVisit_RowDeleting"
                                OnRowDeleted="GrdViewSerVisit_RowDeleted">
                                <EmptyDataRowStyle CssClass="GrdContent" />
                                <HeaderStyle Height="30px" HorizontalAlign="Center" Font-Bold="true" BackColor="#cccccc" BorderColor="Gray" Font-Size="Small"/>
                                <RowStyle Font-Bold="true" HorizontalAlign="Center" Height="30px" Font-Size="Small" ForeColor="#0567AE"/>
                                <Columns>
                                    <asp:BoundField DataField="Row" HeaderText="#" HeaderStyle-Width="60px"/>
                                    <asp:BoundField DataField="Branchcode" HeaderText="Branch Code" ItemStyle-HorizontalAlign="Left"  HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false"/>
                                    <asp:BoundField DataField="BranchName" HeaderText="Branch Name" ItemStyle-HorizontalAlign="Left"  HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false"/>
                                    <asp:BoundField DataField="Branchaddress1" HeaderText="Branch Address1" ItemStyle-HorizontalAlign="Left"  HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false"/>
                                    <%--<asp:BoundField DataField="Branchaddress1" HeaderText="Branch Address" ItemStyle-HorizontalAlign="Left"  HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false"/>
                                    <asp:BoundField DataField="Branchaddress1" HeaderText="Branch Address" ItemStyle-HorizontalAlign="Left"  HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false"/>--%>
                                    <asp:BoundField DataField="BranchLocation" HeaderText="Branch Location" ItemStyle-HorizontalAlign="Left"  HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false"/>
                                    <asp:BoundField DataField="IsActive" HeaderText="IsActive" ItemStyle-HorizontalAlign="Left"  HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false"/>
                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="120px" HeaderText="Edit" HeaderStyle-BorderColor="Gray">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" SkinID="edit"
                                                CommandName="Select" />
                                            <asp:ImageButton ID="btnEditDisabled" Enabled="false" SkinID="editDisable" runat="Server">
                                            </asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField ItemStyle-CssClass="command" HeaderText="Delete" HeaderStyle-Width="50px" HeaderStyle-BorderColor="Gray"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to Delete this Price List Details?"
                                                runat="server">
                                            </cc1:ConfirmButtonExtender>
                                            <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" CommandName="Delete"></asp:ImageButton>
                                            <asp:ImageButton ID="lnkBDisabled" Enabled="false" SkinID="deleteDisable" runat="Server">
                                            </asp:ImageButton>
                                            <asp:HiddenField ID="ldgID" runat="server" Value='<%# Bind("ID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                </Columns>
                                <EmptyDataRowStyle CssClass="GrdContent" />
                                <PagerTemplate>
                                    <table style=" border-color:white">
                                        <tr style=" border-color:white">
                                            <td style=" border-color:white">
                                                Goto Page
                                            </td>
                                            <td style="border-color:white">
                                                <asp:DropDownList ID="ddlPageSelector" runat="server" Width="65px" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSelector_SelectedIndexChanged" style="border:1px solid Gray" BackColor="#e7e7e7">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="border-color:white;Width:5px">
                                            
                                            </td>
                                            <td style="border-color:white">
                                                <asp:Button Text="" CommandName="Page" CommandArgument="First" runat="server" CssClass="NewFirst" EnableTheming="false" Width="22px" Height="18px"
                                                    ID="btnFirst" />
                                            </td>
                                            <td style="border-color:white">
                                                <asp:Button Text="" CommandName="Page" CommandArgument="Prev" runat="server" CssClass="NewPrev" EnableTheming="false" Width="22px" Height="18px"
                                                    ID="btnPrevious" />
                                            </td>
                                            <td style="border-color:white">
                                                <asp:Button Text="" CommandName="Page" CommandArgument="Next" runat="server" CssClass="NewNext" EnableTheming="false" Width="22px" Height="18px"
                                                    ID="btnNext" />
                                            </td>
                                            <td style="border-color:white">
                                                <asp:Button Text="" CommandName="Page" CommandArgument="Last" runat="server" CssClass="NewLast" EnableTheming="false" Width="22px" Height="18px"
                                                    ID="btnLast" />
                                            </td>
                                        </tr>
                                    </table>
                                </PagerTemplate>
                            </asp:GridView>
                        </div>
                        </td>
                        </tr>
                        </table>
                    </td>
                </tr>
                <tr style="width:100%">
                    <td style="width: 918px" align="left">
                        <asp:ObjectDataSource ID="srcDebitors" runat="server" SelectMethod="ListSundryDebtors"
                            TypeName="BusinessLogic">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="hdDataSource" Name="connection" Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="GridSource" runat="server" SelectMethod="ListBranchInfo"
                            TypeName="BusinessLogic" DeleteMethod="DeleteBranch" OnDeleting="GridSource_Deleting">
                            <DeleteParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                <asp:Parameter Name="BranchId" Type="Int32" />
                                <asp:Parameter Name="Username" Type="String" />
                            </DeleteParameters>
                        </asp:ObjectDataSource>
                        <asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="true" ShowSummary="false" />
                        <asp:HiddenField ID="hdDataSource" runat="server" />
                        <asp:HiddenField ID="hdServiceID" runat="server" Value="" />
                        <asp:HiddenField ID="hdCustomerID" runat="server" Value="0" />
                        <asp:HiddenField ID="hdDueDate" runat="server" Value="" />
                        <asp:HiddenField ID="hdRefNumber" runat="server" Value="" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div>
        <table style="width: 100%">
            <tr>
                <td  style="width:19%">
                    </td>
                    <td  style="width:25%">
                        <%--<asp:Button ID="Button2" runat="server"  OnClientClick="window.open('BulkPriceAddition.aspx','billSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=250,width=500,left=425,top=220, scrollbars=yes');" 
                                    EnableTheming="false" Text="Import New Prices using Excel" Visible="false"></asp:Button>--%>
                    </td>
                    <td style="width:20%">
                        <div style="text-align: right;">
                                                <asp:Panel ID="pnlSearch" runat="server" Width="100px">
                                                    <asp:Button ID="lnkBtnAdd" runat="server" OnClick="lnkBtnAdd_Click" Text="Add New"
                                                        EnableTheming="false" ></asp:Button>
                                                </asp:Panel>
                                            </div>
                        <%--<asp:Button ID="BlkAdd" runat="server"  OnClientClick="window.open('BulkPriceUpdation.aspx','billSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=250,width=500,left=425,top=220, scrollbars=yes');"
                                    EnableTheming="false" Text="Import Existing Prices using Excel" Visible="false"></asp:Button>--%>
                    </td>
                    <td  style="width:15%">
                        <%--<asp:Button ID="Button3" runat="server" OnClick="Button3_Click" OnClientClick="javascript:CheckDate();"
                                    EnableTheming="false" Text="Update Price for Single Product"></asp:Button>--%>
                    </td>
                    <td  style="width:15%">
                        <%--<asp:Button ID="Button5" runat="server" OnClick="Button5_Click"
                                    EnableTheming="false" Text="Download sample Excel sheet for importing Prices"></asp:Button>--%>
                    </td> 
                 <td  style="width:20%">
                    </td>                   
                </tr>
        </table>
    </div>

</asp:Content>
