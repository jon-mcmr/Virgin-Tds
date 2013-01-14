<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClubHealthAndBeautyListing.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.ClubHealthAndBeautyListing" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs.HealthAndBeauty" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>

			<section class="width-full">	
               <!-- <h2><%= listing.PageSummary.NavigationTitle.Rendered%></h2>						-->
                <% if (SharedListing != null)
                   { %>
                <div class="panel-content">
                    <%= SharedListing.Abstract.Image.RenderCrop("280x180")%>									
                    <p class="intro"><%= SharedListing.Abstract.Subheading.Rendered%></p>
                    <%= SharedListing.Abstract.Summary.Rendered%>
					<ul class="moreinfo">
                        <%= downloadPriceList%>
                        <li><a href="#BookingForm"><%= Translate.Text("Book a Treatment now")%></a></li>
                    </ul>
                </div>			
                <div class="list-container">
                    <div class="title-note">
                        <h3><%= Translate.Text("What's available")%></h3>
                    </div>
					<section>
                        <asp:ListView ID="TreatmentList" runat="server">
                            <LayoutTemplate>
                                <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                            </LayoutTemplate>
                            <ItemTemplate>
						        <div <%# (Container.DataItem as TreatmentModuleItem).IsFirst? "class=\"treatment first\"" : "class=\"treatment\"" %>">
                                    <h3><%# (Container.DataItem as TreatmentModuleItem).Title.Rendered %></h3>
                                    <p><%# (Container.DataItem as TreatmentModuleItem).Detailcontent.Rendered %></p>
                                    <%# (Container.DataItem as TreatmentModuleItem).Isbookable.Checked ? "<p class=\"moreinfo\"><a href=\"#BookingForm\" data-treatment=\"" + (Container.DataItem as TreatmentModuleItem).Title.Rendered + "\">Book this treatment</a></p>" : ""%>
						        </div>
                            </ItemTemplate>
                        </asp:ListView>
					</section>
					<aside>
                        <placeholder runat="server" id="PriceList">
							<h3><%= Translate.Text("Price List")%></h3>
							<p>For the latest prices download our PDF Pricelist</p>
							<p class="cta"><a class="btn btn-download" target="_blank" href="<%= downloadPriceListUrl%>">Download</a></p>
							<hr />
                        </placeholder>
						<h3><%= Translate.Text("Ready to Book")%></h3>
						<p>Book online using our form or alternatively call the club on <%= listing.Healthandbeautycontactnumber.Rendered%></p>
						<p class="cta"><a class="btn btn-cta-big" href="#BookingForm">Book a Treatment</a></p>
					</aside>
                </div>  
                <% } %> 	
            </section>

                <div class="send-message full-width-float" id="BookingForm">
                    <h3 class="line-through"><span><%= Translate.Text("Book a Treatment")%></span></h3>
                    <div class="column-178"><p>Simply fill in your details below, and one of our team will call you back to book you in for your treatment.
					Alternatively if you wish to speak to a member of our Health &amp; Beauty Team:</p>
                        <div class="vcard">
                            <div class="fn org visuallyhidden"><%= Translate.Text("Virgin Active")%>: <%= Club.Clubname.Rendered%></div>
                            <div class="adr visuallyhidden">
                                <span class="street-address"><%= Club.Addressline1.Rendered%></span>
                                <span class="locality"><%= Club.Addressline2.Rendered%></span>
                                <span class="locality"><%= Club.Addressline3.Rendered%></span>
                                <span class="region"><%= Club.Addressline4.Rendered%></span>
                                <span class="postal-code"><%= Club.Postcode.Rendered%></span>
                            </div>
                            <span class="tel">
                                <span class="adr-row"><span class="type"><%= Translate.Text("Tel")%>:</span> <span class="value"><strong><%= listing.Healthandbeautycontactnumber.Rendered%></strong></span></span>
                            </span>
                        </div>
                    </div>

                    <asp:ScriptManager ID="ScriptManager1" runat="server" />
