<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LandingDemo.ascx.cs"
	Inherits="mm.virginactive.web.layouts.virginactive.landingpages.LandingDemo" %>
<div class="section video_demo">
	<div class="container">
		<div class="row">
			<h2 class="span12">
				<asp:Literal runat="server" ID="ltHeading"></asp:Literal></h2>
		</div>
		<div class="row">
			<div class="span6">
				<h3>
					<asp:Literal runat="server" ID="ltIntroHeading"></asp:Literal></h3>
				<div class="intro">
					<p>
						<asp:Literal runat="server" ID="ltIntro"></asp:Literal></p>
				</div>
				<p>
					<asp:Literal runat="server" ID="ltText"></asp:Literal></p>
				
			</div>
			<div class="span6">
				<iframe width="460" height="259" frameborder="0" allowfullscreen="" id="videoLink"
					runat="server"></iframe>
					<asp:PlaceHolder ID="phImage" runat="server" Visible="false">
				<%= currentItem.Panel1Image.RenderCrop("460x300") %>
				</asp:PlaceHolder>
			</div>
		</div>
	</div>
</div>
