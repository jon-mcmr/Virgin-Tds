<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PersonalDetails.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.PersonalDetails" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ModuleTemplates" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.common.Constants.SitecoreConstants" %>
            <script type="text/javascript">
                var thisUrl = '<%= thisUrl %>';
            </script>

        <div id="content" class="layout-block-full">
            <div class="personal-details-inner">
                <section id="pnlIntroPanel" runat="server" class="full-panel">
                    <div class="panel-content">
                        <h2><%= currentItem.Introheading.Rendered %></h2>
                        <%= currentItem.Introtext.Rendered %>
                        <a href="" id="btn-mship" class="btn btn-cta"><%= Translate.Text("Check your details now")%></a>
                    </div>   
                           <div class="title-wrap"></div>
                            
                    <%--<div id="prize"><strong>1</strong><sup>st</sup><br />prize</div>--%>
                        <asp:ListView ID="ImageList" runat="server">
                            <LayoutTemplate>
							<ul id="carousel">
                                <asp:PlaceHolder ID="ItemPlaceholder" runat="server"></asp:PlaceHolder>
							</ul>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <li>
                                    <img src="<%# (Container.DataItem as ImageCarouselItem).Image.MediaUrl %>" width="440" height="340" alt="" data-quote="<%# (Container.DataItem as ImageCarouselItem).ImageQuote.Raw %>" class="carousel-item carousel-image" />
                                        <div class="carousel-header">
                                    	  <%# (Container.DataItem as ImageCarouselItem).ImageCaption.Raw %>
                                        </div>
                                        <div class="carousel-intro">             
                                        <%# (Container.DataItem as ImageCarouselItem).ImageQuote.Raw %>
                                        </div>
                                </li>
                            </ItemTemplate>
                        </asp:ListView>
                        <div id="carousel-header-bg">&nbsp;</div>
                </section>
                <section id="pnlPanel1" runat="server" class="panel-one panel current-panel">

                    <aside class="steps">
                            <ul>
                                <li><span><%= Translate.Text("Step 1") %></span><%= Translate.Text("Membership Search") %></li>
                                <li><span><%= Translate.Text("Step 2") %></span><%= Translate.Text("Check your details") %></li>
                                <li><span><%= Translate.Text("Step 3") %></span><%= Translate.Text("Save your details") %></li>
                            </ul>                           
                    </aside>

                     <div class="error-msg-wrap hide">
                        <div class="msg">
                            <p><%= Translate.Text("Sorry, we can’t find you on our database.")%></p>
                            <p class="sub"><%= Translate.Text("Please make sure you’ve filled in the information correctly and try again. If you still can’t get it to work, contact your Club for help.")%></p>
                        </div>
                        <div class="msg-b"></div>
                    </div>

					<div class="send-form">
						<h3><%= Translate.Text("Membership search") %></h3>
                        <p class="form-desc"><%= Translate.Text("Please fill in all the fields.")%></p>
                                
						        <%--<div class="send-form form-wrap" id="formToComplete" runat="server">
							        <div class="send-msg-header"> DO WE NEED THIS? </div>
                                </div>--%>
							    <div class="msg-personal-details">
                                     <fieldset>
									    <div class="form-wrap form-wrap-membership clearfix">
                                            <div class="form-field-wrap clearfix">
										        <div class="label-box">
											        <label for="<%=txtMembership.ClientID%>" class="form-label remove-padding"><%= Translate.Text("Swipe No.")%></label>
                                                    <a href="#" class="" title="Check out all the ... "></a>
                                                    <p class="more-info img-rep"><a href=""><span class="rep"></span><span class="visiblyhidden">?</span></a><span class="tooltip">
                                                    Your membership number / swipe number can be found on your membership card. If you have a blue or red Virgin Active card it’s the swipe number below the bar code. If you have a purple Esporta membership card, the number is on the bottom left hand side on the front.
                                                    </span></p>
											        <%--<span class="label-note">(<%= Translate.Text("if applicable")%>)</span>--%>
										        </div>
										        <div class="input-box">
											        <input type="text" data-placeholder="Please enter a membership number" data-validate-msg="Please enter a valid membership number" maxlength="20" class="form-text-field membership validation-required validate-membership" id="txtMembership" runat="server" />
										        </div>
                                             </div>
									    </div>
									    <div class="form-wrap form-wrap-dob clearfix">
                                            <div class="form-field-wrap clearfix">
										        <div class="label-box">
                                                    <label for="<%=drpDOBDay.ClientID%>" class="form-label remove-padding"><%= Translate.Text("Date of birth")%></label>
                                                </div>
                                                <div class="input-box form-wrap-dob">
                                                    <asp:dropdownlist data-placeholder="Please select your year of birth" class="text validation-required validation-group dob-year" data-validate-exception="yyyy" runat="server" id="drpDOBYear"/>
                                                    <asp:dropdownlist data-placeholder="Please select your date of birth" class="text validation-required validation-group dob-month" data-validate-exception="mm" runat="server" id="drpDOBMonth"/>
                                                    <asp:dropdownlist data-placeholder="Please select your date of birth" class="text validation-required validation-group dob-day" data-validate-exception="dd" runat="server" id="drpDOBDay"/>
									            </div>
                                            </div>
                                        </div>
                                        <div class="form-wrap form-wrap-postcode last clearfix">
                                            <div class="form-field-wrap clearfix">
                                                <div class="label-box">
											        <label for="<%=txtPostcode.ClientID%>" class="form-label"><%= Translate.Text("Postcode")%></label>
                                                </div>
										        <div class="input-box">
											        <input type="text" data-placeholder="Please enter your postcode" data-validate-msg="Please enter a valid postcode" maxlength="40" class="form-text-field input-postcode validation-required validate-postcode" id="txtPostcode" runat="server" />
										        </div>
                                            </div>
									    </div>
                                     </fieldset>
                                </div>
                            </div>
                            <div class="panel-footer clearfix">
                                <p><%= Translate.Text("The competition has now closed, but we'd still love to have your updated details. Use the form above to search our membership database for your details - then edit as appropriate.")%></p>
                                <a class="btn btn-cta-big btn-submit show-section" href='#panel-two'><%= Translate.Text("Look up my details") %></a>
                            </div>
                        </section>
                        <section id="panel-two"  class="panel panel-data">
                        <aside class="steps">
                            <ul>
                                <li><span><%= Translate.Text("Step 1") %></span><%= Translate.Text("Membership Search") %></li>
                                <li><span><%= Translate.Text("Step 2") %></span><%= Translate.Text("Check your details") %></li>
                                <li><span><%= Translate.Text("Step 3") %></span><%= Translate.Text("Save your details") %></li>
                            </ul>                           
                        </aside>

                        <div class="msg-wrap">
                            <div class="msg">
                                <p><%= Translate.Text("Thanks! We've found you.")%></p>
                            </div>
                            <div class="msg-b"></div>
                        </div>

                        <h3><%= Translate.Text("Check your details") %></h3>
                        <p><%= Translate.Text("We currently hold the following details for you. Please give them a quick check and make any tweaks if necessary. If your <strong>name or date of birth</strong> are incorrect please ") %><a href="/contact/" target="_blank"><%= Translate.Text("contact customer services") %></a>.</p>
                                <fieldset>
                                <div class="grid1">
                                    <div class="form-wrap form-wrap-membership clearfix">
                                        <div class="form-field-wrap clearfix">
                                            <div class="form-label"><strong><%= Translate.Text("Swipe No.")%></strong></div>
                                            <div class="form-label form-label-text" id="membership-text">&nbsp;</div>
                                        </div>
                                    </div>
                                   <div class="form-wrap form-wrap-name clearfix">
                                        <div class="form-field-wrap clearfix">
                                          <label class="form-label" for="<%=txtFirstName.ClientID%>"><%= Translate.Text("Name")%></label>
                                            <input type="text" maxlength="40" class="form-text-field form-text-field-name" id="txtFirstName" runat="server" />
                                            <input type="text" maxlength="40" class="form-text-field form-text-field-surname" id="txtSurname" runat="server" />
                                        </div>
                                    </div>
                                    <div class="form-wrap form-wrap-email clearfix">
                                        <div class="form-field-wrap clearfix">
		                                    <label for="<%=txtEmail.ClientID%>" class="form-label"><%= Translate.Text("Email")%></label>
		                                    <div class="input-box">
			                                    <input type="text" maxlength="40" data-placeholder="Please enter an email address" data-validate-msg="Please enter a valid email address" class="form-text-field form-text-field-email validate-email validation-contact-group" id="txtEmail" runat="server" />
                                                <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" CssClass="error-msg" EnableClientScript="False" />
                                                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" CssClass="error-msg" EnableClientScript="False" />
		                                    </div>
	                                    </div>
                                    </div>
                                    <div class="form-panel form-wrap-homephone clearfix">
                                        <div class="form-wrap mr">
                                            <div class="form-field-wrap clearfix">
	                                            <label for="<%=txtHomeNo.ClientID%>" class="form-label"><%= Translate.Text("Home phone")%></label>
	                                            <div class="input-box">
		                                            <input type="text" maxlength="40" data-placeholder="or please enter your home phone number" data-validate-msg="Please enter a valid telephone number" class="form-text-field validate-telephone form-text-field-homephone validation-contact-group" id="txtHomeNo" runat="server" />
	                                            </div>
	                                        </div>
                                        </div>
                                        <div class="form-wrap form-wrap-workphone">
                                             <div class="form-field-wrap clearfix">
		                                        <label for="<%=txtWorkNo.ClientID%>" class="form-label"><%= Translate.Text("Work phone")%></label>
		                                        <div class="input-box">
			                                        <input type="text" maxlength="40" data-placeholder="or please enter your work phone number" data-validate-msg="Please enter a valid telephone number" class="form-text-field validate-telephone form-text-field-workphone validation-contact-group" id="txtWorkNo" runat="server" />
		                                        </div>
	                                        </div>
                                        </div>
                                        <div class="form-wrap form-wrap-mobilephone">
                                            <div class="form-field-wrap clearfix">
		                                        <label for="<%=txtMobileNo.ClientID%>" class="form-label"><%= Translate.Text("Mobile phone")%></label>
		                                        <div class="input-box">
			                                        <input type="text" maxlength="40" data-placeholder="or please enter your mobile phone number" data-validate-msg="Please enter a valid telephone number" class="form-text-field form-text-field-mobile validate-telephone validation-contact-group" id="txtMobileNo" runat="server" />
		                                        </div>
	                                        </div>
                                        </div>
                                    </div>
                                 </div>
                                
                                 <div class="grid2">
                                        <div class="form-wrap form-wrap-dob">
                                            <div class="form-field-wrap clearfix">
                                                <div class="form-label"><strong><%= Translate.Text("Date of birth")%></strong></div>
                                                <div class="form-label form-label-text" id="dob-text">&nbsp;</div>
                                            </div>
                                        </div>

                                        <div class="form-panel clearfix">
                                            <div class="form-wrap form-wrap-address1">
                                                <div class="form-field-wrap clearfix">
		                                            <label for="<%=txtAddress1.ClientID%>" class="form-label"><%= Translate.Text("Address")%></label>
		                                            <div class="input-box">
			                                            <input type="text" maxlength="40" data-placeholder="Please enter an address" class="form-text-field form-text-field-address1" id="txtAddress1" runat="server" />
		                                            </div>
                                                </div>
                                            </div>

                                            <div class="form-wrap form-wrap-address2">
                                                <div class="form-field-wrap clearfix">
		                                            <label for="<%=txtAddress2.ClientID%>" class="form-label">&nbsp;</label>
		                                            <div class="input-box">
			                                            <input type="text" maxlength="40" class="form-text-field form-text-field-address2" id="txtAddress2" runat="server" />
		                                            </div>
                                                </div>
                                            </div>

                                            <div class="form-wrap form-wrap-address3">
                                                <div class="form-field-wrap clearfix">
		                                            <label for="<%=txtAddress3.ClientID%>" class="form-label">&nbsp;</label>
		                                            <div class="input-box">
			                                            <input type="text" maxlength="40" class="form-text-field form-text-field-address3" id="txtAddress3" runat="server" />
		                                            </div>
                                                </div>
                                            </div>

                                            <div class="form-wrap form-wrap-address4">
                                                <div class="form-field-wrap clearfix">
		                                            <label for="<%=txtAddress4.ClientID%>" class="form-label">&nbsp;</label>
		                                            <div class="input-box">
			                                            <input type="text" maxlength="40" class="form-text-field form-text-field-address4" id="txtAddress4" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                             <div class="form-wrap form-wrap-address5">
                                                <div class="form-field-wrap clearfix">
		                                            <label for="<%=txtAddress5.ClientID%>" class="form-label">&nbsp;</label>
		                                            <div class="input-box">
			                                            <input type="text" maxlength="40" class="form-text-field form-text-field-address5" id="txtAddress5" runat="server" />
                                                     </div>
                                                </div>
                                            </div>

                                            <div class="form-wrap form-wrap-postcode">
                                                <div class="form-field-wrap clearfix">
		                                            <label for="<%=txtPostcode2.ClientID%>" class="form-label"><%= Translate.Text("Postcode")%></label><!--Not sure if this will stay if we are collecting Postcode on first page !!-->
		                                            <div class="input-box">
			                                            <input type="text" maxlength="40" class="form-text-field form-text-field-postcode validate-postcode" data-validate-msg="Please enter a valid postcode" id="txtPostcode2" runat="server" />
		                                            </div>
                                                 </div>
	                                        </div>
                                    </div>
                                </div>
                            </fieldset>

                            <fieldset id="contact" class="contact">
                            <h4>We'd like to keep in touch</h4>
							    <div class="form-wrap">
                                    <p>We'd like to send you a monthly email packed full of news and information about your Club. From time to time we may also need to let you know about things that might affect your gym visit (pool closures, opening time changes etc).</p>
                                    <div class="form-field-wrap clearfix">
                                        <div class="input-box">
                                            <asp:CheckBox ID="chkContactByMarketing" runat="server" CssClass="checkbox-email" />
                                            <label for="<%=chkContactByMarketing.ClientID%>"><%= Translate.Text("By ticking this box you're agreeing that we can use the information above to keep in touch.")%></label>     
								        </div>
                                        <p class="note"><%= Translate.Text("You can change your preferences at any time by getting in touch with Customer Services.")%></p>  
                                    </div>
							    </div>
                            </fieldset>
                            <div class="panel-footer clearfix">                        
                                <a class="btn btn-cta-big btn-submit show-section" href='#panel-three'><%= Translate.Text("Next")%></a>
                            </div>
                        </section>
