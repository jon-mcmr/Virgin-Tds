<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReferAFriend.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.marketingcampaigns.ReferAFriend" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ModuleTemplates" %>
               
<asp:ScriptManager ID="ScriptManager1" runat="server" /><%-- 
<asp:UpdatePanel ID="pnlForm" UpdateMode="Conditional" runat="server">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnSubmit" />
    </Triggers>
    <ContentTemplate>--%>
	<article id="article" class="static" runat="server">

        <input type="hidden" value="<%= campaign.CampaignBase.CampaignId.Raw %>" id="CampId" />
        
        <div id="panel-wrap">
            <div id="panel-left">
			    <header>
				    <div id="logo" class="img-rep"><a href="/"><span class="rep"></span><span class="bold"><%= Translate.Text("Virgin Active") %></span> <%= Translate.Text("Health Clubs") %></a></div>
			        <div id="speech-bubble">
                        <h1><%= campaign.Heading.Raw %></h1>
                        <span id="speech-bubble-arrow"></span>
                    </div>
			    </header>		            
			    <p class="intro"><%= campaign.Introductiontext.Raw%></p>
                <%= campaign.Bodytext.Text %>
		    </div>
		    <div id="panel-right">
                <div class="inner inner-static" id="imageStatic" runat="server">
                    <%= campaign.Sideheroimage.Rendered %>
                    <p class="btn btn-start launchOverlay"><a href="#"> <%= campaign.Buttontext.Raw %></a></p>
		        </div>
                
                <div class="inner inner-form use-their" id="innerForm" runat="server">
                    <h2> <%= campaign.Formtitle.Raw %></h2>
                    <p class="intro"><%= campaign.Formmessagelabel.Text %></p>
                    <fieldset>
                        <div class="row row-chzn">
                            <label for="<%=clubFindSelect.ClientID%>"><%= Translate.Text("Which club")%>?</label>
                            <%--<select class="text-club chzn-clublist valRequired" id="clubFindSelect" placeholder="Select a club" runat="server"></select>--%>
                            <asp:dropdownlist class="text-club chzn-clublist valRequired" id="clubFindSelect" runat="server" placeholder="Select a club"></asp:dropdownlist>	
                        </div>
                        <div class="row">
                            <label for="<%=yourname.ClientID%>"><%= Translate.Text("Their name")%></label>
                            <input type="text" id="yourname" class="text text-name valRequired" data-placeholder="Please enter their name" runat="server" />
                        </div>
                        <div class="row">
                            <label for="<%=email.ClientID%>"><%= Translate.Text("Email address")%></label>
                            <input type="text" id="email" class="text text-email valRequired" data-placeholder="Please enter their email" runat="server"  />
                        </div>
                        <div class="row">
                            <label for="<%=contact.ClientID%>"><%= Translate.Text("Contact number")%></label>
                            <input type="text" id="contact" class="text text-contact valRequired" data-placeholder="Please enter their contact number" runat="server"  />
                        </div>
                        <div class="row row-declaration">
                            <label><%= Translate.Text("By clicking on submit I confirm that the above named individual has consented to being contacted by Virgin Active by email and telephone")%></label>
                        </div>
                    </fieldset>
                    <ul class="btns">
                        <li class="btn btn-prev closeOverlay"><a href="#"><%= Translate.Text("Cancel")%></a></li>
                        <li id="gaq-<%= campaign.CampaignBase.Isweblead.Checked == true ? "weblead" : "notweblead" %>" data-gaqlabel="campaign-<%= campaign.CampaignBase.Campaigncode.Rendered%>" class="btn btn-submit"><asp:LinkButton ID="btnSubmit" runat="server" onclick="btnSubmit_Click"><%= Translate.Text("Submit")%></asp:LinkButton></li>
                    </ul>
                </div>
                
                <div class="inner inner-thanks" id="formCompleted" runat="server">
                    <div id="close" class="img-rep closeOverlay"><a href="#"><span class="rep"></span><%= Translate.Text("Close")%></a></div>
                    <h2><%= campaign.Thankyoutitle.Raw%></h2>
                    <p class="intro"><%= campaign.Thankyoutext.Raw%></p>
                </div>
            </div>
        </div>
    </article>	
<%--                            </ContentTemplate>
                        </asp:UpdatePanel> --%>


