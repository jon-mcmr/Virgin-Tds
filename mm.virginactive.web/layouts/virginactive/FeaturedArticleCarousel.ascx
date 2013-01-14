<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FeaturedArticleCarousel.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.FeaturedArticleCarousel" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.YourHealthTemplates" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>
						
					<div class="articles-left">
                        <div class="title-wrap">
                            <h3><%= Translate.Text("FEATURED ARTICLES")%></h3>
                        </div>

                        <asp:ListView ID="ImageList" runat="server">
                            <LayoutTemplate>
							<ul id="carousel">
                                <asp:PlaceHolder ID="ItemPlaceholder" runat="server"></asp:PlaceHolder>
							</ul>
                            <div id="carousel-header-bg"></div>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <li>
                                    <%# (Container.DataItem as YourHealthArticleItem).Articleimage.RenderCrop("620x340", "carousel-item carousel-image") %>
                                    <div class="carousel-header">
                                    	<p class="section-name"><%# (Container.DataItem as YourHealthArticleItem).ListingName %></p>
                                        <h2><a href="<%# (Container.DataItem as YourHealthArticleItem).PageSummary.Url %>"><%# (Container.DataItem as YourHealthArticleItem).Articletitle.Text %></a></h2>
                                        <p class="author"><span class="by"><%= Translate.Text("by") %></span><%# (Container.DataItem as YourHealthArticleItem).GetArticleAuthor() %></p>
                                    </div>
                                    <div class="carousel-intro">
                                    	<p><%# (Container.DataItem as YourHealthArticleItem).Articleteaser.Text%></p>
										<a class="moreinfo" href="<%# (Container.DataItem as YourHealthArticleItem).PageSummary.Url %>"><%= Translate.Text("Read more")%><span class="accessibility"> about <%# (Container.DataItem as YourHealthArticleItem).Articletitle.Text %></span></a>
                                    </div>
                                </li>
                            </ItemTemplate>
                        </asp:ListView>
					</div>
                    <script>
                        $(function () {
                            $.va_init.functions.setupImageRotator();
                        }); 
                    </script>
