<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TriathlonSectionListing.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.marketingcampaigns.TriathlonSectionListing" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.Campaigns" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ModuleTemplates" %>

            <div class="mainContent listing-<%= PageName %>">
                <h3><%= currentItem.Abstract.Subheading.Rendered %></h3>
                <div class="intro">
                    <%= currentItem.BodyText.Rendered %>
                </div>

                <!--FAQS -->
                <div class="faqs-panel" id="pnlFaqs" runat="server">              
				    <ul class="faqs-list accordion-list">
                        <asp:ListView ID="FAQPanels" runat="server">
                            <LayoutTemplate>
                                <asp:PlaceHolder ID="ItemPlaceholder" runat="server" />
                            </LayoutTemplate> 
                            <ItemTemplate>
                                <li class="accordion-item">
                                    <h5 class="closed"><a href="#"><%# (Container.DataItem as FAQItem).Question.Rendered%></a></h5>
                                    <div class="accordion-body">
								        <%# (Container.DataItem as FAQItem).Answer.Rendered%>
							        </div>
                                </li>
                            </ItemTemplate>
                        </asp:ListView>
                    </ul>
                </div>
                <!--LINKS-->
                <asp:ListView ID="LinkPanels" runat="server">
                    <LayoutTemplate>
                        <asp:PlaceHolder ID="ItemPlaceholder" runat="server" />
                    </LayoutTemplate> 
                    <ItemTemplate>
                        <section class="listing full-listing">
                            <a href="<%#(Container.DataItem as FileImageLinkWidgetItem).File.MediaUrl != ""? (Container.DataItem as FileImageLinkWidgetItem).File.MediaUrl : (Container.DataItem as FileImageLinkWidgetItem).Widget.Buttonlink.Url + "\" target=\"_blank" %>">                                              
                            <%# (Container.DataItem as FileImageLinkWidgetItem).Image.RenderCrop("220x120")%>
                            </a>	
                            <div class="inner">
                                <h4><%# (Container.DataItem as FileImageLinkWidgetItem).Widget.Heading.Rendered%></h4>
                                <%# (Container.DataItem as FileImageLinkWidgetItem).Widget.Bodytext.Rendered%>
                                <p class="more"><a href="<%#(Container.DataItem as FileImageLinkWidgetItem).File.MediaUrl != ""? (Container.DataItem as FileImageLinkWidgetItem).File.MediaUrl : (Container.DataItem as FileImageLinkWidgetItem).Widget.Buttonlink.Url + "\" target=\"_blank" %>"><%# (Container.DataItem as FileImageLinkWidgetItem).Widget.Buttontext.Rendered%></a></p>
                            </div>                                                
                        </section>
                    </ItemTemplate>
                </asp:ListView>

            </div>

            <% if (clubFinder != null)
               {%>
                <div class="cta-panel">
                    <div class="cta-panel-inner">
                        <h3><%= clubFinder.Widget.Heading.Rendered%></h3>
                        <%= clubFinder.Widget.Bodytext.Rendered%>
                        <a href="<%= clubFinder.Widget.Buttonlink.Url%>" class="btn-club" target="_blank"><%= clubFinder.Widget.Buttontext.Rendered%></a>
                    </div>
               </div>
           <%} %>

          


