<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MobileEnquiryForm.ascx.cs" Inherits="mm.virginactive.web.layouts.Mobile.MobileEnquiryForm" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>

			<div class="content enquiryform">
				<h1 class="icon"><%= Translate.Text("Membership Enquiry")%></h1>
				
                <asp:Panel id="clubDetails" runat="server" Visible="false">
				    <a href="tel:<%= ClubSalesNumber%>" class="button phone_number" id="aTagPhone"><span><strong><%= Translate.Text("Call us now")%> <div id="divPhone" class="rTapNumber<%= ClubResponseTapCode %>"><%= ClubSalesNumber%></div></strong></span></a>				
				    <div class="divider">
					    <div class="text">or</div>
				    </div>
                </asp:Panel>		
				<fieldset id="form_fields" data-name="ClubEnquiry">					
                    <asp:DropDownList ID="clubFindSelect" runat="server" class="required" data-required="Please choose a club"></asp:DropDownList>
                    <%--<input type="text" ID="txtClubName" runat="server" disabled="disabled" Visible="false" />--%>
					
					<input type="text" ID="txtName" runat="server" class="required" data-required="Please enter your name" placeholder="Your name" />
					
					<input type="text" ID="txtEmail" runat="server" class="required email" data-required="Please enter your email address" data-email="Please enter a valid email" data-placeholder="Email address" placeholder="Email address" />
					
					<input type="text" ID="txtPhone" runat="server" class="required phone" data-required="Please enter your contact number" data-phone="Please enter a valid contact number" data-placeholder="Contact number" placeholder="Contact number" />
					
					<div class="form_row checkbox">                        
						<input type="checkbox" name="subscribe" id="chkSubscribe" runat="server" />
						<label for="<%=chkSubscribe.ClientID%>"><%= Translate.Text("Subscribe to our newsletter")%></label>
					</div>
                    <button runat="server" class="button red" ID="btnSubmit" onclick="return virginactive.mobile.validation.validate()">
                        <span><strong><%= Translate.Text("Enquire now") %></strong></span>
                    </button>
                    <asp:LinkButton name="content_0$ctl00$btnSubmitLink" ID="btnSubmitLink" runat="server" class="btn-submit" OnClick="btnSubmit_Click" /><!--this is required to load __doPostBack code -->
					
				</fieldset>
				
			</div>
