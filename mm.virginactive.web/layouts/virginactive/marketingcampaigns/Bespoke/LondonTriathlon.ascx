<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LondonTriathlon.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.marketingcampaigns.LondonTriathlon" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ModuleTemplates" %>

    <header>
        <div class="inner">
            <div id="titles">
		        <div id="logo" class="img-rep"><a href="/"><span class="rep"></span><span class="bold"><%= Translate.Text("Virgin Active") %></span> <%= Translate.Text("Health Clubs") %></a></div>
		        <h1>London Triathlon 2012 Ballot</h1>
            </div>
        </div>	
	</header>

    <div id="wrapper" class="wrapper-form" runat="server">
        <div id="intro" class="inner">
            <div id="speech-bubble">
                <p>it's not too late</p>
                <span id="speech-bubble-arrow"></span>
            </div>

            <div id="intro-box">
                <p class="intro">The <span class="bold">Virgin Active London Triathlon</span> ballot is now open and will close at 23:59 on <span class="bold">Friday 23rd March 2012</span>.</p>
                
                <div id="ballotbox">
                    <p id="enter">Enter the ballot</p>
                    <img src="/va_campaigns/Bespoke/LondonTriathlon/img/arrow.gif" width="32" height="32" alt="" id="arrow" />
                </div>
                <p id="goodluck" class="img-rep"><span class="rep"></span>Good luck!</p>
            </div>
        </div>	

	    <article id="pnlForm" class="form" runat="server">
            <div class="inner">
                <h2>Entry form</h2>
                <p class="intro">There are 50 places available for staff and members in the Virgin Active team. If you’d like to get involved, just complete the form below and we’ll enter you into the ballot. </p>

                <div id="panel-wrap"> 
                    <fieldset id="you">
                        <legend>You and Virgin Active</legend>
                        <div class="row-wrap">
                            <div id="status" class="row row-radio row-toValidate" data-errormsg="Please select your status">
                                <p class="left">Are you a...?</p>
                                <input type="radio" id="member" name="staff" value="member" runat="server" /><label for="<%=member.ClientID%>">Member</label>
                                <input type="radio" id="staff" name="staff" value="staff" runat="server" /><label for="<%=staff.ClientID%>">Staff</label>
                            </div>
                            <div id="membership" class="row" data-errormsg="Please enter your membership number" data-validmsg="Please enter a valid membership number">
                                <p class="left"><label for="<%=memberNo.ClientID%>">Membership number</label></p>
                                <input type="text" id="memberNo" runat="server" class="text text-member" />
                            </div>
                            <div class="row" data-errormsg="Please select a club">
                                <p class="left"><label for="clubname">at which club?</label></p>
                                <%--<select class="chzn-clublist selectclub" id="clubFindSelect" runat="server"></select>--%>
                                <asp:dropdownlist class="chzn-clublist selectclub" id="clubFindSelect" runat="server"></asp:dropdownlist>	
                            </div>
                        </div>
                    </fieldset>
                    <fieldset id="about"> 
                        <legend>About you</legend>
                        <div class="row-wrap">
                            <div id="nameCheck" class="row" data-errormsg="Please enter your name" data-validmsg="Please enter a valid name">
                                <p class="left"><label for="<%=firstName.ClientID%>">Your name</label></p>
                                <input type="text" id="firstName" runat="server" class="text text-half text-firstname text-multi" placeholder="First" />
                                <input type="text" id="surname" runat="server" class="text text-half text-surname text-multi" placeholder="Last" />
                            </div>
                            <div class="row" data-errormsg="Please enter your email address" data-validmsg="Please enter a valid email address">
                                <p class="left"><label for="<%=email.ClientID%>">Email address</label></p>
                                <input type="text" id="email" runat="server" class="text text-email" />
                            </div>
                            <div class="row" data-errormsg="Please enter your address" data-validmsg="Please enter a valid address">
                                <p class="left"><label for="<%=address1.ClientID%>">Address</label></p>
                                <input type="text" id="address1" runat="server" class="text text-address" />
                                <input type="text" id="address2" runat="server" class="text text-address text-address2" />
                            </div>
                            <div class="row" data-errormsg="Please enter your town/city" data-validmsg="Please enter a valid town/city">
                                <p class="left"><label for="<%=address3.ClientID%>">Town/city</label></p>
                                <input type="text" id="address3" runat="server" class="text text-town" />
                            </div>
                            <div class="row" data-errormsg="Please enter your postcode" data-validmsg="Please enter a valid postcode">
                                <p class="left"><label for="<%=postcode.ClientID%>">Postcode</label></p>
                                <input type="text" id="postcode" runat="server" class="text text-half text-postcode" />
                            </div>
                            <div class="row" data-errormsg="Please enter your mobile number" data-validmsg="Please enter a valid mobile number">
                                <p class="left"><label for="<%=number.ClientID%>">Mobile number</label>
                                <a class="tooltip">
                                    <img width="16" height="16" alt="More information" src="/va_campaigns/Bespoke/LondonTriathlon/img/info.png">
                                        <em class="info-box">We will only use your mobile number to contact you directly about the event if you get a place. It will not be used for general marketing purposes.</em></a>
                                </p>
                                
                                <input type="text" id="number" runat="server" class="text text-half text-number" />
                            </div>
                            <div class="row" data-errormsg="Please enter your date of birth" data-validmsg="Please enter a valid date of birth">
                                <p class="left"><label for="<%=dob.ClientID%>">Date of birth</label></p>
                                <div class="input-wrap">
                                    <input type="text" id="dob" runat="server" class="text text-half text-dob" placeholder="dd/mm/yyyy" />
                                </div>
                            </div>
                            <div class="row row-radio row-toValidate" data-errormsg="Please select your gender">
                                <p class="left">Gender</p>
                                <input type="radio" id="male" runat="server" name="gender" value="male" /><label for="<%=male.ClientID%>">Male</label>
                                <input type="radio" id="female" runat="server" name="gender" value="female" /><label for="<%=female.ClientID%>">Female</label>
                            </div>
                            <div class="row row-join-bottom" data-errormsg="Please enter your next of kin" data-validmsg="Please enter a valid next of kin">
                                <p class="left"><label for="<%=nextofkin.ClientID%>">Next of kin</label></p>
                                <input type="text" id="nextofkin" runat="server" class="text text-kin" />
                            </div>
                            <div class="row row-join-top" data-errormsg="Please enter a contact number" data-validmsg="Please enter a valid contact number">
                                <p class="left"><label for="<%=nextofkinnumber.ClientID%>">Contact number</label></p>
                                <input type="text" id="nextofkinnumber" runat="server" class="text text-half text-kinnumber" />
                            </div>
                        </div>
                    </fieldset>
                    <fieldset id="event">
                        <legend>The Event</legend>
                        <div class="row-wrap">
                            <div id="eventType" class="row" data-errormsg="Please select the event you wish to enter">
                                <p class="left"><label for="<%=drpEvent.ClientID%>">Which event?</label></p>
                                <asp:dropdownlist class="text text-event" runat="server" id="drpEvent"/>
                            </div>
                            <div id="teamname" class="row" data-errormsg="Please enter your team name" data-validmsg="Please enter a valid team name">
                                <p class="left"><label for="<%=teamname.ClientID%>">Your team name</label></p>
                                <input type="text" id="teamname" runat="server" class="text text-teamname" />
                            </div>      
                            <div id="topsize" class="row" data-errormsg="Please select your top size" data-validmsg="Please select a valid top size">
                                <p class="left"><label for="<%=drpTopsize.ClientID%>">Top size</label><a class="tooltip">
                                    <img width="16" height="16" alt="More information" src="/va_campaigns/Bespoke/LondonTriathlon/img/info.png">
                                        <em class="info-box">If you get a place we will supply you with a participators t-shirt. We don't want to send you the wrong size!</em></a></p>
                                <asp:dropdownlist class="text text-half text-topsize" runat="server" id="drpTopsize" />
                            </div>
                        </div>
                    </fieldset>
                    <div class="row row-checkbox">
                        <input type="checkbox" id="subscribe" value="subscribe" runat="server" />
                        <label for="<%=subscribe.ClientID%>">Subscribe to our newsletter for the latest news and offers</label>
                    </div>
                    <div class="row row-checkbox row-toValidate" data-errormsg="Please agree to our terms and conditions">
                        <input type="checkbox" id="terms" value="terms" runat="server" />
                        <label for="<%=terms.ClientID%>terms">I agree to the <a href="<%= TermsConditionsUrl %>" target="_blank">terms and conditions</a></label>
                    </div>
                    <div class="row row-checkbox">
                        <p>If I am lucky enough to win a place, I will aim to raise £200 for Sparks</p>
                    </div>
                    <div class="row row-last">                       
                        <asp:button type="submit" class="btn btn-submit" text="Submit your entry" id="btnSubmit" runat="server" onclick="btnSubmit_Click" />
                        <asp:LinkButton ID="btnLink" runat="server" /><!--this is required to load __doPostBack code -->
                    </div>
                </div>

                <aside> 
                    <div class="panel">
                        <p class="panel-title">Highlights from 2011 </p>
                        <%-- <p id="encouraging" class="img-rep"><span class="rep"></span>(if you need encouraging)</p> --%>
                        <iframe width="360" height="213" frameborder="0" allowfullscreen="" src="http://www.youtube.com/embed/KITeb96nEdE?showinfo=0"></iframe>
                        <p id="details">Here’s a reminder about what makes the Virgin Active Triathlon so great (check out Mr. Branson in action too!)  </p>
                    </div>
                    <p id="swim" class="img-rep"><span class="rep"></span>Swim in London Docklands. Bike past Big Ben.  Run alongside Canary Wharf</p>
                    <div id="sparks" class="panel">
                        <p class="panel-title">Our charity partner</p>
                        <div class="panel-inner"> 
                            <a href="http://www.virginactive.co.uk/about-us/our-community" target="_blank" title="Opens more information about Sparks website in a new window"><img src="/va_campaigns/Bespoke/LondonTriathlon/img/sparks.gif" width="140" height="73" alt="Sparks" /></a>
                            <p class="content">Help us support our charity Sparks and raise some money for a good cause.</p> 
                            <p class="content"><a href="http://www.virginactive.co.uk/about-us/our-community" target="_blank" title="Opens more information about Sparks website in a new window">Find out more about Sparks</a></p>
                        </div>
                    </div>
                    
                </aside>
            </div>
        </article>

        <article id="pnlThanks" class="thanks" runat="server">
            <div class="inner">
                <div id="panel-wrap">
                    <div class="intro">
                        <h2>Thank you for entering.</h2>
                        <p>We’ll be in touch during the first week in April if you've won a place in the triathlon. If you haven’t heard from us by 15th April, it's safe to assume you haven’t been picked this time. There's always next year, though! </p>
                    </div>
                
                    <p class="title">You and Virgin Active</p>
                    <ul class="details">
                        <li><span class="left">You are <%= member.Checked == true ? "a " : "" %> </span><%= member.Checked == true ? "Member" : "Staff" %></li>
                        <% if (memberNo.Value.Trim() != "")
                            {%>
                            <li><span class="left">Membership number</span><%=memberNo.Value.Trim()%></li>
                        <% }%>                        
                        <li><span class="left">at which club?</span><%=CurrentClub != null ? CurrentClub.Clubname.Rendered : "N/A"%></li>
                    </ul>
                
                    <p class="title">About you</p>
                    <ul class="details">
                        <li><span class="left">Your name</span><%=firstName.Value.Trim()%> <%=surname.Value.Trim()%></li>
                        <li><span class="left">Email address</span><%=email.Value.Trim()%></li>
                        <li><span class="left">Address</span><%=address1.Value.Trim()%></li>
                        <% if (address2.Value.Trim() != "")
                            {%>
                            <li><span class="left"></span><%=address2.Value.Trim()%></li>
                        <% }%>
                        <li><span class="left">Town/city</span><%=address3.Value.Trim()%></li>
                        <li><span class="left">Postcode</span><%=postcode.Value.Trim()%></li>
                        <li><span class="left">Mobile number</span><%=number.Value.Trim()%></li>
                        <li><span class="left">Date of birth</span><%=dob.Value.Trim()%></li>
                        <li><span class="left">Gender</span><%=male.Checked == true ? "Male" : "Female"%></li>
                        <li><span class="left">Next of kin</span><%=nextofkin.Value.Trim()%></li>
                        <li><span class="left">Contact number</span><%=nextofkinnumber.Value.Trim()%></li>
                    </ul>
                    
                    <p class="title">The Event</p>
                    <ul class="details">
                        <li><span class="left">Which event?</span><%=drpEvent.SelectedValue%></li>
                        <% if (drpEvent.SelectedValue.Contains("Relay"))
                           {%>
                            <li><span class="left">Team name</span><%=teamname.Value.Trim()%></li>
                        <% }%>
                        <li><span class="left">Top size</span><%=drpTopsize.SelectedValue%></li>
                    </ul>
                    <p class="subs"><%=subscribe.Checked == true ? "You have subscribed to our newsletter" : "You have opted NOT to receive our newsletter"%></p>
                    <p class="raise">You will aim to raise a minimum of £200 for Sparks</p>
                </div>


                <aside>
                    <div id="speech-bubble">
                        <p>good luck!</p>
                        <span id="speech-bubble-arrow"></span>
                    </div>
                    <p id="next" class="img-rep"><span class="rep"></span>What’s next?</p>
                    <p id="whynot">While you’re waiting to see if you've won, check out the official <a href="http://www.thelondontriathlon.com/" target="_blank" title="Opens London Triathlon website in a new window">London Triathlon website</a> to find out more about the event. You can take a peek at <a href="http://www.flickr.com/photos/62781550@N07/" target="_blank" title="Opens last year's photos in a new window">last year's photos</a>, too. In the meantime, best of luck!</p>
                </aside>
            </div>
        </article>
    </div>	

	<footer>
		<ul id="footer-list">
			<li><%= Translate.Text("© Copyright ##Year## Virgin Active. All rights reserved.")%></li>
            <li><a href="<%= PrivacyPolicyUrl %>" target="_blank">Privacy Policy</a></li>
            <li><a href="<%= TermsConditionsUrl %>" target="_blank">Terms &amp; Conditions</a></li>
		</ul>
	</footer>
    