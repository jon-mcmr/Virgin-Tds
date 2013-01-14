<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FeaturedPromotionEvent.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.FeaturedPromotionEvent" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ModuleTemplates" %>
            <section class="fl">
                
                <% = fpew.Image.RenderCrop("160x160","fl mr") %>
				<div class="featured-panel">
                    <hgroup>
					    <h2><%= fpew.Linkcategory.Rendered %></h2>
					    <h3><%= fpew.Widget.Heading.Rendered%></h3>
				    </hgroup>
				    <p><%= fpew.GetDurationDates() %></p>
				    <p><%= fpew.Widget.Bodytext.Rendered %></p>
				    <p><a class="btn btn-cta" href="<%= fpew.Widget.Buttonlink.Url %>"><%= mm.virginactive.common.Globalization.Translate.Text("Find out more")%></a></p>
                </div>
			</section>

			<aside class="fr">
				<h2><%= mm.virginactive.common.Globalization.Translate.Text("Ones to watch") %></h2>
                
                <asp:ListView ID="FeatureList" runat="server">
                    <LayoutTemplate>
                    <ul>
                        <asp:PlaceHolder ID="ItemPlaceholder" runat="server" />
                    </ul>
                    </LayoutTemplate>

                    <ItemTemplate>                        
                        <li><a href="<%# (Container.DataItem as FeaturedPromotionEventWidgetItem).Widget.Buttonlink.Url %>" class="<%# (Container.DataItem as FeaturedPromotionEventWidgetItem).GetFeatureCss() %>"><%# (Container.DataItem as FeaturedPromotionEventWidgetItem).Widget.Heading.Rendered %> </a></li>
                    </ItemTemplate>
                </asp:ListView>
 
			</aside>
