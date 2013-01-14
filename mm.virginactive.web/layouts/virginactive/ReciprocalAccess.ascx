<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReciprocalAccess.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.ReciprocalAccess" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.Membership" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates" %>
<%@ Import Namespace="mm.virginactive.common.Constants.SitecoreConstants" %>
        <div id="content" class="layout">
            <div class="reciprocal">
				<div class="content-panel">
                    <h2><%= Translate.Text("Which other clubs can I access?")%></h2>
                    <%= currentItem.Introduction.Rendered%>
                </div>
				<div class="highlight-panel">
					<div>
						<h4><%= currentItem.FileImageLinkWidget.Widget.Heading.Rendered%></h4>
						<p><%= currentItem.FileImageLinkWidget.Widget.Bodytext.Rendered%></p>
						<p class="cta">
                            <a href="<%= currentItem.FileImageLinkWidget.File.MediaUrl %>" class="btn btn-download gaqTag" data-gaqcategory="PDF" data-gaqaction="Download" data-gaqlabel="Multi Club Access Guide">
							<%= currentItem.FileImageLinkWidget.Widget.Buttontext.Rendered%></a>
						</p>
					</div>
                    <%= currentItem.FileImageLinkWidget.Image.RenderCrop("180x180")%>	
				</div>
                <div class="access-content">
                    <h3><%= Translate.Text("Check access by Membership")%></h3>
                    <p><%= Translate.Text("Find out exactly which other clubs you can access by using our multi-club access checker:")%></p>
                </div>
                <!-- From to submit -->
                <div id="formToComplete" class="select-panel-wrap" runat="server">
                    <section class="select-panel memb">
                        <label><%= Translate.Text("Please enter your details")%>:</label>
                        <input type="text" id="txtDateOfBirth" maxlength="20" runat="server" class="date-of-birth"/>
                        <input type="text" id="txtMembershipNumber" maxlength="20" runat="server" class="membership-number" />
                        <a href="#" class="club-access-list btn btn-cta"><%= Translate.Text("Go")%></a>		
                    </section>
                    <div id="error-wrap" class="">
                        <div id="error-panel" class="">
                            <div id="error-message"></div>
                            <asp:RequiredFieldValidator ID="rfvDateOfBirth" runat="server" Display="Dynamic" ControlToValidate="txtDateOfBirth" EnableClientScript="True" />
                            <asp:RegularExpressionValidator ID="revDateOfBirth" runat="server" ControlToValidate="txtDateOfBirth" Display="Dynamic" EnableClientScript="True"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="rfvMembershipNumber" runat="server" Display="Dynamic" ControlToValidate="txtMembershipNumber" EnableClientScript="True" />
                            <asp:RegularExpressionValidator ID="revMembershipNumber" runat="server" ControlToValidate="txtMembershipNumber" Display="Dynamic" EnableClientScript="True"></asp:RegularExpressionValidator>
                            <asp:CustomValidator id="cvMemberNotFound" runat="server" Display="Dynamic"></asp:CustomValidator> 
                        </div>
                    </div>	
                </div>
				<div id="results" class="result-container">
                    <asp:PlaceHolder ID="resultPh" runat="server" />
                </div>
            </div>
        </div>
