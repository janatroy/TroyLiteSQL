<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="MultipleEntry.aspx.cs" Inherits="MultipleEntry" Title="Accounts > Multiple Journal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <script language="javascript" type="text/javascript">

        function CheckDate() {
            if (document.getElementById('ctl00_cplhControlPanel_frmViewAdd_hddatecheck').value != '0') {
                var rv = confirm("Date in Future, Do you still want to continue?");

                if (rv == true) {
                    return true;
                }
                else {
                    return window.event.returnValue = false;
                }
            }                     
        }

        function CheckDateT() {
            if (document.getElementById('ctl00_cplhControlPanel_frmViewAdd_hddatecheck1').value != '0') {
                var rv = confirm("Date in Future, Do you still want to continue?");

                if (rv == true) {
                    return true;
                }
                else {
                    return window.event.returnValue = false;
                }
            }
        }

        function PrintItem(ID) {
            window.showModalDialog('PrintJournal.aspx?ID=' + ID, self, 'dialogWidth:700px;dialogHeight:430px;status:no;dialogHide:yes;unadorned:yes;');
        }

    </script>

    <asp:UpdatePanel ID="UpdatePanel16" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        
            <table style="width: 100%">
                <tr style="width: 100%">
                    <td style="width: 100%">
                                <div class="mainConHd">
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tr valign="middle">
                                        <td>
                                            <span>Multiple Journals</span>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="mainConBody">
                                <%-- class="mainConBody">--%>
                                <table style="width: 100%;" class="searchbg">
                                    <tr style="height: 25px; vertical-align: middle">
                                        <td style="width: 7%">
                                            
                                        </td>
                                        <td style="width: 15%">
                                            <div style="text-align: right;">
                                                <asp:Panel ID="pnlSearch" runat="server" Width="100px">
                                                    <asp:Button ID="lnkBtnAdd" runat="server" OnClick="lnkBtnAdd_Click" CssClass="ButtonAdd"
                                                        EnableTheming="false" Width="80px" Text="Add New"></asp:Button>
                                                </asp:Panel>
                                            </div>
                                        </td>
                                        <td align="right" style="width: 10%">
                                            Trans. No.
                                            <cc1:FilteredTextBoxExtender ID="fttrans" runat="server" TargetControlID="txtTransno"
                                                FilterType="Numbers" />
                                        </td>
                                        <td class="cssTextBoxbg" style="width: 20%">
                                            <asp:TextBox ID="txtTransno" runat="server" Width="92%" CssClass="cssTextBox"></asp:TextBox>
                                        </td>
                                        <td align="right" style="width: 8%">
                                            Ref. No.
                                            <cc1:FilteredTextBoxExtender ID="ftbe" runat="server" TargetControlID="txtRefno"
                                                FilterType="Numbers" />
                                        </td>
                                        <td class="cssTextBoxbg" style="width: 23%">
                                            <asp:TextBox ID="txtRefno" runat="server" Width="92%" CssClass="cssTextBox"></asp:TextBox>
                                        </td>
                                        
                                        <td class="tblLeftNoPad" style="width: 20%">
                                            <asp:Button ID="cmdSearch" runat="server" OnClick="cmdSearch_Click" Text="Search"
                                                SkinID="skinBtnSearch" />
                                        </td>
                                    </tr>
                                </table>
                                <%--<table style="width: 100%;" class="searchbg">
                                    <tr style="height: 25px; vertical-align: middle">
                                        <td style="width: 22%">
                                        </td>
                                        
                                        <%--<td>
                                        </td>--%>
                                    <%--</tr>
                                </table>--%>
                            </div> 
                            <div class="mainConBody">
                                <table style="width: 100%;" cellpadding="3" cellspacing="2" class="searchbg">
                                    <tr style="height: 20px; vertical-align: middle">
                                        <td style="width: 22%">
                                            
                                        </td>
                                        <td align="right" style="width: 10%">
                                            Ledger
                                        </td>
                                        <td class="cssTextBoxbg" style="width: 20%">
                                            <asp:TextBox ID="txtLedger" runat="server" Width="92%" CssClass="cssTextBox"></asp:TextBox>
                                        </td>
                                        <td align="right" style="width: 8%">
                                            Date
                                        </td>
                                        <td class="cssTextBoxbg" style="width: 23%">
                                            <asp:TextBox ID="txtDate" runat="server" Width="92%" MaxLength="10" CssClass="cssTextBox"></asp:TextBox>
                                            <cc1:CalendarExtender ID="calExtender3" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                PopupButtonID="bt3" PopupPosition="BottomLeft" TargetControlID="txtDate">
                                            </cc1:CalendarExtender>
                                            <asp:ImageButton ID="bt3" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                Width="20px" runat="server" />
                                        </td>
                                        <td class="tblLeftNoPad" style="width: 20%">
                                            
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
            <asp:Panel runat="server" ID="popUp" Style="width: 80%">
                <div id="contentPopUp">
                    <table class="tblLeft" cellpadding="0" cellspacing="0" style="border: 0px solid #5078B3;
                        background-color: #fff; color: #000;" width="100%">
                        <tr>
                            <td>
                                <asp:FormView ID="frmViewAdd" runat="server" Width="100%" DataSourceID="frmSource"
                                    DefaultMode="Edit" DataKeyNames="TransNo" Visible="False" OnItemUpdated="frmViewAdd_ItemUpdated"
                                    OnItemCreated="frmViewAdd_ItemCreated" OnItemInserted="frmViewAdd_ItemInserted">
                                    <RowStyle HorizontalAlign="left" CssClass="GrdContent allPad" VerticalAlign="Middle"
                                        BorderColor="#cccccc" Height="20px" />
                                    <EditRowStyle HorizontalAlign="left" CssClass="GrdAlternateColor allPad" BorderColor="#cccccc"
                                        VerticalAlign="middle" Height="20px" />
                                    <HeaderStyle HorizontalAlign="left" CssClass="GrdHeaderbgClr GrdHdrContent allPad"
                                        Height="25px" BorderColor="#cccccc" VerticalAlign="Middle" />
                                    <EditItemTemplate>
                                        <div class="divArea">
                                            <table cellpadding="3" cellspacing="3" style="border: 1px solid #86b2d1; width: 100%;">
                                                <tr>
                                                    <td colspan="5" class="headerPopUp">
                                                        Multiple Journal Details
                                                    </td>
                                                </tr>
                                                <tr style="height:5px">
                                                </tr>
                                                <tr>
                                                    
                                                    <td class="ControlLabel" style="width:20%">
                                                        Ref. No. *
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRefnum"
                                                            ValidationGroup="editVal" Display="Dynamic" EnableClientScript="true" ErrorMessage="Ref. No. is mandatory">*</asp:RequiredFieldValidator>
                                                    </td>
                                                    <td class="ControlTextBox3" style="width:27%">
                                                        <asp:TextBox ID="txtRefnum" ValidationGroup="editVal" runat="server" Text='<%# Bind("RefNo") %>'
                                                            Width="95%" CssClass="cssTextBox"></asp:TextBox>
                                                    </td>
                                                    <td class="ControlLabel" style="width:15%">
                                                        Date *
                                                        <asp:RequiredFieldValidator ID="rvStock" runat="server" ValidationGroup="editVal"
                                                            ControlToValidate="txtTransDate" Display="Dynamic" EnableClientScript="true"
                                                            ErrorMessage="Trasaction Date is mandatory">*</asp:RequiredFieldValidator>
                                                        <%--<asp:RangeValidator ID="myRangeValidator" runat="server" ControlToValidate="txtTransDate"
                                                            Display="Dynamic" EnableClientScript="true" ErrorMessage="Journal date cannot be future date."
                                                            ValidationGroup="editVal" Text="*" Type="Date"></asp:RangeValidator>--%>
                                                    </td>
                                                    <td class="ControlNumberBox3" style="width:27%">
                                                        
                                                                <asp:TextBox ID="txtTransDate" runat="server" ValidationGroup="editVal" Text='<%# Bind("TransDate","{0:dd/MM/yyyy}") %>'
                                                                    MaxLength="10" CssClass="cssTextBox"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="calExt3" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                    PopupButtonID="btnD3" PopupPosition="BottomLeft" TargetControlID="txtTransDate">
                                                                </cc1:CalendarExtender>
                                                          
                                                   </td>
                                                    <td align="left">
                                                        <asp:ImageButton ID="btnD3" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                            Width="20px" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr style="height:3px">
                                                </tr>
                                                <tr>
                                                    <td class="ControlLabel" style="width:20%">
                                                        Debtor 
                                                     </td>  
                                                     <td class="ControlLabel" style="width:20%">
                                                        Amount
                                                     </td>  
                                                     <td class="ControlLabel" style="width:20%">
                                                        
                                                     </td>  
                                                     <td class="ControlLabel" style="width:20%">
                                                        Creditor
                                                     </td>  
                                                     <td class="ControlLabel" style="width:20%">
                                                        Amount
                                                     </td>   
                                                </tr>
                                                <tr>
                                                    
                                                    <td align="left" class="ControlDrpBorder" style="width:27%">
                                                        <asp:DropDownList ID="cmbDebtor1" runat="server"  BackColor = "#90c9fc" CssClass="drpDownListMedium" Width="100%" AutoPostBack="False"
                                                            DataSourceID="srcCreditorDebitor" DataValueField="LedgerID" DataTextField="LedgerName" style="border: 1px solid #90c9fc" height="26px"
                                                            AppendDataBoundItems="true" ValidationGroup="editVal" OnDataBound="ComboBox2_DataBound"
                                                            >
                                                            <asp:ListItem Text="Select Debtor" style="background-color: #90c9fc" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="ControlTextBox3" style="width:27%">
                                                        <asp:TextBox ID="txtAmount1" runat="server" ValidationGroup="editVal" Text='<%# Bind("Amount") %>'
                                                            Width="95%" CssClass="cssTextBox"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    
                                                    <td align="left" class="ControlDrpBorder" style="width:27%">
                                                        <asp:DropDownList ID="cmbCreditor1" runat="server"  BackColor = "#90c9fc" CssClass="drpDownListMedium" Width="100%" AutoPostBack="False"
                                                            DataSourceID="srcCreditorDebitor" DataValueField="LedgerID" DataTextField="LedgerName" style="border: 1px solid #90c9fc" height="26px"
                                                            AppendDataBoundItems="true" ValidationGroup="editVal" OnDataBound="cmbCreditor_DataBound">
                                                            <asp:ListItem Text="Select Creditor" style="background-color: #90c9fc" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="ControlTextBox3" style="width:27%">
                                                        <asp:TextBox ID="txtAmount2" runat="server" ValidationGroup="editVal" Text='<%# Bind("Amount") %>'
                                                            Width="95%" CssClass="cssTextBox"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr style="height:3px">
                                                </tr>
                                                <tr>
                                                    
                                                    <td align="left" class="ControlDrpBorder" style="width:27%">
                                                        <asp:DropDownList ID="cmbDebtor2" runat="server"  BackColor = "#90c9fc" CssClass="drpDownListMedium" Width="100%" AutoPostBack="False"
                                                            DataSourceID="srcCreditorDebitor" DataValueField="LedgerID" DataTextField="LedgerName" style="border: 1px solid #90c9fc" height="26px"
                                                            AppendDataBoundItems="true" ValidationGroup="editVal" OnDataBound="ComboBox2_DataBound"
                                                            >
                                                            <asp:ListItem Text="Select Debtor" style="background-color: #90c9fc" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="ControlTextBox3" style="width:27%">
                                                        <asp:TextBox ID="TextBox1" runat="server" ValidationGroup="editVal" Text='<%# Bind("Amount") %>'
                                                            Width="95%" CssClass="cssTextBox"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        
                                                       
                                                    </td>
                                                    <td align="left" class="ControlDrpBorder" style="width:27%">
                                                        <asp:DropDownList ID="cmbCreditor2" runat="server"  BackColor = "#90c9fc" CssClass="drpDownListMedium" Width="100%" AutoPostBack="False"
                                                            DataSourceID="srcCreditorDebitor" DataValueField="LedgerID" DataTextField="LedgerName" style="border: 1px solid #90c9fc" height="26px"
                                                            AppendDataBoundItems="true" ValidationGroup="editVal" OnDataBound="cmbCreditor_DataBound">
                                                            <asp:ListItem Text="Select Creditor" style="background-color: #90c9fc" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr style="height:3px">
                                                </tr>
                                                <tr>
                                                    <td class="ControlLabel" style="width:20%">
                                                        Debtor 
                                                        
                                                    </td>
                                                    <td align="left" class="ControlDrpBorder" style="width:27%">
                                                        <asp:DropDownList ID="cmbDebtor3" runat="server"  BackColor = "#90c9fc" CssClass="drpDownListMedium" Width="100%" AutoPostBack="False"
                                                            DataSourceID="srcCreditorDebitor" DataValueField="LedgerID" DataTextField="LedgerName" style="border: 1px solid #90c9fc" height="26px"
                                                            AppendDataBoundItems="true" ValidationGroup="editVal" OnDataBound="ComboBox2_DataBound"
                                                            >
                                                            <asp:ListItem Text="Select Debtor" style="background-color: #90c9fc" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="ControlLabel" style="width:15%">
                                                        Creditor 
                                                        
                                                    </td>
                                                    <td align="left" class="ControlDrpBorder" style="width:27%">
                                                        <asp:DropDownList ID="cmbCreditor3" runat="server"  BackColor = "#90c9fc" CssClass="drpDownListMedium" Width="100%" AutoPostBack="False"
                                                            DataSourceID="srcCreditorDebitor" DataValueField="LedgerID" DataTextField="LedgerName" style="border: 1px solid #90c9fc" height="26px"
                                                            AppendDataBoundItems="true" ValidationGroup="editVal" OnDataBound="cmbCreditor_DataBound">
                                                            <asp:ListItem Text="Select Creditor" style="background-color: #90c9fc" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr style="height:3px">
                                                </tr>
                                                <tr>
                                                    <td class="ControlLabel" style="width:20%">
                                                        Debtor 
                                                        
                                                    </td>
                                                    <td align="left" class="ControlDrpBorder" style="width:27%">
                                                        <asp:DropDownList ID="cmbDebtor4" runat="server"  BackColor = "#90c9fc" CssClass="drpDownListMedium" Width="100%" AutoPostBack="False"
                                                            DataSourceID="srcCreditorDebitor" DataValueField="LedgerID" DataTextField="LedgerName" style="border: 1px solid #90c9fc" height="26px"
                                                            AppendDataBoundItems="true" ValidationGroup="editVal" OnDataBound="ComboBox2_DataBound"
                                                            >
                                                            <asp:ListItem Text="Select Debtor" style="background-color: #90c9fc" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="ControlLabel" style="width:15%">
                                                        Creditor 
                                                        
                                                    </td>
                                                    <td align="left" class="ControlDrpBorder" style="width:27%">
                                                        <asp:DropDownList ID="cmbCreditor4" runat="server"  BackColor = "#90c9fc" CssClass="drpDownListMedium" Width="100%" AutoPostBack="False"
                                                            DataSourceID="srcCreditorDebitor" DataValueField="LedgerID" DataTextField="LedgerName" style="border: 1px solid #90c9fc" height="26px"
                                                            AppendDataBoundItems="true" ValidationGroup="editVal" OnDataBound="cmbCreditor_DataBound">
                                                            <asp:ListItem Text="Select Creditor" style="background-color: #90c9fc" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr style="height:3px">
                                                </tr>
                                                <tr>
                                                    <td class="ControlLabel" style="width:20%">
                                                        Debtor 
                                                        
                                                    </td>
                                                    <td align="left" class="ControlDrpBorder" style="width:27%">
                                                        <asp:DropDownList ID="cmbDebtor5" runat="server"  BackColor = "#90c9fc" CssClass="drpDownListMedium" Width="100%" AutoPostBack="False"
                                                            DataSourceID="srcCreditorDebitor" DataValueField="LedgerID" DataTextField="LedgerName" style="border: 1px solid #90c9fc" height="26px"
                                                            AppendDataBoundItems="true" ValidationGroup="editVal" OnDataBound="ComboBox2_DataBound"
                                                            >
                                                            <asp:ListItem Text="Select Debtor" style="background-color: #90c9fc" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="ControlLabel" style="width:15%">
                                                        Creditor 
                                                        
                                                    </td>
                                                    <td align="left" class="ControlDrpBorder" style="width:27%">
                                                        <asp:DropDownList ID="cmbCreditor5" runat="server"  BackColor = "#90c9fc" CssClass="drpDownListMedium" Width="100%" AutoPostBack="False"
                                                            DataSourceID="srcCreditorDebitor" DataValueField="LedgerID" DataTextField="LedgerName" style="border: 1px solid #90c9fc" height="26px"
                                                            AppendDataBoundItems="true" ValidationGroup="editVal" OnDataBound="cmbCreditor_DataBound">
                                                            <asp:ListItem Text="Select Creditor" style="background-color: #90c9fc" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr style="height:3px">
                                                </tr>
                                                <tr>
                                                    <td class="ControlLabel" style="width:20%">
                                                        Debtor 
                                                        
                                                    </td>
                                                    <td align="left" class="ControlDrpBorder" style="width:27%">
                                                        <asp:DropDownList ID="cmbDebtor6" runat="server"  BackColor = "#90c9fc" CssClass="drpDownListMedium" Width="100%" AutoPostBack="False"
                                                            DataSourceID="srcCreditorDebitor" DataValueField="LedgerID" DataTextField="LedgerName" style="border: 1px solid #90c9fc" height="26px"
                                                            AppendDataBoundItems="true" ValidationGroup="editVal" OnDataBound="ComboBox2_DataBound"
                                                            >
                                                            <asp:ListItem Text="Select Debtor" style="background-color: #90c9fc" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="ControlLabel" style="width:15%">
                                                        Creditor 
                                                        
                                                    </td>
                                                    <td align="left" class="ControlDrpBorder" style="width:27%">
                                                        <asp:DropDownList ID="cmbCreditor6" runat="server"  BackColor = "#90c9fc" CssClass="drpDownListMedium" Width="100%" AutoPostBack="False"
                                                            DataSourceID="srcCreditorDebitor" DataValueField="LedgerID" DataTextField="LedgerName" style="border: 1px solid #90c9fc" height="26px"
                                                            AppendDataBoundItems="true" ValidationGroup="editVal" OnDataBound="cmbCreditor_DataBound">
                                                            <asp:ListItem Text="Select Creditor" style="background-color: #90c9fc" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr style="height:3px">
                                                </tr>
                                                <tr>
                                                    <td class="ControlLabel" style="width:20%">
                                                        Amount *
                                                        <asp:RequiredFieldValidator ID="rvModel" runat="server" ControlToValidate="txtAmount"
                                                            Display="Dynamic" EnableClientScript="true" ValidationGroup="editVal" ErrorMessage="Amount is mandatory">*</asp:RequiredFieldValidator>
                                                        <cc1:FilteredTextBoxExtender ID="fltAmt" runat="server" TargetControlID="txtAmount"
                                                            ValidChars="." FilterType="Numbers, Custom" />
                                                    </td>
                                                    <td class="ControlTextBox3" style="width:27%">
                                                        <asp:TextBox ID="txtAmount" runat="server" ValidationGroup="editVal" Text='<%# Bind("Amount") %>'
                                                            Width="95%" CssClass="cssTextBox"></asp:TextBox>
                                                    </td>
                                                    <td class="ControlLabel" style="width:15%">
                                                        Narration *
                                                        <asp:RequiredFieldValidator ID="rvNarration" runat="server" ControlToValidate="txtNarr"
                                                            ValidationGroup="editVal" Display="Dynamic" EnableClientScript="true" ErrorMessage="Narration is mandatory">*</asp:RequiredFieldValidator>
                                                    </td>
                                                    <td class="ControlTextBox3" style="width:27%">
                                                        <asp:TextBox ID="txtNarr" ValidationGroup="editVal" runat="server" Text='<%# Bind("Narration") %>'
                                                            MaxLength="200" TextMode="MultiLine" Height="30px" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr style="height:7px">
                                                                                                    
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                   
                                                    <td align="right">
                                                        <asp:Button ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                            CssClass="cancelbutton" EnableTheming="false" SkinID="skinBtnCancel" OnClick="UpdateCancelButton_Click">
                                                        </asp:Button>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" OnClientClick="javascript:CheckDateT();"
                                                            CssClass="savebutton" EnableTheming="false" ValidationGroup="editVal" SkinID="skinBtnSave"
                                                            OnClick="UpdateButton_Click"></asp:Button>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                 </tr>
                                                 <tr style="height:5px">
                                                                                                    
                                                </tr>
                                            </table>
                                        </div>
                                        <table cellspacing="0">
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:ValidationSummary ID="valSum" DisplayMode="BulletList" ShowMessageBox="true"
                                                        ShowSummary="false" HeaderText="Validation Messages" ValidationGroup="editVal"
                                                        Font-Names="'Trebuchet MS'" Font-Size="12" runat="server" />
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <div style="padding: 5px 5px 5px 5px;">
                                            <table cellpadding="3" cellspacing="2" style="border: 1px solid #86b2d1; width: 100%;">
                                                <tr>
                                                    <td colspan="12" class="headerPopUp">
                                                        Multiple Journal Details
                                                    </td>
                                                </tr>
                                                <tr style="height:7px">
                                                </tr>
                                                
                                                <tr style="height:15px">
                                                </tr>
                                                <tr>
                                                    <td colspan="12" style="width:100%">
                                                        <table width="100%" cellpadding="3" cellspacing="3">
                                                            <tr>
                                                                <td style="width:1%">
                                                                    
                                                                </td>
                                                                <td align="center" style="width:10%">
                                                                    
                                                                </td>
                                                                <td style="width:3%">
                                                                    
                                                                </td>
                                                                <td align="center"  style="width:10%">
                                                                    
                                                                </td>
                                                                <td style="width:3%">
                                                                    
                                                                </td>
                                                                <td align="center" style="width:20%">
                                                                    
                                                                </td>
                                                                <td style="width:2%">
                                                                    
                                                                </td>
                                                                <td  align="center" style="width:20%">
                                                                    
                                                                </td>
                                                                <td style="width:2%">
                                                                    
                                                                </td>
                                                                <td align="center" class="ControlLabel" style="width:10%">
                                                                     No Of Entries *
                                                                </td>
                                                                <td style="width:2%">
                                                                    
                                                                </td>
                                                                <td class="ControlTextBox3" style="width:15%">
                                                                     <asp:TextBox ID="txtnum" runat="server"
                                                                        SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td style="width:1%">
                                                                    
                                                                </td>
                                                                <td align="center" style="width:10%">
                                                                    Ref. No. *
                                                                </td>
                                                                <td style="width:3%">
                                                                    
                                                                </td>
                                                                <td align="center"  style="width:10%">
                                                                    Date *
                                                                </td>
                                                                <td style="width:3%">
                                                                    
                                                                </td>
                                                                <td align="center" style="width:20%">
                                                                    Debtor  
                                                                </td>
                                                                <td style="width:2%">
                                                                    
                                                                </td>
                                                                <td  align="center" style="width:20%">
                                                                    Creditor
                                                                </td>
                                                                <td style="width:2%">
                                                                    
                                                                </td>
                                                                <td align="center" style="width:10%">
                                                                     Amount
                                                                </td>
                                                                <td style="width:2%">
                                                                    
                                                                </td>
                                                                <td align="center" style="width:15%">
                                                                     Narration
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width:1%">
                                                                    
                                                                </td>
                                                                <td  class="ControlTextBox3" style="width:10%">
                                                                    <asp:TextBox ID="txtRefnumAdd1" ValidationGroup="addVal" runat="server" Text='<%# Bind("RefNo1") %>'
                                                                        SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                </td>
                                                                <td style="width:3%">
                                                                     
                                                                </td>
                                                                <td  class="ControlTextBox3" style="width:10%">
                                                                    <asp:TextBox ID="txtTransDateAdd1" CssClass="cssTextBox" runat="server" ValidationGroup="editVal"
                                                                        Text='<%# Bind("TransDate1","{0:dd/MM/yyyy}") %>' Width="100px" MaxLength="10"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="calExtender32" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                        PopupButtonID="btnDate3" PopupPosition="BottomLeft" TargetControlID="txtTransDateAdd1">
                                                                    </cc1:CalendarExtender>
                                                                </td>
                                                                <td style="width:3%">
                                                                    <asp:ImageButton ID="btnDate3" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                        Width="20px" runat="server" />
                                                                </td>
                                                                <td class="ControlDrpBorder" style="width:15%">
                                                                    <asp:DropDownList ID="cmbDebtorAdd1" runat="server" BackColor = "#90c9fc" CssClass="drpDownListMedium" Width="100%" AutoPostBack="False"
                                                                        DataSourceID="srcCreditorDebitorAdd" DataValueField="LedgerID" DataTextField="LedgerName" style="border: 1px solid #90c9fc" height="26px"
                                                                        AppendDataBoundItems="true" ValidationGroup="addVal" OnDataBound="ComboBox2_DataBound">
                                                                        <asp:ListItem Text="Select Debtor" style="background-color: #90c9fc" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="width:2%">
                                                                    
                                                                </td>
                                                                <td class="ControlDrpBorder" style="width:15%">
                                                                    <asp:DropDownList ID="cmbCreditorAdd1" runat="server" BackColor = "#90c9fc" CssClass="drpDownListMedium" Width="100%" AutoPostBack="False"
                                                                        DataSourceID="srcCreditorDebitorAdd" DataValueField="LedgerID" DataTextField="LedgerName" style="border: 1px solid #90c9fc" height="26px"
                                                                        AppendDataBoundItems="true" ValidationGroup="addVal" OnDataBound="cmbCreditorAdd_DataBound">
                                                                        <asp:ListItem Text="Select Creditor" style="background-color: #90c9fc" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="width:2%">
                                                                    
                                                                </td>
                                                                <td class="ControlTextBox3" style="width:10%">
                                                                    <asp:TextBox ID="txtAmountAdd1" runat="server" ValidationGroup="addVal" Text='<%# Bind("Amount1") %>'
                                                                        Width="95%" CssClass="cssTextBox"></asp:TextBox>
                                                                </td>
                                                                <td style="width:2%">
                                                                     
                                                                </td>
                                                                <td class="ControlTextBox3" style="width:15%">
                                                                    <asp:TextBox ID="txtNarrAdd1" ValidationGroup="addVal" runat="server" MaxLength="200"
                                                                        Text='<%# Bind("Narration1") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr style="height:3px">
                                                            </tr>
                                                            <tr>
                                                            <td style="width:1%">
                                                                    
                                                                </td>
                                                                <td class="ControlTextBox3" style="width:10%">
                                                                    <asp:TextBox ID="txtRefnumAdd2" ValidationGroup="addVal" runat="server" Text='<%# Bind("RefNo2") %>'
                                                                        SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                </td>
                                                                <td style="width:3%">
                                                                    
                                                                </td>
                                                                <td  class="ControlTextBox3" style="width:10%">
                                                                           <asp:TextBox ID="txtTransDateAdd2" CssClass="cssTextBox" runat="server" ValidationGroup="editVal" 
                                                                                Text='<%# Bind("TransDate2","{0:dd/MM/yyyy}") %>' Width="100px" MaxLength="10"></asp:TextBox>
                                                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                                PopupButtonID="btnDate3" PopupPosition="BottomLeft" TargetControlID="txtTransDateAdd2">
                                                                            </cc1:CalendarExtender>
                                                                </td>
                                                                <td style="width:3%">
                                                                    <asp:ImageButton ID="ImageButton2" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                        Width="20px" runat="server" />
                                                                </td>
                                                                <td class="ControlDrpBorder" style="width:15%">
                                                                    <asp:DropDownList ID="cmbDebtorAdd2" runat="server" BackColor = "#90c9fc" CssClass="drpDownListMedium" Width="100%" AutoPostBack="False"
                                                                        DataSourceID="srcCreditorDebitorAdd" DataValueField="LedgerID" DataTextField="LedgerName" style="border: 1px solid #90c9fc" height="26px"
                                                                        AppendDataBoundItems="true" ValidationGroup="addVal" OnDataBound="ComboBox2_DataBound">
                                                                        <asp:ListItem Text="Select Debtor" style="background-color: #90c9fc" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                
                                                                <td style="width:2%">
                                                                     
                                                                </td>
                                                                <td class="ControlDrpBorder" style="width:15%">
                                                                    <asp:DropDownList ID="cmbCreditorAdd2" runat="server" BackColor = "#90c9fc" CssClass="drpDownListMedium" Width="100%" AutoPostBack="False"
                                                                        DataSourceID="srcCreditorDebitorAdd" DataValueField="LedgerID" DataTextField="LedgerName" style="border: 1px solid #90c9fc" height="26px"
                                                                        AppendDataBoundItems="true" ValidationGroup="addVal" OnDataBound="cmbCreditorAdd_DataBound">
                                                                        <asp:ListItem Text="Select Creditor" style="background-color: #90c9fc" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="width:2%">
                                                                    
                                                                </td>
                                                                <td class="ControlTextBox3" style="width:10%">
                                                                    <asp:TextBox ID="txtAmountAdd2" runat="server" ValidationGroup="addVal" Text='<%# Bind("Amount2") %>'
                                                                        Width="95%" CssClass="cssTextBox"></asp:TextBox>
                                                                </td>
                                                                <td style="width:2%">
                                                                     
                                                                </td>
                                                                <td class="ControlTextBox3" style="width:15%">
                                                                    <asp:TextBox ID="txtNarrAdd2" ValidationGroup="addVal" runat="server" MaxLength="200"
                                                                        Text='<%# Bind("Narration2") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr style="height:3px">
                                                            </tr>
                                                            <tr>
                                                            <td style="width:1%">
                                                                    
                                                                </td>
                                                                <td class="ControlTextBox3" style="width:10%">
                                                                        <asp:TextBox ID="txtRefnumAdd3" ValidationGroup="addVal" runat="server" Text='<%# Bind("RefNo3") %>'
                                                                            SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                </td>
                                                                <td style="width:3%">
                                                                    
                                                                </td>
                                                                <td  class="ControlTextBox3" style="width:10%">
                                                                       <asp:TextBox ID="txtTransDateAdd3" CssClass="cssTextBox" runat="server" ValidationGroup="editVal"
                                                                                Text='<%# Bind("TransDate3","{0:dd/MM/yyyy}") %>' Width="100px" MaxLength="10"></asp:TextBox>
                                                                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                                PopupButtonID="btnDate3" PopupPosition="BottomLeft" TargetControlID="txtTransDateAdd3">
                                                                            </cc1:CalendarExtender>
                                                                            
                                                                    
                                                                    </td>
                                                                <td style="width:3%">
                                                                    <asp:ImageButton ID="ImageButton3" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                        Width="20px" runat="server" />
                                                                </td>
                                                                <td class="ControlDrpBorder" style="width:15%">
                                                                    <asp:DropDownList ID="cmbDebtorAdd3" runat="server" BackColor = "#90c9fc" CssClass="drpDownListMedium" Width="100%" AutoPostBack="False"
                                                                        DataSourceID="srcCreditorDebitorAdd" DataValueField="LedgerID" DataTextField="LedgerName" style="border: 1px solid #90c9fc" height="26px"
                                                                        AppendDataBoundItems="true" ValidationGroup="addVal" OnDataBound="ComboBox2_DataBound">
                                                                        <asp:ListItem Text="Select Debtor" style="background-color: #90c9fc" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="width:2%">
                                                                     
                                                                </td>
                                                                <td class="ControlDrpBorder" style="width:15%">
                                                                    <asp:DropDownList ID="cmbCreditorAdd3" runat="server" BackColor = "#90c9fc" CssClass="drpDownListMedium" Width="100%" AutoPostBack="False"
                                                                        DataSourceID="srcCreditorDebitorAdd" DataValueField="LedgerID" DataTextField="LedgerName" style="border: 1px solid #90c9fc" height="26px"
                                                                        AppendDataBoundItems="true" ValidationGroup="addVal" OnDataBound="cmbCreditorAdd_DataBound">
                                                                        <asp:ListItem Text="Select Creditor" style="background-color: #90c9fc" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="width:2%">
                                                                     
                                                                </td>
                                                                <td class="ControlTextBox3" style="width:10%">
                                                                    <asp:TextBox ID="txtAmountAdd3" runat="server" ValidationGroup="addVal" Text='<%# Bind("Amount3") %>'
                                                                        Width="95%" CssClass="cssTextBox"></asp:TextBox>
                                                                </td>
                                                                <td style="width:2%">
                                                                     
                                                                </td>
                                                                <td class="ControlTextBox3" style="width:15%">
                                                                    <asp:TextBox ID="txtNarrAdd3" ValidationGroup="addVal" runat="server" MaxLength="200"
                                                                        Text='<%# Bind("Narration3") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr style="height:3px">
                                                            </tr>
                                                            <tr>
                                                            <td style="width:1%">
                                                                     
                                                                </td>
                                                                <td class="ControlTextBox3" style="width:10%">
                                                                    <asp:TextBox ID="txtRefnumAdd4" ValidationGroup="addVal" runat="server" Text='<%# Bind("RefNo4") %>'
                                                                        SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                </td>
                                                                <td style="width:3%">
                                                                     
                                                                </td>
                                                                <td class="ControlTextBox3" style="width:10%">
                                                                           <asp:TextBox ID="txtTransDateAdd4" CssClass="cssTextBox" runat="server" ValidationGroup="editVal"
                                                                                Text='<%# Bind("TransDate4","{0:dd/MM/yyyy}") %>' Width="100px" MaxLength="10"></asp:TextBox>
                                                                            <cc1:CalendarExtender ID="CalendarExtender4" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                                PopupButtonID="btnDate3" PopupPosition="BottomLeft" TargetControlID="txtTransDateAdd4">
                                                                            </cc1:CalendarExtender>
                                                                            
                                                                    
                                                                </td>
                                                                <td style="width:3%">
                                                                     <asp:ImageButton ID="ImageButton4" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                        Width="20px" runat="server" />
                                                                </td>
                                                                <td class="ControlDrpBorder" align="left" style="width:15%">
                                                                    <asp:DropDownList ID="cmbDebtorAdd4" runat="server" BackColor = "#90c9fc" CssClass="drpDownListMedium" Width="100%" AutoPostBack="False"
                                                                        DataSourceID="srcCreditorDebitorAdd" DataValueField="LedgerID" DataTextField="LedgerName" style="border: 1px solid #90c9fc" height="26px"
                                                                        AppendDataBoundItems="true" ValidationGroup="addVal" OnDataBound="ComboBox2_DataBound">
                                                                        <asp:ListItem Text="Select Debtor" style="background-color: #90c9fc" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="width:2%">
                                                                     
                                                                </td>
                                                                <td class="ControlDrpBorder" style="width:15%">
                                                                    <asp:DropDownList ID="cmbCreditorAdd4" runat="server" BackColor = "#90c9fc" CssClass="drpDownListMedium" Width="100%" AutoPostBack="False"
                                                                        DataSourceID="srcCreditorDebitorAdd" DataValueField="LedgerID" DataTextField="LedgerName" style="border: 1px solid #90c9fc" height="26px"
                                                                        AppendDataBoundItems="true" ValidationGroup="addVal" OnDataBound="cmbCreditorAdd_DataBound">
                                                                        <asp:ListItem Text="Select Creditor" style="background-color: #90c9fc" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="width:2%">
                                                                     
                                                                </td>
                                                                <td class="ControlTextBox3" style="width:10%">
                                                                    <asp:TextBox ID="txtAmountAdd4" runat="server" ValidationGroup="addVal" Text='<%# Bind("Amount4") %>'
                                                                        Width="95%" CssClass="cssTextBox"></asp:TextBox>
                                                                </td>
                                                                <td style="width:2%">
                                                                     
                                                                </td>
                                                                <td class="ControlTextBox3" style="width:15%">
                                                                    <asp:TextBox ID="txtNarrAdd4" ValidationGroup="addVal" runat="server" MaxLength="200"
                                                                        Text='<%# Bind("Narration4") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr style="height:3px">
                                                            </tr>
                                                            <tr>
                                                                <td style="width:1%">
                                                                     
                                                                </td>
                                                                <td style="width:10%" class="ControlTextBox3">
                                                                    <asp:TextBox ID="txtRefnumAdd5" ValidationGroup="addVal" runat="server" Text='<%# Bind("RefNo5") %>'
                                                                        SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                </td>
                                                                <td style="width:3%">
                                                                     
                                                                </td>
                                                                <td  class="ControlTextBox3" style="width:10%">
                                                                        
                                                                            <asp:TextBox ID="txtTransDateAdd5" CssClass="cssTextBox" runat="server" ValidationGroup="editVal" 
                                                                                Text='<%# Bind("TransDate5","{0:dd/MM/yyyy}") %>' Width="100px" MaxLength="10"></asp:TextBox>
                                                                            <cc1:CalendarExtender ID="CalendarExtender5" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                                PopupButtonID="btnDate3" PopupPosition="BottomLeft" TargetControlID="txtTransDateAdd5">
                                                                            </cc1:CalendarExtender>
                                                                            
                                                                    
                                                                </td>
                                                                <td style="width:3%">
                                                                     <asp:ImageButton ID="ImageButton5" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                        Width="20px" runat="server" />
                                                                </td>
                                                                <td class="ControlDrpBorder" style="width:15%">
                                                                    <asp:DropDownList ID="cmbDebtorAdd5" runat="server" BackColor = "#90c9fc" CssClass="drpDownListMedium" Width="100%" AutoPostBack="False"
                                                                        DataSourceID="srcCreditorDebitorAdd" DataValueField="LedgerID" DataTextField="LedgerName" style="border: 1px solid #90c9fc" height="26px"
                                                                        AppendDataBoundItems="true" ValidationGroup="addVal" OnDataBound="ComboBox2_DataBound">
                                                                        <asp:ListItem Text="Select Debtor" style="background-color: #90c9fc" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="width:2%">
                                                                     
                                                                </td>
                                                                <td class="ControlDrpBorder" style="width:15%">
                                                                    <asp:DropDownList ID="cmbCreditorAdd5" runat="server" BackColor = "#90c9fc" CssClass="drpDownListMedium" Width="100%" AutoPostBack="False"
                                                                        DataSourceID="srcCreditorDebitorAdd" DataValueField="LedgerID" DataTextField="LedgerName" style="border: 1px solid #90c9fc" height="26px"
                                                                        AppendDataBoundItems="true" ValidationGroup="addVal" OnDataBound="cmbCreditorAdd_DataBound">
                                                                        <asp:ListItem Text="Select Creditor" style="background-color: #90c9fc" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="width:2%">
                                                                     
                                                                </td>
                                                                <td class="ControlTextBox3" style="width:10%">
                                                                    <asp:TextBox ID="txtAmountAdd5" runat="server" ValidationGroup="addVal" Text='<%# Bind("Amount5") %>'
                                                                        Width="95%" CssClass="cssTextBox"></asp:TextBox>
                                                                </td>
                                                                <td style="width:2%">
                                                                     
                                                                </td>
                                                                <td class="ControlTextBox3" style="width:15%">
                                                                    <asp:TextBox ID="txtNarrAdd5" ValidationGroup="addVal" runat="server" MaxLength="200"
                                                                        Text='<%# Bind("Narration5") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr style="height:3px">
                                                            </tr>
                                                            <tr>
                                                            <td style="width:1%">
                                                                     
                                                                </td>
                                                                <td style="width:10%" class="ControlTextBox3">
                                                                    <asp:TextBox ID="txtRefnumAdd6" ValidationGroup="addVal" runat="server" Text='<%# Bind("RefNo6") %>'
                                                                        SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                </td>
                                                                <td style="width:3%">
                                                                     
                                                                </td>
                                                                <td  class="ControlTextBox3" style="width:10%">
                                                                          <asp:TextBox ID="txtTransDateAdd6" CssClass="cssTextBox" runat="server" ValidationGroup="editVal" 
                                                                                Text='<%# Bind("TransDate6","{0:dd/MM/yyyy}") %>' Width="100px" MaxLength="10"></asp:TextBox>
                                                                            <cc1:CalendarExtender ID="CalendarExtender6" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                                PopupButtonID="btnDate3" PopupPosition="BottomLeft" TargetControlID="txtTransDateAdd6">
                                                                            </cc1:CalendarExtender>
                                                                </td>
                                                                <td style="width:3%">
                                                                     <asp:ImageButton ID="ImageButton6" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                        Width="20px" runat="server" />
                                                                </td>
                                                                <td class="ControlDrpBorder" style="width:15%">
                                                                    <asp:DropDownList ID="cmbDebtorAdd6" runat="server" BackColor = "#90c9fc" CssClass="drpDownListMedium" Width="100%" AutoPostBack="False"
                                                                        DataSourceID="srcCreditorDebitorAdd" DataValueField="LedgerID" DataTextField="LedgerName" style="border: 1px solid #90c9fc" height="26px"
                                                                        AppendDataBoundItems="true" ValidationGroup="addVal" OnDataBound="ComboBox2_DataBound">
                                                                        <asp:ListItem Text="Select Debtor" style="background-color: #90c9fc" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                
                                                                <td style="width:2%">
                                                                     
                                                                </td>
                                                                <td class="ControlDrpBorder" style="width:15%">
                                                                    <asp:DropDownList ID="cmbCreditorAdd6" runat="server" BackColor = "#90c9fc" CssClass="drpDownListMedium" Width="100%" AutoPostBack="False"
                                                                        DataSourceID="srcCreditorDebitorAdd" DataValueField="LedgerID" DataTextField="LedgerName" style="border: 1px solid #90c9fc" height="26px"
                                                                        AppendDataBoundItems="true" ValidationGroup="addVal" OnDataBound="cmbCreditorAdd_DataBound">
                                                                        <asp:ListItem Text="Select Creditor" style="background-color: #90c9fc" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="width:2%">
                                                                     
                                                                </td>
                                                                <td class="ControlTextBox3" style="width:10%">
                                                                    <asp:TextBox ID="txtAmountAdd6" runat="server" ValidationGroup="addVal" Text='<%# Bind("Amount6") %>'
                                                                        Width="95%" CssClass="cssTextBox"></asp:TextBox>
                                                                </td>
                                                                <td style="width:2%">
                                                                     
                                                                </td>
                                                                <td class="ControlTextBox3" style="width:15%">
                                                                    <asp:TextBox ID="txtNarrAdd6" ValidationGroup="addVal" runat="server" MaxLength="200"
                                                                        Text='<%# Bind("Narration6") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr style="height:3px">
                                                            </tr>
                                                            
                                                            <tr style="height:3px">

                                                            </tr>
                                                        </table>
                                                        
                                                    </td>
                                                    
                                                </tr>
                                                
                                                <tr style="height:7px">
                                                    
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    
                                                    <td align="right">
                                                        <asp:Button ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                            CssClass="cancelbutton" EnableTheming="false" SkinID="skinBtnCancel" OnClick="InsertCancelButton_Click">
                                                        </asp:Button>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" OnClientClick="javascript:CheckDate();"
                                                            CssClass="savebutton" EnableTheming="false" ValidationGroup="addVal" SkinID="skinBtnSave"
                                                            OnClick="InsertButton_Click"></asp:Button>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    
                                                </tr>
                                                 <tr style="height:5px">
                                                    
                                                </tr>
                                                
                                            </table>
                                        </div>
                                        <table cellspacing="0">
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:ValidationSummary ID="valSumAdd" DisplayMode="BulletList" ShowMessageBox="true"
                                                        ShowSummary="false" HeaderText="Validation Messages" ValidationGroup="addVal"
                                                        Font-Names="'Trebuchet MS'" Font-Size="12" runat="server" />
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                    </InsertItemTemplate>
                                </asp:FormView>
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
            </td> </tr>

            <tr style="width: 100%">
                <td style="width: 100%;">
                <table width="100%" style="margin: -3px 0px 0px 0px;">
                                <tr style="width: 100%">
                                    <td>
                    <div class="mainGridHold" id="searchGrid">
                        <asp:GridView ID="GrdViewJournal" runat="server" AutoGenerateColumns="False" Width="99.9%"
                            DataKeyNames="TransNo" AllowPaging="True" EmptyDataText="No Journals found" OnRowCreated="GrdViewJournal_RowCreated"
                            OnSelectedIndexChanged="GrdViewJournal_SelectedIndexChanged" OnRowCommand="GrdViewJournal_RowCommand"
                            OnPageIndexChanging="GrdViewJournal_PageIndexChanging" OnRowDeleting="GrdViewJournal_RowDeleting"
                            OnSorting="GrdViewJournal_Sorting" OnRowDataBound="GrdViewJournal_RowDataBound" CssClass="someClass">
                            <EmptyDataRowStyle CssClass="GrdContent" />
                            <Columns>
                                <asp:BoundField DataField="TransNo" HeaderText="Trans. No." SortExpression="TransNo"
                                    HeaderStyle-Wrap="false" HeaderStyle-BorderColor="blue" />
                                <asp:BoundField DataField="Refno" HeaderText="Ref. No." HeaderStyle-Wrap="false"  HeaderStyle-BorderColor="blue"/>
                                <asp:BoundField DataField="TransDate" HeaderText="Trans. Date" HeaderStyle-Wrap="false"  HeaderStyle-BorderColor="blue"/>
                                <asp:BoundField DataField="Debi" HeaderText="Debtor"  HeaderStyle-BorderColor="blue"/>
                                <asp:BoundField DataField="Cred" HeaderText="Creditor"  HeaderStyle-BorderColor="blue"/>
                                <asp:BoundField DataField="Amount" HeaderText="Amount"  HeaderStyle-BorderColor="blue"/>
                                <asp:BoundField DataField="Narration" HeaderText="Narration" Visible="false"  HeaderStyle-BorderColor="blue"/>
                                <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="50px" HeaderText="Edit" HeaderStyle-BorderColor="blue">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Select" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="50px" HeaderText="Delete" HeaderStyle-BorderColor="blue">
                                    <ItemTemplate>
                                        <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to Delete this Journal?"
                                            runat="server">
                                        </cc1:ConfirmButtonExtender>
                                        <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" CommandName="Delete"></asp:ImageButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="50px" HeaderText="Print" HeaderStyle-BorderColor="blue">
                                    <ItemTemplate>
                                        <a href='<%# DataBinder.Eval(Container, "DataItem.TransNo", "javascript:PrintItem({0});") %>'>
                                            <img alt="Print" border="0" src="App_Themes/DefaultTheme/Images/Print.png">
                                        </a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataRowStyle CssClass="GrdContent" />
                            <PagerTemplate>
                                <table style=" border-color:white">
                                    <tr style=" border-color:white">
                                        <td style=" border-color:white">
                                            Goto Page
                                        </td>
                                        <td style=" border-color:white">
                                            <asp:DropDownList ID="ddlPageSelector" runat="server" OnSelectedIndexChanged="ddlPageSelector_SelectedIndexChanged"
                                                AutoPostBack="true" width="65px" style="border:1px solid blue">
                                            </asp:DropDownList>
                                        </td>
                                        <td  style=" border-color:white; Width:5px">
                                            
                                        </td>
                                        <td style=" border-color:white">
                                            <asp:Button Text="" CommandName="Page" CommandArgument="First" runat="server" CssClass="NewFirst" EnableTheming="false" Width="22px" Height="18px"
                                                ID="btnFirst" />
                                        </td>
                                        <td style=" border-color:white">
                                            <asp:Button Text="" CommandName="Page" CommandArgument="Prev" runat="server" CssClass="NewPrev" EnableTheming="false" Width="22px" Height="18px"
                                                ID="btnPrevious" />
                                        </td>
                                        <td style=" border-color:white">
                                            <asp:Button Text="" CommandName="Page" CommandArgument="Next" runat="server" CssClass="NewNext" EnableTheming="false" Width="22px" Height="18px"
                                                ID="btnNext" />
                                        </td>
                                        <td style=" border-color:white">
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
                    <asp:ObjectDataSource ID="srcCreditorDebitorAdd" runat="server" SelectMethod="ListCreditorDebitorJ"
                        TypeName="BusinessLogic">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="hdDataSource" Name="connection" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="srcCreditorDebitor" runat="server" SelectMethod="ListCreditorDebitorJ"
                        TypeName="BusinessLogic">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="hdDataSource" Name="connection" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="frmSource" runat="server" SelectMethod="GetJournalForId"
                        TypeName="BusinessLogic" InsertMethod="InsertJournal" OnUpdating="frmSource_Updating"
                        OnInserting="frmSource_Inserting" UpdateMethod="UpdateJournal" OnInserted="frmSource_Inserted"
                        OnUpdated="frmSource_Updated">
                        <UpdateParameters>
                            <asp:Parameter Name="TransNo" Type="Int32" />
                            <asp:Parameter Name="RefNo" Type="String" />
                            <asp:Parameter Name="TransDate" Type="DateTime" />
                            <asp:Parameter Name="DebitorID" Type="Int32" />
                            <asp:Parameter Name="CreditorID" Type="Int32" />
                            <asp:Parameter Name="Amount" Type="Double" />
                            <asp:Parameter Name="Narration" Type="String" />
                            <asp:Parameter Name="VoucherType" Type="String" />
                            <asp:Parameter Name="sPath" Type="String" />
                            <asp:Parameter Name="NewTransNo" Type="Int32" Direction="Output" />
                        </UpdateParameters>
                        <SelectParameters>
                            <asp:ControlParameter ControlID="GrdViewJournal" Name="TransNo" PropertyName="SelectedValue"
                                Type="Int32" />
                            <asp:ControlParameter ControlID="hdDataSource" Name="ConStr" Type="String" />
                        </SelectParameters>
                        <InsertParameters>
                            <asp:Parameter Name="RefNo" Type="String" />
                            <asp:Parameter Name="TransDate" Type="DateTime" />
                            <asp:Parameter Name="DebitorID" Type="Int32" />
                            <asp:Parameter Name="CreditorID" Type="Int32" />
                            <asp:Parameter Name="Amount" Type="Double" />
                            <asp:Parameter Name="Narration" Type="String" />
                            <asp:Parameter Name="VoucherType" Type="String" />
                            <asp:Parameter Name="sPath" Type="String" />
                            <asp:Parameter Name="NewTransNo" Type="Int32" Direction="Output" />
                        </InsertParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="GridSource" runat="server" SelectMethod="ListJournal" TypeName="BusinessLogic"
                        DeleteMethod="DeleteJournal" OnDeleting="GridSource_Deleting">
                        <DeleteParameters>
                            <asp:Parameter Name="TransNo" Type="Int32" />
                            <asp:Parameter Name="sPath" Type="String" />
                        </DeleteParameters>
                    </asp:ObjectDataSource>
                    <asp:ValidationSummary ID="valSummary" runat="server" ShowMessageBox="true" ShowSummary="false" />
                    <asp:HiddenField ID="hdDataSource" runat="server" />
                    <asp:HiddenField ID="hdJournal" runat="server" />

                    
                </td>
            </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
