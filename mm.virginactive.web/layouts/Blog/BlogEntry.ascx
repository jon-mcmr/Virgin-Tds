<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BlogEntry.ascx.cs" Inherits="Sitecore.Modules.Blog.Layouts.BlogEntry" %>
<%@ Import Namespace="mm.virginactive.wrappers.EviBlog" %>
<%@ Import Namespace="Sitecore.Modules.Blog.Items.Blog" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.common.Helpers" %>
                    <article role="article">

		                <p class="calendar">
			                <span class="day"><sc:Date runat="server" ID="PostedDate1" Field="__created" Format="MMM" /></span>
                            <span class="date"><sc:Date runat="server" ID="PostedDate" Field="__created" Format="dd" /></span>
			                <span class="year"><sc:Date runat="server" ID="PostedDate2" Field="__created" Format="yyyy" /></span>				
		                </p>


                        <div class="article-content-wrap">
                            <header>
                                <div class="header-details">
                                    <h2> <sc:Text ID="txtTitle" Field="Title" runat="server" /></h2>
                                    <p class="article-details"><span class="article-by"><%= Translate.Text("by") %></span><span class="bold"><%=CurrentEntry.GetBlogAuthor()%></span></p>

                                	<% if (showSocial == true)
                                           {%>

                                    <ul class="social-info">              
                                        <li class="social-twitter"><span><%= SocialMediaHelper.TweetButtonForUrl(blogEntryUrl) %></span></li>
                                        <li class="social-facebook"><span><%= SocialMediaHelper.LikeButtonIFrame(blogEntryUrl)%></span></li>
                                        <li class="social-googleplus"><span><%= SocialMediaHelper.GooglePlusButton %></span></li>
                                    </ul>

                                    <%  }%>
                                </div>
                            </header>

			                <div class="article-content">
                                <p class="intro"><%=  CurrentEntry.Introduction.Rendered %></p>
                                <%= CurrentEntry.UpperContent.Text %>
                                <%= CurrentEntry.Image.RenderCrop("610x300")%>
                                <%= CurrentEntry.Content.Text  %>
                            </div>

                            <%-- Comment this out if no tag data --%>
                            <footer>
                                <div class="article-tags" id="articleTags" runat="server" visible="false">
                                    <h3><%= Translate.Text("Tags") %></h3>
                                    <ul>
                                        <asp:LoginView ID="LoginTagsView" runat="server">
                                        <AnonymousTemplate>
                                            <asp:Repeater runat="server" ID="TagList">
                                                <ItemTemplate>
                                                    <li class="tag-name">
                                                        <asp:HyperLink runat="server" ID="TagLink" NavigateUrl='<%# GetTagUrl(Container.DataItem as string) %>'>
                                                            <%# Container.DataItem %>
                                                        </asp:HyperLink>
                                                    </li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </AnonymousTemplate>
