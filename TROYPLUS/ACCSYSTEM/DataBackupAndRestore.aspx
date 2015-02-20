<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="DataBackupAndRestore.aspx.cs" Inherits="DataBackupAndRestore" Title="Data BackUp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajX" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server" >
    <%--<h3> BACK UP </h3>
    <br />--%>
    <%--<div  class="lblFont" style="border: solid 1 px black; background-color: aliceblue;
        font-weight: bold; color: Red; width:1000px; margin:0 auto;">
        <%--<center style="font-family : Times New Roman; font-size: medium; color: Red; width:1000px; margin:0 auto;" >--%>
         <%--This screen is used to take Backup Data.This need to be
        used carefully need to be done by the approval of supervisor.
        <br />
        1. Close all the other session before backp - (Only particular user who is taking
        backup need to be in the session).</center>
    </div>
    <br />--%>
    <table style="width: 100%">
        <tr style="width: 100%">
            <td style="width: 100%">
                <div class="mainConBody">
                    <table style="width: 100.3%; margin: -3px 0px 0px 2px;" cellpadding="3" cellspacing="2" class="searchbg">
                        <tr style="height: 25px; vertical-align: middle">
                            <td style="width: 2%">
                            </td>
                            <td style="width: 16%; font-size: 22px; color: #000000;" >
                                Data Backup
                            </td>
                            <td style="width: 14%">
                                <div style="text-align: right;">
                                                
                                </div>
                            </td>
                            <td style="width: 13%; color: #000080;" align="right">
                                            
                            </td>
                            <td style="width: 20%">
                                            
                            </td>
                            <td style="width: 20%">
                                            
                            </td>
                            <td style="width: 17%" class="tblLeftNoPad">
                                            
                            </td>
                                        
                            <td style="width: 20%" class="tblLeftNoPad">
                                            
                            </td>
                        </tr>
                    </table>
                </div>
    <table cellpadding="2" cellspacing="2" width="100%" style="border: solid 0px black"
        >
            
        <tr>
            <td colspan="4">
                <table style="width:100%">
                    <tr>
                        <td colspan="4" class="ControlLabel">
                            <asp:Label ID="lblMsg" runat="server" Style="color: Red; font-weight: bold;"></asp:Label>
                        </td>
                    </tr>
                    
                    <tr>
                        <td style="width:22%" class="ControlLabel">
                            
                        </td>
                        <td style="width:16%" class="ControlLabel">
                            Backup File Path
                        </td>
                        <td style="width:20%" class="ControlDrpBorder">
                            <asp:TextBox ID="txtFilePath" runat="server" CssClass="cssTextBox"></asp:TextBox>
                        </td>
                        <td style="width:45%" align="left">
                            <i>Ex : "C:\BackUp" - (use backward slash as separator)</i>
                        </td>
                    </tr>
                    <tr style="height: 8px">
                                                                                </tr>
                    <tr>
                        <td style="width:22%">
                                    
                        </td>
                        <td style="width:16%">
                                    
                        </td>
                        <td style="width:20%" align="center">
                            <asp:Button ID="btnBackup" runat="server" OnClick="btnBackup_Click"
                                ToolTip="Click Here to Took Backup" CssClass="backup" EnableTheming="false" />
                        </td>
                        <td style="width:3%">
                                    
                        </td>
                        <td style="width:45%">
                                    
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <table style="width:100%">
                                
                            </table>
                         </td>
                    </tr>
                </table>
            </td>
        </tr>

        
    </table>
    <br />
    <%--<table cellpadding="2" cellspacing="2" width="100%" style="border: solid 1px black"
        border="0" class="accordionContent">
        <tr>
            <td colspan="3" class="accordionHeader">
                Restore
            </td>
        </tr>
        <tr>
            <td colspan="3" class="lblFont">
                <asp:Label ID="lblMsg2" runat="server" Style="color: Red; font-weight: bold;"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="lblFont">
                Restore File Name
            </td>
            <td align="left" class="lblFont">
                <asp:TextBox ID="txtFilenameR" runat="server"></asp:TextBox>.MDB
            </td>
            <td class="lblFont">
                <i>Ex : Any Valid name : BackupApril2009</i>
            </td>
        </tr>
        <tr>
            <td class="lblFont">
                Restore File Path
            </td>
            <td align="left" class="lblFont">
                <asp:TextBox ID="txtFilePathR" runat="server" Width="250px"></asp:TextBox>
            </td>
            <td class="lblFont">
                <i>Ex : "E:\databasebackup" - (use backward slash as separator)</i>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <input id="browseFile" type="file" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <ajX:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="btnRestore" ConfirmText="Attention: While Restoring Current Data Will be Lossed Are You Sure Want To Restore ?"
                    runat="server">
                </ajX:ConfirmButtonExtender>
                <asp:Button ID="btnRestore" SkinID="skinButtonBig" runat="server" OnClick="btnRestore_Click"
                    Text="Restore" />
            </td>
        </tr>
    </table>--%>
    </td>
    </tr>
    </table>
</asp:Content>
