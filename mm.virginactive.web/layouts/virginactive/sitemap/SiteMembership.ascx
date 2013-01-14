<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SiteMembership.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.sitemap.SiteMembership" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>
<%@ Register TagPrefix="va" src="/layouts/virginactive/navigation/NavLinkSection.ascx" TagName="NavSection" %>						

                        
                        <section>
							<%= HeaderIsH2? "<h2>":"<h3>" %><a href="<%= MembershipUrl %>">Memberships</a><%= HeaderIsH2? "</h2>":"</h3>" %>
						
							<va:NavSection 
                                ID="MemberOptions"
                                runat="server" />

                            <% if (!memberBenifits.Hidefromsitemap.Checked)
                               { %>
							<h3><a href="<%= memberBenifits.Url %>"><%= memberBenifits.NavigationTitle.Raw%></a></h3>
							<% } %>

                            <% if (!memberReciprocal.Hidefromsitemap.Checked)
                               { %>
                            <h3><a href="<%= memberReciprocal.Url %>"><%= memberReciprocal.NavigationTitle.Raw %></a></h3>
                            <% } %>
						</section>