<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ModuleEnquiry.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.marketingcampaigns.ModuleEnquiry" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.Campaigns" %>
<%@ Import Namespace="Sitecore.Resources.Media" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.common.Constants.SitecoreConstants" %>				

<div class="site_container" id="pageContainer" runat="server">

    <div class="wrapper clearfix">
        <div class="header-top">
            <a href="<%= homePageUrl%>" id="logo"><img src="/virginactive/images/campaigns/personalbest/logo.png"></a>
            <h1 class="left headertext">
                <a href="<%= homePageUrl%>"><%= Translate.Text("Virgin Active") %> <span><%= Translate.Text("Health Clubs")%></span></a>
            </h1>
            <h2><%= Translate.Text("live happily ever active")%></h2>
        </div>
    </div>
    <div id="carouselContainer">
        <div id="carousel" class="wrapper clearfix">
            <div class="header">
                <h1><%= currentCampaign.Header.Rendered%></h1>
            </div>
            <!--Carousel Images-->
            <ul id="carouselImages">
                <asp:ListView ID="CarouselPanels" runat="server">
                    <LayoutTemplate>
                        <asp:PlaceHolder ID="ItemPlaceholder" runat="server" />
                    </LayoutTemplate> 
                    <ItemTemplate>
                        <li id="image_<%# Container.DataItemIndex%>" <%# (Container.DataItem as ModuleItem).IsFirst ? @"class=""first""" : "" %>><a><img src="<%# Sitecore.StringUtil.EnsurePrefix('/', (Container.DataItem as ModuleItem).Carouselimage.MediaUrl) %>" alt="" /></a></li>
                    </ItemTemplate>
                </asp:ListView>	  
            </ul>
            <ul id="carouselCaptions">
                <!--Captions-->
                <asp:ListView ID="CaptionPanels" runat="server" OnItemDataBound="CaptionPanels_OnItemDataBound">
                    <LayoutTemplate>
                        <asp:PlaceHolder ID="ItemPlaceholder" runat="server" />
                    </LayoutTemplate> 
                    <ItemTemplate>  
                        <li <%# (Container.DataItem as ModuleItem).IsFirst ? @"class=""first""" : "" %>>
                            <h3><a href="#image_<%# Container.DataItemIndex%>"><%# (Container.DataItem as ModuleItem).Carouselheading.Rendered%></a></h3>
                            <p><%# (Container.DataItem as ModuleItem).Carouselintroduction.Rendered%></p>
                            <asp:Literal ID="ltrMoreInfoLink" runat="server"></asp:Literal>
                        </li>
                    </ItemTemplate>
                </asp:ListView>
            </ul>

            <!--Overlays-->
            <asp:ListView ID="OverlayPanels" runat="server" OnItemDataBound="OverlayPanels_OnItemDataBound">
                <LayoutTemplate>
                    <asp:PlaceHolder ID="ItemPlaceholder" runat="server" />
                </LayoutTemplate> 
                <ItemTemplate>  
                    <div id="carousel_<%# Container.DataItemIndex%>" class="carousel-modal clearfix">
                        <div class="modal-media">
                            <asp:Literal ID="ltrMediaContent" runat="server"></asp:Literal>
                        </div>
                        <div class="modal-text">    
                            <h2><%# (Container.DataItem as ModuleItem).Detailheading.Rendered%></h2>
                            <%# (Container.DataItem as ModuleItem).Detailbody.Rendered%>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:ListView> 
        </div>	 
    </div>
    <div class="wrapper clearfix">     
        <!--FORM AREA-->
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <%--<asp:UpdateProgress runat="server" id="PageUpdateProgress">
            <ProgressTemplate>
                <div id="skm_LockBackground" class="LockBackground">
                </div>
                <div id="skm_LockPane" class="LockPane">
                    <div id="skm_LockPaneText">
                        <span><img id="imgProgress" alt="Processing Image" src="/virginactive/Images/indicator-big.gif" class="ProgressImage" />
                            <span>Loading...</span>
                        </span>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>--%>
        <asp:UpdatePanel ID="pnlForm" UpdateMode="Conditional" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSubmit" />
            </Triggers>
            <ContentTemplate>
                <div class="enquiryInfo" id="pnlEnquiryPanel" runat="server">
                    <h2><%= currentCampaign.Heading.Rendered%></h2>
                    <%= currentCampaign.Body.Rendered%>
                </div>  
                <!--Uncompleted Form-->
                <div class="enquiry enquiry-member form-wrap clearfix" id="formToComplete" runat="server">
                    <div class="enquirypanel" data-gaqlabel="campaign-<%=currentCampaign.CampaignBase.Campaigncode.Rendered%>" data-gaqaction="EnquireNow" data-gaqcategory="CTA" data-formtype="webleads">
                        <h3 class="ffb"><%= Translate.Text("Enquiry form")%></h3>
                        <p><%= Translate.Text("Please complete all the fields")%></p>
                        <div class="badge">
                            <span><%= currentCampaign.Graphictext.Rendered%></span>
                        </div>
                        <fieldset class="find">		
                            <div class="wrap selectParentWrap wrap-enquiry-member">							
                                <label for="<%=clubFindSelect.ClientID%>"><%= Translate.Text("Preferred club")%></label>
                                <asp:DropDownList class="chzn-clublist update-inputs select-wrap form-element" id="clubFindSelect" runat="server"></asp:DropDownList>
                            </div>
                        </fieldset>
                        <fieldset class="aboutyou">
                            <div class="wrap wrap-name">
                                <div class="form-wrap">
                                    <label for="<%=txtName.ClientID%>"><%= Translate.Text("Your name")%></label>
                                    <input type="text" maxlength="30" id="txtName" class="text form-element" runat="server" />
                                </div>
                            </div>

                            <div class="wrap wrap-email">
                                <div class="form-wrap">
                                    <label for="<%=txtEmail.ClientID%>"><%= Translate.Text("Email address")%></label>
                                    <input type="text" maxlength="40" id="txtEmail" class="text form-element" runat="server" />
                                </div>
                            </div>
                            
                            <div class="wrap wrap-contact">
                                <div class="form-wrap">
                                    <label for="<%=txtPhone.ClientID%>"><%= Translate.Text("Contact number")%></label>
                                    <input type="text" maxlength="20"  id="txtPhone" class="text form-element" runat="server" />
                                </div>
                            </div>	

                            <div class="wrap wrap-how">
                                <div class="form-wrap">
                                    <label for="<%=drpHowDidYouFindUs.ClientID%>"><%= Translate.Text("How did you find us?")%></label>
                                    <asp:dropdownlist class="text chzn-selectbox form-element" runat="server" id="drpHowDidYouFindUs"/>
                                </div>
                            </div>								
                        </fieldset>
                        <fieldset class="check">
                            <div class="wrap">
                                <input type="checkbox" id="chkSubscribe" class="checkbox" runat="server" checked="checked" /> 
                                <label for="<%=chkSubscribe.ClientID%>" class="check"><%= Translate.Text("Subscribe to our newsletter for the latest news and offers")%></label>
                            </div>
                        </fieldset>

                        <fieldset id="gaq-campaign-<%=currentCampaign.CampaignBase.Campaigncode.Rendered%>" class="button">
	                        <asp:button class="btn btn-cta-xl btn-submit" id="btnSubmit" runat="server" onclick="btnSubmit_Click" OnClientClick="return $.virginactive_campaign.module_enquiry.form_validation()" />
	                        <asp:LinkButton ID="btnLink" runat="server" /><!--this is required to load __doPostBack code -->
                        </fieldset>
                        <span class="enquiry-panel-corner">&nbsp;</span>
                    </div>
                </div>
                <!--Confirmation of form submission-->
                <div class="enquiry enquiry-member enquiry-member-thanks" id="formCompleted" runat="server" visible="false">
                    <div class="panel-l left">
                        <div class="grid1 clearfix">
                                <div id="speech-bubble">
                                <h2><%= Translate.Text("thanks for your interest")%></h2>
                            </div>
                        </div>
                        <div class="grid2">
                                <h3><%= Translate.Text("A team member from")%> <%= CurrentClub.Clubname.Rendered%> <%= Translate.Text("will call you shortly.")%></h3>
                        </div>
                        <img class="hero-image" src="/virginactive/images/backgrounds/bg-enquiry-from-thanks-small.gif" alt="">
                    </div>

                    <div class="enquirypanel confirmation">
                        <h3><%= Translate.Text("Details supplied")%></h3>
                        <!--<p class="top"><%= Translate.Text("A team member from")%> <%= CurrentClub.Clubname.Rendered%> <%= Translate.Text("will call you shortly.")%></p>-->
                        <dl class="details-preffered">
                            <dt><%= Translate.Text("Preferred club")%>:</dt>
                            <dd><%= CurrentClub.Clubname.Rendered%></dd>
                        </dl>
                        <dl class="details-name wrap">
                            <dt><%= Translate.Text("Your name")%>:</dt>
                            <dd><%=txtName.Value.Trim()%></dd>
                        </dl>
                        <dl class="details-email wrap">
                            <dt><%= Translate.Text("Email address")%>:</dt>
                            <dd><%=txtEmail.Value.Trim()%></dd>
                        </dl>
                            <% if (txtPhone.Value.Trim() != "")
                                {%>
                            <dl class="details-number wrap">
                                <dt><%= Translate.Text("Contact number")%>:</dt>
                                <dd><%=txtPhone.Value.Trim()%></dd>
                            </dl>
                            <% }%>
                            <% if (drpHowDidYouFindUs.SelectedValue.ToString() != "" && drpHowDidYouFindUs.SelectedValue.ToString() != Translate.Text("Select"))
                                {%>
                            <dl class="details-find wrap">
                                <dt><%= Translate.Text("How did you find us")%>:</dt>
                                <dd><%=drpHowDidYouFindUs.SelectedValue.ToString()%></dd>
                            </dl>
                            <% }%>
                        <p><%=chkSubscribe.Checked ? Translate.Text("You will receive our newsletter but can unsubscribe at any time.") : Translate.Text("You have opted NOT to receive our newsletter.")%></p>
                        <span class="enquiry-panel-corner">&nbsp;</span>
                    </div>
                                    
                </div>
            </ContentTemplate>
        </asp:UpdatePanel> 
        <!--END OF FORM AREA-->
    </div>     
	<div id="footerContainer">
        <div class="footer wrapper clearfix">
		    <ul id="footer-links">
			    <li><%= Translate.Text("© Copyright ##Year## Virgin Active. All rights reserved.")%> / </li>
                <li><a href="<%= PrivacyPolicyUrl %>" title="<%# Translate.Text("Read the Privacy Policy")%>"><%= Translate.Text("Privacy Policy")%></a> /</li>
                <li><a href="<%= TermsConditionsUrl %>" title="<%# Translate.Text("Read the Terms and Conditions")%>"><%= Translate.Text("Terms &amp; Conditions")%></a></li>
		    </ul>
			<ul id="social-links" class="right">
				<li><a href="<%= Settings.YouTubeLinkUrl %>" class="youtube external" target="_blank"><span></span><%= Translate.Text("You Tube")%></a></li>
				<li><a href="<%= Settings.FacebookLinkUrl %>" class="facebook external" target="_blank"><span></span><%= Translate.Text("Facebook")%></a></li>
				<li><a href="<%= Settings.TwitterLinkUrl %>" class="twitter external" target="_blank"><span></span><%= Translate.Text("Twitter")%></a></li>
			</ul>	
	    </div>
    </div>
</div>