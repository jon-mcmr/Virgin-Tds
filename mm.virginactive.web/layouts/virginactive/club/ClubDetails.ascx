<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClubDetails.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.club.ClubDetails" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ModuleTemplates" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>     
<%@ Import Namespace="System.Web" %>
            <input type="hidden" id="hdnClubIsHomeClub" runat="server" class="clubIsHomeClub"/>
            <input type="hidden" id="hdnClubId" runat="server" class="clubId"/>
            <div class="cta-panel">
                 <% if (current.IsClassic)
                 { %>  
                    <p><a href="<%= enqForm.Url + "?sc_trk=enq&c=" + currentClub.InnerItem.ID.ToShortID() %>&Title=<%= bookATourLinkTitle%>" class="btn btn-cta"><%= Translate.Text("Book a Visit")%></a></p>                
                    <p class="call">
                        <%= Translate.Text("CALL US")%>
                        <span><%= currentClub.Salestelephonenumber.Rendered%></span>
                    </p>
                <% } %>
                <% if (!currentClub.DisableSetAsHomeClub.Checked)
                 { %>  
                <div id="SetHomeClubCookie" runat="server" class="set-home-club-cookie">
                    <p class="homeclub">
                        <a href="#" class="club-set-home-club"><asp:Literal runat="server" ID="SetHomeClubCookieLiteral" /></a>
    <%--                <asp:ScriptManager ID="ScriptManager1" runat="server" />
                    <asp:UpdatePanel ID="pnlForm" UpdateMode="Conditional" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSetHomeClub" />
                        </Triggers>
                        <ContentTemplate>
                        </ContentTemplate>
                    </asp:UpdatePanel>--%>
                    </p>
                </div>
                <% } %>

                <% if (!current.IsClassic)
                 { %>  
                 <p class="phone-number-label"><%= currentClub.Phonenoheading.Rendered %></p>
                <p class="call ffb"><span class="rTapNumber<%= currentClub.ResponseTapCode.Rendered %>"><%= currentClub.Salestelephonenumber.Rendered%></span></p>
                <p><a href="<%= enqForm.Url + "?sc_trk=enq&c=" + currentClub.InnerItem.ID.ToShortID() %>&Title=<%= bookATourLinkTitle%>" class="btn btn-cta-big gaqTag" data-gaqcategory="CTA" data-gaqaction="BookATour" data-gaqlabel="Membership"><%= Translate.Text("Book a Visit")%></a></p>
                <p><a href="<%= enqForm.Url + "?sc_trk=enq&c=" + currentClub.InnerItem.ID.ToShortID() %>&Title=<%= membershipEnquiryLinkTitle%>" class="btn btn-cta-big gaqTag" data-gaqcategory="CTA" data-gaqaction="MembershipEnquiry" data-gaqlabel="Membership"><%= Translate.Text("Membership Enquiry")%></a></p>
                <% } %>
            </div>
            <h3><%= Translate.Text("Club information") %></h3>     
            
            <!-- Club widget -->
            <section class="multi-access">
                <h4><%= currentClub.FileImageLinkWidget.Widget.Heading.Rendered%></h4>
                 <%= currentClub.FileImageLinkWidget.Image.RenderCrop("80x80")%>
                 <%= currentClub.FileImageLinkWidget.Widget.Bodytext.Text%>
                <p>
                <% if (!String.IsNullOrEmpty(currentClub.FileImageLinkWidget.File.Raw))
                   { %>
                    <a href="<%= currentClub.FileImageLinkWidget.File.MediaUrl  %>"> <%= currentClub.FileImageLinkWidget.Widget.Buttontext.Raw%></a>
                <% }
                   else
                   { %>
                   <a href="<%= currentClub.FileImageLinkWidget.Widget.Buttonlink.Url  %>"> <%= currentClub.FileImageLinkWidget.Widget.Buttontext.Raw%></a>
                <% } %>
                </p>
            </section>
     
            <section class="manager" runat="server" id="manager">
                <h4><%= StaffMember.Person.Title.Rendered%></h4>
                <%= StaffMember.Person.Profileimage.RenderCrop("80x80")%>
                <div class="overflow">
                    <p class="name"><%= fullname%></p>
                    <blockquote>"<%= StaffMember.Person.Quotetitle.Rendered%>"</blockquote>
                <p class="contact"><a href="<%= contactForm.Url %>#contact-manager">Contact <%= StaffMember.Person.Firstname.Rendered%></a></p>
                </div>
            </section>
              
            <section class="map-overlay-parent findus">
                <hgroup>
                <% if (!current.IsClassic){ %>
                    <h4><%= Translate.Text("How to find us")%></h4>
                     <% } %> 
                    <h5><%= Translate.Text("Address")%></h5>
                </hgroup>
                <div class="vcard">
                    <div class="fn org"><%= currentClub.Clubname.Rendered%></div>
                    <div class="adr">
                        <span class="street-address"><%= currentClub.Addressline1.Rendered%></span>
                        <span class="locality"><%= currentClub.Addressline2.Rendered%></span>
                        <span class="locality"><%= currentClub.Addressline3.Rendered%></span>
                        <span class="region"><%= currentClub.Addressline4.Rendered%></span>
                        <span class="postal-code"><%= currentClub.Postcode.Rendered%></span>
                    </div>
                    <div class="geo hidden">Geolocation: 
                        <span class="lat"><%= currentClub.Lat.Rendered%></span>, 
                        <span class="lng"><%= currentClub.Long.Rendered%></span>
                    </div>
                    <span class="tel">
                        <span class="adr-row"><span class="type"><%= Translate.Text("Sales")%></span> <span class="value rTapNumber<%= currentClub.ResponseTapCode.Rendered %>"><%= currentClub.Salestelephonenumber.Rendered%></span></span>
                        <span class="adr-row"><span class="type"><%= Translate.Text("Members")%></span> <span class="value"><%= currentClub.Memberstelephonenumber.Rendered%></span></span>
                    </span>
				</div>

                <h5 class="map">Map</h5>
                <% if (!current.IsClassic) {%>
                    <a href="#" class="va-overlay-link"><img src="http://maps.googleapis.com/maps/api/staticmap?zoom=14&size=240x160&maptype=roadmap&markers=color%3Ared%7Ccolor%3Ared%7Csize%3Asmall%7C<%= currentClub.Lat.Rendered%>%2C<%= currentClub.Long.Rendered%>&sensor=false" alt="" /></a>
                <% } else {%>
                    <a href="#" class="va-overlay-link"><img src="http://maps.googleapis.com/maps/api/staticmap?zoom=14&size=230x160&maptype=roadmap&markers=color%3Ared%7Ccolor%3Ared%7Csize%3Asmall%7C<%= currentClub.Lat.Rendered%>%2C<%= currentClub.Long.Rendered%>&sensor=false" alt="" /></a>
                <% } %>
                <ul>
                    <li class="static-view va-overlay-link"><a href="#"><%= Translate.Text("View large")%></a></li>
                    <li class="static-directions va-overlay-link"><a href="#"><%= Translate.Text("Get directions")%></a></li>
                </ul>
            </section>
            
            <ul id="map-overlay-info" class="hidden">
                <li class="maptitle" data-getdirections="<%= Translate.Text("Get directions")%>" data-map="<%= Translate.Text("Map")%>" data-address="<%= Translate.Text("Address")%>" data-directions="<%= Translate.Text("Directions")%>" data-starting="Starting location" data-suggestedroutes="Suggested Routes"><%= Translate.Text("How to find us")%></li>
                <li class="clubname"><%= currentClub.Clubname.Rendered%></li>
                <li class="adr">
                    <span class="street-address"><%= currentClub.Addressline1.Rendered%></span>
                    <span class="locality"><%= currentClub.Addressline2.Rendered%></span>
                    <span class="locality"><%= currentClub.Addressline3.Rendered%></span>
                    <span class="region"><%= currentClub.Addressline4.Rendered%></span>
                    <span class="postal-code"><%= currentClub.Postcode.Rendered%></span>
                </li>
                <li class="geo">
                    <span class="lat"><%= currentClub.Lat.Rendered %></span>
                    <span class="lng"><%= currentClub.Long.Rendered %></span>
                </li>
                <li class="tel">
                    <span class="adr-row"><span class="type"><%= Translate.Text("Sales")%></span> <span class="value"><%= currentClub.Salestelephonenumber.Rendered%></span></span>
                    <span class="adr-row"><span class="type"><%= Translate.Text("Members")%></span> <span class="value"><%= currentClub.Memberstelephonenumber.Rendered%></span></span>
                </li>
                <li class="other-info">
                    <%= parking%>
                    <%= transport%>        
                    <%= openingHours%>     
                    <h5><%= Translate.Text("Towels") %></h5>
                    <%= current.ClubItm.GetTowelStatus() %>
                </li>
            </ul>
                    
            <div class="info">
                <%= parking%>
                <%= transport%>        
                <%= openingHours%>     
                <h5><%= Translate.Text("Towels") %></h5>
                <%= current.ClubItm.GetTowelStatus() %>
            </div>
