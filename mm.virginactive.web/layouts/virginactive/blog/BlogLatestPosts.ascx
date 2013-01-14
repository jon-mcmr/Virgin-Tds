<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BlogLatestPosts.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.blog.BlogLatestPosts" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.wrappers.EviBlog" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>



<%--<div class="recommended-blogs">
	<h4 class="aside-header"><%= Translate.Text("RECOMMENDED BLOGS")%></h4>
  
	<ul>
		<li><a href="#">Fitnessista</a></li>
		<li><a href="#">The New York Times Well Blog</a></li>
		<li><a href="#">GOOD Health</a></li>
		<li><a href="#">TIME Healthland</a></li>
		<li><a href="#">Healthy Food for Living</a></li>
	</ul>
</div>--%>

<asp:Panel ID="LatstPostsPane" runat="server" CssClass="most-read-panel articles-list">
	<h4 class="aside-header"><%= Translate.Text("MOST READ POSTS")%></h4>
   <asp:ListView ID="ArticleList" runat="server">
        <LayoutTemplate>
            <asp:PlaceHolder ID="ItemPlaceholder" runat="server"></asp:PlaceHolder>
        </LayoutTemplate>

        <ItemTemplate>

	    <div class="post-summary highlight-panel">
		    <div class="article-text">
			    <h5><a href="<%# (Container.DataItem as BlogEntryItem).PageSummary.Url %>" title="<%# (Container.DataItem as BlogEntryItem).Title.Raw %>"><%# (Container.DataItem as BlogEntryItem).Title.Raw %></a></h5>
			    <p>
				    <%# (Container.DataItem as BlogEntryItem).Introduction.Text %>
			    </p>									
			    <a class="moreinfo" href="<%# (Container.DataItem as BlogEntryItem).PageSummary.Url %>" title="<%# (Container.DataItem as BlogEntryItem).Title.Raw %>"><%= Translate.Text("Read more")%></a>										
		    </div>
	    </div>

        </ItemTemplate>

   </asp:ListView>  
</asp:Panel>