<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IndoorRegistration.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.indoortriathlon.IndoorRegistration" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.IndoorTriathlon" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ModuleTemplates" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.common.Helpers" %>

            <div id="content" class="">            	
                <asp:ScriptManager ID="ScriptManager1" runat="server" />
				<asp:UpdatePanel ID="pnlForm" UpdateMode="Conditional" runat="server">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnFindTimeSlot" />
                        <asp:AsyncPostBackTrigger ControlID="btnSelectTimeSlot" />
                        <asp:AsyncPostBackTrigger ControlID="btnPersonalDetails" />
                        <asp:postbacktrigger controlid="btnResetTimeSlot" />
                        <asp:postbacktrigger controlid="btnResetForm" />
                    </Triggers>	
                    <ContentTemplate>
                    <div class="indoor-tri layout-block">
                    <div id="formIntro" runat="server">
	                    <h2><%= currentItem.Heading.Rendered %></h2>
                        <div class="tri-context clearfix"><%= currentItem.Introduction.Rendered %>
                            <img src="<%= Sitecore.StringUtil.EnsurePrefix('/', currentItem.Introductiongraphic.MediaUrl) %>" alt="" />
                        </div>
	               </div>
	                <fieldset class="step_block" id="pnlStep1" runat="server">
	                	<div class="header">
	                		<h3><%= currentItem.Section1heading.Rendered %></h3>
	                		<p><%= currentItem.Section1subheading.Rendered%></p>
	                	</div>
	                	<div class="form_row clearfix">
	                		<label><%= Translate.Text("Club to take part")%> <span>*</span></label>
                            <asp:DropDownList id="clubFindSelect" runat="server" />
	                	</div>
	                	<div class="form_row clearfix">
	                		<label><%= Translate.Text("Race type")%> <span>*</span> <a class="regtip"><img src="/virginactive/images/icons/info.gif" alt="More information" height="16" width="16" /><span class="infoBox"><%= currentItem.Racetypetooltip.Rendered%></span></a></label>
                            
	                		<ul class="radio">                                
                                <li><asp:RadioButton class="clearfix" ID="radRace1" runat="server" GroupName="RaceType" /></li>
                                <li><asp:RadioButton class="clearfix" ID="radRace2" runat="server" GroupName="RaceType" /></li>
                                <li><asp:RadioButton class="clearfix" ID="radRace3" runat="server" GroupName="RaceType" /></li>
	                		</ul>
	                	</div>
	                	<div class="form_row clearfix">
	                		<label><%= Translate.Text("Race date")%> <span>*</span></label>
                            <asp:DropDownList id="drpRaceDate" runat="server" />
	                	</div>
	                	<div class="form_row clearfix">
	                		<label><%= Translate.Text("Race time")%> <span>*</span></label>
	                		<ul class="radio lined">
                                <li><asp:RadioButton ID="radAMRaceTime" runat="server" GroupName="RaceTime" /></li>
                                <li><asp:RadioButton ID="radPMRaceTime" runat="server" GroupName="RaceTime" /></li>
	                		</ul>
	                	</div>
                        <asp:CustomValidator id="cvFindTimeSlotUnavailable" runat="server" Display="Dynamic" CssClass="full_width_error"></asp:CustomValidator>
                        <asp:CustomValidator id="cvFindTimeSlotError" runat="server" Display="Dynamic" CssClass="full_width_error"></asp:CustomValidator>
	                	<div class="footer clearfix">
	                        <asp:button class="btn btn-cta-big" id="btnFindTimeSlot" runat="server" CausesValidation="false" onclick="btnFindTimeSlot_Click" OnClientClick="return indoorTry.formPostBackHandler1()" />
	                	</div>
	                </fieldset>
	                
	                <fieldset class="step_block hidden" id="pnlStep2" runat="server">
	                	<div class="header">
	                		<h3><%= currentItem.Section2heading.Rendered %></h3>
	                		<p><%= currentItem.Section2subheading.Rendered%></p>
	                	</div>
	                	<div class="form_row clearfix">
	                		<label><%= Translate.Text("Time slot")%> <span>*</span></label>
                            <asp:DropDownList id="drpTimeSlot" runat="server" />
                            <asp:CustomValidator id="cvSelectTimeSlotUnavailable" runat="server" Display="Dynamic" CssClass="error-msg dotnet"></asp:CustomValidator>
                            <%--<span class="error-msg dotnet"><p>Sorry, there are no spaces left for this time. Please select another time slot.</p></span>--%>
	                	</div>
                        <asp:CustomValidator id="cvSelectTimeSlotError" runat="server" Display="Dynamic" CssClass="full_width_error"></asp:CustomValidator>
                        <%--<span class="full_width_error"><p>Sorry, the booking system is not available right now -please try again later</p></span>--%>
	                	<div class="footer clearfix">                            
	                		<asp:button class="btn btn-cta-big" id="btnSelectTimeSlot" runat="server" CausesValidation="false" onclick="btnSelectTimeSlot_Click" OnClientClick="return indoorTry.formPostBackHandler2()" />
                            <asp:button class="btn btn-cta-big btn-restart" id="btnResetTimeSlot" CausesValidation="false" runat="server" onclick="btnResetTimeSlot_Click" />
	                	</div>
	                </fieldset>
	                
	                <div class="time_warning hidden" id="timeWarning"  runat="server">
	                	<%= Translate.Text("Thanks, <strong>your time slot has been reserved for 30 minutes</strong>. If you do not complete your registration within this time the form will timeout and you will need to restart again.")%>
	                </div>
	                
	                <fieldset class="step_block hidden no_footer" id="pnlStep3" runat="server">
	                	<div class="header">
	                		<h3><%= currentItem.Section3heading.Rendered %></h3>
	                		<p><%= currentItem.Section3subheading.Rendered%></p>
	                	</div>
	                	<div class="form_row clearfix">
	                		<label><%= Translate.Text("Individual or team")%> <span>*</span> <a class="regtip"><img src="/virginactive/images/icons/info.gif" alt="More information" height="16" width="16" /><span class="infoBox"><%= currentItem.Teamtooltip.Rendered%></span></a></label>
	                		<ul class="radio lined">
	                			<li><asp:RadioButton ID="radIndividual" runat="server" GroupName="TeamType" value="individual" /></li>
	                			<li><asp:RadioButton ID="radGroup" runat="server" GroupName="TeamType" value="Team" /></li>
	                		</ul>
	                	</div>
	                	<div class="form_row clearfix">
	                		<label><%= Translate.Text("Your Virgin Active status")%> <span>*</span></label>
	                		<ul class="radio lined">
	                			<li><asp:RadioButton ID="radMember" runat="server" GroupName="VAStatus" /></li>
	                			<li><asp:RadioButton ID="radNonMember" runat="server" GroupName="VAStatus" /></li>
                                <li><asp:RadioButton ID="radStaff" runat="server" GroupName="VAStatus" /></li>
	                		</ul>
	                	</div>
	                	<div class="form_row clearfix">
	                		<label><%= Translate.Text("Your name")%> <span>*</span></label>
	                		<input type="text" ID="txtFirstName" runat="server" class="short_text" name="FirstName" value="" placeholder="First" />
	                		<input type="text" ID="txtSurname" runat="server" class="short_text no_margin" name="LastName" value="" placeholder="Last" />
                            <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" EnableClientScript="false" CssClass="error-msg dotnet"
                                ControlToValidate="txtFirstName" ValidationGroup="PersonalDetails" />
                            <asp:RegularExpressionValidator ID="revFirstName" runat="server" EnableClientScript="false" CssClass="error-msg dotnet"
                                ControlToValidate="txtFirstName" ValidationGroup="PersonalDetails" />			
                            <asp:RequiredFieldValidator ID="rfvSurname" runat="server" EnableClientScript="false" CssClass="error-msg dotnet"
                                ControlToValidate="txtSurname" ValidationGroup="PersonalDetails" />
                            <asp:RegularExpressionValidator ID="revSurname" runat="server" EnableClientScript="false" CssClass="error-msg dotnet"
                                ControlToValidate="txtSurname" ValidationGroup="PersonalDetails" />
	                	</div>
	                	
	                	<div class="form_row date clearfix">
	                		<label><%= Translate.Text("Date of birth")%> <span>*</span></label>
                            
	                		<input type="text" class="datefield" ID="txtDOBDay" runat="server" name="DOBday_1" value="" placeholder="DD"  /> <span>/</span>
	                		<input type="text" class="datefield" ID="txtDOBMonth" runat="server" name="DOBmonth_1" value="" placeholder="MM"  /> <span>/</span>
	                		<input type="text" class="datefield" ID="txtDOBYear" runat="server" name="DOByear_1" value="" placeholder="YYYY" />
                            <asp:CustomValidator id="cvDateOfBirth" Display="Dynamic" runat="server" CssClass="error-msg dotnet"></asp:CustomValidator>
	                	</div>

						<div class="form_row clearfix">
	                		<label><%= Translate.Text("Your sex")%> <span>*</span></label>
	                		<ul class="radio lined">
                                <li><asp:RadioButton ID="radMale" runat="server" GroupName="Sex" /></li>
                                <li><asp:RadioButton ID="radFemale" runat="server" GroupName="Sex" /></li>
	                		</ul>
	                	</div>
	                	
	                	<div class="form_row clearfix">
	                		<label><%= Translate.Text("Address")%> <span>*</span></label>
	                		<div class="field_group">
		                		<input type="text" class="text" name="Address1" value="" placeholder="Address Line 1" runat="server" id="txtAddressLine1" />
                                <asp:RequiredFieldValidator ID="rfvAddressLine1" runat="server" CssClass="error-msg dotnet" EnableClientScript="false"
                                    ControlToValidate="txtAddressLine1" ValidationGroup="PersonalDetails" />
                                <asp:RegularExpressionValidator ID="revAddressLine1" runat="server" CssClass="error-msg dotnet" EnableClientScript="false"
                                    ControlToValidate="txtAddressLine1" ValidationGroup="PersonalDetails" />			
		                		<input type="text" class="text" name="Address2" value="" placeholder="Address Line 2" runat="server" id="txtAddressLine2" />
