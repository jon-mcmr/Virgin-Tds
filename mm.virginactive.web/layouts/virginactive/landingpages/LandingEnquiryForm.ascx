<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LandingEnquiryForm.ascx.cs"
	Inherits="mm.virginactive.web.layouts.virginactive.landingpages.LandingEnquiryForm" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ModuleTemplates" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<div class="section enquiry">
	<div class="container">
		<div class="row">
			<div class="span6 image_block">
				<div class="row">
					<asp:ListView ID="ClubImages" runat="server" OnItemDataBound="ClubImagesOnDataBound">
						<LayoutTemplate>
							<asp:PlaceHolder ID="ItemPlaceholder" runat="server" />
						</LayoutTemplate>
						<ItemTemplate>
							<asp:Literal ID="ListViewImage" runat="server" />
						</ItemTemplate>
					</asp:ListView>
				</div>
			</div>
			<div class="span6">
				<fieldset>
					<h2>
						<%= Translate.Text("Enquiry form")%></h2>
					<img class="promo_roundel" src="<%= currentItem.CallToActionImage.MediaUrl %>" width="100"
						height="100" alt="<%# currentItem.CallToActionImage.MediaItem.Alt %>" />
					<div class="form_row clearfix">
						<label for="<%=txtName.ClientID%>">
							<%= Translate.Text("Your name")%></label>
						<input type="text" maxlength="30" id="txtName" runat="server" class="required" data-required="Please tell us your name" />
						<asp:RequiredFieldValidator ID="rfvName" runat="server" Display="None" ControlToValidate="txtName"
							EnableClientScript="True" />
						<asp:RegularExpressionValidator ID="revName" runat="server" ControlToValidate="txtName"
							Display="None" />
					</div>
					<div class="form_row clearfix">
						<label for="<%=txtEmail.ClientID%>">
							<%= Translate.Text("Email address")%></label>
						<input type="text" maxlength="40" id="txtEmail" runat="server" class="required email"
							data-required="Please enter your email address" data-email="Please enter a valid email address" />
						<asp:RequiredFieldValidator ID="rfvEmail" runat="server" Display="None" ControlToValidate="txtEmail"
							EnableClientScript="True" />
						<asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
							Display="None" />
					</div>
					<div class="form_row clearfix">
						<label for="<%=txtPhone.ClientID%>">
							<%= Translate.Text("Contact number")%></label>
						<input type="text" maxlength="20" id="txtPhone" runat="server" class="required phone"
							data-required="Please enter your phone number" data-phone="Please enter a valid phone number" />
						<asp:RequiredFieldValidator ID="rfvPhone" runat="server" Display="None" ControlToValidate="txtPhone"
							EnableClientScript="True" />
						<asp:RegularExpressionValidator ID="revPhone" runat="server" ControlToValidate="txtPhone"
							Display="None" />
					</div>
					<div class="checkbox_row clearfix">
						<input type="checkbox" id="chkSubscribe" class="checkbox" runat="server" checked="checked" />
						<label for="<%=chkSubscribe.ClientID%>" class="check">
							<%= Translate.Text("Subscribe to our newsletter for the latest news and offers")%></label>
					</div>
					<asp:Button class="btn medium" ID="btnSubmit" runat="server" OnClick="btnSubmit_Click"
						data-gaqaction="EnquireNow" data-gaqcategory="CTA" data-formtype="webleads" OnClientClick="return virginactive.campaigns.q1.validateForm();" />
				</fieldset>
			</div>
		</div>
		<div class="row">
			<div class="span12 address">
				<p>
					<asp:Literal ID="litClubAddress" runat="server"></asp:Literal></p>
				<ul>
					<li><a href="http://maps.google.com/maps?q=<%= clubItem.Lat.Rendered%>,<%= clubItem.Long.Rendered %>&ll=<%= clubItem.Lat.Rendered%>,<%= clubItem.Long.Rendered %>"
						target="_blank" />View Map</a> | </li>
					<li><a href="<%= TimeTableUrl %>#clubs" target="_blank">Timetables</a></li>
				</ul>
			</div>
		</div>
	</div>
</div>
