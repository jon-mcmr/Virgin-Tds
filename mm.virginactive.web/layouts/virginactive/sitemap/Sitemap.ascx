<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Sitemap.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.sitemap.Sitemap" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>

            <div id="content" class="layout">
				
				<div class="sitemap">
					<div class="sitemap-col">
						<section>
							<h2><a href="<%= Home.Url %>"><%= Translate.Text("Virgin Active Health Clubs")%></a></h2>
							<ul>
								<li><a href="<%= Home.Url %>"><%= Home.NavigationTitle.Raw %></a></li>
								<li><a href="<%= MemberEnq.Url %>"><%= Translate.Text("Membership Enquiry")%></a></li>
								<li><a href="<%= MemberEnq.Url %>"><%= Translate.Text("Book a Visit")%></a></li>
								<li><a href="<%= ClubFinder.Url %>"><%= ClubFinder.NavigationTitle.Raw %></a></li>
								<li><a href="<%= Contact.Url %>"><%= Contact.NavigationTitle.Raw %></a></li>
							</ul>
						</section>
                        <sc:Placeholder Key="SitemapColOne" runat="server" />
					</div>
                   
					<div class="sitemap-col">
                        <sc:Placeholder Key="SitemapColTwo" runat="server" />						
					</div>
					<div class="sitemap-col">						
                        <sc:Placeholder Key="SitemapColThree" runat="server" />
					</div>
					
					<div class="sitemap-col">
                        <sc:Placeholder Key="SitemapColFour" runat="server" />
					</div>
					
				</div> <!-- /sitemap -->

            </div> <!-- /content -->