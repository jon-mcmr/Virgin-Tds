<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MemberLanding.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.MemberLanding" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.Membership" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs.Membership" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ModuleTemplates" %>

            <div id="content" class="layout">
                <div id="membs-campaign">	
				    <div class="hero">
			            <div class="panel-left">						    
                            <%= context.Panelbody.Rendered %>
					    </div>

				    <div id="outer-media">	
					    <div id="media">
						    <asp:PlaceHolder runat="server" ID="IframePlaceholder">
						        <iframe width="460" height="264" frameborder="0" allowfullscreen="" src="<%= context.Panelvideourl.Raw %>&amp;wmode=transparent"></iframe>
						    </asp:PlaceHolder>
					    </div>
				    </div>

				    </div> <!-- /hero -->
			    </div>

               <div class="membership-pers">
					<h2><%= context.Heading.Rendered %></h2>
					<section class="howtojoin">
                            <% if (buttons.Count > 0)
                               { %>
							<div class="membs-cta">
								<h3><%= buttons[0].Widget.Heading.Rendered %></h3>
								<%= buttons[0].Widget.Bodytext.Text %>                             
			                    <div class="cta-wrap">
									<a href="<%= buttons[0].Widget.Buttonlink.Url %>" class="btn btn-cta-big"><%= buttons[0].Widget.Buttontext.Text%></a>
								</div>
							</div>
                            <% } %>

							<p class="select-membs"><%= Translate.Text("OR")%></p>

                            <% if (buttons.Count > 0)
                               { %>
							<div class="membs-cta last">					
								<h3><%= buttons[1].Widget.Heading.Rendered %></h3>
								<%= buttons[1].Widget.Bodytext.Text %>
			                    <div id="redirect-url" class="cta-wrap" <%=attribute %>>
						                <label class="visuallyhidden"><%= buttons[1].Widget.Heading.Rendered %></label>
						            <select class="chzn-clublist selectclub club-section-redirect" id="clubSelect" runat="server"></select>
								</div>
							</div>
                            <% } %>
					</section>


	                <section class="notes-bottom-panel">
					    <h3><%= Translate.Text("Ready to join?") %></h3>
                        <ul class="membership-steps">
                            <li><span>1</span> <%= Translate.Text("Come see us") %></li>
						    <li><span>2</span> <%= Translate.Text("Choose a membership") %></li>
						    <li class="last"><span>3</span> <%= Translate.Text("Get active!") %></li>
                        </ul>
					    <%= context.Questions.Text%>

				    </section>
			 
                <div class="contrast-cta">
					    <p><%= Translate.Text("Ready to join?") %> <%= Translate.Text("Let us show you around...") %></p> 
					    <a class="btn btn-cta-big" href="<%= enqForm.Url + "?sc_trk=enq" %>"><%= Translate.Text("Membership Enquiry") %></a>
				</div>
            </div> 
        </div>
