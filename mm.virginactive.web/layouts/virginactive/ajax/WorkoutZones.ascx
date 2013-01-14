<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WorkoutZones.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.ajax.WorkoutZones" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.Workouts" %>
<%@ Import Namespace="mm.virginactive.controls.Model" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
                    
                    <div id="workouts-nav-wrap">                    	
                        <asp:ListView ID="CategoryList" runat="server" ItemPlaceholderID="CategoryListPh">
                            <LayoutTemplate>
                            <ul id="workouts-nav">
                                <asp:PlaceHolder ID="CategoryListPh" runat="server" />
                            </ul>
                            </LayoutTemplate>

                            <ItemTemplate>
                                <li id="<%# (Container.DataItem as WorkoutCategory).IdTag %>"><a <%# (Container.DataItem as WorkoutCategory).Guid.Equals(ContextCategory.Guid) ? @"class=""active""" : "" %> onclick="LoadWorkout('<%# (Container.DataItem as WorkoutCategory).Guid %>')"> <%#(Container.DataItem as WorkoutCategory).Name %></a></li>
                            </ItemTemplate>
                        </asp:ListView>
                           
                        <asp:ListView ID="ZoneLinkList" ItemPlaceholderID="ZoneLinkListPh" runat="server">
                            <LayoutTemplate>
                             <ul id="worksouts-sub">
                                <asp:PlaceHolder ID="ZoneLinkListPh" runat="server" />
                             </ul>
                            </LayoutTemplate>

                            <ItemTemplate>
                                <li<%# (Container.DataItem as Zone).IsFirst ? @" class=""active""" : "" %>><a href="#"><%# (Container.DataItem as Zone).ContextZone.Heading.Raw %></a></li>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                    
                    
                    <asp:ListView ID="ZoneList" ItemPlaceholderID="ZoneListPh" runat="server">
                        <LayoutTemplate>
                            <asp:PlaceHolder ID="ZoneListPh" runat="server" />
                        </LayoutTemplate>

                        <ItemTemplate>
                            <div class="workout-article">
                                <%# (Container.DataItem as Zone).ContextZone.Image.RenderCrop("220x120") %>
                                <div class="panel-content">
                        	        <h2><%# (Container.DataItem as Zone).ContextZone.Heading.Rendered %></h2>
                                    <%# (Container.DataItem as Zone).ContextZone.Body.Rendered %>
                                </div>
                                <div class="list-container">
                                    <div class="title-note">
                                        <p><%# (Container.DataItem as Zone).ContextZone.Workoutlisttitle.Rendered %></p>
                                    </div>

                                <asp:ListView ID="WorkoutList" runat="server" ItemPlaceholderID="WorkoutListPh" DataSource="<%# (Container.DataItem as Zone).Workouts %>">
                                    <LayoutTemplate>
                                        <ul class="workouts-list">
                                        <asp:PlaceHolder ID="WorkoutListPh" runat="server" />
                                        </ul>                                    
                                    </LayoutTemplate>

                                    <ItemTemplate>
                                    <li>
                                        <div class="workouts-detail">
                                            <h3><%# (Container.DataItem as WorkoutItem).Workoutname.Rendered %></h3>
                                            <p><%# (Container.DataItem as WorkoutItem).Description.Rendered %></p>
                                        </div>
                                        <%# (Container.DataItem as WorkoutItem).Filelink.Raw.Equals("") ? "" : String.Format(@" <p><a href=""{0}"" class=""btn btn-download-sm"">{1}</a></p>",(Container.DataItem as WorkoutItem).Filelink.MediaUrl,Translate.Text("Download PDF")) %>                                       
                                    </li>
                                    </ItemTemplate>

                                </asp:ListView>
                                </div>
                            </div>                      
                        </ItemTemplate>
                    </asp:ListView>

