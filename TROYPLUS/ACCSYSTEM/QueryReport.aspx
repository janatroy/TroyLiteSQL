<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true" CodeFile="QueryReport.aspx.cs" Inherits="QueryReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    
    <script src="Scripts/JScriptSales.js" type="text/javascript">

        //        function openpop()
        //        {
        //            window.opener.MethodonFirstPage();
        //        }

    </script>
    <asp:UpdatePanel ID="UpdatePnlMaster" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
      <table style="width: 100%">
        <tr style="width: 100%">
            <td style="width: 100%">
                <div class="mainConDiv" id="IdmainConDiv">
                    <div class="mainConHd">
                        <table cellspacing="0" cellpadding="0" border="0">
                            <tr valign="middle">
                                <td>
                                    <span>SQL Generator</span>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <table align="center" width="50%" style="border: 1px solid #86b2d1; margin: 0 0 0 0px"
                                    cellpadding="5" cellspacing="5">
                      <tr>
                        <td>
                        &nbsp;</td>
                        </tr>
                                       
                        <tr align="left" id="Tr1" runat="server">
                            <td class="sqlbutton">
                            <asp:Button ID="newqrybtn" runat="server" TabIndex="2" EnableTheming="false" 
                             Text="Create New Query" Enabled="true" Width="100%" BackColor="Blue" 
                                    ForeColor="White" onclick="newqrybtn_Click">
                            </asp:Button>          
                            </td>
                        </tr>
                        <tr align="left" id="Tr2" runat="server">
                     
                            <td class="sqlbutton">
                                <asp:Button ID="editqrybtn" runat="server" TabIndex="3" Text="Edit Existing Query" 
                                   EnableTheming="false" Enabled="true" Width="100%" Font-Bold="false" BackColor="Blue" 
                                  ForeColor="White" onclick="editqrybtn_Click" />
                            </td>
                     
                         </tr>
                         <tr align="left" id="Tr3" runat="server">
                           <td class="sqlbutton">
                            <asp:Button ID="deleteqrybtn" runat="server" TabIndex="4" EnableTheming="false" 
                            Text="Delete Existing Query" Enabled="true" Width="100%" BackColor="Blue" 
                                   ForeColor="White" onclick="deleteqrybtn_Click">
                            </asp:Button>          
                         </td>
                         </tr>
            
                     </table>
                   </div>
                    <input id="dummyStock" type="button" style="display: none" runat="server" />
   <input id="BtnPopUpCancel" type="button" style="display: none" runat="server" />
                        
                    <cc1:ModalPopupExtender ID="ModalPopupSql" runat="server" BackgroundCssClass="modalBackground"
         RepositionMode="RepositionOnWindowResizeAndScroll" DynamicServicePath="" Enabled="True" PopupControlID="pnlSqlForm" TargetControlID="dummyStock" OkControlID="dummyStock" CancelControlID="BtnPopUpCancel">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlSqlForm" runat="server" Style="width: 65%; display: none">
        <asp:UpdatePanel ID="updatePnlSql" runat="server" UpdateMode="Conditional">
             <ContentTemplate>
                <div id="Div1" class="divArea" style="width:100%">
                    <table style="width: 100%;" align="center">
                        <tr style="width: 100%">
                            <td style="width: 100%">
                                <table style="text-align: left;" width="50%" cellpadding="3" cellspacing="5">
                                    <tr>
                                        <td>
                                            <table class="headerPopUp" width="50%">
                                                <tr>
                                                    <td>
                                                        SQL Report
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                    <cc1:TabContainer ID="tabContol" runat="server" Width="90%"
            ActiveTabIndex="0">
            <cc1:TabPanel ID="tabPanelMaster" runat="server" HeaderText="Edit">
            
                <ContentTemplate>

                    <table width="100%" style="border: 0px solid #5078B3">
                        <tr style="width: 100%">
                            <td class="tblLeft">
                                Reports
                            </td>
                            <td>
                            <div style="border-width:1px;border-color:#bce1fe;border-style:solid;width:200px;font-family:'Trebuchet MS';text-align:center;">
                                <asp:DropDownList TabIndex="1" ID="cmbQuries" AppendDataBoundItems="True" Height="28px" Width="100%" CssClass="drpDownListMedium" BackColor = "#90C9FC"
                                    runat="server" AutoPostBack="True" DataValueField="ID" DataTextField="QueryName"
                                    onselectedindexchanged="cmbQuries_SelectedIndexChanged">
                                    <asp:ListItem Text="Select Report" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="tblLeft">
                                Description:
                            </td>
                            <td>
                                <asp:Label ID="lblDescription" runat="server"></asp:Label>
                            </td>
                             <td>
                              <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                              <ContentTemplate>
                                <asp:TextBox ID="txtDesc" runat="server" Visible="False"  Height="20px" Width="100%" BackColor = "#90C9FC"  AutoPostBack="True"></asp:TextBox>
                                </ContentTemplate>
                                 <Triggers>
                  <asp:PostBackTrigger ControlID="BtnGenerateReport"/>
                        <asp:AsyncPostBackTrigger ControlID="BtnEdit" EventName="Click" />
                         <asp:AsyncPostBackTrigger ControlID="BtnSave" EventName="Click" />
                                                                                          
                  </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td class="tblLeft">
                                Query:
                            </td>
                            <td>
                             <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                              <ContentTemplate>
                                <asp:TextBox ID="txtQuery" runat="server" TextMode="MultiLine" BackColor = "#90C9FC" Height="150px" Width="99%"></asp:TextBox>   
                                 </ContentTemplate>
                                  <Triggers>
                   <asp:PostBackTrigger ControlID="BtnGenerateReport"/>
                        <asp:AsyncPostBackTrigger ControlID="BtnEdit" EventName="Click" />
                         <asp:AsyncPostBackTrigger ControlID="BtnSave" EventName="Click" />
                                                                                          
                  </Triggers>
                                 </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td class="tblLeft">
                            </td>
                            <td width="30%">
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Button ID="BtnGenerateReport" runat="server" Text="Generate Report"
                                    Width="110px" ValidationGroup="sendSMS"  CssClass="generatebutton1"
                                     OnClick="BtnGenerateReport_Click" />
                                     
                   </ContentTemplate>
         <Triggers>
                    <asp:PostBackTrigger ControlID="BtnGenerateReport"/>
                         <asp:AsyncPostBackTrigger ControlID="BtnSave" EventName="Click" />
                              <asp:AsyncPostBackTrigger ControlID="BtnEdit" EventName="Click" />   
                      
                                                                                          
                  </Triggers>
        </asp:UpdatePanel>
                            </td>
                            <td class="lmsrightcolumncolor" width="30%">
                                 <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Button ID="BtnEdit" runat="server"  CssClass="Button" 
                                    onclick="BtnEdit_Click" Text="Edit Report" Width="110px" Visible="False"/>
                                    
                                                                                          
               
                   </ContentTemplate>
         <Triggers>
              
                        <asp:PostBackTrigger ControlID="BtnGenerateReport"/>
                         <asp:AsyncPostBackTrigger ControlID="BtnSave" EventName="Click" />
                              <asp:AsyncPostBackTrigger ControlID="BtnEdit" EventName="Click" />   
                      </Triggers>
        </asp:UpdatePanel>
                            </td>
                            <td width="20%">
                              <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Button ID="BtnSave" runat="server" Width="110px" Text="Save" CssClass="savebutton1" OnClick="BtnSave_Click" Visible="False" />
                                     
                   </ContentTemplate>
        <Triggers>
                <asp:PostBackTrigger ControlID="BtnGenerateReport" />
                         <asp:AsyncPostBackTrigger ControlID="BtnSave" EventName="Click" />
                              <asp:AsyncPostBackTrigger ControlID="BtnEdit" EventName="Click" />                                                               
                  </Triggers>
        </asp:UpdatePanel>
       
                            </td>
                            <td width="20%">
                            <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                            <asp:Button ID="btncancel" runat="server" Text="Cancel" OnClick="btncancel_Click" CssClass="cancelbutton" />
                            </ContentTemplate>
                          <Triggers>
                
                              <asp:AsyncPostBackTrigger ControlID="btncancel" EventName="Click" />                                                               
                         </Triggers>
                            </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td class="tblLeft" style="width: 30%">
                            </td>
                            <td class="lmsrightcolumncolor" colspan="3" 
                                style="width: 75%; text-align: left">
                                <asp:GridView ID="rptData" runat="server">
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                    
                </ContentTemplate>
               
            </cc1:TabPanel>
        </cc1:TabContainer>

        </td>
        </tr>
        </table>
        </td>
        </tr>
        </table>
        </div>
         </ContentTemplate>
        
      </asp:UpdatePanel>
       
     </asp:Panel>
       <input id="Button1" type="button" style="display: none" runat="server" />
   <input id="Button2" type="button" style="display: none" runat="server" />
                        
                    <cc1:ModalPopupExtender ID="ModalPopupCreate" runat="server" BackgroundCssClass="modalBackground"
         RepositionMode="RepositionOnWindowResizeAndScroll" DynamicServicePath="" Enabled="True" PopupControlID="pnlSqlCreateForm" TargetControlID="Button1" OkControlID="Button1" CancelControlID="Button2">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlSqlCreateForm" runat="server" Style="width: 65%; display: none">
        <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
             <ContentTemplate>
                <div id="Div2" class="divArea" style="width:100%">
                    <table style="width: 100%;" align="center">
                        <tr style="width: 100%">
                            <td style="width: 100%">
                                <table style="text-align: left;" width="100%" cellpadding="3" cellspacing="5">
                                    <tr>
                                        <td>
                                            <table class="headerPopUp" width="100%">
                                                <tr>
                                                    <td>
                                                        Create New Sql Report
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <table width="100%" style="border: 0px solid #5078B3">
                        <tr style="width: 100%">
                            <td class="tblLeft" style="width: 30%">
                                Reports
                            </td>
                            <td>
                            <asp:TextBox ID="txtboxqrynme" runat="server"  Height="20px" Width="100%" BackColor = "#90C9FC"  AutoPostBack="True"></asp:TextBox>
                            </td>
                            </tr>
                            <tr>
                             <td class="tblLeft" style="width: 30%">
                                Description
                            </td>
                            <td>
                            <asp:TextBox ID="txtboxdescrip" runat="server"  Height="20px" Width="100%" BackColor = "#90C9FC"  AutoPostBack="True"></asp:TextBox>
                            </td>
                            </tr>
                            <tr>
                            <td class="tblLeft" style="width: 30%">
                                Query
                            </td>
                            <td>
                            <asp:TextBox ID="txtboxqry" runat="server" TextMode="MultiLine" Height="150px" BackColor = "#90C9FC" Width="99%"></asp:TextBox>

                            </td></tr>
                            <tr>
                            <td>
                             <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                            <asp:Button ID="btnsavenew" runat="server" Text="Save" CssClass="savebutton1" OnClick="btnsavenew_Click" />
                            </ContentTemplate>
                            <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnsavenew" EventName="Click" />
                            </Triggers>
                            </asp:UpdatePanel>
                            </td>
                            <td>
                             <asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                            <asp:Button ID="btncancel1" runat="server" Text="Cancel" CssClass="cancelbutton" OnClick="btncancel1_Click" />
                            </ContentTemplate>
                            <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btncancel1" EventName="Click" />
                            </Triggers>
                            </asp:UpdatePanel>
                            </td>
                            </tr>
                          </table>
                        </table>
                      </td>
                     </tr>
                   </table>
                </div>
             </ContentTemplate>
         </asp:UpdatePanel>
     </asp:Panel>

        <input id="Button3" type="button" style="display: none" runat="server" />
   <input id="Button4" type="button" style="display: none" runat="server" />
                        
                    <cc1:ModalPopupExtender ID="ModalPopupDelete" runat="server" BackgroundCssClass="modalBackground"
         RepositionMode="RepositionOnWindowResizeAndScroll" DynamicServicePath="" Enabled="True" PopupControlID="pnlSqlDeleteForm" TargetControlID="Button3" OkControlID="Button3" CancelControlID="Button4">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlSqlDeleteForm" runat="server" Style="width: 65%; display: none">
        <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
             <ContentTemplate>
                <div id="Div3" class="divArea" style="width:100%">
                    <table style="width: 100%;" align="center">
                        <tr style="width: 100%">
                            <td style="width: 100%">
                                <table style="text-align: left;" width="50%" cellpadding="3" cellspacing="5">
                                    <tr>
                                        <td>
                                            <table class="headerPopUp" width="75%">
                                                <tr>
                                                    <td>
                                                        Delete Sql Report
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <table width="100%" style="border: 0px solid #5078B3">
                        <tr style="width: 100%">
                             <td class="tblLeft" style="width: 30%">
                                Reports
                            </td>
                            <td style="width: 40%; text-align: left">
                            <div style="border-width:1px;border-color:#bce1fe;border-style:solid;width:200px;font-family:'Trebuchet MS';text-align:center;">
                            
                                <asp:DropDownList ID="ddlqueries" AppendDataBoundItems="True" Height="28px" Width="100%" CssClass="drpDownListMedium" BackColor = "#90C9FC"
                                   runat="server" AutoPostBack="True" DataValueField="ID" onselectedindexchanged="ddlqueries_SelectedIndexChanged" DataTextField="QueryName" >
                                    <asp:ListItem Text="Select Report" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                               
                            </div>
                           
                            </tr>
                         
                           <tr>
                            <td>
                             <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                            <asp:Button ID="Btndelete" runat="server" Text="Delete" OnClick="Btndelete_Click" />
                            </ContentTemplate>
                            <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Btndelete" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="ddlqueries" EventName="SelectedIndexChanged" />
                            </Triggers>
                            </asp:UpdatePanel>
                            </td>
                            <td>
                            <asp:UpdatePanel ID="UpdatePanel12" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                            <asp:Button ID="btncancel2" runat="server" Text="Cancel" CssClass="cancelbutton" OnClick="btncancel2_Click" />
                            </ContentTemplate>
                            <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btncancel2" EventName="Click" />
                            </Triggers>
                            </asp:UpdatePanel>
                            </td>
                            </tr>
                          </table>
                        </table>
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
   </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>