<%--                                        <LoggedInTemplate>
                                            <sc:Text ID="txtTags" Field="Tags" runat="server" />
                                        </LoggedInTemplate>--%>
                                    </asp:LoginView>
                                    </ul>
                                </div>
                            </footer>
                            <%-- Comment above out if no tag data --%>

                         </div>
        <!--
        <asp:ListView ID="ListViewCategories" runat="server">
        <LayoutTemplate>
            <div>
                <ul class="entry-categories">
                    <li>Posted in&nbsp;</li>
                    <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                </ul>
            </div>
        </LayoutTemplate>
        <ItemTemplate>
            <li>
                <asp:HyperLink ID="hyperlinkCategory" runat="server" NavigateUrl='<%# GetItemUrl(Eval("InnerItem") as Sitecore.Data.Items.Item) %>'>
                    <sc:Text ID="txtCategorie" Field="Title" runat="server" DataSource='<%# Eval("ID") %>' />
                </asp:HyperLink>
            </li>
        </ItemTemplate>
        </asp:ListView>
        -->
                </article>
                <sc:Placeholder runat="server" key="phBlogBelowEntry" />
        
        <!--
        <asp:Panel ID="CommentsPanel" runat="server"  CssClass="entry-comments">
            <h3><sc:Text ID="txtAddYourComment" Field="titleAddYourComment" runat="server" /></h3>
            <asp:validationsummary id="ValidationSummaryComments" runat="server" headertext="The following fields are not filled in:" forecolor="Red" EnableClientScript="true" CssClass="error"  />

            <asp:Panel runat="server" ID="MessagePanel" CssClass="successtext">
                <asp:Literal runat="server" ID="Message" />
            </asp:Panel>

            <asp:Label ID="lblCommentName" runat="server" Text="Name" AssociatedControlID="txtCommentName" />
            <asp:TextBox ID="txtCommentName" runat="server" CssClass="textbox" Width="220"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvCommentName" runat="server" Text="*" ErrorMessage="Username" ControlToValidate="txtCommentName" SetFocusOnError="true" EnableClientScript="true"></asp:RequiredFieldValidator>
            <br />
            <asp:Label ID="lblCommentEmail" runat="server" Text="Email" AssociatedControlID="txtCommentEmail" />
            
            <asp:TextBox ID="txtCommentEmail" runat="server" CssClass="textbox" Width="220"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvCommentEmail" runat="server" ErrorMessage="Email" Text="*" ControlToValidate="txtCommentEmail" SetFocusOnError="true" EnableClientScript="true"></asp:RequiredFieldValidator>
            <br />
            <asp:Label ID="lblCommentWebsite" runat="server" Text="Website" AssociatedControlID="txtCommentWebsite" />
            <asp:TextBox ID="txtCommentWebsite" runat="server" CssClass="textbox" Text="http://" Width="220"></asp:TextBox>
            <br />            
            <asp:Label ID="lblCommentText" runat="server" Text="Comment" AssociatedControlID="txtCommentText" />
            <asp:TextBox ID="txtCommentText" runat="server" TextMode="MultiLine" Rows="10" Columns="60"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvCommentText" runat="server" ErrorMessage="Comment" Text="*" ControlToValidate="txtCommentText" SetFocusOnError="true" EnableClientScript="true"></asp:RequiredFieldValidator>
            <sc:PlaceHolder runat="server" key="phBlogCommentForm" />
            <asp:Button ID="buttonSaveComment" runat="server" Text="Post" onclick="buttonSaveComment_Click" />
        </asp:Panel>
        -->
                   <section class="article-comments-main">
                        <asp:Panel ID="CommentList" runat="server" CssClass="comments-list">
                            <asp:ListView ID="ListViewComments" runat="server">
                                <EmptyDataTemplate>
                                </EmptyDataTemplate>
                                <LayoutTemplate>
                                    <div class="single-comment">
                                        <!-- <h3><sc:Text ID="titleComments" Field="titleComments" runat="server" /></h3> -->
                                        <!--<ul>-->
                                            <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                                        <!--</ul>-->
                                    </div>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <% if (CurrentBlog.EnableGravatar.Checked)
                                        { %>
                                    <img src="<%# GetGravatarUrl((Container.DataItem as CommentItem).Email.Rendered) %>" alt="<%#(Container.DataItem as CommentItem).Name.Rendered%>'s gravatar" width="<%= CurrentBlog.GravatarSizeNumeric %>" height="<%= CurrentBlog.GravatarSizeNumeric %>" />
                                    <% } %>
                                    <p class="user-name">
                                        <asp:HyperLink ID="hyperlinkUsername" runat="server" NavigateUrl='<%#(Container.DataItem as CommentItem).Website.Raw%>'>
                                            <%#(Container.DataItem as CommentItem).Name.Rendered%>
                                        </asp:HyperLink>
                                    </p>
                                    <p class="comment-text">
                                        <%#(Container.DataItem as CommentItem).Comment.Rendered%>
                                    </p>
                                    <% if (!CurrentBlog.ShowEmailWithinComments.Checked)
                                        { %>
                                    <span class="comment-email">
                                        <%#(Container.DataItem as CommentItem).Email.Rendered%>
                                    </span>
                                    <% } %>
                                    <div class="datetime">
                                        <%#(Container.DataItem as CommentItem).Created%>
                                    </div>

                                </ItemTemplate>
                                </asp:ListView>
                            </asp:Panel>   
                        </section>
    