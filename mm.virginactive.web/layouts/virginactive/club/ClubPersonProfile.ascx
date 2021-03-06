﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClubPersonProfile.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.club.ClubPersonProfile" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ComponentTemplates" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.common.Helpers" %>
                <div id="primary-l">
					<div class="people-profile">
							
						<section>
	                   		<h2><%= Person.GetFullName() %></h2>
                            <%= Person.Profileimage.RenderCrop("240x240", "f1") %>	                    	
							<div class="profile">
								<h3><%= Person.Title.Rendered %></h3>
								<p>Birmingham - Broadway Plaza</p>
								<p><strong>Joined</strong><%= Person.Joiningdate.DateTime.ToString(" MMMM dd, yyyy") %></p>
								
								<div class="highlight-panel">
										<h4><%= Person.Quotetitle.Rendered %></h4>
										<blockquote><%= Person.Quote.Rendered %></blockquote>
								</div>
                                	<% if (showSocial == true)
                                           {%>
								<ul class="social-info">
                                    <li class="social-facebook"><span><%= SocialMediaHelper.LikeButton %></span></li> 
                                    <li class="social-twitter"><span><%= SocialMediaHelper.TweetButton %></span></li>
		                   		</ul>
                                        <%  }%>
							</div>
	               		</section> 
                    
						<section class="people-details">
					
							<h3 class="line-through"><span><%= String.Format(Translate.Text("About {0}"), Person.Firstname.Raw) %></span></h3>
							<section>							
								<h4><%= Translate.Text("Bio")%></h4>
				             	<%= Person.Biotext.Text %>
							</section>
					
							<aside class="people-panel">
								<h4 class=""><%= Translate.Text("Qualifications")%></h4>
								<%= Person.Qualifications.Rendered %>					
							</aside>
								
						</section>
					
					    <asp:Panel ID="TestimonalSection" runat="server" Visible="false">
						<section>	
                       		<h3 class="line-through"><span><%= Translate.Text("Testimonials")%></span></h3>
                            <asp:ListView ID="TestList" runat="server">
                                <LayoutTemplate>
		                        <ul class="testimonials show-limit">
                                    <asp:PlaceHolder ID="ItemPlaceholder" runat="server" />
		                        </ul>
                                </LayoutTemplate>

                                <ItemTemplate>
		                        	<li>
		                            	<p class="heading"><%# (Container.DataItem as TestimonialItem).Heading.Rendered %></p>
		                                <blockquote><%# (Container.DataItem as TestimonialItem).Quote.Rendered %></blockquote>
		                            	<p class="timestamp"><time pubdate datetime="<%# (Container.DataItem as TestimonialItem).Date.DateTime.ToString("yyyy-MM-ddTHH:mm") %>"><%# (Container.DataItem as TestimonialItem).Date.DateTime.ToString("dd MMM yyyy h:mm tt")%></time></p>
		                            </li>
                                </ItemTemplate>
                            </asp:ListView>

                            
						</section>
                        </asp:Panel>
		                </div>
                    </div>                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      