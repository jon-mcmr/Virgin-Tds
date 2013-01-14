<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MobileModuleEnquiry.ascx.cs" Inherits="mm.virginactive.web.layouts.Mobile.Campaigns.MobileModuleEnquiry" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.Campaigns" %>
<%@ Import Namespace="Sitecore.Resources.Media" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.common.Constants.SitecoreConstants" %>				
<%@ Import Namespace="mm.virginactive.common.Helpers" %>				

		<div class="content clubfinder">
			<h1 class="campaignname"><%= currentCampaign.Header.Rendered%></h1>
			
			<div class="carousel">
				<ul class="clearfix items">
                    <asp:ListView ID="CarouselPanels" runat="server">
                        <LayoutTemplate>
                            <asp:PlaceHolder ID="ItemPlaceholder" runat="server" />
                        </LayoutTemplate> 
                        <ItemTemplate>
                            <li>
                                <%--<img src="<%# Sitecore.StringUtil.EnsurePrefix('/', (Container.DataItem as ModuleItem).Carouselimage.MediaUrl) %>" />--%>
                                <img src="<%# Sitecore.StringUtil.EnsurePrefix('/', SitecoreHelper.GetOptimizedMediaUrl((Container.DataItem as ModuleItem).Carouselimage)) %>" />
                                <p><%# (Container.DataItem as ModuleItem).Carouselheading.Rendered %></p>
                            </li>
                        </ItemTemplate>
                    </asp:ListView>	  
				</ul>
			</div>
			
			<div class="data_block">
				 <div class="data_content">
					<h2><%= HtmlRemoval.ReplaceLineBreak(currentCampaign.Heading.Rendered).Trim()%></h2>
                    <%= currentCampaign.Body.Rendered%>
				 </div>
			</div>
			
			<h2><%=Translate.Text("Enquiry Form")%></h2>
			
            <fieldset id="form_fields"  data-name="campaign-<%=currentCampaign.CampaignBase.Campaigncode.Rendered%>">	
                    <asp:DropDownList ID="clubFindSelect" runat="server" class="required" data-required="Please choose a club"></asp:DropDownList>
					
					<input type="text" ID="txtName" runat="server" class="required" data-required="Please enter your name" placeholder="Your name" />
					
					<input type="text" ID="txtEmail" runat="server" class="required email" data-required="Please enter your email address" data-email="Please enter a valid email" data-placeholder="Email address" placeholder="Email address" />
					
					<input type="text" ID="txtPhone" runat="server" class="required phone" data-required="Please enter a your contact number" data-phone="Please enter a valid contact number" data-placeholder="Contact number" placeholder="Contact number" />
					
					<div class="form_row checkbox">                        
						<input type="checkbox" name="subscribe" id="chkSubscribe" runat="server" />
						<label for="<%=chkSubscribe.ClientID%>"><%= Translate.Text("Subscribe to our newsletter")%></label>
					</div>

                    <button runat="server" class="button red" ID="btnSubmit" onclick="return virginactive.mobile.validation.validate()">
                        <span><strong><%= Translate.Text("Enquire now") %></strong></span>
                    </button>
                    <asp:LinkButton name="content_0$btnSubmitLink" ID="btnSubmitLink" runat="server" class="btn-submit" OnClick="btnSubmit_Click"/><!--this is required to load __doPostBack code -->
										
		    </fieldset>

		</div>

