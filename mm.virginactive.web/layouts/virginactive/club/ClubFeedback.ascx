﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClubFeedback.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.club.ClubFeedback" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.common.Constants.SitecoreConstants" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ComponentTemplates" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs.People" %>
            	<div class="full-width-float">
                    <h2><%= feedbackItem.Header.Rendered %></h2>
                    <p><%= feedbackItem.Intro.Rendered %></p>
                
                	<h3 id="contact-manager" class="line-through"><span><%= Translate.Text("Club Manager")%></span></h3>
                    <asp:Panel class="club-staff" runat="server" Visible="false" ID="ManagerPanel">
                        <%= manager.Person.Profileimage.RenderCrop("160x160") %>
                        <div class="inner">
                            <p class="staff-name"><%= manager.Person.GetFullName() %></p>
                            <blockquote><%= manager.Person.Quote.Rendered %></blockquote>
                            <%= manager.Person.Biotext.Text %>
                        </div>
                        <div class="contact-email-social">
                            <p class="staff-name"><%= String.Format(Translate.Text("Contact {0}"),manager.Person.Firstname.Text) %></p>
                            <div class="contact-phone">
                                <span><%= manager.Managertelephone.Rendered%></span>
                            </div>
                            <div class="contact-email">
                                <span><a href="mailto:<%= manager.Manageremail.Rendered %>?subject=Feedback%20from%20Virgin%20Active%20website"><%= String.Format(Translate.Text("Email {0}"), manager.Person.Firstname.Text)%></a></span>
                            </div>
                            <div class="contact-twitter">
                                <span><a href="<%= Settings.TwitterLinkUrl %>" class="external"><%= Translate.Text("Follow @virginactiveuk") %></a></span>
                            </div>
                        </div>
                    </asp:Panel>
                    
                    <div class="send-message full-width-float">
                        <h3 class="line-through"><span><%= Translate.Text("Contact us directly")%></span></h3>
                        <div class="column-178">
                        	<p class="heading"><%= Translate.Text("Address")%></p>
                            <div class="vcard">
                                <div class="fn org"><span class="visuallyhidden"><%= Translate.Text("Virgin Active")%>: </span><%= currentClub.Clubname.Rendered%></div>
                                <div class="adr">                              
                                    <span class="street-address"><%= currentClub.Addressline1.Rendered%></span>
                                    <span class="locality"><%= currentClub.Addressline2.Rendered%></span>
                                    <span class="locality"><%= currentClub.Addressline3.Rendered%></span>
                                    <span class="region"><%= currentClub.Addressline4.Rendered%></span>
                                    <span class="postal-code"><%= currentClub.Postcode.Rendered%></span>
                                </div>
                                <span class="tel">
                                    <span class="adr-row"><span class="type"><%= Translate.Text("Sales")%></span> <span class="value"><%= currentClub.Salestelephonenumber.Rendered%></span></span>
                                    <span class="adr-row"><span class="type"><%= Translate.Text("Members")%></span> <span class="value"><%= currentClub.Memberstelephonenumber.Rendered%></span></span>
                                </span>
                            </div>
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
                                <!--Uncompleted Form-->                        
                                <div class="send-message-form form-wrap" id="formToComplete" runat="server">
                                    <div class="send-msg-header">
                                        <h4><%= Translate.Text("Feedback Form")%></h4>
                                        <span>* <%= Translate.Text("required")%></span>
                                    </div>
                            
                                    <div class="msg-personal-details">
                                        <div class="column-350 fl">
                                            <div class="form-row">
                                                <label for="<%=txtName.ClientID%>" class="form-label"><%= Translate.Text("Name")%> *</label>
                                                <div class="input-box">
                                                    <input type="text" maxlength="30" id="txtName" class="form-text-field" aria-required="true" runat="server" />
                                                    <asp:RequiredFieldValidator ID="rfvName" runat="server" Display="Dynamic" ControlToValidate="txtName" EnableClientScript="True" />
                                                    <asp:RegularExpressionValidator ID="revName" runat="server" ControlToValidate="txtName" Display="Dynamic" />
                                                </div>
                                            </div>
                                            <div class="form-row">
                                                <label for="<%=txtEmail.ClientID%>" class="form-label"><%= Translate.Text("Email address")%> *</label>
                                                <div class="input-box">
                                                    <input type="text" maxlength="40" id="txtEmail" class="form-text-field" aria-required="true" runat="server" />
                                                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" Display="Dynamic" ControlToValidate="txtEmail" EnableClientScript="True" />
                                                    <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" Display="Dynamic" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="column-350 fr">
                                            <div class="form-row">
                                                <label for="<%=txtMembership.ClientID%>" class="form-label remove-padding"><%= Translate.Text("Membership No.")%><span class="label-note">(<%= Translate.Text("if applicable")%>)</span></label>
                                                <div class="input-box">
                                                    <input type="text" maxlength="20" id="txtMembership" class="form-text-field" aria-required="true" runat="server" />
                                                </div>
                                            </div>
                                            <div class="form-row">
                                                <label for="<%=txtPhone.ClientID%>" class="form-label"><%= Translate.Text("Telephone")%> *</label>
                                                <div class="input-box">
                                                    <input type="text" maxlength="20" id="txtPhone" class="form-text-field" aria-required="true" runat="server" />
                                                    <asp:RequiredFieldValidator ID="rfvPhone" runat="server" Display="Dynamic" ControlToValidate="txtPhone" EnableClientScript="True" />
                                                    <asp:RegularExpressionValidator ID="revPhone" runat="server" ControlToValidate="txtPhone" Display="Dynamic" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                            
                                    <div class="msg-main-comment">
                                        <div class="form-row">
                                            <label for="<%=drpQueryTypeList.ClientID%>" class="form-label"><%= Translate.Text("Query type")%></label>
                                            <asp:dropdownlist class="text" runat="server" id="drpQueryTypeList"/>
                                        </div>
                                        <div class="form-row">
                                            <label for="<%=txtComments.ClientID%>"><%= Translate.Text("Your comment")%> *</label>
                                            <div class="form-row-textarea">
                                                <textarea class="msg-main-text" maxlength="500" rows="8" aria-required="true" id="txtComments" runat="server"></textarea>
                                                <asp:RequiredFieldValidator ID="rfvComments" runat="server" Display="Dynamic" ControlToValidate="txtComments" CssClass="text-error" EnableClientScript="True" />
                                            </div>
                                        </div>
                                        <div class="form-row form-row-align">
                                	        <input type="checkbox" id="chkConsentToPublish" class="checkbox" runat="server" />
                                            <label for="<%=chkConsentToPublish.ClientID%>" class="terms-text"><%= Translate.Text("We welcome your feedback and with your consent, may publish your comments on our website or in our clubs. Please tick the box if you are happy for us to do so, but leave blank if you would prefer to keep your comments private")%>.</label>
                                        </div>
                                        <div id="gaq-Feedback" class="form-row form-row-align">
                                            <asp:LinkButton ID="btnSubmit" runat="server" class="btn btn-cta-big btn-submit" 
                                                onclick="btnSubmit_Click"><%= Translate.Text("Submit")%></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                               <!--Confirmation of form submission-->
                                <div class="send-message-form" id="formCompleted" runat="server" visible="false">
							        <div class="confirmation">
                                        <p class="top"><%= Translate.Text("A team member from")%> <%= currentClub.Clubname.Rendered%> <%= Translate.Text("will call you shortly.")%></p>
		                                <div class="details">
		                                    <h3><%= Translate.Text("Details supplied")%>:</h3>
		                                    <dl>       
		                                        <dt><%= Translate.Text("Your name")%>:</dt>
		                                        <dd><%=txtName.Value.Trim()%></dd>
                                                <dt><%= Translate.Text("Email")%>:</dt>
		                                        <dd><%=txtEmail.Value.Trim()%></dd>
                                             <% if (txtMembership.Value.Trim() != "")
                                                   {%>
                                                <dt><%= Translate.Text("Membership No")%>:</dt>
		                                        <dd><%=txtMembership.Value.Trim()%></dd>
                                                <% }%>
                                                <dt><%= Translate.Text("Telephone")%>:</dt>
		                                        <dd><%=txtPhone.Value.Trim()%></dd>
                                            <% if (drpQueryTypeList.SelectedValue.ToString() != "" && drpQueryTypeList.SelectedValue.ToString() != Translate.Text("Select"))
                                                {%>
                                                <dt><%= Translate.Text("Query type")%>:</dt>
		                                        <dd><%=drpQueryTypeList.SelectedValue.ToString()%></dd>
                                                <% }%>
                                             <% if (txtComments.Value.Trim() != "")
                                                   {%>
                                                <dt><%= Translate.Text("Comment")%>:</dt>
		                                        <dd><%=txtComments.Value.Trim()%></dd>
                                                <% }%>
		                                    </dl>
		                                    <p><%=chkConsentToPublish.Checked ? Translate.Text("Thank you for letting us use your comments on our website or in our clubs") : Translate.Text("As requested, we won't publish your comments on our website or in our clubs. Thanks for your feedback.")%></p>
		                                </div>
							        </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    
                    <asp:Panel ID="TestimonialPanel" class="send-message full-width-float" runat="server">
                        <h3 class="line-through"><span><%= Translate.Text("Testimonials")%></span></h3>
                        <div class="column-178">
                        	<p><%= Translate.Text("We share feedback from our members with our response for the benefit of other members of the club")%>:</p>
                        </div>
                        <asp:ListView ID="TestimonialList" runat="server">
                            <LayoutTemplate>
                                <ul class="testimonials show-limit">
                                <asp:PlaceHolder ID="ItemPlaceholder" runat="server" />
                                </ul>
                            </LayoutTemplate>

                            <ItemTemplate>
                                <li>
                                    <p class="heading"><%# (Container.DataItem as TestimonialItem).Heading.Rendered %></p>
                                    <blockquote><%# (Container.DataItem as TestimonialItem).Quote.Rendered %></blockquote>
                            	    <p class="timestamp"><time pubdate datetime="<%# (Container.DataItem as TestimonialItem).Date.DateTime.ToString("yyyy-MM-ddTHH:mm") %>"><%# (Container.DataItem as TestimonialItem).Date.DateTime.ToString("dd MMM yyyy h:mm tt")%></time></p>
                                </li>
                            </ItemTemplate>

                        </asp:ListView>
                        
                    </asp:Panel>
                </div>