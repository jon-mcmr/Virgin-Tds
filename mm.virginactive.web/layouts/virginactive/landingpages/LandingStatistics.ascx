<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LandingStatistics.ascx.cs"
    Inherits="mm.virginactive.web.layouts.virginactive.landingpages.LandingStatistics" %>
        <%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ModuleTemplates" %>
<div class="section by_number">
    <div class="container">
        <div class="row">
            <h2 class="span12">
                <%= currentItem.Panel3Heading.Rendered %></h2>
        </div>
        <div class="row">
            <asp:Repeater runat="server" ID="rpImageItems">
                <ItemTemplate>
                    <div class="span3">
                        <img alt="500000 members" src="<%# (Container.DataItem as ImageCarouselItem).Image.MediaUrl %>">
                        <p>
                            <strong><%# (Container.DataItem as ImageCarouselItem).ImageCaption.Rendered %></strong> <%# (Container.DataItem as ImageCarouselItem).ImageQuote.Rendered %></p>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</div>
