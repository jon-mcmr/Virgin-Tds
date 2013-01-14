<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LandingAccreditations.ascx.cs"
    Inherits="mm.virginactive.web.layouts.virginactive.landingpages.LandingAccreditations" %>

    <%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ModuleTemplates" %>
<div class="section accreditations">
    <div class="container">
        <div class="row">
            <h2 class="span12">
                 <%= currentItem.AccreditationHeading.Rendered %></h2>
        </div>
        <div class="row">
            <asp:Repeater runat="server" ID="rpAccreditations">
                <ItemTemplate>
                    <div class="span3">
                        <img alt="<%# (Container.DataItem as ImageCarouselItem).Image.MediaItem.Alt %>" src="<%# (Container.DataItem as ImageCarouselItem).Image.MediaUrl %>">
                        <p>
                            <%# (Container.DataItem as ImageCarouselItem).ImageQuote.Rendered %></p>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</div>
