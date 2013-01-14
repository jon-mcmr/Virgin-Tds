<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LandingWhatNextThanks.ascx.cs"
    Inherits="mm.virginactive.web.layouts.virginactive.landingpages.LandingWhatNextThanks" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<div class="section thank_you">
    <div class="container">
        <h1 class="span12" style="color: #000000;">
            <%= currentItem.ThankYouHeading.Rendered %></h1>
        <p class="span12 subtitle" style="color: #000000;">
            <asp:Literal ID="ltSubHeading" runat="server"></asp:Literal></p>
    </div>
</div>
<div class="wrapper">
    <div class="section info_strip">
        <div class="container">
            <p>
                <%= currentItem.WhatsNextHeading.Rendered %></p>
        </div>
    </div>
</div>
