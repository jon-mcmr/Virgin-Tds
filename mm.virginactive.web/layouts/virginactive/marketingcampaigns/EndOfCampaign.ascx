<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EndOfCampaign.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.marketingcampaigns.EndOfCampaign" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>

        <div id="campaign-end">
			<header>
	            <div id="logo" class="img-rep">
                    <a href="/"><span class="rep"></span><span class="bold"><%= Translate.Text("Virgin Active") %></span> <%= Translate.Text("Health Clubs")%></a>
                </div>
            </header>	
            <div id="content">
                    <%= campaign.Endofcampaignbodytext.Rendered %>
                    <div class="footer-cta">
		                <p><%= campaign.Endofcampaigncalltoaction.Rendered%></p>
		                <p class="cta">
                            <a class="btn btn-cta-big" href="<%= campaign.Endofcampaignlink.Url %>" class="btn btn-cta-xl"><%= campaign.Endofcampaignlinkbuttontitle.Rendered%></a>
		               </p>
	                </div>
            </div>

            <footer>
		        <ul id="footer-list">
			         <li><%= Translate.Text("© Copyright ##Year## Virgin Active. All rights reserved.")%></li>
		        </ul>
	        </footer>    
                  
        </div>
