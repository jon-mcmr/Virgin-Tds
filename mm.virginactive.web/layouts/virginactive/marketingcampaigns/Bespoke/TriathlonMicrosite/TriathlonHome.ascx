<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TriathlonHome.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.marketingcampaigns.TriathlonHome" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ModuleTemplates" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.Campaigns" %>
<div id="centre">
    <div id="content">
        <a href="#email-content" id="btn-email" class="open-overlay">
            <strong>ENJOY<br />EXCLUSIVE CONTENT</strong>
            <span class="desc">Stay on top of the latest offers and updates</span>
            <span class="arrow ir">&gt;</span>
        </a>
        <section id="main" class="clearfix">
            <div class="grid1">
                <h2><strong>the 2012 Virgin Active</strong> <span>London Triathlon</span></h2>
                <p>Congratulations to everyone who took part in this year's Triathlon. It was an amazing event with some outstanding personal bests! Here's to next year.</p>
                <p><a href="https://www.facebook.com/media/set/?set=a.449748961734989.99226.177408402302381&type=3"><strong>See all the action on Facebook</strong></a></p>

            </div>
            <div class="grid2">
                <section id="day-counter">
                    <span id="days-remaining" class="day">0</span>
                    <span class="desc ir">Days to the race</span>
                </section>
                <div id="training">
                    <h2 id="train">Maximise your preparation with our <strong>training resource</strong></h2>
                    <img src="/va_campaigns/Bespoke/LondonTriathlonII/img/arrow.png" width="27" height="27" alt="" id="arrow" />
                </div>  
            </div>
            <aside class="grid3">
                <h2>TRAIN WITH OUR EXPERTS</h2>
                <p>Hit peak condition at one of our <br>122 clubs</p>
                <a href="<%= ClubFinderUrl %>" class="btn-club" target="_blank">Find your club</a>
            </aside>
            <div class="grid-arrow">&nbsp;</div>
        </section>

        
    </div>

    <!-- end content -->
    <section class="ContentFooter">
        <div class="container clearfix">
        <asp:ListView ID="ChildSectionListing" runat="server">
            <LayoutTemplate>
                <asp:PlaceHolder ID="ItemPlaceholder" runat="server" />
            </LayoutTemplate> 
            <ItemTemplate>
            <section class="panel <%# (Container.DataItem as AbstractItem).GetPanelCssClass() %> <%# (Container.DataItem as AbstractItem).IsFirst? "fl" : "fr" %>">
                <h2><a title="<%# new PageSummaryItem((Container.DataItem as AbstractItem).InnerItem).NavigationTitle.Raw%>" href="<%# new PageSummaryItem((Container.DataItem as AbstractItem).InnerItem).Url%>"><%# new PageSummaryItem((Container.DataItem as AbstractItem).InnerItem).NavigationTitle.Raw%></a></h2>
                <a title="<%# new PageSummaryItem((Container.DataItem as AbstractItem).InnerItem).NavigationTitle.Raw%>" href="<%# new PageSummaryItem((Container.DataItem as AbstractItem).InnerItem).Url%>"><%# (Container.DataItem as AbstractItem).Image.RenderCrop("480x210")%></a>
                 <div class="content">
                    <%# (Container.DataItem as AbstractItem).Summary.Rendered%>
                    <div class="panel-arrow"><a class="arrow" title="<%# new PageSummaryItem((Container.DataItem as AbstractItem).InnerItem).NavigationTitle.Raw%>" href="<%# new PageSummaryItem((Container.DataItem as AbstractItem).InnerItem).Url%>"><span></span>Read<br />more</a></div>
                </div>
                <div class="opacity">&nbsp;</div>
            </section>
            </ItemTemplate>
        </asp:ListView>

    <% if (LinkedBlogEntry != null) 
       {%>
        <section id="blog">
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
            </section>
    <% } %> 
        </div>

    </section>
   
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

        <div id="overlay-bg" class="hidden"></div>
  </div>

    