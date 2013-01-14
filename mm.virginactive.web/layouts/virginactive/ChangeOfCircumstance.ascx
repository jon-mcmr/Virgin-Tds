<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChangeOfCircumstance.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.ChangeOfCircumstance" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ModuleTemplates" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.common.Constants.SitecoreConstants" %>
<asp:ScriptManager runat="server"></asp:ScriptManager>
<div id="content" class="layout-block-full">
    <div class="personal-details-inner change-of-circumstance clearfix">
        <section id="Panel1" runat="server" class="panel-one panel current-panel" style="display:block">
            <h2><asp:Literal runat="server" ID="Panel1PageHeadingLiteral" /></h2>
            <p class="intro"><asp:Literal runat="server" ID="Panel1PageIntroductionLiteral" /></p>
            <aside class="steps">
                    <ul>
                        <li><span><%= Translate.Text("Step 1") %></span><asp:Literal runat="server" ID="Panel1Phase1LabelLiteral" /></li>
                        <li><span><%= Translate.Text("Step 2") %></span><asp:Literal runat="server" ID="Panel1Phase2LabelLiteral" /></li>
                        <li><span><%= Translate.Text("Step 3") %></span><asp:Literal runat="server" ID="Panel1Phase3LabelLiteral" /></li>
                    </ul>                           
            </aside>
            
            <asp:PlaceHolder runat="server" ID="Panel1MemberNotFoundPlaceHolder" Visible="False">
                <div class="error-msg-wrap">
                    <div class="msg">
                        <p><%= Translate.Text("Sorry, we can’t find you on our database.")%></p>
                        <p class="sub"><%= Translate.Text("Please make sure you’ve filled in the information correctly and try again. If you still can’t get it to work please contact the friendly team at your club or call Customer Services on ")%> <%=CustomerServicesNumber%>.</p>
                    </div>
                    <div class="msg-b"></div>
                </div>
            </asp:PlaceHolder>

			<div class="send-form">
				<h3><%= Translate.Text("Membership search") %></h3>
				<div class="msg-personal-details">
                    <fieldset>
					    <asp:Panel runat="server" ID="Panel1MembershipNumberWrapperPanel" CssClass="form-wrap form-wrap-membership clearfix">
                            <div class="form-field-wrap clearfix">
							    <div class="label-box">
								    <label for="<%=Panel1MembershipNumberTextBox.ClientID%>" class="form-label remove-padding"><%= Translate.Text("Swipe No.")%></label>
                                    <a href="#" class="" title="Check out all the ... "></a>
                                    <p class="more-info img-rep"><a href=""><span class="rep"></span><span class="visiblyhidden">?</span></a><span class="tooltip">
                                        <asp:Literal runat="server" ID="Panel1SwipeHelpLiteral" />
                                    </span></p>
							    </div>
							    <div class="input-box">
								    <asp:TextBox runat="server" ID="Panel1MembershipNumberTextBox" CssClass="form-text-field membership required" data-required="Please enter your membership number" data-membership="Please enter a valid membership number" MaxLength="20" />
							    </div>
                            </div>
                            <asp:Panel runat="server" ID="Panel1MembershipNumberValidatorPanel" CssClass="error-msg" Visible="False">
                                <p>Please enter a membership number</p>
                            </asp:Panel>
					    </asp:Panel>
					    <asp:Panel runat="server" ID="Panel1DateOfBirthWrapperPanel" CssClass="form-wrap form-wrap-dob clearfix">
                            <div class="form-field-wrap clearfix">
							    <div class="label-box">
                                    <label for="<%=Panel1DateOfBirthDayDropDownList.ClientID%>" class="form-label remove-padding"><%= Translate.Text("Date of birth")%></label>
                                </div>
                                <div class="input-box form-wrap-dob">
                                    <fieldset>
                                    	<asp:DropDownList data-placeholder="Please select your date of birth" CssClass="text required validation-group dob-day" data-default="dd" runat="server" ID="Panel1DateOfBirthDayDropDownList" data-required="Please enter your date of birth" data-group="dob" />
							    	</fieldset>
									<fieldset>
										<asp:DropDownList data-placeholder="Please select your date of birth" CssClass="text required validation-group dob-month" data-default="mm" runat="server" ID="Panel1DateOfBirthMonthDropDownList" data-required="Please enter your date of birth" data-group="dob" />
									</fieldset>							    	
							    	<fieldset>
							    		<asp:DropDownList data-placeholder="Please select your year of birth" CssClass="text required validation-group dob-year" data-default="yyyy" runat="server" ID="Panel1DateOfBirthYearDropDownList" data-required="Please enter your date of birth" data-group="dob" />
							    	</fieldset>
							    </div>
                            </div>
                            <asp:Panel runat="server" ID="Panel1DateOfBirthValidatorPanel" CssClass="error-msg" Visible="False">
                                <p>Please select your date of birth</p>
                            </asp:Panel>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="Panel1PostcodeWrapperPanel" CssClass="form-wrap form-wrap-postcode last clearfix">
                            <div class="form-field-wrap clearfix">
                                <div class="label-box">
								    <label for="<%=Panel1PostcodeTextBox.ClientID%>" class="form-label"><%= Translate.Text("Postcode")%></label>
                                </div>
							    <div class="input-box">
								    <asp:TextBox runat="server" ID="Panel1PostcodeTextBox" CssClass="form-text-field input-postcode validation-required postcode" data-required="Please enter your postcode" data-postcode="Please enter a valid postcode" MaxLength="40" />
							    </div>
                            </div>
                            <asp:Panel runat="server" ID="Panel1PostcodeValidatorPanel" CssClass="error-msg" Visible="False">
                                <p>Please select your date of birth</p>
                            </asp:Panel>
					    </asp:Panel>
                    </fieldset>
                </div>
            </div>
            <div class="panel-footer clearfix">
                <p><asp:Literal runat="server" ID="Panel1FooterTextLiteral" /></p>
                <asp:Button runat="server" ID="Panel1SubmitButton" OnClick="Panel1SubmitButtonClick" OnClientClick="return changeCircumstance.validate();" CssClass="btn-submit submit_button" Text="Look up my details"/>
            </div>
        </section>
        <section runat="server" id="Panel2" class="panel panel-data panel-two" Visible="False" style="display:block">


            <h2><asp:Literal runat="server" ID="Panel2PageHeadingLiteral" /></h2>
            <p class="intro"><asp:Literal runat="server" ID="Panel2PageIntroductionLiteral" /></p>
            <aside class="steps">
                <ul>
                    <li><span><%= Translate.Text("Step 1") %></span><asp:Literal runat="server" ID="Panel2Phase1LabelLiteral" /></li>
                    <li><span><%= Translate.Text("Step 2") %></span><asp:Literal runat="server" ID="Panel2Phase2LabelLiteral" /></li>
                    <li><span><%= Translate.Text("Step 3") %></span><asp:Literal runat="server" ID="Panel2Phase3LabelLiteral" /></li>
                </ul>                           
            </aside>

            <div class="msg-wrap">
                <div class="msg">
                    <p><asp:Literal runat="server" ID="Panel2FoundTextLiteral" /></p>
                </div>
                <div class="msg-b"></div>
            </div>

            <h3><asp:Literal runat="server" ID="Panel2ContactSectionHeadingLiteral" /></h3>
            <asp:Literal runat="server" ID="Panel2ContactSectionCopyLiteral" />
            <div class="form-column left">
         		<fieldset class="form-block non-editable">
         			<dl>
         				<dt class="form-label"><strong><%= Translate.Text("Swipe No.")%></strong></dt>
         			    <dd><asp:Literal runat="server" ID="Panel2SwipeNumberLiteral" /></dd>
         			</dl>
         		</fieldset>
				<fieldset class="form-block">
					<label class="form-label" for="<%=Panel2TitleTextBox.ClientID%>"><%= Translate.Text("Title")%><span class="required">*</span></label>
				    <asp:TextBox runat="server" ID="Panel2TitleTextBox" CssClass="form-text-field form-half-text-field required" data-required="Please enter your title" MaxLength="40" />
                    <asp:Panel runat="server" ID="Panel2TitleValidatorPanel" CssClass="error-msg" Visible="False">
                        <p>Please enter a valid Title</p>
                    </asp:Panel>				
				</fieldset>         				
                <fieldset class="form-block more-information non-editable name">
					<dl>
						<dt class="form-label">	
							<strong><%= Translate.Text("Name")%></strong>
							<p class="more-info img-rep">
								<a href="">
									<span class="rep"></span>
									<span class="visiblyhidden ir">?</span>
								</a>
								<span class="tooltip">
							    <asp:Literal runat="server" ID="Panel2NameTooltipLiteral" />
							</span>
						</p>									
						</dt>									
					    <dd><span><asp:Literal runat="server" ID="Panel2FirstNameLiteral" /></span><asp:TextBox runat="server" ID="Panel2LastNameTextBox" CssClass="form-text-field required" MaxLength="40" data-required="Please provide your surname" /></dd>															
					</dl>		                                								
                    <asp:Panel runat="server" ID="Panel2NameValidatorPanel" CssClass="error-msg" Visible="False">
                        <p>Please enter your last name</p>
                    </asp:Panel>
				</fieldset>
				<fieldset class="form-block">
					<label for="<%=Panel2EmailTextBox.ClientID%>" class="form-label"><%= Translate.Text("Email")%></label>
				    <asp:TextBox runat="server" ID="Panel2EmailTextBox" CssClass="form-text-field form-text-field-email validate-email validation-contact-group email" data-required="Please enter your email address so we can contact you" data-email="Please enter a valid email address" MaxLength="40" />
                    <asp:Panel runat="server" ID="Panel2EmailValidatorPanel" CssClass="error-msg" Visible="False">
                        <p>Please enter a valid email address</p>
                    </asp:Panel>
				</fieldset>
							
							
				<fieldset class="form-block">
					<label for="<%=Panel2HomeTelTextBox.ClientID%>" class="form-label"><%= Translate.Text("Home phone")%></label>
				    <asp:TextBox runat="server" ID="Panel2HomeTelTextBox" CssClass="form-text-field multirow-text-field phone" MaxLength="40" data-phone="Please enter a valid home phone number" data-required="Please provide your home phone so we can contact you" />
					<label for="<%=Panel2WorkTelTextBox.ClientID%>" class="form-label"><%= Translate.Text("Work phone")%></label>
				    <asp:TextBox runat="server" ID="Panel2WorkTelTextBox" CssClass="form-text-field multirow-text-field phone" MaxLength="40" data-phone="Please enter a valid work phone number" data-required="Please provide your work phone so we can contact you" />
					<label for="<%=Panel2MobileTelTextBox.ClientID%>" class="form-label"><%= Translate.Text("Mobile phone")%></label>
				    <asp:TextBox runat="server" ID="Panel2MobileTelTextBox" CssClass="form-text-field multirow-text-field phone last" MaxLength="40"  data-phone="Please enter a valid mobile phone number" data-required="Please provide your mobile phone so we can contact you" />
					<asp:Panel runat="server" ID="Panel2TelValidatorPanel" CssClass="error-msg" Visible="False">
                        <p>Please enter a valid phone number</p>
                    </asp:Panel>
                </fieldset>
  							              				
         				
         				
         				
         	</div>
			<div class="form-column clearfix">
				<fieldset class="form-block non-editable">
					<dl>
						<dt class="form-label"><strong><%= Translate.Text("Membership No.")%></strong></dt>
					    <dd><asp:Literal runat="server" ID="Panel2MembershipNumberLiteral" /></dd>
					</dl>
				</fieldset>
				<fieldset class="form-block dob non-editable">
					<dl>
						<dt class="form-label"><strong><%= Translate.Text("Date of birth")%></strong></dt>
					    <dd><asp:Literal runat="server" ID="Panel2DateOfBirthLiteral" /></dd>
					</dl>
							
				</fieldset>
				<fieldset class="form-block address">
					<label for="<%=Panel2Address1TextBox.ClientID%>" class="form-label"><%= Translate.Text("Address")%><span class="required">*</span></label>
				    <asp:TextBox runat="server" ID="Panel2Address1TextBox" CssClass="form-text-field multirow-text-field required" data-required="Please provide the first line of your address" MaxLength="40" />
                                
				    <asp:TextBox runat="server" ID="Panel2Address2TextBox" CssClass="form-text-field multirow-text-field multirow-label-align" MaxLength="40" />
				    <asp:TextBox runat="server" ID="Panel2Address3TextBox" CssClass="form-text-field multirow-text-field multirow-label-align" MaxLength="40" />
				    <asp:TextBox runat="server" ID="Panel2Address4TextBox" CssClass="form-text-field multirow-text-field multirow-label-align" MaxLength="40" />
                    <asp:TextBox runat="server" ID="Panel2Address5TextBox" CssClass="form-text-field multirow-text-field multirow-label-align" MaxLength="40"/>
                                
					<label for="<%=Panel2PostcodeTextBox.ClientID%>" class="form-label"><%= Translate.Text("Postcode")%></label>
                    <asp:TextBox runat="server" ID="Panel2PostcodeTextBox" CssClass="form-text-field" MaxLength="40"/>
                    <asp:Panel runat="server" ID="Panel2AddressValidatorPanel" CssClass="error-msg" Visible="False">
                        <p>Please enter a valid address</p>
                    </asp:Panel>
				</fieldset>
			</div>         				

            <h3 class="accordion_title"><a href="#"><asp:Literal runat="server" ID="Panel2PackageSectionHeadingLiteral" /></a></h3>
                            
            <div class="accordion_content">
                <fieldset class="change-membership form-block">
                                   
                    <div class="current_details clearfix">
                        <dl class="clearfix multirow">
                            <dt class="form-label"><strong>Current club</strong></dt>
                            <dd class="form-label form-label-text"><asp:Literal runat="server" ID="Panel2CurrentClubLiteral"/></dd>
                        </dl>
                        <dl class="clearfix multirow">
    						<dt class="form-label"><strong>Current membership type</strong></dt>
                            <dd class="form-label form-label-text"><asp:Literal runat="server" ID="Panel2CurrentPackageLiteral" /></dd>                            
                        </dl>
                    </div>
    		                    
                    <div class="form-row bordered clearfix change-club">
						<div class="change-membership-type">
							<fieldset class="change-type-selector">
							    <asp:RadioButton runat="server" ID="Panel2ChangeClubAndPackageRadioButton" GroupName="changeSelector"/>
							    <label for="<%= Panel2ChangeClubAndPackageRadioButton.ClientID %>">Change club and membership type</label>
							</fieldset>
							<div class="form-options clearfix change-club-package">
							    <asp:UpdatePanel runat="server">
							        <ContentTemplate>
								        <fieldset>
									        <label for="<%=Panel2ChangeClubAndPackageClubDropDownList.ClientID%>" class="form-label"><%= Translate.Text("Select your new club")%></label>
								            <asp:DropDownList runat="server" ID="Panel2ChangeClubAndPackageClubDropDownList" CssClass="clearfix" data-required="Please select your new club" OnSelectedIndexChanged="Panel2ChangeClubAndPackageClubDropDownListSelectedIndexChanged" AutoPostBack="True">
							                    <asp:ListItem Value="">Select</asp:ListItem>
							                </asp:DropDownList>
							                <asp:Button runat="server" ID="Panel2ChangeClubAndPackageClubButton" CssClass="btn btn-update js-hide" Text="Update packages" OnClick="Panel2ChangeClubAndPackageClubButtonClick" />
								        </fieldset>
								        <fieldset>
									        <label for="<%=Panel2ChangeClubAndPackagePackageDropDownList.ClientID%>" class="form-label"><%= Translate.Text("Change membership type?")%></label>
								            <asp:DropDownList runat="server" ID="Panel2ChangeClubAndPackagePackageDropDownList" data-required="Please select your new membership type">
							                    <asp:ListItem Value="">Select a Club first</asp:ListItem>
							                </asp:DropDownList>
							                <asp:Panel runat="server" ID="Panel2ChangeClubAndPackageValidatorPanel" CssClass="error-msg" Visible="False">
							                    <p>Please select a new club and membership type</p>
							                </asp:Panel>
								        </fieldset>
							        </ContentTemplate>
							    </asp:UpdatePanel>
							</div>						
						</div>
    					<div class="clearfix or-divider">
    						<div class="or-text">
    							<span>Or</span>
    						</div>
    					</div>
						<div class="change-membership-type">
							<fieldset class="change-type-selector">
							    <asp:RadioButton runat="server" ID="Panel2ChangePackageRadioButton" GroupName="changeSelector"/>
								<label for="<%= Panel2ChangePackageRadioButton.ClientID %>">Change membership type only</label>
							</fieldset>
							<div class="form-options clearfix">
								<fieldset>
									<label for="<%=Panel2ChangePackagePackageDropDownList.ClientID%>" class="form-label"><%= Translate.Text("Change membership type?")%></label>
							        <asp:DropDownList runat="server" ID="Panel2ChangePackagePackageDropDownList" data-required="Please select your new membership type">
							            <asp:ListItem Value="">Select</asp:ListItem>
							        </asp:DropDownList>
							        <asp:Panel runat="server" ID="Panel2ChangePackageValidatorPanel" CssClass="error-msg" Visible="False">
							            <p>Please enter a new membership type</p>
							        </asp:Panel>
								</fieldset>
							</div>						
						</div>
                        <div class="clearfix cancel-changes"><asp:Literal runat="server" ID="Panel2PackageSectionFooterLiteral" /></div>
                    </div>
                </fieldset>
            </div>

            <h3 class="accordion_title"><a href="#"><asp:Literal runat="server" ID="Panel2FreezeSectionHeadingLiteral" /></a></h3>
            <div class="accordion_content">
                <asp:Literal runat="server" ID="Panel2FreezeSectionCopyLiteral" />
                <fieldset class="form-block clearfix freeze-membership">
                    <div class="form-row">
                        <label for="<%=Panel2FreezeMonthDropDownList.ClientID%>" class="form-label auto"><%= Translate.Text("I wish to put my membership on hold from 1st")%></label>
                        <asp:DropDownList runat="server" ID="Panel2FreezeMonthDropDownList">
                            <asp:ListItem Value="">Select</asp:ListItem>
                        </asp:DropDownList>

                        <label for="<%= Panel2FreezeDurationDropDownList.ClientID %>" class="form-label auto for-label"><%= Translate.Text("for")%></label>
                        <asp:DropDownList runat="server" ID="Panel2FreezeDurationDropDownList" data-required="Please select how long you want to put your membership on hold for">
                            <asp:ListItem Value="">Select</asp:ListItem>
                            <asp:ListItem Value="1 month">1 Month</asp:ListItem>
                            <asp:ListItem Value="2 months">2 Months</asp:ListItem>
                            <asp:ListItem Value="3 months">3 Months</asp:ListItem>
                        </asp:DropDownList>
                                        
                    </div>
                    <asp:Panel runat="server" ID="Panel2FreezeValidatorPanel" CssClass="error-msg" Visible="False">
                        <p>Please select a month and duration for putting your membership on hold</p>
                    </asp:Panel>
                </fieldset>
            </div>

            <h3 class="accordion_title"><a href="#"><asp:Literal runat="server" ID="Panel2CancelSectionHeadingLiteral" /></a></h3>
            <div class="accordion_content">
                <asp:Literal runat="server" ID="Panel2CancelSectionCopyLiteral" />
                <fieldset class="form-block">
                    <div class="form-row clearfix">
                        <label for="<%=Panel2CancelMonthDropDownList.ClientID%>" class="form-label auto"><%= Translate.Text("I wish to cancel my membership from the end of")%></label>
                        <asp:DropDownList runat="server" ID="Panel2CancelMonthDropDownList">
                            <asp:ListItem Value="">Select</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-row highlight clearfix">
                        <label for="<%=Panel2CancelReasonTextBox.ClientID%>" class="form-label auto"><%= Translate.Text("Reason for cancellation")%></label>
                        <asp:TextBox runat="server" ID="Panel2CancelReasonTextBox" TextMode="MultiLine" />
                    </div>
                    <asp:Panel runat="server" ID="Panel2CancelValidatorPanel" CssClass="error-msg" Visible="False">
                        <p>Please enter a cancellation month</p>
                    </asp:Panel>    
                </fieldset>
            </div>

            <div id="response-preferences-container">
                <h3 class="accordion_title"><span><asp:Literal runat="server" ID="Panel2ResponseSectionHeadingLiteral" /></span></h3>
                <div class="accordion_content">
	                <fieldset class="form-block response-preferences">
	                    <asp:Literal runat="server" ID="Panel2ResponseSectionCopyLiteral" />
                        <div class="form-row highlight response-pref clearfix">
	                        <ul class="checkbox-group" data-required="Please select a method for us to contact you">
	                            <li><input type="checkbox" runat="server" ID="Panel2PreferencesHomeTelCheckBox" /><label for="<%= Panel2PreferencesHomeTelCheckBox.ClientID %>">Home phone</label></li>
	                            <li><input type="checkbox" runat="server" ID="Panel2PreferencesMobileTelCheckBox" /><label for="<%= Panel2PreferencesMobileTelCheckBox.ClientID %>">Mobile phone</label></li>
	                            <li><input type="checkbox" runat="server" ID="Panel2PreferencesEmailCheckBox" /><label for="<%= Panel2PreferencesEmailCheckBox.ClientID %>">Email</label></li>
	                            <li><input type="checkbox" runat="server" ID="Panel2PreferencesInClubCheckBox" /><label for="<%= Panel2PreferencesInClubCheckBox.ClientID %>">In club</label></li>
	                        </ul>
	                    </div>
	
	                    <div class="form-row clearfix date-time-pref">
	                        <label for="<%= Panel2PreferencesDateTimeTextBox.ClientID %>">Date/Time preference</label>
	                        <asp:TextBox runat="server" ID="Panel2PreferencesDateTimeTextBox" MaxLength="40" data-required="Please provide a date time preference" />
	                    </div>
                        <asp:Panel runat="server" ID="Panel2PreferencesValidatorPanel" CssClass="error-msg" Visible="False">
                            <p>Please choose a contact method</p>
                        </asp:Panel>
	                </fieldset>
                </div>
            </div>
            <br />
            <asp:Literal runat="server" ID="Panel2TermsSectionCopyLiteral" />
            <fieldset class="terms">
                <input type="checkbox" runat="server" ID="Panel2TermsCheckBox" class="required" data-required="Please agree to the terms and conditions" />
                <label for="<%= Panel2TermsCheckBox.ClientID %>">I acknowledge that I am bound by the membership terms and conditions that I signed on joining the club and understand that the requests above will be processed accordingly.</label>
                <asp:Panel runat="server" ID="Panel2TermsValidatorPanel" CssClass="error-msg" Visible="False">
                    <p>Please agree to the terms &amp; conditions</p>
                </asp:Panel>
            </fieldset>
            <div class="panel-footer clearfix">
                <asp:Button runat="server" ID="Panel2SubmitButton" OnClick="Panel2SubmitButtonClick" OnClientClick="return changeCircumstance.validate();" CssClass="btn-submit submit_button" Text="Next"/>
                <div class="cancel">or <a href="/change-of-circumstance">Cancel</a></div>
            </div>
        </section>

                        
        <section ID="Panel3" class="panel panel-data panel-three" runat="server" Visible="False" style="display:block">
            <h2><asp:Literal runat="server" ID="Panel3PageHeadingLiteral" /></h2>
            <p class="intro"><asp:Literal runat="server" ID="Panel3PageIntroductionLiteral" /></p>

            <aside class="steps">
                <ul>
                    <li><span><%= Translate.Text("Step 1")%></span><asp:Literal runat="server" ID="Panel3Phase1LabelLiteral" /></li>
                    <li><span><%= Translate.Text("Step 2")%></span><asp:Literal runat="server" ID="Panel3Phase2LabelLiteral" /></li>
                    <li><span><%= Translate.Text("Step 3")%></span><asp:Literal runat="server" ID="Panel3Phase3LabelLiteral" /></li>
                </ul>                           
            </aside>

            <div class="msg-wrap">
                <div class="msg">
                    <asp:Literal runat="server" ID="Panel3IntroductionLiteral" />
                </div>
                <div class="msg-b"></div>
            </div>

            <div class="form-column left">
                <div class="form-block">
        			<dl>
         				<dt class="form-label"><strong><%= Translate.Text("Swipe No.")%></strong></dt>
        			    <dd><asp:Literal runat="server" ID="Panel3SwipeNumberLiteral" /></dd>
         			</dl>                                
                </div>                                
                <div class="form-block">
                    <dl>
         				<dt class="form-label"><strong><%= Translate.Text("Title")%></strong></dt>
                        <dd><asp:Literal runat="server" ID="Panel3TitleLiteral" /></dd>
                    </dl>
                </div>
                <div class="form-block">
                    <dl>
         				<dt class="form-label"><strong><%= Translate.Text("Name")%></strong></dt>
                        <dd><asp:Literal runat="server" ID="Panel3FirstNameLiteral" /> <asp:Literal runat="server" ID="Panel3LastNameLiteral" /></dd>
                    </dl>
                </div>
                <div class="form-block">
                    <dl>
         				<dt class="form-label"><strong><%= Translate.Text("Email")%></strong></dt>
                        <dd><asp:Literal runat="server" ID="Panel3EmailLiteral" /></dd>
                    </dl>
                </div>
                <div class="form-block">
                    <dl class="multirow clearfix">
         				<dt class="form-label"><strong><%= Translate.Text("Home phone")%></strong></dt>
                        <dd><asp:Literal runat="server" ID="Panel3HomeTelLiteral" /></dd>
                    </dl>
                    <dl class="multirow clearfix">
         				<dt class="form-label"><strong><%= Translate.Text("Work phone")%></strong></dt>
         				<dd><asp:Literal runat="server" ID="Panel3WorkTelLiteral" /></dd>
                    </dl>
                    <dl class="multirow  clearfix">
         				<dt class="form-label"><strong><%= Translate.Text("Mobile phone")%></strong></dt>
         				<dd><asp:Literal runat="server" ID="Panel3MobileTelLiteral" /></dd>
                    </dl>
                </div>
            </div>
            <div class="form-column">
                <div class="form-block">
        			<dl>
         				<dt class="form-label"><strong><%= Translate.Text("Membership No.")%></strong></dt>
         				<dd><asp:Literal runat="server" ID="Panel3MembershipNumberLiteral" /></dd>
         			</dl>                                
                </div>
                <div class="form-block">
                    <dl>
         				<dt class="form-label"><strong><%= Translate.Text("Date of birth")%></strong></dt>
         				<dd><asp:Literal runat="server" ID="Panel3DateOfBirthLiteral" /></dd>
                    </dl>
                </div>                                
                <div class="form-block address">         
                    <dl class="multirow">
                        <dt class="form-label">Address</dt>
                        <dd><asp:Literal runat="server" ID="Panel3Address1Literal" /></dd>
                        <dd><asp:Literal runat="server" ID="Panel3Address2Literal" /></dd>
                        <dd><asp:Literal runat="server" ID="Panel3Address3Literal" /></dd>
                        <dd><asp:Literal runat="server" ID="Panel3Address4Literal" /></dd>
                        <dd><asp:Literal runat="server" ID="Panel3Address5Literal" /></dd>
                    </dl>
                    <dl class="multirow">
                        <dt class="form-label">Post code</dt>
                        <dd><asp:Literal runat="server" ID="Panel3PostcodeLiteral" /></dd>
                    </dl>
                </div>                     
            </div>

            <div style="clear: both;"></div>
            
            <asp:PlaceHolder runat="server" ID="Panel3ChangePlaceHolder" Visible="False">
                <h3><asp:Literal runat="server" ID="Panel3PackageSectionHeadingLiteral" /></h3>
                <div class="form-block membership-change clearfix">
                
                    <div class="current_details">
                        <dl class="clearfix multirow">
                            <dt class="form-label"><strong>Current club</strong></dt>
                            <dd><asp:Literal runat="server" ID="Panel3CurrentClubLiteral" /></dd>
                        </dl>
                        <dl class="clearfix  multirow">
						    <dt class="form-label"><strong>Current membership type</strong></dt>
						    <dd><asp:Literal runat="server" ID="Panel3CurrentPackageLiteral" /></dd>                            
                        </dl>
                    </div>
                    
                    <asp:PlaceHolder runat="server" ID="Panel3ChangeClubAndPackagePlaceHolder" Visible="False">
                        <div class="form-label">Change club and membership type</div>
                        <div class="form-options">
                            <dl class="multirow clearfix">
                                <dt>Select your new club</dt>
                                <dd class="form-label"><asp:Literal runat="server" ID="Panel3ChangeClubAndPackageClubLiteral" /></dd>
                            </dl>
                            <dl class="multirow clearfix">
                                <dt>Select a membership type</dt>
                                <dd class="form-label"><asp:Literal runat="server" ID="Panel3ChangeClubAndPackagePackageLiteral" /></dd>
                            </dl>

                        </div>
                    </asp:PlaceHolder>
                    
                    <asp:PlaceHolder runat="server" ID="Panel3ChangePackagePlaceHolder" Visible="False">
                        <div class="form-label">Change membership type only</div>
                        <div class="form-options">
                            <dl class="multirow clearfix">
                                <dt>Select a membership type</dt>
                                <dd class="form-label"><asp:Literal runat="server" ID="Panel3ChangePackagePackageLiteral" /></dd>
                            </dl>
                        </div>
                    </asp:PlaceHolder>                                 
                                                               
                </div>
                <asp:Literal runat="server" ID="Panel3PackageSectionFooterLiteral" />
            </asp:PlaceHolder>
            
            <asp:PlaceHolder runat="server" ID="Panel3FreezePlaceHolder" Visible="False">
                <h3><asp:Literal runat="server" ID="Panel3FreezeSectionHeadingLiteral" /></h3>
                <asp:Literal runat="server" ID="Panel3FreezeSectionCopyLiteral" />
                <div class="form-block">
                    I wish to put my membership on hold from <strong>1st <asp:Literal runat="server" ID="Panel3FreezeMonthLiteral" /></strong> for <strong><asp:Literal runat="server" ID="Panel3FreezeDurationLiteral" /></strong>.
                </div>
            </asp:PlaceHolder>
            
            <asp:PlaceHolder runat="server" ID="Panel3CancelPlaceHolder" Visible="False">
                <h3><asp:Literal runat="server" ID="Panel3CancelSectionHeadingLiteral" /></h3>
                <asp:Literal runat="server" ID="Panel3CancelSectionCopyLiteral" />
                <div class="form-block cancel-membership">
                    <p><strong>I wish to cancel my membership from the end of <asp:Literal runat="server" ID="Panel3CancelMonthLiteral" />.</strong></p>
                    <dl class="multirow clearfix">
                        <dt class="form-label">Reason for cancellation:</dt>
                        <dd><asp:Literal runat="server" ID="Panel3CancelReasonLiteral" /></dd>
                    </dl>
                </div>
            </asp:PlaceHolder>
            
            <asp:PlaceHolder runat="server" ID="Panel3PreferencesPlaceHolder" Visible="False">
                <h3><asp:Literal runat="server" ID="Panel3ResponseSectionHeadingLiteral" /></h3>
                <div class="form-block response-prefs">
                    <asp:Literal runat="server" ID="Panel3ResponseSectionCopyLiteral" />
                    <dl class="multirow clearfix">
                        <dt class="form-label">Your preferred contact method:</dt>
                        <dd><asp:Literal runat="server" ID="Panel3PreferencesTypeLiteral" /></dd>
                    </dl>
                    <dl class="multirow clearfix">
                        <dt class="form-label">Date / time preference:</dt>
                        <dd><asp:Literal runat="server" ID="Panel3PreferencesDateTimeLiteral" /></dd>
                    </dl>                                                    
                </div>
            </asp:PlaceHolder>

            <p><strong>I acknowledge that I am bound by the membership terms and conditions that I signed on joining the club and understand that the requests above will be processed accordingly.</strong></p>

            <asp:Literal runat="server" ID="Panel3TermsSectionCopyLiteral" />

            <div class="panel-footer clearfix"> 
                <asp:Button runat="server" ID="Panel3CancelButton" Text="Edit" CssClass="btn-edit" OnClick="Panel3CancelButtonClick"/>
                <asp:Button runat="server" ID="Panel3SubmitButton" Text="Save my details" CssClass="btn-submit submit_button" OnClick="Panel3SubmitButtonClick" OnClientClick="return changeCircumstance.saveDetails();"/>
                 <div class="cancel">or <a href="/change-of-circumstance">Cancel</a></div>
            </div>

        </section>
                       
        <section id="PanelThanks" class="panel thanks" runat="server" Visible="False" style="display:block">
            <div class="smiley">
                <h2><asp:Literal runat="server" ID="Panel4TitleLiteral" /></h2>
                <asp:Literal runat="server" ID="Panel4IntroductionLiteral" />
				<asp:HyperLink runat="server" ID="Panel4ButtonHyperLink" CssClass="btn-nrstore" />
            </div>
        </section>
    </div>
</div>
<asp:HiddenField runat="server" ID="ClubManagerHiddenField"/>
<asp:HiddenField runat="server" ID="ClubEmailHiddenField"/>