<%--                                <asp:RequiredFieldValidator ID="rfvAddressLine2" runat="server" CssClass="error-msg dotnet" EnableClientScript="false"
                                    ControlToValidate="txtAddressLine2" ValidationGroup="PersonalDetails" />--%>
                                <asp:RegularExpressionValidator ID="revAddressLine2" runat="server" CssClass="error-msg dotnet" EnableClientScript="false"
                                    ControlToValidate="txtAddressLine2" ValidationGroup="PersonalDetails" />			
		                		<input type="text" class="short_text" name="Address3" value="" placeholder="Town/City" runat="server" id="txtAddressLine3" />
                                <asp:RequiredFieldValidator ID="rfvAddressLine3" runat="server" CssClass="error-msg dotnet" EnableClientScript="false"
                                    ControlToValidate="txtAddressLine3" ValidationGroup="PersonalDetails" />
                                <asp:RegularExpressionValidator ID="revAddressLine3" runat="server" CssClass="error-msg dotnet" EnableClientScript="false"
                                    ControlToValidate="txtAddressLine3" ValidationGroup="PersonalDetails" />
		                		<input type="text" class="short_text no_margin" name="Postcode" value="" placeholder="Post Code" runat="server" id="txtPostcode" />
                                <asp:RequiredFieldValidator ID="rfvPostcode" runat="server" CssClass="error-msg dotnet" EnableClientScript="false"
                                    ControlToValidate="txtPostcode" ValidationGroup="PersonalDetails" />
                                <asp:RegularExpressionValidator ID="revPostcode" runat="server" CssClass="error-msg dotnet" EnableClientScript="false"
                                    ControlToValidate="txtPostcode" ValidationGroup="PersonalDetails" />
		                	</div>
	                	</div>
	                	
	                	<div class="form_row clearfix">
	                		<label><%= Translate.Text("Phone number")%> <span>*</span></label>
	                		<input type="text" class="text" name="PhoneNumber" value="" placeholder="" runat="server" id="txtPhoneNumber" />
                            <asp:RequiredFieldValidator ID="rfvPhoneNumber" runat="server" CssClass="error-msg dotnet" EnableClientScript="false"
                                ControlToValidate="txtPhoneNumber" ValidationGroup="PersonalDetails" />
                            <asp:RegularExpressionValidator ID="revPhoneNumber" runat="server" CssClass="error-msg dotnet" EnableClientScript="false"
                                ControlToValidate="txtPhoneNumber" ValidationGroup="PersonalDetails" />
	                	</div>
	                	
	                	<div class="form_row clearfix">
	                		<label><%= Translate.Text("Email")%> <span>*</span> <a class="regtip"><img src="/virginactive/images/icons/info.gif" alt="More information" height="16" width="16" /><span class="infoBox"><%= currentItem.Emailtooltip.Rendered%></span></a></label>
                            <input type="text" class="text" name="Email" value="" placeholder="" runat="server" id="txtEmail" />
                            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" CssClass="error-msg dotnet" EnableClientScript="false"
                                ControlToValidate="txtEmail" ValidationGroup="PersonalDetails" />
                            <asp:RegularExpressionValidator ID="revEmail" runat="server" CssClass="error-msg dotnet" EnableClientScript="false"
                                ControlToValidate="txtEmail" ValidationGroup="PersonalDetails" />
	                	</div>
	                </fieldset>
	                
	                <fieldset class="step_block hidden no_footer" id="pnlStep4" runat="server">
	                	<div class="header">
	                		<h3><%= currentItem.Section4heading.Rendered %></h3>
	                		<p><%= currentItem.Section4subheading.Rendered%></p>
	                	</div>
	                	<div class="form_row clearfix">
	                		<label><%= Translate.Text("Team name")%> <span>*</span></label>
	                		<input type="text" class="text no_margin" name="TeamName" value="" placeholder="" runat="server" id="txtTeamName" />
                            <asp:RegularExpressionValidator ID="revTeamName" runat="server" CssClass="error-msg dotnet" EnableClientScript="false"
                                ControlToValidate="txtTeamName" ValidationGroup="PersonalDetails" />
	                	</div>
	                	<div class="form_row clearfix">
	                		<label><%= Translate.Text("2nd team member's name")%> <span>*</span></label>
	                		<input type="text" class="short_text" name="FirstName2" id="txtFirstName2" value="" placeholder="First" runat="server" />
	                		<input type="text" class="short_text no_margin" name="LastName2" id="txtSurname2" value="" placeholder="Last" runat="server" />
                            <asp:RegularExpressionValidator ID="revFirstName2" runat="server" CssClass="error-msg dotnet" EnableClientScript="false"
                                ControlToValidate="txtFirstName2" ValidationGroup="PersonalDetails" />
                            <asp:RegularExpressionValidator ID="revSurname2" runat="server" CssClass="error-msg dotnet" EnableClientScript="false"
                                ControlToValidate="txtSurname2" ValidationGroup="PersonalDetails" />
	                	</div>
	                	<div class="form_row date clearfix">
	                		<label><%= Translate.Text("Their date of birth")%> <span>*</span></label>
	                		<input type="text" class="datefield" name="DOBday_2" id="txtDOBDay2" value="" placeholder="DD" runat="server" /> <span>/</span>
	                		<input type="text" class="datefield" name="DOBmonth_2" id="txtDOBMonth2" value="" placeholder="MM" runat="server" /> <span>/</span>
	                		<input type="text" class="datefield" name="DOByear_2" id="txtDOBYear2" value="" placeholder="YYYY" runat="server" />
                            <asp:CustomValidator id="cvDateOfBirth2" runat="server" Display="Dynamic" CssClass="error-msg dotnet"></asp:CustomValidator>	
	                	</div>
						<div class="form_row clearfix">
	                		<label><%= Translate.Text("Their sex")%> <span>*</span></label>
	                		<ul class="radio lined">
                                <li><asp:RadioButton ID="radMale2" runat="server" GroupName="Sex2nd" /></li>
                                <li><asp:RadioButton ID="radFemale2" runat="server" GroupName="Sex2nd" /></li>
	                		</ul>
	                	</div>
	                	<div class="form_row clearfix">
	                		<label><%= Translate.Text("3rd team member's name")%></label>
	                		<input type="text" class="short_text" name="" value="" placeholder="First" id="txtFirstName3" runat="server" />
	                		<input type="text" class="short_text no_margin" name="" value="" placeholder="Last" id="txtSurname3" runat="server" />
                            <asp:RegularExpressionValidator ID="revFirstName3" runat="server" CssClass="error-msg dotnet" EnableClientScript="false"
                                ControlToValidate="txtFirstName3" ValidationGroup="PersonalDetails" />
                            <asp:RegularExpressionValidator ID="revSurname3" runat="server" CssClass="error-msg dotnet" EnableClientScript="false"
                                ControlToValidate="txtSurname3" ValidationGroup="PersonalDetails" />
	                	</div>
	                	<div class="form_row date clearfix">
	                		<label><%= Translate.Text("Their date of birth")%></label>
	                		<input type="text" class="datefield" name="DOBday_3" value="" placeholder="DD" id="txtDOBDay3" runat="server" /> <span>/</span>
	                		<input type="text" class="datefield" name="DOBmonth_3" value="" placeholder="MM" id="txtDOBMonth3" runat="server" /> <span>/</span>
	                		<input type="text" class="datefield" name="DOByear_3" value="" placeholder="YYYY" id="txtDOBYear3" runat="server"/>
                            <asp:CustomValidator id="cvDateOfBirth3" runat="server" Display="Dynamic" CssClass="error-msg dotnet"></asp:CustomValidator>	
	                	</div>
	                	<div class="form_row clearfix">
	                		<label><%= Translate.Text("Their sex")%></label>
	                		<ul class="radio lined">
	                	        <li><asp:RadioButton ID="radMale3" runat="server" GroupName="Sex3rd" /></li>
	                	        <li><asp:RadioButton ID="radFemale3" runat="server" GroupName="Sex3rd" /></li>
	                		</ul>
	                	</div>
	                </fieldset>
	                
	                <fieldset class="step_block hidden no_footer" id="pnlStep5" runat="server">
	                	<div class="header">
	                		<h3><%= currentItem.Section5heading.Rendered %></h3>
	                		<p><%= currentItem.Section5subheading.Rendered%></p>
	                	</div>
	                	
	                	<div class="form_row clearfix">
	                		<label><%= Translate.Text("Their name")%> <span>*</span></label>
	                		<input type="text" class="short_text" name="EmergencyFirstName" id="txtNOKFirstName" value="" placeholder="First" runat="server" />
	                		<input type="text" class="short_text no_margin" name="EmergencyLastName" id="txtNOKSurname" value="" placeholder="Last" runat="server" />
                            <asp:RequiredFieldValidator ID="rfvNOKFirstName" runat="server" CssClass="error-msg dotnet" EnableClientScript="false"
                                ControlToValidate="txtNOKFirstName" ValidationGroup="PersonalDetails" />
                            <asp:RegularExpressionValidator ID="revNOKFirstName" runat="server" CssClass="error-msg dotnet" EnableClientScript="false"
                                ControlToValidate="txtNOKFirstName" ValidationGroup="PersonalDetails" />
                            <asp:RequiredFieldValidator ID="rfvNOKSurname" runat="server" CssClass="error-msg dotnet" EnableClientScript="false"
                                ControlToValidate="txtNOKSurname" ValidationGroup="PersonalDetails" />
                            <asp:RegularExpressionValidator ID="revNOKSurname" runat="server" CssClass="error-msg dotnet" EnableClientScript="false"
                                ControlToValidate="txtNOKSurname" ValidationGroup="PersonalDetails" />
	                	</div>
	                	<div class="form_row clearfix">
	                		<label><%= Translate.Text("Your relationship to them")%> <span>*</span></label>
	                		<input type="text" class="text no_margin" name="EmergencyRelationship" id="txtNOKRelationship" value="" placeholder="" runat="server" />
                            <asp:RequiredFieldValidator ID="rfvNOKRelationship" runat="server" CssClass="error-msg dotnet" EnableClientScript="false"
                                ControlToValidate="txtNOKRelationship" ValidationGroup="PersonalDetails" />
                            <asp:RegularExpressionValidator ID="revNOKRelationship" runat="server" CssClass="error-msg dotnet" EnableClientScript="false"
                                ControlToValidate="txtNOKRelationship" ValidationGroup="PersonalDetails" />
	                	</div>
	                	<div class="form_row clearfix">
	                		<label><%= Translate.Text("Their phone number")%> <span>*</span></label>
	                		<input type="text" class="text no_margin" name="EmergencyPhoneNumber" id="txtNOKPhoneNumber" value="" placeholder="" runat="server" />
                            <asp:RequiredFieldValidator ID="rfvNOKPhoneNumber" runat="server" CssClass="error-msg dotnet" EnableClientScript="false"
                                ControlToValidate="txtNOKPhoneNumber" ValidationGroup="PersonalDetails" />
                            <asp:RegularExpressionValidator ID="revNOKPhoneNumber" runat="server" CssClass="error-msg dotnet" EnableClientScript="false"
                                ControlToValidate="txtNOKPhoneNumber" ValidationGroup="PersonalDetails" />
	                	</div>
	                </fieldset>
	                
	                <fieldset class="step_block hidden" id="pnlStep6" runat="server">
	                	<div class="header">
	                		<h3><%= currentItem.Section6heading.Rendered %></h3>
	                		<p><%= currentItem.Section6subheading.Rendered%></p>
	                	</div>
	                	
	                	<div class="form_row checkbox clearfix">
	                		<input type="checkbox" class="" name="TermsAndConditions" id="chkTermsAndConditions" runat="server" value=""/>
	                		<label><%= currentItem.TermsAndConditionsText != null ? currentItem.TermsAndConditionsText.Rendered : "" %></label>
                            <asp:CustomValidator id="cvTermsAndConditions" runat="server" Display="Dynamic" CssClass="error-msg dotnet"></asp:CustomValidator>
	                	</div>
	                	
	                	<div class="form_row checkbox clearfix">
	                		<input type="checkbox" class="" name="ParentalConsent" id="chkParentalConsent" runat="server" value=""/>
	                		<label><%= currentItem.ParentalConsentText != null ? currentItem.ParentalConsentText.Rendered : "" %></label>
	                	</div>
                        <asp:CustomValidator id="cvSubmitSubscription" runat="server" Display="Dynamic" CssClass="full_width_error"></asp:CustomValidator>
                        <%--<span class="full_width_error"><p>Sorry, the booking system is not available right now -please try again later</p></span>--%>	                	
	                	<div class="footer clearfix">                            
	                	    <asp:button class="btn btn-cta-big" id="btnPersonalDetails" runat="server" onclick="btnPersonalDetails_Click" OnClientClick="return indoorTry.formPostBackHandler3();" ValidationGroup="PersonalDetails" />
                            <asp:button class="btn btn-cta-big btn-restart" id="btnResetForm" runat="server" onclick="btnResetForm_Click" />
	                	</div>
	                </fieldset>

	                <div class="registration_confirmation hidden"  id="pnlConfirmation" runat="server">
	                	<h2><%= currentItem.Confirmationheader.Rendered %></h2>
	                	<p><%= currentItem.Confirmationbody.Rendered  %></p>
	                	<div class="confirm_block">
                		    <div class="share_block clearfix">
                                <p><%= currentItem.Calltoactionheader.Rendered %></p>
	                			<div class="share_inner">
	                				<ul class="social-info">
	                				<li class="social-twitter"><span><iframe allowtransparency="true" frameborder="0" scrolling="no" src="http://platform.twitter.com/widgets/tweet_button.1340179658.html#_=1344263615523&amp;count=horizontal&amp;id=twitter-widget-0&amp;lang=en&amp;original_referer=<%= twitterShareURL %>&amp;size=m&amp;text=<%= twitterText %>&amp;url=<%= twitterShareURL %>&amp;via=VirginActiveUK" class="twitter-share-button twitter-count-horizontal" style="width: 107px; height: 20px; " title="Twitter Tweet Button"></iframe>
			                        <script type="text/javascript" src="http://platform.twitter.com/widgets.js"></script></span></li>
                                    <li class="social-facebook"><span><%= SocialMediaHelper.LikeButtonIFrame(ArticleURL)%></span></li>
                                    <%--<li class="social-googleplus"><span><%= SocialMediaHelper.GooglePlusButtonIFrame(ArticleURL)%></span></li>
                                    <li class="social-googleplus"><span><%= SocialMediaHelper.GooglePlusButton %></span></li>--%>
			                        </ul>
	                			</div>
	                		</div>
	                		<h3><%= currentItem.Whatsnextheading.Rendered %></h3>
	                		<h4><%= currentItem.Whatsnextsubheading.Rendered %></h4>
                            <%= currentItem.Whatsnextbody.Rendered %>                            
	                	</div>
		                <h3><%= currentItem.Section1heading.Rendered %></h3>
		                <div class="confirm_block">
		                	<dl>
		                		<dt><%= Translate.Text("Club to take part")%></dt>
		                		<dd><%=clubFindSelect.SelectedIndex > 0 ? clubFindSelect.SelectedItem.Text.ToString() : ""%></dd>
		                		<dt><%= Translate.Text("Race type")%></dt>
		                		<dd><%=raceType%></dd>
		                		<dt><%= Translate.Text("Race date")%></dt>
		                		<dd><%= drpRaceDate.SelectedItem != null ? drpRaceDate.SelectedItem.Text.ToString() : ""%></dd>
		                		<dt><%= Translate.Text("Race time")%></dt>
		                		<dd><%=radAMRaceTime.Checked ? Translate.Text("AM") : Translate.Text("PM") %></dd>
		                	</dl>
		                </div>
		                	
		                <h3><%= currentItem.Section2heading.Rendered %></h3>
		                <div class="confirm_block">
		                	<dl>
		                		<dt><%= Translate.Text("Time slot")%></dt>
		                		<dd><%=drpTimeSlot.SelectedIndex > 0 ? drpTimeSlot.SelectedItem.Text.ToString() : ""%></dd>
		                	</dl>
		                </div>
		                	
		                <h3><%= currentItem.Section3heading.Rendered %></h3>
		                <div class="confirm_block">
		                	<dl>
		                		<dt><%= Translate.Text("Individual or Team?")%></dt>
		                		<dd><%= radIndividual.Checked ? radIndividual.Text : radGroup.Text%></dd>
		                		<dt><%= Translate.Text("Your Virgin Active status")%></dt>
		                		<dd><%= vaStatus%></dd>
		                		<dt><%= Translate.Text("Your name")%></dt>
		                		<dd><%=txtFirstName.Value.Trim() + " " + txtSurname.Value.Trim()%></dd>
		                		<dt><%= Translate.Text("Date of birth")%></dt>
		                		<dd><%=transaction != null && transaction.Subscriber != null ? transaction.Subscriber.Dob.ToString("dd/MM/yyyy") : ""%></dd>
		                		<dt><%= Translate.Text("Your sex")%></dt>
		                		<dd><%= radMale.Checked ? radMale.Text : radFemale.Text%></dd>
		                		<dt><%= Translate.Text("Address")%></dt>
		                		<dd><%= address%></dd>
		                		<dt><%= Translate.Text("Phone number")%></dt>
		                		<dd><%=txtPhoneNumber.Value.Trim()%></dd>
		                		<dt><%= Translate.Text("Email")%></dt>
		                		<dd><%=txtEmail.Value.Trim()%></dd>
		                	</dl>
		                </div>
                        <% if (radGroup.Checked && txtSurname2.Value.Trim() != "")
                                {%>
		                <h3><%= currentItem.Section4heading.Rendered %></h3>
		                <div class="confirm_block">
		                	<dl>
		                		<dt><%= Translate.Text("Team name")%></dt>
		                		<dd><%=txtTeamName.Value.Trim()%></dd>
		                		<dt><%= Translate.Text("2nd team member's name")%></dt>
		                		<dd><%=txtFirstName2.Value.Trim() + " " + txtSurname2.Value.Trim() %></dd>
		                		<dt><%= Translate.Text("Their date of birth")%></dt>
                                <dd><%=transaction != null && transaction.Subscriber != null ? transaction.Subscriber.Dob2.ToString("dd/MM/yyyy") : ""%></dd>
		                		<dt><%= Translate.Text("Their sex")%></dt>
                                <dd><%= radMale2.Checked ? radMale2.Text : radFemale2.Text%></dd>
                            <% if (txtSurname3.Value.Trim() != "" && txtSurname3.Value.Trim() != "Last")
                                {%>
		                		<dt><%= Translate.Text("3rd team member's name")%></dt>
		                		<dd><%=txtFirstName3.Value.Trim() + " " + txtSurname3.Value.Trim() %></dd>
		                		<dt><%= Translate.Text("Their date of birth")%></dt>
                                <dd><%=transaction != null && transaction.Subscriber != null ? transaction.Subscriber.Dob3.ToString("dd/MM/yyyy") : ""%></dd>
		                		<dt><%= Translate.Text("Their sex")%></dt>
                                <dd><%= radMale3.Checked ? radMale3.Text : radFemale3.Text%></dd>
                            <% }%>
		                	</dl>
		                </div>
                                <% }%>
		                	
		                <h3><%= currentItem.Section5heading.Rendered %></h3>
		                <div class="confirm_block">
		                	<dl>
		                		<dt><%= Translate.Text("Their name")%></dt>
		                		<dd><%=txtNOKFirstName.Value.Trim() + " " + txtNOKSurname.Value.Trim()%></dd>
		                		<dt><%= Translate.Text("Your relationship to them")%></dt>
		                		<dd><%=txtNOKRelationship.Value.Trim()%></dd>
		                		<dt><%= Translate.Text("Their phone number")%></dt>
		                		<dd><%=txtNOKPhoneNumber.Value.Trim()%></dd>
		                	</dl>
		                </div>
		                <h3><%= currentItem.Section6heading.Rendered %></h3>
		                <div class="confirm_block">
                            <%--<p><%= chkTermsAndConditions.Checked ? Translate.Text("I have read and agree to the Virgin Active Indoor Triathlon/Duathlon rules and terms and conditions. I will complete the Indemnity and Release form before I enter the race.") : ""%></p>
		                	<p><%= chkParentalConsent.Checked ? Translate.Text("I understand that 10-12 year olds must be able to sit on the bike saddle and pedal comfortably in order to enter this event.") : "" %></p>--%>
                            <% if (chkTermsAndConditions.Checked)
                                {%>
                            <p><%= currentItem.TermsAndConditionsText != null ? currentItem.TermsAndConditionsText.Rendered : "" %></p>
                            <% }%>
                            <% if (chkParentalConsent.Checked)
                                {%>
		                	<p><%= currentItem.ParentalConsentText != null ? currentItem.ParentalConsentText.Rendered : "" %></p>		                
                            <% }%>
		                </div>
	                </div>
                    </div>
			        <div id="confirmationFooter" class="footer-cta hidden" runat="server">
	                    <p><%= currentItem.Calltoactionfooter.Rendered %></p>
	                    <p class="cta"> 
                            <a class="btn btn-cta-big" href="<%= downloadPDFUrl %>"><%= currentItem.Confirmtationbuttontext.Rendered %></a>
	                    </p>
	                </div>

                    <asp:placeholder ID="AjaxScriptPh" runat="server" />
                    </ContentTemplate>
                    </asp:UpdatePanel>
                

            </div> <!-- /content -->

<div id="page_expiry_overlay" class="overlay hidden">
    <div class="timeout_message">
        <h2><%= currentItem.Timeoutheading.Rendered %></h2>
        <p><%= currentItem.Timeoutsubheading.Rendered %></p>
        <a class="btn btn-cta-big" href="<%= currentItemUrl %>" id="btn-register"><%= currentItem.Timeoutbuttontext.Rendered %></a>
    </div>
</div>