<%--                        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                            <ProgressTemplate>
                                <div id="skm_LockBackground" class="LockBackground"></div><div id="skm_LockPane" class="LockPane">
                                    <div id="skm_LockPaneText">Processing...</div>
                                </div>                              
                            </ProgressTemplate>
                        </asp:UpdateProgress>--%>
                    <asp:UpdatePanel ID="pnlForm" UpdateMode="Conditional" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSubmit" />
                        </Triggers>
                        <ContentTemplate>
                            <!--Uncompleted Form-->
                            <div class="send-message-form form-wrap" id="formToComplete" runat="server">
                                <div class="send-msg-header">
                                    <h4><%= Translate.Text("Booking Form")%></h4>
                                    <span>* <%= Translate.Text("required")%></span>
                                </div>
                                <div class="msg-personal-details">
                                    <div class="column-350 fl">
                                        <div class="form-row">
                                            <label for="<%=txtName.ClientID%>" class="form-label"><%= Translate.Text("Name")%> *</label>
                                            <div class="input-box">
                                                <input type="text" id="txtName" maxlength="30" class="form-text-field" aria-required="true" runat="server"/>
                                                <asp:RequiredFieldValidator ID="rfvName" runat="server" Display="Dynamic" ControlToValidate="txtName" EnableClientScript="True" />
                                                <asp:RegularExpressionValidator ID="revName" runat="server" ControlToValidate="txtName" Display="Dynamic" />
                                            </div>
                                        </div>
                                        <div class="form-row">
                                            <label for="<%=txtEmail.ClientID%>" class="form-label"><%= Translate.Text("Email address")%> *</label>
                                            <div class="input-box">
                                                <input type="text" id="txtEmail" maxlength="40" class="form-text-field" aria-required="true" runat="server"/>
                                                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" Display="Dynamic" ControlToValidate="txtEmail" EnableClientScript="True" />
                                                <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" Display="Dynamic" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="column-350 fr">
                                        <div class="form-row">
                                            <label for="<%=txtMembership.ClientID%>" class="form-label remove-padding"><%= Translate.Text("Membership No.")%><span class="label-note">(if applicable)</span></label>
                                            <div class="input-box">
                                                <input type="text" id="txtMembership" maxlength="20" class="form-text-field" aria-required="true" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-row">
                                            <label for="<%=txtPhone.ClientID%>" class="form-label"><%= Translate.Text("Telephone")%> *</label>
                                            <div class="input-box">
                                                <input type="text" id="txtPhone" maxlength="20" class="form-text-field" aria-required="true" runat="server"/>
                                                <asp:RequiredFieldValidator ID="rfvPhone" runat="server" Display="Dynamic" ControlToValidate="txtPhone" EnableClientScript="True" />
                                                <asp:RegularExpressionValidator ID="revPhone" runat="server" ControlToValidate="txtPhone" Display="Dynamic" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="treatment-details">
                                    <div class="form-row treatment-list">
                                        <label for="<%=drpTreatment.ClientID%>" class="form-label"><%= Translate.Text("What treatment would you like")%>?</label>
                                        <asp:dropdownlist id="drpTreatment" runat="server" class="text treatmentSelect"></asp:dropdownlist>
                                    </div>
							        <div class="column-350 fl">
                                        <div class="form-row">
                                            <label for="<%=txtPreferredDay.ClientID%>" class="form-label"><%= Translate.Text("Which day is best for you")%>?</label>
                                            <input type="text" id="txtPreferredDay" class="form-text-field datepicker" placeholder="dd/mm/yyyy" aria-required="true" runat="server"/>
                                        </div>
							        </div>
							        <div class="column-350 fr">
                                        <div class="form-row">
                                            <label for="<%=drpPreferredTime.ClientID%>" class="form-label"><%= Translate.Text("What time")%>?</label>
                                            <asp:dropdownlist id="drpPreferredTime" runat="server" class="text"></asp:dropdownlist>
                                        </div>
							        </div>
						        </div>
						        <div class="msg-main-comment">
                                    <div class="form-row">
                                        <label for="<%=txtComments.ClientID%>"><%= Translate.Text("Comments")%></label>
                                        <textarea class="msg-main-text" maxlength="500" rows="8" aria-required="true" runat="server" id="txtComments"></textarea>
                                        <%--<asp:RequiredFieldValidator ID="rfvComments" runat="server" Display="Dynamic" ControlToValidate="txtComments" CssClass="error-msg" EnableClientScript="True" />--%>
                                    </div>
                                    <div id="gaq-HealthAndBeauty" class="form-row form-row-align">
                                        <asp:LinkButton ID="btnSubmit" runat="server" class="btn btn-cta-big btn-submit" 
                                            onclick="btnSubmit_Click"><%= Translate.Text("Submit")%></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <!--Confirmation of form submission-->
                            <div class="send-message-form" id="formCompleted" runat="server" visible="false">
							    <div class="confirmation">
									<p class="top">A team member from <%=club.Clubname.Rendered%> will call you shortly.</p>
		                            <div class="details">
		                                <h3>Details supplied:</h3>
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
                                            <% if (drpTreatment.SelectedValue.ToString() != "" && drpTreatment.SelectedValue.ToString() != Translate.Text("Select"))
                                                {%>
                                                <dt><%= Translate.Text("Treatment")%>:</dt>
		                                        <dd><%=drpTreatment.SelectedValue.ToString()%></dd>
                                                <% }%>
                                             <% if (txtPreferredDay.Value.Trim() != "" && txtPreferredDay.Value.Trim() != Translate.Text("dd/mm/yyyy"))
                                                   {%>
                                                <dt><%= Translate.Text("Day")%>:</dt>
		                                        <dd><%=txtPreferredDay.Value.Trim()%></dd>
                                                <% }%>
                                            <% if (drpPreferredTime.SelectedValue.ToString() != "" && drpPreferredTime.SelectedValue.ToString() != Translate.Text("Select"))
                                                {%>
                                                <dt><%= Translate.Text("Time")%>:</dt>
		                                        <dd><%=drpPreferredTime.SelectedValue.ToString()%></dd>
                                                <% }%>
                                             <% if (txtComments.Value.Trim() != "")
                                                   {%>
                                            <dt><%= Translate.Text("Comment")%>:</dt>
		                                    <dd><%=txtComments.Value.Trim()%></dd>
                                                <% }%>
		                                </dl>
		                            </div>
							    </div>
                            </div>
                            </ContentTemplate>
                        </asp:UpdatePanel> 
                </div>
