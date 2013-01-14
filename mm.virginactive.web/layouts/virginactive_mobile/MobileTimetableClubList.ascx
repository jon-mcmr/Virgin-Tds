<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MobileTimetableClubList.ascx.cs" Inherits="mm.virginactive.web.layouts.Mobile.MobileTimetableClubList" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ModuleTemplates" %>
<%@ Import Namespace="Sitecore.Data.Items" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ComponentTemplates" %>
<%@ Import Namespace="mm.virginactive.controls.Model" %>
<%@ Import Namespace="mm.virginactive.common.Helpers" %>


		<div class="content timetables">
            <h1 class="icon"><%= Translate.Text("Timetables")%></h1>
			<div class="subtitle">
                <p><%= Translate.Text("CHOOSE A CLUB TO VIEW A TIMETABLE")%></p>
			</div>
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
		
