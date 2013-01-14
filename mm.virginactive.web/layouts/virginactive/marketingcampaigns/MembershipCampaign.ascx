<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MembershipCampaign.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.marketingcampaigns.MembershipCampaign" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.controls.Model" %>
<%@ Import Namespace="mm.virginactive.common.Helpers" %>
    <asp:ScriptManager ID="ScriptManager2" runat="server" />
    <script type="text/javascript">
        var thisUrl = '<%= thisUrl %>';
        var _ActiveTab = 'list';
    </script>

    <article id="article" runat="server" class="membs">

        <div id="wrapper">
			<header>
				<div id="logo" class="img-rep"><a href="/"><span class="rep"></span><span class="bold"><%= Translate.Text("Virgin Active") %></span> <%= Translate.Text("Health Clubs") %></a></div>
			</header>
            
            <div class="hero">
                <div class="panel-left">
                    <%= current.Paneltext.Rendered %>
			    </div>
			    <div class="panel-right">
				    <iframe width="480" height="300" src="<%= current.Videourl.Raw %>" frameborder="0" allowfullscreen></iframe>
			    </div>
		    </div>

		    <div class="clubfinder-enquiry" id="enquiryform-results">
			    <div id="clubfinderform" class="frm">
		   		    <fieldset class="find">									
		       		    <label class="ffb" for="findclub"><%= Translate.Text("Find your nearest club")%> <span><%= Translate.Text("(We have 122 to choose from)")%></span></label>
		       		    <input type="text" placeholder="Type in postcode, city, town or area..." class="searchclubs" id="findclub" maxlength="40" autocomplete="off">
                        <input type="hidden" id="input-entered" />
		   		    </fieldset>	
			    </div>
		    </div>

            <asp:PlaceHolder ID="ResultsPh" runat="server" Visible="false">
		        <div id="content">
			        <div id="primary-r" class="club-finder-results">
                    <div class="results-head">
		                <h3><%= Translate.Text("Results for")%> <span id="resultsFor"><%= location %></span></h3>
		                <div class="view-links">
			                <span class="clubs-list-view"><a class="active" href=""><%= Translate.Text("List View")%></a></span>
			                <span class="clubs-map-view"><a href=""><%= Translate.Text("Map View")%></a></span>
		                </div>
	                </div>
                </div>
            </asp:PlaceHolder>

            <asp:PlaceHolder ID="ClubInfoPh" runat="server" Visible="false">
                <div id="content">
			        <div id="club-membs-detail">
				        <h2 class="join"><%= Translate.Text("Join")%> <span class="club-name">
                            <%= club.ClubItm.Clubname.Raw %>
                        </span> <%= Translate.Text("for a year and save")%> <span class="club-saving">&pound;<%= club.DoubleClubCost %></span></h2>
			
				        <div class="panel-left">
						        <div class="membs-panel">
								        <span class="membs-type"><%= current.Sectiontitle.Raw %></span>
								        <span class="membs-price">&pound;<%= club.Cost %></span>
								        <span class="membs-period"><%= Translate.Text("a month")%></span>
						        </div>
						        <h3><%= current.Bodytitle.Raw %></h3>
						        <%= current.Body.Raw %>
                                
                                <ul id="social">
                                    <li class="share-this"><%= Translate.Text("Share this offer")%></li>
                                    <li><a href="mailto:?subject=<%= Translate.Text("12 months for the price of 10 from Virgin Active, yes please!")%>&amp;body=<%= thisUrl %>" title="<%= Translate.Text("Share by Email")%>" class="email"><span></span><%= Translate.Text("Email")%></a></li>
                                    <li><a href="https://twitter.com/share" class="twitter-share-button" data-url="<%= thisUrl %>" data-text="<%= Translate.Text("12 months for the price of 10 @virginactiveuk, yes please!")%>" data-via="virginactiveuk" data-lang="en" data-count="none" data-hashtags="moreisless">Tweet</a>
                                    <script>    !function (d, s, id) { var js, fjs = d.getElementsByTagName(s)[0]; if (!d.getElementById(id)) { js = d.createElement(s); js.id = id; js.src = "//platform.twitter.com/widgets.js"; fjs.parentNode.insertBefore(js, fjs); } } (document, "script", "twitter-wjs");</script></li>
                                    <li><div id="fb-root"></div>
                                            <script>                                                (function (d, s, id) {
                                                    var js, fjs = d.getElementsByTagName(s)[0];
                                                    if (d.getElementById(id)) return;
                                                    js = d.createElement(s); js.id = id;
                                                    js.src = "//connect.facebook.net/en_GB/all.js#xfbml=1&appId=64921914414";
                                                    fjs.parentNode.insertBefore(js, fjs);
                                                } (document, 'script', 'facebook-jssdk'));</script><div class="fb-like" data-href="<%= thisUrl %>" data-send="false" data-layout="button_count" data-width="450" data-show-faces="false"></div></li>
                                    </ul>        
				        </div>

				        <div class="panel-right">
						<asp:UpdatePanel ID="pnlForm" UpdateMode="Conditional" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSubmit" />
                            </Triggers>		
                            <ContentTemplate> 
					        <div id="innerform" runat="server" class="inner inner-form">
						        <h3><%= current.Formtitle.Raw %></h3>
						        <p class="intro"><%= Translate.Text("Your Details")%></p>
						        <fieldset>
		   					        <div class="row row-chzn">
					                    <input name="txtClubCode" type="hidden" id="txtClubCode" class="clubCode" runat="server" />
					   		        </div>
					                <div class="row">
					        	        <label for="yourname"><%= Translate.Text("Your name")%></label>
					                    <input name="yourname" type="text" id="yourname" class="text text-name valRequired" data-placeholder="Please enter your name" runat="server" />
		            		        </div>

					                <div class="row">
		                                <label for="email"><%= Translate.Text("Email address")%></label>
		                                <input name="email" type="text" id="email" class="text text-email valRequired" data-placeholder="Please enter your email" runat="server" />
		                            </div>
		                            <div class="row">
		                                <label for="contact"><%= Translate.Text("Contact number")%></label>
		                                <input name="contact" type="text" id="contact" class="text text-contact valRequired" data-placeholder="Please enter your contact number" runat="server" />
		                            </div>

		                            <div class="row row-checkbox">
		                                <input name="subscribe" type="checkbox" id="subscribe" runat="server" />

		                                <label for="<%=subscribe.ClientID%>"><%= Translate.Text("Subscribe to our newsletter for the latest news and offers")%></label>
		                            </div>
		                        </fieldset>
		                        <ul class="btns">
                                    <li id="gaq-<%= current.CampaignBase.Isweblead.Checked == true ? "weblead" : "notweblead" %>" data-gaqlabel="CampaignMore" class="btn btn-submit"><asp:LinkButton ID="btnSubmit" runat="server" onclick="btnSubmit_Click"><%= Translate.Text("Submit")%></asp:LinkButton></li>
		                        </ul>
                                    
		           	        </div>
                            <div class="inner inner-thanks" id="formCompleted" runat="server">
                                <h2><%= current.Thankyoutitle.Raw%></h2>
                                <p class="intro"><%= StringHelper.ParseClubDetails(current.Thankyoutext.Raw, club.ClubItm) %></p>
                            </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
				        </div> <!-- / panel-right -->

                    </div>
                </div>
            </asp:PlaceHolder>

        </div>
    </article>