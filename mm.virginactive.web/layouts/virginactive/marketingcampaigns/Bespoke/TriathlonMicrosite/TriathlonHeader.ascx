<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TriathlonHeader.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.marketingcampaigns.TriathlonHeader" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>
    <header>
        <div class="container clearfix">
            <a id="logo" href="<%= LondonTriathlonUrl %>"><img src="/va_campaigns/Bespoke/LondonTriathlonII/img/logo.png" alt="Virgin Active Health Clubs - London Triathlon"/></a>
            <section id="tagline" class="ir">
                <h1><strong>Virgin Active</strong> Health Clubs</h1>
                <p><strong>London Triathlon &bull;</strong> 2012 22&amp; 23 Sept</p>                	
            </section>
            <nav>
                <ul>
                    <asp:ListView ID="NavLinks" runat="server">
                        <LayoutTemplate>
                            <asp:PlaceHolder ID="ItemPlaceholder" runat="server" />
                        </LayoutTemplate>
                        <ItemTemplate>                        
                            <li><a href="<%# (Container.DataItem as PageSummaryItem).QualifiedUrl%>" <%# (Container.DataItem as PageSummaryItem).IsCurrent? "class=\"active\"" : ""  %>><%# (Container.DataItem as PageSummaryItem).NavigationTitle.Rendered%></a></li>                    
                        </ItemTemplate>
                    </asp:ListView>  
                 </ul>
	         </nav>
        </div>
    </header>