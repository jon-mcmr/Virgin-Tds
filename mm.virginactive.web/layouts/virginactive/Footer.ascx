<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Footer.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.Footer" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.common.Constants.SitecoreConstants" %>

        <footer role="contentinfo">
			<div class="container">
	
                <%= markup %>

				<ul id="social">
					<li><a href="http://www.youtube.com/virginactiveuk" class="yt external" target="_blank"><span></span><%= Translate.Text("You Tube")%></a></li>
					<li><a href="http://www.facebook.com/virginactiveuk" class="fb external" target="_blank"><span></span><%= Translate.Text("Facebook")%></a></li>
					<li><a href="http://twitter.com/VirginActiveUK" class="tw external" target="_blank"><span></span><%= Translate.Text("Twitter")%></a></li>
				</ul>

                <% if (!String.IsNullOrEmpty(footer.Title.Text) || !String.IsNullOrEmpty(footer.Body.Text))
                   { %>
				<section class="fl">
					<h4><%= footer.Title.Rendered %></h4>
				    <%= footer.Body.Text %>
				    <h5><%= Translate.Text("live happily ever")%> <span class="bold"><%= Translate.Text("active")%></span></h5>
                </section>
                <% } %>

				<section class="fr">
					<h4><%= Translate.Text("Partners") %></h4>
					<ul id="partners">
						<li class="first"><a href="http://www.puma.com/" class="external" target="_blank"><img src="/virginactive/images/partner-puma.png" alt="PUMA" width="66" height="43" /></a></li>
						<li><a href="http://www.poweradegb.com/" class="external" target="_blank"><img src="/virginactive/images/partner-powerade.png" alt="Powerade"  width="56" height="43" /></a></li>
                        <li><a href="http://pruhealth.pruhealth.co.uk/individuals/live-well/partners/virgin-active" class="external" target="_blank"><img src="/virginactive/images/partner-pruhealth.png" alt="Pru Health" width="105" height="43" /></a></li>
						<li><a href="http://www.wilson.com/en-gb/" class="external" target="_blank"><img src="/virginactive/images/partner-wilson.png" alt="Wilson" width="84" height="43" /></a></li>
						<li><a href="http://www.sparks.org.uk/" class="external" target="_blank"><img src="/virginactive/images/partner-sparks.png" alt="Sparks" width="67" height="43" /></a></li>
					</ul>
				</section>

				<div id="footer-sub">
                    <ul id="footer-links">        
                        <%= subMenuMarkup%>
                        <li id="change-country"><a href="#"><%= Translate.Text("Change Country")%></a>
                            <div id="country-wrap">
                        	    <ul>
                            	    <li><a href="http://www.virginactive.co.za" class="external sa-th"><%= Translate.Text("South Africa")%></a></li>
                            	    <li><a href="http://www.virginactive.it" class="external it-th"><%= Translate.Text("Italy")%></a></li>
                            	    <li><a href="http://www.virginactive.es" class="external es-th"><%= Translate.Text("Spain")%></a></li>
                            	    <li><a href="http://www.virginactive.pt" class="external po-th"><%= Translate.Text("Portugal")%></a></li>
								    <li class="last"><a href="http://www.virginactive.com.au" class="external au-th"><%= Translate.Text("Australia")%></a></li>
                                </ul>
                            </div>
                        </li>
                    </ul>
					<p><%= Translate.Text("© Copyright ##Year## Virgin Active. All rights reserved.")%></p>
					<p class="fr"><a href="<%= Settings.McCormackMorrisonUrl%>"><img src="/virginactive/images/MM.png" alt="McCormack &amp; Morrison" width="120" height="11" /></a></p>
				</div>
			</div>
		</footer>