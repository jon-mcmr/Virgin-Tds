<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TriathlonTraining.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.marketingcampaigns.TriathlonTraining" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.Campaigns" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ModuleTemplates" %>

            <div class="mainContent training">                
                
                <div class="intro">
                    <h3><%= currentItem.Abstract.Subheading.Rendered %></h3>
                    <%= currentItem.BodyText.Rendered %>
                </div>
                <section id="day-counter">
                    <span id="days-remaining" class="day">&nbsp;</span>
                    <span class="desc ir">Days to the race</span>
                </section>

                <!--FAQS -->
                <asp:ListView ID="FAQPanels" runat="server">
                    <LayoutTemplate>
                        <asp:PlaceHolder ID="ItemPlaceholder" runat="server" />
                    </LayoutTemplate> 
                    <ItemTemplate>

                    <div class="faqs-panel">              
						<ul class="faqs-list accordion-list">
                            <li class="accordion-item">
                                <h5 class="closed"><a href="#"><%# (Container.DataItem as FAQItem).Question.Rendered%></a></h5>
                                <div class="accordion-body">
								    <%# (Container.DataItem as FAQItem).Answer.Rendered%>
							    </div>
                            </li>
                        </ul>
                    </div>

                    </ItemTemplate>
                </asp:ListView>

                <!--LINKS-->
                <asp:ListView ID="LinkPanels" runat="server">
                    <LayoutTemplate>
                        <asp:PlaceHolder ID="ItemPlaceholder" runat="server" />
                    </LayoutTemplate> 
                    <ItemTemplate>
                        <section class="listing full-listing">                                              
                            <%# (Container.DataItem as FileImageLinkWidgetItem).Image.RenderCrop("220x120")%>	
                            <div class="inner">
                                <h4><%# (Container.DataItem as FileImageLinkWidgetItem).Widget.Heading.Rendered%></h4>
                                <%# (Container.DataItem as FileImageLinkWidgetItem).Widget.Bodytext.Rendered%>
                                <p class="more"><a href="">Visit site</a></p>
                            </div>                                                
                        </section>
                    </ItemTemplate>
                </asp:ListView>

            </div>

          


