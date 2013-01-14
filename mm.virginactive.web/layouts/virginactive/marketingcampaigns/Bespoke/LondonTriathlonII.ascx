<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LondonTriathlonII.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.marketingcampaigns.LondonTriathlonII" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ModuleTemplates" %>

<div id="wrapper">
    <header>
        <div class="container clearfix">
            <a id="logo" href="<%= LondonTriathlonUrl %>"><img src="/va_campaigns/Bespoke/LondonTriathlonII/img/logo.png" alt="Virgin Active Health Clubs - London Triathlon"/></a>
            <section id="tagline" class="ir">
                <h1><strong>Virgin Active</strong> Health Clubs</h1>
                <p><strong>London Triathlon &bull;</strong> 2012 22&amp; 23 Sept</p>
            </section>
            <section id="day-counter">
                <span id="days-remaining" class="day">&nbsp;</span>
                <span class="desc ir">Days to the race</span>
            </section>
        </div>
    </header>

    <section id="slider">
        <div id="scrollable" class="scrollable">
            <div class="items">
                 <div>
                    <div id="slide-1" class="slide">&nbsp;</div>
                </div>

                <div>
                    <div id="slide-2" class="slide">&nbsp;</div>
                </div>

                <div>
                    <div id="slide-3" class="slide">&nbsp;</div>
                </div>

                <div>
                    <div id="slide-4" class="slide">&nbsp;</div>
                </div>

                <div>
                    <div id="slide-5" class="slide">&nbsp;</div>
                </div>

                 <div>
                    <div id="slide-6" class="slide">&nbsp;</div>
                </div>

                 <div>
                    <div id="slide-7" class="slide">&nbsp;</div>
                </div>

                 <div>
                    <div id="slide-8" class="slide">&nbsp;</div>
                </div>
            </div>
        </div>
    </section>

    <div id="content">

         <div id="controls">
            <a id="scroll-next" class="next browse right">&gt;</a>
            <a id="scroll-prev" class="prev browse left disabled">&lt;</a>
        </div>

        <a href="#email-content" id="btn-email" class="open-overlay">
            <strong>ENJOY<br />EXCLUSIVE CONTENT</strong>
            <span class="desc">Stay on top of the latest offers and updates</span>
            <span class="arrow ir">&gt;</span>
        </a>
        <section id="main" class="clearfix">
            <div class="grid1">
                <h2>Getting involved with <strong>the 2012 Virgin Active</strong> London Triathlon?</h2>
                <p>We're here to help you every stride, stroke and saddle session of the way.</p>
            </div>
            <div class="grid2">
                <h2>2011 HIGHLIGHTS</h2>
                <a href="#video-overlay-content" id="btn-video" title="Play video" class="open-overlay video-overlay"> <span class="ir">Play video</span> <img src="/va_campaigns/Bespoke/LondonTriathlonII/img/video-placeholder.jpg" alt=""></a>
                <p>See last year's thrills & spills!</p>
            </div>
            <aside class="grid3">
                <h2>TRAIN WITH OUR EXPERTS</h2>
                <p>Hit peak condition at one of our <br>122 clubs</p>
                <a href="<%= ClubFinderUrl %>" id="btn-club" target="_blank">Find your club</a>
            </aside>
        </section>
    </div>
    <!-- end content -->
    <% if (LinkedBlogEntry != null) 
       {%>
    <section id="blog">
        <div class="container clearfix">
            <div class="grid4">
                <h2>Blog</h2>
                <h3><a href="<%= LinkedBlogEntry.Url %>" title="<%= CampaignBlogEntry.Title.Raw %>"><%= CampaignBlogEntry.Title.Raw%></a></h3>
                <p>
                    <%= CampaignBlogEntry.Introduction.Text%>
                </p>
                <a href="<%= LinkedBlogEntry.Url %>" title="<%= CampaignBlogEntry.Title.Raw %>">Continue reading</a>
            </div>
            <aside class="grid3">
                <ul class="list-date">
                    <li class="date">
                        <strong>
                            <%= CampaignBlogEntry.Created.ToString("dd")%> <%= CampaignBlogEntry.Created.ToString("MMM")%>
                        </strong>
                        <span class="year"><%= CampaignBlogEntry.Created.ToString("yyyy")%></span>
                    </li>
                    <li class="author">
                        <em>by</em><%=CampaignBlogEntry.GetBlogAuthor()%>
                    </li>
                </ul>
            </aside>
            <div class="opacity">&nbsp;</div>
        </div>
    </section>
    <% } %>
    <footer>
        <div class="container clearfix">
            <nav>
                <ul class="clearfix">
                    <li>&copy; Copyright 2012 Virgin Active. All rights reserved. <span>/</span></li>
                    <li><a href="<%= PrivacyPolicyUrl %>" target="_blank">Privacy Policy</a> <span>/</span></li>
                    <li><a href="<%= TermsConditionsUrl %>" target="_blank">Terms &amp; Conditions</a></li>
                </ul>
            </nav>
            <ul id="social">
                <li><a href="http://twitter.com/VirginActiveUK" class="tw external" target="_blank"><span></span>Twitter</a>
                </li>
                <li><a href="http://www.facebook.com/virginactiveuk" class="fb external" target="_blank"><span></span>Facebook</a>
                </li>
                <li><a href="http://www.youtube.com/virginactiveuk" class="yt external" target="_blank"><span></span>You Tube</a></li>
            </ul>
        </div>
    </footer>

