<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Blog.ascx.cs" Inherits="Sitecore.Modules.Blog.Layouts.Blog" %>
<!--
<div class="blog-header">
    <asp:HyperLink ID="HyperlinkBlog" runat="server"><sc:Text ID="fieldtextItem" Field="Title" runat="server" /></asp:HyperLink>
</div>
-->
<div id="content" class="layout">
   
    <div id="primary-l" class="blog-wrap">
        <sc:placeholder ID="phBlogMain" key="phBlogMain" runat="server" />
    </div>

    <aside class="column-240">
        <sc:placeholder ID="phBlogSidebar" key="phBlogSidebar" runat="server" />
    </aside>
</div>
