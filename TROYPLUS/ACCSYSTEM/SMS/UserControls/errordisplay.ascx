<%@ Control Language="C#" AutoEventWireup="true" CodeFile="errordisplay.ascx.cs" Inherits="_UserControl_errordisplay" %>
<link href="../../App_Themes/DefaultTheme/DefaultTheme.css" rel="stylesheet" type="text/css" />  
<asp:panel id="uiErrorPanel" runat="server" Width="101%">
	<table width="100%">
		<tr>
			<td style="width:100%; vertical-align:middle" >
				<asp:repeater id="messageRepeater" runat="server" visible="False" enableviewstate="false">
					<headertemplate>
						<table cellspacing="0" width="100%" class="errors">
						<tr class="errors">
							<th class="errors" ></th>
							<th class="errors" width="100%"><b>Information & Errors</b></th>
						</tr>
					</headertemplate>
					<itemtemplate>
						<tr class="errors" style="font-size:11px">
							<td class="errors">
								<img alt="#" class="errors" src="<%#DataBinder.Eval(Container, "DataItem.IconFullName")%>"/>
							</td>
							<td class="errors" style="width:100%"><%#DataBinder.Eval(Container, "DataItem.Message") %>
							</td>
						</tr>
					</itemtemplate>
					<footertemplate>
						</table>
					</footertemplate>
				</asp:repeater>
			</td>
		</tr>
	</table>
</asp:panel>