</div>

<div id="overlay" >
    <div id="email-content" class="overlay hidden">
    <a href="#" class="button-close">CLOSE <span>[X]</span></a>
          <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <asp:UpdatePanel ID="pnlForm" UpdateMode="Conditional" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSubmit" />
            </Triggers>
            <ContentTemplate>
                <!--Uncompleted Form-->
                <div id="formToComplete" runat="server">
    	            <h2>EXCLUSIVE CONTENT AWAITS!</h2>
    	            <h3>Sign up and keep pace with the very latest London Triathlon offers and updates.</h3>
    	            <fieldset>
    			            <input id="email" placeholder="Enter your email address" class="input-email required" type="text" runat="server" />
                            <asp:button text="go" id="btnSubmit" runat="server" class="input-submit" onclick="btnSubmit_Click" />
                            <asp:LinkButton ID="btnLink" runat="server" /><!--this is required to load __doPostBack code -->
    	            </fieldset>
		            <p><a href="<%= PrivacyPolicyUrl %>" target="_blank">Read our privacy policy</a></p>
                </div>
                <!--Confirmation of form submission-->
                <div id="formCompleted" runat="server" visible="false">
                    <div id="email-thanks">
                        <h2>THANKS!</h2>
                        <h3>That’s it – you’re all signed up to our exclusive content.</h3>
                        <p>From now on, if there’s anything worth knowing about the London Triathlon, we’ll share it with you.</p>
                        <p><a href="<%= PrivacyPolicyUrl %>" target="_blank">Read our privacy policy</a></p>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
	</div>

    <div id="video-overlay-content" class="overlay hidden">
        <a href="#" class="button-close video-overlay">CLOSE <span>[X]</span></a>
    	<h2>THE THRILLS &amp; SPILLS OF 2011</h2>
    	<h3>Watch the highlights from last year’s event</h3>
    	<div id="icon-youtube" class="ir">You Tube</div>
        <div class="video">
    	    <iframe width="640" height="400" src="http://www.youtube.com/embed/KITeb96nEdE?wmode=opaque" frameborder="0" allowfullscreen></iframe>
    	</div>
        <p>Here's a little taster of the sights, sounds and special vibes that make the Virgin Active London Triathlon so amazing (including a spot of sporting endeavour from RB himself!).</p>
		<p><a href="http://www.youtube.com/virginactiveuk" target="_blank">See more videos</a></p>
	</div>

        <div id="overlay-bg" class="hidden"></div>
  </div>


    