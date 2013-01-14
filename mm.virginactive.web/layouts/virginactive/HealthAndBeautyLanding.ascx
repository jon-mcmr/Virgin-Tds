<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HealthAndBeautyLanding.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.HealthAndBeautyLanding" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
            
        <div id="content">	            
			<div class="layout-panels">
			    <section class="<%= CssClass %>">
                    <div class="section-image-panel">
                        <%= currentItem.Abstract.Image.RenderCrop("938x310")%>	
	                </div>	
	                <div class="fl">
	                    <h2><%= currentItem.Abstract.Subheading.Text%></h2>
					    <p class="item"><%= MoreLink %></p>
	                </div>
                    <div class="summary-text"><%= currentItem.Abstract.Summary.Text%></div>
	            </section>
                <asp:PlaceHolder ID="phPanels" runat="server" /> 					
            </div>
			<div class="footer-cta">
				<p><%= Translate.Text("Find out what treatments and offers are in your club")%></p> 
				<p class="cta"><%= MoreButtonLink %><%--<a href="#" class="va-overlay-link btn btn-cta" ><%= Translate.Text("Find your club")%></a>--%></p>
			</div>
		</div> <!-- /content -->
            