<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="ReconDate.aspx.cs" Inherits="ReconDate" Title="Administration > Last Verified Date" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">

    <table style="width: 100%">
        <tr style="width: 100%">
            <td style="width: 100%">
            
                <%--<div class="mainConHd">
                     <table cellspacing="0" cellpadding="0" border="0">
                         <tr valign="middle">
                             <td>
                                 <span>Last Verified Date</span>
                             </td>
                         </tr>
                     </table>
                </div>--%>
                <%--<table class="mainConHd" style="width: 994px;">
                                <tr valign="middle">
                                    <td style="font-size: 20px;">
                                        Last Verified Date
                                    </td>
                                </tr>
                            </table>--%>
                            <div class="mainConBody">
                    <table style="width: 100.3%; margin: -3px 0px 0px 2px;" cellpadding="3" cellspacing="2" class="searchbg">
                        <tr style="vertical-align: middle">
                            <td style="width: 2%;"></td>
                            <td style="width: 23%; font-size: 22px; color: #000000;" >
                                Last Verified Date
                            </td>
                            <td style="width: 16%">
                                <div style="text-align: right;">
                                    
                                </div>
                            </td>
                            <td style="width: 20%; color: #000000;" align="right">
                                
                            </td>
                            <td style="width: 20%">
                                
                            </td>
                            <td colspan="2" style="width: 20%">
                                
                            </td>
                        </tr>
                        
                    </table>
                </div>
             <div class="mainConDiv" id="IdmainConDiv">
                <div align="center">
                    <table width="45%" style="border: 1px solid Gray" cellpadding="3" cellspacing="2">
                        <tr style="height:5px">
                        </tr>
                        <tr style="width: 100%">
                            
                                <td class="ControlLabel" style="width: 30%">
                                    
                                    <asp:RequiredFieldValidator ID="rvStock" runat="server" ControlToValidate="txtReconDate"
                                        Display="Dynamic" EnableClientScript="True">Date is mandatory</asp:RequiredFieldValidator>
                                    <asp:CompareValidator ControlToValidate="txtReconDate" Operator="DataTypeCheck" Type="Date"
                                        ErrorMessage="Please enter a valid date" runat="server" ID="cmpValtxtDate"></asp:CompareValidator>
                                    <asp:RangeValidator ID="myRangeValidator" runat="server" ControlToValidate="txtReconDate"
                                        ErrorMessage="Recon date cannot be future date." Text="*" Type="Date"></asp:RangeValidator>
                                    Date *
                                </td>
                                <td style="width: 15%" class="ControlTextBox3">
                                    <asp:TextBox ID="txtReconDate" runat="server" Width="100px" CssClass="cssTextBox"></asp:TextBox>
                                    
                                </td>
                                <td align="left" style="width: 10%">
                                    <script type="text/javascript" language="JavaScript">                                        new tcal({ 'formname': 'aspnetForm', 'controlname': GettxtBoxName('txtReconDate') });</script>
                                </td>
                                <td style="width:10%">
                                </td>
                            </tr>
                            <tr style="height:3px">
                            </tr>
                            <tr>
                                <td style="width: 30%" class="ControlLabel">
                                    Purchase Entry Date Enable
                                </td>
                                <td style="width: 15%" class="ControlTextBox3">
                                    <asp:RadioButtonList ID="rdvoudateenable" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Selected="True" Text="YES"></asp:ListItem>
                                        <asp:ListItem Text="NO"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td align="left" style="width: 10%">
                                    
                                </td>
                                <td style="width:10%">
                                </td>
                            </tr>
                            <tr style="height:3px">
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td style="width: 15%" align="center">
                                    <asp:Button ID="lnkBtnUpdate" runat="server" CssClass="savebutton1231" EnableTheming="false" SkinID="skinBtnSave" OnClick="lnkBtnUpdate_Click" />
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:HiddenField ID="hdSMSRequired" runat="server" Value="NO" />
                                    <asp:HiddenField ID="hdEmailRequired" runat="server" Value="NO" />
                                </td>
                            </tr>
                    </table>
                    </div>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
