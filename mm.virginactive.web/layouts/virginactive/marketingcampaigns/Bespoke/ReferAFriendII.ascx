<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReferAFriendII.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.marketingcampaigns.ReferAFriendII" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ModuleTemplates" %>
    
<div class="outer-wrap" id="outerWrap" runat="server">
    <header>
        <div class="inner">
            <div id="titles">
		        <div id="logo" class="img-rep"><a href="/"><span class="rep"></span><span class="bold"><%= Translate.Text("Virgin Active") %></span> <%= Translate.Text("Health Clubs") %></a></div>
		        <h1><%= campaign.CampaignTitle.Rendered%></h1>
            </div>
        </div>	
	</header>

    <div id="wrapper" class="wrapper-form" runat="server">
        <div id="intro" class="inner">
            <div id="speech-bubble">
                <%= campaign.Introductiontext.Rendered%>
                <span id="speech-bubble-arrow"></span>
            </div>

            <div id="intro-box">
                <%--<p class="intro">This April, if you introduce a chum to us and they sign up for a year's full membership, we'll give you a month for free. Equally great, your mate'll get the remainder of April on us - and they won't pay a joining fee, either.</p>--%>
                <%= campaign.Bodytext.Rendered%>
                <div id="envelope">
                    <p id="enter"><%= campaign.Imagecaption.Rendered%></p>
                    <img src="/va_campaigns/Bespoke/Refer-A-Friend/img/arrow.gif" width="32" height="32" alt="" id="arrow" />
                </div>
            </div>
        </div>	

	    <article id="pnlForm" class="form" runat="server">
            <div class="inner">
                <a id="formSection" name="formSection"></a>
                <h2><%= campaign.Formtext.Rendered%></h2>
                <p class="intro"><%= campaign.Formtextdetail.Rendered%></p>
                <div class="panel-wrap about-you-panel-wrap"> 
                    <fieldset id="you">
                        <legend><%= Translate.Text("About you")%></legend>
                        <div class="row-wrap first">
                            <div id="membership" class="row" data-errormsg="<%= Translate.Text("Please enter your membership number")%>" data-validmsg="<%= Translate.Text("Please enter a valid membership number")%>">
                                <p class="left"><label for="<%=memberNo.ClientID%>"><%= Translate.Text("Membership no.")%></label></p>
                                <div class="more-info img-rep"><a href=""><em class="rep"></em><em class="visiblyhidden">?</em></a><em class="tooltip" style="display: none;">
                                                    <%= campaign.Tooltip.Rendered%>
                                                    </em></div>
                                <input type="text" id="memberNo" runat="server" class="text text-member" />
                            </div>
                            <div class="row" data-errormsg="<%= Translate.Text("Please select a club")%>">
                                <p class="left"><label for="clubname1"><%= Translate.Text("Your club")%></label></p>
                                <%--<select class="chzn-clublist clublist selectclub" id="clubFindSelect1" runat="server"></select>--%>
                                <asp:dropdownlist class="chzn-clublist clublist selectclub" id="clubFindSelect1" runat="server"></asp:dropdownlist>	
                            </div>
                        </div>
                        <div class="row-wrap second">
                            <div class="row" data-errormsg="<%= Translate.Text("Please enter your name")%>" data-validmsg="<%= Translate.Text("Please enter a valid name")%>">
                                <p class="left"><label for="<%=firstName1.ClientID%>"><%= Translate.Text("Your name")%></label></p>
                                <input type="text" id="firstName1" runat="server" class="text text-half text-firstname text-multi" placeholder="First" />
                                <input type="text" id="surname1" runat="server" class="text text-half text-surname text-multi" placeholder="Last" />
                            </div>
                            <div class="row" data-errormsg="<%= Translate.Text("Please enter your email address")%>" data-validmsg="<%= Translate.Text("Please enter a valid email address")%>">
                                <p class="left"><label for="<%=email1.ClientID%>"><%= Translate.Text("Email address")%></label></p>
                                <input type="text" id="email1" runat="server" class="text text-email" />
                            </div>
                            <div class="row" data-errormsg="<%= Translate.Text("Please enter your mobile number")%>" data-validmsg="<%= Translate.Text("Please enter a valid mobile number")%>">
                                <p class="left"><label for="<%=number1.ClientID%>"><%= Translate.Text("Mobile number")%></label></p>
                                <input type="text" id="number1" runat="server" class="text text-half text-number" />
                            </div>
                        </div>
                    </fieldset>
                    <fieldset id="about"> 
                        <legend><%= Translate.Text("About your friend")%></legend>
                        <div class="row-wrap first">
                            <div class="row" data-errormsg="<%= Translate.Text("Please select a club")%>">
                                <p class="left"><label for="clubname2"><%= Translate.Text("Their preferred club")%></label></p>
                                <%--<select class="chzn-clublist clublist2 selectclub" id="clubFindSelect2" runat="server"></select>--%>
                                <asp:dropdownlist class="chzn-clublist clublist2 selectclub" id="clubFindSelect2" runat="server"></asp:dropdownlist>	
                            </div>
                        </div>
                        <div class="row-wrap second">
                            <div id="nameCheck" class="row" data-errormsg="<%= Translate.Text("Please enter their name")%>" data-validmsg="<%= Translate.Text("Please enter a valid name")%>">
                                <p class="left"><label for="<%=firstName2.ClientID%>"><%= Translate.Text("Their name")%></label></p>
                                <input type="text" id="firstName2" runat="server" class="text text-half text-firstname2 text-multi" placeholder="First" />
                                <input type="text" id="surname2" runat="server" class="text text-half text-surname2 text-multi" placeholder="Last" />
                            </div>
                            <div class="row" data-errormsg="<%= Translate.Text("Please enter their email address")%>" data-validmsg="<%= Translate.Text("Please enter a valid email address")%>">
                                <p class="left"><label for="<%=email2.ClientID%>"><%= Translate.Text("Their email address")%></label></p>
                                <input type="text" id="email2" runat="server" class="text text-email2" />
                            </div>
                            <div class="row" data-errormsg="<%= Translate.Text("Please enter their mobile number")%>" data-validmsg="<%= Translate.Text("Please enter a valid mobile number")%>">
                                <p class="left"><label for="<%=number2.ClientID%>"><%= Translate.Text("Their mobile number")%></label></p>                                
                                <input type="text" id="number2" runat="server" class="text text-half text-number2" />
                            </div>
                        </div>
                    </fieldset>
                    <div class="row row-checkbox row-checkbox-last">
                        <input type="checkbox" id="subscribe" value="subscribe" runat="server" />
                        <label for="<%=subscribe.ClientID%>"><%= campaign.Confirmation1.Rendered%></label>
                    </div>
                    <div class="row row-checkbox row-checkbox-last row-toValidate" data-errormsg="<%= Translate.Text("Please agree to our terms and conditions")%>">
                        <input type="checkbox" id="terms" value="terms" runat="server" />
                        <label for="<%=terms.ClientID %>"><%= campaign.Confirmation2.Rendered%></label>
                    </div>
                    <div class="row row-checkbox row-checkbox-last row-toValidate" data-errormsg="<%= Translate.Text("Please confirm you have notified your friend")%>">
                        <input type="checkbox" id="permission" value="terms" runat="server" />
                        <label for="<%=permission.ClientID%>"><%= campaign.Confirmation3.Rendered%></label>
                    </div>

                    <div class="row row-last">                       
                        <asp:button type="submit" class="btn btn-submit" text="Submit" id="btnSubmit" runat="server" onclick="btnSubmit_Click" />
                        <asp:LinkButton ID="btnLink" runat="server" /><!--this is required to load __doPostBack code -->
                    </div>
                </div>  

                <aside> 
                    <%= campaign.Panelcontent.Rendered%>                    
                </aside>
            </div>
        </article>

        <article id="pnlThanks" class="thanks" runat="server">
            <div class="inner">
                <div class="panel-wrap">
                    <div class="intro">
                        <h2><%= campaign.Thankyouheading.Rendered%></h2>
                        <%= campaign.Thankyoubody.Rendered%>
                    </div>
                
                    <p class="title"><%= Translate.Text("About you")%></p>
                    <ul class="details">
                        <li><span class="left"><%= Translate.Text("Membership number")%></span><span class="right"><%=memberNo.Value.Trim()%></span></li>              
                        <li><span class="left"><%= Translate.Text("Your club")%></span><span class="right"><%=PrimaryClub != null ? PrimaryClub.Clubname.Rendered : "N/A"%></span></li>
                        <li><span class="left"><%= Translate.Text("Your name")%></span><span class="right"><%=firstName1.Value.Trim()%> <%=surname1.Value.Trim()%></span></li>
                        <li><span class="left"><%= Translate.Text("Email address")%></span><span class="right"><%=email1.Value.Trim()%></span></li>
                        <li><span class="left"><%= Translate.Text("Mobile number")%></span><span class="right"><%=number1.Value.Trim()%></span></li>
                    </ul>
                
                    <p class="title"><%= Translate.Text("About your friend")%></p>
                    <ul class="details">
                        <li><span class="left"><%= Translate.Text("Their preferred club")%></span><span class="right"><%=SecondaryClub != null ? SecondaryClub.Clubname.Rendered : "N/A"%></span></li>
                        <li><span class="left"><%= Translate.Text("Their name")%></span><span class="right"><%=firstName2.Value.Trim()%> <%=surname2.Value.Trim()%></span></li>
                        <li><span class="left"><%= Translate.Text("Email address")%></span><span class="right"><%=email2.Value.Trim()%></span></li>
                        <li><span class="left"><%= Translate.Text("Mobile number")%></span><span class="right"><%=number2.Value.Trim()%></span></li>
                    </ul>
                    <p class="subs"><%=subscribe.Checked == true ? Translate.Text("You have subscribed to our newsletter") : Translate.Text("You have opted NOT to receive our newsletter")%></p>
                </div>


                <aside>
                    <div id="envelope" class="img-rep"><span class="rep"></span><%= campaign.Confirmationtext.Rendered%></div>
                    <h2><%= campaign.Whatsnextheading.Rendered%></h2>
                    <%= campaign.Whatsnextbody.Rendered%>
                    <%--<p class="next">We've just sent your amigo an email letting them know they've been referred. If they decide to join us, we'll get in touch to sort out your friendly benefit!</p>--%>
                    <p id="btn-refer" class="btn"><asp:LinkButton ID="lnkReferAnotherFriend" runat="server" onclick="lnkReferAnotherFriend_Click" /></p>
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
</div>
    