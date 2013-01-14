<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClubPeopleListing.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.club.ClubPeopleListing" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
                <div class="people-list-wrap">
                  	<h2>Personal Trainers</h2>
                    <div id="people-filter" class="filter-tabs">
				        <ul>
                            <li><a href="#" class="show_all active"><%= Translate.Text("All") %></a></li>
                            
                            <asp:ListView ID="AlphaFilter" runat="server" ItemPlaceholderID="AlphaPh">
                            <LayoutTemplate>
                                <asp:PlaceHolder ID="AlphaPh" runat="server" />
                            </LayoutTemplate>

                            <ItemTemplate>
                                <%# (People.FindAll(delegate(PersonItem p) { return p.StartsWith(Container.DataItem as string); }).Count > 0) ?
                                                                        String.Format(@"<li><a href=""#"" class=""show_{1}"">{0}</a></li>", (Container.DataItem as string).ToUpper(), (Container.DataItem as string)) 
                                : 
                                String.Format(@"<li class=""inactive"">{0}</li>", (Container.DataItem as string).ToUpper())  %>
                            </ItemTemplate>

                            </asp:ListView> 
					    </ul>
					</div>                                

                    <asp:ListView ID="PeopleList" runat="server">
                        <LayoutTemplate>
					    <ul class="people-list">
                            <asp:PlaceHolder ID="ItemPlaceholder" runat="server" />
                        </ul>
                        </LayoutTemplate>

                        <ItemTemplate>
							<li class="people people_<%# (Container.DataItem as PersonItem).Firstname.Raw.Substring(0,1).ToLower() %>">                                                           
                                <%# (Container.DataItem as PersonItem).Profileimage.RenderCrop("80x80") %>
								<p class="moreinfo"><a href="<%# new PageSummaryItem((Container.DataItem as PersonItem).InnerItem).Url %>"><%# String.Format(Translate.Text("See {0}'s profile"),(Container.DataItem as PersonItem).Firstname.Raw)  %></a></p>
								<h3><a href="<%# new PageSummaryItem((Container.DataItem as PersonItem).InnerItem).Url %>"><%# (Container.DataItem as PersonItem).GetFullName() %></a></h3>
								<p><%# (Container.DataItem as PersonItem).Title.Raw %></p>
							</li>
                        </ItemTemplate>
                    </asp:ListView>
                   
                </div> <!-- / people-wrap-->
    