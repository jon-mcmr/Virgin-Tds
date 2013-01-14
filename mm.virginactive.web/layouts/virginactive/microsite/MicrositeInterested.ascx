<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MicrositeInterested.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.microsite.MicrositeInterested" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>


<div id="content" class="memberships">
		
	<div class="section" id="info">
		<h1><%= campaign.Heading.Rendered %></h1>
		<%= campaign.Bodytext.Rendered %>

            <%= campaign.Promoimage1.RenderCrop("215x139", "imgl") %>
            <%= campaign.Promoimage2.RenderCrop("215x139") %>

	</div>

    <div class="section" id="form">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
		<asp:UpdatePanel ID="pnlForm" UpdateMode="Conditional" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSubmit" />
            </Triggers>	
            <ContentTemplate> 
	            <div id="formDetails" runat="server">        
					<h2><%= campaign.Formheading.Rendered %></h2>
					<%= campaign.Formteaser.Rendered %>
					<fieldset>
	                    <span class="required">* <%= Translate.Text("All fields are required") %></span>
	
						<div class="row" data-errormsg="<%= String.Format(Translate.Text("Please enter {0}"), Translate.Text("Your name").ToLower()) %>" data-validmsg="<%= String.Format(Translate.Text("Please enter {0}"), Translate.Text("a valid name")) %>">
							<label for="<%=name.ClientID%>"><%= Translate.Text("Name") %></label>
							<input type="text" id="name" runat="server" class="text-name text" />                    
						</div>
	                     
						<div class="row" data-errormsg="<%= String.Format(Translate.Text("Please enter {0}"), Translate.Text("Your email address").ToLower()) %>" data-validmsg="<%= String.Format(Translate.Text("Please enter {0}"), Translate.Text("a valid email address")) %>">
							<label for="<%=email.ClientID%>"><%= Translate.Text("Email address") %></label>
							<input type="text" id="email" runat="server" class="text-email text" />		
						</div>
						
						<div class="row" data-errormsg="<%= String.Format(Translate.Text("Please enter {0}"), Translate.Text("Your contact number").ToLower()) %>" data-validmsg="<%= String.Format(Translate.Text("Please enter {0}"), Translate.Text("a valid contact number")) %>">
							<label for="<%=tel.ClientID%>"><%= Translate.Text("Contact number") %></label>
							<input type="text" id="tel" runat="server" class="text-number text" />			
						</div>
						<div class="row" data-errormsg="<%= Translate.Text("Please select how you heard about us") %>">
							<label for="<%=drpHowDidYouFindUs.ClientID%>"><%= Translate.Text("How did you hear about us?")%></label>
	                        <asp:DropDownList ID="drpHowDidYouFindUs" runat="server" ></asp:DropDownList>
						</div>
					</fieldset>
					<fieldset>
	                    <div class="row existing-member">
	                        <span><%= Translate.Text("Existing Virgin Active member?")%></span>
	                        <div class="row row-toValidate" data-errormsg="Please select your member status">
	                            <input type="radio" id="chkExistingMember" runat="server" name="Member" value="yes" class="radio" /><label for="<%=chkExistingMember.ClientID%>"><%= Translate.Text("Yes") %></label>   
	                            <input type="radio" id="chkNotExistingMember" runat="server" name="Member" value="no" class="radio" /><label for="<%=chkNotExistingMember.ClientID%>"><%= Translate.Text("No") %></label>	                                                   
	                        </div>
					    </div>
	                    <div class="row gaqTag" data-gaqlabel="campaign-<%=campaign.CampaignBase.Campaigncode.Rendered%>" >
	                        <asp:button class="btn-submit" id="btnSubmit" runat="server" onclick="btnSubmit_Click" />
	                        <asp:LinkButton ID="btnLink" runat="server" /><!--this is required to load __doPostBack code -->
	                    </div>
	                </fieldset>
	            </div>
	                
	            <div id="formConfirmation" runat="server" class="confirmation">
	                <h2><%= campaign.Thankyouheading.Rendered %></h2>
					<%= campaign.Thankyoubodytext.Rendered %>
	                <dl>
	                    <dt><%= Translate.Text("Name") %></dt>
	                    <dd><%=name.Value.Trim()%></dd>
	                    <dt><%= Translate.Text("Email address") %></dt>
	                    <dd><%=email.Value.Trim()%></dd>
	                    <dt><%= Translate.Text("Contact number") %></dt>
	                    <dd><%=tel.Value.Trim()%></dd>  
	                    <dt><%= Translate.Text("How did you hear about us?")%></dt>
	                    <dd class="how"><%=drpHowDidYouFindUs.SelectedValue.ToString()%></dd>
	                    <dt><%= Translate.Text("Existing Virgin Active member?")%></dt>
	                    <dd><%=chkExistingMember.Checked == true ? Translate.Text("Yes") : Translate.Text("No") %></dd>
	                </dl>
	                    <p class="follow"><%= campaign.Thankyoufooter.Rendered %></p> 
	            </div>
            </ContentTemplate>
        </asp:UpdatePanel>      
                     		
	</div>
		
</div>