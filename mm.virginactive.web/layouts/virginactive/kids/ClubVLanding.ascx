<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClubVLanding.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.kids.ClubVLanding" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.Kids" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
	
			<div id="content" class="layout-full">
				<div class="full-width-panel-layout"> 
				    
                    <asp:PlaceHolder runat="server" ID="HeroSectionPlaceholder">
					    <section class="full-width-panel-gold">
					        <h2 class="hide-text"><%= ClubV.Herosectiontitle.Rendered %></h2>
					    
					        <div>
							    <h3><%= ClubV.Herosectionheading.Rendered %></h3>
							    <%= ClubV.Herosectiontext.Text %>
						   
							    <a href="<%= ClubV.Herosectionbuttonlink.Url %>" class="btn btn-cta-big"><%= ClubV.Herosectionbuttontext.Rendered %></a>
					        </div>
					    
					        <img class="medals" src="/virginactive/images/sections/kids/going_for_gold.png" alt="Going 4 Gold medals" width="439" height="172" />
					    
					    
					    </section>	
				    </asp:PlaceHolder>

				    <img src="/virginactive/images/placeholders/kids-hero.jpg" alt="Club-V" width="978" height="361" />				

				    <section class="full-width-panel-1">
					    <img src="/virginactive/images/placeholders/kids-panel-1.png" alt="<%= ClubV.Sectiononeheading.Rendered %>" width="224" height="312" class="fl"  />
					    <h2><%= ClubV.Sectiononeheading.Rendered %></h2>
	              	    <%= ClubV.Sectiononetext.Text %>
					    <%= MoreLink %>
				    </section>
				    
				    <section class="full-width-panel-2">
					    <img src="/virginactive/images/placeholders/kids-panel-2.png" alt="<%= ClubV.Sectiontwoheading.Rendered %>" width="386" height="296" class="fr"  />
					    <h2><%= ClubV.Sectiontwoheading.Rendered %></h2>
                   	    <%= ClubV.Sectiontwotext.Text %>
					    <%= MoreLink %>
				    </section>
				    
                    <section class="full-width-panel-3">
                        <img src="/virginactive/images/placeholders/kids-panel-3.png" alt="<%= ClubV.Sectionthreeheading.Rendered %>" width="386" height="305" />
					    <h2><%= ClubV.Sectionthreeheading.Rendered %></h2>
                   	    <%= ClubV.Sectionthreetext.Text %>
					    <%= MoreLink %>
                    </section>
                    
				    <section class="full-width-panel-4">
					    <img src="/virginactive/images/placeholders/kids-panel-4.png" alt="<%= ClubV.Sectionfourheading.Rendered%>" width="373" height="256" class="fl"  />
					    <h2><%= ClubV.Sectionfourheading.Rendered%></h2>
                   	    <%= ClubV.Sectionfourtext.Text%>
					    <%= MoreLink %>
				    </section>
				    
				    <section class="full-width-panel-5">
					    <img src="/virginactive/images/placeholders/kids-panel-5.png" alt="<%= ClubV.Sectionfiveheading.Rendered %>" width="335" height="272" class="fr"  />
					    <h2><%= ClubV.Sectionfiveheading.Rendered %></h2>
                   	    <%= ClubV.Sectionfivetext.Text %>
					    <%= MoreLink %>
                    </section>
                    
			    </div>
                  <div class="footer-cta">
				    <p><%= Translate.Text("See our Club-V facilities by getting in touch today")%></p> 
                     <p class="cta"><%= MoreBtnLink %></p>
			    </div>
            </div> <!-- /content -->