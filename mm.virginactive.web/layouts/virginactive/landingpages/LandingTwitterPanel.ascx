<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LandingTwitterPanel.ascx.cs"
	Inherits="mm.virginactive.web.layouts.virginactive.landingpages.LandingTwitterPanel" %>
	 <%@ Import Namespace="mm.virginactive.web.layouts.virginactive.landingpages" %>

<div class="section twitter">
	<div class="container">
		<!-- <asp:PlaceHolder ID="phTwitterHeading" runat="server">
			<h2>
				<%= TwitterHeading %></h2>
		</asp:PlaceHolder> -->
		<div class="row">
			<div class="user span3">
				<img class="promo_roundel" src="<%= TwitterImage %>" />
				<iframe scrolling="no" frameborder="0" allowtransparency="true" src="http://platform.twitter.com/widgets/follow_button.1354761327.html#_=1355135201377&amp;id=twitter-widget-0&amp;lang=en&amp;screen_name=<%= TwitterUsername %>&amp;show_count=false&amp;show_screen_name=true&amp;size=l"
					class="twitter-follow-button" style="width: 193px; height: 28px;" title="Twitter Follow Button"
					data-twttr-rendered="true"></iframe>
				<script>					!function (d, s, id) { var js, fjs = d.getElementsByTagName(s)[0]; if (!d.getElementById(id)) { js = d.createElement(s); js.id = id; js.src = "//platform.twitter.com/widgets.js"; fjs.parentNode.insertBefore(js, fjs); } } (document, "script", "twitter-wjs");</script>
			</div>
			<div class="span9">
				<div class="tweet">
					<ul>
						<asp:Repeater ID="repTweets" runat="server">
							<ItemTemplate>
								<li>
									<p class="tweeter">
										<strong>Twitter</strong> 
										<asp:HyperLink ID="idTwitter" Target="_blank" runat="server" NavigateUrl="<%# (Container.DataItem as TwitterList).UserLink %>">
											<asp:Literal ID="litTwitterUser" runat="server" Text="<%# (Container.DataItem as TwitterList).FavUserName %>" /></asp:HyperLink>
									</p>
									<p class="tweet_text">
										<asp:Literal ID="litTweet" runat="server" Text="<%# (Container.DataItem as TwitterList).Tweet %>"></asp:Literal>
									</p>

									<p class="is_retweet">Favourited by Virgin Active UK</p>
								</li>
							</ItemTemplate>
						</asp:Repeater>
					</ul>
				</div>
			</div>
		</div>
	</div>
</div>
