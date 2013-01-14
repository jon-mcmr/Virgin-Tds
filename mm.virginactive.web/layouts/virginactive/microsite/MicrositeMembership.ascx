<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MicrositeMembership.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.microsite.MicrositeMembership" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.Membership" %>
<%@ Import Namespace="Sitecore.Data.Items" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>

<div id="content">
	<div class="section news-section <%= ContextItem.GetListingCssClass() %>">
		<h1><%= ContextItem.Heading.Rendered %></h1>
		<%= ContextItem.Introduction.Rendered %>
	
	    <asp:ListView ID="MemberSectionList" runat="server">
	        <LayoutTemplate>
	            <ul class="membership_options clearfix">
	                <asp:PlaceHolder ID="ItemPlaceholder" runat="server" />
	            </ul>
	        </LayoutTemplate>
	
	        <ItemTemplate>
	                        
	            <li<%# (Container.DataItem as MembershipStaticPageItem).IsLast ? " class=\"last\"" : "" %>>
				    <h2><span><strong><%# (Container.DataItem as MembershipStaticPageItem).Price %></strong> <em><%= Translate.Text("Per<br /> Month")%></em><span> <%# (Container.DataItem as MembershipStaticPageItem).SubHeading.Rendered %></span></span></h2>
				    <div class="copy">
					    <h3><%# new PageSummaryItem( (Container.DataItem as MembershipStaticPageItem).InnerItem ).NavigationTitle.Raw %></h3>
					    <%# (Container.DataItem as MembershipStaticPageItem).Body.Rendered %>
				    </div>
	                <asp:ListView ID="FeatureList" ItemPlaceholderID="FeatureListPh" DataSource="<%# (Container.DataItem as MembershipStaticPageItem).InnerItem.Children %>" runat="server">
	                    <LayoutTemplate>
	                        <ul>
	                            <asp:PlaceHolder ID="FeatureListPh" runat="server" />
	                        </ul>
	                    </LayoutTemplate>
	
	                    <ItemTemplate>                                    
	                        <li><strong><%# new MembershipProductItem((Container.DataItem as Item)).Productname.Raw %> </strong><%# new MembershipProductItem((Container.DataItem as Item)).Productbody.Raw %></li>
	                    </ItemTemplate>
	                </asp:ListView>	
			    </li>
	        </ItemTemplate>
	    </asp:ListView>
	            	
		
		<!--Book a tour-->
		<div class="book_tour_bar clearfix">
			<%= ContextItem.Tourintro.Rendered %>
			<a href="<%= GetInterestedUrl() %>" class="btn btn-cta-big"><%= Translate.Text("Book your tour") %></a>
		</div>
					
	</div>
</div>