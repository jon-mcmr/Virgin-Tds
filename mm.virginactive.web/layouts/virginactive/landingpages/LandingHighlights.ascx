<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LandingHighlights.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.landingpages.LandingHighlights" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ModuleTemplates" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<div class="section two_col_blocks">
    <div class="container">
        <div class="row">
            <h2 class="span12">
				<%= Translate.Text("Club Highlights")%></h2>
                <ul class="row">
				<asp:ListView ID="HighlightPanels" runat="server">
                    <LayoutTemplate>
                        <asp:PlaceHolder ID="ItemPlaceholder" runat="server" />
                    </LayoutTemplate> 
                    <ItemTemplate>
                        <li class="span6">
                        <div class="image">
                        <%# (Container.DataItem as HighlightPanelItem).Image.RenderCrop("200x110")%>
                        </div>
                            <h3><%# (Container.DataItem as HighlightPanelItem).Heading.Rendered%></h3>
							<p><%# (Container.DataItem as HighlightPanelItem).Bodytext.Rendered%></p>
							
                        </li>
                    </ItemTemplate>
                </asp:ListView>
                </ul>
			
        </div>
    </div>
</div>

