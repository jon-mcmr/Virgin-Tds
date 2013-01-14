<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LandingWhatNextThingsToDiscuss.ascx.cs"
    Inherits="mm.virginactive.web.layouts.virginactive.landingpages.LandingWhatNextThingsToDiscuss" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ModuleTemplates" %>
<div class="section two_col_blocks">
    <div class="container">
        <div class="row">
            <h2 class="span12">
                <%= currentItem.TalkToUsHeading.Rendered %>
            </h2>
        </div>
        <ul class="row">
            <asp:Repeater runat="server" ID="rpDiscuss">
                <ItemTemplate>
                    <li class="span6">
                        <div class="image">
                            <img width="200" height="110" src="<%# (Container.DataItem as HighlightPanelItem).Image.MediaUrl %>" />
                        </div>
                        <h3>
                            <%# (Container.DataItem as HighlightPanelItem).Heading.Rendered%></h3>
                        <p>
                            <%# (Container.DataItem as HighlightPanelItem).Bodytext.Rendered%></p>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
</div>
