<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SiteMisc.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.sitemap.SiteMisc" %>
<%@ Register TagPrefix="va" src="/layouts/virginactive/navigation/NavLinkSection.ascx" TagName="NavSection" %>
<%@ Import Namespace="mm.virginactive.common.Constants.SitecoreConstants" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.common.Helpers" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ComponentTemplates" %>	
					
<section class="last">

                            <va:NavSection 
                                ID="AboutUs"
                                runat="server" />

							<h3><a href="http://www.virgin.com"><%= Translate.Text("Virgin Group")%></a></h3>
							<ul>
								<li><a href="http://www.virgin.com">Virgin.com</a></li>
							</ul>
				
							<h3><a href="<%= Press.PageSummary.Url %>"><%= Press.PageSummary.NavigationTitle.Raw %></a></h3>
							<ul>

								<li><a href="<%= Press.PageSummary.Url %>"><%= Translate.Text("Press Releases") %></a></li>
							</ul>

							<h3><a href="<%=  SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.OurPartners) %>"><%= Translate.Text("Partners")%></a></h3>
                            <asp:ListView ID="PartnerList" runat="server" ItemPlaceholderID="PartnerListPh">
                                <LayoutTemplate>
							    <ul>
                                    <asp:PlaceHolder ID="PartnerListPh" runat="server" />
                                </ul>
                                </LayoutTemplate>
                                <ItemTemplate>                                
                                    <li><%# (Container.DataItem as LinkItem).Link.Rendered %></li>
                                </ItemTemplate>
                            </asp:ListView>

							<va:NavSection 
                                ID="Legals" HeaderNavigable="true"
                                runat="server" />
						</section>