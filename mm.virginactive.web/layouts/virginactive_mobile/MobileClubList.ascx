<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MobileClubList.ascx.cs" Inherits="mm.virginactive.web.layouts.Mobile.MobileClubList" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ModuleTemplates" %>
<%@ Import Namespace="Sitecore.Data.Items" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ComponentTemplates" %>
<%@ Import Namespace="mm.virginactive.controls.Model" %>
<%@ Import Namespace="mm.virginactive.common.Helpers" %>

		<div class="content">
			
			<h1><%= Translate.Text("Our Clubs") %></h1>
					
			<ul class="data_list">
            <asp:ListView ID="clubList" runat="server" OnItemDataBound="clubList_OnItemDataBound">						
                <LayoutTemplate>
                    <asp:PlaceHolder ID="ItemPlaceholder" runat="server"></asp:PlaceHolder>
                </LayoutTemplate>
                <ItemTemplate>
                    <asp:Literal ID="ltrClubLink" runat="server"></asp:Literal>				    
                </ItemTemplate>
            </asp:ListView>
			</ul>
			
		</div>
		
