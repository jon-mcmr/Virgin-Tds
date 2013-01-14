<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContactForm.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.ContactForm" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ModuleTemplates" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.common.Constants.SitecoreConstants" %>

                 <div id="content" class="layout-block">									
					<div class="contact-panel contact-panel-club full-width-float">
						<h2><%= currentItem.Pageheader.Rendered %></h2>                        						
                        <div class="contact-intro">
							<p class="intro">
								<strong><%= currentItem.Subheading.Rendered %></strong>
							</p>
							<p>
								<%= currentItem.Leader.Rendered %>
							</p>
						    <div class="msg-select selectParentWrap">
                                <div class="form-row wrap">									
                                    <label for="<%=clubFindSelect_Top.ClientID%>"><%= Translate.Text("Your club is")%>: </label>
                                    <select class="chzn-clublist update-inputs club-select-top" id="clubFindSelect_Top" runat="server"></select>
                                    <asp:CustomValidator ID="cvClubFindSelect_Top" runat="server" ControlToValidate="clubFindSelect_Top" ClientValidationFunction="clubFindSelect_Top_Validate"></asp:CustomValidator>
                                </div>
                            </div>
						</div>
                        <div class="highlight-panel">
                        	<div id="results" class="result-container">
                                <asp:PlaceHolder ID="resultPh" runat="server" />
                            </div>                        
                        </div>
					</div>
					<div class="change-of-circ full-width-float">
						<h3 class="line-through"><span><%= currentItem.ModuleHeading.Rendered %></span></h3>
						<div class="inner-content">
							<p><strong><%= currentItem.ModuleSubHeading.Rendered %></strong></p>
							<%= currentItem.ModuleCopy.Rendered %>

						    <sc:Link runat="server" ID="ModuleLink" CssClass="btn btn-cta-big" Field="Module Link" />
						</div>
					</div>

					<div class="faqs-panel full-width-float">
						<h3 class="line-through"><span><%= Translate.Text("FAQs")%></span></h3>
                        
                        <%= currentItem.FAQbody.Rendered %>
						
                        <asp:ListView ID="FaqList" runat="server">
                        
                        <LayoutTemplate>
						<ul class="faqs-list accordion-list">
                            <asp:placeholder ID="ItemPlaceholder" runat="server" />
						</ul>
                        </LayoutTemplate>

                        <ItemTemplate>
                        <li class="accordion-item">
							<h5 class="closed"><a href="#"><%# (Container.DataItem as FAQItem).Question.Rendered  %></a></h5>
							<div class="accordion-body">
								<%# (Container.DataItem as FAQItem).Answer.Rendered  %>
							</div>
                        </li>
                        </ItemTemplate>

                        </asp:ListView>
                    </div>
                    <div class="contact-panel full-width-float">
						<h3 class="line-through"><span><%= currentItem.Secondsectionheader.Rendered%></span></h3>
						<div class="contact-intro">
                            <%= currentItem.Secondsectionbody.Rendered%>
						</div>
                        <div class="contact-numbers">
	                        <div class="contact-address">
		                        <h5><%= Translate.Text("Customer Services") %></h5>
		                        <p>
                                <%= ContactAddress %>
		                        </p>
	                        </div>
	                        <div class="contact-email-social">
		                        <div class="contact-phone">
			                        <span><%= currentItem.Contacttelephone.Rendered %></span>
		                        </div>
		                        <div class="contact-email">
			                        <span><a href="mailto:<%= currentItem.Contactemail.Rendered %>?subject=Feedback%20from%20Virgin%20Active%20website" class="external" target="_blank"><%= Translate.Text("Email Customer Services") %></a></span>
		                        </div>
		                        <div class="contact-twitter">
			                        <span><a href="<%= Settings.TwitterLinkUrl %>" class="external" target="_blank"><%= Translate.Text("Follow @virginactiveuk") %></a></span>
		                        </div>
		                        <div class="contact-fb">
                                    <span><a href="<%= Settings.FacebookLinkUrl %>" class="external" target="_blank"><%= Translate.Text("Join our Facebook page") %></a></span>
		                        </div>
	                        </div>
                        </div>
					</div>
                    <a name="form"></a>
					<div class="send-message full-width-float">
						<h3 class="line-through" ><span><%= Translate.Text("Send your club a message") %></span></h3>
						<div class="column-160">
							<%= currentItem.Messagebody.Rendered %>
						</div>
                        <asp:ScriptManager ID="ScriptManager1" runat="server" />