<%--                        <asp:ScriptManager ID="ScriptManager1" runat="server" />
                        <asp:UpdatePanel ID="pnlForm" UpdateMode="Conditional" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSubmit" />
                            </Triggers>		
                            <ContentTemplate> --%>
                        <section id="pnlConfirmation" class="panel panel-data panel-three" runat="server">
                        <aside class="steps">
                            <ul>
                                <li><span><%= Translate.Text("Step 1")%></span><%= Translate.Text("Membership Search")%></li>
                                <li><span><%= Translate.Text("Step 2")%></span><%= Translate.Text("Check your details")%></li>
                                <li><span><%= Translate.Text("Step 3")%></span><%= Translate.Text("Save your details")%></li>
                            </ul>                           
                        </aside>

                        <div class="msg-wrap">
                            <div class="msg">
                                <p><%= Translate.Text("Please check the following information is correct before saving.")%></p>
                            </div>
                            <div class="msg-b"></div>
                        </div>

                            <div class="grid1">
                                <div class="form-wrap form-wrap-membership clearfix">
                                    <div class="form-label"><%= Translate.Text("Swipe No.")%></div>
                                    <div class="form-value save-swipeno">&nbsp;</div>
                                </div>
                                <div class="form-wrap form-wrap-name clearfix">
                                    <div class="form-field-wrap clearfix">
                                        <div class="form-label"><%= Translate.Text("Name")%></div>
                                        <div class="form-value save-fistname">&nbsp;</div>
                                        <div class="form-value save-surname">&nbsp;</div>
                                    </div>
                                </div>
                                <div class="form-wrap form-wrap-email clearfix">
                                    <div class="form-field-wrap clearfix">
                                    <div class="form-label"><%= Translate.Text("Email")%></div>
                                    <div class="form-value save-email">&nbsp;</div>
                                    </div>
                                </div>
                                <div class="form-panel form-wrap-homephone clearfix">
                                    <div class="form-wrap mr">
                                        <div class="form-label"><%= Translate.Text("Home phone")%></div>
                                        <div class="form-value save-homephone">&nbsp;</div>
                                    </div>
                                    <div class="form-wrap form-wrap-workphone">
                                    <div class="form-label"><%= Translate.Text("Work phone")%></div>
                                        <div class="form-value save-workphone">&nbsp;</div>
                                    </div>
                                    <div class="form-wrap form-wrap-mobilephone">
                                        <div class="form-label"><%= Translate.Text("Mobile phone")%></div>
                                        <div class="form-value save-mobile">&nbsp;</div>
                                    </div>
                                </div>
                            </div>

                            <div class="grid2">
                                <div class="form-wrap form-wrap-dob">
                                    <div class="form-label"><%= Translate.Text("Date of birth")%></div>
                                    <div class="form-value save-dob">&nbsp;</div>
                                </div>

                                <div class="form-panel clearfix">
                                    <div class="form-wrap form-wrap-address1">
                                    <div class="form-label"><%= Translate.Text("Address")%></div>
                                        <div class="form-value save-address1">&nbsp;</div>
                                    </div>

                                    <div class="form-wrap form-wrap-address2">
                                    <div class="form-label">&nbsp;</div>
                                    <div class="form-value save-address2">&nbsp;</div>
                                    </div>

                                    <div class="form-wrap form-wrap-address3">
                                    <div class="form-label">&nbsp;</div>
                                    <div class="form-value save-address3">&nbsp;</div>
                                    </div>

                                    <div class="form-wrap form-wrap-address4">
                                    <div class="form-label">&nbsp;</div>
                                    <div class="form-value save-address4">&nbsp;</div>
                                    </div>

                                    <div class="form-wrap form-wrap-address5">
                                    <div class="form-label">&nbsp;</div>
                                    <div class="form-value save-address5">&nbsp;</div>
                                    </div>

                                    <div class="form-wrap form-wrap-postcode">
                                    <div class="form-label"><%= Translate.Text("Postcode")%></div>
                                    <div class="form-value save-postcode">&nbsp;</div>
                                    </div>
                                </div>
                            </div>

                            <div class="contact clearfix">
                                <h4>We'd like to keep in touch</h4>
                                 <div class="form-wrap">
                                     <p>We'd like to send you a monthly email packed full of news and information about your Club. From time to time we may also need to let you know about things that might affect your gym visit (pool closures, opening time changes etc).</p>
                                     <div class="input-box">
                                         <div class="form-value save-chk-email hide"><%= Translate.Text("You agree that we can use the information above to keep in touch.")%></div>
                                         <div class="form-value save-chk-email-none hide"><%= Translate.Text("We will not use the information above to keep in touch with you.")%></div>
                                    </div>
                                </div>
                            </div>

                            <div class="panel-footer clearfix"> 
                                <!--<p class="note"><%= Translate.Text("You can change your preferences at any time by getting in touch with Customer Services.")%></p> -->
                                <a class="btn btn-edit" href='#'><%= Translate.Text("Edit")%></a>
                                <asp:LinkButton class="btn btn-cta-big btn-save" ID="btnSubmit" runat="server" onclick="btnSubmit_Click"><%= Translate.Text("Save my details")%></asp:LinkButton>
                                <!--<a class="btn btn-cta-big btn-submit" href='#'><%= Translate.Text("Save my details")%></a>-->
                            </div>

                        </section>
                       
                    <section id="pnlThanks" class="panel thanks" runat="server">
                        <section class="full-panel">
                            <div class="panel-content">
                                <h2><%= currentItem.Thankyouheading.Rendered %></h2>
                                <%= currentItem.Thankyoutext.Rendered%>
                            </div>   
                            <p id="cta-panel"><a href="<%= currentItem.Thankyoulink.Url %>" class="btn btn-cta-big" target="_blank"><%= currentItem.Thankyoulinktext.Rendered%></a> <em><%= currentItem.Thankyoulinkdescription.Rendered%></em></p>                        
                            <img src="../../virginactive/images/membership/thanks.png" alt="5% off Virgin Holidays" />
                        </section>
                        <aside id="extra">
                            <%= currentItem.Additionalinfo.Rendered %>
                        </aside>
                    </section>
                    <input type="hidden" id="hdnRecordId" runat="server" class="recordId" />
                    <input type="hidden" id="hdnReturnedData" runat="server" class="returnedData" />
<%--                            </ContentTemplate>
                        </asp:UpdatePanel>	--%>
            </div>
        </div>
