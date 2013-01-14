<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FeedbackCampaign.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.marketingcampaigns.FeedbackCampaign" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ModuleTemplates" %>
<%--                
<asp:ScriptManager ID="ScriptManager1" runat="server" />
<asp:UpdatePanel ID="pnlForm" UpdateMode="Conditional" runat="server">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnSubmit" />
    </Triggers>
    <ContentTemplate>--%>
	<article id="article" class="feedback" runat="server">

        <input type="hidden" value="<%= campaign.Campaign.CampaignBase.CampaignId.Raw %>" id="CampId" />
        
        <div id="panel-wrap">
            <div id="panel-left">
			    <header>
				    <div id="logo" class="img-rep"><a href="/"><span class="rep"></span><span class="bold"><%= Translate.Text("Virgin Active") %></span> <%= Translate.Text("Health Clubs") %></a></div>
			        <div id="speech-bubble">
                        <h1><%= campaign.Campaign.Heading.Raw %></h1>
                        <span id="speech-bubble-arrow"></span>
                    </div>
			    </header>		            
			    <p class="intro"><%= campaign.Campaign.Introductiontext.Raw%></p>
                <%= campaign.Campaign.Bodytext.Text %>
		    </div>
		    <div id="panel-right">
                <div class="inner inner-carousel" id="imageCarousel" runat="server">
                    <ul id="carousel">
                        <asp:ListView ID="ImageList" runat="server">
                            <LayoutTemplate>
                                <asp:PlaceHolder ID="ItemPlaceholder" runat="server" />
                            </LayoutTemplate>

                            <ItemTemplate>
                                <li><img src="<%# (Container.DataItem as ImageCarouselItem).Image.MediaUrl %>" width="430" height="350" alt="" data-quote="<%# (Container.DataItem as ImageCarouselItem).ImageQuote.Raw %>" data-caption="<%# (Container.DataItem as ImageCarouselItem).ImageCaption.Raw %>" /></li>
                            </ItemTemplate>
                        </asp:ListView>
                    </ul>
                    <p class="btn btn-start launchOverlay"><a href="#"> <%= campaign.Campaign.Buttontext.Raw %></a></p>
		        </div>
                

                <div class="inner inner-story" id="innerstory" runat="server">
                    <h2> <%= campaign.Campaign.Formtitle.Raw %></h2>
                    <p class="intro"><%= campaign.Campaign.Formmessagelabel.Text%></p>
                    <textarea class="story" id="story" runat="server"></textarea>
                    <ul class="btns">
                        <li class="btn btn-prev closeOverlay"><a href="#"><%= Translate.Text("Cancel")%></a></li>
                        <li class="btn btn-next"><a href="#"><%= Translate.Text("Next")%></a></li>
                    </ul>
                </div>

                <div class="inner inner-form" id="innerform" runat="server">
                    <h2><%= campaign.Entrytitle.Raw %></h2>
                    <p class="intro"><%= Translate.Text("Your Details")%></p>
                    <fieldset>
                        <div class="row row-chzn">
                            <label for="<%=clubFindSelect.ClientID%>"><%= Translate.Text("Your club")%></label>
                            <%--<select class="text-club chzn-clublist valRequired" id="clubFindSelect" placeholder="Select a club" runat="server"></select>--%>
                            <asp:dropdownlist class="text-club chzn-clublist valRequired" id="clubFindSelect" placeholder="Select a club" runat="server"></asp:dropdownlist>	
                        </div>
                        <div class="row">
                            <label for="<%=yourname.ClientID%>"><%= Translate.Text("Your name")%></label>
                            <input type="text" id="yourname" class="text text-name valRequired" data-placeholder="Please enter your name" runat="server" />
                        </div>
                        <div class="row">
                            <label for="<%=email.ClientID%>"><%= Translate.Text("Email address")%></label>
                            <input type="text" id="email" class="text text-email valRequired" data-placeholder="Please enter your email" runat="server"  />
                        </div>
                        <div class="row">
                            <label for="<%=contact.ClientID%>"><%= Translate.Text("Contact number")%></label>
                            <input type="text" id="contact" class="text text-contact valRequired" data-placeholder="Please enter your contact number" runat="server"  />
                        </div>
                            
                        <p class="intro divider"><%= Translate.Text("Your Photo (Optional)")%></p>
                        <div class="row">
                            <label for="<%=filename.ClientID%>"><%= Translate.Text("Upload a photo")%></label>
                            <asp:FileUpload ID="filename" class="text file-upload" placeholder="gif/jpg, max 5Mb" data-placeholder="gif/jpg, max 5Mb" data-extplaceholder="gif/jpg format only" data-sizeplaceholder="File size limit 5Mb" runat="server" />
                            <asp:customvalidator ID="cvImageSubmission" runat="server" Display="Dynamic" CssClass="file-upload-error" ></asp:customvalidator>
                        </div>
                        <div class="row row-checkbox">
                            <input type="checkbox" id="subscribe" runat="server"  />
                            <label for="<%=subscribe.ClientID%>"><%= Translate.Text("Subscribe to our newsletter for the latest news and offers")%></label>
                        </div>
                    </fieldset>
                    <ul class="btns">
                        <li class="btn btn-prev"><a href="#"><%= Translate.Text("Back")%></a></li>
                        <li class="btn btn-next"><a href="#"><%= Translate.Text("Next")%></a></li>
                    </ul>
                </div>
                
                <div class="inner inner-check" id="formSummary" runat="server">
                    <h2><%= campaign.Confirmationtitle.Raw %></h2>
                    <p class="intro"><%= campaign.Confirmationlabel.Raw %></p>
                    <div class="box box-story">
                        
                    </div>
                    <div class="box box-details">
                        <p class="intro"><%= Translate.Text("Your Details")%></p>
                        <div class="row">
                            <p class="left"><%= Translate.Text("Your club")%>:</p>
                            <p class="right box-club"></p>
                        </div>
                        <div class="row">
                            <p class="left"><%= Translate.Text("Your name")%>:</p>
                            <p class="right box-name"></p>
                        </div>
                        <div class="row">
                            <p class="left"><%= Translate.Text("Email address")%>:</p>
                            <p class="right box-email"></p>
                        </div>
                        <div class="row">
                            <p class="left"><%= Translate.Text("Contact number")%>:</p>
                            <p class="right box-contact"></p>
                        </div>
                    </div>
                    <asp:customvalidator ID="cvFormSubmission" runat="server" Display="Dynamic" CssClass="FileSizeInvalid"></asp:customvalidator>
                    <ul class="btns">
                        <li class="btn btn-prev"><a href="#"><%= Translate.Text("Back")%></a></li>
                        <li id="gaq-<%= campaign.Campaign.CampaignBase.Isweblead.Checked == true ? "weblead" : "notweblead" %>" data-gaqlabel="campaign-feedback" class="btn btn-submit"><asp:LinkButton ID="btnSubmit" runat="server" onclick="btnSubmit_Click"><%= Translate.Text("Submit")%></asp:LinkButton></li>
                    </ul>
                </div>
                <div class="inner inner-thanks" id="formCompleted" runat="server">
                    <div id="close" class="img-rep closeOverlay"><a href="#"><span class="rep"></span><%= Translate.Text("Close")%></a></div>
                    <h2><%= campaign.Campaign.Thankyoutitle.Raw%></h2>
                    <p class="intro"><%= campaign.Campaign.Thankyoutext.Raw%></p>
                </div>
            </div>
        </div>
    </article>	
<%--                            </ContentTemplate>
                        </asp:UpdatePanel> --%>


