<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LandingWhatNextShare.ascx.cs"
	Inherits="mm.virginactive.web.layouts.virginactive.landingpages.LandingWhatNextShare" %>
<div class="section share_bar">
	<div class="container">
		<div class="row">
			<h2 class="span12">
				<%= currentItem.ShareThisHeading.Rendered %>
			</h2>
			<ul>
				<li class="social-twitter"><span>
					<%= mm.virginactive.common.Helpers.SocialMediaHelper.TweetButtonForUrl(OriginalReferer, TwitterShareText, TwitterUsername)%>
				</span></li>
				<li class="social-facebook"><span>
					<%= mm.virginactive.common.Helpers.SocialMediaHelper.LikeButtonForUrl(OriginalReferer)%>
				</span></li>
				<li class="social-googleplus"><span>
					<%= mm.virginactive.common.Helpers.SocialMediaHelper.GooglePlusButtonAsnc(OriginalReferer) %>
					
				</span></li>
			</ul>
		</div>
	</div>
</div>
