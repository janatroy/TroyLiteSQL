<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="MultipleJournal.aspx.cs" Inherits="MultipleJournal"
    Title="Financials > Journals" %>

<%@ Register Assembly="RealWorld.Grids" Namespace="RealWorld.Grids" TagPrefix="rwg" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <script language="javascript" type="text/javascript">

        function PrintItem(ID) {
            window.showModalDialog('PrintJournal.aspx?ID=' + ID, self, 'dialogWidth:700px;dialogHeight:430px;status:no;dialogHide:yes;unadorned:yes;');

        }
        function ShowModalPopup() {
            $find("mpe").show();
            // alert("show");
            return false;
        }
        function HideModalPopup() {
            $find("mpe").hide();
            document.getElementById('ctl00_cplhControlPanel_optionmethod_0').checked = true;
            //  alert("hide");
            return false;
        }

        <%--window.onload = function Showalert() {

            var txt = document.getElementById("<%= txtSearch.ClientID %>");
            var btn = document.getElementById("<%= BtnClearFilter.ClientID %>");
            if (txt.value == "") {
                // alert(txt.value);
                btn.style.visibility = "hidden";
                // when the window is loaded, hide the button if the textbox is empty
            }

        }

        function EnableDisableButton(sender, target) {
            var first = document.getElementById('<%=txtSearch.ClientID %>');

            if (sender.value.length >= 1 && first.value.length >= 1) {
                document.getElementById('<%=BtnClearFilter.ClientID %>').style.visibility = "visible";

            }

            if (sender.value.length < 1 && first.value.length < 1) {

                document.getElementById('<%=BtnClearFilter.ClientID %>').style.visibility = "Hidden";
            }
        }--%>
        function Showalert() {


            var txt = document.getElementById("<%= txtSearch.ClientID %>");
            var btn = document.getElementById("<%= BtnClearFilter.ClientID %>");
            if (txt.value == "") {
                // alert("show");
                // alert(txt.value);
                btn.style.visibility = "hidden";
                // when the window is loaded, hide the button if the textbox is empty
            }
            else {
                //  alert("hide");
                btn.style.visibility = "visible";
            }

        }

    </script>
    <asp:UpdatePanel ID="UpdatePnlMaster" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div style="">
                <table style="width: 100%;" align="center">
                    <tr style="width: 100%">
                        <td style="width: 100%" valign="middle">
                            <%--<table class="mainConHd" style="width: 994px;">
                                <tr valign="middle">
                                    <td style="font-size: 20px;">
                                        Journals
                                    </td>
                                </tr>
                            </table>--%>
                            <div class="mainConBody">
                                <%-- class="mainConBody">--%>
                                <table style="width: 100%; margin: -1px 0px 0px 1px;" class="searchbg">
                                    <tr style="height: 25px; vertical-align: middle">
                                        <td style="width: 2%;"></td>
                                        <td style="width: 16%; font-size: 22px; color: White;">Journals
                                        </td>
                                        <td style="width: 16%">
                                            <div style="text-align: right;">
                                            </div>
                                        </td>
                                        <td style="width: 16%; color: White;" align="right">Search
                                        </td>
                                        <td class="NewBox" style="width: 20%">
                                            <asp:TextBox ID="txtTransno" runat="server" Width="92%" CssClass="cssTextBox" Visible="False"></asp:TextBox>
                                            <asp:TextBox ID="txtSearch" runat="server" SkinID="skinTxtBoxSearch"></asp:TextBox>
                                        </td>
                                        <%--<td align="right" style="width: 3%">
                                            
                                        </td>--%>
                                        <td class="NewBox" style="width: 20%">
                                            <div style="width: 160px; font-family: 'Trebuchet MS';">
                                                <asp:DropDownList ID="ddCriteria" runat="server" Width="153px" BackColor="White" Height="23px" Style="text-align: center; border: 1px solid White">
                                                    <asp:ListItem Value="0">All</asp:ListItem>
                                                    <asp:ListItem Value="RefNo">Ref No</asp:ListItem>
                                                    <asp:ListItem Value="TransNo">Trans No</asp:ListItem>
                                                    <asp:ListItem Value="Date">Trans Date</asp:ListItem>
                                                    <asp:ListItem Value="LedgerName">Debtor</asp:ListItem>
                                                    <asp:ListItem Value="Creditor">Creditor</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <asp:TextBox ID="txtRefno" Enabled="false" runat="server" Width="92%" CssClass="cssTextBox" Visible="False"></asp:TextBox>
                                            <asp:TextBox ID="txtLedger" Enabled="false" runat="server" Width="92%" CssClass="cssTextBox" Visible="False"></asp:TextBox>
                                            <asp:TextBox ID="txtDate" Enabled="false" runat="server" Width="92%" MaxLength="10" CssClass="cssTextBox" Visible="False"></asp:TextBox>
                                            <cc1:CalendarExtender ID="calExtender3" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                PopupButtonID="bt3" PopupPosition="BottomLeft" TargetControlID="txtDate">
                                            </cc1:CalendarExtender>
                                            <asp:ImageButton ID="bt3" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                Width="20px" runat="server" Visible="False" />
                                        </td>

                                        <td style="width: 20%">
                                            <asp:Button ID="cmdSearch" onkeyup="EnableDisableButton(this,'BtnClearFilter')" runat="server" OnClick="cmdSearch_Click" Text=""
                                                EnableTheming="false" CssClass="ButtonSearch6" />
                                        </td>
                                        <td style="width: 16%" class="tblLeftNoPad">
                                            <asp:Button ID="BtnClearFilter" runat="server" OnClick="BtnClearFilter_Click" EnableTheming="false" Text="" CssClass="ClearFilter6" />
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
                            <%--<div class="mainConBody">
                                <table style="width: 100%;" cellpadding="3" cellspacing="2" class="searchbg">
                                    <tr style="height: 20px; vertical-align: middle">
                                        <td style="width: 22%">
                                            
                                        </td>
                                        <td align="right" style="width: 10%">
                                            Ledger
                                        </td>
                                        <td class="cssTextBoxbg" style="width: 20%">
                                            
                                        </td>
                                        <td align="right" style="width: 8%">
                                            Date
                                        </td>
                                        <td class="cssTextBoxbg" style="width: 23%">
                                            
                                        </td>
                                        <td class="tblLeftNoPad" style="width: 20%">
                                            
                                        </td>
                                    </tr>
                                </table>
                            </div>--%>

                            <cc1:ModalPopupExtender ID="ModalPopupMethod" runat="server" BackgroundCssClass="modalBackground"
                                CancelControlID="CancelPopUpMethod" DynamicServicePath="" Enabled="True" PopupControlID="pnlMethod"
                                TargetControlID="ShowPopUpMethod" BehaviorID="mpe">
                            </cc1:ModalPopupExtender>
                            <input id="ShowPopUpMethod" type="button" style="display: none" runat="server" />
                            <input id="CancelPopUpMethod" runat="server" style="display: none"
                                type="button" />
                            </input>
                                            </input>
                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" Font-Names="'Trebuchet MS'" Font-Size="12pt"
                                                HeaderText="" ShowMessageBox="true" ShowSummary="true" ValidationGroup="contact" />
                            <asp:Panel ID="pnlMethod" runat="server" Style="width: 60%; display: none">
                                <asp:UpdatePanel ID="updatePnlMethod" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Panel ID="Panel1" CssClass="pnlPopUp" runat="server">
                                            <div id="Div3" class="divArea">
                                                <table cellpadding="3" cellspacing="2" style="width: 100%" align="center">
                                                    <tr style="width: 100%">
                                                        <td style="width: 100%">
                                                            <table style="text-align: left; width: 100%; border: 1px solid Gray;" cellpadding="3" cellspacing="2">
                                                                <tr>

                                                                    <td>
                                                                        <table class="headerPopUp" width="100%">
                                                                            <tr>
                                                                                <td>Select the Type
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>

                                                                </tr>

                                                                <tr>

                                                                    <td>
                                                                        <table style="width: 100%;" cellpadding="3" cellspacing="1">
                                                                            <tr style="height: 10px">
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 15%"></td>
                                                                                <td style="width: 70%;" class="ControlTextBox3" align="center">
                                                                                    <asp:RadioButtonList ID="optionmethod" runat="server" Style="font-size: 14px"
                                                                                        RepeatDirection="Horizontal" BackColor="#e7e7e7" Height="35px">
                                                                                        <asp:ListItem Value="Single" Selected="True">Single&nbsp;&nbsp;</asp:ListItem>
                                                                                        <asp:ListItem Value="Multiple">Multiple&nbsp;&nbsp;</asp:ListItem>
                                                                                        <asp:ListItem Value="DebitContra">Debit Contra&nbsp;&nbsp;</asp:ListItem>
                                                                                        <asp:ListItem Value="CreditContra">Credit Contra&nbsp;&nbsp;</asp:ListItem>
                                                                                    </asp:RadioButtonList>
                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtEntries"
                                                                                        ValidChars="." FilterType="Numbers, Custom" />
                                                                                    <asp:TextBox ID="txtEntries" runat="server" MaxLength="2" Width="45px" Visible="false"></asp:TextBox>
                                                                                </td>
                                                                                <td style="width: 15%"></td>

                                                                            </tr>
                                                                            <tr style="height: 7px">
                                                                            </tr>
                                                                        </table>
                                                                    </td>

                                                                </tr>
                                                                <tr>

                                                                    <td>
                                                                        <table cellpadding="1" cellspacing="2"
                                                                            width="100%">
                                                                            <tr>
                                                                                <td align="center">
                                                                                    <table>
                                                                                        <tr>
                                                                                            <td></td>
                                                                                            <td>
                                                                                                <asp:Panel ID="Panel4" runat="server" Width="120px">
                                                                                                    <asp:Button ID="cmdMethod" runat="server" CssClass="Start6"
                                                                                                        EnableTheming="false" OnClick="cmdMethod_Click" Text=""
                                                                                                        ValidationGroup="contact" />

                                                                                                </asp:Panel>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Panel ID="Panel5" runat="server" Width="120px">
                                                                                                    <asp:Button ID="cmdCancelMethod" runat="server" CssClass="cancelbutton6" OnClick="cmdCancelMethod_Click" CausesValidation="false"
                                                                                                       OnClientClick="return HideModalPopup()"  EnableTheming="false" />
                                                                                                </asp:Panel>
                                                                                            </td>
                                                                                        </tr>

                                                                                    </table>
                                                                                </td>

                                                                            </tr>
                                                                        </table>
                                                                    </td>

                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:ValidationSummary ID="valSum" DisplayMode="BulletList" ShowMessageBox="true"
                                                                ShowSummary="false" ValidationGroup="product" HeaderText=""
                                                                Font-Names="'Trebuchet MS'" Font-Size="12" runat="server" />
                                                            <asp:ValidationSummary ID="VS" DisplayMode="BulletList" ShowMessageBox="true" ShowSummary="false"
                                                                ValidationGroup="purchaseval" HeaderText="" Font-Names="'Trebuchet MS'"
                                                                Font-Size="12" runat="server" />
                                                            <input id="dummyPurchase" type="button" style="display: none" runat="server" />
                                                            <input id="BtnPopUpCancel1" type="button" style="display: none" runat="server" />
                                                            <cc1:ModalPopupExtender ID="ModalPopupPurchase" runat="server" BackgroundCssClass="modalBackground"
                                                                CancelControlID="BtnPopUpCancel1" DynamicServicePath="" Enabled="True" PopupControlID="purchasePanel"
                                                                RepositionMode="RepositionOnWindowResizeAndScroll" TargetControlID="dummyPurchase">
                                                            </cc1:ModalPopupExtender>
                                                            <asp:Panel ID="purchasePanel" runat="server" Style="width: 69%; display: none">
                                                                <asp:UpdatePanel ID="updatePnlPurchase" runat="server" RenderMode="Block" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <div id="Div1" style="background-color: White;">
                                                                            <table style="width: 100%;" align="center">
                                                                                <tr style="width: 100%">
                                                                                    <td style="width: 100%">
                                                                                        <table style="text-align: left; border: 1px solid Gray;" width="100%" cellpadding="3" cellspacing="2">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <table class="headerPopUp" width="100%">
                                                                                                        <tr>
                                                                                                            <td>Multiple Journal Details
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <table width="100%" cellpadding="2" cellspacing="1">
                                                                                                        <tr>
                                                                                                            <td colspan="12" style="width: 100%">
                                                                                                                <table width="100%" cellpadding="2" cellspacing="1">
                                                                                                                    <tr style="height: 5px">
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td style="width: 2%"></td>
                                                                                                                        <td align="center" style="width: 5%">Ref. No. *
                                                                                                                        </td>
                                                                                                                        <td style="width: 1%"></td>
                                                                                                                        <td align="center" style="width: 5%">Date *
                                                                                                                        </td>
                                                                                                                        <td style="width: 1%"></td>
                                                                                                                        <td align="center" style="width: 20%">Debtor *
                                                                                                                        </td>
                                                                                                                        <td style="width: 1%"></td>
                                                                                                                        <td align="center" style="width: 20%">Creditor *
                                                                                                                        </td>
                                                                                                                        <td style="width: 1%"></td>
                                                                                                                        <td align="center" style="width: 6%">Amount *
                                                                                                                        </td>
                                                                                                                        <td style="width: 1%"></td>
                                                                                                                        <td align="center" style="width: 30%">Narration *
                                                                                                                        </td>

                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td style="width: 2%"></td>
                                                                                                                        <td class="ControlTextBoxSmall" style="width: 5%">
                                                                                                                            <asp:TextBox ID="txtRefnumAdd1" ValidationGroup="addVal" runat="server" BackColor="#e7e7e7"
                                                                                                                                CssClass="cssTextBoxReport2"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 1%"></td>
                                                                                                                        <td class="ControlTextBoxSmall" style="width: 5%">
                                                                                                                            <asp:TextBox ID="txtTransDateAdd1" Enabled="false" CssClass="cssTextBoxReport2" runat="server" ValidationGroup="editVal"
                                                                                                                                Width="100px" MaxLength="10"></asp:TextBox>
                                                                                                                            <cc1:CalendarExtender ID="calExtender32" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                                                                                PopupButtonID="btnDate3" PopupPosition="BottomLeft" TargetControlID="txtTransDateAdd1">
                                                                                                                            </cc1:CalendarExtender>
                                                                                                                        </td>
                                                                                                                        <td style="width: 4%">
                                                                                                                            <asp:ImageButton ID="btnDate3" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                                                                                Width="20px" runat="server" />
                                                                                                                        </td>
                                                                                                                        <td class="ControlDrpBorder" style="width: 20%">
                                                                                                                            <asp:DropDownList ID="cmbDebtorAdd1" runat="server" BackColor="#e7e7e7" CssClass="drpDownListMedium" Width="100%" AutoPostBack="False"
                                                                                                                                DataValueField="LedgerID" DataTextField="LedgerName" Style="border: 1px solid #e7e7e7" Height="26px"
                                                                                                                                AppendDataBoundItems="true" ValidationGroup="addVal">
                                                                                                                                <asp:ListItem Text="Select Debtor" style="background-color: #e7e7e7" Value="0"></asp:ListItem>
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td style="width: 1%"></td>
                                                                                                                        <td class="ControlDrpBorder" style="width: 20%">
                                                                                                                            <asp:DropDownList ID="cmbCreditorAdd1" runat="server" BackColor="#e7e7e7" CssClass="drpDownListMedium" Width="100%" AutoPostBack="False"
                                                                                                                                DataValueField="LedgerID" DataTextField="LedgerName" Style="border: 1px solid #e7e7e7" Height="26px"
                                                                                                                                AppendDataBoundItems="true" ValidationGroup="addVal">
                                                                                                                                <asp:ListItem Text="Select Creditor" style="background-color: #e7e7e7" Value="0"></asp:ListItem>
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td style="width: 1%"></td>
                                                                                                                        <td class="ControlTextBoxSmall" style="width: 6%">
                                                                                                                            <asp:TextBox ID="txtAmountAdd1" runat="server" ValidationGroup="addVal"
                                                                                                                                CssClass="cssTextBoxReport2"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 1%"></td>
                                                                                                                        <td class="ControlTextBox3" style="width: 30%">
                                                                                                                            <asp:TextBox ID="txtNarrAdd1" ValidationGroup="addVal" runat="server" MaxLength="200"
                                                                                                                                CssClass="cssTextBox"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr style="height: 1px">
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td style="width: 2%"></td>
                                                                                                                        <td class="ControlTextBoxSmall" style="width: 5%">
                                                                                                                            <asp:TextBox ID="txtRefnumAdd2" ValidationGroup="addVal" runat="server"
                                                                                                                                CssClass="cssTextBoxReport2"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 1%"></td>
                                                                                                                        <td class="ControlTextBoxSmall" style="width: 5%">
                                                                                                                            <asp:TextBox ID="txtTransDateAdd2" Enabled="false" CssClass="cssTextBoxReport2" runat="server" ValidationGroup="editVal"
                                                                                                                                Width="100px" MaxLength="10"></asp:TextBox>
                                                                                                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                                                                                PopupButtonID="ImageButton2" PopupPosition="BottomLeft" TargetControlID="txtTransDateAdd2">
                                                                                                                            </cc1:CalendarExtender>
                                                                                                                        </td>
                                                                                                                        <td style="width: 4%">
                                                                                                                            <asp:ImageButton ID="ImageButton2" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                                                                                Width="20px" runat="server" />
                                                                                                                        </td>
                                                                                                                        <td class="ControlDrpBorder" style="width: 15%">
                                                                                                                            <asp:DropDownList ID="cmbDebtorAdd2" runat="server" BackColor="#e7e7e7" CssClass="drpDownListMedium" Width="100%" AutoPostBack="False"
                                                                                                                                DataValueField="LedgerID" DataTextField="LedgerName" Style="border: 1px solid #e7e7e7" Height="26px"
                                                                                                                                AppendDataBoundItems="true" ValidationGroup="addVal">
                                                                                                                                <asp:ListItem Text="Select Debtor" style="background-color: #90c9fc" Value="0"></asp:ListItem>
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>

                                                                                                                        <td style="width: 1%"></td>
                                                                                                                        <td class="ControlDrpBorder" style="width: 15%">
                                                                                                                            <asp:DropDownList ID="cmbCreditorAdd2" runat="server" BackColor="#e7e7e7" CssClass="drpDownListMedium" Width="100%" AutoPostBack="False"
                                                                                                                                DataValueField="LedgerID" DataTextField="LedgerName" Style="border: 1px solid #e7e7e7" Height="26px"
                                                                                                                                AppendDataBoundItems="true" ValidationGroup="addVal">
                                                                                                                                <asp:ListItem Text="Select Creditor" style="background-color: #90c9fc" Value="0"></asp:ListItem>
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td style="width: 1%"></td>
                                                                                                                        <td class="ControlTextBoxSmall" style="width: 6%">
                                                                                                                            <asp:TextBox ID="txtAmountAdd2" runat="server" ValidationGroup="addVal"
                                                                                                                                CssClass="cssTextBoxReport2"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 1%"></td>
                                                                                                                        <td class="ControlTextBox3" style="width: 34%">
                                                                                                                            <asp:TextBox ID="txtNarrAdd2" ValidationGroup="addVal" runat="server" MaxLength="200"
                                                                                                                                CssClass="cssTextBox"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr style="height: 1px">
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td style="width: 2%"></td>
                                                                                                                        <td class="ControlTextBoxSmall" style="width: 5%">
                                                                                                                            <asp:TextBox ID="txtRefnumAdd3" ValidationGroup="addVal" runat="server"
                                                                                                                                CssClass="cssTextBoxReport2"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 1%"></td>
                                                                                                                        <td class="ControlTextBoxSmall" style="width: 5%">
                                                                                                                            <asp:TextBox ID="txtTransDateAdd3" Enabled="false" CssClass="cssTextBoxReport2" runat="server" ValidationGroup="editVal"
                                                                                                                                Width="100px" MaxLength="10"></asp:TextBox>
                                                                                                                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                                                                                PopupButtonID="ImageButton3" PopupPosition="BottomLeft" TargetControlID="txtTransDateAdd3">
                                                                                                                            </cc1:CalendarExtender>
                                                                                                                        </td>
                                                                                                                        <td style="width: 4%">
                                                                                                                            <asp:ImageButton ID="ImageButton3" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                                                                                Width="20px" runat="server" />
                                                                                                                        </td>
                                                                                                                        <td class="ControlDrpBorder" style="width: 20%">
                                                                                                                            <asp:DropDownList ID="cmbDebtorAdd3" runat="server" BackColor="#e7e7e7" CssClass="drpDownListMedium" Width="100%" AutoPostBack="False"
                                                                                                                                DataValueField="LedgerID" DataTextField="LedgerName" Style="border: 1px solid #e7e7e7" Height="26px"
                                                                                                                                AppendDataBoundItems="true" ValidationGroup="addVal">
                                                                                                                                <asp:ListItem Text="Select Debtor" style="background-color: #e7e7e7" Value="0"></asp:ListItem>
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td style="width: 1%"></td>
                                                                                                                        <td class="ControlDrpBorder" style="width: 20%">
                                                                                                                            <asp:DropDownList ID="cmbCreditorAdd3" runat="server" BackColor="#90c9fc" CssClass="drpDownListMedium" Width="100%" AutoPostBack="False"
                                                                                                                                DataValueField="LedgerID" DataTextField="LedgerName" Style="border: 1px solid #90c9fc" Height="26px"
                                                                                                                                AppendDataBoundItems="true" ValidationGroup="addVal">
                                                                                                                                <asp:ListItem Text="Select Creditor" style="background-color: #90c9fc" Value="0"></asp:ListItem>
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td style="width: 1%"></td>
                                                                                                                        <td class="ControlTextBoxSmall" style="width: 6%">
                                                                                                                            <asp:TextBox ID="txtAmountAdd3" runat="server" ValidationGroup="addVal"
                                                                                                                                CssClass="cssTextBoxReport2"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 1%"></td>
                                                                                                                        <td class="ControlTextBox3" style="width: 30%">
                                                                                                                            <asp:TextBox ID="txtNarrAdd3" ValidationGroup="addVal" runat="server" MaxLength="200"
                                                                                                                                CssClass="cssTextBox"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr style="height: 1px">
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td style="width: 2%"></td>
                                                                                                                        <td class="ControlTextBoxSmall" style="width: 5%">
                                                                                                                            <asp:TextBox ID="txtRefnumAdd4" ValidationGroup="addVal" runat="server"
                                                                                                                                CssClass="cssTextBoxReport2"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 1%"></td>
                                                                                                                        <td class="ControlTextBoxSmall" style="width: 5%">
                                                                                                                            <asp:TextBox ID="txtTransDateAdd4" Enabled="false" CssClass="cssTextBoxReport2" runat="server" ValidationGroup="editVal"
                                                                                                                                Width="100px" MaxLength="10"></asp:TextBox>
                                                                                                                            <cc1:CalendarExtender ID="CalendarExtender4" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                                                                                PopupButtonID="ImageButton4" PopupPosition="BottomLeft" TargetControlID="txtTransDateAdd4">
                                                                                                                            </cc1:CalendarExtender>


                                                                                                                        </td>
                                                                                                                        <td style="width: 4%">
                                                                                                                            <asp:ImageButton ID="ImageButton4" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                                                                                Width="20px" runat="server" />
                                                                                                                        </td>
                                                                                                                        <td class="ControlDrpBorder" align="left" style="width: 15%">
                                                                                                                            <asp:DropDownList ID="cmbDebtorAdd4" runat="server" BackColor="#90c9fc" CssClass="drpDownListMedium" Width="100%" AutoPostBack="False"
                                                                                                                                DataValueField="LedgerID" DataTextField="LedgerName" Style="border: 1px solid #90c9fc" Height="26px"
                                                                                                                                AppendDataBoundItems="true" ValidationGroup="addVal">
                                                                                                                                <asp:ListItem Text="Select Debtor" style="background-color: #90c9fc" Value="0"></asp:ListItem>
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td style="width: 1%"></td>
                                                                                                                        <td class="ControlDrpBorder" style="width: 15%">
                                                                                                                            <asp:DropDownList ID="cmbCreditorAdd4" runat="server" BackColor="#90c9fc" CssClass="drpDownListMedium" Width="100%" AutoPostBack="False"
                                                                                                                                DataValueField="LedgerID" DataTextField="LedgerName" Style="border: 1px solid #90c9fc" Height="26px"
                                                                                                                                AppendDataBoundItems="true" ValidationGroup="addVal">
                                                                                                                                <asp:ListItem Text="Select Creditor" style="background-color: #90c9fc" Value="0"></asp:ListItem>
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td style="width: 1%"></td>
                                                                                                                        <td class="ControlTextBoxSmall" style="width: 6%">
                                                                                                                            <asp:TextBox ID="txtAmountAdd4" runat="server" ValidationGroup="addVal"
                                                                                                                                CssClass="cssTextBoxReport2"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 1%"></td>
                                                                                                                        <td class="ControlTextBox3" style="width: 34%">
                                                                                                                            <asp:TextBox ID="txtNarrAdd4" ValidationGroup="addVal" runat="server" MaxLength="200"
                                                                                                                                CssClass="cssTextBox"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr style="height: 1px">
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td style="width: 2%"></td>
                                                                                                                        <td style="width: 5%" class="ControlTextBoxSmall">
                                                                                                                            <asp:TextBox ID="txtRefnumAdd5" ValidationGroup="addVal" runat="server"
                                                                                                                                CssClass="cssTextBoxReport2"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 1%"></td>
                                                                                                                        <td class="ControlTextBoxSmall" style="width: 5%">
                                                                                                                            <asp:TextBox ID="txtTransDateAdd5" Enabled="false" CssClass="cssTextBoxReport2" runat="server" ValidationGroup="editVal"
                                                                                                                                Width="100px" MaxLength="10"></asp:TextBox>
                                                                                                                            <cc1:CalendarExtender ID="CalendarExtender5" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                                                                                PopupButtonID="ImageButton5" PopupPosition="BottomLeft" TargetControlID="txtTransDateAdd5">
                                                                                                                            </cc1:CalendarExtender>
                                                                                                                        </td>
                                                                                                                        <td style="width: 4%">
                                                                                                                            <asp:ImageButton ID="ImageButton5" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                                                                                Width="20px" runat="server" />
                                                                                                                        </td>
                                                                                                                        <td class="ControlDrpBorder" style="width: 15%">
                                                                                                                            <asp:DropDownList ID="cmbDebtorAdd5" runat="server" BackColor="#90c9fc" CssClass="drpDownListMedium" Width="100%" AutoPostBack="False"
                                                                                                                                DataValueField="LedgerID" DataTextField="LedgerName" Style="border: 1px solid #90c9fc" Height="26px"
                                                                                                                                AppendDataBoundItems="true" ValidationGroup="addVal">
                                                                                                                                <asp:ListItem Text="Select Debtor" style="background-color: #90c9fc" Value="0"></asp:ListItem>
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td style="width: 1%"></td>
                                                                                                                        <td class="ControlDrpBorder" style="width: 15%">
                                                                                                                            <asp:DropDownList ID="cmbCreditorAdd5" runat="server" BackColor="#90c9fc" CssClass="drpDownListMedium" Width="100%" AutoPostBack="False"
                                                                                                                                DataValueField="LedgerID" DataTextField="LedgerName" Style="border: 1px solid #90c9fc" Height="26px"
                                                                                                                                AppendDataBoundItems="true" ValidationGroup="addVal">
                                                                                                                                <asp:ListItem Text="Select Creditor" style="background-color: #90c9fc" Value="0"></asp:ListItem>
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td style="width: 1%"></td>
                                                                                                                        <td class="ControlTextBoxSmall" style="width: 6%">
                                                                                                                            <asp:TextBox ID="txtAmountAdd5" runat="server" ValidationGroup="addVal"
                                                                                                                                CssClass="cssTextBoxReport2"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 1%"></td>
                                                                                                                        <td class="ControlTextBox3" style="width: 34%">
                                                                                                                            <asp:TextBox ID="txtNarrAdd5" ValidationGroup="addVal" runat="server" MaxLength="200"
                                                                                                                                CssClass="cssTextBox"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr style="height: 1px">
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td style="width: 2%"></td>
                                                                                                                        <td style="width: 5%" class="ControlTextBoxSmall">
                                                                                                                            <asp:TextBox ID="txtRefnumAdd6" ValidationGroup="addVal" runat="server"
                                                                                                                                CssClass="cssTextBoxReport2"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 1%"></td>
                                                                                                                        <td class="ControlTextBoxSmall" style="width: 5%">
                                                                                                                            <asp:TextBox ID="txtTransDateAdd6" Enabled="false" CssClass="cssTextBoxReport2" runat="server" ValidationGroup="editVal"
                                                                                                                                Width="100px" MaxLength="10"></asp:TextBox>
                                                                                                                            <cc1:CalendarExtender ID="CalendarExtender6" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                                                                                PopupButtonID="ImageButton6" PopupPosition="BottomLeft" TargetControlID="txtTransDateAdd6">
                                                                                                                            </cc1:CalendarExtender>
                                                                                                                        </td>
                                                                                                                        <td style="width: 4%">
                                                                                                                            <asp:ImageButton ID="ImageButton6" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                                                                                Width="20px" runat="server" />
                                                                                                                        </td>
                                                                                                                        <td class="ControlDrpBorder" style="width: 15%">
                                                                                                                            <asp:DropDownList ID="cmbDebtorAdd6" runat="server" BackColor="#90c9fc" CssClass="drpDownListMedium" Width="100%" AutoPostBack="False"
                                                                                                                                DataValueField="LedgerID" DataTextField="LedgerName" Style="border: 1px solid #90c9fc" Height="26px"
                                                                                                                                AppendDataBoundItems="true" ValidationGroup="addVal">
                                                                                                                                <asp:ListItem Text="Select Debtor" style="background-color: #90c9fc" Value="0"></asp:ListItem>
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>

                                                                                                                        <td style="width: 1%"></td>
                                                                                                                        <td class="ControlDrpBorder" style="width: 15%">
                                                                                                                            <asp:DropDownList ID="cmbCreditorAdd6" runat="server" BackColor="#90c9fc" CssClass="drpDownListMedium" Width="100%" AutoPostBack="False"
                                                                                                                                DataValueField="LedgerID" DataTextField="LedgerName" Style="border: 1px solid #90c9fc" Height="26px"
                                                                                                                                AppendDataBoundItems="true" ValidationGroup="addVal">
                                                                                                                                <asp:ListItem Text="Select Creditor" style="background-color: #90c9fc" Value="0"></asp:ListItem>
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td style="width: 1%"></td>
                                                                                                                        <td class="ControlTextBoxSmall" style="width: 6%">
                                                                                                                            <asp:TextBox ID="txtAmountAdd6" runat="server" ValidationGroup="addVal"
                                                                                                                                CssClass="cssTextBoxReport2"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 1%"></td>
                                                                                                                        <td class="ControlTextBox3" style="width: 34%">
                                                                                                                            <asp:TextBox ID="txtNarrAdd6" ValidationGroup="addVal" runat="server" MaxLength="200"
                                                                                                                                CssClass="cssTextBox"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr style="height: 5px">
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td colspan="12">
                                                                                                                            <%--<br />--%>
                                                                                                                            <asp:UpdatePanel ID="UpdatePanel14" runat="server" UpdateMode="Conditional">
                                                                                                                                <ContentTemplate>
                                                                                                                                    <table width="100%">
                                                                                                                                        <tr>
                                                                                                                                            <td style="width: 13px"></td>
                                                                                                                                            <td style="text-align: left;"></td>
                                                                                                                                            <td style="text-align: center">
                                                                                                                                                <div style="text-align: center">
                                                                                                                                                    <asp:Panel ID="PanelCmd" runat="server">
                                                                                                                                                        <table align="center">

                                                                                                                                                            <tr>
                                                                                                                                                                <td style="padding: 1px;">
                                                                                                                                                                    <asp:Button ID="cmdUpdate" ValidationGroup="purchaseval" runat="server" Text="" CssClass="Updatebutton1231"
                                                                                                                                                                        EnableTheming="false" OnClick="cmdUpdate_Click" SkinID="skinBtnSave" />
                                                                                                                                                                    <asp:Button ID="cmdSave" ValidationGroup="purchaseval" runat="server" Text="" CssClass="savebutton1231"
                                                                                                                                                                        EnableTheming="false" OnClick="cmdSave_Click" SkinID="skinBtnSave" />
                                                                                                                                                                </td>
                                                                                                                                                                <td style="padding: 1px;">
                                                                                                                                                                    <asp:Button ID="btnCancel" runat="server" Text="" CssClass="cancelbutton6" EnableTheming="false"
                                                                                                                                                                        Visible="true" OnClick="cmdCancel_Click" SkinID="skinBtnCancel" />
                                                                                                                                                                </td>
                                                                                                                                                                <td>
                                                                                                                                                                    <asp:Button ID="Button1" runat="server" Text="" CssClass="ClearFilter666" EnableTheming="false"
                                                                                                                                                                        Visible="true" OnClick="Button1_Click" SkinID="skinBtnCancel" />
                                                                                                                                                                    <asp:TextBox ID="txtnum" runat="server"
                                                                                                                                                                        SkinID="skinTxtBoxGrid" Visible="False"></asp:TextBox>
                                                                                                                                                                </td>
                                                                                                                                                            </tr>

                                                                                                                                                        </table>

                                                                                                                                                    </asp:Panel>
                                                                                                                                                </div>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                    </table>
                                                                                                                                </ContentTemplate>
                                                                                                                                <Triggers>
                                                                                                                                    <asp:AsyncPostBackTrigger ControlID="cmdSave" EventName="Click" />
                                                                                                                                    <asp:AsyncPostBackTrigger ControlID="cmdUpdate" EventName="Click" />
                                                                                                                                </Triggers>
                                                                                                                            </asp:UpdatePanel>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
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
                                                                        <div>
                                                                            <table>
                                                                                <tr>
                                                                                    <td align="left" colspan="5">
                                                                                        <cc1:ModalPopupExtender ID="ModalPopupContact" runat="server" BackgroundCssClass="modalBackground"
                                                                                            CancelControlID="CancelPopUpContact" DynamicServicePath="" Enabled="True" PopupControlID="pnlContact"
                                                                                            TargetControlID="ShowPopUpContact">
                                                                                        </cc1:ModalPopupExtender>
                                                                                        <input id="ShowPopUpContact" type="button" style="display: none" runat="server" />
                                                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;<input id="CancelPopUpContact" runat="server" style="display: none"
                                                                                            type="button" />
                                                                                        </input>
                                                        </input>
                                                        &nbsp;<asp:ValidationSummary ID="VSContact" runat="server" Font-Names="'Trebuchet MS'" Font-Size="12pt"
                                                            HeaderText="" ShowMessageBox="true" ShowSummary="true" ValidationGroup="contact" />
                                                                                        <asp:Panel ID="pnlContact" runat="server" Width="700px" CssClass="modalPopup">
                                                                                            <asp:UpdatePanel ID="updatePnlContact" runat="server" UpdateMode="Conditional">
                                                                                                <ContentTemplate>
                                                                                                    <asp:Panel ID="pnlContactItems" CssClass="pnlPopUp" runat="server">
                                                                                                        <div id="Div2">
                                                                                                            <table cellpadding="2" cellspacing="1" style="border: 1px solid blue; width: 100%;">
                                                                                                                <tr>
                                                                                                                    <td colspan="6" class="headerPopUp">Journal Details
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr style="height: 5px">
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td class="ControlLabelproject" style="width: 20%">Ref. No. *
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRefnum"
                                                                                            ValidationGroup="editVal" Display="Dynamic" EnableClientScript="true" ErrorMessage="Please enter Ref. No. It cannot be left blank.">*</asp:RequiredFieldValidator>
                                                                                                                    </td>
                                                                                                                    <td class="ControlTextBox3" style="width: 27%">
                                                                                                                        <asp:TextBox ID="txtRefnum" runat="server" ValidationGroup="editVal"
                                                                                                                            Width="95%" CssClass="cssTextBox"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                    <td></td>
                                                                                                                    <td class="ControlLabelproject" style="width: 15%">Date *
                                                                                        <asp:RequiredFieldValidator ID="rvStock" runat="server" ValidationGroup="editVal"
                                                                                            ControlToValidate="txtTransDate" Display="Dynamic" EnableClientScript="true"
                                                                                            ErrorMessage="Please select Date. It cannot be left blank.">*</asp:RequiredFieldValidator>
                                                                                                                        <%--<asp:RangeValidator ID="myRangeValidator" runat="server" ControlToValidate="txtTransDate"
                                                                                            Display="Dynamic" EnableClientScript="true" ErrorMessage="Journal date cannot be future date."
                                                                                            ValidationGroup="editVal" Text="*" Type="Date"></asp:RangeValidator>--%>
                                                                                                                    </td>
                                                                                                                    <td class="ControlNumberBox3" style="width: 27%">
                                                                                                                        <%--<asp:UpdatePanel ID="UP12" runat="server" UpdateMode="Conditional">
                                                                                            <Triggers>
                                                                                                <asp:AsyncPostBackTrigger ControlID="txtTransDate" EventName="TextChanged" />
                                                                                            </Triggers>
                                                                                            <ContentTemplate>--%>
                                                                                                                        <asp:TextBox ID="txtTransDate" Enabled="false" runat="server" ValidationGroup="editVal"
                                                                                                                            MaxLength="10" CssClass="cssTextBox"></asp:TextBox>
                                                                                                                        <cc1:CalendarExtender ID="calExt3" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                                                                            PopupButtonID="btnD3" PopupPosition="BottomLeft" TargetControlID="txtTransDate">
                                                                                                                        </cc1:CalendarExtender>
                                                                                                                        <%--<asp:HiddenField ID="hddatecheck1" runat="server" Value="0" />
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>--%>
                                                                                                                    </td>
                                                                                                                    <td align="left">
                                                                                                                        <asp:ImageButton ID="btnD3" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                                                                            Width="20px" runat="server" />
                                                                                                                        <asp:Label ID="lblBillNo" runat="server" Visible="false"></asp:Label>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr style="height: 1px">
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td class="ControlLabelproject" style="width: 20%">Debtor *
                                                                                        <asp:RequiredFieldValidator ID="reqDebtor" ErrorMessage="Please select Debtor. It cannot be left blank." InitialValue="0"
                                                                                            EnableClientScript="true" Text="*" ControlToValidate="cmbDebtor" runat="server"
                                                                                            ValidationGroup="editVal" Display="Dynamic" />
                                                                                                                    </td>
                                                                                                                    <td align="left" class="ControlDrpBorder" style="width: 27%">
                                                                                                                        <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                                                                                                            <ContentTemplate>
                                                                                                                                <asp:DropDownList ID="cmbDebtor" OnSelectedIndexChanged="cmbDebtor_SelectedIndexChanged" runat="server" BackColor="#e7e7e7" CssClass="drpDownListMedium" Width="100%" AutoPostBack="true"
                                                                                                                                    DataValueField="LedgerID" DataTextField="LedgerName" Style="border: 1px solid #e7e7e7" Height="26px"
                                                                                                                                    AppendDataBoundItems="true" ValidationGroup="editVal">
                                                                                                                                    <asp:ListItem Text="Select Debtor" style="background-color: #e7e7e7" Value="0"></asp:ListItem>
                                                                                                                                </asp:DropDownList>
                                                                                                                            </ContentTemplate>
                                                                                                                        </asp:UpdatePanel>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                                                                                                            <ContentTemplate>
                                                                                                                                <asp:Label runat="server" ID="unitofmeasureheading"></asp:Label>
                                                                                                                            </ContentTemplate>
                                                                                                                        </asp:UpdatePanel>
                                                                                                                    </td>
                                                                                                                    <td class="ControlLabelproject" style="width: 15%">Creditor *
                                                                                        <asp:RequiredFieldValidator ID="reqCreditor" ErrorMessage="Please select Creditor. It cannot be left blank."
                                                                                            EnableClientScript="true" InitialValue="0" ControlToValidate="cmbCreditor" runat="server"
                                                                                            ValidationGroup="editVal" Text="*" Display="Dynamic" />
                                                                                                                    </td>
                                                                                                                    <td align="left" class="ControlDrpBorder" style="width: 27%">
                                                                                                                        <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                                                                                                                            <ContentTemplate>
                                                                                                                                <asp:DropDownList ID="cmbCreditor" OnSelectedIndexChanged="cmbCreditor_SelectedIndexChanged" runat="server" BackColor="#e7e7e7" CssClass="drpDownListMedium" Width="100%" AutoPostBack="true"
                                                                                                                                    DataValueField="LedgerID" DataTextField="LedgerName" Style="border: 1px solid #e7e7e7" Height="26px"
                                                                                                                                    AppendDataBoundItems="true" ValidationGroup="editVal">
                                                                                                                                    <asp:ListItem Text="Select Creditor" style="background-color: #e7e7e7" Value="0"></asp:ListItem>
                                                                                                                                </asp:DropDownList>
                                                                                                                            </ContentTemplate>
                                                                                                                        </asp:UpdatePanel>
                                                                                                                    </td>
                                                                                                                    <td align="left" style="width: 7%">
                                                                                                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                                                                                                            <ContentTemplate>
                                                                                                                                <asp:Label runat="server" ID="unit"></asp:Label>
                                                                                                                            </ContentTemplate>
                                                                                                                        </asp:UpdatePanel>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr style="height: 1px">
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td class="ControlLabelproject" style="width: 20%">Amount *
                                                                                        <asp:RequiredFieldValidator ID="rvModel" runat="server" ControlToValidate="txtAmount"
                                                                                            Display="Dynamic" EnableClientScript="true" ValidationGroup="editVal" ErrorMessage="Please enter Amount. it cannot be left blank.">*</asp:RequiredFieldValidator>
                                                                                                                        <cc1:FilteredTextBoxExtender ID="fltAmt" runat="server" TargetControlID="txtAmount"
                                                                                                                            ValidChars="." FilterType="Numbers, Custom" />
                                                                                                                    </td>
                                                                                                                    <td class="ControlTextBox3" style="width: 27%">
                                                                                                                        <asp:TextBox ID="txtAmount" runat="server" ValidationGroup="editVal"
                                                                                                                            Width="95%" CssClass="cssTextBox"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                    <td></td>
                                                                                                                    <td class="ControlLabelproject" style="width: 15%">Narration *
                                                                                        <asp:RequiredFieldValidator ID="rvNarration" runat="server" ControlToValidate="txtNarr"
                                                                                            ValidationGroup="editVal" Display="Dynamic" EnableClientScript="true" ErrorMessage="Please enter Narration. It cannot be left blank.">*</asp:RequiredFieldValidator>
                                                                                                                    </td>
                                                                                                                    <td class="ControlTextBox3" style="width: 27%">
                                                                                                                        <asp:TextBox ID="txtNarr" runat="server" ValidationGroup="editVal"
                                                                                                                            MaxLength="200" TextMode="MultiLine" Height="30px" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                    <td></td>
                                                                                                                </tr>
                                                                                                                <tr style="height: 7px">
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td colspan="6">
                                                                                                                        <table style="width: 100%;">
                                                                                                                            <tr>
                                                                                                                                <td style="width: 37%;"></td>
                                                                                                                                <td style="width: 18%;">
                                                                                                                                    <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update"
                                                                                                                                        CssClass="savebutton1231" EnableTheming="false" ValidationGroup="editVal" SkinID="skinBtnSave"
                                                                                                                                        OnClick="UpdateButton_Click"></asp:Button>
                                                                                                                                </td>
                                                                                                                                <td style="width: 18%;">

                                                                                                                                    <asp:Button ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                                                                        CssClass="cancelbutton6" EnableTheming="false" SkinID="skinBtnCancel" OnClick="UpdateCancelButton_Click"></asp:Button>
                                                                                                                                </td>
                                                                                                                                <td style="width: 27%;"></td>
                                                                                                                            </tr>
                                                                                                                        </table>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </div>
                                                                                                        <table cellspacing="0">
                                                                                                            <tr>
                                                                                                                <td></td>
                                                                                                                <td>
                                                                                                                    <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="BulletList" ShowMessageBox="true"
                                                                                                                        ShowSummary="false" HeaderText="" ValidationGroup="editVal"
                                                                                                                        Font-Names="'Trebuchet MS'" Font-Size="12" runat="server" />
                                                                                                                </td>
                                                                                                                <td></td>
                                                                                                                <td></td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </asp:Panel>
                                                                                                </ContentTemplate>
                                                                                            </asp:UpdatePanel>
                                                                                        </asp:Panel>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                        <div>
                                                                            <table>
                                                                                <tr>
                                                                                    <td align="left" colspan="5">
                                                                                        <cc1:ModalPopupExtender ID="ModalPopupExtender3" runat="server" BackgroundCssClass="modalBackground"
                                                                                            CancelControlID="Button8" DynamicServicePath="" Enabled="True" PopupControlID="Panel8"
                                                                                            TargetControlID="Button9">
                                                                                        </cc1:ModalPopupExtender>
                                                                                        <input id="Button8" type="button" style="display: none" runat="server" />
                                                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;<input id="Button9" runat="server" style="display: none"
                                                                                            type="button" />
                                                                                        </input>
                                                        </input>
                                                        &nbsp;<asp:ValidationSummary ID="ValidationSummary5" runat="server" Font-Names="'Trebuchet MS'" Font-Size="12pt"
                                                            HeaderText="" ShowMessageBox="true" ShowSummary="true" ValidationGroup="contact" />
                                                                                        <asp:Panel ID="Panel8" runat="server" Width="85%" CssClass="modalPopup">
                                                                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                                                                                <ContentTemplate>
                                                                                                    <asp:Panel ID="Panel9" CssClass="pnlPopUp" runat="server">
                                                                                                        <div id="Div312">
                                                                                                            <table cellpadding="2" cellspacing="1" style="border: 1px solid blue; width: 100%;">
                                                                                                                <tr>
                                                                                                                    <td colspan="5" class="headerPopUp">Multiple Journal Details
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr style="height: 5px">
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td colspan="5">
                                                                                                                        <table style="width: 100%;">
                                                                                                                            <tr>
                                                                                                                                <td>
                                                                                                                                    <div style="height: 300px; overflow: scroll">
                                                                                                                                        <asp:GridView ID="gdm" AutoGenerateColumns="False" BorderWidth="1px" ShowFooter="True"
                                                                                                                                            BorderStyle="Solid" OnRowDataBound="gdm_RowDataBound" OnRowDeleting="gdm_RowDeleting" GridLines="Both" SaveButtonID="SaveButton" runat="server" CssClass="someClass"
                                                                                                                                            Width="100%">
                                                                                                                                            <RowStyle CssClass="dataRow" />
                                                                                                                                            <SelectedRowStyle CssClass="SelectdataRow" />
                                                                                                                                            <AlternatingRowStyle CssClass="altRow" />
                                                                                                                                            <EmptyDataRowStyle CssClass="HeadataRow" Font-Bold="true" />
                                                                                                                                            <HeaderStyle CssClass="HeadataRow" Wrap="false" />
                                                                                                                                            <FooterStyle CssClass="dataRow" />
                                                                                                                                            <Columns>
                                                                                                                                                <asp:BoundField DataField="RowNumber" HeaderText="SNo" ItemStyle-Width="5px" />
                                                                                                                                                <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="RefNo" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="10px">
                                                                                                                                                    <ItemTemplate>
                                                                                                                                                        <asp:TextBox ID="txtRefNoM" runat="server" Width="95%" Height="26px"></asp:TextBox>
                                                                                                                                                    </ItemTemplate>
                                                                                                                                                </asp:TemplateField>
                                                                                                                                                <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Date" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="70px">
                                                                                                                                                    <ItemTemplate>
                                                                                                                                                        <asp:TextBox ID="txtDateM" Enabled="false" runat="server" Width="70%" Height="26px"></asp:TextBox>
                                                                                                                                                        <cc1:CalendarExtender ID="calBillDate" runat="server" Format="dd/MM/yyyy"
                                                                                                                                                            PopupButtonID="btnBillDate123" TargetControlID="txtDateM" Enabled="True">
                                                                                                                                                        </cc1:CalendarExtender>
                                                                                                                                                        <asp:ImageButton ID="btnBillDate123" ImageUrl="App_Themes/NewTheme/images/cal.gif"
                                                                                                                                                            CausesValidation="False" Width="20px" runat="server" />
                                                                                                                                                    </ItemTemplate>
                                                                                                                                                </asp:TemplateField>
                                                                                                                                                <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Debtor" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="80px">
                                                                                                                                                    <ItemTemplate>
                                                                                                                                                        <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                                                                                                                                                            <ContentTemplate>
                                                                                                                                                                <asp:DropDownList ID="drpDebtorM" OnSelectedIndexChanged="drpDebtorM_SelectedIndexChanged" runat="server" BackColor="#e7e7e7" CssClass="drpDownListMedium" Width="100%" AutoPostBack="true"
                                                                                                                                                                    DataValueField="LedgerID" DataTextField="LedgerName" Style="border: 1px solid Gray" Height="28px"
                                                                                                                                                                    AppendDataBoundItems="true">
                                                                                                                                                                    <asp:ListItem Text="Select Debtor" style="background-color: #e7e7e7" Value="0"></asp:ListItem>
                                                                                                                                                                </asp:DropDownList>
                                                                                                                                                            </ContentTemplate>
                                                                                                                                                        </asp:UpdatePanel>
                                                                                                                                                        <asp:UpdatePanel ID="UpdatePanel123" runat="server" UpdateMode="Conditional">
                                                                                                                                                            <ContentTemplate>
                                                                                                                                                                <asp:Label runat="server" ID="unitmultiple"  Width="20px"></asp:Label>
                                                                                                                                                            </ContentTemplate>
                                                                                                                                                        </asp:UpdatePanel>
                                                                                                                                                    </ItemTemplate>
                                                                                                                                                </asp:TemplateField>
                                                                                                                                                <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Creditor" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="80px">
                                                                                                                                                    <ItemTemplate>
                                                                                                                                                        <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                                                                                                                                                            <ContentTemplate>
                                                                                                                                                        <asp:DropDownList ID="drpCreditorM" OnSelectedIndexChanged="drpCreditorM_SelectedIndexChanged" runat="server" BackColor="White" CssClass="drpDownListMedium" Width="100%" AutoPostBack="true"
                                                                                                                                                            DataValueField="LedgerID" DataTextField="LedgerName" Style="border: 1px solid Gray" Height="28px"
                                                                                                                                                            AppendDataBoundItems="true">
                                                                                                                                                            <asp:ListItem Text="Select Creditor" style="background-color: #e7e7e7" Value="0"></asp:ListItem>
                                                                                                                                                        </asp:DropDownList>
                                                                                                                                                                 </ContentTemplate>
                                                                                                                                                        </asp:UpdatePanel>
                                                                                                                                                        <asp:UpdatePanel ID="UpdatePanel1234" runat="server" UpdateMode="Conditional">
                                                                                                                                                            <ContentTemplate>
                                                                                                                                                                <asp:Label runat="server" ID="unitmultiplecre"  Width="20px"></asp:Label>
                                                                                                                                                            </ContentTemplate>
                                                                                                                                                        </asp:UpdatePanel>
                                                                                                                                                    </ItemTemplate>
                                                                                                                                                </asp:TemplateField>
                                                                                                                                                <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Amount" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="20px">
                                                                                                                                                    <ItemTemplate>
                                                                                                                                                        <asp:TextBox ID="txtAmountM" runat="server" Width="90%" Height="26px"></asp:TextBox>
                                                                                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBox312" runat="server" TargetControlID="txtAmountM"
                                                                                                                                                            ValidChars="." FilterType="Numbers, Custom" />
                                                                                                                                                    </ItemTemplate>
                                                                                                                                                </asp:TemplateField>
                                                                                                                                                <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Narration" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="30px">
                                                                                                                                                    <ItemTemplate>
                                                                                                                                                        <asp:TextBox ID="txtNarrationM" runat="server" Width="90%" Height="26px"></asp:TextBox>
                                                                                                                                                    </ItemTemplate>
                                                                                                                                                    <FooterStyle HorizontalAlign="Right" />
                                                                                                                                                    <FooterTemplate>
                                                                                                                                                        <asp:Button ID="ButtonAdd" runat="server" AutoPostback="false" EnableTheming="false"
                                                                                                                                                            Text="Add New Row" OnClick="ButtonAdd_Click" Width="40%" />
                                                                                                                                                    </FooterTemplate>

                                                                                                                                                </asp:TemplateField>
                                                                                                                                                <asp:CommandField ShowDeleteButton="True" HeaderStyle-Width="10px" />
                                                                                                                                            </Columns>
                                                                                                                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                                                                                                            <RowStyle BackColor="#EFF3FB" />
                                                                                                                                            <EditRowStyle BackColor="#2461BF" />
                                                                                                                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                                                                                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                                                                                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                                                                                                            <AlternatingRowStyle BackColor="White" />
                                                                                                                                        </asp:GridView>
                                                                                                                                    </div>
                                                                                                                                    <%--<asp:GridView ID="grvStudentDetails" runat="server" Width="100%"
                                                                                                    ShowFooter="True" AutoGenerateColumns="False"
                                                                                                    CellPadding="4" ForeColor="#333333"
                                                                                                    GridLines="None" OnRowDeleting="grvStudentDetails_RowDeleting">
                                                                                                    <Columns>
                                                                                                        <asp:BoundField DataField="RowNumber" HeaderText="SNo" ItemStyle-Width="50px" />
                                                                                                        <asp:TemplateField HeaderText="Ref No" ItemStyle-Width="1px">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:TextBox ID="txtRefNo" runat="server" Width="150px"></asp:TextBox>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="Creditor" ItemStyle-Width="1px">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:DropDownList ID="drpPrd" runat="server" CssClass="chzn-select">
                                                                                                                    <asp:ListItem Value="G">Item1</asp:ListItem>
                                                                                                                    <asp:ListItem Value="P">Item2</asp:ListItem>
                                                                                                                </asp:DropDownList>

                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        
                                                                                                        <asp:TemplateField HeaderText="Rate" ItemStyle-Width="1px">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:TextBox ID="txtRate" runat="server" Width="50px"></asp:TextBox>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="Quantity" ItemStyle-Width="1px">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:TextBox ID="txtQty" runat="server" Width="50px"></asp:TextBox>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="Executive Commission" ItemStyle-Width="1px">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:TextBox ID="txtExeComm" runat="server" Width="50px"></asp:TextBox>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="Disc(%)" ItemStyle-Width="1px">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:TextBox ID="txtDisPre" runat="server" Width="50px"></asp:TextBox>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="VAT(%)" ItemStyle-Width="1px">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:TextBox ID="txtVATPre" runat="server" Width="50px"></asp:TextBox>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="CST(%)" ItemStyle-Width="1px">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:TextBox ID="txtCSTPre" runat="server" Width="50px"></asp:TextBox>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="VAT Amount" ItemStyle-Width="1px">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:TextBox ID="txtVATAmt" runat="server" Width="50px"></asp:TextBox>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="Rate with VAT" ItemStyle-Width="1px">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:TextBox ID="txtRtVAT" runat="server" Width="50px"></asp:TextBox>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="Total" ItemStyle-Width="1px">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:TextBox ID="txtTotal" runat="server" Width="50px"></asp:TextBox>
                                                                                                            </ItemTemplate>
                                                                                                            <FooterStyle HorizontalAlign="Right" />
                                                                                                            <FooterTemplate>
                                                                                                                <asp:Button ID="ButtonAdd" runat="server" AutoPostback="false" EnableTheming="false"
                                                                                                                    Text="Add New Row" OnClick="ButtonAdd_Click" />
                                                                                                            </FooterTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:CommandField ShowDeleteButton="True" />
                                                                                                    </Columns>
                                                                                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                                                                    <RowStyle BackColor="#EFF3FB" />
                                                                                                    <EditRowStyle BackColor="#2461BF" />
                                                                                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                                                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                                                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                                                                    <AlternatingRowStyle BackColor="White" />
                                                                                                </asp:GridView>--%>

                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                        </table>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr style="height: 7px">
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td colspan="4">
                                                                                                                        <table style="width: 100%;">
                                                                                                                            <tr>
                                                                                                                                <td style="width: 34%;"></td>
                                                                                                                                <td style="width: 18%;">
                                                                                                                                    <asp:Button ID="Save1" runat="server" CausesValidation="false" CommandName="Update"
                                                                                                                                        CssClass="savebutton1231" EnableTheming="false" SkinID="skinBtnSave"
                                                                                                                                        OnClick="Save1_Click"></asp:Button>

                                                                                                                                </td>
                                                                                                                                <td style="width: 18%;">
                                                                                                                                    <asp:Button ID="Cancel1" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                                                                        CssClass="cancelbutton6" EnableTheming="false" SkinID="skinBtnCancel" OnClick="Cancel1_Click"></asp:Button>
                                                                                                                                </td>
                                                                                                                                <td style="width: 30%;"></td>
                                                                                                                            </tr>
                                                                                                                        </table>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </div>
                                                                                                        <table cellspacing="0">
                                                                                                            <tr>
                                                                                                                <td></td>
                                                                                                                <td>
                                                                                                                    <asp:ValidationSummary ID="ValidationSummary6" DisplayMode="BulletList" ShowMessageBox="true"
                                                                                                                        ShowSummary="false" HeaderText="" ValidationGroup="editVal"
                                                                                                                        Font-Names="'Trebuchet MS'" Font-Size="12" runat="server" />
                                                                                                                </td>
                                                                                                                <td></td>
                                                                                                                <td></td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </asp:Panel>
                                                                                                </ContentTemplate>
                                                                                            </asp:UpdatePanel>
                                                                                        </asp:Panel>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                        <div>
                                                                            <table>
                                                                                <tr>
                                                                                    <td align="left" colspan="5">
                                                                                        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                                                                                            CancelControlID="Button3" DynamicServicePath="" Enabled="True" PopupControlID="Panel2"
                                                                                            TargetControlID="Button2">
                                                                                        </cc1:ModalPopupExtender>
                                                                                        <input id="Button2" type="button" style="display: none" runat="server" />
                                                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;<input id="Button3" runat="server" style="display: none"
                                                                                            type="button" />
                                                                                        </input>
                                                        </input>
                                                        &nbsp;<asp:ValidationSummary ID="ValidationSummary3" runat="server" Font-Names="'Trebuchet MS'" Font-Size="12pt"
                                                            HeaderText="" ShowMessageBox="true" ShowSummary="true" ValidationGroup="ValidationSummary3" />
                                                                                        <asp:Panel ID="Panel2" runat="server" Width="85%" CssClass="modalPopup">
                                                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                                                                <ContentTemplate>
                                                                                                    <asp:Panel ID="Panel3" CssClass="pnlPopUp" runat="server">
                                                                                                        <div id="Div123">
                                                                                                            <table cellpadding="2" cellspacing="1" style="border: 1px solid blue; width: 100%;">
                                                                                                                <tr>
                                                                                                                    <td colspan="5" class="headerPopUp">Debtor Contra
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr style="height: 10px">
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td class="ControlLabel" style="width: 35%">Debtor *
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="Please select Debtor. It cannot be left blank." InitialValue="0"
                                                                                            EnableClientScript="true" Text="*" ControlToValidate="drpDebtor" runat="server"
                                                                                            ValidationGroup="ValidationSummary3" Display="Dynamic" />
                                                                                                                    </td>
                                                                                                                    <td align="left" class="ControlDrpBorder" style="width: 25%">
                                                                                                                         <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                                                                                                                                                            <ContentTemplate>
                                                                                                                        <asp:DropDownList ID="drpDebtor" OnSelectedIndexChanged="drpDebtor_SelectedIndexChanged" runat="server"  BackColor="White" CssClass="drpDownListMedium" Width="100%" AutoPostBack="true"
                                                                                                                            DataValueField="LedgerID" DataTextField="LedgerName" Style="border: 1px solid #e7e7e7" Height="26px"
                                                                                                                            AppendDataBoundItems="true" ValidationGroup="ValidationSummary3">
                                                                                                                            <asp:ListItem Text="Select Debtor"  style="background-color: White"  Value="0"></asp:ListItem>
                                                                                                                        </asp:DropDownList>
                                                                                                                                                                 </ContentTemplate>
                                                                                                                                                        </asp:UpdatePanel>
                                                                                                                    </td>
                                                                                                                    <td align="left" >
                                                                                                                       <asp:UpdatePanel ID="UpdatePanel12" runat="server" UpdateMode="Conditional">
                                                                                                                            <ContentTemplate>
                                                                                                                                <asp:Label runat="server" ID="unitdeb1"></asp:Label>
                                                                                                                            </ContentTemplate>
                                                                                                                        </asp:UpdatePanel>
                                                                                                                    </td>
                                                                                                                    <td align="left" style="width: 5%"></td>
                                                                                                                    <td style="width: 5%"></td>
                                                                                                                </tr>
                                                                                                                <tr style="height: 10px">
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td colspan="5">
                                                                                                                        <table style="width: 100%">
                                                                                                                            <tr>
                                                                                                                                <td>
                                                                                                                                    <div id="div" runat="server" style="height: 330px; overflow: scroll">

                                                                                                                                        <asp:GridView ID="GrdViewItems" AutoGenerateColumns="False" BorderWidth="1px" ShowFooter="True"
                                                                                                                                            BorderStyle="Solid" OnRowDataBound="GrdViewItems_RowDataBound" OnRowDeleting="GrdViewItems_RowDeleting" GridLines="Both" SaveButtonID="SaveButton" runat="server" CssClass="someClass"
                                                                                                                                            Width="100%">
                                                                                                                                            <RowStyle CssClass="dataRow" />
                                                                                                                                            <SelectedRowStyle CssClass="SelectdataRow" />
                                                                                                                                            <AlternatingRowStyle CssClass="altRow" />
                                                                                                                                            <EmptyDataRowStyle CssClass="HeadataRow" Font-Bold="true" />
                                                                                                                                            <HeaderStyle CssClass="HeadataRow" Wrap="false" />
                                                                                                                                            <FooterStyle CssClass="dataRow" />

                                                                                                                                            <%--<rwg:BulkEditGridView ID="GrdViewItems" AutoGenerateColumns="False" BorderWidth="1px"
                                                                                                            BorderStyle="Solid" OnRowDataBound="GrdViewItems_RowDataBound" GridLines="Both" SaveButtonID="SaveButton" runat="server" CssClass="someClass"
                                                                                                            Width="100%">
                                                                                                            <RowStyle CssClass="dataRow" />
                                                                                                            <SelectedRowStyle CssClass="SelectdataRow" />
                                                                                                            <AlternatingRowStyle CssClass="altRow" />
                                                                                                            <EmptyDataRowStyle CssClass="HeadataRow" Font-Bold="true" />
                                                                                                            <HeaderStyle CssClass="HeadataRow" Wrap="false" />
                                                                                                            <FooterStyle CssClass="dataRow" />--%>
                                                                                                                                            <Columns>
                                                                                                                                                <asp:BoundField DataField="RowNumber" HeaderText="SNo" ItemStyle-Width="5px" />
                                                                                                                                                <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="RefNo" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="10px">
                                                                                                                                                    <ItemTemplate>
                                                                                                                                                        <asp:TextBox ID="txtRefNo" runat="server" Width="95%" Height="26px"></asp:TextBox>
                                                                                                                                                    </ItemTemplate>
                                                                                                                                                </asp:TemplateField>
                                                                                                                                                <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Date" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="50px">
                                                                                                                                                    <ItemTemplate>
                                                                                                                                                        <asp:TextBox ID="txtDate" runat="server" Width="70%" Height="26px"></asp:TextBox>
                                                                                                                                                        <cc1:CalendarExtender ID="calBillDate" runat="server" Format="dd/MM/yyyy"
                                                                                                                                                            PopupButtonID="btnBillDate" TargetControlID="txtDate" Enabled="True">
                                                                                                                                                        </cc1:CalendarExtender>
                                                                                                                                                        <asp:ImageButton ID="btnBillDate" ImageUrl="App_Themes/NewTheme/images/cal.gif"
                                                                                                                                                            CausesValidation="False" Width="20px" runat="server" />
                                                                                                                                                    </ItemTemplate>
                                                                                                                                                </asp:TemplateField>
                                                                                                                                                <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Creditor" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="80px">
                                                                                                                                                    <ItemTemplate>
                                                                                                                                                          <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                                                                                                                                                            <ContentTemplate>
                                                                                                                                                        <asp:DropDownList ID="drpCreditor" OnSelectedIndexChanged="drpCreditor_SelectedIndexChanged" runat="server" BackColor="White" CssClass="drpDownListMedium" Width="100%" AutoPostBack="true"
                                                                                                                                                            DataValueField="LedgerID" DataTextField="LedgerName" Style="border: 1px solid Gray" Height="28px"
                                                                                                                                                            AppendDataBoundItems="true" ValidationGroup="editVal">
                                                                                                                                                            <asp:ListItem Text="Select Creditor" style="background-color: White" Value="0"></asp:ListItem>
                                                                                                                                                        </asp:DropDownList>
                                                                                                                                                                 </ContentTemplate>
                                                                                                                                                        </asp:UpdatePanel>
                                                                                                                                                        <asp:UpdatePanel ID="UpdatePanel45" runat="server" UpdateMode="Conditional">
                                                                                                                                                            <ContentTemplate>
                                                                                                                                                                <asp:Label runat="server" ID="unitdebt"  Width="20px"></asp:Label>
                                                                                                                                                            </ContentTemplate>
                                                                                                                                                        </asp:UpdatePanel>
                                                                                                                                                    </ItemTemplate>
                                                                                                                                                </asp:TemplateField>
                                                                                                                                                <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Amount" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="20px">
                                                                                                                                                    <ItemTemplate>
                                                                                                                                                        <asp:TextBox ID="txtAmount" runat="server" Width="95%" Height="26px"></asp:TextBox>
                                                                                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender312" runat="server" TargetControlID="txtAmount"
                                                                                                                                                            ValidChars="." FilterType="Numbers, Custom" />
                                                                                                                                                    </ItemTemplate>
                                                                                                                                                </asp:TemplateField>
                                                                                                                                                <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Narration" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="20px">
                                                                                                                                                    <ItemTemplate>
                                                                                                                                                        <asp:TextBox ID="txtNarration" runat="server" Width="95%" Height="26px"></asp:TextBox>
                                                                                                                                                    </ItemTemplate>
                                                                                                                                                    <FooterStyle HorizontalAlign="Right" />
                                                                                                                                                    <FooterTemplate>
                                                                                                                                                        <asp:Button ID="ButtonAdd1" runat="server" AutoPostback="false" EnableTheming="false"
                                                                                                                                                            Text="Add New Row" OnClick="ButtonAdd1_Click" Width="40%" />
                                                                                                                                                    </FooterTemplate>
                                                                                                                                                </asp:TemplateField>
                                                                                                                                                <asp:CommandField ShowDeleteButton="True" HeaderStyle-Width="10px" />
                                                                                                                                            </Columns>
                                                                                                                                        </asp:GridView>
                                                                                                                                    </div>
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                        </table>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td colspan="4">
                                                                                                                        <table style="width: 100%;">
                                                                                                                            <tr>
                                                                                                                                <td style="width: 37%;"></td>
                                                                                                                                <td style="width: 18%;">
                                                                                                                                    <asp:Button ID="UpdButton" runat="server" CausesValidation="true" CommandName="Update"
                                                                                                                                        CssClass="savebutton1231" EnableTheming="false" SkinID="skinBtnSave" ValidationGroup="ValidationSummary3"
                                                                                                                                        OnClick="UpdButton_Click"></asp:Button>
                                                                                                                                </td>
                                                                                                                                <td style="width: 18%;">

                                                                                                                                    <asp:Button ID="UpdCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                                                                        CssClass="cancelbutton6" EnableTheming="false" SkinID="skinBtnCancel" OnClick="UpdCancelButton_Click"></asp:Button>
                                                                                                                                </td>
                                                                                                                                <td style="width: 27%;"></td>
                                                                                                                            </tr>
                                                                                                                        </table>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </div>
                                                                                                    </asp:Panel>
                                                                                                </ContentTemplate>
                                                                                            </asp:UpdatePanel>
                                                                                        </asp:Panel>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                        <div>
                                                                            <table>
                                                                                <tr>
                                                                                    <td align="left" colspan="5">
                                                                                        <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
                                                                                            CancelControlID="Button5" DynamicServicePath="" Enabled="True" PopupControlID="Panel6"
                                                                                            TargetControlID="Button4">
                                                                                        </cc1:ModalPopupExtender>
                                                                                        <input id="Button4" type="button" style="display: none" runat="server" />
                                                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;<input id="Button5" runat="server" style="display: none"
                                                                                            type="button" />
                                                                                        </input>
                                                        </input>
                                                        &nbsp;<asp:ValidationSummary ID="ValidationSummary4" runat="server" Font-Names="'Trebuchet MS'" Font-Size="12pt"
                                                            HeaderText="" ShowMessageBox="true" ShowSummary="true" ValidationGroup="ValidationSummary4" />
                                                                                        <asp:Panel ID="Panel6" runat="server" Width="85%" CssClass="modalPopup">
                                                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                                                                <ContentTemplate>
                                                                                                    <asp:Panel ID="Panel7" CssClass="pnlPopUp" runat="server">
                                                                                                        <div id="Div213">
                                                                                                            <table cellpadding="2" cellspacing="1" style="border: 1px solid blue; width: 100%;">
                                                                                                                <tr>
                                                                                                                    <td colspan="5" class="headerPopUp">Creditor Contra
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr style="height: 10px">
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td class="ControlLabel" style="width: 35%">Creditor *
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ErrorMessage="Please select Creditor. It cannot be left blank." InitialValue="0"
                                                                                            EnableClientScript="true" Text="*" ControlToValidate="drpCreditor1" runat="server"
                                                                                            ValidationGroup="ValidationSummary4" Display="Dynamic" />
                                                                                                                    </td>
                                                                                                                    <td align="left" class="ControlDrpBorder" style="width: 25%">
                                                                                                                        <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                                                                                                                                                            <ContentTemplate>
                                                                                                                        <asp:DropDownList ID="drpCreditor1" OnSelectedIndexChanged="drpCreditor1_SelectedIndexChanged" runat="server" BackColor="White"  CssClass="drpDownListMedium" Width="100%" AutoPostBack="true"
                                                                                                                            DataValueField="LedgerID" DataTextField="LedgerName" Style="border: 1px solid #e7e7e7" Height="26px"
                                                                                                                            AppendDataBoundItems="true" ValidationGroup="ValidationSummary4">
                                                                                                                            <asp:ListItem Text="Select Creditor" style="background-color: White"  Value="0"></asp:ListItem>
                                                                                                                        </asp:DropDownList>
                                                                                                                     </ContentTemplate>
                                                                                                                                                        </asp:UpdatePanel>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                         <asp:UpdatePanel ID="UpdatePanel13" runat="server" UpdateMode="Conditional">
                                                                                                                            <ContentTemplate>
                                                                                                                                <asp:Label runat="server" ID="unitcre1"></asp:Label>
                                                                                                                            </ContentTemplate>
                                                                                                                        </asp:UpdatePanel>
                                                                                                                    </td>
                                                                                                                    <td class="ControlLabel" style="width: 30%"></td>
                                                                                                                    <td align="left" style="width: 5%"></td>
                                                                                                                    <td style="width: 5%"></td>
                                                                                                                </tr>
                                                                                                                <tr style="height: 10px">
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td colspan="5">
                                                                                                                        <table style="width: 100%">
                                                                                                                            <tr>
                                                                                                                                <td>
                                                                                                                                    <div id="div4" runat="server" style="height: 330px; overflow: scroll">
                                                                                                                                        <asp:GridView ID="BulkEditGridView1" AutoGenerateColumns="False" BorderWidth="1px" ShowFooter="True"
                                                                                                                                            BorderStyle="Solid" OnRowDataBound="BulkEditGridView1_RowDataBound" OnRowDeleting="BulkEditGridView1_RowDeleting" GridLines="Both" SaveButtonID="SaveButton" runat="server" CssClass="someClass"
                                                                                                                                            Width="100%">
                                                                                                                                            <RowStyle CssClass="dataRow" />
                                                                                                                                            <SelectedRowStyle CssClass="SelectdataRow" />
                                                                                                                                            <AlternatingRowStyle CssClass="altRow" />
                                                                                                                                            <EmptyDataRowStyle CssClass="HeadataRow" Font-Bold="true" />
                                                                                                                                            <HeaderStyle CssClass="HeadataRow" Wrap="false" />
                                                                                                                                            <FooterStyle CssClass="dataRow" />
                                                                                                                                            <%--<rwg:BulkEditGridView ID="BulkEditGridView1" AutoGenerateColumns="False" BorderWidth="1px"
                                                                                                            BorderStyle="Solid" OnRowDataBound="BulkEditGridView1_RowDataBound" GridLines="Both" SaveButtonID="SaveButton" runat="server" CssClass="someClass"
                                                                                                            Width="100%">
                                                                                                            <RowStyle CssClass="dataRow" />
                                                                                                            <SelectedRowStyle CssClass="SelectdataRow" />
                                                                                                            <AlternatingRowStyle CssClass="altRow" />
                                                                                                            <EmptyDataRowStyle CssClass="HeadataRow" Font-Bold="true" />
                                                                                                            <HeaderStyle CssClass="HeadataRow" Wrap="false" />
                                                                                                            <FooterStyle CssClass="dataRow" />--%>
                                                                                                                                            <Columns>
                                                                                                                                                <asp:BoundField DataField="RowNumber" HeaderText="SNo" ItemStyle-Width="5px" />
                                                                                                                                                <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="RefNo" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="20px">
                                                                                                                                                    <ItemTemplate>
                                                                                                                                                        <asp:TextBox ID="txtRefNo" runat="server" Width="95%" Height="26px"></asp:TextBox>
                                                                                                                                                    </ItemTemplate>
                                                                                                                                                </asp:TemplateField>
                                                                                                                                                <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Date" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="50px">
                                                                                                                                                    <ItemTemplate>
                                                                                                                                                        <asp:TextBox ID="txtDate" runat="server" Height="26px" Width="70%"></asp:TextBox>
                                                                                                                                                        <cc1:CalendarExtender ID="calBillDate" runat="server" Format="dd/MM/yyyy"
                                                                                                                                                            PopupButtonID="btnBillDate" TargetControlID="txtDate" Enabled="True">
                                                                                                                                                        </cc1:CalendarExtender>
                                                                                                                                                        <asp:ImageButton ID="btnBillDate" ImageUrl="App_Themes/NewTheme/images/cal.gif"
                                                                                                                                                            CausesValidation="False" Width="20px" runat="server" />
                                                                                                                                                    </ItemTemplate>
                                                                                                                                                </asp:TemplateField>
                                                                                                                                                <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Debtor" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="80px">
                                                                                                                                                    <ItemTemplate>
                                                                                                                                                            <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                                                                                                                                                            <ContentTemplate>
                                                                                                                                                        <asp:DropDownList ID="drpDebtor1" OnSelectedIndexChanged="drpDebtor1_SelectedIndexChanged" runat="server" BackColor="White" CssClass="drpDownListMedium" Width="100%" AutoPostBack="true"
                                                                                                                                                            DataValueField="LedgerID" DataTextField="LedgerName" Style="border: 1px solid Gray" Height="28px"
                                                                                                                                                            AppendDataBoundItems="true">
                                                                                                                                                            <asp:ListItem Text="Select Debtor" style="background-color: White" Value="0"></asp:ListItem>
                                                                                                                                                        </asp:DropDownList>
                                                                                                                                                                       </ContentTemplate>
                                                                                                                                                        </asp:UpdatePanel>
                                                                                                                                                        <asp:UpdatePanel ID="UpdatePanel456" runat="server" UpdateMode="Conditional">
                                                                                                                                                            <ContentTemplate>
                                                                                                                                                                <asp:Label runat="server" ID="unitdcreb"  Width="20px"></asp:Label>
                                                                                                                                                            </ContentTemplate>
                                                                                                                                                        </asp:UpdatePanel>
                                                                                                                                                    </ItemTemplate>
                                                                                                                                                </asp:TemplateField>
                                                                                                                                                <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Amount" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="20px">
                                                                                                                                                    <ItemTemplate>
                                                                                                                                                        <asp:TextBox ID="txtAmount" runat="server" Width="97%" Height="26px"></asp:TextBox>
                                                                                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender123" runat="server" TargetControlID="txtAmount"
                                                                                                                                                            ValidChars="." FilterType="Numbers, Custom" />
                                                                                                                                                    </ItemTemplate>
                                                                                                                                                </asp:TemplateField>
                                                                                                                                                <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Narration" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="20px">
                                                                                                                                                    <ItemTemplate>
                                                                                                                                                        <asp:TextBox ID="txtNarration" runat="server" Width="99%" Height="26px"></asp:TextBox>
                                                                                                                                                    </ItemTemplate>
                                                                                                                                                    <FooterStyle HorizontalAlign="Right" />
                                                                                                                                                    <FooterTemplate>
                                                                                                                                                        <asp:Button ID="ButtonAdd2" runat="server" AutoPostback="false" EnableTheming="false"
                                                                                                                                                            Text="Add New Row" OnClick="ButtonAdd2_Click" Width="40%" />
                                                                                                                                                    </FooterTemplate>
                                                                                                                                                </asp:TemplateField>
                                                                                                                                                <asp:CommandField ShowDeleteButton="True" HeaderStyle-Width="10px" />
                                                                                                                                            </Columns>
                                                                                                                                        </asp:GridView>
                                                                                                                                    </div>
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                        </table>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td colspan="4">
                                                                                                                        <table style="width: 100%;">
                                                                                                                            <tr>
                                                                                                                                <td style="width: 37%;"></td>
                                                                                                                                <td style="width: 18%;">
                                                                                                                                    <asp:Button ID="Button6" runat="server" CausesValidation="true" CommandName="Update"
                                                                                                                                        CssClass="savebutton1231" EnableTheming="false" SkinID="skinBtnSave" ValidationGroup="ValidationSummary4"
                                                                                                                                        OnClick="Button6_Click"></asp:Button>
                                                                                                                                </td>
                                                                                                                                <td style="width: 18%;">

                                                                                                                                    <asp:Button ID="Button7" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                                                                        CssClass="cancelbutton6" EnableTheming="false" SkinID="skinBtnCancel" OnClick="Button7_Click"></asp:Button>
                                                                                                                                </td>
                                                                                                                                <td style="width: 27%;"></td>
                                                                                                                            </tr>
                                                                                                                        </table>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </div>
                                                                                                    </asp:Panel>
                                                                                                </ContentTemplate>
                                                                                            </asp:UpdatePanel>
                                                                                        </asp:Panel>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>
                            <asp:ObjectDataSource ID="empSrc" runat="server" SelectMethod="ListExecutive" TypeName="BusinessLogic">
                                <SelectParameters>
                                    <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                            <asp:ObjectDataSource ID="srcCreditorDebitor" runat="server" SelectMethod="ListCreditorDebitor"
                                TypeName="BusinessLogic"></asp:ObjectDataSource>
                            <asp:ObjectDataSource ID="srcBanks" runat="server" SelectMethod="ListBanks" TypeName="BusinessLogic">
                                <SelectParameters>
                                    <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>

                        </td>
                    </tr>

                    <tr style="width: 100%">
                        <td style="width: 90%; text-align: left">
                            <asp:Panel ID="PanelBill" Direction="LeftToRight" runat="server">
                                <table width="100%" style="text-align: left; margin: -3px 0px 0px 0px;">
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GrdViewJournal" runat="server" AutoGenerateColumns="False" Width="100%"
                                                DataKeyNames="TransNo" AllowPaging="True" EmptyDataText="No Journals found" OnRowCreated="GrdViewJournal_RowCreated"
                                                OnSelectedIndexChanged="GrdViewJournal_SelectedIndexChanged" OnRowCommand="GrdViewJournal_RowCommand"
                                                OnPageIndexChanging="GrdViewJournal_PageIndexChanging" OnRowDeleting="GrdViewJournal_RowDeleting"
                                                OnSorting="GrdViewJournal_Sorting" OnRowDataBound="GrdViewJournal_RowDataBound" CssClass="someClass">
                                                <EmptyDataRowStyle CssClass="GrdContent" />
                                                <HeaderStyle Height="30px" HorizontalAlign="Center" Font-Bold="true" BackColor="#cccccc" BorderColor="Gray" Font-Size="Small" />
                                                <RowStyle Font-Bold="true" HorizontalAlign="Center" Height="30px" Font-Size="Small" ForeColor="#0567AE" />
                                                <Columns>
                                                    <asp:BoundField DataField="TransNo" HeaderText="Trans. No." SortExpression="TransNo"
                                                        HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Gray" />
                                                    <asp:BoundField DataField="Refno" HeaderText="Ref. No." HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Gray" />
                                                    <asp:BoundField DataField="TransDate" HeaderText="Trans. Date" HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Gray" DataFormatString="{0:dd/MM/yyyy}" />
                                                    <asp:BoundField DataField="Debi" HeaderText="Debtor" HeaderStyle-BorderColor="Gray" />
                                                    <asp:BoundField DataField="Cred" HeaderText="Creditor" HeaderStyle-BorderColor="Gray" />
                                                    <asp:BoundField DataField="Amount" HeaderText="Amount" HeaderStyle-BorderColor="Gray" />
                                                    <asp:BoundField DataField="Narration" HeaderText="Narration" Visible="false" HeaderStyle-BorderColor="Gray" />
                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="50px" HeaderText="Edit" HeaderStyle-BorderColor="Gray">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Select" />
                                                            <asp:ImageButton ID="btnEditDisabled" Enabled="false" SkinID="editDisable" runat="Server"></asp:ImageButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="50px" HeaderText="Delete" HeaderStyle-BorderColor="Gray">
                                                        <ItemTemplate>
                                                            <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to Delete this Journal?"
                                                                runat="server">
                                                            </cc1:ConfirmButtonExtender>
                                                            <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" CommandName="Delete"></asp:ImageButton>
                                                            <asp:ImageButton ID="lnkBDisabled" Enabled="false" SkinID="deleteDisable" runat="Server"></asp:ImageButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="50px" HeaderText="Print" HeaderStyle-BorderColor="Gray">
                                                        <ItemTemplate>
                                                            <a href='<%# DataBinder.Eval(Container, "DataItem.TransNo", "javascript:PrintItem({0});") %>'>
                                                                <asp:Image runat="server" ID="lnkprint" alt="Print" border="0" src="App_Themes/DefaultTheme/Images/Print1.png" />
                                                            </a>
                                                            <asp:ImageButton ID="btnViewDisabled" Enabled="false" SkinID="search" runat="Server"></asp:ImageButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                <PagerTemplate>
                                                    <table style="border-color: white">
                                                        <tr style="border-color: white">
                                                            <td style="border-color: white">Goto Page
                                                            </td>
                                                            <td style="border-color: white">
                                                                <asp:DropDownList ID="ddlPageSelector" OnSelectedIndexChanged="ddlPageSelector_SelectedIndexChanged"
                                                                    runat="server" AutoPostBack="true" Width="70px" Height="24px" Style="border: 1px solid blue" BackColor="#e7e7e7">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td style="border-color: white; width: 5px"></td>
                                                            <td style="border-color: white">
                                                                <asp:Button Text="" CommandName="Page" CommandArgument="First" runat="server" CssClass="NewFirst" EnableTheming="false" Width="22px" Height="18px"
                                                                    ID="btnFirst" />
                                                            </td>
                                                            <td style="border-color: white">
                                                                <asp:Button Text="" CommandName="Page" CommandArgument="Prev" runat="server" CssClass="NewPrev" EnableTheming="false" Width="22px" Height="18px"
                                                                    ID="btnPrevious" />
                                                            </td>
                                                            <td style="border-color: white">
                                                                <asp:Button Text="" CommandName="Page" CommandArgument="Next" runat="server" CssClass="NewNext" EnableTheming="false" Width="22px" Height="18px"
                                                                    ID="btnNext" />
                                                            </td>
                                                            <td style="border-color: white">
                                                                <asp:Button Text="" CommandName="Page" CommandArgument="Last" runat="server" CssClass="NewLast" EnableTheming="false" Width="22px" Height="18px"
                                                                    ID="btnLast" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </PagerTemplate>
                                                <SelectedRowStyle BackColor="#E3F6CE" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                                <input type="hidden" value="" id="hdDel" runat="server" />
                                <input type="hidden" id="delFlag" value="0" runat="server" />
                                <asp:HiddenField ID="hdToDelete" Value="0" runat="server" />
                                <asp:HiddenField ID="hdDataSource" runat="server" />
                                <asp:HiddenField ID="hdSMSRequired" runat="server" Value="NO" />
                                <asp:HiddenField ID="hdEmailRequired" runat="server" Value="NO" />
                            </asp:Panel>
                            <asp:Label ID="err" runat="server" Style="color: Red; font-weight: bold; font-family: 'Trebuchet MS'; font-size: 11px;"
                                Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <table align="center">
        <tr>
            <td style="width: 50%">
                <asp:Panel ID="pnlSearch" runat="server" Width="100px">
                    <asp:Button ID="lnkBtnAdd" runat="server" OnClick="lnkBtnAdd_Click" OnClientClick="return ShowModalPopup()" CssClass="ButtonAdd66"
                        EnableTheming="false" Width="80px" Text=""></asp:Button>
                </asp:Panel>
            </td>
            <td style="width: 50%">
                <asp:Button ID="Creditnote" runat="server"
                    CssClass="exportexl6" EnableTheming="false" CausesValidation="false"
                    OnClientClick="window.open('ReportExcelJournel.aspx','CSTSummary', 'toolbar=no,status=no,menu=no,location=no,height=210,width=500,left=425,top=220 ,resizable=yes, scrollbars=yes');"></asp:Button>
            </td>
        </tr>
    </table>
</asp:Content>