<%--                        <ProgressTemplate>
                            <div id="skm_LockBackground" class="LockBackground">
                            </div>
                            <div id="skm_LockPane" class="LockPane">
                                <div id="skm_LockPaneText">
                                    <span><img id="imgProgress" alt="Processing Image" src="/virginactive/Images/indicator-big.gif" class="ProgressImage" />
                                        <span>Loading...</span>
                                    </span>
                                </div>
                            </div>
                        </ProgressTemplate>--%>
                        <asp:UpdatePanel ID="pnlForm" UpdateMode="Conditional" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSubmit" />
                            </Triggers>
                            <ContentTemplate>
                            <a name="sendmessage-jump"></a>
                                <!--Uncompleted Form-->    						
						        <div class="send-message-form form-wrap form-to-complete" id="formToComplete" runat="server">
							        <div class="send-msg-header">
								        <h4 class="fl"><%= Translate.Text("Contact Form")%></h4>
								        <span class="fr">* <%= Translate.Text("required")%></span>
							        </div>
							
							        <div class="msg-personal-details">
								        <div class="column-350 fl">
									        <div class="form-row">
											    <label for="<%=txtName.ClientID%>" class="form-label"><%= Translate.Text("Name")%> <sup>*</sup></label>
										        <div class="input-box">
											        <input type="text" maxlength="30" class="form-text-field" id="txtName" runat="server"/>
                                                    <asp:RequiredFieldValidator ID="rfvName" runat="server" Display="Dynamic" ControlToValidate="txtName" EnableClientScript="True" CssClass="error-msg" />
                                                    <asp:RegularExpressionValidator ID="revName" runat="server" ControlToValidate="txtName" Display="Dynamic" CssClass="error-msg" />
										        </div>
									        </div>
									
									        <div class="form-row">
											    <label for="<%=txtEmail.ClientID%>" class="form-label"><%= Translate.Text("Email address")%> <sup>*</sup></label>
										        <div class="input-box">
											        <input type="text" maxlength="40" class="form-text-field" id="txtEmail" runat="server" />
                                                    <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" Display="Dynamic" CssClass="error-msg" EnableClientScript="True" />
                                                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" Display="Dynamic" ControlToValidate="txtEmail" CssClass="error-msg" EnableClientScript="True" />
										        </div>
									        </div>
								        </div>
								
								        <div class="column-350 fr">
									        <div class="form-row">
										        <div class="label-box">
											        <label for="<%=txtMembership.ClientID%>" class="form-label remove-padding"><%= Translate.Text("Membership No.")%></label>
											        <span class="label-note">(<%= Translate.Text("if applicable")%>)</span>
										        </div>
										        <div class="input-box">
											        <input type="text" maxlength="20" class="form-text-field" id="txtMembership" runat="server" />
										        </div>
									        </div>
									
									        <div class="form-row">
											    <label class="form-label" for="<%=txtPhone.ClientID%>"><%= Translate.Text("Telephone")%> <sup>*</sup></label>
										        <div class="input-box">
											        <input type="text" maxlength="20" class="form-text-field" id="txtPhone" runat="server" />
                                                    <asp:RequiredFieldValidator ID="rfvPhone" runat="server" Display="Dynamic" ControlToValidate="txtPhone" CssClass="error-msg" EnableClientScript="True" />
                                                    <asp:RegularExpressionValidator ID="revPhone" runat="server" ControlToValidate="txtPhone" Display="Dynamic" CssClass="error-msg" EnableClientScript="True" />
										        </div>
									        </div>
								        </div>
							        </div>
						            <div class="msg-select selectParentWrap row-club-select-bottom">
                                        <div class="form-row wrap">
                                            <label for="<%=clubFindSelect.ClientID%>"><%= Translate.Text("Which club is your message related to?")%> <sup>*</sup></label>
                                            <!-- <input type="text" placeholder="Find your club" class="xxfindclubenq chzn-clublist searchclubs" id="txtFindClub" runat="server" />-->
                                            <%--<select class="chzn-clublist update-inputs club-select-bottom" id="clubFindSelect" runat="server"></select>--%>
                                            <asp:dropdownlist class="chzn-clublist update-inputs club-select-bottom" id="clubFindSelect" runat="server"></asp:dropdownlist>	
											<input type="hidden" id="txtClubGUID" class="clubGUID" runat="server" />
                                            <asp:RequiredFieldValidator CssClass="error-club" ID="rfvClubName" runat="server" Display="Dynamic" ControlToValidate="clubFindSelect" EnableClientScript="True" Enanaled />
                                            <asp:RegularExpressionValidator CssClass="error-club"  ID="revClubName" runat="server" ControlToValidate="clubFindSelect" Display="Dynamic" />
                                            <asp:CustomValidator ID="cvClubName" runat="server" Display="Dynamic" />
                                        </div>
                                    </div>
							        <div class="msg-main-comment">
                                        <div class="form-row">
                                            <label for="<%=drpQueryTypeList.ClientID%>" class="form-label"><%= Translate.Text("Query type")%></label>
                                            <asp:dropdownlist class="text" runat="server" id="drpQueryTypeList"/>
                                        </div>
                                        <div class="form-row">
								            <label for="<%=txtComments.ClientID%>"><%= Translate.Text("Your comment")%> <sup>*</sup></label>
								            <div class="msg-comment-box">
									            <textarea class="msg-main-text" maxlength="500" rows="8" id="txtComments" runat="server"></textarea>
									            <asp:RequiredFieldValidator ID="rfvComments" runat="server" Display="Dynamic" ControlToValidate="txtComments" CssClass="error-msg" EnableClientScript="True" />
								            </div>
                                        </div>
                                        <div class="form-row form-row-align">
                                	        <input type="checkbox" id="chkConsentToPublish" class="checkbox" runat="server" />
                                            <label for="<%=chkConsentToPublish.ClientID%>" class="terms-text"><%= Translate.Text("We welcome your feedback and with your consent, may publish your comments on our website or in our clubs. Please tick the box if you are happy for us to do so, but leave blank if you would prefer to keep your comments private")%>.</label>
                                        </div>
                                        <div id="gaq-Contact" class="form-row form-row-align">
                                            <asp:LinkButton ID="btnSubmit" runat="server" class="btn btn-cta-big btn-submit" 
                                                 onclick="btnSubmit_Click"><%= Translate.Text("Submit")%></asp:LinkButton>
                                        </div>
							        </div>
						        </div>
                               <!--Confirmation of form submission-->
                                <div class="send-message-form form-completed" id="formCompleted" runat="server" visible="false">
							        <div class="confirmation">
		                                <p class="top"><%= Translate.Text("A team member from Virgin Active will call you shortly.")%> </p>
		                                <div class="details">
		                                    <h3><%= Translate.Text("Details supplied")%>:</h3>
		                                    <dl>       
		                                        <dt><%= Translate.Text("Your name")%>:</dt>
		                                        <dd><%=txtName.Value.Trim()%></dd>
                                                <dt><%= Translate.Text("Email")%>:</dt>
		                                        <dd><%=txtEmail.Value.Trim()%></dd>
                                             <% if (txtMembership.Value.Trim() != "")
                                                   {%>
                                                <dt><%= Translate.Text("Membership No.")%>:</dt>
		                                        <dd><%=txtMembership.Value.Trim()%></dd>
                                            <% }%>
                                                <dt><%= Translate.Text("Telephone")%>:</dt>
		                                        <dd><%=txtPhone.Value.Trim()%></dd>
                                            <% if (drpQueryTypeList.SelectedValue.ToString() != "" && drpQueryTypeList.SelectedValue.ToString() != Translate.Text("Select"))
                                                {%>
                                                <dt><%= Translate.Text("Query type")%>:</dt>
		                                        <dd><%=drpQueryTypeList.SelectedValue.ToString()%></dd>
                                            <% }%>
                                                <dt><%= Translate.Text("Your club")%>:</dt>
                                                <dd><%= CurrentClub.Clubname.Rendered%></dd>
                                                <dt><%= Translate.Text("Comment")%>:</dt>
		                                        <dd><%=txtComments.Value.Trim()%></dd>
		                                    </dl>
                                            <p><%=chkConsentToPublish.Checked ? Translate.Text("Thank you for letting us use your comments on our website or in our clubs") : Translate.Text("As requested, we won't publish your comments on our website or in our clubs. Thanks for your feedback.")%></p>
		                                </div>
							        </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
					</div>
                </div> <!-- /content -->

					
