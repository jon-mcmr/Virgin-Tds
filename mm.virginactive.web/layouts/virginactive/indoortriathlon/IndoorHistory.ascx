<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IndoorHistory.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.indoortriathlon.IndoorHistory" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ComponentTemplates" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.IndoorTriathlon" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ModuleTemplates" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.common.Helpers" %>
            <div id="content">
                <div class="layout-block indoor-tri">                
                
                <div class="header_width_social clearfix">
	                <h2><%= currentItem.Heading.Rendered%></h2>
	                
	                <!--Social-->
                    <% if (showSocial == true)
                        {%>
	                <ul class="social-info clearfix">
	                    <li class="social-twitter"><span><%= SocialMediaHelper.TweetButtonForUrl(pageUrl) %></span></li>
                        <li class="social-facebook"><span><%= SocialMediaHelper.LikeButtonIFrame(pageUrl)%></span></li>		
	                    <li class="social-googleplus"><span><%= SocialMediaHelper.GooglePlusButton %></span></li>
	                </ul>
                    <%  }%>
	            </div>
                
                <%= currentItem.Body.Text%>
               <%-- <%= currentItem.Image.RenderCrop("938x310")%>--%>

               <img src="/virginactive/images/indoor-tri/infographic.jpg" alt="Indoor Triathlon Infographic" width="938" height="872" />

                

                <!--Testimonials-->
                <h3 class="line-through"><span>Testimonials</span></h3>
                <div class="testimonials carousel_container">
                    <div class="carousel">
                        <ul>  
                            <asp:ListView ID="Testimonials" runat="server">
                                <LayoutTemplate>
                                    <asp:PlaceHolder ID="ItemPlaceholder" runat="server" />
                                </LayoutTemplate> 
                                <ItemTemplate>
                                    <li class="listing">
                                        <h4>
                                            <%#(Container.DataItem as TestimonialItem).Heading.Rendered %>                                              
                                            <span><%# (Container.DataItem as TestimonialItem).Subheading.Rendered%></span>
                                        </h4>
                                        <blockquote>
                                            <%#(Container.DataItem as TestimonialItem).Quote.Rendered%>
                                        </blockquote>                            
                                    </li>
                                </ItemTemplate>
                            </asp:ListView>
                        </ul>
                    </div>
                </div>

                <!-- Video Panel-->
                <div class="media-panels">                
                    <section class="listing half-listing fl">
                        <h2>Video</h2>
                        <a href="#video-overlay-content" title="Play video" class="btn-video btn-overlay open-overlay video-overlay"> 
                            <%= currentItem.Videoimage.RenderCrop("440x300")%>
                            <span><%= Translate.Text("Play Video")%></span>
                        </a>
                        <p><%= currentItem.Videoteaser.Text%></p>
                    </section>

                    <!-- Image/Photos Panel-->
                    <section class="listing half-listing fr">
                        <h2>Photos</h2>
                        <a href="#photos-overlay-content" id="btn-photo" title="View photos" class="btn-overlay open-overlay photos-overlay">
                             <%= currentItem.Photoimage.RenderCrop("440x300")%>
                             <span><%= Translate.Text("View Photos")%></span>
                        </a>
                        <p><%= currentItem.Phototeaser.Text%></p>
                    </section>

                </div>
            </div>
            
            <% if (registrationForm != null)
               {%>
		    <div class="footer-cta indoor">
                <p><%= registrationForm.Widget.Heading.Rendered%></p>
                <p class="cta"> 
                    <a class="btn btn-cta-big" href="<%= registrationForm.Widget.Buttonlink.Url %>"><%= registrationForm.Widget.Buttontext.Rendered%></a>
                </p>
            </div>
           <%} %>	
        </div> <!-- /content -->

        <!-- Overlays -->

    <div id="video-overlay-content" class="overlay hidden">
    	<h2><%= currentItem.Videotitle.Rendered%></h2>
        <p><%= currentItem.Videointro.Rendered%></p>
        <div class="video">
    	    <iframe width="640" height="400" src="<%= currentItem.Videolink.Url%>" frameborder="0" allowfullscreen></iframe>
    	</div>
	</div>

    <!--Images (For Photos Overlay)-->
    <div id="photos-overlay-content" class="overlay hidden">
    <h2><%= currentItem.Phototitle.Rendered%></h2>
        <div class="image_carousel carousel_container carousel-container-main">
            <div class="carousel">
                <ul>
                    <asp:ListView ID="OverlayImages" runat="server">
                        <LayoutTemplate>
                            <asp:PlaceHolder ID="ItemPlaceholder" runat="server" />
                        </LayoutTemplate> 
                        <ItemTemplate>
                            <li id="overlay-<%# Container.DataItemIndex%>">
                                <%#(Container.DataItem as ImageCarouselItem).Image.RenderCrop("680x360")%>                                              
                                <%# (Container.DataItem as ImageCarouselItem).ImageCaption.Rendered%>                                               
                            </li>
                        </ItemTemplate>
                    </asp:ListView>
                </ul>
            </div>
        </div>

        <div class="image_carousel carousel_container carousel-container-thumbs">
            <div class="carousel carousel-thumbs">
                <ul>
                    <asp:Literal ID="ltrThumbnailItems" runat="server"></asp:Literal>
                </ul>
            </div>
        </div>

        <script>
            var overlay = document.getElementById('va-overlay')
                if (overlay !== null) {
                    indoorTry.setupCarousel('.indoor-va-overlay-prevent-clash .carousel-container-main',false,0,'');
                    indoorTry.setupCarousel('.indoor-va-overlay-prevent-clash .carousel-container-thumbs',true,7,'img');
                    indoorTry.carouselController.init('.indoor-va-overlay-prevent-clash .carousel-container-thumbs','.indoor-va-overlay-prevent-clash .carousel-container-main');
                }
            
        </script>
    </div>


