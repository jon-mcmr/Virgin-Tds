<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SiteClubs.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.sitemap.SiteClubs" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>
<%@ Import Namespace="mm.virginactive.controls.Model" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs" %>


						<section>
							<%= HeaderIsH2? "<h2>":"<h3>" %><a href="<%= ClubFinderUrl %>"><%= Translate.Text("Clubs")%></a><%= HeaderIsH2? "</h2>":"</h3>" %>
                            <asp:ListView ID="ClubList" runat="server" ItemPlaceholderID="ClubListPh" OnItemDataBound="ClubList_OnItemDataBound">
                                <LayoutTemplate>
							    <ul>
                                <asp:PlaceHolder ID="ClubListPh" runat="server" />
                                </ul>
                                </LayoutTemplate>
                                <ItemTemplate>                                
                                    <li>
                                    <asp:Literal ID="ltrClubLink" runat="server"></asp:Literal>
                                    <%--<a href="<%# new PageSummaryItem((Container.DataItem as Club).ClubItm.InnerItem).Url %>"><%# (Container.DataItem as Club).ClubItm.Clubname.Raw %> 
                                        <%# (Container.DataItem as Club).IsClassic ?
                                            String.Format(@" <span class=""small-font"">({0} {1})</span>", Translate.Text("by"), Translate.Text("VIRGIN ACTIVE CLASSIC")) : 
                                            (Container.DataItem as Club).ClubItm.GetCrmSystem() == ClubCrmSystemTypes.ClubCentric? String.Format(@" <span class=""small-font"">({0})</span>",Translate.Text("previously ESPORTA")) : "" 
                                         %>

                                    </a>--%></li>
                                </ItemTemplate>
                            </asp:ListView>
						</section>