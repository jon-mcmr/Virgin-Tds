<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PressArticle.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.press.PressArticle" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.Press" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
                        
            <div id="content" class="layout">
                    
				<div class="press"> 
					<div class="column-wide">    
	           		                    
                    	<section class="article-summary">
                        	<p class="calendar">
                                <span class="day"><%= article.Date.DateTime.ToString("MMM") %></span>
	                            <span class="date"><%= article.Date.DateTime.ToString("dd")%></span>
	                            <span class="year"><%= article.Date.DateTime.ToString("yyyy")%></span>
                        	</p>

                        	<div class="article-summary-main">
	                            <h2><%= article.Articleheading.Rendered %></h2>

	                            <div class="article-summary-copy">
                                    <%= article.Image.RenderCrop("280x160", "fr") %>
                                    <%= article.Body.Text %>	                                
	                            </div>

                                <% if (!String.IsNullOrEmpty(article.Moreinfo.Text))
                                   { %>

								<div class="press-more-info">
                                    <%= article.Moreinfo.Text%>
                                </div>

                                <% } %>

	                        </div>			
						    <% if (!String.IsNullOrEmpty(article.Bottomtext.Raw))
                            { %>

							<div class="highlight-panel">
                                
                                <% if (!String.IsNullOrEmpty(article.Bottombanner.Raw))
                                   { %>

			                	<div class="title-note">
									<p><%= article.Bottombanner.Rendered%></p>
								</div>
                                <% } %>
								<%= article.Bottomtext.Text%>
							</div>

                            <% } %>

                    	</section>  

                        <p><a href="<%= articleListingUrl %>"><%= Translate.Text("Back to Press landing")%></a></p>
                    
                	</div>

                	<aside class="column-240">
                        <sc:Placeholder Key="RightColumn" runat="server" />
	                </aside>
				</div><!-- /press -->
                    
            </div> <!-- /content -->
               