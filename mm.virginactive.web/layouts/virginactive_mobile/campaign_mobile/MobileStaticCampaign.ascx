<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MobileStaticCampaign.ascx.cs" Inherits="mm.virginactive.web.layouts.Mobile.Campaigns.MobileStaticCampaign" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.common.Helpers" %>
		<div class="content clubfinder">
            <%--<img class="max-width" src="<%= Sitecore.StringUtil.EnsurePrefix('/', campaign.Sideheroimage.MediaUrl) %>" />--%>
            <%--<img class="max-width" src="<%= Sitecore.StringUtil.EnsurePrefix('/', SitecoreHelper.GetOptimizedMediaUrl(campaign.Sideheroimage)) %>" />--%>
            <img class="max-width" src="<%= Sitecore.StringUtil.EnsurePrefix('/', SitecoreHelper.GetOptimizedMediaUrl(campaign.Mobileimage)) %>" />
			
			<div class="data_block">
				 <div class="data_content">
					<h2><%= campaign.Heading.Raw %></h2>
                    <p class="intro"><%= campaign.Introductiontext.Raw%></p>
                    <%= campaign.Bodytext.Text %>
				 </div>
			</div>
			
			<h1><%= campaign.Actiontext.Raw %></h1>
			<fieldset id="form_fields" data-name="campaign-<%=campaign.CampaignBase.Campaigncode.Rendered%>">
                    <asp:DropDownList ID="clubFindSelect" runat="server" class="required" data-required="Please choose a club"></asp:DropDownList>
					
					<input type="text" ID="txtName" runat="server" class="required" data-required="Please enter your name" placeholder="Your name" />
					
					<input type="text" ID="txtEmail" runat="server" class="required email" data-required="Please enter your email address" data-email="Please enter a valid email" data-placeholder="Email address" placeholder="Email address" />
					
					<input type="text" ID="txtPhone" runat="server" class="required phone" data-required="Please enter a your contact number" data-email="Please enter a valid contact number" data-placeholder="Contact number" placeholder="Contact number" />
					
					<div class="form_row checkbox">                        
						<input type="checkbox" name="subscribe" id="chkSubscribe" runat="server" />
						<label for="<%=chkSubscribe.ClientID%>"><%= Translate.Text("Subscribe to our newsletter")%></label>
					</div>

                    <button runat="server" class="button red" ID="btnSubmit" onclick="return virginactive.mobile.validation.validate()">
                        <span><strong><%= Translate.Text("Enquire now") %></strong></span>
                    </button>
                    <asp:LinkButton name="content_0$btnSubmitLink" ID="btnSubmitLink" runat="server" class="btn-submit" OnClick="btnSubmit_Click" /><!--this is required to load __doPostBack code -->
										
		</fieldset>
	</div>
